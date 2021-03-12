using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Search;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200009D RID: 157
	public class SetSearchDocumentFormatCommand : SyntheticCommandWithPipelineInputNoOutput<Server>
	{
		// Token: 0x0600195A RID: 6490 RVA: 0x000387DD File Offset: 0x000369DD
		private SetSearchDocumentFormatCommand() : base("Set-SearchDocumentFormat")
		{
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x000387EA File Offset: 0x000369EA
		public SetSearchDocumentFormatCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000387F9 File Offset: 0x000369F9
		public virtual SetSearchDocumentFormatCommand SetParameters(SetSearchDocumentFormatCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200009E RID: 158
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170003D9 RID: 985
			// (set) Token: 0x0600195D RID: 6493 RVA: 0x00038803 File Offset: 0x00036A03
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170003DA RID: 986
			// (set) Token: 0x0600195E RID: 6494 RVA: 0x0003881B File Offset: 0x00036A1B
			public virtual SearchDocumentFormatId Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170003DB RID: 987
			// (set) Token: 0x0600195F RID: 6495 RVA: 0x0003882E File Offset: 0x00036A2E
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170003DC RID: 988
			// (set) Token: 0x06001960 RID: 6496 RVA: 0x00038841 File Offset: 0x00036A41
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170003DD RID: 989
			// (set) Token: 0x06001961 RID: 6497 RVA: 0x00038859 File Offset: 0x00036A59
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170003DE RID: 990
			// (set) Token: 0x06001962 RID: 6498 RVA: 0x00038871 File Offset: 0x00036A71
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170003DF RID: 991
			// (set) Token: 0x06001963 RID: 6499 RVA: 0x00038889 File Offset: 0x00036A89
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170003E0 RID: 992
			// (set) Token: 0x06001964 RID: 6500 RVA: 0x000388A1 File Offset: 0x00036AA1
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
