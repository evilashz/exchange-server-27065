using System;
using System.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001DD RID: 477
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OpenConnection : IDisposable
	{
		// Token: 0x06001131 RID: 4401 RVA: 0x00034ABE File Offset: 0x00032CBE
		public OpenConnection(IDbConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (connection.State == ConnectionState.Closed)
			{
				this.connection = connection;
				this.connection.Open();
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00034AEE File Offset: 0x00032CEE
		public void Dispose()
		{
			if (this.connection != null)
			{
				this.connection.Close();
			}
		}

		// Token: 0x040003CF RID: 975
		private IDbConnection connection;
	}
}
