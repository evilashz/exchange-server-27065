using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning.LoadBalancing;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000048 RID: 72
	[Cmdlet("New", "GroupMailbox", DefaultParameterSetName = "GroupMailbox", SupportsShouldProcess = true)]
	public sealed class NewGroupMailbox : NewMailboxOrSyncMailbox
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x000119B3 File Offset: 0x0000FBB3
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x000119BB File Offset: 0x0000FBBB
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "GroupMailbox", Position = 0)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000119C4 File Offset: 0x0000FBC4
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x000119ED File Offset: 0x0000FBED
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		public ModernGroupTypeInfo ModernGroupType
		{
			get
			{
				object obj = base.Fields[ADRecipientSchema.ModernGroupType];
				if (obj == null)
				{
					return ModernGroupTypeInfo.Public;
				}
				return (ModernGroupTypeInfo)obj;
			}
			set
			{
				base.Fields[ADRecipientSchema.ModernGroupType] = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00011A05 File Offset: 0x0000FC05
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00011A1C File Offset: 0x0000FC1C
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		public RecipientIdParameter[] Owners
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[ADUserSchema.Owners];
			}
			set
			{
				base.Fields[ADUserSchema.Owners] = (value ?? new RecipientIdParameter[0]);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00011A39 File Offset: 0x0000FC39
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00011A50 File Offset: 0x0000FC50
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		private RecipientIdParameter[] PublicToGroups
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[ADMailboxRecipientSchema.DelegateListLink];
			}
			set
			{
				base.Fields[ADMailboxRecipientSchema.DelegateListLink] = (value ?? new RecipientIdParameter[0]);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00011A6D File Offset: 0x0000FC6D
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00011A84 File Offset: 0x0000FC84
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		[ValidateNotNullOrEmpty]
		public string Description
		{
			get
			{
				return (string)base.Fields[ADRecipientSchema.Description];
			}
			set
			{
				base.Fields[ADRecipientSchema.Description] = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00011A97 File Offset: 0x0000FC97
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00011AAE File Offset: 0x0000FCAE
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "GroupMailbox")]
		public RecipientIdParameter ExecutingUser
		{
			get
			{
				return (RecipientIdParameter)base.Fields["ExecutingUser"];
			}
			set
			{
				base.Fields["ExecutingUser"] = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00011AC1 File Offset: 0x0000FCC1
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		public RecipientIdParameter[] Members
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["Members"];
			}
			set
			{
				base.Fields["Members"] = (value ?? new RecipientIdParameter[0]);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00011AF5 File Offset: 0x0000FCF5
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00011B0C File Offset: 0x0000FD0C
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		public bool RequireSenderAuthenticationEnabled
		{
			get
			{
				return (bool)base.Fields["RequireSenderAuthenticationEnabled"];
			}
			set
			{
				base.Fields["RequireSenderAuthenticationEnabled"] = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00011B24 File Offset: 0x0000FD24
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00011B3B File Offset: 0x0000FD3B
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		public SwitchParameter AutoSubscribeNewGroupMembers
		{
			get
			{
				return (SwitchParameter)base.Fields["AutoSubscribeNewGroupMembers"];
			}
			set
			{
				base.Fields["AutoSubscribeNewGroupMembers"] = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00011B53 File Offset: 0x0000FD53
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00011B6A File Offset: 0x0000FD6A
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		public CultureInfo Language
		{
			get
			{
				return (CultureInfo)base.Fields["Language"];
			}
			set
			{
				base.Fields["Language"] = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00011B7D File Offset: 0x0000FD7D
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00011B94 File Offset: 0x0000FD94
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)base.Fields[ADMailboxRecipientSchema.SharePointUrl];
			}
			set
			{
				base.Fields[ADMailboxRecipientSchema.SharePointUrl] = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00011BA7 File Offset: 0x0000FDA7
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00011BBE File Offset: 0x0000FDBE
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		public MultiValuedProperty<string> SharePointResources
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields[ADMailboxRecipientSchema.SharePointResources];
			}
			set
			{
				base.Fields[ADMailboxRecipientSchema.SharePointResources] = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00011BD1 File Offset: 0x0000FDD1
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		public string ValidationOrganization
		{
			get
			{
				return (string)base.Fields["ValidationOrganization"];
			}
			set
			{
				base.Fields["ValidationOrganization"] = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00011BFB File Offset: 0x0000FDFB
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00011C12 File Offset: 0x0000FE12
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		public RecipientIdType RecipientIdType
		{
			get
			{
				return (RecipientIdType)base.Fields["RecipientIdType"];
			}
			set
			{
				base.Fields["RecipientIdType"] = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00011C2A File Offset: 0x0000FE2A
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00011C50 File Offset: 0x0000FE50
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		public SwitchParameter FromSyncClient
		{
			get
			{
				return (SwitchParameter)(base.Fields["FromSyncClient"] ?? false);
			}
			set
			{
				base.Fields["FromSyncClient"] = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00011C68 File Offset: 0x0000FE68
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00011C7F File Offset: 0x0000FE7F
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)base.Fields[GroupMailboxSchema.EmailAddresses];
			}
			set
			{
				base.Fields[GroupMailboxSchema.EmailAddresses] = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00011C92 File Offset: 0x0000FE92
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00011C9A File Offset: 0x0000FE9A
		private new SwitchParameter AccountDisabled { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00011CA3 File Offset: 0x0000FEA3
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00011CAB File Offset: 0x0000FEAB
		private new MailboxPolicyIdParameter ActiveSyncMailboxPolicy { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00011CBC File Offset: 0x0000FEBC
		private new AddressBookMailboxPolicyIdParameter AddressBookPolicy { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00011CC5 File Offset: 0x0000FEC5
		// (set) Token: 0x060003DB RID: 987 RVA: 0x00011CCD File Offset: 0x0000FECD
		private new SwitchParameter Arbitration { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00011CD6 File Offset: 0x0000FED6
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00011CDE File Offset: 0x0000FEDE
		private new MailboxIdParameter ArbitrationMailbox { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00011CE7 File Offset: 0x0000FEE7
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00011CEF File Offset: 0x0000FEEF
		private new SwitchParameter Archive { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00011CF8 File Offset: 0x0000FEF8
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00011D00 File Offset: 0x0000FF00
		private new DatabaseIdParameter ArchiveDatabase { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00011D09 File Offset: 0x0000FF09
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00011D11 File Offset: 0x0000FF11
		private new SmtpDomain ArchiveDomain { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00011D1A File Offset: 0x0000FF1A
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00011D22 File Offset: 0x0000FF22
		private new SwitchParameter BypassLiveId { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00011D2B File Offset: 0x0000FF2B
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00011D33 File Offset: 0x0000FF33
		private new SwitchParameter Discovery { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00011D3C File Offset: 0x0000FF3C
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00011D44 File Offset: 0x0000FF44
		private new SwitchParameter Equipment { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00011D4D File Offset: 0x0000FF4D
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x00011D55 File Offset: 0x0000FF55
		private new SwitchParameter EvictLiveId { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00011D5E File Offset: 0x0000FF5E
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x00011D66 File Offset: 0x0000FF66
		private new string FederatedIdentity { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00011D6F File Offset: 0x0000FF6F
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x00011D77 File Offset: 0x0000FF77
		private new SwitchParameter ForestWideDomainControllerAffinityByExecutingUser { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00011D80 File Offset: 0x0000FF80
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00011D88 File Offset: 0x0000FF88
		private new string FirstName { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00011D91 File Offset: 0x0000FF91
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00011D99 File Offset: 0x0000FF99
		private new SwitchParameter Force { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00011DA2 File Offset: 0x0000FFA2
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00011DAA File Offset: 0x0000FFAA
		private new string ImmutableId { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00011DB3 File Offset: 0x0000FFB3
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00011DBB File Offset: 0x0000FFBB
		private new SwitchParameter ImportLiveId { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00011DC4 File Offset: 0x0000FFC4
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00011DCC File Offset: 0x0000FFCC
		private new string Initials { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00011DD5 File Offset: 0x0000FFD5
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00011DDD File Offset: 0x0000FFDD
		private new string LastName { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00011DE6 File Offset: 0x0000FFE6
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00011DEE File Offset: 0x0000FFEE
		private new PSCredential LinkedCredential { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00011DF7 File Offset: 0x0000FFF7
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00011DFF File Offset: 0x0000FFFF
		private new string LinkedDomainController { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00011E08 File Offset: 0x00010008
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00011E10 File Offset: 0x00010010
		private new UserIdParameter LinkedMasterAccount { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00011E19 File Offset: 0x00010019
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x00011E21 File Offset: 0x00010021
		private new MailboxPlanIdParameter MailboxPlan { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00011E2A File Offset: 0x0001002A
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00011E32 File Offset: 0x00010032
		private new Guid MailboxContainerGuid { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00011E3B File Offset: 0x0001003B
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x00011E43 File Offset: 0x00010043
		private new WindowsLiveId MicrosoftOnlineServicesID { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00011E4C File Offset: 0x0001004C
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00011E54 File Offset: 0x00010054
		private new MultiValuedProperty<ModeratorIDParameter> ModeratedBy { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00011E5D File Offset: 0x0001005D
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00011E65 File Offset: 0x00010065
		private new bool ModerationEnabled { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00011E6E File Offset: 0x0001006E
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00011E76 File Offset: 0x00010076
		private new NetID NetID { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00011E7F File Offset: 0x0001007F
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x00011E87 File Offset: 0x00010087
		private new SecureString Password { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00011E90 File Offset: 0x00010090
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00011E98 File Offset: 0x00010098
		private new SwitchParameter PublicFolder { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00011EA1 File Offset: 0x000100A1
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x00011EA9 File Offset: 0x000100A9
		private new SwitchParameter HoldForMigration { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00011EB2 File Offset: 0x000100B2
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x00011EBA File Offset: 0x000100BA
		private new bool QueryBaseDNRestrictionEnabled { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00011EC3 File Offset: 0x000100C3
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x00011ECB File Offset: 0x000100CB
		private new RemoteAccountPolicyIdParameter RemoteAccountPolicy { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00011ED4 File Offset: 0x000100D4
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x00011EDC File Offset: 0x000100DC
		private new SwitchParameter RemoteArchive { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00011EE5 File Offset: 0x000100E5
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00011EED File Offset: 0x000100ED
		private new bool RemotePowerShellEnabled { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00011EF6 File Offset: 0x000100F6
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00011EFE File Offset: 0x000100FE
		private new RemovedMailboxIdParameter RemovedMailbox { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00011F07 File Offset: 0x00010107
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x00011F0F File Offset: 0x0001010F
		private new bool ResetPasswordOnNextLogon { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00011F18 File Offset: 0x00010118
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x00011F20 File Offset: 0x00010120
		private new MailboxPolicyIdParameter RetentionPolicy { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00011F29 File Offset: 0x00010129
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x00011F31 File Offset: 0x00010131
		private new MailboxPolicyIdParameter RoleAssignmentPolicy { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00011F3A File Offset: 0x0001013A
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x00011F42 File Offset: 0x00010142
		private new SwitchParameter Room { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00011F4B File Offset: 0x0001014B
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x00011F53 File Offset: 0x00010153
		private new string SamAccountName { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00011F5C File Offset: 0x0001015C
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x00011F64 File Offset: 0x00010164
		private new TransportModerationNotificationFlags SendModerationNotifications { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00011F6D File Offset: 0x0001016D
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x00011F75 File Offset: 0x00010175
		private new SwitchParameter Shared { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00011F7E File Offset: 0x0001017E
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x00011F86 File Offset: 0x00010186
		private new SharingPolicyIdParameter SharingPolicy { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00011F8F File Offset: 0x0001018F
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00011F97 File Offset: 0x00010197
		private new bool SKUAssigned { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00011FA0 File Offset: 0x000101A0
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x00011FA8 File Offset: 0x000101A8
		private new MultiValuedProperty<Capability> AddOnSKUCapability { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00011FB1 File Offset: 0x000101B1
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x00011FB9 File Offset: 0x000101B9
		private new Capability SKUCapability { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00011FC2 File Offset: 0x000101C2
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x00011FCA File Offset: 0x000101CA
		private new SwitchParameter TargetAllMDBs { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00011FD3 File Offset: 0x000101D3
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x00011FDB File Offset: 0x000101DB
		private new ThrottlingPolicyIdParameter ThrottlingPolicy { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00011FE4 File Offset: 0x000101E4
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x00011FEC File Offset: 0x000101EC
		private new CountryInfo UsageLocation { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00011FF5 File Offset: 0x000101F5
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x00011FFD File Offset: 0x000101FD
		private new SwitchParameter UseExistingLiveId { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00012006 File Offset: 0x00010206
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0001200E File Offset: 0x0001020E
		private new string UserPrincipalName { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00012017 File Offset: 0x00010217
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0001201F File Offset: 0x0001021F
		private new WindowsLiveId WindowsLiveID { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00012028 File Offset: 0x00010228
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x00012030 File Offset: 0x00010230
		private new SecureString RoomMailboxPassword { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00012039 File Offset: 0x00010239
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x00012041 File Offset: 0x00010241
		private new bool EnableRoomMailboxAccount { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0001204A File Offset: 0x0001024A
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x00012052 File Offset: 0x00010252
		private new bool IsExcludedFromServingHierarchy { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001205B File Offset: 0x0001025B
		protected override bool ShouldGenerateWindowsLiveID
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00012060 File Offset: 0x00010260
		protected override void InternalBeginProcessing()
		{
			if (this.ValidationOrganization != null && !string.Equals(this.ValidationOrganization, base.CurrentOrganizationId.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				base.ThrowTerminatingError(new ValidationOrgCurrentOrgNotMatchException(this.ValidationOrganization, base.CurrentOrganizationId.ToString()), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsChanged(ADRecipientSchema.ModernGroupType))
			{
				this.groupType = (ModernGroupObjectType)base.Fields[ADRecipientSchema.ModernGroupType];
			}
			if (base.Fields.IsChanged(ADMailboxRecipientSchema.DelegateListLink) && this.groupType != ModernGroupObjectType.Public)
			{
				base.WriteError(new GroupMailboxInvalidOperationException(Strings.ErrorInvalidGroupTypeForPublicToGroups), ExchangeErrorCategory.Client, null);
			}
			if (this.IsWarmupCall())
			{
				this.InitializeDependencies();
			}
			if (this.FromSyncClient && base.Fields.IsChanged("Members"))
			{
				base.WriteError(new GroupMailboxInvalidOperationException(Strings.ErrorFromSyncClientAndMembersUsedTogether), ExchangeErrorCategory.Client, null);
			}
			base.InternalBeginProcessing();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00012158 File Offset: 0x00010358
		protected override void InternalStateReset()
		{
			if (base.Database == null)
			{
				if (base.CurrentTaskContext.InvocationInfo.IsCmdletInvokedWithoutPSFramework || ExEnvironment.IsSdfDomain)
				{
					this.selectedLocalDatabase = this.FindDatabaseForProvisioning();
					if (this.selectedLocalDatabase == null)
					{
						this.WriteError(new RecipientTaskException(Strings.ErrorParameterRequiredButNotProvisioned("Database")), ExchangeErrorCategory.ServerOperation, null, true);
					}
					base.UserSpecifiedParameters["Database"] = new DatabaseIdParameter(this.selectedLocalDatabase.MailboxDatabase);
				}
				else
				{
					base.WriteVerbose(new LocalizedString(string.Format("Using MailboxProvisioningHandler to select a database. IsCmdletInvokedWithoutPSFramework={0}, IsSDFDomain={1}", base.CurrentTaskContext.InvocationInfo.IsCmdletInvokedWithoutPSFramework, ExEnvironment.IsSdfDomain)));
				}
			}
			using (new StopwatchPerformanceTracker("NewGroupMailbox.GenerateUniqueName", GenericCmdletInfoDataLogger.Instance))
			{
				this.GenerateUniqueNameIfRequired();
			}
			base.InternalStateReset();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001224C File Offset: 0x0001044C
		protected override void ValidateProvisionedProperties(IConfigurable dataObject)
		{
			if (this.selectedLocalDatabase != null)
			{
				ADUser aduser = (ADUser)dataObject;
				aduser.DatabaseAndLocation = this.selectedLocalDatabase;
				aduser.Database = this.selectedLocalDatabase.MailboxDatabase.Id;
			}
			base.ValidateProvisionedProperties(dataObject);
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00012291 File Offset: 0x00010491
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewGroupMailbox(this.Name.ToString(), base.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000122BC File Offset: 0x000104BC
		protected override void PrepareUserObject(ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareUserObject(user);
			user.SetExchangeVersion(ADUser.GetMaximumSupportedExchangeObjectVersion(RecipientTypeDetails.GroupMailbox, false));
			user.ModernGroupType = this.groupType;
			if (base.Fields.IsChanged(ADRecipientSchema.Description))
			{
				user.Description.Add((string)base.Fields[ADRecipientSchema.Description]);
			}
			if (base.Fields.IsChanged("RequireSenderAuthenticationEnabled"))
			{
				user.RequireAllSendersAreAuthenticated = this.RequireSenderAuthenticationEnabled;
			}
			else
			{
				user.RequireAllSendersAreAuthenticated = true;
			}
			if (base.Fields.IsChanged("AutoSubscribeNewGroupMembers"))
			{
				user.AutoSubscribeNewGroupMembers = this.AutoSubscribeNewGroupMembers;
			}
			if (string.IsNullOrEmpty(user.ExternalDirectoryObjectId) && VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.GenerateNewExternalDirectoryObjectId.Enabled)
			{
				user.ExternalDirectoryObjectId = Guid.NewGuid().ToString("D");
			}
			if (base.Fields.IsChanged(ADMailboxRecipientSchema.SharePointUrl) && this.SharePointUrl != null)
			{
				if (user.SharePointResources == null)
				{
					user.SharePointResources = new MultiValuedProperty<string>();
				}
				user.SharePointResources.Add("SiteUrl=" + this.SharePointUrl);
			}
			if (base.Fields.IsChanged(ADMailboxRecipientSchema.SharePointResources))
			{
				user.SharePointResources = this.SharePointResources;
				user.SharePointUrl = null;
			}
			if (base.Fields.IsChanged(GroupMailboxSchema.EmailAddresses) && !this.FromSyncClient)
			{
				user.EmailAddresses = this.EmailAddresses;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00012454 File Offset: 0x00010654
		protected override void InternalProcessRecord()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.DataSession;
			using (new StopwatchPerformanceTracker("NewGroupMailbox.CreateContext", GenericCmdletInfoDataLogger.Instance))
			{
				this.groupMailboxContext = new GroupMailboxContext(this.DataObject, base.CurrentOrganizationId, recipientSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADGroup>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (base.Fields.IsChanged(ADMailboxRecipientSchema.DelegateListLink))
			{
				this.groupMailboxContext.AddPublicToGroups(this.PublicToGroups);
			}
			if (base.Fields.IsChanged("ExecutingUser") && this.ExecutingUser != null)
			{
				this.groupMailboxContext.SetExecutingUser(this.ExecutingUser);
			}
			if (base.Fields.IsChanged("Language") && this.Language != null)
			{
				this.DataObject.Languages.Add(this.Language);
			}
			else
			{
				this.DataObject.Languages.Add(CultureInfo.CreateSpecificCulture("en-US"));
			}
			if (base.Fields.IsChanged(ADUserSchema.Owners) && this.Owners != null)
			{
				this.groupMailboxContext.SetOwners(this.Owners);
			}
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "NewGroupMailbox", "BaseInternalProcessRecord", LoggerHelper.CmdletPerfMonitors))
			{
				base.InternalProcessRecord();
				this.groupMailboxContext.SetGroupAdUser(this.DataObject);
			}
			if (!this.FromSyncClient)
			{
				if (base.Fields.IsChanged("Members") && this.Members != null)
				{
					this.groupMailboxContext.SetMembers(this.Members);
				}
				Exception ex;
				ExchangeErrorCategory? exchangeErrorCategory;
				this.groupMailboxContext.NewGroupMailbox(this.databaseLocationInfo, out ex, out exchangeErrorCategory);
				if (ex != null)
				{
					base.WriteError(new GroupMailboxFailedToLogonException(Strings.ErrorUnableToLogonGroupMailbox(this.DataObject.ExchangeGuid, this.databaseLocationInfo.ServerFqdn, recipientSession.LastUsedDc, ex.Message), ex), exchangeErrorCategory.GetValueOrDefault(ExchangeErrorCategory.ServerTransient), null);
				}
				base.WriteVerbose(Strings.DatabaseLogonSuccessful(this.DataObject.ExchangeGuid, this.databaseLocationInfo.ServerFqdn, recipientSession.LastUsedDc));
				this.groupMailboxContext.SetExternalResources(this.FromSyncClient);
				this.groupMailboxContext.EnsureGroupIsInDirectoryCache("NewGroupMailbox.InternalProcessRecord");
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000126EC File Offset: 0x000108EC
		protected override void WriteResult(ADObject result)
		{
			ADUser dataObject = (ADUser)result;
			base.WriteResult(GroupMailbox.FromDataObject(dataObject));
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001270C File Offset: 0x0001090C
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new NewGroupMailboxTaskModuleFactory();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00012714 File Offset: 0x00010914
		private void InitializeDependencies()
		{
			using (new StopwatchPerformanceTracker("NewGroupMailbox.InitializeDependencies.InitializeXmlSerializer", GenericCmdletInfoDataLogger.Instance))
			{
				UpdateGroupMailboxEwsBinding.InitializeXmlSerializer();
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00012758 File Offset: 0x00010958
		private void WriteDebugInfo(string message, params object[] args)
		{
			base.WriteDebug(new LocalizedString(string.Format(message, args)));
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001276C File Offset: 0x0001096C
		private MailboxDatabaseWithLocationInfo FindDatabaseForProvisioning()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 874, "FindDatabaseForProvisioning", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\GroupMailbox\\NewGroupMailbox.cs");
			this.stopWatch.Restart();
			Server server = topologyConfigurationSession.FindLocalServer();
			this.WriteDebugInfo("Found localServer. Time took:{0} sec", new object[]
			{
				this.stopWatch.Elapsed.TotalSeconds.ToString("n2")
			});
			if (server == null)
			{
				this.WriteError(new ObjectNotFoundException(new LocalizedString(Environment.MachineName), null), ExchangeErrorCategory.ServerTransient, null, true);
			}
			DatabaseAvailabilityGroup databaseAvailabilityGroup = null;
			this.stopWatch.Restart();
			if (ExEnvironment.IsSdfDomain)
			{
				databaseAvailabilityGroup = DagTaskHelper.ReadDagByName("NAMSR01DG050", topologyConfigurationSession);
				this.WriteDebugInfo("Found Dag {0} . Time took:{1} sec", new object[]
				{
					"NAMSR01DG050",
					this.stopWatch.Elapsed.TotalSeconds.ToString("n2")
				});
				if (databaseAvailabilityGroup == null)
				{
					this.WriteError(new GroupMailboxFailedToFindDagException(Strings.ErrorIncorrectInputDag("NAMSR01DG050")), ExchangeErrorCategory.ServerTransient, null, true);
				}
			}
			else if (server.DatabaseAvailabilityGroup != null)
			{
				databaseAvailabilityGroup = DagTaskHelper.ReadDag(server.DatabaseAvailabilityGroup, topologyConfigurationSession);
				this.WriteDebugInfo("Dag found:{0} for id={1}. Time took:{2} sec", new object[]
				{
					databaseAvailabilityGroup != null,
					server.DatabaseAvailabilityGroup,
					this.stopWatch.Elapsed.TotalSeconds.ToString("n2")
				});
			}
			else
			{
				this.WriteDebugInfo("The current server does not belong to a dag.", new object[0]);
			}
			List<MailboxDatabase> list;
			List<MailboxDatabase> list2;
			this.FetchDatabases(topologyConfigurationSession, databaseAvailabilityGroup, server, out list, out list2);
			Random random = new Random();
			while (list.Count > 0)
			{
				int index = random.Next(list.Count);
				MailboxDatabase mailboxDatabase = list[index];
				list.RemoveAt(index);
				DatabaseLocationInfo databaseLocationInfo = this.GetDatabaseLocationInfo(mailboxDatabase);
				if (databaseLocationInfo != null)
				{
					if (string.Equals(databaseLocationInfo.ServerFqdn, server.Fqdn, StringComparison.OrdinalIgnoreCase))
					{
						return new MailboxDatabaseWithLocationInfo(mailboxDatabase, databaseLocationInfo);
					}
					list2.Add(mailboxDatabase);
				}
			}
			this.WriteDebugInfo("Picking a remote database", new object[0]);
			while (list2.Count > 0)
			{
				int index2 = random.Next(list2.Count);
				MailboxDatabase mailboxDatabase2 = list2[index2];
				list2.RemoveAt(index2);
				DatabaseLocationInfo databaseLocationInfo2 = this.GetDatabaseLocationInfo(mailboxDatabase2);
				if (databaseLocationInfo2 != null)
				{
					return new MailboxDatabaseWithLocationInfo(mailboxDatabase2, databaseLocationInfo2);
				}
			}
			return null;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000129E8 File Offset: 0x00010BE8
		private void FetchDatabases(ITopologyConfigurationSession topologyConfigSession, DatabaseAvailabilityGroup dag, Server localServer, out List<MailboxDatabase> localDatabases, out List<MailboxDatabase> remoteDatabases)
		{
			QueryFilter queryFilter;
			if (this.IsWarmupCall())
			{
				dag = null;
				queryFilter = new ComparisonFilter(ComparisonOperator.Equal, DatabaseSchema.Server, localServer.Id);
			}
			else
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, MailboxDatabaseSchema.IsExcludedFromProvisioning, false),
					new ComparisonFilter(ComparisonOperator.Equal, MailboxDatabaseSchema.IsExcludedFromInitialProvisioning, false),
					new ComparisonFilter(ComparisonOperator.Equal, MailboxDatabaseSchema.IsExcludedFromProvisioningBySpaceMonitoring, false)
				});
				if (dag != null)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.Equal, DatabaseSchema.MasterServerOrAvailabilityGroup, dag.Id)
					});
				}
				else
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.Equal, DatabaseSchema.Server, localServer.Id)
					});
				}
			}
			this.stopWatch.Restart();
			ADPagedReader<MailboxDatabase> adpagedReader = topologyConfigSession.FindPaged<MailboxDatabase>(null, QueryScope.SubTree, queryFilter, null, 0);
			List<MailboxDatabase> list = new List<MailboxDatabase>(adpagedReader.ReadAllPages());
			this.WriteDebugInfo("Found {0} databases on {1}. Time took:{2} sec", new object[]
			{
				list.Count,
				(dag != null) ? dag.Name : localServer.Name,
				this.stopWatch.Elapsed.TotalSeconds.ToString("n2")
			});
			if (list.Count == 0)
			{
				dag = null;
				MailboxDatabase executingUserDatabase = this.GetExecutingUserDatabase(topologyConfigSession);
				if (executingUserDatabase != null)
				{
					list.Add(executingUserDatabase);
				}
				else
				{
					this.WriteError(new GroupMailboxFailedToFindDatabaseException(Strings.ErrorDatabaseUnavailableForProvisioning), ExchangeErrorCategory.ServerTransient, null, true);
				}
			}
			if (dag != null)
			{
				localDatabases = new List<MailboxDatabase>(10);
				remoteDatabases = new List<MailboxDatabase>(list.Count);
				foreach (MailboxDatabase mailboxDatabase in list)
				{
					if (localServer.Name.Equals(mailboxDatabase.ServerName, StringComparison.OrdinalIgnoreCase))
					{
						localDatabases.Add(mailboxDatabase);
					}
					else
					{
						remoteDatabases.Add(mailboxDatabase);
					}
				}
				if (localDatabases.Count == 0)
				{
					this.WriteDebugInfo("No local database found", new object[0]);
					return;
				}
			}
			else
			{
				localDatabases = list;
				remoteDatabases = new List<MailboxDatabase>(0);
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00012CD8 File Offset: 0x00010ED8
		private MailboxDatabase GetExecutingUserDatabase(ITopologyConfigurationSession topologyConfigSession)
		{
			this.WriteDebugInfo("Executing user:{0}", new object[]
			{
				this.ExecutingUser
			});
			ADObjectId databaseId = null;
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.CurrentOrganizationId.OrganizationalUnit, base.CurrentOrganizationId, base.CurrentOrganizationId, true);
			IRecipientSession session = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 1098, "GetExecutingUserDatabase", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\GroupMailbox\\NewGroupMailbox.cs");
			Exception ex = GroupMailboxContext.ExecuteADOperationAndHandleException(delegate
			{
				ADUser aduser = (ADUser)this.GetDataObject<ADUser>(this.ExecutingUser, session, this.CurrentOrganizationId.OrganizationalUnit, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.ExecutingUser.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.ExecutingUser.ToString())), ExchangeErrorCategory.Client);
				databaseId = aduser.Database;
				this.WriteDebugInfo("Located local database for the executing user:{0}", new object[]
				{
					databaseId
				});
			});
			if (ex != null)
			{
				this.WriteDebugInfo("Unable to find database belonging to the executing user because:{0}", new object[]
				{
					ex
				});
			}
			if (databaseId != null)
			{
				return topologyConfigSession.Read<MailboxDatabase>(databaseId);
			}
			return null;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00012D9C File Offset: 0x00010F9C
		private DatabaseLocationInfo GetDatabaseLocationInfo(MailboxDatabase database)
		{
			this.WriteDebugInfo("Checking {0} database with ActiveManager.", new object[]
			{
				database.Name
			});
			try
			{
				return RecipientTaskHelper.GetActiveManagerInstance().GetServerForDatabase(database.Guid);
			}
			catch (ObjectNotFoundException ex)
			{
				this.WriteDebugInfo(ex.Message, new object[0]);
			}
			catch (ServerForDatabaseNotFoundException ex2)
			{
				this.WriteDebugInfo(ex2.Message, new object[0]);
			}
			return null;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00012E24 File Offset: 0x00011024
		private void GenerateUniqueNameIfRequired()
		{
			if (this.IsNameUnique(this.Name))
			{
				this.WriteDebugInfo("Name is unique: {0}", new object[]
				{
					this.Name
				});
				return;
			}
			int num = 1000;
			string text;
			for (;;)
			{
				string str = Guid.NewGuid().ToString().Substring(0, 6);
				text = this.Name + str;
				if (this.IsNameUnique(text))
				{
					break;
				}
				if (num-- <= 0)
				{
					goto Block_3;
				}
			}
			this.WriteDebugInfo("Generated unique name: old: {0}, new: {1}", new object[]
			{
				this.Name,
				text
			});
			this.Name = text;
			return;
			Block_3:
			this.WriteDebugInfo("Could not generate a unique name", new object[0]);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00012EDC File Offset: 0x000110DC
		private bool IsNameUnique(string name)
		{
			ADScope scope = null;
			if (base.CurrentOrganizationId.OrganizationalUnit != null)
			{
				scope = new ADScope(base.CurrentOrganizationId.OrganizationalUnit, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, base.CurrentOrganizationId.OrganizationalUnit));
			}
			return RecipientTaskHelper.IsPropertyValueUnique(base.TenantGlobalCatalogSession, scope, null, new ADPropertyDefinition[]
			{
				ADObjectSchema.Name
			}, ADObjectSchema.Name, name, false, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, ExchangeErrorCategory.Client, false);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00012F58 File Offset: 0x00011158
		private bool IsWarmupCall()
		{
			if (base.CurrentTaskContext.InvocationInfo.IsCmdletInvokedWithoutPSFramework)
			{
				SwitchParameter? switchParameter = base.CurrentTaskContext.InvocationInfo.UserSpecifiedParameters["WhatIf"] as SwitchParameter?;
				return switchParameter != null && switchParameter == SwitchParameter.Present;
			}
			return false;
		}

		// Token: 0x040000C1 RID: 193
		private const string DefaultLanguage = "en-US";

		// Token: 0x040000C2 RID: 194
		private const string ParameterExecutingUser = "ExecutingUser";

		// Token: 0x040000C3 RID: 195
		private const string ParameterMembers = "Members";

		// Token: 0x040000C4 RID: 196
		private const string ParameterRequireSenderAuthenticationEnabled = "RequireSenderAuthenticationEnabled";

		// Token: 0x040000C5 RID: 197
		private const string ParameterAutoSubscribeNewGroupMembers = "AutoSubscribeNewGroupMembers";

		// Token: 0x040000C6 RID: 198
		private const string ParameterLanguage = "Language";

		// Token: 0x040000C7 RID: 199
		private const string ParameterValidationOrganization = "ValidationOrganization";

		// Token: 0x040000C8 RID: 200
		private const string ParameterRecipientIdType = "RecipientIdType";

		// Token: 0x040000C9 RID: 201
		private const string ParameterFromSyncClient = "FromSyncClient";

		// Token: 0x040000CA RID: 202
		private GroupMailboxContext groupMailboxContext;

		// Token: 0x040000CB RID: 203
		private ModernGroupObjectType groupType = ModernGroupObjectType.Public;

		// Token: 0x040000CC RID: 204
		private MailboxDatabaseWithLocationInfo selectedLocalDatabase;

		// Token: 0x040000CD RID: 205
		private Stopwatch stopWatch = new Stopwatch();
	}
}
