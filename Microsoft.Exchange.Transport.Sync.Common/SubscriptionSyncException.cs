using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200004E RID: 78
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionSyncException : LocalizedException
	{
		// Token: 0x06000213 RID: 531 RVA: 0x000062BA File Offset: 0x000044BA
		public SubscriptionSyncException(string subscriptionName) : base(Strings.SubscriptionSyncException(subscriptionName))
		{
			this.subscriptionName = subscriptionName;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000062CF File Offset: 0x000044CF
		public SubscriptionSyncException(string subscriptionName, Exception innerException) : base(Strings.SubscriptionSyncException(subscriptionName), innerException)
		{
			this.subscriptionName = subscriptionName;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000062E5 File Offset: 0x000044E5
		protected SubscriptionSyncException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.subscriptionName = (string)info.GetValue("subscriptionName", typeof(string));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000630F File Offset: 0x0000450F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("subscriptionName", this.subscriptionName);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000632A File Offset: 0x0000452A
		public string SubscriptionName
		{
			get
			{
				return this.subscriptionName;
			}
		}

		// Token: 0x040000F8 RID: 248
		private readonly string subscriptionName;
	}
}
