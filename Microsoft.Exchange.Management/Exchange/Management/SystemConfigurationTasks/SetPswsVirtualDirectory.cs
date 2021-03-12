using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C4F RID: 3151
	[Cmdlet("Set", "PswsVirtualDirectory", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetPswsVirtualDirectory : SetPowerShellCommonVirtualDirectory<ADPswsVirtualDirectory>
	{
		// Token: 0x170024EC RID: 9452
		// (get) Token: 0x0600778E RID: 30606 RVA: 0x001E74FB File Offset: 0x001E56FB
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetPswsVirtualDirectory(this.DataObject.Name, this.DataObject.Server.ToString());
			}
		}

		// Token: 0x170024ED RID: 9453
		// (get) Token: 0x0600778F RID: 30607 RVA: 0x001E751D File Offset: 0x001E571D
		// (set) Token: 0x06007790 RID: 30608 RVA: 0x001E7548 File Offset: 0x001E5748
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

		// Token: 0x170024EE RID: 9454
		// (get) Token: 0x06007791 RID: 30609 RVA: 0x001E7560 File Offset: 0x001E5760
		protected override PowerShellVirtualDirectoryType AllowedVirtualDirectoryType
		{
			get
			{
				return PowerShellVirtualDirectoryType.Psws;
			}
		}

		// Token: 0x06007792 RID: 30610 RVA: 0x001E7564 File Offset: 0x001E5764
		protected override IConfigurable PrepareDataObject()
		{
			ADPswsVirtualDirectory adpswsVirtualDirectory = (ADPswsVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			adpswsVirtualDirectory.OAuthAuthentication = (bool?)base.Fields["OAuthAuthentication"];
			return adpswsVirtualDirectory;
		}

		// Token: 0x06007793 RID: 30611 RVA: 0x001E75A3 File Offset: 0x001E57A3
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (ExchangeServiceVDirHelper.IsBackEndVirtualDirectory(this.DataObject))
			{
				ExchangeServiceVDirHelper.ForceAnonymous(this.DataObject.MetabasePath);
			}
		}
	}
}
