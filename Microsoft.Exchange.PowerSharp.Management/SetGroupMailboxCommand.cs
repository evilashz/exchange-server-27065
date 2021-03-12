using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C42 RID: 3138
	public class SetGroupMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<GroupMailbox>
	{
		// Token: 0x06009954 RID: 39252 RVA: 0x000DEC3B File Offset: 0x000DCE3B
		private SetGroupMailboxCommand() : base("Set-GroupMailbox")
		{
		}

		// Token: 0x06009955 RID: 39253 RVA: 0x000DEC48 File Offset: 0x000DCE48
		public SetGroupMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009956 RID: 39254 RVA: 0x000DEC57 File Offset: 0x000DCE57
		public virtual SetGroupMailboxCommand SetParameters(SetGroupMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009957 RID: 39255 RVA: 0x000DEC61 File Offset: 0x000DCE61
		public virtual SetGroupMailboxCommand SetParameters(SetGroupMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C43 RID: 3139
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006C89 RID: 27785
			// (set) Token: 0x06009958 RID: 39256 RVA: 0x000DEC6B File Offset: 0x000DCE6B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006C8A RID: 27786
			// (set) Token: 0x06009959 RID: 39257 RVA: 0x000DEC7E File Offset: 0x000DCE7E
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006C8B RID: 27787
			// (set) Token: 0x0600995A RID: 39258 RVA: 0x000DEC91 File Offset: 0x000DCE91
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17006C8C RID: 27788
			// (set) Token: 0x0600995B RID: 39259 RVA: 0x000DECA4 File Offset: 0x000DCEA4
			public virtual string ExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ExecutingUser"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17006C8D RID: 27789
			// (set) Token: 0x0600995C RID: 39260 RVA: 0x000DECC2 File Offset: 0x000DCEC2
			public virtual RecipientIdParameter Owners
			{
				set
				{
					base.PowerSharpParameters["Owners"] = value;
				}
			}

			// Token: 0x17006C8E RID: 27790
			// (set) Token: 0x0600995D RID: 39261 RVA: 0x000DECD5 File Offset: 0x000DCED5
			public virtual RecipientIdParameter AddOwners
			{
				set
				{
					base.PowerSharpParameters["AddOwners"] = value;
				}
			}

			// Token: 0x17006C8F RID: 27791
			// (set) Token: 0x0600995E RID: 39262 RVA: 0x000DECE8 File Offset: 0x000DCEE8
			public virtual RecipientIdParameter RemoveOwners
			{
				set
				{
					base.PowerSharpParameters["RemoveOwners"] = value;
				}
			}

			// Token: 0x17006C90 RID: 27792
			// (set) Token: 0x0600995F RID: 39263 RVA: 0x000DECFB File Offset: 0x000DCEFB
			public virtual RecipientIdParameter AddedMembers
			{
				set
				{
					base.PowerSharpParameters["AddedMembers"] = value;
				}
			}

			// Token: 0x17006C91 RID: 27793
			// (set) Token: 0x06009960 RID: 39264 RVA: 0x000DED0E File Offset: 0x000DCF0E
			public virtual RecipientIdParameter RemovedMembers
			{
				set
				{
					base.PowerSharpParameters["RemovedMembers"] = value;
				}
			}

			// Token: 0x17006C92 RID: 27794
			// (set) Token: 0x06009961 RID: 39265 RVA: 0x000DED21 File Offset: 0x000DCF21
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x17006C93 RID: 27795
			// (set) Token: 0x06009962 RID: 39266 RVA: 0x000DED34 File Offset: 0x000DCF34
			public virtual MultiValuedProperty<string> SharePointResources
			{
				set
				{
					base.PowerSharpParameters["SharePointResources"] = value;
				}
			}

			// Token: 0x17006C94 RID: 27796
			// (set) Token: 0x06009963 RID: 39267 RVA: 0x000DED47 File Offset: 0x000DCF47
			public virtual ModernGroupTypeInfo SwitchToGroupType
			{
				set
				{
					base.PowerSharpParameters["SwitchToGroupType"] = value;
				}
			}

			// Token: 0x17006C95 RID: 27797
			// (set) Token: 0x06009964 RID: 39268 RVA: 0x000DED5F File Offset: 0x000DCF5F
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17006C96 RID: 27798
			// (set) Token: 0x06009965 RID: 39269 RVA: 0x000DED77 File Offset: 0x000DCF77
			public virtual string YammerGroupEmailAddress
			{
				set
				{
					base.PowerSharpParameters["YammerGroupEmailAddress"] = value;
				}
			}

			// Token: 0x17006C97 RID: 27799
			// (set) Token: 0x06009966 RID: 39270 RVA: 0x000DED8A File Offset: 0x000DCF8A
			public virtual RecipientIdType RecipientIdType
			{
				set
				{
					base.PowerSharpParameters["RecipientIdType"] = value;
				}
			}

			// Token: 0x17006C98 RID: 27800
			// (set) Token: 0x06009967 RID: 39271 RVA: 0x000DEDA2 File Offset: 0x000DCFA2
			public virtual SwitchParameter FromSyncClient
			{
				set
				{
					base.PowerSharpParameters["FromSyncClient"] = value;
				}
			}

			// Token: 0x17006C99 RID: 27801
			// (set) Token: 0x06009968 RID: 39272 RVA: 0x000DEDBA File Offset: 0x000DCFBA
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006C9A RID: 27802
			// (set) Token: 0x06009969 RID: 39273 RVA: 0x000DEDD2 File Offset: 0x000DCFD2
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006C9B RID: 27803
			// (set) Token: 0x0600996A RID: 39274 RVA: 0x000DEDE5 File Offset: 0x000DCFE5
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006C9C RID: 27804
			// (set) Token: 0x0600996B RID: 39275 RVA: 0x000DEDF8 File Offset: 0x000DCFF8
			public virtual SwitchParameter ForcePublishExternalResources
			{
				set
				{
					base.PowerSharpParameters["ForcePublishExternalResources"] = value;
				}
			}

			// Token: 0x17006C9D RID: 27805
			// (set) Token: 0x0600996C RID: 39276 RVA: 0x000DEE10 File Offset: 0x000DD010
			public virtual MultiValuedProperty<GroupMailboxConfigurationActionType> ConfigurationActions
			{
				set
				{
					base.PowerSharpParameters["ConfigurationActions"] = value;
				}
			}

			// Token: 0x17006C9E RID: 27806
			// (set) Token: 0x0600996D RID: 39277 RVA: 0x000DEE23 File Offset: 0x000DD023
			public virtual CultureInfo Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x17006C9F RID: 27807
			// (set) Token: 0x0600996E RID: 39278 RVA: 0x000DEE36 File Offset: 0x000DD036
			public virtual SwitchParameter AutoSubscribeNewGroupMembers
			{
				set
				{
					base.PowerSharpParameters["AutoSubscribeNewGroupMembers"] = value;
				}
			}

			// Token: 0x17006CA0 RID: 27808
			// (set) Token: 0x0600996F RID: 39279 RVA: 0x000DEE4E File Offset: 0x000DD04E
			public virtual int PermissionsVersion
			{
				set
				{
					base.PowerSharpParameters["PermissionsVersion"] = value;
				}
			}

			// Token: 0x17006CA1 RID: 27809
			// (set) Token: 0x06009970 RID: 39280 RVA: 0x000DEE66 File Offset: 0x000DD066
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006CA2 RID: 27810
			// (set) Token: 0x06009971 RID: 39281 RVA: 0x000DEE79 File Offset: 0x000DD079
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006CA3 RID: 27811
			// (set) Token: 0x06009972 RID: 39282 RVA: 0x000DEE91 File Offset: 0x000DD091
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006CA4 RID: 27812
			// (set) Token: 0x06009973 RID: 39283 RVA: 0x000DEEA9 File Offset: 0x000DD0A9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006CA5 RID: 27813
			// (set) Token: 0x06009974 RID: 39284 RVA: 0x000DEEC1 File Offset: 0x000DD0C1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006CA6 RID: 27814
			// (set) Token: 0x06009975 RID: 39285 RVA: 0x000DEED9 File Offset: 0x000DD0D9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C44 RID: 3140
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006CA7 RID: 27815
			// (set) Token: 0x06009977 RID: 39287 RVA: 0x000DEEF9 File Offset: 0x000DD0F9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17006CA8 RID: 27816
			// (set) Token: 0x06009978 RID: 39288 RVA: 0x000DEF17 File Offset: 0x000DD117
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006CA9 RID: 27817
			// (set) Token: 0x06009979 RID: 39289 RVA: 0x000DEF2A File Offset: 0x000DD12A
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006CAA RID: 27818
			// (set) Token: 0x0600997A RID: 39290 RVA: 0x000DEF3D File Offset: 0x000DD13D
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17006CAB RID: 27819
			// (set) Token: 0x0600997B RID: 39291 RVA: 0x000DEF50 File Offset: 0x000DD150
			public virtual string ExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ExecutingUser"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17006CAC RID: 27820
			// (set) Token: 0x0600997C RID: 39292 RVA: 0x000DEF6E File Offset: 0x000DD16E
			public virtual RecipientIdParameter Owners
			{
				set
				{
					base.PowerSharpParameters["Owners"] = value;
				}
			}

			// Token: 0x17006CAD RID: 27821
			// (set) Token: 0x0600997D RID: 39293 RVA: 0x000DEF81 File Offset: 0x000DD181
			public virtual RecipientIdParameter AddOwners
			{
				set
				{
					base.PowerSharpParameters["AddOwners"] = value;
				}
			}

			// Token: 0x17006CAE RID: 27822
			// (set) Token: 0x0600997E RID: 39294 RVA: 0x000DEF94 File Offset: 0x000DD194
			public virtual RecipientIdParameter RemoveOwners
			{
				set
				{
					base.PowerSharpParameters["RemoveOwners"] = value;
				}
			}

			// Token: 0x17006CAF RID: 27823
			// (set) Token: 0x0600997F RID: 39295 RVA: 0x000DEFA7 File Offset: 0x000DD1A7
			public virtual RecipientIdParameter AddedMembers
			{
				set
				{
					base.PowerSharpParameters["AddedMembers"] = value;
				}
			}

			// Token: 0x17006CB0 RID: 27824
			// (set) Token: 0x06009980 RID: 39296 RVA: 0x000DEFBA File Offset: 0x000DD1BA
			public virtual RecipientIdParameter RemovedMembers
			{
				set
				{
					base.PowerSharpParameters["RemovedMembers"] = value;
				}
			}

			// Token: 0x17006CB1 RID: 27825
			// (set) Token: 0x06009981 RID: 39297 RVA: 0x000DEFCD File Offset: 0x000DD1CD
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x17006CB2 RID: 27826
			// (set) Token: 0x06009982 RID: 39298 RVA: 0x000DEFE0 File Offset: 0x000DD1E0
			public virtual MultiValuedProperty<string> SharePointResources
			{
				set
				{
					base.PowerSharpParameters["SharePointResources"] = value;
				}
			}

			// Token: 0x17006CB3 RID: 27827
			// (set) Token: 0x06009983 RID: 39299 RVA: 0x000DEFF3 File Offset: 0x000DD1F3
			public virtual ModernGroupTypeInfo SwitchToGroupType
			{
				set
				{
					base.PowerSharpParameters["SwitchToGroupType"] = value;
				}
			}

			// Token: 0x17006CB4 RID: 27828
			// (set) Token: 0x06009984 RID: 39300 RVA: 0x000DF00B File Offset: 0x000DD20B
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17006CB5 RID: 27829
			// (set) Token: 0x06009985 RID: 39301 RVA: 0x000DF023 File Offset: 0x000DD223
			public virtual string YammerGroupEmailAddress
			{
				set
				{
					base.PowerSharpParameters["YammerGroupEmailAddress"] = value;
				}
			}

			// Token: 0x17006CB6 RID: 27830
			// (set) Token: 0x06009986 RID: 39302 RVA: 0x000DF036 File Offset: 0x000DD236
			public virtual RecipientIdType RecipientIdType
			{
				set
				{
					base.PowerSharpParameters["RecipientIdType"] = value;
				}
			}

			// Token: 0x17006CB7 RID: 27831
			// (set) Token: 0x06009987 RID: 39303 RVA: 0x000DF04E File Offset: 0x000DD24E
			public virtual SwitchParameter FromSyncClient
			{
				set
				{
					base.PowerSharpParameters["FromSyncClient"] = value;
				}
			}

			// Token: 0x17006CB8 RID: 27832
			// (set) Token: 0x06009988 RID: 39304 RVA: 0x000DF066 File Offset: 0x000DD266
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006CB9 RID: 27833
			// (set) Token: 0x06009989 RID: 39305 RVA: 0x000DF07E File Offset: 0x000DD27E
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006CBA RID: 27834
			// (set) Token: 0x0600998A RID: 39306 RVA: 0x000DF091 File Offset: 0x000DD291
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006CBB RID: 27835
			// (set) Token: 0x0600998B RID: 39307 RVA: 0x000DF0A4 File Offset: 0x000DD2A4
			public virtual SwitchParameter ForcePublishExternalResources
			{
				set
				{
					base.PowerSharpParameters["ForcePublishExternalResources"] = value;
				}
			}

			// Token: 0x17006CBC RID: 27836
			// (set) Token: 0x0600998C RID: 39308 RVA: 0x000DF0BC File Offset: 0x000DD2BC
			public virtual MultiValuedProperty<GroupMailboxConfigurationActionType> ConfigurationActions
			{
				set
				{
					base.PowerSharpParameters["ConfigurationActions"] = value;
				}
			}

			// Token: 0x17006CBD RID: 27837
			// (set) Token: 0x0600998D RID: 39309 RVA: 0x000DF0CF File Offset: 0x000DD2CF
			public virtual CultureInfo Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x17006CBE RID: 27838
			// (set) Token: 0x0600998E RID: 39310 RVA: 0x000DF0E2 File Offset: 0x000DD2E2
			public virtual SwitchParameter AutoSubscribeNewGroupMembers
			{
				set
				{
					base.PowerSharpParameters["AutoSubscribeNewGroupMembers"] = value;
				}
			}

			// Token: 0x17006CBF RID: 27839
			// (set) Token: 0x0600998F RID: 39311 RVA: 0x000DF0FA File Offset: 0x000DD2FA
			public virtual int PermissionsVersion
			{
				set
				{
					base.PowerSharpParameters["PermissionsVersion"] = value;
				}
			}

			// Token: 0x17006CC0 RID: 27840
			// (set) Token: 0x06009990 RID: 39312 RVA: 0x000DF112 File Offset: 0x000DD312
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006CC1 RID: 27841
			// (set) Token: 0x06009991 RID: 39313 RVA: 0x000DF125 File Offset: 0x000DD325
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006CC2 RID: 27842
			// (set) Token: 0x06009992 RID: 39314 RVA: 0x000DF13D File Offset: 0x000DD33D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006CC3 RID: 27843
			// (set) Token: 0x06009993 RID: 39315 RVA: 0x000DF155 File Offset: 0x000DD355
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006CC4 RID: 27844
			// (set) Token: 0x06009994 RID: 39316 RVA: 0x000DF16D File Offset: 0x000DD36D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006CC5 RID: 27845
			// (set) Token: 0x06009995 RID: 39317 RVA: 0x000DF185 File Offset: 0x000DD385
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
