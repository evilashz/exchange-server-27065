using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200042F RID: 1071
	public class GetNotificationCommand : SyntheticCommandWithPipelineInput<AsyncOperationNotification, AsyncOperationNotification>
	{
		// Token: 0x06003E65 RID: 15973 RVA: 0x00068BCB File Offset: 0x00066DCB
		private GetNotificationCommand() : base("Get-Notification")
		{
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x00068BD8 File Offset: 0x00066DD8
		public GetNotificationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x00068BE7 File Offset: 0x00066DE7
		public virtual GetNotificationCommand SetParameters(GetNotificationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x00068BF1 File Offset: 0x00066DF1
		public virtual GetNotificationCommand SetParameters(GetNotificationCommand.FilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x00068BFB File Offset: 0x00066DFB
		public virtual GetNotificationCommand SetParameters(GetNotificationCommand.SettingsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x00068C05 File Offset: 0x00066E05
		public virtual GetNotificationCommand SetParameters(GetNotificationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000430 RID: 1072
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170021C0 RID: 8640
			// (set) Token: 0x06003E6B RID: 15979 RVA: 0x00068C0F File Offset: 0x00066E0F
			public virtual SwitchParameter Summary
			{
				set
				{
					base.PowerSharpParameters["Summary"] = value;
				}
			}

			// Token: 0x170021C1 RID: 8641
			// (set) Token: 0x06003E6C RID: 15980 RVA: 0x00068C27 File Offset: 0x00066E27
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new EwsStoreObjectIdParameter(value) : null);
				}
			}

			// Token: 0x170021C2 RID: 8642
			// (set) Token: 0x06003E6D RID: 15981 RVA: 0x00068C45 File Offset: 0x00066E45
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170021C3 RID: 8643
			// (set) Token: 0x06003E6E RID: 15982 RVA: 0x00068C63 File Offset: 0x00066E63
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170021C4 RID: 8644
			// (set) Token: 0x06003E6F RID: 15983 RVA: 0x00068C76 File Offset: 0x00066E76
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021C5 RID: 8645
			// (set) Token: 0x06003E70 RID: 15984 RVA: 0x00068C8E File Offset: 0x00066E8E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021C6 RID: 8646
			// (set) Token: 0x06003E71 RID: 15985 RVA: 0x00068CA6 File Offset: 0x00066EA6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021C7 RID: 8647
			// (set) Token: 0x06003E72 RID: 15986 RVA: 0x00068CBE File Offset: 0x00066EBE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000431 RID: 1073
		public class FilterParameters : ParametersBase
		{
			// Token: 0x170021C8 RID: 8648
			// (set) Token: 0x06003E74 RID: 15988 RVA: 0x00068CDE File Offset: 0x00066EDE
			public virtual SwitchParameter Summary
			{
				set
				{
					base.PowerSharpParameters["Summary"] = value;
				}
			}

			// Token: 0x170021C9 RID: 8649
			// (set) Token: 0x06003E75 RID: 15989 RVA: 0x00068CF6 File Offset: 0x00066EF6
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170021CA RID: 8650
			// (set) Token: 0x06003E76 RID: 15990 RVA: 0x00068D0E File Offset: 0x00066F0E
			public virtual ExDateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x170021CB RID: 8651
			// (set) Token: 0x06003E77 RID: 15991 RVA: 0x00068D26 File Offset: 0x00066F26
			public virtual AsyncOperationType ProcessType
			{
				set
				{
					base.PowerSharpParameters["ProcessType"] = value;
				}
			}

			// Token: 0x170021CC RID: 8652
			// (set) Token: 0x06003E78 RID: 15992 RVA: 0x00068D3E File Offset: 0x00066F3E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170021CD RID: 8653
			// (set) Token: 0x06003E79 RID: 15993 RVA: 0x00068D5C File Offset: 0x00066F5C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170021CE RID: 8654
			// (set) Token: 0x06003E7A RID: 15994 RVA: 0x00068D6F File Offset: 0x00066F6F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021CF RID: 8655
			// (set) Token: 0x06003E7B RID: 15995 RVA: 0x00068D87 File Offset: 0x00066F87
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021D0 RID: 8656
			// (set) Token: 0x06003E7C RID: 15996 RVA: 0x00068D9F File Offset: 0x00066F9F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021D1 RID: 8657
			// (set) Token: 0x06003E7D RID: 15997 RVA: 0x00068DB7 File Offset: 0x00066FB7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000432 RID: 1074
		public class SettingsParameters : ParametersBase
		{
			// Token: 0x170021D2 RID: 8658
			// (set) Token: 0x06003E7F RID: 15999 RVA: 0x00068DD7 File Offset: 0x00066FD7
			public virtual SwitchParameter Settings
			{
				set
				{
					base.PowerSharpParameters["Settings"] = value;
				}
			}

			// Token: 0x170021D3 RID: 8659
			// (set) Token: 0x06003E80 RID: 16000 RVA: 0x00068DEF File Offset: 0x00066FEF
			public virtual AsyncOperationType ProcessType
			{
				set
				{
					base.PowerSharpParameters["ProcessType"] = value;
				}
			}

			// Token: 0x170021D4 RID: 8660
			// (set) Token: 0x06003E81 RID: 16001 RVA: 0x00068E07 File Offset: 0x00067007
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170021D5 RID: 8661
			// (set) Token: 0x06003E82 RID: 16002 RVA: 0x00068E25 File Offset: 0x00067025
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170021D6 RID: 8662
			// (set) Token: 0x06003E83 RID: 16003 RVA: 0x00068E38 File Offset: 0x00067038
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021D7 RID: 8663
			// (set) Token: 0x06003E84 RID: 16004 RVA: 0x00068E50 File Offset: 0x00067050
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021D8 RID: 8664
			// (set) Token: 0x06003E85 RID: 16005 RVA: 0x00068E68 File Offset: 0x00067068
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021D9 RID: 8665
			// (set) Token: 0x06003E86 RID: 16006 RVA: 0x00068E80 File Offset: 0x00067080
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000433 RID: 1075
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170021DA RID: 8666
			// (set) Token: 0x06003E88 RID: 16008 RVA: 0x00068EA0 File Offset: 0x000670A0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170021DB RID: 8667
			// (set) Token: 0x06003E89 RID: 16009 RVA: 0x00068EBE File Offset: 0x000670BE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170021DC RID: 8668
			// (set) Token: 0x06003E8A RID: 16010 RVA: 0x00068ED1 File Offset: 0x000670D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021DD RID: 8669
			// (set) Token: 0x06003E8B RID: 16011 RVA: 0x00068EE9 File Offset: 0x000670E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021DE RID: 8670
			// (set) Token: 0x06003E8C RID: 16012 RVA: 0x00068F01 File Offset: 0x00067101
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021DF RID: 8671
			// (set) Token: 0x06003E8D RID: 16013 RVA: 0x00068F19 File Offset: 0x00067119
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
