using System;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000A2 RID: 162
	internal abstract class ExchangeMigrationRecipientWithHomeServer : ExchangeMigrationRecipient, IMigrationSerializable
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x000275CF File Offset: 0x000257CF
		public ExchangeMigrationRecipientWithHomeServer(MigrationUserRecipientType recepientType) : base(recepientType)
		{
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x000275D8 File Offset: 0x000257D8
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x000275E0 File Offset: 0x000257E0
		public string MsExchHomeServerName { get; protected set; }

		// Token: 0x06000920 RID: 2336 RVA: 0x000275E9 File Offset: 0x000257E9
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			if (!string.IsNullOrEmpty(this.MsExchHomeServerName))
			{
				message[MigrationBatchMessageSchema.MigrationJobItemExchangeMsExchHomeServerName] = this.MsExchHomeServerName;
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00027611 File Offset: 0x00025811
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			if (!base.ReadFromMessageItem(message))
			{
				return false;
			}
			this.MsExchHomeServerName = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemExchangeMsExchHomeServerName, null);
			return true;
		}
	}
}
