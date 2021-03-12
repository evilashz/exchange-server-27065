using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Search;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000097 RID: 151
	public class GetSearchDocumentFormatCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x06001936 RID: 6454 RVA: 0x00038540 File Offset: 0x00036740
		private GetSearchDocumentFormatCommand() : base("Get-SearchDocumentFormat")
		{
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0003854D File Offset: 0x0003674D
		public GetSearchDocumentFormatCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0003855C File Offset: 0x0003675C
		public virtual GetSearchDocumentFormatCommand SetParameters(GetSearchDocumentFormatCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000098 RID: 152
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170003C1 RID: 961
			// (set) Token: 0x06001939 RID: 6457 RVA: 0x00038566 File Offset: 0x00036766
			public virtual SearchDocumentFormatId Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170003C2 RID: 962
			// (set) Token: 0x0600193A RID: 6458 RVA: 0x00038579 File Offset: 0x00036779
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170003C3 RID: 963
			// (set) Token: 0x0600193B RID: 6459 RVA: 0x0003858C File Offset: 0x0003678C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170003C4 RID: 964
			// (set) Token: 0x0600193C RID: 6460 RVA: 0x000385A4 File Offset: 0x000367A4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170003C5 RID: 965
			// (set) Token: 0x0600193D RID: 6461 RVA: 0x000385BC File Offset: 0x000367BC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170003C6 RID: 966
			// (set) Token: 0x0600193E RID: 6462 RVA: 0x000385D4 File Offset: 0x000367D4
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
