using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
		public override int ExecuteDelete<T>(IQueryable<T> nativeQuery)
		{
			if (nativeQuery == null) throw new ArgumentNullException(nameof(nativeQuery));

			return nativeQuery.DeleteFromQuery();
		}

		/// <inheritdoc/>
		public override Task<int> ExecuteDeleteAsync<T>(
			IQueryable<T> nativeQuery,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (nativeQuery == null) throw new ArgumentNullException(nameof(nativeQuery));

			return nativeQuery.DeleteFromQueryAsync(cancellationToken);
		}

		#endregion
	}
}
