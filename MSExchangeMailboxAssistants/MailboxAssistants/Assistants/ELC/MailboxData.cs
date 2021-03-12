using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000039 RID: 57
	internal abstract class MailboxData : IDisposable
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000D101 File Offset: 0x0000B301
		internal MailboxData(ElcUserInformation elcUserInformation)
		{
			this.Init(elcUserInformation);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000D110 File Offset: 0x0000B310
		internal MailboxData()
		{
			this.elcUserInformation = null;
			this.mailboxSession = null;
			this.ElcReporter = null;
			this.mailboxDN = null;
			this.mailboxSmtpAddress = null;
			this.exceptions = null;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000D144 File Offset: 0x0000B344
		internal void Init(ElcUserInformation elcUserInformation)
		{
			this.elcUserInformation = elcUserInformation;
			this.mailboxSession = elcUserInformation.MailboxSession;
			this.ElcReporter = new ElcReporting(this.MailboxSession);
			this.mailboxDN = this.mailboxSession.MailboxOwner.LegacyDn.ToString();
			this.mailboxSmtpAddress = this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000D1B9 File Offset: 0x0000B3B9
		internal string HoldCleanupInternetMessageId
		{
			get
			{
				if (this.HoldEnabled && this.holdCleanupInternetMessageId == null)
				{
					this.InitializeHoldCleanupWatermark();
				}
				return this.holdCleanupInternetMessageId;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000D1D7 File Offset: 0x0000B3D7
		internal DefaultFolderType HoldCleanupFolderType
		{
			get
			{
				if (this.HoldEnabled && this.holdCleanupFolderType == DefaultFolderType.None)
				{
					this.InitializeHoldCleanupWatermark();
				}
				return this.holdCleanupFolderType;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000D1F5 File Offset: 0x0000B3F5
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000D210 File Offset: 0x0000B410
		internal static int MaxErrorsAllowed
		{
			get
			{
				if (MailboxData.maxErrorsAllowed == null)
				{
					return 10;
				}
				return MailboxData.maxErrorsAllowed.Value;
			}
			set
			{
				MailboxData.maxErrorsAllowed = new int?(value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000D21D File Offset: 0x0000B41D
		internal ElcUserInformation ElcUserInformation
		{
			get
			{
				return this.elcUserInformation;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000D225 File Offset: 0x0000B425
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000D22D File Offset: 0x0000B42D
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
			set
			{
				this.mailboxSession = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000D236 File Offset: 0x0000B436
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000D23E File Offset: 0x0000B43E
		internal int TotalErrors
		{
			get
			{
				return this.totalErrors;
			}
			set
			{
				this.totalErrors = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000D247 File Offset: 0x0000B447
		internal List<Exception> Exceptions
		{
			get
			{
				if (this.exceptions == null)
				{
					this.exceptions = new List<Exception>();
				}
				return this.exceptions;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000D262 File Offset: 0x0000B462
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000D26A File Offset: 0x0000B46A
		internal StatisticsLogEntry StatisticsLogEntry { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000D273 File Offset: 0x0000B473
		internal string MailboxDN
		{
			get
			{
				return this.mailboxDN;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000D27B File Offset: 0x0000B47B
		internal string MailboxSmtpAddress
		{
			get
			{
				return this.mailboxSmtpAddress;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000D283 File Offset: 0x0000B483
		internal bool SuspendExpiration
		{
			get
			{
				return this.elcUserInformation.SuspendExpiration;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000D290 File Offset: 0x0000B490
		internal bool LitigationHoldEnabled
		{
			get
			{
				return this.elcUserInformation.LitigationHoldEnabled;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000D29D File Offset: 0x0000B49D
		internal bool AuditEnabled
		{
			get
			{
				return this.elcUserInformation.AuditEnabled;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		internal bool QueryBasedHoldEnabled
		{
			get
			{
				return this.InPlaceHoldEnabled || (this.LitigationHoldEnabled && this.LitigationHoldDuration != null && this.LitigationHoldDuration != Unlimited<EnhancedTimeSpan>.UnlimitedValue);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000D302 File Offset: 0x0000B502
		internal Unlimited<EnhancedTimeSpan>? LitigationHoldDuration
		{
			get
			{
				return this.elcUserInformation.LitigationHoldDuration;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000D310 File Offset: 0x0000B510
		internal bool AbsoluteLitigationHoldEnabled
		{
			get
			{
				return this.LitigationHoldEnabled && (this.LitigationHoldDuration == null || this.LitigationHoldDuration == Unlimited<EnhancedTimeSpan>.UnlimitedValue);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000D35E File Offset: 0x0000B55E
		internal bool InPlaceHoldEnabled
		{
			get
			{
				return this.elcUserInformation.InPlaceHolds != null && this.elcUserInformation.InPlaceHolds.Count > 0;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000D382 File Offset: 0x0000B582
		internal bool HoldEnabled
		{
			get
			{
				return this.QueryBasedHoldEnabled || this.LitigationHoldEnabled;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000D394 File Offset: 0x0000B594
		internal DateTime Now
		{
			get
			{
				return this.elcUserInformation.Now;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000D3A1 File Offset: 0x0000B5A1
		internal DateTime UtcNow
		{
			get
			{
				return this.elcUserInformation.UtcNow;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000D3AE File Offset: 0x0000B5AE
		internal FolderArchiver FolderArchiver
		{
			get
			{
				if (this.folderArchiver == null && this.ArchiveProcessor != null)
				{
					this.folderArchiver = new FolderArchiver(this.ElcUserInformation, this.ArchiveProcessor);
				}
				return this.folderArchiver;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000D3DD File Offset: 0x0000B5DD
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000D3E5 File Offset: 0x0000B5E5
		internal ElcReporting ElcReporter
		{
			get
			{
				return this.elcReporter;
			}
			private set
			{
				this.elcReporter = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		internal IArchiveProcessor ArchiveProcessor
		{
			get
			{
				if (this.archiveProcessor == null && this.ArchiveLocation != ElcGlobals.ArchiveLocation.ArchiveNotConfigured && this.ArchiveLocation != ElcGlobals.ArchiveLocation.Archive)
				{
					this.archiveProcessor = ArchiveProcessorFactory.Create(this.ArchiveLocation, this.MailboxSession, this.ElcUserInformation.ADUser, this.StatisticsLogEntry, false);
					this.StatisticsLogEntry.ArchiveProcessor = ((this.archiveProcessor == null) ? "None" : this.archiveProcessor.GetType().Name);
				}
				return this.archiveProcessor;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000D470 File Offset: 0x0000B670
		internal ElcGlobals.ArchiveLocation ArchiveLocation
		{
			get
			{
				if (!this.mailboxSession.MailboxOwner.MailboxInfo.IsArchive)
				{
					if (this.archiveLocation == ElcGlobals.ArchiveLocation.None)
					{
						IMailboxInfo archiveMailbox = this.mailboxSession.MailboxOwner.GetArchiveMailbox();
						if (archiveMailbox != null)
						{
							if (archiveMailbox.ArchiveState == ArchiveState.Local)
							{
								if (archiveMailbox.Location != MailboxDatabaseLocation.Unknown)
								{
									if (string.Compare(this.mailboxSession.MailboxOwner.MailboxInfo.Location.ServerLegacyDn, archiveMailbox.Location.ServerLegacyDn, true) == 0)
									{
										this.archiveLocation = ElcGlobals.ArchiveLocation.SameServer;
									}
									else
									{
										this.archiveLocation = ElcGlobals.ArchiveLocation.CrossServerSameForest;
									}
								}
								else
								{
									this.archiveLocation = ElcGlobals.ArchiveLocation.ArchiveNotConfigured;
								}
							}
							else
							{
								this.archiveLocation = ElcGlobals.ArchiveLocation.RemoteArchive;
							}
						}
						else
						{
							this.archiveLocation = ElcGlobals.ArchiveLocation.ArchiveNotConfigured;
						}
					}
				}
				else
				{
					this.archiveLocation = ElcGlobals.ArchiveLocation.Archive;
				}
				return this.archiveLocation;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000D534 File Offset: 0x0000B734
		public bool IsEHAHiddenFolderWatermarkSet()
		{
			ElcMailboxHelper.ConfigState configState;
			Exception ex;
			ElcMailboxHelper.TryGetEHAHiddenFolderCleanupWatermarkInStore(this.mailboxSession.MailboxOwner, this.mailboxSession.ClientInfoString, out configState, out ex);
			return ex == null && configState == ElcMailboxHelper.ConfigState.Found;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000D569 File Offset: 0x0000B769
		public bool SetEHAHiddenFolderCleanupWatermark(out Exception exp)
		{
			return ElcMailboxHelper.SetEHAHiddenFolderCleanupWatermark(this.mailboxSession.MailboxOwner, this.mailboxSession.ClientInfoString, out exp);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000D587 File Offset: 0x0000B787
		public bool ClearEHAHiddenFolderCleanupWatermark(out Exception exp)
		{
			return ElcMailboxHelper.ClearEHAHiddenFolderCleanupWatermark(this.mailboxSession.MailboxOwner, this.mailboxSession.ClientInfoString, out exp);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000D5A5 File Offset: 0x0000B7A5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		internal void ThrowIfErrorsOverLimit()
		{
			if (this.TotalErrors++ > MailboxData.MaxErrorsAllowed)
			{
				throw new TransientMailboxException(Strings.descELCEnforcerTooManyErrors(this.mailboxSmtpAddress, MailboxData.MaxErrorsAllowed));
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000D5F0 File Offset: 0x0000B7F0
		internal void ThrowIfErrorsOverLimit(Exception e)
		{
			if (e != null)
			{
				this.Exceptions.Add(e);
			}
			if (this.TotalErrors++ > MailboxData.MaxErrorsAllowed)
			{
				throw new TransientMailboxException(Strings.descELCEnforcerTooManyErrors(this.mailboxSmtpAddress, MailboxData.MaxErrorsAllowed), new AggregateException(this.Exceptions), null);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000D648 File Offset: 0x0000B848
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this.folderArchiver != null)
					{
						this.folderArchiver.Dispose();
					}
					IDisposable disposable = this.archiveProcessor as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
						this.archiveProcessor = null;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000D698 File Offset: 0x0000B898
		private void InitializeHoldCleanupWatermark()
		{
			ElcMailboxHelper.ConfigState configState;
			Exception ex;
			ElcMailboxHelper.TryGetHoldCleanupWatermarkInStore(this.mailboxSession.MailboxOwner, this.mailboxSession.ClientInfoString, out this.holdCleanupFolderType, out this.holdCleanupInternetMessageId, out configState, out ex);
			if (ex != null)
			{
				MailboxData.Tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: Unable to retrieve the hold cleanup watermark for this mailbox.", this.mailboxSession.MailboxOwner);
				return;
			}
			if (configState != ElcMailboxHelper.ConfigState.Found || configState != ElcMailboxHelper.ConfigState.Empty)
			{
				MailboxData.Tracer.TraceDebug<IExchangePrincipal, ElcMailboxHelper.ConfigState>((long)this.GetHashCode(), "{0}: Unable to retrieve hold cleanup for this mailbox. ConfigState is {1}. No error encountered.", this.mailboxSession.MailboxOwner, configState);
			}
		}

		// Token: 0x04000198 RID: 408
		private const int DefaultMaxErrorsAllowed = 10;

		// Token: 0x04000199 RID: 409
		internal static readonly int MaxRetryCount = 3;

		// Token: 0x0400019A RID: 410
		private static int? maxErrorsAllowed;

		// Token: 0x0400019B RID: 411
		private ElcGlobals.ArchiveLocation archiveLocation;

		// Token: 0x0400019C RID: 412
		private IArchiveProcessor archiveProcessor;

		// Token: 0x0400019D RID: 413
		protected static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;

		// Token: 0x0400019E RID: 414
		private ElcUserInformation elcUserInformation;

		// Token: 0x0400019F RID: 415
		private MailboxSession mailboxSession;

		// Token: 0x040001A0 RID: 416
		private int totalErrors;

		// Token: 0x040001A1 RID: 417
		private List<Exception> exceptions;

		// Token: 0x040001A2 RID: 418
		private string mailboxDN;

		// Token: 0x040001A3 RID: 419
		private string mailboxSmtpAddress;

		// Token: 0x040001A4 RID: 420
		private FolderArchiver folderArchiver;

		// Token: 0x040001A5 RID: 421
		private bool disposed;

		// Token: 0x040001A6 RID: 422
		private ElcReporting elcReporter;

		// Token: 0x040001A7 RID: 423
		private string holdCleanupInternetMessageId;

		// Token: 0x040001A8 RID: 424
		private DefaultFolderType holdCleanupFolderType;
	}
}
