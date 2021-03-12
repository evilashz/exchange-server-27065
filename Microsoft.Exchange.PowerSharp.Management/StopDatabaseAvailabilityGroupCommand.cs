using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200057E RID: 1406
	public class StopDatabaseAvailabilityGroupCommand : SyntheticCommandWithPipelineInputNoOutput<DatabaseAvailabilityGroupIdParameter>
	{
		// Token: 0x060049F0 RID: 18928 RVA: 0x00077487 File Offset: 0x00075687
		private StopDatabaseAvailabilityGroupCommand() : base("Stop-DatabaseAvailabilityGroup")
		{
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x00077494 File Offset: 0x00075694
		public StopDatabaseAvailabilityGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x000774A3 File Offset: 0x000756A3
		public virtual StopDatabaseAvailabilityGroupCommand SetParameters(StopDatabaseAvailabilityGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x000774AD File Offset: 0x000756AD
		public virtual StopDatabaseAvailabilityGroupCommand SetParameters(StopDatabaseAvailabilityGroupCommand.MailboxSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x000774B7 File Offset: 0x000756B7
		public virtual StopDatabaseAvailabilityGroupCommand SetParameters(StopDatabaseAvailabilityGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200057F RID: 1407
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002AAD RID: 10925
			// (set) Token: 0x060049F5 RID: 18933 RVA: 0x000774C1 File Offset: 0x000756C1
			public virtual DatabaseAvailabilityGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002AAE RID: 10926
			// (set) Token: 0x060049F6 RID: 18934 RVA: 0x000774D4 File Offset: 0x000756D4
			public virtual string ActiveDirectorySite
			{
				set
				{
					base.PowerSharpParameters["ActiveDirectorySite"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x17002AAF RID: 10927
			// (set) Token: 0x060049F7 RID: 18935 RVA: 0x000774F2 File Offset: 0x000756F2
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x17002AB0 RID: 10928
			// (set) Token: 0x060049F8 RID: 18936 RVA: 0x0007750A File Offset: 0x0007570A
			public virtual SwitchParameter QuorumOnly
			{
				set
				{
					base.PowerSharpParameters["QuorumOnly"] = value;
				}
			}

			// Token: 0x17002AB1 RID: 10929
			// (set) Token: 0x060049F9 RID: 18937 RVA: 0x00077522 File Offset: 0x00075722
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002AB2 RID: 10930
			// (set) Token: 0x060049FA RID: 18938 RVA: 0x00077535 File Offset: 0x00075735
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002AB3 RID: 10931
			// (set) Token: 0x060049FB RID: 18939 RVA: 0x0007754D File Offset: 0x0007574D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002AB4 RID: 10932
			// (set) Token: 0x060049FC RID: 18940 RVA: 0x00077565 File Offset: 0x00075765
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002AB5 RID: 10933
			// (set) Token: 0x060049FD RID: 18941 RVA: 0x0007757D File Offset: 0x0007577D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002AB6 RID: 10934
			// (set) Token: 0x060049FE RID: 18942 RVA: 0x00077595 File Offset: 0x00075795
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002AB7 RID: 10935
			// (set) Token: 0x060049FF RID: 18943 RVA: 0x000775AD File Offset: 0x000757AD
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000580 RID: 1408
		public class MailboxSetParameters : ParametersBase
		{
			// Token: 0x17002AB8 RID: 10936
			// (set) Token: 0x06004A01 RID: 18945 RVA: 0x000775CD File Offset: 0x000757CD
			public virtual DatabaseAvailabilityGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002AB9 RID: 10937
			// (set) Token: 0x06004A02 RID: 18946 RVA: 0x000775E0 File Offset: 0x000757E0
			public virtual MailboxServerIdParameter MailboxServer
			{
				set
				{
					base.PowerSharpParameters["MailboxServer"] = value;
				}
			}

			// Token: 0x17002ABA RID: 10938
			// (set) Token: 0x06004A03 RID: 18947 RVA: 0x000775F3 File Offset: 0x000757F3
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x17002ABB RID: 10939
			// (set) Token: 0x06004A04 RID: 18948 RVA: 0x0007760B File Offset: 0x0007580B
			public virtual SwitchParameter QuorumOnly
			{
				set
				{
					base.PowerSharpParameters["QuorumOnly"] = value;
				}
			}

			// Token: 0x17002ABC RID: 10940
			// (set) Token: 0x06004A05 RID: 18949 RVA: 0x00077623 File Offset: 0x00075823
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002ABD RID: 10941
			// (set) Token: 0x06004A06 RID: 18950 RVA: 0x00077636 File Offset: 0x00075836
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002ABE RID: 10942
			// (set) Token: 0x06004A07 RID: 18951 RVA: 0x0007764E File Offset: 0x0007584E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002ABF RID: 10943
			// (set) Token: 0x06004A08 RID: 18952 RVA: 0x00077666 File Offset: 0x00075866
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002AC0 RID: 10944
			// (set) Token: 0x06004A09 RID: 18953 RVA: 0x0007767E File Offset: 0x0007587E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002AC1 RID: 10945
			// (set) Token: 0x06004A0A RID: 18954 RVA: 0x00077696 File Offset: 0x00075896
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002AC2 RID: 10946
			// (set) Token: 0x06004A0B RID: 18955 RVA: 0x000776AE File Offset: 0x000758AE
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000581 RID: 1409
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002AC3 RID: 10947
			// (set) Token: 0x06004A0D RID: 18957 RVA: 0x000776CE File Offset: 0x000758CE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002AC4 RID: 10948
			// (set) Token: 0x06004A0E RID: 18958 RVA: 0x000776E1 File Offset: 0x000758E1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002AC5 RID: 10949
			// (set) Token: 0x06004A0F RID: 18959 RVA: 0x000776F9 File Offset: 0x000758F9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002AC6 RID: 10950
			// (set) Token: 0x06004A10 RID: 18960 RVA: 0x00077711 File Offset: 0x00075911
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002AC7 RID: 10951
			// (set) Token: 0x06004A11 RID: 18961 RVA: 0x00077729 File Offset: 0x00075929
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002AC8 RID: 10952
			// (set) Token: 0x06004A12 RID: 18962 RVA: 0x00077741 File Offset: 0x00075941
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002AC9 RID: 10953
			// (set) Token: 0x06004A13 RID: 18963 RVA: 0x00077759 File Offset: 0x00075959
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
