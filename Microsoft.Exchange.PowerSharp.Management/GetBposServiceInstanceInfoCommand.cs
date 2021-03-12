using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000111 RID: 273
	public class GetBposServiceInstanceInfoCommand : SyntheticCommandWithPipelineInputNoOutput<ServiceInstanceId>
	{
		// Token: 0x06001F34 RID: 7988 RVA: 0x00040326 File Offset: 0x0003E526
		private GetBposServiceInstanceInfoCommand() : base("Get-BposServiceInstanceInfo")
		{
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00040333 File Offset: 0x0003E533
		public GetBposServiceInstanceInfoCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00040342 File Offset: 0x0003E542
		public virtual GetBposServiceInstanceInfoCommand SetParameters(GetBposServiceInstanceInfoCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000112 RID: 274
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170008CB RID: 2251
			// (set) Token: 0x06001F37 RID: 7991 RVA: 0x0004034C File Offset: 0x0003E54C
			public virtual ServiceInstanceId Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170008CC RID: 2252
			// (set) Token: 0x06001F38 RID: 7992 RVA: 0x0004035F File Offset: 0x0003E55F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008CD RID: 2253
			// (set) Token: 0x06001F39 RID: 7993 RVA: 0x00040377 File Offset: 0x0003E577
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008CE RID: 2254
			// (set) Token: 0x06001F3A RID: 7994 RVA: 0x0004038F File Offset: 0x0003E58F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008CF RID: 2255
			// (set) Token: 0x06001F3B RID: 7995 RVA: 0x000403A7 File Offset: 0x0003E5A7
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
