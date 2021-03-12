using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C44 RID: 3140
	[Cmdlet("Set", "AutodiscoverVirtualDirectory", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetAutodiscoverVirtualDirectory : SetExchangeServiceVirtualDirectory<ADAutodiscoverVirtualDirectory>
	{
		// Token: 0x17002494 RID: 9364
		// (get) Token: 0x060076B8 RID: 30392 RVA: 0x001E4861 File Offset: 0x001E2A61
		// (set) Token: 0x060076B9 RID: 30393 RVA: 0x001E488C File Offset: 0x001E2A8C
		[Parameter(Mandatory = false)]
		public bool WSSecurityAuthentication
		{
			get
			{
				return base.Fields["WSSecurityAuthentication"] != null && (bool)base.Fields["WSSecurityAuthentication"];
			}
			set
			{
				base.Fields["WSSecurityAuthentication"] = value;
			}
		}

		// Token: 0x17002495 RID: 9365
		// (get) Token: 0x060076BA RID: 30394 RVA: 0x001E48A4 File Offset: 0x001E2AA4
		// (set) Token: 0x060076BB RID: 30395 RVA: 0x001E48CF File Offset: 0x001E2ACF
		[Parameter(Mandatory = false)]
		public bool OAuthAuthentication
		{
			get
			{
				return base.Fields["OAuthAuthentication"] != null && (bool)base.Fields["OAuthAuthentication"];
			}
			set
			{
				base.Fields["OAuthAuthentication"] = value;
			}
		}

		// Token: 0x17002496 RID: 9366
		// (get) Token: 0x060076BC RID: 30396 RVA: 0x001E48E7 File Offset: 0x001E2AE7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAutodiscoverVirtualDirectory;
			}
		}

		// Token: 0x060076BD RID: 30397 RVA: 0x001E48F0 File Offset: 0x001E2AF0
		protected override IConfigurable PrepareDataObject()
		{
			ADAutodiscoverVirtualDirectory adautodiscoverVirtualDirectory = (ADAutodiscoverVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			adautodiscoverVirtualDirectory.WSSecurityAuthentication = (bool?)base.Fields["WSSecurityAuthentication"];
			adautodiscoverVirtualDirectory.OAuthAuthentication = (bool?)base.Fields["OAuthAuthentication"];
			return adautodiscoverVirtualDirectory;
		}

		// Token: 0x060076BE RID: 30398 RVA: 0x001E494A File Offset: 0x001E2B4A
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			base.InternalValidateBasicLiveIdBasic();
		}

		// Token: 0x060076BF RID: 30399 RVA: 0x001E4961 File Offset: 0x001E2B61
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			base.InternalEnableLiveIdNegotiateAuxiliaryModule();
			ExchangeServiceVDirHelper.ForceAnonymous(this.DataObject.MetabasePath);
			ExchangeServiceVDirHelper.EwsAutodiscMWA.OnSetManageWCFEndpoints(this, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointProtocol.Autodiscover, this.WSSecurityAuthentication, this.DataObject);
		}
	}
}
