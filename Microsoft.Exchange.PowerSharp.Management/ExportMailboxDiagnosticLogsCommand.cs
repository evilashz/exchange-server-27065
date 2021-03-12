using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200044C RID: 1100
	public class ExportMailboxDiagnosticLogsCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x06003FA3 RID: 16291 RVA: 0x0006A5E8 File Offset: 0x000687E8
		private ExportMailboxDiagnosticLogsCommand() : base("Export-MailboxDiagnosticLogs")
		{
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x0006A5F5 File Offset: 0x000687F5
		public ExportMailboxDiagnosticLogsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x0006A604 File Offset: 0x00068804
		public virtual ExportMailboxDiagnosticLogsCommand SetParameters(ExportMailboxDiagnosticLogsCommand.MailboxLogParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x0006A60E File Offset: 0x0006880E
		public virtual ExportMailboxDiagnosticLogsCommand SetParameters(ExportMailboxDiagnosticLogsCommand.ExtendedPropertiesParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x0006A618 File Offset: 0x00068818
		public virtual ExportMailboxDiagnosticLogsCommand SetParameters(ExportMailboxDiagnosticLogsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200044D RID: 1101
		public class MailboxLogParameterSetParameters : ParametersBase
		{
			// Token: 0x170022C4 RID: 8900
			// (set) Token: 0x06003FA8 RID: 16296 RVA: 0x0006A622 File Offset: 0x00068822
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserOrGeneralMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170022C5 RID: 8901
			// (set) Token: 0x06003FA9 RID: 16297 RVA: 0x0006A640 File Offset: 0x00068840
			public virtual string ComponentName
			{
				set
				{
					base.PowerSharpParameters["ComponentName"] = value;
				}
			}

			// Token: 0x170022C6 RID: 8902
			// (set) Token: 0x06003FAA RID: 16298 RVA: 0x0006A653 File Offset: 0x00068853
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170022C7 RID: 8903
			// (set) Token: 0x06003FAB RID: 16299 RVA: 0x0006A66B File Offset: 0x0006886B
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170022C8 RID: 8904
			// (set) Token: 0x06003FAC RID: 16300 RVA: 0x0006A67E File Offset: 0x0006887E
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170022C9 RID: 8905
			// (set) Token: 0x06003FAD RID: 16301 RVA: 0x0006A696 File Offset: 0x00068896
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170022CA RID: 8906
			// (set) Token: 0x06003FAE RID: 16302 RVA: 0x0006A6AE File Offset: 0x000688AE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170022CB RID: 8907
			// (set) Token: 0x06003FAF RID: 16303 RVA: 0x0006A6C1 File Offset: 0x000688C1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170022CC RID: 8908
			// (set) Token: 0x06003FB0 RID: 16304 RVA: 0x0006A6D9 File Offset: 0x000688D9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170022CD RID: 8909
			// (set) Token: 0x06003FB1 RID: 16305 RVA: 0x0006A6F1 File Offset: 0x000688F1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170022CE RID: 8910
			// (set) Token: 0x06003FB2 RID: 16306 RVA: 0x0006A709 File Offset: 0x00068909
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170022CF RID: 8911
			// (set) Token: 0x06003FB3 RID: 16307 RVA: 0x0006A721 File Offset: 0x00068921
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200044E RID: 1102
		public class ExtendedPropertiesParameterSetParameters : ParametersBase
		{
			// Token: 0x170022D0 RID: 8912
			// (set) Token: 0x06003FB5 RID: 16309 RVA: 0x0006A741 File Offset: 0x00068941
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserOrGeneralMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170022D1 RID: 8913
			// (set) Token: 0x06003FB6 RID: 16310 RVA: 0x0006A75F File Offset: 0x0006895F
			public virtual SwitchParameter ExtendedProperties
			{
				set
				{
					base.PowerSharpParameters["ExtendedProperties"] = value;
				}
			}

			// Token: 0x170022D2 RID: 8914
			// (set) Token: 0x06003FB7 RID: 16311 RVA: 0x0006A777 File Offset: 0x00068977
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170022D3 RID: 8915
			// (set) Token: 0x06003FB8 RID: 16312 RVA: 0x0006A78F File Offset: 0x0006898F
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170022D4 RID: 8916
			// (set) Token: 0x06003FB9 RID: 16313 RVA: 0x0006A7A2 File Offset: 0x000689A2
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170022D5 RID: 8917
			// (set) Token: 0x06003FBA RID: 16314 RVA: 0x0006A7BA File Offset: 0x000689BA
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170022D6 RID: 8918
			// (set) Token: 0x06003FBB RID: 16315 RVA: 0x0006A7D2 File Offset: 0x000689D2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170022D7 RID: 8919
			// (set) Token: 0x06003FBC RID: 16316 RVA: 0x0006A7E5 File Offset: 0x000689E5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170022D8 RID: 8920
			// (set) Token: 0x06003FBD RID: 16317 RVA: 0x0006A7FD File Offset: 0x000689FD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170022D9 RID: 8921
			// (set) Token: 0x06003FBE RID: 16318 RVA: 0x0006A815 File Offset: 0x00068A15
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170022DA RID: 8922
			// (set) Token: 0x06003FBF RID: 16319 RVA: 0x0006A82D File Offset: 0x00068A2D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170022DB RID: 8923
			// (set) Token: 0x06003FC0 RID: 16320 RVA: 0x0006A845 File Offset: 0x00068A45
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200044F RID: 1103
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170022DC RID: 8924
			// (set) Token: 0x06003FC2 RID: 16322 RVA: 0x0006A865 File Offset: 0x00068A65
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170022DD RID: 8925
			// (set) Token: 0x06003FC3 RID: 16323 RVA: 0x0006A878 File Offset: 0x00068A78
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170022DE RID: 8926
			// (set) Token: 0x06003FC4 RID: 16324 RVA: 0x0006A890 File Offset: 0x00068A90
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170022DF RID: 8927
			// (set) Token: 0x06003FC5 RID: 16325 RVA: 0x0006A8A8 File Offset: 0x00068AA8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170022E0 RID: 8928
			// (set) Token: 0x06003FC6 RID: 16326 RVA: 0x0006A8BB File Offset: 0x00068ABB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170022E1 RID: 8929
			// (set) Token: 0x06003FC7 RID: 16327 RVA: 0x0006A8D3 File Offset: 0x00068AD3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170022E2 RID: 8930
			// (set) Token: 0x06003FC8 RID: 16328 RVA: 0x0006A8EB File Offset: 0x00068AEB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170022E3 RID: 8931
			// (set) Token: 0x06003FC9 RID: 16329 RVA: 0x0006A903 File Offset: 0x00068B03
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170022E4 RID: 8932
			// (set) Token: 0x06003FCA RID: 16330 RVA: 0x0006A91B File Offset: 0x00068B1B
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
