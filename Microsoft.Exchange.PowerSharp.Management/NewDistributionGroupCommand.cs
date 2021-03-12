using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C09 RID: 3081
	public class NewDistributionGroupCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06009586 RID: 38278 RVA: 0x000D9D05 File Offset: 0x000D7F05
		private NewDistributionGroupCommand() : base("New-DistributionGroup")
		{
		}

		// Token: 0x06009587 RID: 38279 RVA: 0x000D9D12 File Offset: 0x000D7F12
		public NewDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009588 RID: 38280 RVA: 0x000D9D21 File Offset: 0x000D7F21
		public virtual NewDistributionGroupCommand SetParameters(NewDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C0A RID: 3082
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700692D RID: 26925
			// (set) Token: 0x06009589 RID: 38281 RVA: 0x000D9D2B File Offset: 0x000D7F2B
			public virtual GroupType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x1700692E RID: 26926
			// (set) Token: 0x0600958A RID: 38282 RVA: 0x000D9D43 File Offset: 0x000D7F43
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700692F RID: 26927
			// (set) Token: 0x0600958B RID: 38283 RVA: 0x000D9D56 File Offset: 0x000D7F56
			public virtual MultiValuedProperty<GeneralRecipientIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17006930 RID: 26928
			// (set) Token: 0x0600958C RID: 38284 RVA: 0x000D9D69 File Offset: 0x000D7F69
			public virtual MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x17006931 RID: 26929
			// (set) Token: 0x0600958D RID: 38285 RVA: 0x000D9D7C File Offset: 0x000D7F7C
			public virtual MemberUpdateType MemberJoinRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberJoinRestriction"] = value;
				}
			}

			// Token: 0x17006932 RID: 26930
			// (set) Token: 0x0600958E RID: 38286 RVA: 0x000D9D94 File Offset: 0x000D7F94
			public virtual MemberUpdateType MemberDepartRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberDepartRestriction"] = value;
				}
			}

			// Token: 0x17006933 RID: 26931
			// (set) Token: 0x0600958F RID: 38287 RVA: 0x000D9DAC File Offset: 0x000D7FAC
			public virtual bool BypassNestedModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["BypassNestedModerationEnabled"] = value;
				}
			}

			// Token: 0x17006934 RID: 26932
			// (set) Token: 0x06009590 RID: 38288 RVA: 0x000D9DC4 File Offset: 0x000D7FC4
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006935 RID: 26933
			// (set) Token: 0x06009591 RID: 38289 RVA: 0x000D9DD7 File Offset: 0x000D7FD7
			public virtual SwitchParameter CopyOwnerToMember
			{
				set
				{
					base.PowerSharpParameters["CopyOwnerToMember"] = value;
				}
			}

			// Token: 0x17006936 RID: 26934
			// (set) Token: 0x06009592 RID: 38290 RVA: 0x000D9DEF File Offset: 0x000D7FEF
			public virtual SwitchParameter RoomList
			{
				set
				{
					base.PowerSharpParameters["RoomList"] = value;
				}
			}

			// Token: 0x17006937 RID: 26935
			// (set) Token: 0x06009593 RID: 38291 RVA: 0x000D9E07 File Offset: 0x000D8007
			public virtual SwitchParameter IgnoreNamingPolicy
			{
				set
				{
					base.PowerSharpParameters["IgnoreNamingPolicy"] = value;
				}
			}

			// Token: 0x17006938 RID: 26936
			// (set) Token: 0x06009594 RID: 38292 RVA: 0x000D9E1F File Offset: 0x000D801F
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006939 RID: 26937
			// (set) Token: 0x06009595 RID: 38293 RVA: 0x000D9E3D File Offset: 0x000D803D
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700693A RID: 26938
			// (set) Token: 0x06009596 RID: 38294 RVA: 0x000D9E50 File Offset: 0x000D8050
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700693B RID: 26939
			// (set) Token: 0x06009597 RID: 38295 RVA: 0x000D9E68 File Offset: 0x000D8068
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700693C RID: 26940
			// (set) Token: 0x06009598 RID: 38296 RVA: 0x000D9E80 File Offset: 0x000D8080
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700693D RID: 26941
			// (set) Token: 0x06009599 RID: 38297 RVA: 0x000D9E98 File Offset: 0x000D8098
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700693E RID: 26942
			// (set) Token: 0x0600959A RID: 38298 RVA: 0x000D9EAB File Offset: 0x000D80AB
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700693F RID: 26943
			// (set) Token: 0x0600959B RID: 38299 RVA: 0x000D9EC3 File Offset: 0x000D80C3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006940 RID: 26944
			// (set) Token: 0x0600959C RID: 38300 RVA: 0x000D9ED6 File Offset: 0x000D80D6
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006941 RID: 26945
			// (set) Token: 0x0600959D RID: 38301 RVA: 0x000D9EE9 File Offset: 0x000D80E9
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006942 RID: 26946
			// (set) Token: 0x0600959E RID: 38302 RVA: 0x000D9F07 File Offset: 0x000D8107
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006943 RID: 26947
			// (set) Token: 0x0600959F RID: 38303 RVA: 0x000D9F1A File Offset: 0x000D811A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006944 RID: 26948
			// (set) Token: 0x060095A0 RID: 38304 RVA: 0x000D9F38 File Offset: 0x000D8138
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006945 RID: 26949
			// (set) Token: 0x060095A1 RID: 38305 RVA: 0x000D9F4B File Offset: 0x000D814B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006946 RID: 26950
			// (set) Token: 0x060095A2 RID: 38306 RVA: 0x000D9F63 File Offset: 0x000D8163
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006947 RID: 26951
			// (set) Token: 0x060095A3 RID: 38307 RVA: 0x000D9F7B File Offset: 0x000D817B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006948 RID: 26952
			// (set) Token: 0x060095A4 RID: 38308 RVA: 0x000D9F93 File Offset: 0x000D8193
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006949 RID: 26953
			// (set) Token: 0x060095A5 RID: 38309 RVA: 0x000D9FAB File Offset: 0x000D81AB
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
