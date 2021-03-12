using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000E6 RID: 230
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationNspiGetNewDsaRpcArgs : MigrationNspiRpcArgs
	{
		// Token: 0x06000BCB RID: 3019 RVA: 0x00034043 File Offset: 0x00032243
		public MigrationNspiGetNewDsaRpcArgs(ExchangeOutlookAnywhereEndpoint endpoint) : base(endpoint, MigrationProxyRpcType.GetNewDSA)
		{
			base.RpcHostServer = endpoint.ExchangeServer;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00034059 File Offset: 0x00032259
		public MigrationNspiGetNewDsaRpcArgs(byte[] requestBlob) : base(requestBlob, MigrationProxyRpcType.GetNewDSA)
		{
		}
	}
}
