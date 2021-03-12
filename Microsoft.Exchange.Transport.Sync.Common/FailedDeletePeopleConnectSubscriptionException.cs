using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200000D RID: 13
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedDeletePeopleConnectSubscriptionException : LocalizedException
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00004B55 File Offset: 0x00002D55
		public FailedDeletePeopleConnectSubscriptionException(AggregationSubscriptionType subscriptionType) : base(Strings.FailedDeletePeopleConnectSubscription(subscriptionType))
		{
			this.subscriptionType = subscriptionType;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004B6A File Offset: 0x00002D6A
		public FailedDeletePeopleConnectSubscriptionException(AggregationSubscriptionType subscriptionType, Exception innerException) : base(Strings.FailedDeletePeopleConnectSubscription(subscriptionType), innerException)
		{
			this.subscriptionType = subscriptionType;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004B80 File Offset: 0x00002D80
		protected FailedDeletePeopleConnectSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.subscriptionType = (AggregationSubscriptionType)info.GetValue("subscriptionType", typeof(AggregationSubscriptionType));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004BAA File Offset: 0x00002DAA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("subscriptionType", this.subscriptionType);
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004BCA File Offset: 0x00002DCA
		public AggregationSubscriptionType SubscriptionType
		{
			get
			{
				return this.subscriptionType;
			}
		}

		// Token: 0x040000D1 RID: 209
		private readonly AggregationSubscriptionType subscriptionType;
	}
}
