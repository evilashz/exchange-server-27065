using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000305 RID: 773
	public class GetPopSettingsCommand : SyntheticCommandWithPipelineInput<Pop3AdConfiguration, Pop3AdConfiguration>
	{
		// Token: 0x0600337B RID: 13179 RVA: 0x0005AA34 File Offset: 0x00058C34
		private GetPopSettingsCommand() : base("Get-PopSettings")
		{
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x0005AA41 File Offset: 0x00058C41
		public GetPopSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x0005AA50 File Offset: 0x00058C50
		public virtual GetPopSettingsCommand SetParameters(GetPopSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000306 RID: 774
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700192A RID: 6442
			// (set) Token: 0x0600337E RID: 13182 RVA: 0x0005AA5A File Offset: 0x00058C5A
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700192B RID: 6443
			// (set) Token: 0x0600337F RID: 13183 RVA: 0x0005AA6D File Offset: 0x00058C6D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700192C RID: 6444
			// (set) Token: 0x06003380 RID: 13184 RVA: 0x0005AA80 File Offset: 0x00058C80
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700192D RID: 6445
			// (set) Token: 0x06003381 RID: 13185 RVA: 0x0005AA98 File Offset: 0x00058C98
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700192E RID: 6446
			// (set) Token: 0x06003382 RID: 13186 RVA: 0x0005AAB0 File Offset: 0x00058CB0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700192F RID: 6447
			// (set) Token: 0x06003383 RID: 13187 RVA: 0x0005AAC8 File Offset: 0x00058CC8
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
