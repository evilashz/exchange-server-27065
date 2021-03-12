using System;
using System.Management.Automation;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000021 RID: 33
	[Cmdlet("Get", "Subscription", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class GetSubscription : GetSubscriptionBase<PimSubscriptionProxy>
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000077F1 File Offset: 0x000059F1
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00007808 File Offset: 0x00005A08
		[Parameter(Mandatory = false)]
		public AggregationSubscriptionType SubscriptionType
		{
			get
			{
				return (AggregationSubscriptionType)base.Fields["SubscriptionType"];
			}
			set
			{
				base.Fields["SubscriptionType"] = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00007820 File Offset: 0x00005A20
		protected override AggregationSubscriptionType IdentityType
		{
			get
			{
				if (!base.Fields.IsModified("SubscriptionType"))
				{
					return AggregationSubscriptionType.All;
				}
				return this.SubscriptionType;
			}
		}
	}
}
