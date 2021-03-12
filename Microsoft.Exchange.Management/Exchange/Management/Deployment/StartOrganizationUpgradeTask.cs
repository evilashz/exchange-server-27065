using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000261 RID: 609
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Start", "OrganizationUpgrade", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class StartOrganizationUpgradeTask : StartOrganizationUpgradeBase
	{
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00061156 File Offset: 0x0005F356
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x0006115E File Offset: 0x0005F35E
		[Parameter(Mandatory = false)]
		public SwitchParameter AuthoritativeOnly
		{
			get
			{
				return this.authoritativeOnly;
			}
			set
			{
				this.authoritativeOnly = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x00061167 File Offset: 0x0005F367
		// (set) Token: 0x060016BB RID: 5819 RVA: 0x0006116F File Offset: 0x0005F36F
		[Parameter(Mandatory = false)]
		public SwitchParameter ConfigOnly
		{
			get
			{
				return this.configOnly;
			}
			set
			{
				this.configOnly = value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00061178 File Offset: 0x0005F378
		protected override LocalizedString Description
		{
			get
			{
				return Strings.StartOrganizationUpgradeDescription;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x0006117F File Offset: 0x0005F37F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageStartOrganizationUpgrade(base.Identity.ToString());
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00061191 File Offset: 0x0005F391
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new StartOrganizationUpgradeTaskModuleFactory();
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00061198 File Offset: 0x0005F398
		protected override void SetRunspaceVariables()
		{
			base.SetRunspaceVariables();
			this.monadConnection.RunspaceProxy.SetVariable(StartOrganizationUpgradeTask.ConfigOnlyVarName, this.ConfigOnly);
			if (this.authoritativeOnly || base.IsMSITTenant(base.CurrentOrganizationId))
			{
				this.monadConnection.RunspaceProxy.SetVariable(StartOrganizationUpgradeTask.AuthoritativeOnlyVarName, true);
			}
		}

		// Token: 0x040009E1 RID: 2529
		internal static readonly string AuthoritativeOnlyVarName = "AuthoritativeOnly";

		// Token: 0x040009E2 RID: 2530
		internal static readonly string ConfigOnlyVarName = "ConfigOnly";

		// Token: 0x040009E3 RID: 2531
		private SwitchParameter authoritativeOnly;

		// Token: 0x040009E4 RID: 2532
		private SwitchParameter configOnly;
	}
}
