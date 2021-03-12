using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client
{
	// Token: 0x020001CD RID: 461
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class IMAPClient
	{
		// Token: 0x06000D78 RID: 3448 RVA: 0x00020870 File Offset: 0x0001EA70
		internal static IAsyncResult BeginConnectAndAuthenticate(IMAPClientState clientState, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogDebugging((TSLID)813UL, IMAPClient.Tracer, "Beginning connect-and-authenticate process", new object[0]);
			AsyncResult<IMAPClientState, DBNull> asyncResult = new AsyncResult<IMAPClientState, DBNull>(clientState, clientState, callback, callbackState, syncPoisonContext);
			AsyncCallback callback2;
			switch (clientState.IMAPSecurityMechanism)
			{
			case IMAPSecurityMechanism.None:
				callback2 = new AsyncCallback(IMAPClient.OnEndConnectToAuthenticate);
				break;
			case IMAPSecurityMechanism.Ssl:
				callback2 = new AsyncCallback(IMAPClient.OnEndConnectToAuthenticate);
				break;
			case IMAPSecurityMechanism.Tls:
				callback2 = new AsyncCallback(IMAPClient.OnEndConnectToStarttls);
				break;
			default:
				throw new InvalidOperationException("Unexpected security mechanism " + clientState.IMAPSecurityMechanism);
			}
			asyncResult.PendingAsyncResult = clientState.CommClient.BeginConnect(clientState, callback2, asyncResult, syncPoisonContext);
			return asyncResult;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00020928 File Offset: 0x0001EB28
		internal static AsyncOperationResult<DBNull> EndConnectAndAuthenticate(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00020944 File Offset: 0x0001EB44
		internal static IAsyncResult BeginCapabilities(IMAPClientState clientState, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogRawData((TSLID)814UL, IMAPClient.Tracer, "Getting server capabilities", new object[0]);
			clientState.CachedCommand.ResetAsCapability(clientState.UniqueCommandId);
			return IMAPClient.CreateAsyncResultAndBeginCommand<IList<string>>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndCapabilityInternal), callback, callbackState, syncPoisonContext);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000209A4 File Offset: 0x0001EBA4
		internal static AsyncOperationResult<IList<string>> EndCapabilities(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, IList<string>> asyncResult2 = (AsyncResult<IMAPClientState, IList<string>>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000209C0 File Offset: 0x0001EBC0
		internal static IAsyncResult BeginExpunge(IMAPClientState clientState, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogRawData((TSLID)815UL, IMAPClient.Tracer, "Expunging messages.", new object[0]);
			clientState.CachedCommand.ResetAsExpunge(clientState.UniqueCommandId);
			return IMAPClient.CreateAsyncResultAndBeginCommand<DBNull>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndExpungeInternal), callback, callbackState, syncPoisonContext);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00020A20 File Offset: 0x0001EC20
		internal static AsyncOperationResult<DBNull> EndExpunge(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00020A3C File Offset: 0x0001EC3C
		internal static IAsyncResult BeginSelectImapMailbox(IMAPClientState clientState, IMAPMailbox imapMailbox, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogRawData((TSLID)816UL, IMAPClient.Tracer, "Selecting mailbox {0}", new object[]
			{
				imapMailbox.Name
			});
			clientState.CachedCommand.ResetAsSelect(clientState.UniqueCommandId, imapMailbox);
			return IMAPClient.CreateAsyncResultAndBeginCommand<IMAPMailbox>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndSelectImapMailboxCallStatusIfNeeded), callback, callbackState, syncPoisonContext);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00020AAC File Offset: 0x0001ECAC
		internal static AsyncOperationResult<IMAPMailbox> EndSelectImapMailbox(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, IMAPMailbox> asyncResult2 = (AsyncResult<IMAPClientState, IMAPMailbox>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		internal static IAsyncResult BeginGetMessageInfoByRange(IMAPClientState clientState, string start, string end, bool uidFetch, IList<string> messageDataItems, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			AsyncResult<IMAPClientState, IMAPResultData> asyncResult = new AsyncResult<IMAPClientState, IMAPResultData>(clientState, clientState, callback, callbackState, syncPoisonContext);
			clientState.CachedCommand.ResetAsFetch(clientState.UniqueCommandId, start, end, uidFetch, messageDataItems);
			clientState.Log.LogRawData((TSLID)817UL, IMAPClient.Tracer, "IMAP Fetch Message Headers in range.  Start={0}.  End={1}", new object[]
			{
				start,
				end
			});
			asyncResult.PendingAsyncResult = clientState.CommClient.BeginCommand(clientState.CachedCommand, clientState, new AsyncCallback(IMAPClient.OnEndGetMessageInfoByRangeInternal), asyncResult, syncPoisonContext);
			return asyncResult;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00020B54 File Offset: 0x0001ED54
		internal static AsyncOperationResult<IMAPResultData> EndGetMessageInfoByRange(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00020B70 File Offset: 0x0001ED70
		internal static IAsyncResult BeginGetMessageItemByUid(IMAPClientState clientState, string uid, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogRawData((TSLID)818UL, IMAPClient.Tracer, "IMAP Fetch Message Body.  Uid={0}.", new object[]
			{
				uid
			});
			clientState.CachedCommand.ResetAsFetch(clientState.UniqueCommandId, uid, null, true, IMAPClient.messageBodyDataItems);
			return IMAPClient.CreateAsyncResultAndBeginCommand<IMAPResultData>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndGetMessageBodyInternal), callback, callbackState, syncPoisonContext);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00020BE0 File Offset: 0x0001EDE0
		internal static AsyncOperationResult<IMAPResultData> EndGetMessageItemByUid(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00020BFC File Offset: 0x0001EDFC
		internal static IAsyncResult BeginAppendMessageToIMAPMailbox(IMAPClientState clientState, string mailboxName, IMAPMailFlags messageFlags, Stream messageMimeStream, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogDebugging((TSLID)819UL, IMAPClient.Tracer, "Appending message to mailbox {0}", new object[]
			{
				mailboxName
			});
			clientState.CachedCommand.ResetAsAppend(clientState.UniqueCommandId, mailboxName, messageFlags, messageMimeStream);
			return IMAPClient.CreateAsyncResultAndBeginCommand<string>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndAppendMessageInternal), callback, callbackState, syncPoisonContext);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00020C68 File Offset: 0x0001EE68
		internal static AsyncOperationResult<string> EndAppendMessageToIMAPMailbox(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, string> asyncResult2 = (AsyncResult<IMAPClientState, string>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00020C84 File Offset: 0x0001EE84
		internal static IAsyncResult BeginSearchForMessageByMessageId(IMAPClientState clientState, string messageId, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogRawData((TSLID)820UL, IMAPClient.Tracer, "Searching for message by ID.  ID = {0}", new object[]
			{
				messageId
			});
			clientState.CachedCommand.ResetAsSearch(clientState.UniqueCommandId, new string[]
			{
				"HEADER Message-Id",
				messageId
			});
			return IMAPClient.CreateAsyncResultAndBeginCommand<IList<string>>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndSearchForMessageInternal), callback, callbackState, syncPoisonContext);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00020D00 File Offset: 0x0001EF00
		internal static AsyncOperationResult<IList<string>> EndSearchForMessageByMessageId(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, IList<string>> asyncResult2 = (AsyncResult<IMAPClientState, IList<string>>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00020D1C File Offset: 0x0001EF1C
		internal static IAsyncResult BeginStoreMessageFlags(IMAPClientState clientState, string uid, IMAPMailFlags flagsToStore, IMAPMailFlags previousFlags, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogRawData((TSLID)821UL, IMAPClient.Tracer, "Storing flags for message.  UID = {0}", new object[]
			{
				uid
			});
			IMAPMailFlags imapmailFlags = flagsToStore & ~previousFlags;
			IMAPMailFlags imapmailFlags2 = ~flagsToStore & previousFlags;
			if (imapmailFlags != IMAPMailFlags.None)
			{
				clientState.CachedCommand.ResetAsUidStore(clientState.UniqueCommandId, uid, imapmailFlags, true);
				clientState.FlagsToRemove = imapmailFlags2;
				return IMAPClient.CreateAsyncResultAndBeginCommand<DBNull>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndStoreMessageFlagsInternal), callback, callbackState, syncPoisonContext);
			}
			if (imapmailFlags2 != IMAPMailFlags.None)
			{
				clientState.CachedCommand.ResetAsUidStore(clientState.UniqueCommandId, uid, imapmailFlags2, false);
				clientState.FlagsToRemove = IMAPMailFlags.None;
				return IMAPClient.CreateAsyncResultAndBeginCommand<DBNull>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndStoreMessageFlagsInternal), callback, callbackState, syncPoisonContext);
			}
			clientState.Log.LogError((TSLID)822UL, IMAPClient.Tracer, "Attempt to store the same flags that were already set.", new object[0]);
			return null;
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00020E04 File Offset: 0x0001F004
		internal static AsyncOperationResult<DBNull> EndStoreMessageFlags(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00020E20 File Offset: 0x0001F020
		internal static IAsyncResult BeginCreateImapMailbox(IMAPClientState clientState, string mailboxName, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogRawData((TSLID)823UL, IMAPClient.Tracer, "Creating new IMAP Mailbox.  Name = {0}", new object[]
			{
				mailboxName
			});
			clientState.CachedCommand.ResetAsCreate(clientState.UniqueCommandId, mailboxName);
			return IMAPClient.CreateAsyncResultAndBeginCommand<DBNull>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndCreateImapMailboxInternal), callback, callbackState, syncPoisonContext);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00020E88 File Offset: 0x0001F088
		internal static AsyncOperationResult<DBNull> EndCreateImapMailbox(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00020EA4 File Offset: 0x0001F0A4
		internal static IAsyncResult BeginDeleteImapMailbox(IMAPClientState clientState, string mailboxName, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogRawData((TSLID)824UL, IMAPClient.Tracer, "Deleting IMAP Mailbox.  Name = {0}", new object[]
			{
				mailboxName
			});
			clientState.CachedCommand.ResetAsDelete(clientState.UniqueCommandId, mailboxName);
			return IMAPClient.CreateAsyncResultAndBeginCommand<DBNull>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndDeleteImapMailboxInternal), callback, callbackState, syncPoisonContext);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00020F0C File Offset: 0x0001F10C
		internal static AsyncOperationResult<DBNull> EndDeleteImapMailbox(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00020F28 File Offset: 0x0001F128
		internal static IAsyncResult BeginRenameImapMailbox(IMAPClientState clientState, string oldMailboxName, string newMailboxName, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogRawData((TSLID)825UL, IMAPClient.Tracer, "Renaming IMAP Mailbox.  {0} => {1}", new object[]
			{
				oldMailboxName,
				newMailboxName
			});
			clientState.CachedCommand.ResetAsRename(clientState.UniqueCommandId, oldMailboxName, newMailboxName);
			return IMAPClient.CreateAsyncResultAndBeginCommand<DBNull>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndRenameImapMailboxInternal), callback, callbackState, syncPoisonContext);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00020F98 File Offset: 0x0001F198
		internal static AsyncOperationResult<DBNull> EndRenameImapMailbox(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00020FB4 File Offset: 0x0001F1B4
		internal static IAsyncResult BeginListImapMailboxesByLevel(IMAPClientState clientState, int level, char separator, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			clientState.Log.LogRawData((TSLID)826UL, IMAPClient.Tracer, "Listing mailboxes at level {0}", new object[]
			{
				level
			});
			clientState.InitializeRootPathProcessingFlags(level, separator);
			clientState.CachedCommand.ResetAsList(clientState.UniqueCommandId, separator, new int?(level), clientState.RootFolderPath);
			return IMAPClient.CreateAsyncResultAndBeginCommand<IList<IMAPMailbox>>(clientState, clientState.CachedCommand, true, new AsyncCallback(IMAPClient.OnEndListImapMailboxesInternal), callback, callbackState, syncPoisonContext);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00021038 File Offset: 0x0001F238
		internal static AsyncOperationResult<IList<IMAPMailbox>> EndListImapMailboxes(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, IList<IMAPMailbox>> asyncResult2 = (AsyncResult<IMAPClientState, IList<IMAPMailbox>>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00021054 File Offset: 0x0001F254
		internal static IAsyncResult BeginLogOff(IMAPClientState clientState, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult = new AsyncResult<IMAPClientState, DBNull>(clientState, clientState, callback, callbackState, syncPoisonContext);
			clientState.Log.LogRawData((TSLID)827UL, "Logging out", new object[0]);
			clientState.CachedCommand.ResetAsLogout(clientState.UniqueCommandId);
			try
			{
				asyncResult.PendingAsyncResult = clientState.CommClient.BeginCommand(clientState.CachedCommand, false, clientState, new AsyncCallback(IMAPClient.OnEndLogOffInternal), asyncResult, syncPoisonContext);
			}
			catch (InvalidOperationException ex)
			{
				string message = "BUG: BeginLogOff : should never throw InvalidOperationException.";
				clientState.Log.LogError((TSLID)828UL, IMAPClient.Tracer, "Caught InvalidOperationException while logging off.  Ignoring.  Exception = {0}", new object[]
				{
					ex
				});
				clientState.Log.ReportWatson(message, ex);
			}
			asyncResult.SetCompletedSynchronously();
			asyncResult.ProcessCompleted(DBNull.Value);
			return asyncResult;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0002112C File Offset: 0x0001F32C
		internal static AsyncOperationResult<DBNull> EndLogOff(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00021146 File Offset: 0x0001F346
		internal static void Cancel(IMAPClientState clientState)
		{
			clientState.CommClient.Cancel();
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00021154 File Offset: 0x0001F354
		private static void OnEndConnectToStarttls(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult.AsyncState;
			IMAPClientState state = asyncResult2.State;
			AsyncOperationResult<IMAPResultData> asyncOperationResult = state.CommClient.EndConnect(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				IMAPUtils.LogExceptionDetails(state.Log, IMAPClient.Tracer, "Connecting", asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(null, asyncOperationResult.Exception);
				return;
			}
			state.CachedCommand.ResetAsStarttls(state.UniqueCommandId);
			asyncResult2.PendingAsyncResult = state.CommClient.BeginCommand(state.CachedCommand, state, new AsyncCallback(IMAPClient.OnEndStarttlsToBeginTlsNegotiation), asyncResult2, asyncResult2.SyncPoisonContext);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x000211F0 File Offset: 0x0001F3F0
		private static void OnEndStarttlsToBeginTlsNegotiation(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult.AsyncState;
			IMAPClientState state = asyncResult2.State;
			AsyncOperationResult<IMAPResultData> asyncOperationResult = state.CommClient.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				IMAPUtils.LogExceptionDetails(state.Log, IMAPClient.Tracer, state.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(null, asyncOperationResult.Exception);
				return;
			}
			IMAPResultData data = asyncOperationResult.Data;
			if (data.Status != IMAPStatus.Ok)
			{
				data.FailureException = IMAPClient.BuildFailureException(state.CachedCommand, data.Status);
				IMAPUtils.LogExceptionDetails(state.Log, IMAPClient.Tracer, state.CachedCommand, data.FailureException);
				asyncResult2.ProcessCompleted(null, data.FailureException);
				return;
			}
			asyncResult2.PendingAsyncResult = state.CommClient.BeginNegotiateTlsAsClient(state, new AsyncCallback(IMAPClient.OnEndConnectToAuthenticate), asyncResult2, asyncResult2.SyncPoisonContext);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x000212C8 File Offset: 0x0001F4C8
		private static void OnEndConnectToAuthenticate(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult.AsyncState;
			IMAPClientState state = asyncResult2.State;
			switch (state.IMAPSecurityMechanism)
			{
			case IMAPSecurityMechanism.None:
			case IMAPSecurityMechanism.Ssl:
			{
				AsyncOperationResult<IMAPResultData> asyncOperationResult = state.CommClient.EndConnect(asyncResult);
				if (!asyncOperationResult.IsSucceeded)
				{
					IMAPUtils.LogExceptionDetails(state.Log, IMAPClient.Tracer, "Connecting", asyncOperationResult.Exception);
					asyncResult2.ProcessCompleted(null, asyncOperationResult.Exception);
					return;
				}
				break;
			}
			case IMAPSecurityMechanism.Tls:
			{
				AsyncOperationResult<IMAPResultData> asyncOperationResult2 = state.CommClient.EndNegotiateTlsAsClient(asyncResult);
				if (!asyncOperationResult2.IsSucceeded)
				{
					IMAPUtils.LogExceptionDetails(state.Log, IMAPClient.Tracer, "Tls negotiation", asyncOperationResult2.Exception);
					asyncResult2.ProcessCompleted(null, asyncOperationResult2.Exception);
					return;
				}
				break;
			}
			default:
				throw new InvalidOperationException("Unexpected security mechanism " + state.IMAPSecurityMechanism);
			}
			IMAPAuthenticationMechanism imapauthenticationMechanism = state.IMAPAuthenticationMechanism;
			if (imapauthenticationMechanism == IMAPAuthenticationMechanism.Basic)
			{
				state.CachedCommand.ResetAsLogin(state.UniqueCommandId, state.LogonName, state.LogonPassword);
				asyncResult2.PendingAsyncResult = state.CommClient.BeginCommand(state.CachedCommand, state, new AsyncCallback(IMAPClient.OnEndLoginFallbackToAuthenticatePlainIfNeeded), asyncResult2, asyncResult2.SyncPoisonContext);
				return;
			}
			if (imapauthenticationMechanism != IMAPAuthenticationMechanism.Ntlm)
			{
				throw new InvalidOperationException("Unexpected authentication mechanism" + state.IMAPAuthenticationMechanism);
			}
			AuthenticationContext authContext = null;
			state.CachedCommand.ResetAsAuthenticate(state.UniqueCommandId, IMAPAuthenticationMechanism.Ntlm, state.LogonName, state.LogonPassword, authContext);
			asyncResult2.PendingAsyncResult = state.CommClient.BeginCommand(state.CachedCommand, state, new AsyncCallback(IMAPClient.OnEndCompleteConnectAndAuthenticate), asyncResult2, asyncResult2.SyncPoisonContext);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00021470 File Offset: 0x0001F670
		private static void OnEndCompleteConnectAndAuthenticate(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult.AsyncState;
			AsyncOperationResult<IMAPResultData> asyncOperationResult = asyncResult2.State.CommClient.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				IMAPUtils.LogExceptionDetails(asyncResult2.State.Log, IMAPClient.Tracer, asyncResult2.State.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(asyncOperationResult.Exception);
				return;
			}
			IMAPResultData data = asyncOperationResult.Data;
			Exception ex = null;
			if (data.Status == IMAPStatus.No)
			{
				ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.AuthenticationError, new IMAPAuthenticationException(), true);
			}
			else if (data.Status == IMAPStatus.Bad)
			{
				ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.AuthenticationError, IMAPClient.BuildBadResponseInnerException(asyncResult2.State.CachedCommand), true);
			}
			else if (data.Status != IMAPStatus.Ok)
			{
				ex = IMAPClient.BuildFailureException(asyncResult2.State.CachedCommand, data.Status);
			}
			if (ex == null)
			{
				asyncResult2.State.Log.LogRawData((TSLID)829UL, "{0}: Command completed successfully.", new object[]
				{
					asyncResult2.State.CachedCommand.ToPiiCleanString()
				});
			}
			else
			{
				IMAPUtils.LogExceptionDetails(asyncResult2.State.Log, IMAPClient.Tracer, asyncResult2.State.CachedCommand, ex);
			}
			asyncResult2.ProcessCompleted(DBNull.Value, ex);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x000215AC File Offset: 0x0001F7AC
		private static void OnEndLoginFallbackToAuthenticatePlainIfNeeded(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult.AsyncState;
			IMAPClientState state = asyncResult2.State;
			AsyncOperationResult<IMAPResultData> asyncOperationResult = state.CommClient.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				IMAPUtils.LogExceptionDetails(state.Log, IMAPClient.Tracer, state.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(asyncOperationResult.Exception);
				return;
			}
			IMAPResultData data = asyncOperationResult.Data;
			Exception ex = null;
			if (data.Status == IMAPStatus.No || data.Status == IMAPStatus.Bad)
			{
				AuthenticationContext authContext = null;
				state.CachedCommand.ResetAsAuthenticate(state.UniqueCommandId, IMAPAuthenticationMechanism.Basic, state.LogonName, state.LogonPassword, authContext);
				asyncResult2.PendingAsyncResult = state.CommClient.BeginCommand(state.CachedCommand, state, new AsyncCallback(IMAPClient.OnEndCompleteConnectAndAuthenticate), asyncResult2, asyncResult2.SyncPoisonContext);
				return;
			}
			if (data.Status != IMAPStatus.Ok)
			{
				ex = IMAPClient.BuildFailureException(state.CachedCommand, data.Status);
			}
			if (ex == null)
			{
				state.Log.LogRawData((TSLID)1240UL, "{0}: Command completed successfully.", new object[]
				{
					state.CachedCommand.ToPiiCleanString()
				});
			}
			else
			{
				IMAPUtils.LogExceptionDetails(state.Log, IMAPClient.Tracer, state.CachedCommand, ex);
			}
			asyncResult2.ProcessCompleted(DBNull.Value, ex);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00021728 File Offset: 0x0001F928
		private static void OnEndCapabilityInternal(IAsyncResult asyncResult)
		{
			IMAPClient.ProcessResultAndCompleteRequest<IList<string>>(asyncResult, delegate(IMAPResultData resultData, IMAPClientState clientState)
			{
				if (resultData.IsParseSuccessful)
				{
					clientState.Log.LogRawData((TSLID)830UL, IMAPClient.Tracer, "Found capabilities from server", new object[0]);
					return resultData.Capabilities;
				}
				return null;
			});
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00021754 File Offset: 0x0001F954
		private static void OnEndExpungeInternal(IAsyncResult asyncResult)
		{
			IMAPClient.ProcessResultAndCompleteRequest<DBNull>(asyncResult, (IMAPResultData resultData, IMAPClientState clientState) => DBNull.Value);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0002177C File Offset: 0x0001F97C
		private static void OnEndSelectImapMailboxCallStatusIfNeeded(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, IMAPMailbox> asyncResult2 = (AsyncResult<IMAPClientState, IMAPMailbox>)asyncResult.AsyncState;
			IMAPClientState state = asyncResult2.State;
			Exception ex = null;
			IMAPMailbox imapmailbox = null;
			AsyncOperationResult<IMAPResultData> asyncOperationResult = state.CommClient.EndCommand(asyncResult);
			if (asyncOperationResult.IsSucceeded)
			{
				IMAPResultData data = asyncOperationResult.Data;
				if (data.IsParseSuccessful && data.Mailboxes.Count == 1)
				{
					if (data.Mailboxes[0].NumberOfMessages == null)
					{
						ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, new IMAPException("Mailbox is missing EXISTS data."), true);
					}
					else
					{
						imapmailbox = data.Mailboxes[0];
						if (data.Mailboxes[0].UidNext == null)
						{
							state.CachedCommand.ResetAsStatus(state.UniqueCommandId, imapmailbox);
							asyncResult2.PendingAsyncResult = state.CommClient.BeginCommand(state.CachedCommand, state, new AsyncCallback(IMAPClient.OnEndSelectFollowedByStatus), asyncResult2, asyncResult2.SyncPoisonContext);
							return;
						}
						state.Log.LogRawData((TSLID)831UL, IMAPClient.Tracer, "Selected mailbox {0}", new object[]
						{
							imapmailbox.Name
						});
					}
				}
				else
				{
					ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, new IMAPException("Failed to select mailbox"), true);
				}
			}
			else
			{
				ex = asyncOperationResult.Exception;
			}
			if (ex == null)
			{
				state.Log.LogRawData((TSLID)1160UL, "{0}: Command completed successfully.", new object[]
				{
					state.CachedCommand.ToPiiCleanString()
				});
			}
			else
			{
				IMAPUtils.LogExceptionDetails(state.Log, IMAPClient.Tracer, state.CachedCommand, ex);
			}
			asyncResult2.ProcessCompleted(imapmailbox, ex);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000219F5 File Offset: 0x0001FBF5
		private static void OnEndSelectFollowedByStatus(IAsyncResult asyncResult)
		{
			IMAPClient.ProcessResultAndCompleteRequest<IMAPMailbox>(asyncResult, delegate(IMAPResultData resultData, IMAPClientState clientState)
			{
				if (resultData.IsParseSuccessful && resultData.Mailboxes.Count == 1 && resultData.Mailboxes[0].UidNext != null)
				{
					clientState.Log.LogRawData((TSLID)1161UL, IMAPClient.Tracer, "Selected mailbox {0} after succesful STATUS command", new object[]
					{
						resultData.Mailboxes[0].Name
					});
					return resultData.Mailboxes[0];
				}
				resultData.FailureException = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, new IMAPException(string.Format("Failed to get STATUS for mailbox. {0}", resultData.IsParseSuccessful ? ((resultData.Mailboxes.Count == 1) ? "Missing UIDNEXT" : "No mailbox returned") : " Parsing unsuccessful.")), true);
				return null;
			});
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00021A1C File Offset: 0x0001FC1C
		private static void OnEndGetMessageInfoByRangeInternal(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult.AsyncState;
			AsyncOperationResult<IMAPResultData> asyncOperationResult = asyncResult2.State.CommClient.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				IMAPUtils.LogExceptionDetails(asyncResult2.State.Log, IMAPClient.Tracer, asyncResult2.State.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(null, asyncOperationResult.Exception);
				return;
			}
			IMAPResultData data = asyncOperationResult.Data;
			if (data.Status != IMAPStatus.Ok)
			{
				data.FailureException = IMAPClient.BuildFailureException(asyncResult2.State.CachedCommand, data.Status);
				IMAPUtils.LogExceptionDetails(asyncResult2.State.Log, IMAPClient.Tracer, asyncResult2.State.CachedCommand, data.FailureException);
				asyncResult2.ProcessCompleted(null, data.FailureException);
				return;
			}
			if (!data.IsParseSuccessful)
			{
				IMAPUtils.LogExceptionDetails(asyncResult2.State.Log, IMAPClient.Tracer, asyncResult2.State.CachedCommand, data.FailureException);
			}
			asyncResult2.ProcessCompleted(data, data.FailureException);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00021BAA File Offset: 0x0001FDAA
		private static void OnEndGetMessageBodyInternal(IAsyncResult asyncResult)
		{
			IMAPClient.ProcessResultAndCompleteRequest<IMAPResultData>(asyncResult, delegate(IMAPResultData resultData, IMAPClientState clientState)
			{
				if (resultData.IsParseSuccessful)
				{
					clientState.Log.LogRawData((TSLID)832UL, IMAPClient.Tracer, "Successful UID FETCH of message body", new object[0]);
					IList<string> messageUids = resultData.MessageUids;
					if (messageUids.Count != 1)
					{
						clientState.Log.LogRawData((TSLID)833UL, IMAPClient.Tracer, "Unexpected number of UIDs returned during single message fetch: {0}", new object[]
						{
							messageUids.Count
						});
					}
				}
				clientState.ActivatePerfMsgDownloadEvent(clientState, null);
				return resultData;
			});
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00021BFD File Offset: 0x0001FDFD
		private static void OnEndAppendMessageInternal(IAsyncResult asyncResult)
		{
			IMAPClient.ProcessResultAndCompleteRequest<string>(asyncResult, delegate(IMAPResultData resultData, IMAPClientState clientState)
			{
				clientState.ActivatePerfMsgUploadEvent(clientState, null);
				if (resultData.MessageUids != null && resultData.MessageUids.Count > 0)
				{
					return resultData.MessageUids[0];
				}
				return null;
			});
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00021C34 File Offset: 0x0001FE34
		private static void OnEndSearchForMessageInternal(IAsyncResult asyncResult)
		{
			IMAPClient.ProcessResultAndCompleteRequest<IList<string>>(asyncResult, delegate(IMAPResultData resultData, IMAPClientState state)
			{
				if (resultData.IsParseSuccessful)
				{
					return resultData.MessageUids;
				}
				return null;
			});
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00021C60 File Offset: 0x0001FE60
		private static void OnEndStoreMessageFlagsInternal(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, DBNull> asyncResult2 = (AsyncResult<IMAPClientState, DBNull>)asyncResult.AsyncState;
			IMAPClientState state = asyncResult2.State;
			if (state.FlagsToRemove == IMAPMailFlags.None)
			{
				IMAPClient.ProcessResultAndCompleteRequest<DBNull>(asyncResult, (IMAPResultData resultData, IMAPClientState clientState) => DBNull.Value);
				return;
			}
			IMAPMailFlags flagsToRemove = state.FlagsToRemove;
			state.FlagsToRemove = IMAPMailFlags.None;
			AsyncOperationResult<IMAPResultData> asyncOperationResult = asyncResult2.State.CommClient.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				IMAPUtils.LogExceptionDetails(asyncResult2.State.Log, IMAPClient.Tracer, asyncResult2.State.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(DBNull.Value, asyncOperationResult.Exception);
				return;
			}
			IMAPResultData data = asyncOperationResult.Data;
			if (data.Status != IMAPStatus.Ok)
			{
				data.FailureException = IMAPClient.BuildFailureException(asyncResult2.State.CachedCommand, data.Status);
				IMAPUtils.LogExceptionDetails(asyncResult2.State.Log, IMAPClient.Tracer, asyncResult2.State.CachedCommand, data.FailureException);
				asyncResult2.ProcessCompleted(DBNull.Value, data.FailureException);
				return;
			}
			if (!data.IsParseSuccessful)
			{
				IMAPUtils.LogExceptionDetails(asyncResult2.State.Log, IMAPClient.Tracer, asyncResult2.State.CachedCommand, data.FailureException);
				asyncResult2.ProcessCompleted(DBNull.Value, data.FailureException);
				return;
			}
			state.CachedCommand.ResetAsUidStore(state.UniqueCommandId, (string)state.CachedCommand.CommandParameters[0], flagsToRemove, false);
			asyncResult2.PendingAsyncResult = state.CommClient.BeginCommand(state.CachedCommand, state, new AsyncCallback(IMAPClient.OnEndStoreMessageFlagsInternal), asyncResult2, asyncResult2.SyncPoisonContext);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00021E17 File Offset: 0x00020017
		private static void OnEndCreateImapMailboxInternal(IAsyncResult asyncResult)
		{
			IMAPClient.ProcessResultAndCompleteRequest<DBNull>(asyncResult, (IMAPResultData resultData, IMAPClientState clientState) => DBNull.Value);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00021E43 File Offset: 0x00020043
		private static void OnEndDeleteImapMailboxInternal(IAsyncResult asyncResult)
		{
			IMAPClient.ProcessResultAndCompleteRequest<DBNull>(asyncResult, (IMAPResultData resultData, IMAPClientState state) => DBNull.Value);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00021E6F File Offset: 0x0002006F
		private static void OnEndRenameImapMailboxInternal(IAsyncResult asyncResult)
		{
			IMAPClient.ProcessResultAndCompleteRequest<DBNull>(asyncResult, (IMAPResultData resultData, IMAPClientState state) => DBNull.Value);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00022238 File Offset: 0x00020438
		private static void OnEndListImapMailboxesInternal(IAsyncResult asyncResult)
		{
			IMAPClient.ProcessResultAndCompleteRequest<IList<IMAPMailbox>>(asyncResult, delegate(IMAPResultData resultData, IMAPClientState state)
			{
				if (!resultData.IsParseSuccessful || resultData.FailureException != null)
				{
					return null;
				}
				if (!string.IsNullOrEmpty(state.RootFolderPath) && (int?)state.CachedCommand.CommandParameters[0] != null)
				{
					state.Log.LogDebugging((TSLID)834UL, IMAPClient.Tracer, "Need to post-process the LIST results, with RootFolderPath: {0}", new object[]
					{
						state.RootFolderPath
					});
					if ((state.RootPathProcessingFlags & RootPathProcessingFlags.UnableToProcess) == RootPathProcessingFlags.UnableToProcess)
					{
						resultData.FailureException = IMAPResponse.Fail("Failed to process LIST command with root folder path", state.CachedCommand, state.RootFolderPath);
						IMAPUtils.LogExceptionDetails(state.Log, IMAPClient.Tracer, state.CachedCommand, resultData.FailureException);
						resultData.Mailboxes.Clear();
						return null;
					}
					int value = ((int?)state.CachedCommand.CommandParameters[0]).Value;
					char c = (char)state.CachedCommand.CommandParameters[1];
					bool flag = false;
					if (resultData.Mailboxes.Count > 0)
					{
						if (resultData.Mailboxes[0].Separator != null)
						{
							c = resultData.Mailboxes[0].Separator.Value;
						}
						state.UpdateRootPathProcessingFlags(IMAPClient.Tracer, resultData.Mailboxes[0].NameOnTheWire, c, new int?(value), resultData.Mailboxes.Count);
						if (value == 1 && (state.RootPathProcessingFlags & RootPathProcessingFlags.FolderPathPrefixIsInbox) == RootPathProcessingFlags.FolderPathPrefixIsInbox)
						{
							resultData.Mailboxes[0].Name = IMAPMailbox.Inbox;
							flag = true;
							state.Log.LogVerbose((TSLID)1408UL, IMAPClient.Tracer, "The mailbox {0} is the only level 1 folder and it's the same as the path prefix. It will be treated as being the user's Inbox", new object[]
							{
								resultData.Mailboxes[0].NameOnTheWire
							});
						}
						else
						{
							foreach (IMAPMailbox imapmailbox in resultData.Mailboxes)
							{
								string imapmailboxNameFromWireName = IMAPClient.GetIMAPMailboxNameFromWireName(state, imapmailbox.NameOnTheWire, c);
								if (imapmailboxNameFromWireName == null)
								{
									resultData.FailureException = IMAPResponse.Fail("Failed to process LIST command with root folder path", state.CachedCommand, state.RootFolderPath);
									IMAPUtils.LogExceptionDetails(state.Log, IMAPClient.Tracer, state.CachedCommand, resultData.FailureException);
									resultData.Mailboxes.Clear();
									return null;
								}
								imapmailbox.Name = imapmailboxNameFromWireName;
								if (value == 1 && string.Equals(imapmailbox.Name, IMAPMailbox.Inbox, StringComparison.InvariantCultureIgnoreCase))
								{
									flag = true;
								}
							}
						}
					}
					if (value == 1 && !flag)
					{
						foreach (IMAPMailbox imapmailbox2 in resultData.Mailboxes)
						{
							imapmailbox2.Name = IMAPMailbox.Inbox + c.ToString() + imapmailbox2.Name;
						}
						IMAPMailbox imapmailbox3 = new IMAPMailbox(state.RootFolderPath.TrimEnd(new char[]
						{
							c
						}));
						imapmailbox3.Name = IMAPMailbox.Inbox;
						resultData.Mailboxes.Insert(0, imapmailbox3);
						state.UpdateRootPathProcessingFlags(IMAPClient.Tracer, RootPathProcessingFlags.FolderPathPrefixIsInbox);
						state.Log.LogVerbose((TSLID)835UL, IMAPClient.Tracer, "Treating the RootFolderPath prefix {0} as being the user's Inbox", new object[]
						{
							imapmailbox3.NameOnTheWire
						});
					}
				}
				return resultData.Mailboxes;
			});
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0002225D File Offset: 0x0002045D
		private static void OnEndLogOffInternal(IAsyncResult asyncResult)
		{
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00022260 File Offset: 0x00020460
		private static IAsyncResult CreateAsyncResultAndBeginCommand<TOutput>(IMAPClientState clientState, IMAPCommand commandToExecute, bool processResponse, AsyncCallback commandCompletionCallback, AsyncCallback externalCallback, object externalCallbackState, object syncPoisonContext) where TOutput : class
		{
			AsyncResult<IMAPClientState, TOutput> asyncResult = new AsyncResult<IMAPClientState, TOutput>(clientState, clientState, externalCallback, externalCallbackState, syncPoisonContext);
			asyncResult.PendingAsyncResult = clientState.CommClient.BeginCommand(commandToExecute, processResponse, clientState, commandCompletionCallback, asyncResult, syncPoisonContext);
			return asyncResult;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00022294 File Offset: 0x00020494
		private static void ProcessResultAndCompleteRequest<TOutput>(IAsyncResult asyncResult, IMAPClient.ResultConverter<TOutput> resultConverter) where TOutput : class
		{
			AsyncResult<IMAPClientState, TOutput> asyncResult2 = (AsyncResult<IMAPClientState, TOutput>)asyncResult.AsyncState;
			AsyncOperationResult<IMAPResultData> asyncOperationResult = asyncResult2.State.CommClient.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				IMAPUtils.LogExceptionDetails(asyncResult2.State.Log, IMAPClient.Tracer, asyncResult2.State.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(asyncOperationResult.Exception);
				return;
			}
			IMAPResultData data = asyncOperationResult.Data;
			TOutput result = default(TOutput);
			if (data.Status == IMAPStatus.Ok)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2271620413U);
				result = resultConverter(data, asyncResult2.State);
			}
			else
			{
				data.FailureException = IMAPClient.BuildFailureException(asyncResult2.State.CachedCommand, data.Status);
			}
			if (data.FailureException == null)
			{
				asyncResult2.State.Log.LogRawData((TSLID)836UL, IMAPClient.Tracer, "{0}: Command completed successfully.", new object[]
				{
					asyncResult2.State.CachedCommand.ToPiiCleanString()
				});
			}
			else
			{
				IMAPUtils.LogExceptionDetails(asyncResult2.State.Log, IMAPClient.Tracer, asyncResult2.State.CachedCommand, data.FailureException);
			}
			asyncResult2.ProcessCompleted(result, data.FailureException);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x000223D0 File Offset: 0x000205D0
		private static string GetIMAPMailboxNameFromWireName(IMAPClientState state, string mailboxName, char separator)
		{
			if (string.IsNullOrEmpty(state.RootFolderPath))
			{
				return mailboxName;
			}
			if ((state.RootPathProcessingFlags & RootPathProcessingFlags.UnableToProcess) == RootPathProcessingFlags.UnableToProcess)
			{
				state.Log.LogDebugging((TSLID)837UL, IMAPClient.Tracer, "Wire name {0} not translated. Unable to process server responses.", new object[]
				{
					mailboxName
				});
				return null;
			}
			string text = null;
			if ((state.RootPathProcessingFlags & RootPathProcessingFlags.FolderPathPrefixIsInbox) == RootPathProcessingFlags.FolderPathPrefixIsInbox)
			{
				if ((state.RootPathProcessingFlags & RootPathProcessingFlags.ResponseIncludesRootPathPrefix) == RootPathProcessingFlags.ResponseIncludesRootPathPrefix)
				{
					if (!mailboxName.StartsWith(state.RootFolderPath))
					{
						state.Log.LogDebugging((TSLID)838UL, IMAPClient.Tracer, "Wire name {0} not translated. It does not begin with expected prefix {1}", new object[]
						{
							mailboxName,
							state.RootFolderPath
						});
						return null;
					}
					text = IMAPMailbox.Inbox + separator.ToString() + mailboxName.Substring(state.RootFolderPath.Length);
				}
				else
				{
					text = IMAPMailbox.Inbox + separator.ToString() + mailboxName;
				}
			}
			else if ((state.RootPathProcessingFlags & RootPathProcessingFlags.ResponseIncludesRootPathPrefix) == RootPathProcessingFlags.ResponseIncludesRootPathPrefix)
			{
				if (!mailboxName.StartsWith(state.RootFolderPath))
				{
					state.Log.LogDebugging((TSLID)840UL, IMAPClient.Tracer, "Wire name {0} not translated. It does not begin with expected prefix {1}", new object[]
					{
						mailboxName,
						state.RootFolderPath
					});
					return null;
				}
				text = mailboxName.Substring(state.RootFolderPath.Length);
				if (string.IsNullOrEmpty(text))
				{
					state.Log.LogDebugging((TSLID)839UL, IMAPClient.Tracer, "Wire name {0} not translated. Removing the prefix {1} makes it an empty string", new object[]
					{
						mailboxName,
						state.RootFolderPath
					});
					return null;
				}
			}
			state.Log.LogDebugging((TSLID)841UL, IMAPClient.Tracer, "Wire name {0} translated to actual name {1}", new object[]
			{
				mailboxName,
				text
			});
			return text;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000225A4 File Offset: 0x000207A4
		private static Exception BuildFailureException(IMAPCommand command, IMAPStatus completionCode)
		{
			switch (completionCode)
			{
			case IMAPStatus.No:
				return SyncTransientException.CreateItemLevelException(new IMAPException(string.Format(CultureInfo.InvariantCulture, "Error while executing [{0}]", new object[]
				{
					(command == null) ? "Initial handshake" : command.ToPiiCleanString()
				})));
			case IMAPStatus.Bad:
				return SyncPermanentException.CreateItemLevelException(IMAPClient.BuildBadResponseInnerException(command));
			case IMAPStatus.Bye:
				return SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, new IMAPException("IMAP Server disconnected."), true);
			default:
				throw new InvalidOperationException("Unknown response failure code: " + completionCode);
			}
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00022634 File Offset: 0x00020834
		private static Exception BuildBadResponseInnerException(IMAPCommand command)
		{
			return new IMAPException(string.Format(CultureInfo.InvariantCulture, "Error while executing [{0}]", new object[]
			{
				(command == null) ? "Initial handshake" : command.ToPiiCleanString()
			}));
		}

		// Token: 0x04000751 RID: 1873
		internal static readonly Trace Tracer = ExTraceGlobals.IMAPClientTracer;

		// Token: 0x04000752 RID: 1874
		internal static readonly string ImapComponentId = "IMAP";

		// Token: 0x04000753 RID: 1875
		internal static IList<string> MessageInfoDataItemsForChangesOnly = new List<string>(new string[]
		{
			"UID",
			"FLAGS"
		}).AsReadOnly();

		// Token: 0x04000754 RID: 1876
		internal static IList<string> MessageInfoDataItemsForUidValidityRecovery = new List<string>(new string[]
		{
			"UID",
			"BODY.PEEK[HEADER.FIELDS (Message-ID)]"
		}).AsReadOnly();

		// Token: 0x04000755 RID: 1877
		internal static IList<string> MessageInfoDataItemsForNewMessages = new List<string>(new string[]
		{
			"UID",
			"FLAGS",
			"BODY.PEEK[HEADER.FIELDS (Message-ID)]",
			"RFC822.SIZE"
		}).AsReadOnly();

		// Token: 0x04000756 RID: 1878
		private static IList<string> messageBodyDataItems = new List<string>(new string[]
		{
			"UID",
			"INTERNALDATE",
			"BODY.PEEK[]"
		}).AsReadOnly();

		// Token: 0x020001CE RID: 462
		// (Invoke) Token: 0x06000DBA RID: 3514
		private delegate TOutput ResultConverter<TOutput>(IMAPResultData resultData, IMAPClientState state);
	}
}
