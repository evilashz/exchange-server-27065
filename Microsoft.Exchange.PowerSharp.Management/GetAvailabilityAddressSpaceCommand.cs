using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004E5 RID: 1253
	public class GetAvailabilityAddressSpaceCommand : SyntheticCommandWithPipelineInput<AvailabilityAddressSpace, AvailabilityAddressSpace>
	{
		// Token: 0x060044FC RID: 17660 RVA: 0x000711A7 File Offset: 0x0006F3A7
		private GetAvailabilityAddressSpaceCommand() : base("Get-AvailabilityAddressSpace")
		{
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x000711B4 File Offset: 0x0006F3B4
		public GetAvailabilityAddressSpaceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x000711C3 File Offset: 0x0006F3C3
		public virtual GetAvailabilityAddressSpaceCommand SetParameters(GetAvailabilityAddressSpaceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x000711CD File Offset: 0x0006F3CD
		public virtual GetAvailabilityAddressSpaceCommand SetParameters(GetAvailabilityAddressSpaceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004E6 RID: 1254
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170026EB RID: 9963
			// (set) Token: 0x06004500 RID: 17664 RVA: 0x000711D7 File Offset: 0x0006F3D7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170026EC RID: 9964
			// (set) Token: 0x06004501 RID: 17665 RVA: 0x000711F5 File Offset: 0x0006F3F5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026ED RID: 9965
			// (set) Token: 0x06004502 RID: 17666 RVA: 0x00071208 File Offset: 0x0006F408
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026EE RID: 9966
			// (set) Token: 0x06004503 RID: 17667 RVA: 0x00071220 File Offset: 0x0006F420
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026EF RID: 9967
			// (set) Token: 0x06004504 RID: 17668 RVA: 0x00071238 File Offset: 0x0006F438
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026F0 RID: 9968
			// (set) Token: 0x06004505 RID: 17669 RVA: 0x00071250 File Offset: 0x0006F450
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020004E7 RID: 1255
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170026F1 RID: 9969
			// (set) Token: 0x06004507 RID: 17671 RVA: 0x00071270 File Offset: 0x0006F470
			public virtual AvailabilityAddressSpaceIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170026F2 RID: 9970
			// (set) Token: 0x06004508 RID: 17672 RVA: 0x00071283 File Offset: 0x0006F483
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170026F3 RID: 9971
			// (set) Token: 0x06004509 RID: 17673 RVA: 0x000712A1 File Offset: 0x0006F4A1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026F4 RID: 9972
			// (set) Token: 0x0600450A RID: 17674 RVA: 0x000712B4 File Offset: 0x0006F4B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026F5 RID: 9973
			// (set) Token: 0x0600450B RID: 17675 RVA: 0x000712CC File Offset: 0x0006F4CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026F6 RID: 9974
			// (set) Token: 0x0600450C RID: 17676 RVA: 0x000712E4 File Offset: 0x0006F4E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026F7 RID: 9975
			// (set) Token: 0x0600450D RID: 17677 RVA: 0x000712FC File Offset: 0x0006F4FC
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
