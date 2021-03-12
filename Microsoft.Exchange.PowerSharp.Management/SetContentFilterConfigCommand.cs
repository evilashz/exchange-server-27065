using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000702 RID: 1794
	public class SetContentFilterConfigCommand : SyntheticCommandWithPipelineInputNoOutput<ContentFilterConfig>
	{
		// Token: 0x06005CBF RID: 23743 RVA: 0x0008FF69 File Offset: 0x0008E169
		private SetContentFilterConfigCommand() : base("Set-ContentFilterConfig")
		{
		}

		// Token: 0x06005CC0 RID: 23744 RVA: 0x0008FF76 File Offset: 0x0008E176
		public SetContentFilterConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x0008FF85 File Offset: 0x0008E185
		public virtual SetContentFilterConfigCommand SetParameters(SetContentFilterConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000703 RID: 1795
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003A74 RID: 14964
			// (set) Token: 0x06005CC2 RID: 23746 RVA: 0x0008FF8F File Offset: 0x0008E18F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A75 RID: 14965
			// (set) Token: 0x06005CC3 RID: 23747 RVA: 0x0008FFA2 File Offset: 0x0008E1A2
			public virtual AsciiString RejectionResponse
			{
				set
				{
					base.PowerSharpParameters["RejectionResponse"] = value;
				}
			}

			// Token: 0x17003A76 RID: 14966
			// (set) Token: 0x06005CC4 RID: 23748 RVA: 0x0008FFB5 File Offset: 0x0008E1B5
			public virtual bool OutlookEmailPostmarkValidationEnabled
			{
				set
				{
					base.PowerSharpParameters["OutlookEmailPostmarkValidationEnabled"] = value;
				}
			}

			// Token: 0x17003A77 RID: 14967
			// (set) Token: 0x06005CC5 RID: 23749 RVA: 0x0008FFCD File Offset: 0x0008E1CD
			public virtual MultiValuedProperty<SmtpAddress> BypassedRecipients
			{
				set
				{
					base.PowerSharpParameters["BypassedRecipients"] = value;
				}
			}

			// Token: 0x17003A78 RID: 14968
			// (set) Token: 0x06005CC6 RID: 23750 RVA: 0x0008FFE0 File Offset: 0x0008E1E0
			public virtual SmtpAddress? QuarantineMailbox
			{
				set
				{
					base.PowerSharpParameters["QuarantineMailbox"] = value;
				}
			}

			// Token: 0x17003A79 RID: 14969
			// (set) Token: 0x06005CC7 RID: 23751 RVA: 0x0008FFF8 File Offset: 0x0008E1F8
			public virtual int SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17003A7A RID: 14970
			// (set) Token: 0x06005CC8 RID: 23752 RVA: 0x00090010 File Offset: 0x0008E210
			public virtual bool SCLRejectEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLRejectEnabled"] = value;
				}
			}

			// Token: 0x17003A7B RID: 14971
			// (set) Token: 0x06005CC9 RID: 23753 RVA: 0x00090028 File Offset: 0x0008E228
			public virtual int SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17003A7C RID: 14972
			// (set) Token: 0x06005CCA RID: 23754 RVA: 0x00090040 File Offset: 0x0008E240
			public virtual bool SCLDeleteEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteEnabled"] = value;
				}
			}

			// Token: 0x17003A7D RID: 14973
			// (set) Token: 0x06005CCB RID: 23755 RVA: 0x00090058 File Offset: 0x0008E258
			public virtual int SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17003A7E RID: 14974
			// (set) Token: 0x06005CCC RID: 23756 RVA: 0x00090070 File Offset: 0x0008E270
			public virtual bool SCLQuarantineEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineEnabled"] = value;
				}
			}

			// Token: 0x17003A7F RID: 14975
			// (set) Token: 0x06005CCD RID: 23757 RVA: 0x00090088 File Offset: 0x0008E288
			public virtual MultiValuedProperty<SmtpAddress> BypassedSenders
			{
				set
				{
					base.PowerSharpParameters["BypassedSenders"] = value;
				}
			}

			// Token: 0x17003A80 RID: 14976
			// (set) Token: 0x06005CCE RID: 23758 RVA: 0x0009009B File Offset: 0x0008E29B
			public virtual MultiValuedProperty<SmtpDomainWithSubdomains> BypassedSenderDomains
			{
				set
				{
					base.PowerSharpParameters["BypassedSenderDomains"] = value;
				}
			}

			// Token: 0x17003A81 RID: 14977
			// (set) Token: 0x06005CCF RID: 23759 RVA: 0x000900AE File Offset: 0x0008E2AE
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003A82 RID: 14978
			// (set) Token: 0x06005CD0 RID: 23760 RVA: 0x000900C6 File Offset: 0x0008E2C6
			public virtual bool ExternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003A83 RID: 14979
			// (set) Token: 0x06005CD1 RID: 23761 RVA: 0x000900DE File Offset: 0x0008E2DE
			public virtual bool InternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003A84 RID: 14980
			// (set) Token: 0x06005CD2 RID: 23762 RVA: 0x000900F6 File Offset: 0x0008E2F6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A85 RID: 14981
			// (set) Token: 0x06005CD3 RID: 23763 RVA: 0x0009010E File Offset: 0x0008E30E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A86 RID: 14982
			// (set) Token: 0x06005CD4 RID: 23764 RVA: 0x00090126 File Offset: 0x0008E326
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A87 RID: 14983
			// (set) Token: 0x06005CD5 RID: 23765 RVA: 0x0009013E File Offset: 0x0008E33E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003A88 RID: 14984
			// (set) Token: 0x06005CD6 RID: 23766 RVA: 0x00090156 File Offset: 0x0008E356
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
