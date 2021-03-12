using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009C3 RID: 2499
	public class ResumeFolderMoveRequestCommand : SyntheticCommandWithPipelineInput<FolderMoveRequestIdParameter, FolderMoveRequestIdParameter>
	{
		// Token: 0x06007D55 RID: 32085 RVA: 0x000BA6F5 File Offset: 0x000B88F5
		private ResumeFolderMoveRequestCommand() : base("Resume-FolderMoveRequest")
		{
		}

		// Token: 0x06007D56 RID: 32086 RVA: 0x000BA702 File Offset: 0x000B8902
		public ResumeFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007D57 RID: 32087 RVA: 0x000BA711 File Offset: 0x000B8911
		public virtual ResumeFolderMoveRequestCommand SetParameters(ResumeFolderMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007D58 RID: 32088 RVA: 0x000BA71B File Offset: 0x000B891B
		public virtual ResumeFolderMoveRequestCommand SetParameters(ResumeFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009C4 RID: 2500
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005588 RID: 21896
			// (set) Token: 0x06007D59 RID: 32089 RVA: 0x000BA725 File Offset: 0x000B8925
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new FolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005589 RID: 21897
			// (set) Token: 0x06007D5A RID: 32090 RVA: 0x000BA743 File Offset: 0x000B8943
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700558A RID: 21898
			// (set) Token: 0x06007D5B RID: 32091 RVA: 0x000BA756 File Offset: 0x000B8956
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700558B RID: 21899
			// (set) Token: 0x06007D5C RID: 32092 RVA: 0x000BA76E File Offset: 0x000B896E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700558C RID: 21900
			// (set) Token: 0x06007D5D RID: 32093 RVA: 0x000BA786 File Offset: 0x000B8986
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700558D RID: 21901
			// (set) Token: 0x06007D5E RID: 32094 RVA: 0x000BA79E File Offset: 0x000B899E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700558E RID: 21902
			// (set) Token: 0x06007D5F RID: 32095 RVA: 0x000BA7B6 File Offset: 0x000B89B6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009C5 RID: 2501
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700558F RID: 21903
			// (set) Token: 0x06007D61 RID: 32097 RVA: 0x000BA7D6 File Offset: 0x000B89D6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005590 RID: 21904
			// (set) Token: 0x06007D62 RID: 32098 RVA: 0x000BA7E9 File Offset: 0x000B89E9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005591 RID: 21905
			// (set) Token: 0x06007D63 RID: 32099 RVA: 0x000BA801 File Offset: 0x000B8A01
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005592 RID: 21906
			// (set) Token: 0x06007D64 RID: 32100 RVA: 0x000BA819 File Offset: 0x000B8A19
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005593 RID: 21907
			// (set) Token: 0x06007D65 RID: 32101 RVA: 0x000BA831 File Offset: 0x000B8A31
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005594 RID: 21908
			// (set) Token: 0x06007D66 RID: 32102 RVA: 0x000BA849 File Offset: 0x000B8A49
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
