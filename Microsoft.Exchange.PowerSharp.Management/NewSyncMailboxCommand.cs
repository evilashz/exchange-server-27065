using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D81 RID: 3457
	public class NewSyncMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600B869 RID: 47209 RVA: 0x0010917A File Offset: 0x0010737A
		private NewSyncMailboxCommand() : base("New-SyncMailbox")
		{
		}

		// Token: 0x0600B86A RID: 47210 RVA: 0x00109187 File Offset: 0x00107387
		public NewSyncMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B86B RID: 47211 RVA: 0x00109196 File Offset: 0x00107396
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B86C RID: 47212 RVA: 0x001091A0 File Offset: 0x001073A0
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.LinkedWithSyncMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B86D RID: 47213 RVA: 0x001091AA File Offset: 0x001073AA
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.RoomParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B86E RID: 47214 RVA: 0x001091B4 File Offset: 0x001073B4
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.EquipmentParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B86F RID: 47215 RVA: 0x001091BE File Offset: 0x001073BE
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.WindowsLiveCustomDomainsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B870 RID: 47216 RVA: 0x001091C8 File Offset: 0x001073C8
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.FederatedUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B871 RID: 47217 RVA: 0x001091D2 File Offset: 0x001073D2
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.MicrosoftOnlineServicesIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B872 RID: 47218 RVA: 0x001091DC File Offset: 0x001073DC
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.UserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B873 RID: 47219 RVA: 0x001091E6 File Offset: 0x001073E6
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.WindowsLiveIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B874 RID: 47220 RVA: 0x001091F0 File Offset: 0x001073F0
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.DisabledUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B875 RID: 47221 RVA: 0x001091FA File Offset: 0x001073FA
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.MicrosoftOnlineServicesFederatedUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B876 RID: 47222 RVA: 0x00109204 File Offset: 0x00107404
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.ImportLiveIdParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B877 RID: 47223 RVA: 0x0010920E File Offset: 0x0010740E
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.RemoteArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B878 RID: 47224 RVA: 0x00109218 File Offset: 0x00107418
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.MailboxPlanParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B879 RID: 47225 RVA: 0x00109222 File Offset: 0x00107422
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.RemovedMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B87A RID: 47226 RVA: 0x0010922C File Offset: 0x0010742C
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.DiscoveryParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B87B RID: 47227 RVA: 0x00109236 File Offset: 0x00107436
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.TeamMailboxIWParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B87C RID: 47228 RVA: 0x00109240 File Offset: 0x00107440
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.ArbitrationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B87D RID: 47229 RVA: 0x0010924A File Offset: 0x0010744A
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.AuxMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B87E RID: 47230 RVA: 0x00109254 File Offset: 0x00107454
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.TeamMailboxITProParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B87F RID: 47231 RVA: 0x0010925E File Offset: 0x0010745E
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.LinkedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B880 RID: 47232 RVA: 0x00109268 File Offset: 0x00107468
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.SharedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B881 RID: 47233 RVA: 0x00109272 File Offset: 0x00107472
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.LinkedRoomMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B882 RID: 47234 RVA: 0x0010927C File Offset: 0x0010747C
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.EnableRoomMailboxAccountParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B883 RID: 47235 RVA: 0x00109286 File Offset: 0x00107486
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.AuditLogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B884 RID: 47236 RVA: 0x00109290 File Offset: 0x00107490
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.GroupMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B885 RID: 47237 RVA: 0x0010929A File Offset: 0x0010749A
		public virtual NewSyncMailboxCommand SetParameters(NewSyncMailboxCommand.PublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D82 RID: 3458
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008920 RID: 35104
			// (set) Token: 0x0600B886 RID: 47238 RVA: 0x001092A4 File Offset: 0x001074A4
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008921 RID: 35105
			// (set) Token: 0x0600B887 RID: 47239 RVA: 0x001092BC File Offset: 0x001074BC
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008922 RID: 35106
			// (set) Token: 0x0600B888 RID: 47240 RVA: 0x001092CF File Offset: 0x001074CF
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008923 RID: 35107
			// (set) Token: 0x0600B889 RID: 47241 RVA: 0x001092E2 File Offset: 0x001074E2
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008924 RID: 35108
			// (set) Token: 0x0600B88A RID: 47242 RVA: 0x001092F5 File Offset: 0x001074F5
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008925 RID: 35109
			// (set) Token: 0x0600B88B RID: 47243 RVA: 0x00109308 File Offset: 0x00107508
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008926 RID: 35110
			// (set) Token: 0x0600B88C RID: 47244 RVA: 0x0010931B File Offset: 0x0010751B
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008927 RID: 35111
			// (set) Token: 0x0600B88D RID: 47245 RVA: 0x0010932E File Offset: 0x0010752E
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008928 RID: 35112
			// (set) Token: 0x0600B88E RID: 47246 RVA: 0x00109341 File Offset: 0x00107541
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008929 RID: 35113
			// (set) Token: 0x0600B88F RID: 47247 RVA: 0x00109359 File Offset: 0x00107559
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x1700892A RID: 35114
			// (set) Token: 0x0600B890 RID: 47248 RVA: 0x0010936C File Offset: 0x0010756C
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x1700892B RID: 35115
			// (set) Token: 0x0600B891 RID: 47249 RVA: 0x00109384 File Offset: 0x00107584
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700892C RID: 35116
			// (set) Token: 0x0600B892 RID: 47250 RVA: 0x00109397 File Offset: 0x00107597
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700892D RID: 35117
			// (set) Token: 0x0600B893 RID: 47251 RVA: 0x001093AA File Offset: 0x001075AA
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700892E RID: 35118
			// (set) Token: 0x0600B894 RID: 47252 RVA: 0x001093BD File Offset: 0x001075BD
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700892F RID: 35119
			// (set) Token: 0x0600B895 RID: 47253 RVA: 0x001093D0 File Offset: 0x001075D0
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008930 RID: 35120
			// (set) Token: 0x0600B896 RID: 47254 RVA: 0x001093E3 File Offset: 0x001075E3
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008931 RID: 35121
			// (set) Token: 0x0600B897 RID: 47255 RVA: 0x001093F6 File Offset: 0x001075F6
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008932 RID: 35122
			// (set) Token: 0x0600B898 RID: 47256 RVA: 0x00109409 File Offset: 0x00107609
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008933 RID: 35123
			// (set) Token: 0x0600B899 RID: 47257 RVA: 0x0010941C File Offset: 0x0010761C
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008934 RID: 35124
			// (set) Token: 0x0600B89A RID: 47258 RVA: 0x0010942F File Offset: 0x0010762F
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008935 RID: 35125
			// (set) Token: 0x0600B89B RID: 47259 RVA: 0x00109442 File Offset: 0x00107642
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008936 RID: 35126
			// (set) Token: 0x0600B89C RID: 47260 RVA: 0x00109455 File Offset: 0x00107655
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008937 RID: 35127
			// (set) Token: 0x0600B89D RID: 47261 RVA: 0x00109468 File Offset: 0x00107668
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008938 RID: 35128
			// (set) Token: 0x0600B89E RID: 47262 RVA: 0x0010947B File Offset: 0x0010767B
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008939 RID: 35129
			// (set) Token: 0x0600B89F RID: 47263 RVA: 0x0010948E File Offset: 0x0010768E
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x1700893A RID: 35130
			// (set) Token: 0x0600B8A0 RID: 47264 RVA: 0x001094A1 File Offset: 0x001076A1
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700893B RID: 35131
			// (set) Token: 0x0600B8A1 RID: 47265 RVA: 0x001094B4 File Offset: 0x001076B4
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700893C RID: 35132
			// (set) Token: 0x0600B8A2 RID: 47266 RVA: 0x001094C7 File Offset: 0x001076C7
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700893D RID: 35133
			// (set) Token: 0x0600B8A3 RID: 47267 RVA: 0x001094DA File Offset: 0x001076DA
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700893E RID: 35134
			// (set) Token: 0x0600B8A4 RID: 47268 RVA: 0x001094ED File Offset: 0x001076ED
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700893F RID: 35135
			// (set) Token: 0x0600B8A5 RID: 47269 RVA: 0x00109500 File Offset: 0x00107700
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008940 RID: 35136
			// (set) Token: 0x0600B8A6 RID: 47270 RVA: 0x00109513 File Offset: 0x00107713
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008941 RID: 35137
			// (set) Token: 0x0600B8A7 RID: 47271 RVA: 0x00109526 File Offset: 0x00107726
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008942 RID: 35138
			// (set) Token: 0x0600B8A8 RID: 47272 RVA: 0x00109539 File Offset: 0x00107739
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008943 RID: 35139
			// (set) Token: 0x0600B8A9 RID: 47273 RVA: 0x00109551 File Offset: 0x00107751
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008944 RID: 35140
			// (set) Token: 0x0600B8AA RID: 47274 RVA: 0x00109564 File Offset: 0x00107764
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008945 RID: 35141
			// (set) Token: 0x0600B8AB RID: 47275 RVA: 0x0010957C File Offset: 0x0010777C
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008946 RID: 35142
			// (set) Token: 0x0600B8AC RID: 47276 RVA: 0x0010958F File Offset: 0x0010778F
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008947 RID: 35143
			// (set) Token: 0x0600B8AD RID: 47277 RVA: 0x001095A7 File Offset: 0x001077A7
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008948 RID: 35144
			// (set) Token: 0x0600B8AE RID: 47278 RVA: 0x001095BF File Offset: 0x001077BF
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008949 RID: 35145
			// (set) Token: 0x0600B8AF RID: 47279 RVA: 0x001095D7 File Offset: 0x001077D7
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x1700894A RID: 35146
			// (set) Token: 0x0600B8B0 RID: 47280 RVA: 0x001095EF File Offset: 0x001077EF
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x1700894B RID: 35147
			// (set) Token: 0x0600B8B1 RID: 47281 RVA: 0x00109607 File Offset: 0x00107807
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x1700894C RID: 35148
			// (set) Token: 0x0600B8B2 RID: 47282 RVA: 0x0010961F File Offset: 0x0010781F
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x1700894D RID: 35149
			// (set) Token: 0x0600B8B3 RID: 47283 RVA: 0x00109637 File Offset: 0x00107837
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x1700894E RID: 35150
			// (set) Token: 0x0600B8B4 RID: 47284 RVA: 0x0010964F File Offset: 0x0010784F
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x1700894F RID: 35151
			// (set) Token: 0x0600B8B5 RID: 47285 RVA: 0x00109667 File Offset: 0x00107867
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008950 RID: 35152
			// (set) Token: 0x0600B8B6 RID: 47286 RVA: 0x0010967A File Offset: 0x0010787A
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008951 RID: 35153
			// (set) Token: 0x0600B8B7 RID: 47287 RVA: 0x0010968D File Offset: 0x0010788D
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008952 RID: 35154
			// (set) Token: 0x0600B8B8 RID: 47288 RVA: 0x001096A0 File Offset: 0x001078A0
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008953 RID: 35155
			// (set) Token: 0x0600B8B9 RID: 47289 RVA: 0x001096B3 File Offset: 0x001078B3
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008954 RID: 35156
			// (set) Token: 0x0600B8BA RID: 47290 RVA: 0x001096C6 File Offset: 0x001078C6
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008955 RID: 35157
			// (set) Token: 0x0600B8BB RID: 47291 RVA: 0x001096D9 File Offset: 0x001078D9
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008956 RID: 35158
			// (set) Token: 0x0600B8BC RID: 47292 RVA: 0x001096F1 File Offset: 0x001078F1
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008957 RID: 35159
			// (set) Token: 0x0600B8BD RID: 47293 RVA: 0x00109704 File Offset: 0x00107904
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008958 RID: 35160
			// (set) Token: 0x0600B8BE RID: 47294 RVA: 0x00109717 File Offset: 0x00107917
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008959 RID: 35161
			// (set) Token: 0x0600B8BF RID: 47295 RVA: 0x0010972A File Offset: 0x0010792A
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700895A RID: 35162
			// (set) Token: 0x0600B8C0 RID: 47296 RVA: 0x00109748 File Offset: 0x00107948
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x1700895B RID: 35163
			// (set) Token: 0x0600B8C1 RID: 47297 RVA: 0x0010975B File Offset: 0x0010795B
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700895C RID: 35164
			// (set) Token: 0x0600B8C2 RID: 47298 RVA: 0x0010976E File Offset: 0x0010796E
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700895D RID: 35165
			// (set) Token: 0x0600B8C3 RID: 47299 RVA: 0x00109781 File Offset: 0x00107981
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700895E RID: 35166
			// (set) Token: 0x0600B8C4 RID: 47300 RVA: 0x00109794 File Offset: 0x00107994
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700895F RID: 35167
			// (set) Token: 0x0600B8C5 RID: 47301 RVA: 0x001097A7 File Offset: 0x001079A7
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008960 RID: 35168
			// (set) Token: 0x0600B8C6 RID: 47302 RVA: 0x001097BA File Offset: 0x001079BA
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008961 RID: 35169
			// (set) Token: 0x0600B8C7 RID: 47303 RVA: 0x001097CD File Offset: 0x001079CD
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008962 RID: 35170
			// (set) Token: 0x0600B8C8 RID: 47304 RVA: 0x001097E0 File Offset: 0x001079E0
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008963 RID: 35171
			// (set) Token: 0x0600B8C9 RID: 47305 RVA: 0x001097F3 File Offset: 0x001079F3
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008964 RID: 35172
			// (set) Token: 0x0600B8CA RID: 47306 RVA: 0x00109806 File Offset: 0x00107A06
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008965 RID: 35173
			// (set) Token: 0x0600B8CB RID: 47307 RVA: 0x00109819 File Offset: 0x00107A19
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008966 RID: 35174
			// (set) Token: 0x0600B8CC RID: 47308 RVA: 0x0010982C File Offset: 0x00107A2C
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008967 RID: 35175
			// (set) Token: 0x0600B8CD RID: 47309 RVA: 0x0010983F File Offset: 0x00107A3F
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008968 RID: 35176
			// (set) Token: 0x0600B8CE RID: 47310 RVA: 0x0010985D File Offset: 0x00107A5D
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008969 RID: 35177
			// (set) Token: 0x0600B8CF RID: 47311 RVA: 0x00109875 File Offset: 0x00107A75
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x1700896A RID: 35178
			// (set) Token: 0x0600B8D0 RID: 47312 RVA: 0x0010988D File Offset: 0x00107A8D
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700896B RID: 35179
			// (set) Token: 0x0600B8D1 RID: 47313 RVA: 0x001098A5 File Offset: 0x00107AA5
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700896C RID: 35180
			// (set) Token: 0x0600B8D2 RID: 47314 RVA: 0x001098B8 File Offset: 0x00107AB8
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700896D RID: 35181
			// (set) Token: 0x0600B8D3 RID: 47315 RVA: 0x001098CB File Offset: 0x00107ACB
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700896E RID: 35182
			// (set) Token: 0x0600B8D4 RID: 47316 RVA: 0x001098E3 File Offset: 0x00107AE3
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700896F RID: 35183
			// (set) Token: 0x0600B8D5 RID: 47317 RVA: 0x001098F6 File Offset: 0x00107AF6
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008970 RID: 35184
			// (set) Token: 0x0600B8D6 RID: 47318 RVA: 0x0010990E File Offset: 0x00107B0E
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008971 RID: 35185
			// (set) Token: 0x0600B8D7 RID: 47319 RVA: 0x00109926 File Offset: 0x00107B26
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008972 RID: 35186
			// (set) Token: 0x0600B8D8 RID: 47320 RVA: 0x00109939 File Offset: 0x00107B39
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008973 RID: 35187
			// (set) Token: 0x0600B8D9 RID: 47321 RVA: 0x0010994C File Offset: 0x00107B4C
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008974 RID: 35188
			// (set) Token: 0x0600B8DA RID: 47322 RVA: 0x0010995F File Offset: 0x00107B5F
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008975 RID: 35189
			// (set) Token: 0x0600B8DB RID: 47323 RVA: 0x00109972 File Offset: 0x00107B72
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008976 RID: 35190
			// (set) Token: 0x0600B8DC RID: 47324 RVA: 0x0010998A File Offset: 0x00107B8A
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008977 RID: 35191
			// (set) Token: 0x0600B8DD RID: 47325 RVA: 0x0010999D File Offset: 0x00107B9D
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008978 RID: 35192
			// (set) Token: 0x0600B8DE RID: 47326 RVA: 0x001099B0 File Offset: 0x00107BB0
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008979 RID: 35193
			// (set) Token: 0x0600B8DF RID: 47327 RVA: 0x001099C8 File Offset: 0x00107BC8
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700897A RID: 35194
			// (set) Token: 0x0600B8E0 RID: 47328 RVA: 0x001099E0 File Offset: 0x00107BE0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700897B RID: 35195
			// (set) Token: 0x0600B8E1 RID: 47329 RVA: 0x001099F3 File Offset: 0x00107BF3
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x1700897C RID: 35196
			// (set) Token: 0x0600B8E2 RID: 47330 RVA: 0x00109A0B File Offset: 0x00107C0B
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700897D RID: 35197
			// (set) Token: 0x0600B8E3 RID: 47331 RVA: 0x00109A1E File Offset: 0x00107C1E
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700897E RID: 35198
			// (set) Token: 0x0600B8E4 RID: 47332 RVA: 0x00109A31 File Offset: 0x00107C31
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700897F RID: 35199
			// (set) Token: 0x0600B8E5 RID: 47333 RVA: 0x00109A44 File Offset: 0x00107C44
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008980 RID: 35200
			// (set) Token: 0x0600B8E6 RID: 47334 RVA: 0x00109A62 File Offset: 0x00107C62
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008981 RID: 35201
			// (set) Token: 0x0600B8E7 RID: 47335 RVA: 0x00109A80 File Offset: 0x00107C80
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008982 RID: 35202
			// (set) Token: 0x0600B8E8 RID: 47336 RVA: 0x00109A9E File Offset: 0x00107C9E
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008983 RID: 35203
			// (set) Token: 0x0600B8E9 RID: 47337 RVA: 0x00109ABC File Offset: 0x00107CBC
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008984 RID: 35204
			// (set) Token: 0x0600B8EA RID: 47338 RVA: 0x00109ACF File Offset: 0x00107CCF
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008985 RID: 35205
			// (set) Token: 0x0600B8EB RID: 47339 RVA: 0x00109AE7 File Offset: 0x00107CE7
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008986 RID: 35206
			// (set) Token: 0x0600B8EC RID: 47340 RVA: 0x00109B05 File Offset: 0x00107D05
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008987 RID: 35207
			// (set) Token: 0x0600B8ED RID: 47341 RVA: 0x00109B1D File Offset: 0x00107D1D
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008988 RID: 35208
			// (set) Token: 0x0600B8EE RID: 47342 RVA: 0x00109B35 File Offset: 0x00107D35
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008989 RID: 35209
			// (set) Token: 0x0600B8EF RID: 47343 RVA: 0x00109B48 File Offset: 0x00107D48
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700898A RID: 35210
			// (set) Token: 0x0600B8F0 RID: 47344 RVA: 0x00109B60 File Offset: 0x00107D60
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700898B RID: 35211
			// (set) Token: 0x0600B8F1 RID: 47345 RVA: 0x00109B73 File Offset: 0x00107D73
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700898C RID: 35212
			// (set) Token: 0x0600B8F2 RID: 47346 RVA: 0x00109B86 File Offset: 0x00107D86
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700898D RID: 35213
			// (set) Token: 0x0600B8F3 RID: 47347 RVA: 0x00109B99 File Offset: 0x00107D99
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700898E RID: 35214
			// (set) Token: 0x0600B8F4 RID: 47348 RVA: 0x00109BAC File Offset: 0x00107DAC
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700898F RID: 35215
			// (set) Token: 0x0600B8F5 RID: 47349 RVA: 0x00109BC4 File Offset: 0x00107DC4
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008990 RID: 35216
			// (set) Token: 0x0600B8F6 RID: 47350 RVA: 0x00109BD7 File Offset: 0x00107DD7
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008991 RID: 35217
			// (set) Token: 0x0600B8F7 RID: 47351 RVA: 0x00109BEA File Offset: 0x00107DEA
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008992 RID: 35218
			// (set) Token: 0x0600B8F8 RID: 47352 RVA: 0x00109C02 File Offset: 0x00107E02
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008993 RID: 35219
			// (set) Token: 0x0600B8F9 RID: 47353 RVA: 0x00109C15 File Offset: 0x00107E15
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008994 RID: 35220
			// (set) Token: 0x0600B8FA RID: 47354 RVA: 0x00109C2D File Offset: 0x00107E2D
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008995 RID: 35221
			// (set) Token: 0x0600B8FB RID: 47355 RVA: 0x00109C40 File Offset: 0x00107E40
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008996 RID: 35222
			// (set) Token: 0x0600B8FC RID: 47356 RVA: 0x00109C5E File Offset: 0x00107E5E
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008997 RID: 35223
			// (set) Token: 0x0600B8FD RID: 47357 RVA: 0x00109C71 File Offset: 0x00107E71
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008998 RID: 35224
			// (set) Token: 0x0600B8FE RID: 47358 RVA: 0x00109C8F File Offset: 0x00107E8F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008999 RID: 35225
			// (set) Token: 0x0600B8FF RID: 47359 RVA: 0x00109CA2 File Offset: 0x00107EA2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700899A RID: 35226
			// (set) Token: 0x0600B900 RID: 47360 RVA: 0x00109CBA File Offset: 0x00107EBA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700899B RID: 35227
			// (set) Token: 0x0600B901 RID: 47361 RVA: 0x00109CD2 File Offset: 0x00107ED2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700899C RID: 35228
			// (set) Token: 0x0600B902 RID: 47362 RVA: 0x00109CEA File Offset: 0x00107EEA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700899D RID: 35229
			// (set) Token: 0x0600B903 RID: 47363 RVA: 0x00109D02 File Offset: 0x00107F02
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D83 RID: 3459
		public class LinkedWithSyncMailboxParameters : ParametersBase
		{
			// Token: 0x1700899E RID: 35230
			// (set) Token: 0x0600B905 RID: 47365 RVA: 0x00109D22 File Offset: 0x00107F22
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x1700899F RID: 35231
			// (set) Token: 0x0600B906 RID: 47366 RVA: 0x00109D35 File Offset: 0x00107F35
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170089A0 RID: 35232
			// (set) Token: 0x0600B907 RID: 47367 RVA: 0x00109D48 File Offset: 0x00107F48
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170089A1 RID: 35233
			// (set) Token: 0x0600B908 RID: 47368 RVA: 0x00109D5B File Offset: 0x00107F5B
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170089A2 RID: 35234
			// (set) Token: 0x0600B909 RID: 47369 RVA: 0x00109D79 File Offset: 0x00107F79
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170089A3 RID: 35235
			// (set) Token: 0x0600B90A RID: 47370 RVA: 0x00109D8C File Offset: 0x00107F8C
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170089A4 RID: 35236
			// (set) Token: 0x0600B90B RID: 47371 RVA: 0x00109DA4 File Offset: 0x00107FA4
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170089A5 RID: 35237
			// (set) Token: 0x0600B90C RID: 47372 RVA: 0x00109DBC File Offset: 0x00107FBC
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x170089A6 RID: 35238
			// (set) Token: 0x0600B90D RID: 47373 RVA: 0x00109DD4 File Offset: 0x00107FD4
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x170089A7 RID: 35239
			// (set) Token: 0x0600B90E RID: 47374 RVA: 0x00109DE7 File Offset: 0x00107FE7
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x170089A8 RID: 35240
			// (set) Token: 0x0600B90F RID: 47375 RVA: 0x00109DFA File Offset: 0x00107FFA
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x170089A9 RID: 35241
			// (set) Token: 0x0600B910 RID: 47376 RVA: 0x00109E0D File Offset: 0x0010800D
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x170089AA RID: 35242
			// (set) Token: 0x0600B911 RID: 47377 RVA: 0x00109E20 File Offset: 0x00108020
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170089AB RID: 35243
			// (set) Token: 0x0600B912 RID: 47378 RVA: 0x00109E33 File Offset: 0x00108033
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170089AC RID: 35244
			// (set) Token: 0x0600B913 RID: 47379 RVA: 0x00109E46 File Offset: 0x00108046
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x170089AD RID: 35245
			// (set) Token: 0x0600B914 RID: 47380 RVA: 0x00109E59 File Offset: 0x00108059
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x170089AE RID: 35246
			// (set) Token: 0x0600B915 RID: 47381 RVA: 0x00109E71 File Offset: 0x00108071
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x170089AF RID: 35247
			// (set) Token: 0x0600B916 RID: 47382 RVA: 0x00109E84 File Offset: 0x00108084
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x170089B0 RID: 35248
			// (set) Token: 0x0600B917 RID: 47383 RVA: 0x00109E9C File Offset: 0x0010809C
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170089B1 RID: 35249
			// (set) Token: 0x0600B918 RID: 47384 RVA: 0x00109EAF File Offset: 0x001080AF
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170089B2 RID: 35250
			// (set) Token: 0x0600B919 RID: 47385 RVA: 0x00109EC2 File Offset: 0x001080C2
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170089B3 RID: 35251
			// (set) Token: 0x0600B91A RID: 47386 RVA: 0x00109ED5 File Offset: 0x001080D5
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170089B4 RID: 35252
			// (set) Token: 0x0600B91B RID: 47387 RVA: 0x00109EE8 File Offset: 0x001080E8
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170089B5 RID: 35253
			// (set) Token: 0x0600B91C RID: 47388 RVA: 0x00109EFB File Offset: 0x001080FB
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170089B6 RID: 35254
			// (set) Token: 0x0600B91D RID: 47389 RVA: 0x00109F0E File Offset: 0x0010810E
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170089B7 RID: 35255
			// (set) Token: 0x0600B91E RID: 47390 RVA: 0x00109F21 File Offset: 0x00108121
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170089B8 RID: 35256
			// (set) Token: 0x0600B91F RID: 47391 RVA: 0x00109F34 File Offset: 0x00108134
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170089B9 RID: 35257
			// (set) Token: 0x0600B920 RID: 47392 RVA: 0x00109F47 File Offset: 0x00108147
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170089BA RID: 35258
			// (set) Token: 0x0600B921 RID: 47393 RVA: 0x00109F5A File Offset: 0x0010815A
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170089BB RID: 35259
			// (set) Token: 0x0600B922 RID: 47394 RVA: 0x00109F6D File Offset: 0x0010816D
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170089BC RID: 35260
			// (set) Token: 0x0600B923 RID: 47395 RVA: 0x00109F80 File Offset: 0x00108180
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170089BD RID: 35261
			// (set) Token: 0x0600B924 RID: 47396 RVA: 0x00109F93 File Offset: 0x00108193
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170089BE RID: 35262
			// (set) Token: 0x0600B925 RID: 47397 RVA: 0x00109FA6 File Offset: 0x001081A6
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170089BF RID: 35263
			// (set) Token: 0x0600B926 RID: 47398 RVA: 0x00109FB9 File Offset: 0x001081B9
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170089C0 RID: 35264
			// (set) Token: 0x0600B927 RID: 47399 RVA: 0x00109FCC File Offset: 0x001081CC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170089C1 RID: 35265
			// (set) Token: 0x0600B928 RID: 47400 RVA: 0x00109FDF File Offset: 0x001081DF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170089C2 RID: 35266
			// (set) Token: 0x0600B929 RID: 47401 RVA: 0x00109FF2 File Offset: 0x001081F2
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170089C3 RID: 35267
			// (set) Token: 0x0600B92A RID: 47402 RVA: 0x0010A005 File Offset: 0x00108205
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170089C4 RID: 35268
			// (set) Token: 0x0600B92B RID: 47403 RVA: 0x0010A018 File Offset: 0x00108218
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170089C5 RID: 35269
			// (set) Token: 0x0600B92C RID: 47404 RVA: 0x0010A02B File Offset: 0x0010822B
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170089C6 RID: 35270
			// (set) Token: 0x0600B92D RID: 47405 RVA: 0x0010A03E File Offset: 0x0010823E
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170089C7 RID: 35271
			// (set) Token: 0x0600B92E RID: 47406 RVA: 0x0010A051 File Offset: 0x00108251
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170089C8 RID: 35272
			// (set) Token: 0x0600B92F RID: 47407 RVA: 0x0010A069 File Offset: 0x00108269
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x170089C9 RID: 35273
			// (set) Token: 0x0600B930 RID: 47408 RVA: 0x0010A07C File Offset: 0x0010827C
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x170089CA RID: 35274
			// (set) Token: 0x0600B931 RID: 47409 RVA: 0x0010A094 File Offset: 0x00108294
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x170089CB RID: 35275
			// (set) Token: 0x0600B932 RID: 47410 RVA: 0x0010A0A7 File Offset: 0x001082A7
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x170089CC RID: 35276
			// (set) Token: 0x0600B933 RID: 47411 RVA: 0x0010A0BF File Offset: 0x001082BF
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x170089CD RID: 35277
			// (set) Token: 0x0600B934 RID: 47412 RVA: 0x0010A0D7 File Offset: 0x001082D7
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x170089CE RID: 35278
			// (set) Token: 0x0600B935 RID: 47413 RVA: 0x0010A0EF File Offset: 0x001082EF
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x170089CF RID: 35279
			// (set) Token: 0x0600B936 RID: 47414 RVA: 0x0010A107 File Offset: 0x00108307
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170089D0 RID: 35280
			// (set) Token: 0x0600B937 RID: 47415 RVA: 0x0010A11F File Offset: 0x0010831F
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x170089D1 RID: 35281
			// (set) Token: 0x0600B938 RID: 47416 RVA: 0x0010A137 File Offset: 0x00108337
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x170089D2 RID: 35282
			// (set) Token: 0x0600B939 RID: 47417 RVA: 0x0010A14F File Offset: 0x0010834F
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x170089D3 RID: 35283
			// (set) Token: 0x0600B93A RID: 47418 RVA: 0x0010A167 File Offset: 0x00108367
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x170089D4 RID: 35284
			// (set) Token: 0x0600B93B RID: 47419 RVA: 0x0010A17F File Offset: 0x0010837F
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x170089D5 RID: 35285
			// (set) Token: 0x0600B93C RID: 47420 RVA: 0x0010A192 File Offset: 0x00108392
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x170089D6 RID: 35286
			// (set) Token: 0x0600B93D RID: 47421 RVA: 0x0010A1A5 File Offset: 0x001083A5
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x170089D7 RID: 35287
			// (set) Token: 0x0600B93E RID: 47422 RVA: 0x0010A1B8 File Offset: 0x001083B8
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x170089D8 RID: 35288
			// (set) Token: 0x0600B93F RID: 47423 RVA: 0x0010A1CB File Offset: 0x001083CB
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x170089D9 RID: 35289
			// (set) Token: 0x0600B940 RID: 47424 RVA: 0x0010A1DE File Offset: 0x001083DE
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x170089DA RID: 35290
			// (set) Token: 0x0600B941 RID: 47425 RVA: 0x0010A1F1 File Offset: 0x001083F1
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x170089DB RID: 35291
			// (set) Token: 0x0600B942 RID: 47426 RVA: 0x0010A209 File Offset: 0x00108409
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x170089DC RID: 35292
			// (set) Token: 0x0600B943 RID: 47427 RVA: 0x0010A21C File Offset: 0x0010841C
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x170089DD RID: 35293
			// (set) Token: 0x0600B944 RID: 47428 RVA: 0x0010A22F File Offset: 0x0010842F
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x170089DE RID: 35294
			// (set) Token: 0x0600B945 RID: 47429 RVA: 0x0010A242 File Offset: 0x00108442
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x170089DF RID: 35295
			// (set) Token: 0x0600B946 RID: 47430 RVA: 0x0010A260 File Offset: 0x00108460
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x170089E0 RID: 35296
			// (set) Token: 0x0600B947 RID: 47431 RVA: 0x0010A273 File Offset: 0x00108473
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x170089E1 RID: 35297
			// (set) Token: 0x0600B948 RID: 47432 RVA: 0x0010A286 File Offset: 0x00108486
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x170089E2 RID: 35298
			// (set) Token: 0x0600B949 RID: 47433 RVA: 0x0010A299 File Offset: 0x00108499
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x170089E3 RID: 35299
			// (set) Token: 0x0600B94A RID: 47434 RVA: 0x0010A2AC File Offset: 0x001084AC
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x170089E4 RID: 35300
			// (set) Token: 0x0600B94B RID: 47435 RVA: 0x0010A2BF File Offset: 0x001084BF
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x170089E5 RID: 35301
			// (set) Token: 0x0600B94C RID: 47436 RVA: 0x0010A2D2 File Offset: 0x001084D2
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x170089E6 RID: 35302
			// (set) Token: 0x0600B94D RID: 47437 RVA: 0x0010A2E5 File Offset: 0x001084E5
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x170089E7 RID: 35303
			// (set) Token: 0x0600B94E RID: 47438 RVA: 0x0010A2F8 File Offset: 0x001084F8
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x170089E8 RID: 35304
			// (set) Token: 0x0600B94F RID: 47439 RVA: 0x0010A30B File Offset: 0x0010850B
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x170089E9 RID: 35305
			// (set) Token: 0x0600B950 RID: 47440 RVA: 0x0010A31E File Offset: 0x0010851E
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x170089EA RID: 35306
			// (set) Token: 0x0600B951 RID: 47441 RVA: 0x0010A331 File Offset: 0x00108531
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x170089EB RID: 35307
			// (set) Token: 0x0600B952 RID: 47442 RVA: 0x0010A344 File Offset: 0x00108544
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x170089EC RID: 35308
			// (set) Token: 0x0600B953 RID: 47443 RVA: 0x0010A357 File Offset: 0x00108557
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170089ED RID: 35309
			// (set) Token: 0x0600B954 RID: 47444 RVA: 0x0010A375 File Offset: 0x00108575
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x170089EE RID: 35310
			// (set) Token: 0x0600B955 RID: 47445 RVA: 0x0010A38D File Offset: 0x0010858D
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170089EF RID: 35311
			// (set) Token: 0x0600B956 RID: 47446 RVA: 0x0010A3A5 File Offset: 0x001085A5
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x170089F0 RID: 35312
			// (set) Token: 0x0600B957 RID: 47447 RVA: 0x0010A3BD File Offset: 0x001085BD
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x170089F1 RID: 35313
			// (set) Token: 0x0600B958 RID: 47448 RVA: 0x0010A3D0 File Offset: 0x001085D0
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x170089F2 RID: 35314
			// (set) Token: 0x0600B959 RID: 47449 RVA: 0x0010A3E3 File Offset: 0x001085E3
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x170089F3 RID: 35315
			// (set) Token: 0x0600B95A RID: 47450 RVA: 0x0010A3FB File Offset: 0x001085FB
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x170089F4 RID: 35316
			// (set) Token: 0x0600B95B RID: 47451 RVA: 0x0010A40E File Offset: 0x0010860E
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x170089F5 RID: 35317
			// (set) Token: 0x0600B95C RID: 47452 RVA: 0x0010A426 File Offset: 0x00108626
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x170089F6 RID: 35318
			// (set) Token: 0x0600B95D RID: 47453 RVA: 0x0010A43E File Offset: 0x0010863E
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170089F7 RID: 35319
			// (set) Token: 0x0600B95E RID: 47454 RVA: 0x0010A451 File Offset: 0x00108651
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170089F8 RID: 35320
			// (set) Token: 0x0600B95F RID: 47455 RVA: 0x0010A464 File Offset: 0x00108664
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x170089F9 RID: 35321
			// (set) Token: 0x0600B960 RID: 47456 RVA: 0x0010A477 File Offset: 0x00108677
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x170089FA RID: 35322
			// (set) Token: 0x0600B961 RID: 47457 RVA: 0x0010A48A File Offset: 0x0010868A
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x170089FB RID: 35323
			// (set) Token: 0x0600B962 RID: 47458 RVA: 0x0010A4A2 File Offset: 0x001086A2
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x170089FC RID: 35324
			// (set) Token: 0x0600B963 RID: 47459 RVA: 0x0010A4B5 File Offset: 0x001086B5
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x170089FD RID: 35325
			// (set) Token: 0x0600B964 RID: 47460 RVA: 0x0010A4C8 File Offset: 0x001086C8
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x170089FE RID: 35326
			// (set) Token: 0x0600B965 RID: 47461 RVA: 0x0010A4E0 File Offset: 0x001086E0
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x170089FF RID: 35327
			// (set) Token: 0x0600B966 RID: 47462 RVA: 0x0010A4F8 File Offset: 0x001086F8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008A00 RID: 35328
			// (set) Token: 0x0600B967 RID: 47463 RVA: 0x0010A50B File Offset: 0x0010870B
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008A01 RID: 35329
			// (set) Token: 0x0600B968 RID: 47464 RVA: 0x0010A523 File Offset: 0x00108723
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008A02 RID: 35330
			// (set) Token: 0x0600B969 RID: 47465 RVA: 0x0010A536 File Offset: 0x00108736
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008A03 RID: 35331
			// (set) Token: 0x0600B96A RID: 47466 RVA: 0x0010A549 File Offset: 0x00108749
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008A04 RID: 35332
			// (set) Token: 0x0600B96B RID: 47467 RVA: 0x0010A55C File Offset: 0x0010875C
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008A05 RID: 35333
			// (set) Token: 0x0600B96C RID: 47468 RVA: 0x0010A57A File Offset: 0x0010877A
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008A06 RID: 35334
			// (set) Token: 0x0600B96D RID: 47469 RVA: 0x0010A598 File Offset: 0x00108798
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008A07 RID: 35335
			// (set) Token: 0x0600B96E RID: 47470 RVA: 0x0010A5B6 File Offset: 0x001087B6
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008A08 RID: 35336
			// (set) Token: 0x0600B96F RID: 47471 RVA: 0x0010A5D4 File Offset: 0x001087D4
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008A09 RID: 35337
			// (set) Token: 0x0600B970 RID: 47472 RVA: 0x0010A5E7 File Offset: 0x001087E7
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008A0A RID: 35338
			// (set) Token: 0x0600B971 RID: 47473 RVA: 0x0010A5FF File Offset: 0x001087FF
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008A0B RID: 35339
			// (set) Token: 0x0600B972 RID: 47474 RVA: 0x0010A61D File Offset: 0x0010881D
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008A0C RID: 35340
			// (set) Token: 0x0600B973 RID: 47475 RVA: 0x0010A635 File Offset: 0x00108835
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008A0D RID: 35341
			// (set) Token: 0x0600B974 RID: 47476 RVA: 0x0010A64D File Offset: 0x0010884D
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008A0E RID: 35342
			// (set) Token: 0x0600B975 RID: 47477 RVA: 0x0010A660 File Offset: 0x00108860
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008A0F RID: 35343
			// (set) Token: 0x0600B976 RID: 47478 RVA: 0x0010A678 File Offset: 0x00108878
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008A10 RID: 35344
			// (set) Token: 0x0600B977 RID: 47479 RVA: 0x0010A68B File Offset: 0x0010888B
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008A11 RID: 35345
			// (set) Token: 0x0600B978 RID: 47480 RVA: 0x0010A69E File Offset: 0x0010889E
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008A12 RID: 35346
			// (set) Token: 0x0600B979 RID: 47481 RVA: 0x0010A6B1 File Offset: 0x001088B1
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008A13 RID: 35347
			// (set) Token: 0x0600B97A RID: 47482 RVA: 0x0010A6C4 File Offset: 0x001088C4
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008A14 RID: 35348
			// (set) Token: 0x0600B97B RID: 47483 RVA: 0x0010A6DC File Offset: 0x001088DC
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008A15 RID: 35349
			// (set) Token: 0x0600B97C RID: 47484 RVA: 0x0010A6EF File Offset: 0x001088EF
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008A16 RID: 35350
			// (set) Token: 0x0600B97D RID: 47485 RVA: 0x0010A702 File Offset: 0x00108902
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008A17 RID: 35351
			// (set) Token: 0x0600B97E RID: 47486 RVA: 0x0010A71A File Offset: 0x0010891A
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008A18 RID: 35352
			// (set) Token: 0x0600B97F RID: 47487 RVA: 0x0010A72D File Offset: 0x0010892D
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008A19 RID: 35353
			// (set) Token: 0x0600B980 RID: 47488 RVA: 0x0010A745 File Offset: 0x00108945
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008A1A RID: 35354
			// (set) Token: 0x0600B981 RID: 47489 RVA: 0x0010A758 File Offset: 0x00108958
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008A1B RID: 35355
			// (set) Token: 0x0600B982 RID: 47490 RVA: 0x0010A776 File Offset: 0x00108976
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008A1C RID: 35356
			// (set) Token: 0x0600B983 RID: 47491 RVA: 0x0010A789 File Offset: 0x00108989
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008A1D RID: 35357
			// (set) Token: 0x0600B984 RID: 47492 RVA: 0x0010A7A7 File Offset: 0x001089A7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008A1E RID: 35358
			// (set) Token: 0x0600B985 RID: 47493 RVA: 0x0010A7BA File Offset: 0x001089BA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008A1F RID: 35359
			// (set) Token: 0x0600B986 RID: 47494 RVA: 0x0010A7D2 File Offset: 0x001089D2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008A20 RID: 35360
			// (set) Token: 0x0600B987 RID: 47495 RVA: 0x0010A7EA File Offset: 0x001089EA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008A21 RID: 35361
			// (set) Token: 0x0600B988 RID: 47496 RVA: 0x0010A802 File Offset: 0x00108A02
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008A22 RID: 35362
			// (set) Token: 0x0600B989 RID: 47497 RVA: 0x0010A81A File Offset: 0x00108A1A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D84 RID: 3460
		public class RoomParameters : ParametersBase
		{
			// Token: 0x17008A23 RID: 35363
			// (set) Token: 0x0600B98B RID: 47499 RVA: 0x0010A83A File Offset: 0x00108A3A
			public virtual WindowsLiveId ResourceWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["ResourceWindowsLiveID"] = value;
				}
			}

			// Token: 0x17008A24 RID: 35364
			// (set) Token: 0x0600B98C RID: 47500 RVA: 0x0010A84D File Offset: 0x00108A4D
			public virtual SwitchParameter UseExistingResourceLiveId
			{
				set
				{
					base.PowerSharpParameters["UseExistingResourceLiveId"] = value;
				}
			}

			// Token: 0x17008A25 RID: 35365
			// (set) Token: 0x0600B98D RID: 47501 RVA: 0x0010A865 File Offset: 0x00108A65
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17008A26 RID: 35366
			// (set) Token: 0x0600B98E RID: 47502 RVA: 0x0010A878 File Offset: 0x00108A78
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x17008A27 RID: 35367
			// (set) Token: 0x0600B98F RID: 47503 RVA: 0x0010A890 File Offset: 0x00108A90
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008A28 RID: 35368
			// (set) Token: 0x0600B990 RID: 47504 RVA: 0x0010A8A3 File Offset: 0x00108AA3
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17008A29 RID: 35369
			// (set) Token: 0x0600B991 RID: 47505 RVA: 0x0010A8B6 File Offset: 0x00108AB6
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17008A2A RID: 35370
			// (set) Token: 0x0600B992 RID: 47506 RVA: 0x0010A8C9 File Offset: 0x00108AC9
			public virtual SwitchParameter Room
			{
				set
				{
					base.PowerSharpParameters["Room"] = value;
				}
			}

			// Token: 0x17008A2B RID: 35371
			// (set) Token: 0x0600B993 RID: 47507 RVA: 0x0010A8E1 File Offset: 0x00108AE1
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008A2C RID: 35372
			// (set) Token: 0x0600B994 RID: 47508 RVA: 0x0010A8FF File Offset: 0x00108AFF
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008A2D RID: 35373
			// (set) Token: 0x0600B995 RID: 47509 RVA: 0x0010A912 File Offset: 0x00108B12
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008A2E RID: 35374
			// (set) Token: 0x0600B996 RID: 47510 RVA: 0x0010A92A File Offset: 0x00108B2A
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008A2F RID: 35375
			// (set) Token: 0x0600B997 RID: 47511 RVA: 0x0010A942 File Offset: 0x00108B42
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008A30 RID: 35376
			// (set) Token: 0x0600B998 RID: 47512 RVA: 0x0010A95A File Offset: 0x00108B5A
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008A31 RID: 35377
			// (set) Token: 0x0600B999 RID: 47513 RVA: 0x0010A96D File Offset: 0x00108B6D
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008A32 RID: 35378
			// (set) Token: 0x0600B99A RID: 47514 RVA: 0x0010A980 File Offset: 0x00108B80
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008A33 RID: 35379
			// (set) Token: 0x0600B99B RID: 47515 RVA: 0x0010A993 File Offset: 0x00108B93
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008A34 RID: 35380
			// (set) Token: 0x0600B99C RID: 47516 RVA: 0x0010A9A6 File Offset: 0x00108BA6
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008A35 RID: 35381
			// (set) Token: 0x0600B99D RID: 47517 RVA: 0x0010A9B9 File Offset: 0x00108BB9
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008A36 RID: 35382
			// (set) Token: 0x0600B99E RID: 47518 RVA: 0x0010A9CC File Offset: 0x00108BCC
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008A37 RID: 35383
			// (set) Token: 0x0600B99F RID: 47519 RVA: 0x0010A9DF File Offset: 0x00108BDF
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008A38 RID: 35384
			// (set) Token: 0x0600B9A0 RID: 47520 RVA: 0x0010A9F7 File Offset: 0x00108BF7
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17008A39 RID: 35385
			// (set) Token: 0x0600B9A1 RID: 47521 RVA: 0x0010AA0A File Offset: 0x00108C0A
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008A3A RID: 35386
			// (set) Token: 0x0600B9A2 RID: 47522 RVA: 0x0010AA22 File Offset: 0x00108C22
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008A3B RID: 35387
			// (set) Token: 0x0600B9A3 RID: 47523 RVA: 0x0010AA35 File Offset: 0x00108C35
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008A3C RID: 35388
			// (set) Token: 0x0600B9A4 RID: 47524 RVA: 0x0010AA48 File Offset: 0x00108C48
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008A3D RID: 35389
			// (set) Token: 0x0600B9A5 RID: 47525 RVA: 0x0010AA5B File Offset: 0x00108C5B
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008A3E RID: 35390
			// (set) Token: 0x0600B9A6 RID: 47526 RVA: 0x0010AA6E File Offset: 0x00108C6E
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008A3F RID: 35391
			// (set) Token: 0x0600B9A7 RID: 47527 RVA: 0x0010AA81 File Offset: 0x00108C81
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008A40 RID: 35392
			// (set) Token: 0x0600B9A8 RID: 47528 RVA: 0x0010AA94 File Offset: 0x00108C94
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008A41 RID: 35393
			// (set) Token: 0x0600B9A9 RID: 47529 RVA: 0x0010AAA7 File Offset: 0x00108CA7
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008A42 RID: 35394
			// (set) Token: 0x0600B9AA RID: 47530 RVA: 0x0010AABA File Offset: 0x00108CBA
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008A43 RID: 35395
			// (set) Token: 0x0600B9AB RID: 47531 RVA: 0x0010AACD File Offset: 0x00108CCD
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008A44 RID: 35396
			// (set) Token: 0x0600B9AC RID: 47532 RVA: 0x0010AAE0 File Offset: 0x00108CE0
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008A45 RID: 35397
			// (set) Token: 0x0600B9AD RID: 47533 RVA: 0x0010AAF3 File Offset: 0x00108CF3
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008A46 RID: 35398
			// (set) Token: 0x0600B9AE RID: 47534 RVA: 0x0010AB06 File Offset: 0x00108D06
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008A47 RID: 35399
			// (set) Token: 0x0600B9AF RID: 47535 RVA: 0x0010AB19 File Offset: 0x00108D19
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008A48 RID: 35400
			// (set) Token: 0x0600B9B0 RID: 47536 RVA: 0x0010AB2C File Offset: 0x00108D2C
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008A49 RID: 35401
			// (set) Token: 0x0600B9B1 RID: 47537 RVA: 0x0010AB3F File Offset: 0x00108D3F
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008A4A RID: 35402
			// (set) Token: 0x0600B9B2 RID: 47538 RVA: 0x0010AB52 File Offset: 0x00108D52
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008A4B RID: 35403
			// (set) Token: 0x0600B9B3 RID: 47539 RVA: 0x0010AB65 File Offset: 0x00108D65
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008A4C RID: 35404
			// (set) Token: 0x0600B9B4 RID: 47540 RVA: 0x0010AB78 File Offset: 0x00108D78
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008A4D RID: 35405
			// (set) Token: 0x0600B9B5 RID: 47541 RVA: 0x0010AB8B File Offset: 0x00108D8B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008A4E RID: 35406
			// (set) Token: 0x0600B9B6 RID: 47542 RVA: 0x0010AB9E File Offset: 0x00108D9E
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008A4F RID: 35407
			// (set) Token: 0x0600B9B7 RID: 47543 RVA: 0x0010ABB1 File Offset: 0x00108DB1
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008A50 RID: 35408
			// (set) Token: 0x0600B9B8 RID: 47544 RVA: 0x0010ABC4 File Offset: 0x00108DC4
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008A51 RID: 35409
			// (set) Token: 0x0600B9B9 RID: 47545 RVA: 0x0010ABD7 File Offset: 0x00108DD7
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008A52 RID: 35410
			// (set) Token: 0x0600B9BA RID: 47546 RVA: 0x0010ABEF File Offset: 0x00108DEF
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008A53 RID: 35411
			// (set) Token: 0x0600B9BB RID: 47547 RVA: 0x0010AC02 File Offset: 0x00108E02
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008A54 RID: 35412
			// (set) Token: 0x0600B9BC RID: 47548 RVA: 0x0010AC1A File Offset: 0x00108E1A
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008A55 RID: 35413
			// (set) Token: 0x0600B9BD RID: 47549 RVA: 0x0010AC2D File Offset: 0x00108E2D
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008A56 RID: 35414
			// (set) Token: 0x0600B9BE RID: 47550 RVA: 0x0010AC45 File Offset: 0x00108E45
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008A57 RID: 35415
			// (set) Token: 0x0600B9BF RID: 47551 RVA: 0x0010AC5D File Offset: 0x00108E5D
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008A58 RID: 35416
			// (set) Token: 0x0600B9C0 RID: 47552 RVA: 0x0010AC75 File Offset: 0x00108E75
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17008A59 RID: 35417
			// (set) Token: 0x0600B9C1 RID: 47553 RVA: 0x0010AC8D File Offset: 0x00108E8D
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17008A5A RID: 35418
			// (set) Token: 0x0600B9C2 RID: 47554 RVA: 0x0010ACA5 File Offset: 0x00108EA5
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17008A5B RID: 35419
			// (set) Token: 0x0600B9C3 RID: 47555 RVA: 0x0010ACBD File Offset: 0x00108EBD
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008A5C RID: 35420
			// (set) Token: 0x0600B9C4 RID: 47556 RVA: 0x0010ACD5 File Offset: 0x00108ED5
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008A5D RID: 35421
			// (set) Token: 0x0600B9C5 RID: 47557 RVA: 0x0010ACED File Offset: 0x00108EED
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008A5E RID: 35422
			// (set) Token: 0x0600B9C6 RID: 47558 RVA: 0x0010AD05 File Offset: 0x00108F05
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008A5F RID: 35423
			// (set) Token: 0x0600B9C7 RID: 47559 RVA: 0x0010AD18 File Offset: 0x00108F18
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008A60 RID: 35424
			// (set) Token: 0x0600B9C8 RID: 47560 RVA: 0x0010AD2B File Offset: 0x00108F2B
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008A61 RID: 35425
			// (set) Token: 0x0600B9C9 RID: 47561 RVA: 0x0010AD3E File Offset: 0x00108F3E
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008A62 RID: 35426
			// (set) Token: 0x0600B9CA RID: 47562 RVA: 0x0010AD51 File Offset: 0x00108F51
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008A63 RID: 35427
			// (set) Token: 0x0600B9CB RID: 47563 RVA: 0x0010AD64 File Offset: 0x00108F64
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008A64 RID: 35428
			// (set) Token: 0x0600B9CC RID: 47564 RVA: 0x0010AD77 File Offset: 0x00108F77
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008A65 RID: 35429
			// (set) Token: 0x0600B9CD RID: 47565 RVA: 0x0010AD8F File Offset: 0x00108F8F
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008A66 RID: 35430
			// (set) Token: 0x0600B9CE RID: 47566 RVA: 0x0010ADA2 File Offset: 0x00108FA2
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008A67 RID: 35431
			// (set) Token: 0x0600B9CF RID: 47567 RVA: 0x0010ADB5 File Offset: 0x00108FB5
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008A68 RID: 35432
			// (set) Token: 0x0600B9D0 RID: 47568 RVA: 0x0010ADC8 File Offset: 0x00108FC8
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008A69 RID: 35433
			// (set) Token: 0x0600B9D1 RID: 47569 RVA: 0x0010ADE6 File Offset: 0x00108FE6
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17008A6A RID: 35434
			// (set) Token: 0x0600B9D2 RID: 47570 RVA: 0x0010ADF9 File Offset: 0x00108FF9
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17008A6B RID: 35435
			// (set) Token: 0x0600B9D3 RID: 47571 RVA: 0x0010AE0C File Offset: 0x0010900C
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17008A6C RID: 35436
			// (set) Token: 0x0600B9D4 RID: 47572 RVA: 0x0010AE1F File Offset: 0x0010901F
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17008A6D RID: 35437
			// (set) Token: 0x0600B9D5 RID: 47573 RVA: 0x0010AE32 File Offset: 0x00109032
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17008A6E RID: 35438
			// (set) Token: 0x0600B9D6 RID: 47574 RVA: 0x0010AE45 File Offset: 0x00109045
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008A6F RID: 35439
			// (set) Token: 0x0600B9D7 RID: 47575 RVA: 0x0010AE58 File Offset: 0x00109058
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008A70 RID: 35440
			// (set) Token: 0x0600B9D8 RID: 47576 RVA: 0x0010AE6B File Offset: 0x0010906B
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008A71 RID: 35441
			// (set) Token: 0x0600B9D9 RID: 47577 RVA: 0x0010AE7E File Offset: 0x0010907E
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008A72 RID: 35442
			// (set) Token: 0x0600B9DA RID: 47578 RVA: 0x0010AE91 File Offset: 0x00109091
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008A73 RID: 35443
			// (set) Token: 0x0600B9DB RID: 47579 RVA: 0x0010AEA4 File Offset: 0x001090A4
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008A74 RID: 35444
			// (set) Token: 0x0600B9DC RID: 47580 RVA: 0x0010AEB7 File Offset: 0x001090B7
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008A75 RID: 35445
			// (set) Token: 0x0600B9DD RID: 47581 RVA: 0x0010AECA File Offset: 0x001090CA
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008A76 RID: 35446
			// (set) Token: 0x0600B9DE RID: 47582 RVA: 0x0010AEDD File Offset: 0x001090DD
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008A77 RID: 35447
			// (set) Token: 0x0600B9DF RID: 47583 RVA: 0x0010AEFB File Offset: 0x001090FB
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008A78 RID: 35448
			// (set) Token: 0x0600B9E0 RID: 47584 RVA: 0x0010AF13 File Offset: 0x00109113
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008A79 RID: 35449
			// (set) Token: 0x0600B9E1 RID: 47585 RVA: 0x0010AF2B File Offset: 0x0010912B
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17008A7A RID: 35450
			// (set) Token: 0x0600B9E2 RID: 47586 RVA: 0x0010AF43 File Offset: 0x00109143
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17008A7B RID: 35451
			// (set) Token: 0x0600B9E3 RID: 47587 RVA: 0x0010AF56 File Offset: 0x00109156
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17008A7C RID: 35452
			// (set) Token: 0x0600B9E4 RID: 47588 RVA: 0x0010AF69 File Offset: 0x00109169
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17008A7D RID: 35453
			// (set) Token: 0x0600B9E5 RID: 47589 RVA: 0x0010AF81 File Offset: 0x00109181
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008A7E RID: 35454
			// (set) Token: 0x0600B9E6 RID: 47590 RVA: 0x0010AF94 File Offset: 0x00109194
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008A7F RID: 35455
			// (set) Token: 0x0600B9E7 RID: 47591 RVA: 0x0010AFAC File Offset: 0x001091AC
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008A80 RID: 35456
			// (set) Token: 0x0600B9E8 RID: 47592 RVA: 0x0010AFC4 File Offset: 0x001091C4
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008A81 RID: 35457
			// (set) Token: 0x0600B9E9 RID: 47593 RVA: 0x0010AFD7 File Offset: 0x001091D7
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008A82 RID: 35458
			// (set) Token: 0x0600B9EA RID: 47594 RVA: 0x0010AFEA File Offset: 0x001091EA
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008A83 RID: 35459
			// (set) Token: 0x0600B9EB RID: 47595 RVA: 0x0010AFFD File Offset: 0x001091FD
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008A84 RID: 35460
			// (set) Token: 0x0600B9EC RID: 47596 RVA: 0x0010B010 File Offset: 0x00109210
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008A85 RID: 35461
			// (set) Token: 0x0600B9ED RID: 47597 RVA: 0x0010B028 File Offset: 0x00109228
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008A86 RID: 35462
			// (set) Token: 0x0600B9EE RID: 47598 RVA: 0x0010B03B File Offset: 0x0010923B
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008A87 RID: 35463
			// (set) Token: 0x0600B9EF RID: 47599 RVA: 0x0010B04E File Offset: 0x0010924E
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008A88 RID: 35464
			// (set) Token: 0x0600B9F0 RID: 47600 RVA: 0x0010B066 File Offset: 0x00109266
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17008A89 RID: 35465
			// (set) Token: 0x0600B9F1 RID: 47601 RVA: 0x0010B07E File Offset: 0x0010927E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008A8A RID: 35466
			// (set) Token: 0x0600B9F2 RID: 47602 RVA: 0x0010B091 File Offset: 0x00109291
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008A8B RID: 35467
			// (set) Token: 0x0600B9F3 RID: 47603 RVA: 0x0010B0A9 File Offset: 0x001092A9
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008A8C RID: 35468
			// (set) Token: 0x0600B9F4 RID: 47604 RVA: 0x0010B0BC File Offset: 0x001092BC
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008A8D RID: 35469
			// (set) Token: 0x0600B9F5 RID: 47605 RVA: 0x0010B0CF File Offset: 0x001092CF
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008A8E RID: 35470
			// (set) Token: 0x0600B9F6 RID: 47606 RVA: 0x0010B0E2 File Offset: 0x001092E2
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008A8F RID: 35471
			// (set) Token: 0x0600B9F7 RID: 47607 RVA: 0x0010B100 File Offset: 0x00109300
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008A90 RID: 35472
			// (set) Token: 0x0600B9F8 RID: 47608 RVA: 0x0010B11E File Offset: 0x0010931E
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008A91 RID: 35473
			// (set) Token: 0x0600B9F9 RID: 47609 RVA: 0x0010B13C File Offset: 0x0010933C
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008A92 RID: 35474
			// (set) Token: 0x0600B9FA RID: 47610 RVA: 0x0010B15A File Offset: 0x0010935A
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008A93 RID: 35475
			// (set) Token: 0x0600B9FB RID: 47611 RVA: 0x0010B16D File Offset: 0x0010936D
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008A94 RID: 35476
			// (set) Token: 0x0600B9FC RID: 47612 RVA: 0x0010B185 File Offset: 0x00109385
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008A95 RID: 35477
			// (set) Token: 0x0600B9FD RID: 47613 RVA: 0x0010B1A3 File Offset: 0x001093A3
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008A96 RID: 35478
			// (set) Token: 0x0600B9FE RID: 47614 RVA: 0x0010B1BB File Offset: 0x001093BB
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008A97 RID: 35479
			// (set) Token: 0x0600B9FF RID: 47615 RVA: 0x0010B1D3 File Offset: 0x001093D3
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008A98 RID: 35480
			// (set) Token: 0x0600BA00 RID: 47616 RVA: 0x0010B1E6 File Offset: 0x001093E6
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008A99 RID: 35481
			// (set) Token: 0x0600BA01 RID: 47617 RVA: 0x0010B1FE File Offset: 0x001093FE
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008A9A RID: 35482
			// (set) Token: 0x0600BA02 RID: 47618 RVA: 0x0010B211 File Offset: 0x00109411
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008A9B RID: 35483
			// (set) Token: 0x0600BA03 RID: 47619 RVA: 0x0010B224 File Offset: 0x00109424
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008A9C RID: 35484
			// (set) Token: 0x0600BA04 RID: 47620 RVA: 0x0010B237 File Offset: 0x00109437
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008A9D RID: 35485
			// (set) Token: 0x0600BA05 RID: 47621 RVA: 0x0010B24A File Offset: 0x0010944A
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008A9E RID: 35486
			// (set) Token: 0x0600BA06 RID: 47622 RVA: 0x0010B262 File Offset: 0x00109462
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008A9F RID: 35487
			// (set) Token: 0x0600BA07 RID: 47623 RVA: 0x0010B275 File Offset: 0x00109475
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008AA0 RID: 35488
			// (set) Token: 0x0600BA08 RID: 47624 RVA: 0x0010B288 File Offset: 0x00109488
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008AA1 RID: 35489
			// (set) Token: 0x0600BA09 RID: 47625 RVA: 0x0010B2A0 File Offset: 0x001094A0
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008AA2 RID: 35490
			// (set) Token: 0x0600BA0A RID: 47626 RVA: 0x0010B2B3 File Offset: 0x001094B3
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008AA3 RID: 35491
			// (set) Token: 0x0600BA0B RID: 47627 RVA: 0x0010B2CB File Offset: 0x001094CB
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008AA4 RID: 35492
			// (set) Token: 0x0600BA0C RID: 47628 RVA: 0x0010B2DE File Offset: 0x001094DE
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008AA5 RID: 35493
			// (set) Token: 0x0600BA0D RID: 47629 RVA: 0x0010B2FC File Offset: 0x001094FC
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008AA6 RID: 35494
			// (set) Token: 0x0600BA0E RID: 47630 RVA: 0x0010B30F File Offset: 0x0010950F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008AA7 RID: 35495
			// (set) Token: 0x0600BA0F RID: 47631 RVA: 0x0010B32D File Offset: 0x0010952D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008AA8 RID: 35496
			// (set) Token: 0x0600BA10 RID: 47632 RVA: 0x0010B340 File Offset: 0x00109540
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008AA9 RID: 35497
			// (set) Token: 0x0600BA11 RID: 47633 RVA: 0x0010B358 File Offset: 0x00109558
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008AAA RID: 35498
			// (set) Token: 0x0600BA12 RID: 47634 RVA: 0x0010B370 File Offset: 0x00109570
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008AAB RID: 35499
			// (set) Token: 0x0600BA13 RID: 47635 RVA: 0x0010B388 File Offset: 0x00109588
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008AAC RID: 35500
			// (set) Token: 0x0600BA14 RID: 47636 RVA: 0x0010B3A0 File Offset: 0x001095A0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D85 RID: 3461
		public class EquipmentParameters : ParametersBase
		{
			// Token: 0x17008AAD RID: 35501
			// (set) Token: 0x0600BA16 RID: 47638 RVA: 0x0010B3C0 File Offset: 0x001095C0
			public virtual WindowsLiveId ResourceWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["ResourceWindowsLiveID"] = value;
				}
			}

			// Token: 0x17008AAE RID: 35502
			// (set) Token: 0x0600BA17 RID: 47639 RVA: 0x0010B3D3 File Offset: 0x001095D3
			public virtual SwitchParameter UseExistingResourceLiveId
			{
				set
				{
					base.PowerSharpParameters["UseExistingResourceLiveId"] = value;
				}
			}

			// Token: 0x17008AAF RID: 35503
			// (set) Token: 0x0600BA18 RID: 47640 RVA: 0x0010B3EB File Offset: 0x001095EB
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17008AB0 RID: 35504
			// (set) Token: 0x0600BA19 RID: 47641 RVA: 0x0010B3FE File Offset: 0x001095FE
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x17008AB1 RID: 35505
			// (set) Token: 0x0600BA1A RID: 47642 RVA: 0x0010B416 File Offset: 0x00109616
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008AB2 RID: 35506
			// (set) Token: 0x0600BA1B RID: 47643 RVA: 0x0010B429 File Offset: 0x00109629
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17008AB3 RID: 35507
			// (set) Token: 0x0600BA1C RID: 47644 RVA: 0x0010B43C File Offset: 0x0010963C
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17008AB4 RID: 35508
			// (set) Token: 0x0600BA1D RID: 47645 RVA: 0x0010B44F File Offset: 0x0010964F
			public virtual SwitchParameter Equipment
			{
				set
				{
					base.PowerSharpParameters["Equipment"] = value;
				}
			}

			// Token: 0x17008AB5 RID: 35509
			// (set) Token: 0x0600BA1E RID: 47646 RVA: 0x0010B467 File Offset: 0x00109667
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008AB6 RID: 35510
			// (set) Token: 0x0600BA1F RID: 47647 RVA: 0x0010B485 File Offset: 0x00109685
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008AB7 RID: 35511
			// (set) Token: 0x0600BA20 RID: 47648 RVA: 0x0010B498 File Offset: 0x00109698
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008AB8 RID: 35512
			// (set) Token: 0x0600BA21 RID: 47649 RVA: 0x0010B4B0 File Offset: 0x001096B0
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008AB9 RID: 35513
			// (set) Token: 0x0600BA22 RID: 47650 RVA: 0x0010B4C8 File Offset: 0x001096C8
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008ABA RID: 35514
			// (set) Token: 0x0600BA23 RID: 47651 RVA: 0x0010B4E0 File Offset: 0x001096E0
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008ABB RID: 35515
			// (set) Token: 0x0600BA24 RID: 47652 RVA: 0x0010B4F3 File Offset: 0x001096F3
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008ABC RID: 35516
			// (set) Token: 0x0600BA25 RID: 47653 RVA: 0x0010B506 File Offset: 0x00109706
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008ABD RID: 35517
			// (set) Token: 0x0600BA26 RID: 47654 RVA: 0x0010B519 File Offset: 0x00109719
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008ABE RID: 35518
			// (set) Token: 0x0600BA27 RID: 47655 RVA: 0x0010B52C File Offset: 0x0010972C
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008ABF RID: 35519
			// (set) Token: 0x0600BA28 RID: 47656 RVA: 0x0010B53F File Offset: 0x0010973F
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008AC0 RID: 35520
			// (set) Token: 0x0600BA29 RID: 47657 RVA: 0x0010B552 File Offset: 0x00109752
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008AC1 RID: 35521
			// (set) Token: 0x0600BA2A RID: 47658 RVA: 0x0010B565 File Offset: 0x00109765
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008AC2 RID: 35522
			// (set) Token: 0x0600BA2B RID: 47659 RVA: 0x0010B57D File Offset: 0x0010977D
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17008AC3 RID: 35523
			// (set) Token: 0x0600BA2C RID: 47660 RVA: 0x0010B590 File Offset: 0x00109790
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008AC4 RID: 35524
			// (set) Token: 0x0600BA2D RID: 47661 RVA: 0x0010B5A8 File Offset: 0x001097A8
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008AC5 RID: 35525
			// (set) Token: 0x0600BA2E RID: 47662 RVA: 0x0010B5BB File Offset: 0x001097BB
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008AC6 RID: 35526
			// (set) Token: 0x0600BA2F RID: 47663 RVA: 0x0010B5CE File Offset: 0x001097CE
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008AC7 RID: 35527
			// (set) Token: 0x0600BA30 RID: 47664 RVA: 0x0010B5E1 File Offset: 0x001097E1
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008AC8 RID: 35528
			// (set) Token: 0x0600BA31 RID: 47665 RVA: 0x0010B5F4 File Offset: 0x001097F4
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008AC9 RID: 35529
			// (set) Token: 0x0600BA32 RID: 47666 RVA: 0x0010B607 File Offset: 0x00109807
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008ACA RID: 35530
			// (set) Token: 0x0600BA33 RID: 47667 RVA: 0x0010B61A File Offset: 0x0010981A
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008ACB RID: 35531
			// (set) Token: 0x0600BA34 RID: 47668 RVA: 0x0010B62D File Offset: 0x0010982D
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008ACC RID: 35532
			// (set) Token: 0x0600BA35 RID: 47669 RVA: 0x0010B640 File Offset: 0x00109840
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008ACD RID: 35533
			// (set) Token: 0x0600BA36 RID: 47670 RVA: 0x0010B653 File Offset: 0x00109853
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008ACE RID: 35534
			// (set) Token: 0x0600BA37 RID: 47671 RVA: 0x0010B666 File Offset: 0x00109866
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008ACF RID: 35535
			// (set) Token: 0x0600BA38 RID: 47672 RVA: 0x0010B679 File Offset: 0x00109879
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008AD0 RID: 35536
			// (set) Token: 0x0600BA39 RID: 47673 RVA: 0x0010B68C File Offset: 0x0010988C
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008AD1 RID: 35537
			// (set) Token: 0x0600BA3A RID: 47674 RVA: 0x0010B69F File Offset: 0x0010989F
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008AD2 RID: 35538
			// (set) Token: 0x0600BA3B RID: 47675 RVA: 0x0010B6B2 File Offset: 0x001098B2
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008AD3 RID: 35539
			// (set) Token: 0x0600BA3C RID: 47676 RVA: 0x0010B6C5 File Offset: 0x001098C5
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008AD4 RID: 35540
			// (set) Token: 0x0600BA3D RID: 47677 RVA: 0x0010B6D8 File Offset: 0x001098D8
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008AD5 RID: 35541
			// (set) Token: 0x0600BA3E RID: 47678 RVA: 0x0010B6EB File Offset: 0x001098EB
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008AD6 RID: 35542
			// (set) Token: 0x0600BA3F RID: 47679 RVA: 0x0010B6FE File Offset: 0x001098FE
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008AD7 RID: 35543
			// (set) Token: 0x0600BA40 RID: 47680 RVA: 0x0010B711 File Offset: 0x00109911
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008AD8 RID: 35544
			// (set) Token: 0x0600BA41 RID: 47681 RVA: 0x0010B724 File Offset: 0x00109924
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008AD9 RID: 35545
			// (set) Token: 0x0600BA42 RID: 47682 RVA: 0x0010B737 File Offset: 0x00109937
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008ADA RID: 35546
			// (set) Token: 0x0600BA43 RID: 47683 RVA: 0x0010B74A File Offset: 0x0010994A
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008ADB RID: 35547
			// (set) Token: 0x0600BA44 RID: 47684 RVA: 0x0010B75D File Offset: 0x0010995D
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008ADC RID: 35548
			// (set) Token: 0x0600BA45 RID: 47685 RVA: 0x0010B775 File Offset: 0x00109975
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008ADD RID: 35549
			// (set) Token: 0x0600BA46 RID: 47686 RVA: 0x0010B788 File Offset: 0x00109988
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008ADE RID: 35550
			// (set) Token: 0x0600BA47 RID: 47687 RVA: 0x0010B7A0 File Offset: 0x001099A0
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008ADF RID: 35551
			// (set) Token: 0x0600BA48 RID: 47688 RVA: 0x0010B7B3 File Offset: 0x001099B3
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008AE0 RID: 35552
			// (set) Token: 0x0600BA49 RID: 47689 RVA: 0x0010B7CB File Offset: 0x001099CB
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008AE1 RID: 35553
			// (set) Token: 0x0600BA4A RID: 47690 RVA: 0x0010B7E3 File Offset: 0x001099E3
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008AE2 RID: 35554
			// (set) Token: 0x0600BA4B RID: 47691 RVA: 0x0010B7FB File Offset: 0x001099FB
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17008AE3 RID: 35555
			// (set) Token: 0x0600BA4C RID: 47692 RVA: 0x0010B813 File Offset: 0x00109A13
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17008AE4 RID: 35556
			// (set) Token: 0x0600BA4D RID: 47693 RVA: 0x0010B82B File Offset: 0x00109A2B
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17008AE5 RID: 35557
			// (set) Token: 0x0600BA4E RID: 47694 RVA: 0x0010B843 File Offset: 0x00109A43
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008AE6 RID: 35558
			// (set) Token: 0x0600BA4F RID: 47695 RVA: 0x0010B85B File Offset: 0x00109A5B
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008AE7 RID: 35559
			// (set) Token: 0x0600BA50 RID: 47696 RVA: 0x0010B873 File Offset: 0x00109A73
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008AE8 RID: 35560
			// (set) Token: 0x0600BA51 RID: 47697 RVA: 0x0010B88B File Offset: 0x00109A8B
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008AE9 RID: 35561
			// (set) Token: 0x0600BA52 RID: 47698 RVA: 0x0010B89E File Offset: 0x00109A9E
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008AEA RID: 35562
			// (set) Token: 0x0600BA53 RID: 47699 RVA: 0x0010B8B1 File Offset: 0x00109AB1
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008AEB RID: 35563
			// (set) Token: 0x0600BA54 RID: 47700 RVA: 0x0010B8C4 File Offset: 0x00109AC4
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008AEC RID: 35564
			// (set) Token: 0x0600BA55 RID: 47701 RVA: 0x0010B8D7 File Offset: 0x00109AD7
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008AED RID: 35565
			// (set) Token: 0x0600BA56 RID: 47702 RVA: 0x0010B8EA File Offset: 0x00109AEA
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008AEE RID: 35566
			// (set) Token: 0x0600BA57 RID: 47703 RVA: 0x0010B8FD File Offset: 0x00109AFD
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008AEF RID: 35567
			// (set) Token: 0x0600BA58 RID: 47704 RVA: 0x0010B915 File Offset: 0x00109B15
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008AF0 RID: 35568
			// (set) Token: 0x0600BA59 RID: 47705 RVA: 0x0010B928 File Offset: 0x00109B28
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008AF1 RID: 35569
			// (set) Token: 0x0600BA5A RID: 47706 RVA: 0x0010B93B File Offset: 0x00109B3B
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008AF2 RID: 35570
			// (set) Token: 0x0600BA5B RID: 47707 RVA: 0x0010B94E File Offset: 0x00109B4E
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008AF3 RID: 35571
			// (set) Token: 0x0600BA5C RID: 47708 RVA: 0x0010B96C File Offset: 0x00109B6C
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17008AF4 RID: 35572
			// (set) Token: 0x0600BA5D RID: 47709 RVA: 0x0010B97F File Offset: 0x00109B7F
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17008AF5 RID: 35573
			// (set) Token: 0x0600BA5E RID: 47710 RVA: 0x0010B992 File Offset: 0x00109B92
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17008AF6 RID: 35574
			// (set) Token: 0x0600BA5F RID: 47711 RVA: 0x0010B9A5 File Offset: 0x00109BA5
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17008AF7 RID: 35575
			// (set) Token: 0x0600BA60 RID: 47712 RVA: 0x0010B9B8 File Offset: 0x00109BB8
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17008AF8 RID: 35576
			// (set) Token: 0x0600BA61 RID: 47713 RVA: 0x0010B9CB File Offset: 0x00109BCB
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008AF9 RID: 35577
			// (set) Token: 0x0600BA62 RID: 47714 RVA: 0x0010B9DE File Offset: 0x00109BDE
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008AFA RID: 35578
			// (set) Token: 0x0600BA63 RID: 47715 RVA: 0x0010B9F1 File Offset: 0x00109BF1
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008AFB RID: 35579
			// (set) Token: 0x0600BA64 RID: 47716 RVA: 0x0010BA04 File Offset: 0x00109C04
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008AFC RID: 35580
			// (set) Token: 0x0600BA65 RID: 47717 RVA: 0x0010BA17 File Offset: 0x00109C17
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008AFD RID: 35581
			// (set) Token: 0x0600BA66 RID: 47718 RVA: 0x0010BA2A File Offset: 0x00109C2A
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008AFE RID: 35582
			// (set) Token: 0x0600BA67 RID: 47719 RVA: 0x0010BA3D File Offset: 0x00109C3D
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008AFF RID: 35583
			// (set) Token: 0x0600BA68 RID: 47720 RVA: 0x0010BA50 File Offset: 0x00109C50
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008B00 RID: 35584
			// (set) Token: 0x0600BA69 RID: 47721 RVA: 0x0010BA63 File Offset: 0x00109C63
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008B01 RID: 35585
			// (set) Token: 0x0600BA6A RID: 47722 RVA: 0x0010BA81 File Offset: 0x00109C81
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008B02 RID: 35586
			// (set) Token: 0x0600BA6B RID: 47723 RVA: 0x0010BA99 File Offset: 0x00109C99
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008B03 RID: 35587
			// (set) Token: 0x0600BA6C RID: 47724 RVA: 0x0010BAB1 File Offset: 0x00109CB1
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17008B04 RID: 35588
			// (set) Token: 0x0600BA6D RID: 47725 RVA: 0x0010BAC9 File Offset: 0x00109CC9
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17008B05 RID: 35589
			// (set) Token: 0x0600BA6E RID: 47726 RVA: 0x0010BADC File Offset: 0x00109CDC
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17008B06 RID: 35590
			// (set) Token: 0x0600BA6F RID: 47727 RVA: 0x0010BAEF File Offset: 0x00109CEF
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17008B07 RID: 35591
			// (set) Token: 0x0600BA70 RID: 47728 RVA: 0x0010BB07 File Offset: 0x00109D07
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008B08 RID: 35592
			// (set) Token: 0x0600BA71 RID: 47729 RVA: 0x0010BB1A File Offset: 0x00109D1A
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008B09 RID: 35593
			// (set) Token: 0x0600BA72 RID: 47730 RVA: 0x0010BB32 File Offset: 0x00109D32
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008B0A RID: 35594
			// (set) Token: 0x0600BA73 RID: 47731 RVA: 0x0010BB4A File Offset: 0x00109D4A
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008B0B RID: 35595
			// (set) Token: 0x0600BA74 RID: 47732 RVA: 0x0010BB5D File Offset: 0x00109D5D
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008B0C RID: 35596
			// (set) Token: 0x0600BA75 RID: 47733 RVA: 0x0010BB70 File Offset: 0x00109D70
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008B0D RID: 35597
			// (set) Token: 0x0600BA76 RID: 47734 RVA: 0x0010BB83 File Offset: 0x00109D83
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008B0E RID: 35598
			// (set) Token: 0x0600BA77 RID: 47735 RVA: 0x0010BB96 File Offset: 0x00109D96
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008B0F RID: 35599
			// (set) Token: 0x0600BA78 RID: 47736 RVA: 0x0010BBAE File Offset: 0x00109DAE
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008B10 RID: 35600
			// (set) Token: 0x0600BA79 RID: 47737 RVA: 0x0010BBC1 File Offset: 0x00109DC1
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008B11 RID: 35601
			// (set) Token: 0x0600BA7A RID: 47738 RVA: 0x0010BBD4 File Offset: 0x00109DD4
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008B12 RID: 35602
			// (set) Token: 0x0600BA7B RID: 47739 RVA: 0x0010BBEC File Offset: 0x00109DEC
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17008B13 RID: 35603
			// (set) Token: 0x0600BA7C RID: 47740 RVA: 0x0010BC04 File Offset: 0x00109E04
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008B14 RID: 35604
			// (set) Token: 0x0600BA7D RID: 47741 RVA: 0x0010BC17 File Offset: 0x00109E17
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008B15 RID: 35605
			// (set) Token: 0x0600BA7E RID: 47742 RVA: 0x0010BC2F File Offset: 0x00109E2F
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008B16 RID: 35606
			// (set) Token: 0x0600BA7F RID: 47743 RVA: 0x0010BC42 File Offset: 0x00109E42
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008B17 RID: 35607
			// (set) Token: 0x0600BA80 RID: 47744 RVA: 0x0010BC55 File Offset: 0x00109E55
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008B18 RID: 35608
			// (set) Token: 0x0600BA81 RID: 47745 RVA: 0x0010BC68 File Offset: 0x00109E68
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008B19 RID: 35609
			// (set) Token: 0x0600BA82 RID: 47746 RVA: 0x0010BC86 File Offset: 0x00109E86
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008B1A RID: 35610
			// (set) Token: 0x0600BA83 RID: 47747 RVA: 0x0010BCA4 File Offset: 0x00109EA4
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008B1B RID: 35611
			// (set) Token: 0x0600BA84 RID: 47748 RVA: 0x0010BCC2 File Offset: 0x00109EC2
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008B1C RID: 35612
			// (set) Token: 0x0600BA85 RID: 47749 RVA: 0x0010BCE0 File Offset: 0x00109EE0
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008B1D RID: 35613
			// (set) Token: 0x0600BA86 RID: 47750 RVA: 0x0010BCF3 File Offset: 0x00109EF3
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008B1E RID: 35614
			// (set) Token: 0x0600BA87 RID: 47751 RVA: 0x0010BD0B File Offset: 0x00109F0B
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008B1F RID: 35615
			// (set) Token: 0x0600BA88 RID: 47752 RVA: 0x0010BD29 File Offset: 0x00109F29
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008B20 RID: 35616
			// (set) Token: 0x0600BA89 RID: 47753 RVA: 0x0010BD41 File Offset: 0x00109F41
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008B21 RID: 35617
			// (set) Token: 0x0600BA8A RID: 47754 RVA: 0x0010BD59 File Offset: 0x00109F59
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008B22 RID: 35618
			// (set) Token: 0x0600BA8B RID: 47755 RVA: 0x0010BD6C File Offset: 0x00109F6C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008B23 RID: 35619
			// (set) Token: 0x0600BA8C RID: 47756 RVA: 0x0010BD84 File Offset: 0x00109F84
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008B24 RID: 35620
			// (set) Token: 0x0600BA8D RID: 47757 RVA: 0x0010BD97 File Offset: 0x00109F97
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008B25 RID: 35621
			// (set) Token: 0x0600BA8E RID: 47758 RVA: 0x0010BDAA File Offset: 0x00109FAA
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008B26 RID: 35622
			// (set) Token: 0x0600BA8F RID: 47759 RVA: 0x0010BDBD File Offset: 0x00109FBD
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008B27 RID: 35623
			// (set) Token: 0x0600BA90 RID: 47760 RVA: 0x0010BDD0 File Offset: 0x00109FD0
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008B28 RID: 35624
			// (set) Token: 0x0600BA91 RID: 47761 RVA: 0x0010BDE8 File Offset: 0x00109FE8
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008B29 RID: 35625
			// (set) Token: 0x0600BA92 RID: 47762 RVA: 0x0010BDFB File Offset: 0x00109FFB
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008B2A RID: 35626
			// (set) Token: 0x0600BA93 RID: 47763 RVA: 0x0010BE0E File Offset: 0x0010A00E
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008B2B RID: 35627
			// (set) Token: 0x0600BA94 RID: 47764 RVA: 0x0010BE26 File Offset: 0x0010A026
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008B2C RID: 35628
			// (set) Token: 0x0600BA95 RID: 47765 RVA: 0x0010BE39 File Offset: 0x0010A039
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008B2D RID: 35629
			// (set) Token: 0x0600BA96 RID: 47766 RVA: 0x0010BE51 File Offset: 0x0010A051
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008B2E RID: 35630
			// (set) Token: 0x0600BA97 RID: 47767 RVA: 0x0010BE64 File Offset: 0x0010A064
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008B2F RID: 35631
			// (set) Token: 0x0600BA98 RID: 47768 RVA: 0x0010BE82 File Offset: 0x0010A082
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008B30 RID: 35632
			// (set) Token: 0x0600BA99 RID: 47769 RVA: 0x0010BE95 File Offset: 0x0010A095
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008B31 RID: 35633
			// (set) Token: 0x0600BA9A RID: 47770 RVA: 0x0010BEB3 File Offset: 0x0010A0B3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008B32 RID: 35634
			// (set) Token: 0x0600BA9B RID: 47771 RVA: 0x0010BEC6 File Offset: 0x0010A0C6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008B33 RID: 35635
			// (set) Token: 0x0600BA9C RID: 47772 RVA: 0x0010BEDE File Offset: 0x0010A0DE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008B34 RID: 35636
			// (set) Token: 0x0600BA9D RID: 47773 RVA: 0x0010BEF6 File Offset: 0x0010A0F6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008B35 RID: 35637
			// (set) Token: 0x0600BA9E RID: 47774 RVA: 0x0010BF0E File Offset: 0x0010A10E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008B36 RID: 35638
			// (set) Token: 0x0600BA9F RID: 47775 RVA: 0x0010BF26 File Offset: 0x0010A126
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D86 RID: 3462
		public class WindowsLiveCustomDomainsParameters : ParametersBase
		{
			// Token: 0x17008B37 RID: 35639
			// (set) Token: 0x0600BAA1 RID: 47777 RVA: 0x0010BF46 File Offset: 0x0010A146
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17008B38 RID: 35640
			// (set) Token: 0x0600BAA2 RID: 47778 RVA: 0x0010BF59 File Offset: 0x0010A159
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x17008B39 RID: 35641
			// (set) Token: 0x0600BAA3 RID: 47779 RVA: 0x0010BF71 File Offset: 0x0010A171
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008B3A RID: 35642
			// (set) Token: 0x0600BAA4 RID: 47780 RVA: 0x0010BF84 File Offset: 0x0010A184
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008B3B RID: 35643
			// (set) Token: 0x0600BAA5 RID: 47781 RVA: 0x0010BF9C File Offset: 0x0010A19C
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008B3C RID: 35644
			// (set) Token: 0x0600BAA6 RID: 47782 RVA: 0x0010BFAF File Offset: 0x0010A1AF
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008B3D RID: 35645
			// (set) Token: 0x0600BAA7 RID: 47783 RVA: 0x0010BFC7 File Offset: 0x0010A1C7
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008B3E RID: 35646
			// (set) Token: 0x0600BAA8 RID: 47784 RVA: 0x0010BFE5 File Offset: 0x0010A1E5
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008B3F RID: 35647
			// (set) Token: 0x0600BAA9 RID: 47785 RVA: 0x0010BFF8 File Offset: 0x0010A1F8
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008B40 RID: 35648
			// (set) Token: 0x0600BAAA RID: 47786 RVA: 0x0010C010 File Offset: 0x0010A210
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008B41 RID: 35649
			// (set) Token: 0x0600BAAB RID: 47787 RVA: 0x0010C028 File Offset: 0x0010A228
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008B42 RID: 35650
			// (set) Token: 0x0600BAAC RID: 47788 RVA: 0x0010C046 File Offset: 0x0010A246
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17008B43 RID: 35651
			// (set) Token: 0x0600BAAD RID: 47789 RVA: 0x0010C059 File Offset: 0x0010A259
			public virtual SwitchParameter UseExistingLiveId
			{
				set
				{
					base.PowerSharpParameters["UseExistingLiveId"] = value;
				}
			}

			// Token: 0x17008B44 RID: 35652
			// (set) Token: 0x0600BAAE RID: 47790 RVA: 0x0010C071 File Offset: 0x0010A271
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008B45 RID: 35653
			// (set) Token: 0x0600BAAF RID: 47791 RVA: 0x0010C089 File Offset: 0x0010A289
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008B46 RID: 35654
			// (set) Token: 0x0600BAB0 RID: 47792 RVA: 0x0010C09C File Offset: 0x0010A29C
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008B47 RID: 35655
			// (set) Token: 0x0600BAB1 RID: 47793 RVA: 0x0010C0AF File Offset: 0x0010A2AF
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008B48 RID: 35656
			// (set) Token: 0x0600BAB2 RID: 47794 RVA: 0x0010C0C2 File Offset: 0x0010A2C2
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008B49 RID: 35657
			// (set) Token: 0x0600BAB3 RID: 47795 RVA: 0x0010C0D5 File Offset: 0x0010A2D5
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008B4A RID: 35658
			// (set) Token: 0x0600BAB4 RID: 47796 RVA: 0x0010C0E8 File Offset: 0x0010A2E8
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008B4B RID: 35659
			// (set) Token: 0x0600BAB5 RID: 47797 RVA: 0x0010C0FB File Offset: 0x0010A2FB
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008B4C RID: 35660
			// (set) Token: 0x0600BAB6 RID: 47798 RVA: 0x0010C10E File Offset: 0x0010A30E
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008B4D RID: 35661
			// (set) Token: 0x0600BAB7 RID: 47799 RVA: 0x0010C126 File Offset: 0x0010A326
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17008B4E RID: 35662
			// (set) Token: 0x0600BAB8 RID: 47800 RVA: 0x0010C139 File Offset: 0x0010A339
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008B4F RID: 35663
			// (set) Token: 0x0600BAB9 RID: 47801 RVA: 0x0010C151 File Offset: 0x0010A351
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008B50 RID: 35664
			// (set) Token: 0x0600BABA RID: 47802 RVA: 0x0010C164 File Offset: 0x0010A364
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008B51 RID: 35665
			// (set) Token: 0x0600BABB RID: 47803 RVA: 0x0010C177 File Offset: 0x0010A377
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008B52 RID: 35666
			// (set) Token: 0x0600BABC RID: 47804 RVA: 0x0010C18A File Offset: 0x0010A38A
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008B53 RID: 35667
			// (set) Token: 0x0600BABD RID: 47805 RVA: 0x0010C19D File Offset: 0x0010A39D
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008B54 RID: 35668
			// (set) Token: 0x0600BABE RID: 47806 RVA: 0x0010C1B0 File Offset: 0x0010A3B0
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008B55 RID: 35669
			// (set) Token: 0x0600BABF RID: 47807 RVA: 0x0010C1C3 File Offset: 0x0010A3C3
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008B56 RID: 35670
			// (set) Token: 0x0600BAC0 RID: 47808 RVA: 0x0010C1D6 File Offset: 0x0010A3D6
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008B57 RID: 35671
			// (set) Token: 0x0600BAC1 RID: 47809 RVA: 0x0010C1E9 File Offset: 0x0010A3E9
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008B58 RID: 35672
			// (set) Token: 0x0600BAC2 RID: 47810 RVA: 0x0010C1FC File Offset: 0x0010A3FC
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008B59 RID: 35673
			// (set) Token: 0x0600BAC3 RID: 47811 RVA: 0x0010C20F File Offset: 0x0010A40F
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008B5A RID: 35674
			// (set) Token: 0x0600BAC4 RID: 47812 RVA: 0x0010C222 File Offset: 0x0010A422
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008B5B RID: 35675
			// (set) Token: 0x0600BAC5 RID: 47813 RVA: 0x0010C235 File Offset: 0x0010A435
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008B5C RID: 35676
			// (set) Token: 0x0600BAC6 RID: 47814 RVA: 0x0010C248 File Offset: 0x0010A448
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008B5D RID: 35677
			// (set) Token: 0x0600BAC7 RID: 47815 RVA: 0x0010C25B File Offset: 0x0010A45B
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008B5E RID: 35678
			// (set) Token: 0x0600BAC8 RID: 47816 RVA: 0x0010C26E File Offset: 0x0010A46E
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008B5F RID: 35679
			// (set) Token: 0x0600BAC9 RID: 47817 RVA: 0x0010C281 File Offset: 0x0010A481
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008B60 RID: 35680
			// (set) Token: 0x0600BACA RID: 47818 RVA: 0x0010C294 File Offset: 0x0010A494
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008B61 RID: 35681
			// (set) Token: 0x0600BACB RID: 47819 RVA: 0x0010C2A7 File Offset: 0x0010A4A7
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008B62 RID: 35682
			// (set) Token: 0x0600BACC RID: 47820 RVA: 0x0010C2BA File Offset: 0x0010A4BA
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008B63 RID: 35683
			// (set) Token: 0x0600BACD RID: 47821 RVA: 0x0010C2CD File Offset: 0x0010A4CD
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008B64 RID: 35684
			// (set) Token: 0x0600BACE RID: 47822 RVA: 0x0010C2E0 File Offset: 0x0010A4E0
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008B65 RID: 35685
			// (set) Token: 0x0600BACF RID: 47823 RVA: 0x0010C2F3 File Offset: 0x0010A4F3
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008B66 RID: 35686
			// (set) Token: 0x0600BAD0 RID: 47824 RVA: 0x0010C306 File Offset: 0x0010A506
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008B67 RID: 35687
			// (set) Token: 0x0600BAD1 RID: 47825 RVA: 0x0010C31E File Offset: 0x0010A51E
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008B68 RID: 35688
			// (set) Token: 0x0600BAD2 RID: 47826 RVA: 0x0010C331 File Offset: 0x0010A531
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008B69 RID: 35689
			// (set) Token: 0x0600BAD3 RID: 47827 RVA: 0x0010C349 File Offset: 0x0010A549
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008B6A RID: 35690
			// (set) Token: 0x0600BAD4 RID: 47828 RVA: 0x0010C35C File Offset: 0x0010A55C
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008B6B RID: 35691
			// (set) Token: 0x0600BAD5 RID: 47829 RVA: 0x0010C374 File Offset: 0x0010A574
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008B6C RID: 35692
			// (set) Token: 0x0600BAD6 RID: 47830 RVA: 0x0010C38C File Offset: 0x0010A58C
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008B6D RID: 35693
			// (set) Token: 0x0600BAD7 RID: 47831 RVA: 0x0010C3A4 File Offset: 0x0010A5A4
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17008B6E RID: 35694
			// (set) Token: 0x0600BAD8 RID: 47832 RVA: 0x0010C3BC File Offset: 0x0010A5BC
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17008B6F RID: 35695
			// (set) Token: 0x0600BAD9 RID: 47833 RVA: 0x0010C3D4 File Offset: 0x0010A5D4
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17008B70 RID: 35696
			// (set) Token: 0x0600BADA RID: 47834 RVA: 0x0010C3EC File Offset: 0x0010A5EC
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008B71 RID: 35697
			// (set) Token: 0x0600BADB RID: 47835 RVA: 0x0010C404 File Offset: 0x0010A604
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008B72 RID: 35698
			// (set) Token: 0x0600BADC RID: 47836 RVA: 0x0010C41C File Offset: 0x0010A61C
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008B73 RID: 35699
			// (set) Token: 0x0600BADD RID: 47837 RVA: 0x0010C434 File Offset: 0x0010A634
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008B74 RID: 35700
			// (set) Token: 0x0600BADE RID: 47838 RVA: 0x0010C447 File Offset: 0x0010A647
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008B75 RID: 35701
			// (set) Token: 0x0600BADF RID: 47839 RVA: 0x0010C45A File Offset: 0x0010A65A
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008B76 RID: 35702
			// (set) Token: 0x0600BAE0 RID: 47840 RVA: 0x0010C46D File Offset: 0x0010A66D
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008B77 RID: 35703
			// (set) Token: 0x0600BAE1 RID: 47841 RVA: 0x0010C480 File Offset: 0x0010A680
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008B78 RID: 35704
			// (set) Token: 0x0600BAE2 RID: 47842 RVA: 0x0010C493 File Offset: 0x0010A693
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008B79 RID: 35705
			// (set) Token: 0x0600BAE3 RID: 47843 RVA: 0x0010C4A6 File Offset: 0x0010A6A6
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008B7A RID: 35706
			// (set) Token: 0x0600BAE4 RID: 47844 RVA: 0x0010C4BE File Offset: 0x0010A6BE
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008B7B RID: 35707
			// (set) Token: 0x0600BAE5 RID: 47845 RVA: 0x0010C4D1 File Offset: 0x0010A6D1
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008B7C RID: 35708
			// (set) Token: 0x0600BAE6 RID: 47846 RVA: 0x0010C4E4 File Offset: 0x0010A6E4
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008B7D RID: 35709
			// (set) Token: 0x0600BAE7 RID: 47847 RVA: 0x0010C4F7 File Offset: 0x0010A6F7
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008B7E RID: 35710
			// (set) Token: 0x0600BAE8 RID: 47848 RVA: 0x0010C515 File Offset: 0x0010A715
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17008B7F RID: 35711
			// (set) Token: 0x0600BAE9 RID: 47849 RVA: 0x0010C528 File Offset: 0x0010A728
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17008B80 RID: 35712
			// (set) Token: 0x0600BAEA RID: 47850 RVA: 0x0010C53B File Offset: 0x0010A73B
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17008B81 RID: 35713
			// (set) Token: 0x0600BAEB RID: 47851 RVA: 0x0010C54E File Offset: 0x0010A74E
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17008B82 RID: 35714
			// (set) Token: 0x0600BAEC RID: 47852 RVA: 0x0010C561 File Offset: 0x0010A761
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17008B83 RID: 35715
			// (set) Token: 0x0600BAED RID: 47853 RVA: 0x0010C574 File Offset: 0x0010A774
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008B84 RID: 35716
			// (set) Token: 0x0600BAEE RID: 47854 RVA: 0x0010C587 File Offset: 0x0010A787
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008B85 RID: 35717
			// (set) Token: 0x0600BAEF RID: 47855 RVA: 0x0010C59A File Offset: 0x0010A79A
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008B86 RID: 35718
			// (set) Token: 0x0600BAF0 RID: 47856 RVA: 0x0010C5AD File Offset: 0x0010A7AD
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008B87 RID: 35719
			// (set) Token: 0x0600BAF1 RID: 47857 RVA: 0x0010C5C0 File Offset: 0x0010A7C0
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008B88 RID: 35720
			// (set) Token: 0x0600BAF2 RID: 47858 RVA: 0x0010C5D3 File Offset: 0x0010A7D3
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008B89 RID: 35721
			// (set) Token: 0x0600BAF3 RID: 47859 RVA: 0x0010C5E6 File Offset: 0x0010A7E6
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008B8A RID: 35722
			// (set) Token: 0x0600BAF4 RID: 47860 RVA: 0x0010C5F9 File Offset: 0x0010A7F9
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008B8B RID: 35723
			// (set) Token: 0x0600BAF5 RID: 47861 RVA: 0x0010C60C File Offset: 0x0010A80C
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008B8C RID: 35724
			// (set) Token: 0x0600BAF6 RID: 47862 RVA: 0x0010C62A File Offset: 0x0010A82A
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008B8D RID: 35725
			// (set) Token: 0x0600BAF7 RID: 47863 RVA: 0x0010C642 File Offset: 0x0010A842
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008B8E RID: 35726
			// (set) Token: 0x0600BAF8 RID: 47864 RVA: 0x0010C65A File Offset: 0x0010A85A
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17008B8F RID: 35727
			// (set) Token: 0x0600BAF9 RID: 47865 RVA: 0x0010C672 File Offset: 0x0010A872
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17008B90 RID: 35728
			// (set) Token: 0x0600BAFA RID: 47866 RVA: 0x0010C685 File Offset: 0x0010A885
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17008B91 RID: 35729
			// (set) Token: 0x0600BAFB RID: 47867 RVA: 0x0010C698 File Offset: 0x0010A898
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17008B92 RID: 35730
			// (set) Token: 0x0600BAFC RID: 47868 RVA: 0x0010C6B0 File Offset: 0x0010A8B0
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008B93 RID: 35731
			// (set) Token: 0x0600BAFD RID: 47869 RVA: 0x0010C6C3 File Offset: 0x0010A8C3
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008B94 RID: 35732
			// (set) Token: 0x0600BAFE RID: 47870 RVA: 0x0010C6DB File Offset: 0x0010A8DB
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008B95 RID: 35733
			// (set) Token: 0x0600BAFF RID: 47871 RVA: 0x0010C6F3 File Offset: 0x0010A8F3
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008B96 RID: 35734
			// (set) Token: 0x0600BB00 RID: 47872 RVA: 0x0010C706 File Offset: 0x0010A906
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008B97 RID: 35735
			// (set) Token: 0x0600BB01 RID: 47873 RVA: 0x0010C719 File Offset: 0x0010A919
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008B98 RID: 35736
			// (set) Token: 0x0600BB02 RID: 47874 RVA: 0x0010C72C File Offset: 0x0010A92C
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008B99 RID: 35737
			// (set) Token: 0x0600BB03 RID: 47875 RVA: 0x0010C73F File Offset: 0x0010A93F
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008B9A RID: 35738
			// (set) Token: 0x0600BB04 RID: 47876 RVA: 0x0010C757 File Offset: 0x0010A957
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008B9B RID: 35739
			// (set) Token: 0x0600BB05 RID: 47877 RVA: 0x0010C76A File Offset: 0x0010A96A
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008B9C RID: 35740
			// (set) Token: 0x0600BB06 RID: 47878 RVA: 0x0010C77D File Offset: 0x0010A97D
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008B9D RID: 35741
			// (set) Token: 0x0600BB07 RID: 47879 RVA: 0x0010C795 File Offset: 0x0010A995
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17008B9E RID: 35742
			// (set) Token: 0x0600BB08 RID: 47880 RVA: 0x0010C7AD File Offset: 0x0010A9AD
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008B9F RID: 35743
			// (set) Token: 0x0600BB09 RID: 47881 RVA: 0x0010C7C0 File Offset: 0x0010A9C0
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008BA0 RID: 35744
			// (set) Token: 0x0600BB0A RID: 47882 RVA: 0x0010C7D8 File Offset: 0x0010A9D8
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008BA1 RID: 35745
			// (set) Token: 0x0600BB0B RID: 47883 RVA: 0x0010C7EB File Offset: 0x0010A9EB
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008BA2 RID: 35746
			// (set) Token: 0x0600BB0C RID: 47884 RVA: 0x0010C7FE File Offset: 0x0010A9FE
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008BA3 RID: 35747
			// (set) Token: 0x0600BB0D RID: 47885 RVA: 0x0010C811 File Offset: 0x0010AA11
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008BA4 RID: 35748
			// (set) Token: 0x0600BB0E RID: 47886 RVA: 0x0010C82F File Offset: 0x0010AA2F
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008BA5 RID: 35749
			// (set) Token: 0x0600BB0F RID: 47887 RVA: 0x0010C84D File Offset: 0x0010AA4D
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008BA6 RID: 35750
			// (set) Token: 0x0600BB10 RID: 47888 RVA: 0x0010C86B File Offset: 0x0010AA6B
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008BA7 RID: 35751
			// (set) Token: 0x0600BB11 RID: 47889 RVA: 0x0010C889 File Offset: 0x0010AA89
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008BA8 RID: 35752
			// (set) Token: 0x0600BB12 RID: 47890 RVA: 0x0010C89C File Offset: 0x0010AA9C
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008BA9 RID: 35753
			// (set) Token: 0x0600BB13 RID: 47891 RVA: 0x0010C8B4 File Offset: 0x0010AAB4
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008BAA RID: 35754
			// (set) Token: 0x0600BB14 RID: 47892 RVA: 0x0010C8D2 File Offset: 0x0010AAD2
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008BAB RID: 35755
			// (set) Token: 0x0600BB15 RID: 47893 RVA: 0x0010C8EA File Offset: 0x0010AAEA
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008BAC RID: 35756
			// (set) Token: 0x0600BB16 RID: 47894 RVA: 0x0010C902 File Offset: 0x0010AB02
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008BAD RID: 35757
			// (set) Token: 0x0600BB17 RID: 47895 RVA: 0x0010C915 File Offset: 0x0010AB15
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008BAE RID: 35758
			// (set) Token: 0x0600BB18 RID: 47896 RVA: 0x0010C92D File Offset: 0x0010AB2D
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008BAF RID: 35759
			// (set) Token: 0x0600BB19 RID: 47897 RVA: 0x0010C940 File Offset: 0x0010AB40
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008BB0 RID: 35760
			// (set) Token: 0x0600BB1A RID: 47898 RVA: 0x0010C953 File Offset: 0x0010AB53
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008BB1 RID: 35761
			// (set) Token: 0x0600BB1B RID: 47899 RVA: 0x0010C966 File Offset: 0x0010AB66
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008BB2 RID: 35762
			// (set) Token: 0x0600BB1C RID: 47900 RVA: 0x0010C979 File Offset: 0x0010AB79
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008BB3 RID: 35763
			// (set) Token: 0x0600BB1D RID: 47901 RVA: 0x0010C991 File Offset: 0x0010AB91
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008BB4 RID: 35764
			// (set) Token: 0x0600BB1E RID: 47902 RVA: 0x0010C9A4 File Offset: 0x0010ABA4
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008BB5 RID: 35765
			// (set) Token: 0x0600BB1F RID: 47903 RVA: 0x0010C9B7 File Offset: 0x0010ABB7
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008BB6 RID: 35766
			// (set) Token: 0x0600BB20 RID: 47904 RVA: 0x0010C9CF File Offset: 0x0010ABCF
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008BB7 RID: 35767
			// (set) Token: 0x0600BB21 RID: 47905 RVA: 0x0010C9E2 File Offset: 0x0010ABE2
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008BB8 RID: 35768
			// (set) Token: 0x0600BB22 RID: 47906 RVA: 0x0010C9FA File Offset: 0x0010ABFA
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008BB9 RID: 35769
			// (set) Token: 0x0600BB23 RID: 47907 RVA: 0x0010CA0D File Offset: 0x0010AC0D
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008BBA RID: 35770
			// (set) Token: 0x0600BB24 RID: 47908 RVA: 0x0010CA2B File Offset: 0x0010AC2B
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008BBB RID: 35771
			// (set) Token: 0x0600BB25 RID: 47909 RVA: 0x0010CA3E File Offset: 0x0010AC3E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008BBC RID: 35772
			// (set) Token: 0x0600BB26 RID: 47910 RVA: 0x0010CA5C File Offset: 0x0010AC5C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008BBD RID: 35773
			// (set) Token: 0x0600BB27 RID: 47911 RVA: 0x0010CA6F File Offset: 0x0010AC6F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008BBE RID: 35774
			// (set) Token: 0x0600BB28 RID: 47912 RVA: 0x0010CA87 File Offset: 0x0010AC87
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008BBF RID: 35775
			// (set) Token: 0x0600BB29 RID: 47913 RVA: 0x0010CA9F File Offset: 0x0010AC9F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008BC0 RID: 35776
			// (set) Token: 0x0600BB2A RID: 47914 RVA: 0x0010CAB7 File Offset: 0x0010ACB7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008BC1 RID: 35777
			// (set) Token: 0x0600BB2B RID: 47915 RVA: 0x0010CACF File Offset: 0x0010ACCF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D87 RID: 3463
		public class FederatedUserParameters : ParametersBase
		{
			// Token: 0x17008BC2 RID: 35778
			// (set) Token: 0x0600BB2D RID: 47917 RVA: 0x0010CAEF File Offset: 0x0010ACEF
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17008BC3 RID: 35779
			// (set) Token: 0x0600BB2E RID: 47918 RVA: 0x0010CB02 File Offset: 0x0010AD02
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008BC4 RID: 35780
			// (set) Token: 0x0600BB2F RID: 47919 RVA: 0x0010CB15 File Offset: 0x0010AD15
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008BC5 RID: 35781
			// (set) Token: 0x0600BB30 RID: 47920 RVA: 0x0010CB2D File Offset: 0x0010AD2D
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008BC6 RID: 35782
			// (set) Token: 0x0600BB31 RID: 47921 RVA: 0x0010CB40 File Offset: 0x0010AD40
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008BC7 RID: 35783
			// (set) Token: 0x0600BB32 RID: 47922 RVA: 0x0010CB58 File Offset: 0x0010AD58
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008BC8 RID: 35784
			// (set) Token: 0x0600BB33 RID: 47923 RVA: 0x0010CB76 File Offset: 0x0010AD76
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17008BC9 RID: 35785
			// (set) Token: 0x0600BB34 RID: 47924 RVA: 0x0010CB89 File Offset: 0x0010AD89
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x17008BCA RID: 35786
			// (set) Token: 0x0600BB35 RID: 47925 RVA: 0x0010CBA1 File Offset: 0x0010ADA1
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17008BCB RID: 35787
			// (set) Token: 0x0600BB36 RID: 47926 RVA: 0x0010CBB4 File Offset: 0x0010ADB4
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008BCC RID: 35788
			// (set) Token: 0x0600BB37 RID: 47927 RVA: 0x0010CBCC File Offset: 0x0010ADCC
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008BCD RID: 35789
			// (set) Token: 0x0600BB38 RID: 47928 RVA: 0x0010CBDF File Offset: 0x0010ADDF
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008BCE RID: 35790
			// (set) Token: 0x0600BB39 RID: 47929 RVA: 0x0010CBF2 File Offset: 0x0010ADF2
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008BCF RID: 35791
			// (set) Token: 0x0600BB3A RID: 47930 RVA: 0x0010CC05 File Offset: 0x0010AE05
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008BD0 RID: 35792
			// (set) Token: 0x0600BB3B RID: 47931 RVA: 0x0010CC18 File Offset: 0x0010AE18
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008BD1 RID: 35793
			// (set) Token: 0x0600BB3C RID: 47932 RVA: 0x0010CC2B File Offset: 0x0010AE2B
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008BD2 RID: 35794
			// (set) Token: 0x0600BB3D RID: 47933 RVA: 0x0010CC3E File Offset: 0x0010AE3E
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008BD3 RID: 35795
			// (set) Token: 0x0600BB3E RID: 47934 RVA: 0x0010CC51 File Offset: 0x0010AE51
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008BD4 RID: 35796
			// (set) Token: 0x0600BB3F RID: 47935 RVA: 0x0010CC69 File Offset: 0x0010AE69
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17008BD5 RID: 35797
			// (set) Token: 0x0600BB40 RID: 47936 RVA: 0x0010CC7C File Offset: 0x0010AE7C
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008BD6 RID: 35798
			// (set) Token: 0x0600BB41 RID: 47937 RVA: 0x0010CC94 File Offset: 0x0010AE94
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008BD7 RID: 35799
			// (set) Token: 0x0600BB42 RID: 47938 RVA: 0x0010CCA7 File Offset: 0x0010AEA7
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008BD8 RID: 35800
			// (set) Token: 0x0600BB43 RID: 47939 RVA: 0x0010CCBA File Offset: 0x0010AEBA
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008BD9 RID: 35801
			// (set) Token: 0x0600BB44 RID: 47940 RVA: 0x0010CCCD File Offset: 0x0010AECD
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008BDA RID: 35802
			// (set) Token: 0x0600BB45 RID: 47941 RVA: 0x0010CCE0 File Offset: 0x0010AEE0
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008BDB RID: 35803
			// (set) Token: 0x0600BB46 RID: 47942 RVA: 0x0010CCF3 File Offset: 0x0010AEF3
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008BDC RID: 35804
			// (set) Token: 0x0600BB47 RID: 47943 RVA: 0x0010CD06 File Offset: 0x0010AF06
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008BDD RID: 35805
			// (set) Token: 0x0600BB48 RID: 47944 RVA: 0x0010CD19 File Offset: 0x0010AF19
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008BDE RID: 35806
			// (set) Token: 0x0600BB49 RID: 47945 RVA: 0x0010CD2C File Offset: 0x0010AF2C
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008BDF RID: 35807
			// (set) Token: 0x0600BB4A RID: 47946 RVA: 0x0010CD3F File Offset: 0x0010AF3F
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008BE0 RID: 35808
			// (set) Token: 0x0600BB4B RID: 47947 RVA: 0x0010CD52 File Offset: 0x0010AF52
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008BE1 RID: 35809
			// (set) Token: 0x0600BB4C RID: 47948 RVA: 0x0010CD65 File Offset: 0x0010AF65
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008BE2 RID: 35810
			// (set) Token: 0x0600BB4D RID: 47949 RVA: 0x0010CD78 File Offset: 0x0010AF78
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008BE3 RID: 35811
			// (set) Token: 0x0600BB4E RID: 47950 RVA: 0x0010CD8B File Offset: 0x0010AF8B
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008BE4 RID: 35812
			// (set) Token: 0x0600BB4F RID: 47951 RVA: 0x0010CD9E File Offset: 0x0010AF9E
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008BE5 RID: 35813
			// (set) Token: 0x0600BB50 RID: 47952 RVA: 0x0010CDB1 File Offset: 0x0010AFB1
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008BE6 RID: 35814
			// (set) Token: 0x0600BB51 RID: 47953 RVA: 0x0010CDC4 File Offset: 0x0010AFC4
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008BE7 RID: 35815
			// (set) Token: 0x0600BB52 RID: 47954 RVA: 0x0010CDD7 File Offset: 0x0010AFD7
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008BE8 RID: 35816
			// (set) Token: 0x0600BB53 RID: 47955 RVA: 0x0010CDEA File Offset: 0x0010AFEA
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008BE9 RID: 35817
			// (set) Token: 0x0600BB54 RID: 47956 RVA: 0x0010CDFD File Offset: 0x0010AFFD
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008BEA RID: 35818
			// (set) Token: 0x0600BB55 RID: 47957 RVA: 0x0010CE10 File Offset: 0x0010B010
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008BEB RID: 35819
			// (set) Token: 0x0600BB56 RID: 47958 RVA: 0x0010CE23 File Offset: 0x0010B023
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008BEC RID: 35820
			// (set) Token: 0x0600BB57 RID: 47959 RVA: 0x0010CE36 File Offset: 0x0010B036
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008BED RID: 35821
			// (set) Token: 0x0600BB58 RID: 47960 RVA: 0x0010CE49 File Offset: 0x0010B049
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008BEE RID: 35822
			// (set) Token: 0x0600BB59 RID: 47961 RVA: 0x0010CE61 File Offset: 0x0010B061
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008BEF RID: 35823
			// (set) Token: 0x0600BB5A RID: 47962 RVA: 0x0010CE74 File Offset: 0x0010B074
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008BF0 RID: 35824
			// (set) Token: 0x0600BB5B RID: 47963 RVA: 0x0010CE8C File Offset: 0x0010B08C
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008BF1 RID: 35825
			// (set) Token: 0x0600BB5C RID: 47964 RVA: 0x0010CE9F File Offset: 0x0010B09F
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008BF2 RID: 35826
			// (set) Token: 0x0600BB5D RID: 47965 RVA: 0x0010CEB7 File Offset: 0x0010B0B7
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008BF3 RID: 35827
			// (set) Token: 0x0600BB5E RID: 47966 RVA: 0x0010CECF File Offset: 0x0010B0CF
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008BF4 RID: 35828
			// (set) Token: 0x0600BB5F RID: 47967 RVA: 0x0010CEE7 File Offset: 0x0010B0E7
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17008BF5 RID: 35829
			// (set) Token: 0x0600BB60 RID: 47968 RVA: 0x0010CEFF File Offset: 0x0010B0FF
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17008BF6 RID: 35830
			// (set) Token: 0x0600BB61 RID: 47969 RVA: 0x0010CF17 File Offset: 0x0010B117
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17008BF7 RID: 35831
			// (set) Token: 0x0600BB62 RID: 47970 RVA: 0x0010CF2F File Offset: 0x0010B12F
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008BF8 RID: 35832
			// (set) Token: 0x0600BB63 RID: 47971 RVA: 0x0010CF47 File Offset: 0x0010B147
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008BF9 RID: 35833
			// (set) Token: 0x0600BB64 RID: 47972 RVA: 0x0010CF5F File Offset: 0x0010B15F
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008BFA RID: 35834
			// (set) Token: 0x0600BB65 RID: 47973 RVA: 0x0010CF77 File Offset: 0x0010B177
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008BFB RID: 35835
			// (set) Token: 0x0600BB66 RID: 47974 RVA: 0x0010CF8A File Offset: 0x0010B18A
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008BFC RID: 35836
			// (set) Token: 0x0600BB67 RID: 47975 RVA: 0x0010CF9D File Offset: 0x0010B19D
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008BFD RID: 35837
			// (set) Token: 0x0600BB68 RID: 47976 RVA: 0x0010CFB0 File Offset: 0x0010B1B0
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008BFE RID: 35838
			// (set) Token: 0x0600BB69 RID: 47977 RVA: 0x0010CFC3 File Offset: 0x0010B1C3
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008BFF RID: 35839
			// (set) Token: 0x0600BB6A RID: 47978 RVA: 0x0010CFD6 File Offset: 0x0010B1D6
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008C00 RID: 35840
			// (set) Token: 0x0600BB6B RID: 47979 RVA: 0x0010CFE9 File Offset: 0x0010B1E9
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008C01 RID: 35841
			// (set) Token: 0x0600BB6C RID: 47980 RVA: 0x0010D001 File Offset: 0x0010B201
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008C02 RID: 35842
			// (set) Token: 0x0600BB6D RID: 47981 RVA: 0x0010D014 File Offset: 0x0010B214
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008C03 RID: 35843
			// (set) Token: 0x0600BB6E RID: 47982 RVA: 0x0010D027 File Offset: 0x0010B227
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008C04 RID: 35844
			// (set) Token: 0x0600BB6F RID: 47983 RVA: 0x0010D03A File Offset: 0x0010B23A
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008C05 RID: 35845
			// (set) Token: 0x0600BB70 RID: 47984 RVA: 0x0010D058 File Offset: 0x0010B258
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17008C06 RID: 35846
			// (set) Token: 0x0600BB71 RID: 47985 RVA: 0x0010D06B File Offset: 0x0010B26B
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17008C07 RID: 35847
			// (set) Token: 0x0600BB72 RID: 47986 RVA: 0x0010D07E File Offset: 0x0010B27E
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17008C08 RID: 35848
			// (set) Token: 0x0600BB73 RID: 47987 RVA: 0x0010D091 File Offset: 0x0010B291
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17008C09 RID: 35849
			// (set) Token: 0x0600BB74 RID: 47988 RVA: 0x0010D0A4 File Offset: 0x0010B2A4
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17008C0A RID: 35850
			// (set) Token: 0x0600BB75 RID: 47989 RVA: 0x0010D0B7 File Offset: 0x0010B2B7
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008C0B RID: 35851
			// (set) Token: 0x0600BB76 RID: 47990 RVA: 0x0010D0CA File Offset: 0x0010B2CA
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008C0C RID: 35852
			// (set) Token: 0x0600BB77 RID: 47991 RVA: 0x0010D0DD File Offset: 0x0010B2DD
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008C0D RID: 35853
			// (set) Token: 0x0600BB78 RID: 47992 RVA: 0x0010D0F0 File Offset: 0x0010B2F0
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008C0E RID: 35854
			// (set) Token: 0x0600BB79 RID: 47993 RVA: 0x0010D103 File Offset: 0x0010B303
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008C0F RID: 35855
			// (set) Token: 0x0600BB7A RID: 47994 RVA: 0x0010D116 File Offset: 0x0010B316
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008C10 RID: 35856
			// (set) Token: 0x0600BB7B RID: 47995 RVA: 0x0010D129 File Offset: 0x0010B329
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008C11 RID: 35857
			// (set) Token: 0x0600BB7C RID: 47996 RVA: 0x0010D13C File Offset: 0x0010B33C
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008C12 RID: 35858
			// (set) Token: 0x0600BB7D RID: 47997 RVA: 0x0010D14F File Offset: 0x0010B34F
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008C13 RID: 35859
			// (set) Token: 0x0600BB7E RID: 47998 RVA: 0x0010D16D File Offset: 0x0010B36D
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008C14 RID: 35860
			// (set) Token: 0x0600BB7F RID: 47999 RVA: 0x0010D185 File Offset: 0x0010B385
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008C15 RID: 35861
			// (set) Token: 0x0600BB80 RID: 48000 RVA: 0x0010D19D File Offset: 0x0010B39D
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17008C16 RID: 35862
			// (set) Token: 0x0600BB81 RID: 48001 RVA: 0x0010D1B5 File Offset: 0x0010B3B5
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17008C17 RID: 35863
			// (set) Token: 0x0600BB82 RID: 48002 RVA: 0x0010D1C8 File Offset: 0x0010B3C8
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17008C18 RID: 35864
			// (set) Token: 0x0600BB83 RID: 48003 RVA: 0x0010D1DB File Offset: 0x0010B3DB
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17008C19 RID: 35865
			// (set) Token: 0x0600BB84 RID: 48004 RVA: 0x0010D1F3 File Offset: 0x0010B3F3
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008C1A RID: 35866
			// (set) Token: 0x0600BB85 RID: 48005 RVA: 0x0010D206 File Offset: 0x0010B406
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008C1B RID: 35867
			// (set) Token: 0x0600BB86 RID: 48006 RVA: 0x0010D21E File Offset: 0x0010B41E
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008C1C RID: 35868
			// (set) Token: 0x0600BB87 RID: 48007 RVA: 0x0010D236 File Offset: 0x0010B436
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008C1D RID: 35869
			// (set) Token: 0x0600BB88 RID: 48008 RVA: 0x0010D249 File Offset: 0x0010B449
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008C1E RID: 35870
			// (set) Token: 0x0600BB89 RID: 48009 RVA: 0x0010D25C File Offset: 0x0010B45C
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008C1F RID: 35871
			// (set) Token: 0x0600BB8A RID: 48010 RVA: 0x0010D26F File Offset: 0x0010B46F
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008C20 RID: 35872
			// (set) Token: 0x0600BB8B RID: 48011 RVA: 0x0010D282 File Offset: 0x0010B482
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008C21 RID: 35873
			// (set) Token: 0x0600BB8C RID: 48012 RVA: 0x0010D29A File Offset: 0x0010B49A
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008C22 RID: 35874
			// (set) Token: 0x0600BB8D RID: 48013 RVA: 0x0010D2AD File Offset: 0x0010B4AD
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008C23 RID: 35875
			// (set) Token: 0x0600BB8E RID: 48014 RVA: 0x0010D2C0 File Offset: 0x0010B4C0
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008C24 RID: 35876
			// (set) Token: 0x0600BB8F RID: 48015 RVA: 0x0010D2D8 File Offset: 0x0010B4D8
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17008C25 RID: 35877
			// (set) Token: 0x0600BB90 RID: 48016 RVA: 0x0010D2F0 File Offset: 0x0010B4F0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008C26 RID: 35878
			// (set) Token: 0x0600BB91 RID: 48017 RVA: 0x0010D303 File Offset: 0x0010B503
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008C27 RID: 35879
			// (set) Token: 0x0600BB92 RID: 48018 RVA: 0x0010D31B File Offset: 0x0010B51B
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008C28 RID: 35880
			// (set) Token: 0x0600BB93 RID: 48019 RVA: 0x0010D32E File Offset: 0x0010B52E
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008C29 RID: 35881
			// (set) Token: 0x0600BB94 RID: 48020 RVA: 0x0010D341 File Offset: 0x0010B541
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008C2A RID: 35882
			// (set) Token: 0x0600BB95 RID: 48021 RVA: 0x0010D354 File Offset: 0x0010B554
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008C2B RID: 35883
			// (set) Token: 0x0600BB96 RID: 48022 RVA: 0x0010D372 File Offset: 0x0010B572
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008C2C RID: 35884
			// (set) Token: 0x0600BB97 RID: 48023 RVA: 0x0010D390 File Offset: 0x0010B590
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008C2D RID: 35885
			// (set) Token: 0x0600BB98 RID: 48024 RVA: 0x0010D3AE File Offset: 0x0010B5AE
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008C2E RID: 35886
			// (set) Token: 0x0600BB99 RID: 48025 RVA: 0x0010D3CC File Offset: 0x0010B5CC
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008C2F RID: 35887
			// (set) Token: 0x0600BB9A RID: 48026 RVA: 0x0010D3DF File Offset: 0x0010B5DF
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008C30 RID: 35888
			// (set) Token: 0x0600BB9B RID: 48027 RVA: 0x0010D3F7 File Offset: 0x0010B5F7
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008C31 RID: 35889
			// (set) Token: 0x0600BB9C RID: 48028 RVA: 0x0010D415 File Offset: 0x0010B615
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008C32 RID: 35890
			// (set) Token: 0x0600BB9D RID: 48029 RVA: 0x0010D42D File Offset: 0x0010B62D
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008C33 RID: 35891
			// (set) Token: 0x0600BB9E RID: 48030 RVA: 0x0010D445 File Offset: 0x0010B645
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008C34 RID: 35892
			// (set) Token: 0x0600BB9F RID: 48031 RVA: 0x0010D458 File Offset: 0x0010B658
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008C35 RID: 35893
			// (set) Token: 0x0600BBA0 RID: 48032 RVA: 0x0010D470 File Offset: 0x0010B670
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008C36 RID: 35894
			// (set) Token: 0x0600BBA1 RID: 48033 RVA: 0x0010D483 File Offset: 0x0010B683
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008C37 RID: 35895
			// (set) Token: 0x0600BBA2 RID: 48034 RVA: 0x0010D496 File Offset: 0x0010B696
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008C38 RID: 35896
			// (set) Token: 0x0600BBA3 RID: 48035 RVA: 0x0010D4A9 File Offset: 0x0010B6A9
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008C39 RID: 35897
			// (set) Token: 0x0600BBA4 RID: 48036 RVA: 0x0010D4BC File Offset: 0x0010B6BC
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008C3A RID: 35898
			// (set) Token: 0x0600BBA5 RID: 48037 RVA: 0x0010D4D4 File Offset: 0x0010B6D4
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008C3B RID: 35899
			// (set) Token: 0x0600BBA6 RID: 48038 RVA: 0x0010D4E7 File Offset: 0x0010B6E7
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008C3C RID: 35900
			// (set) Token: 0x0600BBA7 RID: 48039 RVA: 0x0010D4FA File Offset: 0x0010B6FA
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008C3D RID: 35901
			// (set) Token: 0x0600BBA8 RID: 48040 RVA: 0x0010D512 File Offset: 0x0010B712
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008C3E RID: 35902
			// (set) Token: 0x0600BBA9 RID: 48041 RVA: 0x0010D525 File Offset: 0x0010B725
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008C3F RID: 35903
			// (set) Token: 0x0600BBAA RID: 48042 RVA: 0x0010D53D File Offset: 0x0010B73D
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008C40 RID: 35904
			// (set) Token: 0x0600BBAB RID: 48043 RVA: 0x0010D550 File Offset: 0x0010B750
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008C41 RID: 35905
			// (set) Token: 0x0600BBAC RID: 48044 RVA: 0x0010D56E File Offset: 0x0010B76E
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008C42 RID: 35906
			// (set) Token: 0x0600BBAD RID: 48045 RVA: 0x0010D581 File Offset: 0x0010B781
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008C43 RID: 35907
			// (set) Token: 0x0600BBAE RID: 48046 RVA: 0x0010D59F File Offset: 0x0010B79F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008C44 RID: 35908
			// (set) Token: 0x0600BBAF RID: 48047 RVA: 0x0010D5B2 File Offset: 0x0010B7B2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008C45 RID: 35909
			// (set) Token: 0x0600BBB0 RID: 48048 RVA: 0x0010D5CA File Offset: 0x0010B7CA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008C46 RID: 35910
			// (set) Token: 0x0600BBB1 RID: 48049 RVA: 0x0010D5E2 File Offset: 0x0010B7E2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008C47 RID: 35911
			// (set) Token: 0x0600BBB2 RID: 48050 RVA: 0x0010D5FA File Offset: 0x0010B7FA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008C48 RID: 35912
			// (set) Token: 0x0600BBB3 RID: 48051 RVA: 0x0010D612 File Offset: 0x0010B812
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D88 RID: 3464
		public class MicrosoftOnlineServicesIDParameters : ParametersBase
		{
			// Token: 0x17008C49 RID: 35913
			// (set) Token: 0x0600BBB5 RID: 48053 RVA: 0x0010D632 File Offset: 0x0010B832
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008C4A RID: 35914
			// (set) Token: 0x0600BBB6 RID: 48054 RVA: 0x0010D645 File Offset: 0x0010B845
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008C4B RID: 35915
			// (set) Token: 0x0600BBB7 RID: 48055 RVA: 0x0010D65D File Offset: 0x0010B85D
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008C4C RID: 35916
			// (set) Token: 0x0600BBB8 RID: 48056 RVA: 0x0010D670 File Offset: 0x0010B870
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008C4D RID: 35917
			// (set) Token: 0x0600BBB9 RID: 48057 RVA: 0x0010D688 File Offset: 0x0010B888
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17008C4E RID: 35918
			// (set) Token: 0x0600BBBA RID: 48058 RVA: 0x0010D69B File Offset: 0x0010B89B
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008C4F RID: 35919
			// (set) Token: 0x0600BBBB RID: 48059 RVA: 0x0010D6B9 File Offset: 0x0010B8B9
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008C50 RID: 35920
			// (set) Token: 0x0600BBBC RID: 48060 RVA: 0x0010D6CC File Offset: 0x0010B8CC
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008C51 RID: 35921
			// (set) Token: 0x0600BBBD RID: 48061 RVA: 0x0010D6E4 File Offset: 0x0010B8E4
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008C52 RID: 35922
			// (set) Token: 0x0600BBBE RID: 48062 RVA: 0x0010D6FC File Offset: 0x0010B8FC
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008C53 RID: 35923
			// (set) Token: 0x0600BBBF RID: 48063 RVA: 0x0010D71A File Offset: 0x0010B91A
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17008C54 RID: 35924
			// (set) Token: 0x0600BBC0 RID: 48064 RVA: 0x0010D72D File Offset: 0x0010B92D
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008C55 RID: 35925
			// (set) Token: 0x0600BBC1 RID: 48065 RVA: 0x0010D745 File Offset: 0x0010B945
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008C56 RID: 35926
			// (set) Token: 0x0600BBC2 RID: 48066 RVA: 0x0010D758 File Offset: 0x0010B958
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008C57 RID: 35927
			// (set) Token: 0x0600BBC3 RID: 48067 RVA: 0x0010D76B File Offset: 0x0010B96B
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008C58 RID: 35928
			// (set) Token: 0x0600BBC4 RID: 48068 RVA: 0x0010D77E File Offset: 0x0010B97E
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008C59 RID: 35929
			// (set) Token: 0x0600BBC5 RID: 48069 RVA: 0x0010D791 File Offset: 0x0010B991
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008C5A RID: 35930
			// (set) Token: 0x0600BBC6 RID: 48070 RVA: 0x0010D7A4 File Offset: 0x0010B9A4
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008C5B RID: 35931
			// (set) Token: 0x0600BBC7 RID: 48071 RVA: 0x0010D7B7 File Offset: 0x0010B9B7
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008C5C RID: 35932
			// (set) Token: 0x0600BBC8 RID: 48072 RVA: 0x0010D7CA File Offset: 0x0010B9CA
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008C5D RID: 35933
			// (set) Token: 0x0600BBC9 RID: 48073 RVA: 0x0010D7E2 File Offset: 0x0010B9E2
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17008C5E RID: 35934
			// (set) Token: 0x0600BBCA RID: 48074 RVA: 0x0010D7F5 File Offset: 0x0010B9F5
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008C5F RID: 35935
			// (set) Token: 0x0600BBCB RID: 48075 RVA: 0x0010D80D File Offset: 0x0010BA0D
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008C60 RID: 35936
			// (set) Token: 0x0600BBCC RID: 48076 RVA: 0x0010D820 File Offset: 0x0010BA20
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008C61 RID: 35937
			// (set) Token: 0x0600BBCD RID: 48077 RVA: 0x0010D833 File Offset: 0x0010BA33
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008C62 RID: 35938
			// (set) Token: 0x0600BBCE RID: 48078 RVA: 0x0010D846 File Offset: 0x0010BA46
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008C63 RID: 35939
			// (set) Token: 0x0600BBCF RID: 48079 RVA: 0x0010D859 File Offset: 0x0010BA59
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008C64 RID: 35940
			// (set) Token: 0x0600BBD0 RID: 48080 RVA: 0x0010D86C File Offset: 0x0010BA6C
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008C65 RID: 35941
			// (set) Token: 0x0600BBD1 RID: 48081 RVA: 0x0010D87F File Offset: 0x0010BA7F
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008C66 RID: 35942
			// (set) Token: 0x0600BBD2 RID: 48082 RVA: 0x0010D892 File Offset: 0x0010BA92
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008C67 RID: 35943
			// (set) Token: 0x0600BBD3 RID: 48083 RVA: 0x0010D8A5 File Offset: 0x0010BAA5
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008C68 RID: 35944
			// (set) Token: 0x0600BBD4 RID: 48084 RVA: 0x0010D8B8 File Offset: 0x0010BAB8
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008C69 RID: 35945
			// (set) Token: 0x0600BBD5 RID: 48085 RVA: 0x0010D8CB File Offset: 0x0010BACB
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008C6A RID: 35946
			// (set) Token: 0x0600BBD6 RID: 48086 RVA: 0x0010D8DE File Offset: 0x0010BADE
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008C6B RID: 35947
			// (set) Token: 0x0600BBD7 RID: 48087 RVA: 0x0010D8F1 File Offset: 0x0010BAF1
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008C6C RID: 35948
			// (set) Token: 0x0600BBD8 RID: 48088 RVA: 0x0010D904 File Offset: 0x0010BB04
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008C6D RID: 35949
			// (set) Token: 0x0600BBD9 RID: 48089 RVA: 0x0010D917 File Offset: 0x0010BB17
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008C6E RID: 35950
			// (set) Token: 0x0600BBDA RID: 48090 RVA: 0x0010D92A File Offset: 0x0010BB2A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008C6F RID: 35951
			// (set) Token: 0x0600BBDB RID: 48091 RVA: 0x0010D93D File Offset: 0x0010BB3D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008C70 RID: 35952
			// (set) Token: 0x0600BBDC RID: 48092 RVA: 0x0010D950 File Offset: 0x0010BB50
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008C71 RID: 35953
			// (set) Token: 0x0600BBDD RID: 48093 RVA: 0x0010D963 File Offset: 0x0010BB63
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008C72 RID: 35954
			// (set) Token: 0x0600BBDE RID: 48094 RVA: 0x0010D976 File Offset: 0x0010BB76
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008C73 RID: 35955
			// (set) Token: 0x0600BBDF RID: 48095 RVA: 0x0010D989 File Offset: 0x0010BB89
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008C74 RID: 35956
			// (set) Token: 0x0600BBE0 RID: 48096 RVA: 0x0010D99C File Offset: 0x0010BB9C
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008C75 RID: 35957
			// (set) Token: 0x0600BBE1 RID: 48097 RVA: 0x0010D9AF File Offset: 0x0010BBAF
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008C76 RID: 35958
			// (set) Token: 0x0600BBE2 RID: 48098 RVA: 0x0010D9C2 File Offset: 0x0010BBC2
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008C77 RID: 35959
			// (set) Token: 0x0600BBE3 RID: 48099 RVA: 0x0010D9DA File Offset: 0x0010BBDA
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008C78 RID: 35960
			// (set) Token: 0x0600BBE4 RID: 48100 RVA: 0x0010D9ED File Offset: 0x0010BBED
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008C79 RID: 35961
			// (set) Token: 0x0600BBE5 RID: 48101 RVA: 0x0010DA05 File Offset: 0x0010BC05
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008C7A RID: 35962
			// (set) Token: 0x0600BBE6 RID: 48102 RVA: 0x0010DA18 File Offset: 0x0010BC18
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008C7B RID: 35963
			// (set) Token: 0x0600BBE7 RID: 48103 RVA: 0x0010DA30 File Offset: 0x0010BC30
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008C7C RID: 35964
			// (set) Token: 0x0600BBE8 RID: 48104 RVA: 0x0010DA48 File Offset: 0x0010BC48
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008C7D RID: 35965
			// (set) Token: 0x0600BBE9 RID: 48105 RVA: 0x0010DA60 File Offset: 0x0010BC60
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17008C7E RID: 35966
			// (set) Token: 0x0600BBEA RID: 48106 RVA: 0x0010DA78 File Offset: 0x0010BC78
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17008C7F RID: 35967
			// (set) Token: 0x0600BBEB RID: 48107 RVA: 0x0010DA90 File Offset: 0x0010BC90
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17008C80 RID: 35968
			// (set) Token: 0x0600BBEC RID: 48108 RVA: 0x0010DAA8 File Offset: 0x0010BCA8
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008C81 RID: 35969
			// (set) Token: 0x0600BBED RID: 48109 RVA: 0x0010DAC0 File Offset: 0x0010BCC0
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008C82 RID: 35970
			// (set) Token: 0x0600BBEE RID: 48110 RVA: 0x0010DAD8 File Offset: 0x0010BCD8
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008C83 RID: 35971
			// (set) Token: 0x0600BBEF RID: 48111 RVA: 0x0010DAF0 File Offset: 0x0010BCF0
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008C84 RID: 35972
			// (set) Token: 0x0600BBF0 RID: 48112 RVA: 0x0010DB03 File Offset: 0x0010BD03
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008C85 RID: 35973
			// (set) Token: 0x0600BBF1 RID: 48113 RVA: 0x0010DB16 File Offset: 0x0010BD16
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008C86 RID: 35974
			// (set) Token: 0x0600BBF2 RID: 48114 RVA: 0x0010DB29 File Offset: 0x0010BD29
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008C87 RID: 35975
			// (set) Token: 0x0600BBF3 RID: 48115 RVA: 0x0010DB3C File Offset: 0x0010BD3C
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008C88 RID: 35976
			// (set) Token: 0x0600BBF4 RID: 48116 RVA: 0x0010DB4F File Offset: 0x0010BD4F
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008C89 RID: 35977
			// (set) Token: 0x0600BBF5 RID: 48117 RVA: 0x0010DB62 File Offset: 0x0010BD62
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008C8A RID: 35978
			// (set) Token: 0x0600BBF6 RID: 48118 RVA: 0x0010DB7A File Offset: 0x0010BD7A
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008C8B RID: 35979
			// (set) Token: 0x0600BBF7 RID: 48119 RVA: 0x0010DB8D File Offset: 0x0010BD8D
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008C8C RID: 35980
			// (set) Token: 0x0600BBF8 RID: 48120 RVA: 0x0010DBA0 File Offset: 0x0010BDA0
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008C8D RID: 35981
			// (set) Token: 0x0600BBF9 RID: 48121 RVA: 0x0010DBB3 File Offset: 0x0010BDB3
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008C8E RID: 35982
			// (set) Token: 0x0600BBFA RID: 48122 RVA: 0x0010DBD1 File Offset: 0x0010BDD1
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17008C8F RID: 35983
			// (set) Token: 0x0600BBFB RID: 48123 RVA: 0x0010DBE4 File Offset: 0x0010BDE4
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17008C90 RID: 35984
			// (set) Token: 0x0600BBFC RID: 48124 RVA: 0x0010DBF7 File Offset: 0x0010BDF7
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17008C91 RID: 35985
			// (set) Token: 0x0600BBFD RID: 48125 RVA: 0x0010DC0A File Offset: 0x0010BE0A
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17008C92 RID: 35986
			// (set) Token: 0x0600BBFE RID: 48126 RVA: 0x0010DC1D File Offset: 0x0010BE1D
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17008C93 RID: 35987
			// (set) Token: 0x0600BBFF RID: 48127 RVA: 0x0010DC30 File Offset: 0x0010BE30
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008C94 RID: 35988
			// (set) Token: 0x0600BC00 RID: 48128 RVA: 0x0010DC43 File Offset: 0x0010BE43
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008C95 RID: 35989
			// (set) Token: 0x0600BC01 RID: 48129 RVA: 0x0010DC56 File Offset: 0x0010BE56
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008C96 RID: 35990
			// (set) Token: 0x0600BC02 RID: 48130 RVA: 0x0010DC69 File Offset: 0x0010BE69
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008C97 RID: 35991
			// (set) Token: 0x0600BC03 RID: 48131 RVA: 0x0010DC7C File Offset: 0x0010BE7C
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008C98 RID: 35992
			// (set) Token: 0x0600BC04 RID: 48132 RVA: 0x0010DC8F File Offset: 0x0010BE8F
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008C99 RID: 35993
			// (set) Token: 0x0600BC05 RID: 48133 RVA: 0x0010DCA2 File Offset: 0x0010BEA2
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008C9A RID: 35994
			// (set) Token: 0x0600BC06 RID: 48134 RVA: 0x0010DCB5 File Offset: 0x0010BEB5
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008C9B RID: 35995
			// (set) Token: 0x0600BC07 RID: 48135 RVA: 0x0010DCC8 File Offset: 0x0010BEC8
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008C9C RID: 35996
			// (set) Token: 0x0600BC08 RID: 48136 RVA: 0x0010DCE6 File Offset: 0x0010BEE6
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008C9D RID: 35997
			// (set) Token: 0x0600BC09 RID: 48137 RVA: 0x0010DCFE File Offset: 0x0010BEFE
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008C9E RID: 35998
			// (set) Token: 0x0600BC0A RID: 48138 RVA: 0x0010DD16 File Offset: 0x0010BF16
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17008C9F RID: 35999
			// (set) Token: 0x0600BC0B RID: 48139 RVA: 0x0010DD2E File Offset: 0x0010BF2E
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17008CA0 RID: 36000
			// (set) Token: 0x0600BC0C RID: 48140 RVA: 0x0010DD41 File Offset: 0x0010BF41
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17008CA1 RID: 36001
			// (set) Token: 0x0600BC0D RID: 48141 RVA: 0x0010DD54 File Offset: 0x0010BF54
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17008CA2 RID: 36002
			// (set) Token: 0x0600BC0E RID: 48142 RVA: 0x0010DD6C File Offset: 0x0010BF6C
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008CA3 RID: 36003
			// (set) Token: 0x0600BC0F RID: 48143 RVA: 0x0010DD7F File Offset: 0x0010BF7F
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008CA4 RID: 36004
			// (set) Token: 0x0600BC10 RID: 48144 RVA: 0x0010DD97 File Offset: 0x0010BF97
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008CA5 RID: 36005
			// (set) Token: 0x0600BC11 RID: 48145 RVA: 0x0010DDAF File Offset: 0x0010BFAF
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008CA6 RID: 36006
			// (set) Token: 0x0600BC12 RID: 48146 RVA: 0x0010DDC2 File Offset: 0x0010BFC2
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008CA7 RID: 36007
			// (set) Token: 0x0600BC13 RID: 48147 RVA: 0x0010DDD5 File Offset: 0x0010BFD5
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008CA8 RID: 36008
			// (set) Token: 0x0600BC14 RID: 48148 RVA: 0x0010DDE8 File Offset: 0x0010BFE8
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008CA9 RID: 36009
			// (set) Token: 0x0600BC15 RID: 48149 RVA: 0x0010DDFB File Offset: 0x0010BFFB
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008CAA RID: 36010
			// (set) Token: 0x0600BC16 RID: 48150 RVA: 0x0010DE13 File Offset: 0x0010C013
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008CAB RID: 36011
			// (set) Token: 0x0600BC17 RID: 48151 RVA: 0x0010DE26 File Offset: 0x0010C026
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008CAC RID: 36012
			// (set) Token: 0x0600BC18 RID: 48152 RVA: 0x0010DE39 File Offset: 0x0010C039
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008CAD RID: 36013
			// (set) Token: 0x0600BC19 RID: 48153 RVA: 0x0010DE51 File Offset: 0x0010C051
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17008CAE RID: 36014
			// (set) Token: 0x0600BC1A RID: 48154 RVA: 0x0010DE69 File Offset: 0x0010C069
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008CAF RID: 36015
			// (set) Token: 0x0600BC1B RID: 48155 RVA: 0x0010DE7C File Offset: 0x0010C07C
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008CB0 RID: 36016
			// (set) Token: 0x0600BC1C RID: 48156 RVA: 0x0010DE94 File Offset: 0x0010C094
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008CB1 RID: 36017
			// (set) Token: 0x0600BC1D RID: 48157 RVA: 0x0010DEA7 File Offset: 0x0010C0A7
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008CB2 RID: 36018
			// (set) Token: 0x0600BC1E RID: 48158 RVA: 0x0010DEBA File Offset: 0x0010C0BA
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008CB3 RID: 36019
			// (set) Token: 0x0600BC1F RID: 48159 RVA: 0x0010DECD File Offset: 0x0010C0CD
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008CB4 RID: 36020
			// (set) Token: 0x0600BC20 RID: 48160 RVA: 0x0010DEEB File Offset: 0x0010C0EB
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008CB5 RID: 36021
			// (set) Token: 0x0600BC21 RID: 48161 RVA: 0x0010DF09 File Offset: 0x0010C109
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008CB6 RID: 36022
			// (set) Token: 0x0600BC22 RID: 48162 RVA: 0x0010DF27 File Offset: 0x0010C127
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008CB7 RID: 36023
			// (set) Token: 0x0600BC23 RID: 48163 RVA: 0x0010DF45 File Offset: 0x0010C145
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008CB8 RID: 36024
			// (set) Token: 0x0600BC24 RID: 48164 RVA: 0x0010DF58 File Offset: 0x0010C158
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008CB9 RID: 36025
			// (set) Token: 0x0600BC25 RID: 48165 RVA: 0x0010DF70 File Offset: 0x0010C170
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008CBA RID: 36026
			// (set) Token: 0x0600BC26 RID: 48166 RVA: 0x0010DF8E File Offset: 0x0010C18E
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008CBB RID: 36027
			// (set) Token: 0x0600BC27 RID: 48167 RVA: 0x0010DFA6 File Offset: 0x0010C1A6
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008CBC RID: 36028
			// (set) Token: 0x0600BC28 RID: 48168 RVA: 0x0010DFBE File Offset: 0x0010C1BE
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008CBD RID: 36029
			// (set) Token: 0x0600BC29 RID: 48169 RVA: 0x0010DFD1 File Offset: 0x0010C1D1
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008CBE RID: 36030
			// (set) Token: 0x0600BC2A RID: 48170 RVA: 0x0010DFE9 File Offset: 0x0010C1E9
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008CBF RID: 36031
			// (set) Token: 0x0600BC2B RID: 48171 RVA: 0x0010DFFC File Offset: 0x0010C1FC
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008CC0 RID: 36032
			// (set) Token: 0x0600BC2C RID: 48172 RVA: 0x0010E00F File Offset: 0x0010C20F
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008CC1 RID: 36033
			// (set) Token: 0x0600BC2D RID: 48173 RVA: 0x0010E022 File Offset: 0x0010C222
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008CC2 RID: 36034
			// (set) Token: 0x0600BC2E RID: 48174 RVA: 0x0010E035 File Offset: 0x0010C235
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008CC3 RID: 36035
			// (set) Token: 0x0600BC2F RID: 48175 RVA: 0x0010E04D File Offset: 0x0010C24D
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008CC4 RID: 36036
			// (set) Token: 0x0600BC30 RID: 48176 RVA: 0x0010E060 File Offset: 0x0010C260
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008CC5 RID: 36037
			// (set) Token: 0x0600BC31 RID: 48177 RVA: 0x0010E073 File Offset: 0x0010C273
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008CC6 RID: 36038
			// (set) Token: 0x0600BC32 RID: 48178 RVA: 0x0010E08B File Offset: 0x0010C28B
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008CC7 RID: 36039
			// (set) Token: 0x0600BC33 RID: 48179 RVA: 0x0010E09E File Offset: 0x0010C29E
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008CC8 RID: 36040
			// (set) Token: 0x0600BC34 RID: 48180 RVA: 0x0010E0B6 File Offset: 0x0010C2B6
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008CC9 RID: 36041
			// (set) Token: 0x0600BC35 RID: 48181 RVA: 0x0010E0C9 File Offset: 0x0010C2C9
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008CCA RID: 36042
			// (set) Token: 0x0600BC36 RID: 48182 RVA: 0x0010E0E7 File Offset: 0x0010C2E7
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008CCB RID: 36043
			// (set) Token: 0x0600BC37 RID: 48183 RVA: 0x0010E0FA File Offset: 0x0010C2FA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008CCC RID: 36044
			// (set) Token: 0x0600BC38 RID: 48184 RVA: 0x0010E118 File Offset: 0x0010C318
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008CCD RID: 36045
			// (set) Token: 0x0600BC39 RID: 48185 RVA: 0x0010E12B File Offset: 0x0010C32B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008CCE RID: 36046
			// (set) Token: 0x0600BC3A RID: 48186 RVA: 0x0010E143 File Offset: 0x0010C343
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008CCF RID: 36047
			// (set) Token: 0x0600BC3B RID: 48187 RVA: 0x0010E15B File Offset: 0x0010C35B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008CD0 RID: 36048
			// (set) Token: 0x0600BC3C RID: 48188 RVA: 0x0010E173 File Offset: 0x0010C373
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008CD1 RID: 36049
			// (set) Token: 0x0600BC3D RID: 48189 RVA: 0x0010E18B File Offset: 0x0010C38B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D89 RID: 3465
		public class UserParameters : ParametersBase
		{
			// Token: 0x17008CD2 RID: 36050
			// (set) Token: 0x0600BC3F RID: 48191 RVA: 0x0010E1AB File Offset: 0x0010C3AB
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008CD3 RID: 36051
			// (set) Token: 0x0600BC40 RID: 48192 RVA: 0x0010E1BE File Offset: 0x0010C3BE
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008CD4 RID: 36052
			// (set) Token: 0x0600BC41 RID: 48193 RVA: 0x0010E1D6 File Offset: 0x0010C3D6
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008CD5 RID: 36053
			// (set) Token: 0x0600BC42 RID: 48194 RVA: 0x0010E1E9 File Offset: 0x0010C3E9
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008CD6 RID: 36054
			// (set) Token: 0x0600BC43 RID: 48195 RVA: 0x0010E201 File Offset: 0x0010C401
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17008CD7 RID: 36055
			// (set) Token: 0x0600BC44 RID: 48196 RVA: 0x0010E214 File Offset: 0x0010C414
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17008CD8 RID: 36056
			// (set) Token: 0x0600BC45 RID: 48197 RVA: 0x0010E227 File Offset: 0x0010C427
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008CD9 RID: 36057
			// (set) Token: 0x0600BC46 RID: 48198 RVA: 0x0010E245 File Offset: 0x0010C445
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008CDA RID: 36058
			// (set) Token: 0x0600BC47 RID: 48199 RVA: 0x0010E258 File Offset: 0x0010C458
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008CDB RID: 36059
			// (set) Token: 0x0600BC48 RID: 48200 RVA: 0x0010E270 File Offset: 0x0010C470
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008CDC RID: 36060
			// (set) Token: 0x0600BC49 RID: 48201 RVA: 0x0010E288 File Offset: 0x0010C488
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008CDD RID: 36061
			// (set) Token: 0x0600BC4A RID: 48202 RVA: 0x0010E2A6 File Offset: 0x0010C4A6
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008CDE RID: 36062
			// (set) Token: 0x0600BC4B RID: 48203 RVA: 0x0010E2BE File Offset: 0x0010C4BE
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008CDF RID: 36063
			// (set) Token: 0x0600BC4C RID: 48204 RVA: 0x0010E2D1 File Offset: 0x0010C4D1
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008CE0 RID: 36064
			// (set) Token: 0x0600BC4D RID: 48205 RVA: 0x0010E2E4 File Offset: 0x0010C4E4
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008CE1 RID: 36065
			// (set) Token: 0x0600BC4E RID: 48206 RVA: 0x0010E2F7 File Offset: 0x0010C4F7
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008CE2 RID: 36066
			// (set) Token: 0x0600BC4F RID: 48207 RVA: 0x0010E30A File Offset: 0x0010C50A
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008CE3 RID: 36067
			// (set) Token: 0x0600BC50 RID: 48208 RVA: 0x0010E31D File Offset: 0x0010C51D
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008CE4 RID: 36068
			// (set) Token: 0x0600BC51 RID: 48209 RVA: 0x0010E330 File Offset: 0x0010C530
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008CE5 RID: 36069
			// (set) Token: 0x0600BC52 RID: 48210 RVA: 0x0010E343 File Offset: 0x0010C543
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008CE6 RID: 36070
			// (set) Token: 0x0600BC53 RID: 48211 RVA: 0x0010E35B File Offset: 0x0010C55B
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17008CE7 RID: 36071
			// (set) Token: 0x0600BC54 RID: 48212 RVA: 0x0010E36E File Offset: 0x0010C56E
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008CE8 RID: 36072
			// (set) Token: 0x0600BC55 RID: 48213 RVA: 0x0010E386 File Offset: 0x0010C586
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008CE9 RID: 36073
			// (set) Token: 0x0600BC56 RID: 48214 RVA: 0x0010E399 File Offset: 0x0010C599
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008CEA RID: 36074
			// (set) Token: 0x0600BC57 RID: 48215 RVA: 0x0010E3AC File Offset: 0x0010C5AC
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008CEB RID: 36075
			// (set) Token: 0x0600BC58 RID: 48216 RVA: 0x0010E3BF File Offset: 0x0010C5BF
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008CEC RID: 36076
			// (set) Token: 0x0600BC59 RID: 48217 RVA: 0x0010E3D2 File Offset: 0x0010C5D2
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008CED RID: 36077
			// (set) Token: 0x0600BC5A RID: 48218 RVA: 0x0010E3E5 File Offset: 0x0010C5E5
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008CEE RID: 36078
			// (set) Token: 0x0600BC5B RID: 48219 RVA: 0x0010E3F8 File Offset: 0x0010C5F8
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008CEF RID: 36079
			// (set) Token: 0x0600BC5C RID: 48220 RVA: 0x0010E40B File Offset: 0x0010C60B
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008CF0 RID: 36080
			// (set) Token: 0x0600BC5D RID: 48221 RVA: 0x0010E41E File Offset: 0x0010C61E
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008CF1 RID: 36081
			// (set) Token: 0x0600BC5E RID: 48222 RVA: 0x0010E431 File Offset: 0x0010C631
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008CF2 RID: 36082
			// (set) Token: 0x0600BC5F RID: 48223 RVA: 0x0010E444 File Offset: 0x0010C644
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008CF3 RID: 36083
			// (set) Token: 0x0600BC60 RID: 48224 RVA: 0x0010E457 File Offset: 0x0010C657
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008CF4 RID: 36084
			// (set) Token: 0x0600BC61 RID: 48225 RVA: 0x0010E46A File Offset: 0x0010C66A
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008CF5 RID: 36085
			// (set) Token: 0x0600BC62 RID: 48226 RVA: 0x0010E47D File Offset: 0x0010C67D
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008CF6 RID: 36086
			// (set) Token: 0x0600BC63 RID: 48227 RVA: 0x0010E490 File Offset: 0x0010C690
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008CF7 RID: 36087
			// (set) Token: 0x0600BC64 RID: 48228 RVA: 0x0010E4A3 File Offset: 0x0010C6A3
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008CF8 RID: 36088
			// (set) Token: 0x0600BC65 RID: 48229 RVA: 0x0010E4B6 File Offset: 0x0010C6B6
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008CF9 RID: 36089
			// (set) Token: 0x0600BC66 RID: 48230 RVA: 0x0010E4C9 File Offset: 0x0010C6C9
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008CFA RID: 36090
			// (set) Token: 0x0600BC67 RID: 48231 RVA: 0x0010E4DC File Offset: 0x0010C6DC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008CFB RID: 36091
			// (set) Token: 0x0600BC68 RID: 48232 RVA: 0x0010E4EF File Offset: 0x0010C6EF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008CFC RID: 36092
			// (set) Token: 0x0600BC69 RID: 48233 RVA: 0x0010E502 File Offset: 0x0010C702
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008CFD RID: 36093
			// (set) Token: 0x0600BC6A RID: 48234 RVA: 0x0010E515 File Offset: 0x0010C715
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008CFE RID: 36094
			// (set) Token: 0x0600BC6B RID: 48235 RVA: 0x0010E528 File Offset: 0x0010C728
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008CFF RID: 36095
			// (set) Token: 0x0600BC6C RID: 48236 RVA: 0x0010E53B File Offset: 0x0010C73B
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008D00 RID: 36096
			// (set) Token: 0x0600BC6D RID: 48237 RVA: 0x0010E553 File Offset: 0x0010C753
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008D01 RID: 36097
			// (set) Token: 0x0600BC6E RID: 48238 RVA: 0x0010E566 File Offset: 0x0010C766
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008D02 RID: 36098
			// (set) Token: 0x0600BC6F RID: 48239 RVA: 0x0010E57E File Offset: 0x0010C77E
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008D03 RID: 36099
			// (set) Token: 0x0600BC70 RID: 48240 RVA: 0x0010E591 File Offset: 0x0010C791
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008D04 RID: 36100
			// (set) Token: 0x0600BC71 RID: 48241 RVA: 0x0010E5A9 File Offset: 0x0010C7A9
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008D05 RID: 36101
			// (set) Token: 0x0600BC72 RID: 48242 RVA: 0x0010E5C1 File Offset: 0x0010C7C1
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008D06 RID: 36102
			// (set) Token: 0x0600BC73 RID: 48243 RVA: 0x0010E5D9 File Offset: 0x0010C7D9
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17008D07 RID: 36103
			// (set) Token: 0x0600BC74 RID: 48244 RVA: 0x0010E5F1 File Offset: 0x0010C7F1
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17008D08 RID: 36104
			// (set) Token: 0x0600BC75 RID: 48245 RVA: 0x0010E609 File Offset: 0x0010C809
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17008D09 RID: 36105
			// (set) Token: 0x0600BC76 RID: 48246 RVA: 0x0010E621 File Offset: 0x0010C821
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008D0A RID: 36106
			// (set) Token: 0x0600BC77 RID: 48247 RVA: 0x0010E639 File Offset: 0x0010C839
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008D0B RID: 36107
			// (set) Token: 0x0600BC78 RID: 48248 RVA: 0x0010E651 File Offset: 0x0010C851
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008D0C RID: 36108
			// (set) Token: 0x0600BC79 RID: 48249 RVA: 0x0010E669 File Offset: 0x0010C869
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008D0D RID: 36109
			// (set) Token: 0x0600BC7A RID: 48250 RVA: 0x0010E67C File Offset: 0x0010C87C
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008D0E RID: 36110
			// (set) Token: 0x0600BC7B RID: 48251 RVA: 0x0010E68F File Offset: 0x0010C88F
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008D0F RID: 36111
			// (set) Token: 0x0600BC7C RID: 48252 RVA: 0x0010E6A2 File Offset: 0x0010C8A2
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008D10 RID: 36112
			// (set) Token: 0x0600BC7D RID: 48253 RVA: 0x0010E6B5 File Offset: 0x0010C8B5
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008D11 RID: 36113
			// (set) Token: 0x0600BC7E RID: 48254 RVA: 0x0010E6C8 File Offset: 0x0010C8C8
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008D12 RID: 36114
			// (set) Token: 0x0600BC7F RID: 48255 RVA: 0x0010E6DB File Offset: 0x0010C8DB
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008D13 RID: 36115
			// (set) Token: 0x0600BC80 RID: 48256 RVA: 0x0010E6F3 File Offset: 0x0010C8F3
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008D14 RID: 36116
			// (set) Token: 0x0600BC81 RID: 48257 RVA: 0x0010E706 File Offset: 0x0010C906
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008D15 RID: 36117
			// (set) Token: 0x0600BC82 RID: 48258 RVA: 0x0010E719 File Offset: 0x0010C919
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008D16 RID: 36118
			// (set) Token: 0x0600BC83 RID: 48259 RVA: 0x0010E72C File Offset: 0x0010C92C
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008D17 RID: 36119
			// (set) Token: 0x0600BC84 RID: 48260 RVA: 0x0010E74A File Offset: 0x0010C94A
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17008D18 RID: 36120
			// (set) Token: 0x0600BC85 RID: 48261 RVA: 0x0010E75D File Offset: 0x0010C95D
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17008D19 RID: 36121
			// (set) Token: 0x0600BC86 RID: 48262 RVA: 0x0010E770 File Offset: 0x0010C970
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17008D1A RID: 36122
			// (set) Token: 0x0600BC87 RID: 48263 RVA: 0x0010E783 File Offset: 0x0010C983
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17008D1B RID: 36123
			// (set) Token: 0x0600BC88 RID: 48264 RVA: 0x0010E796 File Offset: 0x0010C996
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17008D1C RID: 36124
			// (set) Token: 0x0600BC89 RID: 48265 RVA: 0x0010E7A9 File Offset: 0x0010C9A9
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008D1D RID: 36125
			// (set) Token: 0x0600BC8A RID: 48266 RVA: 0x0010E7BC File Offset: 0x0010C9BC
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008D1E RID: 36126
			// (set) Token: 0x0600BC8B RID: 48267 RVA: 0x0010E7CF File Offset: 0x0010C9CF
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008D1F RID: 36127
			// (set) Token: 0x0600BC8C RID: 48268 RVA: 0x0010E7E2 File Offset: 0x0010C9E2
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008D20 RID: 36128
			// (set) Token: 0x0600BC8D RID: 48269 RVA: 0x0010E7F5 File Offset: 0x0010C9F5
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008D21 RID: 36129
			// (set) Token: 0x0600BC8E RID: 48270 RVA: 0x0010E808 File Offset: 0x0010CA08
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008D22 RID: 36130
			// (set) Token: 0x0600BC8F RID: 48271 RVA: 0x0010E81B File Offset: 0x0010CA1B
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008D23 RID: 36131
			// (set) Token: 0x0600BC90 RID: 48272 RVA: 0x0010E82E File Offset: 0x0010CA2E
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008D24 RID: 36132
			// (set) Token: 0x0600BC91 RID: 48273 RVA: 0x0010E841 File Offset: 0x0010CA41
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008D25 RID: 36133
			// (set) Token: 0x0600BC92 RID: 48274 RVA: 0x0010E85F File Offset: 0x0010CA5F
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008D26 RID: 36134
			// (set) Token: 0x0600BC93 RID: 48275 RVA: 0x0010E877 File Offset: 0x0010CA77
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008D27 RID: 36135
			// (set) Token: 0x0600BC94 RID: 48276 RVA: 0x0010E88F File Offset: 0x0010CA8F
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17008D28 RID: 36136
			// (set) Token: 0x0600BC95 RID: 48277 RVA: 0x0010E8A7 File Offset: 0x0010CAA7
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17008D29 RID: 36137
			// (set) Token: 0x0600BC96 RID: 48278 RVA: 0x0010E8BA File Offset: 0x0010CABA
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17008D2A RID: 36138
			// (set) Token: 0x0600BC97 RID: 48279 RVA: 0x0010E8CD File Offset: 0x0010CACD
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17008D2B RID: 36139
			// (set) Token: 0x0600BC98 RID: 48280 RVA: 0x0010E8E5 File Offset: 0x0010CAE5
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008D2C RID: 36140
			// (set) Token: 0x0600BC99 RID: 48281 RVA: 0x0010E8F8 File Offset: 0x0010CAF8
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008D2D RID: 36141
			// (set) Token: 0x0600BC9A RID: 48282 RVA: 0x0010E910 File Offset: 0x0010CB10
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008D2E RID: 36142
			// (set) Token: 0x0600BC9B RID: 48283 RVA: 0x0010E928 File Offset: 0x0010CB28
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008D2F RID: 36143
			// (set) Token: 0x0600BC9C RID: 48284 RVA: 0x0010E93B File Offset: 0x0010CB3B
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008D30 RID: 36144
			// (set) Token: 0x0600BC9D RID: 48285 RVA: 0x0010E94E File Offset: 0x0010CB4E
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008D31 RID: 36145
			// (set) Token: 0x0600BC9E RID: 48286 RVA: 0x0010E961 File Offset: 0x0010CB61
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008D32 RID: 36146
			// (set) Token: 0x0600BC9F RID: 48287 RVA: 0x0010E974 File Offset: 0x0010CB74
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008D33 RID: 36147
			// (set) Token: 0x0600BCA0 RID: 48288 RVA: 0x0010E98C File Offset: 0x0010CB8C
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008D34 RID: 36148
			// (set) Token: 0x0600BCA1 RID: 48289 RVA: 0x0010E99F File Offset: 0x0010CB9F
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008D35 RID: 36149
			// (set) Token: 0x0600BCA2 RID: 48290 RVA: 0x0010E9B2 File Offset: 0x0010CBB2
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008D36 RID: 36150
			// (set) Token: 0x0600BCA3 RID: 48291 RVA: 0x0010E9CA File Offset: 0x0010CBCA
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17008D37 RID: 36151
			// (set) Token: 0x0600BCA4 RID: 48292 RVA: 0x0010E9E2 File Offset: 0x0010CBE2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008D38 RID: 36152
			// (set) Token: 0x0600BCA5 RID: 48293 RVA: 0x0010E9F5 File Offset: 0x0010CBF5
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008D39 RID: 36153
			// (set) Token: 0x0600BCA6 RID: 48294 RVA: 0x0010EA0D File Offset: 0x0010CC0D
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008D3A RID: 36154
			// (set) Token: 0x0600BCA7 RID: 48295 RVA: 0x0010EA20 File Offset: 0x0010CC20
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008D3B RID: 36155
			// (set) Token: 0x0600BCA8 RID: 48296 RVA: 0x0010EA33 File Offset: 0x0010CC33
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008D3C RID: 36156
			// (set) Token: 0x0600BCA9 RID: 48297 RVA: 0x0010EA46 File Offset: 0x0010CC46
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008D3D RID: 36157
			// (set) Token: 0x0600BCAA RID: 48298 RVA: 0x0010EA64 File Offset: 0x0010CC64
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008D3E RID: 36158
			// (set) Token: 0x0600BCAB RID: 48299 RVA: 0x0010EA82 File Offset: 0x0010CC82
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008D3F RID: 36159
			// (set) Token: 0x0600BCAC RID: 48300 RVA: 0x0010EAA0 File Offset: 0x0010CCA0
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008D40 RID: 36160
			// (set) Token: 0x0600BCAD RID: 48301 RVA: 0x0010EABE File Offset: 0x0010CCBE
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008D41 RID: 36161
			// (set) Token: 0x0600BCAE RID: 48302 RVA: 0x0010EAD1 File Offset: 0x0010CCD1
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008D42 RID: 36162
			// (set) Token: 0x0600BCAF RID: 48303 RVA: 0x0010EAE9 File Offset: 0x0010CCE9
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008D43 RID: 36163
			// (set) Token: 0x0600BCB0 RID: 48304 RVA: 0x0010EB07 File Offset: 0x0010CD07
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008D44 RID: 36164
			// (set) Token: 0x0600BCB1 RID: 48305 RVA: 0x0010EB1F File Offset: 0x0010CD1F
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008D45 RID: 36165
			// (set) Token: 0x0600BCB2 RID: 48306 RVA: 0x0010EB37 File Offset: 0x0010CD37
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008D46 RID: 36166
			// (set) Token: 0x0600BCB3 RID: 48307 RVA: 0x0010EB4A File Offset: 0x0010CD4A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008D47 RID: 36167
			// (set) Token: 0x0600BCB4 RID: 48308 RVA: 0x0010EB62 File Offset: 0x0010CD62
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008D48 RID: 36168
			// (set) Token: 0x0600BCB5 RID: 48309 RVA: 0x0010EB75 File Offset: 0x0010CD75
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008D49 RID: 36169
			// (set) Token: 0x0600BCB6 RID: 48310 RVA: 0x0010EB88 File Offset: 0x0010CD88
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008D4A RID: 36170
			// (set) Token: 0x0600BCB7 RID: 48311 RVA: 0x0010EB9B File Offset: 0x0010CD9B
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008D4B RID: 36171
			// (set) Token: 0x0600BCB8 RID: 48312 RVA: 0x0010EBAE File Offset: 0x0010CDAE
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008D4C RID: 36172
			// (set) Token: 0x0600BCB9 RID: 48313 RVA: 0x0010EBC6 File Offset: 0x0010CDC6
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008D4D RID: 36173
			// (set) Token: 0x0600BCBA RID: 48314 RVA: 0x0010EBD9 File Offset: 0x0010CDD9
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008D4E RID: 36174
			// (set) Token: 0x0600BCBB RID: 48315 RVA: 0x0010EBEC File Offset: 0x0010CDEC
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008D4F RID: 36175
			// (set) Token: 0x0600BCBC RID: 48316 RVA: 0x0010EC04 File Offset: 0x0010CE04
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008D50 RID: 36176
			// (set) Token: 0x0600BCBD RID: 48317 RVA: 0x0010EC17 File Offset: 0x0010CE17
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008D51 RID: 36177
			// (set) Token: 0x0600BCBE RID: 48318 RVA: 0x0010EC2F File Offset: 0x0010CE2F
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008D52 RID: 36178
			// (set) Token: 0x0600BCBF RID: 48319 RVA: 0x0010EC42 File Offset: 0x0010CE42
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008D53 RID: 36179
			// (set) Token: 0x0600BCC0 RID: 48320 RVA: 0x0010EC60 File Offset: 0x0010CE60
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008D54 RID: 36180
			// (set) Token: 0x0600BCC1 RID: 48321 RVA: 0x0010EC73 File Offset: 0x0010CE73
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008D55 RID: 36181
			// (set) Token: 0x0600BCC2 RID: 48322 RVA: 0x0010EC91 File Offset: 0x0010CE91
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008D56 RID: 36182
			// (set) Token: 0x0600BCC3 RID: 48323 RVA: 0x0010ECA4 File Offset: 0x0010CEA4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008D57 RID: 36183
			// (set) Token: 0x0600BCC4 RID: 48324 RVA: 0x0010ECBC File Offset: 0x0010CEBC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008D58 RID: 36184
			// (set) Token: 0x0600BCC5 RID: 48325 RVA: 0x0010ECD4 File Offset: 0x0010CED4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008D59 RID: 36185
			// (set) Token: 0x0600BCC6 RID: 48326 RVA: 0x0010ECEC File Offset: 0x0010CEEC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008D5A RID: 36186
			// (set) Token: 0x0600BCC7 RID: 48327 RVA: 0x0010ED04 File Offset: 0x0010CF04
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D8A RID: 3466
		public class WindowsLiveIDParameters : ParametersBase
		{
			// Token: 0x17008D5B RID: 36187
			// (set) Token: 0x0600BCC9 RID: 48329 RVA: 0x0010ED24 File Offset: 0x0010CF24
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008D5C RID: 36188
			// (set) Token: 0x0600BCCA RID: 48330 RVA: 0x0010ED37 File Offset: 0x0010CF37
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008D5D RID: 36189
			// (set) Token: 0x0600BCCB RID: 48331 RVA: 0x0010ED4F File Offset: 0x0010CF4F
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008D5E RID: 36190
			// (set) Token: 0x0600BCCC RID: 48332 RVA: 0x0010ED62 File Offset: 0x0010CF62
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008D5F RID: 36191
			// (set) Token: 0x0600BCCD RID: 48333 RVA: 0x0010ED7A File Offset: 0x0010CF7A
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17008D60 RID: 36192
			// (set) Token: 0x0600BCCE RID: 48334 RVA: 0x0010ED8D File Offset: 0x0010CF8D
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008D61 RID: 36193
			// (set) Token: 0x0600BCCF RID: 48335 RVA: 0x0010EDAB File Offset: 0x0010CFAB
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008D62 RID: 36194
			// (set) Token: 0x0600BCD0 RID: 48336 RVA: 0x0010EDBE File Offset: 0x0010CFBE
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008D63 RID: 36195
			// (set) Token: 0x0600BCD1 RID: 48337 RVA: 0x0010EDD6 File Offset: 0x0010CFD6
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008D64 RID: 36196
			// (set) Token: 0x0600BCD2 RID: 48338 RVA: 0x0010EDEE File Offset: 0x0010CFEE
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008D65 RID: 36197
			// (set) Token: 0x0600BCD3 RID: 48339 RVA: 0x0010EE0C File Offset: 0x0010D00C
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17008D66 RID: 36198
			// (set) Token: 0x0600BCD4 RID: 48340 RVA: 0x0010EE1F File Offset: 0x0010D01F
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x17008D67 RID: 36199
			// (set) Token: 0x0600BCD5 RID: 48341 RVA: 0x0010EE37 File Offset: 0x0010D037
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008D68 RID: 36200
			// (set) Token: 0x0600BCD6 RID: 48342 RVA: 0x0010EE4F File Offset: 0x0010D04F
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008D69 RID: 36201
			// (set) Token: 0x0600BCD7 RID: 48343 RVA: 0x0010EE62 File Offset: 0x0010D062
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008D6A RID: 36202
			// (set) Token: 0x0600BCD8 RID: 48344 RVA: 0x0010EE75 File Offset: 0x0010D075
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008D6B RID: 36203
			// (set) Token: 0x0600BCD9 RID: 48345 RVA: 0x0010EE88 File Offset: 0x0010D088
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008D6C RID: 36204
			// (set) Token: 0x0600BCDA RID: 48346 RVA: 0x0010EE9B File Offset: 0x0010D09B
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008D6D RID: 36205
			// (set) Token: 0x0600BCDB RID: 48347 RVA: 0x0010EEAE File Offset: 0x0010D0AE
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008D6E RID: 36206
			// (set) Token: 0x0600BCDC RID: 48348 RVA: 0x0010EEC1 File Offset: 0x0010D0C1
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008D6F RID: 36207
			// (set) Token: 0x0600BCDD RID: 48349 RVA: 0x0010EED4 File Offset: 0x0010D0D4
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008D70 RID: 36208
			// (set) Token: 0x0600BCDE RID: 48350 RVA: 0x0010EEEC File Offset: 0x0010D0EC
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17008D71 RID: 36209
			// (set) Token: 0x0600BCDF RID: 48351 RVA: 0x0010EEFF File Offset: 0x0010D0FF
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008D72 RID: 36210
			// (set) Token: 0x0600BCE0 RID: 48352 RVA: 0x0010EF17 File Offset: 0x0010D117
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008D73 RID: 36211
			// (set) Token: 0x0600BCE1 RID: 48353 RVA: 0x0010EF2A File Offset: 0x0010D12A
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008D74 RID: 36212
			// (set) Token: 0x0600BCE2 RID: 48354 RVA: 0x0010EF3D File Offset: 0x0010D13D
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008D75 RID: 36213
			// (set) Token: 0x0600BCE3 RID: 48355 RVA: 0x0010EF50 File Offset: 0x0010D150
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008D76 RID: 36214
			// (set) Token: 0x0600BCE4 RID: 48356 RVA: 0x0010EF63 File Offset: 0x0010D163
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008D77 RID: 36215
			// (set) Token: 0x0600BCE5 RID: 48357 RVA: 0x0010EF76 File Offset: 0x0010D176
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008D78 RID: 36216
			// (set) Token: 0x0600BCE6 RID: 48358 RVA: 0x0010EF89 File Offset: 0x0010D189
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008D79 RID: 36217
			// (set) Token: 0x0600BCE7 RID: 48359 RVA: 0x0010EF9C File Offset: 0x0010D19C
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008D7A RID: 36218
			// (set) Token: 0x0600BCE8 RID: 48360 RVA: 0x0010EFAF File Offset: 0x0010D1AF
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008D7B RID: 36219
			// (set) Token: 0x0600BCE9 RID: 48361 RVA: 0x0010EFC2 File Offset: 0x0010D1C2
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008D7C RID: 36220
			// (set) Token: 0x0600BCEA RID: 48362 RVA: 0x0010EFD5 File Offset: 0x0010D1D5
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008D7D RID: 36221
			// (set) Token: 0x0600BCEB RID: 48363 RVA: 0x0010EFE8 File Offset: 0x0010D1E8
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008D7E RID: 36222
			// (set) Token: 0x0600BCEC RID: 48364 RVA: 0x0010EFFB File Offset: 0x0010D1FB
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008D7F RID: 36223
			// (set) Token: 0x0600BCED RID: 48365 RVA: 0x0010F00E File Offset: 0x0010D20E
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008D80 RID: 36224
			// (set) Token: 0x0600BCEE RID: 48366 RVA: 0x0010F021 File Offset: 0x0010D221
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008D81 RID: 36225
			// (set) Token: 0x0600BCEF RID: 48367 RVA: 0x0010F034 File Offset: 0x0010D234
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008D82 RID: 36226
			// (set) Token: 0x0600BCF0 RID: 48368 RVA: 0x0010F047 File Offset: 0x0010D247
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008D83 RID: 36227
			// (set) Token: 0x0600BCF1 RID: 48369 RVA: 0x0010F05A File Offset: 0x0010D25A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008D84 RID: 36228
			// (set) Token: 0x0600BCF2 RID: 48370 RVA: 0x0010F06D File Offset: 0x0010D26D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008D85 RID: 36229
			// (set) Token: 0x0600BCF3 RID: 48371 RVA: 0x0010F080 File Offset: 0x0010D280
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008D86 RID: 36230
			// (set) Token: 0x0600BCF4 RID: 48372 RVA: 0x0010F093 File Offset: 0x0010D293
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008D87 RID: 36231
			// (set) Token: 0x0600BCF5 RID: 48373 RVA: 0x0010F0A6 File Offset: 0x0010D2A6
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008D88 RID: 36232
			// (set) Token: 0x0600BCF6 RID: 48374 RVA: 0x0010F0B9 File Offset: 0x0010D2B9
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008D89 RID: 36233
			// (set) Token: 0x0600BCF7 RID: 48375 RVA: 0x0010F0CC File Offset: 0x0010D2CC
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008D8A RID: 36234
			// (set) Token: 0x0600BCF8 RID: 48376 RVA: 0x0010F0E4 File Offset: 0x0010D2E4
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008D8B RID: 36235
			// (set) Token: 0x0600BCF9 RID: 48377 RVA: 0x0010F0F7 File Offset: 0x0010D2F7
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008D8C RID: 36236
			// (set) Token: 0x0600BCFA RID: 48378 RVA: 0x0010F10F File Offset: 0x0010D30F
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008D8D RID: 36237
			// (set) Token: 0x0600BCFB RID: 48379 RVA: 0x0010F122 File Offset: 0x0010D322
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008D8E RID: 36238
			// (set) Token: 0x0600BCFC RID: 48380 RVA: 0x0010F13A File Offset: 0x0010D33A
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008D8F RID: 36239
			// (set) Token: 0x0600BCFD RID: 48381 RVA: 0x0010F152 File Offset: 0x0010D352
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008D90 RID: 36240
			// (set) Token: 0x0600BCFE RID: 48382 RVA: 0x0010F16A File Offset: 0x0010D36A
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17008D91 RID: 36241
			// (set) Token: 0x0600BCFF RID: 48383 RVA: 0x0010F182 File Offset: 0x0010D382
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17008D92 RID: 36242
			// (set) Token: 0x0600BD00 RID: 48384 RVA: 0x0010F19A File Offset: 0x0010D39A
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17008D93 RID: 36243
			// (set) Token: 0x0600BD01 RID: 48385 RVA: 0x0010F1B2 File Offset: 0x0010D3B2
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008D94 RID: 36244
			// (set) Token: 0x0600BD02 RID: 48386 RVA: 0x0010F1CA File Offset: 0x0010D3CA
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008D95 RID: 36245
			// (set) Token: 0x0600BD03 RID: 48387 RVA: 0x0010F1E2 File Offset: 0x0010D3E2
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008D96 RID: 36246
			// (set) Token: 0x0600BD04 RID: 48388 RVA: 0x0010F1FA File Offset: 0x0010D3FA
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008D97 RID: 36247
			// (set) Token: 0x0600BD05 RID: 48389 RVA: 0x0010F20D File Offset: 0x0010D40D
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008D98 RID: 36248
			// (set) Token: 0x0600BD06 RID: 48390 RVA: 0x0010F220 File Offset: 0x0010D420
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008D99 RID: 36249
			// (set) Token: 0x0600BD07 RID: 48391 RVA: 0x0010F233 File Offset: 0x0010D433
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008D9A RID: 36250
			// (set) Token: 0x0600BD08 RID: 48392 RVA: 0x0010F246 File Offset: 0x0010D446
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008D9B RID: 36251
			// (set) Token: 0x0600BD09 RID: 48393 RVA: 0x0010F259 File Offset: 0x0010D459
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008D9C RID: 36252
			// (set) Token: 0x0600BD0A RID: 48394 RVA: 0x0010F26C File Offset: 0x0010D46C
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008D9D RID: 36253
			// (set) Token: 0x0600BD0B RID: 48395 RVA: 0x0010F284 File Offset: 0x0010D484
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008D9E RID: 36254
			// (set) Token: 0x0600BD0C RID: 48396 RVA: 0x0010F297 File Offset: 0x0010D497
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008D9F RID: 36255
			// (set) Token: 0x0600BD0D RID: 48397 RVA: 0x0010F2AA File Offset: 0x0010D4AA
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008DA0 RID: 36256
			// (set) Token: 0x0600BD0E RID: 48398 RVA: 0x0010F2BD File Offset: 0x0010D4BD
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008DA1 RID: 36257
			// (set) Token: 0x0600BD0F RID: 48399 RVA: 0x0010F2DB File Offset: 0x0010D4DB
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17008DA2 RID: 36258
			// (set) Token: 0x0600BD10 RID: 48400 RVA: 0x0010F2EE File Offset: 0x0010D4EE
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17008DA3 RID: 36259
			// (set) Token: 0x0600BD11 RID: 48401 RVA: 0x0010F301 File Offset: 0x0010D501
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17008DA4 RID: 36260
			// (set) Token: 0x0600BD12 RID: 48402 RVA: 0x0010F314 File Offset: 0x0010D514
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17008DA5 RID: 36261
			// (set) Token: 0x0600BD13 RID: 48403 RVA: 0x0010F327 File Offset: 0x0010D527
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17008DA6 RID: 36262
			// (set) Token: 0x0600BD14 RID: 48404 RVA: 0x0010F33A File Offset: 0x0010D53A
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008DA7 RID: 36263
			// (set) Token: 0x0600BD15 RID: 48405 RVA: 0x0010F34D File Offset: 0x0010D54D
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008DA8 RID: 36264
			// (set) Token: 0x0600BD16 RID: 48406 RVA: 0x0010F360 File Offset: 0x0010D560
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008DA9 RID: 36265
			// (set) Token: 0x0600BD17 RID: 48407 RVA: 0x0010F373 File Offset: 0x0010D573
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008DAA RID: 36266
			// (set) Token: 0x0600BD18 RID: 48408 RVA: 0x0010F386 File Offset: 0x0010D586
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008DAB RID: 36267
			// (set) Token: 0x0600BD19 RID: 48409 RVA: 0x0010F399 File Offset: 0x0010D599
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008DAC RID: 36268
			// (set) Token: 0x0600BD1A RID: 48410 RVA: 0x0010F3AC File Offset: 0x0010D5AC
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008DAD RID: 36269
			// (set) Token: 0x0600BD1B RID: 48411 RVA: 0x0010F3BF File Offset: 0x0010D5BF
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008DAE RID: 36270
			// (set) Token: 0x0600BD1C RID: 48412 RVA: 0x0010F3D2 File Offset: 0x0010D5D2
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008DAF RID: 36271
			// (set) Token: 0x0600BD1D RID: 48413 RVA: 0x0010F3F0 File Offset: 0x0010D5F0
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008DB0 RID: 36272
			// (set) Token: 0x0600BD1E RID: 48414 RVA: 0x0010F408 File Offset: 0x0010D608
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008DB1 RID: 36273
			// (set) Token: 0x0600BD1F RID: 48415 RVA: 0x0010F420 File Offset: 0x0010D620
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17008DB2 RID: 36274
			// (set) Token: 0x0600BD20 RID: 48416 RVA: 0x0010F438 File Offset: 0x0010D638
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17008DB3 RID: 36275
			// (set) Token: 0x0600BD21 RID: 48417 RVA: 0x0010F44B File Offset: 0x0010D64B
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17008DB4 RID: 36276
			// (set) Token: 0x0600BD22 RID: 48418 RVA: 0x0010F45E File Offset: 0x0010D65E
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17008DB5 RID: 36277
			// (set) Token: 0x0600BD23 RID: 48419 RVA: 0x0010F476 File Offset: 0x0010D676
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008DB6 RID: 36278
			// (set) Token: 0x0600BD24 RID: 48420 RVA: 0x0010F489 File Offset: 0x0010D689
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008DB7 RID: 36279
			// (set) Token: 0x0600BD25 RID: 48421 RVA: 0x0010F4A1 File Offset: 0x0010D6A1
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008DB8 RID: 36280
			// (set) Token: 0x0600BD26 RID: 48422 RVA: 0x0010F4B9 File Offset: 0x0010D6B9
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008DB9 RID: 36281
			// (set) Token: 0x0600BD27 RID: 48423 RVA: 0x0010F4CC File Offset: 0x0010D6CC
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008DBA RID: 36282
			// (set) Token: 0x0600BD28 RID: 48424 RVA: 0x0010F4DF File Offset: 0x0010D6DF
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008DBB RID: 36283
			// (set) Token: 0x0600BD29 RID: 48425 RVA: 0x0010F4F2 File Offset: 0x0010D6F2
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008DBC RID: 36284
			// (set) Token: 0x0600BD2A RID: 48426 RVA: 0x0010F505 File Offset: 0x0010D705
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008DBD RID: 36285
			// (set) Token: 0x0600BD2B RID: 48427 RVA: 0x0010F51D File Offset: 0x0010D71D
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008DBE RID: 36286
			// (set) Token: 0x0600BD2C RID: 48428 RVA: 0x0010F530 File Offset: 0x0010D730
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008DBF RID: 36287
			// (set) Token: 0x0600BD2D RID: 48429 RVA: 0x0010F543 File Offset: 0x0010D743
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008DC0 RID: 36288
			// (set) Token: 0x0600BD2E RID: 48430 RVA: 0x0010F55B File Offset: 0x0010D75B
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17008DC1 RID: 36289
			// (set) Token: 0x0600BD2F RID: 48431 RVA: 0x0010F573 File Offset: 0x0010D773
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008DC2 RID: 36290
			// (set) Token: 0x0600BD30 RID: 48432 RVA: 0x0010F586 File Offset: 0x0010D786
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008DC3 RID: 36291
			// (set) Token: 0x0600BD31 RID: 48433 RVA: 0x0010F59E File Offset: 0x0010D79E
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008DC4 RID: 36292
			// (set) Token: 0x0600BD32 RID: 48434 RVA: 0x0010F5B1 File Offset: 0x0010D7B1
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008DC5 RID: 36293
			// (set) Token: 0x0600BD33 RID: 48435 RVA: 0x0010F5C4 File Offset: 0x0010D7C4
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008DC6 RID: 36294
			// (set) Token: 0x0600BD34 RID: 48436 RVA: 0x0010F5D7 File Offset: 0x0010D7D7
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008DC7 RID: 36295
			// (set) Token: 0x0600BD35 RID: 48437 RVA: 0x0010F5F5 File Offset: 0x0010D7F5
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008DC8 RID: 36296
			// (set) Token: 0x0600BD36 RID: 48438 RVA: 0x0010F613 File Offset: 0x0010D813
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008DC9 RID: 36297
			// (set) Token: 0x0600BD37 RID: 48439 RVA: 0x0010F631 File Offset: 0x0010D831
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008DCA RID: 36298
			// (set) Token: 0x0600BD38 RID: 48440 RVA: 0x0010F64F File Offset: 0x0010D84F
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008DCB RID: 36299
			// (set) Token: 0x0600BD39 RID: 48441 RVA: 0x0010F662 File Offset: 0x0010D862
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008DCC RID: 36300
			// (set) Token: 0x0600BD3A RID: 48442 RVA: 0x0010F67A File Offset: 0x0010D87A
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008DCD RID: 36301
			// (set) Token: 0x0600BD3B RID: 48443 RVA: 0x0010F698 File Offset: 0x0010D898
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008DCE RID: 36302
			// (set) Token: 0x0600BD3C RID: 48444 RVA: 0x0010F6B0 File Offset: 0x0010D8B0
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008DCF RID: 36303
			// (set) Token: 0x0600BD3D RID: 48445 RVA: 0x0010F6C8 File Offset: 0x0010D8C8
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008DD0 RID: 36304
			// (set) Token: 0x0600BD3E RID: 48446 RVA: 0x0010F6DB File Offset: 0x0010D8DB
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008DD1 RID: 36305
			// (set) Token: 0x0600BD3F RID: 48447 RVA: 0x0010F6F3 File Offset: 0x0010D8F3
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008DD2 RID: 36306
			// (set) Token: 0x0600BD40 RID: 48448 RVA: 0x0010F706 File Offset: 0x0010D906
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008DD3 RID: 36307
			// (set) Token: 0x0600BD41 RID: 48449 RVA: 0x0010F719 File Offset: 0x0010D919
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008DD4 RID: 36308
			// (set) Token: 0x0600BD42 RID: 48450 RVA: 0x0010F72C File Offset: 0x0010D92C
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008DD5 RID: 36309
			// (set) Token: 0x0600BD43 RID: 48451 RVA: 0x0010F73F File Offset: 0x0010D93F
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008DD6 RID: 36310
			// (set) Token: 0x0600BD44 RID: 48452 RVA: 0x0010F757 File Offset: 0x0010D957
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008DD7 RID: 36311
			// (set) Token: 0x0600BD45 RID: 48453 RVA: 0x0010F76A File Offset: 0x0010D96A
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008DD8 RID: 36312
			// (set) Token: 0x0600BD46 RID: 48454 RVA: 0x0010F77D File Offset: 0x0010D97D
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008DD9 RID: 36313
			// (set) Token: 0x0600BD47 RID: 48455 RVA: 0x0010F795 File Offset: 0x0010D995
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008DDA RID: 36314
			// (set) Token: 0x0600BD48 RID: 48456 RVA: 0x0010F7A8 File Offset: 0x0010D9A8
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008DDB RID: 36315
			// (set) Token: 0x0600BD49 RID: 48457 RVA: 0x0010F7C0 File Offset: 0x0010D9C0
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008DDC RID: 36316
			// (set) Token: 0x0600BD4A RID: 48458 RVA: 0x0010F7D3 File Offset: 0x0010D9D3
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008DDD RID: 36317
			// (set) Token: 0x0600BD4B RID: 48459 RVA: 0x0010F7F1 File Offset: 0x0010D9F1
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008DDE RID: 36318
			// (set) Token: 0x0600BD4C RID: 48460 RVA: 0x0010F804 File Offset: 0x0010DA04
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008DDF RID: 36319
			// (set) Token: 0x0600BD4D RID: 48461 RVA: 0x0010F822 File Offset: 0x0010DA22
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008DE0 RID: 36320
			// (set) Token: 0x0600BD4E RID: 48462 RVA: 0x0010F835 File Offset: 0x0010DA35
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008DE1 RID: 36321
			// (set) Token: 0x0600BD4F RID: 48463 RVA: 0x0010F84D File Offset: 0x0010DA4D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008DE2 RID: 36322
			// (set) Token: 0x0600BD50 RID: 48464 RVA: 0x0010F865 File Offset: 0x0010DA65
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008DE3 RID: 36323
			// (set) Token: 0x0600BD51 RID: 48465 RVA: 0x0010F87D File Offset: 0x0010DA7D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008DE4 RID: 36324
			// (set) Token: 0x0600BD52 RID: 48466 RVA: 0x0010F895 File Offset: 0x0010DA95
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D8B RID: 3467
		public class DisabledUserParameters : ParametersBase
		{
			// Token: 0x17008DE5 RID: 36325
			// (set) Token: 0x0600BD54 RID: 48468 RVA: 0x0010F8B5 File Offset: 0x0010DAB5
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008DE6 RID: 36326
			// (set) Token: 0x0600BD55 RID: 48469 RVA: 0x0010F8C8 File Offset: 0x0010DAC8
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008DE7 RID: 36327
			// (set) Token: 0x0600BD56 RID: 48470 RVA: 0x0010F8E0 File Offset: 0x0010DAE0
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008DE8 RID: 36328
			// (set) Token: 0x0600BD57 RID: 48471 RVA: 0x0010F8F3 File Offset: 0x0010DAF3
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008DE9 RID: 36329
			// (set) Token: 0x0600BD58 RID: 48472 RVA: 0x0010F90B File Offset: 0x0010DB0B
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17008DEA RID: 36330
			// (set) Token: 0x0600BD59 RID: 48473 RVA: 0x0010F91E File Offset: 0x0010DB1E
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17008DEB RID: 36331
			// (set) Token: 0x0600BD5A RID: 48474 RVA: 0x0010F931 File Offset: 0x0010DB31
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008DEC RID: 36332
			// (set) Token: 0x0600BD5B RID: 48475 RVA: 0x0010F94F File Offset: 0x0010DB4F
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008DED RID: 36333
			// (set) Token: 0x0600BD5C RID: 48476 RVA: 0x0010F962 File Offset: 0x0010DB62
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008DEE RID: 36334
			// (set) Token: 0x0600BD5D RID: 48477 RVA: 0x0010F97A File Offset: 0x0010DB7A
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008DEF RID: 36335
			// (set) Token: 0x0600BD5E RID: 48478 RVA: 0x0010F992 File Offset: 0x0010DB92
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008DF0 RID: 36336
			// (set) Token: 0x0600BD5F RID: 48479 RVA: 0x0010F9AA File Offset: 0x0010DBAA
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008DF1 RID: 36337
			// (set) Token: 0x0600BD60 RID: 48480 RVA: 0x0010F9BD File Offset: 0x0010DBBD
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008DF2 RID: 36338
			// (set) Token: 0x0600BD61 RID: 48481 RVA: 0x0010F9D0 File Offset: 0x0010DBD0
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008DF3 RID: 36339
			// (set) Token: 0x0600BD62 RID: 48482 RVA: 0x0010F9E3 File Offset: 0x0010DBE3
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008DF4 RID: 36340
			// (set) Token: 0x0600BD63 RID: 48483 RVA: 0x0010F9F6 File Offset: 0x0010DBF6
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008DF5 RID: 36341
			// (set) Token: 0x0600BD64 RID: 48484 RVA: 0x0010FA09 File Offset: 0x0010DC09
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008DF6 RID: 36342
			// (set) Token: 0x0600BD65 RID: 48485 RVA: 0x0010FA1C File Offset: 0x0010DC1C
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008DF7 RID: 36343
			// (set) Token: 0x0600BD66 RID: 48486 RVA: 0x0010FA2F File Offset: 0x0010DC2F
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008DF8 RID: 36344
			// (set) Token: 0x0600BD67 RID: 48487 RVA: 0x0010FA47 File Offset: 0x0010DC47
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17008DF9 RID: 36345
			// (set) Token: 0x0600BD68 RID: 48488 RVA: 0x0010FA5A File Offset: 0x0010DC5A
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008DFA RID: 36346
			// (set) Token: 0x0600BD69 RID: 48489 RVA: 0x0010FA72 File Offset: 0x0010DC72
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008DFB RID: 36347
			// (set) Token: 0x0600BD6A RID: 48490 RVA: 0x0010FA85 File Offset: 0x0010DC85
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008DFC RID: 36348
			// (set) Token: 0x0600BD6B RID: 48491 RVA: 0x0010FA98 File Offset: 0x0010DC98
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008DFD RID: 36349
			// (set) Token: 0x0600BD6C RID: 48492 RVA: 0x0010FAAB File Offset: 0x0010DCAB
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008DFE RID: 36350
			// (set) Token: 0x0600BD6D RID: 48493 RVA: 0x0010FABE File Offset: 0x0010DCBE
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008DFF RID: 36351
			// (set) Token: 0x0600BD6E RID: 48494 RVA: 0x0010FAD1 File Offset: 0x0010DCD1
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008E00 RID: 36352
			// (set) Token: 0x0600BD6F RID: 48495 RVA: 0x0010FAE4 File Offset: 0x0010DCE4
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008E01 RID: 36353
			// (set) Token: 0x0600BD70 RID: 48496 RVA: 0x0010FAF7 File Offset: 0x0010DCF7
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008E02 RID: 36354
			// (set) Token: 0x0600BD71 RID: 48497 RVA: 0x0010FB0A File Offset: 0x0010DD0A
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008E03 RID: 36355
			// (set) Token: 0x0600BD72 RID: 48498 RVA: 0x0010FB1D File Offset: 0x0010DD1D
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008E04 RID: 36356
			// (set) Token: 0x0600BD73 RID: 48499 RVA: 0x0010FB30 File Offset: 0x0010DD30
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008E05 RID: 36357
			// (set) Token: 0x0600BD74 RID: 48500 RVA: 0x0010FB43 File Offset: 0x0010DD43
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008E06 RID: 36358
			// (set) Token: 0x0600BD75 RID: 48501 RVA: 0x0010FB56 File Offset: 0x0010DD56
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008E07 RID: 36359
			// (set) Token: 0x0600BD76 RID: 48502 RVA: 0x0010FB69 File Offset: 0x0010DD69
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008E08 RID: 36360
			// (set) Token: 0x0600BD77 RID: 48503 RVA: 0x0010FB7C File Offset: 0x0010DD7C
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008E09 RID: 36361
			// (set) Token: 0x0600BD78 RID: 48504 RVA: 0x0010FB8F File Offset: 0x0010DD8F
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008E0A RID: 36362
			// (set) Token: 0x0600BD79 RID: 48505 RVA: 0x0010FBA2 File Offset: 0x0010DDA2
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008E0B RID: 36363
			// (set) Token: 0x0600BD7A RID: 48506 RVA: 0x0010FBB5 File Offset: 0x0010DDB5
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008E0C RID: 36364
			// (set) Token: 0x0600BD7B RID: 48507 RVA: 0x0010FBC8 File Offset: 0x0010DDC8
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008E0D RID: 36365
			// (set) Token: 0x0600BD7C RID: 48508 RVA: 0x0010FBDB File Offset: 0x0010DDDB
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008E0E RID: 36366
			// (set) Token: 0x0600BD7D RID: 48509 RVA: 0x0010FBEE File Offset: 0x0010DDEE
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008E0F RID: 36367
			// (set) Token: 0x0600BD7E RID: 48510 RVA: 0x0010FC01 File Offset: 0x0010DE01
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008E10 RID: 36368
			// (set) Token: 0x0600BD7F RID: 48511 RVA: 0x0010FC14 File Offset: 0x0010DE14
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008E11 RID: 36369
			// (set) Token: 0x0600BD80 RID: 48512 RVA: 0x0010FC27 File Offset: 0x0010DE27
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008E12 RID: 36370
			// (set) Token: 0x0600BD81 RID: 48513 RVA: 0x0010FC3F File Offset: 0x0010DE3F
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008E13 RID: 36371
			// (set) Token: 0x0600BD82 RID: 48514 RVA: 0x0010FC52 File Offset: 0x0010DE52
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008E14 RID: 36372
			// (set) Token: 0x0600BD83 RID: 48515 RVA: 0x0010FC6A File Offset: 0x0010DE6A
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008E15 RID: 36373
			// (set) Token: 0x0600BD84 RID: 48516 RVA: 0x0010FC7D File Offset: 0x0010DE7D
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008E16 RID: 36374
			// (set) Token: 0x0600BD85 RID: 48517 RVA: 0x0010FC95 File Offset: 0x0010DE95
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008E17 RID: 36375
			// (set) Token: 0x0600BD86 RID: 48518 RVA: 0x0010FCAD File Offset: 0x0010DEAD
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008E18 RID: 36376
			// (set) Token: 0x0600BD87 RID: 48519 RVA: 0x0010FCC5 File Offset: 0x0010DEC5
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17008E19 RID: 36377
			// (set) Token: 0x0600BD88 RID: 48520 RVA: 0x0010FCDD File Offset: 0x0010DEDD
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17008E1A RID: 36378
			// (set) Token: 0x0600BD89 RID: 48521 RVA: 0x0010FCF5 File Offset: 0x0010DEF5
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17008E1B RID: 36379
			// (set) Token: 0x0600BD8A RID: 48522 RVA: 0x0010FD0D File Offset: 0x0010DF0D
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008E1C RID: 36380
			// (set) Token: 0x0600BD8B RID: 48523 RVA: 0x0010FD25 File Offset: 0x0010DF25
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008E1D RID: 36381
			// (set) Token: 0x0600BD8C RID: 48524 RVA: 0x0010FD3D File Offset: 0x0010DF3D
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008E1E RID: 36382
			// (set) Token: 0x0600BD8D RID: 48525 RVA: 0x0010FD55 File Offset: 0x0010DF55
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008E1F RID: 36383
			// (set) Token: 0x0600BD8E RID: 48526 RVA: 0x0010FD68 File Offset: 0x0010DF68
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008E20 RID: 36384
			// (set) Token: 0x0600BD8F RID: 48527 RVA: 0x0010FD7B File Offset: 0x0010DF7B
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008E21 RID: 36385
			// (set) Token: 0x0600BD90 RID: 48528 RVA: 0x0010FD8E File Offset: 0x0010DF8E
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008E22 RID: 36386
			// (set) Token: 0x0600BD91 RID: 48529 RVA: 0x0010FDA1 File Offset: 0x0010DFA1
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008E23 RID: 36387
			// (set) Token: 0x0600BD92 RID: 48530 RVA: 0x0010FDB4 File Offset: 0x0010DFB4
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008E24 RID: 36388
			// (set) Token: 0x0600BD93 RID: 48531 RVA: 0x0010FDC7 File Offset: 0x0010DFC7
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008E25 RID: 36389
			// (set) Token: 0x0600BD94 RID: 48532 RVA: 0x0010FDDF File Offset: 0x0010DFDF
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008E26 RID: 36390
			// (set) Token: 0x0600BD95 RID: 48533 RVA: 0x0010FDF2 File Offset: 0x0010DFF2
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008E27 RID: 36391
			// (set) Token: 0x0600BD96 RID: 48534 RVA: 0x0010FE05 File Offset: 0x0010E005
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008E28 RID: 36392
			// (set) Token: 0x0600BD97 RID: 48535 RVA: 0x0010FE18 File Offset: 0x0010E018
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008E29 RID: 36393
			// (set) Token: 0x0600BD98 RID: 48536 RVA: 0x0010FE36 File Offset: 0x0010E036
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17008E2A RID: 36394
			// (set) Token: 0x0600BD99 RID: 48537 RVA: 0x0010FE49 File Offset: 0x0010E049
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17008E2B RID: 36395
			// (set) Token: 0x0600BD9A RID: 48538 RVA: 0x0010FE5C File Offset: 0x0010E05C
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17008E2C RID: 36396
			// (set) Token: 0x0600BD9B RID: 48539 RVA: 0x0010FE6F File Offset: 0x0010E06F
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17008E2D RID: 36397
			// (set) Token: 0x0600BD9C RID: 48540 RVA: 0x0010FE82 File Offset: 0x0010E082
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17008E2E RID: 36398
			// (set) Token: 0x0600BD9D RID: 48541 RVA: 0x0010FE95 File Offset: 0x0010E095
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008E2F RID: 36399
			// (set) Token: 0x0600BD9E RID: 48542 RVA: 0x0010FEA8 File Offset: 0x0010E0A8
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008E30 RID: 36400
			// (set) Token: 0x0600BD9F RID: 48543 RVA: 0x0010FEBB File Offset: 0x0010E0BB
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008E31 RID: 36401
			// (set) Token: 0x0600BDA0 RID: 48544 RVA: 0x0010FECE File Offset: 0x0010E0CE
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008E32 RID: 36402
			// (set) Token: 0x0600BDA1 RID: 48545 RVA: 0x0010FEE1 File Offset: 0x0010E0E1
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008E33 RID: 36403
			// (set) Token: 0x0600BDA2 RID: 48546 RVA: 0x0010FEF4 File Offset: 0x0010E0F4
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008E34 RID: 36404
			// (set) Token: 0x0600BDA3 RID: 48547 RVA: 0x0010FF07 File Offset: 0x0010E107
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008E35 RID: 36405
			// (set) Token: 0x0600BDA4 RID: 48548 RVA: 0x0010FF1A File Offset: 0x0010E11A
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008E36 RID: 36406
			// (set) Token: 0x0600BDA5 RID: 48549 RVA: 0x0010FF2D File Offset: 0x0010E12D
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008E37 RID: 36407
			// (set) Token: 0x0600BDA6 RID: 48550 RVA: 0x0010FF4B File Offset: 0x0010E14B
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008E38 RID: 36408
			// (set) Token: 0x0600BDA7 RID: 48551 RVA: 0x0010FF63 File Offset: 0x0010E163
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008E39 RID: 36409
			// (set) Token: 0x0600BDA8 RID: 48552 RVA: 0x0010FF7B File Offset: 0x0010E17B
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17008E3A RID: 36410
			// (set) Token: 0x0600BDA9 RID: 48553 RVA: 0x0010FF93 File Offset: 0x0010E193
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17008E3B RID: 36411
			// (set) Token: 0x0600BDAA RID: 48554 RVA: 0x0010FFA6 File Offset: 0x0010E1A6
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17008E3C RID: 36412
			// (set) Token: 0x0600BDAB RID: 48555 RVA: 0x0010FFB9 File Offset: 0x0010E1B9
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17008E3D RID: 36413
			// (set) Token: 0x0600BDAC RID: 48556 RVA: 0x0010FFD1 File Offset: 0x0010E1D1
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008E3E RID: 36414
			// (set) Token: 0x0600BDAD RID: 48557 RVA: 0x0010FFE4 File Offset: 0x0010E1E4
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008E3F RID: 36415
			// (set) Token: 0x0600BDAE RID: 48558 RVA: 0x0010FFFC File Offset: 0x0010E1FC
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008E40 RID: 36416
			// (set) Token: 0x0600BDAF RID: 48559 RVA: 0x00110014 File Offset: 0x0010E214
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008E41 RID: 36417
			// (set) Token: 0x0600BDB0 RID: 48560 RVA: 0x00110027 File Offset: 0x0010E227
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008E42 RID: 36418
			// (set) Token: 0x0600BDB1 RID: 48561 RVA: 0x0011003A File Offset: 0x0010E23A
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008E43 RID: 36419
			// (set) Token: 0x0600BDB2 RID: 48562 RVA: 0x0011004D File Offset: 0x0010E24D
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008E44 RID: 36420
			// (set) Token: 0x0600BDB3 RID: 48563 RVA: 0x00110060 File Offset: 0x0010E260
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008E45 RID: 36421
			// (set) Token: 0x0600BDB4 RID: 48564 RVA: 0x00110078 File Offset: 0x0010E278
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008E46 RID: 36422
			// (set) Token: 0x0600BDB5 RID: 48565 RVA: 0x0011008B File Offset: 0x0010E28B
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008E47 RID: 36423
			// (set) Token: 0x0600BDB6 RID: 48566 RVA: 0x0011009E File Offset: 0x0010E29E
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008E48 RID: 36424
			// (set) Token: 0x0600BDB7 RID: 48567 RVA: 0x001100B6 File Offset: 0x0010E2B6
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17008E49 RID: 36425
			// (set) Token: 0x0600BDB8 RID: 48568 RVA: 0x001100CE File Offset: 0x0010E2CE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008E4A RID: 36426
			// (set) Token: 0x0600BDB9 RID: 48569 RVA: 0x001100E1 File Offset: 0x0010E2E1
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008E4B RID: 36427
			// (set) Token: 0x0600BDBA RID: 48570 RVA: 0x001100F9 File Offset: 0x0010E2F9
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008E4C RID: 36428
			// (set) Token: 0x0600BDBB RID: 48571 RVA: 0x0011010C File Offset: 0x0010E30C
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008E4D RID: 36429
			// (set) Token: 0x0600BDBC RID: 48572 RVA: 0x0011011F File Offset: 0x0010E31F
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008E4E RID: 36430
			// (set) Token: 0x0600BDBD RID: 48573 RVA: 0x00110132 File Offset: 0x0010E332
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008E4F RID: 36431
			// (set) Token: 0x0600BDBE RID: 48574 RVA: 0x00110150 File Offset: 0x0010E350
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008E50 RID: 36432
			// (set) Token: 0x0600BDBF RID: 48575 RVA: 0x0011016E File Offset: 0x0010E36E
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008E51 RID: 36433
			// (set) Token: 0x0600BDC0 RID: 48576 RVA: 0x0011018C File Offset: 0x0010E38C
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008E52 RID: 36434
			// (set) Token: 0x0600BDC1 RID: 48577 RVA: 0x001101AA File Offset: 0x0010E3AA
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008E53 RID: 36435
			// (set) Token: 0x0600BDC2 RID: 48578 RVA: 0x001101BD File Offset: 0x0010E3BD
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008E54 RID: 36436
			// (set) Token: 0x0600BDC3 RID: 48579 RVA: 0x001101D5 File Offset: 0x0010E3D5
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008E55 RID: 36437
			// (set) Token: 0x0600BDC4 RID: 48580 RVA: 0x001101F3 File Offset: 0x0010E3F3
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008E56 RID: 36438
			// (set) Token: 0x0600BDC5 RID: 48581 RVA: 0x0011020B File Offset: 0x0010E40B
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008E57 RID: 36439
			// (set) Token: 0x0600BDC6 RID: 48582 RVA: 0x00110223 File Offset: 0x0010E423
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008E58 RID: 36440
			// (set) Token: 0x0600BDC7 RID: 48583 RVA: 0x00110236 File Offset: 0x0010E436
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008E59 RID: 36441
			// (set) Token: 0x0600BDC8 RID: 48584 RVA: 0x0011024E File Offset: 0x0010E44E
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008E5A RID: 36442
			// (set) Token: 0x0600BDC9 RID: 48585 RVA: 0x00110261 File Offset: 0x0010E461
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008E5B RID: 36443
			// (set) Token: 0x0600BDCA RID: 48586 RVA: 0x00110274 File Offset: 0x0010E474
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008E5C RID: 36444
			// (set) Token: 0x0600BDCB RID: 48587 RVA: 0x00110287 File Offset: 0x0010E487
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008E5D RID: 36445
			// (set) Token: 0x0600BDCC RID: 48588 RVA: 0x0011029A File Offset: 0x0010E49A
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008E5E RID: 36446
			// (set) Token: 0x0600BDCD RID: 48589 RVA: 0x001102B2 File Offset: 0x0010E4B2
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008E5F RID: 36447
			// (set) Token: 0x0600BDCE RID: 48590 RVA: 0x001102C5 File Offset: 0x0010E4C5
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008E60 RID: 36448
			// (set) Token: 0x0600BDCF RID: 48591 RVA: 0x001102D8 File Offset: 0x0010E4D8
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008E61 RID: 36449
			// (set) Token: 0x0600BDD0 RID: 48592 RVA: 0x001102F0 File Offset: 0x0010E4F0
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008E62 RID: 36450
			// (set) Token: 0x0600BDD1 RID: 48593 RVA: 0x00110303 File Offset: 0x0010E503
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008E63 RID: 36451
			// (set) Token: 0x0600BDD2 RID: 48594 RVA: 0x0011031B File Offset: 0x0010E51B
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008E64 RID: 36452
			// (set) Token: 0x0600BDD3 RID: 48595 RVA: 0x0011032E File Offset: 0x0010E52E
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008E65 RID: 36453
			// (set) Token: 0x0600BDD4 RID: 48596 RVA: 0x0011034C File Offset: 0x0010E54C
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008E66 RID: 36454
			// (set) Token: 0x0600BDD5 RID: 48597 RVA: 0x0011035F File Offset: 0x0010E55F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008E67 RID: 36455
			// (set) Token: 0x0600BDD6 RID: 48598 RVA: 0x0011037D File Offset: 0x0010E57D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008E68 RID: 36456
			// (set) Token: 0x0600BDD7 RID: 48599 RVA: 0x00110390 File Offset: 0x0010E590
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008E69 RID: 36457
			// (set) Token: 0x0600BDD8 RID: 48600 RVA: 0x001103A8 File Offset: 0x0010E5A8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008E6A RID: 36458
			// (set) Token: 0x0600BDD9 RID: 48601 RVA: 0x001103C0 File Offset: 0x0010E5C0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008E6B RID: 36459
			// (set) Token: 0x0600BDDA RID: 48602 RVA: 0x001103D8 File Offset: 0x0010E5D8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008E6C RID: 36460
			// (set) Token: 0x0600BDDB RID: 48603 RVA: 0x001103F0 File Offset: 0x0010E5F0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D8C RID: 3468
		public class MicrosoftOnlineServicesFederatedUserParameters : ParametersBase
		{
			// Token: 0x17008E6D RID: 36461
			// (set) Token: 0x0600BDDD RID: 48605 RVA: 0x00110410 File Offset: 0x0010E610
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008E6E RID: 36462
			// (set) Token: 0x0600BDDE RID: 48606 RVA: 0x00110423 File Offset: 0x0010E623
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008E6F RID: 36463
			// (set) Token: 0x0600BDDF RID: 48607 RVA: 0x0011043B File Offset: 0x0010E63B
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008E70 RID: 36464
			// (set) Token: 0x0600BDE0 RID: 48608 RVA: 0x0011044E File Offset: 0x0010E64E
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008E71 RID: 36465
			// (set) Token: 0x0600BDE1 RID: 48609 RVA: 0x00110466 File Offset: 0x0010E666
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008E72 RID: 36466
			// (set) Token: 0x0600BDE2 RID: 48610 RVA: 0x00110484 File Offset: 0x0010E684
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17008E73 RID: 36467
			// (set) Token: 0x0600BDE3 RID: 48611 RVA: 0x00110497 File Offset: 0x0010E697
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17008E74 RID: 36468
			// (set) Token: 0x0600BDE4 RID: 48612 RVA: 0x001104AA File Offset: 0x0010E6AA
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008E75 RID: 36469
			// (set) Token: 0x0600BDE5 RID: 48613 RVA: 0x001104C2 File Offset: 0x0010E6C2
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008E76 RID: 36470
			// (set) Token: 0x0600BDE6 RID: 48614 RVA: 0x001104D5 File Offset: 0x0010E6D5
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008E77 RID: 36471
			// (set) Token: 0x0600BDE7 RID: 48615 RVA: 0x001104E8 File Offset: 0x0010E6E8
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008E78 RID: 36472
			// (set) Token: 0x0600BDE8 RID: 48616 RVA: 0x001104FB File Offset: 0x0010E6FB
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008E79 RID: 36473
			// (set) Token: 0x0600BDE9 RID: 48617 RVA: 0x0011050E File Offset: 0x0010E70E
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008E7A RID: 36474
			// (set) Token: 0x0600BDEA RID: 48618 RVA: 0x00110521 File Offset: 0x0010E721
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008E7B RID: 36475
			// (set) Token: 0x0600BDEB RID: 48619 RVA: 0x00110534 File Offset: 0x0010E734
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008E7C RID: 36476
			// (set) Token: 0x0600BDEC RID: 48620 RVA: 0x00110547 File Offset: 0x0010E747
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008E7D RID: 36477
			// (set) Token: 0x0600BDED RID: 48621 RVA: 0x0011055F File Offset: 0x0010E75F
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17008E7E RID: 36478
			// (set) Token: 0x0600BDEE RID: 48622 RVA: 0x00110572 File Offset: 0x0010E772
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008E7F RID: 36479
			// (set) Token: 0x0600BDEF RID: 48623 RVA: 0x0011058A File Offset: 0x0010E78A
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008E80 RID: 36480
			// (set) Token: 0x0600BDF0 RID: 48624 RVA: 0x0011059D File Offset: 0x0010E79D
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008E81 RID: 36481
			// (set) Token: 0x0600BDF1 RID: 48625 RVA: 0x001105B0 File Offset: 0x0010E7B0
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008E82 RID: 36482
			// (set) Token: 0x0600BDF2 RID: 48626 RVA: 0x001105C3 File Offset: 0x0010E7C3
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008E83 RID: 36483
			// (set) Token: 0x0600BDF3 RID: 48627 RVA: 0x001105D6 File Offset: 0x0010E7D6
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008E84 RID: 36484
			// (set) Token: 0x0600BDF4 RID: 48628 RVA: 0x001105E9 File Offset: 0x0010E7E9
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008E85 RID: 36485
			// (set) Token: 0x0600BDF5 RID: 48629 RVA: 0x001105FC File Offset: 0x0010E7FC
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008E86 RID: 36486
			// (set) Token: 0x0600BDF6 RID: 48630 RVA: 0x0011060F File Offset: 0x0010E80F
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008E87 RID: 36487
			// (set) Token: 0x0600BDF7 RID: 48631 RVA: 0x00110622 File Offset: 0x0010E822
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008E88 RID: 36488
			// (set) Token: 0x0600BDF8 RID: 48632 RVA: 0x00110635 File Offset: 0x0010E835
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008E89 RID: 36489
			// (set) Token: 0x0600BDF9 RID: 48633 RVA: 0x00110648 File Offset: 0x0010E848
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008E8A RID: 36490
			// (set) Token: 0x0600BDFA RID: 48634 RVA: 0x0011065B File Offset: 0x0010E85B
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008E8B RID: 36491
			// (set) Token: 0x0600BDFB RID: 48635 RVA: 0x0011066E File Offset: 0x0010E86E
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008E8C RID: 36492
			// (set) Token: 0x0600BDFC RID: 48636 RVA: 0x00110681 File Offset: 0x0010E881
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008E8D RID: 36493
			// (set) Token: 0x0600BDFD RID: 48637 RVA: 0x00110694 File Offset: 0x0010E894
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008E8E RID: 36494
			// (set) Token: 0x0600BDFE RID: 48638 RVA: 0x001106A7 File Offset: 0x0010E8A7
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008E8F RID: 36495
			// (set) Token: 0x0600BDFF RID: 48639 RVA: 0x001106BA File Offset: 0x0010E8BA
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008E90 RID: 36496
			// (set) Token: 0x0600BE00 RID: 48640 RVA: 0x001106CD File Offset: 0x0010E8CD
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008E91 RID: 36497
			// (set) Token: 0x0600BE01 RID: 48641 RVA: 0x001106E0 File Offset: 0x0010E8E0
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008E92 RID: 36498
			// (set) Token: 0x0600BE02 RID: 48642 RVA: 0x001106F3 File Offset: 0x0010E8F3
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008E93 RID: 36499
			// (set) Token: 0x0600BE03 RID: 48643 RVA: 0x00110706 File Offset: 0x0010E906
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008E94 RID: 36500
			// (set) Token: 0x0600BE04 RID: 48644 RVA: 0x00110719 File Offset: 0x0010E919
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008E95 RID: 36501
			// (set) Token: 0x0600BE05 RID: 48645 RVA: 0x0011072C File Offset: 0x0010E92C
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008E96 RID: 36502
			// (set) Token: 0x0600BE06 RID: 48646 RVA: 0x0011073F File Offset: 0x0010E93F
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008E97 RID: 36503
			// (set) Token: 0x0600BE07 RID: 48647 RVA: 0x00110757 File Offset: 0x0010E957
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008E98 RID: 36504
			// (set) Token: 0x0600BE08 RID: 48648 RVA: 0x0011076A File Offset: 0x0010E96A
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008E99 RID: 36505
			// (set) Token: 0x0600BE09 RID: 48649 RVA: 0x00110782 File Offset: 0x0010E982
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008E9A RID: 36506
			// (set) Token: 0x0600BE0A RID: 48650 RVA: 0x00110795 File Offset: 0x0010E995
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008E9B RID: 36507
			// (set) Token: 0x0600BE0B RID: 48651 RVA: 0x001107AD File Offset: 0x0010E9AD
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008E9C RID: 36508
			// (set) Token: 0x0600BE0C RID: 48652 RVA: 0x001107C5 File Offset: 0x0010E9C5
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008E9D RID: 36509
			// (set) Token: 0x0600BE0D RID: 48653 RVA: 0x001107DD File Offset: 0x0010E9DD
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17008E9E RID: 36510
			// (set) Token: 0x0600BE0E RID: 48654 RVA: 0x001107F5 File Offset: 0x0010E9F5
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17008E9F RID: 36511
			// (set) Token: 0x0600BE0F RID: 48655 RVA: 0x0011080D File Offset: 0x0010EA0D
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17008EA0 RID: 36512
			// (set) Token: 0x0600BE10 RID: 48656 RVA: 0x00110825 File Offset: 0x0010EA25
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008EA1 RID: 36513
			// (set) Token: 0x0600BE11 RID: 48657 RVA: 0x0011083D File Offset: 0x0010EA3D
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008EA2 RID: 36514
			// (set) Token: 0x0600BE12 RID: 48658 RVA: 0x00110855 File Offset: 0x0010EA55
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008EA3 RID: 36515
			// (set) Token: 0x0600BE13 RID: 48659 RVA: 0x0011086D File Offset: 0x0010EA6D
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008EA4 RID: 36516
			// (set) Token: 0x0600BE14 RID: 48660 RVA: 0x00110880 File Offset: 0x0010EA80
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008EA5 RID: 36517
			// (set) Token: 0x0600BE15 RID: 48661 RVA: 0x00110893 File Offset: 0x0010EA93
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008EA6 RID: 36518
			// (set) Token: 0x0600BE16 RID: 48662 RVA: 0x001108A6 File Offset: 0x0010EAA6
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008EA7 RID: 36519
			// (set) Token: 0x0600BE17 RID: 48663 RVA: 0x001108B9 File Offset: 0x0010EAB9
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008EA8 RID: 36520
			// (set) Token: 0x0600BE18 RID: 48664 RVA: 0x001108CC File Offset: 0x0010EACC
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008EA9 RID: 36521
			// (set) Token: 0x0600BE19 RID: 48665 RVA: 0x001108DF File Offset: 0x0010EADF
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008EAA RID: 36522
			// (set) Token: 0x0600BE1A RID: 48666 RVA: 0x001108F7 File Offset: 0x0010EAF7
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008EAB RID: 36523
			// (set) Token: 0x0600BE1B RID: 48667 RVA: 0x0011090A File Offset: 0x0010EB0A
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008EAC RID: 36524
			// (set) Token: 0x0600BE1C RID: 48668 RVA: 0x0011091D File Offset: 0x0010EB1D
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008EAD RID: 36525
			// (set) Token: 0x0600BE1D RID: 48669 RVA: 0x00110930 File Offset: 0x0010EB30
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008EAE RID: 36526
			// (set) Token: 0x0600BE1E RID: 48670 RVA: 0x0011094E File Offset: 0x0010EB4E
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17008EAF RID: 36527
			// (set) Token: 0x0600BE1F RID: 48671 RVA: 0x00110961 File Offset: 0x0010EB61
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17008EB0 RID: 36528
			// (set) Token: 0x0600BE20 RID: 48672 RVA: 0x00110974 File Offset: 0x0010EB74
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17008EB1 RID: 36529
			// (set) Token: 0x0600BE21 RID: 48673 RVA: 0x00110987 File Offset: 0x0010EB87
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17008EB2 RID: 36530
			// (set) Token: 0x0600BE22 RID: 48674 RVA: 0x0011099A File Offset: 0x0010EB9A
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17008EB3 RID: 36531
			// (set) Token: 0x0600BE23 RID: 48675 RVA: 0x001109AD File Offset: 0x0010EBAD
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008EB4 RID: 36532
			// (set) Token: 0x0600BE24 RID: 48676 RVA: 0x001109C0 File Offset: 0x0010EBC0
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008EB5 RID: 36533
			// (set) Token: 0x0600BE25 RID: 48677 RVA: 0x001109D3 File Offset: 0x0010EBD3
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008EB6 RID: 36534
			// (set) Token: 0x0600BE26 RID: 48678 RVA: 0x001109E6 File Offset: 0x0010EBE6
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008EB7 RID: 36535
			// (set) Token: 0x0600BE27 RID: 48679 RVA: 0x001109F9 File Offset: 0x0010EBF9
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008EB8 RID: 36536
			// (set) Token: 0x0600BE28 RID: 48680 RVA: 0x00110A0C File Offset: 0x0010EC0C
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008EB9 RID: 36537
			// (set) Token: 0x0600BE29 RID: 48681 RVA: 0x00110A1F File Offset: 0x0010EC1F
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008EBA RID: 36538
			// (set) Token: 0x0600BE2A RID: 48682 RVA: 0x00110A32 File Offset: 0x0010EC32
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008EBB RID: 36539
			// (set) Token: 0x0600BE2B RID: 48683 RVA: 0x00110A45 File Offset: 0x0010EC45
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008EBC RID: 36540
			// (set) Token: 0x0600BE2C RID: 48684 RVA: 0x00110A63 File Offset: 0x0010EC63
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008EBD RID: 36541
			// (set) Token: 0x0600BE2D RID: 48685 RVA: 0x00110A7B File Offset: 0x0010EC7B
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008EBE RID: 36542
			// (set) Token: 0x0600BE2E RID: 48686 RVA: 0x00110A93 File Offset: 0x0010EC93
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17008EBF RID: 36543
			// (set) Token: 0x0600BE2F RID: 48687 RVA: 0x00110AAB File Offset: 0x0010ECAB
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17008EC0 RID: 36544
			// (set) Token: 0x0600BE30 RID: 48688 RVA: 0x00110ABE File Offset: 0x0010ECBE
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17008EC1 RID: 36545
			// (set) Token: 0x0600BE31 RID: 48689 RVA: 0x00110AD1 File Offset: 0x0010ECD1
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17008EC2 RID: 36546
			// (set) Token: 0x0600BE32 RID: 48690 RVA: 0x00110AE9 File Offset: 0x0010ECE9
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008EC3 RID: 36547
			// (set) Token: 0x0600BE33 RID: 48691 RVA: 0x00110AFC File Offset: 0x0010ECFC
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008EC4 RID: 36548
			// (set) Token: 0x0600BE34 RID: 48692 RVA: 0x00110B14 File Offset: 0x0010ED14
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008EC5 RID: 36549
			// (set) Token: 0x0600BE35 RID: 48693 RVA: 0x00110B2C File Offset: 0x0010ED2C
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008EC6 RID: 36550
			// (set) Token: 0x0600BE36 RID: 48694 RVA: 0x00110B3F File Offset: 0x0010ED3F
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008EC7 RID: 36551
			// (set) Token: 0x0600BE37 RID: 48695 RVA: 0x00110B52 File Offset: 0x0010ED52
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008EC8 RID: 36552
			// (set) Token: 0x0600BE38 RID: 48696 RVA: 0x00110B65 File Offset: 0x0010ED65
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008EC9 RID: 36553
			// (set) Token: 0x0600BE39 RID: 48697 RVA: 0x00110B78 File Offset: 0x0010ED78
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008ECA RID: 36554
			// (set) Token: 0x0600BE3A RID: 48698 RVA: 0x00110B90 File Offset: 0x0010ED90
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008ECB RID: 36555
			// (set) Token: 0x0600BE3B RID: 48699 RVA: 0x00110BA3 File Offset: 0x0010EDA3
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008ECC RID: 36556
			// (set) Token: 0x0600BE3C RID: 48700 RVA: 0x00110BB6 File Offset: 0x0010EDB6
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008ECD RID: 36557
			// (set) Token: 0x0600BE3D RID: 48701 RVA: 0x00110BCE File Offset: 0x0010EDCE
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17008ECE RID: 36558
			// (set) Token: 0x0600BE3E RID: 48702 RVA: 0x00110BE6 File Offset: 0x0010EDE6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008ECF RID: 36559
			// (set) Token: 0x0600BE3F RID: 48703 RVA: 0x00110BF9 File Offset: 0x0010EDF9
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008ED0 RID: 36560
			// (set) Token: 0x0600BE40 RID: 48704 RVA: 0x00110C11 File Offset: 0x0010EE11
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008ED1 RID: 36561
			// (set) Token: 0x0600BE41 RID: 48705 RVA: 0x00110C24 File Offset: 0x0010EE24
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008ED2 RID: 36562
			// (set) Token: 0x0600BE42 RID: 48706 RVA: 0x00110C37 File Offset: 0x0010EE37
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008ED3 RID: 36563
			// (set) Token: 0x0600BE43 RID: 48707 RVA: 0x00110C4A File Offset: 0x0010EE4A
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008ED4 RID: 36564
			// (set) Token: 0x0600BE44 RID: 48708 RVA: 0x00110C68 File Offset: 0x0010EE68
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008ED5 RID: 36565
			// (set) Token: 0x0600BE45 RID: 48709 RVA: 0x00110C86 File Offset: 0x0010EE86
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008ED6 RID: 36566
			// (set) Token: 0x0600BE46 RID: 48710 RVA: 0x00110CA4 File Offset: 0x0010EEA4
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008ED7 RID: 36567
			// (set) Token: 0x0600BE47 RID: 48711 RVA: 0x00110CC2 File Offset: 0x0010EEC2
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008ED8 RID: 36568
			// (set) Token: 0x0600BE48 RID: 48712 RVA: 0x00110CD5 File Offset: 0x0010EED5
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008ED9 RID: 36569
			// (set) Token: 0x0600BE49 RID: 48713 RVA: 0x00110CED File Offset: 0x0010EEED
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008EDA RID: 36570
			// (set) Token: 0x0600BE4A RID: 48714 RVA: 0x00110D0B File Offset: 0x0010EF0B
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008EDB RID: 36571
			// (set) Token: 0x0600BE4B RID: 48715 RVA: 0x00110D23 File Offset: 0x0010EF23
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008EDC RID: 36572
			// (set) Token: 0x0600BE4C RID: 48716 RVA: 0x00110D3B File Offset: 0x0010EF3B
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008EDD RID: 36573
			// (set) Token: 0x0600BE4D RID: 48717 RVA: 0x00110D4E File Offset: 0x0010EF4E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008EDE RID: 36574
			// (set) Token: 0x0600BE4E RID: 48718 RVA: 0x00110D66 File Offset: 0x0010EF66
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008EDF RID: 36575
			// (set) Token: 0x0600BE4F RID: 48719 RVA: 0x00110D79 File Offset: 0x0010EF79
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008EE0 RID: 36576
			// (set) Token: 0x0600BE50 RID: 48720 RVA: 0x00110D8C File Offset: 0x0010EF8C
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008EE1 RID: 36577
			// (set) Token: 0x0600BE51 RID: 48721 RVA: 0x00110D9F File Offset: 0x0010EF9F
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008EE2 RID: 36578
			// (set) Token: 0x0600BE52 RID: 48722 RVA: 0x00110DB2 File Offset: 0x0010EFB2
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008EE3 RID: 36579
			// (set) Token: 0x0600BE53 RID: 48723 RVA: 0x00110DCA File Offset: 0x0010EFCA
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008EE4 RID: 36580
			// (set) Token: 0x0600BE54 RID: 48724 RVA: 0x00110DDD File Offset: 0x0010EFDD
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008EE5 RID: 36581
			// (set) Token: 0x0600BE55 RID: 48725 RVA: 0x00110DF0 File Offset: 0x0010EFF0
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008EE6 RID: 36582
			// (set) Token: 0x0600BE56 RID: 48726 RVA: 0x00110E08 File Offset: 0x0010F008
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008EE7 RID: 36583
			// (set) Token: 0x0600BE57 RID: 48727 RVA: 0x00110E1B File Offset: 0x0010F01B
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008EE8 RID: 36584
			// (set) Token: 0x0600BE58 RID: 48728 RVA: 0x00110E33 File Offset: 0x0010F033
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008EE9 RID: 36585
			// (set) Token: 0x0600BE59 RID: 48729 RVA: 0x00110E46 File Offset: 0x0010F046
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008EEA RID: 36586
			// (set) Token: 0x0600BE5A RID: 48730 RVA: 0x00110E64 File Offset: 0x0010F064
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008EEB RID: 36587
			// (set) Token: 0x0600BE5B RID: 48731 RVA: 0x00110E77 File Offset: 0x0010F077
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008EEC RID: 36588
			// (set) Token: 0x0600BE5C RID: 48732 RVA: 0x00110E95 File Offset: 0x0010F095
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008EED RID: 36589
			// (set) Token: 0x0600BE5D RID: 48733 RVA: 0x00110EA8 File Offset: 0x0010F0A8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008EEE RID: 36590
			// (set) Token: 0x0600BE5E RID: 48734 RVA: 0x00110EC0 File Offset: 0x0010F0C0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008EEF RID: 36591
			// (set) Token: 0x0600BE5F RID: 48735 RVA: 0x00110ED8 File Offset: 0x0010F0D8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008EF0 RID: 36592
			// (set) Token: 0x0600BE60 RID: 48736 RVA: 0x00110EF0 File Offset: 0x0010F0F0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008EF1 RID: 36593
			// (set) Token: 0x0600BE61 RID: 48737 RVA: 0x00110F08 File Offset: 0x0010F108
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D8D RID: 3469
		public class ImportLiveIdParameters : ParametersBase
		{
			// Token: 0x17008EF2 RID: 36594
			// (set) Token: 0x0600BE63 RID: 48739 RVA: 0x00110F28 File Offset: 0x0010F128
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008EF3 RID: 36595
			// (set) Token: 0x0600BE64 RID: 48740 RVA: 0x00110F3B File Offset: 0x0010F13B
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008EF4 RID: 36596
			// (set) Token: 0x0600BE65 RID: 48741 RVA: 0x00110F53 File Offset: 0x0010F153
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008EF5 RID: 36597
			// (set) Token: 0x0600BE66 RID: 48742 RVA: 0x00110F66 File Offset: 0x0010F166
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008EF6 RID: 36598
			// (set) Token: 0x0600BE67 RID: 48743 RVA: 0x00110F7E File Offset: 0x0010F17E
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008EF7 RID: 36599
			// (set) Token: 0x0600BE68 RID: 48744 RVA: 0x00110F9C File Offset: 0x0010F19C
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008EF8 RID: 36600
			// (set) Token: 0x0600BE69 RID: 48745 RVA: 0x00110FAF File Offset: 0x0010F1AF
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008EF9 RID: 36601
			// (set) Token: 0x0600BE6A RID: 48746 RVA: 0x00110FC7 File Offset: 0x0010F1C7
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008EFA RID: 36602
			// (set) Token: 0x0600BE6B RID: 48747 RVA: 0x00110FDF File Offset: 0x0010F1DF
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008EFB RID: 36603
			// (set) Token: 0x0600BE6C RID: 48748 RVA: 0x00110FFD File Offset: 0x0010F1FD
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17008EFC RID: 36604
			// (set) Token: 0x0600BE6D RID: 48749 RVA: 0x00111010 File Offset: 0x0010F210
			public virtual SwitchParameter ImportLiveId
			{
				set
				{
					base.PowerSharpParameters["ImportLiveId"] = value;
				}
			}

			// Token: 0x17008EFD RID: 36605
			// (set) Token: 0x0600BE6E RID: 48750 RVA: 0x00111028 File Offset: 0x0010F228
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008EFE RID: 36606
			// (set) Token: 0x0600BE6F RID: 48751 RVA: 0x00111040 File Offset: 0x0010F240
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008EFF RID: 36607
			// (set) Token: 0x0600BE70 RID: 48752 RVA: 0x00111053 File Offset: 0x0010F253
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008F00 RID: 36608
			// (set) Token: 0x0600BE71 RID: 48753 RVA: 0x00111066 File Offset: 0x0010F266
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008F01 RID: 36609
			// (set) Token: 0x0600BE72 RID: 48754 RVA: 0x00111079 File Offset: 0x0010F279
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008F02 RID: 36610
			// (set) Token: 0x0600BE73 RID: 48755 RVA: 0x0011108C File Offset: 0x0010F28C
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008F03 RID: 36611
			// (set) Token: 0x0600BE74 RID: 48756 RVA: 0x0011109F File Offset: 0x0010F29F
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008F04 RID: 36612
			// (set) Token: 0x0600BE75 RID: 48757 RVA: 0x001110B2 File Offset: 0x0010F2B2
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008F05 RID: 36613
			// (set) Token: 0x0600BE76 RID: 48758 RVA: 0x001110C5 File Offset: 0x0010F2C5
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008F06 RID: 36614
			// (set) Token: 0x0600BE77 RID: 48759 RVA: 0x001110DD File Offset: 0x0010F2DD
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17008F07 RID: 36615
			// (set) Token: 0x0600BE78 RID: 48760 RVA: 0x001110F0 File Offset: 0x0010F2F0
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008F08 RID: 36616
			// (set) Token: 0x0600BE79 RID: 48761 RVA: 0x00111108 File Offset: 0x0010F308
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008F09 RID: 36617
			// (set) Token: 0x0600BE7A RID: 48762 RVA: 0x0011111B File Offset: 0x0010F31B
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008F0A RID: 36618
			// (set) Token: 0x0600BE7B RID: 48763 RVA: 0x0011112E File Offset: 0x0010F32E
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008F0B RID: 36619
			// (set) Token: 0x0600BE7C RID: 48764 RVA: 0x00111141 File Offset: 0x0010F341
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008F0C RID: 36620
			// (set) Token: 0x0600BE7D RID: 48765 RVA: 0x00111154 File Offset: 0x0010F354
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008F0D RID: 36621
			// (set) Token: 0x0600BE7E RID: 48766 RVA: 0x00111167 File Offset: 0x0010F367
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008F0E RID: 36622
			// (set) Token: 0x0600BE7F RID: 48767 RVA: 0x0011117A File Offset: 0x0010F37A
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008F0F RID: 36623
			// (set) Token: 0x0600BE80 RID: 48768 RVA: 0x0011118D File Offset: 0x0010F38D
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008F10 RID: 36624
			// (set) Token: 0x0600BE81 RID: 48769 RVA: 0x001111A0 File Offset: 0x0010F3A0
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008F11 RID: 36625
			// (set) Token: 0x0600BE82 RID: 48770 RVA: 0x001111B3 File Offset: 0x0010F3B3
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008F12 RID: 36626
			// (set) Token: 0x0600BE83 RID: 48771 RVA: 0x001111C6 File Offset: 0x0010F3C6
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008F13 RID: 36627
			// (set) Token: 0x0600BE84 RID: 48772 RVA: 0x001111D9 File Offset: 0x0010F3D9
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008F14 RID: 36628
			// (set) Token: 0x0600BE85 RID: 48773 RVA: 0x001111EC File Offset: 0x0010F3EC
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008F15 RID: 36629
			// (set) Token: 0x0600BE86 RID: 48774 RVA: 0x001111FF File Offset: 0x0010F3FF
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008F16 RID: 36630
			// (set) Token: 0x0600BE87 RID: 48775 RVA: 0x00111212 File Offset: 0x0010F412
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008F17 RID: 36631
			// (set) Token: 0x0600BE88 RID: 48776 RVA: 0x00111225 File Offset: 0x0010F425
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008F18 RID: 36632
			// (set) Token: 0x0600BE89 RID: 48777 RVA: 0x00111238 File Offset: 0x0010F438
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008F19 RID: 36633
			// (set) Token: 0x0600BE8A RID: 48778 RVA: 0x0011124B File Offset: 0x0010F44B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008F1A RID: 36634
			// (set) Token: 0x0600BE8B RID: 48779 RVA: 0x0011125E File Offset: 0x0010F45E
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008F1B RID: 36635
			// (set) Token: 0x0600BE8C RID: 48780 RVA: 0x00111271 File Offset: 0x0010F471
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008F1C RID: 36636
			// (set) Token: 0x0600BE8D RID: 48781 RVA: 0x00111284 File Offset: 0x0010F484
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008F1D RID: 36637
			// (set) Token: 0x0600BE8E RID: 48782 RVA: 0x00111297 File Offset: 0x0010F497
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008F1E RID: 36638
			// (set) Token: 0x0600BE8F RID: 48783 RVA: 0x001112AA File Offset: 0x0010F4AA
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008F1F RID: 36639
			// (set) Token: 0x0600BE90 RID: 48784 RVA: 0x001112BD File Offset: 0x0010F4BD
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008F20 RID: 36640
			// (set) Token: 0x0600BE91 RID: 48785 RVA: 0x001112D5 File Offset: 0x0010F4D5
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008F21 RID: 36641
			// (set) Token: 0x0600BE92 RID: 48786 RVA: 0x001112E8 File Offset: 0x0010F4E8
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008F22 RID: 36642
			// (set) Token: 0x0600BE93 RID: 48787 RVA: 0x00111300 File Offset: 0x0010F500
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008F23 RID: 36643
			// (set) Token: 0x0600BE94 RID: 48788 RVA: 0x00111313 File Offset: 0x0010F513
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008F24 RID: 36644
			// (set) Token: 0x0600BE95 RID: 48789 RVA: 0x0011132B File Offset: 0x0010F52B
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008F25 RID: 36645
			// (set) Token: 0x0600BE96 RID: 48790 RVA: 0x00111343 File Offset: 0x0010F543
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008F26 RID: 36646
			// (set) Token: 0x0600BE97 RID: 48791 RVA: 0x0011135B File Offset: 0x0010F55B
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17008F27 RID: 36647
			// (set) Token: 0x0600BE98 RID: 48792 RVA: 0x00111373 File Offset: 0x0010F573
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17008F28 RID: 36648
			// (set) Token: 0x0600BE99 RID: 48793 RVA: 0x0011138B File Offset: 0x0010F58B
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17008F29 RID: 36649
			// (set) Token: 0x0600BE9A RID: 48794 RVA: 0x001113A3 File Offset: 0x0010F5A3
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008F2A RID: 36650
			// (set) Token: 0x0600BE9B RID: 48795 RVA: 0x001113BB File Offset: 0x0010F5BB
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008F2B RID: 36651
			// (set) Token: 0x0600BE9C RID: 48796 RVA: 0x001113D3 File Offset: 0x0010F5D3
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008F2C RID: 36652
			// (set) Token: 0x0600BE9D RID: 48797 RVA: 0x001113EB File Offset: 0x0010F5EB
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008F2D RID: 36653
			// (set) Token: 0x0600BE9E RID: 48798 RVA: 0x001113FE File Offset: 0x0010F5FE
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008F2E RID: 36654
			// (set) Token: 0x0600BE9F RID: 48799 RVA: 0x00111411 File Offset: 0x0010F611
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008F2F RID: 36655
			// (set) Token: 0x0600BEA0 RID: 48800 RVA: 0x00111424 File Offset: 0x0010F624
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008F30 RID: 36656
			// (set) Token: 0x0600BEA1 RID: 48801 RVA: 0x00111437 File Offset: 0x0010F637
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008F31 RID: 36657
			// (set) Token: 0x0600BEA2 RID: 48802 RVA: 0x0011144A File Offset: 0x0010F64A
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008F32 RID: 36658
			// (set) Token: 0x0600BEA3 RID: 48803 RVA: 0x0011145D File Offset: 0x0010F65D
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008F33 RID: 36659
			// (set) Token: 0x0600BEA4 RID: 48804 RVA: 0x00111475 File Offset: 0x0010F675
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008F34 RID: 36660
			// (set) Token: 0x0600BEA5 RID: 48805 RVA: 0x00111488 File Offset: 0x0010F688
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008F35 RID: 36661
			// (set) Token: 0x0600BEA6 RID: 48806 RVA: 0x0011149B File Offset: 0x0010F69B
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008F36 RID: 36662
			// (set) Token: 0x0600BEA7 RID: 48807 RVA: 0x001114AE File Offset: 0x0010F6AE
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008F37 RID: 36663
			// (set) Token: 0x0600BEA8 RID: 48808 RVA: 0x001114CC File Offset: 0x0010F6CC
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17008F38 RID: 36664
			// (set) Token: 0x0600BEA9 RID: 48809 RVA: 0x001114DF File Offset: 0x0010F6DF
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17008F39 RID: 36665
			// (set) Token: 0x0600BEAA RID: 48810 RVA: 0x001114F2 File Offset: 0x0010F6F2
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17008F3A RID: 36666
			// (set) Token: 0x0600BEAB RID: 48811 RVA: 0x00111505 File Offset: 0x0010F705
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17008F3B RID: 36667
			// (set) Token: 0x0600BEAC RID: 48812 RVA: 0x00111518 File Offset: 0x0010F718
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17008F3C RID: 36668
			// (set) Token: 0x0600BEAD RID: 48813 RVA: 0x0011152B File Offset: 0x0010F72B
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008F3D RID: 36669
			// (set) Token: 0x0600BEAE RID: 48814 RVA: 0x0011153E File Offset: 0x0010F73E
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008F3E RID: 36670
			// (set) Token: 0x0600BEAF RID: 48815 RVA: 0x00111551 File Offset: 0x0010F751
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008F3F RID: 36671
			// (set) Token: 0x0600BEB0 RID: 48816 RVA: 0x00111564 File Offset: 0x0010F764
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008F40 RID: 36672
			// (set) Token: 0x0600BEB1 RID: 48817 RVA: 0x00111577 File Offset: 0x0010F777
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008F41 RID: 36673
			// (set) Token: 0x0600BEB2 RID: 48818 RVA: 0x0011158A File Offset: 0x0010F78A
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008F42 RID: 36674
			// (set) Token: 0x0600BEB3 RID: 48819 RVA: 0x0011159D File Offset: 0x0010F79D
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008F43 RID: 36675
			// (set) Token: 0x0600BEB4 RID: 48820 RVA: 0x001115B0 File Offset: 0x0010F7B0
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008F44 RID: 36676
			// (set) Token: 0x0600BEB5 RID: 48821 RVA: 0x001115C3 File Offset: 0x0010F7C3
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008F45 RID: 36677
			// (set) Token: 0x0600BEB6 RID: 48822 RVA: 0x001115E1 File Offset: 0x0010F7E1
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008F46 RID: 36678
			// (set) Token: 0x0600BEB7 RID: 48823 RVA: 0x001115F9 File Offset: 0x0010F7F9
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008F47 RID: 36679
			// (set) Token: 0x0600BEB8 RID: 48824 RVA: 0x00111611 File Offset: 0x0010F811
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17008F48 RID: 36680
			// (set) Token: 0x0600BEB9 RID: 48825 RVA: 0x00111629 File Offset: 0x0010F829
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17008F49 RID: 36681
			// (set) Token: 0x0600BEBA RID: 48826 RVA: 0x0011163C File Offset: 0x0010F83C
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17008F4A RID: 36682
			// (set) Token: 0x0600BEBB RID: 48827 RVA: 0x0011164F File Offset: 0x0010F84F
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17008F4B RID: 36683
			// (set) Token: 0x0600BEBC RID: 48828 RVA: 0x00111667 File Offset: 0x0010F867
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008F4C RID: 36684
			// (set) Token: 0x0600BEBD RID: 48829 RVA: 0x0011167A File Offset: 0x0010F87A
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008F4D RID: 36685
			// (set) Token: 0x0600BEBE RID: 48830 RVA: 0x00111692 File Offset: 0x0010F892
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008F4E RID: 36686
			// (set) Token: 0x0600BEBF RID: 48831 RVA: 0x001116AA File Offset: 0x0010F8AA
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008F4F RID: 36687
			// (set) Token: 0x0600BEC0 RID: 48832 RVA: 0x001116BD File Offset: 0x0010F8BD
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008F50 RID: 36688
			// (set) Token: 0x0600BEC1 RID: 48833 RVA: 0x001116D0 File Offset: 0x0010F8D0
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008F51 RID: 36689
			// (set) Token: 0x0600BEC2 RID: 48834 RVA: 0x001116E3 File Offset: 0x0010F8E3
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008F52 RID: 36690
			// (set) Token: 0x0600BEC3 RID: 48835 RVA: 0x001116F6 File Offset: 0x0010F8F6
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008F53 RID: 36691
			// (set) Token: 0x0600BEC4 RID: 48836 RVA: 0x0011170E File Offset: 0x0010F90E
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008F54 RID: 36692
			// (set) Token: 0x0600BEC5 RID: 48837 RVA: 0x00111721 File Offset: 0x0010F921
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008F55 RID: 36693
			// (set) Token: 0x0600BEC6 RID: 48838 RVA: 0x00111734 File Offset: 0x0010F934
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008F56 RID: 36694
			// (set) Token: 0x0600BEC7 RID: 48839 RVA: 0x0011174C File Offset: 0x0010F94C
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17008F57 RID: 36695
			// (set) Token: 0x0600BEC8 RID: 48840 RVA: 0x00111764 File Offset: 0x0010F964
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008F58 RID: 36696
			// (set) Token: 0x0600BEC9 RID: 48841 RVA: 0x00111777 File Offset: 0x0010F977
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008F59 RID: 36697
			// (set) Token: 0x0600BECA RID: 48842 RVA: 0x0011178F File Offset: 0x0010F98F
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008F5A RID: 36698
			// (set) Token: 0x0600BECB RID: 48843 RVA: 0x001117A2 File Offset: 0x0010F9A2
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008F5B RID: 36699
			// (set) Token: 0x0600BECC RID: 48844 RVA: 0x001117B5 File Offset: 0x0010F9B5
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008F5C RID: 36700
			// (set) Token: 0x0600BECD RID: 48845 RVA: 0x001117C8 File Offset: 0x0010F9C8
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008F5D RID: 36701
			// (set) Token: 0x0600BECE RID: 48846 RVA: 0x001117E6 File Offset: 0x0010F9E6
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008F5E RID: 36702
			// (set) Token: 0x0600BECF RID: 48847 RVA: 0x00111804 File Offset: 0x0010FA04
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008F5F RID: 36703
			// (set) Token: 0x0600BED0 RID: 48848 RVA: 0x00111822 File Offset: 0x0010FA22
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008F60 RID: 36704
			// (set) Token: 0x0600BED1 RID: 48849 RVA: 0x00111840 File Offset: 0x0010FA40
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008F61 RID: 36705
			// (set) Token: 0x0600BED2 RID: 48850 RVA: 0x00111853 File Offset: 0x0010FA53
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008F62 RID: 36706
			// (set) Token: 0x0600BED3 RID: 48851 RVA: 0x0011186B File Offset: 0x0010FA6B
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008F63 RID: 36707
			// (set) Token: 0x0600BED4 RID: 48852 RVA: 0x00111889 File Offset: 0x0010FA89
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008F64 RID: 36708
			// (set) Token: 0x0600BED5 RID: 48853 RVA: 0x001118A1 File Offset: 0x0010FAA1
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008F65 RID: 36709
			// (set) Token: 0x0600BED6 RID: 48854 RVA: 0x001118B9 File Offset: 0x0010FAB9
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008F66 RID: 36710
			// (set) Token: 0x0600BED7 RID: 48855 RVA: 0x001118CC File Offset: 0x0010FACC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008F67 RID: 36711
			// (set) Token: 0x0600BED8 RID: 48856 RVA: 0x001118E4 File Offset: 0x0010FAE4
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008F68 RID: 36712
			// (set) Token: 0x0600BED9 RID: 48857 RVA: 0x001118F7 File Offset: 0x0010FAF7
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008F69 RID: 36713
			// (set) Token: 0x0600BEDA RID: 48858 RVA: 0x0011190A File Offset: 0x0010FB0A
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008F6A RID: 36714
			// (set) Token: 0x0600BEDB RID: 48859 RVA: 0x0011191D File Offset: 0x0010FB1D
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008F6B RID: 36715
			// (set) Token: 0x0600BEDC RID: 48860 RVA: 0x00111930 File Offset: 0x0010FB30
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008F6C RID: 36716
			// (set) Token: 0x0600BEDD RID: 48861 RVA: 0x00111948 File Offset: 0x0010FB48
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008F6D RID: 36717
			// (set) Token: 0x0600BEDE RID: 48862 RVA: 0x0011195B File Offset: 0x0010FB5B
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008F6E RID: 36718
			// (set) Token: 0x0600BEDF RID: 48863 RVA: 0x0011196E File Offset: 0x0010FB6E
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008F6F RID: 36719
			// (set) Token: 0x0600BEE0 RID: 48864 RVA: 0x00111986 File Offset: 0x0010FB86
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008F70 RID: 36720
			// (set) Token: 0x0600BEE1 RID: 48865 RVA: 0x00111999 File Offset: 0x0010FB99
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008F71 RID: 36721
			// (set) Token: 0x0600BEE2 RID: 48866 RVA: 0x001119B1 File Offset: 0x0010FBB1
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008F72 RID: 36722
			// (set) Token: 0x0600BEE3 RID: 48867 RVA: 0x001119C4 File Offset: 0x0010FBC4
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008F73 RID: 36723
			// (set) Token: 0x0600BEE4 RID: 48868 RVA: 0x001119E2 File Offset: 0x0010FBE2
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008F74 RID: 36724
			// (set) Token: 0x0600BEE5 RID: 48869 RVA: 0x001119F5 File Offset: 0x0010FBF5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008F75 RID: 36725
			// (set) Token: 0x0600BEE6 RID: 48870 RVA: 0x00111A13 File Offset: 0x0010FC13
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008F76 RID: 36726
			// (set) Token: 0x0600BEE7 RID: 48871 RVA: 0x00111A26 File Offset: 0x0010FC26
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008F77 RID: 36727
			// (set) Token: 0x0600BEE8 RID: 48872 RVA: 0x00111A3E File Offset: 0x0010FC3E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008F78 RID: 36728
			// (set) Token: 0x0600BEE9 RID: 48873 RVA: 0x00111A56 File Offset: 0x0010FC56
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008F79 RID: 36729
			// (set) Token: 0x0600BEEA RID: 48874 RVA: 0x00111A6E File Offset: 0x0010FC6E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008F7A RID: 36730
			// (set) Token: 0x0600BEEB RID: 48875 RVA: 0x00111A86 File Offset: 0x0010FC86
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D8E RID: 3470
		public class RemoteArchiveParameters : ParametersBase
		{
			// Token: 0x17008F7B RID: 36731
			// (set) Token: 0x0600BEED RID: 48877 RVA: 0x00111AA6 File Offset: 0x0010FCA6
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008F7C RID: 36732
			// (set) Token: 0x0600BEEE RID: 48878 RVA: 0x00111ABE File Offset: 0x0010FCBE
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008F7D RID: 36733
			// (set) Token: 0x0600BEEF RID: 48879 RVA: 0x00111AD1 File Offset: 0x0010FCD1
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008F7E RID: 36734
			// (set) Token: 0x0600BEF0 RID: 48880 RVA: 0x00111AE9 File Offset: 0x0010FCE9
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17008F7F RID: 36735
			// (set) Token: 0x0600BEF1 RID: 48881 RVA: 0x00111AFC File Offset: 0x0010FCFC
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17008F80 RID: 36736
			// (set) Token: 0x0600BEF2 RID: 48882 RVA: 0x00111B0F File Offset: 0x0010FD0F
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008F81 RID: 36737
			// (set) Token: 0x0600BEF3 RID: 48883 RVA: 0x00111B2D File Offset: 0x0010FD2D
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008F82 RID: 36738
			// (set) Token: 0x0600BEF4 RID: 48884 RVA: 0x00111B40 File Offset: 0x0010FD40
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008F83 RID: 36739
			// (set) Token: 0x0600BEF5 RID: 48885 RVA: 0x00111B58 File Offset: 0x0010FD58
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008F84 RID: 36740
			// (set) Token: 0x0600BEF6 RID: 48886 RVA: 0x00111B70 File Offset: 0x0010FD70
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008F85 RID: 36741
			// (set) Token: 0x0600BEF7 RID: 48887 RVA: 0x00111B8E File Offset: 0x0010FD8E
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x17008F86 RID: 36742
			// (set) Token: 0x0600BEF8 RID: 48888 RVA: 0x00111BA6 File Offset: 0x0010FDA6
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x17008F87 RID: 36743
			// (set) Token: 0x0600BEF9 RID: 48889 RVA: 0x00111BB9 File Offset: 0x0010FDB9
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008F88 RID: 36744
			// (set) Token: 0x0600BEFA RID: 48890 RVA: 0x00111BD1 File Offset: 0x0010FDD1
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008F89 RID: 36745
			// (set) Token: 0x0600BEFB RID: 48891 RVA: 0x00111BE4 File Offset: 0x0010FDE4
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008F8A RID: 36746
			// (set) Token: 0x0600BEFC RID: 48892 RVA: 0x00111BF7 File Offset: 0x0010FDF7
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008F8B RID: 36747
			// (set) Token: 0x0600BEFD RID: 48893 RVA: 0x00111C0A File Offset: 0x0010FE0A
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008F8C RID: 36748
			// (set) Token: 0x0600BEFE RID: 48894 RVA: 0x00111C1D File Offset: 0x0010FE1D
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008F8D RID: 36749
			// (set) Token: 0x0600BEFF RID: 48895 RVA: 0x00111C30 File Offset: 0x0010FE30
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008F8E RID: 36750
			// (set) Token: 0x0600BF00 RID: 48896 RVA: 0x00111C43 File Offset: 0x0010FE43
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008F8F RID: 36751
			// (set) Token: 0x0600BF01 RID: 48897 RVA: 0x00111C56 File Offset: 0x0010FE56
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17008F90 RID: 36752
			// (set) Token: 0x0600BF02 RID: 48898 RVA: 0x00111C6E File Offset: 0x0010FE6E
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17008F91 RID: 36753
			// (set) Token: 0x0600BF03 RID: 48899 RVA: 0x00111C81 File Offset: 0x0010FE81
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008F92 RID: 36754
			// (set) Token: 0x0600BF04 RID: 48900 RVA: 0x00111C99 File Offset: 0x0010FE99
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008F93 RID: 36755
			// (set) Token: 0x0600BF05 RID: 48901 RVA: 0x00111CAC File Offset: 0x0010FEAC
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008F94 RID: 36756
			// (set) Token: 0x0600BF06 RID: 48902 RVA: 0x00111CBF File Offset: 0x0010FEBF
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008F95 RID: 36757
			// (set) Token: 0x0600BF07 RID: 48903 RVA: 0x00111CD2 File Offset: 0x0010FED2
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008F96 RID: 36758
			// (set) Token: 0x0600BF08 RID: 48904 RVA: 0x00111CE5 File Offset: 0x0010FEE5
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008F97 RID: 36759
			// (set) Token: 0x0600BF09 RID: 48905 RVA: 0x00111CF8 File Offset: 0x0010FEF8
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008F98 RID: 36760
			// (set) Token: 0x0600BF0A RID: 48906 RVA: 0x00111D0B File Offset: 0x0010FF0B
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008F99 RID: 36761
			// (set) Token: 0x0600BF0B RID: 48907 RVA: 0x00111D1E File Offset: 0x0010FF1E
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008F9A RID: 36762
			// (set) Token: 0x0600BF0C RID: 48908 RVA: 0x00111D31 File Offset: 0x0010FF31
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008F9B RID: 36763
			// (set) Token: 0x0600BF0D RID: 48909 RVA: 0x00111D44 File Offset: 0x0010FF44
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008F9C RID: 36764
			// (set) Token: 0x0600BF0E RID: 48910 RVA: 0x00111D57 File Offset: 0x0010FF57
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008F9D RID: 36765
			// (set) Token: 0x0600BF0F RID: 48911 RVA: 0x00111D6A File Offset: 0x0010FF6A
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008F9E RID: 36766
			// (set) Token: 0x0600BF10 RID: 48912 RVA: 0x00111D7D File Offset: 0x0010FF7D
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008F9F RID: 36767
			// (set) Token: 0x0600BF11 RID: 48913 RVA: 0x00111D90 File Offset: 0x0010FF90
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008FA0 RID: 36768
			// (set) Token: 0x0600BF12 RID: 48914 RVA: 0x00111DA3 File Offset: 0x0010FFA3
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008FA1 RID: 36769
			// (set) Token: 0x0600BF13 RID: 48915 RVA: 0x00111DB6 File Offset: 0x0010FFB6
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008FA2 RID: 36770
			// (set) Token: 0x0600BF14 RID: 48916 RVA: 0x00111DC9 File Offset: 0x0010FFC9
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008FA3 RID: 36771
			// (set) Token: 0x0600BF15 RID: 48917 RVA: 0x00111DDC File Offset: 0x0010FFDC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008FA4 RID: 36772
			// (set) Token: 0x0600BF16 RID: 48918 RVA: 0x00111DEF File Offset: 0x0010FFEF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008FA5 RID: 36773
			// (set) Token: 0x0600BF17 RID: 48919 RVA: 0x00111E02 File Offset: 0x00110002
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008FA6 RID: 36774
			// (set) Token: 0x0600BF18 RID: 48920 RVA: 0x00111E15 File Offset: 0x00110015
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008FA7 RID: 36775
			// (set) Token: 0x0600BF19 RID: 48921 RVA: 0x00111E28 File Offset: 0x00110028
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008FA8 RID: 36776
			// (set) Token: 0x0600BF1A RID: 48922 RVA: 0x00111E3B File Offset: 0x0011003B
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008FA9 RID: 36777
			// (set) Token: 0x0600BF1B RID: 48923 RVA: 0x00111E4E File Offset: 0x0011004E
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008FAA RID: 36778
			// (set) Token: 0x0600BF1C RID: 48924 RVA: 0x00111E66 File Offset: 0x00110066
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008FAB RID: 36779
			// (set) Token: 0x0600BF1D RID: 48925 RVA: 0x00111E79 File Offset: 0x00110079
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17008FAC RID: 36780
			// (set) Token: 0x0600BF1E RID: 48926 RVA: 0x00111E91 File Offset: 0x00110091
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17008FAD RID: 36781
			// (set) Token: 0x0600BF1F RID: 48927 RVA: 0x00111EA4 File Offset: 0x001100A4
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008FAE RID: 36782
			// (set) Token: 0x0600BF20 RID: 48928 RVA: 0x00111EBC File Offset: 0x001100BC
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008FAF RID: 36783
			// (set) Token: 0x0600BF21 RID: 48929 RVA: 0x00111ED4 File Offset: 0x001100D4
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17008FB0 RID: 36784
			// (set) Token: 0x0600BF22 RID: 48930 RVA: 0x00111EEC File Offset: 0x001100EC
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17008FB1 RID: 36785
			// (set) Token: 0x0600BF23 RID: 48931 RVA: 0x00111F04 File Offset: 0x00110104
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17008FB2 RID: 36786
			// (set) Token: 0x0600BF24 RID: 48932 RVA: 0x00111F1C File Offset: 0x0011011C
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17008FB3 RID: 36787
			// (set) Token: 0x0600BF25 RID: 48933 RVA: 0x00111F34 File Offset: 0x00110134
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17008FB4 RID: 36788
			// (set) Token: 0x0600BF26 RID: 48934 RVA: 0x00111F4C File Offset: 0x0011014C
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008FB5 RID: 36789
			// (set) Token: 0x0600BF27 RID: 48935 RVA: 0x00111F64 File Offset: 0x00110164
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008FB6 RID: 36790
			// (set) Token: 0x0600BF28 RID: 48936 RVA: 0x00111F7C File Offset: 0x0011017C
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008FB7 RID: 36791
			// (set) Token: 0x0600BF29 RID: 48937 RVA: 0x00111F8F File Offset: 0x0011018F
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17008FB8 RID: 36792
			// (set) Token: 0x0600BF2A RID: 48938 RVA: 0x00111FA2 File Offset: 0x001101A2
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17008FB9 RID: 36793
			// (set) Token: 0x0600BF2B RID: 48939 RVA: 0x00111FB5 File Offset: 0x001101B5
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17008FBA RID: 36794
			// (set) Token: 0x0600BF2C RID: 48940 RVA: 0x00111FC8 File Offset: 0x001101C8
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17008FBB RID: 36795
			// (set) Token: 0x0600BF2D RID: 48941 RVA: 0x00111FDB File Offset: 0x001101DB
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17008FBC RID: 36796
			// (set) Token: 0x0600BF2E RID: 48942 RVA: 0x00111FEE File Offset: 0x001101EE
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17008FBD RID: 36797
			// (set) Token: 0x0600BF2F RID: 48943 RVA: 0x00112006 File Offset: 0x00110206
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17008FBE RID: 36798
			// (set) Token: 0x0600BF30 RID: 48944 RVA: 0x00112019 File Offset: 0x00110219
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17008FBF RID: 36799
			// (set) Token: 0x0600BF31 RID: 48945 RVA: 0x0011202C File Offset: 0x0011022C
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17008FC0 RID: 36800
			// (set) Token: 0x0600BF32 RID: 48946 RVA: 0x0011203F File Offset: 0x0011023F
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008FC1 RID: 36801
			// (set) Token: 0x0600BF33 RID: 48947 RVA: 0x0011205D File Offset: 0x0011025D
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17008FC2 RID: 36802
			// (set) Token: 0x0600BF34 RID: 48948 RVA: 0x00112070 File Offset: 0x00110270
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17008FC3 RID: 36803
			// (set) Token: 0x0600BF35 RID: 48949 RVA: 0x00112083 File Offset: 0x00110283
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17008FC4 RID: 36804
			// (set) Token: 0x0600BF36 RID: 48950 RVA: 0x00112096 File Offset: 0x00110296
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17008FC5 RID: 36805
			// (set) Token: 0x0600BF37 RID: 48951 RVA: 0x001120A9 File Offset: 0x001102A9
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17008FC6 RID: 36806
			// (set) Token: 0x0600BF38 RID: 48952 RVA: 0x001120BC File Offset: 0x001102BC
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17008FC7 RID: 36807
			// (set) Token: 0x0600BF39 RID: 48953 RVA: 0x001120CF File Offset: 0x001102CF
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17008FC8 RID: 36808
			// (set) Token: 0x0600BF3A RID: 48954 RVA: 0x001120E2 File Offset: 0x001102E2
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17008FC9 RID: 36809
			// (set) Token: 0x0600BF3B RID: 48955 RVA: 0x001120F5 File Offset: 0x001102F5
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17008FCA RID: 36810
			// (set) Token: 0x0600BF3C RID: 48956 RVA: 0x00112108 File Offset: 0x00110308
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17008FCB RID: 36811
			// (set) Token: 0x0600BF3D RID: 48957 RVA: 0x0011211B File Offset: 0x0011031B
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17008FCC RID: 36812
			// (set) Token: 0x0600BF3E RID: 48958 RVA: 0x0011212E File Offset: 0x0011032E
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17008FCD RID: 36813
			// (set) Token: 0x0600BF3F RID: 48959 RVA: 0x00112141 File Offset: 0x00110341
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17008FCE RID: 36814
			// (set) Token: 0x0600BF40 RID: 48960 RVA: 0x00112154 File Offset: 0x00110354
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008FCF RID: 36815
			// (set) Token: 0x0600BF41 RID: 48961 RVA: 0x00112172 File Offset: 0x00110372
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17008FD0 RID: 36816
			// (set) Token: 0x0600BF42 RID: 48962 RVA: 0x0011218A File Offset: 0x0011038A
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008FD1 RID: 36817
			// (set) Token: 0x0600BF43 RID: 48963 RVA: 0x001121A2 File Offset: 0x001103A2
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17008FD2 RID: 36818
			// (set) Token: 0x0600BF44 RID: 48964 RVA: 0x001121BA File Offset: 0x001103BA
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17008FD3 RID: 36819
			// (set) Token: 0x0600BF45 RID: 48965 RVA: 0x001121CD File Offset: 0x001103CD
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17008FD4 RID: 36820
			// (set) Token: 0x0600BF46 RID: 48966 RVA: 0x001121E0 File Offset: 0x001103E0
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17008FD5 RID: 36821
			// (set) Token: 0x0600BF47 RID: 48967 RVA: 0x001121F8 File Offset: 0x001103F8
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008FD6 RID: 36822
			// (set) Token: 0x0600BF48 RID: 48968 RVA: 0x0011220B File Offset: 0x0011040B
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008FD7 RID: 36823
			// (set) Token: 0x0600BF49 RID: 48969 RVA: 0x00112223 File Offset: 0x00110423
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17008FD8 RID: 36824
			// (set) Token: 0x0600BF4A RID: 48970 RVA: 0x0011223B File Offset: 0x0011043B
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008FD9 RID: 36825
			// (set) Token: 0x0600BF4B RID: 48971 RVA: 0x0011224E File Offset: 0x0011044E
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008FDA RID: 36826
			// (set) Token: 0x0600BF4C RID: 48972 RVA: 0x00112261 File Offset: 0x00110461
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008FDB RID: 36827
			// (set) Token: 0x0600BF4D RID: 48973 RVA: 0x00112274 File Offset: 0x00110474
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008FDC RID: 36828
			// (set) Token: 0x0600BF4E RID: 48974 RVA: 0x00112287 File Offset: 0x00110487
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17008FDD RID: 36829
			// (set) Token: 0x0600BF4F RID: 48975 RVA: 0x0011229F File Offset: 0x0011049F
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008FDE RID: 36830
			// (set) Token: 0x0600BF50 RID: 48976 RVA: 0x001122B2 File Offset: 0x001104B2
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17008FDF RID: 36831
			// (set) Token: 0x0600BF51 RID: 48977 RVA: 0x001122C5 File Offset: 0x001104C5
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17008FE0 RID: 36832
			// (set) Token: 0x0600BF52 RID: 48978 RVA: 0x001122DD File Offset: 0x001104DD
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17008FE1 RID: 36833
			// (set) Token: 0x0600BF53 RID: 48979 RVA: 0x001122F5 File Offset: 0x001104F5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008FE2 RID: 36834
			// (set) Token: 0x0600BF54 RID: 48980 RVA: 0x00112308 File Offset: 0x00110508
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17008FE3 RID: 36835
			// (set) Token: 0x0600BF55 RID: 48981 RVA: 0x00112320 File Offset: 0x00110520
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008FE4 RID: 36836
			// (set) Token: 0x0600BF56 RID: 48982 RVA: 0x00112333 File Offset: 0x00110533
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008FE5 RID: 36837
			// (set) Token: 0x0600BF57 RID: 48983 RVA: 0x00112346 File Offset: 0x00110546
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17008FE6 RID: 36838
			// (set) Token: 0x0600BF58 RID: 48984 RVA: 0x00112359 File Offset: 0x00110559
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008FE7 RID: 36839
			// (set) Token: 0x0600BF59 RID: 48985 RVA: 0x00112377 File Offset: 0x00110577
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008FE8 RID: 36840
			// (set) Token: 0x0600BF5A RID: 48986 RVA: 0x00112395 File Offset: 0x00110595
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008FE9 RID: 36841
			// (set) Token: 0x0600BF5B RID: 48987 RVA: 0x001123B3 File Offset: 0x001105B3
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008FEA RID: 36842
			// (set) Token: 0x0600BF5C RID: 48988 RVA: 0x001123D1 File Offset: 0x001105D1
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17008FEB RID: 36843
			// (set) Token: 0x0600BF5D RID: 48989 RVA: 0x001123E4 File Offset: 0x001105E4
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008FEC RID: 36844
			// (set) Token: 0x0600BF5E RID: 48990 RVA: 0x001123FC File Offset: 0x001105FC
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17008FED RID: 36845
			// (set) Token: 0x0600BF5F RID: 48991 RVA: 0x0011241A File Offset: 0x0011061A
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17008FEE RID: 36846
			// (set) Token: 0x0600BF60 RID: 48992 RVA: 0x00112432 File Offset: 0x00110632
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008FEF RID: 36847
			// (set) Token: 0x0600BF61 RID: 48993 RVA: 0x0011244A File Offset: 0x0011064A
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17008FF0 RID: 36848
			// (set) Token: 0x0600BF62 RID: 48994 RVA: 0x0011245D File Offset: 0x0011065D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17008FF1 RID: 36849
			// (set) Token: 0x0600BF63 RID: 48995 RVA: 0x00112475 File Offset: 0x00110675
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008FF2 RID: 36850
			// (set) Token: 0x0600BF64 RID: 48996 RVA: 0x00112488 File Offset: 0x00110688
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008FF3 RID: 36851
			// (set) Token: 0x0600BF65 RID: 48997 RVA: 0x0011249B File Offset: 0x0011069B
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008FF4 RID: 36852
			// (set) Token: 0x0600BF66 RID: 48998 RVA: 0x001124AE File Offset: 0x001106AE
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008FF5 RID: 36853
			// (set) Token: 0x0600BF67 RID: 48999 RVA: 0x001124C1 File Offset: 0x001106C1
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008FF6 RID: 36854
			// (set) Token: 0x0600BF68 RID: 49000 RVA: 0x001124D9 File Offset: 0x001106D9
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008FF7 RID: 36855
			// (set) Token: 0x0600BF69 RID: 49001 RVA: 0x001124EC File Offset: 0x001106EC
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008FF8 RID: 36856
			// (set) Token: 0x0600BF6A RID: 49002 RVA: 0x001124FF File Offset: 0x001106FF
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008FF9 RID: 36857
			// (set) Token: 0x0600BF6B RID: 49003 RVA: 0x00112517 File Offset: 0x00110717
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008FFA RID: 36858
			// (set) Token: 0x0600BF6C RID: 49004 RVA: 0x0011252A File Offset: 0x0011072A
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008FFB RID: 36859
			// (set) Token: 0x0600BF6D RID: 49005 RVA: 0x00112542 File Offset: 0x00110742
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008FFC RID: 36860
			// (set) Token: 0x0600BF6E RID: 49006 RVA: 0x00112555 File Offset: 0x00110755
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008FFD RID: 36861
			// (set) Token: 0x0600BF6F RID: 49007 RVA: 0x00112573 File Offset: 0x00110773
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008FFE RID: 36862
			// (set) Token: 0x0600BF70 RID: 49008 RVA: 0x00112586 File Offset: 0x00110786
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008FFF RID: 36863
			// (set) Token: 0x0600BF71 RID: 49009 RVA: 0x001125A4 File Offset: 0x001107A4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009000 RID: 36864
			// (set) Token: 0x0600BF72 RID: 49010 RVA: 0x001125B7 File Offset: 0x001107B7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009001 RID: 36865
			// (set) Token: 0x0600BF73 RID: 49011 RVA: 0x001125CF File Offset: 0x001107CF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009002 RID: 36866
			// (set) Token: 0x0600BF74 RID: 49012 RVA: 0x001125E7 File Offset: 0x001107E7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009003 RID: 36867
			// (set) Token: 0x0600BF75 RID: 49013 RVA: 0x001125FF File Offset: 0x001107FF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009004 RID: 36868
			// (set) Token: 0x0600BF76 RID: 49014 RVA: 0x00112617 File Offset: 0x00110817
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D8F RID: 3471
		public class MailboxPlanParameters : ParametersBase
		{
			// Token: 0x17009005 RID: 36869
			// (set) Token: 0x0600BF78 RID: 49016 RVA: 0x00112637 File Offset: 0x00110837
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17009006 RID: 36870
			// (set) Token: 0x0600BF79 RID: 49017 RVA: 0x0011264F File Offset: 0x0011084F
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17009007 RID: 36871
			// (set) Token: 0x0600BF7A RID: 49018 RVA: 0x00112662 File Offset: 0x00110862
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17009008 RID: 36872
			// (set) Token: 0x0600BF7B RID: 49019 RVA: 0x0011267A File Offset: 0x0011087A
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17009009 RID: 36873
			// (set) Token: 0x0600BF7C RID: 49020 RVA: 0x0011268D File Offset: 0x0011088D
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700900A RID: 36874
			// (set) Token: 0x0600BF7D RID: 49021 RVA: 0x001126AB File Offset: 0x001108AB
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700900B RID: 36875
			// (set) Token: 0x0600BF7E RID: 49022 RVA: 0x001126BE File Offset: 0x001108BE
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700900C RID: 36876
			// (set) Token: 0x0600BF7F RID: 49023 RVA: 0x001126D6 File Offset: 0x001108D6
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700900D RID: 36877
			// (set) Token: 0x0600BF80 RID: 49024 RVA: 0x001126EE File Offset: 0x001108EE
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x1700900E RID: 36878
			// (set) Token: 0x0600BF81 RID: 49025 RVA: 0x00112706 File Offset: 0x00110906
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700900F RID: 36879
			// (set) Token: 0x0600BF82 RID: 49026 RVA: 0x00112719 File Offset: 0x00110919
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009010 RID: 36880
			// (set) Token: 0x0600BF83 RID: 49027 RVA: 0x0011272C File Offset: 0x0011092C
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009011 RID: 36881
			// (set) Token: 0x0600BF84 RID: 49028 RVA: 0x0011273F File Offset: 0x0011093F
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009012 RID: 36882
			// (set) Token: 0x0600BF85 RID: 49029 RVA: 0x00112752 File Offset: 0x00110952
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009013 RID: 36883
			// (set) Token: 0x0600BF86 RID: 49030 RVA: 0x00112765 File Offset: 0x00110965
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009014 RID: 36884
			// (set) Token: 0x0600BF87 RID: 49031 RVA: 0x00112778 File Offset: 0x00110978
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009015 RID: 36885
			// (set) Token: 0x0600BF88 RID: 49032 RVA: 0x0011278B File Offset: 0x0011098B
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17009016 RID: 36886
			// (set) Token: 0x0600BF89 RID: 49033 RVA: 0x001127A3 File Offset: 0x001109A3
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009017 RID: 36887
			// (set) Token: 0x0600BF8A RID: 49034 RVA: 0x001127B6 File Offset: 0x001109B6
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009018 RID: 36888
			// (set) Token: 0x0600BF8B RID: 49035 RVA: 0x001127CE File Offset: 0x001109CE
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009019 RID: 36889
			// (set) Token: 0x0600BF8C RID: 49036 RVA: 0x001127E1 File Offset: 0x001109E1
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700901A RID: 36890
			// (set) Token: 0x0600BF8D RID: 49037 RVA: 0x001127F4 File Offset: 0x001109F4
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700901B RID: 36891
			// (set) Token: 0x0600BF8E RID: 49038 RVA: 0x00112807 File Offset: 0x00110A07
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700901C RID: 36892
			// (set) Token: 0x0600BF8F RID: 49039 RVA: 0x0011281A File Offset: 0x00110A1A
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x1700901D RID: 36893
			// (set) Token: 0x0600BF90 RID: 49040 RVA: 0x0011282D File Offset: 0x00110A2D
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x1700901E RID: 36894
			// (set) Token: 0x0600BF91 RID: 49041 RVA: 0x00112840 File Offset: 0x00110A40
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x1700901F RID: 36895
			// (set) Token: 0x0600BF92 RID: 49042 RVA: 0x00112853 File Offset: 0x00110A53
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009020 RID: 36896
			// (set) Token: 0x0600BF93 RID: 49043 RVA: 0x00112866 File Offset: 0x00110A66
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009021 RID: 36897
			// (set) Token: 0x0600BF94 RID: 49044 RVA: 0x00112879 File Offset: 0x00110A79
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009022 RID: 36898
			// (set) Token: 0x0600BF95 RID: 49045 RVA: 0x0011288C File Offset: 0x00110A8C
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009023 RID: 36899
			// (set) Token: 0x0600BF96 RID: 49046 RVA: 0x0011289F File Offset: 0x00110A9F
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009024 RID: 36900
			// (set) Token: 0x0600BF97 RID: 49047 RVA: 0x001128B2 File Offset: 0x00110AB2
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009025 RID: 36901
			// (set) Token: 0x0600BF98 RID: 49048 RVA: 0x001128C5 File Offset: 0x00110AC5
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009026 RID: 36902
			// (set) Token: 0x0600BF99 RID: 49049 RVA: 0x001128D8 File Offset: 0x00110AD8
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009027 RID: 36903
			// (set) Token: 0x0600BF9A RID: 49050 RVA: 0x001128EB File Offset: 0x00110AEB
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009028 RID: 36904
			// (set) Token: 0x0600BF9B RID: 49051 RVA: 0x001128FE File Offset: 0x00110AFE
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009029 RID: 36905
			// (set) Token: 0x0600BF9C RID: 49052 RVA: 0x00112911 File Offset: 0x00110B11
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700902A RID: 36906
			// (set) Token: 0x0600BF9D RID: 49053 RVA: 0x00112924 File Offset: 0x00110B24
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700902B RID: 36907
			// (set) Token: 0x0600BF9E RID: 49054 RVA: 0x00112937 File Offset: 0x00110B37
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700902C RID: 36908
			// (set) Token: 0x0600BF9F RID: 49055 RVA: 0x0011294A File Offset: 0x00110B4A
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700902D RID: 36909
			// (set) Token: 0x0600BFA0 RID: 49056 RVA: 0x0011295D File Offset: 0x00110B5D
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700902E RID: 36910
			// (set) Token: 0x0600BFA1 RID: 49057 RVA: 0x00112970 File Offset: 0x00110B70
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700902F RID: 36911
			// (set) Token: 0x0600BFA2 RID: 49058 RVA: 0x00112983 File Offset: 0x00110B83
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009030 RID: 36912
			// (set) Token: 0x0600BFA3 RID: 49059 RVA: 0x0011299B File Offset: 0x00110B9B
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009031 RID: 36913
			// (set) Token: 0x0600BFA4 RID: 49060 RVA: 0x001129AE File Offset: 0x00110BAE
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009032 RID: 36914
			// (set) Token: 0x0600BFA5 RID: 49061 RVA: 0x001129C6 File Offset: 0x00110BC6
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17009033 RID: 36915
			// (set) Token: 0x0600BFA6 RID: 49062 RVA: 0x001129D9 File Offset: 0x00110BD9
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009034 RID: 36916
			// (set) Token: 0x0600BFA7 RID: 49063 RVA: 0x001129F1 File Offset: 0x00110BF1
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009035 RID: 36917
			// (set) Token: 0x0600BFA8 RID: 49064 RVA: 0x00112A09 File Offset: 0x00110C09
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17009036 RID: 36918
			// (set) Token: 0x0600BFA9 RID: 49065 RVA: 0x00112A21 File Offset: 0x00110C21
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17009037 RID: 36919
			// (set) Token: 0x0600BFAA RID: 49066 RVA: 0x00112A39 File Offset: 0x00110C39
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17009038 RID: 36920
			// (set) Token: 0x0600BFAB RID: 49067 RVA: 0x00112A51 File Offset: 0x00110C51
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17009039 RID: 36921
			// (set) Token: 0x0600BFAC RID: 49068 RVA: 0x00112A69 File Offset: 0x00110C69
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x1700903A RID: 36922
			// (set) Token: 0x0600BFAD RID: 49069 RVA: 0x00112A81 File Offset: 0x00110C81
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x1700903B RID: 36923
			// (set) Token: 0x0600BFAE RID: 49070 RVA: 0x00112A99 File Offset: 0x00110C99
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x1700903C RID: 36924
			// (set) Token: 0x0600BFAF RID: 49071 RVA: 0x00112AB1 File Offset: 0x00110CB1
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x1700903D RID: 36925
			// (set) Token: 0x0600BFB0 RID: 49072 RVA: 0x00112AC4 File Offset: 0x00110CC4
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700903E RID: 36926
			// (set) Token: 0x0600BFB1 RID: 49073 RVA: 0x00112AD7 File Offset: 0x00110CD7
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700903F RID: 36927
			// (set) Token: 0x0600BFB2 RID: 49074 RVA: 0x00112AEA File Offset: 0x00110CEA
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009040 RID: 36928
			// (set) Token: 0x0600BFB3 RID: 49075 RVA: 0x00112AFD File Offset: 0x00110CFD
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009041 RID: 36929
			// (set) Token: 0x0600BFB4 RID: 49076 RVA: 0x00112B10 File Offset: 0x00110D10
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009042 RID: 36930
			// (set) Token: 0x0600BFB5 RID: 49077 RVA: 0x00112B23 File Offset: 0x00110D23
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009043 RID: 36931
			// (set) Token: 0x0600BFB6 RID: 49078 RVA: 0x00112B3B File Offset: 0x00110D3B
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009044 RID: 36932
			// (set) Token: 0x0600BFB7 RID: 49079 RVA: 0x00112B4E File Offset: 0x00110D4E
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009045 RID: 36933
			// (set) Token: 0x0600BFB8 RID: 49080 RVA: 0x00112B61 File Offset: 0x00110D61
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009046 RID: 36934
			// (set) Token: 0x0600BFB9 RID: 49081 RVA: 0x00112B74 File Offset: 0x00110D74
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009047 RID: 36935
			// (set) Token: 0x0600BFBA RID: 49082 RVA: 0x00112B92 File Offset: 0x00110D92
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009048 RID: 36936
			// (set) Token: 0x0600BFBB RID: 49083 RVA: 0x00112BA5 File Offset: 0x00110DA5
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009049 RID: 36937
			// (set) Token: 0x0600BFBC RID: 49084 RVA: 0x00112BB8 File Offset: 0x00110DB8
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700904A RID: 36938
			// (set) Token: 0x0600BFBD RID: 49085 RVA: 0x00112BCB File Offset: 0x00110DCB
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700904B RID: 36939
			// (set) Token: 0x0600BFBE RID: 49086 RVA: 0x00112BDE File Offset: 0x00110DDE
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700904C RID: 36940
			// (set) Token: 0x0600BFBF RID: 49087 RVA: 0x00112BF1 File Offset: 0x00110DF1
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700904D RID: 36941
			// (set) Token: 0x0600BFC0 RID: 49088 RVA: 0x00112C04 File Offset: 0x00110E04
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700904E RID: 36942
			// (set) Token: 0x0600BFC1 RID: 49089 RVA: 0x00112C17 File Offset: 0x00110E17
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700904F RID: 36943
			// (set) Token: 0x0600BFC2 RID: 49090 RVA: 0x00112C2A File Offset: 0x00110E2A
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009050 RID: 36944
			// (set) Token: 0x0600BFC3 RID: 49091 RVA: 0x00112C3D File Offset: 0x00110E3D
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009051 RID: 36945
			// (set) Token: 0x0600BFC4 RID: 49092 RVA: 0x00112C50 File Offset: 0x00110E50
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009052 RID: 36946
			// (set) Token: 0x0600BFC5 RID: 49093 RVA: 0x00112C63 File Offset: 0x00110E63
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009053 RID: 36947
			// (set) Token: 0x0600BFC6 RID: 49094 RVA: 0x00112C76 File Offset: 0x00110E76
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009054 RID: 36948
			// (set) Token: 0x0600BFC7 RID: 49095 RVA: 0x00112C89 File Offset: 0x00110E89
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009055 RID: 36949
			// (set) Token: 0x0600BFC8 RID: 49096 RVA: 0x00112CA7 File Offset: 0x00110EA7
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009056 RID: 36950
			// (set) Token: 0x0600BFC9 RID: 49097 RVA: 0x00112CBF File Offset: 0x00110EBF
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009057 RID: 36951
			// (set) Token: 0x0600BFCA RID: 49098 RVA: 0x00112CD7 File Offset: 0x00110ED7
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009058 RID: 36952
			// (set) Token: 0x0600BFCB RID: 49099 RVA: 0x00112CEF File Offset: 0x00110EEF
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009059 RID: 36953
			// (set) Token: 0x0600BFCC RID: 49100 RVA: 0x00112D02 File Offset: 0x00110F02
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700905A RID: 36954
			// (set) Token: 0x0600BFCD RID: 49101 RVA: 0x00112D15 File Offset: 0x00110F15
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700905B RID: 36955
			// (set) Token: 0x0600BFCE RID: 49102 RVA: 0x00112D2D File Offset: 0x00110F2D
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700905C RID: 36956
			// (set) Token: 0x0600BFCF RID: 49103 RVA: 0x00112D40 File Offset: 0x00110F40
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x1700905D RID: 36957
			// (set) Token: 0x0600BFD0 RID: 49104 RVA: 0x00112D58 File Offset: 0x00110F58
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700905E RID: 36958
			// (set) Token: 0x0600BFD1 RID: 49105 RVA: 0x00112D70 File Offset: 0x00110F70
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x1700905F RID: 36959
			// (set) Token: 0x0600BFD2 RID: 49106 RVA: 0x00112D83 File Offset: 0x00110F83
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009060 RID: 36960
			// (set) Token: 0x0600BFD3 RID: 49107 RVA: 0x00112D96 File Offset: 0x00110F96
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009061 RID: 36961
			// (set) Token: 0x0600BFD4 RID: 49108 RVA: 0x00112DA9 File Offset: 0x00110FA9
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009062 RID: 36962
			// (set) Token: 0x0600BFD5 RID: 49109 RVA: 0x00112DBC File Offset: 0x00110FBC
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009063 RID: 36963
			// (set) Token: 0x0600BFD6 RID: 49110 RVA: 0x00112DD4 File Offset: 0x00110FD4
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009064 RID: 36964
			// (set) Token: 0x0600BFD7 RID: 49111 RVA: 0x00112DE7 File Offset: 0x00110FE7
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009065 RID: 36965
			// (set) Token: 0x0600BFD8 RID: 49112 RVA: 0x00112DFA File Offset: 0x00110FFA
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009066 RID: 36966
			// (set) Token: 0x0600BFD9 RID: 49113 RVA: 0x00112E12 File Offset: 0x00111012
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009067 RID: 36967
			// (set) Token: 0x0600BFDA RID: 49114 RVA: 0x00112E2A File Offset: 0x0011102A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009068 RID: 36968
			// (set) Token: 0x0600BFDB RID: 49115 RVA: 0x00112E3D File Offset: 0x0011103D
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17009069 RID: 36969
			// (set) Token: 0x0600BFDC RID: 49116 RVA: 0x00112E55 File Offset: 0x00111055
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700906A RID: 36970
			// (set) Token: 0x0600BFDD RID: 49117 RVA: 0x00112E68 File Offset: 0x00111068
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700906B RID: 36971
			// (set) Token: 0x0600BFDE RID: 49118 RVA: 0x00112E7B File Offset: 0x0011107B
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700906C RID: 36972
			// (set) Token: 0x0600BFDF RID: 49119 RVA: 0x00112E8E File Offset: 0x0011108E
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700906D RID: 36973
			// (set) Token: 0x0600BFE0 RID: 49120 RVA: 0x00112EAC File Offset: 0x001110AC
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700906E RID: 36974
			// (set) Token: 0x0600BFE1 RID: 49121 RVA: 0x00112ECA File Offset: 0x001110CA
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700906F RID: 36975
			// (set) Token: 0x0600BFE2 RID: 49122 RVA: 0x00112EE8 File Offset: 0x001110E8
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009070 RID: 36976
			// (set) Token: 0x0600BFE3 RID: 49123 RVA: 0x00112F06 File Offset: 0x00111106
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17009071 RID: 36977
			// (set) Token: 0x0600BFE4 RID: 49124 RVA: 0x00112F19 File Offset: 0x00111119
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009072 RID: 36978
			// (set) Token: 0x0600BFE5 RID: 49125 RVA: 0x00112F31 File Offset: 0x00111131
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009073 RID: 36979
			// (set) Token: 0x0600BFE6 RID: 49126 RVA: 0x00112F4F File Offset: 0x0011114F
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17009074 RID: 36980
			// (set) Token: 0x0600BFE7 RID: 49127 RVA: 0x00112F67 File Offset: 0x00111167
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17009075 RID: 36981
			// (set) Token: 0x0600BFE8 RID: 49128 RVA: 0x00112F7F File Offset: 0x0011117F
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17009076 RID: 36982
			// (set) Token: 0x0600BFE9 RID: 49129 RVA: 0x00112F92 File Offset: 0x00111192
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17009077 RID: 36983
			// (set) Token: 0x0600BFEA RID: 49130 RVA: 0x00112FAA File Offset: 0x001111AA
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009078 RID: 36984
			// (set) Token: 0x0600BFEB RID: 49131 RVA: 0x00112FBD File Offset: 0x001111BD
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009079 RID: 36985
			// (set) Token: 0x0600BFEC RID: 49132 RVA: 0x00112FD0 File Offset: 0x001111D0
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700907A RID: 36986
			// (set) Token: 0x0600BFED RID: 49133 RVA: 0x00112FE3 File Offset: 0x001111E3
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700907B RID: 36987
			// (set) Token: 0x0600BFEE RID: 49134 RVA: 0x00112FF6 File Offset: 0x001111F6
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700907C RID: 36988
			// (set) Token: 0x0600BFEF RID: 49135 RVA: 0x0011300E File Offset: 0x0011120E
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700907D RID: 36989
			// (set) Token: 0x0600BFF0 RID: 49136 RVA: 0x00113021 File Offset: 0x00111221
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700907E RID: 36990
			// (set) Token: 0x0600BFF1 RID: 49137 RVA: 0x00113034 File Offset: 0x00111234
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700907F RID: 36991
			// (set) Token: 0x0600BFF2 RID: 49138 RVA: 0x0011304C File Offset: 0x0011124C
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009080 RID: 36992
			// (set) Token: 0x0600BFF3 RID: 49139 RVA: 0x0011305F File Offset: 0x0011125F
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009081 RID: 36993
			// (set) Token: 0x0600BFF4 RID: 49140 RVA: 0x00113077 File Offset: 0x00111277
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009082 RID: 36994
			// (set) Token: 0x0600BFF5 RID: 49141 RVA: 0x0011308A File Offset: 0x0011128A
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009083 RID: 36995
			// (set) Token: 0x0600BFF6 RID: 49142 RVA: 0x001130A8 File Offset: 0x001112A8
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009084 RID: 36996
			// (set) Token: 0x0600BFF7 RID: 49143 RVA: 0x001130BB File Offset: 0x001112BB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009085 RID: 36997
			// (set) Token: 0x0600BFF8 RID: 49144 RVA: 0x001130D9 File Offset: 0x001112D9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009086 RID: 36998
			// (set) Token: 0x0600BFF9 RID: 49145 RVA: 0x001130EC File Offset: 0x001112EC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009087 RID: 36999
			// (set) Token: 0x0600BFFA RID: 49146 RVA: 0x00113104 File Offset: 0x00111304
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009088 RID: 37000
			// (set) Token: 0x0600BFFB RID: 49147 RVA: 0x0011311C File Offset: 0x0011131C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009089 RID: 37001
			// (set) Token: 0x0600BFFC RID: 49148 RVA: 0x00113134 File Offset: 0x00111334
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700908A RID: 37002
			// (set) Token: 0x0600BFFD RID: 49149 RVA: 0x0011314C File Offset: 0x0011134C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D90 RID: 3472
		public class RemovedMailboxParameters : ParametersBase
		{
			// Token: 0x1700908B RID: 37003
			// (set) Token: 0x0600BFFF RID: 49151 RVA: 0x0011316C File Offset: 0x0011136C
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700908C RID: 37004
			// (set) Token: 0x0600C000 RID: 49152 RVA: 0x00113184 File Offset: 0x00111384
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700908D RID: 37005
			// (set) Token: 0x0600C001 RID: 49153 RVA: 0x00113197 File Offset: 0x00111397
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700908E RID: 37006
			// (set) Token: 0x0600C002 RID: 49154 RVA: 0x001131AF File Offset: 0x001113AF
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700908F RID: 37007
			// (set) Token: 0x0600C003 RID: 49155 RVA: 0x001131C2 File Offset: 0x001113C2
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009090 RID: 37008
			// (set) Token: 0x0600C004 RID: 49156 RVA: 0x001131E0 File Offset: 0x001113E0
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009091 RID: 37009
			// (set) Token: 0x0600C005 RID: 49157 RVA: 0x001131F3 File Offset: 0x001113F3
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009092 RID: 37010
			// (set) Token: 0x0600C006 RID: 49158 RVA: 0x0011320B File Offset: 0x0011140B
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009093 RID: 37011
			// (set) Token: 0x0600C007 RID: 49159 RVA: 0x00113223 File Offset: 0x00111423
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009094 RID: 37012
			// (set) Token: 0x0600C008 RID: 49160 RVA: 0x00113241 File Offset: 0x00111441
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009095 RID: 37013
			// (set) Token: 0x0600C009 RID: 49161 RVA: 0x00113259 File Offset: 0x00111459
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009096 RID: 37014
			// (set) Token: 0x0600C00A RID: 49162 RVA: 0x0011326C File Offset: 0x0011146C
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009097 RID: 37015
			// (set) Token: 0x0600C00B RID: 49163 RVA: 0x0011327F File Offset: 0x0011147F
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009098 RID: 37016
			// (set) Token: 0x0600C00C RID: 49164 RVA: 0x00113292 File Offset: 0x00111492
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009099 RID: 37017
			// (set) Token: 0x0600C00D RID: 49165 RVA: 0x001132A5 File Offset: 0x001114A5
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700909A RID: 37018
			// (set) Token: 0x0600C00E RID: 49166 RVA: 0x001132B8 File Offset: 0x001114B8
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700909B RID: 37019
			// (set) Token: 0x0600C00F RID: 49167 RVA: 0x001132CB File Offset: 0x001114CB
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x1700909C RID: 37020
			// (set) Token: 0x0600C010 RID: 49168 RVA: 0x001132DE File Offset: 0x001114DE
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x1700909D RID: 37021
			// (set) Token: 0x0600C011 RID: 49169 RVA: 0x001132F6 File Offset: 0x001114F6
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x1700909E RID: 37022
			// (set) Token: 0x0600C012 RID: 49170 RVA: 0x00113309 File Offset: 0x00111509
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x1700909F RID: 37023
			// (set) Token: 0x0600C013 RID: 49171 RVA: 0x00113321 File Offset: 0x00111521
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170090A0 RID: 37024
			// (set) Token: 0x0600C014 RID: 49172 RVA: 0x00113334 File Offset: 0x00111534
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170090A1 RID: 37025
			// (set) Token: 0x0600C015 RID: 49173 RVA: 0x00113347 File Offset: 0x00111547
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170090A2 RID: 37026
			// (set) Token: 0x0600C016 RID: 49174 RVA: 0x0011335A File Offset: 0x0011155A
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170090A3 RID: 37027
			// (set) Token: 0x0600C017 RID: 49175 RVA: 0x0011336D File Offset: 0x0011156D
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170090A4 RID: 37028
			// (set) Token: 0x0600C018 RID: 49176 RVA: 0x00113380 File Offset: 0x00111580
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170090A5 RID: 37029
			// (set) Token: 0x0600C019 RID: 49177 RVA: 0x00113393 File Offset: 0x00111593
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170090A6 RID: 37030
			// (set) Token: 0x0600C01A RID: 49178 RVA: 0x001133A6 File Offset: 0x001115A6
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170090A7 RID: 37031
			// (set) Token: 0x0600C01B RID: 49179 RVA: 0x001133B9 File Offset: 0x001115B9
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170090A8 RID: 37032
			// (set) Token: 0x0600C01C RID: 49180 RVA: 0x001133CC File Offset: 0x001115CC
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170090A9 RID: 37033
			// (set) Token: 0x0600C01D RID: 49181 RVA: 0x001133DF File Offset: 0x001115DF
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170090AA RID: 37034
			// (set) Token: 0x0600C01E RID: 49182 RVA: 0x001133F2 File Offset: 0x001115F2
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170090AB RID: 37035
			// (set) Token: 0x0600C01F RID: 49183 RVA: 0x00113405 File Offset: 0x00111605
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170090AC RID: 37036
			// (set) Token: 0x0600C020 RID: 49184 RVA: 0x00113418 File Offset: 0x00111618
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170090AD RID: 37037
			// (set) Token: 0x0600C021 RID: 49185 RVA: 0x0011342B File Offset: 0x0011162B
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170090AE RID: 37038
			// (set) Token: 0x0600C022 RID: 49186 RVA: 0x0011343E File Offset: 0x0011163E
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170090AF RID: 37039
			// (set) Token: 0x0600C023 RID: 49187 RVA: 0x00113451 File Offset: 0x00111651
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170090B0 RID: 37040
			// (set) Token: 0x0600C024 RID: 49188 RVA: 0x00113464 File Offset: 0x00111664
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170090B1 RID: 37041
			// (set) Token: 0x0600C025 RID: 49189 RVA: 0x00113477 File Offset: 0x00111677
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170090B2 RID: 37042
			// (set) Token: 0x0600C026 RID: 49190 RVA: 0x0011348A File Offset: 0x0011168A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170090B3 RID: 37043
			// (set) Token: 0x0600C027 RID: 49191 RVA: 0x0011349D File Offset: 0x0011169D
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170090B4 RID: 37044
			// (set) Token: 0x0600C028 RID: 49192 RVA: 0x001134B0 File Offset: 0x001116B0
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170090B5 RID: 37045
			// (set) Token: 0x0600C029 RID: 49193 RVA: 0x001134C3 File Offset: 0x001116C3
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170090B6 RID: 37046
			// (set) Token: 0x0600C02A RID: 49194 RVA: 0x001134D6 File Offset: 0x001116D6
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170090B7 RID: 37047
			// (set) Token: 0x0600C02B RID: 49195 RVA: 0x001134EE File Offset: 0x001116EE
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x170090B8 RID: 37048
			// (set) Token: 0x0600C02C RID: 49196 RVA: 0x00113501 File Offset: 0x00111701
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x170090B9 RID: 37049
			// (set) Token: 0x0600C02D RID: 49197 RVA: 0x00113519 File Offset: 0x00111719
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x170090BA RID: 37050
			// (set) Token: 0x0600C02E RID: 49198 RVA: 0x0011352C File Offset: 0x0011172C
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x170090BB RID: 37051
			// (set) Token: 0x0600C02F RID: 49199 RVA: 0x00113544 File Offset: 0x00111744
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x170090BC RID: 37052
			// (set) Token: 0x0600C030 RID: 49200 RVA: 0x0011355C File Offset: 0x0011175C
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x170090BD RID: 37053
			// (set) Token: 0x0600C031 RID: 49201 RVA: 0x00113574 File Offset: 0x00111774
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x170090BE RID: 37054
			// (set) Token: 0x0600C032 RID: 49202 RVA: 0x0011358C File Offset: 0x0011178C
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170090BF RID: 37055
			// (set) Token: 0x0600C033 RID: 49203 RVA: 0x001135A4 File Offset: 0x001117A4
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x170090C0 RID: 37056
			// (set) Token: 0x0600C034 RID: 49204 RVA: 0x001135BC File Offset: 0x001117BC
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x170090C1 RID: 37057
			// (set) Token: 0x0600C035 RID: 49205 RVA: 0x001135D4 File Offset: 0x001117D4
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x170090C2 RID: 37058
			// (set) Token: 0x0600C036 RID: 49206 RVA: 0x001135EC File Offset: 0x001117EC
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x170090C3 RID: 37059
			// (set) Token: 0x0600C037 RID: 49207 RVA: 0x00113604 File Offset: 0x00111804
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x170090C4 RID: 37060
			// (set) Token: 0x0600C038 RID: 49208 RVA: 0x00113617 File Offset: 0x00111817
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x170090C5 RID: 37061
			// (set) Token: 0x0600C039 RID: 49209 RVA: 0x0011362A File Offset: 0x0011182A
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x170090C6 RID: 37062
			// (set) Token: 0x0600C03A RID: 49210 RVA: 0x0011363D File Offset: 0x0011183D
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x170090C7 RID: 37063
			// (set) Token: 0x0600C03B RID: 49211 RVA: 0x00113650 File Offset: 0x00111850
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x170090C8 RID: 37064
			// (set) Token: 0x0600C03C RID: 49212 RVA: 0x00113663 File Offset: 0x00111863
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x170090C9 RID: 37065
			// (set) Token: 0x0600C03D RID: 49213 RVA: 0x00113676 File Offset: 0x00111876
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x170090CA RID: 37066
			// (set) Token: 0x0600C03E RID: 49214 RVA: 0x0011368E File Offset: 0x0011188E
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x170090CB RID: 37067
			// (set) Token: 0x0600C03F RID: 49215 RVA: 0x001136A1 File Offset: 0x001118A1
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x170090CC RID: 37068
			// (set) Token: 0x0600C040 RID: 49216 RVA: 0x001136B4 File Offset: 0x001118B4
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x170090CD RID: 37069
			// (set) Token: 0x0600C041 RID: 49217 RVA: 0x001136C7 File Offset: 0x001118C7
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x170090CE RID: 37070
			// (set) Token: 0x0600C042 RID: 49218 RVA: 0x001136E5 File Offset: 0x001118E5
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x170090CF RID: 37071
			// (set) Token: 0x0600C043 RID: 49219 RVA: 0x001136F8 File Offset: 0x001118F8
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x170090D0 RID: 37072
			// (set) Token: 0x0600C044 RID: 49220 RVA: 0x0011370B File Offset: 0x0011190B
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x170090D1 RID: 37073
			// (set) Token: 0x0600C045 RID: 49221 RVA: 0x0011371E File Offset: 0x0011191E
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x170090D2 RID: 37074
			// (set) Token: 0x0600C046 RID: 49222 RVA: 0x00113731 File Offset: 0x00111931
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x170090D3 RID: 37075
			// (set) Token: 0x0600C047 RID: 49223 RVA: 0x00113744 File Offset: 0x00111944
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x170090D4 RID: 37076
			// (set) Token: 0x0600C048 RID: 49224 RVA: 0x00113757 File Offset: 0x00111957
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x170090D5 RID: 37077
			// (set) Token: 0x0600C049 RID: 49225 RVA: 0x0011376A File Offset: 0x0011196A
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x170090D6 RID: 37078
			// (set) Token: 0x0600C04A RID: 49226 RVA: 0x0011377D File Offset: 0x0011197D
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x170090D7 RID: 37079
			// (set) Token: 0x0600C04B RID: 49227 RVA: 0x00113790 File Offset: 0x00111990
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x170090D8 RID: 37080
			// (set) Token: 0x0600C04C RID: 49228 RVA: 0x001137A3 File Offset: 0x001119A3
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x170090D9 RID: 37081
			// (set) Token: 0x0600C04D RID: 49229 RVA: 0x001137B6 File Offset: 0x001119B6
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x170090DA RID: 37082
			// (set) Token: 0x0600C04E RID: 49230 RVA: 0x001137C9 File Offset: 0x001119C9
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x170090DB RID: 37083
			// (set) Token: 0x0600C04F RID: 49231 RVA: 0x001137DC File Offset: 0x001119DC
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170090DC RID: 37084
			// (set) Token: 0x0600C050 RID: 49232 RVA: 0x001137FA File Offset: 0x001119FA
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x170090DD RID: 37085
			// (set) Token: 0x0600C051 RID: 49233 RVA: 0x00113812 File Offset: 0x00111A12
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170090DE RID: 37086
			// (set) Token: 0x0600C052 RID: 49234 RVA: 0x0011382A File Offset: 0x00111A2A
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x170090DF RID: 37087
			// (set) Token: 0x0600C053 RID: 49235 RVA: 0x00113842 File Offset: 0x00111A42
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x170090E0 RID: 37088
			// (set) Token: 0x0600C054 RID: 49236 RVA: 0x00113855 File Offset: 0x00111A55
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x170090E1 RID: 37089
			// (set) Token: 0x0600C055 RID: 49237 RVA: 0x00113868 File Offset: 0x00111A68
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x170090E2 RID: 37090
			// (set) Token: 0x0600C056 RID: 49238 RVA: 0x00113880 File Offset: 0x00111A80
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x170090E3 RID: 37091
			// (set) Token: 0x0600C057 RID: 49239 RVA: 0x00113893 File Offset: 0x00111A93
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x170090E4 RID: 37092
			// (set) Token: 0x0600C058 RID: 49240 RVA: 0x001138AB File Offset: 0x00111AAB
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x170090E5 RID: 37093
			// (set) Token: 0x0600C059 RID: 49241 RVA: 0x001138C3 File Offset: 0x00111AC3
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170090E6 RID: 37094
			// (set) Token: 0x0600C05A RID: 49242 RVA: 0x001138D6 File Offset: 0x00111AD6
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170090E7 RID: 37095
			// (set) Token: 0x0600C05B RID: 49243 RVA: 0x001138E9 File Offset: 0x00111AE9
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x170090E8 RID: 37096
			// (set) Token: 0x0600C05C RID: 49244 RVA: 0x001138FC File Offset: 0x00111AFC
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x170090E9 RID: 37097
			// (set) Token: 0x0600C05D RID: 49245 RVA: 0x0011390F File Offset: 0x00111B0F
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x170090EA RID: 37098
			// (set) Token: 0x0600C05E RID: 49246 RVA: 0x00113927 File Offset: 0x00111B27
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x170090EB RID: 37099
			// (set) Token: 0x0600C05F RID: 49247 RVA: 0x0011393A File Offset: 0x00111B3A
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x170090EC RID: 37100
			// (set) Token: 0x0600C060 RID: 49248 RVA: 0x0011394D File Offset: 0x00111B4D
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x170090ED RID: 37101
			// (set) Token: 0x0600C061 RID: 49249 RVA: 0x00113965 File Offset: 0x00111B65
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x170090EE RID: 37102
			// (set) Token: 0x0600C062 RID: 49250 RVA: 0x0011397D File Offset: 0x00111B7D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170090EF RID: 37103
			// (set) Token: 0x0600C063 RID: 49251 RVA: 0x00113990 File Offset: 0x00111B90
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170090F0 RID: 37104
			// (set) Token: 0x0600C064 RID: 49252 RVA: 0x001139A8 File Offset: 0x00111BA8
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170090F1 RID: 37105
			// (set) Token: 0x0600C065 RID: 49253 RVA: 0x001139BB File Offset: 0x00111BBB
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170090F2 RID: 37106
			// (set) Token: 0x0600C066 RID: 49254 RVA: 0x001139CE File Offset: 0x00111BCE
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170090F3 RID: 37107
			// (set) Token: 0x0600C067 RID: 49255 RVA: 0x001139E1 File Offset: 0x00111BE1
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170090F4 RID: 37108
			// (set) Token: 0x0600C068 RID: 49256 RVA: 0x001139FF File Offset: 0x00111BFF
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170090F5 RID: 37109
			// (set) Token: 0x0600C069 RID: 49257 RVA: 0x00113A1D File Offset: 0x00111C1D
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170090F6 RID: 37110
			// (set) Token: 0x0600C06A RID: 49258 RVA: 0x00113A3B File Offset: 0x00111C3B
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170090F7 RID: 37111
			// (set) Token: 0x0600C06B RID: 49259 RVA: 0x00113A59 File Offset: 0x00111C59
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170090F8 RID: 37112
			// (set) Token: 0x0600C06C RID: 49260 RVA: 0x00113A6C File Offset: 0x00111C6C
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170090F9 RID: 37113
			// (set) Token: 0x0600C06D RID: 49261 RVA: 0x00113A84 File Offset: 0x00111C84
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170090FA RID: 37114
			// (set) Token: 0x0600C06E RID: 49262 RVA: 0x00113AA2 File Offset: 0x00111CA2
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170090FB RID: 37115
			// (set) Token: 0x0600C06F RID: 49263 RVA: 0x00113ABA File Offset: 0x00111CBA
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170090FC RID: 37116
			// (set) Token: 0x0600C070 RID: 49264 RVA: 0x00113AD2 File Offset: 0x00111CD2
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170090FD RID: 37117
			// (set) Token: 0x0600C071 RID: 49265 RVA: 0x00113AE5 File Offset: 0x00111CE5
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170090FE RID: 37118
			// (set) Token: 0x0600C072 RID: 49266 RVA: 0x00113AFD File Offset: 0x00111CFD
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170090FF RID: 37119
			// (set) Token: 0x0600C073 RID: 49267 RVA: 0x00113B10 File Offset: 0x00111D10
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009100 RID: 37120
			// (set) Token: 0x0600C074 RID: 49268 RVA: 0x00113B23 File Offset: 0x00111D23
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009101 RID: 37121
			// (set) Token: 0x0600C075 RID: 49269 RVA: 0x00113B36 File Offset: 0x00111D36
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009102 RID: 37122
			// (set) Token: 0x0600C076 RID: 49270 RVA: 0x00113B49 File Offset: 0x00111D49
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009103 RID: 37123
			// (set) Token: 0x0600C077 RID: 49271 RVA: 0x00113B61 File Offset: 0x00111D61
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009104 RID: 37124
			// (set) Token: 0x0600C078 RID: 49272 RVA: 0x00113B74 File Offset: 0x00111D74
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009105 RID: 37125
			// (set) Token: 0x0600C079 RID: 49273 RVA: 0x00113B87 File Offset: 0x00111D87
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009106 RID: 37126
			// (set) Token: 0x0600C07A RID: 49274 RVA: 0x00113B9F File Offset: 0x00111D9F
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009107 RID: 37127
			// (set) Token: 0x0600C07B RID: 49275 RVA: 0x00113BB2 File Offset: 0x00111DB2
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009108 RID: 37128
			// (set) Token: 0x0600C07C RID: 49276 RVA: 0x00113BCA File Offset: 0x00111DCA
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009109 RID: 37129
			// (set) Token: 0x0600C07D RID: 49277 RVA: 0x00113BDD File Offset: 0x00111DDD
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700910A RID: 37130
			// (set) Token: 0x0600C07E RID: 49278 RVA: 0x00113BFB File Offset: 0x00111DFB
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700910B RID: 37131
			// (set) Token: 0x0600C07F RID: 49279 RVA: 0x00113C0E File Offset: 0x00111E0E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700910C RID: 37132
			// (set) Token: 0x0600C080 RID: 49280 RVA: 0x00113C2C File Offset: 0x00111E2C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700910D RID: 37133
			// (set) Token: 0x0600C081 RID: 49281 RVA: 0x00113C3F File Offset: 0x00111E3F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700910E RID: 37134
			// (set) Token: 0x0600C082 RID: 49282 RVA: 0x00113C57 File Offset: 0x00111E57
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700910F RID: 37135
			// (set) Token: 0x0600C083 RID: 49283 RVA: 0x00113C6F File Offset: 0x00111E6F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009110 RID: 37136
			// (set) Token: 0x0600C084 RID: 49284 RVA: 0x00113C87 File Offset: 0x00111E87
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009111 RID: 37137
			// (set) Token: 0x0600C085 RID: 49285 RVA: 0x00113C9F File Offset: 0x00111E9F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D91 RID: 3473
		public class DiscoveryParameters : ParametersBase
		{
			// Token: 0x17009112 RID: 37138
			// (set) Token: 0x0600C087 RID: 49287 RVA: 0x00113CBF File Offset: 0x00111EBF
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17009113 RID: 37139
			// (set) Token: 0x0600C088 RID: 49288 RVA: 0x00113CD2 File Offset: 0x00111ED2
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17009114 RID: 37140
			// (set) Token: 0x0600C089 RID: 49289 RVA: 0x00113CE5 File Offset: 0x00111EE5
			public virtual SwitchParameter Discovery
			{
				set
				{
					base.PowerSharpParameters["Discovery"] = value;
				}
			}

			// Token: 0x17009115 RID: 37141
			// (set) Token: 0x0600C08A RID: 49290 RVA: 0x00113CFD File Offset: 0x00111EFD
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009116 RID: 37142
			// (set) Token: 0x0600C08B RID: 49291 RVA: 0x00113D15 File Offset: 0x00111F15
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009117 RID: 37143
			// (set) Token: 0x0600C08C RID: 49292 RVA: 0x00113D28 File Offset: 0x00111F28
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009118 RID: 37144
			// (set) Token: 0x0600C08D RID: 49293 RVA: 0x00113D3B File Offset: 0x00111F3B
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009119 RID: 37145
			// (set) Token: 0x0600C08E RID: 49294 RVA: 0x00113D4E File Offset: 0x00111F4E
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700911A RID: 37146
			// (set) Token: 0x0600C08F RID: 49295 RVA: 0x00113D61 File Offset: 0x00111F61
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700911B RID: 37147
			// (set) Token: 0x0600C090 RID: 49296 RVA: 0x00113D74 File Offset: 0x00111F74
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700911C RID: 37148
			// (set) Token: 0x0600C091 RID: 49297 RVA: 0x00113D87 File Offset: 0x00111F87
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x1700911D RID: 37149
			// (set) Token: 0x0600C092 RID: 49298 RVA: 0x00113D9A File Offset: 0x00111F9A
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x1700911E RID: 37150
			// (set) Token: 0x0600C093 RID: 49299 RVA: 0x00113DB2 File Offset: 0x00111FB2
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x1700911F RID: 37151
			// (set) Token: 0x0600C094 RID: 49300 RVA: 0x00113DC5 File Offset: 0x00111FC5
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009120 RID: 37152
			// (set) Token: 0x0600C095 RID: 49301 RVA: 0x00113DDD File Offset: 0x00111FDD
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009121 RID: 37153
			// (set) Token: 0x0600C096 RID: 49302 RVA: 0x00113DF0 File Offset: 0x00111FF0
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009122 RID: 37154
			// (set) Token: 0x0600C097 RID: 49303 RVA: 0x00113E03 File Offset: 0x00112003
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009123 RID: 37155
			// (set) Token: 0x0600C098 RID: 49304 RVA: 0x00113E16 File Offset: 0x00112016
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009124 RID: 37156
			// (set) Token: 0x0600C099 RID: 49305 RVA: 0x00113E29 File Offset: 0x00112029
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009125 RID: 37157
			// (set) Token: 0x0600C09A RID: 49306 RVA: 0x00113E3C File Offset: 0x0011203C
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009126 RID: 37158
			// (set) Token: 0x0600C09B RID: 49307 RVA: 0x00113E4F File Offset: 0x0011204F
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009127 RID: 37159
			// (set) Token: 0x0600C09C RID: 49308 RVA: 0x00113E62 File Offset: 0x00112062
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009128 RID: 37160
			// (set) Token: 0x0600C09D RID: 49309 RVA: 0x00113E75 File Offset: 0x00112075
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009129 RID: 37161
			// (set) Token: 0x0600C09E RID: 49310 RVA: 0x00113E88 File Offset: 0x00112088
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x1700912A RID: 37162
			// (set) Token: 0x0600C09F RID: 49311 RVA: 0x00113E9B File Offset: 0x0011209B
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x1700912B RID: 37163
			// (set) Token: 0x0600C0A0 RID: 49312 RVA: 0x00113EAE File Offset: 0x001120AE
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x1700912C RID: 37164
			// (set) Token: 0x0600C0A1 RID: 49313 RVA: 0x00113EC1 File Offset: 0x001120C1
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x1700912D RID: 37165
			// (set) Token: 0x0600C0A2 RID: 49314 RVA: 0x00113ED4 File Offset: 0x001120D4
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x1700912E RID: 37166
			// (set) Token: 0x0600C0A3 RID: 49315 RVA: 0x00113EE7 File Offset: 0x001120E7
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x1700912F RID: 37167
			// (set) Token: 0x0600C0A4 RID: 49316 RVA: 0x00113EFA File Offset: 0x001120FA
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009130 RID: 37168
			// (set) Token: 0x0600C0A5 RID: 49317 RVA: 0x00113F0D File Offset: 0x0011210D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009131 RID: 37169
			// (set) Token: 0x0600C0A6 RID: 49318 RVA: 0x00113F20 File Offset: 0x00112120
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009132 RID: 37170
			// (set) Token: 0x0600C0A7 RID: 49319 RVA: 0x00113F33 File Offset: 0x00112133
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009133 RID: 37171
			// (set) Token: 0x0600C0A8 RID: 49320 RVA: 0x00113F46 File Offset: 0x00112146
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009134 RID: 37172
			// (set) Token: 0x0600C0A9 RID: 49321 RVA: 0x00113F59 File Offset: 0x00112159
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009135 RID: 37173
			// (set) Token: 0x0600C0AA RID: 49322 RVA: 0x00113F6C File Offset: 0x0011216C
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009136 RID: 37174
			// (set) Token: 0x0600C0AB RID: 49323 RVA: 0x00113F7F File Offset: 0x0011217F
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009137 RID: 37175
			// (set) Token: 0x0600C0AC RID: 49324 RVA: 0x00113F92 File Offset: 0x00112192
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009138 RID: 37176
			// (set) Token: 0x0600C0AD RID: 49325 RVA: 0x00113FAA File Offset: 0x001121AA
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009139 RID: 37177
			// (set) Token: 0x0600C0AE RID: 49326 RVA: 0x00113FBD File Offset: 0x001121BD
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700913A RID: 37178
			// (set) Token: 0x0600C0AF RID: 49327 RVA: 0x00113FD5 File Offset: 0x001121D5
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x1700913B RID: 37179
			// (set) Token: 0x0600C0B0 RID: 49328 RVA: 0x00113FE8 File Offset: 0x001121E8
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x1700913C RID: 37180
			// (set) Token: 0x0600C0B1 RID: 49329 RVA: 0x00114000 File Offset: 0x00112200
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x1700913D RID: 37181
			// (set) Token: 0x0600C0B2 RID: 49330 RVA: 0x00114018 File Offset: 0x00112218
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x1700913E RID: 37182
			// (set) Token: 0x0600C0B3 RID: 49331 RVA: 0x00114030 File Offset: 0x00112230
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x1700913F RID: 37183
			// (set) Token: 0x0600C0B4 RID: 49332 RVA: 0x00114048 File Offset: 0x00112248
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17009140 RID: 37184
			// (set) Token: 0x0600C0B5 RID: 49333 RVA: 0x00114060 File Offset: 0x00112260
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17009141 RID: 37185
			// (set) Token: 0x0600C0B6 RID: 49334 RVA: 0x00114078 File Offset: 0x00112278
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009142 RID: 37186
			// (set) Token: 0x0600C0B7 RID: 49335 RVA: 0x00114090 File Offset: 0x00112290
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17009143 RID: 37187
			// (set) Token: 0x0600C0B8 RID: 49336 RVA: 0x001140A8 File Offset: 0x001122A8
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009144 RID: 37188
			// (set) Token: 0x0600C0B9 RID: 49337 RVA: 0x001140C0 File Offset: 0x001122C0
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009145 RID: 37189
			// (set) Token: 0x0600C0BA RID: 49338 RVA: 0x001140D3 File Offset: 0x001122D3
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009146 RID: 37190
			// (set) Token: 0x0600C0BB RID: 49339 RVA: 0x001140E6 File Offset: 0x001122E6
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009147 RID: 37191
			// (set) Token: 0x0600C0BC RID: 49340 RVA: 0x001140F9 File Offset: 0x001122F9
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009148 RID: 37192
			// (set) Token: 0x0600C0BD RID: 49341 RVA: 0x0011410C File Offset: 0x0011230C
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009149 RID: 37193
			// (set) Token: 0x0600C0BE RID: 49342 RVA: 0x0011411F File Offset: 0x0011231F
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x1700914A RID: 37194
			// (set) Token: 0x0600C0BF RID: 49343 RVA: 0x00114132 File Offset: 0x00112332
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x1700914B RID: 37195
			// (set) Token: 0x0600C0C0 RID: 49344 RVA: 0x0011414A File Offset: 0x0011234A
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700914C RID: 37196
			// (set) Token: 0x0600C0C1 RID: 49345 RVA: 0x0011415D File Offset: 0x0011235D
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700914D RID: 37197
			// (set) Token: 0x0600C0C2 RID: 49346 RVA: 0x00114170 File Offset: 0x00112370
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700914E RID: 37198
			// (set) Token: 0x0600C0C3 RID: 49347 RVA: 0x00114183 File Offset: 0x00112383
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700914F RID: 37199
			// (set) Token: 0x0600C0C4 RID: 49348 RVA: 0x001141A1 File Offset: 0x001123A1
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009150 RID: 37200
			// (set) Token: 0x0600C0C5 RID: 49349 RVA: 0x001141B4 File Offset: 0x001123B4
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009151 RID: 37201
			// (set) Token: 0x0600C0C6 RID: 49350 RVA: 0x001141C7 File Offset: 0x001123C7
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009152 RID: 37202
			// (set) Token: 0x0600C0C7 RID: 49351 RVA: 0x001141DA File Offset: 0x001123DA
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009153 RID: 37203
			// (set) Token: 0x0600C0C8 RID: 49352 RVA: 0x001141ED File Offset: 0x001123ED
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009154 RID: 37204
			// (set) Token: 0x0600C0C9 RID: 49353 RVA: 0x00114200 File Offset: 0x00112400
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009155 RID: 37205
			// (set) Token: 0x0600C0CA RID: 49354 RVA: 0x00114213 File Offset: 0x00112413
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009156 RID: 37206
			// (set) Token: 0x0600C0CB RID: 49355 RVA: 0x00114226 File Offset: 0x00112426
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009157 RID: 37207
			// (set) Token: 0x0600C0CC RID: 49356 RVA: 0x00114239 File Offset: 0x00112439
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009158 RID: 37208
			// (set) Token: 0x0600C0CD RID: 49357 RVA: 0x0011424C File Offset: 0x0011244C
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009159 RID: 37209
			// (set) Token: 0x0600C0CE RID: 49358 RVA: 0x0011425F File Offset: 0x0011245F
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700915A RID: 37210
			// (set) Token: 0x0600C0CF RID: 49359 RVA: 0x00114272 File Offset: 0x00112472
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700915B RID: 37211
			// (set) Token: 0x0600C0D0 RID: 49360 RVA: 0x00114285 File Offset: 0x00112485
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700915C RID: 37212
			// (set) Token: 0x0600C0D1 RID: 49361 RVA: 0x00114298 File Offset: 0x00112498
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700915D RID: 37213
			// (set) Token: 0x0600C0D2 RID: 49362 RVA: 0x001142B6 File Offset: 0x001124B6
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x1700915E RID: 37214
			// (set) Token: 0x0600C0D3 RID: 49363 RVA: 0x001142CE File Offset: 0x001124CE
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x1700915F RID: 37215
			// (set) Token: 0x0600C0D4 RID: 49364 RVA: 0x001142E6 File Offset: 0x001124E6
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009160 RID: 37216
			// (set) Token: 0x0600C0D5 RID: 49365 RVA: 0x001142FE File Offset: 0x001124FE
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009161 RID: 37217
			// (set) Token: 0x0600C0D6 RID: 49366 RVA: 0x00114311 File Offset: 0x00112511
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009162 RID: 37218
			// (set) Token: 0x0600C0D7 RID: 49367 RVA: 0x00114324 File Offset: 0x00112524
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009163 RID: 37219
			// (set) Token: 0x0600C0D8 RID: 49368 RVA: 0x0011433C File Offset: 0x0011253C
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009164 RID: 37220
			// (set) Token: 0x0600C0D9 RID: 49369 RVA: 0x0011434F File Offset: 0x0011254F
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009165 RID: 37221
			// (set) Token: 0x0600C0DA RID: 49370 RVA: 0x00114367 File Offset: 0x00112567
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009166 RID: 37222
			// (set) Token: 0x0600C0DB RID: 49371 RVA: 0x0011437F File Offset: 0x0011257F
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009167 RID: 37223
			// (set) Token: 0x0600C0DC RID: 49372 RVA: 0x00114392 File Offset: 0x00112592
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009168 RID: 37224
			// (set) Token: 0x0600C0DD RID: 49373 RVA: 0x001143A5 File Offset: 0x001125A5
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009169 RID: 37225
			// (set) Token: 0x0600C0DE RID: 49374 RVA: 0x001143B8 File Offset: 0x001125B8
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700916A RID: 37226
			// (set) Token: 0x0600C0DF RID: 49375 RVA: 0x001143CB File Offset: 0x001125CB
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700916B RID: 37227
			// (set) Token: 0x0600C0E0 RID: 49376 RVA: 0x001143E3 File Offset: 0x001125E3
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700916C RID: 37228
			// (set) Token: 0x0600C0E1 RID: 49377 RVA: 0x001143F6 File Offset: 0x001125F6
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700916D RID: 37229
			// (set) Token: 0x0600C0E2 RID: 49378 RVA: 0x00114409 File Offset: 0x00112609
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700916E RID: 37230
			// (set) Token: 0x0600C0E3 RID: 49379 RVA: 0x00114421 File Offset: 0x00112621
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700916F RID: 37231
			// (set) Token: 0x0600C0E4 RID: 49380 RVA: 0x00114439 File Offset: 0x00112639
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009170 RID: 37232
			// (set) Token: 0x0600C0E5 RID: 49381 RVA: 0x0011444C File Offset: 0x0011264C
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17009171 RID: 37233
			// (set) Token: 0x0600C0E6 RID: 49382 RVA: 0x00114464 File Offset: 0x00112664
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009172 RID: 37234
			// (set) Token: 0x0600C0E7 RID: 49383 RVA: 0x00114477 File Offset: 0x00112677
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009173 RID: 37235
			// (set) Token: 0x0600C0E8 RID: 49384 RVA: 0x0011448A File Offset: 0x0011268A
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17009174 RID: 37236
			// (set) Token: 0x0600C0E9 RID: 49385 RVA: 0x0011449D File Offset: 0x0011269D
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009175 RID: 37237
			// (set) Token: 0x0600C0EA RID: 49386 RVA: 0x001144BB File Offset: 0x001126BB
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009176 RID: 37238
			// (set) Token: 0x0600C0EB RID: 49387 RVA: 0x001144D9 File Offset: 0x001126D9
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009177 RID: 37239
			// (set) Token: 0x0600C0EC RID: 49388 RVA: 0x001144F7 File Offset: 0x001126F7
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009178 RID: 37240
			// (set) Token: 0x0600C0ED RID: 49389 RVA: 0x00114515 File Offset: 0x00112715
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17009179 RID: 37241
			// (set) Token: 0x0600C0EE RID: 49390 RVA: 0x00114528 File Offset: 0x00112728
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700917A RID: 37242
			// (set) Token: 0x0600C0EF RID: 49391 RVA: 0x00114540 File Offset: 0x00112740
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700917B RID: 37243
			// (set) Token: 0x0600C0F0 RID: 49392 RVA: 0x0011455E File Offset: 0x0011275E
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700917C RID: 37244
			// (set) Token: 0x0600C0F1 RID: 49393 RVA: 0x00114576 File Offset: 0x00112776
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700917D RID: 37245
			// (set) Token: 0x0600C0F2 RID: 49394 RVA: 0x0011458E File Offset: 0x0011278E
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x1700917E RID: 37246
			// (set) Token: 0x0600C0F3 RID: 49395 RVA: 0x001145A1 File Offset: 0x001127A1
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700917F RID: 37247
			// (set) Token: 0x0600C0F4 RID: 49396 RVA: 0x001145B9 File Offset: 0x001127B9
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009180 RID: 37248
			// (set) Token: 0x0600C0F5 RID: 49397 RVA: 0x001145CC File Offset: 0x001127CC
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009181 RID: 37249
			// (set) Token: 0x0600C0F6 RID: 49398 RVA: 0x001145DF File Offset: 0x001127DF
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009182 RID: 37250
			// (set) Token: 0x0600C0F7 RID: 49399 RVA: 0x001145F2 File Offset: 0x001127F2
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009183 RID: 37251
			// (set) Token: 0x0600C0F8 RID: 49400 RVA: 0x00114605 File Offset: 0x00112805
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009184 RID: 37252
			// (set) Token: 0x0600C0F9 RID: 49401 RVA: 0x0011461D File Offset: 0x0011281D
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009185 RID: 37253
			// (set) Token: 0x0600C0FA RID: 49402 RVA: 0x00114630 File Offset: 0x00112830
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009186 RID: 37254
			// (set) Token: 0x0600C0FB RID: 49403 RVA: 0x00114643 File Offset: 0x00112843
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009187 RID: 37255
			// (set) Token: 0x0600C0FC RID: 49404 RVA: 0x0011465B File Offset: 0x0011285B
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009188 RID: 37256
			// (set) Token: 0x0600C0FD RID: 49405 RVA: 0x0011466E File Offset: 0x0011286E
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009189 RID: 37257
			// (set) Token: 0x0600C0FE RID: 49406 RVA: 0x00114686 File Offset: 0x00112886
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700918A RID: 37258
			// (set) Token: 0x0600C0FF RID: 49407 RVA: 0x00114699 File Offset: 0x00112899
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700918B RID: 37259
			// (set) Token: 0x0600C100 RID: 49408 RVA: 0x001146B7 File Offset: 0x001128B7
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700918C RID: 37260
			// (set) Token: 0x0600C101 RID: 49409 RVA: 0x001146CA File Offset: 0x001128CA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700918D RID: 37261
			// (set) Token: 0x0600C102 RID: 49410 RVA: 0x001146E8 File Offset: 0x001128E8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700918E RID: 37262
			// (set) Token: 0x0600C103 RID: 49411 RVA: 0x001146FB File Offset: 0x001128FB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700918F RID: 37263
			// (set) Token: 0x0600C104 RID: 49412 RVA: 0x00114713 File Offset: 0x00112913
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009190 RID: 37264
			// (set) Token: 0x0600C105 RID: 49413 RVA: 0x0011472B File Offset: 0x0011292B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009191 RID: 37265
			// (set) Token: 0x0600C106 RID: 49414 RVA: 0x00114743 File Offset: 0x00112943
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009192 RID: 37266
			// (set) Token: 0x0600C107 RID: 49415 RVA: 0x0011475B File Offset: 0x0011295B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D92 RID: 3474
		public class TeamMailboxIWParameters : ParametersBase
		{
			// Token: 0x17009193 RID: 37267
			// (set) Token: 0x0600C109 RID: 49417 RVA: 0x0011477B File Offset: 0x0011297B
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17009194 RID: 37268
			// (set) Token: 0x0600C10A RID: 49418 RVA: 0x0011478E File Offset: 0x0011298E
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17009195 RID: 37269
			// (set) Token: 0x0600C10B RID: 49419 RVA: 0x001147A1 File Offset: 0x001129A1
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009196 RID: 37270
			// (set) Token: 0x0600C10C RID: 49420 RVA: 0x001147BF File Offset: 0x001129BF
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009197 RID: 37271
			// (set) Token: 0x0600C10D RID: 49421 RVA: 0x001147D2 File Offset: 0x001129D2
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009198 RID: 37272
			// (set) Token: 0x0600C10E RID: 49422 RVA: 0x001147EA File Offset: 0x001129EA
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009199 RID: 37273
			// (set) Token: 0x0600C10F RID: 49423 RVA: 0x00114802 File Offset: 0x00112A02
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x1700919A RID: 37274
			// (set) Token: 0x0600C110 RID: 49424 RVA: 0x0011481A File Offset: 0x00112A1A
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700919B RID: 37275
			// (set) Token: 0x0600C111 RID: 49425 RVA: 0x0011482D File Offset: 0x00112A2D
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700919C RID: 37276
			// (set) Token: 0x0600C112 RID: 49426 RVA: 0x00114840 File Offset: 0x00112A40
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700919D RID: 37277
			// (set) Token: 0x0600C113 RID: 49427 RVA: 0x00114853 File Offset: 0x00112A53
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700919E RID: 37278
			// (set) Token: 0x0600C114 RID: 49428 RVA: 0x00114866 File Offset: 0x00112A66
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700919F RID: 37279
			// (set) Token: 0x0600C115 RID: 49429 RVA: 0x00114879 File Offset: 0x00112A79
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170091A0 RID: 37280
			// (set) Token: 0x0600C116 RID: 49430 RVA: 0x0011488C File Offset: 0x00112A8C
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x170091A1 RID: 37281
			// (set) Token: 0x0600C117 RID: 49431 RVA: 0x0011489F File Offset: 0x00112A9F
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x170091A2 RID: 37282
			// (set) Token: 0x0600C118 RID: 49432 RVA: 0x001148B7 File Offset: 0x00112AB7
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x170091A3 RID: 37283
			// (set) Token: 0x0600C119 RID: 49433 RVA: 0x001148CA File Offset: 0x00112ACA
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x170091A4 RID: 37284
			// (set) Token: 0x0600C11A RID: 49434 RVA: 0x001148E2 File Offset: 0x00112AE2
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170091A5 RID: 37285
			// (set) Token: 0x0600C11B RID: 49435 RVA: 0x001148F5 File Offset: 0x00112AF5
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170091A6 RID: 37286
			// (set) Token: 0x0600C11C RID: 49436 RVA: 0x00114908 File Offset: 0x00112B08
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170091A7 RID: 37287
			// (set) Token: 0x0600C11D RID: 49437 RVA: 0x0011491B File Offset: 0x00112B1B
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170091A8 RID: 37288
			// (set) Token: 0x0600C11E RID: 49438 RVA: 0x0011492E File Offset: 0x00112B2E
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170091A9 RID: 37289
			// (set) Token: 0x0600C11F RID: 49439 RVA: 0x00114941 File Offset: 0x00112B41
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170091AA RID: 37290
			// (set) Token: 0x0600C120 RID: 49440 RVA: 0x00114954 File Offset: 0x00112B54
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170091AB RID: 37291
			// (set) Token: 0x0600C121 RID: 49441 RVA: 0x00114967 File Offset: 0x00112B67
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170091AC RID: 37292
			// (set) Token: 0x0600C122 RID: 49442 RVA: 0x0011497A File Offset: 0x00112B7A
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170091AD RID: 37293
			// (set) Token: 0x0600C123 RID: 49443 RVA: 0x0011498D File Offset: 0x00112B8D
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170091AE RID: 37294
			// (set) Token: 0x0600C124 RID: 49444 RVA: 0x001149A0 File Offset: 0x00112BA0
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170091AF RID: 37295
			// (set) Token: 0x0600C125 RID: 49445 RVA: 0x001149B3 File Offset: 0x00112BB3
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170091B0 RID: 37296
			// (set) Token: 0x0600C126 RID: 49446 RVA: 0x001149C6 File Offset: 0x00112BC6
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170091B1 RID: 37297
			// (set) Token: 0x0600C127 RID: 49447 RVA: 0x001149D9 File Offset: 0x00112BD9
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170091B2 RID: 37298
			// (set) Token: 0x0600C128 RID: 49448 RVA: 0x001149EC File Offset: 0x00112BEC
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170091B3 RID: 37299
			// (set) Token: 0x0600C129 RID: 49449 RVA: 0x001149FF File Offset: 0x00112BFF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170091B4 RID: 37300
			// (set) Token: 0x0600C12A RID: 49450 RVA: 0x00114A12 File Offset: 0x00112C12
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170091B5 RID: 37301
			// (set) Token: 0x0600C12B RID: 49451 RVA: 0x00114A25 File Offset: 0x00112C25
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170091B6 RID: 37302
			// (set) Token: 0x0600C12C RID: 49452 RVA: 0x00114A38 File Offset: 0x00112C38
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170091B7 RID: 37303
			// (set) Token: 0x0600C12D RID: 49453 RVA: 0x00114A4B File Offset: 0x00112C4B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170091B8 RID: 37304
			// (set) Token: 0x0600C12E RID: 49454 RVA: 0x00114A5E File Offset: 0x00112C5E
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170091B9 RID: 37305
			// (set) Token: 0x0600C12F RID: 49455 RVA: 0x00114A71 File Offset: 0x00112C71
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170091BA RID: 37306
			// (set) Token: 0x0600C130 RID: 49456 RVA: 0x00114A84 File Offset: 0x00112C84
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170091BB RID: 37307
			// (set) Token: 0x0600C131 RID: 49457 RVA: 0x00114A97 File Offset: 0x00112C97
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170091BC RID: 37308
			// (set) Token: 0x0600C132 RID: 49458 RVA: 0x00114AAF File Offset: 0x00112CAF
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x170091BD RID: 37309
			// (set) Token: 0x0600C133 RID: 49459 RVA: 0x00114AC2 File Offset: 0x00112CC2
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x170091BE RID: 37310
			// (set) Token: 0x0600C134 RID: 49460 RVA: 0x00114ADA File Offset: 0x00112CDA
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x170091BF RID: 37311
			// (set) Token: 0x0600C135 RID: 49461 RVA: 0x00114AED File Offset: 0x00112CED
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x170091C0 RID: 37312
			// (set) Token: 0x0600C136 RID: 49462 RVA: 0x00114B05 File Offset: 0x00112D05
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x170091C1 RID: 37313
			// (set) Token: 0x0600C137 RID: 49463 RVA: 0x00114B1D File Offset: 0x00112D1D
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x170091C2 RID: 37314
			// (set) Token: 0x0600C138 RID: 49464 RVA: 0x00114B35 File Offset: 0x00112D35
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x170091C3 RID: 37315
			// (set) Token: 0x0600C139 RID: 49465 RVA: 0x00114B4D File Offset: 0x00112D4D
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170091C4 RID: 37316
			// (set) Token: 0x0600C13A RID: 49466 RVA: 0x00114B65 File Offset: 0x00112D65
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x170091C5 RID: 37317
			// (set) Token: 0x0600C13B RID: 49467 RVA: 0x00114B7D File Offset: 0x00112D7D
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x170091C6 RID: 37318
			// (set) Token: 0x0600C13C RID: 49468 RVA: 0x00114B95 File Offset: 0x00112D95
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x170091C7 RID: 37319
			// (set) Token: 0x0600C13D RID: 49469 RVA: 0x00114BAD File Offset: 0x00112DAD
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x170091C8 RID: 37320
			// (set) Token: 0x0600C13E RID: 49470 RVA: 0x00114BC5 File Offset: 0x00112DC5
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x170091C9 RID: 37321
			// (set) Token: 0x0600C13F RID: 49471 RVA: 0x00114BD8 File Offset: 0x00112DD8
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x170091CA RID: 37322
			// (set) Token: 0x0600C140 RID: 49472 RVA: 0x00114BEB File Offset: 0x00112DEB
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x170091CB RID: 37323
			// (set) Token: 0x0600C141 RID: 49473 RVA: 0x00114BFE File Offset: 0x00112DFE
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x170091CC RID: 37324
			// (set) Token: 0x0600C142 RID: 49474 RVA: 0x00114C11 File Offset: 0x00112E11
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x170091CD RID: 37325
			// (set) Token: 0x0600C143 RID: 49475 RVA: 0x00114C24 File Offset: 0x00112E24
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x170091CE RID: 37326
			// (set) Token: 0x0600C144 RID: 49476 RVA: 0x00114C37 File Offset: 0x00112E37
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x170091CF RID: 37327
			// (set) Token: 0x0600C145 RID: 49477 RVA: 0x00114C4F File Offset: 0x00112E4F
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x170091D0 RID: 37328
			// (set) Token: 0x0600C146 RID: 49478 RVA: 0x00114C62 File Offset: 0x00112E62
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x170091D1 RID: 37329
			// (set) Token: 0x0600C147 RID: 49479 RVA: 0x00114C75 File Offset: 0x00112E75
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x170091D2 RID: 37330
			// (set) Token: 0x0600C148 RID: 49480 RVA: 0x00114C88 File Offset: 0x00112E88
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x170091D3 RID: 37331
			// (set) Token: 0x0600C149 RID: 49481 RVA: 0x00114CA6 File Offset: 0x00112EA6
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x170091D4 RID: 37332
			// (set) Token: 0x0600C14A RID: 49482 RVA: 0x00114CB9 File Offset: 0x00112EB9
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x170091D5 RID: 37333
			// (set) Token: 0x0600C14B RID: 49483 RVA: 0x00114CCC File Offset: 0x00112ECC
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x170091D6 RID: 37334
			// (set) Token: 0x0600C14C RID: 49484 RVA: 0x00114CDF File Offset: 0x00112EDF
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x170091D7 RID: 37335
			// (set) Token: 0x0600C14D RID: 49485 RVA: 0x00114CF2 File Offset: 0x00112EF2
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x170091D8 RID: 37336
			// (set) Token: 0x0600C14E RID: 49486 RVA: 0x00114D05 File Offset: 0x00112F05
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x170091D9 RID: 37337
			// (set) Token: 0x0600C14F RID: 49487 RVA: 0x00114D18 File Offset: 0x00112F18
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x170091DA RID: 37338
			// (set) Token: 0x0600C150 RID: 49488 RVA: 0x00114D2B File Offset: 0x00112F2B
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x170091DB RID: 37339
			// (set) Token: 0x0600C151 RID: 49489 RVA: 0x00114D3E File Offset: 0x00112F3E
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x170091DC RID: 37340
			// (set) Token: 0x0600C152 RID: 49490 RVA: 0x00114D51 File Offset: 0x00112F51
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x170091DD RID: 37341
			// (set) Token: 0x0600C153 RID: 49491 RVA: 0x00114D64 File Offset: 0x00112F64
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x170091DE RID: 37342
			// (set) Token: 0x0600C154 RID: 49492 RVA: 0x00114D77 File Offset: 0x00112F77
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x170091DF RID: 37343
			// (set) Token: 0x0600C155 RID: 49493 RVA: 0x00114D8A File Offset: 0x00112F8A
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x170091E0 RID: 37344
			// (set) Token: 0x0600C156 RID: 49494 RVA: 0x00114D9D File Offset: 0x00112F9D
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170091E1 RID: 37345
			// (set) Token: 0x0600C157 RID: 49495 RVA: 0x00114DBB File Offset: 0x00112FBB
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x170091E2 RID: 37346
			// (set) Token: 0x0600C158 RID: 49496 RVA: 0x00114DD3 File Offset: 0x00112FD3
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170091E3 RID: 37347
			// (set) Token: 0x0600C159 RID: 49497 RVA: 0x00114DEB File Offset: 0x00112FEB
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x170091E4 RID: 37348
			// (set) Token: 0x0600C15A RID: 49498 RVA: 0x00114E03 File Offset: 0x00113003
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x170091E5 RID: 37349
			// (set) Token: 0x0600C15B RID: 49499 RVA: 0x00114E16 File Offset: 0x00113016
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x170091E6 RID: 37350
			// (set) Token: 0x0600C15C RID: 49500 RVA: 0x00114E29 File Offset: 0x00113029
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x170091E7 RID: 37351
			// (set) Token: 0x0600C15D RID: 49501 RVA: 0x00114E41 File Offset: 0x00113041
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x170091E8 RID: 37352
			// (set) Token: 0x0600C15E RID: 49502 RVA: 0x00114E54 File Offset: 0x00113054
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x170091E9 RID: 37353
			// (set) Token: 0x0600C15F RID: 49503 RVA: 0x00114E6C File Offset: 0x0011306C
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x170091EA RID: 37354
			// (set) Token: 0x0600C160 RID: 49504 RVA: 0x00114E84 File Offset: 0x00113084
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170091EB RID: 37355
			// (set) Token: 0x0600C161 RID: 49505 RVA: 0x00114E97 File Offset: 0x00113097
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170091EC RID: 37356
			// (set) Token: 0x0600C162 RID: 49506 RVA: 0x00114EAA File Offset: 0x001130AA
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x170091ED RID: 37357
			// (set) Token: 0x0600C163 RID: 49507 RVA: 0x00114EBD File Offset: 0x001130BD
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x170091EE RID: 37358
			// (set) Token: 0x0600C164 RID: 49508 RVA: 0x00114ED0 File Offset: 0x001130D0
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x170091EF RID: 37359
			// (set) Token: 0x0600C165 RID: 49509 RVA: 0x00114EE8 File Offset: 0x001130E8
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x170091F0 RID: 37360
			// (set) Token: 0x0600C166 RID: 49510 RVA: 0x00114EFB File Offset: 0x001130FB
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x170091F1 RID: 37361
			// (set) Token: 0x0600C167 RID: 49511 RVA: 0x00114F0E File Offset: 0x0011310E
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x170091F2 RID: 37362
			// (set) Token: 0x0600C168 RID: 49512 RVA: 0x00114F26 File Offset: 0x00113126
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x170091F3 RID: 37363
			// (set) Token: 0x0600C169 RID: 49513 RVA: 0x00114F3E File Offset: 0x0011313E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170091F4 RID: 37364
			// (set) Token: 0x0600C16A RID: 49514 RVA: 0x00114F51 File Offset: 0x00113151
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170091F5 RID: 37365
			// (set) Token: 0x0600C16B RID: 49515 RVA: 0x00114F69 File Offset: 0x00113169
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170091F6 RID: 37366
			// (set) Token: 0x0600C16C RID: 49516 RVA: 0x00114F7C File Offset: 0x0011317C
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170091F7 RID: 37367
			// (set) Token: 0x0600C16D RID: 49517 RVA: 0x00114F8F File Offset: 0x0011318F
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170091F8 RID: 37368
			// (set) Token: 0x0600C16E RID: 49518 RVA: 0x00114FA2 File Offset: 0x001131A2
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170091F9 RID: 37369
			// (set) Token: 0x0600C16F RID: 49519 RVA: 0x00114FC0 File Offset: 0x001131C0
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170091FA RID: 37370
			// (set) Token: 0x0600C170 RID: 49520 RVA: 0x00114FDE File Offset: 0x001131DE
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170091FB RID: 37371
			// (set) Token: 0x0600C171 RID: 49521 RVA: 0x00114FFC File Offset: 0x001131FC
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170091FC RID: 37372
			// (set) Token: 0x0600C172 RID: 49522 RVA: 0x0011501A File Offset: 0x0011321A
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170091FD RID: 37373
			// (set) Token: 0x0600C173 RID: 49523 RVA: 0x0011502D File Offset: 0x0011322D
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170091FE RID: 37374
			// (set) Token: 0x0600C174 RID: 49524 RVA: 0x00115045 File Offset: 0x00113245
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170091FF RID: 37375
			// (set) Token: 0x0600C175 RID: 49525 RVA: 0x00115063 File Offset: 0x00113263
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17009200 RID: 37376
			// (set) Token: 0x0600C176 RID: 49526 RVA: 0x0011507B File Offset: 0x0011327B
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17009201 RID: 37377
			// (set) Token: 0x0600C177 RID: 49527 RVA: 0x00115093 File Offset: 0x00113293
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17009202 RID: 37378
			// (set) Token: 0x0600C178 RID: 49528 RVA: 0x001150A6 File Offset: 0x001132A6
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17009203 RID: 37379
			// (set) Token: 0x0600C179 RID: 49529 RVA: 0x001150BE File Offset: 0x001132BE
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009204 RID: 37380
			// (set) Token: 0x0600C17A RID: 49530 RVA: 0x001150D1 File Offset: 0x001132D1
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009205 RID: 37381
			// (set) Token: 0x0600C17B RID: 49531 RVA: 0x001150E4 File Offset: 0x001132E4
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009206 RID: 37382
			// (set) Token: 0x0600C17C RID: 49532 RVA: 0x001150F7 File Offset: 0x001132F7
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009207 RID: 37383
			// (set) Token: 0x0600C17D RID: 49533 RVA: 0x0011510A File Offset: 0x0011330A
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009208 RID: 37384
			// (set) Token: 0x0600C17E RID: 49534 RVA: 0x00115122 File Offset: 0x00113322
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009209 RID: 37385
			// (set) Token: 0x0600C17F RID: 49535 RVA: 0x00115135 File Offset: 0x00113335
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700920A RID: 37386
			// (set) Token: 0x0600C180 RID: 49536 RVA: 0x00115148 File Offset: 0x00113348
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700920B RID: 37387
			// (set) Token: 0x0600C181 RID: 49537 RVA: 0x00115160 File Offset: 0x00113360
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700920C RID: 37388
			// (set) Token: 0x0600C182 RID: 49538 RVA: 0x00115173 File Offset: 0x00113373
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700920D RID: 37389
			// (set) Token: 0x0600C183 RID: 49539 RVA: 0x0011518B File Offset: 0x0011338B
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700920E RID: 37390
			// (set) Token: 0x0600C184 RID: 49540 RVA: 0x0011519E File Offset: 0x0011339E
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700920F RID: 37391
			// (set) Token: 0x0600C185 RID: 49541 RVA: 0x001151BC File Offset: 0x001133BC
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009210 RID: 37392
			// (set) Token: 0x0600C186 RID: 49542 RVA: 0x001151CF File Offset: 0x001133CF
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009211 RID: 37393
			// (set) Token: 0x0600C187 RID: 49543 RVA: 0x001151ED File Offset: 0x001133ED
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009212 RID: 37394
			// (set) Token: 0x0600C188 RID: 49544 RVA: 0x00115200 File Offset: 0x00113400
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009213 RID: 37395
			// (set) Token: 0x0600C189 RID: 49545 RVA: 0x00115218 File Offset: 0x00113418
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009214 RID: 37396
			// (set) Token: 0x0600C18A RID: 49546 RVA: 0x00115230 File Offset: 0x00113430
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009215 RID: 37397
			// (set) Token: 0x0600C18B RID: 49547 RVA: 0x00115248 File Offset: 0x00113448
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009216 RID: 37398
			// (set) Token: 0x0600C18C RID: 49548 RVA: 0x00115260 File Offset: 0x00113460
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D93 RID: 3475
		public class ArbitrationParameters : ParametersBase
		{
			// Token: 0x17009217 RID: 37399
			// (set) Token: 0x0600C18E RID: 49550 RVA: 0x00115280 File Offset: 0x00113480
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17009218 RID: 37400
			// (set) Token: 0x0600C18F RID: 49551 RVA: 0x00115293 File Offset: 0x00113493
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17009219 RID: 37401
			// (set) Token: 0x0600C190 RID: 49552 RVA: 0x001152A6 File Offset: 0x001134A6
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700921A RID: 37402
			// (set) Token: 0x0600C191 RID: 49553 RVA: 0x001152BE File Offset: 0x001134BE
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x1700921B RID: 37403
			// (set) Token: 0x0600C192 RID: 49554 RVA: 0x001152D6 File Offset: 0x001134D6
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700921C RID: 37404
			// (set) Token: 0x0600C193 RID: 49555 RVA: 0x001152E9 File Offset: 0x001134E9
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700921D RID: 37405
			// (set) Token: 0x0600C194 RID: 49556 RVA: 0x001152FC File Offset: 0x001134FC
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700921E RID: 37406
			// (set) Token: 0x0600C195 RID: 49557 RVA: 0x0011530F File Offset: 0x0011350F
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700921F RID: 37407
			// (set) Token: 0x0600C196 RID: 49558 RVA: 0x00115322 File Offset: 0x00113522
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009220 RID: 37408
			// (set) Token: 0x0600C197 RID: 49559 RVA: 0x00115335 File Offset: 0x00113535
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009221 RID: 37409
			// (set) Token: 0x0600C198 RID: 49560 RVA: 0x00115348 File Offset: 0x00113548
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009222 RID: 37410
			// (set) Token: 0x0600C199 RID: 49561 RVA: 0x0011535B File Offset: 0x0011355B
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17009223 RID: 37411
			// (set) Token: 0x0600C19A RID: 49562 RVA: 0x00115373 File Offset: 0x00113573
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009224 RID: 37412
			// (set) Token: 0x0600C19B RID: 49563 RVA: 0x00115386 File Offset: 0x00113586
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009225 RID: 37413
			// (set) Token: 0x0600C19C RID: 49564 RVA: 0x0011539E File Offset: 0x0011359E
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009226 RID: 37414
			// (set) Token: 0x0600C19D RID: 49565 RVA: 0x001153B1 File Offset: 0x001135B1
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009227 RID: 37415
			// (set) Token: 0x0600C19E RID: 49566 RVA: 0x001153C4 File Offset: 0x001135C4
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009228 RID: 37416
			// (set) Token: 0x0600C19F RID: 49567 RVA: 0x001153D7 File Offset: 0x001135D7
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009229 RID: 37417
			// (set) Token: 0x0600C1A0 RID: 49568 RVA: 0x001153EA File Offset: 0x001135EA
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x1700922A RID: 37418
			// (set) Token: 0x0600C1A1 RID: 49569 RVA: 0x001153FD File Offset: 0x001135FD
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x1700922B RID: 37419
			// (set) Token: 0x0600C1A2 RID: 49570 RVA: 0x00115410 File Offset: 0x00113610
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x1700922C RID: 37420
			// (set) Token: 0x0600C1A3 RID: 49571 RVA: 0x00115423 File Offset: 0x00113623
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x1700922D RID: 37421
			// (set) Token: 0x0600C1A4 RID: 49572 RVA: 0x00115436 File Offset: 0x00113636
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x1700922E RID: 37422
			// (set) Token: 0x0600C1A5 RID: 49573 RVA: 0x00115449 File Offset: 0x00113649
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x1700922F RID: 37423
			// (set) Token: 0x0600C1A6 RID: 49574 RVA: 0x0011545C File Offset: 0x0011365C
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009230 RID: 37424
			// (set) Token: 0x0600C1A7 RID: 49575 RVA: 0x0011546F File Offset: 0x0011366F
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009231 RID: 37425
			// (set) Token: 0x0600C1A8 RID: 49576 RVA: 0x00115482 File Offset: 0x00113682
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009232 RID: 37426
			// (set) Token: 0x0600C1A9 RID: 49577 RVA: 0x00115495 File Offset: 0x00113695
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009233 RID: 37427
			// (set) Token: 0x0600C1AA RID: 49578 RVA: 0x001154A8 File Offset: 0x001136A8
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009234 RID: 37428
			// (set) Token: 0x0600C1AB RID: 49579 RVA: 0x001154BB File Offset: 0x001136BB
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009235 RID: 37429
			// (set) Token: 0x0600C1AC RID: 49580 RVA: 0x001154CE File Offset: 0x001136CE
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009236 RID: 37430
			// (set) Token: 0x0600C1AD RID: 49581 RVA: 0x001154E1 File Offset: 0x001136E1
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009237 RID: 37431
			// (set) Token: 0x0600C1AE RID: 49582 RVA: 0x001154F4 File Offset: 0x001136F4
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009238 RID: 37432
			// (set) Token: 0x0600C1AF RID: 49583 RVA: 0x00115507 File Offset: 0x00113707
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009239 RID: 37433
			// (set) Token: 0x0600C1B0 RID: 49584 RVA: 0x0011551A File Offset: 0x0011371A
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700923A RID: 37434
			// (set) Token: 0x0600C1B1 RID: 49585 RVA: 0x0011552D File Offset: 0x0011372D
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700923B RID: 37435
			// (set) Token: 0x0600C1B2 RID: 49586 RVA: 0x00115540 File Offset: 0x00113740
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700923C RID: 37436
			// (set) Token: 0x0600C1B3 RID: 49587 RVA: 0x00115553 File Offset: 0x00113753
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x1700923D RID: 37437
			// (set) Token: 0x0600C1B4 RID: 49588 RVA: 0x0011556B File Offset: 0x0011376B
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700923E RID: 37438
			// (set) Token: 0x0600C1B5 RID: 49589 RVA: 0x0011557E File Offset: 0x0011377E
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700923F RID: 37439
			// (set) Token: 0x0600C1B6 RID: 49590 RVA: 0x00115596 File Offset: 0x00113796
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17009240 RID: 37440
			// (set) Token: 0x0600C1B7 RID: 49591 RVA: 0x001155A9 File Offset: 0x001137A9
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009241 RID: 37441
			// (set) Token: 0x0600C1B8 RID: 49592 RVA: 0x001155C1 File Offset: 0x001137C1
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009242 RID: 37442
			// (set) Token: 0x0600C1B9 RID: 49593 RVA: 0x001155D9 File Offset: 0x001137D9
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17009243 RID: 37443
			// (set) Token: 0x0600C1BA RID: 49594 RVA: 0x001155F1 File Offset: 0x001137F1
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17009244 RID: 37444
			// (set) Token: 0x0600C1BB RID: 49595 RVA: 0x00115609 File Offset: 0x00113809
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17009245 RID: 37445
			// (set) Token: 0x0600C1BC RID: 49596 RVA: 0x00115621 File Offset: 0x00113821
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17009246 RID: 37446
			// (set) Token: 0x0600C1BD RID: 49597 RVA: 0x00115639 File Offset: 0x00113839
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009247 RID: 37447
			// (set) Token: 0x0600C1BE RID: 49598 RVA: 0x00115651 File Offset: 0x00113851
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17009248 RID: 37448
			// (set) Token: 0x0600C1BF RID: 49599 RVA: 0x00115669 File Offset: 0x00113869
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009249 RID: 37449
			// (set) Token: 0x0600C1C0 RID: 49600 RVA: 0x00115681 File Offset: 0x00113881
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x1700924A RID: 37450
			// (set) Token: 0x0600C1C1 RID: 49601 RVA: 0x00115694 File Offset: 0x00113894
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700924B RID: 37451
			// (set) Token: 0x0600C1C2 RID: 49602 RVA: 0x001156A7 File Offset: 0x001138A7
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700924C RID: 37452
			// (set) Token: 0x0600C1C3 RID: 49603 RVA: 0x001156BA File Offset: 0x001138BA
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700924D RID: 37453
			// (set) Token: 0x0600C1C4 RID: 49604 RVA: 0x001156CD File Offset: 0x001138CD
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x1700924E RID: 37454
			// (set) Token: 0x0600C1C5 RID: 49605 RVA: 0x001156E0 File Offset: 0x001138E0
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x1700924F RID: 37455
			// (set) Token: 0x0600C1C6 RID: 49606 RVA: 0x001156F3 File Offset: 0x001138F3
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009250 RID: 37456
			// (set) Token: 0x0600C1C7 RID: 49607 RVA: 0x0011570B File Offset: 0x0011390B
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009251 RID: 37457
			// (set) Token: 0x0600C1C8 RID: 49608 RVA: 0x0011571E File Offset: 0x0011391E
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009252 RID: 37458
			// (set) Token: 0x0600C1C9 RID: 49609 RVA: 0x00115731 File Offset: 0x00113931
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009253 RID: 37459
			// (set) Token: 0x0600C1CA RID: 49610 RVA: 0x00115744 File Offset: 0x00113944
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009254 RID: 37460
			// (set) Token: 0x0600C1CB RID: 49611 RVA: 0x00115762 File Offset: 0x00113962
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009255 RID: 37461
			// (set) Token: 0x0600C1CC RID: 49612 RVA: 0x00115775 File Offset: 0x00113975
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009256 RID: 37462
			// (set) Token: 0x0600C1CD RID: 49613 RVA: 0x00115788 File Offset: 0x00113988
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009257 RID: 37463
			// (set) Token: 0x0600C1CE RID: 49614 RVA: 0x0011579B File Offset: 0x0011399B
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009258 RID: 37464
			// (set) Token: 0x0600C1CF RID: 49615 RVA: 0x001157AE File Offset: 0x001139AE
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009259 RID: 37465
			// (set) Token: 0x0600C1D0 RID: 49616 RVA: 0x001157C1 File Offset: 0x001139C1
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700925A RID: 37466
			// (set) Token: 0x0600C1D1 RID: 49617 RVA: 0x001157D4 File Offset: 0x001139D4
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700925B RID: 37467
			// (set) Token: 0x0600C1D2 RID: 49618 RVA: 0x001157E7 File Offset: 0x001139E7
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700925C RID: 37468
			// (set) Token: 0x0600C1D3 RID: 49619 RVA: 0x001157FA File Offset: 0x001139FA
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700925D RID: 37469
			// (set) Token: 0x0600C1D4 RID: 49620 RVA: 0x0011580D File Offset: 0x00113A0D
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700925E RID: 37470
			// (set) Token: 0x0600C1D5 RID: 49621 RVA: 0x00115820 File Offset: 0x00113A20
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700925F RID: 37471
			// (set) Token: 0x0600C1D6 RID: 49622 RVA: 0x00115833 File Offset: 0x00113A33
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009260 RID: 37472
			// (set) Token: 0x0600C1D7 RID: 49623 RVA: 0x00115846 File Offset: 0x00113A46
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009261 RID: 37473
			// (set) Token: 0x0600C1D8 RID: 49624 RVA: 0x00115859 File Offset: 0x00113A59
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009262 RID: 37474
			// (set) Token: 0x0600C1D9 RID: 49625 RVA: 0x00115877 File Offset: 0x00113A77
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009263 RID: 37475
			// (set) Token: 0x0600C1DA RID: 49626 RVA: 0x0011588F File Offset: 0x00113A8F
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009264 RID: 37476
			// (set) Token: 0x0600C1DB RID: 49627 RVA: 0x001158A7 File Offset: 0x00113AA7
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009265 RID: 37477
			// (set) Token: 0x0600C1DC RID: 49628 RVA: 0x001158BF File Offset: 0x00113ABF
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009266 RID: 37478
			// (set) Token: 0x0600C1DD RID: 49629 RVA: 0x001158D2 File Offset: 0x00113AD2
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009267 RID: 37479
			// (set) Token: 0x0600C1DE RID: 49630 RVA: 0x001158E5 File Offset: 0x00113AE5
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009268 RID: 37480
			// (set) Token: 0x0600C1DF RID: 49631 RVA: 0x001158FD File Offset: 0x00113AFD
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009269 RID: 37481
			// (set) Token: 0x0600C1E0 RID: 49632 RVA: 0x00115910 File Offset: 0x00113B10
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x1700926A RID: 37482
			// (set) Token: 0x0600C1E1 RID: 49633 RVA: 0x00115928 File Offset: 0x00113B28
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700926B RID: 37483
			// (set) Token: 0x0600C1E2 RID: 49634 RVA: 0x00115940 File Offset: 0x00113B40
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x1700926C RID: 37484
			// (set) Token: 0x0600C1E3 RID: 49635 RVA: 0x00115953 File Offset: 0x00113B53
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x1700926D RID: 37485
			// (set) Token: 0x0600C1E4 RID: 49636 RVA: 0x00115966 File Offset: 0x00113B66
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700926E RID: 37486
			// (set) Token: 0x0600C1E5 RID: 49637 RVA: 0x00115979 File Offset: 0x00113B79
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700926F RID: 37487
			// (set) Token: 0x0600C1E6 RID: 49638 RVA: 0x0011598C File Offset: 0x00113B8C
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009270 RID: 37488
			// (set) Token: 0x0600C1E7 RID: 49639 RVA: 0x001159A4 File Offset: 0x00113BA4
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009271 RID: 37489
			// (set) Token: 0x0600C1E8 RID: 49640 RVA: 0x001159B7 File Offset: 0x00113BB7
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009272 RID: 37490
			// (set) Token: 0x0600C1E9 RID: 49641 RVA: 0x001159CA File Offset: 0x00113BCA
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009273 RID: 37491
			// (set) Token: 0x0600C1EA RID: 49642 RVA: 0x001159E2 File Offset: 0x00113BE2
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009274 RID: 37492
			// (set) Token: 0x0600C1EB RID: 49643 RVA: 0x001159FA File Offset: 0x00113BFA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009275 RID: 37493
			// (set) Token: 0x0600C1EC RID: 49644 RVA: 0x00115A0D File Offset: 0x00113C0D
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17009276 RID: 37494
			// (set) Token: 0x0600C1ED RID: 49645 RVA: 0x00115A25 File Offset: 0x00113C25
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009277 RID: 37495
			// (set) Token: 0x0600C1EE RID: 49646 RVA: 0x00115A38 File Offset: 0x00113C38
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009278 RID: 37496
			// (set) Token: 0x0600C1EF RID: 49647 RVA: 0x00115A4B File Offset: 0x00113C4B
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17009279 RID: 37497
			// (set) Token: 0x0600C1F0 RID: 49648 RVA: 0x00115A5E File Offset: 0x00113C5E
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700927A RID: 37498
			// (set) Token: 0x0600C1F1 RID: 49649 RVA: 0x00115A7C File Offset: 0x00113C7C
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700927B RID: 37499
			// (set) Token: 0x0600C1F2 RID: 49650 RVA: 0x00115A9A File Offset: 0x00113C9A
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700927C RID: 37500
			// (set) Token: 0x0600C1F3 RID: 49651 RVA: 0x00115AB8 File Offset: 0x00113CB8
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700927D RID: 37501
			// (set) Token: 0x0600C1F4 RID: 49652 RVA: 0x00115AD6 File Offset: 0x00113CD6
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700927E RID: 37502
			// (set) Token: 0x0600C1F5 RID: 49653 RVA: 0x00115AE9 File Offset: 0x00113CE9
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700927F RID: 37503
			// (set) Token: 0x0600C1F6 RID: 49654 RVA: 0x00115B01 File Offset: 0x00113D01
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009280 RID: 37504
			// (set) Token: 0x0600C1F7 RID: 49655 RVA: 0x00115B1F File Offset: 0x00113D1F
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17009281 RID: 37505
			// (set) Token: 0x0600C1F8 RID: 49656 RVA: 0x00115B37 File Offset: 0x00113D37
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17009282 RID: 37506
			// (set) Token: 0x0600C1F9 RID: 49657 RVA: 0x00115B4F File Offset: 0x00113D4F
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17009283 RID: 37507
			// (set) Token: 0x0600C1FA RID: 49658 RVA: 0x00115B62 File Offset: 0x00113D62
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17009284 RID: 37508
			// (set) Token: 0x0600C1FB RID: 49659 RVA: 0x00115B7A File Offset: 0x00113D7A
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009285 RID: 37509
			// (set) Token: 0x0600C1FC RID: 49660 RVA: 0x00115B8D File Offset: 0x00113D8D
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009286 RID: 37510
			// (set) Token: 0x0600C1FD RID: 49661 RVA: 0x00115BA0 File Offset: 0x00113DA0
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009287 RID: 37511
			// (set) Token: 0x0600C1FE RID: 49662 RVA: 0x00115BB3 File Offset: 0x00113DB3
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009288 RID: 37512
			// (set) Token: 0x0600C1FF RID: 49663 RVA: 0x00115BC6 File Offset: 0x00113DC6
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009289 RID: 37513
			// (set) Token: 0x0600C200 RID: 49664 RVA: 0x00115BDE File Offset: 0x00113DDE
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700928A RID: 37514
			// (set) Token: 0x0600C201 RID: 49665 RVA: 0x00115BF1 File Offset: 0x00113DF1
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700928B RID: 37515
			// (set) Token: 0x0600C202 RID: 49666 RVA: 0x00115C04 File Offset: 0x00113E04
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700928C RID: 37516
			// (set) Token: 0x0600C203 RID: 49667 RVA: 0x00115C1C File Offset: 0x00113E1C
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700928D RID: 37517
			// (set) Token: 0x0600C204 RID: 49668 RVA: 0x00115C2F File Offset: 0x00113E2F
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700928E RID: 37518
			// (set) Token: 0x0600C205 RID: 49669 RVA: 0x00115C47 File Offset: 0x00113E47
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700928F RID: 37519
			// (set) Token: 0x0600C206 RID: 49670 RVA: 0x00115C5A File Offset: 0x00113E5A
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009290 RID: 37520
			// (set) Token: 0x0600C207 RID: 49671 RVA: 0x00115C78 File Offset: 0x00113E78
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009291 RID: 37521
			// (set) Token: 0x0600C208 RID: 49672 RVA: 0x00115C8B File Offset: 0x00113E8B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009292 RID: 37522
			// (set) Token: 0x0600C209 RID: 49673 RVA: 0x00115CA9 File Offset: 0x00113EA9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009293 RID: 37523
			// (set) Token: 0x0600C20A RID: 49674 RVA: 0x00115CBC File Offset: 0x00113EBC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009294 RID: 37524
			// (set) Token: 0x0600C20B RID: 49675 RVA: 0x00115CD4 File Offset: 0x00113ED4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009295 RID: 37525
			// (set) Token: 0x0600C20C RID: 49676 RVA: 0x00115CEC File Offset: 0x00113EEC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009296 RID: 37526
			// (set) Token: 0x0600C20D RID: 49677 RVA: 0x00115D04 File Offset: 0x00113F04
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009297 RID: 37527
			// (set) Token: 0x0600C20E RID: 49678 RVA: 0x00115D1C File Offset: 0x00113F1C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D94 RID: 3476
		public class AuxMailboxParameters : ParametersBase
		{
			// Token: 0x17009298 RID: 37528
			// (set) Token: 0x0600C210 RID: 49680 RVA: 0x00115D3C File Offset: 0x00113F3C
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17009299 RID: 37529
			// (set) Token: 0x0600C211 RID: 49681 RVA: 0x00115D4F File Offset: 0x00113F4F
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700929A RID: 37530
			// (set) Token: 0x0600C212 RID: 49682 RVA: 0x00115D62 File Offset: 0x00113F62
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x1700929B RID: 37531
			// (set) Token: 0x0600C213 RID: 49683 RVA: 0x00115D7A File Offset: 0x00113F7A
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x1700929C RID: 37532
			// (set) Token: 0x0600C214 RID: 49684 RVA: 0x00115D92 File Offset: 0x00113F92
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700929D RID: 37533
			// (set) Token: 0x0600C215 RID: 49685 RVA: 0x00115DA5 File Offset: 0x00113FA5
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700929E RID: 37534
			// (set) Token: 0x0600C216 RID: 49686 RVA: 0x00115DB8 File Offset: 0x00113FB8
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700929F RID: 37535
			// (set) Token: 0x0600C217 RID: 49687 RVA: 0x00115DCB File Offset: 0x00113FCB
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x170092A0 RID: 37536
			// (set) Token: 0x0600C218 RID: 49688 RVA: 0x00115DDE File Offset: 0x00113FDE
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170092A1 RID: 37537
			// (set) Token: 0x0600C219 RID: 49689 RVA: 0x00115DF1 File Offset: 0x00113FF1
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170092A2 RID: 37538
			// (set) Token: 0x0600C21A RID: 49690 RVA: 0x00115E04 File Offset: 0x00114004
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x170092A3 RID: 37539
			// (set) Token: 0x0600C21B RID: 49691 RVA: 0x00115E17 File Offset: 0x00114017
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x170092A4 RID: 37540
			// (set) Token: 0x0600C21C RID: 49692 RVA: 0x00115E2F File Offset: 0x0011402F
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x170092A5 RID: 37541
			// (set) Token: 0x0600C21D RID: 49693 RVA: 0x00115E42 File Offset: 0x00114042
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x170092A6 RID: 37542
			// (set) Token: 0x0600C21E RID: 49694 RVA: 0x00115E5A File Offset: 0x0011405A
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170092A7 RID: 37543
			// (set) Token: 0x0600C21F RID: 49695 RVA: 0x00115E6D File Offset: 0x0011406D
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170092A8 RID: 37544
			// (set) Token: 0x0600C220 RID: 49696 RVA: 0x00115E80 File Offset: 0x00114080
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170092A9 RID: 37545
			// (set) Token: 0x0600C221 RID: 49697 RVA: 0x00115E93 File Offset: 0x00114093
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170092AA RID: 37546
			// (set) Token: 0x0600C222 RID: 49698 RVA: 0x00115EA6 File Offset: 0x001140A6
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170092AB RID: 37547
			// (set) Token: 0x0600C223 RID: 49699 RVA: 0x00115EB9 File Offset: 0x001140B9
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170092AC RID: 37548
			// (set) Token: 0x0600C224 RID: 49700 RVA: 0x00115ECC File Offset: 0x001140CC
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170092AD RID: 37549
			// (set) Token: 0x0600C225 RID: 49701 RVA: 0x00115EDF File Offset: 0x001140DF
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170092AE RID: 37550
			// (set) Token: 0x0600C226 RID: 49702 RVA: 0x00115EF2 File Offset: 0x001140F2
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170092AF RID: 37551
			// (set) Token: 0x0600C227 RID: 49703 RVA: 0x00115F05 File Offset: 0x00114105
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170092B0 RID: 37552
			// (set) Token: 0x0600C228 RID: 49704 RVA: 0x00115F18 File Offset: 0x00114118
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170092B1 RID: 37553
			// (set) Token: 0x0600C229 RID: 49705 RVA: 0x00115F2B File Offset: 0x0011412B
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170092B2 RID: 37554
			// (set) Token: 0x0600C22A RID: 49706 RVA: 0x00115F3E File Offset: 0x0011413E
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170092B3 RID: 37555
			// (set) Token: 0x0600C22B RID: 49707 RVA: 0x00115F51 File Offset: 0x00114151
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170092B4 RID: 37556
			// (set) Token: 0x0600C22C RID: 49708 RVA: 0x00115F64 File Offset: 0x00114164
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170092B5 RID: 37557
			// (set) Token: 0x0600C22D RID: 49709 RVA: 0x00115F77 File Offset: 0x00114177
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170092B6 RID: 37558
			// (set) Token: 0x0600C22E RID: 49710 RVA: 0x00115F8A File Offset: 0x0011418A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170092B7 RID: 37559
			// (set) Token: 0x0600C22F RID: 49711 RVA: 0x00115F9D File Offset: 0x0011419D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170092B8 RID: 37560
			// (set) Token: 0x0600C230 RID: 49712 RVA: 0x00115FB0 File Offset: 0x001141B0
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170092B9 RID: 37561
			// (set) Token: 0x0600C231 RID: 49713 RVA: 0x00115FC3 File Offset: 0x001141C3
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170092BA RID: 37562
			// (set) Token: 0x0600C232 RID: 49714 RVA: 0x00115FD6 File Offset: 0x001141D6
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170092BB RID: 37563
			// (set) Token: 0x0600C233 RID: 49715 RVA: 0x00115FE9 File Offset: 0x001141E9
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170092BC RID: 37564
			// (set) Token: 0x0600C234 RID: 49716 RVA: 0x00115FFC File Offset: 0x001141FC
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170092BD RID: 37565
			// (set) Token: 0x0600C235 RID: 49717 RVA: 0x0011600F File Offset: 0x0011420F
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170092BE RID: 37566
			// (set) Token: 0x0600C236 RID: 49718 RVA: 0x00116027 File Offset: 0x00114227
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x170092BF RID: 37567
			// (set) Token: 0x0600C237 RID: 49719 RVA: 0x0011603A File Offset: 0x0011423A
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x170092C0 RID: 37568
			// (set) Token: 0x0600C238 RID: 49720 RVA: 0x00116052 File Offset: 0x00114252
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x170092C1 RID: 37569
			// (set) Token: 0x0600C239 RID: 49721 RVA: 0x00116065 File Offset: 0x00114265
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x170092C2 RID: 37570
			// (set) Token: 0x0600C23A RID: 49722 RVA: 0x0011607D File Offset: 0x0011427D
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x170092C3 RID: 37571
			// (set) Token: 0x0600C23B RID: 49723 RVA: 0x00116095 File Offset: 0x00114295
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x170092C4 RID: 37572
			// (set) Token: 0x0600C23C RID: 49724 RVA: 0x001160AD File Offset: 0x001142AD
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x170092C5 RID: 37573
			// (set) Token: 0x0600C23D RID: 49725 RVA: 0x001160C5 File Offset: 0x001142C5
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170092C6 RID: 37574
			// (set) Token: 0x0600C23E RID: 49726 RVA: 0x001160DD File Offset: 0x001142DD
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x170092C7 RID: 37575
			// (set) Token: 0x0600C23F RID: 49727 RVA: 0x001160F5 File Offset: 0x001142F5
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x170092C8 RID: 37576
			// (set) Token: 0x0600C240 RID: 49728 RVA: 0x0011610D File Offset: 0x0011430D
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x170092C9 RID: 37577
			// (set) Token: 0x0600C241 RID: 49729 RVA: 0x00116125 File Offset: 0x00114325
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x170092CA RID: 37578
			// (set) Token: 0x0600C242 RID: 49730 RVA: 0x0011613D File Offset: 0x0011433D
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x170092CB RID: 37579
			// (set) Token: 0x0600C243 RID: 49731 RVA: 0x00116150 File Offset: 0x00114350
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x170092CC RID: 37580
			// (set) Token: 0x0600C244 RID: 49732 RVA: 0x00116163 File Offset: 0x00114363
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x170092CD RID: 37581
			// (set) Token: 0x0600C245 RID: 49733 RVA: 0x00116176 File Offset: 0x00114376
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x170092CE RID: 37582
			// (set) Token: 0x0600C246 RID: 49734 RVA: 0x00116189 File Offset: 0x00114389
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x170092CF RID: 37583
			// (set) Token: 0x0600C247 RID: 49735 RVA: 0x0011619C File Offset: 0x0011439C
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x170092D0 RID: 37584
			// (set) Token: 0x0600C248 RID: 49736 RVA: 0x001161AF File Offset: 0x001143AF
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x170092D1 RID: 37585
			// (set) Token: 0x0600C249 RID: 49737 RVA: 0x001161C7 File Offset: 0x001143C7
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x170092D2 RID: 37586
			// (set) Token: 0x0600C24A RID: 49738 RVA: 0x001161DA File Offset: 0x001143DA
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x170092D3 RID: 37587
			// (set) Token: 0x0600C24B RID: 49739 RVA: 0x001161ED File Offset: 0x001143ED
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x170092D4 RID: 37588
			// (set) Token: 0x0600C24C RID: 49740 RVA: 0x00116200 File Offset: 0x00114400
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x170092D5 RID: 37589
			// (set) Token: 0x0600C24D RID: 49741 RVA: 0x0011621E File Offset: 0x0011441E
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x170092D6 RID: 37590
			// (set) Token: 0x0600C24E RID: 49742 RVA: 0x00116231 File Offset: 0x00114431
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x170092D7 RID: 37591
			// (set) Token: 0x0600C24F RID: 49743 RVA: 0x00116244 File Offset: 0x00114444
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x170092D8 RID: 37592
			// (set) Token: 0x0600C250 RID: 49744 RVA: 0x00116257 File Offset: 0x00114457
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x170092D9 RID: 37593
			// (set) Token: 0x0600C251 RID: 49745 RVA: 0x0011626A File Offset: 0x0011446A
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x170092DA RID: 37594
			// (set) Token: 0x0600C252 RID: 49746 RVA: 0x0011627D File Offset: 0x0011447D
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x170092DB RID: 37595
			// (set) Token: 0x0600C253 RID: 49747 RVA: 0x00116290 File Offset: 0x00114490
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x170092DC RID: 37596
			// (set) Token: 0x0600C254 RID: 49748 RVA: 0x001162A3 File Offset: 0x001144A3
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x170092DD RID: 37597
			// (set) Token: 0x0600C255 RID: 49749 RVA: 0x001162B6 File Offset: 0x001144B6
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x170092DE RID: 37598
			// (set) Token: 0x0600C256 RID: 49750 RVA: 0x001162C9 File Offset: 0x001144C9
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x170092DF RID: 37599
			// (set) Token: 0x0600C257 RID: 49751 RVA: 0x001162DC File Offset: 0x001144DC
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x170092E0 RID: 37600
			// (set) Token: 0x0600C258 RID: 49752 RVA: 0x001162EF File Offset: 0x001144EF
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x170092E1 RID: 37601
			// (set) Token: 0x0600C259 RID: 49753 RVA: 0x00116302 File Offset: 0x00114502
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x170092E2 RID: 37602
			// (set) Token: 0x0600C25A RID: 49754 RVA: 0x00116315 File Offset: 0x00114515
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170092E3 RID: 37603
			// (set) Token: 0x0600C25B RID: 49755 RVA: 0x00116333 File Offset: 0x00114533
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x170092E4 RID: 37604
			// (set) Token: 0x0600C25C RID: 49756 RVA: 0x0011634B File Offset: 0x0011454B
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170092E5 RID: 37605
			// (set) Token: 0x0600C25D RID: 49757 RVA: 0x00116363 File Offset: 0x00114563
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x170092E6 RID: 37606
			// (set) Token: 0x0600C25E RID: 49758 RVA: 0x0011637B File Offset: 0x0011457B
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x170092E7 RID: 37607
			// (set) Token: 0x0600C25F RID: 49759 RVA: 0x0011638E File Offset: 0x0011458E
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x170092E8 RID: 37608
			// (set) Token: 0x0600C260 RID: 49760 RVA: 0x001163A1 File Offset: 0x001145A1
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x170092E9 RID: 37609
			// (set) Token: 0x0600C261 RID: 49761 RVA: 0x001163B9 File Offset: 0x001145B9
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x170092EA RID: 37610
			// (set) Token: 0x0600C262 RID: 49762 RVA: 0x001163CC File Offset: 0x001145CC
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x170092EB RID: 37611
			// (set) Token: 0x0600C263 RID: 49763 RVA: 0x001163E4 File Offset: 0x001145E4
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x170092EC RID: 37612
			// (set) Token: 0x0600C264 RID: 49764 RVA: 0x001163FC File Offset: 0x001145FC
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170092ED RID: 37613
			// (set) Token: 0x0600C265 RID: 49765 RVA: 0x0011640F File Offset: 0x0011460F
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170092EE RID: 37614
			// (set) Token: 0x0600C266 RID: 49766 RVA: 0x00116422 File Offset: 0x00114622
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x170092EF RID: 37615
			// (set) Token: 0x0600C267 RID: 49767 RVA: 0x00116435 File Offset: 0x00114635
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x170092F0 RID: 37616
			// (set) Token: 0x0600C268 RID: 49768 RVA: 0x00116448 File Offset: 0x00114648
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x170092F1 RID: 37617
			// (set) Token: 0x0600C269 RID: 49769 RVA: 0x00116460 File Offset: 0x00114660
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x170092F2 RID: 37618
			// (set) Token: 0x0600C26A RID: 49770 RVA: 0x00116473 File Offset: 0x00114673
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x170092F3 RID: 37619
			// (set) Token: 0x0600C26B RID: 49771 RVA: 0x00116486 File Offset: 0x00114686
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x170092F4 RID: 37620
			// (set) Token: 0x0600C26C RID: 49772 RVA: 0x0011649E File Offset: 0x0011469E
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x170092F5 RID: 37621
			// (set) Token: 0x0600C26D RID: 49773 RVA: 0x001164B6 File Offset: 0x001146B6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170092F6 RID: 37622
			// (set) Token: 0x0600C26E RID: 49774 RVA: 0x001164C9 File Offset: 0x001146C9
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170092F7 RID: 37623
			// (set) Token: 0x0600C26F RID: 49775 RVA: 0x001164E1 File Offset: 0x001146E1
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170092F8 RID: 37624
			// (set) Token: 0x0600C270 RID: 49776 RVA: 0x001164F4 File Offset: 0x001146F4
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170092F9 RID: 37625
			// (set) Token: 0x0600C271 RID: 49777 RVA: 0x00116507 File Offset: 0x00114707
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170092FA RID: 37626
			// (set) Token: 0x0600C272 RID: 49778 RVA: 0x0011651A File Offset: 0x0011471A
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170092FB RID: 37627
			// (set) Token: 0x0600C273 RID: 49779 RVA: 0x00116538 File Offset: 0x00114738
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170092FC RID: 37628
			// (set) Token: 0x0600C274 RID: 49780 RVA: 0x00116556 File Offset: 0x00114756
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170092FD RID: 37629
			// (set) Token: 0x0600C275 RID: 49781 RVA: 0x00116574 File Offset: 0x00114774
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170092FE RID: 37630
			// (set) Token: 0x0600C276 RID: 49782 RVA: 0x00116592 File Offset: 0x00114792
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170092FF RID: 37631
			// (set) Token: 0x0600C277 RID: 49783 RVA: 0x001165A5 File Offset: 0x001147A5
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009300 RID: 37632
			// (set) Token: 0x0600C278 RID: 49784 RVA: 0x001165BD File Offset: 0x001147BD
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009301 RID: 37633
			// (set) Token: 0x0600C279 RID: 49785 RVA: 0x001165DB File Offset: 0x001147DB
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17009302 RID: 37634
			// (set) Token: 0x0600C27A RID: 49786 RVA: 0x001165F3 File Offset: 0x001147F3
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17009303 RID: 37635
			// (set) Token: 0x0600C27B RID: 49787 RVA: 0x0011660B File Offset: 0x0011480B
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17009304 RID: 37636
			// (set) Token: 0x0600C27C RID: 49788 RVA: 0x0011661E File Offset: 0x0011481E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17009305 RID: 37637
			// (set) Token: 0x0600C27D RID: 49789 RVA: 0x00116636 File Offset: 0x00114836
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009306 RID: 37638
			// (set) Token: 0x0600C27E RID: 49790 RVA: 0x00116649 File Offset: 0x00114849
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009307 RID: 37639
			// (set) Token: 0x0600C27F RID: 49791 RVA: 0x0011665C File Offset: 0x0011485C
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009308 RID: 37640
			// (set) Token: 0x0600C280 RID: 49792 RVA: 0x0011666F File Offset: 0x0011486F
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009309 RID: 37641
			// (set) Token: 0x0600C281 RID: 49793 RVA: 0x00116682 File Offset: 0x00114882
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700930A RID: 37642
			// (set) Token: 0x0600C282 RID: 49794 RVA: 0x0011669A File Offset: 0x0011489A
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700930B RID: 37643
			// (set) Token: 0x0600C283 RID: 49795 RVA: 0x001166AD File Offset: 0x001148AD
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700930C RID: 37644
			// (set) Token: 0x0600C284 RID: 49796 RVA: 0x001166C0 File Offset: 0x001148C0
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700930D RID: 37645
			// (set) Token: 0x0600C285 RID: 49797 RVA: 0x001166D8 File Offset: 0x001148D8
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700930E RID: 37646
			// (set) Token: 0x0600C286 RID: 49798 RVA: 0x001166EB File Offset: 0x001148EB
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700930F RID: 37647
			// (set) Token: 0x0600C287 RID: 49799 RVA: 0x00116703 File Offset: 0x00114903
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009310 RID: 37648
			// (set) Token: 0x0600C288 RID: 49800 RVA: 0x00116716 File Offset: 0x00114916
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009311 RID: 37649
			// (set) Token: 0x0600C289 RID: 49801 RVA: 0x00116734 File Offset: 0x00114934
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009312 RID: 37650
			// (set) Token: 0x0600C28A RID: 49802 RVA: 0x00116747 File Offset: 0x00114947
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009313 RID: 37651
			// (set) Token: 0x0600C28B RID: 49803 RVA: 0x00116765 File Offset: 0x00114965
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009314 RID: 37652
			// (set) Token: 0x0600C28C RID: 49804 RVA: 0x00116778 File Offset: 0x00114978
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009315 RID: 37653
			// (set) Token: 0x0600C28D RID: 49805 RVA: 0x00116790 File Offset: 0x00114990
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009316 RID: 37654
			// (set) Token: 0x0600C28E RID: 49806 RVA: 0x001167A8 File Offset: 0x001149A8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009317 RID: 37655
			// (set) Token: 0x0600C28F RID: 49807 RVA: 0x001167C0 File Offset: 0x001149C0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009318 RID: 37656
			// (set) Token: 0x0600C290 RID: 49808 RVA: 0x001167D8 File Offset: 0x001149D8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D95 RID: 3477
		public class TeamMailboxITProParameters : ParametersBase
		{
			// Token: 0x17009319 RID: 37657
			// (set) Token: 0x0600C292 RID: 49810 RVA: 0x001167F8 File Offset: 0x001149F8
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700931A RID: 37658
			// (set) Token: 0x0600C293 RID: 49811 RVA: 0x0011680B File Offset: 0x00114A0B
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700931B RID: 37659
			// (set) Token: 0x0600C294 RID: 49812 RVA: 0x0011681E File Offset: 0x00114A1E
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700931C RID: 37660
			// (set) Token: 0x0600C295 RID: 49813 RVA: 0x0011683C File Offset: 0x00114A3C
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700931D RID: 37661
			// (set) Token: 0x0600C296 RID: 49814 RVA: 0x0011684F File Offset: 0x00114A4F
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700931E RID: 37662
			// (set) Token: 0x0600C297 RID: 49815 RVA: 0x00116867 File Offset: 0x00114A67
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700931F RID: 37663
			// (set) Token: 0x0600C298 RID: 49816 RVA: 0x0011687F File Offset: 0x00114A7F
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009320 RID: 37664
			// (set) Token: 0x0600C299 RID: 49817 RVA: 0x00116897 File Offset: 0x00114A97
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009321 RID: 37665
			// (set) Token: 0x0600C29A RID: 49818 RVA: 0x001168AA File Offset: 0x00114AAA
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009322 RID: 37666
			// (set) Token: 0x0600C29B RID: 49819 RVA: 0x001168BD File Offset: 0x00114ABD
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009323 RID: 37667
			// (set) Token: 0x0600C29C RID: 49820 RVA: 0x001168D0 File Offset: 0x00114AD0
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009324 RID: 37668
			// (set) Token: 0x0600C29D RID: 49821 RVA: 0x001168E3 File Offset: 0x00114AE3
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009325 RID: 37669
			// (set) Token: 0x0600C29E RID: 49822 RVA: 0x001168F6 File Offset: 0x00114AF6
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009326 RID: 37670
			// (set) Token: 0x0600C29F RID: 49823 RVA: 0x00116909 File Offset: 0x00114B09
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009327 RID: 37671
			// (set) Token: 0x0600C2A0 RID: 49824 RVA: 0x0011691C File Offset: 0x00114B1C
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17009328 RID: 37672
			// (set) Token: 0x0600C2A1 RID: 49825 RVA: 0x00116934 File Offset: 0x00114B34
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009329 RID: 37673
			// (set) Token: 0x0600C2A2 RID: 49826 RVA: 0x00116947 File Offset: 0x00114B47
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x1700932A RID: 37674
			// (set) Token: 0x0600C2A3 RID: 49827 RVA: 0x0011695F File Offset: 0x00114B5F
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700932B RID: 37675
			// (set) Token: 0x0600C2A4 RID: 49828 RVA: 0x00116972 File Offset: 0x00114B72
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700932C RID: 37676
			// (set) Token: 0x0600C2A5 RID: 49829 RVA: 0x00116985 File Offset: 0x00114B85
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700932D RID: 37677
			// (set) Token: 0x0600C2A6 RID: 49830 RVA: 0x00116998 File Offset: 0x00114B98
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700932E RID: 37678
			// (set) Token: 0x0600C2A7 RID: 49831 RVA: 0x001169AB File Offset: 0x00114BAB
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x1700932F RID: 37679
			// (set) Token: 0x0600C2A8 RID: 49832 RVA: 0x001169BE File Offset: 0x00114BBE
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009330 RID: 37680
			// (set) Token: 0x0600C2A9 RID: 49833 RVA: 0x001169D1 File Offset: 0x00114BD1
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009331 RID: 37681
			// (set) Token: 0x0600C2AA RID: 49834 RVA: 0x001169E4 File Offset: 0x00114BE4
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009332 RID: 37682
			// (set) Token: 0x0600C2AB RID: 49835 RVA: 0x001169F7 File Offset: 0x00114BF7
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009333 RID: 37683
			// (set) Token: 0x0600C2AC RID: 49836 RVA: 0x00116A0A File Offset: 0x00114C0A
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009334 RID: 37684
			// (set) Token: 0x0600C2AD RID: 49837 RVA: 0x00116A1D File Offset: 0x00114C1D
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009335 RID: 37685
			// (set) Token: 0x0600C2AE RID: 49838 RVA: 0x00116A30 File Offset: 0x00114C30
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009336 RID: 37686
			// (set) Token: 0x0600C2AF RID: 49839 RVA: 0x00116A43 File Offset: 0x00114C43
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009337 RID: 37687
			// (set) Token: 0x0600C2B0 RID: 49840 RVA: 0x00116A56 File Offset: 0x00114C56
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009338 RID: 37688
			// (set) Token: 0x0600C2B1 RID: 49841 RVA: 0x00116A69 File Offset: 0x00114C69
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009339 RID: 37689
			// (set) Token: 0x0600C2B2 RID: 49842 RVA: 0x00116A7C File Offset: 0x00114C7C
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700933A RID: 37690
			// (set) Token: 0x0600C2B3 RID: 49843 RVA: 0x00116A8F File Offset: 0x00114C8F
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700933B RID: 37691
			// (set) Token: 0x0600C2B4 RID: 49844 RVA: 0x00116AA2 File Offset: 0x00114CA2
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700933C RID: 37692
			// (set) Token: 0x0600C2B5 RID: 49845 RVA: 0x00116AB5 File Offset: 0x00114CB5
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700933D RID: 37693
			// (set) Token: 0x0600C2B6 RID: 49846 RVA: 0x00116AC8 File Offset: 0x00114CC8
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700933E RID: 37694
			// (set) Token: 0x0600C2B7 RID: 49847 RVA: 0x00116ADB File Offset: 0x00114CDB
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700933F RID: 37695
			// (set) Token: 0x0600C2B8 RID: 49848 RVA: 0x00116AEE File Offset: 0x00114CEE
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009340 RID: 37696
			// (set) Token: 0x0600C2B9 RID: 49849 RVA: 0x00116B01 File Offset: 0x00114D01
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009341 RID: 37697
			// (set) Token: 0x0600C2BA RID: 49850 RVA: 0x00116B14 File Offset: 0x00114D14
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009342 RID: 37698
			// (set) Token: 0x0600C2BB RID: 49851 RVA: 0x00116B2C File Offset: 0x00114D2C
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009343 RID: 37699
			// (set) Token: 0x0600C2BC RID: 49852 RVA: 0x00116B3F File Offset: 0x00114D3F
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009344 RID: 37700
			// (set) Token: 0x0600C2BD RID: 49853 RVA: 0x00116B57 File Offset: 0x00114D57
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17009345 RID: 37701
			// (set) Token: 0x0600C2BE RID: 49854 RVA: 0x00116B6A File Offset: 0x00114D6A
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009346 RID: 37702
			// (set) Token: 0x0600C2BF RID: 49855 RVA: 0x00116B82 File Offset: 0x00114D82
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009347 RID: 37703
			// (set) Token: 0x0600C2C0 RID: 49856 RVA: 0x00116B9A File Offset: 0x00114D9A
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17009348 RID: 37704
			// (set) Token: 0x0600C2C1 RID: 49857 RVA: 0x00116BB2 File Offset: 0x00114DB2
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17009349 RID: 37705
			// (set) Token: 0x0600C2C2 RID: 49858 RVA: 0x00116BCA File Offset: 0x00114DCA
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x1700934A RID: 37706
			// (set) Token: 0x0600C2C3 RID: 49859 RVA: 0x00116BE2 File Offset: 0x00114DE2
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x1700934B RID: 37707
			// (set) Token: 0x0600C2C4 RID: 49860 RVA: 0x00116BFA File Offset: 0x00114DFA
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x1700934C RID: 37708
			// (set) Token: 0x0600C2C5 RID: 49861 RVA: 0x00116C12 File Offset: 0x00114E12
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x1700934D RID: 37709
			// (set) Token: 0x0600C2C6 RID: 49862 RVA: 0x00116C2A File Offset: 0x00114E2A
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x1700934E RID: 37710
			// (set) Token: 0x0600C2C7 RID: 49863 RVA: 0x00116C42 File Offset: 0x00114E42
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x1700934F RID: 37711
			// (set) Token: 0x0600C2C8 RID: 49864 RVA: 0x00116C55 File Offset: 0x00114E55
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009350 RID: 37712
			// (set) Token: 0x0600C2C9 RID: 49865 RVA: 0x00116C68 File Offset: 0x00114E68
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009351 RID: 37713
			// (set) Token: 0x0600C2CA RID: 49866 RVA: 0x00116C7B File Offset: 0x00114E7B
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009352 RID: 37714
			// (set) Token: 0x0600C2CB RID: 49867 RVA: 0x00116C8E File Offset: 0x00114E8E
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009353 RID: 37715
			// (set) Token: 0x0600C2CC RID: 49868 RVA: 0x00116CA1 File Offset: 0x00114EA1
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009354 RID: 37716
			// (set) Token: 0x0600C2CD RID: 49869 RVA: 0x00116CB4 File Offset: 0x00114EB4
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009355 RID: 37717
			// (set) Token: 0x0600C2CE RID: 49870 RVA: 0x00116CCC File Offset: 0x00114ECC
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009356 RID: 37718
			// (set) Token: 0x0600C2CF RID: 49871 RVA: 0x00116CDF File Offset: 0x00114EDF
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009357 RID: 37719
			// (set) Token: 0x0600C2D0 RID: 49872 RVA: 0x00116CF2 File Offset: 0x00114EF2
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009358 RID: 37720
			// (set) Token: 0x0600C2D1 RID: 49873 RVA: 0x00116D05 File Offset: 0x00114F05
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009359 RID: 37721
			// (set) Token: 0x0600C2D2 RID: 49874 RVA: 0x00116D23 File Offset: 0x00114F23
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x1700935A RID: 37722
			// (set) Token: 0x0600C2D3 RID: 49875 RVA: 0x00116D36 File Offset: 0x00114F36
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700935B RID: 37723
			// (set) Token: 0x0600C2D4 RID: 49876 RVA: 0x00116D49 File Offset: 0x00114F49
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700935C RID: 37724
			// (set) Token: 0x0600C2D5 RID: 49877 RVA: 0x00116D5C File Offset: 0x00114F5C
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700935D RID: 37725
			// (set) Token: 0x0600C2D6 RID: 49878 RVA: 0x00116D6F File Offset: 0x00114F6F
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700935E RID: 37726
			// (set) Token: 0x0600C2D7 RID: 49879 RVA: 0x00116D82 File Offset: 0x00114F82
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700935F RID: 37727
			// (set) Token: 0x0600C2D8 RID: 49880 RVA: 0x00116D95 File Offset: 0x00114F95
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009360 RID: 37728
			// (set) Token: 0x0600C2D9 RID: 49881 RVA: 0x00116DA8 File Offset: 0x00114FA8
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009361 RID: 37729
			// (set) Token: 0x0600C2DA RID: 49882 RVA: 0x00116DBB File Offset: 0x00114FBB
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009362 RID: 37730
			// (set) Token: 0x0600C2DB RID: 49883 RVA: 0x00116DCE File Offset: 0x00114FCE
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009363 RID: 37731
			// (set) Token: 0x0600C2DC RID: 49884 RVA: 0x00116DE1 File Offset: 0x00114FE1
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009364 RID: 37732
			// (set) Token: 0x0600C2DD RID: 49885 RVA: 0x00116DF4 File Offset: 0x00114FF4
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009365 RID: 37733
			// (set) Token: 0x0600C2DE RID: 49886 RVA: 0x00116E07 File Offset: 0x00115007
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009366 RID: 37734
			// (set) Token: 0x0600C2DF RID: 49887 RVA: 0x00116E1A File Offset: 0x0011501A
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009367 RID: 37735
			// (set) Token: 0x0600C2E0 RID: 49888 RVA: 0x00116E38 File Offset: 0x00115038
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009368 RID: 37736
			// (set) Token: 0x0600C2E1 RID: 49889 RVA: 0x00116E50 File Offset: 0x00115050
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009369 RID: 37737
			// (set) Token: 0x0600C2E2 RID: 49890 RVA: 0x00116E68 File Offset: 0x00115068
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700936A RID: 37738
			// (set) Token: 0x0600C2E3 RID: 49891 RVA: 0x00116E80 File Offset: 0x00115080
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700936B RID: 37739
			// (set) Token: 0x0600C2E4 RID: 49892 RVA: 0x00116E93 File Offset: 0x00115093
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700936C RID: 37740
			// (set) Token: 0x0600C2E5 RID: 49893 RVA: 0x00116EA6 File Offset: 0x001150A6
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700936D RID: 37741
			// (set) Token: 0x0600C2E6 RID: 49894 RVA: 0x00116EBE File Offset: 0x001150BE
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700936E RID: 37742
			// (set) Token: 0x0600C2E7 RID: 49895 RVA: 0x00116ED1 File Offset: 0x001150D1
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x1700936F RID: 37743
			// (set) Token: 0x0600C2E8 RID: 49896 RVA: 0x00116EE9 File Offset: 0x001150E9
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009370 RID: 37744
			// (set) Token: 0x0600C2E9 RID: 49897 RVA: 0x00116F01 File Offset: 0x00115101
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009371 RID: 37745
			// (set) Token: 0x0600C2EA RID: 49898 RVA: 0x00116F14 File Offset: 0x00115114
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009372 RID: 37746
			// (set) Token: 0x0600C2EB RID: 49899 RVA: 0x00116F27 File Offset: 0x00115127
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009373 RID: 37747
			// (set) Token: 0x0600C2EC RID: 49900 RVA: 0x00116F3A File Offset: 0x0011513A
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009374 RID: 37748
			// (set) Token: 0x0600C2ED RID: 49901 RVA: 0x00116F4D File Offset: 0x0011514D
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009375 RID: 37749
			// (set) Token: 0x0600C2EE RID: 49902 RVA: 0x00116F65 File Offset: 0x00115165
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009376 RID: 37750
			// (set) Token: 0x0600C2EF RID: 49903 RVA: 0x00116F78 File Offset: 0x00115178
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009377 RID: 37751
			// (set) Token: 0x0600C2F0 RID: 49904 RVA: 0x00116F8B File Offset: 0x0011518B
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009378 RID: 37752
			// (set) Token: 0x0600C2F1 RID: 49905 RVA: 0x00116FA3 File Offset: 0x001151A3
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009379 RID: 37753
			// (set) Token: 0x0600C2F2 RID: 49906 RVA: 0x00116FBB File Offset: 0x001151BB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700937A RID: 37754
			// (set) Token: 0x0600C2F3 RID: 49907 RVA: 0x00116FCE File Offset: 0x001151CE
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x1700937B RID: 37755
			// (set) Token: 0x0600C2F4 RID: 49908 RVA: 0x00116FE6 File Offset: 0x001151E6
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700937C RID: 37756
			// (set) Token: 0x0600C2F5 RID: 49909 RVA: 0x00116FF9 File Offset: 0x001151F9
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700937D RID: 37757
			// (set) Token: 0x0600C2F6 RID: 49910 RVA: 0x0011700C File Offset: 0x0011520C
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700937E RID: 37758
			// (set) Token: 0x0600C2F7 RID: 49911 RVA: 0x0011701F File Offset: 0x0011521F
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700937F RID: 37759
			// (set) Token: 0x0600C2F8 RID: 49912 RVA: 0x0011703D File Offset: 0x0011523D
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009380 RID: 37760
			// (set) Token: 0x0600C2F9 RID: 49913 RVA: 0x0011705B File Offset: 0x0011525B
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009381 RID: 37761
			// (set) Token: 0x0600C2FA RID: 49914 RVA: 0x00117079 File Offset: 0x00115279
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009382 RID: 37762
			// (set) Token: 0x0600C2FB RID: 49915 RVA: 0x00117097 File Offset: 0x00115297
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17009383 RID: 37763
			// (set) Token: 0x0600C2FC RID: 49916 RVA: 0x001170AA File Offset: 0x001152AA
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009384 RID: 37764
			// (set) Token: 0x0600C2FD RID: 49917 RVA: 0x001170C2 File Offset: 0x001152C2
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009385 RID: 37765
			// (set) Token: 0x0600C2FE RID: 49918 RVA: 0x001170E0 File Offset: 0x001152E0
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17009386 RID: 37766
			// (set) Token: 0x0600C2FF RID: 49919 RVA: 0x001170F8 File Offset: 0x001152F8
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17009387 RID: 37767
			// (set) Token: 0x0600C300 RID: 49920 RVA: 0x00117110 File Offset: 0x00115310
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17009388 RID: 37768
			// (set) Token: 0x0600C301 RID: 49921 RVA: 0x00117123 File Offset: 0x00115323
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17009389 RID: 37769
			// (set) Token: 0x0600C302 RID: 49922 RVA: 0x0011713B File Offset: 0x0011533B
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700938A RID: 37770
			// (set) Token: 0x0600C303 RID: 49923 RVA: 0x0011714E File Offset: 0x0011534E
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700938B RID: 37771
			// (set) Token: 0x0600C304 RID: 49924 RVA: 0x00117161 File Offset: 0x00115361
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700938C RID: 37772
			// (set) Token: 0x0600C305 RID: 49925 RVA: 0x00117174 File Offset: 0x00115374
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700938D RID: 37773
			// (set) Token: 0x0600C306 RID: 49926 RVA: 0x00117187 File Offset: 0x00115387
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700938E RID: 37774
			// (set) Token: 0x0600C307 RID: 49927 RVA: 0x0011719F File Offset: 0x0011539F
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700938F RID: 37775
			// (set) Token: 0x0600C308 RID: 49928 RVA: 0x001171B2 File Offset: 0x001153B2
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009390 RID: 37776
			// (set) Token: 0x0600C309 RID: 49929 RVA: 0x001171C5 File Offset: 0x001153C5
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009391 RID: 37777
			// (set) Token: 0x0600C30A RID: 49930 RVA: 0x001171DD File Offset: 0x001153DD
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009392 RID: 37778
			// (set) Token: 0x0600C30B RID: 49931 RVA: 0x001171F0 File Offset: 0x001153F0
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009393 RID: 37779
			// (set) Token: 0x0600C30C RID: 49932 RVA: 0x00117208 File Offset: 0x00115408
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009394 RID: 37780
			// (set) Token: 0x0600C30D RID: 49933 RVA: 0x0011721B File Offset: 0x0011541B
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009395 RID: 37781
			// (set) Token: 0x0600C30E RID: 49934 RVA: 0x00117239 File Offset: 0x00115439
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009396 RID: 37782
			// (set) Token: 0x0600C30F RID: 49935 RVA: 0x0011724C File Offset: 0x0011544C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009397 RID: 37783
			// (set) Token: 0x0600C310 RID: 49936 RVA: 0x0011726A File Offset: 0x0011546A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009398 RID: 37784
			// (set) Token: 0x0600C311 RID: 49937 RVA: 0x0011727D File Offset: 0x0011547D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009399 RID: 37785
			// (set) Token: 0x0600C312 RID: 49938 RVA: 0x00117295 File Offset: 0x00115495
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700939A RID: 37786
			// (set) Token: 0x0600C313 RID: 49939 RVA: 0x001172AD File Offset: 0x001154AD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700939B RID: 37787
			// (set) Token: 0x0600C314 RID: 49940 RVA: 0x001172C5 File Offset: 0x001154C5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700939C RID: 37788
			// (set) Token: 0x0600C315 RID: 49941 RVA: 0x001172DD File Offset: 0x001154DD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D96 RID: 3478
		public class LinkedParameters : ParametersBase
		{
			// Token: 0x1700939D RID: 37789
			// (set) Token: 0x0600C317 RID: 49943 RVA: 0x001172FD File Offset: 0x001154FD
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700939E RID: 37790
			// (set) Token: 0x0600C318 RID: 49944 RVA: 0x00117310 File Offset: 0x00115510
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700939F RID: 37791
			// (set) Token: 0x0600C319 RID: 49945 RVA: 0x00117323 File Offset: 0x00115523
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x170093A0 RID: 37792
			// (set) Token: 0x0600C31A RID: 49946 RVA: 0x00117341 File Offset: 0x00115541
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x170093A1 RID: 37793
			// (set) Token: 0x0600C31B RID: 49947 RVA: 0x00117354 File Offset: 0x00115554
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x170093A2 RID: 37794
			// (set) Token: 0x0600C31C RID: 49948 RVA: 0x00117367 File Offset: 0x00115567
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170093A3 RID: 37795
			// (set) Token: 0x0600C31D RID: 49949 RVA: 0x00117385 File Offset: 0x00115585
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170093A4 RID: 37796
			// (set) Token: 0x0600C31E RID: 49950 RVA: 0x00117398 File Offset: 0x00115598
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170093A5 RID: 37797
			// (set) Token: 0x0600C31F RID: 49951 RVA: 0x001173B0 File Offset: 0x001155B0
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170093A6 RID: 37798
			// (set) Token: 0x0600C320 RID: 49952 RVA: 0x001173C8 File Offset: 0x001155C8
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x170093A7 RID: 37799
			// (set) Token: 0x0600C321 RID: 49953 RVA: 0x001173E0 File Offset: 0x001155E0
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x170093A8 RID: 37800
			// (set) Token: 0x0600C322 RID: 49954 RVA: 0x001173F3 File Offset: 0x001155F3
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x170093A9 RID: 37801
			// (set) Token: 0x0600C323 RID: 49955 RVA: 0x00117406 File Offset: 0x00115606
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x170093AA RID: 37802
			// (set) Token: 0x0600C324 RID: 49956 RVA: 0x00117419 File Offset: 0x00115619
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x170093AB RID: 37803
			// (set) Token: 0x0600C325 RID: 49957 RVA: 0x0011742C File Offset: 0x0011562C
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170093AC RID: 37804
			// (set) Token: 0x0600C326 RID: 49958 RVA: 0x0011743F File Offset: 0x0011563F
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170093AD RID: 37805
			// (set) Token: 0x0600C327 RID: 49959 RVA: 0x00117452 File Offset: 0x00115652
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x170093AE RID: 37806
			// (set) Token: 0x0600C328 RID: 49960 RVA: 0x00117465 File Offset: 0x00115665
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x170093AF RID: 37807
			// (set) Token: 0x0600C329 RID: 49961 RVA: 0x0011747D File Offset: 0x0011567D
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x170093B0 RID: 37808
			// (set) Token: 0x0600C32A RID: 49962 RVA: 0x00117490 File Offset: 0x00115690
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x170093B1 RID: 37809
			// (set) Token: 0x0600C32B RID: 49963 RVA: 0x001174A8 File Offset: 0x001156A8
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170093B2 RID: 37810
			// (set) Token: 0x0600C32C RID: 49964 RVA: 0x001174BB File Offset: 0x001156BB
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170093B3 RID: 37811
			// (set) Token: 0x0600C32D RID: 49965 RVA: 0x001174CE File Offset: 0x001156CE
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170093B4 RID: 37812
			// (set) Token: 0x0600C32E RID: 49966 RVA: 0x001174E1 File Offset: 0x001156E1
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170093B5 RID: 37813
			// (set) Token: 0x0600C32F RID: 49967 RVA: 0x001174F4 File Offset: 0x001156F4
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170093B6 RID: 37814
			// (set) Token: 0x0600C330 RID: 49968 RVA: 0x00117507 File Offset: 0x00115707
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170093B7 RID: 37815
			// (set) Token: 0x0600C331 RID: 49969 RVA: 0x0011751A File Offset: 0x0011571A
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170093B8 RID: 37816
			// (set) Token: 0x0600C332 RID: 49970 RVA: 0x0011752D File Offset: 0x0011572D
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170093B9 RID: 37817
			// (set) Token: 0x0600C333 RID: 49971 RVA: 0x00117540 File Offset: 0x00115740
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170093BA RID: 37818
			// (set) Token: 0x0600C334 RID: 49972 RVA: 0x00117553 File Offset: 0x00115753
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170093BB RID: 37819
			// (set) Token: 0x0600C335 RID: 49973 RVA: 0x00117566 File Offset: 0x00115766
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170093BC RID: 37820
			// (set) Token: 0x0600C336 RID: 49974 RVA: 0x00117579 File Offset: 0x00115779
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170093BD RID: 37821
			// (set) Token: 0x0600C337 RID: 49975 RVA: 0x0011758C File Offset: 0x0011578C
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170093BE RID: 37822
			// (set) Token: 0x0600C338 RID: 49976 RVA: 0x0011759F File Offset: 0x0011579F
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170093BF RID: 37823
			// (set) Token: 0x0600C339 RID: 49977 RVA: 0x001175B2 File Offset: 0x001157B2
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170093C0 RID: 37824
			// (set) Token: 0x0600C33A RID: 49978 RVA: 0x001175C5 File Offset: 0x001157C5
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170093C1 RID: 37825
			// (set) Token: 0x0600C33B RID: 49979 RVA: 0x001175D8 File Offset: 0x001157D8
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170093C2 RID: 37826
			// (set) Token: 0x0600C33C RID: 49980 RVA: 0x001175EB File Offset: 0x001157EB
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170093C3 RID: 37827
			// (set) Token: 0x0600C33D RID: 49981 RVA: 0x001175FE File Offset: 0x001157FE
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170093C4 RID: 37828
			// (set) Token: 0x0600C33E RID: 49982 RVA: 0x00117611 File Offset: 0x00115811
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170093C5 RID: 37829
			// (set) Token: 0x0600C33F RID: 49983 RVA: 0x00117624 File Offset: 0x00115824
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170093C6 RID: 37830
			// (set) Token: 0x0600C340 RID: 49984 RVA: 0x00117637 File Offset: 0x00115837
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170093C7 RID: 37831
			// (set) Token: 0x0600C341 RID: 49985 RVA: 0x0011764A File Offset: 0x0011584A
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170093C8 RID: 37832
			// (set) Token: 0x0600C342 RID: 49986 RVA: 0x0011765D File Offset: 0x0011585D
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170093C9 RID: 37833
			// (set) Token: 0x0600C343 RID: 49987 RVA: 0x00117675 File Offset: 0x00115875
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x170093CA RID: 37834
			// (set) Token: 0x0600C344 RID: 49988 RVA: 0x00117688 File Offset: 0x00115888
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x170093CB RID: 37835
			// (set) Token: 0x0600C345 RID: 49989 RVA: 0x001176A0 File Offset: 0x001158A0
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x170093CC RID: 37836
			// (set) Token: 0x0600C346 RID: 49990 RVA: 0x001176B3 File Offset: 0x001158B3
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x170093CD RID: 37837
			// (set) Token: 0x0600C347 RID: 49991 RVA: 0x001176CB File Offset: 0x001158CB
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x170093CE RID: 37838
			// (set) Token: 0x0600C348 RID: 49992 RVA: 0x001176E3 File Offset: 0x001158E3
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x170093CF RID: 37839
			// (set) Token: 0x0600C349 RID: 49993 RVA: 0x001176FB File Offset: 0x001158FB
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x170093D0 RID: 37840
			// (set) Token: 0x0600C34A RID: 49994 RVA: 0x00117713 File Offset: 0x00115913
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170093D1 RID: 37841
			// (set) Token: 0x0600C34B RID: 49995 RVA: 0x0011772B File Offset: 0x0011592B
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x170093D2 RID: 37842
			// (set) Token: 0x0600C34C RID: 49996 RVA: 0x00117743 File Offset: 0x00115943
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x170093D3 RID: 37843
			// (set) Token: 0x0600C34D RID: 49997 RVA: 0x0011775B File Offset: 0x0011595B
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x170093D4 RID: 37844
			// (set) Token: 0x0600C34E RID: 49998 RVA: 0x00117773 File Offset: 0x00115973
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x170093D5 RID: 37845
			// (set) Token: 0x0600C34F RID: 49999 RVA: 0x0011778B File Offset: 0x0011598B
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x170093D6 RID: 37846
			// (set) Token: 0x0600C350 RID: 50000 RVA: 0x0011779E File Offset: 0x0011599E
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x170093D7 RID: 37847
			// (set) Token: 0x0600C351 RID: 50001 RVA: 0x001177B1 File Offset: 0x001159B1
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x170093D8 RID: 37848
			// (set) Token: 0x0600C352 RID: 50002 RVA: 0x001177C4 File Offset: 0x001159C4
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x170093D9 RID: 37849
			// (set) Token: 0x0600C353 RID: 50003 RVA: 0x001177D7 File Offset: 0x001159D7
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x170093DA RID: 37850
			// (set) Token: 0x0600C354 RID: 50004 RVA: 0x001177EA File Offset: 0x001159EA
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x170093DB RID: 37851
			// (set) Token: 0x0600C355 RID: 50005 RVA: 0x001177FD File Offset: 0x001159FD
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x170093DC RID: 37852
			// (set) Token: 0x0600C356 RID: 50006 RVA: 0x00117815 File Offset: 0x00115A15
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x170093DD RID: 37853
			// (set) Token: 0x0600C357 RID: 50007 RVA: 0x00117828 File Offset: 0x00115A28
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x170093DE RID: 37854
			// (set) Token: 0x0600C358 RID: 50008 RVA: 0x0011783B File Offset: 0x00115A3B
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x170093DF RID: 37855
			// (set) Token: 0x0600C359 RID: 50009 RVA: 0x0011784E File Offset: 0x00115A4E
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x170093E0 RID: 37856
			// (set) Token: 0x0600C35A RID: 50010 RVA: 0x0011786C File Offset: 0x00115A6C
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x170093E1 RID: 37857
			// (set) Token: 0x0600C35B RID: 50011 RVA: 0x0011787F File Offset: 0x00115A7F
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x170093E2 RID: 37858
			// (set) Token: 0x0600C35C RID: 50012 RVA: 0x00117892 File Offset: 0x00115A92
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x170093E3 RID: 37859
			// (set) Token: 0x0600C35D RID: 50013 RVA: 0x001178A5 File Offset: 0x00115AA5
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x170093E4 RID: 37860
			// (set) Token: 0x0600C35E RID: 50014 RVA: 0x001178B8 File Offset: 0x00115AB8
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x170093E5 RID: 37861
			// (set) Token: 0x0600C35F RID: 50015 RVA: 0x001178CB File Offset: 0x00115ACB
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x170093E6 RID: 37862
			// (set) Token: 0x0600C360 RID: 50016 RVA: 0x001178DE File Offset: 0x00115ADE
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x170093E7 RID: 37863
			// (set) Token: 0x0600C361 RID: 50017 RVA: 0x001178F1 File Offset: 0x00115AF1
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x170093E8 RID: 37864
			// (set) Token: 0x0600C362 RID: 50018 RVA: 0x00117904 File Offset: 0x00115B04
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x170093E9 RID: 37865
			// (set) Token: 0x0600C363 RID: 50019 RVA: 0x00117917 File Offset: 0x00115B17
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x170093EA RID: 37866
			// (set) Token: 0x0600C364 RID: 50020 RVA: 0x0011792A File Offset: 0x00115B2A
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x170093EB RID: 37867
			// (set) Token: 0x0600C365 RID: 50021 RVA: 0x0011793D File Offset: 0x00115B3D
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x170093EC RID: 37868
			// (set) Token: 0x0600C366 RID: 50022 RVA: 0x00117950 File Offset: 0x00115B50
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x170093ED RID: 37869
			// (set) Token: 0x0600C367 RID: 50023 RVA: 0x00117963 File Offset: 0x00115B63
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170093EE RID: 37870
			// (set) Token: 0x0600C368 RID: 50024 RVA: 0x00117981 File Offset: 0x00115B81
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x170093EF RID: 37871
			// (set) Token: 0x0600C369 RID: 50025 RVA: 0x00117999 File Offset: 0x00115B99
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170093F0 RID: 37872
			// (set) Token: 0x0600C36A RID: 50026 RVA: 0x001179B1 File Offset: 0x00115BB1
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x170093F1 RID: 37873
			// (set) Token: 0x0600C36B RID: 50027 RVA: 0x001179C9 File Offset: 0x00115BC9
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x170093F2 RID: 37874
			// (set) Token: 0x0600C36C RID: 50028 RVA: 0x001179DC File Offset: 0x00115BDC
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x170093F3 RID: 37875
			// (set) Token: 0x0600C36D RID: 50029 RVA: 0x001179EF File Offset: 0x00115BEF
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x170093F4 RID: 37876
			// (set) Token: 0x0600C36E RID: 50030 RVA: 0x00117A07 File Offset: 0x00115C07
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x170093F5 RID: 37877
			// (set) Token: 0x0600C36F RID: 50031 RVA: 0x00117A1A File Offset: 0x00115C1A
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x170093F6 RID: 37878
			// (set) Token: 0x0600C370 RID: 50032 RVA: 0x00117A32 File Offset: 0x00115C32
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x170093F7 RID: 37879
			// (set) Token: 0x0600C371 RID: 50033 RVA: 0x00117A4A File Offset: 0x00115C4A
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170093F8 RID: 37880
			// (set) Token: 0x0600C372 RID: 50034 RVA: 0x00117A5D File Offset: 0x00115C5D
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170093F9 RID: 37881
			// (set) Token: 0x0600C373 RID: 50035 RVA: 0x00117A70 File Offset: 0x00115C70
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x170093FA RID: 37882
			// (set) Token: 0x0600C374 RID: 50036 RVA: 0x00117A83 File Offset: 0x00115C83
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x170093FB RID: 37883
			// (set) Token: 0x0600C375 RID: 50037 RVA: 0x00117A96 File Offset: 0x00115C96
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x170093FC RID: 37884
			// (set) Token: 0x0600C376 RID: 50038 RVA: 0x00117AAE File Offset: 0x00115CAE
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x170093FD RID: 37885
			// (set) Token: 0x0600C377 RID: 50039 RVA: 0x00117AC1 File Offset: 0x00115CC1
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x170093FE RID: 37886
			// (set) Token: 0x0600C378 RID: 50040 RVA: 0x00117AD4 File Offset: 0x00115CD4
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x170093FF RID: 37887
			// (set) Token: 0x0600C379 RID: 50041 RVA: 0x00117AEC File Offset: 0x00115CEC
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009400 RID: 37888
			// (set) Token: 0x0600C37A RID: 50042 RVA: 0x00117B04 File Offset: 0x00115D04
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009401 RID: 37889
			// (set) Token: 0x0600C37B RID: 50043 RVA: 0x00117B17 File Offset: 0x00115D17
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17009402 RID: 37890
			// (set) Token: 0x0600C37C RID: 50044 RVA: 0x00117B2F File Offset: 0x00115D2F
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009403 RID: 37891
			// (set) Token: 0x0600C37D RID: 50045 RVA: 0x00117B42 File Offset: 0x00115D42
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009404 RID: 37892
			// (set) Token: 0x0600C37E RID: 50046 RVA: 0x00117B55 File Offset: 0x00115D55
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17009405 RID: 37893
			// (set) Token: 0x0600C37F RID: 50047 RVA: 0x00117B68 File Offset: 0x00115D68
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009406 RID: 37894
			// (set) Token: 0x0600C380 RID: 50048 RVA: 0x00117B86 File Offset: 0x00115D86
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009407 RID: 37895
			// (set) Token: 0x0600C381 RID: 50049 RVA: 0x00117BA4 File Offset: 0x00115DA4
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009408 RID: 37896
			// (set) Token: 0x0600C382 RID: 50050 RVA: 0x00117BC2 File Offset: 0x00115DC2
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009409 RID: 37897
			// (set) Token: 0x0600C383 RID: 50051 RVA: 0x00117BE0 File Offset: 0x00115DE0
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700940A RID: 37898
			// (set) Token: 0x0600C384 RID: 50052 RVA: 0x00117BF3 File Offset: 0x00115DF3
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700940B RID: 37899
			// (set) Token: 0x0600C385 RID: 50053 RVA: 0x00117C0B File Offset: 0x00115E0B
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700940C RID: 37900
			// (set) Token: 0x0600C386 RID: 50054 RVA: 0x00117C29 File Offset: 0x00115E29
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700940D RID: 37901
			// (set) Token: 0x0600C387 RID: 50055 RVA: 0x00117C41 File Offset: 0x00115E41
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700940E RID: 37902
			// (set) Token: 0x0600C388 RID: 50056 RVA: 0x00117C59 File Offset: 0x00115E59
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x1700940F RID: 37903
			// (set) Token: 0x0600C389 RID: 50057 RVA: 0x00117C6C File Offset: 0x00115E6C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17009410 RID: 37904
			// (set) Token: 0x0600C38A RID: 50058 RVA: 0x00117C84 File Offset: 0x00115E84
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009411 RID: 37905
			// (set) Token: 0x0600C38B RID: 50059 RVA: 0x00117C97 File Offset: 0x00115E97
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009412 RID: 37906
			// (set) Token: 0x0600C38C RID: 50060 RVA: 0x00117CAA File Offset: 0x00115EAA
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009413 RID: 37907
			// (set) Token: 0x0600C38D RID: 50061 RVA: 0x00117CBD File Offset: 0x00115EBD
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009414 RID: 37908
			// (set) Token: 0x0600C38E RID: 50062 RVA: 0x00117CD0 File Offset: 0x00115ED0
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009415 RID: 37909
			// (set) Token: 0x0600C38F RID: 50063 RVA: 0x00117CE8 File Offset: 0x00115EE8
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009416 RID: 37910
			// (set) Token: 0x0600C390 RID: 50064 RVA: 0x00117CFB File Offset: 0x00115EFB
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009417 RID: 37911
			// (set) Token: 0x0600C391 RID: 50065 RVA: 0x00117D0E File Offset: 0x00115F0E
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009418 RID: 37912
			// (set) Token: 0x0600C392 RID: 50066 RVA: 0x00117D26 File Offset: 0x00115F26
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009419 RID: 37913
			// (set) Token: 0x0600C393 RID: 50067 RVA: 0x00117D39 File Offset: 0x00115F39
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700941A RID: 37914
			// (set) Token: 0x0600C394 RID: 50068 RVA: 0x00117D51 File Offset: 0x00115F51
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700941B RID: 37915
			// (set) Token: 0x0600C395 RID: 50069 RVA: 0x00117D64 File Offset: 0x00115F64
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700941C RID: 37916
			// (set) Token: 0x0600C396 RID: 50070 RVA: 0x00117D82 File Offset: 0x00115F82
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700941D RID: 37917
			// (set) Token: 0x0600C397 RID: 50071 RVA: 0x00117D95 File Offset: 0x00115F95
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700941E RID: 37918
			// (set) Token: 0x0600C398 RID: 50072 RVA: 0x00117DB3 File Offset: 0x00115FB3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700941F RID: 37919
			// (set) Token: 0x0600C399 RID: 50073 RVA: 0x00117DC6 File Offset: 0x00115FC6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009420 RID: 37920
			// (set) Token: 0x0600C39A RID: 50074 RVA: 0x00117DDE File Offset: 0x00115FDE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009421 RID: 37921
			// (set) Token: 0x0600C39B RID: 50075 RVA: 0x00117DF6 File Offset: 0x00115FF6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009422 RID: 37922
			// (set) Token: 0x0600C39C RID: 50076 RVA: 0x00117E0E File Offset: 0x0011600E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009423 RID: 37923
			// (set) Token: 0x0600C39D RID: 50077 RVA: 0x00117E26 File Offset: 0x00116026
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D97 RID: 3479
		public class SharedParameters : ParametersBase
		{
			// Token: 0x17009424 RID: 37924
			// (set) Token: 0x0600C39F RID: 50079 RVA: 0x00117E46 File Offset: 0x00116046
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17009425 RID: 37925
			// (set) Token: 0x0600C3A0 RID: 50080 RVA: 0x00117E59 File Offset: 0x00116059
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17009426 RID: 37926
			// (set) Token: 0x0600C3A1 RID: 50081 RVA: 0x00117E6C File Offset: 0x0011606C
			public virtual SwitchParameter Shared
			{
				set
				{
					base.PowerSharpParameters["Shared"] = value;
				}
			}

			// Token: 0x17009427 RID: 37927
			// (set) Token: 0x0600C3A2 RID: 50082 RVA: 0x00117E84 File Offset: 0x00116084
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009428 RID: 37928
			// (set) Token: 0x0600C3A3 RID: 50083 RVA: 0x00117EA2 File Offset: 0x001160A2
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009429 RID: 37929
			// (set) Token: 0x0600C3A4 RID: 50084 RVA: 0x00117EB5 File Offset: 0x001160B5
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700942A RID: 37930
			// (set) Token: 0x0600C3A5 RID: 50085 RVA: 0x00117ECD File Offset: 0x001160CD
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700942B RID: 37931
			// (set) Token: 0x0600C3A6 RID: 50086 RVA: 0x00117EE5 File Offset: 0x001160E5
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x1700942C RID: 37932
			// (set) Token: 0x0600C3A7 RID: 50087 RVA: 0x00117EFD File Offset: 0x001160FD
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700942D RID: 37933
			// (set) Token: 0x0600C3A8 RID: 50088 RVA: 0x00117F10 File Offset: 0x00116110
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700942E RID: 37934
			// (set) Token: 0x0600C3A9 RID: 50089 RVA: 0x00117F23 File Offset: 0x00116123
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700942F RID: 37935
			// (set) Token: 0x0600C3AA RID: 50090 RVA: 0x00117F36 File Offset: 0x00116136
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009430 RID: 37936
			// (set) Token: 0x0600C3AB RID: 50091 RVA: 0x00117F49 File Offset: 0x00116149
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009431 RID: 37937
			// (set) Token: 0x0600C3AC RID: 50092 RVA: 0x00117F5C File Offset: 0x0011615C
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009432 RID: 37938
			// (set) Token: 0x0600C3AD RID: 50093 RVA: 0x00117F6F File Offset: 0x0011616F
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009433 RID: 37939
			// (set) Token: 0x0600C3AE RID: 50094 RVA: 0x00117F82 File Offset: 0x00116182
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17009434 RID: 37940
			// (set) Token: 0x0600C3AF RID: 50095 RVA: 0x00117F9A File Offset: 0x0011619A
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009435 RID: 37941
			// (set) Token: 0x0600C3B0 RID: 50096 RVA: 0x00117FAD File Offset: 0x001161AD
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009436 RID: 37942
			// (set) Token: 0x0600C3B1 RID: 50097 RVA: 0x00117FC5 File Offset: 0x001161C5
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009437 RID: 37943
			// (set) Token: 0x0600C3B2 RID: 50098 RVA: 0x00117FD8 File Offset: 0x001161D8
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009438 RID: 37944
			// (set) Token: 0x0600C3B3 RID: 50099 RVA: 0x00117FEB File Offset: 0x001161EB
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009439 RID: 37945
			// (set) Token: 0x0600C3B4 RID: 50100 RVA: 0x00117FFE File Offset: 0x001161FE
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700943A RID: 37946
			// (set) Token: 0x0600C3B5 RID: 50101 RVA: 0x00118011 File Offset: 0x00116211
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x1700943B RID: 37947
			// (set) Token: 0x0600C3B6 RID: 50102 RVA: 0x00118024 File Offset: 0x00116224
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x1700943C RID: 37948
			// (set) Token: 0x0600C3B7 RID: 50103 RVA: 0x00118037 File Offset: 0x00116237
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x1700943D RID: 37949
			// (set) Token: 0x0600C3B8 RID: 50104 RVA: 0x0011804A File Offset: 0x0011624A
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x1700943E RID: 37950
			// (set) Token: 0x0600C3B9 RID: 50105 RVA: 0x0011805D File Offset: 0x0011625D
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x1700943F RID: 37951
			// (set) Token: 0x0600C3BA RID: 50106 RVA: 0x00118070 File Offset: 0x00116270
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009440 RID: 37952
			// (set) Token: 0x0600C3BB RID: 50107 RVA: 0x00118083 File Offset: 0x00116283
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009441 RID: 37953
			// (set) Token: 0x0600C3BC RID: 50108 RVA: 0x00118096 File Offset: 0x00116296
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009442 RID: 37954
			// (set) Token: 0x0600C3BD RID: 50109 RVA: 0x001180A9 File Offset: 0x001162A9
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009443 RID: 37955
			// (set) Token: 0x0600C3BE RID: 50110 RVA: 0x001180BC File Offset: 0x001162BC
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009444 RID: 37956
			// (set) Token: 0x0600C3BF RID: 50111 RVA: 0x001180CF File Offset: 0x001162CF
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009445 RID: 37957
			// (set) Token: 0x0600C3C0 RID: 50112 RVA: 0x001180E2 File Offset: 0x001162E2
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009446 RID: 37958
			// (set) Token: 0x0600C3C1 RID: 50113 RVA: 0x001180F5 File Offset: 0x001162F5
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009447 RID: 37959
			// (set) Token: 0x0600C3C2 RID: 50114 RVA: 0x00118108 File Offset: 0x00116308
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009448 RID: 37960
			// (set) Token: 0x0600C3C3 RID: 50115 RVA: 0x0011811B File Offset: 0x0011631B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009449 RID: 37961
			// (set) Token: 0x0600C3C4 RID: 50116 RVA: 0x0011812E File Offset: 0x0011632E
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700944A RID: 37962
			// (set) Token: 0x0600C3C5 RID: 50117 RVA: 0x00118141 File Offset: 0x00116341
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700944B RID: 37963
			// (set) Token: 0x0600C3C6 RID: 50118 RVA: 0x00118154 File Offset: 0x00116354
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700944C RID: 37964
			// (set) Token: 0x0600C3C7 RID: 50119 RVA: 0x00118167 File Offset: 0x00116367
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700944D RID: 37965
			// (set) Token: 0x0600C3C8 RID: 50120 RVA: 0x0011817A File Offset: 0x0011637A
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x1700944E RID: 37966
			// (set) Token: 0x0600C3C9 RID: 50121 RVA: 0x00118192 File Offset: 0x00116392
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700944F RID: 37967
			// (set) Token: 0x0600C3CA RID: 50122 RVA: 0x001181A5 File Offset: 0x001163A5
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009450 RID: 37968
			// (set) Token: 0x0600C3CB RID: 50123 RVA: 0x001181BD File Offset: 0x001163BD
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17009451 RID: 37969
			// (set) Token: 0x0600C3CC RID: 50124 RVA: 0x001181D0 File Offset: 0x001163D0
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009452 RID: 37970
			// (set) Token: 0x0600C3CD RID: 50125 RVA: 0x001181E8 File Offset: 0x001163E8
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009453 RID: 37971
			// (set) Token: 0x0600C3CE RID: 50126 RVA: 0x00118200 File Offset: 0x00116400
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17009454 RID: 37972
			// (set) Token: 0x0600C3CF RID: 50127 RVA: 0x00118218 File Offset: 0x00116418
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17009455 RID: 37973
			// (set) Token: 0x0600C3D0 RID: 50128 RVA: 0x00118230 File Offset: 0x00116430
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17009456 RID: 37974
			// (set) Token: 0x0600C3D1 RID: 50129 RVA: 0x00118248 File Offset: 0x00116448
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17009457 RID: 37975
			// (set) Token: 0x0600C3D2 RID: 50130 RVA: 0x00118260 File Offset: 0x00116460
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009458 RID: 37976
			// (set) Token: 0x0600C3D3 RID: 50131 RVA: 0x00118278 File Offset: 0x00116478
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17009459 RID: 37977
			// (set) Token: 0x0600C3D4 RID: 50132 RVA: 0x00118290 File Offset: 0x00116490
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x1700945A RID: 37978
			// (set) Token: 0x0600C3D5 RID: 50133 RVA: 0x001182A8 File Offset: 0x001164A8
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x1700945B RID: 37979
			// (set) Token: 0x0600C3D6 RID: 50134 RVA: 0x001182BB File Offset: 0x001164BB
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700945C RID: 37980
			// (set) Token: 0x0600C3D7 RID: 50135 RVA: 0x001182CE File Offset: 0x001164CE
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700945D RID: 37981
			// (set) Token: 0x0600C3D8 RID: 50136 RVA: 0x001182E1 File Offset: 0x001164E1
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700945E RID: 37982
			// (set) Token: 0x0600C3D9 RID: 50137 RVA: 0x001182F4 File Offset: 0x001164F4
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x1700945F RID: 37983
			// (set) Token: 0x0600C3DA RID: 50138 RVA: 0x00118307 File Offset: 0x00116507
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009460 RID: 37984
			// (set) Token: 0x0600C3DB RID: 50139 RVA: 0x0011831A File Offset: 0x0011651A
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009461 RID: 37985
			// (set) Token: 0x0600C3DC RID: 50140 RVA: 0x00118332 File Offset: 0x00116532
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009462 RID: 37986
			// (set) Token: 0x0600C3DD RID: 50141 RVA: 0x00118345 File Offset: 0x00116545
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009463 RID: 37987
			// (set) Token: 0x0600C3DE RID: 50142 RVA: 0x00118358 File Offset: 0x00116558
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009464 RID: 37988
			// (set) Token: 0x0600C3DF RID: 50143 RVA: 0x0011836B File Offset: 0x0011656B
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009465 RID: 37989
			// (set) Token: 0x0600C3E0 RID: 50144 RVA: 0x00118389 File Offset: 0x00116589
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009466 RID: 37990
			// (set) Token: 0x0600C3E1 RID: 50145 RVA: 0x0011839C File Offset: 0x0011659C
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009467 RID: 37991
			// (set) Token: 0x0600C3E2 RID: 50146 RVA: 0x001183AF File Offset: 0x001165AF
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009468 RID: 37992
			// (set) Token: 0x0600C3E3 RID: 50147 RVA: 0x001183C2 File Offset: 0x001165C2
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009469 RID: 37993
			// (set) Token: 0x0600C3E4 RID: 50148 RVA: 0x001183D5 File Offset: 0x001165D5
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700946A RID: 37994
			// (set) Token: 0x0600C3E5 RID: 50149 RVA: 0x001183E8 File Offset: 0x001165E8
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700946B RID: 37995
			// (set) Token: 0x0600C3E6 RID: 50150 RVA: 0x001183FB File Offset: 0x001165FB
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700946C RID: 37996
			// (set) Token: 0x0600C3E7 RID: 50151 RVA: 0x0011840E File Offset: 0x0011660E
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700946D RID: 37997
			// (set) Token: 0x0600C3E8 RID: 50152 RVA: 0x00118421 File Offset: 0x00116621
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700946E RID: 37998
			// (set) Token: 0x0600C3E9 RID: 50153 RVA: 0x00118434 File Offset: 0x00116634
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700946F RID: 37999
			// (set) Token: 0x0600C3EA RID: 50154 RVA: 0x00118447 File Offset: 0x00116647
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009470 RID: 38000
			// (set) Token: 0x0600C3EB RID: 50155 RVA: 0x0011845A File Offset: 0x0011665A
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009471 RID: 38001
			// (set) Token: 0x0600C3EC RID: 50156 RVA: 0x0011846D File Offset: 0x0011666D
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009472 RID: 38002
			// (set) Token: 0x0600C3ED RID: 50157 RVA: 0x00118480 File Offset: 0x00116680
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009473 RID: 38003
			// (set) Token: 0x0600C3EE RID: 50158 RVA: 0x0011849E File Offset: 0x0011669E
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009474 RID: 38004
			// (set) Token: 0x0600C3EF RID: 50159 RVA: 0x001184B6 File Offset: 0x001166B6
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009475 RID: 38005
			// (set) Token: 0x0600C3F0 RID: 50160 RVA: 0x001184CE File Offset: 0x001166CE
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009476 RID: 38006
			// (set) Token: 0x0600C3F1 RID: 50161 RVA: 0x001184E6 File Offset: 0x001166E6
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009477 RID: 38007
			// (set) Token: 0x0600C3F2 RID: 50162 RVA: 0x001184F9 File Offset: 0x001166F9
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009478 RID: 38008
			// (set) Token: 0x0600C3F3 RID: 50163 RVA: 0x0011850C File Offset: 0x0011670C
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009479 RID: 38009
			// (set) Token: 0x0600C3F4 RID: 50164 RVA: 0x00118524 File Offset: 0x00116724
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700947A RID: 38010
			// (set) Token: 0x0600C3F5 RID: 50165 RVA: 0x00118537 File Offset: 0x00116737
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x1700947B RID: 38011
			// (set) Token: 0x0600C3F6 RID: 50166 RVA: 0x0011854F File Offset: 0x0011674F
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700947C RID: 38012
			// (set) Token: 0x0600C3F7 RID: 50167 RVA: 0x00118567 File Offset: 0x00116767
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x1700947D RID: 38013
			// (set) Token: 0x0600C3F8 RID: 50168 RVA: 0x0011857A File Offset: 0x0011677A
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x1700947E RID: 38014
			// (set) Token: 0x0600C3F9 RID: 50169 RVA: 0x0011858D File Offset: 0x0011678D
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700947F RID: 38015
			// (set) Token: 0x0600C3FA RID: 50170 RVA: 0x001185A0 File Offset: 0x001167A0
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009480 RID: 38016
			// (set) Token: 0x0600C3FB RID: 50171 RVA: 0x001185B3 File Offset: 0x001167B3
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009481 RID: 38017
			// (set) Token: 0x0600C3FC RID: 50172 RVA: 0x001185CB File Offset: 0x001167CB
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009482 RID: 38018
			// (set) Token: 0x0600C3FD RID: 50173 RVA: 0x001185DE File Offset: 0x001167DE
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009483 RID: 38019
			// (set) Token: 0x0600C3FE RID: 50174 RVA: 0x001185F1 File Offset: 0x001167F1
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009484 RID: 38020
			// (set) Token: 0x0600C3FF RID: 50175 RVA: 0x00118609 File Offset: 0x00116809
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009485 RID: 38021
			// (set) Token: 0x0600C400 RID: 50176 RVA: 0x00118621 File Offset: 0x00116821
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009486 RID: 38022
			// (set) Token: 0x0600C401 RID: 50177 RVA: 0x00118634 File Offset: 0x00116834
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17009487 RID: 38023
			// (set) Token: 0x0600C402 RID: 50178 RVA: 0x0011864C File Offset: 0x0011684C
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009488 RID: 38024
			// (set) Token: 0x0600C403 RID: 50179 RVA: 0x0011865F File Offset: 0x0011685F
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009489 RID: 38025
			// (set) Token: 0x0600C404 RID: 50180 RVA: 0x00118672 File Offset: 0x00116872
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700948A RID: 38026
			// (set) Token: 0x0600C405 RID: 50181 RVA: 0x00118685 File Offset: 0x00116885
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700948B RID: 38027
			// (set) Token: 0x0600C406 RID: 50182 RVA: 0x001186A3 File Offset: 0x001168A3
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700948C RID: 38028
			// (set) Token: 0x0600C407 RID: 50183 RVA: 0x001186C1 File Offset: 0x001168C1
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700948D RID: 38029
			// (set) Token: 0x0600C408 RID: 50184 RVA: 0x001186DF File Offset: 0x001168DF
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700948E RID: 38030
			// (set) Token: 0x0600C409 RID: 50185 RVA: 0x001186FD File Offset: 0x001168FD
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700948F RID: 38031
			// (set) Token: 0x0600C40A RID: 50186 RVA: 0x00118710 File Offset: 0x00116910
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009490 RID: 38032
			// (set) Token: 0x0600C40B RID: 50187 RVA: 0x00118728 File Offset: 0x00116928
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009491 RID: 38033
			// (set) Token: 0x0600C40C RID: 50188 RVA: 0x00118746 File Offset: 0x00116946
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17009492 RID: 38034
			// (set) Token: 0x0600C40D RID: 50189 RVA: 0x0011875E File Offset: 0x0011695E
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17009493 RID: 38035
			// (set) Token: 0x0600C40E RID: 50190 RVA: 0x00118776 File Offset: 0x00116976
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17009494 RID: 38036
			// (set) Token: 0x0600C40F RID: 50191 RVA: 0x00118789 File Offset: 0x00116989
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17009495 RID: 38037
			// (set) Token: 0x0600C410 RID: 50192 RVA: 0x001187A1 File Offset: 0x001169A1
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009496 RID: 38038
			// (set) Token: 0x0600C411 RID: 50193 RVA: 0x001187B4 File Offset: 0x001169B4
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009497 RID: 38039
			// (set) Token: 0x0600C412 RID: 50194 RVA: 0x001187C7 File Offset: 0x001169C7
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009498 RID: 38040
			// (set) Token: 0x0600C413 RID: 50195 RVA: 0x001187DA File Offset: 0x001169DA
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009499 RID: 38041
			// (set) Token: 0x0600C414 RID: 50196 RVA: 0x001187ED File Offset: 0x001169ED
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700949A RID: 38042
			// (set) Token: 0x0600C415 RID: 50197 RVA: 0x00118805 File Offset: 0x00116A05
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700949B RID: 38043
			// (set) Token: 0x0600C416 RID: 50198 RVA: 0x00118818 File Offset: 0x00116A18
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700949C RID: 38044
			// (set) Token: 0x0600C417 RID: 50199 RVA: 0x0011882B File Offset: 0x00116A2B
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700949D RID: 38045
			// (set) Token: 0x0600C418 RID: 50200 RVA: 0x00118843 File Offset: 0x00116A43
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700949E RID: 38046
			// (set) Token: 0x0600C419 RID: 50201 RVA: 0x00118856 File Offset: 0x00116A56
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700949F RID: 38047
			// (set) Token: 0x0600C41A RID: 50202 RVA: 0x0011886E File Offset: 0x00116A6E
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170094A0 RID: 38048
			// (set) Token: 0x0600C41B RID: 50203 RVA: 0x00118881 File Offset: 0x00116A81
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170094A1 RID: 38049
			// (set) Token: 0x0600C41C RID: 50204 RVA: 0x0011889F File Offset: 0x00116A9F
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170094A2 RID: 38050
			// (set) Token: 0x0600C41D RID: 50205 RVA: 0x001188B2 File Offset: 0x00116AB2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170094A3 RID: 38051
			// (set) Token: 0x0600C41E RID: 50206 RVA: 0x001188D0 File Offset: 0x00116AD0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170094A4 RID: 38052
			// (set) Token: 0x0600C41F RID: 50207 RVA: 0x001188E3 File Offset: 0x00116AE3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170094A5 RID: 38053
			// (set) Token: 0x0600C420 RID: 50208 RVA: 0x001188FB File Offset: 0x00116AFB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170094A6 RID: 38054
			// (set) Token: 0x0600C421 RID: 50209 RVA: 0x00118913 File Offset: 0x00116B13
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170094A7 RID: 38055
			// (set) Token: 0x0600C422 RID: 50210 RVA: 0x0011892B File Offset: 0x00116B2B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170094A8 RID: 38056
			// (set) Token: 0x0600C423 RID: 50211 RVA: 0x00118943 File Offset: 0x00116B43
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D98 RID: 3480
		public class LinkedRoomMailboxParameters : ParametersBase
		{
			// Token: 0x170094A9 RID: 38057
			// (set) Token: 0x0600C425 RID: 50213 RVA: 0x00118963 File Offset: 0x00116B63
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170094AA RID: 38058
			// (set) Token: 0x0600C426 RID: 50214 RVA: 0x00118976 File Offset: 0x00116B76
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170094AB RID: 38059
			// (set) Token: 0x0600C427 RID: 50215 RVA: 0x00118989 File Offset: 0x00116B89
			public virtual SwitchParameter LinkedRoom
			{
				set
				{
					base.PowerSharpParameters["LinkedRoom"] = value;
				}
			}

			// Token: 0x170094AC RID: 38060
			// (set) Token: 0x0600C428 RID: 50216 RVA: 0x001189A1 File Offset: 0x00116BA1
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x170094AD RID: 38061
			// (set) Token: 0x0600C429 RID: 50217 RVA: 0x001189BF File Offset: 0x00116BBF
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x170094AE RID: 38062
			// (set) Token: 0x0600C42A RID: 50218 RVA: 0x001189D2 File Offset: 0x00116BD2
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x170094AF RID: 38063
			// (set) Token: 0x0600C42B RID: 50219 RVA: 0x001189E5 File Offset: 0x00116BE5
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170094B0 RID: 38064
			// (set) Token: 0x0600C42C RID: 50220 RVA: 0x00118A03 File Offset: 0x00116C03
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170094B1 RID: 38065
			// (set) Token: 0x0600C42D RID: 50221 RVA: 0x00118A16 File Offset: 0x00116C16
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170094B2 RID: 38066
			// (set) Token: 0x0600C42E RID: 50222 RVA: 0x00118A2E File Offset: 0x00116C2E
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170094B3 RID: 38067
			// (set) Token: 0x0600C42F RID: 50223 RVA: 0x00118A46 File Offset: 0x00116C46
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x170094B4 RID: 38068
			// (set) Token: 0x0600C430 RID: 50224 RVA: 0x00118A5E File Offset: 0x00116C5E
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x170094B5 RID: 38069
			// (set) Token: 0x0600C431 RID: 50225 RVA: 0x00118A71 File Offset: 0x00116C71
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x170094B6 RID: 38070
			// (set) Token: 0x0600C432 RID: 50226 RVA: 0x00118A84 File Offset: 0x00116C84
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x170094B7 RID: 38071
			// (set) Token: 0x0600C433 RID: 50227 RVA: 0x00118A97 File Offset: 0x00116C97
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x170094B8 RID: 38072
			// (set) Token: 0x0600C434 RID: 50228 RVA: 0x00118AAA File Offset: 0x00116CAA
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170094B9 RID: 38073
			// (set) Token: 0x0600C435 RID: 50229 RVA: 0x00118ABD File Offset: 0x00116CBD
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170094BA RID: 38074
			// (set) Token: 0x0600C436 RID: 50230 RVA: 0x00118AD0 File Offset: 0x00116CD0
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x170094BB RID: 38075
			// (set) Token: 0x0600C437 RID: 50231 RVA: 0x00118AE3 File Offset: 0x00116CE3
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x170094BC RID: 38076
			// (set) Token: 0x0600C438 RID: 50232 RVA: 0x00118AFB File Offset: 0x00116CFB
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x170094BD RID: 38077
			// (set) Token: 0x0600C439 RID: 50233 RVA: 0x00118B0E File Offset: 0x00116D0E
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x170094BE RID: 38078
			// (set) Token: 0x0600C43A RID: 50234 RVA: 0x00118B26 File Offset: 0x00116D26
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170094BF RID: 38079
			// (set) Token: 0x0600C43B RID: 50235 RVA: 0x00118B39 File Offset: 0x00116D39
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170094C0 RID: 38080
			// (set) Token: 0x0600C43C RID: 50236 RVA: 0x00118B4C File Offset: 0x00116D4C
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170094C1 RID: 38081
			// (set) Token: 0x0600C43D RID: 50237 RVA: 0x00118B5F File Offset: 0x00116D5F
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170094C2 RID: 38082
			// (set) Token: 0x0600C43E RID: 50238 RVA: 0x00118B72 File Offset: 0x00116D72
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170094C3 RID: 38083
			// (set) Token: 0x0600C43F RID: 50239 RVA: 0x00118B85 File Offset: 0x00116D85
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170094C4 RID: 38084
			// (set) Token: 0x0600C440 RID: 50240 RVA: 0x00118B98 File Offset: 0x00116D98
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170094C5 RID: 38085
			// (set) Token: 0x0600C441 RID: 50241 RVA: 0x00118BAB File Offset: 0x00116DAB
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170094C6 RID: 38086
			// (set) Token: 0x0600C442 RID: 50242 RVA: 0x00118BBE File Offset: 0x00116DBE
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170094C7 RID: 38087
			// (set) Token: 0x0600C443 RID: 50243 RVA: 0x00118BD1 File Offset: 0x00116DD1
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170094C8 RID: 38088
			// (set) Token: 0x0600C444 RID: 50244 RVA: 0x00118BE4 File Offset: 0x00116DE4
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170094C9 RID: 38089
			// (set) Token: 0x0600C445 RID: 50245 RVA: 0x00118BF7 File Offset: 0x00116DF7
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170094CA RID: 38090
			// (set) Token: 0x0600C446 RID: 50246 RVA: 0x00118C0A File Offset: 0x00116E0A
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170094CB RID: 38091
			// (set) Token: 0x0600C447 RID: 50247 RVA: 0x00118C1D File Offset: 0x00116E1D
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170094CC RID: 38092
			// (set) Token: 0x0600C448 RID: 50248 RVA: 0x00118C30 File Offset: 0x00116E30
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170094CD RID: 38093
			// (set) Token: 0x0600C449 RID: 50249 RVA: 0x00118C43 File Offset: 0x00116E43
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170094CE RID: 38094
			// (set) Token: 0x0600C44A RID: 50250 RVA: 0x00118C56 File Offset: 0x00116E56
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170094CF RID: 38095
			// (set) Token: 0x0600C44B RID: 50251 RVA: 0x00118C69 File Offset: 0x00116E69
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170094D0 RID: 38096
			// (set) Token: 0x0600C44C RID: 50252 RVA: 0x00118C7C File Offset: 0x00116E7C
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170094D1 RID: 38097
			// (set) Token: 0x0600C44D RID: 50253 RVA: 0x00118C8F File Offset: 0x00116E8F
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170094D2 RID: 38098
			// (set) Token: 0x0600C44E RID: 50254 RVA: 0x00118CA2 File Offset: 0x00116EA2
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170094D3 RID: 38099
			// (set) Token: 0x0600C44F RID: 50255 RVA: 0x00118CB5 File Offset: 0x00116EB5
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170094D4 RID: 38100
			// (set) Token: 0x0600C450 RID: 50256 RVA: 0x00118CC8 File Offset: 0x00116EC8
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170094D5 RID: 38101
			// (set) Token: 0x0600C451 RID: 50257 RVA: 0x00118CDB File Offset: 0x00116EDB
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170094D6 RID: 38102
			// (set) Token: 0x0600C452 RID: 50258 RVA: 0x00118CF3 File Offset: 0x00116EF3
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x170094D7 RID: 38103
			// (set) Token: 0x0600C453 RID: 50259 RVA: 0x00118D06 File Offset: 0x00116F06
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x170094D8 RID: 38104
			// (set) Token: 0x0600C454 RID: 50260 RVA: 0x00118D1E File Offset: 0x00116F1E
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x170094D9 RID: 38105
			// (set) Token: 0x0600C455 RID: 50261 RVA: 0x00118D31 File Offset: 0x00116F31
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x170094DA RID: 38106
			// (set) Token: 0x0600C456 RID: 50262 RVA: 0x00118D49 File Offset: 0x00116F49
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x170094DB RID: 38107
			// (set) Token: 0x0600C457 RID: 50263 RVA: 0x00118D61 File Offset: 0x00116F61
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x170094DC RID: 38108
			// (set) Token: 0x0600C458 RID: 50264 RVA: 0x00118D79 File Offset: 0x00116F79
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x170094DD RID: 38109
			// (set) Token: 0x0600C459 RID: 50265 RVA: 0x00118D91 File Offset: 0x00116F91
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170094DE RID: 38110
			// (set) Token: 0x0600C45A RID: 50266 RVA: 0x00118DA9 File Offset: 0x00116FA9
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x170094DF RID: 38111
			// (set) Token: 0x0600C45B RID: 50267 RVA: 0x00118DC1 File Offset: 0x00116FC1
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x170094E0 RID: 38112
			// (set) Token: 0x0600C45C RID: 50268 RVA: 0x00118DD9 File Offset: 0x00116FD9
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x170094E1 RID: 38113
			// (set) Token: 0x0600C45D RID: 50269 RVA: 0x00118DF1 File Offset: 0x00116FF1
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x170094E2 RID: 38114
			// (set) Token: 0x0600C45E RID: 50270 RVA: 0x00118E09 File Offset: 0x00117009
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x170094E3 RID: 38115
			// (set) Token: 0x0600C45F RID: 50271 RVA: 0x00118E1C File Offset: 0x0011701C
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x170094E4 RID: 38116
			// (set) Token: 0x0600C460 RID: 50272 RVA: 0x00118E2F File Offset: 0x0011702F
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x170094E5 RID: 38117
			// (set) Token: 0x0600C461 RID: 50273 RVA: 0x00118E42 File Offset: 0x00117042
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x170094E6 RID: 38118
			// (set) Token: 0x0600C462 RID: 50274 RVA: 0x00118E55 File Offset: 0x00117055
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x170094E7 RID: 38119
			// (set) Token: 0x0600C463 RID: 50275 RVA: 0x00118E68 File Offset: 0x00117068
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x170094E8 RID: 38120
			// (set) Token: 0x0600C464 RID: 50276 RVA: 0x00118E7B File Offset: 0x0011707B
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x170094E9 RID: 38121
			// (set) Token: 0x0600C465 RID: 50277 RVA: 0x00118E93 File Offset: 0x00117093
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x170094EA RID: 38122
			// (set) Token: 0x0600C466 RID: 50278 RVA: 0x00118EA6 File Offset: 0x001170A6
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x170094EB RID: 38123
			// (set) Token: 0x0600C467 RID: 50279 RVA: 0x00118EB9 File Offset: 0x001170B9
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x170094EC RID: 38124
			// (set) Token: 0x0600C468 RID: 50280 RVA: 0x00118ECC File Offset: 0x001170CC
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x170094ED RID: 38125
			// (set) Token: 0x0600C469 RID: 50281 RVA: 0x00118EEA File Offset: 0x001170EA
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x170094EE RID: 38126
			// (set) Token: 0x0600C46A RID: 50282 RVA: 0x00118EFD File Offset: 0x001170FD
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x170094EF RID: 38127
			// (set) Token: 0x0600C46B RID: 50283 RVA: 0x00118F10 File Offset: 0x00117110
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x170094F0 RID: 38128
			// (set) Token: 0x0600C46C RID: 50284 RVA: 0x00118F23 File Offset: 0x00117123
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x170094F1 RID: 38129
			// (set) Token: 0x0600C46D RID: 50285 RVA: 0x00118F36 File Offset: 0x00117136
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x170094F2 RID: 38130
			// (set) Token: 0x0600C46E RID: 50286 RVA: 0x00118F49 File Offset: 0x00117149
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x170094F3 RID: 38131
			// (set) Token: 0x0600C46F RID: 50287 RVA: 0x00118F5C File Offset: 0x0011715C
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x170094F4 RID: 38132
			// (set) Token: 0x0600C470 RID: 50288 RVA: 0x00118F6F File Offset: 0x0011716F
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x170094F5 RID: 38133
			// (set) Token: 0x0600C471 RID: 50289 RVA: 0x00118F82 File Offset: 0x00117182
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x170094F6 RID: 38134
			// (set) Token: 0x0600C472 RID: 50290 RVA: 0x00118F95 File Offset: 0x00117195
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x170094F7 RID: 38135
			// (set) Token: 0x0600C473 RID: 50291 RVA: 0x00118FA8 File Offset: 0x001171A8
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x170094F8 RID: 38136
			// (set) Token: 0x0600C474 RID: 50292 RVA: 0x00118FBB File Offset: 0x001171BB
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x170094F9 RID: 38137
			// (set) Token: 0x0600C475 RID: 50293 RVA: 0x00118FCE File Offset: 0x001171CE
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x170094FA RID: 38138
			// (set) Token: 0x0600C476 RID: 50294 RVA: 0x00118FE1 File Offset: 0x001171E1
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170094FB RID: 38139
			// (set) Token: 0x0600C477 RID: 50295 RVA: 0x00118FFF File Offset: 0x001171FF
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x170094FC RID: 38140
			// (set) Token: 0x0600C478 RID: 50296 RVA: 0x00119017 File Offset: 0x00117217
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170094FD RID: 38141
			// (set) Token: 0x0600C479 RID: 50297 RVA: 0x0011902F File Offset: 0x0011722F
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x170094FE RID: 38142
			// (set) Token: 0x0600C47A RID: 50298 RVA: 0x00119047 File Offset: 0x00117247
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x170094FF RID: 38143
			// (set) Token: 0x0600C47B RID: 50299 RVA: 0x0011905A File Offset: 0x0011725A
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009500 RID: 38144
			// (set) Token: 0x0600C47C RID: 50300 RVA: 0x0011906D File Offset: 0x0011726D
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009501 RID: 38145
			// (set) Token: 0x0600C47D RID: 50301 RVA: 0x00119085 File Offset: 0x00117285
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009502 RID: 38146
			// (set) Token: 0x0600C47E RID: 50302 RVA: 0x00119098 File Offset: 0x00117298
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009503 RID: 38147
			// (set) Token: 0x0600C47F RID: 50303 RVA: 0x001190B0 File Offset: 0x001172B0
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009504 RID: 38148
			// (set) Token: 0x0600C480 RID: 50304 RVA: 0x001190C8 File Offset: 0x001172C8
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009505 RID: 38149
			// (set) Token: 0x0600C481 RID: 50305 RVA: 0x001190DB File Offset: 0x001172DB
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009506 RID: 38150
			// (set) Token: 0x0600C482 RID: 50306 RVA: 0x001190EE File Offset: 0x001172EE
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009507 RID: 38151
			// (set) Token: 0x0600C483 RID: 50307 RVA: 0x00119101 File Offset: 0x00117301
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009508 RID: 38152
			// (set) Token: 0x0600C484 RID: 50308 RVA: 0x00119114 File Offset: 0x00117314
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009509 RID: 38153
			// (set) Token: 0x0600C485 RID: 50309 RVA: 0x0011912C File Offset: 0x0011732C
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700950A RID: 38154
			// (set) Token: 0x0600C486 RID: 50310 RVA: 0x0011913F File Offset: 0x0011733F
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700950B RID: 38155
			// (set) Token: 0x0600C487 RID: 50311 RVA: 0x00119152 File Offset: 0x00117352
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700950C RID: 38156
			// (set) Token: 0x0600C488 RID: 50312 RVA: 0x0011916A File Offset: 0x0011736A
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700950D RID: 38157
			// (set) Token: 0x0600C489 RID: 50313 RVA: 0x00119182 File Offset: 0x00117382
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700950E RID: 38158
			// (set) Token: 0x0600C48A RID: 50314 RVA: 0x00119195 File Offset: 0x00117395
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x1700950F RID: 38159
			// (set) Token: 0x0600C48B RID: 50315 RVA: 0x001191AD File Offset: 0x001173AD
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009510 RID: 38160
			// (set) Token: 0x0600C48C RID: 50316 RVA: 0x001191C0 File Offset: 0x001173C0
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009511 RID: 38161
			// (set) Token: 0x0600C48D RID: 50317 RVA: 0x001191D3 File Offset: 0x001173D3
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17009512 RID: 38162
			// (set) Token: 0x0600C48E RID: 50318 RVA: 0x001191E6 File Offset: 0x001173E6
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009513 RID: 38163
			// (set) Token: 0x0600C48F RID: 50319 RVA: 0x00119204 File Offset: 0x00117404
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009514 RID: 38164
			// (set) Token: 0x0600C490 RID: 50320 RVA: 0x00119222 File Offset: 0x00117422
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009515 RID: 38165
			// (set) Token: 0x0600C491 RID: 50321 RVA: 0x00119240 File Offset: 0x00117440
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009516 RID: 38166
			// (set) Token: 0x0600C492 RID: 50322 RVA: 0x0011925E File Offset: 0x0011745E
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17009517 RID: 38167
			// (set) Token: 0x0600C493 RID: 50323 RVA: 0x00119271 File Offset: 0x00117471
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009518 RID: 38168
			// (set) Token: 0x0600C494 RID: 50324 RVA: 0x00119289 File Offset: 0x00117489
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009519 RID: 38169
			// (set) Token: 0x0600C495 RID: 50325 RVA: 0x001192A7 File Offset: 0x001174A7
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700951A RID: 38170
			// (set) Token: 0x0600C496 RID: 50326 RVA: 0x001192BF File Offset: 0x001174BF
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700951B RID: 38171
			// (set) Token: 0x0600C497 RID: 50327 RVA: 0x001192D7 File Offset: 0x001174D7
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x1700951C RID: 38172
			// (set) Token: 0x0600C498 RID: 50328 RVA: 0x001192EA File Offset: 0x001174EA
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700951D RID: 38173
			// (set) Token: 0x0600C499 RID: 50329 RVA: 0x00119302 File Offset: 0x00117502
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700951E RID: 38174
			// (set) Token: 0x0600C49A RID: 50330 RVA: 0x00119315 File Offset: 0x00117515
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700951F RID: 38175
			// (set) Token: 0x0600C49B RID: 50331 RVA: 0x00119328 File Offset: 0x00117528
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009520 RID: 38176
			// (set) Token: 0x0600C49C RID: 50332 RVA: 0x0011933B File Offset: 0x0011753B
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009521 RID: 38177
			// (set) Token: 0x0600C49D RID: 50333 RVA: 0x0011934E File Offset: 0x0011754E
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009522 RID: 38178
			// (set) Token: 0x0600C49E RID: 50334 RVA: 0x00119366 File Offset: 0x00117566
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009523 RID: 38179
			// (set) Token: 0x0600C49F RID: 50335 RVA: 0x00119379 File Offset: 0x00117579
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009524 RID: 38180
			// (set) Token: 0x0600C4A0 RID: 50336 RVA: 0x0011938C File Offset: 0x0011758C
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009525 RID: 38181
			// (set) Token: 0x0600C4A1 RID: 50337 RVA: 0x001193A4 File Offset: 0x001175A4
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009526 RID: 38182
			// (set) Token: 0x0600C4A2 RID: 50338 RVA: 0x001193B7 File Offset: 0x001175B7
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009527 RID: 38183
			// (set) Token: 0x0600C4A3 RID: 50339 RVA: 0x001193CF File Offset: 0x001175CF
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009528 RID: 38184
			// (set) Token: 0x0600C4A4 RID: 50340 RVA: 0x001193E2 File Offset: 0x001175E2
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009529 RID: 38185
			// (set) Token: 0x0600C4A5 RID: 50341 RVA: 0x00119400 File Offset: 0x00117600
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700952A RID: 38186
			// (set) Token: 0x0600C4A6 RID: 50342 RVA: 0x00119413 File Offset: 0x00117613
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700952B RID: 38187
			// (set) Token: 0x0600C4A7 RID: 50343 RVA: 0x00119431 File Offset: 0x00117631
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700952C RID: 38188
			// (set) Token: 0x0600C4A8 RID: 50344 RVA: 0x00119444 File Offset: 0x00117644
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700952D RID: 38189
			// (set) Token: 0x0600C4A9 RID: 50345 RVA: 0x0011945C File Offset: 0x0011765C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700952E RID: 38190
			// (set) Token: 0x0600C4AA RID: 50346 RVA: 0x00119474 File Offset: 0x00117674
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700952F RID: 38191
			// (set) Token: 0x0600C4AB RID: 50347 RVA: 0x0011948C File Offset: 0x0011768C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009530 RID: 38192
			// (set) Token: 0x0600C4AC RID: 50348 RVA: 0x001194A4 File Offset: 0x001176A4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D99 RID: 3481
		public class EnableRoomMailboxAccountParameters : ParametersBase
		{
			// Token: 0x17009531 RID: 38193
			// (set) Token: 0x0600C4AE RID: 50350 RVA: 0x001194C4 File Offset: 0x001176C4
			public virtual SecureString RoomMailboxPassword
			{
				set
				{
					base.PowerSharpParameters["RoomMailboxPassword"] = value;
				}
			}

			// Token: 0x17009532 RID: 38194
			// (set) Token: 0x0600C4AF RID: 50351 RVA: 0x001194D7 File Offset: 0x001176D7
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17009533 RID: 38195
			// (set) Token: 0x0600C4B0 RID: 50352 RVA: 0x001194EA File Offset: 0x001176EA
			public virtual SwitchParameter Room
			{
				set
				{
					base.PowerSharpParameters["Room"] = value;
				}
			}

			// Token: 0x17009534 RID: 38196
			// (set) Token: 0x0600C4B1 RID: 50353 RVA: 0x00119502 File Offset: 0x00117702
			public virtual bool EnableRoomMailboxAccount
			{
				set
				{
					base.PowerSharpParameters["EnableRoomMailboxAccount"] = value;
				}
			}

			// Token: 0x17009535 RID: 38197
			// (set) Token: 0x0600C4B2 RID: 50354 RVA: 0x0011951A File Offset: 0x0011771A
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17009536 RID: 38198
			// (set) Token: 0x0600C4B3 RID: 50355 RVA: 0x0011952D File Offset: 0x0011772D
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009537 RID: 38199
			// (set) Token: 0x0600C4B4 RID: 50356 RVA: 0x00119545 File Offset: 0x00117745
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009538 RID: 38200
			// (set) Token: 0x0600C4B5 RID: 50357 RVA: 0x00119558 File Offset: 0x00117758
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009539 RID: 38201
			// (set) Token: 0x0600C4B6 RID: 50358 RVA: 0x0011956B File Offset: 0x0011776B
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700953A RID: 38202
			// (set) Token: 0x0600C4B7 RID: 50359 RVA: 0x0011957E File Offset: 0x0011777E
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700953B RID: 38203
			// (set) Token: 0x0600C4B8 RID: 50360 RVA: 0x00119591 File Offset: 0x00117791
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700953C RID: 38204
			// (set) Token: 0x0600C4B9 RID: 50361 RVA: 0x001195A4 File Offset: 0x001177A4
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700953D RID: 38205
			// (set) Token: 0x0600C4BA RID: 50362 RVA: 0x001195B7 File Offset: 0x001177B7
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x1700953E RID: 38206
			// (set) Token: 0x0600C4BB RID: 50363 RVA: 0x001195CA File Offset: 0x001177CA
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x1700953F RID: 38207
			// (set) Token: 0x0600C4BC RID: 50364 RVA: 0x001195E2 File Offset: 0x001177E2
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009540 RID: 38208
			// (set) Token: 0x0600C4BD RID: 50365 RVA: 0x001195F5 File Offset: 0x001177F5
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009541 RID: 38209
			// (set) Token: 0x0600C4BE RID: 50366 RVA: 0x0011960D File Offset: 0x0011780D
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009542 RID: 38210
			// (set) Token: 0x0600C4BF RID: 50367 RVA: 0x00119620 File Offset: 0x00117820
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009543 RID: 38211
			// (set) Token: 0x0600C4C0 RID: 50368 RVA: 0x00119633 File Offset: 0x00117833
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009544 RID: 38212
			// (set) Token: 0x0600C4C1 RID: 50369 RVA: 0x00119646 File Offset: 0x00117846
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009545 RID: 38213
			// (set) Token: 0x0600C4C2 RID: 50370 RVA: 0x00119659 File Offset: 0x00117859
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009546 RID: 38214
			// (set) Token: 0x0600C4C3 RID: 50371 RVA: 0x0011966C File Offset: 0x0011786C
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009547 RID: 38215
			// (set) Token: 0x0600C4C4 RID: 50372 RVA: 0x0011967F File Offset: 0x0011787F
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009548 RID: 38216
			// (set) Token: 0x0600C4C5 RID: 50373 RVA: 0x00119692 File Offset: 0x00117892
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009549 RID: 38217
			// (set) Token: 0x0600C4C6 RID: 50374 RVA: 0x001196A5 File Offset: 0x001178A5
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x1700954A RID: 38218
			// (set) Token: 0x0600C4C7 RID: 50375 RVA: 0x001196B8 File Offset: 0x001178B8
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x1700954B RID: 38219
			// (set) Token: 0x0600C4C8 RID: 50376 RVA: 0x001196CB File Offset: 0x001178CB
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x1700954C RID: 38220
			// (set) Token: 0x0600C4C9 RID: 50377 RVA: 0x001196DE File Offset: 0x001178DE
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x1700954D RID: 38221
			// (set) Token: 0x0600C4CA RID: 50378 RVA: 0x001196F1 File Offset: 0x001178F1
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x1700954E RID: 38222
			// (set) Token: 0x0600C4CB RID: 50379 RVA: 0x00119704 File Offset: 0x00117904
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x1700954F RID: 38223
			// (set) Token: 0x0600C4CC RID: 50380 RVA: 0x00119717 File Offset: 0x00117917
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009550 RID: 38224
			// (set) Token: 0x0600C4CD RID: 50381 RVA: 0x0011972A File Offset: 0x0011792A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009551 RID: 38225
			// (set) Token: 0x0600C4CE RID: 50382 RVA: 0x0011973D File Offset: 0x0011793D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009552 RID: 38226
			// (set) Token: 0x0600C4CF RID: 50383 RVA: 0x00119750 File Offset: 0x00117950
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009553 RID: 38227
			// (set) Token: 0x0600C4D0 RID: 50384 RVA: 0x00119763 File Offset: 0x00117963
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009554 RID: 38228
			// (set) Token: 0x0600C4D1 RID: 50385 RVA: 0x00119776 File Offset: 0x00117976
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009555 RID: 38229
			// (set) Token: 0x0600C4D2 RID: 50386 RVA: 0x00119789 File Offset: 0x00117989
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009556 RID: 38230
			// (set) Token: 0x0600C4D3 RID: 50387 RVA: 0x0011979C File Offset: 0x0011799C
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009557 RID: 38231
			// (set) Token: 0x0600C4D4 RID: 50388 RVA: 0x001197AF File Offset: 0x001179AF
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009558 RID: 38232
			// (set) Token: 0x0600C4D5 RID: 50389 RVA: 0x001197C2 File Offset: 0x001179C2
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009559 RID: 38233
			// (set) Token: 0x0600C4D6 RID: 50390 RVA: 0x001197DA File Offset: 0x001179DA
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700955A RID: 38234
			// (set) Token: 0x0600C4D7 RID: 50391 RVA: 0x001197ED File Offset: 0x001179ED
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700955B RID: 38235
			// (set) Token: 0x0600C4D8 RID: 50392 RVA: 0x00119805 File Offset: 0x00117A05
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x1700955C RID: 38236
			// (set) Token: 0x0600C4D9 RID: 50393 RVA: 0x00119818 File Offset: 0x00117A18
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x1700955D RID: 38237
			// (set) Token: 0x0600C4DA RID: 50394 RVA: 0x00119830 File Offset: 0x00117A30
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x1700955E RID: 38238
			// (set) Token: 0x0600C4DB RID: 50395 RVA: 0x00119848 File Offset: 0x00117A48
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x1700955F RID: 38239
			// (set) Token: 0x0600C4DC RID: 50396 RVA: 0x00119860 File Offset: 0x00117A60
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17009560 RID: 38240
			// (set) Token: 0x0600C4DD RID: 50397 RVA: 0x00119878 File Offset: 0x00117A78
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17009561 RID: 38241
			// (set) Token: 0x0600C4DE RID: 50398 RVA: 0x00119890 File Offset: 0x00117A90
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17009562 RID: 38242
			// (set) Token: 0x0600C4DF RID: 50399 RVA: 0x001198A8 File Offset: 0x00117AA8
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009563 RID: 38243
			// (set) Token: 0x0600C4E0 RID: 50400 RVA: 0x001198C0 File Offset: 0x00117AC0
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17009564 RID: 38244
			// (set) Token: 0x0600C4E1 RID: 50401 RVA: 0x001198D8 File Offset: 0x00117AD8
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009565 RID: 38245
			// (set) Token: 0x0600C4E2 RID: 50402 RVA: 0x001198F0 File Offset: 0x00117AF0
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009566 RID: 38246
			// (set) Token: 0x0600C4E3 RID: 50403 RVA: 0x00119903 File Offset: 0x00117B03
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009567 RID: 38247
			// (set) Token: 0x0600C4E4 RID: 50404 RVA: 0x00119916 File Offset: 0x00117B16
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009568 RID: 38248
			// (set) Token: 0x0600C4E5 RID: 50405 RVA: 0x00119929 File Offset: 0x00117B29
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009569 RID: 38249
			// (set) Token: 0x0600C4E6 RID: 50406 RVA: 0x0011993C File Offset: 0x00117B3C
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x1700956A RID: 38250
			// (set) Token: 0x0600C4E7 RID: 50407 RVA: 0x0011994F File Offset: 0x00117B4F
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x1700956B RID: 38251
			// (set) Token: 0x0600C4E8 RID: 50408 RVA: 0x00119962 File Offset: 0x00117B62
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x1700956C RID: 38252
			// (set) Token: 0x0600C4E9 RID: 50409 RVA: 0x0011997A File Offset: 0x00117B7A
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700956D RID: 38253
			// (set) Token: 0x0600C4EA RID: 50410 RVA: 0x0011998D File Offset: 0x00117B8D
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700956E RID: 38254
			// (set) Token: 0x0600C4EB RID: 50411 RVA: 0x001199A0 File Offset: 0x00117BA0
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700956F RID: 38255
			// (set) Token: 0x0600C4EC RID: 50412 RVA: 0x001199B3 File Offset: 0x00117BB3
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009570 RID: 38256
			// (set) Token: 0x0600C4ED RID: 50413 RVA: 0x001199D1 File Offset: 0x00117BD1
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009571 RID: 38257
			// (set) Token: 0x0600C4EE RID: 50414 RVA: 0x001199E4 File Offset: 0x00117BE4
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009572 RID: 38258
			// (set) Token: 0x0600C4EF RID: 50415 RVA: 0x001199F7 File Offset: 0x00117BF7
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009573 RID: 38259
			// (set) Token: 0x0600C4F0 RID: 50416 RVA: 0x00119A0A File Offset: 0x00117C0A
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009574 RID: 38260
			// (set) Token: 0x0600C4F1 RID: 50417 RVA: 0x00119A1D File Offset: 0x00117C1D
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009575 RID: 38261
			// (set) Token: 0x0600C4F2 RID: 50418 RVA: 0x00119A30 File Offset: 0x00117C30
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009576 RID: 38262
			// (set) Token: 0x0600C4F3 RID: 50419 RVA: 0x00119A43 File Offset: 0x00117C43
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009577 RID: 38263
			// (set) Token: 0x0600C4F4 RID: 50420 RVA: 0x00119A56 File Offset: 0x00117C56
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009578 RID: 38264
			// (set) Token: 0x0600C4F5 RID: 50421 RVA: 0x00119A69 File Offset: 0x00117C69
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009579 RID: 38265
			// (set) Token: 0x0600C4F6 RID: 50422 RVA: 0x00119A7C File Offset: 0x00117C7C
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700957A RID: 38266
			// (set) Token: 0x0600C4F7 RID: 50423 RVA: 0x00119A8F File Offset: 0x00117C8F
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700957B RID: 38267
			// (set) Token: 0x0600C4F8 RID: 50424 RVA: 0x00119AA2 File Offset: 0x00117CA2
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700957C RID: 38268
			// (set) Token: 0x0600C4F9 RID: 50425 RVA: 0x00119AB5 File Offset: 0x00117CB5
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700957D RID: 38269
			// (set) Token: 0x0600C4FA RID: 50426 RVA: 0x00119AC8 File Offset: 0x00117CC8
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700957E RID: 38270
			// (set) Token: 0x0600C4FB RID: 50427 RVA: 0x00119AE6 File Offset: 0x00117CE6
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x1700957F RID: 38271
			// (set) Token: 0x0600C4FC RID: 50428 RVA: 0x00119AFE File Offset: 0x00117CFE
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009580 RID: 38272
			// (set) Token: 0x0600C4FD RID: 50429 RVA: 0x00119B16 File Offset: 0x00117D16
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009581 RID: 38273
			// (set) Token: 0x0600C4FE RID: 50430 RVA: 0x00119B2E File Offset: 0x00117D2E
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009582 RID: 38274
			// (set) Token: 0x0600C4FF RID: 50431 RVA: 0x00119B41 File Offset: 0x00117D41
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009583 RID: 38275
			// (set) Token: 0x0600C500 RID: 50432 RVA: 0x00119B54 File Offset: 0x00117D54
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009584 RID: 38276
			// (set) Token: 0x0600C501 RID: 50433 RVA: 0x00119B6C File Offset: 0x00117D6C
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009585 RID: 38277
			// (set) Token: 0x0600C502 RID: 50434 RVA: 0x00119B7F File Offset: 0x00117D7F
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009586 RID: 38278
			// (set) Token: 0x0600C503 RID: 50435 RVA: 0x00119B97 File Offset: 0x00117D97
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009587 RID: 38279
			// (set) Token: 0x0600C504 RID: 50436 RVA: 0x00119BAF File Offset: 0x00117DAF
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009588 RID: 38280
			// (set) Token: 0x0600C505 RID: 50437 RVA: 0x00119BC2 File Offset: 0x00117DC2
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009589 RID: 38281
			// (set) Token: 0x0600C506 RID: 50438 RVA: 0x00119BD5 File Offset: 0x00117DD5
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700958A RID: 38282
			// (set) Token: 0x0600C507 RID: 50439 RVA: 0x00119BE8 File Offset: 0x00117DE8
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700958B RID: 38283
			// (set) Token: 0x0600C508 RID: 50440 RVA: 0x00119BFB File Offset: 0x00117DFB
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700958C RID: 38284
			// (set) Token: 0x0600C509 RID: 50441 RVA: 0x00119C13 File Offset: 0x00117E13
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700958D RID: 38285
			// (set) Token: 0x0600C50A RID: 50442 RVA: 0x00119C26 File Offset: 0x00117E26
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700958E RID: 38286
			// (set) Token: 0x0600C50B RID: 50443 RVA: 0x00119C39 File Offset: 0x00117E39
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700958F RID: 38287
			// (set) Token: 0x0600C50C RID: 50444 RVA: 0x00119C51 File Offset: 0x00117E51
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009590 RID: 38288
			// (set) Token: 0x0600C50D RID: 50445 RVA: 0x00119C69 File Offset: 0x00117E69
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009591 RID: 38289
			// (set) Token: 0x0600C50E RID: 50446 RVA: 0x00119C7C File Offset: 0x00117E7C
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17009592 RID: 38290
			// (set) Token: 0x0600C50F RID: 50447 RVA: 0x00119C94 File Offset: 0x00117E94
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009593 RID: 38291
			// (set) Token: 0x0600C510 RID: 50448 RVA: 0x00119CA7 File Offset: 0x00117EA7
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009594 RID: 38292
			// (set) Token: 0x0600C511 RID: 50449 RVA: 0x00119CBA File Offset: 0x00117EBA
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17009595 RID: 38293
			// (set) Token: 0x0600C512 RID: 50450 RVA: 0x00119CCD File Offset: 0x00117ECD
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009596 RID: 38294
			// (set) Token: 0x0600C513 RID: 50451 RVA: 0x00119CEB File Offset: 0x00117EEB
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009597 RID: 38295
			// (set) Token: 0x0600C514 RID: 50452 RVA: 0x00119D09 File Offset: 0x00117F09
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009598 RID: 38296
			// (set) Token: 0x0600C515 RID: 50453 RVA: 0x00119D27 File Offset: 0x00117F27
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009599 RID: 38297
			// (set) Token: 0x0600C516 RID: 50454 RVA: 0x00119D45 File Offset: 0x00117F45
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700959A RID: 38298
			// (set) Token: 0x0600C517 RID: 50455 RVA: 0x00119D58 File Offset: 0x00117F58
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700959B RID: 38299
			// (set) Token: 0x0600C518 RID: 50456 RVA: 0x00119D70 File Offset: 0x00117F70
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700959C RID: 38300
			// (set) Token: 0x0600C519 RID: 50457 RVA: 0x00119D8E File Offset: 0x00117F8E
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700959D RID: 38301
			// (set) Token: 0x0600C51A RID: 50458 RVA: 0x00119DA6 File Offset: 0x00117FA6
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700959E RID: 38302
			// (set) Token: 0x0600C51B RID: 50459 RVA: 0x00119DBE File Offset: 0x00117FBE
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x1700959F RID: 38303
			// (set) Token: 0x0600C51C RID: 50460 RVA: 0x00119DD1 File Offset: 0x00117FD1
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170095A0 RID: 38304
			// (set) Token: 0x0600C51D RID: 50461 RVA: 0x00119DE9 File Offset: 0x00117FE9
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170095A1 RID: 38305
			// (set) Token: 0x0600C51E RID: 50462 RVA: 0x00119DFC File Offset: 0x00117FFC
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170095A2 RID: 38306
			// (set) Token: 0x0600C51F RID: 50463 RVA: 0x00119E0F File Offset: 0x0011800F
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170095A3 RID: 38307
			// (set) Token: 0x0600C520 RID: 50464 RVA: 0x00119E22 File Offset: 0x00118022
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170095A4 RID: 38308
			// (set) Token: 0x0600C521 RID: 50465 RVA: 0x00119E35 File Offset: 0x00118035
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170095A5 RID: 38309
			// (set) Token: 0x0600C522 RID: 50466 RVA: 0x00119E4D File Offset: 0x0011804D
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170095A6 RID: 38310
			// (set) Token: 0x0600C523 RID: 50467 RVA: 0x00119E60 File Offset: 0x00118060
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170095A7 RID: 38311
			// (set) Token: 0x0600C524 RID: 50468 RVA: 0x00119E73 File Offset: 0x00118073
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170095A8 RID: 38312
			// (set) Token: 0x0600C525 RID: 50469 RVA: 0x00119E8B File Offset: 0x0011808B
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170095A9 RID: 38313
			// (set) Token: 0x0600C526 RID: 50470 RVA: 0x00119E9E File Offset: 0x0011809E
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170095AA RID: 38314
			// (set) Token: 0x0600C527 RID: 50471 RVA: 0x00119EB6 File Offset: 0x001180B6
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170095AB RID: 38315
			// (set) Token: 0x0600C528 RID: 50472 RVA: 0x00119EC9 File Offset: 0x001180C9
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170095AC RID: 38316
			// (set) Token: 0x0600C529 RID: 50473 RVA: 0x00119EE7 File Offset: 0x001180E7
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170095AD RID: 38317
			// (set) Token: 0x0600C52A RID: 50474 RVA: 0x00119EFA File Offset: 0x001180FA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170095AE RID: 38318
			// (set) Token: 0x0600C52B RID: 50475 RVA: 0x00119F18 File Offset: 0x00118118
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170095AF RID: 38319
			// (set) Token: 0x0600C52C RID: 50476 RVA: 0x00119F2B File Offset: 0x0011812B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170095B0 RID: 38320
			// (set) Token: 0x0600C52D RID: 50477 RVA: 0x00119F43 File Offset: 0x00118143
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170095B1 RID: 38321
			// (set) Token: 0x0600C52E RID: 50478 RVA: 0x00119F5B File Offset: 0x0011815B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170095B2 RID: 38322
			// (set) Token: 0x0600C52F RID: 50479 RVA: 0x00119F73 File Offset: 0x00118173
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170095B3 RID: 38323
			// (set) Token: 0x0600C530 RID: 50480 RVA: 0x00119F8B File Offset: 0x0011818B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D9A RID: 3482
		public class AuditLogParameters : ParametersBase
		{
			// Token: 0x170095B4 RID: 38324
			// (set) Token: 0x0600C532 RID: 50482 RVA: 0x00119FAB File Offset: 0x001181AB
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170095B5 RID: 38325
			// (set) Token: 0x0600C533 RID: 50483 RVA: 0x00119FBE File Offset: 0x001181BE
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170095B6 RID: 38326
			// (set) Token: 0x0600C534 RID: 50484 RVA: 0x00119FD6 File Offset: 0x001181D6
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x170095B7 RID: 38327
			// (set) Token: 0x0600C535 RID: 50485 RVA: 0x00119FEE File Offset: 0x001181EE
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x170095B8 RID: 38328
			// (set) Token: 0x0600C536 RID: 50486 RVA: 0x0011A001 File Offset: 0x00118201
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x170095B9 RID: 38329
			// (set) Token: 0x0600C537 RID: 50487 RVA: 0x0011A014 File Offset: 0x00118214
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x170095BA RID: 38330
			// (set) Token: 0x0600C538 RID: 50488 RVA: 0x0011A027 File Offset: 0x00118227
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x170095BB RID: 38331
			// (set) Token: 0x0600C539 RID: 50489 RVA: 0x0011A03A File Offset: 0x0011823A
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170095BC RID: 38332
			// (set) Token: 0x0600C53A RID: 50490 RVA: 0x0011A04D File Offset: 0x0011824D
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170095BD RID: 38333
			// (set) Token: 0x0600C53B RID: 50491 RVA: 0x0011A060 File Offset: 0x00118260
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x170095BE RID: 38334
			// (set) Token: 0x0600C53C RID: 50492 RVA: 0x0011A073 File Offset: 0x00118273
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x170095BF RID: 38335
			// (set) Token: 0x0600C53D RID: 50493 RVA: 0x0011A08B File Offset: 0x0011828B
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x170095C0 RID: 38336
			// (set) Token: 0x0600C53E RID: 50494 RVA: 0x0011A09E File Offset: 0x0011829E
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x170095C1 RID: 38337
			// (set) Token: 0x0600C53F RID: 50495 RVA: 0x0011A0B6 File Offset: 0x001182B6
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170095C2 RID: 38338
			// (set) Token: 0x0600C540 RID: 50496 RVA: 0x0011A0C9 File Offset: 0x001182C9
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170095C3 RID: 38339
			// (set) Token: 0x0600C541 RID: 50497 RVA: 0x0011A0DC File Offset: 0x001182DC
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170095C4 RID: 38340
			// (set) Token: 0x0600C542 RID: 50498 RVA: 0x0011A0EF File Offset: 0x001182EF
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170095C5 RID: 38341
			// (set) Token: 0x0600C543 RID: 50499 RVA: 0x0011A102 File Offset: 0x00118302
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170095C6 RID: 38342
			// (set) Token: 0x0600C544 RID: 50500 RVA: 0x0011A115 File Offset: 0x00118315
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170095C7 RID: 38343
			// (set) Token: 0x0600C545 RID: 50501 RVA: 0x0011A128 File Offset: 0x00118328
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170095C8 RID: 38344
			// (set) Token: 0x0600C546 RID: 50502 RVA: 0x0011A13B File Offset: 0x0011833B
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170095C9 RID: 38345
			// (set) Token: 0x0600C547 RID: 50503 RVA: 0x0011A14E File Offset: 0x0011834E
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170095CA RID: 38346
			// (set) Token: 0x0600C548 RID: 50504 RVA: 0x0011A161 File Offset: 0x00118361
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170095CB RID: 38347
			// (set) Token: 0x0600C549 RID: 50505 RVA: 0x0011A174 File Offset: 0x00118374
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170095CC RID: 38348
			// (set) Token: 0x0600C54A RID: 50506 RVA: 0x0011A187 File Offset: 0x00118387
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170095CD RID: 38349
			// (set) Token: 0x0600C54B RID: 50507 RVA: 0x0011A19A File Offset: 0x0011839A
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170095CE RID: 38350
			// (set) Token: 0x0600C54C RID: 50508 RVA: 0x0011A1AD File Offset: 0x001183AD
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170095CF RID: 38351
			// (set) Token: 0x0600C54D RID: 50509 RVA: 0x0011A1C0 File Offset: 0x001183C0
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170095D0 RID: 38352
			// (set) Token: 0x0600C54E RID: 50510 RVA: 0x0011A1D3 File Offset: 0x001183D3
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170095D1 RID: 38353
			// (set) Token: 0x0600C54F RID: 50511 RVA: 0x0011A1E6 File Offset: 0x001183E6
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170095D2 RID: 38354
			// (set) Token: 0x0600C550 RID: 50512 RVA: 0x0011A1F9 File Offset: 0x001183F9
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170095D3 RID: 38355
			// (set) Token: 0x0600C551 RID: 50513 RVA: 0x0011A20C File Offset: 0x0011840C
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170095D4 RID: 38356
			// (set) Token: 0x0600C552 RID: 50514 RVA: 0x0011A21F File Offset: 0x0011841F
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170095D5 RID: 38357
			// (set) Token: 0x0600C553 RID: 50515 RVA: 0x0011A232 File Offset: 0x00118432
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170095D6 RID: 38358
			// (set) Token: 0x0600C554 RID: 50516 RVA: 0x0011A245 File Offset: 0x00118445
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170095D7 RID: 38359
			// (set) Token: 0x0600C555 RID: 50517 RVA: 0x0011A258 File Offset: 0x00118458
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170095D8 RID: 38360
			// (set) Token: 0x0600C556 RID: 50518 RVA: 0x0011A26B File Offset: 0x0011846B
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170095D9 RID: 38361
			// (set) Token: 0x0600C557 RID: 50519 RVA: 0x0011A283 File Offset: 0x00118483
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x170095DA RID: 38362
			// (set) Token: 0x0600C558 RID: 50520 RVA: 0x0011A296 File Offset: 0x00118496
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x170095DB RID: 38363
			// (set) Token: 0x0600C559 RID: 50521 RVA: 0x0011A2AE File Offset: 0x001184AE
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x170095DC RID: 38364
			// (set) Token: 0x0600C55A RID: 50522 RVA: 0x0011A2C1 File Offset: 0x001184C1
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x170095DD RID: 38365
			// (set) Token: 0x0600C55B RID: 50523 RVA: 0x0011A2D9 File Offset: 0x001184D9
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x170095DE RID: 38366
			// (set) Token: 0x0600C55C RID: 50524 RVA: 0x0011A2F1 File Offset: 0x001184F1
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x170095DF RID: 38367
			// (set) Token: 0x0600C55D RID: 50525 RVA: 0x0011A309 File Offset: 0x00118509
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x170095E0 RID: 38368
			// (set) Token: 0x0600C55E RID: 50526 RVA: 0x0011A321 File Offset: 0x00118521
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170095E1 RID: 38369
			// (set) Token: 0x0600C55F RID: 50527 RVA: 0x0011A339 File Offset: 0x00118539
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x170095E2 RID: 38370
			// (set) Token: 0x0600C560 RID: 50528 RVA: 0x0011A351 File Offset: 0x00118551
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x170095E3 RID: 38371
			// (set) Token: 0x0600C561 RID: 50529 RVA: 0x0011A369 File Offset: 0x00118569
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x170095E4 RID: 38372
			// (set) Token: 0x0600C562 RID: 50530 RVA: 0x0011A381 File Offset: 0x00118581
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x170095E5 RID: 38373
			// (set) Token: 0x0600C563 RID: 50531 RVA: 0x0011A399 File Offset: 0x00118599
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x170095E6 RID: 38374
			// (set) Token: 0x0600C564 RID: 50532 RVA: 0x0011A3AC File Offset: 0x001185AC
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x170095E7 RID: 38375
			// (set) Token: 0x0600C565 RID: 50533 RVA: 0x0011A3BF File Offset: 0x001185BF
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x170095E8 RID: 38376
			// (set) Token: 0x0600C566 RID: 50534 RVA: 0x0011A3D2 File Offset: 0x001185D2
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x170095E9 RID: 38377
			// (set) Token: 0x0600C567 RID: 50535 RVA: 0x0011A3E5 File Offset: 0x001185E5
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x170095EA RID: 38378
			// (set) Token: 0x0600C568 RID: 50536 RVA: 0x0011A3F8 File Offset: 0x001185F8
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x170095EB RID: 38379
			// (set) Token: 0x0600C569 RID: 50537 RVA: 0x0011A40B File Offset: 0x0011860B
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x170095EC RID: 38380
			// (set) Token: 0x0600C56A RID: 50538 RVA: 0x0011A423 File Offset: 0x00118623
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x170095ED RID: 38381
			// (set) Token: 0x0600C56B RID: 50539 RVA: 0x0011A436 File Offset: 0x00118636
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x170095EE RID: 38382
			// (set) Token: 0x0600C56C RID: 50540 RVA: 0x0011A449 File Offset: 0x00118649
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x170095EF RID: 38383
			// (set) Token: 0x0600C56D RID: 50541 RVA: 0x0011A45C File Offset: 0x0011865C
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x170095F0 RID: 38384
			// (set) Token: 0x0600C56E RID: 50542 RVA: 0x0011A47A File Offset: 0x0011867A
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x170095F1 RID: 38385
			// (set) Token: 0x0600C56F RID: 50543 RVA: 0x0011A48D File Offset: 0x0011868D
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x170095F2 RID: 38386
			// (set) Token: 0x0600C570 RID: 50544 RVA: 0x0011A4A0 File Offset: 0x001186A0
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x170095F3 RID: 38387
			// (set) Token: 0x0600C571 RID: 50545 RVA: 0x0011A4B3 File Offset: 0x001186B3
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x170095F4 RID: 38388
			// (set) Token: 0x0600C572 RID: 50546 RVA: 0x0011A4C6 File Offset: 0x001186C6
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x170095F5 RID: 38389
			// (set) Token: 0x0600C573 RID: 50547 RVA: 0x0011A4D9 File Offset: 0x001186D9
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x170095F6 RID: 38390
			// (set) Token: 0x0600C574 RID: 50548 RVA: 0x0011A4EC File Offset: 0x001186EC
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x170095F7 RID: 38391
			// (set) Token: 0x0600C575 RID: 50549 RVA: 0x0011A4FF File Offset: 0x001186FF
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x170095F8 RID: 38392
			// (set) Token: 0x0600C576 RID: 50550 RVA: 0x0011A512 File Offset: 0x00118712
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x170095F9 RID: 38393
			// (set) Token: 0x0600C577 RID: 50551 RVA: 0x0011A525 File Offset: 0x00118725
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x170095FA RID: 38394
			// (set) Token: 0x0600C578 RID: 50552 RVA: 0x0011A538 File Offset: 0x00118738
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x170095FB RID: 38395
			// (set) Token: 0x0600C579 RID: 50553 RVA: 0x0011A54B File Offset: 0x0011874B
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x170095FC RID: 38396
			// (set) Token: 0x0600C57A RID: 50554 RVA: 0x0011A55E File Offset: 0x0011875E
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x170095FD RID: 38397
			// (set) Token: 0x0600C57B RID: 50555 RVA: 0x0011A571 File Offset: 0x00118771
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170095FE RID: 38398
			// (set) Token: 0x0600C57C RID: 50556 RVA: 0x0011A58F File Offset: 0x0011878F
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x170095FF RID: 38399
			// (set) Token: 0x0600C57D RID: 50557 RVA: 0x0011A5A7 File Offset: 0x001187A7
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009600 RID: 38400
			// (set) Token: 0x0600C57E RID: 50558 RVA: 0x0011A5BF File Offset: 0x001187BF
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009601 RID: 38401
			// (set) Token: 0x0600C57F RID: 50559 RVA: 0x0011A5D7 File Offset: 0x001187D7
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009602 RID: 38402
			// (set) Token: 0x0600C580 RID: 50560 RVA: 0x0011A5EA File Offset: 0x001187EA
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009603 RID: 38403
			// (set) Token: 0x0600C581 RID: 50561 RVA: 0x0011A5FD File Offset: 0x001187FD
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009604 RID: 38404
			// (set) Token: 0x0600C582 RID: 50562 RVA: 0x0011A615 File Offset: 0x00118815
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009605 RID: 38405
			// (set) Token: 0x0600C583 RID: 50563 RVA: 0x0011A628 File Offset: 0x00118828
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009606 RID: 38406
			// (set) Token: 0x0600C584 RID: 50564 RVA: 0x0011A640 File Offset: 0x00118840
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009607 RID: 38407
			// (set) Token: 0x0600C585 RID: 50565 RVA: 0x0011A658 File Offset: 0x00118858
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009608 RID: 38408
			// (set) Token: 0x0600C586 RID: 50566 RVA: 0x0011A66B File Offset: 0x0011886B
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009609 RID: 38409
			// (set) Token: 0x0600C587 RID: 50567 RVA: 0x0011A67E File Offset: 0x0011887E
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700960A RID: 38410
			// (set) Token: 0x0600C588 RID: 50568 RVA: 0x0011A691 File Offset: 0x00118891
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700960B RID: 38411
			// (set) Token: 0x0600C589 RID: 50569 RVA: 0x0011A6A4 File Offset: 0x001188A4
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700960C RID: 38412
			// (set) Token: 0x0600C58A RID: 50570 RVA: 0x0011A6BC File Offset: 0x001188BC
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700960D RID: 38413
			// (set) Token: 0x0600C58B RID: 50571 RVA: 0x0011A6CF File Offset: 0x001188CF
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700960E RID: 38414
			// (set) Token: 0x0600C58C RID: 50572 RVA: 0x0011A6E2 File Offset: 0x001188E2
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700960F RID: 38415
			// (set) Token: 0x0600C58D RID: 50573 RVA: 0x0011A6FA File Offset: 0x001188FA
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009610 RID: 38416
			// (set) Token: 0x0600C58E RID: 50574 RVA: 0x0011A712 File Offset: 0x00118912
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009611 RID: 38417
			// (set) Token: 0x0600C58F RID: 50575 RVA: 0x0011A725 File Offset: 0x00118925
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17009612 RID: 38418
			// (set) Token: 0x0600C590 RID: 50576 RVA: 0x0011A73D File Offset: 0x0011893D
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009613 RID: 38419
			// (set) Token: 0x0600C591 RID: 50577 RVA: 0x0011A750 File Offset: 0x00118950
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009614 RID: 38420
			// (set) Token: 0x0600C592 RID: 50578 RVA: 0x0011A763 File Offset: 0x00118963
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17009615 RID: 38421
			// (set) Token: 0x0600C593 RID: 50579 RVA: 0x0011A776 File Offset: 0x00118976
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009616 RID: 38422
			// (set) Token: 0x0600C594 RID: 50580 RVA: 0x0011A794 File Offset: 0x00118994
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009617 RID: 38423
			// (set) Token: 0x0600C595 RID: 50581 RVA: 0x0011A7B2 File Offset: 0x001189B2
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009618 RID: 38424
			// (set) Token: 0x0600C596 RID: 50582 RVA: 0x0011A7D0 File Offset: 0x001189D0
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009619 RID: 38425
			// (set) Token: 0x0600C597 RID: 50583 RVA: 0x0011A7EE File Offset: 0x001189EE
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700961A RID: 38426
			// (set) Token: 0x0600C598 RID: 50584 RVA: 0x0011A801 File Offset: 0x00118A01
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700961B RID: 38427
			// (set) Token: 0x0600C599 RID: 50585 RVA: 0x0011A819 File Offset: 0x00118A19
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700961C RID: 38428
			// (set) Token: 0x0600C59A RID: 50586 RVA: 0x0011A837 File Offset: 0x00118A37
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700961D RID: 38429
			// (set) Token: 0x0600C59B RID: 50587 RVA: 0x0011A84F File Offset: 0x00118A4F
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700961E RID: 38430
			// (set) Token: 0x0600C59C RID: 50588 RVA: 0x0011A867 File Offset: 0x00118A67
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x1700961F RID: 38431
			// (set) Token: 0x0600C59D RID: 50589 RVA: 0x0011A87A File Offset: 0x00118A7A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17009620 RID: 38432
			// (set) Token: 0x0600C59E RID: 50590 RVA: 0x0011A892 File Offset: 0x00118A92
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009621 RID: 38433
			// (set) Token: 0x0600C59F RID: 50591 RVA: 0x0011A8A5 File Offset: 0x00118AA5
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009622 RID: 38434
			// (set) Token: 0x0600C5A0 RID: 50592 RVA: 0x0011A8B8 File Offset: 0x00118AB8
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009623 RID: 38435
			// (set) Token: 0x0600C5A1 RID: 50593 RVA: 0x0011A8CB File Offset: 0x00118ACB
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009624 RID: 38436
			// (set) Token: 0x0600C5A2 RID: 50594 RVA: 0x0011A8DE File Offset: 0x00118ADE
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009625 RID: 38437
			// (set) Token: 0x0600C5A3 RID: 50595 RVA: 0x0011A8F6 File Offset: 0x00118AF6
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009626 RID: 38438
			// (set) Token: 0x0600C5A4 RID: 50596 RVA: 0x0011A909 File Offset: 0x00118B09
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009627 RID: 38439
			// (set) Token: 0x0600C5A5 RID: 50597 RVA: 0x0011A91C File Offset: 0x00118B1C
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009628 RID: 38440
			// (set) Token: 0x0600C5A6 RID: 50598 RVA: 0x0011A934 File Offset: 0x00118B34
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009629 RID: 38441
			// (set) Token: 0x0600C5A7 RID: 50599 RVA: 0x0011A947 File Offset: 0x00118B47
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700962A RID: 38442
			// (set) Token: 0x0600C5A8 RID: 50600 RVA: 0x0011A95F File Offset: 0x00118B5F
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700962B RID: 38443
			// (set) Token: 0x0600C5A9 RID: 50601 RVA: 0x0011A972 File Offset: 0x00118B72
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700962C RID: 38444
			// (set) Token: 0x0600C5AA RID: 50602 RVA: 0x0011A990 File Offset: 0x00118B90
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700962D RID: 38445
			// (set) Token: 0x0600C5AB RID: 50603 RVA: 0x0011A9A3 File Offset: 0x00118BA3
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700962E RID: 38446
			// (set) Token: 0x0600C5AC RID: 50604 RVA: 0x0011A9C1 File Offset: 0x00118BC1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700962F RID: 38447
			// (set) Token: 0x0600C5AD RID: 50605 RVA: 0x0011A9D4 File Offset: 0x00118BD4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009630 RID: 38448
			// (set) Token: 0x0600C5AE RID: 50606 RVA: 0x0011A9EC File Offset: 0x00118BEC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009631 RID: 38449
			// (set) Token: 0x0600C5AF RID: 50607 RVA: 0x0011AA04 File Offset: 0x00118C04
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009632 RID: 38450
			// (set) Token: 0x0600C5B0 RID: 50608 RVA: 0x0011AA1C File Offset: 0x00118C1C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009633 RID: 38451
			// (set) Token: 0x0600C5B1 RID: 50609 RVA: 0x0011AA34 File Offset: 0x00118C34
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D9B RID: 3483
		public class GroupMailboxParameters : ParametersBase
		{
			// Token: 0x17009634 RID: 38452
			// (set) Token: 0x0600C5B3 RID: 50611 RVA: 0x0011AA54 File Offset: 0x00118C54
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009635 RID: 38453
			// (set) Token: 0x0600C5B4 RID: 50612 RVA: 0x0011AA72 File Offset: 0x00118C72
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009636 RID: 38454
			// (set) Token: 0x0600C5B5 RID: 50613 RVA: 0x0011AA85 File Offset: 0x00118C85
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009637 RID: 38455
			// (set) Token: 0x0600C5B6 RID: 50614 RVA: 0x0011AA9D File Offset: 0x00118C9D
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009638 RID: 38456
			// (set) Token: 0x0600C5B7 RID: 50615 RVA: 0x0011AAB5 File Offset: 0x00118CB5
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009639 RID: 38457
			// (set) Token: 0x0600C5B8 RID: 50616 RVA: 0x0011AAC8 File Offset: 0x00118CC8
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700963A RID: 38458
			// (set) Token: 0x0600C5B9 RID: 50617 RVA: 0x0011AADB File Offset: 0x00118CDB
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700963B RID: 38459
			// (set) Token: 0x0600C5BA RID: 50618 RVA: 0x0011AAEE File Offset: 0x00118CEE
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700963C RID: 38460
			// (set) Token: 0x0600C5BB RID: 50619 RVA: 0x0011AB01 File Offset: 0x00118D01
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700963D RID: 38461
			// (set) Token: 0x0600C5BC RID: 50620 RVA: 0x0011AB14 File Offset: 0x00118D14
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700963E RID: 38462
			// (set) Token: 0x0600C5BD RID: 50621 RVA: 0x0011AB27 File Offset: 0x00118D27
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x1700963F RID: 38463
			// (set) Token: 0x0600C5BE RID: 50622 RVA: 0x0011AB3A File Offset: 0x00118D3A
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17009640 RID: 38464
			// (set) Token: 0x0600C5BF RID: 50623 RVA: 0x0011AB52 File Offset: 0x00118D52
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009641 RID: 38465
			// (set) Token: 0x0600C5C0 RID: 50624 RVA: 0x0011AB65 File Offset: 0x00118D65
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009642 RID: 38466
			// (set) Token: 0x0600C5C1 RID: 50625 RVA: 0x0011AB7D File Offset: 0x00118D7D
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009643 RID: 38467
			// (set) Token: 0x0600C5C2 RID: 50626 RVA: 0x0011AB90 File Offset: 0x00118D90
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009644 RID: 38468
			// (set) Token: 0x0600C5C3 RID: 50627 RVA: 0x0011ABA3 File Offset: 0x00118DA3
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009645 RID: 38469
			// (set) Token: 0x0600C5C4 RID: 50628 RVA: 0x0011ABB6 File Offset: 0x00118DB6
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009646 RID: 38470
			// (set) Token: 0x0600C5C5 RID: 50629 RVA: 0x0011ABC9 File Offset: 0x00118DC9
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009647 RID: 38471
			// (set) Token: 0x0600C5C6 RID: 50630 RVA: 0x0011ABDC File Offset: 0x00118DDC
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009648 RID: 38472
			// (set) Token: 0x0600C5C7 RID: 50631 RVA: 0x0011ABEF File Offset: 0x00118DEF
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009649 RID: 38473
			// (set) Token: 0x0600C5C8 RID: 50632 RVA: 0x0011AC02 File Offset: 0x00118E02
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x1700964A RID: 38474
			// (set) Token: 0x0600C5C9 RID: 50633 RVA: 0x0011AC15 File Offset: 0x00118E15
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x1700964B RID: 38475
			// (set) Token: 0x0600C5CA RID: 50634 RVA: 0x0011AC28 File Offset: 0x00118E28
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x1700964C RID: 38476
			// (set) Token: 0x0600C5CB RID: 50635 RVA: 0x0011AC3B File Offset: 0x00118E3B
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x1700964D RID: 38477
			// (set) Token: 0x0600C5CC RID: 50636 RVA: 0x0011AC4E File Offset: 0x00118E4E
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x1700964E RID: 38478
			// (set) Token: 0x0600C5CD RID: 50637 RVA: 0x0011AC61 File Offset: 0x00118E61
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x1700964F RID: 38479
			// (set) Token: 0x0600C5CE RID: 50638 RVA: 0x0011AC74 File Offset: 0x00118E74
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009650 RID: 38480
			// (set) Token: 0x0600C5CF RID: 50639 RVA: 0x0011AC87 File Offset: 0x00118E87
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009651 RID: 38481
			// (set) Token: 0x0600C5D0 RID: 50640 RVA: 0x0011AC9A File Offset: 0x00118E9A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009652 RID: 38482
			// (set) Token: 0x0600C5D1 RID: 50641 RVA: 0x0011ACAD File Offset: 0x00118EAD
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009653 RID: 38483
			// (set) Token: 0x0600C5D2 RID: 50642 RVA: 0x0011ACC0 File Offset: 0x00118EC0
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009654 RID: 38484
			// (set) Token: 0x0600C5D3 RID: 50643 RVA: 0x0011ACD3 File Offset: 0x00118ED3
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009655 RID: 38485
			// (set) Token: 0x0600C5D4 RID: 50644 RVA: 0x0011ACE6 File Offset: 0x00118EE6
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009656 RID: 38486
			// (set) Token: 0x0600C5D5 RID: 50645 RVA: 0x0011ACF9 File Offset: 0x00118EF9
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009657 RID: 38487
			// (set) Token: 0x0600C5D6 RID: 50646 RVA: 0x0011AD0C File Offset: 0x00118F0C
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009658 RID: 38488
			// (set) Token: 0x0600C5D7 RID: 50647 RVA: 0x0011AD1F File Offset: 0x00118F1F
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009659 RID: 38489
			// (set) Token: 0x0600C5D8 RID: 50648 RVA: 0x0011AD32 File Offset: 0x00118F32
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x1700965A RID: 38490
			// (set) Token: 0x0600C5D9 RID: 50649 RVA: 0x0011AD4A File Offset: 0x00118F4A
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700965B RID: 38491
			// (set) Token: 0x0600C5DA RID: 50650 RVA: 0x0011AD5D File Offset: 0x00118F5D
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700965C RID: 38492
			// (set) Token: 0x0600C5DB RID: 50651 RVA: 0x0011AD75 File Offset: 0x00118F75
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x1700965D RID: 38493
			// (set) Token: 0x0600C5DC RID: 50652 RVA: 0x0011AD88 File Offset: 0x00118F88
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x1700965E RID: 38494
			// (set) Token: 0x0600C5DD RID: 50653 RVA: 0x0011ADA0 File Offset: 0x00118FA0
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x1700965F RID: 38495
			// (set) Token: 0x0600C5DE RID: 50654 RVA: 0x0011ADB8 File Offset: 0x00118FB8
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17009660 RID: 38496
			// (set) Token: 0x0600C5DF RID: 50655 RVA: 0x0011ADD0 File Offset: 0x00118FD0
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17009661 RID: 38497
			// (set) Token: 0x0600C5E0 RID: 50656 RVA: 0x0011ADE8 File Offset: 0x00118FE8
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17009662 RID: 38498
			// (set) Token: 0x0600C5E1 RID: 50657 RVA: 0x0011AE00 File Offset: 0x00119000
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17009663 RID: 38499
			// (set) Token: 0x0600C5E2 RID: 50658 RVA: 0x0011AE18 File Offset: 0x00119018
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009664 RID: 38500
			// (set) Token: 0x0600C5E3 RID: 50659 RVA: 0x0011AE30 File Offset: 0x00119030
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17009665 RID: 38501
			// (set) Token: 0x0600C5E4 RID: 50660 RVA: 0x0011AE48 File Offset: 0x00119048
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009666 RID: 38502
			// (set) Token: 0x0600C5E5 RID: 50661 RVA: 0x0011AE60 File Offset: 0x00119060
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009667 RID: 38503
			// (set) Token: 0x0600C5E6 RID: 50662 RVA: 0x0011AE73 File Offset: 0x00119073
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009668 RID: 38504
			// (set) Token: 0x0600C5E7 RID: 50663 RVA: 0x0011AE86 File Offset: 0x00119086
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009669 RID: 38505
			// (set) Token: 0x0600C5E8 RID: 50664 RVA: 0x0011AE99 File Offset: 0x00119099
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700966A RID: 38506
			// (set) Token: 0x0600C5E9 RID: 50665 RVA: 0x0011AEAC File Offset: 0x001190AC
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x1700966B RID: 38507
			// (set) Token: 0x0600C5EA RID: 50666 RVA: 0x0011AEBF File Offset: 0x001190BF
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x1700966C RID: 38508
			// (set) Token: 0x0600C5EB RID: 50667 RVA: 0x0011AED2 File Offset: 0x001190D2
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x1700966D RID: 38509
			// (set) Token: 0x0600C5EC RID: 50668 RVA: 0x0011AEEA File Offset: 0x001190EA
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700966E RID: 38510
			// (set) Token: 0x0600C5ED RID: 50669 RVA: 0x0011AEFD File Offset: 0x001190FD
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700966F RID: 38511
			// (set) Token: 0x0600C5EE RID: 50670 RVA: 0x0011AF10 File Offset: 0x00119110
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009670 RID: 38512
			// (set) Token: 0x0600C5EF RID: 50671 RVA: 0x0011AF23 File Offset: 0x00119123
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009671 RID: 38513
			// (set) Token: 0x0600C5F0 RID: 50672 RVA: 0x0011AF41 File Offset: 0x00119141
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009672 RID: 38514
			// (set) Token: 0x0600C5F1 RID: 50673 RVA: 0x0011AF54 File Offset: 0x00119154
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009673 RID: 38515
			// (set) Token: 0x0600C5F2 RID: 50674 RVA: 0x0011AF67 File Offset: 0x00119167
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009674 RID: 38516
			// (set) Token: 0x0600C5F3 RID: 50675 RVA: 0x0011AF7A File Offset: 0x0011917A
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009675 RID: 38517
			// (set) Token: 0x0600C5F4 RID: 50676 RVA: 0x0011AF8D File Offset: 0x0011918D
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009676 RID: 38518
			// (set) Token: 0x0600C5F5 RID: 50677 RVA: 0x0011AFA0 File Offset: 0x001191A0
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009677 RID: 38519
			// (set) Token: 0x0600C5F6 RID: 50678 RVA: 0x0011AFB3 File Offset: 0x001191B3
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009678 RID: 38520
			// (set) Token: 0x0600C5F7 RID: 50679 RVA: 0x0011AFC6 File Offset: 0x001191C6
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009679 RID: 38521
			// (set) Token: 0x0600C5F8 RID: 50680 RVA: 0x0011AFD9 File Offset: 0x001191D9
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700967A RID: 38522
			// (set) Token: 0x0600C5F9 RID: 50681 RVA: 0x0011AFEC File Offset: 0x001191EC
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700967B RID: 38523
			// (set) Token: 0x0600C5FA RID: 50682 RVA: 0x0011AFFF File Offset: 0x001191FF
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700967C RID: 38524
			// (set) Token: 0x0600C5FB RID: 50683 RVA: 0x0011B012 File Offset: 0x00119212
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700967D RID: 38525
			// (set) Token: 0x0600C5FC RID: 50684 RVA: 0x0011B025 File Offset: 0x00119225
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700967E RID: 38526
			// (set) Token: 0x0600C5FD RID: 50685 RVA: 0x0011B038 File Offset: 0x00119238
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700967F RID: 38527
			// (set) Token: 0x0600C5FE RID: 50686 RVA: 0x0011B056 File Offset: 0x00119256
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009680 RID: 38528
			// (set) Token: 0x0600C5FF RID: 50687 RVA: 0x0011B06E File Offset: 0x0011926E
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009681 RID: 38529
			// (set) Token: 0x0600C600 RID: 50688 RVA: 0x0011B086 File Offset: 0x00119286
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009682 RID: 38530
			// (set) Token: 0x0600C601 RID: 50689 RVA: 0x0011B09E File Offset: 0x0011929E
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009683 RID: 38531
			// (set) Token: 0x0600C602 RID: 50690 RVA: 0x0011B0B1 File Offset: 0x001192B1
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009684 RID: 38532
			// (set) Token: 0x0600C603 RID: 50691 RVA: 0x0011B0C4 File Offset: 0x001192C4
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009685 RID: 38533
			// (set) Token: 0x0600C604 RID: 50692 RVA: 0x0011B0DC File Offset: 0x001192DC
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009686 RID: 38534
			// (set) Token: 0x0600C605 RID: 50693 RVA: 0x0011B0EF File Offset: 0x001192EF
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009687 RID: 38535
			// (set) Token: 0x0600C606 RID: 50694 RVA: 0x0011B107 File Offset: 0x00119307
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009688 RID: 38536
			// (set) Token: 0x0600C607 RID: 50695 RVA: 0x0011B11F File Offset: 0x0011931F
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009689 RID: 38537
			// (set) Token: 0x0600C608 RID: 50696 RVA: 0x0011B132 File Offset: 0x00119332
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x1700968A RID: 38538
			// (set) Token: 0x0600C609 RID: 50697 RVA: 0x0011B145 File Offset: 0x00119345
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700968B RID: 38539
			// (set) Token: 0x0600C60A RID: 50698 RVA: 0x0011B158 File Offset: 0x00119358
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700968C RID: 38540
			// (set) Token: 0x0600C60B RID: 50699 RVA: 0x0011B16B File Offset: 0x0011936B
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700968D RID: 38541
			// (set) Token: 0x0600C60C RID: 50700 RVA: 0x0011B183 File Offset: 0x00119383
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700968E RID: 38542
			// (set) Token: 0x0600C60D RID: 50701 RVA: 0x0011B196 File Offset: 0x00119396
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700968F RID: 38543
			// (set) Token: 0x0600C60E RID: 50702 RVA: 0x0011B1A9 File Offset: 0x001193A9
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009690 RID: 38544
			// (set) Token: 0x0600C60F RID: 50703 RVA: 0x0011B1C1 File Offset: 0x001193C1
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009691 RID: 38545
			// (set) Token: 0x0600C610 RID: 50704 RVA: 0x0011B1D9 File Offset: 0x001193D9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009692 RID: 38546
			// (set) Token: 0x0600C611 RID: 50705 RVA: 0x0011B1EC File Offset: 0x001193EC
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17009693 RID: 38547
			// (set) Token: 0x0600C612 RID: 50706 RVA: 0x0011B204 File Offset: 0x00119404
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009694 RID: 38548
			// (set) Token: 0x0600C613 RID: 50707 RVA: 0x0011B217 File Offset: 0x00119417
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009695 RID: 38549
			// (set) Token: 0x0600C614 RID: 50708 RVA: 0x0011B22A File Offset: 0x0011942A
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17009696 RID: 38550
			// (set) Token: 0x0600C615 RID: 50709 RVA: 0x0011B23D File Offset: 0x0011943D
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009697 RID: 38551
			// (set) Token: 0x0600C616 RID: 50710 RVA: 0x0011B25B File Offset: 0x0011945B
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009698 RID: 38552
			// (set) Token: 0x0600C617 RID: 50711 RVA: 0x0011B279 File Offset: 0x00119479
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009699 RID: 38553
			// (set) Token: 0x0600C618 RID: 50712 RVA: 0x0011B297 File Offset: 0x00119497
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700969A RID: 38554
			// (set) Token: 0x0600C619 RID: 50713 RVA: 0x0011B2B5 File Offset: 0x001194B5
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700969B RID: 38555
			// (set) Token: 0x0600C61A RID: 50714 RVA: 0x0011B2C8 File Offset: 0x001194C8
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700969C RID: 38556
			// (set) Token: 0x0600C61B RID: 50715 RVA: 0x0011B2E0 File Offset: 0x001194E0
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700969D RID: 38557
			// (set) Token: 0x0600C61C RID: 50716 RVA: 0x0011B2FE File Offset: 0x001194FE
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700969E RID: 38558
			// (set) Token: 0x0600C61D RID: 50717 RVA: 0x0011B316 File Offset: 0x00119516
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700969F RID: 38559
			// (set) Token: 0x0600C61E RID: 50718 RVA: 0x0011B32E File Offset: 0x0011952E
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170096A0 RID: 38560
			// (set) Token: 0x0600C61F RID: 50719 RVA: 0x0011B341 File Offset: 0x00119541
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170096A1 RID: 38561
			// (set) Token: 0x0600C620 RID: 50720 RVA: 0x0011B359 File Offset: 0x00119559
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170096A2 RID: 38562
			// (set) Token: 0x0600C621 RID: 50721 RVA: 0x0011B36C File Offset: 0x0011956C
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170096A3 RID: 38563
			// (set) Token: 0x0600C622 RID: 50722 RVA: 0x0011B37F File Offset: 0x0011957F
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170096A4 RID: 38564
			// (set) Token: 0x0600C623 RID: 50723 RVA: 0x0011B392 File Offset: 0x00119592
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170096A5 RID: 38565
			// (set) Token: 0x0600C624 RID: 50724 RVA: 0x0011B3A5 File Offset: 0x001195A5
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170096A6 RID: 38566
			// (set) Token: 0x0600C625 RID: 50725 RVA: 0x0011B3BD File Offset: 0x001195BD
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170096A7 RID: 38567
			// (set) Token: 0x0600C626 RID: 50726 RVA: 0x0011B3D0 File Offset: 0x001195D0
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170096A8 RID: 38568
			// (set) Token: 0x0600C627 RID: 50727 RVA: 0x0011B3E3 File Offset: 0x001195E3
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170096A9 RID: 38569
			// (set) Token: 0x0600C628 RID: 50728 RVA: 0x0011B3FB File Offset: 0x001195FB
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170096AA RID: 38570
			// (set) Token: 0x0600C629 RID: 50729 RVA: 0x0011B40E File Offset: 0x0011960E
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170096AB RID: 38571
			// (set) Token: 0x0600C62A RID: 50730 RVA: 0x0011B426 File Offset: 0x00119626
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170096AC RID: 38572
			// (set) Token: 0x0600C62B RID: 50731 RVA: 0x0011B439 File Offset: 0x00119639
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170096AD RID: 38573
			// (set) Token: 0x0600C62C RID: 50732 RVA: 0x0011B457 File Offset: 0x00119657
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170096AE RID: 38574
			// (set) Token: 0x0600C62D RID: 50733 RVA: 0x0011B46A File Offset: 0x0011966A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170096AF RID: 38575
			// (set) Token: 0x0600C62E RID: 50734 RVA: 0x0011B488 File Offset: 0x00119688
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170096B0 RID: 38576
			// (set) Token: 0x0600C62F RID: 50735 RVA: 0x0011B49B File Offset: 0x0011969B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170096B1 RID: 38577
			// (set) Token: 0x0600C630 RID: 50736 RVA: 0x0011B4B3 File Offset: 0x001196B3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170096B2 RID: 38578
			// (set) Token: 0x0600C631 RID: 50737 RVA: 0x0011B4CB File Offset: 0x001196CB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170096B3 RID: 38579
			// (set) Token: 0x0600C632 RID: 50738 RVA: 0x0011B4E3 File Offset: 0x001196E3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170096B4 RID: 38580
			// (set) Token: 0x0600C633 RID: 50739 RVA: 0x0011B4FB File Offset: 0x001196FB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D9C RID: 3484
		public class PublicFolderParameters : ParametersBase
		{
			// Token: 0x170096B5 RID: 38581
			// (set) Token: 0x0600C635 RID: 50741 RVA: 0x0011B51B File Offset: 0x0011971B
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x170096B6 RID: 38582
			// (set) Token: 0x0600C636 RID: 50742 RVA: 0x0011B533 File Offset: 0x00119733
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x170096B7 RID: 38583
			// (set) Token: 0x0600C637 RID: 50743 RVA: 0x0011B54B File Offset: 0x0011974B
			public virtual SwitchParameter HoldForMigration
			{
				set
				{
					base.PowerSharpParameters["HoldForMigration"] = value;
				}
			}

			// Token: 0x170096B8 RID: 38584
			// (set) Token: 0x0600C638 RID: 50744 RVA: 0x0011B563 File Offset: 0x00119763
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x170096B9 RID: 38585
			// (set) Token: 0x0600C639 RID: 50745 RVA: 0x0011B57B File Offset: 0x0011977B
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x170096BA RID: 38586
			// (set) Token: 0x0600C63A RID: 50746 RVA: 0x0011B58E File Offset: 0x0011978E
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x170096BB RID: 38587
			// (set) Token: 0x0600C63B RID: 50747 RVA: 0x0011B5A1 File Offset: 0x001197A1
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x170096BC RID: 38588
			// (set) Token: 0x0600C63C RID: 50748 RVA: 0x0011B5B4 File Offset: 0x001197B4
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x170096BD RID: 38589
			// (set) Token: 0x0600C63D RID: 50749 RVA: 0x0011B5C7 File Offset: 0x001197C7
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170096BE RID: 38590
			// (set) Token: 0x0600C63E RID: 50750 RVA: 0x0011B5DA File Offset: 0x001197DA
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170096BF RID: 38591
			// (set) Token: 0x0600C63F RID: 50751 RVA: 0x0011B5ED File Offset: 0x001197ED
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x170096C0 RID: 38592
			// (set) Token: 0x0600C640 RID: 50752 RVA: 0x0011B600 File Offset: 0x00119800
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x170096C1 RID: 38593
			// (set) Token: 0x0600C641 RID: 50753 RVA: 0x0011B618 File Offset: 0x00119818
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x170096C2 RID: 38594
			// (set) Token: 0x0600C642 RID: 50754 RVA: 0x0011B62B File Offset: 0x0011982B
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x170096C3 RID: 38595
			// (set) Token: 0x0600C643 RID: 50755 RVA: 0x0011B643 File Offset: 0x00119843
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170096C4 RID: 38596
			// (set) Token: 0x0600C644 RID: 50756 RVA: 0x0011B656 File Offset: 0x00119856
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170096C5 RID: 38597
			// (set) Token: 0x0600C645 RID: 50757 RVA: 0x0011B669 File Offset: 0x00119869
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170096C6 RID: 38598
			// (set) Token: 0x0600C646 RID: 50758 RVA: 0x0011B67C File Offset: 0x0011987C
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170096C7 RID: 38599
			// (set) Token: 0x0600C647 RID: 50759 RVA: 0x0011B68F File Offset: 0x0011988F
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170096C8 RID: 38600
			// (set) Token: 0x0600C648 RID: 50760 RVA: 0x0011B6A2 File Offset: 0x001198A2
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170096C9 RID: 38601
			// (set) Token: 0x0600C649 RID: 50761 RVA: 0x0011B6B5 File Offset: 0x001198B5
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170096CA RID: 38602
			// (set) Token: 0x0600C64A RID: 50762 RVA: 0x0011B6C8 File Offset: 0x001198C8
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170096CB RID: 38603
			// (set) Token: 0x0600C64B RID: 50763 RVA: 0x0011B6DB File Offset: 0x001198DB
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170096CC RID: 38604
			// (set) Token: 0x0600C64C RID: 50764 RVA: 0x0011B6EE File Offset: 0x001198EE
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170096CD RID: 38605
			// (set) Token: 0x0600C64D RID: 50765 RVA: 0x0011B701 File Offset: 0x00119901
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170096CE RID: 38606
			// (set) Token: 0x0600C64E RID: 50766 RVA: 0x0011B714 File Offset: 0x00119914
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170096CF RID: 38607
			// (set) Token: 0x0600C64F RID: 50767 RVA: 0x0011B727 File Offset: 0x00119927
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170096D0 RID: 38608
			// (set) Token: 0x0600C650 RID: 50768 RVA: 0x0011B73A File Offset: 0x0011993A
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170096D1 RID: 38609
			// (set) Token: 0x0600C651 RID: 50769 RVA: 0x0011B74D File Offset: 0x0011994D
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170096D2 RID: 38610
			// (set) Token: 0x0600C652 RID: 50770 RVA: 0x0011B760 File Offset: 0x00119960
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170096D3 RID: 38611
			// (set) Token: 0x0600C653 RID: 50771 RVA: 0x0011B773 File Offset: 0x00119973
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170096D4 RID: 38612
			// (set) Token: 0x0600C654 RID: 50772 RVA: 0x0011B786 File Offset: 0x00119986
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170096D5 RID: 38613
			// (set) Token: 0x0600C655 RID: 50773 RVA: 0x0011B799 File Offset: 0x00119999
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170096D6 RID: 38614
			// (set) Token: 0x0600C656 RID: 50774 RVA: 0x0011B7AC File Offset: 0x001199AC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170096D7 RID: 38615
			// (set) Token: 0x0600C657 RID: 50775 RVA: 0x0011B7BF File Offset: 0x001199BF
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170096D8 RID: 38616
			// (set) Token: 0x0600C658 RID: 50776 RVA: 0x0011B7D2 File Offset: 0x001199D2
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170096D9 RID: 38617
			// (set) Token: 0x0600C659 RID: 50777 RVA: 0x0011B7E5 File Offset: 0x001199E5
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170096DA RID: 38618
			// (set) Token: 0x0600C65A RID: 50778 RVA: 0x0011B7F8 File Offset: 0x001199F8
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170096DB RID: 38619
			// (set) Token: 0x0600C65B RID: 50779 RVA: 0x0011B810 File Offset: 0x00119A10
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x170096DC RID: 38620
			// (set) Token: 0x0600C65C RID: 50780 RVA: 0x0011B823 File Offset: 0x00119A23
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x170096DD RID: 38621
			// (set) Token: 0x0600C65D RID: 50781 RVA: 0x0011B83B File Offset: 0x00119A3B
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x170096DE RID: 38622
			// (set) Token: 0x0600C65E RID: 50782 RVA: 0x0011B84E File Offset: 0x00119A4E
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x170096DF RID: 38623
			// (set) Token: 0x0600C65F RID: 50783 RVA: 0x0011B866 File Offset: 0x00119A66
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x170096E0 RID: 38624
			// (set) Token: 0x0600C660 RID: 50784 RVA: 0x0011B87E File Offset: 0x00119A7E
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x170096E1 RID: 38625
			// (set) Token: 0x0600C661 RID: 50785 RVA: 0x0011B896 File Offset: 0x00119A96
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x170096E2 RID: 38626
			// (set) Token: 0x0600C662 RID: 50786 RVA: 0x0011B8AE File Offset: 0x00119AAE
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170096E3 RID: 38627
			// (set) Token: 0x0600C663 RID: 50787 RVA: 0x0011B8C6 File Offset: 0x00119AC6
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x170096E4 RID: 38628
			// (set) Token: 0x0600C664 RID: 50788 RVA: 0x0011B8DE File Offset: 0x00119ADE
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x170096E5 RID: 38629
			// (set) Token: 0x0600C665 RID: 50789 RVA: 0x0011B8F6 File Offset: 0x00119AF6
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x170096E6 RID: 38630
			// (set) Token: 0x0600C666 RID: 50790 RVA: 0x0011B90E File Offset: 0x00119B0E
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x170096E7 RID: 38631
			// (set) Token: 0x0600C667 RID: 50791 RVA: 0x0011B926 File Offset: 0x00119B26
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x170096E8 RID: 38632
			// (set) Token: 0x0600C668 RID: 50792 RVA: 0x0011B939 File Offset: 0x00119B39
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x170096E9 RID: 38633
			// (set) Token: 0x0600C669 RID: 50793 RVA: 0x0011B94C File Offset: 0x00119B4C
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x170096EA RID: 38634
			// (set) Token: 0x0600C66A RID: 50794 RVA: 0x0011B95F File Offset: 0x00119B5F
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x170096EB RID: 38635
			// (set) Token: 0x0600C66B RID: 50795 RVA: 0x0011B972 File Offset: 0x00119B72
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x170096EC RID: 38636
			// (set) Token: 0x0600C66C RID: 50796 RVA: 0x0011B985 File Offset: 0x00119B85
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x170096ED RID: 38637
			// (set) Token: 0x0600C66D RID: 50797 RVA: 0x0011B998 File Offset: 0x00119B98
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x170096EE RID: 38638
			// (set) Token: 0x0600C66E RID: 50798 RVA: 0x0011B9B0 File Offset: 0x00119BB0
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x170096EF RID: 38639
			// (set) Token: 0x0600C66F RID: 50799 RVA: 0x0011B9C3 File Offset: 0x00119BC3
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x170096F0 RID: 38640
			// (set) Token: 0x0600C670 RID: 50800 RVA: 0x0011B9D6 File Offset: 0x00119BD6
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x170096F1 RID: 38641
			// (set) Token: 0x0600C671 RID: 50801 RVA: 0x0011B9E9 File Offset: 0x00119BE9
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x170096F2 RID: 38642
			// (set) Token: 0x0600C672 RID: 50802 RVA: 0x0011BA07 File Offset: 0x00119C07
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x170096F3 RID: 38643
			// (set) Token: 0x0600C673 RID: 50803 RVA: 0x0011BA1A File Offset: 0x00119C1A
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x170096F4 RID: 38644
			// (set) Token: 0x0600C674 RID: 50804 RVA: 0x0011BA2D File Offset: 0x00119C2D
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x170096F5 RID: 38645
			// (set) Token: 0x0600C675 RID: 50805 RVA: 0x0011BA40 File Offset: 0x00119C40
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x170096F6 RID: 38646
			// (set) Token: 0x0600C676 RID: 50806 RVA: 0x0011BA53 File Offset: 0x00119C53
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x170096F7 RID: 38647
			// (set) Token: 0x0600C677 RID: 50807 RVA: 0x0011BA66 File Offset: 0x00119C66
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x170096F8 RID: 38648
			// (set) Token: 0x0600C678 RID: 50808 RVA: 0x0011BA79 File Offset: 0x00119C79
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x170096F9 RID: 38649
			// (set) Token: 0x0600C679 RID: 50809 RVA: 0x0011BA8C File Offset: 0x00119C8C
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x170096FA RID: 38650
			// (set) Token: 0x0600C67A RID: 50810 RVA: 0x0011BA9F File Offset: 0x00119C9F
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x170096FB RID: 38651
			// (set) Token: 0x0600C67B RID: 50811 RVA: 0x0011BAB2 File Offset: 0x00119CB2
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x170096FC RID: 38652
			// (set) Token: 0x0600C67C RID: 50812 RVA: 0x0011BAC5 File Offset: 0x00119CC5
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x170096FD RID: 38653
			// (set) Token: 0x0600C67D RID: 50813 RVA: 0x0011BAD8 File Offset: 0x00119CD8
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x170096FE RID: 38654
			// (set) Token: 0x0600C67E RID: 50814 RVA: 0x0011BAEB File Offset: 0x00119CEB
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x170096FF RID: 38655
			// (set) Token: 0x0600C67F RID: 50815 RVA: 0x0011BAFE File Offset: 0x00119CFE
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009700 RID: 38656
			// (set) Token: 0x0600C680 RID: 50816 RVA: 0x0011BB1C File Offset: 0x00119D1C
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009701 RID: 38657
			// (set) Token: 0x0600C681 RID: 50817 RVA: 0x0011BB34 File Offset: 0x00119D34
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009702 RID: 38658
			// (set) Token: 0x0600C682 RID: 50818 RVA: 0x0011BB4C File Offset: 0x00119D4C
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009703 RID: 38659
			// (set) Token: 0x0600C683 RID: 50819 RVA: 0x0011BB64 File Offset: 0x00119D64
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009704 RID: 38660
			// (set) Token: 0x0600C684 RID: 50820 RVA: 0x0011BB77 File Offset: 0x00119D77
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009705 RID: 38661
			// (set) Token: 0x0600C685 RID: 50821 RVA: 0x0011BB8A File Offset: 0x00119D8A
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009706 RID: 38662
			// (set) Token: 0x0600C686 RID: 50822 RVA: 0x0011BBA2 File Offset: 0x00119DA2
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009707 RID: 38663
			// (set) Token: 0x0600C687 RID: 50823 RVA: 0x0011BBB5 File Offset: 0x00119DB5
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009708 RID: 38664
			// (set) Token: 0x0600C688 RID: 50824 RVA: 0x0011BBCD File Offset: 0x00119DCD
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009709 RID: 38665
			// (set) Token: 0x0600C689 RID: 50825 RVA: 0x0011BBE5 File Offset: 0x00119DE5
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x1700970A RID: 38666
			// (set) Token: 0x0600C68A RID: 50826 RVA: 0x0011BBF8 File Offset: 0x00119DF8
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x1700970B RID: 38667
			// (set) Token: 0x0600C68B RID: 50827 RVA: 0x0011BC0B File Offset: 0x00119E0B
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700970C RID: 38668
			// (set) Token: 0x0600C68C RID: 50828 RVA: 0x0011BC1E File Offset: 0x00119E1E
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700970D RID: 38669
			// (set) Token: 0x0600C68D RID: 50829 RVA: 0x0011BC31 File Offset: 0x00119E31
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700970E RID: 38670
			// (set) Token: 0x0600C68E RID: 50830 RVA: 0x0011BC49 File Offset: 0x00119E49
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700970F RID: 38671
			// (set) Token: 0x0600C68F RID: 50831 RVA: 0x0011BC5C File Offset: 0x00119E5C
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009710 RID: 38672
			// (set) Token: 0x0600C690 RID: 50832 RVA: 0x0011BC6F File Offset: 0x00119E6F
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009711 RID: 38673
			// (set) Token: 0x0600C691 RID: 50833 RVA: 0x0011BC87 File Offset: 0x00119E87
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009712 RID: 38674
			// (set) Token: 0x0600C692 RID: 50834 RVA: 0x0011BC9F File Offset: 0x00119E9F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009713 RID: 38675
			// (set) Token: 0x0600C693 RID: 50835 RVA: 0x0011BCB2 File Offset: 0x00119EB2
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17009714 RID: 38676
			// (set) Token: 0x0600C694 RID: 50836 RVA: 0x0011BCCA File Offset: 0x00119ECA
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009715 RID: 38677
			// (set) Token: 0x0600C695 RID: 50837 RVA: 0x0011BCDD File Offset: 0x00119EDD
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009716 RID: 38678
			// (set) Token: 0x0600C696 RID: 50838 RVA: 0x0011BCF0 File Offset: 0x00119EF0
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17009717 RID: 38679
			// (set) Token: 0x0600C697 RID: 50839 RVA: 0x0011BD03 File Offset: 0x00119F03
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009718 RID: 38680
			// (set) Token: 0x0600C698 RID: 50840 RVA: 0x0011BD21 File Offset: 0x00119F21
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009719 RID: 38681
			// (set) Token: 0x0600C699 RID: 50841 RVA: 0x0011BD3F File Offset: 0x00119F3F
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700971A RID: 38682
			// (set) Token: 0x0600C69A RID: 50842 RVA: 0x0011BD5D File Offset: 0x00119F5D
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700971B RID: 38683
			// (set) Token: 0x0600C69B RID: 50843 RVA: 0x0011BD7B File Offset: 0x00119F7B
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700971C RID: 38684
			// (set) Token: 0x0600C69C RID: 50844 RVA: 0x0011BD8E File Offset: 0x00119F8E
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700971D RID: 38685
			// (set) Token: 0x0600C69D RID: 50845 RVA: 0x0011BDA6 File Offset: 0x00119FA6
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700971E RID: 38686
			// (set) Token: 0x0600C69E RID: 50846 RVA: 0x0011BDC4 File Offset: 0x00119FC4
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700971F RID: 38687
			// (set) Token: 0x0600C69F RID: 50847 RVA: 0x0011BDDC File Offset: 0x00119FDC
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17009720 RID: 38688
			// (set) Token: 0x0600C6A0 RID: 50848 RVA: 0x0011BDF4 File Offset: 0x00119FF4
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17009721 RID: 38689
			// (set) Token: 0x0600C6A1 RID: 50849 RVA: 0x0011BE07 File Offset: 0x0011A007
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17009722 RID: 38690
			// (set) Token: 0x0600C6A2 RID: 50850 RVA: 0x0011BE1F File Offset: 0x0011A01F
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009723 RID: 38691
			// (set) Token: 0x0600C6A3 RID: 50851 RVA: 0x0011BE32 File Offset: 0x0011A032
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009724 RID: 38692
			// (set) Token: 0x0600C6A4 RID: 50852 RVA: 0x0011BE45 File Offset: 0x0011A045
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009725 RID: 38693
			// (set) Token: 0x0600C6A5 RID: 50853 RVA: 0x0011BE58 File Offset: 0x0011A058
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009726 RID: 38694
			// (set) Token: 0x0600C6A6 RID: 50854 RVA: 0x0011BE6B File Offset: 0x0011A06B
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009727 RID: 38695
			// (set) Token: 0x0600C6A7 RID: 50855 RVA: 0x0011BE83 File Offset: 0x0011A083
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009728 RID: 38696
			// (set) Token: 0x0600C6A8 RID: 50856 RVA: 0x0011BE96 File Offset: 0x0011A096
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009729 RID: 38697
			// (set) Token: 0x0600C6A9 RID: 50857 RVA: 0x0011BEA9 File Offset: 0x0011A0A9
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700972A RID: 38698
			// (set) Token: 0x0600C6AA RID: 50858 RVA: 0x0011BEC1 File Offset: 0x0011A0C1
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700972B RID: 38699
			// (set) Token: 0x0600C6AB RID: 50859 RVA: 0x0011BED4 File Offset: 0x0011A0D4
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700972C RID: 38700
			// (set) Token: 0x0600C6AC RID: 50860 RVA: 0x0011BEEC File Offset: 0x0011A0EC
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700972D RID: 38701
			// (set) Token: 0x0600C6AD RID: 50861 RVA: 0x0011BEFF File Offset: 0x0011A0FF
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700972E RID: 38702
			// (set) Token: 0x0600C6AE RID: 50862 RVA: 0x0011BF1D File Offset: 0x0011A11D
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700972F RID: 38703
			// (set) Token: 0x0600C6AF RID: 50863 RVA: 0x0011BF30 File Offset: 0x0011A130
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009730 RID: 38704
			// (set) Token: 0x0600C6B0 RID: 50864 RVA: 0x0011BF4E File Offset: 0x0011A14E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009731 RID: 38705
			// (set) Token: 0x0600C6B1 RID: 50865 RVA: 0x0011BF61 File Offset: 0x0011A161
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009732 RID: 38706
			// (set) Token: 0x0600C6B2 RID: 50866 RVA: 0x0011BF79 File Offset: 0x0011A179
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009733 RID: 38707
			// (set) Token: 0x0600C6B3 RID: 50867 RVA: 0x0011BF91 File Offset: 0x0011A191
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009734 RID: 38708
			// (set) Token: 0x0600C6B4 RID: 50868 RVA: 0x0011BFA9 File Offset: 0x0011A1A9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009735 RID: 38709
			// (set) Token: 0x0600C6B5 RID: 50869 RVA: 0x0011BFC1 File Offset: 0x0011A1C1
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
