using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Search;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200009B RID: 155
	public class RemoveSearchDocumentFormatCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x0600194F RID: 6479 RVA: 0x00038711 File Offset: 0x00036911
		private RemoveSearchDocumentFormatCommand() : base("Remove-SearchDocumentFormat")
		{
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0003871E File Offset: 0x0003691E
		public RemoveSearchDocumentFormatCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0003872D File Offset: 0x0003692D
		public virtual RemoveSearchDocumentFormatCommand SetParameters(RemoveSearchDocumentFormatCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200009C RID: 156
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170003D2 RID: 978
			// (set) Token: 0x06001952 RID: 6482 RVA: 0x00038737 File Offset: 0x00036937
			public virtual SearchDocumentFormatId Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170003D3 RID: 979
			// (set) Token: 0x06001953 RID: 6483 RVA: 0x0003874A File Offset: 0x0003694A
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170003D4 RID: 980
			// (set) Token: 0x06001954 RID: 6484 RVA: 0x0003875D File Offset: 0x0003695D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170003D5 RID: 981
			// (set) Token: 0x06001955 RID: 6485 RVA: 0x00038775 File Offset: 0x00036975
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170003D6 RID: 982
			// (set) Token: 0x06001956 RID: 6486 RVA: 0x0003878D File Offset: 0x0003698D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170003D7 RID: 983
			// (set) Token: 0x06001957 RID: 6487 RVA: 0x000387A5 File Offset: 0x000369A5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170003D8 RID: 984
			// (set) Token: 0x06001958 RID: 6488 RVA: 0x000387BD File Offset: 0x000369BD
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
