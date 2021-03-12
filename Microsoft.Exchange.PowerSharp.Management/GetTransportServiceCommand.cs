using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000645 RID: 1605
	public class GetTransportServiceCommand : SyntheticCommand<object>
	{
		// Token: 0x0600510D RID: 20749 RVA: 0x00080382 File Offset: 0x0007E582
		private GetTransportServiceCommand() : base("Get-TransportService")
		{
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x0008038F File Offset: 0x0007E58F
		public GetTransportServiceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x0008039E File Offset: 0x0007E59E
		public virtual GetTransportServiceCommand SetParameters(GetTransportServiceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x000803A8 File Offset: 0x0007E5A8
		public virtual GetTransportServiceCommand SetParameters(GetTransportServiceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000646 RID: 1606
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700303C RID: 12348
			// (set) Token: 0x06005111 RID: 20753 RVA: 0x000803B2 File Offset: 0x0007E5B2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700303D RID: 12349
			// (set) Token: 0x06005112 RID: 20754 RVA: 0x000803C5 File Offset: 0x0007E5C5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700303E RID: 12350
			// (set) Token: 0x06005113 RID: 20755 RVA: 0x000803DD File Offset: 0x0007E5DD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700303F RID: 12351
			// (set) Token: 0x06005114 RID: 20756 RVA: 0x000803F5 File Offset: 0x0007E5F5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003040 RID: 12352
			// (set) Token: 0x06005115 RID: 20757 RVA: 0x0008040D File Offset: 0x0007E60D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000647 RID: 1607
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003041 RID: 12353
			// (set) Token: 0x06005117 RID: 20759 RVA: 0x0008042D File Offset: 0x0007E62D
			public virtual TransportServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003042 RID: 12354
			// (set) Token: 0x06005118 RID: 20760 RVA: 0x00080440 File Offset: 0x0007E640
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003043 RID: 12355
			// (set) Token: 0x06005119 RID: 20761 RVA: 0x00080453 File Offset: 0x0007E653
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003044 RID: 12356
			// (set) Token: 0x0600511A RID: 20762 RVA: 0x0008046B File Offset: 0x0007E66B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003045 RID: 12357
			// (set) Token: 0x0600511B RID: 20763 RVA: 0x00080483 File Offset: 0x0007E683
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003046 RID: 12358
			// (set) Token: 0x0600511C RID: 20764 RVA: 0x0008049B File Offset: 0x0007E69B
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
