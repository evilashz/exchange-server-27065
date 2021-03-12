using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000678 RID: 1656
	public class GetFederatedDomainProofCommand : SyntheticCommandWithPipelineInputNoOutput<SmtpDomain>
	{
		// Token: 0x06005887 RID: 22663 RVA: 0x0008AB0A File Offset: 0x00088D0A
		private GetFederatedDomainProofCommand() : base("Get-FederatedDomainProof")
		{
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x0008AB17 File Offset: 0x00088D17
		public GetFederatedDomainProofCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x0008AB26 File Offset: 0x00088D26
		public virtual GetFederatedDomainProofCommand SetParameters(GetFederatedDomainProofCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000679 RID: 1657
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003750 RID: 14160
			// (set) Token: 0x0600588A RID: 22666 RVA: 0x0008AB30 File Offset: 0x00088D30
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003751 RID: 14161
			// (set) Token: 0x0600588B RID: 22667 RVA: 0x0008AB43 File Offset: 0x00088D43
			public virtual string Thumbprint
			{
				set
				{
					base.PowerSharpParameters["Thumbprint"] = value;
				}
			}

			// Token: 0x17003752 RID: 14162
			// (set) Token: 0x0600588C RID: 22668 RVA: 0x0008AB56 File Offset: 0x00088D56
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003753 RID: 14163
			// (set) Token: 0x0600588D RID: 22669 RVA: 0x0008AB69 File Offset: 0x00088D69
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003754 RID: 14164
			// (set) Token: 0x0600588E RID: 22670 RVA: 0x0008AB81 File Offset: 0x00088D81
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003755 RID: 14165
			// (set) Token: 0x0600588F RID: 22671 RVA: 0x0008AB99 File Offset: 0x00088D99
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003756 RID: 14166
			// (set) Token: 0x06005890 RID: 22672 RVA: 0x0008ABB1 File Offset: 0x00088DB1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
