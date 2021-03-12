using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000619 RID: 1561
	public class NewRpcClientAccessCommand : SyntheticCommandWithPipelineInput<ExchangeRpcClientAccess, ExchangeRpcClientAccess>
	{
		// Token: 0x06004FF6 RID: 20470 RVA: 0x0007EED5 File Offset: 0x0007D0D5
		private NewRpcClientAccessCommand() : base("New-RpcClientAccess")
		{
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x0007EEE2 File Offset: 0x0007D0E2
		public NewRpcClientAccessCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0007EEF1 File Offset: 0x0007D0F1
		public virtual NewRpcClientAccessCommand SetParameters(NewRpcClientAccessCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200061A RID: 1562
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002F7D RID: 12157
			// (set) Token: 0x06004FF9 RID: 20473 RVA: 0x0007EEFB File Offset: 0x0007D0FB
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002F7E RID: 12158
			// (set) Token: 0x06004FFA RID: 20474 RVA: 0x0007EF0E File Offset: 0x0007D10E
			public virtual bool EncryptionRequired
			{
				set
				{
					base.PowerSharpParameters["EncryptionRequired"] = value;
				}
			}

			// Token: 0x17002F7F RID: 12159
			// (set) Token: 0x06004FFB RID: 20475 RVA: 0x0007EF26 File Offset: 0x0007D126
			public virtual int MaximumConnections
			{
				set
				{
					base.PowerSharpParameters["MaximumConnections"] = value;
				}
			}

			// Token: 0x17002F80 RID: 12160
			// (set) Token: 0x06004FFC RID: 20476 RVA: 0x0007EF3E File Offset: 0x0007D13E
			public virtual string BlockedClientVersions
			{
				set
				{
					base.PowerSharpParameters["BlockedClientVersions"] = value;
				}
			}

			// Token: 0x17002F81 RID: 12161
			// (set) Token: 0x06004FFD RID: 20477 RVA: 0x0007EF51 File Offset: 0x0007D151
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F82 RID: 12162
			// (set) Token: 0x06004FFE RID: 20478 RVA: 0x0007EF64 File Offset: 0x0007D164
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F83 RID: 12163
			// (set) Token: 0x06004FFF RID: 20479 RVA: 0x0007EF7C File Offset: 0x0007D17C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F84 RID: 12164
			// (set) Token: 0x06005000 RID: 20480 RVA: 0x0007EF94 File Offset: 0x0007D194
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F85 RID: 12165
			// (set) Token: 0x06005001 RID: 20481 RVA: 0x0007EFAC File Offset: 0x0007D1AC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002F86 RID: 12166
			// (set) Token: 0x06005002 RID: 20482 RVA: 0x0007EFC4 File Offset: 0x0007D1C4
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
