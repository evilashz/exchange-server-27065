using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	public class PushNotificationStoreId : XsoMailboxObjectId
	{
		// Token: 0x06000379 RID: 889 RVA: 0x0000E116 File Offset: 0x0000C316
		internal PushNotificationStoreId(ADObjectId mailboxOwnerId, StoreObjectId storeObjectIdValue, string subscriptionId) : base(mailboxOwnerId)
		{
			ArgumentValidator.ThrowIfNull("storeObjectId", storeObjectIdValue);
			this.StoreObjectId = storeObjectIdValue.ToString();
			this.StoreObjectIdValue = storeObjectIdValue;
			this.SubscriptionId = subscriptionId;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000E144 File Offset: 0x0000C344
		internal PushNotificationStoreId(ADObjectId mailboxOwnerId, StoreObjectId storeObjectIdValue) : this(mailboxOwnerId, storeObjectIdValue, string.Empty)
		{
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000E153 File Offset: 0x0000C353
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000E15B File Offset: 0x0000C35B
		public string SubscriptionId { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000E164 File Offset: 0x0000C364
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000E16C File Offset: 0x0000C36C
		public string StoreObjectId { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000E175 File Offset: 0x0000C375
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000E17D File Offset: 0x0000C37D
		internal StoreObjectId StoreObjectIdValue { get; private set; }

		// Token: 0x06000381 RID: 897 RVA: 0x0000E188 File Offset: 0x0000C388
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.SubscriptionId))
			{
				return string.Format("{0}\\{1}", base.MailboxOwnerId.Name, this.StoreObjectId);
			}
			return string.Format("{0}\\{1} ({2})", base.MailboxOwnerId.Name, this.StoreObjectId, this.SubscriptionId);
		}
	}
}
