using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000860 RID: 2144
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class Subscription : DirectoryObject
	{
		// Token: 0x06006B62 RID: 27490 RVA: 0x001738D6 File Offset: 0x00171AD6
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
		}

		// Token: 0x17002635 RID: 9781
		// (get) Token: 0x06006B63 RID: 27491 RVA: 0x001738D8 File Offset: 0x00171AD8
		// (set) Token: 0x06006B64 RID: 27492 RVA: 0x001738E0 File Offset: 0x00171AE0
		[XmlElement(Order = 0)]
		public DirectoryPropertyGuidSingle AccountId
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

		// Token: 0x17002636 RID: 9782
		// (get) Token: 0x06006B65 RID: 27493 RVA: 0x001738E9 File Offset: 0x00171AE9
		// (set) Token: 0x06006B66 RID: 27494 RVA: 0x001738F1 File Offset: 0x00171AF1
		[XmlElement(Order = 1)]
		public DirectoryPropertyStringSingleLength1To1024 CommerceSubscriptionContext
		{
			get
			{
				return this.commerceSubscriptionContextField;
			}
			set
			{
				this.commerceSubscriptionContextField = value;
			}
		}

		// Token: 0x17002637 RID: 9783
		// (get) Token: 0x06006B67 RID: 27495 RVA: 0x001738FA File Offset: 0x00171AFA
		// (set) Token: 0x06006B68 RID: 27496 RVA: 0x00173902 File Offset: 0x00171B02
		[XmlElement(Order = 2)]
		public DirectoryPropertyInt32SingleMin0 MaximumOverageUnits
		{
			get
			{
				return this.maximumOverageUnitsField;
			}
			set
			{
				this.maximumOverageUnitsField = value;
			}
		}

		// Token: 0x17002638 RID: 9784
		// (get) Token: 0x06006B69 RID: 27497 RVA: 0x0017390B File Offset: 0x00171B0B
		// (set) Token: 0x06006B6A RID: 27498 RVA: 0x00173913 File Offset: 0x00171B13
		[XmlElement(Order = 3)]
		public DirectoryPropertyDateTimeSingle NextLifecycleDate
		{
			get
			{
				return this.nextLifecycleDateField;
			}
			set
			{
				this.nextLifecycleDateField = value;
			}
		}

		// Token: 0x17002639 RID: 9785
		// (get) Token: 0x06006B6B RID: 27499 RVA: 0x0017391C File Offset: 0x00171B1C
		// (set) Token: 0x06006B6C RID: 27500 RVA: 0x00173924 File Offset: 0x00171B24
		[XmlElement(Order = 4)]
		public DirectoryPropertyGuidSingle OcpSubscriptionId
		{
			get
			{
				return this.ocpSubscriptionIdField;
			}
			set
			{
				this.ocpSubscriptionIdField = value;
			}
		}

		// Token: 0x1700263A RID: 9786
		// (get) Token: 0x06006B6D RID: 27501 RVA: 0x0017392D File Offset: 0x00171B2D
		// (set) Token: 0x06006B6E RID: 27502 RVA: 0x00173935 File Offset: 0x00171B35
		[XmlElement(Order = 5)]
		public DirectoryPropertyInt32SingleMin0 PrepaidUnits
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

		// Token: 0x1700263B RID: 9787
		// (get) Token: 0x06006B6F RID: 27503 RVA: 0x0017393E File Offset: 0x00171B3E
		// (set) Token: 0x06006B70 RID: 27504 RVA: 0x00173946 File Offset: 0x00171B46
		[XmlElement(Order = 6)]
		public DirectoryPropertyGuidSingle SkuId
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

		// Token: 0x1700263C RID: 9788
		// (get) Token: 0x06006B71 RID: 27505 RVA: 0x0017394F File Offset: 0x00171B4F
		// (set) Token: 0x06006B72 RID: 27506 RVA: 0x00173957 File Offset: 0x00171B57
		[XmlElement(Order = 7)]
		public DirectoryPropertyDateTimeSingle StartDate
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

		// Token: 0x1700263D RID: 9789
		// (get) Token: 0x06006B73 RID: 27507 RVA: 0x00173960 File Offset: 0x00171B60
		// (set) Token: 0x06006B74 RID: 27508 RVA: 0x00173968 File Offset: 0x00171B68
		[XmlElement(Order = 8)]
		public DirectoryPropertyGuidSingle SubscriptionChangeNotificationId
		{
			get
			{
				return this.subscriptionChangeNotificationIdField;
			}
			set
			{
				this.subscriptionChangeNotificationIdField = value;
			}
		}

		// Token: 0x1700263E RID: 9790
		// (get) Token: 0x06006B75 RID: 27509 RVA: 0x00173971 File Offset: 0x00171B71
		// (set) Token: 0x06006B76 RID: 27510 RVA: 0x00173979 File Offset: 0x00171B79
		[XmlElement(Order = 9)]
		public DirectoryPropertyInt32SingleMin0 SubscriptionStatus
		{
			get
			{
				return this.subscriptionStatusField;
			}
			set
			{
				this.subscriptionStatusField = value;
			}
		}

		// Token: 0x1700263F RID: 9791
		// (get) Token: 0x06006B77 RID: 27511 RVA: 0x00173982 File Offset: 0x00171B82
		// (set) Token: 0x06006B78 RID: 27512 RVA: 0x0017398A File Offset: 0x00171B8A
		[XmlElement(Order = 10)]
		public DirectoryPropertyBooleanSingle TrialSubscription
		{
			get
			{
				return this.trialSubscriptionField;
			}
			set
			{
				this.trialSubscriptionField = value;
			}
		}

		// Token: 0x17002640 RID: 9792
		// (get) Token: 0x06006B79 RID: 27513 RVA: 0x00173993 File Offset: 0x00171B93
		// (set) Token: 0x06006B7A RID: 27514 RVA: 0x0017399B File Offset: 0x00171B9B
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x040045FA RID: 17914
		private DirectoryPropertyGuidSingle accountIdField;

		// Token: 0x040045FB RID: 17915
		private DirectoryPropertyStringSingleLength1To1024 commerceSubscriptionContextField;

		// Token: 0x040045FC RID: 17916
		private DirectoryPropertyInt32SingleMin0 maximumOverageUnitsField;

		// Token: 0x040045FD RID: 17917
		private DirectoryPropertyDateTimeSingle nextLifecycleDateField;

		// Token: 0x040045FE RID: 17918
		private DirectoryPropertyGuidSingle ocpSubscriptionIdField;

		// Token: 0x040045FF RID: 17919
		private DirectoryPropertyInt32SingleMin0 prepaidUnitsField;

		// Token: 0x04004600 RID: 17920
		private DirectoryPropertyGuidSingle skuIdField;

		// Token: 0x04004601 RID: 17921
		private DirectoryPropertyDateTimeSingle startDateField;

		// Token: 0x04004602 RID: 17922
		private DirectoryPropertyGuidSingle subscriptionChangeNotificationIdField;

		// Token: 0x04004603 RID: 17923
		private DirectoryPropertyInt32SingleMin0 subscriptionStatusField;

		// Token: 0x04004604 RID: 17924
		private DirectoryPropertyBooleanSingle trialSubscriptionField;

		// Token: 0x04004605 RID: 17925
		private XmlAttribute[] anyAttrField;
	}
}
