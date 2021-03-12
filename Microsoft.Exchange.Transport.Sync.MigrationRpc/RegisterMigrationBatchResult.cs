using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RegisterMigrationBatchResult : MigrationServiceRpcResult
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003F46 File Offset: 0x00002146
		internal RegisterMigrationBatchResult(MdbefPropertyCollection args, MigrationServiceRpcMethodCode expectedMethodCode) : base(args)
		{
			base.ThrowIfVerifyFails(expectedMethodCode);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003F56 File Offset: 0x00002156
		internal RegisterMigrationBatchResult(MigrationServiceRpcMethodCode methodCode) : base(methodCode)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003F5F File Offset: 0x0000215F
		internal RegisterMigrationBatchResult(MigrationServiceRpcMethodCode methodCode, MigrationServiceRpcResultCode resultCode, string errorDetails) : base(methodCode, resultCode, errorDetails)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003F6A File Offset: 0x0000216A
		protected override void WriteTo(MdbefPropertyCollection collection)
		{
		}
	}
}
