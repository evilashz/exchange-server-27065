using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000278 RID: 632
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Uninstall", "ExchangeOrganization", SupportsShouldProcess = true)]
	public sealed class UninstallExchangeOrganization : ComponentInfoBasedTask
	{
		// Token: 0x06001766 RID: 5990 RVA: 0x00063500 File Offset: 0x00061700
		public UninstallExchangeOrganization()
		{
			base.Fields["InstallationMode"] = InstallationModes.Uninstall;
			base.Fields["RemoveOrganization"] = false;
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00063534 File Offset: 0x00061734
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UninstallExchangeOrganizationDescription;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x0006353B File Offset: 0x0006173B
		// (set) Token: 0x06001769 RID: 5993 RVA: 0x00063552 File Offset: 0x00061752
		[Parameter(Mandatory = false)]
		public bool RemoveOrganization
		{
			get
			{
				return (bool)base.Fields["RemoveOrganization"];
			}
			set
			{
				base.Fields["RemoveOrganization"] = value;
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0006356C File Offset: 0x0006176C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.ComponentInfoFileNames = new List<string>();
			if (this.RemoveOrganization)
			{
				base.ComponentInfoFileNames.Add("setup\\data\\UpdateResourcePropertySchemaComponent.xml");
				base.ComponentInfoFileNames.Add("Setup\\data\\ADSchemaComponent.xml");
				base.ComponentInfoFileNames.Add("Setup\\data\\CommonGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("Setup\\data\\TransportGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("Setup\\data\\BridgeheadGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("Setup\\data\\ClientAccessGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("Setup\\data\\MailboxGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("Setup\\data\\UnifiedMessagingGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\PostPrepForestGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("Setup\\data\\DomainGlobalConfig.xml");
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0006363F File Offset: 0x0006183F
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.ComponentInfoFileNames.Count == 0)
			{
				base.WriteProgress(new LocalizedString(this.Description), Strings.ProgressStatusCompleted, 100);
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}
	}
}
