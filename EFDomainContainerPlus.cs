using Grammophone.DataAccess.EntityFramework;

namespace Grammophone.DataAccess.EntityFramework.Plus
{
	/// <summary>
	/// Entity Framework domain container with Entity Framework Plus set-operation support.
	/// </summary>
	public abstract class EFDomainContainerPlus : EFDomainContainer
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		protected EFDomainContainerPlus()
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="transactionMode">The transaction behavior.</param>
		protected EFDomainContainerPlus(TransactionMode transactionMode)
			: base(transactionMode)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="nameOrConnectionString">The database name or connection string.</param>
		protected EFDomainContainerPlus(string nameOrConnectionString)
			: base(nameOrConnectionString)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="nameOrConnectionString">The database name or connection string.</param>
		/// <param name="transactionMode">The transaction behavior.</param>
		protected EFDomainContainerPlus(string nameOrConnectionString, TransactionMode transactionMode)
			: base(nameOrConnectionString, transactionMode)
		{
		}

		#endregion

		#region Public methods

		/// <inheritdoc/>
		public override QueryTranslator TryGetQueryTranslator()
		{
			return EFQueryTranslatorPlusFactory.GetQueryTranslator();
		}

		#endregion
	}
}
