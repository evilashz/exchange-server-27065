using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000C5 RID: 197
	[Serializable]
	internal class ADSubscribedPlan : ADObject
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00014F2B File Offset: 0x0001312B
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x00014F3D File Offset: 0x0001313D
		internal ADObjectId AccountId
		{
			get
			{
				return this[ADSubscribedPlanSchema.AccountIdProperty] as ADObjectId;
			}
			set
			{
				this[ADSubscribedPlanSchema.AccountIdProperty] = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00014F4B File Offset: 0x0001314B
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00014F5D File Offset: 0x0001315D
		internal string ServiceType
		{
			get
			{
				return this[ADSubscribedPlanSchema.ServiceTypeProperty] as string;
			}
			set
			{
				this[ADSubscribedPlanSchema.ServiceTypeProperty] = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00014F6B File Offset: 0x0001316B
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00014F7D File Offset: 0x0001317D
		internal string Capability
		{
			get
			{
				return this[ADSubscribedPlanSchema.CapabilityProperty] as string;
			}
			set
			{
				this[ADSubscribedPlanSchema.CapabilityProperty] = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00014F8B File Offset: 0x0001318B
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x00014F9D File Offset: 0x0001319D
		internal string MaximumOverageUnitsDetail
		{
			get
			{
				return this[ADSubscribedPlanSchema.MaximumOverageUnitsDetailProperty] as string;
			}
			set
			{
				this[ADSubscribedPlanSchema.MaximumOverageUnitsDetailProperty] = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00014FAB File Offset: 0x000131AB
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x00014FBD File Offset: 0x000131BD
		internal string PrepaidUnitsDetail
		{
			get
			{
				return this[ADSubscribedPlanSchema.PrepaidUnitsDetailProperty] as string;
			}
			set
			{
				this[ADSubscribedPlanSchema.PrepaidUnitsDetailProperty] = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00014FCB File Offset: 0x000131CB
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x00014FDD File Offset: 0x000131DD
		internal string TotalTrialUnitsDetail
		{
			get
			{
				return this[ADSubscribedPlanSchema.TotalTrialUnitsDetailProperty] as string;
			}
			set
			{
				this[ADSubscribedPlanSchema.TotalTrialUnitsDetailProperty] = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00014FEB File Offset: 0x000131EB
		internal int EffectiveSeatCount
		{
			get
			{
				return this.PrepaidUnitsEnabled + this.PrepaidUnitsWarning + this.MaximumOverageUnitsEnabled + this.MaximumOverageUnitsWarning;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00015008 File Offset: 0x00013208
		internal bool IsTrialOnly
		{
			get
			{
				return this.TotalTrialUnitsDetail != null && (this.TotalTrialUnitsEnabled == this.MaximumOverageUnitsEnabled + this.PrepaidUnitsEnabled && this.TotalTrialUnitsWarning == this.MaximumOverageUnitsWarning + this.PrepaidUnitsWarning) && this.TotalTrialUnitsSuspended == this.MaximumOverageUnitsSuspended + this.PrepaidUnitsSuspended;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00015060 File Offset: 0x00013260
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADSubscribedPlan.schema;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00015067 File Offset: 0x00013267
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADSubscribedPlan.mostDerivedClass;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001506E File Offset: 0x0001326E
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00015075 File Offset: 0x00013275
		internal int PrepaidUnitsEnabled
		{
			get
			{
				return ADSubscribedPlan.GetPlanAttributeValue(this.PrepaidUnitsDetail, "Enabled");
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00015087 File Offset: 0x00013287
		internal int PrepaidUnitsWarning
		{
			get
			{
				return ADSubscribedPlan.GetPlanAttributeValue(this.PrepaidUnitsDetail, "Warning");
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00015099 File Offset: 0x00013299
		internal int PrepaidUnitsSuspended
		{
			get
			{
				return ADSubscribedPlan.GetPlanAttributeValue(this.PrepaidUnitsDetail, "Suspended");
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x000150AB File Offset: 0x000132AB
		internal int MaximumOverageUnitsEnabled
		{
			get
			{
				return ADSubscribedPlan.GetPlanAttributeValue(this.MaximumOverageUnitsDetail, "Enabled");
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x000150BD File Offset: 0x000132BD
		internal int MaximumOverageUnitsWarning
		{
			get
			{
				return ADSubscribedPlan.GetPlanAttributeValue(this.MaximumOverageUnitsDetail, "Warning");
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x000150CF File Offset: 0x000132CF
		internal int MaximumOverageUnitsSuspended
		{
			get
			{
				return ADSubscribedPlan.GetPlanAttributeValue(this.MaximumOverageUnitsDetail, "Suspended");
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000150E1 File Offset: 0x000132E1
		internal int TotalTrialUnitsEnabled
		{
			get
			{
				return ADSubscribedPlan.GetPlanAttributeValue(this.TotalTrialUnitsDetail, "Enabled");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x000150F3 File Offset: 0x000132F3
		internal int TotalTrialUnitsWarning
		{
			get
			{
				return ADSubscribedPlan.GetPlanAttributeValue(this.TotalTrialUnitsDetail, "Warning");
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00015105 File Offset: 0x00013305
		internal int TotalTrialUnitsSuspended
		{
			get
			{
				return ADSubscribedPlan.GetPlanAttributeValue(this.TotalTrialUnitsDetail, "Suspended");
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00015118 File Offset: 0x00013318
		private static int GetPlanAttributeValue(string plan, string attributeName)
		{
			if (!string.IsNullOrWhiteSpace(plan))
			{
				XElement xelement = XElement.Parse(plan);
				return Convert.ToInt32(xelement.Attribute(attributeName).Value);
			}
			return 0;
		}

		// Token: 0x04000405 RID: 1029
		private const string EnabledAttribueName = "Enabled";

		// Token: 0x04000406 RID: 1030
		private const string WarningAttribueName = "Warning";

		// Token: 0x04000407 RID: 1031
		private const string SuspendedAttribueName = "Suspended";

		// Token: 0x04000408 RID: 1032
		private static readonly ADSubscribedPlanSchema schema = ObjectSchema.GetInstance<ADSubscribedPlanSchema>();

		// Token: 0x04000409 RID: 1033
		private static string mostDerivedClass = "ADSubscribedPlan";
	}
}
