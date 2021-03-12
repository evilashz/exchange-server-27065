using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000623 RID: 1571
	public class SetRpcClientAccessCommand : SyntheticCommandWithPipelineInputNoOutput<ExchangeRpcClientAccess>
	{
		// Token: 0x06005040 RID: 20544 RVA: 0x0007F482 File Offset: 0x0007D682
		private SetRpcClientAccessCommand() : base("Set-RpcClientAccess")
		{
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x0007F48F File Offset: 0x0007D68F
		public SetRpcClientAccessCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x0007F49E File Offset: 0x0007D69E
		public virtual SetRpcClientAccessCommand SetParameters(SetRpcClientAccessCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000624 RID: 1572
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002FB3 RID: 12211
			// (set) Token: 0x06005043 RID: 20547 RVA: 0x0007F4A8 File Offset: 0x0007D6A8
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002FB4 RID: 12212
			// (set) Token: 0x06005044 RID: 20548 RVA: 0x0007F4BB File Offset: 0x0007D6BB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002FB5 RID: 12213
			// (set) Token: 0x06005045 RID: 20549 RVA: 0x0007F4CE File Offset: 0x0007D6CE
			public virtual int MaximumConnections
			{
				set
				{
					base.PowerSharpParameters["MaximumConnections"] = value;
				}
			}

			// Token: 0x17002FB6 RID: 12214
			// (set) Token: 0x06005046 RID: 20550 RVA: 0x0007F4E6 File Offset: 0x0007D6E6
			public virtual bool EncryptionRequired
			{
				set
				{
					base.PowerSharpParameters["EncryptionRequired"] = value;
				}
			}

			// Token: 0x17002FB7 RID: 12215
			// (set) Token: 0x06005047 RID: 20551 RVA: 0x0007F4FE File Offset: 0x0007D6FE
			public virtual string BlockedClientVersions
			{
				set
				{
					base.PowerSharpParameters["BlockedClientVersions"] = value;
				}
			}

			// Token: 0x17002FB8 RID: 12216
			// (set) Token: 0x06005048 RID: 20552 RVA: 0x0007F511 File Offset: 0x0007D711
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002FB9 RID: 12217
			// (set) Token: 0x06005049 RID: 20553 RVA: 0x0007F524 File Offset: 0x0007D724
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FBA RID: 12218
			// (set) Token: 0x0600504A RID: 20554 RVA: 0x0007F53C File Offset: 0x0007D73C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FBB RID: 12219
			// (set) Token: 0x0600504B RID: 20555 RVA: 0x0007F554 File Offset: 0x0007D754
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FBC RID: 12220
			// (set) Token: 0x0600504C RID: 20556 RVA: 0x0007F56C File Offset: 0x0007D76C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002FBD RID: 12221
			// (set) Token: 0x0600504D RID: 20557 RVA: 0x0007F584 File Offset: 0x0007D784
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
