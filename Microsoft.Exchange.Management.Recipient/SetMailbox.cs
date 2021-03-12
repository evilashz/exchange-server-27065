using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Management.Tasks.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.FaultInjection;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200006E RID: 110
	[Cmdlet("Set", "Mailbox", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailbox : SetMailboxBase<MailboxIdParameter, Mailbox>
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00021E64 File Offset: 0x00020064
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x00021E7B File Offset: 0x0002007B
		[Parameter(Mandatory = true, ParameterSetName = "RemoveAggregatedMailbox", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "AddAggregatedMailbox", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00021E8E File Offset: 0x0002008E
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x00021E96 File Offset: 0x00020096
		[Parameter(Mandatory = false)]
		public new MailboxPlanIdParameter MailboxPlan
		{
			get
			{
				return base.MailboxPlan;
			}
			set
			{
				base.MailboxPlan = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00021E9F File Offset: 0x0002009F
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00021ECA File Offset: 0x000200CA
		[Parameter(Mandatory = false)]
		public bool EnableRoomMailboxAccount
		{
			get
			{
				return base.Fields["EnableRoomMailboxAccount"] != null && (bool)base.Fields["EnableRoomMailboxAccount"];
			}
			set
			{
				base.Fields["EnableRoomMailboxAccount"] = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00021EE2 File Offset: 0x000200E2
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x00021EF9 File Offset: 0x000200F9
		[Parameter(Mandatory = false)]
		public SecureString RoomMailboxPassword
		{
			private get
			{
				return (SecureString)base.Fields["RoomMailboxPassword"];
			}
			set
			{
				base.Fields["RoomMailboxPassword"] = value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00021F0C File Offset: 0x0002010C
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x00021F32 File Offset: 0x00020132
		[Parameter(Mandatory = false)]
		public SwitchParameter SkipMailboxProvisioningConstraintValidation
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipMailboxProvisioningConstraintValidation"] ?? false);
			}
			set
			{
				base.Fields["SkipMailboxProvisioningConstraintValidation"] = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00021F4A File Offset: 0x0002014A
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00021F61 File Offset: 0x00020161
		[Parameter(Mandatory = false)]
		public MailboxProvisioningConstraint MailboxProvisioningConstraint
		{
			get
			{
				return (MailboxProvisioningConstraint)base.Fields[ADRecipientSchema.MailboxProvisioningConstraint];
			}
			set
			{
				base.Fields[ADRecipientSchema.MailboxProvisioningConstraint] = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00021F74 File Offset: 0x00020174
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x00021F8B File Offset: 0x0002018B
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
		{
			get
			{
				return (MultiValuedProperty<MailboxProvisioningConstraint>)base.Fields[ADRecipientSchema.MailboxProvisioningPreferences];
			}
			set
			{
				base.Fields[ADRecipientSchema.MailboxProvisioningPreferences] = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00021F9E File Offset: 0x0002019E
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x00021FA6 File Offset: 0x000201A6
		[Parameter(Mandatory = false)]
		public SwitchParameter EvictLiveId { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00021FAF File Offset: 0x000201AF
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x00021FB7 File Offset: 0x000201B7
		[Parameter(Mandatory = false)]
		public bool RequireSecretQA { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00021FC0 File Offset: 0x000201C0
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00021FD7 File Offset: 0x000201D7
		[Parameter(Mandatory = false)]
		public NetID OriginalNetID
		{
			get
			{
				return (NetID)base.Fields["OriginalNetID"];
			}
			set
			{
				base.Fields["OriginalNetID"] = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00021FEA File Offset: 0x000201EA
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00022001 File Offset: 0x00020201
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields[ADMailboxRecipientSchema.Database];
			}
			set
			{
				base.Fields[ADMailboxRecipientSchema.Database] = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00022014 File Offset: 0x00020214
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0002202B File Offset: 0x0002022B
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> LitigationHoldDuration
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)base.Fields[ADRecipientSchema.LitigationHoldDuration];
			}
			set
			{
				base.Fields[ADRecipientSchema.LitigationHoldDuration] = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00022043 File Offset: 0x00020243
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0002205A File Offset: 0x0002025A
		[Parameter(Mandatory = false)]
		public bool UMDataStorage
		{
			get
			{
				return (bool)base.Fields["UMDataStorage"];
			}
			set
			{
				base.Fields["UMDataStorage"] = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00022072 File Offset: 0x00020272
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x00022089 File Offset: 0x00020289
		[Parameter(Mandatory = false)]
		public bool UMGrammar
		{
			get
			{
				return (bool)base.Fields["UMGrammar"];
			}
			set
			{
				base.Fields["UMGrammar"] = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000220A1 File Offset: 0x000202A1
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x000220B8 File Offset: 0x000202B8
		[Parameter(Mandatory = false)]
		public bool OABGen
		{
			get
			{
				return (bool)base.Fields["OABGen"];
			}
			set
			{
				base.Fields["OABGen"] = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x000220D0 File Offset: 0x000202D0
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x000220E7 File Offset: 0x000202E7
		[Parameter(Mandatory = false)]
		public bool GMGen
		{
			get
			{
				return (bool)base.Fields["GMGen"];
			}
			set
			{
				base.Fields["GMGen"] = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000220FF File Offset: 0x000202FF
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00022116 File Offset: 0x00020316
		[Parameter(Mandatory = false)]
		public bool ClientExtensions
		{
			get
			{
				return (bool)base.Fields["ClientExtensions"];
			}
			set
			{
				base.Fields["ClientExtensions"] = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0002212E File Offset: 0x0002032E
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x00022145 File Offset: 0x00020345
		[Parameter(Mandatory = false)]
		public bool MailRouting
		{
			get
			{
				return (bool)base.Fields["MailRouting"];
			}
			set
			{
				base.Fields["MailRouting"] = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0002215D File Offset: 0x0002035D
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00022174 File Offset: 0x00020374
		[Parameter(Mandatory = false)]
		public bool Management
		{
			get
			{
				return (bool)base.Fields["Management"];
			}
			set
			{
				base.Fields["Management"] = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0002218C File Offset: 0x0002038C
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x000221A3 File Offset: 0x000203A3
		[Parameter(Mandatory = false)]
		public bool TenantUpgrade
		{
			get
			{
				return (bool)base.Fields["TenantUpgrade"];
			}
			set
			{
				base.Fields["TenantUpgrade"] = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x000221BB File Offset: 0x000203BB
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x000221D2 File Offset: 0x000203D2
		[Parameter(Mandatory = false)]
		public bool Migration
		{
			get
			{
				return (bool)base.Fields["Migration"];
			}
			set
			{
				base.Fields["Migration"] = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x000221EA File Offset: 0x000203EA
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x00022201 File Offset: 0x00020401
		[Parameter(Mandatory = false)]
		public bool MessageTracking
		{
			get
			{
				return (bool)base.Fields["MessageTracking"];
			}
			set
			{
				base.Fields["MessageTracking"] = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00022219 File Offset: 0x00020419
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x00022230 File Offset: 0x00020430
		[Parameter(Mandatory = false)]
		public bool OMEncryption
		{
			get
			{
				return (bool)base.Fields["OMEncryption"];
			}
			set
			{
				base.Fields["OMEncryption"] = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00022248 File Offset: 0x00020448
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0002225F File Offset: 0x0002045F
		[Parameter(Mandatory = false)]
		public bool PstProvider
		{
			get
			{
				return (bool)base.Fields["PstProvider"];
			}
			set
			{
				base.Fields["PstProvider"] = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00022277 File Offset: 0x00020477
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0002228E File Offset: 0x0002048E
		[Parameter(Mandatory = false)]
		public bool SuiteServiceStorage
		{
			get
			{
				return (bool)base.Fields["SuiteServiceStorage"];
			}
			set
			{
				base.Fields["SuiteServiceStorage"] = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x000222A6 File Offset: 0x000204A6
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x000222AE File Offset: 0x000204AE
		[Parameter(Mandatory = false)]
		public SecureString OldPassword { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x000222B7 File Offset: 0x000204B7
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x000222BF File Offset: 0x000204BF
		[Parameter(Mandatory = false)]
		public SecureString NewPassword { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x000222C8 File Offset: 0x000204C8
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x000222DF File Offset: 0x000204DF
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public DatabaseIdParameter ArchiveDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields[ADUserSchema.ArchiveDatabase];
			}
			set
			{
				base.Fields[ADUserSchema.ArchiveDatabase] = value;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x000222F2 File Offset: 0x000204F2
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00022309 File Offset: 0x00020509
		[Parameter(Mandatory = false)]
		public OrganizationalUnitIdParameter QueryBaseDN
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields[ADUserSchema.QueryBaseDN];
			}
			set
			{
				base.Fields[ADUserSchema.QueryBaseDN] = value;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0002231C File Offset: 0x0002051C
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00022333 File Offset: 0x00020533
		[Parameter(Mandatory = false)]
		public RecipientIdParameter DefaultPublicFolderMailbox
		{
			get
			{
				return (RecipientIdParameter)base.Fields[MailboxSchema.DefaultPublicFolderMailboxValue];
			}
			set
			{
				base.Fields[MailboxSchema.DefaultPublicFolderMailboxValue] = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00022346 File Offset: 0x00020546
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0002235D File Offset: 0x0002055D
		[Parameter(Mandatory = false)]
		public int? MailboxMessagesPerFolderCountWarningQuota
		{
			get
			{
				return (int?)base.Fields["MailboxMessagesPerFolderCountWarningQuota"];
			}
			set
			{
				base.Fields["MailboxMessagesPerFolderCountWarningQuota"] = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00022375 File Offset: 0x00020575
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0002238C File Offset: 0x0002058C
		[Parameter(Mandatory = false)]
		public int? MailboxMessagesPerFolderCountReceiveQuota
		{
			get
			{
				return (int?)base.Fields["MailboxMessagesPerFolderCountReceiveQuota"];
			}
			set
			{
				base.Fields["MailboxMessagesPerFolderCountReceiveQuota"] = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x000223A4 File Offset: 0x000205A4
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x000223BB File Offset: 0x000205BB
		[Parameter(Mandatory = false)]
		public int? DumpsterMessagesPerFolderCountWarningQuota
		{
			get
			{
				return (int?)base.Fields["DumpsterMessagesPerFolderCountWarningQuota"];
			}
			set
			{
				base.Fields["DumpsterMessagesPerFolderCountWarningQuota"] = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x000223D3 File Offset: 0x000205D3
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x000223EA File Offset: 0x000205EA
		[Parameter(Mandatory = false)]
		public int? DumpsterMessagesPerFolderCountReceiveQuota
		{
			get
			{
				return (int?)base.Fields["DumpsterMessagesPerFolderCountReceiveQuota"];
			}
			set
			{
				base.Fields["DumpsterMessagesPerFolderCountReceiveQuota"] = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00022402 File Offset: 0x00020602
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x00022419 File Offset: 0x00020619
		[Parameter(Mandatory = false)]
		public int? FolderHierarchyChildrenCountWarningQuota
		{
			get
			{
				return (int?)base.Fields["FolderHierarchyChildrenCountWarningQuota"];
			}
			set
			{
				base.Fields["FolderHierarchyChildrenCountWarningQuota"] = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00022431 File Offset: 0x00020631
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00022448 File Offset: 0x00020648
		[Parameter(Mandatory = false)]
		public int? FolderHierarchyChildrenCountReceiveQuota
		{
			get
			{
				return (int?)base.Fields["FolderHierarchyChildrenCountReceiveQuota"];
			}
			set
			{
				base.Fields["FolderHierarchyChildrenCountReceiveQuota"] = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00022460 File Offset: 0x00020660
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00022477 File Offset: 0x00020677
		[Parameter(Mandatory = false)]
		public int? FolderHierarchyDepthWarningQuota
		{
			get
			{
				return (int?)base.Fields["FolderHierarchyDepthWarningQuota"];
			}
			set
			{
				base.Fields["FolderHierarchyDepthWarningQuota"] = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0002248F File Offset: 0x0002068F
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x000224A6 File Offset: 0x000206A6
		[Parameter(Mandatory = false)]
		public int? FolderHierarchyDepthReceiveQuota
		{
			get
			{
				return (int?)base.Fields["FolderHierarchyDepthReceiveQuota"];
			}
			set
			{
				base.Fields["FolderHierarchyDepthReceiveQuota"] = value;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x000224BE File Offset: 0x000206BE
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x000224D5 File Offset: 0x000206D5
		[Parameter(Mandatory = false)]
		public int? FoldersCountWarningQuota
		{
			get
			{
				return (int?)base.Fields["FoldersCountWarningQuota"];
			}
			set
			{
				base.Fields["FoldersCountWarningQuota"] = value;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x000224ED File Offset: 0x000206ED
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x00022504 File Offset: 0x00020704
		[Parameter(Mandatory = false)]
		public int? FoldersCountReceiveQuota
		{
			get
			{
				return (int?)base.Fields["FoldersCountReceiveQuota"];
			}
			set
			{
				base.Fields["FoldersCountReceiveQuota"] = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0002251C File Offset: 0x0002071C
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x00022533 File Offset: 0x00020733
		[Parameter(Mandatory = false)]
		public int? ExtendedPropertiesCountQuota
		{
			get
			{
				return (int?)base.Fields["ExtendedPropertiesCountQuota"];
			}
			set
			{
				base.Fields["ExtendedPropertiesCountQuota"] = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0002254B File Offset: 0x0002074B
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x00022562 File Offset: 0x00020762
		[Parameter(Mandatory = false)]
		public Guid? MailboxContainerGuid
		{
			get
			{
				return (Guid?)base.Fields[ADUserSchema.MailboxContainerGuid];
			}
			set
			{
				base.Fields[ADUserSchema.MailboxContainerGuid] = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0002257A File Offset: 0x0002077A
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x000225A0 File Offset: 0x000207A0
		[Parameter(Mandatory = true, ParameterSetName = "AddAggregatedMailbox")]
		public SwitchParameter AddAggregatedAccount
		{
			get
			{
				return (SwitchParameter)(base.Fields["AddAggregatedAccount"] ?? false);
			}
			set
			{
				base.Fields["AddAggregatedAccount"] = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x000225B8 File Offset: 0x000207B8
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x000225DE File Offset: 0x000207DE
		[Parameter(Mandatory = true, ParameterSetName = "RemoveAggregatedMailbox")]
		public SwitchParameter RemoveAggregatedAccount
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveAggregatedAccount"] ?? false);
			}
			set
			{
				base.Fields["RemoveAggregatedAccount"] = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x000225F6 File Offset: 0x000207F6
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0002261B File Offset: 0x0002081B
		[Parameter(Mandatory = true, ParameterSetName = "AddAggregatedMailbox")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveAggregatedMailbox")]
		public Guid AggregatedMailboxGuid
		{
			get
			{
				return (Guid)(base.Fields["AggregatedMailboxGuid"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["AggregatedMailboxGuid"] = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00022633 File Offset: 0x00020833
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0002264A File Offset: 0x0002084A
		[Parameter(Mandatory = false)]
		public CrossTenantObjectId UnifiedMailbox
		{
			get
			{
				return (CrossTenantObjectId)base.Fields[ADUserSchema.UnifiedMailbox];
			}
			set
			{
				base.Fields[ADUserSchema.UnifiedMailbox] = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0002265D File Offset: 0x0002085D
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x00022683 File Offset: 0x00020883
		[Parameter(Mandatory = false)]
		public SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForestWideDomainControllerAffinityByExecutingUser"] ?? false);
			}
			set
			{
				base.Fields["ForestWideDomainControllerAffinityByExecutingUser"] = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0002269B File Offset: 0x0002089B
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x000226BC File Offset: 0x000208BC
		[Parameter(Mandatory = false)]
		public bool MessageCopyForSentAsEnabled
		{
			get
			{
				return (bool)(base.Fields["MessageCopyForSentAsEnabled"] ?? false);
			}
			set
			{
				base.Fields["MessageCopyForSentAsEnabled"] = value;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x000226D4 File Offset: 0x000208D4
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x000226F5 File Offset: 0x000208F5
		[Parameter(Mandatory = false)]
		public bool MessageCopyForSendOnBehalfEnabled
		{
			get
			{
				return (bool)(base.Fields["MessageCopyForSendOnBehalfEnabled"] ?? false);
			}
			set
			{
				base.Fields["MessageCopyForSendOnBehalfEnabled"] = value;
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00022710 File Offset: 0x00020910
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeArbitrationMailbox(adrecipient, base.Arbitration) || MailboxTaskHelper.ExcludePublicFolderMailbox(adrecipient, base.PublicFolder) || MailboxTaskHelper.ExcludeMailboxPlan(adrecipient, false) || MailboxTaskHelper.ExcludeAuditLogMailbox(adrecipient, base.AuditLog))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ExchangeErrorCategory.Client, this.Identity);
			}
			return adrecipient;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000227B8 File Offset: 0x000209B8
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if (base.Fields.IsModified(MailboxSchema.RetentionPolicy) && base.RetentionPolicy != null && SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
			{
				base.WriteError(new InvalidOperationInDehydratedContextException(Strings.ErrorLinkOpOnDehydratedTenant("RetentionPolicy")), ExchangeErrorCategory.Client, null);
			}
			base.InternalBeginProcessing();
			if (base.Fields.IsModified("AggregatedMailboxGuid"))
			{
				if (!this.AddAggregatedAccount.IsPresent && !this.RemoveAggregatedAccount.IsPresent)
				{
					base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorNoAggregatedAccountOperationSpecified, null), ExchangeErrorCategory.Client, null);
				}
				else if (this.AddAggregatedAccount.IsPresent && this.RemoveAggregatedAccount.IsPresent)
				{
					base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorMoreThanOneAggregatedAccountOperationSpecified, null), ExchangeErrorCategory.Client, null);
				}
			}
			else if (this.AddAggregatedAccount.IsPresent || this.RemoveAggregatedAccount.IsPresent)
			{
				base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorAggregatedMailboxGuidNotSpecified, null), ExchangeErrorCategory.Client, null);
			}
			Mailbox mailbox = (Mailbox)this.GetDynamicParameters();
			if (base.PublicFolder)
			{
				foreach (object obj in SetMailbox.InvalidPublicFolderParameters)
				{
					ProviderPropertyDefinition providerPropertyDefinition = obj as ProviderPropertyDefinition;
					if (base.Fields.IsModified(obj) || (providerPropertyDefinition != null && mailbox.IsModified(providerPropertyDefinition)))
					{
						base.WriteError(new TaskArgumentException(Strings.ErrorInvalidParameterForPublicFolderTasks((providerPropertyDefinition == null) ? obj.ToString() : providerPropertyDefinition.Name, "PublicFolder")), ExchangeErrorCategory.Client, this.Identity);
					}
				}
			}
			else
			{
				foreach (object obj2 in SetMailbox.PublicFolderOnlyParameters)
				{
					ProviderPropertyDefinition providerPropertyDefinition2 = obj2 as ProviderPropertyDefinition;
					if (base.Fields.IsModified(obj2) || (providerPropertyDefinition2 != null && mailbox.IsModified(providerPropertyDefinition2)))
					{
						base.WriteError(new TaskArgumentException(Strings.ErrorInvalidMandatoryParameterForPublicFolderTasks((providerPropertyDefinition2 == null) ? obj2.ToString() : providerPropertyDefinition2.Name, "PublicFolder")), ExchangeErrorCategory.Client, this.Identity);
					}
				}
			}
			if (base.Fields.IsModified(ADMailboxRecipientSchema.Database))
			{
				this.rehomeDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.Database, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorMailboxDatabaseNotFound(this.Database.ToString())), new LocalizedString?(Strings.ErrorMailboxDatabaseNotUnique(this.Database.ToString())), ExchangeErrorCategory.Client);
				mailbox[ADMailboxRecipientSchema.Database] = (ADObjectId)this.rehomeDatabase.Identity;
				if (this.rehomeDatabase.Server == null)
				{
					base.WriteError(new NullServerObjectException(), ExchangeErrorCategory.ServerOperation, this.Identity);
				}
				Server server = (Server)base.GetDataObject<Server>(new ServerIdParameter(this.rehomeDatabase.Server), base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.rehomeDatabase.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.rehomeDatabase.Server.ToString())), ExchangeErrorCategory.Client);
				mailbox[ADMailboxRecipientSchema.ServerLegacyDN] = server.ExchangeLegacyDN;
				mailbox[ADRecipientSchema.HomeMTA] = null;
			}
			if (base.Fields.IsModified(ADUserSchema.ArchiveDatabase))
			{
				this.rehomeArchiveDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.ArchiveDatabase, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorMailboxDatabaseNotFound(this.ArchiveDatabase.ToString())), new LocalizedString?(Strings.ErrorMailboxDatabaseNotUnique(this.ArchiveDatabase.ToString())), ExchangeErrorCategory.Client);
				mailbox[ADUserSchema.ArchiveDatabase] = (ADObjectId)this.rehomeArchiveDatabase.Identity;
				if (this.rehomeArchiveDatabase.Server == null)
				{
					base.WriteError(new NullServerObjectException(), ExchangeErrorCategory.ServerOperation, this.Identity);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00022B98 File Offset: 0x00020D98
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			if (base.Fields.IsChanged(ADUserSchema.QueryBaseDN) && this.QueryBaseDN != null)
			{
				this.querybaseDNId = RecipientTaskHelper.ResolveOrganizationalUnitInOrganization(this.QueryBaseDN, this.ConfigurationSession, base.CurrentOrganizationId, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ExchangeOrganizationalUnit>), ExchangeErrorCategory.ServerOperation, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError)).Id;
			}
			Mailbox mailbox = (Mailbox)this.GetDynamicParameters();
			if (base.Fields.IsModified(ADRecipientSchema.DefaultPublicFolderMailbox))
			{
				if (this.DefaultPublicFolderMailbox != null)
				{
					ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.DefaultPublicFolderMailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.DefaultPublicFolderMailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.DefaultPublicFolderMailbox.ToString())), ExchangeErrorCategory.Client);
					ADObjectId id = aduser.Id;
					if (aduser == null || aduser.RecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
					{
						IConfigurationSession tenantLocalConfigSession = RecipientTaskHelper.GetTenantLocalConfigSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId);
						Organization orgContainer = tenantLocalConfigSession.GetOrgContainer();
						if (orgContainer.RemotePublicFolderMailboxes == null || !orgContainer.RemotePublicFolderMailboxes.Contains(id))
						{
							base.WriteError(new ObjectNotFoundException(Strings.PublicFolderMailboxNotFound), ExchangeErrorCategory.Client, aduser);
						}
					}
					mailbox[ADRecipientSchema.DefaultPublicFolderMailbox] = id;
					return;
				}
				mailbox[ADRecipientSchema.DefaultPublicFolderMailbox] = null;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00022D06 File Offset: 0x00020F06
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new SetMailboxTaskModuleFactory();
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00022D10 File Offset: 0x00020F10
		protected override void InternalValidate()
		{
			this.latencyContext = ProvisioningPerformanceHelper.StartLatencyDetection(this);
			base.InternalValidate();
			this.ValidateLitigationHoldLicenseCheck();
			RecipientTypeDetails recipientTypeDetails = this.DataObject.RecipientTypeDetails;
			MailboxTaskHelper.ValidateRoomMailboxPasswordParameterCanOnlyBeUsedWithEnableRoomMailboxPassword(base.Fields.IsModified("RoomMailboxPassword"), base.Fields.IsModified("EnableRoomMailboxAccount"), new Task.ErrorLoggerDelegate(base.WriteError));
			if (base.Fields.IsModified("EnableRoomMailboxAccount"))
			{
				this.ValidateEnableRoomMailboxAccountParameter();
			}
			this.ValidateParametersForChangingPassword();
			bool flag = this.NewPassword != null && this.NewPassword.Length > 0;
			bool flag2 = base.CurrentTaskContext.ExchangeRunspaceConfig != null && base.CurrentTaskContext.ExchangeRunspaceConfig.IsAppPasswordUsed;
			if ((base.UserSpecifiedParameters.IsModified(UserSchema.ResetPasswordOnNextLogon) || base.IsChangingOnPassword || flag) && flag2)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorChangePasswordForAppPasswordAccount), ExchangeErrorCategory.Client, this.Identity);
			}
			if (base.IsChangingOnPassword || (this.EnableRoomMailboxAccount && this.RoomMailboxPassword != null))
			{
				this.ValidatePassword(recipientTypeDetails);
			}
			if (this.rehomeDatabase != null && this.originalDatabase != null)
			{
				this.ValidateCrossVersionRehoming(this.originalDatabase, this.rehomeDatabase);
			}
			if (this.rehomeArchiveDatabase != null && this.originalArchiveDatabase != null)
			{
				this.ValidateCrossVersionRehoming(this.originalArchiveDatabase, this.rehomeArchiveDatabase);
			}
			MailboxTaskHelper.EnsureUserSpecifiedDatabaseMatchesMailboxProvisioningConstraint(this.rehomeDatabase, this.rehomeArchiveDatabase, base.Fields, this.DataObject.MailboxProvisioningConstraint, new Task.ErrorLoggerDelegate(base.WriteError), ADMailboxRecipientSchema.Database);
			if (this.DataObject.IsSoftDeleted)
			{
				foreach (object obj in base.UserSpecifiedParameters.Keys)
				{
					string item = (string)obj;
					if (!SetMailbox.LitigationHoldEnabledParameters.Contains(item))
					{
						base.WriteError(new TaskArgumentException(Strings.ErrorSoftDeletedMailboxCanOnlyChangeLitigationHoldEnabled), ExchangeErrorCategory.Client, this.Identity);
					}
				}
			}
			if ((this.DataObject.IsModified(MailboxSchema.ProhibitSendReceiveQuota) || this.DataObject.IsModified(MailboxSchema.ProhibitSendQuota) || this.DataObject.IsModified(MailboxSchema.IssueWarningQuota)) && this.DataObject.UseDatabaseQuotaDefaults != null && this.DataObject.UseDatabaseQuotaDefaults.Value)
			{
				this.WriteWarning(Strings.WarnAboutSetDatabaseDefaults);
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled && base.CurrentTaskContext.ExchangeRunspaceConfig != null && "LiveIdBasic".Equals(base.CurrentTaskContext.ExchangeRunspaceConfig.AuthenticationType, StringComparison.OrdinalIgnoreCase))
			{
				if (this.DataObject.IsModified(MailboxSchema.UseDatabaseQuotaDefaults) && this.DataObject.UseDatabaseQuotaDefaults.Value)
				{
					base.WriteError(new TaskArgumentException(Strings.ErrorUseDatabaseQuotaDefaultsCanOnlySetToFalse), ExchangeErrorCategory.Client, this.Identity);
				}
				if (this.DataObject.IsModified(MailboxSchema.UseDatabaseRetentionDefaults) && this.DataObject.UseDatabaseRetentionDefaults)
				{
					base.WriteError(new TaskArgumentException(Strings.ErrorUseDatabaseRetentionDefaultsCanOnlySetToFalse), ExchangeErrorCategory.Client, this.Identity);
				}
			}
			this.ValidateOrganizationCapabilities();
			this.ValidateMailboxShapeFeatureVersion();
			this.ValidateCopySentItemToSenderFlags();
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00023070 File Offset: 0x00021270
		private void ValidateCopySentItemToSenderFlags()
		{
			if (this.DataObject.RecipientTypeDetails != RecipientTypeDetails.SharedMailbox && this.DataObject.RecipientTypeDetails != RecipientTypeDetails.RemoteSharedMailbox)
			{
				if (base.Fields.IsModified("MessageCopyForSentAsEnabled"))
				{
					base.WriteError(new TaskArgumentException(Strings.ErrorMessageCopyForSentAsEnabledCanOnlySetOnSharedMailbox), ExchangeErrorCategory.Client, this.Identity);
				}
				if (base.Fields.IsModified("MessageCopyForSendOnBehalfEnabled"))
				{
					base.WriteError(new TaskArgumentException(Strings.ErrorMessageCopyForSendOnBehalfEnabledCanOnlySetOnSharedMailbox), ExchangeErrorCategory.Client, this.Identity);
				}
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000230FC File Offset: 0x000212FC
		private void ValidateCrossVersionRehoming(ADObjectId originalDatabase, MailboxDatabase rehomeDatabase)
		{
			MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(new DatabaseIdParameter(originalDatabase), base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorMailboxDatabaseNotFound(originalDatabase.ToString())), new LocalizedString?(Strings.ErrorMailboxDatabaseNotUnique(originalDatabase.ToString())), ExchangeErrorCategory.Client);
			ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
			DatabaseLocationInfo databaseLocationInfo = MailboxTaskHelper.GetDatabaseLocationInfo(mailboxDatabase, activeManagerInstance, new Task.ErrorLoggerDelegate(base.WriteError));
			if (databaseLocationInfo == null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorReHomeOriginalDatabaseLocationNotFound(mailboxDatabase.ToString())), ExchangeErrorCategory.Client, null);
			}
			DatabaseLocationInfo databaseLocationInfo2 = MailboxTaskHelper.GetDatabaseLocationInfo(rehomeDatabase, activeManagerInstance, new Task.ErrorLoggerDelegate(base.WriteError));
			if (databaseLocationInfo2 == null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorReHomeTargetDatabaseLocationNotFound(rehomeDatabase.ToString())), ExchangeErrorCategory.Client, null);
			}
			if (new ServerVersion(databaseLocationInfo.ServerVersion).Major != new ServerVersion(databaseLocationInfo2.ServerVersion).Major && !base.ShouldContinue(Strings.ConfirmationMessageSetMailboxCrossVersionRehoming(this.Identity.ToString(), mailboxDatabase.Identity.ToString(), rehomeDatabase.Identity.ToString())))
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorSetMailboxCrossVersionRehoming, null), ExchangeErrorCategory.Client, this.Identity);
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00023220 File Offset: 0x00021420
		private void ValidateLitigationHoldLicenseCheck()
		{
			if (base.UserSpecifiedParameters.Contains("LitigationHoldEnabled") && (this.originalLitigationHold ^ this.DataObject.LitigationHoldEnabled) && this.DataObject.LitigationHoldEnabled && base.ExchangeRunspaceConfig != null && !base.ExchangeRunspaceConfig.IsCmdletAllowedInScope("Set-Mailbox", new string[]
			{
				"LitigationHoldDate"
			}, this.DataObject, ScopeLocation.RecipientWrite))
			{
				base.WriteError(new RecipientTaskException(DirectoryStrings.LitigationHold_License_Violation(this.Identity.ToString(), "Set-Mailbox", "LitigationHoldEnabled", "")), ExchangeErrorCategory.Client, this.Identity);
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000232CC File Offset: 0x000214CC
		private void ValidateParametersForChangingPassword()
		{
			bool flag = this.OldPassword != null && this.OldPassword.Length > 0;
			bool flag2 = this.NewPassword != null && this.NewPassword.Length > 0;
			if (base.IsChangingOnPassword && flag2)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorConflictingRestrictionParameters("NewPassword", "Password")), ExchangeErrorCategory.Client, this.Identity);
			}
			if ((!flag && flag2) || (flag && !flag2))
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorChangePasswordRequiresOldPasswordNewPassword, null), ExchangeErrorCategory.Client, this.Identity);
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00023368 File Offset: 0x00021568
		private void ValidatePassword(RecipientTypeDetails recipientTypeDetails)
		{
			if (!base.Fields.IsModified("EnableRoomMailboxAccount") && (this.DataObject.IsResource || recipientTypeDetails == RecipientTypeDetails.SharedMailbox))
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorSetPasswordForLogonDisabledAccount, null), ExchangeErrorCategory.Client, this.Identity);
			}
			if (!base.HasSetPasswordPermission && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.SetPasswordWithoutOldPassword.Enabled)
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorSetPasswordWithoutPermission, null), ExchangeErrorCategory.Client, this.Identity);
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00023418 File Offset: 0x00021618
		protected override IConfigurable PrepareDataObject()
		{
			IConfigurable configurable = base.PrepareDataObject();
			ADUser aduser = (ADUser)configurable;
			if ((this.originalRetentionHold ^ aduser.RetentionHoldEnabled) || aduser.IsModified(ADUserSchema.RetentionComment) || aduser.IsModified(ADUserSchema.RetentionUrl))
			{
				CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, aduser, false, this.ConfirmationMessage, new CmdletProxyInfo.ChangeCmdletProxyParametersDelegate(CmdletProxy.AppendForceToProxyCmdlet));
			}
			if (!this.SkipMailboxProvisioningConstraintValidation)
			{
				if (this.MailboxProvisioningConstraint != null)
				{
					MailboxTaskHelper.ValidateMailboxProvisioningConstraintEntries(new MailboxProvisioningConstraint[]
					{
						this.MailboxProvisioningConstraint
					}, base.DomainController, delegate(string message)
					{
						base.WriteVerbose(new LocalizedString(message));
					}, new Task.ErrorLoggerDelegate(base.WriteError));
				}
				if (this.MailboxProvisioningPreferences != null)
				{
					MailboxTaskHelper.ValidateMailboxProvisioningConstraintEntries(this.MailboxProvisioningPreferences, base.DomainController, delegate(string message)
					{
						base.WriteVerbose(new LocalizedString(message));
					}, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.MailboxProvisioningConstraint))
			{
				aduser.MailboxProvisioningConstraint = this.MailboxProvisioningConstraint;
			}
			if (base.Fields.IsModified(ADRecipientSchema.MailboxProvisioningPreferences))
			{
				aduser.MailboxProvisioningPreferences = this.MailboxProvisioningPreferences;
			}
			if (base.Fields.IsModified("MessageCopyForSentAsEnabled"))
			{
				this.DataObject[MailboxSchema.MessageCopyForSentAsEnabled] = this.MessageCopyForSentAsEnabled;
			}
			if (base.Fields.IsModified("MessageCopyForSendOnBehalfEnabled"))
			{
				this.DataObject[MailboxSchema.MessageCopyForSendOnBehalfEnabled] = this.MessageCopyForSendOnBehalfEnabled;
			}
			return configurable;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000235AA File Offset: 0x000217AA
		protected override bool IsObjectStateChanged()
		{
			return base.RemoveManagedFolderAndPolicy || (this.OldPassword != null && this.OldPassword.Length > 0) || base.IsObjectStateChanged();
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000235D8 File Offset: 0x000217D8
		protected override void InternalProcessRecord()
		{
			try
			{
				if (base.Fields.IsModified(ADMailboxRecipientSchema.Database) && false == base.Force)
				{
					if (base.PublicFolder)
					{
						if (!base.ShouldContinue(Strings.SetPublicFolderMailboxRehomeDatabaseConfirmationMessage(this.Identity.ToString())))
						{
							return;
						}
					}
					else if (!base.ShouldContinue(Strings.ConfirmationMessageSetMailboxWithDatabase(this.Identity.ToString(), this.rehomeDatabase.Identity.ToString())))
					{
						return;
					}
				}
				if (base.Fields.IsModified(ADUserSchema.ArchiveDatabase))
				{
					Guid archiveGuid = this.DataObject.ArchiveGuid;
					if (this.DataObject.ArchiveGuid == Guid.Empty)
					{
						base.WriteError(new TaskArgumentException(Strings.ErrorArchiveNotEnabled(this.Identity.ToString()), null), ExchangeErrorCategory.Client, this.Identity);
					}
				}
				if (!base.Fields.IsModified(ADUserSchema.ArchiveDatabase) || !(false == base.Force) || base.ShouldContinue(Strings.ConfirmationMessageSetMailboxWithDatabase(this.Identity.ToString(), this.rehomeArchiveDatabase.Identity.ToString())))
				{
					bool flag = false;
					if (this.originalLitigationHold ^ this.DataObject.LitigationHoldEnabled)
					{
						flag = true;
						ADUser dataObject = this.DataObject;
						int num;
						TaskHelper.GetRemoteServerForADUser(dataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), out num);
						this.WriteWarning(Strings.WarningSetMailboxLitigationHoldDelay(COWSettings.COWCacheLifeTime.TotalMinutes));
						if (this.DataObject.LitigationHoldEnabled && base.ExchangeRunspaceConfig != null)
						{
							string text = base.ExchangeRunspaceConfig.ExecutingUserPrincipalName;
							if (string.IsNullOrEmpty(text))
							{
								SmtpAddress executingUserPrimarySmtpAddress = base.ExchangeRunspaceConfig.ExecutingUserPrimarySmtpAddress;
								if (!string.IsNullOrEmpty(base.ExchangeRunspaceConfig.ExecutingUserPrimarySmtpAddress.ToString()))
								{
									text = base.ExchangeRunspaceConfig.ExecutingUserPrimarySmtpAddress.ToString();
									base.WriteVerbose(Strings.WarningSetMailboxLitigationOwnerSMTP(text));
								}
								else
								{
									text = base.ExchangeRunspaceConfig.IdentityName;
									base.WriteVerbose(Strings.WarningSetMailboxLitigationOwnerIdentity(text));
								}
							}
							this.DataObject.LitigationHoldOwner = text;
						}
						if (dataObject != null && num >= Server.E15MinVersion)
						{
							dataObject.SetLitigationHoldEnabledWellKnownInPlaceHoldGuid(this.DataObject.LitigationHoldEnabled);
						}
					}
					if (base.Fields.IsModified(ADRecipientSchema.LitigationHoldDuration))
					{
						if (flag)
						{
							if (this.originalLitigationHold)
							{
								base.WriteError(new TaskArgumentException(Strings.WarningSetMailboxLitigationHoldDuration), ExchangeErrorCategory.Client, this.Identity);
								return;
							}
						}
						else if (!this.originalLitigationHold)
						{
							base.WriteError(new TaskArgumentException(Strings.WarningSetMailboxLitigationHoldDuration), ExchangeErrorCategory.Client, this.Identity);
							return;
						}
						if (this.LitigationHoldDuration <= EnhancedTimeSpan.Zero)
						{
							base.WriteError(new LocalizedException(Strings.ErrorSetMailboxLitigationHoldDuration), ExchangeErrorCategory.Client, this.Identity);
						}
						ADUser dataObject2 = this.DataObject;
						dataObject2.LitigationHoldDuration = new Unlimited<EnhancedTimeSpan>?(this.LitigationHoldDuration);
					}
					if (this.originalSingleItemRecovery ^ this.DataObject.SingleItemRecoveryEnabled)
					{
						this.WriteWarning(Strings.WarningSetMailboxSingleItemRecoveryDelay(COWSettings.COWCacheLifeTime.TotalMinutes));
					}
					if ((this.originalRetentionHold ^ this.DataObject.RetentionHoldEnabled) || this.DataObject.IsChanged(ADUserSchema.RetentionComment) || this.DataObject.IsChanged(ADUserSchema.RetentionUrl))
					{
						this.UpdateRetentionDataInStoreConfiguration();
					}
					this.AdjustUMEnabledStatus();
					this.StampOrganizationCapabilities();
					if (base.Fields.IsModified("EnableRoomMailboxAccount"))
					{
						this.ProcessEnableRoomMailboxAccountParameter();
					}
					if (base.Fields.IsModified(ADUserSchema.MailboxContainerGuid))
					{
						this.DataObject.MailboxContainerGuid = this.MailboxContainerGuid;
					}
					if (base.Fields.IsModified("AggregatedMailboxGuid"))
					{
						if (this.AddAggregatedAccount.IsPresent)
						{
							if (!this.DataObject.AggregatedMailboxGuids.Contains(this.AggregatedMailboxGuid))
							{
								this.DataObject.AggregatedMailboxGuids.Add(this.AggregatedMailboxGuid);
							}
						}
						else if (this.RemoveAggregatedAccount.IsPresent && this.DataObject.AggregatedMailboxGuids.Contains(this.AggregatedMailboxGuid))
						{
							this.DataObject.AggregatedMailboxGuids.Remove(this.AggregatedMailboxGuid);
						}
					}
					if (base.Fields.IsModified(ADUserSchema.UnifiedMailbox))
					{
						this.DataObject.UnifiedMailbox = this.UnifiedMailbox;
					}
					if (this.NewPassword != null && this.NewPassword.Length > 0 && this.OldPassword != null && this.OldPassword.Length > 0)
					{
						this.ChangePassword(this.OldPassword, this.NewPassword);
					}
					if (this.DataObject.IsSoftDeleted && this.DataObject.IsModified(MailboxSchema.LitigationHoldEnabled))
					{
						if (!this.DataObject.IsInactiveMailbox && this.DataObject.LitigationHoldEnabled)
						{
							this.DataObject.UpdateSoftDeletedStatusForHold(true);
						}
						else if (this.DataObject.IsInactiveMailbox && !this.DataObject.IsInLitigationHoldOrInplaceHold)
						{
							if (!base.Force && !base.ShouldContinue(Strings.ConfirmationTurnOffLitigationHold(this.DataObject.WhenSoftDeleted.ToString())))
							{
								return;
							}
							this.DataObject.UpdateSoftDeletedStatusForHold(false);
						}
					}
					base.InternalProcessRecord();
					if (this.rehomeDatabase != null)
					{
						if (this.originalDatabase != null)
						{
							this.RefreshMailboxInDatabase(this.DataObject.ExchangeGuid, this.originalDatabase.ObjectGuid);
						}
						this.RefreshMailboxInDatabase(this.DataObject.ExchangeGuid, this.rehomeDatabase.Id.ObjectGuid);
					}
					if (this.rehomeArchiveDatabase != null)
					{
						if (this.originalArchiveDatabase != null)
						{
							this.RefreshMailboxInDatabase(this.DataObject.ArchiveGuid, this.originalArchiveDatabase.ObjectGuid);
						}
						this.RefreshMailboxInDatabase(this.DataObject.ArchiveGuid, this.rehomeArchiveDatabase.Id.ObjectGuid);
					}
					List<PropValue> mailboxShapeParametersToSet = new List<PropValue>();
					List<PropTag> mailboxShapeParametersToDelete = new List<PropTag>();
					if (base.Fields.IsModified("MailboxMessagesPerFolderCountWarningQuota"))
					{
						this.ProcessMailboxShapeParameter(this.MailboxMessagesPerFolderCountWarningQuota, PropTag.MailboxMessagesPerFolderCountWarningQuota, mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
					}
					if (base.Fields.IsModified("MailboxMessagesPerFolderCountReceiveQuota"))
					{
						this.ProcessMailboxShapeParameter(this.MailboxMessagesPerFolderCountReceiveQuota, PropTag.MailboxMessagesPerFolderCountReceiveQuota, mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
					}
					if (base.Fields.IsModified("DumpsterMessagesPerFolderCountWarningQuota"))
					{
						this.ProcessMailboxShapeParameter(this.DumpsterMessagesPerFolderCountWarningQuota, PropTag.DumpsterMessagesPerFolderCountWarningQuota, mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
					}
					if (base.Fields.IsModified("DumpsterMessagesPerFolderCountReceiveQuota"))
					{
						this.ProcessMailboxShapeParameter(this.DumpsterMessagesPerFolderCountReceiveQuota, PropTag.DumpsterMessagesPerFolderCountReceiveQuota, mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
					}
					if (base.Fields.IsModified("FolderHierarchyChildrenCountWarningQuota"))
					{
						this.ProcessMailboxShapeParameter(this.FolderHierarchyChildrenCountWarningQuota, PropTag.FolderHierarchyChildrenCountWarningQuota, mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
					}
					if (base.Fields.IsModified("FolderHierarchyChildrenCountReceiveQuota"))
					{
						this.ProcessMailboxShapeParameter(this.FolderHierarchyChildrenCountReceiveQuota, PropTag.FolderHierarchyChildrenCountReceiveQuota, mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
					}
					if (base.Fields.IsModified("FolderHierarchyDepthWarningQuota"))
					{
						this.ProcessMailboxShapeParameter(this.FolderHierarchyDepthWarningQuota, PropTag.FolderHierarchyDepthWarningQuota, mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
					}
					if (base.Fields.IsModified("FolderHierarchyDepthReceiveQuota"))
					{
						this.ProcessMailboxShapeParameter(this.FolderHierarchyDepthReceiveQuota, PropTag.FolderHierarchyDepthReceiveQuota, mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
					}
					if (base.Fields.IsModified("FoldersCountWarningQuota"))
					{
						this.ProcessMailboxShapeParameter(this.FoldersCountWarningQuota, PropTag.FoldersCountWarningQuota, mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
					}
					if (base.Fields.IsModified("FoldersCountReceiveQuota"))
					{
						this.ProcessMailboxShapeParameter(this.FoldersCountReceiveQuota, PropTag.FoldersCountReceiveQuota, mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
					}
					if (base.Fields.IsModified("ExtendedPropertiesCountQuota"))
					{
						this.ProcessMailboxShapeParameter(this.ExtendedPropertiesCountQuota, PropTag.NamedPropertiesCountQuota, mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
					}
					if (base.Fields.IsModified("AggregatedMailboxGuid") && this.AddAggregatedAccount.IsPresent)
					{
						this.PrePopulateStoreCacheForAggregatedMailbox();
					}
					this.UpdateMailboxShapeParameters(mailboxShapeParametersToSet, mailboxShapeParametersToDelete);
				}
			}
			finally
			{
				ProvisioningPerformanceHelper.StopLatencyDetection(this.latencyContext);
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00023DDC File Offset: 0x00021FDC
		private void UpdateMailboxShapeParameters(List<PropValue> mailboxShapeParametersToSet, List<PropTag> mailboxShapeParametersToDelete)
		{
			if (mailboxShapeParametersToSet.Count > 0 || mailboxShapeParametersToDelete.Count > 0)
			{
				DatabaseLocationInfo databaseLocationInfo = null;
				try
				{
					databaseLocationInfo = ActiveManager.GetActiveManagerInstance().GetServerForDatabase(this.DataObject.Database.ObjectGuid);
				}
				catch (ObjectNotFoundException exception)
				{
					base.WriteError(exception, ExchangeErrorCategory.ServerOperation, null);
				}
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseConnectionMapiRpcInterface(databaseLocationInfo.ServerFqdn));
				}
				using (MapiStore mapiStore = MapiStore.OpenMailbox(databaseLocationInfo.ServerFqdn, this.DataObject.LegacyExchangeDN, this.DataObject.ExchangeGuid, this.DataObject.Database.ObjectGuid, this.DataObject.Name, null, null, ConnectFlag.UseAdminPrivilege | ConnectFlag.UseSeparateConnection, OpenStoreFlag.UseAdminPrivilege | OpenStoreFlag.TakeOwnership | OpenStoreFlag.MailboxGuid, CultureInfo.InvariantCulture, null, "Client=Management;Action=SetMailbox", null))
				{
					PropProblem[] array = null;
					List<PropProblem> list = null;
					if (mailboxShapeParametersToSet.Count > 0)
					{
						array = mapiStore.SetProps(mailboxShapeParametersToSet.ToArray());
					}
					if (array != null)
					{
						list = new List<PropProblem>(array);
					}
					if (mailboxShapeParametersToDelete.Count > 0)
					{
						array = mapiStore.DeleteProps(mailboxShapeParametersToDelete);
					}
					if (array != null)
					{
						if (list != null)
						{
							list.AddRange(array);
						}
						else
						{
							list = new List<PropProblem>(array);
						}
					}
					if (list != null)
					{
						if (base.IsVerboseOn)
						{
							foreach (PropProblem propProblem in list)
							{
								base.WriteVerbose(Strings.VerbosePropertyProblem(propProblem.ToString()));
							}
						}
						base.WriteError(new FailedToUpdateStoreMailboxInformationException(this.Identity.ToString()), ExchangeErrorCategory.ServerTransient, this.Identity);
					}
				}
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00023F90 File Offset: 0x00022190
		private void ProcessMailboxShapeParameter(int? paramValue, PropTag propTag, List<PropValue> mailboxShapeParametersToSet, List<PropTag> mailboxShapeParametersToDelete)
		{
			if (paramValue != null)
			{
				PropValue item = new PropValue(propTag, paramValue.Value);
				mailboxShapeParametersToSet.Add(item);
				return;
			}
			mailboxShapeParametersToDelete.Add(propTag);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00023FCC File Offset: 0x000221CC
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			if (Globals.IsMicrosoftHostedOnly && base.CurrentOrganizationId != null && base.CurrentOrganizationId.OrganizationalUnit != null && base.UserSpecifiedParameters.Contains("LitigationHoldEnabled"))
			{
				recipientSession = SoftDeletedTaskHelper.GetSessionForSoftDeletedObjects(recipientSession, recipientSession.SearchRoot);
			}
			return recipientSession;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00024028 File Offset: 0x00022228
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			ADUser aduser = (ADUser)dataObject;
			if (aduser.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
			{
				base.WriteError(new LocalizedException(Strings.ErrorCmdletNotSupportedForGroupMailbox("Set-Mailbox")), ExchangeErrorCategory.Client, this.Identity);
			}
			if (this.rehomeDatabase != null)
			{
				this.originalDatabase = aduser.Database;
				if (aduser.ArchiveGuid != Guid.Empty && this.rehomeArchiveDatabase == null && aduser.ArchiveDatabase == null)
				{
					aduser.ArchiveDatabase = this.rehomeDatabase.Id;
				}
			}
			if (this.rehomeArchiveDatabase != null)
			{
				this.originalArchiveDatabase = aduser.ArchiveDatabase;
				aduser.ArchiveDatabase = this.rehomeArchiveDatabase.Id;
			}
			this.originalSingleItemRecovery = aduser.SingleItemRecoveryEnabled;
			this.originalLitigationHold = aduser.LitigationHoldEnabled;
			this.originalRetentionHold = aduser.RetentionHoldEnabled;
			if (base.Fields.IsModified(ADUserSchema.QueryBaseDN))
			{
				aduser.QueryBaseDN = this.querybaseDNId;
			}
			base.StampChangesOn(dataObject);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00024124 File Offset: 0x00022324
		private void RefreshMailboxInDatabase(Guid mailboxGuid, Guid dbGuid)
		{
			try
			{
				DatabaseLocationInfo serverForDatabase = ActiveManager.GetActiveManagerInstance().GetServerForDatabase(dbGuid, GetServerForDatabaseFlags.ThrowServerForDatabaseNotFoundException);
				using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=Management", serverForDatabase.ServerFqdn, null, null, null))
				{
					exRpcAdmin.SyncMailboxWithDS(dbGuid, mailboxGuid);
				}
			}
			catch (LocalizedException)
			{
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00024188 File Offset: 0x00022388
		private void ValidateEnableRoomMailboxAccountParameter()
		{
			if (this.DataObject.RecipientTypeDetails == RecipientTypeDetails.LinkedRoomMailbox)
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorEnableRoomMailboxAccountCannotBeUsedWithLinkedRoomMailbox), ExchangeErrorCategory.Client, null);
			}
			if (this.DataObject.ResourceType != ExchangeResourceType.Room)
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorEnableRoomMailboxAccountCanBeUsedWithRoomsOnly), ExchangeErrorCategory.Client, null);
			}
			if (this.EnableRoomMailboxAccount && this.RoomMailboxPassword == null)
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorRoomPasswordMustBeSetWhenEnablingRoomADAccount), ExchangeErrorCategory.Client, null);
			}
			if (!this.EnableRoomMailboxAccount && this.RoomMailboxPassword != null)
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorRoomMailboxPasswordCannotBeSetIfEnableRoomMailboxAccountIsFalse), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00024248 File Offset: 0x00022448
		private void ProcessEnableRoomMailboxAccountParameter()
		{
			if (this.EnableRoomMailboxAccount)
			{
				this.DataObject.ExchangeUserAccountControl &= ~UserAccountControlFlags.AccountDisabled;
				this.DataObject.ExchangeUserAccountControl |= UserAccountControlFlags.NormalAccount;
				base.Password = this.RoomMailboxPassword;
				return;
			}
			this.DataObject.ExchangeUserAccountControl |= (UserAccountControlFlags.AccountDisabled | UserAccountControlFlags.NormalAccount | UserAccountControlFlags.DoNotExpirePassword);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000242AC File Offset: 0x000224AC
		private void AdjustUMEnabledStatus()
		{
			if (this.DataObject.UMEnabled && !Utils.UnifiedMessagingAvailable(this.DataObject))
			{
				try
				{
					if (Utils.RunningInTestMode)
					{
						FaultInjectionUtils.FaultInjectionTracer.TraceTest(3341167933U);
					}
					Utils.ResetUMMailbox(this.DataObject, true);
					Utility.ResetUMADProperties(this.DataObject, true);
				}
				catch (LocalizedException ex)
				{
					base.WriteError(new RecipientTaskException(Strings.MailboxCouldNotBeUmDisabled(this.Identity.ToString(), ex.LocalizedString), ex), ExchangeErrorCategory.ServerOperation, this.DataObject);
				}
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00024344 File Offset: 0x00022544
		private void PrePopulateStoreCacheForAggregatedMailbox()
		{
			DatabaseLocationInfo databaseLocationInfo = null;
			try
			{
				databaseLocationInfo = ActiveManager.GetActiveManagerInstance().GetServerForDatabase(this.DataObject.Database.ObjectGuid);
			}
			catch (ObjectNotFoundException exception)
			{
				base.WriteError(exception, ExchangeErrorCategory.ServerOperation, null);
			}
			if (base.IsVerboseOn)
			{
				base.WriteVerbose(Strings.VerboseConnectionMapiRpcInterface(databaseLocationInfo.ServerFqdn));
			}
			if (databaseLocationInfo != null)
			{
				MailboxTaskHelper.PrepopulateCacheForMailbox(this.DataObject.Database.ObjectGuid, databaseLocationInfo.DatabaseName, databaseLocationInfo.ServerFqdn, this.DataObject.OrganizationId, this.DataObject.LegacyExchangeDN, this.AggregatedMailboxGuid, this.DataObject.OriginatingServer, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00024410 File Offset: 0x00022610
		private void UpdateRetentionDataInStoreConfiguration()
		{
			try
			{
				using (StoreRetentionPolicyTagHelper storeRetentionPolicyTagHelper = StoreRetentionPolicyTagHelper.FromMailboxId(base.DomainController, this.Identity, this.DataObject.OrganizationId))
				{
					storeRetentionPolicyTagHelper.HoldData = new RetentionHoldData(this.DataObject.RetentionHoldEnabled, this.DataObject.RetentionComment, this.DataObject.RetentionUrl);
					storeRetentionPolicyTagHelper.Save();
				}
			}
			catch (ElcUserConfigurationException ex)
			{
				TaskLogger.LogError(ex);
				this.WriteWarning(Strings.WarningSetMailboxRetentionHoldDataFAI(ex.Message));
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000244B8 File Offset: 0x000226B8
		private void ValidateOrganizationCapabilities()
		{
			if ((base.Fields.IsModified("UMDataStorage") || base.Fields.IsModified("OABGen") || base.Fields.IsModified("UMGrammar") || base.Fields.IsModified("ClientExtensions") || base.Fields.IsModified("GMGen") || base.Fields.IsModified("MailRouting") || base.Fields.IsModified("Management") || base.Fields.IsModified("TenantUpgrade") || base.Fields.IsModified("Migration") || base.Fields.IsModified("MessageTracking") || base.Fields.IsModified("OMEncryption") || base.Fields.IsModified("PstProvider") || base.Fields.IsModified("SuiteServiceStorage")) && !base.Arbitration)
			{
				base.WriteError(new InvalidOrgCapabilityParameterException(), ExchangeErrorCategory.Client, null);
			}
			Dictionary<OrganizationCapability, string> dictionary = new Dictionary<OrganizationCapability, string>
			{
				{
					OrganizationCapability.UMDataStorage,
					"UMDataStorage"
				},
				{
					OrganizationCapability.ClientExtensions,
					"ClientExtensions"
				},
				{
					OrganizationCapability.OfficeMessageEncryption,
					"OMEncryption"
				},
				{
					OrganizationCapability.SuiteServiceStorage,
					"SuiteServiceStorage"
				}
			};
			foreach (KeyValuePair<OrganizationCapability, string> keyValuePair in dictionary)
			{
				OrganizationCapability key = keyValuePair.Key;
				string value = keyValuePair.Value;
				if (base.Fields.IsModified(value) && (bool)base.Fields[value] && !this.DataObject.PersistedCapabilities.Contains((Capability)key))
				{
					List<ADUser> organizationMailboxesByCapability = OrganizationMailbox.GetOrganizationMailboxesByCapability((IRecipientSession)base.DataSession, key);
					if (organizationMailboxesByCapability.Count > 0)
					{
						base.WriteError(new MultipleOrgMbxesWithCapabilityException(key.ToString()), ExchangeErrorCategory.Client, null);
					}
				}
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000246DC File Offset: 0x000228DC
		private void ValidateMailboxShapeFeatureVersion()
		{
			long num = 0L;
			if (base.Fields.IsModified("MailboxMessagesPerFolderCountWarningQuota") || base.Fields.IsModified("MailboxMessagesPerFolderCountReceiveQuota") || base.Fields.IsModified("DumpsterMessagesPerFolderCountWarningQuota") || base.Fields.IsModified("DumpsterMessagesPerFolderCountReceiveQuota") || base.Fields.IsModified("FolderHierarchyChildrenCountWarningQuota") || base.Fields.IsModified("FolderHierarchyChildrenCountReceiveQuota"))
			{
				num = 1L;
			}
			if (base.Fields.IsModified("FolderHierarchyDepthWarningQuota") || base.Fields.IsModified("FolderHierarchyDepthReceiveQuota"))
			{
				num = 2L;
			}
			if (base.Fields.IsModified("FoldersCountWarningQuota") || base.Fields.IsModified("FoldersCountReceiveQuota") || base.Fields.IsModified("ExtendedPropertiesCountQuota"))
			{
				num = 4L;
			}
			if (num > 0L)
			{
				DatabaseLocationInfo databaseLocationInfo = null;
				try
				{
					databaseLocationInfo = ActiveManager.GetActiveManagerInstance().GetServerForDatabase(this.DataObject.Database.ObjectGuid);
				}
				catch (ObjectNotFoundException exception)
				{
					base.WriteError(exception, ExchangeErrorCategory.ServerOperation, null);
				}
				using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=Management", databaseLocationInfo.ServerFqdn, null, null, null))
				{
					if ((ulong)exRpcAdmin.GetMailboxShapeServerVersion() < (ulong)num)
					{
						base.WriteError(new UnsupportedMailboxShapeFeatureVersionException(this.Identity.ToString()), ExchangeErrorCategory.Client, this.Identity);
					}
				}
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00024858 File Offset: 0x00022A58
		private void StampOrganizationCapabilities()
		{
			if (base.Fields.IsModified("UMDataStorage"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.UMDataStorage, (bool)base.Fields["UMDataStorage"]);
			}
			if (base.Fields.IsModified("UMGrammar"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.UMGrammar, (bool)base.Fields["UMGrammar"]);
			}
			if (base.Fields.IsModified("OABGen"))
			{
				if ((bool)base.Fields["OABGen"])
				{
					this.WriteWarning(Strings.WarningMustInvokeUpdateOABToStartScheduledGeneration);
				}
				this.AddRemoveOrganizationCapability(OrganizationCapability.OABGen, (bool)base.Fields["OABGen"]);
			}
			if (base.Fields.IsModified("GMGen"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.GMGen, (bool)base.Fields["GMGen"]);
			}
			if (base.Fields.IsModified("ClientExtensions"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.ClientExtensions, (bool)base.Fields["ClientExtensions"]);
			}
			if (base.Fields.IsModified("MailRouting"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.MailRouting, (bool)base.Fields["MailRouting"]);
			}
			if (base.Fields.IsModified("Management"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.Management, (bool)base.Fields["Management"]);
			}
			if (base.Fields.IsModified("TenantUpgrade"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.TenantUpgrade, (bool)base.Fields["TenantUpgrade"]);
			}
			if (base.Fields.IsModified("Migration"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.Migration, (bool)base.Fields["Migration"]);
			}
			if (base.Fields.IsModified("MessageTracking"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.MessageTracking, (bool)base.Fields["MessageTracking"]);
			}
			if (base.Fields.IsModified("OMEncryption"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.OfficeMessageEncryption, (bool)base.Fields["OMEncryption"]);
			}
			if (base.Fields.IsModified("PstProvider"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.PstProvider, (bool)base.Fields["PstProvider"]);
			}
			if (base.Fields.IsModified("SuiteServiceStorage"))
			{
				this.AddRemoveOrganizationCapability(OrganizationCapability.SuiteServiceStorage, (bool)base.Fields["SuiteServiceStorage"]);
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00024AEC File Offset: 0x00022CEC
		private void AddRemoveOrganizationCapability(OrganizationCapability orgCapability, bool add)
		{
			if (add)
			{
				if (!this.DataObject.PersistedCapabilities.Contains((Capability)orgCapability))
				{
					this.DataObject.PersistedCapabilities.Add((Capability)orgCapability);
					return;
				}
			}
			else if (this.DataObject.PersistedCapabilities.Contains((Capability)orgCapability))
			{
				this.DataObject.PersistedCapabilities.Remove((Capability)orgCapability);
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00024B48 File Offset: 0x00022D48
		private void ChangePassword(SecureString oldPassword, SecureString newPassword)
		{
			string domainname = ((ADObjectId)this.DataObject.Identity).DomainId.ToString();
			string username = this.DataObject.SamAccountName;
			SecurityIdentifier masterAccountSid = this.DataObject.MasterAccountSid;
			if (masterAccountSid != null && masterAccountSid.IsAccountSid())
			{
				string friendlyUserName = SecurityPrincipalIdParameter.GetFriendlyUserName(masterAccountSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				if (string.IsNullOrEmpty(friendlyUserName))
				{
					base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(masterAccountSid.ToString(), null, null)), ExchangeErrorCategory.Client, this.Identity);
				}
				string[] array = friendlyUserName.Split(new char[]
				{
					'\\'
				});
				if (array.Length == 2)
				{
					domainname = array[0];
					username = array[1];
				}
				else
				{
					username = array[0];
				}
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(oldPassword);
				intPtr2 = Marshal.SecureStringToGlobalAllocUnicode(newPassword);
				uint num = NativeMethods.NetUserChangePassword(domainname, username, intPtr, intPtr2);
				uint num2 = num;
				if (num2 <= 5U)
				{
					if (num2 == 0U)
					{
						goto IL_18D;
					}
					if (num2 == 5U)
					{
						base.WriteError(new RecipientTaskException(Strings.ChangePasswordLockedOut), ExchangeErrorCategory.Client, this.DataObject.Identity);
						goto IL_18D;
					}
				}
				else
				{
					if (num2 == 86U)
					{
						base.WriteError(new RecipientTaskException(Strings.ChangePasswordInvalidCredentials), ExchangeErrorCategory.Client, this.DataObject.Identity);
						goto IL_18D;
					}
					if (num2 == 2245U)
					{
						base.WriteError(new RecipientTaskException(Strings.ChangePasswordInvalidNewPassword), ExchangeErrorCategory.Client, this.DataObject.Identity);
						goto IL_18D;
					}
				}
				base.WriteError(new RecipientTaskException(Strings.ChangePasswordFailed), ExchangeErrorCategory.Client, this.DataObject.Identity);
				IL_18D:;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr2);
				}
			}
		}

		// Token: 0x040001B3 RID: 435
		private const string ClientExtensionsParameterName = "ClientExtensions";

		// Token: 0x040001B4 RID: 436
		private const string GMGenParameterName = "GMGen";

		// Token: 0x040001B5 RID: 437
		private const string MailRoutingParameterName = "MailRouting";

		// Token: 0x040001B6 RID: 438
		private const string OABGenParameterName = "OABGen";

		// Token: 0x040001B7 RID: 439
		private const string UMGrammarParameterName = "UMGrammar";

		// Token: 0x040001B8 RID: 440
		private const string UMDataStorageParameterName = "UMDataStorage";

		// Token: 0x040001B9 RID: 441
		private const string ManagementParameterName = "Management";

		// Token: 0x040001BA RID: 442
		private const string TenantUpgradeParameterName = "TenantUpgrade";

		// Token: 0x040001BB RID: 443
		private const string MessageTrackingParameterName = "MessageTracking";

		// Token: 0x040001BC RID: 444
		private const string PstProviderParameterName = "PstProvider";

		// Token: 0x040001BD RID: 445
		private const string SuiteServiceStorageParameterName = "SuiteServiceStorage";

		// Token: 0x040001BE RID: 446
		private const string ForestWideDomainControllerAffinityName = "ForestWideDomainControllerAffinityByExecutingUser";

		// Token: 0x040001BF RID: 447
		private const string OMEncryptionParameterName = "OMEncryption";

		// Token: 0x040001C0 RID: 448
		private const string MigrationParameterName = "Migration";

		// Token: 0x040001C1 RID: 449
		private const string MailboxMessagesPerFolderCountWarningQuotaName = "MailboxMessagesPerFolderCountWarningQuota";

		// Token: 0x040001C2 RID: 450
		private const string MailboxMessagesPerFolderCountReceiveQuotaName = "MailboxMessagesPerFolderCountReceiveQuota";

		// Token: 0x040001C3 RID: 451
		private const string DumpsterMessagesPerFolderCountWarningQuotaName = "DumpsterMessagesPerFolderCountWarningQuota";

		// Token: 0x040001C4 RID: 452
		private const string DumpsterMessagesPerFolderCountReceiveQuotaName = "DumpsterMessagesPerFolderCountReceiveQuota";

		// Token: 0x040001C5 RID: 453
		private const string FolderHierarchyChildrenCountWarningQuotaName = "FolderHierarchyChildrenCountWarningQuota";

		// Token: 0x040001C6 RID: 454
		private const string FolderHierarchyChildrenCountReceiveQuotaName = "FolderHierarchyChildrenCountReceiveQuota";

		// Token: 0x040001C7 RID: 455
		private const string FolderHierarchyDepthWarningQuotaName = "FolderHierarchyDepthWarningQuota";

		// Token: 0x040001C8 RID: 456
		private const string FolderHierarchyDepthReceiveQuotaName = "FolderHierarchyDepthReceiveQuota";

		// Token: 0x040001C9 RID: 457
		private const string FoldersCountWarningQuotaName = "FoldersCountWarningQuota";

		// Token: 0x040001CA RID: 458
		private const string FoldersCountReceiveQuotaName = "FoldersCountReceiveQuota";

		// Token: 0x040001CB RID: 459
		private const string ExtendedPropertiesCountQuotaName = "ExtendedPropertiesCountQuota";

		// Token: 0x040001CC RID: 460
		private const string AggregatedMailboxGuidName = "AggregatedMailboxGuid";

		// Token: 0x040001CD RID: 461
		private const string AddAggregatedAccountName = "AddAggregatedAccount";

		// Token: 0x040001CE RID: 462
		private const string RemoveAggregatedAccountName = "RemoveAggregatedAccount";

		// Token: 0x040001CF RID: 463
		private const string MessageCopyForSendOnBehalfEnabledParameterName = "MessageCopyForSendOnBehalfEnabled";

		// Token: 0x040001D0 RID: 464
		private const string MessageCopyForSentAsEnabledParameterName = "MessageCopyForSentAsEnabled";

		// Token: 0x040001D1 RID: 465
		private const string LitigationHoldEnabledName = "LitigationHoldEnabled";

		// Token: 0x040001D2 RID: 466
		private static readonly object[] InvalidPublicFolderParameters = new object[]
		{
			"Arbitration",
			ADRecipientSchema.ArbitrationMailbox,
			ADUserSchema.ArchiveDatabase,
			ADRecipientSchema.AuditEnabled,
			MailboxSchema.ArchiveDomain,
			MailboxSchema.ArchiveName,
			MailboxSchema.ArchiveQuota,
			MailboxSchema.ArchiveStatus,
			MailboxSchema.ArchiveWarningQuota,
			MailboxSchema.RetentionPolicy
		};

		// Token: 0x040001D3 RID: 467
		private static readonly HashSet<string> LitigationHoldEnabledParameters = new HashSet<string>(new string[]
		{
			"Identity",
			"LitigationHoldEnabled",
			"Confirm",
			"Debug",
			"ErrorAction",
			"ErrorVariable",
			"Force",
			"OutBuffer",
			"OutVariable",
			"Verbose",
			"WarningAction",
			"WarningVariable",
			"WhatIf"
		}, StringComparer.OrdinalIgnoreCase);

		// Token: 0x040001D4 RID: 468
		private static readonly object[] PublicFolderOnlyParameters = new object[]
		{
			MailboxSchema.IsExcludedFromServingHierarchy,
			MailboxSchema.IsHierarchyReady
		};

		// Token: 0x040001D5 RID: 469
		private LatencyDetectionContext latencyContext;

		// Token: 0x040001D6 RID: 470
		private MailboxDatabase rehomeDatabase;

		// Token: 0x040001D7 RID: 471
		private ADObjectId originalDatabase;

		// Token: 0x040001D8 RID: 472
		private MailboxDatabase rehomeArchiveDatabase;

		// Token: 0x040001D9 RID: 473
		private ADObjectId originalArchiveDatabase;

		// Token: 0x040001DA RID: 474
		private ADObjectId querybaseDNId;

		// Token: 0x040001DB RID: 475
		private bool originalSingleItemRecovery;

		// Token: 0x040001DC RID: 476
		private bool originalLitigationHold;

		// Token: 0x040001DD RID: 477
		private bool originalRetentionHold;
	}
}
