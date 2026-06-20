using Grammophone.DataAccess.EntityFramework;

namespace Grammophone.DataAccess.EntityFramework.Plus
{
	/// <summary>
	/// Factory for the Entity Framework query translator with Entity Framework Plus set operations.
	/// </summary>
	public static class EFQueryTranslatorPlusFactory
	{
		#region Private fields

		private static readonly QueryTranslator QueryTranslator = new QueryTranslator(
			new EFTerminalMethodsAdapter(),
			new EFShapingMethodsAdapter(),
			new EFPlusSetOperationMethodsAdapter(),
			EFQueryTranslatorFactory.GetQueryTranslator().MethodMappingsByMethodInfo);

		#endregion

		#region Public methods

		/// <summary>
		/// Get the shared Entity Framework Plus query translator.
		/// </summary>
		/// <returns>Returns the shared Entity Framework Plus query translator.</returns>
		public static QueryTranslator GetQueryTranslator()
		{
			return QueryTranslator;
		}

		#endregion
	}
}
