using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.TransportProcessingQuota
{
	// Token: 0x020000B1 RID: 177
	[Cmdlet("Set", "TransportProcessingQuotaConfig", SupportsShouldProcess = true)]
	public sealed class SetTransportProcessingQuotaConfig : TransportProcessingQuotaBaseTask
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001A59A File Offset: 0x0001879A
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0001A5A2 File Offset: 0x000187A2
		[Parameter(Mandatory = false)]
		public double? AmWeight { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001A5AB File Offset: 0x000187AB
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0001A5B3 File Offset: 0x000187B3
		[Parameter(Mandatory = false)]
		public double? AsWeight { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001A5BC File Offset: 0x000187BC
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001A5C4 File Offset: 0x000187C4
		[Parameter(Mandatory = false)]
		public bool? CalculationEnabled { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001A5CD File Offset: 0x000187CD
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0001A5D5 File Offset: 0x000187D5
		[Parameter(Mandatory = false)]
		public int? CalculationFrequency { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001A5DE File Offset: 0x000187DE
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0001A5E6 File Offset: 0x000187E6
		[Parameter(Mandatory = false)]
		public int? CostThreshold { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001A5EF File Offset: 0x000187EF
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x0001A5F7 File Offset: 0x000187F7
		[Parameter(Mandatory = false)]
		public double? EtrWeight { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001A600 File Offset: 0x00018800
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0001A608 File Offset: 0x00018808
		[Parameter(Mandatory = false)]
		public bool? ThrottlingEnabled { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001A611 File Offset: 0x00018811
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0001A619 File Offset: 0x00018819
		[Parameter(Mandatory = false)]
		public int? TimeWindow { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001A622 File Offset: 0x00018822
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0001A62A File Offset: 0x0001882A
		[Parameter(Mandatory = false)]
		public double? ThrottleFactor { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001A633 File Offset: 0x00018833
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x0001A63B File Offset: 0x0001883B
		[Parameter(Mandatory = false)]
		public double? RelativeCostThreshold { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001A644 File Offset: 0x00018844
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetTransportProcessingConfig;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001A64C File Offset: 0x0001884C
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			TransportProcessingQuotaConfig transportThrottlingConfig = base.Session.GetTransportThrottlingConfig();
			if (this.AmWeight != null)
			{
				transportThrottlingConfig.AmWeight = this.AmWeight.Value;
			}
			if (this.AsWeight != null)
			{
				transportThrottlingConfig.AsWeight = this.AsWeight.Value;
			}
			if (this.CalculationEnabled != null)
			{
				transportThrottlingConfig.CalculationEnabled = this.CalculationEnabled.Value;
			}
			if (this.CalculationFrequency != null)
			{
				transportThrottlingConfig.CalculationFrequency = this.CalculationFrequency.Value;
			}
			if (this.CostThreshold != null)
			{
				transportThrottlingConfig.CostThreshold = this.CostThreshold.Value;
			}
			if (this.ThrottlingEnabled != null)
			{
				transportThrottlingConfig.ThrottlingEnabled = this.ThrottlingEnabled.Value;
			}
			if (this.EtrWeight != null)
			{
				transportThrottlingConfig.EtrWeight = this.EtrWeight.Value;
			}
			if (this.TimeWindow != null)
			{
				transportThrottlingConfig.TimeWindow = this.TimeWindow.Value;
			}
			if (this.ThrottleFactor != null)
			{
				transportThrottlingConfig.ThrottleFactor = this.ThrottleFactor.Value;
			}
			if (this.RelativeCostThreshold != null)
			{
				transportThrottlingConfig.RelativeCostThreshold = this.RelativeCostThreshold.Value;
			}
			base.Session.SetTransportThrottlingConfig(transportThrottlingConfig);
		}
	}
}
