using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200018E RID: 398
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(TypeName = "Subscription", Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class Subscription1 : DirectoryObject
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x000217C3 File Offset: 0x0001F9C3
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x000217CB File Offset: 0x0001F9CB
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

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x000217D4 File Offset: 0x0001F9D4
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x000217DC File Offset: 0x0001F9DC
		public DirectoryPropertyBooleanSingle BelongsToFirstLoginObjectSet
		{
			get
			{
				return this.belongsToFirstLoginObjectSetField;
			}
			set
			{
				this.belongsToFirstLoginObjectSetField = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x000217E5 File Offset: 0x0001F9E5
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x000217ED File Offset: 0x0001F9ED
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

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x000217F6 File Offset: 0x0001F9F6
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x000217FE File Offset: 0x0001F9FE
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

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00021807 File Offset: 0x0001FA07
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x0002180F File Offset: 0x0001FA0F
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

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00021818 File Offset: 0x0001FA18
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x00021820 File Offset: 0x0001FA20
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

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00021829 File Offset: 0x0001FA29
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x00021831 File Offset: 0x0001FA31
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

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0002183A File Offset: 0x0001FA3A
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x00021842 File Offset: 0x0001FA42
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

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0002184B File Offset: 0x0001FA4B
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x00021853 File Offset: 0x0001FA53
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

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002185C File Offset: 0x0001FA5C
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x00021864 File Offset: 0x0001FA64
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

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0002186D File Offset: 0x0001FA6D
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x00021875 File Offset: 0x0001FA75
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

		// Token: 0x0400054B RID: 1355
		private DirectoryPropertyGuidSingle accountIdField;

		// Token: 0x0400054C RID: 1356
		private DirectoryPropertyBooleanSingle belongsToFirstLoginObjectSetField;

		// Token: 0x0400054D RID: 1357
		private DirectoryPropertyInt32SingleMin0 maximumOverageUnitsField;

		// Token: 0x0400054E RID: 1358
		private DirectoryPropertyDateTimeSingle nextLifecycleDateField;

		// Token: 0x0400054F RID: 1359
		private DirectoryPropertyGuidSingle ocpSubscriptionIdField;

		// Token: 0x04000550 RID: 1360
		private DirectoryPropertyInt32SingleMin0 prepaidUnitsField;

		// Token: 0x04000551 RID: 1361
		private DirectoryPropertyGuidSingle skuIdField;

		// Token: 0x04000552 RID: 1362
		private DirectoryPropertyDateTimeSingle startDateField;

		// Token: 0x04000553 RID: 1363
		private DirectoryPropertyInt32SingleMin0 subscriptionStatusField;

		// Token: 0x04000554 RID: 1364
		private DirectoryPropertyBooleanSingle trialSubscriptionField;

		// Token: 0x04000555 RID: 1365
		private XmlAttribute[] anyAttrField;
	}
}
