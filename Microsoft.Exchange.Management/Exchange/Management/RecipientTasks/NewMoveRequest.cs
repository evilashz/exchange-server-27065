using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Management.Tasks.UM;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C79 RID: 3193
	[Cmdlet("New", "MoveRequest", SupportsShouldProcess = true, DefaultParameterSetName = "MigrationLocal")]
	public sealed class NewMoveRequest : NewTaskBase<TransactionalRequestJob>
	{
		// Token: 0x060079DE RID: 31198 RVA: 0x001F0868 File Offset: 0x001EEA68
		public NewMoveRequest()
		{
			this.mrsClient = null;
			this.adSession = null;
			this.mrProvider = null;
			this.BadItemLimit = 0;
			this.specifiedTargetMDB = null;
			this.specifiedArchiveTargetMDB = null;
			this.targetDatabaseForMailbox = null;
			this.targetDatabaseForMailboxArchive = null;
			this.sourceMbxInfo = null;
			this.sourceArchiveInfo = null;
			this.targetMbxInfo = null;
			this.targetArchiveInfo = null;
			this.adUser = null;
			this.moveFlags = RequestFlags.None;
			this.unreachableMrsServers = new List<string>();
			TestIntegration.Instance.ForceRefresh();
			this.globalConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 255, ".ctor", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\MoveRequest\\NewMoveRequest.cs");
		}

		// Token: 0x170025BD RID: 9661
		// (get) Token: 0x060079DF RID: 31199 RVA: 0x001F091F File Offset: 0x001EEB1F
		// (set) Token: 0x060079E0 RID: 31200 RVA: 0x001F0936 File Offset: 0x001EEB36
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public MailboxOrMailUserIdParameter Identity
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170025BE RID: 9662
		// (get) Token: 0x060079E1 RID: 31201 RVA: 0x001F0949 File Offset: 0x001EEB49
		// (set) Token: 0x060079E2 RID: 31202 RVA: 0x001F0960 File Offset: 0x001EEB60
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemoteLegacy")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemote")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocal")]
		public DatabaseIdParameter TargetDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["TargetDatabase"];
			}
			set
			{
				base.Fields["TargetDatabase"] = value;
			}
		}

		// Token: 0x170025BF RID: 9663
		// (get) Token: 0x060079E3 RID: 31203 RVA: 0x001F0973 File Offset: 0x001EEB73
		// (set) Token: 0x060079E4 RID: 31204 RVA: 0x001F098A File Offset: 0x001EEB8A
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemote")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocal")]
		public DatabaseIdParameter ArchiveTargetDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["ArchiveTargetDatabase"];
			}
			set
			{
				base.Fields["ArchiveTargetDatabase"] = value;
			}
		}

		// Token: 0x170025C0 RID: 9664
		// (get) Token: 0x060079E5 RID: 31205 RVA: 0x001F099D File Offset: 0x001EEB9D
		// (set) Token: 0x060079E6 RID: 31206 RVA: 0x001F09C3 File Offset: 0x001EEBC3
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocal")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutbound")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemote")]
		public SwitchParameter PrimaryOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["PrimaryOnly"] ?? false);
			}
			set
			{
				base.Fields["PrimaryOnly"] = value;
			}
		}

		// Token: 0x170025C1 RID: 9665
		// (get) Token: 0x060079E7 RID: 31207 RVA: 0x001F09DB File Offset: 0x001EEBDB
		// (set) Token: 0x060079E8 RID: 31208 RVA: 0x001F0A01 File Offset: 0x001EEC01
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemote")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocal")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutbound")]
		public SwitchParameter ArchiveOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["ArchiveOnly"] ?? false);
			}
			set
			{
				base.Fields["ArchiveOnly"] = value;
			}
		}

		// Token: 0x170025C2 RID: 9666
		// (get) Token: 0x060079E9 RID: 31209 RVA: 0x001F0A19 File Offset: 0x001EEC19
		// (set) Token: 0x060079EA RID: 31210 RVA: 0x001F0A3E File Offset: 0x001EEC3E
		[Parameter(Mandatory = false)]
		public Unlimited<int> BadItemLimit
		{
			get
			{
				return (Unlimited<int>)(base.Fields["BadItemLimit"] ?? RequestTaskHelper.UnlimitedZero);
			}
			set
			{
				base.Fields["BadItemLimit"] = value;
			}
		}

		// Token: 0x170025C3 RID: 9667
		// (get) Token: 0x060079EB RID: 31211 RVA: 0x001F0A56 File Offset: 0x001EEC56
		// (set) Token: 0x060079EC RID: 31212 RVA: 0x001F0A7B File Offset: 0x001EEC7B
		[Parameter(Mandatory = false)]
		public Unlimited<int> LargeItemLimit
		{
			get
			{
				return (Unlimited<int>)(base.Fields["LargeItemLimit"] ?? RequestTaskHelper.UnlimitedZero);
			}
			set
			{
				base.Fields["LargeItemLimit"] = value;
			}
		}

		// Token: 0x170025C4 RID: 9668
		// (get) Token: 0x060079ED RID: 31213 RVA: 0x001F0A93 File Offset: 0x001EEC93
		// (set) Token: 0x060079EE RID: 31214 RVA: 0x001F0AB9 File Offset: 0x001EECB9
		[Parameter(Mandatory = false)]
		public SwitchParameter AcceptLargeDataLoss
		{
			get
			{
				return (SwitchParameter)(base.Fields["AcceptLargeDataLoss"] ?? false);
			}
			set
			{
				base.Fields["AcceptLargeDataLoss"] = value;
			}
		}

		// Token: 0x170025C5 RID: 9669
		// (get) Token: 0x060079EF RID: 31215 RVA: 0x001F0AD4 File Offset: 0x001EECD4
		// (set) Token: 0x060079F0 RID: 31216 RVA: 0x001F0B13 File Offset: 0x001EED13
		[Parameter(Mandatory = false)]
		public SwitchParameter AllowLargeItems
		{
			get
			{
				bool value = this.MoveRequestIs(RequestFlags.IntraOrg);
				return (SwitchParameter)(base.Fields["AllowLargeItems"] ?? value);
			}
			set
			{
				base.Fields["AllowLargeItems"] = value;
			}
		}

		// Token: 0x170025C6 RID: 9670
		// (get) Token: 0x060079F1 RID: 31217 RVA: 0x001F0B2B File Offset: 0x001EED2B
		// (set) Token: 0x060079F2 RID: 31218 RVA: 0x001F0B51 File Offset: 0x001EED51
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemote")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemoteLegacy")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutbound")]
		public SwitchParameter IgnoreTenantMigrationPolicies
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreTenantMigrationPolicies"] ?? false);
			}
			set
			{
				base.Fields["IgnoreTenantMigrationPolicies"] = value;
			}
		}

		// Token: 0x170025C7 RID: 9671
		// (get) Token: 0x060079F3 RID: 31219 RVA: 0x001F0B69 File Offset: 0x001EED69
		// (set) Token: 0x060079F4 RID: 31220 RVA: 0x001F0B8F File Offset: 0x001EED8F
		[Parameter(Mandatory = false)]
		public SwitchParameter CheckInitialProvisioningSetting
		{
			get
			{
				return (SwitchParameter)(base.Fields["CheckInitialProvisioningSetting"] ?? false);
			}
			set
			{
				base.Fields["CheckInitialProvisioningSetting"] = value;
			}
		}

		// Token: 0x170025C8 RID: 9672
		// (get) Token: 0x060079F5 RID: 31221 RVA: 0x001F0BA7 File Offset: 0x001EEDA7
		// (set) Token: 0x060079F6 RID: 31222 RVA: 0x001F0BBE File Offset: 0x001EEDBE
		[Parameter(Mandatory = true, ParameterSetName = "MigrationOutbound")]
		[Parameter(Mandatory = true, ParameterSetName = "MigrationRemote")]
		public Fqdn RemoteHostName
		{
			get
			{
				return (Fqdn)base.Fields["RemoteHostName"];
			}
			set
			{
				base.Fields["RemoteHostName"] = value;
			}
		}

		// Token: 0x170025C9 RID: 9673
		// (get) Token: 0x060079F7 RID: 31223 RVA: 0x001F0BD1 File Offset: 0x001EEDD1
		// (set) Token: 0x060079F8 RID: 31224 RVA: 0x001F0BE8 File Offset: 0x001EEDE8
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemoteLegacy")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutbound")]
		public string RemoteTargetDatabase
		{
			get
			{
				return (string)base.Fields["RemoteTargetDatabase"];
			}
			set
			{
				base.Fields["RemoteTargetDatabase"] = value;
			}
		}

		// Token: 0x170025CA RID: 9674
		// (get) Token: 0x060079F9 RID: 31225 RVA: 0x001F0BFB File Offset: 0x001EEDFB
		// (set) Token: 0x060079FA RID: 31226 RVA: 0x001F0C12 File Offset: 0x001EEE12
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutbound")]
		public string RemoteArchiveTargetDatabase
		{
			get
			{
				return (string)base.Fields["RemoteArchiveTargetDatabase"];
			}
			set
			{
				base.Fields["RemoteArchiveTargetDatabase"] = value;
			}
		}

		// Token: 0x170025CB RID: 9675
		// (get) Token: 0x060079FB RID: 31227 RVA: 0x001F0C25 File Offset: 0x001EEE25
		// (set) Token: 0x060079FC RID: 31228 RVA: 0x001F0C3C File Offset: 0x001EEE3C
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemote")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutbound")]
		public string RemoteOrganizationName
		{
			get
			{
				return (string)base.Fields["RemoteOrganizationName"];
			}
			set
			{
				base.Fields["RemoteOrganizationName"] = value;
			}
		}

		// Token: 0x170025CC RID: 9676
		// (get) Token: 0x060079FD RID: 31229 RVA: 0x001F0C4F File Offset: 0x001EEE4F
		// (set) Token: 0x060079FE RID: 31230 RVA: 0x001F0C66 File Offset: 0x001EEE66
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemote")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutbound")]
		public string ArchiveDomain
		{
			get
			{
				return (string)base.Fields["ArchiveDomain"];
			}
			set
			{
				base.Fields["ArchiveDomain"] = value;
			}
		}

		// Token: 0x170025CD RID: 9677
		// (get) Token: 0x060079FF RID: 31231 RVA: 0x001F0C79 File Offset: 0x001EEE79
		// (set) Token: 0x06007A00 RID: 31232 RVA: 0x001F0C90 File Offset: 0x001EEE90
		[Parameter(Mandatory = false)]
		public string BatchName
		{
			get
			{
				return (string)base.Fields["BatchName"];
			}
			set
			{
				base.Fields["BatchName"] = value;
			}
		}

		// Token: 0x170025CE RID: 9678
		// (get) Token: 0x06007A01 RID: 31233 RVA: 0x001F0CA3 File Offset: 0x001EEEA3
		// (set) Token: 0x06007A02 RID: 31234 RVA: 0x001F0CBA File Offset: 0x001EEEBA
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemote")]
		[Parameter(Mandatory = true, ParameterSetName = "MigrationRemoteLegacy")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutbound")]
		public PSCredential RemoteCredential
		{
			get
			{
				return (PSCredential)base.Fields["RemoteCredential"];
			}
			set
			{
				base.Fields["RemoteCredential"] = value;
			}
		}

		// Token: 0x170025CF RID: 9679
		// (get) Token: 0x06007A03 RID: 31235 RVA: 0x001F0CCD File Offset: 0x001EEECD
		// (set) Token: 0x06007A04 RID: 31236 RVA: 0x001F0CF3 File Offset: 0x001EEEF3
		[Parameter(Mandatory = true, ParameterSetName = "MigrationRemote")]
		public SwitchParameter Remote
		{
			get
			{
				return (SwitchParameter)(base.Fields["Remote"] ?? false);
			}
			set
			{
				base.Fields["Remote"] = value;
			}
		}

		// Token: 0x170025D0 RID: 9680
		// (get) Token: 0x06007A05 RID: 31237 RVA: 0x001F0D0B File Offset: 0x001EEF0B
		// (set) Token: 0x06007A06 RID: 31238 RVA: 0x001F0D31 File Offset: 0x001EEF31
		[Parameter(Mandatory = true, ParameterSetName = "MigrationOutbound")]
		public SwitchParameter Outbound
		{
			get
			{
				return (SwitchParameter)(base.Fields["Outbound"] ?? false);
			}
			set
			{
				base.Fields["Outbound"] = value;
			}
		}

		// Token: 0x170025D1 RID: 9681
		// (get) Token: 0x06007A07 RID: 31239 RVA: 0x001F0D49 File Offset: 0x001EEF49
		// (set) Token: 0x06007A08 RID: 31240 RVA: 0x001F0D6F File Offset: 0x001EEF6F
		[Parameter(Mandatory = true, ParameterSetName = "MigrationRemoteLegacy")]
		public SwitchParameter RemoteLegacy
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoteLegacy"] ?? false);
			}
			set
			{
				base.Fields["RemoteLegacy"] = value;
			}
		}

		// Token: 0x170025D2 RID: 9682
		// (get) Token: 0x06007A09 RID: 31241 RVA: 0x001F0D87 File Offset: 0x001EEF87
		// (set) Token: 0x06007A0A RID: 31242 RVA: 0x001F0D9E File Offset: 0x001EEF9E
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutbound")]
		[Parameter(Mandatory = true, ParameterSetName = "MigrationRemoteLegacy")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemote")]
		public Fqdn RemoteGlobalCatalog
		{
			get
			{
				return (Fqdn)base.Fields["RemoteGlobalCatalog"];
			}
			set
			{
				base.Fields["RemoteGlobalCatalog"] = value;
			}
		}

		// Token: 0x170025D3 RID: 9683
		// (get) Token: 0x06007A0B RID: 31243 RVA: 0x001F0DB1 File Offset: 0x001EEFB1
		// (set) Token: 0x06007A0C RID: 31244 RVA: 0x001F0DC8 File Offset: 0x001EEFC8
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemote")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationRemoteLegacy")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutbound")]
		public Fqdn TargetDeliveryDomain
		{
			get
			{
				return (Fqdn)base.Fields["TargetDeliveryDomain"];
			}
			set
			{
				base.Fields["TargetDeliveryDomain"] = value;
			}
		}

		// Token: 0x170025D4 RID: 9684
		// (get) Token: 0x06007A0D RID: 31245 RVA: 0x001F0DDB File Offset: 0x001EEFDB
		// (set) Token: 0x06007A0E RID: 31246 RVA: 0x001F0DE3 File Offset: 0x001EEFE3
		[Parameter(Mandatory = false)]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x170025D5 RID: 9685
		// (get) Token: 0x06007A0F RID: 31247 RVA: 0x001F0DEC File Offset: 0x001EEFEC
		// (set) Token: 0x06007A10 RID: 31248 RVA: 0x001F0E12 File Offset: 0x001EF012
		[Parameter(Mandatory = false)]
		public SwitchParameter Protect
		{
			get
			{
				return (SwitchParameter)(base.Fields["Protect"] ?? false);
			}
			set
			{
				base.Fields["Protect"] = value;
			}
		}

		// Token: 0x170025D6 RID: 9686
		// (get) Token: 0x06007A11 RID: 31249 RVA: 0x001F0E2A File Offset: 0x001EF02A
		// (set) Token: 0x06007A12 RID: 31250 RVA: 0x001F0E50 File Offset: 0x001EF050
		[Parameter(Mandatory = false)]
		public SwitchParameter SuspendWhenReadyToComplete
		{
			get
			{
				return (SwitchParameter)(base.Fields["SuspendWhenReadyToComplete"] ?? false);
			}
			set
			{
				base.Fields["SuspendWhenReadyToComplete"] = value;
			}
		}

		// Token: 0x170025D7 RID: 9687
		// (get) Token: 0x06007A13 RID: 31251 RVA: 0x001F0E68 File Offset: 0x001EF068
		// (set) Token: 0x06007A14 RID: 31252 RVA: 0x001F0E8E File Offset: 0x001EF08E
		[Parameter(Mandatory = false)]
		public SwitchParameter Suspend
		{
			get
			{
				return (SwitchParameter)(base.Fields["Suspend"] ?? false);
			}
			set
			{
				base.Fields["Suspend"] = value;
			}
		}

		// Token: 0x170025D8 RID: 9688
		// (get) Token: 0x06007A15 RID: 31253 RVA: 0x001F0EA6 File Offset: 0x001EF0A6
		// (set) Token: 0x06007A16 RID: 31254 RVA: 0x001F0EBD File Offset: 0x001EF0BD
		[Parameter(Mandatory = false)]
		public string SuspendComment
		{
			get
			{
				return (string)base.Fields["SuspendComment"];
			}
			set
			{
				base.Fields["SuspendComment"] = value;
			}
		}

		// Token: 0x170025D9 RID: 9689
		// (get) Token: 0x06007A17 RID: 31255 RVA: 0x001F0ED0 File Offset: 0x001EF0D0
		// (set) Token: 0x06007A18 RID: 31256 RVA: 0x001F0EF6 File Offset: 0x001EF0F6
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreRuleLimitErrors
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreRuleLimitErrors"] ?? false);
			}
			set
			{
				base.Fields["IgnoreRuleLimitErrors"] = value;
			}
		}

		// Token: 0x170025DA RID: 9690
		// (get) Token: 0x06007A19 RID: 31257 RVA: 0x001F0F0E File Offset: 0x001EF10E
		// (set) Token: 0x06007A1A RID: 31258 RVA: 0x001F0F34 File Offset: 0x001EF134
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocal")]
		public SwitchParameter DoNotPreserveMailboxSignature
		{
			get
			{
				return (SwitchParameter)(base.Fields["DoNotPreserveMailboxSignature"] ?? false);
			}
			set
			{
				base.Fields["DoNotPreserveMailboxSignature"] = value;
			}
		}

		// Token: 0x170025DB RID: 9691
		// (get) Token: 0x06007A1B RID: 31259 RVA: 0x001F0F4C File Offset: 0x001EF14C
		// (set) Token: 0x06007A1C RID: 31260 RVA: 0x001F0F6E File Offset: 0x001EF16E
		[Parameter(Mandatory = false)]
		public RequestPriority Priority
		{
			get
			{
				return (RequestPriority)(base.Fields["Priority"] ?? RequestPriority.Normal);
			}
			set
			{
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x170025DC RID: 9692
		// (get) Token: 0x06007A1D RID: 31261 RVA: 0x001F0F86 File Offset: 0x001EF186
		// (set) Token: 0x06007A1E RID: 31262 RVA: 0x001F0FA7 File Offset: 0x001EF1A7
		[Parameter(Mandatory = false)]
		public RequestWorkloadType WorkloadType
		{
			get
			{
				return (RequestWorkloadType)(base.Fields["WorkloadType"] ?? RequestWorkloadType.None);
			}
			set
			{
				base.Fields["WorkloadType"] = value;
			}
		}

		// Token: 0x170025DD RID: 9693
		// (get) Token: 0x06007A1F RID: 31263 RVA: 0x001F0FBF File Offset: 0x001EF1BF
		// (set) Token: 0x06007A20 RID: 31264 RVA: 0x001F0FE4 File Offset: 0x001EF1E4
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)(base.Fields["CompletedRequestAgeLimit"] ?? RequestTaskHelper.DefaultCompletedRequestAgeLimit);
			}
			set
			{
				base.Fields["CompletedRequestAgeLimit"] = value;
			}
		}

		// Token: 0x170025DE RID: 9694
		// (get) Token: 0x06007A21 RID: 31265 RVA: 0x001F0FFC File Offset: 0x001EF1FC
		// (set) Token: 0x06007A22 RID: 31266 RVA: 0x001F1022 File Offset: 0x001EF222
		[Parameter(Mandatory = false)]
		public SwitchParameter ForceOffline
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceOffline"] ?? false);
			}
			set
			{
				base.Fields["ForceOffline"] = value;
			}
		}

		// Token: 0x170025DF RID: 9695
		// (get) Token: 0x06007A23 RID: 31267 RVA: 0x001F103A File Offset: 0x001EF23A
		// (set) Token: 0x06007A24 RID: 31268 RVA: 0x001F1060 File Offset: 0x001EF260
		[Parameter(Mandatory = false)]
		public SwitchParameter PreventCompletion
		{
			get
			{
				return (SwitchParameter)(base.Fields["PreventCompletion"] ?? false);
			}
			set
			{
				base.Fields["PreventCompletion"] = value;
			}
		}

		// Token: 0x170025E0 RID: 9696
		// (get) Token: 0x06007A25 RID: 31269 RVA: 0x001F1078 File Offset: 0x001EF278
		// (set) Token: 0x06007A26 RID: 31270 RVA: 0x001F1094 File Offset: 0x001EF294
		[Parameter(Mandatory = false)]
		public SkippableMoveComponent[] SkipMoving
		{
			get
			{
				return (SkippableMoveComponent[])(base.Fields["SkipMoving"] ?? null);
			}
			set
			{
				base.Fields["SkipMoving"] = value;
			}
		}

		// Token: 0x170025E1 RID: 9697
		// (get) Token: 0x06007A27 RID: 31271 RVA: 0x001F10A7 File Offset: 0x001EF2A7
		// (set) Token: 0x06007A28 RID: 31272 RVA: 0x001F10C3 File Offset: 0x001EF2C3
		[Parameter(Mandatory = false)]
		public InternalMrsFlag[] InternalFlags
		{
			get
			{
				return (InternalMrsFlag[])(base.Fields["InternalFlags"] ?? null);
			}
			set
			{
				base.Fields["InternalFlags"] = value;
			}
		}

		// Token: 0x170025E2 RID: 9698
		// (get) Token: 0x06007A29 RID: 31273 RVA: 0x001F10D6 File Offset: 0x001EF2D6
		// (set) Token: 0x06007A2A RID: 31274 RVA: 0x001F10ED File Offset: 0x001EF2ED
		[Parameter(Mandatory = false)]
		public DateTime StartAfter
		{
			get
			{
				return (DateTime)base.Fields["StartAfter"];
			}
			set
			{
				base.Fields["StartAfter"] = value;
			}
		}

		// Token: 0x170025E3 RID: 9699
		// (get) Token: 0x06007A2B RID: 31275 RVA: 0x001F1105 File Offset: 0x001EF305
		// (set) Token: 0x06007A2C RID: 31276 RVA: 0x001F111C File Offset: 0x001EF31C
		[Parameter(Mandatory = false)]
		public DateTime CompleteAfter
		{
			get
			{
				return (DateTime)base.Fields["CompleteAfter"];
			}
			set
			{
				base.Fields["CompleteAfter"] = value;
			}
		}

		// Token: 0x170025E4 RID: 9700
		// (get) Token: 0x06007A2D RID: 31277 RVA: 0x001F1134 File Offset: 0x001EF334
		// (set) Token: 0x06007A2E RID: 31278 RVA: 0x001F1159 File Offset: 0x001EF359
		[Parameter(Mandatory = false)]
		public TimeSpan IncrementalSyncInterval
		{
			get
			{
				return (TimeSpan)(base.Fields["IncrementalSyncInterval"] ?? NewMoveRequest.defaultIncrementalSyncIntervalForMove);
			}
			set
			{
				base.Fields["IncrementalSyncInterval"] = value;
			}
		}

		// Token: 0x170025E5 RID: 9701
		// (get) Token: 0x06007A2F RID: 31279 RVA: 0x001F1171 File Offset: 0x001EF371
		// (set) Token: 0x06007A30 RID: 31280 RVA: 0x001F1197 File Offset: 0x001EF397
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocal")]
		public SwitchParameter ForcePull
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForcePull"] ?? false);
			}
			set
			{
				base.Fields["ForcePull"] = value;
			}
		}

		// Token: 0x170025E6 RID: 9702
		// (get) Token: 0x06007A31 RID: 31281 RVA: 0x001F11AF File Offset: 0x001EF3AF
		// (set) Token: 0x06007A32 RID: 31282 RVA: 0x001F11D5 File Offset: 0x001EF3D5
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocal")]
		public SwitchParameter ForcePush
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForcePush"] ?? false);
			}
			set
			{
				base.Fields["ForcePush"] = value;
			}
		}

		// Token: 0x170025E7 RID: 9703
		// (get) Token: 0x06007A33 RID: 31283 RVA: 0x001F11ED File Offset: 0x001EF3ED
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Microsoft.Exchange.Management.Tasks.Strings.ConfirmationMessageNewMoveRequest(this.adUser.Id.ToString());
			}
		}

		// Token: 0x170025E8 RID: 9704
		// (get) Token: 0x06007A34 RID: 31284 RVA: 0x001F1204 File Offset: 0x001EF404
		private bool PrimaryIsMoving
		{
			get
			{
				return !this.MoveRequestIs(RequestFlags.MoveOnlyArchiveMailbox);
			}
		}

		// Token: 0x170025E9 RID: 9705
		// (get) Token: 0x06007A35 RID: 31285 RVA: 0x001F1214 File Offset: 0x001EF414
		private bool ArchiveIsMoving
		{
			get
			{
				return this.MoveRequestIs(RequestFlags.MoveOnlyArchiveMailbox) || (!this.MoveRequestIs(RequestFlags.MoveOnlyPrimaryMailbox) && this.SourceHasArchive);
			}
		}

		// Token: 0x170025EA RID: 9706
		// (get) Token: 0x06007A36 RID: 31286 RVA: 0x001F123A File Offset: 0x001EF43A
		private bool SourceIsRemote
		{
			get
			{
				return this.MoveRequestIs(RequestFlags.CrossOrg | RequestFlags.Pull);
			}
		}

		// Token: 0x170025EB RID: 9707
		// (get) Token: 0x06007A37 RID: 31287 RVA: 0x001F1244 File Offset: 0x001EF444
		private bool SourceIsLocal
		{
			get
			{
				return !this.SourceIsRemote;
			}
		}

		// Token: 0x170025EC RID: 9708
		// (get) Token: 0x06007A38 RID: 31288 RVA: 0x001F124F File Offset: 0x001EF44F
		private bool TargetIsRemote
		{
			get
			{
				return this.MoveRequestIs(RequestFlags.CrossOrg | RequestFlags.Push);
			}
		}

		// Token: 0x170025ED RID: 9709
		// (get) Token: 0x06007A39 RID: 31289 RVA: 0x001F1258 File Offset: 0x001EF458
		private bool TargetIsLocal
		{
			get
			{
				return !this.TargetIsRemote;
			}
		}

		// Token: 0x170025EE RID: 9710
		// (get) Token: 0x06007A3A RID: 31290 RVA: 0x001F1263 File Offset: 0x001EF463
		private ADUser SourceUser
		{
			get
			{
				if (!this.SourceIsRemote)
				{
					return this.adUser;
				}
				return this.remoteADUser;
			}
		}

		// Token: 0x170025EF RID: 9711
		// (get) Token: 0x06007A3B RID: 31291 RVA: 0x001F127A File Offset: 0x001EF47A
		private ADUser TargetUser
		{
			get
			{
				if (!this.TargetIsRemote)
				{
					return this.adUser;
				}
				return this.remoteADUser;
			}
		}

		// Token: 0x170025F0 RID: 9712
		// (get) Token: 0x06007A3C RID: 31292 RVA: 0x001F1291 File Offset: 0x001EF491
		private bool MailboxHasArchive
		{
			get
			{
				return this.adUser.ArchiveGuid != Guid.Empty;
			}
		}

		// Token: 0x170025F1 RID: 9713
		// (get) Token: 0x06007A3D RID: 31293 RVA: 0x001F12A8 File Offset: 0x001EF4A8
		private bool SourceHasPrimary
		{
			get
			{
				return this.SourceUser.Database != null;
			}
		}

		// Token: 0x170025F2 RID: 9714
		// (get) Token: 0x06007A3E RID: 31294 RVA: 0x001F12BB File Offset: 0x001EF4BB
		private bool SourceHasArchive
		{
			get
			{
				return this.SourceUser.HasLocalArchive;
			}
		}

		// Token: 0x170025F3 RID: 9715
		// (get) Token: 0x06007A3F RID: 31295 RVA: 0x001F12C8 File Offset: 0x001EF4C8
		private bool TargetHasPrimary
		{
			get
			{
				return this.TargetUser.Database != null;
			}
		}

		// Token: 0x170025F4 RID: 9716
		// (get) Token: 0x06007A40 RID: 31296 RVA: 0x001F12DB File Offset: 0x001EF4DB
		private bool TargetHasArchive
		{
			get
			{
				return this.TargetUser.HasLocalArchive;
			}
		}

		// Token: 0x170025F5 RID: 9717
		// (get) Token: 0x06007A41 RID: 31297 RVA: 0x001F12E8 File Offset: 0x001EF4E8
		private bool IsSplitPrimaryAndArchiveScenario
		{
			get
			{
				return this.PrimaryOnly || this.ArchiveOnly || (this.MailboxHasArchive && (!this.SourceHasArchive || this.sourceMbxInfo.MdbGuid != this.sourceArchiveInfo.MdbGuid || this.targetMbxInfo.MdbGuid != this.targetArchiveInfo.MdbGuid));
			}
		}

		// Token: 0x170025F6 RID: 9718
		// (get) Token: 0x06007A42 RID: 31298 RVA: 0x001F1364 File Offset: 0x001EF564
		private string ExecutingUserIdentity
		{
			get
			{
				return base.ExecutingUserIdentityName;
			}
		}

		// Token: 0x06007A43 RID: 31299 RVA: 0x001F136C File Offset: 0x001EF56C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.mrProvider != null)
				{
					this.mrProvider.Dispose();
					this.mrProvider = null;
				}
				if (this.mrsClient != null)
				{
					this.mrsClient.Dispose();
					this.mrsClient = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06007A44 RID: 31300 RVA: 0x001F13AC File Offset: 0x001EF5AC
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (base.ParameterSetName.Equals("MigrationRemote"))
			{
				this.moveFlags |= (RequestFlags.CrossOrg | RequestFlags.Pull);
				if (this.WorkloadType == RequestWorkloadType.None)
				{
					this.WorkloadType = RequestWorkloadType.Onboarding;
				}
			}
			else if (base.ParameterSetName.Equals("MigrationOutbound"))
			{
				this.moveFlags |= (RequestFlags.CrossOrg | RequestFlags.Push);
				if (this.WorkloadType == RequestWorkloadType.None)
				{
					this.WorkloadType = RequestWorkloadType.Offboarding;
				}
			}
			else if (base.ParameterSetName.Equals("MigrationRemoteLegacy"))
			{
				this.moveFlags |= (RequestFlags.CrossOrg | RequestFlags.Pull | RequestFlags.RemoteLegacy);
				if (this.WorkloadType == RequestWorkloadType.None)
				{
					this.WorkloadType = RequestWorkloadType.Onboarding;
				}
			}
			else
			{
				this.moveFlags |= RequestFlags.IntraOrg;
				if (this.WorkloadType == RequestWorkloadType.None)
				{
					this.WorkloadType = RequestWorkloadType.Local;
				}
			}
			if (this.ArchiveOnly)
			{
				this.moveFlags |= RequestFlags.MoveOnlyArchiveMailbox;
			}
			if (this.PrimaryOnly)
			{
				this.moveFlags |= RequestFlags.MoveOnlyPrimaryMailbox;
			}
			if (this.ArchiveOnly)
			{
				if (this.PrimaryOnly)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorIncompatibleParameters("PrimaryOnly", "ArchiveOnly")), ErrorCategory.InvalidArgument, this.Identity);
				}
				if (this.TargetDatabase != null)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorIncompatibleParameters("TargetDatabase", "ArchiveOnly")), ErrorCategory.InvalidArgument, this.Identity);
				}
				if (!string.IsNullOrEmpty(this.RemoteTargetDatabase))
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorIncompatibleParameters("RemoteTargetDatabase", "ArchiveOnly")), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
			if (this.PrimaryOnly)
			{
				if (this.ArchiveTargetDatabase != null)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorIncompatibleParameters("ArchiveTargetDatabase", "PrimaryOnly")), ErrorCategory.InvalidArgument, this.Identity);
				}
				if (!string.IsNullOrEmpty(this.RemoteArchiveTargetDatabase))
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorIncompatibleParameters("RemoteArchiveTargetDatabase", "PrimaryOnly")), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
			if (!string.IsNullOrEmpty(this.RemoteOrganizationName) && this.RemoteCredential != null)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorIncompatibleParameters("RemoteOrganizationName", "RemoteCredential")), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.Outbound)
			{
				if (this.ArchiveOnly)
				{
					if (string.IsNullOrEmpty(this.RemoteArchiveTargetDatabase))
					{
					}
				}
				else if (string.IsNullOrEmpty(this.RemoteTargetDatabase))
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorMissingDependentParameter("RemoteTargetDatabase", "Outbound")), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
			if (this.MoveRequestIs(RequestFlags.IntraOrg) && !this.IsFieldSet("AllowLargeItems") && this.IsFieldSet("LargeItemLimit"))
			{
				this.AllowLargeItems = false;
			}
			if (this.AllowLargeItems && this.IsFieldSet("LargeItemLimit"))
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorIncompatibleParameters("AllowLargeItems", "LargeItemLimit")), ErrorCategory.InvalidArgument, this.Identity);
			}
			RequestTaskHelper.ValidateItemLimits(this.BadItemLimit, this.LargeItemLimit, this.AcceptLargeDataLoss, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), this.ExecutingUserIdentity);
			if (this.SuspendComment != null && !this.Suspend)
			{
				base.WriteError(new SuspendCommentWithoutSuspendPermanentException(), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (!string.IsNullOrEmpty(this.SuspendComment) && this.SuspendComment.Length > 4096)
			{
				base.WriteError(new ParameterLengthExceededPermanentException("SuspendComment", 4096), ErrorCategory.InvalidArgument, this.SuspendComment);
			}
			if (!string.IsNullOrEmpty(this.BatchName) && this.BatchName.Length > 255)
			{
				base.WriteError(new ParameterLengthExceededPermanentException("BatchName", 255), ErrorCategory.InvalidArgument, this.BatchName);
			}
			if (this.ForcePush && this.ForcePull)
			{
				base.WriteError(new IncompatibleParametersException("ForcePull", "ForcePush"), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.ForceOffline && this.SuspendWhenReadyToComplete)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorIncompatibleParameters("ForceOffline", "SuspendWhenReadyToComplete")), ErrorCategory.InvalidArgument, this.ForceOffline);
			}
			if (this.ForceOffline && this.PreventCompletion)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorIncompatibleParameters("ForceOffline", "PreventCompletion")), ErrorCategory.InvalidArgument, this.ForceOffline);
			}
			if (this.MoveRequestIs(RequestFlags.IntraOrg) && SetMoveRequestBase.CheckUserOrgIdIsTenant(base.ExecutingUserOrganizationId))
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorLocalNotForTenantAdmins), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.TargetDatabase != null)
			{
				int num;
				this.specifiedTargetMDB = this.LocateAndVerifyMdb(this.TargetDatabase, false, out num);
			}
			if (this.ArchiveTargetDatabase != null)
			{
				int num;
				this.specifiedArchiveTargetMDB = this.LocateAndVerifyMdb(this.ArchiveTargetDatabase, false, out num);
			}
			DateTime utcNow = DateTime.UtcNow;
			if (this.IsFieldSet("StartAfter"))
			{
				RequestTaskHelper.ValidateStartAfterTime(this.StartAfter.ToUniversalTime(), new Task.TaskErrorLoggingDelegate(base.WriteError), utcNow);
			}
			if ((this.IsFieldSet("StartAfter") || this.IsFieldSet("CompleteAfter")) && this.IsFieldSet("SuspendWhenReadyToComplete"))
			{
				DateTime? startAfter = this.IsFieldSet("StartAfter") ? new DateTime?(this.StartAfter) : null;
				DateTime? completeAfter = this.IsFieldSet("CompleteAfter") ? new DateTime?(this.CompleteAfter) : null;
				RequestTaskHelper.ValidateStartAfterCompleteAfterWithSuspendWhenReadyToComplete(startAfter, completeAfter, this.SuspendWhenReadyToComplete.ToBool(), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.IsFieldSet("IncrementalSyncInterval") && this.IsFieldSet("SuspendWhenReadyToComplete") && this.SuspendWhenReadyToComplete.ToBool())
			{
				base.WriteError(new SuspendWhenReadyToCompleteCannotBeSetWithIncrementalSyncIntervalException(), ErrorCategory.InvalidArgument, this.SuspendWhenReadyToComplete);
			}
			if (this.IsFieldSet("IncrementalSyncInterval"))
			{
				RequestTaskHelper.ValidateIncrementalSyncInterval(this.IncrementalSyncInterval, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.IsFieldSet("StartAfter") && this.IsFieldSet("CompleteAfter"))
			{
				RequestTaskHelper.ValidateStartAfterComesBeforeCompleteAfter(new DateTime?(this.StartAfter.ToUniversalTime()), new DateTime?(this.CompleteAfter.ToUniversalTime()), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
		}

		// Token: 0x06007A45 RID: 31301 RVA: 0x001F1A18 File Offset: 0x001EFC18
		protected override IConfigDataProvider CreateSession()
		{
			this.sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			ADSessionSettings adsessionSettings = ADSessionSettings.RescopeToSubtree(this.sessionSettings);
			if (this.MoveRequestIs(RequestFlags.IntraOrg) && (MapiTaskHelper.IsDatacenter || MapiTaskHelper.IsDatacenterDedicated))
			{
				adsessionSettings.IncludeSoftDeletedObjects = true;
			}
			this.gcSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 1235, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\MoveRequest\\NewMoveRequest.cs");
			this.adSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, adsessionSettings, 1243, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\MoveRequest\\NewMoveRequest.cs");
			this.tenantLocalConfigSession = null;
			if (this.mrProvider != null)
			{
				this.mrProvider.Dispose();
				this.mrProvider = null;
			}
			this.mrProvider = new RequestJobProvider(this.adSession, this.globalConfigSession);
			return this.mrProvider;
		}

		// Token: 0x06007A46 RID: 31302 RVA: 0x001F1B0C File Offset: 0x001EFD0C
		protected override IConfigurable PrepareDataObject()
		{
			TransactionalRequestJob transactionalRequestJob = null;
			IConfigurable result;
			try
			{
				TransactionalRequestJob transactionalRequestJob2 = (TransactionalRequestJob)base.PrepareDataObject();
				transactionalRequestJob = transactionalRequestJob2;
				this.adUser = RequestTaskHelper.ResolveADUser(this.adSession, this.gcSession, base.ServerSettings, this.Identity, base.OptionalIdentityData, this.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), true);
				this.adSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(this.adSession, this.adUser.OrganizationId, true);
				base.CurrentOrganizationId = this.adUser.OrganizationId;
				this.mrProvider.IndexProvider.RecipientSession = this.adSession;
				this.tenantLocalConfigSession = RecipientTaskHelper.GetTenantLocalConfigSession(this.adUser.OrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId);
				transactionalRequestJob2.UserId = this.adUser.Id;
				transactionalRequestJob2.DistinguishedName = this.adUser.DistinguishedName;
				transactionalRequestJob = null;
				result = transactionalRequestJob2;
			}
			finally
			{
				if (transactionalRequestJob != null)
				{
					transactionalRequestJob.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06007A47 RID: 31303 RVA: 0x001F1C34 File Offset: 0x001EFE34
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				RequestTaskHelper.SetSkipMoving(this.SkipMoving, this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError), false);
				RequestTaskHelper.SetInternalFlags(this.InternalFlags, this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
				if (this.SkipMoving == null)
				{
					this.DataObject.SkipKnownCorruptions = ConfigBase<MRSConfigSchema>.GetConfig<bool>("SkipKnownCorruptionsDefault");
				}
				if (this.IsFieldSet("InternalFlags"))
				{
					RequestTaskHelper.SetInternalFlags(this.InternalFlags, this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				if (this.MoveRequestIs(RequestFlags.RemoteLegacy))
				{
					this.moveFlags &= ~(RequestFlags.Push | RequestFlags.Pull);
					if (this.adUser.RecipientType == RecipientType.MailUser)
					{
						this.moveFlags |= RequestFlags.Pull;
						if (!string.IsNullOrEmpty(this.RemoteTargetDatabase))
						{
							base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorParameterValueNotAllowed("RemoteTargetDatabase")), ErrorCategory.InvalidArgument, this.Identity);
						}
					}
					else if (this.adUser.RecipientType == RecipientType.UserMailbox)
					{
						this.moveFlags |= RequestFlags.Push;
						if (string.IsNullOrEmpty(this.RemoteTargetDatabase))
						{
							base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorParameterValueRequired("RemoteTargetDatabase")), ErrorCategory.InvalidArgument, this.Identity);
						}
						if (this.TargetDatabase != null)
						{
							base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorParameterValueNotAllowed("TargetDatabase")), ErrorCategory.InvalidArgument, this.Identity);
						}
					}
					else
					{
						base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorInvalidRecipientType(this.adUser.ToString(), this.adUser.RecipientType.ToString())), ErrorCategory.InvalidArgument, this.Identity);
					}
				}
				this.CheckTenantMigrationPolicySettings();
				this.EnsureUserNotAlreadyBeingMoved();
				this.CheckRequiredPropertiesSetOnUser();
				if (this.adUser.MailboxProvisioningConstraint == null && VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.LegacyRegCodeSupport.Enabled)
				{
					string dedicatedMailboxPlansCustomAttributeName = AppSettings.Current.DedicatedMailboxPlansCustomAttributeName;
					string customAttribute = ADRecipient.GetCustomAttribute(this.adUser, dedicatedMailboxPlansCustomAttributeName);
					if (!string.IsNullOrEmpty(customAttribute))
					{
						string text = null;
						MailboxProvisioningConstraint mailboxProvisioningConstraint = null;
						if (!ADRecipient.TryParseMailboxProvisioningData(customAttribute, out text, out mailboxProvisioningConstraint))
						{
							base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.Error_InvalidLegacyRegionCode(customAttribute)), ExchangeErrorCategory.Client, null);
						}
					}
				}
				if (this.TargetDatabase != null)
				{
					this.EnsureDatabaseAttributesMatchUser(this.specifiedTargetMDB);
				}
				if (this.ArchiveTargetDatabase != null)
				{
					this.EnsureDatabaseAttributesMatchUser(this.specifiedArchiveTargetMDB);
				}
				this.ChooseTargetMDBs();
				if (this.MoveRequestIs(RequestFlags.IntraOrg))
				{
					int num = 0;
					ADObjectId adObjectId = (this.ArchiveOnly && this.adUser.ArchiveDatabase != null) ? this.adUser.ArchiveDatabase : this.adUser.Database;
					MailboxDatabase sourceDatabase = this.LocateAndVerifyMdb(new DatabaseIdParameter(adObjectId), true, out num);
					if (base.ExchangeRunspaceConfig != null && base.ExchangeRunspaceConfig.IsDedicatedTenantAdmin)
					{
						this.DisallowLocalMoveSubmittedByTenantAdmins(this.ArchiveOnly ? this.ArchiveTargetDatabase : this.TargetDatabase, sourceDatabase);
					}
				}
				this.remoteOrgName = this.RemoteOrganizationName;
				if (string.IsNullOrEmpty(this.remoteOrgName) && this.SourceIsRemote && this.RemoteCredential == null)
				{
					if (this.adUser.ExternalEmailAddress == null)
					{
						base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorParameterValueRequired("RemoteOrganizationName")), ErrorCategory.InvalidArgument, this.Identity);
					}
					SmtpAddress smtpAddress = new SmtpAddress(this.adUser.ExternalEmailAddress.AddressString);
					this.remoteOrgName = smtpAddress.Domain;
				}
				this.mrsClient = MailboxReplicationServiceClient.Create(CommonUtils.LocalComputerName, MRSJobType.RequestJobE15_TenantHint);
				this.remoteCred = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, null);
				this.RetrieveSourceMailboxesInfo();
				this.RetrieveTargetMailboxesInfo();
				if (this.MoveRequestIs(RequestFlags.IntraOrg))
				{
					this.ComputeLocalPushPull();
				}
				ADObjectId mdbQueue = this.GetMdbQueue();
				ADObjectId adobjectId = null;
				this.VerifyMdbIsCurrentVersion(mdbQueue, out adobjectId);
				this.mrProvider.AttachToMDB(mdbQueue.ObjectGuid);
				this.CheckForDuplicateMR(true, null);
				this.mrsClient.Dispose();
				this.mrsClient = null;
				this.mrsClient = MailboxReplicationServiceClient.Create(this.globalConfigSession, MRSJobType.RequestJobE15_TenantHint, mdbQueue.ObjectGuid, this.unreachableMrsServers);
				this.RetrieveNonMovingTargetMailboxes();
				this.EnsurePrimaryOnlyIsPresentWhenOnboardingWithCloudArchive();
				this.ValidateMailboxes();
				this.ValidateRecipients();
				this.CheckHalfMailboxOnlyMovingFromOrToDatacenter();
				this.CheckPrimaryCannotBeInDatacenterWhenArchiveIsOnPremise();
				this.CheckOnlineMoveSupported();
				this.DisallowPrimaryOnlyCrossForestMovesWhenMailboxHasNoArchive();
				this.CheckVersionsForArchiveSeparation();
				this.DisallowUsersOnLitigationHoldToPreE14();
				this.CheckDifferentSourceAndTargets();
				this.DisallowRemoteLegacyWithE15Plus();
				this.DisallowArbitrationMailboxesToPreE14();
				this.DisallowArbitrationMailboxesCrossForest();
				this.DisallowDiscoveryMailboxesToPreE14();
				this.DisallowPublicFolderMailboxesToPreE15();
				this.DisallowPublicFolderMailboxesCrossForestOrCrossPremise();
				this.DisallowPublicFolderMailboxesMoveDuringFinalization();
				this.DisallowTeamMailboxesToPreE15();
				this.CheckTeamMailboxesCrossForestOrCrossPremise();
				this.DisallowDiscoveryMailboxesCrossForest();
				this.DisallowUCSMigratedMailboxesToPreE15();
				this.DisallowMailboxMovesWithInPlaceHoldToPreE15();
				this.DisallowMailboxMoveWithSubscriptions();
				if (this.PrimaryIsMoving)
				{
					this.VerifyMailboxQuota(this.sourceMbxInfo, this.targetMbxInfo, false);
				}
				if (this.ArchiveIsMoving)
				{
					this.VerifyMailboxQuota(this.sourceArchiveInfo, this.targetArchiveInfo, true);
				}
				if (this.PrimaryIsMoving && this.targetMbxInfo.ServerVersion < Server.E2007MinVersion && this.sourceMbxInfo.RulesSize > 32768)
				{
					if (this.IgnoreRuleLimitErrors)
					{
						this.WriteWarning(Microsoft.Exchange.Management.Tasks.Strings.WarningRulesWillNotBeCopied(this.sourceMbxInfo.MailboxIdentity));
					}
					else
					{
						base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorRulesSizeOverLimit(this.sourceMbxInfo.MailboxIdentity)), ErrorCategory.InvalidArgument, this.Identity);
					}
				}
				if (this.MoveRequestIs(RequestFlags.CrossOrg) && this.PrimaryIsMoving)
				{
					if (this.TargetDeliveryDomain == null)
					{
						base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorParameterValueRequired("TargetDeliveryDomain")), ErrorCategory.InvalidArgument, this.Identity);
					}
					ADUser aduser = ConfigurableObjectXML.Deserialize<ADUser>(this.targetMbxInfo.UserDataXML);
					if (aduser == null)
					{
						base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorMRSIsDownlevel(this.mrsClient.ServerVersion.ComputerName, this.mrsClient.ServerVersion.ToString())), ErrorCategory.InvalidArgument, this.Identity);
					}
					CommonUtils.ValidateTargetDeliveryDomain(aduser.EmailAddresses, this.TargetDeliveryDomain);
					if (this.SourceIsRemote && VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
					{
						RecipientTaskHelper.ValidateSmtpAddress(this.tenantLocalConfigSession, this.TargetUser.EmailAddresses, this.TargetUser, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache, true);
					}
				}
				if (this.specifiedTargetMDB != null)
				{
					MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets(this.sessionSettings, this.specifiedTargetMDB, new Task.ErrorLoggerDelegate(base.WriteError));
				}
				if (this.specifiedArchiveTargetMDB != null)
				{
					MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets(this.sessionSettings, this.specifiedArchiveTargetMDB, new Task.ErrorLoggerDelegate(base.WriteError));
				}
				if (this.SourceIsRemote)
				{
					this.ValidateUMSettings();
				}
				RequestTaskHelper.ValidateNotImplicitSplit(this.moveFlags, this.SourceUser, new Task.TaskErrorLoggingDelegate(base.WriteError), this.Identity);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007A48 RID: 31304 RVA: 0x001F2304 File Offset: 0x001F0504
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				TransactionalRequestJob dataObject = this.DataObject;
				DateTime utcNow = DateTime.UtcNow;
				dataObject.TimeTracker.SetTimestamp(RequestJobTimestamp.Creation, new DateTime?(utcNow));
				dataObject.TimeTracker.CurrentState = RequestState.Queued;
				if (this.PreventCompletion || this.SuspendWhenReadyToComplete)
				{
					dataObject.JobType = MRSJobType.RequestJobE15_TenantHint;
				}
				else
				{
					dataObject.JobType = MRSJobType.RequestJobE15_AutoResume;
				}
				dataObject.WorkloadType = this.WorkloadType;
				dataObject.RequestGuid = Guid.NewGuid();
				dataObject.UserId = this.adUser.Id;
				dataObject.ExchangeGuid = this.adUser.ExchangeGuid;
				RequestTaskHelper.ApplyOrganization(dataObject, this.adUser.OrganizationId ?? OrganizationId.ForestWideOrgId);
				if (this.IsFieldSet("StartAfter"))
				{
					RequestTaskHelper.SetStartAfter(new DateTime?(this.StartAfter), dataObject, null);
				}
				if (this.IsFieldSet("CompleteAfter"))
				{
					RequestTaskHelper.SetCompleteAfter(new DateTime?(this.CompleteAfter), dataObject, null);
				}
				dataObject.TimeTracker.SetTimestamp(RequestJobTimestamp.LastSuccessfulSourceConnection, new DateTime?(utcNow));
				dataObject.TimeTracker.SetTimestamp(RequestJobTimestamp.LastSuccessfulTargetConnection, new DateTime?(utcNow));
				dataObject.IncrementalSyncInterval = this.IncrementalSyncInterval;
				if (this.SourceUser.ArchiveGuid != Guid.Empty && this.SourceUser.ArchiveDomain == null)
				{
					dataObject.ArchiveGuid = new Guid?(this.SourceUser.ArchiveGuid);
				}
				dataObject.UserOrgName = ((this.adUser.OrganizationId != null && this.adUser.OrganizationId.OrganizationalUnit != null) ? this.adUser.OrganizationId.OrganizationalUnit.Name : this.adUser.Id.DomainId.ToString());
				dataObject.DistinguishedName = this.adUser.DistinguishedName;
				dataObject.DisplayName = this.adUser.DisplayName;
				dataObject.Alias = this.adUser.Alias;
				dataObject.User = this.adUser;
				dataObject.Flags = this.moveFlags;
				if (this.MoveRequestIs(RequestFlags.IntraOrg))
				{
					if (this.PrimaryIsMoving)
					{
						dataObject.SourceDatabase = ADObjectIdResolutionHelper.ResolveDN(this.adUser.Database);
						dataObject.TargetDatabase = this.targetDatabaseForMailbox;
					}
					if (this.ArchiveIsMoving)
					{
						dataObject.SourceArchiveDatabase = ADObjectIdResolutionHelper.ResolveDN(this.adUser.ArchiveDatabase ?? this.adUser.Database);
						dataObject.TargetArchiveDatabase = this.targetDatabaseForMailboxArchive;
					}
					dataObject.PreserveMailboxSignature = !this.DoNotPreserveMailboxSignature;
				}
				else if (this.MoveRequestIs(RequestFlags.CrossOrg))
				{
					dataObject.RemoteCredential = this.remoteCred;
					dataObject.TargetDeliveryDomain = this.TargetDeliveryDomain;
					dataObject.PreserveMailboxSignature = false;
					if (this.MoveRequestIs(RequestFlags.Push))
					{
						if (this.PrimaryIsMoving)
						{
							dataObject.SourceDatabase = ADObjectIdResolutionHelper.ResolveDN(this.adUser.Database);
							dataObject.RemoteDatabaseName = this.targetMbxInfo.MdbName;
							dataObject.RemoteDatabaseGuid = new Guid?(this.targetMbxInfo.MdbGuid);
						}
						if (this.ArchiveIsMoving)
						{
							dataObject.SourceArchiveDatabase = ADObjectIdResolutionHelper.ResolveDN(this.adUser.ArchiveDatabase ?? this.adUser.Database);
							dataObject.RemoteArchiveDatabaseName = this.targetArchiveInfo.MdbName;
							dataObject.RemoteArchiveDatabaseGuid = new Guid?(this.targetArchiveInfo.MdbGuid);
						}
						dataObject.TargetDatabase = null;
						dataObject.TargetArchiveDatabase = null;
						if (this.MoveRequestIs(RequestFlags.RemoteLegacy))
						{
							dataObject.TargetDCName = this.RemoteGlobalCatalog;
							dataObject.TargetCredential = this.remoteCred;
						}
					}
					else
					{
						dataObject.SourceDatabase = null;
						dataObject.SourceArchiveDatabase = null;
						if (this.PrimaryIsMoving)
						{
							dataObject.TargetDatabase = this.targetDatabaseForMailbox;
							dataObject.RemoteDatabaseName = this.sourceMbxInfo.MdbName;
							dataObject.RemoteDatabaseGuid = new Guid?(this.sourceMbxInfo.MdbGuid);
						}
						if (this.ArchiveIsMoving)
						{
							dataObject.TargetArchiveDatabase = this.targetDatabaseForMailboxArchive;
							dataObject.RemoteArchiveDatabaseName = this.sourceArchiveInfo.MdbName;
							dataObject.RemoteArchiveDatabaseGuid = new Guid?(this.sourceArchiveInfo.MdbGuid);
						}
						if (this.MoveRequestIs(RequestFlags.RemoteLegacy))
						{
							dataObject.SourceDCName = this.RemoteGlobalCatalog;
							dataObject.SourceCredential = this.remoteCred;
						}
					}
					if (!this.MoveRequestIs(RequestFlags.RemoteLegacy))
					{
						dataObject.RemoteHostName = this.RemoteHostName;
						dataObject.RemoteOrgName = this.remoteOrgName;
						if (!string.IsNullOrEmpty(this.RemoteGlobalCatalog))
						{
							dataObject.RemoteDomainControllerToUpdate = this.RemoteGlobalCatalog;
						}
						if (this.Outbound && !this.ArchiveIsMoving && this.MailboxHasArchive)
						{
							ExAssert.RetailAssert(MapiTaskHelper.IsDatacenter, "Splitting archive is only supported from datacenter.");
							if (string.IsNullOrEmpty(this.ArchiveDomain))
							{
								base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorParameterRequired("ArchiveDomain")), ErrorCategory.InvalidArgument, this.Identity);
							}
						}
						dataObject.ArchiveDomain = (this.ArchiveDomain ?? this.TargetDeliveryDomain);
					}
				}
				if (this.PrimaryIsMoving)
				{
					dataObject.SourceVersion = this.sourceMbxInfo.ServerVersion;
					dataObject.SourceServer = ((this.sourceMbxInfo.ServerInformation != null) ? this.sourceMbxInfo.ServerInformation.MailboxServerName : null);
					dataObject.TargetVersion = this.targetMbxInfo.ServerVersion;
					dataObject.TargetServer = ((this.targetMbxInfo.ServerInformation != null) ? this.targetMbxInfo.ServerInformation.MailboxServerName : null);
					dataObject.TotalMailboxItemCount = this.sourceMbxInfo.MailboxItemCount;
					dataObject.TotalMailboxSize = this.sourceMbxInfo.MailboxSize;
					dataObject.TargetContainerGuid = this.adUser.MailboxContainerGuid;
				}
				if (this.ArchiveIsMoving)
				{
					dataObject.SourceArchiveVersion = this.sourceArchiveInfo.ServerVersion;
					dataObject.SourceArchiveServer = ((this.sourceArchiveInfo.ServerInformation != null) ? this.sourceArchiveInfo.ServerInformation.MailboxServerName : null);
					dataObject.TargetArchiveVersion = this.targetArchiveInfo.ServerVersion;
					dataObject.TargetArchiveServer = ((this.targetArchiveInfo.ServerInformation != null) ? this.targetArchiveInfo.ServerInformation.MailboxServerName : null);
					dataObject.TotalArchiveItemCount = new ulong?(this.sourceArchiveInfo.MailboxItemCount);
					dataObject.TotalArchiveSize = new ulong?(this.sourceArchiveInfo.MailboxSize);
				}
				dataObject.BatchName = this.BatchName;
				dataObject.IgnoreRuleLimitErrors = this.IgnoreRuleLimitErrors;
				dataObject.Priority = this.ComputePriority();
				dataObject.CompletedRequestAgeLimit = this.CompletedRequestAgeLimit;
				dataObject.RequestCreator = this.ExecutingUserIdentity;
				dataObject.BadItemLimit = this.BadItemLimit;
				dataObject.LargeItemLimit = this.LargeItemLimit;
				dataObject.AllowLargeItems = this.AllowLargeItems;
				if (this.Protect)
				{
					dataObject.Flags |= RequestFlags.Protected;
				}
				if (this.ForceOffline)
				{
					dataObject.ForceOfflineMove = true;
					dataObject.Flags |= RequestFlags.Offline;
				}
				if (this.PreventCompletion)
				{
					dataObject.PreventCompletion = true;
				}
				RequestTaskHelper.SetSkipMoving(this.SkipMoving, dataObject, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
				RequestTaskHelper.SetInternalFlags(this.InternalFlags, dataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
				if (this.SkipMoving == null)
				{
					dataObject.SkipKnownCorruptions = ConfigBase<MRSConfigSchema>.GetConfig<bool>("SkipKnownCorruptionsDefault");
				}
				bool flag = this.SourceUser.IsFromDatacenter && this.TargetUser.IsFromDatacenter && (!dataObject.SkipMailboxReleaseCheck && !dataObject.BlockFinalization) && !dataObject.PreventCompletion;
				if (flag)
				{
					this.CheckMailboxRelease();
				}
				dataObject.Status = RequestStatus.Queued;
				dataObject.Suspend = this.Suspend;
				dataObject.SuspendWhenReadyToComplete = this.SuspendWhenReadyToComplete;
				if (!string.IsNullOrEmpty(this.SuspendComment))
				{
					dataObject.Message = MrsStrings.MoveRequestMessageInformational(new LocalizedString(this.SuspendComment));
				}
				dataObject.DomainControllerToUpdate = this.adUser.OriginatingServer;
				dataObject.RequestJobState = JobProcessingState.Ready;
				dataObject.Identity = new RequestJobObjectId(dataObject.ExchangeGuid, dataObject.WorkItemQueueMdb.ObjectGuid, null);
				dataObject.RequestQueue = ADObjectIdResolutionHelper.ResolveDN(dataObject.WorkItemQueueMdb);
				if (!base.Stopping)
				{
					ReportData reportData = new ReportData(dataObject.ExchangeGuid, dataObject.ReportVersion);
					reportData.Delete(this.mrProvider.SystemMailbox);
					ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.Cmdlet, VersionInformation.MRS);
					reportData.Append(MrsStrings.ReportMoveRequestCreated(this.ExecutingUserIdentity), connectivityRec);
					if (this.AcceptLargeDataLoss)
					{
						reportData.Append(MrsStrings.ReportLargeAmountOfDataLossAccepted2(this.BadItemLimit.ToString(), this.LargeItemLimit.ToString(), this.ExecutingUserIdentity));
					}
					reportData.Flush(this.mrProvider.SystemMailbox);
					base.InternalProcessRecord();
					RequestJobLog.Write(dataObject);
					this.CheckForDuplicateMR(false, dataObject);
					if (this.mrsClient.ServerVersion[3])
					{
						this.mrsClient.RefreshMoveRequest2(dataObject.ExchangeGuid, dataObject.WorkItemQueueMdb.ObjectGuid, (int)dataObject.Flags, MoveRequestNotification.Created);
					}
					else
					{
						this.mrsClient.RefreshMoveRequest(dataObject.ExchangeGuid, dataObject.WorkItemQueueMdb.ObjectGuid, MoveRequestNotification.Created);
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007A49 RID: 31305 RVA: 0x001F2C84 File Offset: 0x001F0E84
		protected override void InternalStateReset()
		{
			this.adUser = null;
			this.remoteADUser = null;
			if (this.DataObject != null)
			{
				this.DataObject.Dispose();
				this.DataObject = null;
			}
			base.InternalStateReset();
			if (this.mrsClient != null)
			{
				this.mrsClient.Dispose();
				this.mrsClient = null;
			}
			this.sourceMbxInfo = null;
			this.sourceArchiveInfo = null;
			this.targetMbxInfo = null;
			this.targetArchiveInfo = null;
		}

		// Token: 0x06007A4A RID: 31306 RVA: 0x001F2CF5 File Offset: 0x001F0EF5
		protected override bool IsKnownException(Exception exception)
		{
			return SetMoveRequestBase.IsKnownExceptionHandler(exception, new WriteVerboseDelegate(base.WriteVerbose)) || base.IsKnownException(exception);
		}

		// Token: 0x06007A4B RID: 31307 RVA: 0x001F2D14 File Offset: 0x001F0F14
		protected override void TranslateException(ref Exception e, out ErrorCategory category)
		{
			LocalizedException ex = SetMoveRequestBase.TranslateExceptionHandler(e);
			if (ex == null)
			{
				ErrorCategory errorCategory;
				base.TranslateException(ref e, out errorCategory);
				category = errorCategory;
				return;
			}
			e = ex;
			category = ErrorCategory.ResourceUnavailable;
		}

		// Token: 0x06007A4C RID: 31308 RVA: 0x001F2D40 File Offset: 0x001F0F40
		protected override void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			base.WriteVerbose(TaskVerboseStringHelper.GetReadObjectVerboseString(this.DataObject.Identity, base.DataSession, typeof(TransactionalRequestJob)));
			IConfigurable configurable = null;
			try
			{
				try
				{
					configurable = base.DataSession.Read<TransactionalRequestJob>(this.DataObject.Identity);
				}
				finally
				{
					base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
				}
				if (configurable == null)
				{
					base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.ResolveIdentityString(this.DataObject.Identity), typeof(TransactionalRequestJob).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, this.DataObject.Identity);
				}
				this.WriteResult(configurable);
			}
			finally
			{
				IDisposable disposable = configurable as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
					configurable = null;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007A4D RID: 31309 RVA: 0x001F2E54 File Offset: 0x001F1054
		protected override void WriteResult(IConfigurable dataObject)
		{
			MoveRequestStatistics moveRequestStatistics = new MoveRequestStatistics((TransactionalRequestJob)dataObject);
			if (this.adUser != null)
			{
				moveRequestStatistics.DisplayName = this.adUser.DisplayName;
				moveRequestStatistics.Alias = this.adUser.Alias;
			}
			base.WriteResult(moveRequestStatistics);
		}

		// Token: 0x06007A4E RID: 31310 RVA: 0x001F2EA0 File Offset: 0x001F10A0
		private void CheckRequiredPropertiesSetOnUser()
		{
			if (this.adUser.ExchangeGuid == Guid.Empty)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorInvalidObjectMissingCriticalProperty(this.adUser.RecipientType.ToString(), this.adUser.ToString(), ADMailboxRecipientSchema.ExchangeGuid.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (string.IsNullOrEmpty(this.adUser.LegacyExchangeDN))
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorInvalidObjectMissingCriticalProperty(this.adUser.RecipientType.ToString(), this.adUser.ToString(), ADRecipientSchema.LegacyExchangeDN.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (string.IsNullOrEmpty(this.adUser.Alias))
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorInvalidObjectMissingCriticalProperty(this.adUser.RecipientType.ToString(), this.adUser.ToString(), ADRecipientSchema.Alias.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.SourceIsLocal)
			{
				if (this.PrimaryIsMoving && this.adUser.Database == null)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorSourcePrimaryMailboxDoesNotExist(this.adUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
				}
				if (this.ArchiveOnly && !this.adUser.HasLocalArchive)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorSourceArchiveMailboxDoesNotExist(this.adUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
			if (this.SourceIsRemote && this.TargetIsLocal)
			{
				if (this.PrimaryIsMoving && this.adUser.Database != null)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorTargetUserAlreadyHasPrimaryMailbox(this.adUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
				}
				if (this.ArchiveOnly && this.adUser.HasLocalArchive)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorTargetUserAlreadyHasArchiveMailbox(this.adUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
		}

		// Token: 0x06007A4F RID: 31311 RVA: 0x001F30AC File Offset: 0x001F12AC
		private void DisallowLocalMoveSubmittedByTenantAdmins(DatabaseIdParameter specifiedTargetMDB, MailboxDatabase sourceDatabase)
		{
			if (specifiedTargetMDB == null && (this.adUser.MailboxProvisioningConstraint == null || this.adUser.MailboxProvisioningConstraint.IsMatch(sourceDatabase.MailboxProvisioningAttributes)))
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorLocalNotForTenantAdmins), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A50 RID: 31312 RVA: 0x001F30F8 File Offset: 0x001F12F8
		private void DisallowPrimaryOnlyCrossForestMovesWhenMailboxHasNoArchive()
		{
			if (this.PrimaryOnly && this.MoveRequestIs(RequestFlags.CrossOrg) && !this.MailboxHasArchive)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorPrimaryOnlyCrossForestMovesWithoutArchive(this.adUser.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A51 RID: 31313 RVA: 0x001F3148 File Offset: 0x001F1348
		private void CheckTenantMigrationPolicySettings()
		{
			if (this.IgnoreTenantMigrationPolicies)
			{
				return;
			}
			if (!this.CheckOnboardingOffboarding())
			{
				return;
			}
			ADObjectId organizationalUnit = this.adUser.OrganizationId.OrganizationalUnit;
			ADObjectId configurationUnit = this.adUser.OrganizationId.ConfigurationUnit;
			Organization organization = this.GetOrganization();
			if (organization == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(Microsoft.Exchange.Management.Tasks.Strings.ErrorOrganizationNotFound(configurationUnit.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.SourceIsRemote && organization.IsExcludedFromOnboardMigration)
			{
				base.WriteError(new OnboardingDisabledException(), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.TargetIsRemote && organization.IsExcludedFromOffboardMigration)
			{
				base.WriteError(new OffboardingDisabledException(), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (organization.MaxConcurrentMigrations.IsUnlimited)
			{
				return;
			}
			int value = organization.MaxConcurrentMigrations.Value;
			if (value > 1000)
			{
				return;
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ExistsFilter(ADUserSchema.MailboxMoveStatus),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADUserSchema.MailboxMoveStatus, RequestStatus.None),
				new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox),
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.MailUser)
				}),
				new BitMaskAndFilter(ADUserSchema.MailboxMoveFlags, 1UL)
			});
			MrsTracer.Cmdlet.Debug("Querying for up to {0} remote MoveRequests...", new object[]
			{
				value
			});
			ADRawEntry[] array = this.adSession.Find(organizationalUnit, QueryScope.OneLevel, filter, null, value, NewMoveRequest.simpleSearchResult);
			MrsTracer.Cmdlet.Debug("Found {0} remote MoveRequests.", new object[]
			{
				(array == null) ? "null" : array.Length.ToString()
			});
			if (array != null && array.Length >= value)
			{
				base.WriteError(new MaxConcurrentMigrationsExceededException(value), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A52 RID: 31314 RVA: 0x001F3334 File Offset: 0x001F1534
		private void EnsureUserNotAlreadyBeingMoved()
		{
			if (this.adUser.MailboxMoveStatus == RequestStatus.Completed || this.adUser.MailboxMoveStatus == RequestStatus.CompletedWithWarning)
			{
				base.WriteError(new ManagementObjectAlreadyExistsException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCompletedMoveMustBeCleared(this.adUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
				return;
			}
			if (this.adUser.MailboxMoveStatus != RequestStatus.None)
			{
				string target;
				if (this.adUser.MailboxMoveTargetMDB != null)
				{
					target = this.adUser.MailboxMoveTargetMDB.ToString();
				}
				else if (this.adUser.MailboxMoveTargetArchiveMDB != null)
				{
					target = this.adUser.MailboxMoveTargetArchiveMDB.ToString();
				}
				else
				{
					target = this.adUser.MailboxMoveRemoteHostName;
				}
				base.WriteError(new ManagementObjectAlreadyExistsException(Microsoft.Exchange.Management.Tasks.Strings.ErrorUserIsAlreadyBeingMoved(this.adUser.ToString(), target)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A53 RID: 31315 RVA: 0x001F3400 File Offset: 0x001F1600
		private void ChooseTargetMDBs()
		{
			if (this.TargetIsLocal)
			{
				MailboxDatabase mailboxDatabase = null;
				if (!this.ArchiveOnly)
				{
					if (this.specifiedTargetMDB != null)
					{
						this.targetDatabaseForMailbox = this.specifiedTargetMDB.Id;
						mailboxDatabase = this.specifiedTargetMDB;
					}
					else
					{
						mailboxDatabase = this.ChooseTargetMDB(this.adUser.Database);
						this.targetDatabaseForMailbox = ADObjectIdResolutionHelper.ResolveDN(mailboxDatabase.Id);
					}
					if (CommonUtils.ShouldHonorProvisioningSettings() && (this.MoveRequestIs(RequestFlags.IntraOrg) || this.MoveRequestIs(RequestFlags.Pull)) && !this.InternalFlagsContains(InternalMrsFlag.SkipProvisioningCheck) && mailboxDatabase.IsExcludedFromProvisioning)
					{
						base.WriteError(new DatabaseExcludedFromProvisioningException(mailboxDatabase.Name), ErrorCategory.InvalidArgument, this.Identity);
					}
				}
				if (!this.PrimaryOnly)
				{
					MailboxDatabase mailboxDatabase2;
					if (this.specifiedArchiveTargetMDB != null)
					{
						this.targetDatabaseForMailboxArchive = this.specifiedArchiveTargetMDB.Id;
						mailboxDatabase2 = this.specifiedArchiveTargetMDB;
					}
					else if (this.ArchiveOnly)
					{
						mailboxDatabase2 = this.ChooseTargetMDB(this.adUser.ArchiveDatabase ?? this.adUser.Database);
						this.targetDatabaseForMailboxArchive = ADObjectIdResolutionHelper.ResolveDN(mailboxDatabase2.Id);
					}
					else
					{
						this.targetDatabaseForMailboxArchive = this.targetDatabaseForMailbox;
						mailboxDatabase2 = mailboxDatabase;
					}
					if (CommonUtils.ShouldHonorProvisioningSettings() && (this.MoveRequestIs(RequestFlags.IntraOrg) || this.MoveRequestIs(RequestFlags.Pull)) && !this.InternalFlagsContains(InternalMrsFlag.SkipProvisioningCheck) && mailboxDatabase2.IsExcludedFromProvisioning)
					{
						base.WriteError(new DatabaseExcludedFromProvisioningException(mailboxDatabase2.Name), ErrorCategory.InvalidArgument, this.Identity);
					}
				}
			}
		}

		// Token: 0x06007A54 RID: 31316 RVA: 0x001F3574 File Offset: 0x001F1774
		private void ComputeLocalPushPull()
		{
			this.moveFlags &= ~(RequestFlags.Push | RequestFlags.Pull);
			if (this.ForcePush || this.ForcePull)
			{
				this.moveFlags |= (this.ForcePush ? RequestFlags.Push : RequestFlags.Pull);
				return;
			}
			MailboxInformation mailboxInformation = this.ArchiveOnly ? this.sourceArchiveInfo : this.sourceMbxInfo;
			MailboxInformation mailboxInformation2 = this.ArchiveOnly ? this.targetArchiveInfo : this.targetMbxInfo;
			float num = mailboxInformation.MrsVersion;
			float num2 = mailboxInformation2.MrsVersion;
			if (num == 0f && mailboxInformation.ServerVersion < Server.E15MinVersion)
			{
				num = -1f;
			}
			if (num2 == 0f && mailboxInformation2.ServerVersion < Server.E15MinVersion)
			{
				num2 = -1f;
			}
			bool flag = num > num2;
			this.moveFlags |= (flag ? RequestFlags.Push : RequestFlags.Pull);
			if (this.PrimaryIsMoving && this.ArchiveIsMoving)
			{
				bool flag2 = this.sourceArchiveInfo.MrsVersion > this.targetArchiveInfo.MrsVersion;
				if (flag2 != flag)
				{
					base.WriteError(new CannotMovePrimaryAndArchiveToOpposingMrsVersions(mailboxInformation.MrsVersion.ToString(), mailboxInformation2.MrsVersion.ToString(), this.sourceArchiveInfo.MrsVersion.ToString(), this.targetArchiveInfo.MrsVersion.ToString()), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
		}

		// Token: 0x06007A55 RID: 31317 RVA: 0x001F36EC File Offset: 0x001F18EC
		private void VerifyMdbIsCurrentVersion(ADObjectId mdbQueue, out ADObjectId mdbqServerSite)
		{
			DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(mdbQueue.ObjectGuid, null, null, FindServerFlags.None);
			ServerVersion serverVersion = new ServerVersion(databaseInformation.ServerVersion);
			ServerVersion serverVersion2 = new ServerVersion(Server.E15MinVersion);
			if (serverVersion.Major < serverVersion2.Major)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorMailboxDatabaseNotOnCurrentCmdletVersion2(databaseInformation.DatabaseName)), ErrorCategory.InvalidArgument, this.Identity);
			}
			mdbqServerSite = databaseInformation.ServerSite;
		}

		// Token: 0x06007A56 RID: 31318 RVA: 0x001F3755 File Offset: 0x001F1955
		private bool MoveRequestIs(RequestFlags requiredFlags)
		{
			return (this.moveFlags & requiredFlags) == requiredFlags;
		}

		// Token: 0x06007A57 RID: 31319 RVA: 0x001F3774 File Offset: 0x001F1974
		private void RetrieveSourceMailboxesInfo()
		{
			TenantPartitionHint partitionHint = null;
			if (this.adUser.OrganizationId != null && (this.SourceIsLocal || TestIntegration.Instance.RemoteExchangeGuidOverride != Guid.Empty || this.DataObject.CrossResourceForest))
			{
				partitionHint = TenantPartitionHint.FromOrganizationId(this.adUser.OrganizationId);
			}
			if (this.PrimaryIsMoving)
			{
				MrsTracer.Cmdlet.Debug("Loading source mailbox info", new object[0]);
				this.sourceMbxInfo = this.mrsClient.GetMailboxInformation(this.DataObject, this.adUser.ExchangeGuid, this.adUser.ExchangeGuid, partitionHint, Guid.Empty, null, this.SourceIsRemote ? this.RemoteHostName : null, this.SourceIsRemote ? this.remoteOrgName : null, this.SourceIsRemote ? this.RemoteGlobalCatalog : null, this.SourceIsRemote ? this.remoteCred : null);
				MrsTracer.Cmdlet.Debug("Source mailbox info:\n{0}", new object[]
				{
					this.sourceMbxInfo.ToString()
				});
				if (this.SourceIsRemote)
				{
					this.remoteADUser = ConfigurableObjectXML.Deserialize<ADUser>(this.sourceMbxInfo.UserDataXML);
				}
			}
			if (this.ArchiveIsMoving)
			{
				if (!this.MailboxHasArchive)
				{
					base.WriteError(new ArchiveDisabledPermanentException(), ErrorCategory.InvalidArgument, this.Identity);
				}
				MrsTracer.Cmdlet.Debug("Loading source archive info", new object[0]);
				this.sourceArchiveInfo = this.mrsClient.GetMailboxInformation(this.DataObject, this.adUser.ExchangeGuid, this.adUser.ArchiveGuid, partitionHint, Guid.Empty, null, this.SourceIsRemote ? this.RemoteHostName : null, this.SourceIsRemote ? this.remoteOrgName : null, this.SourceIsRemote ? this.RemoteGlobalCatalog : null, this.SourceIsRemote ? this.remoteCred : null);
				MrsTracer.Cmdlet.Debug("Source archive info:\n{0}", new object[]
				{
					this.sourceArchiveInfo.ToString()
				});
				if (this.SourceIsRemote && !this.PrimaryIsMoving)
				{
					this.remoteADUser = ConfigurableObjectXML.Deserialize<ADUser>(this.sourceArchiveInfo.UserDataXML);
				}
			}
			if (this.PrimaryOnly)
			{
				RequestTaskHelper.ValidatePrimaryOnlyMoveArchiveDatabase(this.SourceUser, delegate(Exception exception, ErrorCategory category)
				{
					base.WriteError(exception, category, this.Identity);
				});
			}
		}

		// Token: 0x06007A58 RID: 31320 RVA: 0x001F39EC File Offset: 0x001F1BEC
		private void RetrieveTargetMailboxesInfo()
		{
			TenantPartitionHint partitionHint = (this.TargetIsLocal || TestIntegration.Instance.RemoteExchangeGuidOverride != Guid.Empty) ? TenantPartitionHint.FromOrganizationId(this.adUser.OrganizationId) : null;
			if (this.PrimaryIsMoving)
			{
				MrsTracer.Cmdlet.Debug("Loading target mailbox info", new object[0]);
				this.targetMbxInfo = this.mrsClient.GetMailboxInformation(this.DataObject, this.adUser.ExchangeGuid, this.adUser.ExchangeGuid, partitionHint, this.TargetIsRemote ? Guid.Empty : this.targetDatabaseForMailbox.ObjectGuid, this.TargetIsRemote ? this.RemoteTargetDatabase : null, this.TargetIsRemote ? this.RemoteHostName : null, this.TargetIsRemote ? this.remoteOrgName : null, this.TargetIsRemote ? this.RemoteGlobalCatalog : null, this.TargetIsRemote ? this.remoteCred : null);
				MrsTracer.Cmdlet.Debug("Target mailbox info:\n{0}", new object[]
				{
					this.targetMbxInfo.ToString()
				});
				if (this.TargetIsRemote)
				{
					this.remoteADUser = ConfigurableObjectXML.Deserialize<ADUser>(this.targetMbxInfo.UserDataXML);
				}
			}
			if (this.ArchiveIsMoving)
			{
				MrsTracer.Cmdlet.Debug("Loading target archive info", new object[0]);
				string text = this.RemoteArchiveTargetDatabase ?? this.RemoteTargetDatabase;
				if (this.TargetIsRemote && string.IsNullOrEmpty(text))
				{
					if (this.SourceHasPrimary)
					{
						base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorMissingDependentParameter("RemoteArchiveTargetDatabase", "Outbound")), ErrorCategory.InvalidArgument, this.Identity);
					}
					MrsTracer.Cmdlet.Debug("Loading target primary mailbox info", new object[0]);
					MailboxInformation mailboxInformation = this.mrsClient.GetMailboxInformation(this.DataObject, this.adUser.ExchangeGuid, this.adUser.ExchangeGuid, partitionHint, Guid.Empty, null, this.RemoteHostName, this.remoteOrgName, this.RemoteGlobalCatalog, this.remoteCred);
					text = mailboxInformation.MdbName;
					MrsTracer.Cmdlet.Debug("RemoteArchiveTargetDatabase was not specified. It was defaulted to the HomeMDB '{0}' of the target primary mailbox.", new object[]
					{
						mailboxInformation.MdbName
					});
				}
				this.targetArchiveInfo = this.mrsClient.GetMailboxInformation(this.DataObject, this.adUser.ExchangeGuid, this.adUser.ArchiveGuid, partitionHint, this.TargetIsRemote ? Guid.Empty : this.targetDatabaseForMailboxArchive.ObjectGuid, this.TargetIsRemote ? text : null, this.TargetIsRemote ? this.RemoteHostName : null, this.TargetIsRemote ? this.remoteOrgName : null, this.TargetIsRemote ? this.RemoteGlobalCatalog : null, this.TargetIsRemote ? this.remoteCred : null);
				MrsTracer.Cmdlet.Debug("Target archive info:\n{0}", new object[]
				{
					this.targetArchiveInfo.ToString()
				});
				if (this.TargetIsRemote && !this.PrimaryIsMoving)
				{
					this.remoteADUser = ConfigurableObjectXML.Deserialize<ADUser>(this.targetArchiveInfo.UserDataXML);
				}
			}
		}

		// Token: 0x06007A59 RID: 31321 RVA: 0x001F3D28 File Offset: 0x001F1F28
		private void RetrieveNonMovingTargetMailboxes()
		{
			TenantPartitionHint partitionHint = (this.TargetIsLocal || TestIntegration.Instance.RemoteExchangeGuidOverride != Guid.Empty) ? TenantPartitionHint.FromOrganizationId(this.adUser.OrganizationId) : null;
			if (this.targetMbxInfo == null && this.TargetHasPrimary)
			{
				MrsTracer.Cmdlet.Debug("Loading target mailbox info for non-moving primary", new object[0]);
				this.targetMbxInfo = this.mrsClient.GetMailboxInformation(this.DataObject, this.adUser.ExchangeGuid, this.adUser.ExchangeGuid, partitionHint, this.TargetUser.Database.ObjectGuid, this.TargetUser.Database.Name, this.TargetIsRemote ? this.RemoteHostName : null, this.TargetIsRemote ? this.remoteOrgName : null, this.TargetIsRemote ? this.RemoteGlobalCatalog : null, this.TargetIsRemote ? this.remoteCred : null);
				MrsTracer.Cmdlet.Debug("Target mailbox info for non-moving primary:\n{0}", new object[]
				{
					this.targetMbxInfo.ToString()
				});
			}
			if (this.targetArchiveInfo == null && this.TargetHasArchive)
			{
				this.targetArchiveInfo = this.mrsClient.GetMailboxInformation(this.DataObject, this.adUser.ExchangeGuid, this.adUser.ArchiveGuid, partitionHint, this.TargetUser.ArchiveDatabase.ObjectGuid, this.TargetUser.ArchiveDatabase.Name, this.TargetIsRemote ? this.RemoteHostName : null, this.TargetIsRemote ? this.remoteOrgName : null, this.TargetIsRemote ? this.RemoteGlobalCatalog : null, this.TargetIsRemote ? this.remoteCred : null);
				MrsTracer.Cmdlet.Debug("Target archive info for non-moving archive:\n{0}", new object[]
				{
					this.targetArchiveInfo.ToString()
				});
			}
		}

		// Token: 0x06007A5A RID: 31322 RVA: 0x001F3F2C File Offset: 0x001F212C
		private void ValidateMailboxes()
		{
			if (this.SourceUser.ArchiveGuid != this.TargetUser.ArchiveGuid && TestIntegration.Instance.RemoteArchiveGuidOverride == Guid.Empty)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorArchiveGuidsDontMatch(this.SourceUser.ToString(), this.TargetUser.ToString(), this.SourceUser.ArchiveGuid, this.TargetUser.ArchiveGuid)), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (!this.ArchiveOnly && !this.SourceHasPrimary)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorSourcePrimaryMailboxDoesNotExist(this.SourceUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.ArchiveOnly && !this.SourceHasArchive)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorSourceArchiveMailboxDoesNotExist(this.SourceUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.MoveRequestIs(RequestFlags.CrossOrg))
			{
				if (this.PrimaryIsMoving && this.TargetHasPrimary)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorTargetUserAlreadyHasPrimaryMailbox(this.TargetUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
				}
				if (this.ArchiveIsMoving && this.TargetHasArchive)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorTargetUserAlreadyHasArchiveMailbox(this.TargetUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
			if (this.PrimaryIsMoving)
			{
				this.EnsureSupportedServerVersion(this.sourceMbxInfo.ServerVersion, true);
				this.EnsureSupportedServerVersion(this.targetMbxInfo.ServerVersion, false);
			}
			if (this.ArchiveIsMoving)
			{
				this.EnsureSupportedServerVersionForArchiveScenarios(this.sourceArchiveInfo.ServerVersion, true);
				this.EnsureSupportedServerVersionForArchiveScenarios(this.targetArchiveInfo.ServerVersion, false);
			}
			ServerVersion serverVersion = new ServerVersion(Server.E15MinVersion);
			if (this.PrimaryIsMoving && this.sourceMbxInfo.ServerVersion < Server.E15MinVersion && this.targetMbxInfo.ServerVersion < Server.E15MinVersion)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorAtLeastOneSideMustBeCurrentProductVersion(serverVersion.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.ArchiveIsMoving && this.sourceArchiveInfo.ServerVersion < Server.E15MinVersion && this.targetArchiveInfo.ServerVersion < Server.E15MinVersion)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorAtLeastOneSideMustBeCurrentProductVersion(serverVersion.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			ServerVersion serverVersion2 = null;
			ServerVersion serverVersion3 = null;
			if (this.PrimaryIsMoving || this.TargetHasPrimary)
			{
				serverVersion2 = new ServerVersion(this.targetMbxInfo.ServerVersion);
			}
			if (this.ArchiveIsMoving || this.TargetHasArchive)
			{
				serverVersion3 = new ServerVersion(this.targetArchiveInfo.ServerVersion);
			}
			if (serverVersion2 != null && serverVersion3 != null && serverVersion2.Major != serverVersion3.Major)
			{
				base.WriteError(new PrimaryAndArchiveNotOnSameVersionPermanentException(serverVersion2.ToString(), serverVersion3.ToString()), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A5B RID: 31323 RVA: 0x001F4204 File Offset: 0x001F2404
		private void ValidateRecipients()
		{
			if (this.MoveRequestIs(RequestFlags.CrossOrg))
			{
				if (this.PrimaryIsMoving && this.targetMbxInfo.ServerVersion >= Server.E2007MinVersion && this.TargetUser.RecipientType != RecipientType.MailUser)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorPrimaryTargetIsNotAnMEU(this.TargetUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
				}
				if (this.ArchiveIsMoving && !this.TargetHasPrimary && this.targetArchiveInfo.ServerVersion >= Server.E14SP1MinVersion && this.TargetUser.RecipientType != RecipientType.MailUser)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorArchiveTargetIsNotAnMEU(this.TargetUser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
		}

		// Token: 0x06007A5C RID: 31324 RVA: 0x001F42BC File Offset: 0x001F24BC
		private void CheckHalfMailboxOnlyMovingFromOrToDatacenter()
		{
			if (this.MoveRequestIs(RequestFlags.CrossOrg) && (this.PrimaryOnly || this.ArchiveOnly) && !this.SourceUser.IsFromDatacenter && !this.TargetUser.IsFromDatacenter && !TestIntegration.Instance.AllowRemoteArchivesInEnt)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotMoveHalfMailboxBetweenTwoNonDatacenterForests), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A5D RID: 31325 RVA: 0x001F432C File Offset: 0x001F252C
		private void CheckPrimaryCannotBeInDatacenterWhenArchiveIsOnPremise()
		{
			if (this.MoveRequestIs(RequestFlags.CrossOrg) && this.SourceHasPrimary && this.SourceHasArchive)
			{
				if (!this.SourceUser.IsFromDatacenter && this.TargetUser.IsFromDatacenter && this.PrimaryIsMoving && !this.ArchiveIsMoving)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotHavePrimaryInDatacenterAndArchiveOnPremise), ErrorCategory.InvalidArgument, this.Identity);
				}
				if (this.SourceUser.IsFromDatacenter && !this.TargetUser.IsFromDatacenter && this.ArchiveIsMoving && !this.PrimaryIsMoving)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotHavePrimaryInDatacenterAndArchiveOnPremise), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
		}

		// Token: 0x06007A5E RID: 31326 RVA: 0x001F43E0 File Offset: 0x001F25E0
		private void CheckOnlineMoveSupported()
		{
			if (this.SuspendWhenReadyToComplete && ((this.PrimaryIsMoving && !this.SupportsOnline(this.sourceMbxInfo.ServerVersion, this.targetMbxInfo.ServerVersion)) || (this.ArchiveIsMoving && !this.SupportsOnline(this.sourceArchiveInfo.ServerVersion, this.targetArchiveInfo.ServerVersion))))
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorSuspendWRTCForOnlineOnly), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A5F RID: 31327 RVA: 0x001F4460 File Offset: 0x001F2660
		private void CheckVersionsForArchiveSeparation()
		{
			if (!this.IsSplitPrimaryAndArchiveScenario)
			{
				return;
			}
			int e14SP1MinVersion = Server.E14SP1MinVersion;
			string serverName;
			string serverVersion;
			if (this.mrsClient.ServerVersion[2])
			{
				if (this.PrimaryIsMoving)
				{
					if (this.sourceMbxInfo.ServerVersion < e14SP1MinVersion)
					{
						MrsTracer.Cmdlet.Warning("Source primary MDB is not E14R5 or above", new object[0]);
						serverName = this.sourceMbxInfo.ServerInformation.MailboxServerName;
						serverVersion = new ServerVersion(this.sourceMbxInfo.ServerInformation.MailboxServerVersion).ToString();
						goto IL_39B;
					}
					if (this.targetMbxInfo.ServerVersion < e14SP1MinVersion)
					{
						MrsTracer.Cmdlet.Warning("Target primary MDB is not E14R5 or above", new object[0]);
						serverName = this.targetMbxInfo.ServerInformation.MailboxServerName;
						serverVersion = new ServerVersion(this.targetMbxInfo.ServerInformation.MailboxServerVersion).ToString();
						goto IL_39B;
					}
					if (this.SourceIsRemote && !this.MoveRequestIs(RequestFlags.RemoteLegacy) && !this.sourceMbxInfo.ServerInformation.ProxyServerVersion[9])
					{
						MrsTracer.Cmdlet.Warning("Source MRSProxy does not support archive separation", new object[0]);
						serverName = this.sourceMbxInfo.ServerInformation.ProxyServerVersion.ComputerName;
						serverVersion = this.sourceMbxInfo.ServerInformation.ProxyServerVersion.ComputerName.ToString();
						goto IL_39B;
					}
					if (this.TargetIsRemote && !this.MoveRequestIs(RequestFlags.RemoteLegacy) && !this.targetMbxInfo.ServerInformation.ProxyServerVersion[9])
					{
						MrsTracer.Cmdlet.Warning("Target MRSProxy does not support archive separation", new object[0]);
						serverName = this.targetMbxInfo.ServerInformation.ProxyServerVersion.ComputerName;
						serverVersion = this.targetMbxInfo.ServerInformation.ProxyServerVersion.ComputerName.ToString();
						goto IL_39B;
					}
				}
				if (this.ArchiveIsMoving)
				{
					if (this.sourceArchiveInfo.ServerVersion < e14SP1MinVersion)
					{
						MrsTracer.Cmdlet.Warning("Source archive MDB is not E14R5 or above", new object[0]);
						serverName = this.sourceArchiveInfo.ServerInformation.MailboxServerName;
						serverVersion = new ServerVersion(this.sourceArchiveInfo.ServerInformation.MailboxServerVersion).ToString();
						goto IL_39B;
					}
					if (this.targetArchiveInfo.ServerVersion < e14SP1MinVersion)
					{
						MrsTracer.Cmdlet.Warning("Target archive MDB is not E14R5 or above", new object[0]);
						serverName = this.targetArchiveInfo.ServerInformation.MailboxServerName;
						serverVersion = new ServerVersion(this.targetArchiveInfo.ServerInformation.MailboxServerVersion).ToString();
						goto IL_39B;
					}
					if (this.SourceIsRemote && !this.MoveRequestIs(RequestFlags.RemoteLegacy) && !this.sourceArchiveInfo.ServerInformation.ProxyServerVersion[9])
					{
						MrsTracer.Cmdlet.Warning("Source MRSProxy does not support archive separation", new object[0]);
						serverName = this.sourceArchiveInfo.ServerInformation.ProxyServerVersion.ComputerName;
						serverVersion = this.sourceArchiveInfo.ServerInformation.ProxyServerVersion.ComputerName.ToString();
						goto IL_39B;
					}
					if (this.TargetIsRemote && !this.MoveRequestIs(RequestFlags.RemoteLegacy) && !this.targetArchiveInfo.ServerInformation.ProxyServerVersion[9])
					{
						MrsTracer.Cmdlet.Warning("Target MRSProxy does not support archive separation", new object[0]);
						serverName = this.targetArchiveInfo.ServerInformation.ProxyServerVersion.ComputerName;
						serverVersion = this.targetArchiveInfo.ServerInformation.ProxyServerVersion.ComputerName.ToString();
						goto IL_39B;
					}
				}
				return;
			}
			MrsTracer.Cmdlet.Warning("MRS does not support archive separation", new object[0]);
			serverName = this.mrsClient.ServerVersion.ComputerName;
			serverVersion = this.mrsClient.ServerVersion.ToString();
			IL_39B:
			base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotSeparatePrimaryFromArchiveBecauseServerIsDownlevel(serverName, serverVersion)), ErrorCategory.InvalidArgument, this.Identity);
		}

		// Token: 0x06007A60 RID: 31328 RVA: 0x001F4824 File Offset: 0x001F2A24
		private void DisallowUsersOnLitigationHoldToPreE14()
		{
			if (this.PrimaryIsMoving && this.SourceUser.LitigationHoldEnabled && this.targetMbxInfo.ServerVersion < Server.E14MinVersion)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotMoveLitigationHoldToPreE14(this.adUser.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A61 RID: 31329 RVA: 0x001F487C File Offset: 0x001F2A7C
		private void CheckDifferentSourceAndTargets()
		{
			if (this.PrimaryIsMoving && this.sourceMbxInfo.MdbGuid == this.targetMbxInfo.MdbGuid)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorUserAlreadyInTargetDatabase(this.sourceMbxInfo.MailboxIdentity, this.sourceMbxInfo.MdbName)), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.ArchiveIsMoving && this.sourceArchiveInfo.MdbGuid == this.targetArchiveInfo.MdbGuid)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorArchiveAlreadyInTargetArchiveDatabase(this.sourceArchiveInfo.MailboxIdentity, this.sourceArchiveInfo.MdbName)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A62 RID: 31330 RVA: 0x001F4930 File Offset: 0x001F2B30
		private void DisallowRemoteLegacyWithE15Plus()
		{
			if (TestIntegration.Instance.AllowRemoteLegacyMovesWithE15)
			{
				return;
			}
			int num = this.PrimaryIsMoving ? this.sourceMbxInfo.ServerVersion : this.sourceArchiveInfo.ServerVersion;
			int num2 = this.PrimaryIsMoving ? this.targetMbxInfo.ServerVersion : this.targetArchiveInfo.ServerVersion;
			bool flag = num >= Server.E14MinVersion && num < Server.E15MinVersion;
			bool flag2 = num2 >= Server.E14MinVersion && num2 < Server.E15MinVersion;
			bool flag3 = num >= Server.E14MinVersion;
			bool flag4 = num2 >= Server.E14MinVersion;
			if (this.MoveRequestIs(RequestFlags.RemoteLegacy) && flag3 && flag4 && (!flag || !flag2))
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorRemoteLegacyWithE15NotAllowed), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A63 RID: 31331 RVA: 0x001F4A00 File Offset: 0x001F2C00
		private void DisallowArbitrationMailboxesToPreE14()
		{
			if (this.PrimaryIsMoving && this.sourceMbxInfo.RecipientDisplayType == 10 && this.targetMbxInfo.ServerVersion < Server.E14MinVersion)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotMoveArbitrationMailboxesDownlevel(this.adUser.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A64 RID: 31332 RVA: 0x001F4A58 File Offset: 0x001F2C58
		private void DisallowArbitrationMailboxesCrossForest()
		{
			if (this.MoveRequestIs(RequestFlags.CrossOrg) && this.PrimaryIsMoving && this.sourceMbxInfo.RecipientDisplayType == 10)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotMoveArbitrationMailboxesCrossForest(this.adUser.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A65 RID: 31333 RVA: 0x001F4AA8 File Offset: 0x001F2CA8
		private void DisallowDiscoveryMailboxesToPreE14()
		{
			if (this.PrimaryIsMoving && this.sourceMbxInfo.RecipientTypeDetailsLong == 536870912L && this.targetMbxInfo.ServerVersion < Server.E14MinVersion)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotMoveDiscoveryMailboxesDownlevel(this.adUser.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A66 RID: 31334 RVA: 0x001F4B04 File Offset: 0x001F2D04
		private void DisallowMailboxMovesWithInPlaceHoldToPreE15()
		{
			if (this.PrimaryIsMoving && this.targetMbxInfo.ServerVersion < Server.E15MinVersion && this.sourceMbxInfo.ServerVersion >= Server.E15MinVersion && this.SourceUser.InPlaceHolds != null && this.SourceUser.InPlaceHolds.Count > 0)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotMoveMailboxesWithInPlaceHoldDownlevel(this.adUser.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A67 RID: 31335 RVA: 0x001F4B80 File Offset: 0x001F2D80
		private void DisallowPublicFolderMailboxesToPreE15()
		{
			if (this.PrimaryIsMoving && this.sourceMbxInfo.RecipientTypeDetailsLong == 68719476736L && this.targetMbxInfo.ServerVersion < Server.E15MinVersion)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotMovePublicFolderMailboxesDownlevel(this.adUser.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A68 RID: 31336 RVA: 0x001F4BE0 File Offset: 0x001F2DE0
		private void DisallowPublicFolderMailboxesCrossForestOrCrossPremise()
		{
			if (this.MoveRequestIs(RequestFlags.CrossOrg) && this.PrimaryIsMoving && this.sourceMbxInfo.RecipientTypeDetailsLong == 68719476736L)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotMovePublicFolderMailboxesCrossForestOrCrossPremise(this.adUser.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A69 RID: 31337 RVA: 0x001F4C38 File Offset: 0x001F2E38
		private void DisallowPublicFolderMailboxesMoveDuringFinalization()
		{
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Mrs.PublicFolderMailboxesMigration.Enabled && this.PrimaryIsMoving && this.sourceMbxInfo.RecipientTypeDetailsLong == 68719476736L && CommonUtils.IsPublicFolderMailboxesLockedForNewConnectionsFlagSet(base.CurrentOrganizationId))
			{
				base.WriteError(new RecipientTaskException(new LocalizedString(ServerStrings.PublicFolderMailboxesCannotBeMovedDuringMigration)), ErrorCategory.InvalidOperation, this.Identity);
			}
		}

		// Token: 0x06007A6A RID: 31338 RVA: 0x001F4CB4 File Offset: 0x001F2EB4
		private void DisallowTeamMailboxesToPreE15()
		{
			if (this.PrimaryIsMoving && this.sourceMbxInfo.RecipientTypeDetailsLong == 137438953472L && this.targetMbxInfo.ServerVersion < Server.E15MinVersion)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotMoveTeamMailboxesDownlevel(this.adUser.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A6B RID: 31339 RVA: 0x001F4D13 File Offset: 0x001F2F13
		private void CheckTeamMailboxesCrossForestOrCrossPremise()
		{
			if (this.MoveRequestIs(RequestFlags.CrossOrg) && this.PrimaryIsMoving && this.sourceMbxInfo.RecipientTypeDetailsLong == 137438953472L)
			{
				this.WriteWarning(Microsoft.Exchange.Management.Tasks.Strings.WarningMovingTeamMailboxesCrossForestOrCrossPremise(this.adUser.Name));
			}
		}

		// Token: 0x06007A6C RID: 31340 RVA: 0x001F4D54 File Offset: 0x001F2F54
		private void DisallowDiscoveryMailboxesCrossForest()
		{
			if (this.MoveRequestIs(RequestFlags.CrossOrg) && this.PrimaryIsMoving && this.sourceMbxInfo.RecipientTypeDetailsLong == 536870912L)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotMoveDiscoveryMailboxesCrossForest(this.adUser.Name)), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A6D RID: 31341 RVA: 0x001F4DA8 File Offset: 0x001F2FA8
		private void DisallowUCSMigratedMailboxesToPreE15()
		{
			if (this.PrimaryIsMoving && this.targetMbxInfo.ServerVersion < Server.E15MinVersion && (bool)this.SourceUser[ADRecipientSchema.UCSImListMigrationCompleted])
			{
				base.WriteError(new UnableToMoveUCSMigratedMailboxToDownlevelException(this.adUser.Name), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A6E RID: 31342 RVA: 0x001F4E04 File Offset: 0x001F3004
		private void DisallowMailboxMoveWithSubscriptions()
		{
			if (this.PrimaryIsMoving && this.CheckOnboardingOffboarding() && this.TargetIsRemote && this.sourceMbxInfo.ContentAggregationFlags == 1)
			{
				base.WriteError(new UnableToMoveMailboxWithSubscriptionsException(this.adUser.Name), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A6F RID: 31343 RVA: 0x001F4E54 File Offset: 0x001F3054
		private void EnsureSupportedServerVersion(int serverVersion, bool isSourceDatabase)
		{
			bool flag = serverVersion >= Server.E15MinVersion;
			bool flag2 = serverVersion >= Server.E14MinVersion && serverVersion < Server.E15MinVersion;
			bool flag3 = serverVersion >= Server.E2007SP2MinVersion && serverVersion < Server.E14MinVersion;
			bool flag4 = serverVersion >= Server.E2k3SP2MinVersion && serverVersion < Server.E2007MinVersion;
			bool flag5;
			if (isSourceDatabase)
			{
				flag5 = (flag || flag2 || flag3 || (MapiTaskHelper.IsDatacenter && flag4));
			}
			else
			{
				flag5 = (flag || flag2 || flag3);
			}
			if (!flag5)
			{
				if (isSourceDatabase)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorMovingOldExchangeUsersUnsupported), ErrorCategory.InvalidArgument, this.Identity);
					return;
				}
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorMovingToOldExchangeDatabaseUnsupported), ErrorCategory.InvalidArgument, this.TargetDatabase);
			}
		}

		// Token: 0x06007A70 RID: 31344 RVA: 0x001F4F0C File Offset: 0x001F310C
		private void EnsureSupportedServerVersionForArchiveScenarios(int serverVersion, bool isSourceDatabase)
		{
			if (serverVersion < Server.E14MinVersion)
			{
				if (isSourceDatabase)
				{
					base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorMovingUnsupportedArchiveUser), ErrorCategory.InvalidArgument, this.Identity);
					return;
				}
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotMoveArchiveMailboxesDownlevel(this.adUser.Name)), ErrorCategory.InvalidArgument, this.TargetDatabase);
			}
		}

		// Token: 0x06007A71 RID: 31345 RVA: 0x001F4F68 File Offset: 0x001F3168
		private bool SupportsOnline(int sourceVersion, int targetVersion)
		{
			int e15MinVersion = Server.E15MinVersion;
			if (sourceVersion >= Server.E14MinVersion)
			{
				int e15MinVersion2 = Server.E15MinVersion;
			}
			if (sourceVersion >= Server.E2007MinVersion)
			{
				int e14MinVersion = Server.E14MinVersion;
			}
			bool flag = sourceVersion >= Server.E2k3MinVersion && sourceVersion < Server.E2007MinVersion;
			int e15MinVersion3 = Server.E15MinVersion;
			if (targetVersion >= Server.E14MinVersion)
			{
				int e15MinVersion4 = Server.E15MinVersion;
			}
			bool flag2 = targetVersion >= Server.E2007MinVersion && targetVersion < Server.E14MinVersion;
			bool flag3 = targetVersion >= Server.E2k3MinVersion && targetVersion < Server.E2007MinVersion;
			bool flag4 = flag2 || flag || flag3;
			return !flag4;
		}

		// Token: 0x06007A72 RID: 31346 RVA: 0x001F4FF8 File Offset: 0x001F31F8
		private MailboxDatabase LocateAndVerifyMdb(DatabaseIdParameter databaseId, bool isSourceDatabase, out int serverVersion)
		{
			ITopologyConfigurationSession configSessionForDatabase = RequestTaskHelper.GetConfigSessionForDatabase(this.globalConfigSession, databaseId.InternalADObjectId);
			MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(databaseId, configSessionForDatabase, null, new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorMailboxDatabaseNotFound(databaseId.ToString())), new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorMailboxDatabaseNotUnique(databaseId.ToString())));
			serverVersion = MapiUtils.FindServerForMdb(mailboxDatabase.Id, null, null, FindServerFlags.None).ServerVersion;
			this.EnsureSupportedServerVersion(serverVersion, isSourceDatabase);
			if (!isSourceDatabase && mailboxDatabase.Recovery)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorTargetDatabaseIsRecovery(mailboxDatabase.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			return mailboxDatabase;
		}

		// Token: 0x06007A73 RID: 31347 RVA: 0x001F5090 File Offset: 0x001F3290
		private void EnsureDatabaseAttributesMatchUser(MailboxDatabase targetMdb)
		{
			if (this.adUser == null || this.adUser.MailboxProvisioningConstraint == null)
			{
				return;
			}
			if (targetMdb.MailboxProvisioningAttributes == null || !this.adUser.MailboxProvisioningConstraint.IsMatch(targetMdb.MailboxProvisioningAttributes))
			{
				base.WriteError(new MailboxConstraintsMismatchException(this.adUser.ToString(), targetMdb.Name, this.adUser.MailboxProvisioningConstraint.Value), ErrorCategory.InvalidData, this.Identity);
			}
		}

		// Token: 0x06007A74 RID: 31348 RVA: 0x001F5108 File Offset: 0x001F3308
		private MailboxDatabase ChooseTargetMDB(ADObjectId sourceMDB)
		{
			bool checkInitialProvisioningSetting = this.CheckInitialProvisioningSetting;
			if (!this.IsFieldSet("CheckInitialProvisioningSetting") && VariantConfiguration.InvariantNoFlightingSnapshot.Mrs.UseDefaultValueForCheckInitialProvisioningForMovesParameter.Enabled)
			{
				checkInitialProvisioningSetting = TestIntegration.Instance.CheckInitialProvisioningForMoves;
			}
			return RequestTaskHelper.ChooseTargetMDB(new ADObjectId[]
			{
				sourceMDB
			}, checkInitialProvisioningSetting, this.adUser, this.DomainController, base.ScopeSet, new Action<LocalizedString>(base.WriteVerbose), new Action<LocalizedException, ExchangeErrorCategory, object>(base.WriteError), new Action<Exception, ErrorCategory, object>(base.WriteError), this.Identity);
		}

		// Token: 0x06007A75 RID: 31349 RVA: 0x001F51A0 File Offset: 0x001F33A0
		private ADObjectId GetMdbQueue()
		{
			ADObjectId adobjectId;
			if (this.MoveRequestIs(RequestFlags.Pull))
			{
				if (this.ArchiveOnly)
				{
					adobjectId = this.targetDatabaseForMailboxArchive;
				}
				else
				{
					adobjectId = this.targetDatabaseForMailbox;
				}
			}
			else if (this.ArchiveOnly)
			{
				adobjectId = this.adUser.ArchiveDatabase;
			}
			else
			{
				adobjectId = this.adUser.Database;
			}
			if (adobjectId == null)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorUnableToDetermineMdbQueue(this.Identity.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			return adobjectId;
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x001F5224 File Offset: 0x001F3424
		private void CheckForDuplicateMR(bool verifyOnly, TransactionalRequestJob newMR)
		{
			RequestJobQueryFilter filter = new RequestJobQueryFilter(this.adUser.ExchangeGuid, this.mrProvider.MdbGuid, MRSRequestType.Move);
			IEnumerable<MoveRequestStatistics> enumerable = this.mrProvider.FindPaged<MoveRequestStatistics>(filter, null, true, null, 100);
			RequestJobObjectId requestJobObjectId = null;
			bool flag = false;
			using (IEnumerator<MoveRequestStatistics> enumerator = enumerable.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					for (;;)
					{
						MoveRequestStatistics moveRequestStatistics = enumerator.Current;
						if (!moveRequestStatistics.CancelRequest)
						{
							goto IL_CC;
						}
						if (!verifyOnly)
						{
							using (TransactionalRequestJob transactionalRequestJob = (TransactionalRequestJob)this.mrProvider.Read<TransactionalRequestJob>(moveRequestStatistics.Identity))
							{
								if (transactionalRequestJob != null && !transactionalRequestJob.IsFake)
								{
									MrsTracer.Cmdlet.Warning("Removing existing canceled move request.", new object[0]);
									this.mrProvider.Delete(transactionalRequestJob);
								}
								goto IL_11F;
							}
							goto IL_CC;
						}
						MrsTracer.Cmdlet.Warning("Canceled MoveRequest will be overwritten.", new object[0]);
						IL_11F:
						if (!enumerator.MoveNext())
						{
							break;
						}
						continue;
						IL_CC:
						if (newMR != null && moveRequestStatistics.RequestGuid == newMR.RequestGuid)
						{
							requestJobObjectId = new RequestJobObjectId(moveRequestStatistics.RequestGuid, moveRequestStatistics.RequestQueue.ObjectGuid, null);
							if (flag)
							{
								break;
							}
							goto IL_11F;
						}
						else
						{
							if (verifyOnly)
							{
								goto IL_11F;
							}
							MrsTracer.Cmdlet.Warning("Someone managed to create another MoveRequest.  The MoveRequest we just created will be removed.", new object[0]);
							flag = true;
							if (requestJobObjectId == null)
							{
								goto IL_11F;
							}
							break;
						}
					}
					if (flag && requestJobObjectId != null)
					{
						using (TransactionalRequestJob transactionalRequestJob2 = (TransactionalRequestJob)this.mrProvider.Read<TransactionalRequestJob>(requestJobObjectId))
						{
							if (transactionalRequestJob2 != null && !transactionalRequestJob2.IsFake)
							{
								MrsTracer.Cmdlet.Warning("Removing our newly created move request because someone managed to create another one.", new object[0]);
								this.mrProvider.Delete(transactionalRequestJob2);
							}
						}
						base.WriteError(new ManagementObjectAlreadyExistsException(Microsoft.Exchange.Management.Tasks.Strings.ErrorMoveRequestAlreadyExistsInMDBQueue(this.GetMdbQueue().ToString(), this.adUser.ToString(), this.adUser.ExchangeGuid)), ErrorCategory.InvalidArgument, this.Identity);
					}
				}
			}
		}

		// Token: 0x06007A77 RID: 31351 RVA: 0x001F5488 File Offset: 0x001F3688
		private void GetMailboxQuotas(MailboxInformation mbxInfo, bool isArchive, bool isTarget, out ulong mbxQuota, out ulong dumpsterQuota)
		{
			if (isTarget && this.SourceIsRemote && this.PrimaryIsMoving && ConfigBase<MRSConfigSchema>.GetConfig<bool>("CheckMailUserPlanQuotasForOnboarding"))
			{
				MailboxInformation mailboxInformation = new MailboxInformation();
				mailboxInformation.MdbQuota = mbxInfo.MdbQuota;
				ADUser aduser = (ADUser)this.TargetUser.Clone();
				ADUser aduser2 = (ADUser)this.SourceUser.Clone();
				MrsTracer.Cmdlet.Debug("Getting target mailbox quotas from in-memory application of mailbox plan to AD user...", new object[0]);
				if (base.IsProvisioningLayerAvailable)
				{
					aduser.RecipientTypeDetails = RecipientTypeDetails.UserMailbox;
					ProvisioningLayer.UpdateAffectedIConfigurable(this, this.ConvertDataObjectToPresentationObject(aduser), false);
				}
				MailboxTaskHelper.ApplyMbxPlanSettingsInTargetForest(aduser, (ADObjectId mbxPlanId) => (ADUser)base.GetDataObject<ADUser>(new MailboxPlanIdParameter(mbxPlanId), base.TenantGlobalCatalogSession, null, new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorMailboxPlanNotFound(mbxPlanId.ToString())), new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorMailboxPlanNotUnique(mbxPlanId.ToString()))), ApplyMailboxPlanFlags.PreservePreviousExplicitlySetValues);
				mailboxInformation.MailboxQuota = (aduser.ProhibitSendReceiveQuota.IsUnlimited ? null : new ulong?(aduser.ProhibitSendReceiveQuota.Value.ToBytes()));
				mailboxInformation.MailboxArchiveQuota = (aduser.ArchiveQuota.IsUnlimited ? null : new ulong?(aduser.ArchiveQuota.Value.ToBytes()));
				mailboxInformation.MailboxDumpsterQuota = (aduser.RecoverableItemsQuota.IsUnlimited ? null : new ulong?(aduser.RecoverableItemsQuota.Value.ToBytes()));
				base.WriteVerbose(Microsoft.Exchange.Management.Tasks.Strings.MailboxQuotaValues("TargetUser-original", this.TargetUser.ProhibitSendReceiveQuota.IsUnlimited ? new ByteQuantifiedSize(ulong.MaxValue).ToString() : this.TargetUser.ProhibitSendReceiveQuota.Value.ToString(), this.TargetUser.ArchiveQuota.IsUnlimited ? new ByteQuantifiedSize(ulong.MaxValue).ToString() : this.TargetUser.ArchiveQuota.Value.ToString(), this.TargetUser.RecoverableItemsQuota.IsUnlimited ? new ByteQuantifiedSize(ulong.MaxValue).ToString() : this.TargetUser.RecoverableItemsQuota.Value.ToString()));
				mbxInfo = mailboxInformation;
			}
			ulong? num;
			ulong? num2;
			if (isArchive)
			{
				num = mbxInfo.MailboxArchiveQuota;
				num2 = mbxInfo.MailboxDumpsterQuota;
			}
			else if (mbxInfo.UseMdbQuotaDefaults != null && mbxInfo.UseMdbQuotaDefaults == true)
			{
				num = mbxInfo.MdbQuota;
				num2 = mbxInfo.MdbDumpsterQuota;
			}
			else
			{
				num = mbxInfo.MailboxQuota;
				num2 = mbxInfo.MailboxDumpsterQuota;
			}
			base.WriteVerbose(Microsoft.Exchange.Management.Tasks.Strings.MailboxQuotaValues(isTarget ? "Effective-Target" : "Effective-Source", (num == null) ? new ByteQuantifiedSize(ulong.MaxValue).ToString() : new ByteQuantifiedSize(num.Value).ToString(), "N.A.", (num2 == null) ? new ByteQuantifiedSize(ulong.MaxValue).ToString() : new ByteQuantifiedSize(num2.Value).ToString()));
			mbxQuota = ((num != null) ? num.Value : ulong.MaxValue);
			dumpsterQuota = ((num2 != null) ? num2.Value : ulong.MaxValue);
		}

		// Token: 0x06007A78 RID: 31352 RVA: 0x001F5830 File Offset: 0x001F3A30
		private void VerifyMailboxQuota(MailboxInformation sourceInfo, MailboxInformation targetInfo, bool isArchive)
		{
			ulong num;
			ulong num2;
			this.GetMailboxQuotas(targetInfo, isArchive, true, out num, out num2);
			ulong num3;
			ulong num4;
			this.GetMailboxQuotas(sourceInfo, isArchive, false, out num3, out num4);
			ulong num5 = (sourceInfo.MailboxSize > sourceInfo.RegularDeletedItemsSize) ? (sourceInfo.MailboxSize - sourceInfo.RegularDeletedItemsSize) : 0UL;
			if (num5 > num)
			{
				ByteQuantifiedSize byteQuantifiedSize = new ByteQuantifiedSize(num);
				ByteQuantifiedSize byteQuantifiedSize2 = new ByteQuantifiedSize(num5);
				LocalizedString message = isArchive ? Microsoft.Exchange.Management.Tasks.Strings.ErrorArchiveExceedsTargetQuota(byteQuantifiedSize2.ToString(), byteQuantifiedSize.ToString()) : Microsoft.Exchange.Management.Tasks.Strings.ErrorMailboxExceedsTargetQuota(byteQuantifiedSize2.ToString(), byteQuantifiedSize.ToString());
				base.WriteError(new RecipientTaskException(message), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (sourceInfo.ServerVersion >= Server.E14MinVersion && num4 > num2)
			{
				ulong regularDeletedItemsSize = sourceInfo.RegularDeletedItemsSize;
				if (regularDeletedItemsSize > num2)
				{
					ByteQuantifiedSize byteQuantifiedSize3 = new ByteQuantifiedSize(num2);
					ByteQuantifiedSize byteQuantifiedSize4 = new ByteQuantifiedSize(regularDeletedItemsSize);
					LocalizedString message2 = isArchive ? Microsoft.Exchange.Management.Tasks.Strings.ErrorArchiveDumpsterExceedsTargetQuota(byteQuantifiedSize4.ToString(), byteQuantifiedSize3.ToString()) : Microsoft.Exchange.Management.Tasks.Strings.ErrorMailboxDumpsterExceedsTargetQuota(byteQuantifiedSize4.ToString(), byteQuantifiedSize3.ToString());
					base.WriteError(new RecipientTaskException(message2), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
		}

		// Token: 0x06007A79 RID: 31353 RVA: 0x001F5970 File Offset: 0x001F3B70
		private void CheckMailboxRelease()
		{
			MailboxRelease requiredMailboxRelease = MailboxTaskHelper.ComputeRequiredMailboxRelease(this.ConfigurationSession, this.adUser, (ExchangeConfigurationUnit)this.orgConfig, new Task.ErrorLoggerDelegate(base.WriteError));
			if (this.PrimaryIsMoving)
			{
				MailboxRelease targetServerMailboxRelease = MailboxRelease.None;
				Enum.TryParse<MailboxRelease>(this.targetMbxInfo.ServerMailboxRelease, true, out targetServerMailboxRelease);
				MailboxTaskHelper.ValidateMailboxRelease(targetServerMailboxRelease, requiredMailboxRelease, this.adUser.Id.ToString(), this.targetMbxInfo.MdbName, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (this.ArchiveIsMoving)
			{
				MailboxRelease targetServerMailboxRelease2 = MailboxRelease.None;
				Enum.TryParse<MailboxRelease>(this.targetArchiveInfo.ServerMailboxRelease, true, out targetServerMailboxRelease2);
				MailboxTaskHelper.ValidateMailboxRelease(targetServerMailboxRelease2, requiredMailboxRelease, this.adUser.Id.ToString(), this.targetArchiveInfo.MdbName, new Task.ErrorLoggerDelegate(base.WriteError));
			}
		}

		// Token: 0x06007A7A RID: 31354 RVA: 0x001F5A40 File Offset: 0x001F3C40
		private void ValidateUMSettings()
		{
			IRecipientSession tenantLocalRecipientSession = RecipientTaskHelper.GetTenantLocalRecipientSession(this.TargetUser.OrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId);
			try
			{
				MigrationHelper.ValidateTargetUserCanBeEnabledForUM(tenantLocalRecipientSession, this.tenantLocalConfigSession, Datacenter.IsMicrosoftHostedOnly(true), this.SourceUser, this.TargetUser);
			}
			catch (LocalizedException ex)
			{
				base.WriteError(new RecipientTaskException(Microsoft.Exchange.Management.Tasks.Strings.ErrorCannotUMEnableInTarget(this.SourceUser.ToString(), ex.LocalizedString), ex), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A7B RID: 31355 RVA: 0x001F5ACC File Offset: 0x001F3CCC
		private bool CheckOnboardingOffboarding()
		{
			return Datacenter.IsMicrosoftHostedOnly(true) && this.adUser != null && this.adUser.IsFromDatacenter && this.adUser.OrganizationId != null && !this.adUser.OrganizationId.Equals(OrganizationId.ForestWideOrgId) && (this.SourceIsRemote || this.TargetIsRemote);
		}

		// Token: 0x06007A7C RID: 31356 RVA: 0x001F5B34 File Offset: 0x001F3D34
		private Organization GetOrganization()
		{
			if (this.orgConfig == null)
			{
				if (!this.CheckOnboardingOffboarding())
				{
					return null;
				}
				ADObjectId organizationalUnit = this.adUser.OrganizationId.OrganizationalUnit;
				ADObjectId configurationUnit = this.adUser.OrganizationId.ConfigurationUnit;
				this.orgConfig = this.tenantLocalConfigSession.Read<ExchangeConfigurationUnit>(configurationUnit);
			}
			return this.orgConfig;
		}

		// Token: 0x06007A7D RID: 31357 RVA: 0x001F5B90 File Offset: 0x001F3D90
		private RequestPriority ComputePriority()
		{
			RequestPriority result = RequestPriority.Normal;
			if (base.Fields.IsModified("Priority"))
			{
				result = this.Priority;
			}
			else
			{
				Organization organization = this.GetOrganization();
				if (organization != null && organization.DefaultMovePriority > 0)
				{
					result = (RequestPriority)organization.DefaultMovePriority;
				}
			}
			return result;
		}

		// Token: 0x06007A7E RID: 31358 RVA: 0x001F5BD8 File Offset: 0x001F3DD8
		private void EnsurePrimaryOnlyIsPresentWhenOnboardingWithCloudArchive()
		{
			if (this.TargetHasArchive && !this.PrimaryOnly && this.SourceIsRemote)
			{
				LocalizedString message = MrsStrings.CompositeDataContext(Microsoft.Exchange.Management.Tasks.Strings.ErrorParameterValueRequired("PrimaryOnly"), Microsoft.Exchange.Management.Tasks.Strings.ErrorTargetUserAlreadyHasArchiveMailbox(this.adUser.ToString()));
				base.WriteError(new RecipientTaskException(message), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007A7F RID: 31359 RVA: 0x001F5C35 File Offset: 0x001F3E35
		private bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x06007A80 RID: 31360 RVA: 0x001F5C54 File Offset: 0x001F3E54
		private bool InternalFlagsContains(InternalMrsFlag flag)
		{
			if (this.InternalFlags == null)
			{
				return false;
			}
			foreach (InternalMrsFlag internalMrsFlag in this.InternalFlags)
			{
				if (internalMrsFlag == flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003C8E RID: 15502
		public const string ParameterSetRemote = "MigrationRemote";

		// Token: 0x04003C8F RID: 15503
		public const string ParameterSetOutbound = "MigrationOutbound";

		// Token: 0x04003C90 RID: 15504
		public const string ParameterSetRemoteLegacy = "MigrationRemoteLegacy";

		// Token: 0x04003C91 RID: 15505
		public const string ParameterSetLocal = "MigrationLocal";

		// Token: 0x04003C92 RID: 15506
		private static ADPropertyDefinition[] simpleSearchResult = new ADPropertyDefinition[]
		{
			ADObjectSchema.Guid
		};

		// Token: 0x04003C93 RID: 15507
		private static TimeSpan defaultIncrementalSyncIntervalForMove = TimeSpan.FromDays(1.0);

		// Token: 0x04003C94 RID: 15508
		private MailboxDatabase specifiedTargetMDB;

		// Token: 0x04003C95 RID: 15509
		private MailboxDatabase specifiedArchiveTargetMDB;

		// Token: 0x04003C96 RID: 15510
		private ADUser adUser;

		// Token: 0x04003C97 RID: 15511
		private ADUser remoteADUser;

		// Token: 0x04003C98 RID: 15512
		private MailboxInformation sourceMbxInfo;

		// Token: 0x04003C99 RID: 15513
		private MailboxInformation sourceArchiveInfo;

		// Token: 0x04003C9A RID: 15514
		private MailboxInformation targetMbxInfo;

		// Token: 0x04003C9B RID: 15515
		private MailboxInformation targetArchiveInfo;

		// Token: 0x04003C9C RID: 15516
		private string remoteOrgName;

		// Token: 0x04003C9D RID: 15517
		private RequestFlags moveFlags;

		// Token: 0x04003C9E RID: 15518
		private ADObjectId targetDatabaseForMailbox;

		// Token: 0x04003C9F RID: 15519
		private ADObjectId targetDatabaseForMailboxArchive;

		// Token: 0x04003CA0 RID: 15520
		private MailboxReplicationServiceClient mrsClient;

		// Token: 0x04003CA1 RID: 15521
		private IRecipientSession gcSession;

		// Token: 0x04003CA2 RID: 15522
		private IRecipientSession adSession;

		// Token: 0x04003CA3 RID: 15523
		private ITopologyConfigurationSession globalConfigSession;

		// Token: 0x04003CA4 RID: 15524
		private IConfigurationSession tenantLocalConfigSession;

		// Token: 0x04003CA5 RID: 15525
		private RequestJobProvider mrProvider;

		// Token: 0x04003CA6 RID: 15526
		private List<string> unreachableMrsServers;

		// Token: 0x04003CA7 RID: 15527
		private NetworkCredential remoteCred;

		// Token: 0x04003CA8 RID: 15528
		private ADSessionSettings sessionSettings;

		// Token: 0x04003CA9 RID: 15529
		private Organization orgConfig;
	}
}
