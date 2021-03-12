using System;
using System.Collections;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000B2 RID: 178
	public class NewOrganizationCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06001A43 RID: 6723 RVA: 0x00039A8A File Offset: 0x00037C8A
		private NewOrganizationCommand() : base("New-Organization")
		{
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00039A97 File Offset: 0x00037C97
		public NewOrganizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00039AA6 File Offset: 0x00037CA6
		public virtual NewOrganizationCommand SetParameters(NewOrganizationCommand.DatacenterParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00039AB0 File Offset: 0x00037CB0
		public virtual NewOrganizationCommand SetParameters(NewOrganizationCommand.SharedConfigurationParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00039ABA File Offset: 0x00037CBA
		public virtual NewOrganizationCommand SetParameters(NewOrganizationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000B3 RID: 179
		public class DatacenterParameterSetParameters : ParametersBase
		{
			// Token: 0x17000498 RID: 1176
			// (set) Token: 0x06001A48 RID: 6728 RVA: 0x00039AC4 File Offset: 0x00037CC4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000499 RID: 1177
			// (set) Token: 0x06001A49 RID: 6729 RVA: 0x00039AD7 File Offset: 0x00037CD7
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x1700049A RID: 1178
			// (set) Token: 0x06001A4A RID: 6730 RVA: 0x00039AEA File Offset: 0x00037CEA
			public virtual SwitchParameter HotmailMigration
			{
				set
				{
					base.PowerSharpParameters["HotmailMigration"] = value;
				}
			}

			// Token: 0x1700049B RID: 1179
			// (set) Token: 0x06001A4B RID: 6731 RVA: 0x00039B02 File Offset: 0x00037D02
			public virtual WindowsLiveId Administrator
			{
				set
				{
					base.PowerSharpParameters["Administrator"] = value;
				}
			}

			// Token: 0x1700049C RID: 1180
			// (set) Token: 0x06001A4C RID: 6732 RVA: 0x00039B15 File Offset: 0x00037D15
			public virtual NetID AdministratorNetID
			{
				set
				{
					base.PowerSharpParameters["AdministratorNetID"] = value;
				}
			}

			// Token: 0x1700049D RID: 1181
			// (set) Token: 0x06001A4D RID: 6733 RVA: 0x00039B28 File Offset: 0x00037D28
			public virtual SecureString AdministratorPassword
			{
				set
				{
					base.PowerSharpParameters["AdministratorPassword"] = value;
				}
			}

			// Token: 0x1700049E RID: 1182
			// (set) Token: 0x06001A4E RID: 6734 RVA: 0x00039B3B File Offset: 0x00037D3B
			public virtual string ProgramId
			{
				set
				{
					base.PowerSharpParameters["ProgramId"] = value;
				}
			}

			// Token: 0x1700049F RID: 1183
			// (set) Token: 0x06001A4F RID: 6735 RVA: 0x00039B4E File Offset: 0x00037D4E
			public virtual string OfferId
			{
				set
				{
					base.PowerSharpParameters["OfferId"] = value;
				}
			}

			// Token: 0x170004A0 RID: 1184
			// (set) Token: 0x06001A50 RID: 6736 RVA: 0x00039B61 File Offset: 0x00037D61
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x170004A1 RID: 1185
			// (set) Token: 0x06001A51 RID: 6737 RVA: 0x00039B74 File Offset: 0x00037D74
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170004A2 RID: 1186
			// (set) Token: 0x06001A52 RID: 6738 RVA: 0x00039B8C File Offset: 0x00037D8C
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x170004A3 RID: 1187
			// (set) Token: 0x06001A53 RID: 6739 RVA: 0x00039BA4 File Offset: 0x00037DA4
			public virtual string DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x170004A4 RID: 1188
			// (set) Token: 0x06001A54 RID: 6740 RVA: 0x00039BB7 File Offset: 0x00037DB7
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x170004A5 RID: 1189
			// (set) Token: 0x06001A55 RID: 6741 RVA: 0x00039BCA File Offset: 0x00037DCA
			public virtual MultiValuedProperty<Capability> PersistedCapabilities
			{
				set
				{
					base.PowerSharpParameters["PersistedCapabilities"] = value;
				}
			}

			// Token: 0x170004A6 RID: 1190
			// (set) Token: 0x06001A56 RID: 6742 RVA: 0x00039BDD File Offset: 0x00037DDD
			public virtual bool OutBoundOnly
			{
				set
				{
					base.PowerSharpParameters["OutBoundOnly"] = value;
				}
			}

			// Token: 0x170004A7 RID: 1191
			// (set) Token: 0x06001A57 RID: 6743 RVA: 0x00039BF5 File Offset: 0x00037DF5
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x170004A8 RID: 1192
			// (set) Token: 0x06001A58 RID: 6744 RVA: 0x00039C0D File Offset: 0x00037E0D
			public virtual LiveIdInstanceType LiveIdInstanceType
			{
				set
				{
					base.PowerSharpParameters["LiveIdInstanceType"] = value;
				}
			}

			// Token: 0x170004A9 RID: 1193
			// (set) Token: 0x06001A59 RID: 6745 RVA: 0x00039C25 File Offset: 0x00037E25
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x170004AA RID: 1194
			// (set) Token: 0x06001A5A RID: 6746 RVA: 0x00039C38 File Offset: 0x00037E38
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170004AB RID: 1195
			// (set) Token: 0x06001A5B RID: 6747 RVA: 0x00039C4B File Offset: 0x00037E4B
			public virtual byte RMSOnlineConfig
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineConfig"] = value;
				}
			}

			// Token: 0x170004AC RID: 1196
			// (set) Token: 0x06001A5C RID: 6748 RVA: 0x00039C63 File Offset: 0x00037E63
			public virtual Hashtable RMSOnlineKeys
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineKeys"] = value;
				}
			}

			// Token: 0x170004AD RID: 1197
			// (set) Token: 0x06001A5D RID: 6749 RVA: 0x00039C76 File Offset: 0x00037E76
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x170004AE RID: 1198
			// (set) Token: 0x06001A5E RID: 6750 RVA: 0x00039C8E File Offset: 0x00037E8E
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x170004AF RID: 1199
			// (set) Token: 0x06001A5F RID: 6751 RVA: 0x00039CA6 File Offset: 0x00037EA6
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x170004B0 RID: 1200
			// (set) Token: 0x06001A60 RID: 6752 RVA: 0x00039CBE File Offset: 0x00037EBE
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x170004B1 RID: 1201
			// (set) Token: 0x06001A61 RID: 6753 RVA: 0x00039CD6 File Offset: 0x00037ED6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170004B2 RID: 1202
			// (set) Token: 0x06001A62 RID: 6754 RVA: 0x00039CEE File Offset: 0x00037EEE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170004B3 RID: 1203
			// (set) Token: 0x06001A63 RID: 6755 RVA: 0x00039D06 File Offset: 0x00037F06
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170004B4 RID: 1204
			// (set) Token: 0x06001A64 RID: 6756 RVA: 0x00039D1E File Offset: 0x00037F1E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170004B5 RID: 1205
			// (set) Token: 0x06001A65 RID: 6757 RVA: 0x00039D36 File Offset: 0x00037F36
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000B4 RID: 180
		public class SharedConfigurationParameterSetParameters : ParametersBase
		{
			// Token: 0x170004B6 RID: 1206
			// (set) Token: 0x06001A67 RID: 6759 RVA: 0x00039D56 File Offset: 0x00037F56
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170004B7 RID: 1207
			// (set) Token: 0x06001A68 RID: 6760 RVA: 0x00039D69 File Offset: 0x00037F69
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x170004B8 RID: 1208
			// (set) Token: 0x06001A69 RID: 6761 RVA: 0x00039D7C File Offset: 0x00037F7C
			public virtual SwitchParameter CreateSharedConfiguration
			{
				set
				{
					base.PowerSharpParameters["CreateSharedConfiguration"] = value;
				}
			}

			// Token: 0x170004B9 RID: 1209
			// (set) Token: 0x06001A6A RID: 6762 RVA: 0x00039D94 File Offset: 0x00037F94
			public virtual WindowsLiveId Administrator
			{
				set
				{
					base.PowerSharpParameters["Administrator"] = value;
				}
			}

			// Token: 0x170004BA RID: 1210
			// (set) Token: 0x06001A6B RID: 6763 RVA: 0x00039DA7 File Offset: 0x00037FA7
			public virtual NetID AdministratorNetID
			{
				set
				{
					base.PowerSharpParameters["AdministratorNetID"] = value;
				}
			}

			// Token: 0x170004BB RID: 1211
			// (set) Token: 0x06001A6C RID: 6764 RVA: 0x00039DBA File Offset: 0x00037FBA
			public virtual SecureString AdministratorPassword
			{
				set
				{
					base.PowerSharpParameters["AdministratorPassword"] = value;
				}
			}

			// Token: 0x170004BC RID: 1212
			// (set) Token: 0x06001A6D RID: 6765 RVA: 0x00039DCD File Offset: 0x00037FCD
			public virtual string ProgramId
			{
				set
				{
					base.PowerSharpParameters["ProgramId"] = value;
				}
			}

			// Token: 0x170004BD RID: 1213
			// (set) Token: 0x06001A6E RID: 6766 RVA: 0x00039DE0 File Offset: 0x00037FE0
			public virtual string OfferId
			{
				set
				{
					base.PowerSharpParameters["OfferId"] = value;
				}
			}

			// Token: 0x170004BE RID: 1214
			// (set) Token: 0x06001A6F RID: 6767 RVA: 0x00039DF3 File Offset: 0x00037FF3
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x170004BF RID: 1215
			// (set) Token: 0x06001A70 RID: 6768 RVA: 0x00039E06 File Offset: 0x00038006
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170004C0 RID: 1216
			// (set) Token: 0x06001A71 RID: 6769 RVA: 0x00039E1E File Offset: 0x0003801E
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x170004C1 RID: 1217
			// (set) Token: 0x06001A72 RID: 6770 RVA: 0x00039E36 File Offset: 0x00038036
			public virtual string DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x170004C2 RID: 1218
			// (set) Token: 0x06001A73 RID: 6771 RVA: 0x00039E49 File Offset: 0x00038049
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x170004C3 RID: 1219
			// (set) Token: 0x06001A74 RID: 6772 RVA: 0x00039E5C File Offset: 0x0003805C
			public virtual MultiValuedProperty<Capability> PersistedCapabilities
			{
				set
				{
					base.PowerSharpParameters["PersistedCapabilities"] = value;
				}
			}

			// Token: 0x170004C4 RID: 1220
			// (set) Token: 0x06001A75 RID: 6773 RVA: 0x00039E6F File Offset: 0x0003806F
			public virtual bool OutBoundOnly
			{
				set
				{
					base.PowerSharpParameters["OutBoundOnly"] = value;
				}
			}

			// Token: 0x170004C5 RID: 1221
			// (set) Token: 0x06001A76 RID: 6774 RVA: 0x00039E87 File Offset: 0x00038087
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x170004C6 RID: 1222
			// (set) Token: 0x06001A77 RID: 6775 RVA: 0x00039E9F File Offset: 0x0003809F
			public virtual LiveIdInstanceType LiveIdInstanceType
			{
				set
				{
					base.PowerSharpParameters["LiveIdInstanceType"] = value;
				}
			}

			// Token: 0x170004C7 RID: 1223
			// (set) Token: 0x06001A78 RID: 6776 RVA: 0x00039EB7 File Offset: 0x000380B7
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x170004C8 RID: 1224
			// (set) Token: 0x06001A79 RID: 6777 RVA: 0x00039ECA File Offset: 0x000380CA
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170004C9 RID: 1225
			// (set) Token: 0x06001A7A RID: 6778 RVA: 0x00039EDD File Offset: 0x000380DD
			public virtual byte RMSOnlineConfig
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineConfig"] = value;
				}
			}

			// Token: 0x170004CA RID: 1226
			// (set) Token: 0x06001A7B RID: 6779 RVA: 0x00039EF5 File Offset: 0x000380F5
			public virtual Hashtable RMSOnlineKeys
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineKeys"] = value;
				}
			}

			// Token: 0x170004CB RID: 1227
			// (set) Token: 0x06001A7C RID: 6780 RVA: 0x00039F08 File Offset: 0x00038108
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x170004CC RID: 1228
			// (set) Token: 0x06001A7D RID: 6781 RVA: 0x00039F20 File Offset: 0x00038120
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x170004CD RID: 1229
			// (set) Token: 0x06001A7E RID: 6782 RVA: 0x00039F38 File Offset: 0x00038138
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x170004CE RID: 1230
			// (set) Token: 0x06001A7F RID: 6783 RVA: 0x00039F50 File Offset: 0x00038150
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x170004CF RID: 1231
			// (set) Token: 0x06001A80 RID: 6784 RVA: 0x00039F68 File Offset: 0x00038168
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170004D0 RID: 1232
			// (set) Token: 0x06001A81 RID: 6785 RVA: 0x00039F80 File Offset: 0x00038180
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (set) Token: 0x06001A82 RID: 6786 RVA: 0x00039F98 File Offset: 0x00038198
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170004D2 RID: 1234
			// (set) Token: 0x06001A83 RID: 6787 RVA: 0x00039FB0 File Offset: 0x000381B0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170004D3 RID: 1235
			// (set) Token: 0x06001A84 RID: 6788 RVA: 0x00039FC8 File Offset: 0x000381C8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000B5 RID: 181
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170004D4 RID: 1236
			// (set) Token: 0x06001A86 RID: 6790 RVA: 0x00039FE8 File Offset: 0x000381E8
			public virtual WindowsLiveId Administrator
			{
				set
				{
					base.PowerSharpParameters["Administrator"] = value;
				}
			}

			// Token: 0x170004D5 RID: 1237
			// (set) Token: 0x06001A87 RID: 6791 RVA: 0x00039FFB File Offset: 0x000381FB
			public virtual NetID AdministratorNetID
			{
				set
				{
					base.PowerSharpParameters["AdministratorNetID"] = value;
				}
			}

			// Token: 0x170004D6 RID: 1238
			// (set) Token: 0x06001A88 RID: 6792 RVA: 0x0003A00E File Offset: 0x0003820E
			public virtual SecureString AdministratorPassword
			{
				set
				{
					base.PowerSharpParameters["AdministratorPassword"] = value;
				}
			}

			// Token: 0x170004D7 RID: 1239
			// (set) Token: 0x06001A89 RID: 6793 RVA: 0x0003A021 File Offset: 0x00038221
			public virtual string ProgramId
			{
				set
				{
					base.PowerSharpParameters["ProgramId"] = value;
				}
			}

			// Token: 0x170004D8 RID: 1240
			// (set) Token: 0x06001A8A RID: 6794 RVA: 0x0003A034 File Offset: 0x00038234
			public virtual string OfferId
			{
				set
				{
					base.PowerSharpParameters["OfferId"] = value;
				}
			}

			// Token: 0x170004D9 RID: 1241
			// (set) Token: 0x06001A8B RID: 6795 RVA: 0x0003A047 File Offset: 0x00038247
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x170004DA RID: 1242
			// (set) Token: 0x06001A8C RID: 6796 RVA: 0x0003A05A File Offset: 0x0003825A
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170004DB RID: 1243
			// (set) Token: 0x06001A8D RID: 6797 RVA: 0x0003A072 File Offset: 0x00038272
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x170004DC RID: 1244
			// (set) Token: 0x06001A8E RID: 6798 RVA: 0x0003A08A File Offset: 0x0003828A
			public virtual string DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x170004DD RID: 1245
			// (set) Token: 0x06001A8F RID: 6799 RVA: 0x0003A09D File Offset: 0x0003829D
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x170004DE RID: 1246
			// (set) Token: 0x06001A90 RID: 6800 RVA: 0x0003A0B0 File Offset: 0x000382B0
			public virtual MultiValuedProperty<Capability> PersistedCapabilities
			{
				set
				{
					base.PowerSharpParameters["PersistedCapabilities"] = value;
				}
			}

			// Token: 0x170004DF RID: 1247
			// (set) Token: 0x06001A91 RID: 6801 RVA: 0x0003A0C3 File Offset: 0x000382C3
			public virtual bool OutBoundOnly
			{
				set
				{
					base.PowerSharpParameters["OutBoundOnly"] = value;
				}
			}

			// Token: 0x170004E0 RID: 1248
			// (set) Token: 0x06001A92 RID: 6802 RVA: 0x0003A0DB File Offset: 0x000382DB
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x170004E1 RID: 1249
			// (set) Token: 0x06001A93 RID: 6803 RVA: 0x0003A0F3 File Offset: 0x000382F3
			public virtual LiveIdInstanceType LiveIdInstanceType
			{
				set
				{
					base.PowerSharpParameters["LiveIdInstanceType"] = value;
				}
			}

			// Token: 0x170004E2 RID: 1250
			// (set) Token: 0x06001A94 RID: 6804 RVA: 0x0003A10B File Offset: 0x0003830B
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x170004E3 RID: 1251
			// (set) Token: 0x06001A95 RID: 6805 RVA: 0x0003A11E File Offset: 0x0003831E
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170004E4 RID: 1252
			// (set) Token: 0x06001A96 RID: 6806 RVA: 0x0003A131 File Offset: 0x00038331
			public virtual byte RMSOnlineConfig
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineConfig"] = value;
				}
			}

			// Token: 0x170004E5 RID: 1253
			// (set) Token: 0x06001A97 RID: 6807 RVA: 0x0003A149 File Offset: 0x00038349
			public virtual Hashtable RMSOnlineKeys
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineKeys"] = value;
				}
			}

			// Token: 0x170004E6 RID: 1254
			// (set) Token: 0x06001A98 RID: 6808 RVA: 0x0003A15C File Offset: 0x0003835C
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x170004E7 RID: 1255
			// (set) Token: 0x06001A99 RID: 6809 RVA: 0x0003A174 File Offset: 0x00038374
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x170004E8 RID: 1256
			// (set) Token: 0x06001A9A RID: 6810 RVA: 0x0003A18C File Offset: 0x0003838C
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x170004E9 RID: 1257
			// (set) Token: 0x06001A9B RID: 6811 RVA: 0x0003A1A4 File Offset: 0x000383A4
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x170004EA RID: 1258
			// (set) Token: 0x06001A9C RID: 6812 RVA: 0x0003A1BC File Offset: 0x000383BC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170004EB RID: 1259
			// (set) Token: 0x06001A9D RID: 6813 RVA: 0x0003A1D4 File Offset: 0x000383D4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170004EC RID: 1260
			// (set) Token: 0x06001A9E RID: 6814 RVA: 0x0003A1EC File Offset: 0x000383EC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170004ED RID: 1261
			// (set) Token: 0x06001A9F RID: 6815 RVA: 0x0003A204 File Offset: 0x00038404
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170004EE RID: 1262
			// (set) Token: 0x06001AA0 RID: 6816 RVA: 0x0003A21C File Offset: 0x0003841C
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
