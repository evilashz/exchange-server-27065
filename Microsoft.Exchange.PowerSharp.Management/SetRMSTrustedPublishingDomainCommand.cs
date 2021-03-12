using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003F8 RID: 1016
	public class SetRMSTrustedPublishingDomainCommand : SyntheticCommandWithPipelineInputNoOutput<RMSTrustedPublishingDomain>
	{
		// Token: 0x06003C3B RID: 15419 RVA: 0x00065F31 File Offset: 0x00064131
		private SetRMSTrustedPublishingDomainCommand() : base("Set-RMSTrustedPublishingDomain")
		{
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x00065F3E File Offset: 0x0006413E
		public SetRMSTrustedPublishingDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x00065F4D File Offset: 0x0006414D
		public virtual SetRMSTrustedPublishingDomainCommand SetParameters(SetRMSTrustedPublishingDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x00065F57 File Offset: 0x00064157
		public virtual SetRMSTrustedPublishingDomainCommand SetParameters(SetRMSTrustedPublishingDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003F9 RID: 1017
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002004 RID: 8196
			// (set) Token: 0x06003C3F RID: 15423 RVA: 0x00065F61 File Offset: 0x00064161
			public virtual Uri IntranetLicensingUrl
			{
				set
				{
					base.PowerSharpParameters["IntranetLicensingUrl"] = value;
				}
			}

			// Token: 0x17002005 RID: 8197
			// (set) Token: 0x06003C40 RID: 15424 RVA: 0x00065F74 File Offset: 0x00064174
			public virtual Uri ExtranetLicensingUrl
			{
				set
				{
					base.PowerSharpParameters["ExtranetLicensingUrl"] = value;
				}
			}

			// Token: 0x17002006 RID: 8198
			// (set) Token: 0x06003C41 RID: 15425 RVA: 0x00065F87 File Offset: 0x00064187
			public virtual Uri IntranetCertificationUrl
			{
				set
				{
					base.PowerSharpParameters["IntranetCertificationUrl"] = value;
				}
			}

			// Token: 0x17002007 RID: 8199
			// (set) Token: 0x06003C42 RID: 15426 RVA: 0x00065F9A File Offset: 0x0006419A
			public virtual Uri ExtranetCertificationUrl
			{
				set
				{
					base.PowerSharpParameters["ExtranetCertificationUrl"] = value;
				}
			}

			// Token: 0x17002008 RID: 8200
			// (set) Token: 0x06003C43 RID: 15427 RVA: 0x00065FAD File Offset: 0x000641AD
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x17002009 RID: 8201
			// (set) Token: 0x06003C44 RID: 15428 RVA: 0x00065FC5 File Offset: 0x000641C5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700200A RID: 8202
			// (set) Token: 0x06003C45 RID: 15429 RVA: 0x00065FD8 File Offset: 0x000641D8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700200B RID: 8203
			// (set) Token: 0x06003C46 RID: 15430 RVA: 0x00065FEB File Offset: 0x000641EB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700200C RID: 8204
			// (set) Token: 0x06003C47 RID: 15431 RVA: 0x00066003 File Offset: 0x00064203
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700200D RID: 8205
			// (set) Token: 0x06003C48 RID: 15432 RVA: 0x0006601B File Offset: 0x0006421B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700200E RID: 8206
			// (set) Token: 0x06003C49 RID: 15433 RVA: 0x00066033 File Offset: 0x00064233
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700200F RID: 8207
			// (set) Token: 0x06003C4A RID: 15434 RVA: 0x0006604B File Offset: 0x0006424B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020003FA RID: 1018
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002010 RID: 8208
			// (set) Token: 0x06003C4C RID: 15436 RVA: 0x0006606B File Offset: 0x0006426B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RmsTrustedPublishingDomainIdParameter(value) : null);
				}
			}

			// Token: 0x17002011 RID: 8209
			// (set) Token: 0x06003C4D RID: 15437 RVA: 0x00066089 File Offset: 0x00064289
			public virtual Uri IntranetLicensingUrl
			{
				set
				{
					base.PowerSharpParameters["IntranetLicensingUrl"] = value;
				}
			}

			// Token: 0x17002012 RID: 8210
			// (set) Token: 0x06003C4E RID: 15438 RVA: 0x0006609C File Offset: 0x0006429C
			public virtual Uri ExtranetLicensingUrl
			{
				set
				{
					base.PowerSharpParameters["ExtranetLicensingUrl"] = value;
				}
			}

			// Token: 0x17002013 RID: 8211
			// (set) Token: 0x06003C4F RID: 15439 RVA: 0x000660AF File Offset: 0x000642AF
			public virtual Uri IntranetCertificationUrl
			{
				set
				{
					base.PowerSharpParameters["IntranetCertificationUrl"] = value;
				}
			}

			// Token: 0x17002014 RID: 8212
			// (set) Token: 0x06003C50 RID: 15440 RVA: 0x000660C2 File Offset: 0x000642C2
			public virtual Uri ExtranetCertificationUrl
			{
				set
				{
					base.PowerSharpParameters["ExtranetCertificationUrl"] = value;
				}
			}

			// Token: 0x17002015 RID: 8213
			// (set) Token: 0x06003C51 RID: 15441 RVA: 0x000660D5 File Offset: 0x000642D5
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x17002016 RID: 8214
			// (set) Token: 0x06003C52 RID: 15442 RVA: 0x000660ED File Offset: 0x000642ED
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002017 RID: 8215
			// (set) Token: 0x06003C53 RID: 15443 RVA: 0x00066100 File Offset: 0x00064300
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002018 RID: 8216
			// (set) Token: 0x06003C54 RID: 15444 RVA: 0x00066113 File Offset: 0x00064313
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002019 RID: 8217
			// (set) Token: 0x06003C55 RID: 15445 RVA: 0x0006612B File Offset: 0x0006432B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700201A RID: 8218
			// (set) Token: 0x06003C56 RID: 15446 RVA: 0x00066143 File Offset: 0x00064343
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700201B RID: 8219
			// (set) Token: 0x06003C57 RID: 15447 RVA: 0x0006615B File Offset: 0x0006435B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700201C RID: 8220
			// (set) Token: 0x06003C58 RID: 15448 RVA: 0x00066173 File Offset: 0x00064373
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
