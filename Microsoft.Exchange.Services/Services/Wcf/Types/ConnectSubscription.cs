using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AF4 RID: 2804
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ConnectSubscription : Subscription
	{
		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06004FC8 RID: 20424 RVA: 0x00108F08 File Offset: 0x00107108
		// (set) Token: 0x06004FC9 RID: 20425 RVA: 0x00108F10 File Offset: 0x00107110
		[IgnoreDataMember]
		public AggregationType AggregationType
		{
			get
			{
				return this.aggregationType;
			}
			set
			{
				this.aggregationType = value;
				base.TrackPropertyChanged("AggregationType");
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06004FCA RID: 20426 RVA: 0x00108F24 File Offset: 0x00107124
		// (set) Token: 0x06004FCB RID: 20427 RVA: 0x00108F36 File Offset: 0x00107136
		[DataMember(Name = "AggregationType")]
		public string AggregationTypeString
		{
			get
			{
				return this.aggregationType.ToString();
			}
			set
			{
				this.aggregationType = EnumUtilities.Parse<AggregationType>(value);
				base.TrackPropertyChanged("AggregationTypeString");
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06004FCC RID: 20428 RVA: 0x00108F4F File Offset: 0x0010714F
		// (set) Token: 0x06004FCD RID: 20429 RVA: 0x00108F57 File Offset: 0x00107157
		[IgnoreDataMember]
		public AggregationSubscriptionType AggregationSubscriptionType
		{
			get
			{
				return this.aggregationSubscriptionType;
			}
			set
			{
				this.aggregationSubscriptionType = value;
				base.TrackPropertyChanged("AggregationSubscriptionType");
			}
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06004FCE RID: 20430 RVA: 0x00108F6B File Offset: 0x0010716B
		// (set) Token: 0x06004FCF RID: 20431 RVA: 0x00108F7D File Offset: 0x0010717D
		[DataMember(Name = "AggregationSubscriptionType")]
		public string AggregationSubscriptionTypeString
		{
			get
			{
				return this.aggregationSubscriptionType.ToString();
			}
			set
			{
				this.aggregationSubscriptionType = EnumUtilities.Parse<AggregationSubscriptionType>(value);
				base.TrackPropertyChanged("AggregationSubscriptionTypeString");
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x00108F96 File Offset: 0x00107196
		// (set) Token: 0x06004FD1 RID: 20433 RVA: 0x00108F9E File Offset: 0x0010719E
		[IgnoreDataMember]
		public ConnectState ConnectState
		{
			get
			{
				return this.connectState;
			}
			set
			{
				this.connectState = value;
				base.TrackPropertyChanged("ConnectState");
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06004FD2 RID: 20434 RVA: 0x00108FB2 File Offset: 0x001071B2
		// (set) Token: 0x06004FD3 RID: 20435 RVA: 0x00108FC4 File Offset: 0x001071C4
		[DataMember(Name = "ConnectState")]
		public string ConnectStateString
		{
			get
			{
				return this.connectState.ToString();
			}
			set
			{
				this.connectState = EnumUtilities.Parse<ConnectState>(value);
				base.TrackPropertyChanged("ConnectStateString");
			}
		}

		// Token: 0x04002CB3 RID: 11443
		private AggregationSubscriptionType aggregationSubscriptionType;

		// Token: 0x04002CB4 RID: 11444
		private AggregationType aggregationType;

		// Token: 0x04002CB5 RID: 11445
		private ConnectState connectState;
	}
}
