using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CreateSyncSubscriptionResult : MigrationServiceRpcResult
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003A3C File Offset: 0x00001C3C
		internal CreateSyncSubscriptionResult(MdbefPropertyCollection args, MigrationServiceRpcMethodCode expectedMethodCode) : base(args)
		{
			object obj;
			if (args.TryGetValue(2936078594U, out obj))
			{
				byte[] byteArray = (byte[])obj;
				this.subscriptionMessageId = StoreObjectId.Deserialize(byteArray);
			}
			if (args.TryGetValue(2936799304U, out obj))
			{
				this.subscriptionGuid = new Guid?((Guid)obj);
			}
			base.ThrowIfVerifyFails(expectedMethodCode);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003A99 File Offset: 0x00001C99
		internal CreateSyncSubscriptionResult(MigrationServiceRpcMethodCode methodCode, StoreObjectId subscriptionMessageId, Guid subscriptionGuid) : base(methodCode)
		{
			this.subscriptionMessageId = subscriptionMessageId;
			this.subscriptionGuid = new Guid?(subscriptionGuid);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003AB5 File Offset: 0x00001CB5
		internal CreateSyncSubscriptionResult(MigrationServiceRpcMethodCode methodCode, MigrationServiceRpcResultCode resultCode, string errorDetails) : base(methodCode, resultCode, errorDetails)
		{
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003AC0 File Offset: 0x00001CC0
		internal StoreObjectId SubscriptionMessageId
		{
			get
			{
				return this.subscriptionMessageId;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003AC8 File Offset: 0x00001CC8
		internal Guid? SubscriptionGuid
		{
			get
			{
				return this.subscriptionGuid;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public override string ToString()
		{
			if (this.subscriptionGuid != null && this.subscriptionGuid.Value != Guid.Empty)
			{
				return this.subscriptionGuid.ToString();
			}
			if (this.subscriptionMessageId != null)
			{
				return this.subscriptionMessageId.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003B38 File Offset: 0x00001D38
		protected override void WriteTo(MdbefPropertyCollection collection)
		{
			if (this.SubscriptionMessageId != null)
			{
				collection[2936078594U] = this.SubscriptionMessageId.GetBytes();
			}
			if (this.subscriptionGuid != null && this.subscriptionGuid.Value != Guid.Empty)
			{
				collection[2936799304U] = this.subscriptionGuid;
			}
		}

		// Token: 0x040000A0 RID: 160
		private readonly StoreObjectId subscriptionMessageId;

		// Token: 0x040000A1 RID: 161
		private readonly Guid? subscriptionGuid;
	}
}
