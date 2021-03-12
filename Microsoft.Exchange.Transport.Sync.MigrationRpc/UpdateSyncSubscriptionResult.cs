using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UpdateSyncSubscriptionResult : MigrationServiceRpcResult
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00003F6C File Offset: 0x0000216C
		internal UpdateSyncSubscriptionResult(MdbefPropertyCollection args, MigrationServiceRpcMethodCode expectedMethodCode) : base(args)
		{
			base.ThrowIfVerifyFails(expectedMethodCode);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003F7C File Offset: 0x0000217C
		internal UpdateSyncSubscriptionResult(MigrationServiceRpcMethodCode methodCode) : base(methodCode)
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003F85 File Offset: 0x00002185
		internal UpdateSyncSubscriptionResult(MigrationServiceRpcMethodCode methodCode, MigrationServiceRpcResultCode resultCode, string errorDetails) : base(methodCode, resultCode, errorDetails)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003F90 File Offset: 0x00002190
		protected override void WriteTo(MdbefPropertyCollection collection)
		{
		}
	}
}
