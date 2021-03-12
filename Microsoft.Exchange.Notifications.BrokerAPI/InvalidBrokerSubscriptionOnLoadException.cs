using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000031 RID: 49
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidBrokerSubscriptionOnLoadException : NotificationsBrokerPermanentException
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00003C11 File Offset: 0x00001E11
		public InvalidBrokerSubscriptionOnLoadException(string storeId, string mailbox) : base(ClientAPIStrings.InvalidBrokerSubscriptionOnLoadException(storeId, mailbox))
		{
			this.storeId = storeId;
			this.mailbox = mailbox;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003C2E File Offset: 0x00001E2E
		public InvalidBrokerSubscriptionOnLoadException(string storeId, string mailbox, Exception innerException) : base(ClientAPIStrings.InvalidBrokerSubscriptionOnLoadException(storeId, mailbox), innerException)
		{
			this.storeId = storeId;
			this.mailbox = mailbox;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003C4C File Offset: 0x00001E4C
		protected InvalidBrokerSubscriptionOnLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.storeId = (string)info.GetValue("storeId", typeof(string));
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003CA1 File Offset: 0x00001EA1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("storeId", this.storeId);
			info.AddValue("mailbox", this.mailbox);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00003CCD File Offset: 0x00001ECD
		public string StoreId
		{
			get
			{
				return this.storeId;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00003CD5 File Offset: 0x00001ED5
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x0400007E RID: 126
		private readonly string storeId;

		// Token: 0x0400007F RID: 127
		private readonly string mailbox;
	}
}
