using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000798 RID: 1944
	public class SetRecipientFilterConfigCommand : SyntheticCommandWithPipelineInputNoOutput<RecipientFilterConfig>
	{
		// Token: 0x060061E6 RID: 25062 RVA: 0x00096802 File Offset: 0x00094A02
		private SetRecipientFilterConfigCommand() : base("Set-RecipientFilterConfig")
		{
		}

		// Token: 0x060061E7 RID: 25063 RVA: 0x0009680F File Offset: 0x00094A0F
		public SetRecipientFilterConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x0009681E File Offset: 0x00094A1E
		public virtual SetRecipientFilterConfigCommand SetParameters(SetRecipientFilterConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000799 RID: 1945
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003E6F RID: 15983
			// (set) Token: 0x060061E9 RID: 25065 RVA: 0x00096828 File Offset: 0x00094A28
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003E70 RID: 15984
			// (set) Token: 0x060061EA RID: 25066 RVA: 0x0009683B File Offset: 0x00094A3B
			public virtual MultiValuedProperty<SmtpAddress> BlockedRecipients
			{
				set
				{
					base.PowerSharpParameters["BlockedRecipients"] = value;
				}
			}

			// Token: 0x17003E71 RID: 15985
			// (set) Token: 0x060061EB RID: 25067 RVA: 0x0009684E File Offset: 0x00094A4E
			public virtual bool RecipientValidationEnabled
			{
				set
				{
					base.PowerSharpParameters["RecipientValidationEnabled"] = value;
				}
			}

			// Token: 0x17003E72 RID: 15986
			// (set) Token: 0x060061EC RID: 25068 RVA: 0x00096866 File Offset: 0x00094A66
			public virtual bool BlockListEnabled
			{
				set
				{
					base.PowerSharpParameters["BlockListEnabled"] = value;
				}
			}

			// Token: 0x17003E73 RID: 15987
			// (set) Token: 0x060061ED RID: 25069 RVA: 0x0009687E File Offset: 0x00094A7E
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003E74 RID: 15988
			// (set) Token: 0x060061EE RID: 25070 RVA: 0x00096896 File Offset: 0x00094A96
			public virtual bool ExternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003E75 RID: 15989
			// (set) Token: 0x060061EF RID: 25071 RVA: 0x000968AE File Offset: 0x00094AAE
			public virtual bool InternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003E76 RID: 15990
			// (set) Token: 0x060061F0 RID: 25072 RVA: 0x000968C6 File Offset: 0x00094AC6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003E77 RID: 15991
			// (set) Token: 0x060061F1 RID: 25073 RVA: 0x000968DE File Offset: 0x00094ADE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003E78 RID: 15992
			// (set) Token: 0x060061F2 RID: 25074 RVA: 0x000968F6 File Offset: 0x00094AF6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003E79 RID: 15993
			// (set) Token: 0x060061F3 RID: 25075 RVA: 0x0009690E File Offset: 0x00094B0E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003E7A RID: 15994
			// (set) Token: 0x060061F4 RID: 25076 RVA: 0x00096926 File Offset: 0x00094B26
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
