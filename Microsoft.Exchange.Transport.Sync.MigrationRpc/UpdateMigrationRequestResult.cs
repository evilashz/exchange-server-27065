using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpdateMigrationRequestResult : MigrationServiceRpcResult
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00003F94 File Offset: 0x00002194
		internal UpdateMigrationRequestResult(MdbefPropertyCollection args, MigrationServiceRpcMethodCode expectedMethodCode) : base(args)
		{
			object obj;
			if (args.TryGetValue(2936274947U, out obj) && obj is int)
			{
				this.responseCode = (SubscriptionStatusChangedResponse)((int)obj);
			}
			else
			{
				this.responseCode = SubscriptionStatusChangedResponse.OK;
			}
			base.ThrowIfVerifyFails(expectedMethodCode);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003FDB File Offset: 0x000021DB
		internal UpdateMigrationRequestResult(MigrationServiceRpcMethodCode methodCode, SubscriptionStatusChangedResponse responseCode) : base(methodCode)
		{
			this.responseCode = responseCode;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003FEB File Offset: 0x000021EB
		internal UpdateMigrationRequestResult(MigrationServiceRpcMethodCode methodCode, MigrationServiceRpcResultCode resultCode, string errorDetails) : base(methodCode, resultCode, errorDetails)
		{
			this.responseCode = SubscriptionStatusChangedResponse.OK;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003FFD File Offset: 0x000021FD
		internal bool IsActionRequired()
		{
			return (this.responseCode & SubscriptionStatusChangedResponse.ActionRequired) != (SubscriptionStatusChangedResponse)0;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004011 File Offset: 0x00002211
		internal bool IsDeleteRequested()
		{
			return this.responseCode == SubscriptionStatusChangedResponse.Delete;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004020 File Offset: 0x00002220
		internal bool IsDisableRequested()
		{
			return this.responseCode == SubscriptionStatusChangedResponse.Disable;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000402F File Offset: 0x0000222F
		protected override void WriteTo(MdbefPropertyCollection collection)
		{
			collection[2936274947U] = (int)this.responseCode;
		}

		// Token: 0x040000B0 RID: 176
		private readonly SubscriptionStatusChangedResponse responseCode;
	}
}
