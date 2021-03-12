using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Imap;
using Microsoft.Exchange.Connections.Pop;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000031 RID: 49
	internal class MailboxCopierBase
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x0000A898 File Offset: 0x00008A98
		public MailboxCopierBase(Guid sourceMailboxGuid, Guid targetMailboxGuid, TransactionalRequestJob requestJob, BaseJob mrsJob, MailboxCopierFlags flags, LocalizedString sourceTracingID, LocalizedString targetTracingID)
		{
			this.SourceMailboxGuid = sourceMailboxGuid;
			this.TargetMailboxGuid = targetMailboxGuid;
			this.MRSJob = mrsJob;
			this.Report = mrsJob.Report;
			this.SourceTracingID = sourceTracingID;
			this.TargetTracingID = targetTracingID;
			this.MailboxSizeTracker = new MailboxSizeTracker();
			this.SourceMdbGuid = ((requestJob.SourceDatabase != null) ? requestJob.SourceDatabase.ObjectGuid : (requestJob.RemoteDatabaseGuid ?? Guid.Empty));
			this.DestMdbGuid = ((requestJob.TargetDatabase != null) ? requestJob.TargetDatabase.ObjectGuid : (requestJob.RemoteDatabaseGuid ?? Guid.Empty));
			this.TargetMailboxContainerGuid = requestJob.TargetContainerGuid;
			this.Direction = requestJob.Direction;
			this.Flags = flags;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000A97B File Offset: 0x00008B7B
		protected bool SourceSupportsPagedEnumerateChanges
		{
			get
			{
				return this.SourceMailbox.IsMailboxCapabilitySupported(MailboxCapabilities.PagedEnumerateChanges);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000A989 File Offset: 0x00008B89
		protected int MaxIncrementalChanges
		{
			get
			{
				return this.GetConfig<int>("MaxIncrementalChanges");
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000A998 File Offset: 0x00008B98
		public static ProxyControlFlags DefaultProxyControlFlags
		{
			get
			{
				ProxyControlFlags proxyControlFlags = ProxyControlFlags.DoNotCompress;
				if (ConfigBase<MRSConfigSchema>.GetConfig<bool>("DisableMrsProxyBuffering"))
				{
					proxyControlFlags |= ProxyControlFlags.DoNotBuffer;
				}
				return proxyControlFlags;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public Guid SourceMailboxGuid { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000A9C9 File Offset: 0x00008BC9
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000A9D1 File Offset: 0x00008BD1
		public Guid TargetMailboxGuid { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000A9DA File Offset: 0x00008BDA
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000A9E2 File Offset: 0x00008BE2
		public Guid? TargetMailboxContainerGuid { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000A9EB File Offset: 0x00008BEB
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000A9F3 File Offset: 0x00008BF3
		public LocalizedString SourceTracingID { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000A9FC File Offset: 0x00008BFC
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000AA04 File Offset: 0x00008C04
		public LocalizedString TargetTracingID { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000AA0D File Offset: 0x00008C0D
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000AA15 File Offset: 0x00008C15
		public ReportData Report { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000AA1E File Offset: 0x00008C1E
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000AA26 File Offset: 0x00008C26
		public BaseJob MRSJob { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000AA2F File Offset: 0x00008C2F
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000AA37 File Offset: 0x00008C37
		public Guid SourceMdbGuid { get; protected set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000AA40 File Offset: 0x00008C40
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000AA48 File Offset: 0x00008C48
		public Guid DestMdbGuid { get; protected set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000AA51 File Offset: 0x00008C51
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000AA59 File Offset: 0x00008C59
		public RequestDirection Direction { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000AA62 File Offset: 0x00008C62
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000AA6A File Offset: 0x00008C6A
		public MailboxCopierFlags Flags { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000AA73 File Offset: 0x00008C73
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000AA7B File Offset: 0x00008C7B
		public bool CopyMessagesCompleted { get; internal set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000AA84 File Offset: 0x00008C84
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public Guid IsIntegRequestGuid { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000AA95 File Offset: 0x00008C95
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000AA9D File Offset: 0x00008C9D
		public bool IsIntegDone { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000AAA6 File Offset: 0x00008CA6
		public bool IsSourceConnected
		{
			get
			{
				return this.SourceMailboxWrapper != null && this.SourceMailbox.IsConnected();
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000AABD File Offset: 0x00008CBD
		public bool IsDestinationConnected
		{
			get
			{
				return this.DestMailboxWrapper != null && this.DestMailbox.IsConnected();
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		public ISourceMailbox SourceMailbox
		{
			get
			{
				if (this.SourceMailboxWrapper != null)
				{
					return this.SourceMailboxWrapper.SourceMailbox;
				}
				return null;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000AAEB File Offset: 0x00008CEB
		public IDestinationMailbox DestMailbox
		{
			get
			{
				if (this.DestMailboxWrapper != null)
				{
					return this.DestMailboxWrapper.DestinationMailbox;
				}
				return null;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000AB02 File Offset: 0x00008D02
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000AB0A File Offset: 0x00008D0A
		public SourceMailboxWrapper SourceMailboxWrapper { get; protected set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000AB13 File Offset: 0x00008D13
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000AB1B File Offset: 0x00008D1B
		public DestinationMailboxWrapper DestMailboxWrapper { get; protected set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000AB24 File Offset: 0x00008D24
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000AB2C File Offset: 0x00008D2C
		public NamedPropTranslator NamedPropTranslator { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000AB35 File Offset: 0x00008D35
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000AB3D File Offset: 0x00008D3D
		public PrincipalTranslator PrincipalTranslator { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000AB46 File Offset: 0x00008D46
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000AB4E File Offset: 0x00008D4E
		public FolderIdTranslator FolderIdTranslator { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000AB57 File Offset: 0x00008D57
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000AB6E File Offset: 0x00008D6E
		public PersistedSyncData SyncState
		{
			get
			{
				if (this.DestMailboxWrapper == null)
				{
					return null;
				}
				return this.DestMailboxWrapper.SyncState;
			}
			set
			{
				this.DestMailboxWrapper.SyncState = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000AB7C File Offset: 0x00008D7C
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000AB93 File Offset: 0x00008D93
		public MailboxMapiSyncState ICSSyncState
		{
			get
			{
				if (this.DestMailboxWrapper == null)
				{
					return null;
				}
				return this.DestMailboxWrapper.ICSSyncState;
			}
			set
			{
				this.DestMailboxWrapper.ICSSyncState = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000ABA1 File Offset: 0x00008DA1
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000ABA9 File Offset: 0x00008DA9
		public DateTime TimestampWhenPersistentProgressWasMade { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000ABB2 File Offset: 0x00008DB2
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000ABBA File Offset: 0x00008DBA
		public MailboxSizeTracker MailboxSizeTracker { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000ABC4 File Offset: 0x00008DC4
		public bool SupportsRuleAPIs
		{
			get
			{
				return this.SourceMailbox.IsMailboxCapabilitySupported(MailboxCapabilities.FolderRules) && this.DestMailbox.IsMailboxCapabilitySupported(MailboxCapabilities.FolderRules) && !this.IsPst && this.SourceMailbox.IsCapabilitySupported(MRSProxyCapabilities.MergeMailbox) && this.SourceMailbox.IsCapabilitySupported(MRSProxyCapabilities.MailboxOptions) && this.DestMailbox.IsCapabilitySupported(MRSProxyCapabilities.MergeMailbox);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000AC20 File Offset: 0x00008E20
		public bool SupportsAcls
		{
			get
			{
				return !this.MRSJob.CachedRequestJob.SkipFolderACLs && !this.IsPst && this.SourceMailbox.IsMailboxCapabilitySupported(MailboxCapabilities.FolderAcls);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000AC4C File Offset: 0x00008E4C
		public bool SourceIsE15OrHigher
		{
			get
			{
				return this.SourceMailboxWrapper.MailboxVersion != null && this.SourceMailboxWrapper.MailboxVersion.Value >= Server.E15MinVersion;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000AC90 File Offset: 0x00008E90
		public bool DestinationIsE15OrHigher
		{
			get
			{
				return this.DestMailboxWrapper.MailboxVersion != null && this.DestMailboxWrapper.MailboxVersion.Value >= Server.E15MinVersion;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000ACD1 File Offset: 0x00008ED1
		public bool IsUpgradeToE15OrHigher
		{
			get
			{
				return !this.SourceIsE15OrHigher && this.DestinationIsE15OrHigher;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		public bool SupportsPerUserReadUnreadDataTransfer
		{
			get
			{
				return this.SourceMailbox.IsCapabilitySupported(MRSProxyCapabilities.CopyToWithFlags) && this.DestMailbox.IsCapabilitySupported(MRSProxyCapabilities.CopyToWithFlags) && this.SourceMailboxWrapper.MailboxVersion != null && this.SourceMailboxWrapper.MailboxVersion.Value >= MailboxCopierBase.MinExchangeVersionForPerUserReadUnreadDataTransfer && this.DestMailboxWrapper.MailboxVersion != null && this.DestMailboxWrapper.MailboxVersion.Value >= MailboxCopierBase.MinExchangeVersionForPerUserReadUnreadDataTransfer;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000AD74 File Offset: 0x00008F74
		public bool IsPublicFolder
		{
			get
			{
				return this.MRSJob.CachedRequestJob.IsPublicFolderMailboxRestore || this.MRSJob.CachedRequestJob.RequestType == MRSRequestType.PublicFolderMove || this.MRSJob.CachedRequestJob.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000ADC3 File Offset: 0x00008FC3
		public bool IsPublicFolderMigration
		{
			get
			{
				return this.MRSJob.CachedRequestJob.RequestType == MRSRequestType.PublicFolderMigration || this.MRSJob.CachedRequestJob.RequestType == MRSRequestType.PublicFolderMailboxMigration;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000ADEE File Offset: 0x00008FEE
		public bool IsPrimary
		{
			get
			{
				return !this.IsArchive;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000ADF9 File Offset: 0x00008FF9
		public bool IsArchive
		{
			get
			{
				return this.Flags.HasFlag(MailboxCopierFlags.SourceIsArchive) || this.Flags.HasFlag(MailboxCopierFlags.TargetIsArchive);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000AE2C File Offset: 0x0000902C
		public bool IsPst
		{
			get
			{
				return this.Flags.HasFlag(MailboxCopierFlags.SourceIsPST) || this.Flags.HasFlag(MailboxCopierFlags.TargetIsPST);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000AE60 File Offset: 0x00009060
		public bool IsOlcSync
		{
			get
			{
				return this.Flags.HasFlag(MailboxCopierFlags.Olc);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000AE7C File Offset: 0x0000907C
		public bool IsRoot
		{
			get
			{
				return this.Flags.HasFlag(MailboxCopierFlags.Root);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000AE98 File Offset: 0x00009098
		public bool IsIncrementalSyncPaged
		{
			get
			{
				return this.SourceSupportsPagedEnumerateChanges && this.MaxIncrementalChanges != 0;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000AEB0 File Offset: 0x000090B0
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000AEB8 File Offset: 0x000090B8
		public MailboxServerInformation SourceServerInfo { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000AEC1 File Offset: 0x000090C1
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000AEC9 File Offset: 0x000090C9
		public MailboxServerInformation TargetServerInfo { get; set; }

		// Token: 0x06000231 RID: 561 RVA: 0x0000AED4 File Offset: 0x000090D4
		public void ConfigDestinationMailbox(IDestinationMailbox destMailbox)
		{
			this.DestMailboxWrapper = new DestinationMailboxWrapper(destMailbox, MailboxWrapperFlags.Target, this.TargetTracingID, new Guid[]
			{
				this.MRSJob.RequestJobGuid,
				this.TargetMailboxGuid
			});
			this.NamedPropTranslator = new NamedPropTranslator(new Action<List<BadMessageRec>>(this.ReportBadItems), this.SourceMailboxWrapper.NamedPropMapper, this.DestMailboxWrapper.NamedPropMapper);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000AF54 File Offset: 0x00009154
		public void Config(ISourceMailbox sourceMailbox, IDestinationMailbox destMailbox)
		{
			MailboxWrapperFlags mailboxWrapperFlags = MailboxWrapperFlags.Source;
			MailboxWrapperFlags mailboxWrapperFlags2 = MailboxWrapperFlags.Target;
			if ((this.Flags & MailboxCopierFlags.SourceIsPST) != MailboxCopierFlags.None)
			{
				mailboxWrapperFlags |= MailboxWrapperFlags.PST;
			}
			if ((this.Flags & MailboxCopierFlags.TargetIsPST) != MailboxCopierFlags.None)
			{
				mailboxWrapperFlags2 |= MailboxWrapperFlags.PST;
			}
			if ((this.Flags & MailboxCopierFlags.SourceIsArchive) != MailboxCopierFlags.None)
			{
				mailboxWrapperFlags |= MailboxWrapperFlags.Archive;
			}
			if ((this.Flags & MailboxCopierFlags.TargetIsArchive) != MailboxCopierFlags.None)
			{
				mailboxWrapperFlags2 |= MailboxWrapperFlags.Archive;
			}
			this.SourceMailboxWrapper = new SourceMailboxWrapper(sourceMailbox, mailboxWrapperFlags, this.SourceTracingID);
			this.DestMailboxWrapper = new DestinationMailboxWrapper(destMailbox, mailboxWrapperFlags2, this.TargetTracingID, new Guid[]
			{
				this.MRSJob.RequestJobGuid,
				this.TargetMailboxGuid
			});
			this.NamedPropTranslator = new NamedPropTranslator(new Action<List<BadMessageRec>>(this.ReportBadItems), this.SourceMailboxWrapper.NamedPropMapper, this.DestMailboxWrapper.NamedPropMapper);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000B027 File Offset: 0x00009227
		public void ConfigTranslators(PrincipalTranslator principalTranslator, FolderIdTranslator folderIdTranslator)
		{
			this.PrincipalTranslator = principalTranslator;
			this.FolderIdTranslator = folderIdTranslator;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B038 File Offset: 0x00009238
		public virtual void ClearCachedData()
		{
			if (this.NamedPropTranslator != null)
			{
				this.NamedPropTranslator.Clear();
			}
			if (this.PrincipalTranslator != null)
			{
				this.PrincipalTranslator.Clear();
			}
			if (this.SourceMailboxWrapper != null)
			{
				this.SourceMailboxWrapper.Clear();
			}
			if (this.DestMailboxWrapper != null)
			{
				this.DestMailboxWrapper.Clear();
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000B094 File Offset: 0x00009294
		public SyncStateError LoadSyncState(ReportData report)
		{
			SyncStateFlags flags = (this.MRSJob.CachedRequestJob.RequestType == MRSRequestType.Sync) ? SyncStateFlags.Replay : SyncStateFlags.Default;
			return this.DestMailboxWrapper.LoadSyncState(this.MRSJob.CachedRequestJob.RequestGuid, report, flags);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B0D6 File Offset: 0x000092D6
		public void SaveSyncState()
		{
			MrsTracer.Service.Debug("Saving state message changes", new object[0]);
			this.DestMailboxWrapper.SaveSyncState();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000B0F8 File Offset: 0x000092F8
		public void ClearSyncState(SyncStateClearReason reason)
		{
			this.DestMailboxWrapper.ClearSyncState();
			this.UpdateTimestampWhenPersistentProgressWasMade();
			this.MRSJob.Report.Append(MrsStrings.ReportSyncStateCleared(this.SourceMailboxGuid, reason.ToString()));
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000B131 File Offset: 0x00009331
		public T GetConfig<T>(string settingName)
		{
			return this.MRSJob.GetConfig<T>(settingName);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000B140 File Offset: 0x00009340
		public virtual FolderHierarchy GetSourceFolderHierarchy()
		{
			FolderHierarchyFlags folderHierarchyFlags = FolderHierarchyFlags.None;
			if (this.IsPublicFolder)
			{
				folderHierarchyFlags |= FolderHierarchyFlags.PublicFolderMailbox;
			}
			FolderHierarchy folderHierarchy = new FolderHierarchy(folderHierarchyFlags, this.SourceMailboxWrapper);
			folderHierarchy.LoadHierarchy(EnumerateFolderHierarchyFlags.None, null, false, this.GetAdditionalFolderPtags());
			this.cachedSourceHierarchy = folderHierarchy;
			return folderHierarchy;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000B17F File Offset: 0x0000937F
		public virtual void ConfigureProviders()
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000B181 File Offset: 0x00009381
		public virtual void UnconfigureProviders()
		{
			this.ClearCachedData();
			if (this.SourceMailboxWrapper != null)
			{
				this.SourceMailboxWrapper.Dispose();
				this.SourceMailboxWrapper = null;
			}
			if (this.DestMailboxWrapper != null)
			{
				this.DestMailboxWrapper.Dispose();
				this.DestMailboxWrapper = null;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000B1BD File Offset: 0x000093BD
		public virtual void ConnectSourceMailbox(MailboxConnectFlags flags)
		{
			this.SourceMailbox.Connect(this.MRSJob.GetConnectFlags(flags));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000B1D6 File Offset: 0x000093D6
		public virtual void ConnectDestinationMailbox(MailboxConnectFlags flags)
		{
			this.DestMailbox.Connect(this.MRSJob.GetConnectFlags(flags));
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000B1EF File Offset: 0x000093EF
		public virtual void PostCreateDestinationMailbox()
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000B248 File Offset: 0x00009448
		public virtual void Disconnect()
		{
			if (this.SourceMailboxWrapper != null)
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					this.SourceMailbox.Disconnect();
				}, delegate(Exception failure)
				{
					this.Report.Append(MrsStrings.ReportFailedToDisconnectFromSource2(CommonUtils.GetFailureType(failure)), failure, ReportEntryFlags.Cleanup | ReportEntryFlags.Source);
				});
			}
			if (this.DestMailboxWrapper != null)
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					this.DestMailbox.Disconnect();
				}, delegate(Exception failure)
				{
					this.Report.Append(MrsStrings.ReportFailedToDisconnectFromDestination2(CommonUtils.GetFailureType(failure)), failure, ReportEntryFlags.Cleanup | ReportEntryFlags.Target);
				});
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000B2D4 File Offset: 0x000094D4
		public virtual FolderMap GetSourceFolderMap(GetFolderMapFlags flags)
		{
			this.SourceMailboxWrapper.LoadFolderMap(flags, () => new FolderMap(this.SourceMailboxWrapper.LoadFolders<FolderRecWrapper>(EnumerateFolderHierarchyFlags.None, this.GetAdditionalFolderPtags())));
			return this.SourceMailboxWrapper.FolderMap;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000B30D File Offset: 0x0000950D
		public virtual FolderMap GetDestinationFolderMap(GetFolderMapFlags flags)
		{
			this.DestMailboxWrapper.LoadFolderMap(flags, () => new FolderMap(this.DestMailboxWrapper.LoadFolders<FolderRecWrapper>(EnumerateFolderHierarchyFlags.None, null)));
			return this.DestMailboxWrapper.FolderMap;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000B332 File Offset: 0x00009532
		protected virtual bool ShouldCompareParentIDs()
		{
			return true;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000B335 File Offset: 0x00009535
		protected virtual EnumerateMessagesFlags GetAdditionalEnumerateMessagesFlagsForContentVerification()
		{
			return (EnumerateMessagesFlags)0;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B338 File Offset: 0x00009538
		protected virtual byte[] GetMessageKey(MessageRec messageRec, MailboxWrapperFlags flags)
		{
			return messageRec.EntryId;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B340 File Offset: 0x00009540
		protected virtual PropTag[] GetEnumerateMessagesPropsForContentVerification(MailboxWrapperFlags flags)
		{
			return null;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000B344 File Offset: 0x00009544
		protected virtual bool IsIgnorableItem(MessageRec msg)
		{
			if (msg.IsFAI && this.GetConfig<bool>("ContentVerificationIgnoreFAI"))
			{
				MrsTracer.Service.Debug("Ignoring missing FAI item", new object[0]);
				return true;
			}
			string text = msg[PropTag.MessageClass] as string;
			if (text != null && CommonUtils.IsValueInWildcardedList(text, this.GetConfig<string>("ContentVerificationIgnorableMsgClasses")))
			{
				MrsTracer.Service.Debug("Ignoring missing {0} item", new object[]
				{
					text
				});
				return true;
			}
			return false;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000B3C2 File Offset: 0x000095C2
		protected virtual RestrictionData GetContentRestriction()
		{
			return null;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000B408 File Offset: 0x00009608
		public FolderSizeRec VerifyFolderContents(FolderRecWrapper folderRecWrapper, WellKnownFolderType wellKnownFolderType, bool verifyInboxProperties = false)
		{
			MrsTracer.Service.Debug("Verifying folder '{0}'", new object[]
			{
				folderRecWrapper.FullFolderName
			});
			FolderSizeRec folderSizeRec = new FolderSizeRec();
			folderSizeRec.FolderID = folderRecWrapper.EntryId;
			folderSizeRec.ParentID = folderRecWrapper.ParentId;
			folderSizeRec.FolderPath = folderRecWrapper.FullFolderName;
			folderSizeRec.WKFType = wellKnownFolderType;
			folderSizeRec.MailboxGuid = this.TargetMailboxGuid;
			byte[] destinationFolderEntryId = this.GetDestinationFolderEntryId(folderRecWrapper.EntryId);
			PropValueData[] array = null;
			PropValueData[] array2 = null;
			if (wellKnownFolderType != WellKnownFolderType.Inbox)
			{
				verifyInboxProperties = false;
			}
			FolderMap destinationFolderMap = this.GetDestinationFolderMap(GetFolderMapFlags.None);
			FolderRecWrapper folderRecWrapper2 = destinationFolderMap[destinationFolderEntryId];
			if (folderRecWrapper2 == null)
			{
				if (folderSizeRec.MissingItems == null)
				{
					folderSizeRec.MissingItems = new List<BadMessageRec>();
				}
				MrsTracer.Service.Error("Target folder is missing.", new object[0]);
				folderSizeRec.MissingItems.Add(BadMessageRec.MissingFolder(folderRecWrapper.FolderRec, folderRecWrapper.FullFolderName, wellKnownFolderType));
			}
			else if (this.ShouldCompareParentIDs())
			{
				byte[] destinationFolderEntryId2 = this.GetDestinationFolderEntryId(folderRecWrapper.ParentId);
				if (!CommonUtils.IsSameEntryId(destinationFolderEntryId2, folderRecWrapper2.ParentId))
				{
					if (folderSizeRec.MissingItems == null)
					{
						folderSizeRec.MissingItems = new List<BadMessageRec>();
					}
					MrsTracer.Service.Error("Target folder is misplaced.", new object[0]);
					folderSizeRec.MissingItems.Add(BadMessageRec.MisplacedFolder(folderRecWrapper.FolderRec, folderRecWrapper.FullFolderName, wellKnownFolderType, folderRecWrapper2.ParentId));
				}
			}
			if (this.SourceIsE15OrHigher && !verifyInboxProperties && (int)(folderRecWrapper.FolderRec[PropTag.ContentCount] ?? -1) == 0 && (int)(folderRecWrapper.FolderRec[PropTag.AssocContentCount] ?? -1) == 0)
			{
				return folderSizeRec;
			}
			EnumerateMessagesFlags emFlags = EnumerateMessagesFlags.RegularMessages | EnumerateMessagesFlags.IncludeExtendedData | this.GetAdditionalEnumerateMessagesFlagsForContentVerification();
			List<MessageRec> list = null;
			EntryIdMap<MessageRec> entryIdMap = new EntryIdMap<MessageRec>();
			StringBuilder stringBuilder = null;
			using (ISourceFolder folder = this.SourceMailbox.GetFolder(folderRecWrapper.EntryId))
			{
				if (folder == null)
				{
					MrsTracer.Service.Error("Something deleted source folder from under us?", new object[0]);
					if ((this.Flags & MailboxCopierFlags.Merge) != MailboxCopierFlags.None)
					{
						return null;
					}
					throw new FolderIsMissingPermanentException(folderRecWrapper.FullFolderName);
				}
				else
				{
					RestrictionData contentRestriction = this.GetContentRestriction();
					if (contentRestriction != null)
					{
						folder.SetContentsRestriction(contentRestriction);
					}
					MrsTracer.Service.Debug("Enumerating messages in source folder", new object[0]);
					list = folder.EnumerateMessages(emFlags, this.GetEnumerateMessagesPropsForContentVerification(MailboxWrapperFlags.Source));
					MrsTracer.Service.Debug("{0} message(s) found.", new object[]
					{
						list.Count
					});
					foreach (MessageRec messageRec in list)
					{
						if (messageRec.IsFAI)
						{
							folderSizeRec.SourceFAI.Add(messageRec);
						}
						else
						{
							folderSizeRec.Source.Add(messageRec);
						}
					}
					if (verifyInboxProperties)
					{
						MrsTracer.Service.Debug("Verifying default folder references on inbox", new object[0]);
						array = folder.GetProps(MailboxCopierBase.InboxPropertiesToValidate);
					}
				}
			}
			if (folderRecWrapper2 != null)
			{
				using (IDestinationFolder folder2 = this.DestMailbox.GetFolder(destinationFolderEntryId))
				{
					if (folder2 != null)
					{
						MrsTracer.Service.Debug("Enumerating messages in dest folder", new object[0]);
						List<MessageRec> list2 = folder2.EnumerateMessages(emFlags, this.GetEnumerateMessagesPropsForContentVerification(MailboxWrapperFlags.Target));
						MrsTracer.Service.Debug("{0} message(s) found.", new object[]
						{
							list2.Count
						});
						if (this.MRSJob.TestIntegration.LogContentDetails)
						{
							stringBuilder = new StringBuilder();
							stringBuilder.AppendLine(string.Format("Target folder contents: {0}", folderRecWrapper.FullFolderName));
						}
						foreach (MessageRec messageRec2 in list2)
						{
							entryIdMap[this.GetMessageKey(messageRec2, MailboxWrapperFlags.Target)] = messageRec2;
							if (this.MRSJob.TestIntegration.LogContentDetails)
							{
								stringBuilder.AppendLine(string.Format("TargetItem: {0}{1}", TraceUtils.DumpEntryId(this.GetMessageKey(messageRec2, MailboxWrapperFlags.Target)), messageRec2.IsFAI ? ", FAI" : string.Empty));
							}
						}
						if (this.MRSJob.TestIntegration.LogContentDetails)
						{
							this.MRSJob.Report.AppendDebug(stringBuilder.ToString());
						}
						if (verifyInboxProperties)
						{
							MrsTracer.Service.Debug("Verifying default folder references on inbox", new object[0]);
							array2 = folder2.GetProps(MailboxCopierBase.InboxPropertiesToValidate);
						}
					}
					else
					{
						MrsTracer.Service.Error("Something deleted target folder from under us.", new object[0]);
					}
				}
			}
			List<MessageRec> list3 = null;
			if (this.MRSJob.TestIntegration.LogContentDetails)
			{
				stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format("Source folder verification: {0}", folderRecWrapper.FullFolderName));
			}
			foreach (MessageRec messageRec3 in list)
			{
				if (this.MRSJob.TestIntegration.LogContentDetails)
				{
					stringBuilder.AppendLine(string.Format("SourceItem: {0}{1}", TraceUtils.DumpEntryId(this.GetMessageKey(messageRec3, MailboxWrapperFlags.Source)), messageRec3.IsFAI ? ", FAI" : string.Empty));
				}
				MessageRec messageRec4;
				if (entryIdMap.TryGetValue(this.GetMessageKey(messageRec3, MailboxWrapperFlags.Source), out messageRec4))
				{
					if (messageRec4.IsFAI)
					{
						folderSizeRec.TargetFAI.Add(messageRec4);
					}
					else
					{
						folderSizeRec.Target.Add(messageRec4);
					}
				}
				else
				{
					BadItemMarker badItemMarker;
					this.SyncState.BadItems.TryGetValue(messageRec3.EntryId, out badItemMarker);
					if (badItemMarker != null && badItemMarker.Kind == BadItemKind.CorruptItem)
					{
						folderSizeRec.Corrupt.Add(messageRec3);
					}
					else if (badItemMarker != null && badItemMarker.Kind == BadItemKind.LargeItem)
					{
						folderSizeRec.Large.Add(messageRec3);
					}
					else
					{
						if (list3 == null)
						{
							list3 = new List<MessageRec>();
						}
						list3.Add(messageRec3);
						if (this.MRSJob.TestIntegration.LogContentDetails)
						{
							stringBuilder.AppendLine("The above item is suspect.");
						}
					}
				}
			}
			if (this.MRSJob.TestIntegration.LogContentDetails)
			{
				this.MRSJob.Report.AppendDebug(stringBuilder.ToString());
			}
			if (list3 != null)
			{
				List<MessageRec> lookedUpItems = new List<MessageRec>(list3.Count);
				CommonUtils.ProcessInBatches<MessageRec>(list3.ToArray(), 1000, delegate(MessageRec[] batch)
				{
					EntryIdMap<MessageRec> entryIdMap2;
					EntryIdMap<FolderRec> entryIdMap3;
					MapiUtils.LookupBadMessagesInMailbox(this.SourceMailbox, new List<MessageRec>(batch), out entryIdMap2, out entryIdMap3);
					lookedUpItems.AddRange(entryIdMap2.Values);
				});
				foreach (MessageRec messageRec5 in lookedUpItems)
				{
					if (this.IsIgnorableItem(messageRec5))
					{
						folderSizeRec.Skipped.Add(messageRec5);
					}
					else
					{
						folderSizeRec.Missing.Add(messageRec5);
						if (folderSizeRec.MissingItems == null)
						{
							folderSizeRec.MissingItems = new List<BadMessageRec>();
						}
						BadMessageRec badMessageRec = BadMessageRec.Item(messageRec5, folderRecWrapper.FolderRec, null);
						MrsTracer.Service.Error("Missing item found: {0}", new object[]
						{
							badMessageRec.ToString()
						});
						folderSizeRec.MissingItems.Add(badMessageRec);
					}
				}
			}
			if (verifyInboxProperties)
			{
				if (array == null || array2 == null || array.Length != array2.Length)
				{
					this.ReportBadItem(BadMessageRec.FolderProperty(folderRecWrapper.FolderRec, PropTag.Unresolved, "Unknown", "Unknown"));
				}
				for (int i = 0; i < array.Length; i++)
				{
					PropValueData propValueData = array[i];
					PropValueData propValueData2 = array2[i];
					PropTag propTag = (PropTag)propValueData.PropTag;
					PropTag propTag2 = (PropTag)propValueData2.PropTag;
					if (propTag.ValueType() == PropType.Error || propValueData.Value == null)
					{
						MrsTracer.Service.Debug("Property '{0}' does not exist on source.", new object[]
						{
							propTag
						});
					}
					else if (propTag != propTag2)
					{
						this.ReportBadItem(BadMessageRec.FolderProperty(folderRecWrapper.FolderRec, propTag, propTag.ToString(), propTag2.ToString()));
					}
					else
					{
						PropType propType = propTag.ValueType();
						if (propType == PropType.Binary && !ArrayComparer<byte>.EqualityComparer.Equals(propValueData.Value as byte[], propValueData2.Value as byte[]))
						{
							this.ReportBadItem(BadMessageRec.FolderProperty(folderRecWrapper.FolderRec, propTag, TraceUtils.DumpEntryId(propValueData.Value as byte[]), TraceUtils.DumpEntryId(propValueData2.Value as byte[])));
						}
					}
				}
			}
			return folderSizeRec;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000BD00 File Offset: 0x00009F00
		private void ReportBadItem(BadMessageRec record)
		{
			this.ReportBadItems(new List<BadMessageRec>(1)
			{
				record
			});
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000BD22 File Offset: 0x00009F22
		public virtual void PreProcessHierarchy()
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000BD24 File Offset: 0x00009F24
		public virtual PropTag[] GetAdditionalFolderPtags()
		{
			return MailboxCopierBase.AdditionalFolderPtags;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000BD2B File Offset: 0x00009F2B
		protected virtual PropTag[] GetAdditionalExcludedFolderPtags()
		{
			return null;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000BF04 File Offset: 0x0000A104
		public virtual void CopyFolderProperties(FolderRecWrapper folderRec, ISourceFolder sourceFolder, IDestinationFolder destFolder, FolderRecDataFlags dataToCopy, out bool wasPropertyCopyingSkipped)
		{
			if (destFolder == null)
			{
				throw new FolderCopyFailedPermanentException(folderRec.FullFolderName);
			}
			if (this.MRSJob.CachedRequestJob.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox || this.IsPublicFolderMigration)
			{
				destFolder.SetProps(CommonUtils.PropertiesToDelete);
			}
			wasPropertyCopyingSkipped = false;
			MrsTracer.Service.Debug("Copying folder properties: \"{0}\"", new object[]
			{
				folderRec.FullFolderName
			});
			folderRec.EnsureDataLoaded(sourceFolder, dataToCopy, this);
			this.TranslateFolderData(folderRec);
			if (folderRec.FolderType == FolderType.Search && (dataToCopy & FolderRecDataFlags.SearchCriteria) != FolderRecDataFlags.None)
			{
				this.CopySearchFolderCriteria(folderRec, destFolder);
			}
			HashSet<PropTag> ptagsToExclude = new HashSet<PropTag>(MailboxCopierBase.AlwaysExcludedFolderPtags);
			if (folderRec.FolderType == FolderType.Root)
			{
				ptagsToExclude.UnionWith(MailboxCopierBase.ExcludedRootFolderPtags);
			}
			FolderMapping folderMapping = folderRec as FolderMapping;
			if (folderMapping != null && folderMapping.TargetFolder != null)
			{
				if (folderMapping.TargetFolder.Flags.HasFlag(FolderMappingFlags.Root))
				{
					ptagsToExclude.UnionWith(MailboxCopierBase.ExcludedRootFolderPtags);
					ptagsToExclude.UnionWith(FolderHierarchyUtils.GetInboxReferencePtags(this.SourceMailboxWrapper.NamedPropMapper, this.SourceMailboxWrapper.MailboxVersion));
				}
				if (folderMapping.TargetFolder.WKFType != WellKnownFolderType.None || StringComparer.OrdinalIgnoreCase.Equals(folderMapping.FolderName, folderMapping.TargetFolder.FolderName))
				{
					ptagsToExclude.Add(PropTag.DisplayName);
				}
				if (folderMapping.TargetFolder.WKFType == WellKnownFolderType.Inbox)
				{
					ptagsToExclude.UnionWith(FolderHierarchyUtils.GetInboxReferencePtags(this.SourceMailboxWrapper.NamedPropMapper, this.SourceMailboxWrapper.MailboxVersion));
				}
			}
			if (this.MRSJob.CachedRequestJob.SkipFolderRules || this.SupportsRuleAPIs)
			{
				ptagsToExclude.UnionWith(MailboxCopierBase.RuleFolderPtags);
			}
			if (this.GetAdditionalExcludedFolderPtags() != null)
			{
				ptagsToExclude.UnionWith(this.GetAdditionalExcludedFolderPtags());
			}
			bool propertiesSkippedAndUnreported = false;
			CommonUtils.ProcessKnownExceptions(delegate
			{
				using (IFxProxy fxProxy = destFolder.GetFxProxy(FastTransferFlags.None))
				{
					using (IFxProxy fxProxy2 = this.CreateFxProxyTransmissionPipeline(fxProxy))
					{
						sourceFolder.CopyTo(fxProxy2, CopyPropertiesFlags.None, ptagsToExclude.ToArray<PropTag>());
					}
				}
			}, delegate(Exception failure)
			{
				if (MapiUtils.IsBadItemIndicator(failure))
				{
					if (!this.GetConfig<MRSConfigurableFeatures>("DisabledFeatures").HasFlag(MRSConfigurableFeatures.SkipCopyFolderPropertyCheck) && CommonUtils.ExceptionIsAny(failure, new WellKnownException[]
					{
						WellKnownException.MapiCorruptMidsetDeleted
					}))
					{
						this.MRSJob.Report.AppendDebug("Encountered midset deleted exception while copying properties, will skip next time");
						propertiesSkippedAndUnreported = true;
					}
					else
					{
						List<BadMessageRec> list = new List<BadMessageRec>(1);
						list.Add(BadMessageRec.Folder(folderRec.FolderRec, BadItemKind.CorruptFolderProperty, failure));
						this.ReportBadItems(list);
					}
					return true;
				}
				return false;
			});
			wasPropertyCopyingSkipped = propertiesSkippedAndUnreported;
			if (this.IsOlcSync && this.DestMailbox.IsMailboxCapabilitySupported(MailboxCapabilities.ExportFolders) && this.SourceMailbox.IsMailboxCapabilitySupported(MailboxCapabilities.ExportFolders))
			{
				EntryIdMap<byte[]> entryIdMap = new EntryIdMap<byte[]>();
				byte[] destinationFolderEntryId = this.GetDestinationFolderEntryId(folderRec.EntryId);
				entryIdMap[folderRec.EntryId] = destinationFolderEntryId;
				CommonUtils.ProcessKnownExceptions(delegate
				{
					using (IFxProxyPool fxProxyPoolTransmissionPipeline = this.GetFxProxyPoolTransmissionPipeline(entryIdMap))
					{
						this.SourceMailbox.ExportFolders(new List<byte[]>(entryIdMap.Keys), fxProxyPoolTransmissionPipeline, ExportFoldersDataToCopyFlags.None, GetFolderRecFlags.None, null, CopyPropertiesFlags.None, null, AclFlags.None);
					}
				}, delegate(Exception failure)
				{
					if (MapiUtils.IsBadItemIndicator(failure))
					{
						List<BadMessageRec> list = new List<BadMessageRec>(1);
						list.Add(BadMessageRec.Folder(folderRec.FolderRec, BadItemKind.CorruptFolderProperty, failure));
						this.ReportBadItems(list);
						return true;
					}
					return false;
				});
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C21C File Offset: 0x0000A41C
		public void CopyFolderRules(FolderRecWrapper folderRec, ISourceFolder sourceFolder, IDestinationFolder destFolder)
		{
			if (destFolder == null)
			{
				throw new FolderCopyFailedPermanentException(folderRec.FullFolderName);
			}
			if (this.MRSJob.CachedRequestJob.SkipFolderRules || !this.SupportsRuleAPIs || !folderRec.AreRulesSupported())
			{
				return;
			}
			MrsTracer.Service.Debug("Copying folder rules: \"{0}\"", new object[]
			{
				folderRec.FullFolderName
			});
			folderRec.EnsureDataLoaded(sourceFolder, FolderRecDataFlags.Rules, this);
			this.TranslateFolderData(folderRec);
			folderRec.WriteRules(destFolder, new Action<List<BadMessageRec>>(this.ReportBadItems));
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		public void CopyFolderAcl(FolderRecWrapper folderRec, ISourceFolder sourceFolder, IDestinationFolder destFolder)
		{
			if (folderRec.FolderType == FolderType.Search || !this.SupportsAcls)
			{
				return;
			}
			FolderRecDataFlags dataToCopy;
			if (CommonUtils.ShouldUseExtendedAclInformation(this.SourceMailbox, this.DestMailbox))
			{
				dataToCopy = FolderRecDataFlags.ExtendedAclInformation;
			}
			else if (this.Flags.HasFlag(MailboxCopierFlags.CrossOrg) && (this.Flags.HasFlag(MailboxCopierFlags.Merge) || this.Flags.HasFlag(MailboxCopierFlags.PublicFolderMigration)))
			{
				dataToCopy = FolderRecDataFlags.FolderAcls;
			}
			else
			{
				dataToCopy = FolderRecDataFlags.SecurityDescriptors;
			}
			folderRec.EnsureDataLoaded(sourceFolder, dataToCopy, this);
			this.TranslateFolderData(folderRec);
			bool sourceIsTitanium = this.SourceMailboxWrapper.MailboxVersion < Server.E2007MinVersion;
			bool targetIsTitanium = this.DestMailboxWrapper.MailboxVersion < Server.E2007MinVersion;
			CommonUtils.ProcessKnownExceptions(delegate
			{
				if (dataToCopy.HasFlag(FolderRecDataFlags.FolderAcls))
				{
					destFolder.SetACL(SecurityProp.NTSD, folderRec.FolderACL);
					if (!sourceIsTitanium && !targetIsTitanium)
					{
						destFolder.SetACL(SecurityProp.FreeBusyNTSD, folderRec.FolderFreeBusyACL);
						return;
					}
				}
				else
				{
					if (dataToCopy.HasFlag(FolderRecDataFlags.ExtendedAclInformation))
					{
						destFolder.SetExtendedAcl(AclFlags.FolderAcl, folderRec.FolderACL);
						destFolder.SetExtendedAcl(AclFlags.FreeBusyAcl, folderRec.FolderFreeBusyACL);
						return;
					}
					if (dataToCopy.HasFlag(FolderRecDataFlags.SecurityDescriptors))
					{
						destFolder.SetSecurityDescriptor(SecurityProp.NTSD, folderRec.FolderNTSD);
						if (!sourceIsTitanium && !targetIsTitanium)
						{
							destFolder.SetSecurityDescriptor(SecurityProp.FreeBusyNTSD, folderRec.FolderFreeBusyNTSD);
						}
					}
				}
			}, delegate(Exception failure)
			{
				if (MapiUtils.IsBadItemIndicator(failure))
				{
					List<BadMessageRec> list = new List<BadMessageRec>(1);
					list.Add(BadMessageRec.Folder(folderRec.FolderRec, BadItemKind.CorruptFolderACL, failure));
					this.ReportBadItems(list);
					return true;
				}
				return false;
			});
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C678 File Offset: 0x0000A878
		public void CopyLocalDirectoryEntryId()
		{
			if (this.IsPublicFolderMigration || this.MRSJob.CachedRequestJob.RequestType == MRSRequestType.PublicFolderMove || this.MRSJob.CachedRequestJob.RequestType == MRSRequestType.FolderMove)
			{
				return;
			}
			CommonUtils.ProcessKnownExceptions(delegate
			{
				PropValueData[] props = this.DestMailbox.GetProps(MailboxCopierBase.LocalDirectoryEntryIdArray);
				if (props == null || props.Length != 1)
				{
					MrsTracer.Service.Warning("Failed to read local directory entry id from destination.", new object[0]);
					return;
				}
				int propTag = props[0].PropTag;
				byte[] entryId = props[0].Value as byte[];
				if (IdConverter.IsValidMessageEntryId(entryId))
				{
					return;
				}
				props = this.SourceMailbox.GetProps(MailboxCopierBase.LocalDirectoryEntryIdArray);
				if (props != null && props.Length == 1)
				{
					byte[] entryId2 = props[0].Value as byte[];
					if (IdConverter.IsValidMessageEntryId(entryId2))
					{
						this.DestMailbox.SetProps(props);
					}
				}
			}, delegate(Exception failure)
			{
				if (CommonUtils.ExceptionIsAny(failure, new WellKnownException[]
				{
					WellKnownException.MRSPermanent,
					WellKnownException.DataProviderPermanent,
					WellKnownException.CorruptData
				}))
				{
					MrsTracer.Service.Warning("Failed to update local directory entry id on mailbox. Error {0}", new object[]
					{
						failure.ToString()
					});
					CommonUtils.FullExceptionMessage(failure);
					ExecutionContext.GetDataContext(failure);
					this.ReportBadItems(new List<BadMessageRec>(1)
					{
						BadMessageRec.Folder(new FolderRec
						{
							EntryId = new byte[1]
						}, BadItemKind.CorruptFolderProperty, failure as LocalizedException)
					});
					return true;
				}
				return false;
			});
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C6D2 File Offset: 0x0000A8D2
		public void TranslateFolderData(FolderRecWrapper folderRec)
		{
			folderRec.EnumerateMappableData(this);
			folderRec.TranslateMappableData(this);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000C6E2 File Offset: 0x0000A8E2
		public virtual bool ShouldCreateFolder(FolderMap.EnumFolderContext context, FolderRecWrapper sourceFolderRecWrapper)
		{
			return true;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000C6E5 File Offset: 0x0000A8E5
		public void ExchangeSourceAndTargetVersions()
		{
			if (!this.IsOlcSync)
			{
				this.SourceMailbox.SetOtherSideVersion(this.DestMailbox.GetVersion());
			}
			this.DestMailbox.SetOtherSideVersion(this.SourceMailbox.GetVersion());
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C71B File Offset: 0x0000A91B
		protected virtual bool HasSourceFolderContents(FolderRecWrapper sourceFolderRec)
		{
			return true;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C71E File Offset: 0x0000A91E
		protected virtual bool ShouldCopyFolderProperties(FolderRecWrapper sourceFolderRec)
		{
			return true;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C724 File Offset: 0x0000A924
		protected virtual IFxProxyPool GetFxProxyPoolTransmissionPipeline(EntryIdMap<byte[]> sourceMap)
		{
			IFxProxyPool fxProxyPool = this.DestMailbox.GetFxProxyPool(sourceMap.Keys);
			return this.CreateFxProxyPoolTransmissionPipeline(fxProxyPool);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000C77C File Offset: 0x0000A97C
		public virtual void CreateFolder(FolderMap.EnumFolderContext context, FolderRecWrapper sourceFolderRecWrapper, CreateFolderFlags createFolderFlags, out byte[] newFolderEntryId)
		{
			byte[] entryId = null;
			if (sourceFolderRecWrapper.IsInternalAccess)
			{
				if (!this.DestMailbox.IsCapabilitySupported(MRSProxyCapabilities.InternalAccessFolderCreation))
				{
					throw new InternalAccessFolderCreationIsNotSupportedException();
				}
				createFolderFlags |= CreateFolderFlags.InternalAccess;
			}
			CommonUtils.TreatMissingFolderAsTransient(delegate
			{
				this.DestMailbox.CreateFolder(sourceFolderRecWrapper.FolderRec, createFolderFlags, out entryId);
			}, sourceFolderRecWrapper.FolderRec.ParentId, new Func<byte[], IFolder>(this.DestMailboxWrapper.GetFolder));
			newFolderEntryId = entryId;
			if (this.MRSJob.TestIntegration.LogContentDetails)
			{
				this.MRSJob.Report.AppendDebug(string.Format("Folder created: Name '{0}', FolderID {1}, ParentID {2}", sourceFolderRecWrapper.FolderName, TraceUtils.DumpEntryId(newFolderEntryId), TraceUtils.DumpEntryId(sourceFolderRecWrapper.ParentId)));
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000C866 File Offset: 0x0000AA66
		public virtual byte[] GetSourceFolderEntryId(FolderRecWrapper destinationFolderRec)
		{
			return destinationFolderRec.EntryId;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000C86E File Offset: 0x0000AA6E
		public virtual byte[] GetDestinationFolderEntryId(byte[] srcFolderEntryId)
		{
			return srcFolderEntryId;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000C871 File Offset: 0x0000AA71
		public virtual IFxProxyPool GetDestinationFxProxyPool(ICollection<byte[]> folderIds)
		{
			return this.DestMailbox.GetFxProxyPool(folderIds);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000C87F File Offset: 0x0000AA7F
		public virtual bool IsContentAvailableInTargetMailbox(FolderRecWrapper destinationFolderRec)
		{
			return true;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000C884 File Offset: 0x0000AA84
		public void CatchupFolder(FolderRec folderRec, ISourceFolder srcFolder)
		{
			FolderStateSnapshot folderStateSnapshot = this.ICSSyncState[folderRec.EntryId];
			if (folderStateSnapshot.LocalCommitTimeMax != DateTime.MinValue)
			{
				MrsTracer.Service.Debug("Folder contents catchup was already run.", new object[0]);
				return;
			}
			MrsTracer.Service.Debug("Catching up folder contents.", new object[0]);
			if (!folderRec.IsGhosted)
			{
				srcFolder.EnumerateChanges(EnumerateContentChangesFlags.Catchup, 0);
			}
			folderStateSnapshot.UpdateContentsCopied(folderRec);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		public void CopyMessages(List<MessageRec> batch)
		{
			this.CopyMessageBatch(batch, null);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000C904 File Offset: 0x0000AB04
		public ServerHealthStatus CheckServersHealth()
		{
			ServerHealthStatus serverHealthStatus = new ServerHealthStatus(ServerHealthState.Healthy);
			if (this.MRSJob.TestIntegration.UseLegacyCheckForHaCiHealthQuery || (this.DestMailboxWrapper.MailboxVersion != null && this.DestMailboxWrapper.MailboxVersion.Value < Server.E15MinVersion))
			{
				serverHealthStatus = this.DestMailbox.CheckServerHealth();
			}
			if (serverHealthStatus.HealthState == ServerHealthState.Healthy)
			{
				if ((this.MRSJob.TestIntegration.UseLegacyCheckForHaCiHealthQuery || (this.SourceMailboxWrapper.MailboxVersion != null && this.SourceMailboxWrapper.MailboxVersion.Value < Server.E15MinVersion)) && !this.IsOlcSync)
				{
					serverHealthStatus = this.SourceMailbox.CheckServerHealth();
				}
			}
			else
			{
				this.SourceMailboxWrapper.Ping();
			}
			return serverHealthStatus;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000C9D2 File Offset: 0x0000ABD2
		public virtual void ReportBadItems(List<BadMessageRec> badItems)
		{
			if (badItems != null && badItems.Count > 0)
			{
				this.DetermineWellKnownFolders(badItems);
				this.MRSJob.ReportBadItems(this, badItems);
				this.MRSJob.CheckBadItemCount(false);
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000CA00 File Offset: 0x0000AC00
		public void ReportSourceMailboxSize()
		{
			MailboxInformation mailboxInformation = this.SourceMailbox.GetMailboxInformation();
			if (mailboxInformation != null)
			{
				MailboxSizeRec mailboxSizeRec = new MailboxSizeRec(mailboxInformation);
				if ((this.Flags & MailboxCopierFlags.SourceIsArchive) == MailboxCopierFlags.None)
				{
					this.Report.Append(MrsStrings.ReportMailboxInfoBeforeMoveLoc(mailboxInformation.GetItemCountsAndSizesString()), mailboxSizeRec, ReportEntryFlags.Primary | ReportEntryFlags.Source);
					return;
				}
				this.Report.Append(MrsStrings.ReportMailboxArchiveInfoBeforeMoveLoc(mailboxInformation.GetItemCountsAndSizesString()), mailboxSizeRec, ReportEntryFlags.Archive | ReportEntryFlags.Source);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CA68 File Offset: 0x0000AC68
		public void ReportTargetMailboxSize()
		{
			MailboxInformation mailboxInformation = this.DestMailbox.GetMailboxInformation();
			if (mailboxInformation != null)
			{
				MailboxSizeRec mailboxSizeRec = new MailboxSizeRec(mailboxInformation);
				if ((this.Flags & MailboxCopierFlags.TargetIsArchive) == MailboxCopierFlags.None)
				{
					this.MRSJob.Report.Append(MrsStrings.ReportMailboxInfoAfterMoveLoc(mailboxInformation.GetItemCountsAndSizesString()), mailboxSizeRec, ReportEntryFlags.Primary | ReportEntryFlags.Target);
				}
				else
				{
					this.MRSJob.Report.Append(MrsStrings.ReportMailboxArchiveInfoAfterMoveLoc(mailboxInformation.GetItemCountsAndSizesString()), mailboxSizeRec, ReportEntryFlags.Archive | ReportEntryFlags.Target);
				}
				int num = 0;
				int num2 = 0;
				ulong num3 = 0UL;
				ulong num4 = 0UL;
				foreach (BadItemMarker badItemMarker in this.SyncState.BadItems.Values)
				{
					if (badItemMarker.Kind == BadItemKind.LargeItem)
					{
						num2++;
						num4 += (ulong)((long)(badItemMarker.MessageSize ?? 0));
					}
					else
					{
						num++;
						num3 += (ulong)((long)(badItemMarker.MessageSize ?? 0));
					}
				}
				if (num > 0)
				{
					this.MRSJob.Report.Append(MrsStrings.ReportCorruptItemsSkipped(num, new ByteQuantifiedSize(num3).ToString()));
				}
				if (num2 > 0)
				{
					this.MRSJob.Report.Append(MrsStrings.ReportLargeItemsSkipped(num2, new ByteQuantifiedSize(num4).ToString()));
				}
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		public virtual bool ShouldReportEntry(ReportEntryKind reportEntryKind)
		{
			return reportEntryKind != ReportEntryKind.AggregatedSoftDeletedMessages;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000D018 File Offset: 0x0000B218
		public void CopyChangedFoldersData()
		{
			this.GetSourceFolderHierarchy().ResetFolderHierarchyEnumerator();
			IEnumerator<FolderRecWrapper> sourceHierarchyEnumeratorForChangedFolders = this.GetSourceHierarchyEnumeratorForChangedFolders();
			ExDateTime exDateTime = ExDateTime.UtcNow + MailboxCopierBase.CopyFolderPropertyReportingFrequency;
			ulong num = 0UL;
			while (sourceHierarchyEnumeratorForChangedFolders.MoveNext())
			{
				num += 1UL;
				ExDateTime utcNow = ExDateTime.UtcNow;
				if (utcNow > exDateTime)
				{
					this.MRSJob.Report.Append(MrsStrings.ReportCopyFolderPropertyProgress(num - 1UL));
					exDateTime += MailboxCopierBase.CopyFolderPropertyReportingFrequency;
				}
				FolderRecWrapper folderRec = sourceHierarchyEnumeratorForChangedFolders.Current;
				ExecutionContext.Create(new DataContext[]
				{
					new FolderRecWrapperDataContext(folderRec)
				}).Execute(delegate
				{
					if (!this.ShouldCopyFolderProperties(folderRec))
					{
						MrsTracer.Service.Debug("Ignoring folder '{0}' since its contents do not reside in the mailbox '{1}'", new object[]
						{
							folderRec.FullFolderName,
							this.TargetMailboxGuid
						});
						return;
					}
					byte[] entryId = folderRec.EntryId;
					FolderStateSnapshot folderStateSnapshot = this.ICSSyncState[entryId];
					if (!folderStateSnapshot.IsFolderDataChanged(folderRec.FolderRec))
					{
						MrsTracer.Service.Debug("CopyChangedFoldersData: Skipping unchanged folder '{0}'.", new object[]
						{
							folderRec.FullFolderName
						});
						return;
					}
					if (this.Flags.HasFlag(MailboxCopierFlags.Merge))
					{
						FolderMapping folderMapping = folderRec as FolderMapping;
						if (folderMapping != null && !folderMapping.IsIncluded)
						{
							MrsTracer.Service.Debug("Changed source folder '{0}' is excluded, skipping.", new object[]
							{
								folderMapping.FullFolderName
							});
							return;
						}
					}
					if (folderStateSnapshot.State.HasFlag(FolderState.PropertiesNotCopied) && folderStateSnapshot.CopyPropertiesTimestamp < folderStateSnapshot.LocalCommitTimeMax)
					{
						this.MRSJob.Report.AppendDebug("Properties weren't copied over in an earlier pass but content has since copied over. Clearing PropertiesNotCopied flag");
						folderStateSnapshot.State &= ~FolderState.PropertiesNotCopied;
					}
					bool flag = false;
					using (ISourceFolder folder = this.SourceMailbox.GetFolder(entryId))
					{
						if (folder == null)
						{
							MrsTracer.Service.Debug("Source folder '{0}' disappeared.", new object[]
							{
								folderRec
							});
							return;
						}
						byte[] destinationFolderEntryId = this.GetDestinationFolderEntryId(entryId);
						if (destinationFolderEntryId == null)
						{
							MrsTracer.Service.Debug("Folder does not map to destination: '{0}'.", new object[]
							{
								folderRec
							});
							return;
						}
						using (IDestinationFolder folder2 = this.DestMailbox.GetFolder(destinationFolderEntryId))
						{
							if (folder2 == null)
							{
								MrsTracer.Service.Debug("Destination folder '{0}' disappeared.", new object[]
								{
									folderRec
								});
								if (!this.Flags.HasFlag(MailboxCopierFlags.Merge) && !this.Flags.HasFlag(MailboxCopierFlags.PublicFolderMigration) && this.SyncState != null && this.SyncState.SyncStage == SyncStage.FinalIncrementalSync)
								{
									throw new FolderIsMissingPermanentException(folderRec.FullFolderName);
								}
								return;
							}
							else
							{
								this.CopyFolderRules(folderRec, folder, folder2);
								this.CopyFolderAcl(folderRec, folder, folder2);
								if (!this.Flags.HasFlag(MailboxCopierFlags.Merge) && !folderStateSnapshot.State.HasFlag(FolderState.PropertiesNotCopied))
								{
									this.CopyFolderProperties(folderRec, folder, folder2, FolderRecDataFlags.None, out flag);
								}
							}
						}
					}
					if (!flag && !folderStateSnapshot.State.HasFlag(FolderState.PropertiesNotCopied))
					{
						folderStateSnapshot.UpdateFolderDataCopied(folderRec.FolderRec);
					}
					else
					{
						folderStateSnapshot.State |= FolderState.PropertiesNotCopied;
					}
					MrsTracer.Service.Debug("CopyChangedFoldersData: Copied folder '{0}'.", new object[]
					{
						folderRec.FullFolderName
					});
					if (this.MRSJob != null && this.MRSJob.TestIntegration.InjectTransientExceptionAfterFolderDataCopy && folderRec.FolderName == "FolderToInjectTransientException")
					{
						this.SaveICSSyncState(true);
						throw new CommunicationErrorTransientException("Test hook exception", new LocalizedString("Exception due to test hook after copying folder data"));
					}
				});
				this.SaveICSSyncState(false);
			}
			this.GetSourceFolderHierarchy().ResetFolderHierarchyEnumerator();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D0F2 File Offset: 0x0000B2F2
		public virtual IEnumerator<FolderRecWrapper> GetSourceHierarchyEnumeratorForChangedFolders()
		{
			return this.GetSourceFolderHierarchy().GetFolderHierarchyEnumerator(EnumHierarchyFlags.AllFolders);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D100 File Offset: 0x0000B300
		public virtual SyncContext CreateSyncContext()
		{
			return new SyncContext(this.GetSourceFolderMap(GetFolderMapFlags.ForceRefresh), this.GetDestinationFolderMap(GetFolderMapFlags.ForceRefresh));
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D118 File Offset: 0x0000B318
		public virtual IDestinationMailbox GetDestinationMailbox(Guid mdbGuid, LocalMailboxFlags targetMbxFlags, IEnumerable<MRSProxyCapabilities> mrsProxyCaps)
		{
			RequestStatisticsBase cachedRequestJob = this.MRSJob.CachedRequestJob;
			string serverName;
			string remoteOrgName;
			NetworkCredential remoteCred;
			LocalMailboxFlags localMailboxFlags;
			MailboxCopierBase.ProviderType mailboxHelper = this.GetMailboxHelper(new ADObjectId(mdbGuid, PartitionId.LocalForest.ForestFQDN), targetMbxFlags, true, out serverName, out remoteOrgName, out remoteCred, out localMailboxFlags);
			targetMbxFlags |= localMailboxFlags;
			switch (mailboxHelper)
			{
			case MailboxCopierBase.ProviderType.MAPI:
				return new MapiDestinationMailbox(targetMbxFlags);
			case MailboxCopierBase.ProviderType.Storage:
				return new StorageDestinationMailbox(targetMbxFlags);
			case MailboxCopierBase.ProviderType.TcpRemote:
			case MailboxCopierBase.ProviderType.HttpsRemote:
				return new RemoteDestinationMailbox(serverName, remoteOrgName, remoteCred, this.MRSJob.CachedRequestJob.GetProxyControlFlags() | MailboxCopierBase.DefaultProxyControlFlags, mrsProxyCaps, mailboxHelper == MailboxCopierBase.ProviderType.HttpsRemote, targetMbxFlags);
			case MailboxCopierBase.ProviderType.PST:
			{
				if (cachedRequestJob.RemoteHostName == null && !this.MRSJob.TestIntegration.UseRemoteForDestination)
				{
					return new PstDestinationMailbox();
				}
				string text = cachedRequestJob.RemoteHostName;
				NetworkCredential remoteCred2 = cachedRequestJob.RemoteCredential;
				bool useHttps = true;
				if (this.MRSJob.TestIntegration.UseRemoteForDestination && string.IsNullOrEmpty(text))
				{
					text = CommonUtils.MapDatabaseToProxyServer(this.MRSJob.CachedRequestJob.SourceMDBGuid).Fqdn;
					remoteCred2 = null;
					useHttps = false;
				}
				return new RemoteDestinationMailbox(text, null, remoteCred2, this.MRSJob.CachedRequestJob.GetProxyControlFlags() | MailboxCopierBase.DefaultProxyControlFlags, new MRSProxyCapabilities[]
				{
					MRSProxyCapabilities.RemotePstExport
				}, useHttps, LocalMailboxFlags.PstExport);
			}
			default:
				return null;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D260 File Offset: 0x0000B460
		public virtual ISourceMailbox GetSourceMailbox(ADObjectId database, LocalMailboxFlags sourceMbxFlags, IEnumerable<MRSProxyCapabilities> mrsProxyCaps)
		{
			RequestStatisticsBase cachedRequestJob = this.MRSJob.CachedRequestJob;
			string serverName;
			string remoteOrgName;
			NetworkCredential remoteCred;
			LocalMailboxFlags localMailboxFlags;
			MailboxCopierBase.ProviderType mailboxHelper = this.GetMailboxHelper(database, sourceMbxFlags, false, out serverName, out remoteOrgName, out remoteCred, out localMailboxFlags);
			sourceMbxFlags |= localMailboxFlags;
			switch (mailboxHelper)
			{
			case MailboxCopierBase.ProviderType.MAPI:
				return new MapiSourceMailbox(sourceMbxFlags);
			case MailboxCopierBase.ProviderType.Storage:
				return new StorageSourceMailbox(sourceMbxFlags);
			case MailboxCopierBase.ProviderType.TcpRemote:
			case MailboxCopierBase.ProviderType.HttpsRemote:
				return new RemoteSourceMailbox(serverName, remoteOrgName, remoteCred, this.MRSJob.CachedRequestJob.GetProxyControlFlags() | MailboxCopierBase.DefaultProxyControlFlags, mrsProxyCaps, mailboxHelper == MailboxCopierBase.ProviderType.HttpsRemote, sourceMbxFlags);
			case MailboxCopierBase.ProviderType.PST:
			{
				if (cachedRequestJob.RemoteHostName == null && !this.MRSJob.TestIntegration.UseRemoteForSource)
				{
					return new PstSourceMailbox();
				}
				string text = cachedRequestJob.RemoteHostName;
				NetworkCredential remoteCred2 = cachedRequestJob.RemoteCredential;
				bool useHttps = true;
				if (this.MRSJob.TestIntegration.UseRemoteForSource && string.IsNullOrEmpty(text))
				{
					text = CommonUtils.MapDatabaseToProxyServer(new ADObjectId(cachedRequestJob.TargetMDBGuid, PartitionId.LocalForest.ForestFQDN)).Fqdn;
					remoteCred2 = null;
					useHttps = false;
				}
				MRSProxyCapabilities mrsproxyCapabilities = (cachedRequestJob.ContentCodePage != null) ? MRSProxyCapabilities.ConfigPst : MRSProxyCapabilities.Pst;
				return new RemoteSourceMailbox(text, null, remoteCred2, this.MRSJob.CachedRequestJob.GetProxyControlFlags() | MailboxCopierBase.DefaultProxyControlFlags, new MRSProxyCapabilities[]
				{
					mrsproxyCapabilities
				}, useHttps, LocalMailboxFlags.PstImport);
			}
			case MailboxCopierBase.ProviderType.IMAP:
			{
				ConnectionParameters connectionParameters = new ConnectionParameters(new UniquelyNamedObject(), new NullLog(), long.MaxValue, ImapMailbox.ImapTimeout);
				AuthenticationMethod authenticationMethod = cachedRequestJob.AuthenticationMethod ?? AuthenticationMethod.Basic;
				ImapAuthenticationMechanism imapAuthenticationMechanism = (authenticationMethod == AuthenticationMethod.Ntlm) ? ImapAuthenticationMechanism.Ntlm : ImapAuthenticationMechanism.Basic;
				ImapSecurityMechanism securityMechanism = (ImapSecurityMechanism)cachedRequestJob.SecurityMechanism;
				ImapAuthenticationParameters authenticationParameters = new ImapAuthenticationParameters(cachedRequestJob.RemoteCredential, imapAuthenticationMechanism, securityMechanism);
				ImapServerParameters serverParameters = new ImapServerParameters(cachedRequestJob.RemoteHostName, cachedRequestJob.RemoteHostPort);
				SmtpServerParameters smtpParameters = new SmtpServerParameters(cachedRequestJob.SmtpServerName, cachedRequestJob.SmtpServerPort);
				return new ImapSourceMailbox(connectionParameters, authenticationParameters, serverParameters, smtpParameters);
			}
			case MailboxCopierBase.ProviderType.EAS:
				if (!this.MRSJob.TestIntegration.UseRemoteForSource)
				{
					return new EasSourceMailbox();
				}
				return new RemoteSourceMailbox(CommonUtils.MapDatabaseToProxyServer(cachedRequestJob.TargetMDBGuid).Fqdn, null, null, this.MRSJob.CachedRequestJob.GetProxyControlFlags() | MailboxCopierBase.DefaultProxyControlFlags, new MRSProxyCapabilities[]
				{
					MRSProxyCapabilities.Eas
				}, false, LocalMailboxFlags.EasSync);
			case MailboxCopierBase.ProviderType.POP:
			{
				ConnectionParameters connectionParameters2 = new ConnectionParameters(new UniquelyNamedObject(), new NullLog(), long.MaxValue, (int)Pop3Constants.PopConnectionTimeout.TotalMilliseconds);
				AuthenticationMethod authenticationMethod2 = cachedRequestJob.AuthenticationMethod ?? AuthenticationMethod.Basic;
				Pop3AuthenticationMechanism pop3AuthenticationMechanism = (authenticationMethod2 == AuthenticationMethod.Basic) ? Pop3AuthenticationMechanism.Basic : Pop3AuthenticationMechanism.Spa;
				Pop3SecurityMechanism securityMechanism2 = (Pop3SecurityMechanism)cachedRequestJob.SecurityMechanism;
				Pop3AuthenticationParameters authenticationParameters2 = new Pop3AuthenticationParameters(cachedRequestJob.RemoteCredential, pop3AuthenticationMechanism, securityMechanism2);
				Pop3ServerParameters serverParameters2 = new Pop3ServerParameters(cachedRequestJob.RemoteHostName, cachedRequestJob.RemoteHostPort);
				SmtpServerParameters smtpParameters2 = new SmtpServerParameters(cachedRequestJob.SmtpServerName, cachedRequestJob.SmtpServerPort);
				return new PopSourceMailbox(connectionParameters2, authenticationParameters2, serverParameters2, smtpParameters2);
			}
			default:
				return null;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D620 File Offset: 0x0000B820
		public MailboxChangesManifest EnumerateHierarchyChanges(SyncContext syncContext)
		{
			MrsTracer.Service.Debug("Enumerating source hierarchy changes.", new object[0]);
			MailboxChangesManifest changes = this.SourceMailbox.EnumerateHierarchyChanges(EnumerateHierarchyChangesFlags.None, 0);
			syncContext.SourceFolderMap.EnumerateFolderHierarchy(EnumHierarchyFlags.SearchFolders, delegate(FolderRecWrapper srcFolder, FolderMap.EnumFolderContext context)
			{
				if ((this.Flags & MailboxCopierFlags.Merge) != MailboxCopierFlags.None)
				{
					FolderMapping folderMapping = srcFolder as FolderMapping;
					if (folderMapping != null && !folderMapping.IsIncluded)
					{
						return;
					}
				}
				FolderRecWrapper targetFolderBySourceId = syncContext.GetTargetFolderBySourceId(srcFolder.EntryId);
				if (targetFolderBySourceId == null || srcFolder.FolderRec.LastModifyTimestamp > targetFolderBySourceId.FolderRec.LastModifyTimestamp)
				{
					changes.ChangedFolders.Add(srcFolder.EntryId);
				}
			});
			syncContext.TargetFolderMap.EnumerateFolderHierarchy(EnumHierarchyFlags.SearchFolders, delegate(FolderRecWrapper destFolder, FolderMap.EnumFolderContext context)
			{
				byte[] sourceEntryIdFromTargetFolder = syncContext.GetSourceEntryIdFromTargetFolder(destFolder);
				if (sourceEntryIdFromTargetFolder != null && syncContext.SourceFolderMap[sourceEntryIdFromTargetFolder] == null)
				{
					changes.DeletedFolders.Add(sourceEntryIdFromTargetFolder);
				}
			});
			List<byte[]> list = new List<byte[]>(changes.ChangedFolders.Count);
			List<byte[]> list2 = new List<byte[]>(changes.DeletedFolders.Count);
			EntryIdMap<byte[]> entryIdMap = new EntryIdMap<byte[]>();
			foreach (byte[] array in changes.DeletedFolders)
			{
				if (!entryIdMap.ContainsKey(array))
				{
					entryIdMap.Add(array, array);
					list2.Add(array);
				}
			}
			foreach (byte[] array2 in changes.ChangedFolders)
			{
				if (!entryIdMap.ContainsKey(array2))
				{
					list.Add(array2);
				}
			}
			changes.ChangedFolders = list;
			changes.DeletedFolders = list2;
			return changes;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		public IEnumerable<MailboxChanges> EnumerateContentChanges(SyncContext syncContext, MailboxChangesManifest hierarchyChanges)
		{
			EntryIdMap<byte[]> deletedSourceIDs = new EntryIdMap<byte[]>();
			EntryIdMap<byte[]> enumeratedSourceIDs = new EntryIdMap<byte[]>();
			EntryIdMap<FolderChangesManifest> contentChanges = new EntryIdMap<FolderChangesManifest>();
			foreach (byte[] array in hierarchyChanges.DeletedFolders)
			{
				deletedSourceIDs[array] = array;
			}
			syncContext.TargetFolderMap.ResetFolderHierarchyEnumerator();
			IEnumerator<FolderRecWrapper> folderHierarchyEnumerator = syncContext.TargetFolderMap.GetFolderHierarchyEnumerator(EnumHierarchyFlags.NormalFolders | EnumHierarchyFlags.RootFolder);
			while (folderHierarchyEnumerator.MoveNext())
			{
				FolderRecWrapper destFolder = folderHierarchyEnumerator.Current;
				if (!this.IsContentAvailableInTargetMailbox(destFolder))
				{
					MrsTracer.Service.Debug("Ignoring folder '{0}' since it's contents do not reside in this mailbox", new object[]
					{
						destFolder.FullFolderName
					});
				}
				else
				{
					byte[] sourceFolderId = syncContext.GetSourceEntryIdFromTargetFolder(destFolder);
					if (sourceFolderId != null && !deletedSourceIDs.ContainsKey(sourceFolderId))
					{
						enumeratedSourceIDs[sourceFolderId] = sourceFolderId;
						FolderRecWrapper srcFolderRec = syncContext.SourceFolderMap[sourceFolderId];
						if (srcFolderRec == null)
						{
							MrsTracer.Service.Debug("Folder {0} is not present in source, not syncing", new object[]
							{
								TraceUtils.DumpEntryId(sourceFolderId)
							});
						}
						else if (srcFolderRec.FolderRec.IsGhosted)
						{
							MrsTracer.Service.Debug("Source folder '{0}' is Ghosted, skipping", new object[]
							{
								srcFolderRec.FullFolderName
							});
						}
						else
						{
							foreach (MailboxChanges mailboxChanges in this.EnumerateFolderContentChanges(srcFolderRec, contentChanges))
							{
								yield return mailboxChanges;
							}
						}
					}
				}
			}
			syncContext.TargetFolderMap.ResetFolderHierarchyEnumerator();
			foreach (byte[] sourceFolderId2 in hierarchyChanges.ChangedFolders)
			{
				if (!enumeratedSourceIDs.ContainsKey(sourceFolderId2))
				{
					FolderRecWrapper srcFolderRec2 = syncContext.SourceFolderMap[sourceFolderId2];
					if (srcFolderRec2 == null)
					{
						MrsTracer.Service.Debug("Folder {0} is not present in source, not syncing", new object[]
						{
							TraceUtils.DumpEntryId(sourceFolderId2)
						});
					}
					else if (srcFolderRec2.FolderRec.IsGhosted)
					{
						MrsTracer.Service.Debug("Source folder '{0}' is Ghosted, skipping", new object[]
						{
							srcFolderRec2.FullFolderName
						});
					}
					else if (!this.HasSourceFolderContents(srcFolderRec2))
					{
						MrsTracer.Service.Debug("Ignoring folder '{0}' since its contents do not reside in this mailbox", new object[]
						{
							srcFolderRec2.FullFolderName
						});
					}
					else
					{
						foreach (MailboxChanges mailboxChanges2 in this.EnumerateFolderContentChanges(srcFolderRec2, contentChanges))
						{
							yield return mailboxChanges2;
						}
					}
				}
			}
			MailboxChanges lastMailboxChanges = new MailboxChanges(contentChanges);
			if (lastMailboxChanges.HasChanges)
			{
				yield return lastMailboxChanges;
			}
			yield break;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000DE07 File Offset: 0x0000C007
		public void AddResources(HashSet<ResourceKey> addTo)
		{
			this.AddResources(this.SourceMdbGuid, addTo, ResourceDirection.Source);
			this.AddResources(this.DestMdbGuid, addTo, ResourceDirection.Target);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000DE25 File Offset: 0x0000C025
		public void SaveICSSyncState(bool force)
		{
			this.DestMailboxWrapper.SaveICSSyncState(force);
			this.UpdateTimestampWhenPersistentProgressWasMade();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000DE3C File Offset: 0x0000C03C
		public void UpdateTimestampWhenPersistentProgressWasMade()
		{
			MrsTracer.Service.Debug("Job {0} {1} made progress", new object[]
			{
				this.MRSJob.RequestJobGuid,
				this.MRSJob.RequestJobIdentity
			});
			this.TimestampWhenPersistentProgressWasMade = DateTime.UtcNow;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000DE8C File Offset: 0x0000C08C
		public int GetBadItemsCountForCounter(BadItemCounter counter)
		{
			int num = 0;
			if (this.SyncState != null)
			{
				foreach (BadItemMarker badItemMarker in this.SyncState.BadItems.Values)
				{
					switch (badItemMarker.Kind)
					{
					case BadItemKind.CorruptItem:
					case BadItemKind.CorruptSearchFolderCriteria:
					case BadItemKind.CorruptFolderACL:
					case BadItemKind.CorruptFolderRule:
					case BadItemKind.CorruptFolderProperty:
					case BadItemKind.CorruptInferenceProperties:
					case BadItemKind.CorruptMailboxSetting:
					case BadItemKind.FolderPropertyMismatch:
						num += (counter.Count(badItemMarker.Category) ? 1 : 0);
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000DF48 File Offset: 0x0000C148
		public int GetMissingItemsCount(BadItemCounter counter)
		{
			int num = 0;
			if (this.SyncState != null)
			{
				foreach (BadItemMarker badItemMarker in this.SyncState.BadItems.Values)
				{
					BadItemKind kind = badItemMarker.Kind;
					if (kind != BadItemKind.MissingItem)
					{
						switch (kind)
						{
						case BadItemKind.MissingFolder:
						case BadItemKind.MisplacedFolder:
							break;
						default:
							continue;
						}
					}
					num += (counter.Count(badItemMarker.Category) ? 1 : 0);
				}
			}
			return num;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		public int GetLargeItemsCount(BadItemCounter counter)
		{
			int num = 0;
			if (this.SyncState != null)
			{
				foreach (BadItemMarker badItemMarker in this.SyncState.BadItems.Values)
				{
					BadItemKind kind = badItemMarker.Kind;
					if (kind == BadItemKind.LargeItem)
					{
						num += (counter.Count(badItemMarker.Category) ? 1 : 0);
					}
				}
			}
			return num;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000E060 File Offset: 0x0000C260
		public bool HasMissingItems()
		{
			BadItemCounter counter = new BadItemCounter(this.MRSJob.CachedRequestJob.SkipKnownCorruptions);
			return this.SyncState != null && this.GetMissingItemsCount(counter) > 0;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000E098 File Offset: 0x0000C298
		public void StartIsInteg()
		{
			if (this.IsIntegRequestGuid == Guid.Empty)
			{
				this.IsIntegRequestGuid = this.SourceMailbox.StartIsInteg(IsInteg.CorruptionTypesToFix.ToList<uint>());
				this.Report.Append(MrsStrings.ReportStartedIsInteg(this.SourceMailboxGuid, this.IsIntegRequestGuid));
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000E188 File Offset: 0x0000C388
		public bool QueryIsInteg()
		{
			List<StoreIntegrityCheckJob> list = this.SourceMailbox.QueryIsInteg(this.IsIntegRequestGuid);
			bool isIntegComplete = true;
			if (list == null || list.Count == 0)
			{
				throw new StoreIntegFailedTransientException(999);
			}
			string percentages = string.Empty;
			list.ForEach(delegate(StoreIntegrityCheckJob job)
			{
				if (job.JobState == 4)
				{
					throw new StoreIntegFailedTransientException(job.ErrorCode.Value);
				}
				if (job.JobState != 3)
				{
					isIntegComplete = false;
					MrsTracer.Service.Debug("Waiting Store IsInteg task for Mailbox {0}: {1}", new object[]
					{
						this.SourceMailboxGuid,
						job.ToString()
					});
				}
				percentages = percentages + job.Progress + " ";
			});
			if (isIntegComplete)
			{
				this.Report.Append(MrsStrings.ReportCompletedIsInteg(this.SourceMailboxGuid, this.IsIntegRequestGuid));
				this.IsIntegRequestGuid = Guid.Empty;
				this.IsIntegDone = true;
			}
			else
			{
				this.Report.Append(MrsStrings.ReportWaitingIsInteg(this.SourceMailboxGuid, this.IsIntegRequestGuid, percentages));
			}
			return isIntegComplete;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000E250 File Offset: 0x0000C450
		protected IFxProxy CreateFxProxyTransmissionPipeline(IFxProxy destinationProxy)
		{
			FxProxyReceiver destination = new FxProxyReceiver(destinationProxy, false);
			ProgressTrackerTransmitter destination2 = new ProgressTrackerTransmitter(destination, this.MRSJob);
			return new FxProxyTransmitter(destination2, true);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000E27C File Offset: 0x0000C47C
		protected IFxProxyPool CreateFxProxyPoolTransmissionPipeline(IFxProxyPool destinationProxy)
		{
			FxProxyPoolReceiver destination = new FxProxyPoolReceiver(destinationProxy, true);
			ProgressTrackerTransmitter destination2 = new ProgressTrackerTransmitter(destination, this.MRSJob);
			return new FxProxyPoolTransmitter(destination2, true, this.DestMailbox.GetVersion());
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		public virtual void ApplyContentsChanges(SyncContext ctx, MailboxChanges changes)
		{
			CopyMessagesCount right = this.CopyMessageBatch(null, changes);
			ctx.CopyMessagesCount += right;
			this.ReportContentChangesSynced(ctx);
			this.ICSSyncState.ProviderState = this.SourceMailbox.GetMailboxSyncState();
			this.SaveICSSyncState(false);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000E300 File Offset: 0x0000C500
		[Conditional("DEBUG")]
		public void ValidateContentChanges(MailboxChanges changes)
		{
			bool isIncrementalSyncPaged = this.IsIncrementalSyncPaged;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000E30C File Offset: 0x0000C50C
		protected void ReportContentChangesSynced(SyncContext syncContext)
		{
			if (!this.Flags.HasFlag(MailboxCopierFlags.Merge) && !this.Flags.HasFlag(MailboxCopierFlags.Imap) && !this.Flags.HasFlag(MailboxCopierFlags.Eas) && !this.Flags.HasFlag(MailboxCopierFlags.Pop))
			{
				this.Flags.HasFlag(MailboxCopierFlags.Olc);
			}
			this.Report.Append(MrsStrings.ReportIncrementalSyncContentChangesSynced2(this.TargetTracingID, syncContext.CopyMessagesCount.NewMessages, syncContext.CopyMessagesCount.Changed, syncContext.CopyMessagesCount.Deleted, syncContext.CopyMessagesCount.Read, syncContext.CopyMessagesCount.Unread, syncContext.CopyMessagesCount.Skipped, syncContext.CopyMessagesCount.TotalContentCopied));
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000E408 File Offset: 0x0000C608
		private void AddResources(Guid mdbGuid, HashSet<ResourceKey> addTo, ResourceDirection direction)
		{
			if (mdbGuid == Guid.Empty)
			{
				return;
			}
			Guid a = Guid.Empty;
			if (direction == ResourceDirection.Source && this.SourceServerInfo != null)
			{
				a = this.SourceServerInfo.MailboxServerGuid;
			}
			if (direction == ResourceDirection.Target && this.TargetServerInfo != null)
			{
				a = this.TargetServerInfo.MailboxServerGuid;
			}
			if (a != CommonUtils.LocalServerGuid)
			{
				return;
			}
			DatabaseResource instance;
			if (direction == ResourceDirection.Source)
			{
				instance = DatabaseReadResource.Cache.GetInstance(mdbGuid, WorkloadType.MailboxReplicationService);
			}
			else
			{
				instance = DatabaseWriteResource.Cache.GetInstance(mdbGuid, WorkloadType.MailboxReplicationService);
			}
			if (instance != null)
			{
				foreach (WlmResourceHealthMonitor wlmResourceHealthMonitor in instance.GetWlmResources())
				{
					addTo.Add(wlmResourceHealthMonitor.WlmResourceKey);
				}
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000E5F0 File Offset: 0x0000C7F0
		private void CopySearchFolderCriteria(FolderRecWrapper folderRec, IDestinationFolder destFolder)
		{
			if (folderRec.SearchFolderRestriction == null || folderRec.SearchFolderScopeIDs == null || folderRec.SearchFolderScopeIDs.Length == 0)
			{
				return;
			}
			if (folderRec.IsSpoolerQueue)
			{
				return;
			}
			SearchCriteriaFlags flags = SearchCriteriaFlags.None;
			if ((folderRec.SearchFolderState & SearchState.TwirTotally) != SearchState.None)
			{
				flags |= SearchCriteriaFlags.NonContentIndexed;
			}
			if ((folderRec.SearchFolderState & SearchState.Recursive) != SearchState.None)
			{
				flags |= SearchCriteriaFlags.Recursive;
			}
			if ((folderRec.SearchFolderState & SearchState.Foreground) != SearchState.None)
			{
				flags |= SearchCriteriaFlags.Foreground;
			}
			flags |= SearchCriteriaFlags.FailOnForeignEID;
			CommonUtils.ProcessKnownExceptions(delegate
			{
				destFolder.SetSearchCriteria(folderRec.SearchFolderRestriction, folderRec.SearchFolderScopeIDs, flags);
			}, delegate(Exception failure)
			{
				if (CommonUtils.ExceptionIsAny(failure, new WellKnownException[]
				{
					WellKnownException.MRSPermanent,
					WellKnownException.DataProviderPermanent,
					WellKnownException.CorruptData
				}))
				{
					MrsTracer.Service.Warning("Failed to update search criteria on folder \"{0}\", ignoring. Error {1}", new object[]
					{
						folderRec.FullFolderName,
						failure.ToString()
					});
					LocalizedString localizedString = CommonUtils.FullExceptionMessage(failure);
					string dataContext = ExecutionContext.GetDataContext(failure);
					MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_MoveUnableToApplySearchCriteria, new object[]
					{
						this.TargetTracingID,
						folderRec.FullFolderName,
						localizedString,
						dataContext
					});
					List<BadMessageRec> list = new List<BadMessageRec>(1);
					list.Add(BadMessageRec.Folder(folderRec.FolderRec, BadItemKind.CorruptSearchFolderCriteria, failure as LocalizedException));
					this.ReportBadItems(list);
					return true;
				}
				return false;
			});
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		private IEnumerable<MailboxChanges> EnumerateFolderContentChanges(FolderRecWrapper srcFolderRec, EntryIdMap<FolderChangesManifest> contentChanges)
		{
			IEnumerable<FolderChangesManifest> folderChangesManifests = null;
			ExecutionContext.Create(new DataContext[]
			{
				new FolderRecWrapperDataContext(srcFolderRec)
			}).Execute(delegate
			{
				folderChangesManifests = this.EnumerateChanges(srcFolderRec, contentChanges);
			});
			foreach (FolderChangesManifest folderChanges in folderChangesManifests)
			{
				if (folderChanges != null && folderChanges.HasChanges)
				{
					contentChanges[srcFolderRec.EntryId] = folderChanges;
					this.Report.AppendDebug(string.Format("{0}: {1}", srcFolderRec.FullFolderName, folderChanges));
					if (this.IsPageFull(srcFolderRec, folderChanges, contentChanges))
					{
						yield return new MailboxChanges(contentChanges);
						contentChanges.Clear();
					}
				}
			}
			yield break;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000EA27 File Offset: 0x0000CC27
		private bool IsPageFull(FolderRecWrapper srcFolderRec, FolderChangesManifest folderChanges, EntryIdMap<FolderChangesManifest> contentChanges)
		{
			if (!this.SourceSupportsPagedEnumerateChanges)
			{
				bool hasMoreChanges = folderChanges.HasMoreChanges;
			}
			if (!this.IsIncrementalSyncPaged)
			{
				bool hasMoreChanges2 = folderChanges.HasMoreChanges;
			}
			return this.IsIncrementalSyncPaged && folderChanges.HasMoreChanges;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		private IEnumerable<FolderChangesManifest> EnumerateChanges(FolderRecWrapper srcFolderRec, EntryIdMap<FolderChangesManifest> contentChanges)
		{
			byte[] sourceFolderId = srcFolderRec.EntryId;
			if (srcFolderRec.FolderType != FolderType.Search)
			{
				if (this.Flags.HasFlag(MailboxCopierFlags.Merge))
				{
					FolderMapping folderMapping = (FolderMapping)srcFolderRec;
					if (!folderMapping.IsIncluded)
					{
						MrsTracer.Service.Debug("Source folder '{0}' is excluded, skipping.", new object[]
						{
							folderMapping.FullFolderName
						});
						goto IL_391;
					}
				}
				FolderStateSnapshot folderSnapshot = this.ICSSyncState[sourceFolderId];
				bool checkIfGhostedFolderContentHasChanged = false;
				ContentChangeResult contentChangeResult = folderSnapshot.VerifyContentsChanged(srcFolderRec.FolderRec);
				if (contentChangeResult != ContentChangeResult.Changed)
				{
					if (!this.IsPublicFolderMigration || contentChangeResult != ContentChangeResult.Ghosted)
					{
						MrsTracer.Service.Debug("Folder '{0}' appears unchanged, will not run ICS sync.", new object[]
						{
							srcFolderRec.FullFolderName
						});
						goto IL_391;
					}
					checkIfGhostedFolderContentHasChanged = true;
					MrsTracer.Service.Debug("Folder '{0}' is ghosted, will check again from the right DB", new object[]
					{
						srcFolderRec.FullFolderName
					});
				}
				MrsTracer.Service.Debug("Folder '{0}' appears changed, running ICS sync.", new object[]
				{
					srcFolderRec.FullFolderName
				});
				FolderRec frec = srcFolderRec.FolderRec;
				using (ISourceFolder srcFolder = this.SourceMailbox.GetFolder(sourceFolderId))
				{
					if (srcFolder == null)
					{
						MrsTracer.Service.Debug("Folder '{0}' disappeared from source, will sync deletion later", new object[]
						{
							srcFolderRec.FullFolderName
						});
						yield break;
					}
					if (checkIfGhostedFolderContentHasChanged)
					{
						frec = srcFolder.GetFolderRec(this.GetAdditionalFolderPtags(), GetFolderRecFlags.None);
						if (!folderSnapshot.IsFolderChanged(frec))
						{
							MrsTracer.Service.Debug("Ghosted Folder '{0}' appears unchanged, will not run ICS sync.", new object[]
							{
								srcFolderRec.FullFolderName
							});
							yield break;
						}
					}
					FolderChangesManifest folderChanges = null;
					bool firstPage = true;
					do
					{
						CommonUtils.TreatMissingFolderAsTransient(delegate
						{
							EnumerateContentChangesFlags enumerateContentChangesFlags = EnumerateContentChangesFlags.None;
							int maxChanges = 0;
							if (this.IsIncrementalSyncPaged)
							{
								maxChanges = this.MaxIncrementalChanges - CommonUtils.CountNewOrUpdatedMessages(contentChanges);
								if (firstPage)
								{
									enumerateContentChangesFlags |= EnumerateContentChangesFlags.FirstPage;
									firstPage = false;
								}
							}
							folderChanges = srcFolder.EnumerateChanges(enumerateContentChangesFlags, maxChanges);
						}, sourceFolderId, new Func<byte[], IFolder>(this.SourceMailboxWrapper.GetFolder));
						yield return folderChanges;
					}
					while (folderChanges != null && folderChanges.HasMoreChanges);
				}
				folderSnapshot.UpdateContentsCopied(frec);
			}
			IL_391:
			yield break;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000F194 File Offset: 0x0000D394
		public void ApplyHierarchyChanges(SyncContext ctx, MailboxChangesManifest changes)
		{
			foreach (byte[] array in changes.ChangedFolders)
			{
				MailboxCopierBase.<>c__DisplayClass60 CS$<>8__locals2 = new MailboxCopierBase.<>c__DisplayClass60();
				CS$<>8__locals2.srcFolderRec = ctx.SourceFolderMap[array];
				if ((this.Flags & MailboxCopierFlags.Merge) != MailboxCopierFlags.None && CS$<>8__locals2.srcFolderRec != null)
				{
					FolderMapping folderMapping = (FolderMapping)CS$<>8__locals2.srcFolderRec;
					if (!folderMapping.IsIncluded)
					{
						MrsTracer.Service.Debug("Changed source folder '{0}' is excluded, skipping.", new object[]
						{
							folderMapping.FullFolderName
						});
						continue;
					}
				}
				using (ISourceFolder srcFolder = this.SourceMailbox.GetFolder(array))
				{
					if (srcFolder == null)
					{
						MrsTracer.Service.Debug("Changed source folder {0} was recently deleted. Will pick up deletion on the next iteration.", new object[]
						{
							TraceUtils.DumpEntryId(array)
						});
					}
					else
					{
						FolderRec folderRec = srcFolder.GetFolderRec(this.GetAdditionalFolderPtags(), GetFolderRecFlags.None);
						if (CS$<>8__locals2.srcFolderRec == null)
						{
							CS$<>8__locals2.srcFolderRec = ctx.CreateSourceFolderRec(folderRec);
							ctx.SourceFolderMap.InsertFolder(CS$<>8__locals2.srcFolderRec);
						}
						else
						{
							ctx.SourceFolderMap.UpdateFolder(folderRec);
						}
						ExecutionContext.Create(new DataContext[]
						{
							new FolderRecWrapperDataContext(CS$<>8__locals2.srcFolderRec)
						}).Execute(delegate
						{
							this.UpdateFolderAfterHierarchyChange(ctx, CS$<>8__locals2.srcFolderRec, srcFolder);
						});
					}
				}
			}
			foreach (byte[] array2 in changes.DeletedFolders)
			{
				FolderRecWrapper deletedFolderRec = ctx.GetTargetFolderBySourceId(array2);
				if (deletedFolderRec != null)
				{
					ExecutionContext.Create(new DataContext[]
					{
						new FolderRecWrapperDataContext(deletedFolderRec)
					}).Execute(delegate
					{
						MrsTracer.Service.Debug("Removing destination folder \"{0}\".", new object[]
						{
							deletedFolderRec.FolderName
						});
						TimeSpan timeout = TimeSpan.FromSeconds(1.0);
						int num = 0;
						try
						{
							IL_36:
							this.DestMailbox.DeleteFolder(deletedFolderRec.FolderRec);
						}
						catch (Exception ex)
						{
							if (CommonUtils.ExceptionIs(ex, new WellKnownException[]
							{
								WellKnownException.MapiPartialCompletion
							}) && num < 10)
							{
								num++;
								MrsTracer.Service.Warning("Got PartialCompletion during DeleteFolder(), will retry ({0}/{1})", new object[]
								{
									num,
									10
								});
								Thread.Sleep(timeout);
								goto IL_36;
							}
							throw;
						}
						if (this.MRSJob.TestIntegration.LogContentDetails)
						{
							this.MRSJob.Report.AppendDebug(string.Format("Folder deleted: Name '{0}', FolderID {1}, ParentID {2}", deletedFolderRec.FolderName, TraceUtils.DumpEntryId(deletedFolderRec.EntryId), TraceUtils.DumpEntryId(deletedFolderRec.ParentId)));
						}
						ctx.TargetFolderMap.RemoveFolder(deletedFolderRec);
						ctx.NumberOfHierarchyUpdates++;
					});
				}
				else
				{
					MrsTracer.Service.Debug("Deleted folder {0} is already not present in dest, skipping.", new object[]
					{
						TraceUtils.DumpEntryId(array2)
					});
				}
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000F47C File Offset: 0x0000D67C
		private void UpdateFolderAfterHierarchyChange(SyncContext ctx, FolderRecWrapper srcFolderRec, ISourceFolder srcFolder)
		{
			MrsTracer.Service.Function("MailboxCopier.UpdateFolderAfterHierarchyChange(\"{0}\")", new object[]
			{
				srcFolderRec.FullFolderName
			});
			FolderRecWrapper folderRecWrapper = ctx.GetTargetFolderBySourceId(srcFolderRec.EntryId);
			FolderRecWrapper targetParentFolderBySourceParentId = ctx.GetTargetParentFolderBySourceParentId(srcFolderRec.ParentId);
			if (targetParentFolderBySourceParentId == null)
			{
				MrsTracer.Service.Warning("Destination parent folder does not exist. Will pick up this change on the next iteration.", new object[0]);
				return;
			}
			if (folderRecWrapper == null || !CommonUtils.IsSameEntryId(targetParentFolderBySourceParentId.EntryId, folderRecWrapper.ParentId))
			{
				if (folderRecWrapper == null)
				{
					MrsTracer.Service.Debug("Destination folder \"{0}\" does not exist. Creating it.", new object[]
					{
						srcFolderRec.FullFolderName
					});
					if (this.ShouldCreateFolder(new FolderMap.EnumFolderContext(), srcFolderRec))
					{
						folderRecWrapper = this.CreateDestinationFolder(ctx, srcFolderRec, targetParentFolderBySourceParentId);
					}
					if (folderRecWrapper == null)
					{
						return;
					}
					ctx.TargetFolderMap.InsertFolder(folderRecWrapper);
					if (this.MRSJob.TestIntegration.LogContentDetails)
					{
						this.MRSJob.Report.AppendDebug(string.Format("Folder created: Name '{0}', FolderID {1}, ParentID {2}", folderRecWrapper.FolderName, TraceUtils.DumpEntryId(folderRecWrapper.FolderRec.EntryId), TraceUtils.DumpEntryId(folderRecWrapper.ParentId)));
					}
				}
				else
				{
					MrsTracer.Service.Debug("Destination folder \"{0}\" needs to be moved to a new parent. Moving it.", new object[]
					{
						folderRecWrapper.FullFolderName
					});
					this.DestMailbox.MoveFolder(folderRecWrapper.EntryId, folderRecWrapper.ParentId, targetParentFolderBySourceParentId.EntryId);
					byte[] parentId = folderRecWrapper.ParentId;
					FolderRec folderRec = new FolderRec(folderRecWrapper.FolderRec);
					folderRec.ParentId = targetParentFolderBySourceParentId.EntryId;
					ctx.TargetFolderMap.UpdateFolder(folderRec);
					if (this.MRSJob.TestIntegration.LogContentDetails)
					{
						this.MRSJob.Report.AppendDebug(string.Format("Folder moved: Name '{0}', FolderID {1}, OldParentID {2}, NewParentID {3}", new object[]
						{
							folderRecWrapper.FolderName,
							TraceUtils.DumpEntryId(folderRecWrapper.EntryId),
							TraceUtils.DumpEntryId(parentId),
							TraceUtils.DumpEntryId(targetParentFolderBySourceParentId.EntryId)
						}));
					}
				}
			}
			FolderRecDataFlags dataToCopy = FolderRecDataFlags.SearchCriteria;
			using (IDestinationFolder folder = this.DestMailbox.GetFolder(folderRecWrapper.EntryId))
			{
				if (folder == null)
				{
					MrsTracer.Service.Error("Something deleted destination folder from under us", new object[0]);
					throw new FolderIsMissingTransientException();
				}
				bool flag;
				this.CopyFolderProperties(srcFolderRec, srcFolder, folder, dataToCopy, out flag);
				if (flag)
				{
					FolderStateSnapshot folderStateSnapshot = this.ICSSyncState[srcFolderRec.EntryId];
					folderStateSnapshot.State |= FolderState.PropertiesNotCopied;
					this.SaveICSSyncState(false);
				}
			}
			folderRecWrapper.FolderRec.FolderName = srcFolderRec.FolderRec.FolderName;
			folderRecWrapper.FolderRec.LastModifyTimestamp = srcFolderRec.FolderRec.LastModifyTimestamp;
			ctx.NumberOfHierarchyUpdates++;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000F73C File Offset: 0x0000D93C
		protected virtual FolderRecWrapper CreateDestinationFolder(SyncContext syncContext, FolderRecWrapper srcFolderRec, FolderRecWrapper destParentRec)
		{
			CreateFolderFlags createFolderFlags = CreateFolderFlags.FailIfExists;
			if (srcFolderRec.IsInternalAccess)
			{
				if (!this.DestMailbox.IsCapabilitySupported(MRSProxyCapabilities.InternalAccessFolderCreation))
				{
					throw new InternalAccessFolderCreationIsNotSupportedException();
				}
				createFolderFlags |= CreateFolderFlags.InternalAccess;
			}
			byte[] entryId = this.Flags.HasFlag(MailboxCopierFlags.Merge) ? null : srcFolderRec.EntryId;
			FolderRecWrapper folderRecWrapper = syncContext.CreateTargetFolderRec(srcFolderRec);
			folderRecWrapper.FolderRec.EntryId = entryId;
			folderRecWrapper.FolderRec.ParentId = destParentRec.EntryId;
			byte[] entryId2;
			this.DestMailbox.CreateFolder(folderRecWrapper.FolderRec, createFolderFlags, out entryId2);
			folderRecWrapper.FolderRec.EntryId = entryId2;
			return folderRecWrapper;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000FE70 File Offset: 0x0000E070
		private CopyMessagesCount CopyMessageBatch(List<MessageRec> batch, MailboxChanges mailboxChanges)
		{
			int newMessages = 0;
			int changed = 0;
			int deleted = 0;
			int read = 0;
			int unread = 0;
			ExDateTime dtLastMessage = ExDateTime.UtcNow;
			MailboxUpdates mailboxUpdates = new MailboxUpdates();
			int batchSize;
			if (batch == null)
			{
				batch = new List<MessageRec>();
				if (mailboxChanges.FolderChanges != null)
				{
					foreach (FolderChangesManifest folderChangesManifest in mailboxChanges.FolderChanges.Values)
					{
						if (folderChangesManifest.ChangedMessages != null)
						{
							foreach (MessageRec messageRec in folderChangesManifest.ChangedMessages)
							{
								if (messageRec.IsDeleted)
								{
									mailboxUpdates.AddMessage(messageRec.FolderId, messageRec.EntryId, MessageUpdateType.Delete);
								}
								else
								{
									batch.Add(messageRec);
								}
							}
						}
						mailboxUpdates.AddReadUnread(folderChangesManifest.FolderId, folderChangesManifest.ReadMessages, folderChangesManifest.UnreadMessages);
					}
				}
				batchSize = this.GetConfig<int>("MinBatchSize");
			}
			else
			{
				batchSize = batch.Count;
			}
			int itemsCopied = 0;
			if (batch.Count > 0)
			{
				CommonUtils.ProcessInBatches<MessageRec>(batch.ToArray(), batchSize, delegate(MessageRec[] subBatch)
				{
					MrsTracer.Service.Debug("Copying {0} messages", new object[]
					{
						subBatch.Length
					});
					this.CheckHealthCallback();
					EntryIdMap<byte[]> folders = new EntryIdMap<byte[]>();
					foreach (MessageRec messageRec2 in subBatch)
					{
						if (messageRec2.IsNew)
						{
							newMessages++;
						}
						else
						{
							changed++;
						}
						byte[] destinationFolderEntryId = this.GetDestinationFolderEntryId(messageRec2.FolderId);
						if (!folders.ContainsKey(destinationFolderEntryId))
						{
							folders.Add(destinationFolderEntryId, null);
						}
					}
					List<BadMessageRec> list = new List<BadMessageRec>();
					MapiUtils.ExportMessagesWithBadItemDetection(this.SourceMailbox, new List<MessageRec>(subBatch), delegate
					{
						IFxProxyPool destinationFxProxyPool = this.GetDestinationFxProxyPool(folders.EntryIds);
						return this.CreateFxProxyPoolTransmissionPipeline(destinationFxProxyPool);
					}, ExportMessagesFlags.None, null, null, this.MRSJob.TestIntegration, ref list);
					MrsTracer.Service.Debug("Message copy is successful.", new object[0]);
					itemsCopied += subBatch.Length;
					if (this.MRSJob.TestIntegration.LogContentDetails)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendLine(string.Format("CopyMessageBatch: {0} items copied", subBatch.Length));
						foreach (MessageRec messageRec3 in subBatch)
						{
							stringBuilder.AppendLine(string.Format("ItemID {0}, FolderID {1}{2}", TraceUtils.DumpEntryId(messageRec3.EntryId), TraceUtils.DumpEntryId(messageRec3.FolderId), messageRec3.IsFAI ? ", FAI" : string.Empty));
						}
						this.MRSJob.Report.AppendDebug(stringBuilder.ToString());
					}
					if (list != null && list.Count > 0)
					{
						MrsTracer.Service.Debug("Message copy returned {0} bad/missing messages", new object[]
						{
							list.Count
						});
						List<BadMessageRec> list2 = new List<BadMessageRec>();
						foreach (BadMessageRec badMessageRec in list)
						{
							if (badMessageRec.Kind == BadItemKind.MissingItem)
							{
								mailboxUpdates.AddMessage(badMessageRec.FolderId, badMessageRec.EntryId, MessageUpdateType.Delete);
							}
							else
							{
								list2.Add(badMessageRec);
							}
						}
						if (this.MRSJob.TestIntegration.LogContentDetails && list2.Count > 0)
						{
							StringBuilder stringBuilder2 = new StringBuilder();
							stringBuilder2.AppendLine(string.Format("Bad items reported: {0} items", list2.Count));
							foreach (BadMessageRec badMessageRec2 in list2)
							{
								stringBuilder2.AppendLine(string.Format("ItemID {0}, {1}", TraceUtils.DumpEntryId(badMessageRec2.EntryId), badMessageRec2.ToString()));
							}
							this.MRSJob.Report.AppendDebug(stringBuilder2.ToString());
						}
						this.ReportBadItems(list2);
					}
					ExDateTime utcNow = ExDateTime.UtcNow;
					if (mailboxChanges != null && utcNow - dtLastMessage > BaseJob.FlushInterval)
					{
						this.MRSJob.Report.Append(MrsStrings.ReportIncrementalSyncProgress(this.TargetTracingID, itemsCopied, batch.Count));
						this.MRSJob.FlushReport(null);
						dtLastMessage = utcNow;
					}
				});
			}
			if (!mailboxUpdates.IsEmpty())
			{
				MrsTracer.Service.Debug("Applying destination updates: {0} deleted, {1} read, {2} unread", new object[]
				{
					mailboxUpdates.GetUpdateCount(MessageUpdateType.Delete),
					mailboxUpdates.GetUpdateCount(MessageUpdateType.SetRead),
					mailboxUpdates.GetUpdateCount(MessageUpdateType.SetUnread)
				});
				using (Dictionary<byte[], FolderUpdates>.ValueCollection.Enumerator enumerator3 = mailboxUpdates.FolderData.Values.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						FolderUpdates fu = enumerator3.Current;
						ExecutionContext.Create(new DataContext[]
						{
							new OperationDataContext("Applying dest folder changes", OperationType.None),
							new FolderIdDataContext(fu.FolderId)
						}).Execute(delegate
						{
							using (IDestinationFolder folder = this.DestMailbox.GetFolder(fu.FolderId))
							{
								if (folder == null)
								{
									MrsTracer.Service.Error("Destination folder {0} does not exist, unable to apply updates.", new object[]
									{
										TraceUtils.DumpEntryId(fu.FolderId)
									});
								}
								else
								{
									if (fu.DeletedMessages != null && fu.DeletedMessages.Count > 0)
									{
										folder.DeleteMessages(fu.DeletedMessages.ToArray());
										deleted += fu.DeletedMessages.Count;
										if (this.MRSJob.TestIntegration.LogContentDetails)
										{
											this.LogMessageIDs(fu.DeletedMessages, "Delete", fu.FolderId);
										}
									}
									if (fu.ReadMessages != null && fu.ReadMessages.Count > 0)
									{
										folder.SetReadFlagsOnMessages(SetReadFlags.None, fu.ReadMessages.ToArray());
										read += fu.ReadMessages.Count;
										if (this.MRSJob.TestIntegration.LogContentDetails)
										{
											this.LogMessageIDs(fu.ReadMessages, "MarkRead", fu.FolderId);
										}
									}
									if (fu.UnreadMessages != null && fu.UnreadMessages.Count > 0)
									{
										folder.SetReadFlagsOnMessages(SetReadFlags.ClearRead, fu.UnreadMessages.ToArray());
										unread += fu.UnreadMessages.Count;
										if (this.MRSJob.TestIntegration.LogContentDetails)
										{
											this.LogMessageIDs(fu.UnreadMessages, "MarkUnread", fu.FolderId);
										}
									}
								}
							}
						});
					}
				}
			}
			this.MRSJob.ProgressTracker.AddItems((uint)(newMessages + changed + deleted + read + unread));
			return new CopyMessagesCount(newMessages, changed, deleted, read, unread, 0);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000101BC File Offset: 0x0000E3BC
		private void LogMessageIDs(List<byte[]> list, string operationName, byte[] folderID)
		{
			if (list == null || list.Count == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("{0} {1} items, FolderID {2}", operationName, list.Count, TraceUtils.DumpEntryId(folderID)));
			foreach (byte[] entryId in list)
			{
				stringBuilder.AppendLine(string.Format("ItemID {0}", TraceUtils.DumpEntryId(entryId)));
			}
			this.MRSJob.Report.AppendDebug(stringBuilder.ToString());
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00010274 File Offset: 0x0000E474
		private void DetermineWellKnownFolders(List<BadMessageRec> badItems)
		{
			if (this.cachedSourceHierarchy == null)
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					this.GetSourceFolderHierarchy();
				}, null);
			}
			if (this.cachedSourceHierarchy == null)
			{
				MrsTracer.Service.Warning("Unable to load source hierarchy, will not classify bad item folders.", new object[0]);
				return;
			}
			foreach (BadMessageRec badMessageRec in badItems)
			{
				FolderMapping folderMapping = this.cachedSourceHierarchy[badMessageRec.FolderId] as FolderMapping;
				if (folderMapping != null)
				{
					badMessageRec.WellKnownFolderType = folderMapping.WKFType;
				}
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00010320 File Offset: 0x0000E520
		private void CheckHealthCallback()
		{
			MoveBaseJob moveBaseJob = this.MRSJob as MoveBaseJob;
			if (moveBaseJob != null && moveBaseJob.IsOnlineMove && moveBaseJob.SyncStage == SyncStage.IncrementalSync)
			{
				moveBaseJob.CheckServersHealth();
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00010354 File Offset: 0x0000E554
		private MailboxCopierBase.ProviderType GetMailboxHelper(ADObjectId database, LocalMailboxFlags mbxFlags, bool isDestinationMailbox, out string mrsProxyFqdn, out string remoteOrgName, out NetworkCredential remoteCredential, out LocalMailboxFlags extraMbxFlags)
		{
			RequestStatisticsBase cachedRequestJob = this.MRSJob.CachedRequestJob;
			bool flag = cachedRequestJob.Direction == RequestDirection.Push;
			bool flag2 = cachedRequestJob.RequestStyle == RequestStyle.IntraOrg || (!flag && isDestinationMailbox) || (flag && !isDestinationMailbox);
			extraMbxFlags = LocalMailboxFlags.None;
			mrsProxyFqdn = null;
			remoteOrgName = null;
			remoteCredential = null;
			if (cachedRequestJob.RequestType == MRSRequestType.Sync && !isDestinationMailbox)
			{
				switch (cachedRequestJob.SyncProtocol)
				{
				case SyncProtocol.Imap:
					return MailboxCopierBase.ProviderType.IMAP;
				case SyncProtocol.Eas:
					return MailboxCopierBase.ProviderType.EAS;
				case SyncProtocol.Pop:
					return MailboxCopierBase.ProviderType.POP;
				case SyncProtocol.Olc:
					mrsProxyFqdn = cachedRequestJob.RemoteHostName;
					return MailboxCopierBase.ProviderType.HttpsRemote;
				default:
					throw new UnexpectedErrorPermanentException((int)(100 + cachedRequestJob.SyncProtocol));
				}
			}
			else
			{
				if ((cachedRequestJob.RequestType == MRSRequestType.MailboxExport && isDestinationMailbox) || (cachedRequestJob.RequestType == MRSRequestType.MailboxImport && !isDestinationMailbox))
				{
					return MailboxCopierBase.ProviderType.PST;
				}
				if (cachedRequestJob.RequestType == MRSRequestType.Merge && !flag2)
				{
					return MailboxCopierBase.ProviderType.MAPI;
				}
				bool flag3 = !isDestinationMailbox && (cachedRequestJob.SkipStorageProviderForSource || mbxFlags.HasFlag(LocalMailboxFlags.UseMapiProvider));
				if (flag3)
				{
					extraMbxFlags |= LocalMailboxFlags.UseMapiProvider;
				}
				if (!cachedRequestJob.SkipWordBreaking && !this.GetConfig<bool>("SkipWordBreaking") && !this.MRSJob.TestIntegration.SkipWordBreaking)
				{
					extraMbxFlags |= LocalMailboxFlags.WordBreak;
					if (cachedRequestJob.InvalidateContentIndexAnnotations)
					{
						extraMbxFlags |= LocalMailboxFlags.InvalidateContentIndexAnnotations;
					}
				}
				if (!flag2)
				{
					if (cachedRequestJob.Flags.HasFlag(RequestFlags.RemoteLegacy))
					{
						return MailboxCopierBase.ProviderType.MAPI;
					}
					mrsProxyFqdn = cachedRequestJob.RemoteHostName;
					remoteOrgName = cachedRequestJob.RemoteOrgName;
					remoteCredential = cachedRequestJob.RemoteCredential;
					return MailboxCopierBase.ProviderType.HttpsRemote;
				}
				else
				{
					ProxyServerSettings proxyServerSettings = CommonUtils.MapDatabaseToProxyServer(database);
					extraMbxFlags |= proxyServerSettings.ExtraFlags;
					mrsProxyFqdn = proxyServerSettings.Fqdn;
					if (!isDestinationMailbox && this.MRSJob.TestIntegration.UseRemoteForSource && cachedRequestJob.RequestType != MRSRequestType.MailboxExport)
					{
						return MailboxCopierBase.ProviderType.TcpRemote;
					}
					if (isDestinationMailbox && this.MRSJob.TestIntegration.UseRemoteForDestination)
					{
						return MailboxCopierBase.ProviderType.TcpRemote;
					}
					if (!extraMbxFlags.HasFlag(LocalMailboxFlags.LocalMachineMapiOnly))
					{
						return MailboxCopierBase.ProviderType.MAPI;
					}
					if (!proxyServerSettings.IsProxyLocal)
					{
						return MailboxCopierBase.ProviderType.TcpRemote;
					}
					if (flag3)
					{
						return MailboxCopierBase.ProviderType.MAPI;
					}
					return MailboxCopierBase.ProviderType.Storage;
				}
			}
		}

		// Token: 0x040000F2 RID: 242
		protected static readonly PropTag[] MidsetDeletedPropTags = new PropTag[]
		{
			PropTag.MidsetDeletedExport
		};

		// Token: 0x040000F3 RID: 243
		private static readonly PropTag[] LocalDirectoryEntryIdArray = new PropTag[]
		{
			PropTag.LocalDirectoryEntryId
		};

		// Token: 0x040000F4 RID: 244
		public static readonly int MinExchangeVersionForPerUserReadUnreadDataTransfer = new ServerVersion(Server.Exchange2011MajorVersion, 0, 444, 0).ToInt();

		// Token: 0x040000F5 RID: 245
		public static readonly PropTag[] AlwaysExcludedFolderPtags = new PropTag[]
		{
			PropTag.ContainerContents,
			PropTag.FolderAssociatedContents,
			PropTag.ContainerHierarchy,
			(PropTag)1638859010U,
			PropTag.NTSD,
			PropTag.FreeBusyNTSD,
			(PropTag)1071644930U,
			PropTag.AclTable,
			(PropTag)1073611010U
		};

		// Token: 0x040000F6 RID: 246
		private static readonly PropTag[] InboxPropertiesToValidate = new PropTag[]
		{
			PropTag.ContactsFolderEntryId,
			PropTag.CalendarFolderEntryId
		};

		// Token: 0x040000F7 RID: 247
		public static readonly PropTag[] PSTIncludeMessagePtags = new PropTag[]
		{
			PropTag.DisplayCc,
			PropTag.DisplayTo,
			PropTag.DisplayBcc
		};

		// Token: 0x040000F8 RID: 248
		public static readonly PropTag[] RuleFolderPtags = new PropTag[]
		{
			PropTag.RulesTable,
			(PropTag)1071710466U
		};

		// Token: 0x040000F9 RID: 249
		public static readonly PropTag[] ExcludedRootFolderPtags = new PropTag[]
		{
			PropTag.DisplayName,
			PropTag.Comment
		};

		// Token: 0x040000FA RID: 250
		private static readonly PropTag[] AdditionalFolderPtags = new PropTag[]
		{
			PropTag.LocalCommitTimeMax,
			PropTag.DeletedCountTotal,
			PropTag.ChangeKey,
			PropTag.ContentCount,
			PropTag.MessageSizeExtended,
			PropTag.AssocContentCount,
			PropTag.AssocMessageSizeExtended,
			PropTag.ReplicaList,
			PropTag.InternalAccess
		};

		// Token: 0x040000FB RID: 251
		private FolderHierarchy cachedSourceHierarchy;

		// Token: 0x040000FC RID: 252
		private static readonly TimeSpan CopyFolderPropertyReportingFrequency = TimeSpan.FromMinutes(5.0);

		// Token: 0x02000032 RID: 50
		protected enum ProviderType
		{
			// Token: 0x04000115 RID: 277
			MAPI = 1,
			// Token: 0x04000116 RID: 278
			Storage,
			// Token: 0x04000117 RID: 279
			TcpRemote,
			// Token: 0x04000118 RID: 280
			HttpsRemote,
			// Token: 0x04000119 RID: 281
			PST,
			// Token: 0x0400011A RID: 282
			IMAP,
			// Token: 0x0400011B RID: 283
			EAS,
			// Token: 0x0400011C RID: 284
			POP,
			// Token: 0x0400011D RID: 285
			ContactSync
		}
	}
}
