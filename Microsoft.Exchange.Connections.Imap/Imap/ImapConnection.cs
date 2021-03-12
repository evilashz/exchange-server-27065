using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ImapConnection : DisposeTrackableBase, IConnection<ImapConnection>
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000030EF File Offset: 0x000012EF
		private ImapConnection(ConnectionParameters connectionParameters)
		{
			this.connectionParameters = connectionParameters;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003127 File Offset: 0x00001327
		internal static IList<string> MessageInfoDataItemsForChangesOnly
		{
			get
			{
				return ImapConnection.messageInfoDataItemsForChangesOnly;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000312E File Offset: 0x0000132E
		internal static IList<string> MessageInfoDataItemsForNewMessages
		{
			get
			{
				return ImapConnection.messageInfoDataItemsForNewMessages;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003135 File Offset: 0x00001335
		internal static IList<string> MessageBodyDataItems
		{
			get
			{
				return ImapConnection.messageBodyDataItems;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000313C File Offset: 0x0000133C
		internal ImapConnectionContext ConnectionContext
		{
			get
			{
				base.CheckDisposed();
				return this.connectionContext;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000314A File Offset: 0x0000134A
		private ConnectionParameters ConnectionParameters
		{
			get
			{
				base.CheckDisposed();
				return this.connectionParameters;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003158 File Offset: 0x00001358
		public static ImapConnection CreateInstance(ConnectionParameters connectionParameters)
		{
			return new ImapConnection(connectionParameters).Initialize();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003165 File Offset: 0x00001365
		public ImapConnection Initialize()
		{
			this.connectionContext = new ImapConnectionContext(this.ConnectionParameters, null);
			return this;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000317C File Offset: 0x0000137C
		public void ConnectAndAuthenticate(ImapServerParameters serverParameters, ImapAuthenticationParameters authenticationParameters, IServerCapabilities capabilities = null)
		{
			base.CheckDisposed();
			this.ThrowIfConnected();
			ImapConnectionContext imapConnectionContext = this.ConnectionContext;
			imapConnectionContext.AuthenticationParameters = authenticationParameters;
			imapConnectionContext.ServerParameters = serverParameters;
			imapConnectionContext.NetworkFacade = this.CreateNetworkFacade.Value(this.ConnectionContext, serverParameters);
			AsyncOperationResult<DBNull> asyncOperationResult = ImapConnectionCore.ConnectAndAuthenticate(this.ConnectionContext, capabilities, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000031E4 File Offset: 0x000013E4
		public OperationStatusCode TestLogon(ImapServerParameters serverParameters, ImapAuthenticationParameters authenticationParameters, IServerCapabilities capabilities)
		{
			base.CheckDisposed();
			this.ThrowIfConnected();
			OperationStatusCode result;
			try
			{
				this.ConnectAndAuthenticate(serverParameters, authenticationParameters, capabilities);
				result = OperationStatusCode.Success;
			}
			catch (ImapConnectionException)
			{
				result = OperationStatusCode.ErrorCannotCommunicateWithRemoteServer;
			}
			catch (ImapAuthenticationException ex)
			{
				if (ex.InnerException == null)
				{
					result = OperationStatusCode.ErrorInvalidCredentials;
				}
				else
				{
					result = OperationStatusCode.ErrorInvalidRemoteServer;
				}
			}
			catch (ImapCommunicationException)
			{
				result = OperationStatusCode.ErrorInvalidRemoteServer;
			}
			catch (MissingCapabilitiesException)
			{
				result = OperationStatusCode.ErrorUnsupportedProtocolVersion;
			}
			finally
			{
				if (this.IsConnected())
				{
					ImapConnectionCore.LogOff(this.ConnectionContext, null, null);
				}
			}
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003284 File Offset: 0x00001484
		public ImapServerCapabilities GetServerCapabilities()
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<ImapServerCapabilities> asyncOperationResult = ImapConnectionCore.Capabilities(this.ConnectionContext, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
			return asyncOperationResult.Data;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000032C0 File Offset: 0x000014C0
		public void Expunge()
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<DBNull> asyncOperationResult = ImapConnectionCore.Expunge(this.ConnectionContext, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000032F4 File Offset: 0x000014F4
		public ImapMailbox SelectImapMailbox(ImapMailbox imapMailbox)
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<ImapMailbox> asyncOperationResult = ImapConnectionCore.SelectImapMailbox(this.ConnectionContext, imapMailbox, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
			return asyncOperationResult.Data;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003330 File Offset: 0x00001530
		public ImapResultData GetMessageInfoByRange(string start, string end, bool uidFetch, IList<string> messageDataItems)
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<ImapResultData> messageInfoByRange = ImapConnectionCore.GetMessageInfoByRange(this.ConnectionContext, start, end, uidFetch, messageDataItems, null, null);
			this.ThrowIfExceptionNotNull(messageInfoByRange.Exception);
			return messageInfoByRange.Data;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003370 File Offset: 0x00001570
		public ImapResultData GetMessageItemByUid(string uid, IList<string> messageBodyDataItems)
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<ImapResultData> messageItemByUid = ImapConnectionCore.GetMessageItemByUid(this.ConnectionContext, uid, messageBodyDataItems, null, null);
			this.ThrowIfExceptionNotNull(messageItemByUid.Exception);
			return messageItemByUid.Data;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000033AC File Offset: 0x000015AC
		public string AppendMessageToImapMailbox(string mailboxName, ImapMailFlags messageFlags, Stream messageMimeStream)
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<string> asyncOperationResult = ImapConnectionCore.AppendMessageToImapMailbox(this.ConnectionContext, mailboxName, messageFlags, messageMimeStream, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
			return asyncOperationResult.Data;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000033E8 File Offset: 0x000015E8
		public IList<string> SearchForMessageByMessageId(string messageId)
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<IList<string>> asyncOperationResult = ImapConnectionCore.SearchForMessageByMessageId(this.ConnectionContext, messageId, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
			return asyncOperationResult.Data;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003424 File Offset: 0x00001624
		public void StoreMessageFlags(string uid, ImapMailFlags flagsToStore, ImapMailFlags previousFlags)
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<DBNull> asyncOperationResult = ImapConnectionCore.StoreMessageFlags(this.ConnectionContext, uid, flagsToStore, previousFlags, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000345C File Offset: 0x0000165C
		public void CreateImapMailbox(string mailboxName)
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<DBNull> asyncOperationResult = ImapConnectionCore.CreateImapMailbox(this.ConnectionContext, mailboxName, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003490 File Offset: 0x00001690
		public void DeleteImapMailbox(string mailboxName)
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<DBNull> asyncOperationResult = ImapConnectionCore.DeleteImapMailbox(this.ConnectionContext, mailboxName, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000034C4 File Offset: 0x000016C4
		public void RenameImapMailbox(string oldMailboxName, string newMailboxName)
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<DBNull> asyncOperationResult = ImapConnectionCore.RenameImapMailbox(this.ConnectionContext, oldMailboxName, newMailboxName, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000034FC File Offset: 0x000016FC
		public IList<ImapMailbox> ListImapMailboxesByLevel(int level, char separator)
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<IList<ImapMailbox>> asyncOperationResult = ImapConnectionCore.ListImapMailboxesByLevel(this.ConnectionContext, level, separator, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
			return asyncOperationResult.Data;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003538 File Offset: 0x00001738
		public void LogOff()
		{
			base.CheckDisposed();
			this.ThrowIfNotConnected();
			AsyncOperationResult<DBNull> asyncOperationResult = ImapConnectionCore.LogOff(this.ConnectionContext, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000356C File Offset: 0x0000176C
		public bool IsConnected()
		{
			base.CheckDisposed();
			ImapConnectionContext imapConnectionContext = this.ConnectionContext;
			return imapConnectionContext != null && imapConnectionContext.NetworkFacade != null && imapConnectionContext.NetworkFacade.IsConnected;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000359E File Offset: 0x0000179E
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.ConnectionContext != null)
			{
				this.ConnectionContext.Dispose();
				this.connectionContext = null;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000035BD File Offset: 0x000017BD
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ImapConnection>(this);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000035C5 File Offset: 0x000017C5
		private void ThrowIfConnected()
		{
			if (this.IsConnected())
			{
				throw new ConnectionAlreadyOpenException();
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000035D5 File Offset: 0x000017D5
		private void ThrowIfNotConnected()
		{
			if (!this.IsConnected())
			{
				throw new ConnectionClosedException();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000035E8 File Offset: 0x000017E8
		private void ThrowIfExceptionNotNull(Exception exceptionOrNull)
		{
			if (exceptionOrNull == null)
			{
				return;
			}
			if (exceptionOrNull is LocalizedException)
			{
				throw exceptionOrNull;
			}
			string fullName = exceptionOrNull.GetType().FullName;
			throw new UnhandledException(fullName, exceptionOrNull);
		}

		// Token: 0x0400003D RID: 61
		internal readonly Hookable<Func<ImapConnectionContext, ImapServerParameters, INetworkFacade>> CreateNetworkFacade = Hookable<Func<ImapConnectionContext, ImapServerParameters, INetworkFacade>>.Create(true, (ImapConnectionContext connectionContext, ImapServerParameters serverParams) => new ImapNetworkFacade(connectionContext.ConnectionParameters, serverParams));

		// Token: 0x0400003E RID: 62
		private static readonly IList<string> messageInfoDataItemsForChangesOnly = new List<string>(new string[]
		{
			"UID",
			"FLAGS"
		}).AsReadOnly();

		// Token: 0x0400003F RID: 63
		private static readonly IList<string> messageInfoDataItemsForUidValidityRecovery = new List<string>(new string[]
		{
			"UID",
			"BODY.PEEK[HEADER.FIELDS (Message-ID)]"
		}).AsReadOnly();

		// Token: 0x04000040 RID: 64
		private static readonly IList<string> messageInfoDataItemsForNewMessages = new List<string>(new string[]
		{
			"UID",
			"FLAGS",
			"BODY.PEEK[HEADER.FIELDS (Message-ID)]",
			"RFC822.SIZE"
		}).AsReadOnly();

		// Token: 0x04000041 RID: 65
		private static readonly IList<string> messageBodyDataItems = new List<string>(new string[]
		{
			"UID",
			"INTERNALDATE",
			"BODY.PEEK[]"
		}).AsReadOnly();

		// Token: 0x04000042 RID: 66
		private ImapConnectionContext connectionContext;

		// Token: 0x04000043 RID: 67
		private ConnectionParameters connectionParameters;
	}
}
