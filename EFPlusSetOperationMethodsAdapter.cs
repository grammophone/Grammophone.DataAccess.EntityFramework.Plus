using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Grammophone.DataAccess.QueryExtensions;
using Z.EntityFramework.Plus;

namespace Grammophone.DataAccess.EntityFramework.Plus
{
	/// <summary>
	/// Entity Framework Plus implementation of set-based terminal mutation methods.
	/// </summary>
	public class EFPlusSetOperationMethodsAdapter : SetOperationMethodsAdapter
	{
		#region Public methods

		/// <inheritdoc/>
		/// <remarks>
		/// Delegates to Entity Framework Plus <c>DeleteFromQuery</c>, which executes a set-based delete without materializing
		/// entities and without synchronizing already tracked entities.
		/// </remarks>
		public override int ExecuteDelete<T>(IQueryable<T> nativeQuery)
		{
			if (nativeQuery == null) throw new ArgumentNullException(nameof(nativeQuery));

			return nativeQuery.DeleteFromQuery();
		}

		/// <inheritdoc/>
		/// <remarks>
		/// Delegates to Entity Framework Plus <c>DeleteFromQueryAsync</c>, which executes a set-based delete without materializing
		/// entities and without synchronizing already tracked entities.
		/// </remarks>
		public override Task<int> ExecuteDeleteAsync<T>(
			IQueryable<T> nativeQuery,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (nativeQuery == null) throw new ArgumentNullException(nameof(nativeQuery));

			return nativeQuery.DeleteFromQueryAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public override int ExecuteUpdate<T>(
			IQueryable<T> nativeQuery,
			Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
		{
			if (nativeQuery == null) throw new ArgumentNullException(nameof(nativeQuery));
			if (setPropertyCalls == null) throw new ArgumentNullException(nameof(setPropertyCalls));

			return nativeQuery.UpdateFromQuery(EFPlusUpdateExpressionTranslator.Translate(setPropertyCalls));
		}

		/// <inheritdoc/>
		public override Task<int> ExecuteUpdateAsync<T>(
			IQueryable<T> nativeQuery,
			Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (nativeQuery == null) throw new ArgumentNullException(nameof(nativeQuery));
			if (setPropertyCalls == null) throw new ArgumentNullException(nameof(setPropertyCalls));

			return nativeQuery.UpdateFromQueryAsync(EFPlusUpdateExpressionTranslator.Translate(setPropertyCalls), cancellationToken);
		}

		#endregion
	}
}
