using System;
using System.Globalization;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ImapConnectionContext : DisposeTrackableBase
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000036D9 File Offset: 0x000018D9
		internal ImapConnectionContext(ConnectionParameters connectionParameters, IMonitorEvents eventsMonitor = null)
		{
			this.connectionParameters = connectionParameters;
			this.cachedCommand = new ImapCommand();
			this.WireUpOptionalEventHandlers(eventsMonitor);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000057 RID: 87 RVA: 0x000036FC File Offset: 0x000018FC
		// (remove) Token: 0x06000058 RID: 88 RVA: 0x00003734 File Offset: 0x00001934
		private event EventHandler<RoundtripCompleteEventArgs> RoundtripComplete;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000059 RID: 89 RVA: 0x0000376C File Offset: 0x0000196C
		// (remove) Token: 0x0600005A RID: 90 RVA: 0x000037A4 File Offset: 0x000019A4
		private event EventHandler<DownloadCompleteEventArgs> DownloadsCompleted;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600005B RID: 91 RVA: 0x000037DC File Offset: 0x000019DC
		// (remove) Token: 0x0600005C RID: 92 RVA: 0x00003814 File Offset: 0x00001A14
		private event EventHandler<EventArgs> MessagesDownloaded;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600005D RID: 93 RVA: 0x0000384C File Offset: 0x00001A4C
		// (remove) Token: 0x0600005E RID: 94 RVA: 0x00003884 File Offset: 0x00001A84
		private event EventHandler<EventArgs> MessagesUploaded;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000038B9 File Offset: 0x00001AB9
		internal ConnectionParameters ConnectionParameters
		{
			get
			{
				base.CheckDisposed();
				return this.connectionParameters;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000038C7 File Offset: 0x00001AC7
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000038D5 File Offset: 0x00001AD5
		internal INetworkFacade NetworkFacade
		{
			get
			{
				base.CheckDisposed();
				return this.networkFacade;
			}
			set
			{
				base.CheckDisposed();
				this.networkFacade = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000038E4 File Offset: 0x00001AE4
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000038F2 File Offset: 0x00001AF2
		internal ImapAuthenticationParameters AuthenticationParameters
		{
			get
			{
				base.CheckDisposed();
				return this.authenticationParameters;
			}
			set
			{
				base.CheckDisposed();
				this.authenticationParameters = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003901 File Offset: 0x00001B01
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000390F File Offset: 0x00001B0F
		internal ServerParameters ServerParameters
		{
			get
			{
				base.CheckDisposed();
				return this.serverParameters;
			}
			set
			{
				base.CheckDisposed();
				this.serverParameters = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000391E File Offset: 0x00001B1E
		internal string UserName
		{
			get
			{
				base.CheckDisposed();
				if (this.AuthenticationParameters == null || this.AuthenticationParameters.NetworkCredential == null)
				{
					return string.Empty;
				}
				return this.AuthenticationParameters.NetworkCredential.UserName;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003951 File Offset: 0x00001B51
		internal string Server
		{
			get
			{
				base.CheckDisposed();
				if (this.ServerParameters == null)
				{
					return string.Empty;
				}
				return this.ServerParameters.Server;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003972 File Offset: 0x00001B72
		internal ImapCommand CachedCommand
		{
			get
			{
				base.CheckDisposed();
				return this.cachedCommand;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003980 File Offset: 0x00001B80
		internal int CurrentCommandIndex
		{
			get
			{
				base.CheckDisposed();
				return this.currentCommandIndex;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000398E File Offset: 0x00001B8E
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000399C File Offset: 0x00001B9C
		internal ImapMailFlags FlagsToRemove
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000039AB File Offset: 0x00001BAB
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000039B9 File Offset: 0x00001BB9
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000039C8 File Offset: 0x00001BC8
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000039D6 File Offset: 0x00001BD6
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000039E5 File Offset: 0x00001BE5
		internal ILog Log
		{
			get
			{
				return this.ConnectionParameters.Log;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000039F2 File Offset: 0x00001BF2
		internal ImapRootPathProcessingFlags ImapRootPathProcessingFlags
		{
			get
			{
				base.CheckDisposed();
				return this.imapRootPathProcessingFlags;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003A00 File Offset: 0x00001C00
		internal ImapAuthenticationMechanism ImapAuthenticationMechanism
		{
			get
			{
				return this.AuthenticationParameters.ImapAuthenticationMechanism;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003A0D File Offset: 0x00001C0D
		internal ImapSecurityMechanism ImapSecurityMechanism
		{
			get
			{
				return this.AuthenticationParameters.ImapSecurityMechanism;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003A1A File Offset: 0x00001C1A
		internal bool IsConnected
		{
			get
			{
				return this.NetworkFacade.IsConnected;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003A27 File Offset: 0x00001C27
		internal void NotifyRoundtripComplete(object sender, RoundtripCompleteEventArgs roundtripCompleteEventArgs)
		{
			base.CheckDisposed();
			if (this.RoundtripComplete != null)
			{
				this.RoundtripComplete(sender, roundtripCompleteEventArgs);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003A44 File Offset: 0x00001C44
		internal void ActivatePerfDownloadEvent(object sender, DownloadCompleteEventArgs eventArgs)
		{
			base.CheckDisposed();
			if (this.DownloadsCompleted != null)
			{
				this.DownloadsCompleted(sender, eventArgs);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003A61 File Offset: 0x00001C61
		internal void ActivatePerfMsgDownloadEvent(object sender, EventArgs eventArgs)
		{
			base.CheckDisposed();
			if (this.MessagesDownloaded != null)
			{
				this.MessagesDownloaded(sender, eventArgs);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003A7E File Offset: 0x00001C7E
		internal void ActivatePerfMsgUploadEvent(object sender, EventArgs eventArgs)
		{
			base.CheckDisposed();
			if (this.MessagesUploaded != null)
			{
				this.MessagesUploaded(sender, eventArgs);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003A9C File Offset: 0x00001C9C
		internal string UniqueCommandId()
		{
			base.CheckDisposed();
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
			{
				"E",
				this.currentCommandIndex++
			});
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003AE8 File Offset: 0x00001CE8
		internal void InitializeRootPathProcessingFlags(int level, char separator)
		{
			base.CheckDisposed();
			if (level < 1 || level > 20)
			{
				throw new ArgumentException("level");
			}
			if (string.IsNullOrEmpty(this.RootFolderPath) || this.ImapRootPathProcessingFlags != ImapRootPathProcessingFlags.None)
			{
				return;
			}
			this.imapRootPathProcessingFlags = ImapRootPathProcessingFlags.FlagsInitialized;
			if (level != 1)
			{
				this.imapRootPathProcessingFlags |= ImapRootPathProcessingFlags.UnableToProcess;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003B40 File Offset: 0x00001D40
		internal void UpdateRootPathProcessingFlags(ImapRootPathProcessingFlags flagToAdd)
		{
			base.CheckDisposed();
			if (string.IsNullOrEmpty(this.RootFolderPath))
			{
				return;
			}
			if ((this.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.UnableToProcess) == ImapRootPathProcessingFlags.UnableToProcess)
			{
				this.Log.Debug("Can't add the processing flag. We are in unable to process state.", new object[0]);
				return;
			}
			this.Log.Assert((this.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.FlagsInitialized) == ImapRootPathProcessingFlags.FlagsInitialized, "We should not have uninitialized root path processing flags at this point", new object[0]);
			this.imapRootPathProcessingFlags |= flagToAdd;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003BB4 File Offset: 0x00001DB4
		internal void UpdateRootPathProcessingFlags(string mailboxName, char separator, int? level, int currentLevelMailboxCount)
		{
			base.CheckDisposed();
			if (string.IsNullOrEmpty(this.RootFolderPath))
			{
				return;
			}
			if (level == null || level.Value <= 0)
			{
				throw new ArgumentException("level");
			}
			if ((this.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.FlagsDetermined) == ImapRootPathProcessingFlags.FlagsDetermined || (this.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.UnableToProcess) == ImapRootPathProcessingFlags.UnableToProcess)
			{
				return;
			}
			this.Log.Assert((this.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.FlagsInitialized) == ImapRootPathProcessingFlags.FlagsInitialized, "We should never have uninitialized root path processing flags at this point", new object[0]);
			this.imapRootPathProcessingFlags |= ImapRootPathProcessingFlags.FlagsDetermined;
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
					this.Log.Debug("The server returns prefixed mailbox names", new object[0]);
					this.imapRootPathProcessingFlags |= ImapRootPathProcessingFlags.ResponseIncludesRootPathPrefix;
					if (text.Equals(text2) && level == 1)
					{
						if (currentLevelMailboxCount == 1)
						{
							this.Log.Debug("Single folder equal to the path prefix at level 1. Will be treated as INBOX.", new object[0]);
							this.imapRootPathProcessingFlags |= ImapRootPathProcessingFlags.FolderPathPrefixIsInbox;
							return;
						}
						this.Log.Error("Invalid server response. mailbox name at level 1 equals the path prefix but multiple folders exist", new object[0]);
						this.imapRootPathProcessingFlags |= ImapRootPathProcessingFlags.UnableToProcess;
						return;
					}
				}
				else
				{
					this.Log.Error("Cannot parse prefixed mailbox name {0}. It does not start with expected prefix {1}", new object[]
					{
						mailboxName,
						this.RootFolderPath
					});
					this.imapRootPathProcessingFlags |= ImapRootPathProcessingFlags.UnableToProcess;
				}
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003DA6 File Offset: 0x00001FA6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ImapConnectionContext>(this);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003DAE File Offset: 0x00001FAE
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.NetworkFacade != null)
			{
				this.NetworkFacade.Dispose();
				this.NetworkFacade = null;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003DCD File Offset: 0x00001FCD
		private void WireUpOptionalEventHandlers(IMonitorEvents eventsMonitor)
		{
			if (eventsMonitor == null)
			{
				return;
			}
			this.DownloadsCompleted += eventsMonitor.DownloadsCompletedEventHandler;
			this.MessagesDownloaded += eventsMonitor.MessagesDownloadedEventHandler;
			this.MessagesUploaded += eventsMonitor.MessagesUploadedEventHandler;
			if (eventsMonitor.RoundtripCompleteEventHandler != null)
			{
				this.RoundtripComplete += eventsMonitor.RoundtripCompleteEventHandler;
			}
		}

		// Token: 0x04000045 RID: 69
		private readonly ConnectionParameters connectionParameters;

		// Token: 0x04000046 RID: 70
		private readonly ImapCommand cachedCommand;

		// Token: 0x04000047 RID: 71
		private ServerParameters serverParameters;

		// Token: 0x04000048 RID: 72
		private ImapAuthenticationParameters authenticationParameters;

		// Token: 0x04000049 RID: 73
		private INetworkFacade networkFacade;

		// Token: 0x0400004A RID: 74
		private int currentCommandIndex;

		// Token: 0x0400004B RID: 75
		private ImapMailFlags flagsToRemove;

		// Token: 0x0400004C RID: 76
		private ImapRootPathProcessingFlags imapRootPathProcessingFlags;

		// Token: 0x0400004D RID: 77
		private string rootFolderPath;

		// Token: 0x0400004E RID: 78
		private ExDateTime timeSent;
	}
}
