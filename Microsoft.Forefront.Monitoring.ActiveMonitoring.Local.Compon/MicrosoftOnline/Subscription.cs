using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001A0 RID: 416
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class Subscription
	{
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00022F6F File Offset: 0x0002116F
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x00022F77 File Offset: 0x00021177
		public Guid ContextId
		{
			get
			{
				return this.contextIdField;
			}
			set
			{
				this.contextIdField = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x00022F80 File Offset: 0x00021180
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x00022F88 File Offset: 0x00021188
		public Guid AccountId
		{
			get
			{
				return this.accountIdField;
			}
			set
			{
				this.accountIdField = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00022F91 File Offset: 0x00021191
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x00022F99 File Offset: 0x00021199
		public Guid SubscriptionId
		{
			get
			{
				return this.subscriptionIdField;
			}
			set
			{
				this.subscriptionIdField = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00022FA2 File Offset: 0x000211A2
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x00022FAA File Offset: 0x000211AA
		public Guid SkuId
		{
			get
			{
				return this.skuIdField;
			}
			set
			{
				this.skuIdField = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00022FB3 File Offset: 0x000211B3
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x00022FBB File Offset: 0x000211BB
		public int PrepaidUnits
		{
			get
			{
				return this.prepaidUnitsField;
			}
			set
			{
				this.prepaidUnitsField = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00022FC4 File Offset: 0x000211C4
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x00022FCC File Offset: 0x000211CC
		public int AllowedOverageUnits
		{
			get
			{
				return this.allowedOverageUnitsField;
			}
			set
			{
				this.allowedOverageUnitsField = value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00022FD5 File Offset: 0x000211D5
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x00022FDD File Offset: 0x000211DD
		public SubscriptionState LifecycleState
		{
			get
			{
				return this.lifecycleStateField;
			}
			set
			{
				this.lifecycleStateField = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00022FE6 File Offset: 0x000211E6
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x00022FEE File Offset: 0x000211EE
		public DateTime LifecycleNextStateChangeDate
		{
			get
			{
				return this.lifecycleNextStateChangeDateField;
			}
			set
			{
				this.lifecycleNextStateChangeDateField = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00022FF7 File Offset: 0x000211F7
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x00022FFF File Offset: 0x000211FF
		public DateTime StartDate
		{
			get
			{
				return this.startDateField;
			}
			set
			{
				this.startDateField = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x00023008 File Offset: 0x00021208
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x00023010 File Offset: 0x00021210
		public string OfferType
		{
			get
			{
				return this.offerTypeField;
			}
			set
			{
				this.offerTypeField = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00023019 File Offset: 0x00021219
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x00023021 File Offset: 0x00021221
		public string PartNumber
		{
			get
			{
				return this.partNumberField;
			}
			set
			{
				this.partNumberField = value;
			}
		}

		// Token: 0x040006A7 RID: 1703
		private Guid contextIdField;

		// Token: 0x040006A8 RID: 1704
		private Guid accountIdField;

		// Token: 0x040006A9 RID: 1705
		private Guid subscriptionIdField;

		// Token: 0x040006AA RID: 1706
		private Guid skuIdField;

		// Token: 0x040006AB RID: 1707
		private int prepaidUnitsField;

		// Token: 0x040006AC RID: 1708
		private int allowedOverageUnitsField;

		// Token: 0x040006AD RID: 1709
		private SubscriptionState lifecycleStateField;

		// Token: 0x040006AE RID: 1710
		private DateTime lifecycleNextStateChangeDateField;

		// Token: 0x040006AF RID: 1711
		private DateTime startDateField;

		// Token: 0x040006B0 RID: 1712
		private string offerTypeField;

		// Token: 0x040006B1 RID: 1713
		private string partNumberField;
	}
}
