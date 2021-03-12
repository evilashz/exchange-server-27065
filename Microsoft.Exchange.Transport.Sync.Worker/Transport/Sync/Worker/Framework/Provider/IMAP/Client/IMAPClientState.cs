using System;
using System.Globalization;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client
{
	// Token: 0x020001CF RID: 463
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IMAPClientState : DisposeTrackableBase
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x00022748 File Offset: 0x00020948
		internal IMAPClientState(Fqdn imapServer, int imapPort, string logonName, SecureString logonPassword, string rootFolderPath, SyncLogSession syncLogSession, string sessionId, Guid subscriptionGuid, IMAPAuthenticationMechanism authMechanism, IMAPSecurityMechanism secMechanism, AggregationType aggregationType, long maxDownloadBytesAllowed, int connectionTimeout, EventHandler<DownloadCompleteEventArgs> downloadsCompletedEventHandler, EventHandler<EventArgs> messagesDownloadedEventHandler, EventHandler<EventArgs> messagesUploadedEventHandler, EventHandler<RoundtripCompleteEventArgs> roundtripCompleteEventHandler) : this(logonName, logonPassword, rootFolderPath, syncLogSession, authMechanism, secMechanism, aggregationType)
		{
			this.commClient = new IMAPCommClient(imapServer, imapPort, syncLogSession, sessionId, subscriptionGuid, maxDownloadBytesAllowed, connectionTimeout);
			this.DownloadsCompleted += downloadsCompletedEventHandler;
			this.MessagesDownloaded += messagesDownloadedEventHandler;
			this.MessagesUploaded += messagesUploadedEventHandler;
			if (roundtripCompleteEventHandler != null)
			{
				this.RoundtripComplete += roundtripCompleteEventHandler;
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000227A3 File Offset: 0x000209A3
		internal IMAPClientState(ICommClient commClient, string logonName, SecureString logonPassword, string rootFolderPath, SyncLogSession syncLogSession, IMAPAuthenticationMechanism authMechanism, IMAPSecurityMechanism secMechanism) : this(logonName, logonPassword, rootFolderPath, syncLogSession, authMechanism, secMechanism, AggregationType.Aggregation)
		{
			this.commClient = commClient;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000227C0 File Offset: 0x000209C0
		private IMAPClientState(string logonName, SecureString logonPassword, string rootFolderPath, SyncLogSession syncLogSession, IMAPAuthenticationMechanism authMechanism, IMAPSecurityMechanism secMechanism, AggregationType aggregationType)
		{
			this.logonName = logonName;
			this.logonPassword = logonPassword;
			this.rootFolderPath = rootFolderPath;
			this.rootPathProcessingFlags = RootPathProcessingFlags.None;
			this.logSession = syncLogSession;
			this.imapAuthenticationMechanism = authMechanism;
			this.imapSecurityMechanism = secMechanism;
			this.aggregationType = aggregationType;
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000DC0 RID: 3520 RVA: 0x00022810 File Offset: 0x00020A10
		// (remove) Token: 0x06000DC1 RID: 3521 RVA: 0x00022848 File Offset: 0x00020A48
		private event EventHandler<DownloadCompleteEventArgs> DownloadsCompleted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000DC2 RID: 3522 RVA: 0x00022880 File Offset: 0x00020A80
		// (remove) Token: 0x06000DC3 RID: 3523 RVA: 0x000228B8 File Offset: 0x00020AB8
		private event EventHandler<EventArgs> MessagesDownloaded;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000DC4 RID: 3524 RVA: 0x000228F0 File Offset: 0x00020AF0
		// (remove) Token: 0x06000DC5 RID: 3525 RVA: 0x00022928 File Offset: 0x00020B28
		private event EventHandler<EventArgs> MessagesUploaded;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000DC6 RID: 3526 RVA: 0x00022960 File Offset: 0x00020B60
		// (remove) Token: 0x06000DC7 RID: 3527 RVA: 0x00022998 File Offset: 0x00020B98
		private event EventHandler<RoundtripCompleteEventArgs> RoundtripComplete;

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x000229CD File Offset: 0x00020BCD
		internal SyncLogSession Log
		{
			get
			{
				base.CheckDisposed();
				return this.logSession;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x000229DB File Offset: 0x00020BDB
		internal ICommClient CommClient
		{
			get
			{
				base.CheckDisposed();
				return this.commClient;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x000229E9 File Offset: 0x00020BE9
		internal string LogonName
		{
			get
			{
				base.CheckDisposed();
				return this.logonName;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x000229F7 File Offset: 0x00020BF7
		internal SecureString LogonPassword
		{
			get
			{
				base.CheckDisposed();
				return this.logonPassword;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00022A05 File Offset: 0x00020C05
		internal IMAPCommand CachedCommand
		{
			get
			{
				base.CheckDisposed();
				if (this.cachedCommand == null)
				{
					this.cachedCommand = new IMAPCommand();
				}
				return this.cachedCommand;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00022A26 File Offset: 0x00020C26
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x00022A34 File Offset: 0x00020C34
		internal IMAPMailFlags FlagsToRemove
		{
			get
			{
				base.CheckDisposed();
				return this.flagsToRemove;
			}
			set
			{
				base.CheckDisposed();
				this.flagsToRemove = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00022A44 File Offset: 0x00020C44
		internal string UniqueCommandId
		{
			get
			{
				base.CheckDisposed();
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
				{
					"E",
					this.currentCommandIndex++
				});
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00022A8F File Offset: 0x00020C8F
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x00022A9D File Offset: 0x00020C9D
		internal string RootFolderPath
		{
			get
			{
				base.CheckDisposed();
				return this.rootFolderPath;
			}
			set
			{
				base.CheckDisposed();
				this.rootFolderPath = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00022AAC File Offset: 0x00020CAC
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x00022ABA File Offset: 0x00020CBA
		internal ExDateTime TimeSent
		{
			get
			{
				base.CheckDisposed();
				return this.timeSent;
			}
			set
			{
				base.CheckDisposed();
				this.timeSent = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00022AC9 File Offset: 0x00020CC9
		internal RootPathProcessingFlags RootPathProcessingFlags
		{
			get
			{
				base.CheckDisposed();
				return this.rootPathProcessingFlags;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00022AD7 File Offset: 0x00020CD7
		internal IMAPAuthenticationMechanism IMAPAuthenticationMechanism
		{
			get
			{
				base.CheckDisposed();
				return this.imapAuthenticationMechanism;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00022AE5 File Offset: 0x00020CE5
		internal IMAPSecurityMechanism IMAPSecurityMechanism
		{
			get
			{
				base.CheckDisposed();
				return this.imapSecurityMechanism;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00022AF3 File Offset: 0x00020CF3
		internal AggregationType AggregationType
		{
			get
			{
				base.CheckDisposed();
				return this.aggregationType;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00022B01 File Offset: 0x00020D01
		internal void NotifyRoundtripComplete(object sender, RoundtripCompleteEventArgs roundtripCompleteEventArgs)
		{
			base.CheckDisposed();
			if (this.RoundtripComplete != null)
			{
				this.RoundtripComplete(sender, roundtripCompleteEventArgs);
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00022B1E File Offset: 0x00020D1E
		internal void ActivatePerfDownloadEvent(object sender, DownloadCompleteEventArgs eventArgs)
		{
			base.CheckDisposed();
			if (this.DownloadsCompleted != null)
			{
				this.DownloadsCompleted(sender, eventArgs);
			}
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00022B3B File Offset: 0x00020D3B
		internal void ActivatePerfMsgDownloadEvent(object sender, EventArgs eventArgs)
		{
			base.CheckDisposed();
			if (this.MessagesDownloaded != null)
			{
				this.MessagesDownloaded(sender, eventArgs);
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00022B58 File Offset: 0x00020D58
		internal void ActivatePerfMsgUploadEvent(object sender, EventArgs eventArgs)
		{
			base.CheckDisposed();
			if (this.MessagesUploaded != null)
			{
				this.MessagesUploaded(sender, eventArgs);
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00022B78 File Offset: 0x00020D78
		internal void InitializeRootPathProcessingFlags(int level, char separator)
		{
			if (level < 1 || level > IMAPSyncStorageProvider.MaxFolderLevelDepth)
			{
				throw new ArgumentException("level");
			}
			if (string.IsNullOrEmpty(this.rootFolderPath) || this.rootPathProcessingFlags != RootPathProcessingFlags.None)
			{
				return;
			}
			this.rootPathProcessingFlags = RootPathProcessingFlags.FlagsInitialized;
			if (level != 1)
			{
				this.rootPathProcessingFlags |= RootPathProcessingFlags.UnableToProcess;
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00022BCC File Offset: 0x00020DCC
		internal void UpdateRootPathProcessingFlags(Trace tracer, RootPathProcessingFlags flagToAdd)
		{
			if (string.IsNullOrEmpty(this.rootFolderPath))
			{
				return;
			}
			if ((this.rootPathProcessingFlags & RootPathProcessingFlags.UnableToProcess) == RootPathProcessingFlags.UnableToProcess)
			{
				this.Log.LogDebugging((TSLID)842UL, tracer, "Can't add the processing flag. We are in unable to process state.", new object[0]);
				return;
			}
			this.rootPathProcessingFlags |= flagToAdd;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00022C28 File Offset: 0x00020E28
		internal void UpdateRootPathProcessingFlags(Trace tracer, string mailboxName, char separator, int? level, int currentLevelMailboxCount)
		{
			if (string.IsNullOrEmpty(this.rootFolderPath))
			{
				return;
			}
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxName", mailboxName);
			if (level == null || level.Value <= 0)
			{
				throw new ArgumentException("level");
			}
			if ((this.rootPathProcessingFlags & RootPathProcessingFlags.FlagsDetermined) == RootPathProcessingFlags.FlagsDetermined || (this.rootPathProcessingFlags & RootPathProcessingFlags.UnableToProcess) == RootPathProcessingFlags.UnableToProcess)
			{
				return;
			}
			this.rootPathProcessingFlags |= RootPathProcessingFlags.FlagsDetermined;
			string text = this.RootFolderPath.TrimEnd(new char[]
			{
				separator
			});
			string text2 = mailboxName.TrimEnd(new char[]
			{
				separator
			});
			if (text2.Split(new char[]
			{
				separator
			}).Length > level || (level == 1 && text.Equals(text)))
			{
				if (mailboxName.StartsWith(text))
				{
					this.Log.LogDebugging((TSLID)843UL, tracer, "The server returns prefixed mailbox names", new object[0]);
					this.rootPathProcessingFlags |= RootPathProcessingFlags.ResponseIncludesRootPathPrefix;
					if (text.Equals(text2) && level == 1)
					{
						if (currentLevelMailboxCount == 1)
						{
							this.Log.LogVerbose((TSLID)1409UL, tracer, "Single folder equal to the path prefix at level 1. Will be treated as INBOX.", new object[0]);
							this.rootPathProcessingFlags |= RootPathProcessingFlags.FolderPathPrefixIsInbox;
							return;
						}
						this.Log.LogError((TSLID)1410UL, tracer, "Invalid server response. Mailbox name at level 1 equals the path prefix but multiple folders exist", new object[0]);
						this.rootPathProcessingFlags |= RootPathProcessingFlags.UnableToProcess;
						return;
					}
				}
				else
				{
					this.Log.LogError((TSLID)844UL, tracer, "Cannot parse prefixed mailbox name {0}. It does not start with expected prefix {1}", new object[]
					{
						mailboxName,
						this.rootFolderPath
					});
					this.rootPathProcessingFlags |= RootPathProcessingFlags.UnableToProcess;
				}
			}
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00022E31 File Offset: 0x00021031
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.commClient != null)
			{
				this.commClient.Dispose();
				this.commClient = null;
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00022E50 File Offset: 0x00021050
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IMAPClientState>(this);
		}

		// Token: 0x04000762 RID: 1890
		private ICommClient commClient;

		// Token: 0x04000763 RID: 1891
		private string logonName;

		// Token: 0x04000764 RID: 1892
		private SecureString logonPassword;

		// Token: 0x04000765 RID: 1893
		private SyncLogSession logSession;

		// Token: 0x04000766 RID: 1894
		private IMAPAuthenticationMechanism imapAuthenticationMechanism;

		// Token: 0x04000767 RID: 1895
		private IMAPSecurityMechanism imapSecurityMechanism;

		// Token: 0x04000768 RID: 1896
		private AggregationType aggregationType;

		// Token: 0x04000769 RID: 1897
		private string rootFolderPath;

		// Token: 0x0400076A RID: 1898
		private RootPathProcessingFlags rootPathProcessingFlags;

		// Token: 0x0400076B RID: 1899
		private int currentCommandIndex;

		// Token: 0x0400076C RID: 1900
		private IMAPCommand cachedCommand;

		// Token: 0x0400076D RID: 1901
		private IMAPMailFlags flagsToRemove;

		// Token: 0x0400076E RID: 1902
		private ExDateTime timeSent;
	}
}
