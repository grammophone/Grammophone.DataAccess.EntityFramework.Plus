using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Grammophone.DataAccess.QueryExtensions;

namespace Grammophone.DataAccess.EntityFramework.Plus
{
	/// <summary>
	/// Translates portable set property calls to Entity Framework Plus update expressions.
	/// </summary>
	internal static class EFPlusUpdateExpressionTranslator
	{
		#region Public methods

		/// <summary>
		/// Translate portable set property calls.
		/// </summary>
		public static Expression<Func<T, T>> Translate<T>(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> expression)
		{
			if (expression == null) throw new ArgumentNullException(nameof(expression));

			var targetParameter = Expression.Parameter(typeof(T), "entity");
			var bindings = new List<MemberBinding>();

			CollectBindings(expression.Body, targetParameter, bindings);

			return Expression.Lambda<Func<T, T>>(
				Expression.MemberInit(Expression.New(typeof(T)), bindings),
				targetParameter);
		}

		#endregion

		#region Private methods

		private static void CollectBindings(Expression expression, ParameterExpression targetParameter, ICollection<MemberBinding> bindings)
		{
			if (!(expression is MethodCallExpression methodCall))
			{
				throw new NotSupportedException("ExecuteUpdate expressions must contain a chain of SetProperty calls.");
			}

			if (methodCall.Arguments[0] is MethodCallExpression previousCall)
			{
				CollectBindings(previousCall, targetParameter, bindings);
			}

			var propertyLambda = (LambdaExpression)methodCall.Arguments[1];
			var memberExpression = GetMemberExpression(propertyLambda.Body);

			Expression valueExpression;

			if (methodCall.Arguments[2] is LambdaExpression valueLambda)
			{
				valueExpression = new ParameterReplacingVisitor(valueLambda.Parameters[0], targetParameter).Visit(valueLambda.Body);
			}
			else
			{
				valueExpression = methodCall.Arguments[2];
			}

			bindings.Add(Expression.Bind(memberExpression.Member, valueExpression));
		}

		private static MemberExpression GetMemberExpression(Expression expression)
		{
			if (expression is MemberExpression memberExpression)
			{
				return memberExpression;
			}

			throw new NotSupportedException("SetProperty requires a direct member access property expression.");
		}

		#endregion

		#region Private types

		private sealed class ParameterReplacingVisitor : ExpressionVisitor
		{
			private readonly ParameterExpression sourceParameter;

			private readonly ParameterExpression targetParameter;

			public ParameterReplacingVisitor(ParameterExpression sourceParameter, ParameterExpression targetParameter)
			{
				this.sourceParameter = sourceParameter;
				this.targetParameter = targetParameter;
			}

			protected override Expression VisitParameter(ParameterExpression node)
			{
				return node == sourceParameter ? targetParameter : base.VisitParameter(node);
			}
		}

		#endregion
	}
}
