using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ImapConnectionCore
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003E0C File Offset: 0x0000200C
		public static IAsyncResult BeginConnectAndAuthenticate(ImapConnectionContext context, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("Beginning connect-and-authenticate process", new object[0]);
			AsyncResult<ImapConnectionContext, DBNull> asyncResult = new AsyncResult<ImapConnectionContext, DBNull>(context, context, callback, callbackState);
			AsyncCallback callback2;
			switch (context.ImapSecurityMechanism)
			{
			case ImapSecurityMechanism.None:
			case ImapSecurityMechanism.Ssl:
				callback2 = new AsyncCallback(ImapConnectionCore.OnEndConnectToAuthenticate);
				break;
			case ImapSecurityMechanism.Tls:
				callback2 = new AsyncCallback(ImapConnectionCore.OnEndConnectToStarttls);
				break;
			default:
				throw new InvalidOperationException("Unexpected security mechanism " + context.ImapSecurityMechanism);
			}
			asyncResult.PendingAsyncResult = context.NetworkFacade.BeginConnect(context, callback2, asyncResult);
			return asyncResult;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003EA4 File Offset: 0x000020A4
		public static AsyncOperationResult<DBNull> EndConnectAndAuthenticate(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003EC0 File Offset: 0x000020C0
		public static IAsyncResult BeginCapabilities(ImapConnectionContext context, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("Getting server capabilities", new object[0]);
			context.CachedCommand.ResetAsCapability(context.UniqueCommandId());
			return ImapConnectionCore.CreateAsyncResultAndBeginCommand<ImapServerCapabilities>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndCapabilityInternal), callback, callbackState);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003F10 File Offset: 0x00002110
		public static AsyncOperationResult<ImapServerCapabilities> EndCapabilities(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, ImapServerCapabilities> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapServerCapabilities>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003F2C File Offset: 0x0000212C
		public static IAsyncResult BeginExpunge(ImapConnectionContext context, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("Expunging messages.", new object[0]);
			context.CachedCommand.ResetAsExpunge(context.UniqueCommandId());
			return ImapConnectionCore.CreateAsyncResultAndBeginCommand<DBNull>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndExpungeInternal), callback, callbackState);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003F7C File Offset: 0x0000217C
		public static AsyncOperationResult<DBNull> EndExpunge(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003F98 File Offset: 0x00002198
		public static IAsyncResult BeginSelectImapMailbox(ImapConnectionContext context, ImapMailbox imapMailbox, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("Selecting mailbox {0}", new object[]
			{
				imapMailbox.Name
			});
			context.CachedCommand.ResetAsSelect(context.UniqueCommandId(), imapMailbox);
			return ImapConnectionCore.CreateAsyncResultAndBeginCommand<ImapMailbox>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndSelectImapMailboxCallStatusIfNeeded), callback, callbackState);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003FF4 File Offset: 0x000021F4
		public static AsyncOperationResult<ImapMailbox> EndSelectImapMailbox(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, ImapMailbox> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapMailbox>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004010 File Offset: 0x00002210
		public static IAsyncResult BeginGetMessageInfoByRange(ImapConnectionContext context, string start, string end, bool uidFetch, IList<string> messageDataItems, AsyncCallback callback, object callbackState)
		{
			AsyncResult<ImapConnectionContext, ImapResultData> asyncResult = new AsyncResult<ImapConnectionContext, ImapResultData>(context, context, callback, callbackState);
			context.CachedCommand.ResetAsFetch(context.UniqueCommandId(), start, end, uidFetch, messageDataItems);
			context.Log.Debug("IMAP Fetch Message Headers in range.  Start={0}.  End={1}", new object[]
			{
				start,
				end
			});
			asyncResult.PendingAsyncResult = context.NetworkFacade.BeginCommand(context.CachedCommand, context, new AsyncCallback(ImapConnectionCore.OnEndGetMessageInfoByRangeInternal), asyncResult);
			return asyncResult;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004088 File Offset: 0x00002288
		public static AsyncOperationResult<ImapResultData> EndGetMessageInfoByRange(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000040A4 File Offset: 0x000022A4
		public static IAsyncResult BeginGetMessageItemByUid(ImapConnectionContext context, string uid, IList<string> messageBodyDataItems, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("IMAP Fetch Message Body.  Uid={0}.", new object[]
			{
				uid
			});
			context.CachedCommand.ResetAsFetch(context.UniqueCommandId(), uid, null, true, messageBodyDataItems);
			return ImapConnectionCore.CreateAsyncResultAndBeginCommand<ImapResultData>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndGetMessageBodyInternal), callback, callbackState);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004100 File Offset: 0x00002300
		public static AsyncOperationResult<ImapResultData> EndGetMessageItemByUid(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000411C File Offset: 0x0000231C
		public static IAsyncResult BeginAppendMessageToImapMailbox(ImapConnectionContext context, string mailboxName, ImapMailFlags messageFlags, Stream messageMimeStream, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("Appending message to mailbox {0}", new object[]
			{
				mailboxName
			});
			context.CachedCommand.ResetAsAppend(context.UniqueCommandId(), mailboxName, messageFlags, messageMimeStream);
			return ImapConnectionCore.CreateAsyncResultAndBeginCommand<string>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndAppendMessageInternal), callback, callbackState);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004178 File Offset: 0x00002378
		public static AsyncOperationResult<string> EndAppendMessageToImapMailbox(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, string> asyncResult2 = (AsyncResult<ImapConnectionContext, string>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004194 File Offset: 0x00002394
		public static IAsyncResult BeginSearchForMessageByMessageId(ImapConnectionContext context, string messageId, AsyncCallback callback = null, object callbackState = null)
		{
			context.Log.Debug("Searching for message by ID.  ID = {0}", new object[]
			{
				messageId
			});
			context.CachedCommand.ResetAsSearch(context.UniqueCommandId(), new string[]
			{
				"HEADER Message-Id",
				messageId
			});
			return ImapConnectionCore.CreateAsyncResultAndBeginCommand<IList<string>>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndSearchForMessageInternal), callback, callbackState);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004200 File Offset: 0x00002400
		public static AsyncOperationResult<IList<string>> EndSearchForMessageByMessageId(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, IList<string>> asyncResult2 = (AsyncResult<ImapConnectionContext, IList<string>>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000421C File Offset: 0x0000241C
		public static IAsyncResult BeginStoreMessageFlags(ImapConnectionContext context, string uid, ImapMailFlags flagsToStore, ImapMailFlags previousFlags, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("Storing flags for message.  UID = {0}", new object[]
			{
				uid
			});
			ImapMailFlags imapMailFlags = flagsToStore & ~previousFlags;
			ImapMailFlags imapMailFlags2 = ~flagsToStore & previousFlags;
			if (imapMailFlags != ImapMailFlags.None)
			{
				context.CachedCommand.ResetAsUidStore(context.UniqueCommandId(), uid, imapMailFlags, true);
				context.FlagsToRemove = imapMailFlags2;
				return ImapConnectionCore.CreateAsyncResultAndBeginCommand<DBNull>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndStoreMessageFlagsInternal), callback, callbackState);
			}
			if (imapMailFlags2 != ImapMailFlags.None)
			{
				context.CachedCommand.ResetAsUidStore(context.UniqueCommandId(), uid, imapMailFlags2, false);
				context.FlagsToRemove = ImapMailFlags.None;
				return ImapConnectionCore.CreateAsyncResultAndBeginCommand<DBNull>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndStoreMessageFlagsInternal), callback, callbackState);
			}
			context.Log.Error("Attempt to store the same flags that were already set.", new object[0]);
			return null;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000042E0 File Offset: 0x000024E0
		public static AsyncOperationResult<DBNull> EndStoreMessageFlags(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000042FC File Offset: 0x000024FC
		public static IAsyncResult BeginCreateImapMailbox(ImapConnectionContext context, string mailboxName, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("Creating new IMAP mailbox.  Name = {0}", new object[]
			{
				mailboxName
			});
			context.CachedCommand.ResetAsCreate(context.UniqueCommandId(), mailboxName);
			return ImapConnectionCore.CreateAsyncResultAndBeginCommand<DBNull>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndCreateImapMailboxInternal), callback, callbackState);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004354 File Offset: 0x00002554
		public static AsyncOperationResult<DBNull> EndCreateImapMailbox(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004370 File Offset: 0x00002570
		public static IAsyncResult BeginDeleteImapMailbox(ImapConnectionContext context, string mailboxName, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("Deleting IMAP mailbox.  Name = {0}", new object[]
			{
				mailboxName
			});
			context.CachedCommand.ResetAsDelete(context.UniqueCommandId(), mailboxName);
			return ImapConnectionCore.CreateAsyncResultAndBeginCommand<DBNull>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndDeleteImapMailboxInternal), callback, callbackState);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000043C8 File Offset: 0x000025C8
		public static AsyncOperationResult<DBNull> EndDeleteImapMailbox(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000043E4 File Offset: 0x000025E4
		public static IAsyncResult BeginRenameImapMailbox(ImapConnectionContext context, string oldMailboxName, string newMailboxName, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("Renaming IMAP mailbox.  {0} => {1}", new object[]
			{
				oldMailboxName,
				newMailboxName
			});
			context.CachedCommand.ResetAsRename(context.UniqueCommandId(), oldMailboxName, newMailboxName);
			return ImapConnectionCore.CreateAsyncResultAndBeginCommand<DBNull>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndRenameImapMailboxInternal), callback, callbackState);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004440 File Offset: 0x00002640
		public static AsyncOperationResult<DBNull> EndRenameImapMailbox(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000445C File Offset: 0x0000265C
		public static IAsyncResult BeginListImapMailboxesByLevel(ImapConnectionContext context, int level, char separator, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("Listing mailboxes at level {0}", new object[]
			{
				level
			});
			context.InitializeRootPathProcessingFlags(level, separator);
			context.CachedCommand.ResetAsList(context.UniqueCommandId(), separator, new int?(level), context.RootFolderPath);
			return ImapConnectionCore.CreateAsyncResultAndBeginCommand<IList<ImapMailbox>>(context, context.CachedCommand, true, new AsyncCallback(ImapConnectionCore.OnEndListImapMailboxesInternal), callback, callbackState);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000044CC File Offset: 0x000026CC
		public static AsyncOperationResult<IList<ImapMailbox>> EndListImapMailboxesByLevel(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, IList<ImapMailbox>> asyncResult2 = (AsyncResult<ImapConnectionContext, IList<ImapMailbox>>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000044E8 File Offset: 0x000026E8
		public static IAsyncResult BeginLogOff(ImapConnectionContext context, AsyncCallback callback, object callbackState)
		{
			context.Log.Debug("Logging out", new object[0]);
			AsyncResult<ImapConnectionContext, DBNull> asyncResult = new AsyncResult<ImapConnectionContext, DBNull>(context, context, callback, callbackState);
			context.CachedCommand.ResetAsLogout(context.UniqueCommandId());
			try
			{
				asyncResult.PendingAsyncResult = context.NetworkFacade.BeginCommand(context.CachedCommand, false, context, new AsyncCallback(ImapConnectionCore.OnEndLogOffInternal), asyncResult);
			}
			catch (InvalidOperationException ex)
			{
				context.Log.Error("Caught InvalidOperationException while logging off. Exception = {0}", new object[]
				{
					ex
				});
				context.Log.Fatal(ex, "BUG: BeginLogOff : should never throw InvalidOperationException.");
			}
			asyncResult.SetCompletedSynchronously();
			asyncResult.ProcessCompleted(DBNull.Value);
			return asyncResult;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000045A0 File Offset: 0x000027A0
		public static AsyncOperationResult<DBNull> EndLogOff(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000045BA File Offset: 0x000027BA
		public static void Cancel(ImapConnectionContext context)
		{
			context.NetworkFacade.Cancel();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000045C8 File Offset: 0x000027C8
		private static void OnEndConnectToStarttls(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult.AsyncState;
			ImapConnectionContext state = asyncResult2.State;
			AsyncOperationResult<ImapResultData> asyncOperationResult = state.NetworkFacade.EndConnect(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				ImapUtilities.LogExceptionDetails(state.Log, "Connecting", asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(null, asyncOperationResult.Exception);
				return;
			}
			state.CachedCommand.ResetAsStarttls(state.UniqueCommandId());
			asyncResult2.PendingAsyncResult = state.NetworkFacade.BeginCommand(state.CachedCommand, state, new AsyncCallback(ImapConnectionCore.OnEndStarttlsToBeginTlsNegotiation), asyncResult2);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004658 File Offset: 0x00002858
		private static void OnEndStarttlsToBeginTlsNegotiation(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult.AsyncState;
			ImapConnectionContext state = asyncResult2.State;
			AsyncOperationResult<ImapResultData> asyncOperationResult = state.NetworkFacade.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				ImapUtilities.LogExceptionDetails(state.Log, state.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(null, asyncOperationResult.Exception);
				return;
			}
			ImapResultData data = asyncOperationResult.Data;
			if (data.Status != ImapStatus.Ok)
			{
				data.FailureException = ImapConnectionCore.BuildFailureException(state.CachedCommand, data.Status);
				ImapUtilities.LogExceptionDetails(state.Log, state.CachedCommand, data.FailureException);
				asyncResult2.ProcessCompleted(null, data.FailureException);
				return;
			}
			asyncResult2.PendingAsyncResult = state.NetworkFacade.BeginNegotiateTlsAsClient(state, new AsyncCallback(ImapConnectionCore.OnEndConnectToAuthenticate), asyncResult2);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004720 File Offset: 0x00002920
		private static void OnEndConnectToAuthenticate(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult.AsyncState;
			ImapConnectionContext state = asyncResult2.State;
			switch (state.ImapSecurityMechanism)
			{
			case ImapSecurityMechanism.None:
			case ImapSecurityMechanism.Ssl:
			{
				AsyncOperationResult<ImapResultData> asyncOperationResult = state.NetworkFacade.EndConnect(asyncResult);
				if (!asyncOperationResult.IsSucceeded)
				{
					ImapUtilities.LogExceptionDetails(state.Log, "Connecting", asyncOperationResult.Exception);
					asyncResult2.ProcessCompleted(null, asyncOperationResult.Exception);
					return;
				}
				break;
			}
			case ImapSecurityMechanism.Tls:
			{
				AsyncOperationResult<ImapResultData> asyncOperationResult2 = state.NetworkFacade.EndNegotiateTlsAsClient(asyncResult);
				if (!asyncOperationResult2.IsSucceeded)
				{
					ImapUtilities.LogExceptionDetails(state.Log, "Tls negotiation", asyncOperationResult2.Exception);
					asyncResult2.ProcessCompleted(null, asyncOperationResult2.Exception);
					return;
				}
				break;
			}
			default:
				throw new InvalidOperationException("Unexpected security mechanism " + state.ImapSecurityMechanism);
			}
			NetworkCredential networkCredential = state.AuthenticationParameters.NetworkCredential;
			ImapAuthenticationMechanism imapAuthenticationMechanism = state.ImapAuthenticationMechanism;
			if (imapAuthenticationMechanism == ImapAuthenticationMechanism.Basic)
			{
				state.CachedCommand.ResetAsLogin(state.UniqueCommandId(), networkCredential.UserName, networkCredential.SecurePassword);
				asyncResult2.PendingAsyncResult = state.NetworkFacade.BeginCommand(state.CachedCommand, state, new AsyncCallback(ImapConnectionCore.OnEndLoginFallbackToAuthenticatePlainIfNeeded), asyncResult2);
				return;
			}
			if (imapAuthenticationMechanism != ImapAuthenticationMechanism.Ntlm)
			{
				throw new InvalidOperationException("Unexpected authentication mechanism" + state.ImapAuthenticationMechanism);
			}
			AuthenticationContext authContext = null;
			state.CachedCommand.ResetAsAuthenticate(state.UniqueCommandId(), ImapAuthenticationMechanism.Ntlm, networkCredential.UserName, networkCredential.SecurePassword, authContext);
			asyncResult2.PendingAsyncResult = state.NetworkFacade.BeginCommand(state.CachedCommand, state, new AsyncCallback(ImapConnectionCore.OnEndCompleteConnectAndAuthenticate), asyncResult2);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000048C0 File Offset: 0x00002AC0
		private static void OnEndCompleteConnectAndAuthenticate(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult.AsyncState;
			AsyncOperationResult<ImapResultData> asyncOperationResult = asyncResult2.State.NetworkFacade.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				ImapUtilities.LogExceptionDetails(asyncResult2.State.Log, asyncResult2.State.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(asyncOperationResult.Exception);
				return;
			}
			ImapResultData data = asyncOperationResult.Data;
			Exception ex = null;
			if (data.Status == ImapStatus.No)
			{
				ImapAuthenticationMechanism imapAuthenticationMechanism = asyncResult2.State.ImapAuthenticationMechanism;
				string imapAuthenticationErrorMsg = asyncResult2.State.ServerParameters.Server.ToString();
				ex = new ImapAuthenticationException(imapAuthenticationErrorMsg, imapAuthenticationMechanism.ToString(), RetryPolicy.Backoff);
			}
			else if (data.Status == ImapStatus.Bad)
			{
				ImapAuthenticationMechanism imapAuthenticationMechanism2 = asyncResult2.State.ImapAuthenticationMechanism;
				string imapAuthenticationErrorMsg2 = asyncResult2.State.ServerParameters.Server.ToString();
				Exception innerException = ImapConnectionCore.BuildBadResponseException(asyncResult2.State.CachedCommand);
				ex = new ImapAuthenticationException(imapAuthenticationErrorMsg2, imapAuthenticationMechanism2.ToString(), RetryPolicy.Backoff, innerException);
			}
			else if (data.Status != ImapStatus.Ok)
			{
				ex = ImapConnectionCore.BuildFailureException(asyncResult2.State.CachedCommand, data.Status);
			}
			if (ex == null)
			{
				asyncResult2.State.Log.Debug("{0}: Command completed successfully.", new object[]
				{
					asyncResult2.State.CachedCommand.ToPiiCleanString()
				});
			}
			else
			{
				ImapUtilities.LogExceptionDetails(asyncResult2.State.Log, asyncResult2.State.CachedCommand, ex);
			}
			asyncResult2.ProcessCompleted(DBNull.Value, ex);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004A48 File Offset: 0x00002C48
		private static void OnEndLoginFallbackToAuthenticatePlainIfNeeded(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult.AsyncState;
			ImapConnectionContext state = asyncResult2.State;
			AsyncOperationResult<ImapResultData> asyncOperationResult = state.NetworkFacade.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				ImapUtilities.LogExceptionDetails(state.Log, state.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(asyncOperationResult.Exception);
				return;
			}
			ImapResultData data = asyncOperationResult.Data;
			Exception ex = null;
			if (data.Status == ImapStatus.No || data.Status == ImapStatus.Bad)
			{
				AuthenticationContext authContext = null;
				NetworkCredential networkCredential = state.AuthenticationParameters.NetworkCredential;
				state.CachedCommand.ResetAsAuthenticate(state.UniqueCommandId(), ImapAuthenticationMechanism.Basic, networkCredential.UserName, networkCredential.SecurePassword, authContext);
				asyncResult2.PendingAsyncResult = state.NetworkFacade.BeginCommand(state.CachedCommand, state, new AsyncCallback(ImapConnectionCore.OnEndCompleteConnectAndAuthenticate), asyncResult2);
				return;
			}
			if (data.Status != ImapStatus.Ok)
			{
				ex = ImapConnectionCore.BuildFailureException(state.CachedCommand, data.Status);
			}
			if (ex == null)
			{
				state.Log.Debug("{0}: Command completed successfully.", new object[]
				{
					state.CachedCommand.ToPiiCleanString()
				});
			}
			else
			{
				ImapUtilities.LogExceptionDetails(state.Log, state.CachedCommand, ex);
			}
			asyncResult2.ProcessCompleted(DBNull.Value, ex);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004B80 File Offset: 0x00002D80
		private static void OnEndCapabilityInternal(IAsyncResult asyncResult)
		{
			ImapConnectionCore.ProcessResultAndCompleteRequest<ImapServerCapabilities>(asyncResult, new ImapConnectionCore.ResultConverter<ImapServerCapabilities>(ImapConnectionCore.EndCapabilityResultConverter));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004B94 File Offset: 0x00002D94
		private static ImapServerCapabilities EndCapabilityResultConverter(ImapResultData resultData, ImapConnectionContext context)
		{
			if (resultData.IsParseSuccessful)
			{
				context.Log.Debug("Found capabilities from server", new object[0]);
				return resultData.Capabilities;
			}
			return null;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004BC3 File Offset: 0x00002DC3
		private static void OnEndExpungeInternal(IAsyncResult asyncResult)
		{
			ImapConnectionCore.ProcessResultAndCompleteRequest<DBNull>(asyncResult, (ImapResultData resultData, ImapConnectionContext context) => DBNull.Value);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004BE8 File Offset: 0x00002DE8
		private static void OnEndSelectImapMailboxCallStatusIfNeeded(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, ImapMailbox> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapMailbox>)asyncResult.AsyncState;
			ImapConnectionContext state = asyncResult2.State;
			Exception ex = null;
			ImapMailbox imapMailbox = null;
			AsyncOperationResult<ImapResultData> asyncOperationResult = state.NetworkFacade.EndCommand(asyncResult);
			if (asyncOperationResult.IsSucceeded)
			{
				ImapResultData data = asyncOperationResult.Data;
				if (data.IsParseSuccessful && data.Mailboxes.Count == 1)
				{
					if (data.Mailboxes[0].NumberOfMessages == null)
					{
						ex = new ImapCommunicationException(CXStrings.ImapNoExistsData, RetryPolicy.Backoff);
					}
					else
					{
						imapMailbox = data.Mailboxes[0];
						if (data.Mailboxes[0].UidNext == null)
						{
							state.CachedCommand.ResetAsStatus(state.UniqueCommandId(), imapMailbox);
							asyncResult2.PendingAsyncResult = state.NetworkFacade.BeginCommand(state.CachedCommand, state, new AsyncCallback(ImapConnectionCore.OnEndSelectFollowedByStatus), asyncResult2);
							return;
						}
						state.Log.Debug("Selected mailbox {0}", new object[]
						{
							imapMailbox.Name
						});
					}
				}
				else
				{
					ex = new ImapCommunicationException(CXStrings.ImapSelectMailboxFailed, RetryPolicy.Backoff);
				}
			}
			else
			{
				ex = asyncOperationResult.Exception;
			}
			if (ex == null)
			{
				state.Log.Debug("{0}: Command completed successfully.", new object[]
				{
					state.CachedCommand.ToPiiCleanString()
				});
			}
			else
			{
				ImapUtilities.LogExceptionDetails(state.Log, state.CachedCommand, ex);
			}
			asyncResult2.ProcessCompleted(imapMailbox, ex);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004D6C File Offset: 0x00002F6C
		private static void OnEndSelectFollowedByStatus(IAsyncResult asyncResult)
		{
			ImapConnectionCore.ProcessResultAndCompleteRequest<ImapMailbox>(asyncResult, new ImapConnectionCore.ResultConverter<ImapMailbox>(ImapConnectionCore.EndSelectFollowedByStatusResultConverter));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004D80 File Offset: 0x00002F80
		private static ImapMailbox EndSelectFollowedByStatusResultConverter(ImapResultData resultData, ImapConnectionContext context)
		{
			if (resultData.IsParseSuccessful && resultData.Mailboxes.Count == 1 && resultData.Mailboxes[0].UidNext != null)
			{
				context.Log.Debug("Selected mailbox {0} after succesful STATUS command", new object[]
				{
					resultData.Mailboxes[0].Name
				});
				return resultData.Mailboxes[0];
			}
			string imapCommunicationErrorMsg = string.Format("Failed to get STATUS for mailbox. {0}", resultData.IsParseSuccessful ? ((resultData.Mailboxes.Count == 1) ? "Missing UIDNEXT" : "No mailbox returned") : " Parsing unsuccessful.");
			resultData.FailureException = new ImapCommunicationException(imapCommunicationErrorMsg, RetryPolicy.Backoff);
			return null;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004E3C File Offset: 0x0000303C
		private static void OnEndGetMessageInfoByRangeInternal(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult.AsyncState;
			AsyncOperationResult<ImapResultData> asyncOperationResult = asyncResult2.State.NetworkFacade.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				ImapUtilities.LogExceptionDetails(asyncResult2.State.Log, asyncResult2.State.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(null, asyncOperationResult.Exception);
				return;
			}
			ImapResultData data = asyncOperationResult.Data;
			if (data.Status != ImapStatus.Ok)
			{
				data.FailureException = ImapConnectionCore.BuildFailureException(asyncResult2.State.CachedCommand, data.Status);
				ImapUtilities.LogExceptionDetails(asyncResult2.State.Log, asyncResult2.State.CachedCommand, data.FailureException);
				asyncResult2.ProcessCompleted(null, data.FailureException);
				return;
			}
			if (!data.IsParseSuccessful)
			{
				ImapUtilities.LogExceptionDetails(asyncResult2.State.Log, asyncResult2.State.CachedCommand, data.FailureException);
			}
			asyncResult2.ProcessCompleted(data, data.FailureException);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004F2F File Offset: 0x0000312F
		private static void OnEndGetMessageBodyInternal(IAsyncResult asyncResult)
		{
			ImapConnectionCore.ProcessResultAndCompleteRequest<ImapResultData>(asyncResult, new ImapConnectionCore.ResultConverter<ImapResultData>(ImapConnectionCore.EndGetMessageBodyResultConverter));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004F44 File Offset: 0x00003144
		private static ImapResultData EndGetMessageBodyResultConverter(ImapResultData resultData, ImapConnectionContext context)
		{
			if (resultData.IsParseSuccessful)
			{
				context.Log.Debug("Successful UID FETCH of message body", new object[0]);
				IList<string> messageUids = resultData.MessageUids;
				if (messageUids.Count != 1)
				{
					context.Log.Debug("Unexpected number of UIDs returned during single message fetch: {0}", new object[]
					{
						messageUids.Count
					});
				}
			}
			context.ActivatePerfMsgDownloadEvent(context, null);
			return resultData;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004FAE File Offset: 0x000031AE
		private static void OnEndAppendMessageInternal(IAsyncResult asyncResult)
		{
			ImapConnectionCore.ProcessResultAndCompleteRequest<string>(asyncResult, new ImapConnectionCore.ResultConverter<string>(ImapConnectionCore.EndAppendMessageResultConverter));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004FC2 File Offset: 0x000031C2
		private static string EndAppendMessageResultConverter(ImapResultData resultData, ImapConnectionContext context)
		{
			context.ActivatePerfMsgUploadEvent(context, null);
			if (resultData.MessageUids != null && resultData.MessageUids.Count > 0)
			{
				return resultData.MessageUids[0];
			}
			return null;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004FF0 File Offset: 0x000031F0
		private static void OnEndSearchForMessageInternal(IAsyncResult asyncResult)
		{
			ImapConnectionCore.ProcessResultAndCompleteRequest<IList<string>>(asyncResult, new ImapConnectionCore.ResultConverter<IList<string>>(ImapConnectionCore.EndSearchForMessageResultConverter));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005004 File Offset: 0x00003204
		private static IList<string> EndSearchForMessageResultConverter(ImapResultData resultData, ImapConnectionContext state)
		{
			if (resultData.IsParseSuccessful)
			{
				return resultData.MessageUids;
			}
			return null;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005020 File Offset: 0x00003220
		private static void OnEndStoreMessageFlagsInternal(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, DBNull> asyncResult2 = (AsyncResult<ImapConnectionContext, DBNull>)asyncResult.AsyncState;
			ImapConnectionContext state = asyncResult2.State;
			if (state.FlagsToRemove == ImapMailFlags.None)
			{
				ImapConnectionCore.ProcessResultAndCompleteRequest<DBNull>(asyncResult, (ImapResultData resultData, ImapConnectionContext context) => DBNull.Value);
				return;
			}
			ImapMailFlags flagsToRemove = state.FlagsToRemove;
			state.FlagsToRemove = ImapMailFlags.None;
			AsyncOperationResult<ImapResultData> asyncOperationResult = asyncResult2.State.NetworkFacade.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				ImapUtilities.LogExceptionDetails(asyncResult2.State.Log, asyncResult2.State.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(DBNull.Value, asyncOperationResult.Exception);
				return;
			}
			ImapResultData data = asyncOperationResult.Data;
			if (data.Status != ImapStatus.Ok)
			{
				data.FailureException = ImapConnectionCore.BuildFailureException(asyncResult2.State.CachedCommand, data.Status);
				ImapUtilities.LogExceptionDetails(asyncResult2.State.Log, asyncResult2.State.CachedCommand, data.FailureException);
				asyncResult2.ProcessCompleted(DBNull.Value, data.FailureException);
				return;
			}
			if (!data.IsParseSuccessful)
			{
				ImapUtilities.LogExceptionDetails(asyncResult2.State.Log, asyncResult2.State.CachedCommand, data.FailureException);
				asyncResult2.ProcessCompleted(DBNull.Value, data.FailureException);
				return;
			}
			state.CachedCommand.ResetAsUidStore(state.UniqueCommandId(), (string)state.CachedCommand.CommandParameters[0], flagsToRemove, false);
			asyncResult2.PendingAsyncResult = state.NetworkFacade.BeginCommand(state.CachedCommand, state, new AsyncCallback(ImapConnectionCore.OnEndStoreMessageFlagsInternal), asyncResult2);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000051C2 File Offset: 0x000033C2
		private static void OnEndCreateImapMailboxInternal(IAsyncResult asyncResult)
		{
			ImapConnectionCore.ProcessResultAndCompleteRequest<DBNull>(asyncResult, (ImapResultData resultData, ImapConnectionContext context) => DBNull.Value);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000051EE File Offset: 0x000033EE
		private static void OnEndDeleteImapMailboxInternal(IAsyncResult asyncResult)
		{
			ImapConnectionCore.ProcessResultAndCompleteRequest<DBNull>(asyncResult, (ImapResultData resultData, ImapConnectionContext state) => DBNull.Value);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000521A File Offset: 0x0000341A
		private static void OnEndRenameImapMailboxInternal(IAsyncResult asyncResult)
		{
			ImapConnectionCore.ProcessResultAndCompleteRequest<DBNull>(asyncResult, (ImapResultData resultData, ImapConnectionContext state) => DBNull.Value);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000523F File Offset: 0x0000343F
		private static void OnEndListImapMailboxesInternal(IAsyncResult asyncResult)
		{
			ImapConnectionCore.ProcessResultAndCompleteRequest<IList<ImapMailbox>>(asyncResult, new ImapConnectionCore.ResultConverter<IList<ImapMailbox>>(ImapConnectionCore.EndListImapMailboxesResultConverter));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005254 File Offset: 0x00003454
		private static IList<ImapMailbox> EndListImapMailboxesResultConverter(ImapResultData resultData, ImapConnectionContext context)
		{
			if (!resultData.IsParseSuccessful || resultData.FailureException != null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(context.RootFolderPath) && (int?)context.CachedCommand.CommandParameters[0] != null)
			{
				context.Log.Debug("Need to post-process the LIST results, with RootFolderPath: {0}", new object[]
				{
					context.RootFolderPath
				});
				if ((context.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.UnableToProcess) == ImapRootPathProcessingFlags.UnableToProcess)
				{
					resultData.FailureException = ImapResponse.Fail("Failed to process LIST command with root folder path", context.CachedCommand, context.RootFolderPath);
					ImapUtilities.LogExceptionDetails(context.Log, context.CachedCommand, resultData.FailureException);
					resultData.Mailboxes.Clear();
					return null;
				}
				int value = ((int?)context.CachedCommand.CommandParameters[0]).Value;
				char c = (char)context.CachedCommand.CommandParameters[1];
				bool flag = false;
				if (resultData.Mailboxes.Count > 0)
				{
					if (resultData.Mailboxes[0].Separator != null)
					{
						c = resultData.Mailboxes[0].Separator.Value;
					}
					context.UpdateRootPathProcessingFlags(resultData.Mailboxes[0].NameOnTheWire, c, new int?(value), resultData.Mailboxes.Count);
					if (value == 1 && (context.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.FolderPathPrefixIsInbox) == ImapRootPathProcessingFlags.FolderPathPrefixIsInbox)
					{
						context.Log.Assert(resultData.Mailboxes.Count == 1 && (context.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.ResponseIncludesRootPathPrefix) == ImapRootPathProcessingFlags.ResponseIncludesRootPathPrefix, "INBOX decision incorrect", new object[0]);
						resultData.Mailboxes[0].Name = ImapMailbox.Inbox;
						flag = true;
						context.Log.Debug("The mailbox {0} is the only level 1 folder and it's the same as the path prefix. It will be treated as being the user's Inbox", new object[]
						{
							resultData.Mailboxes[0].NameOnTheWire
						});
					}
					else
					{
						foreach (ImapMailbox imapMailbox in resultData.Mailboxes)
						{
							string imapMailboxNameFromWireName = ImapConnectionCore.GetImapMailboxNameFromWireName(context, imapMailbox.NameOnTheWire, c);
							if (imapMailboxNameFromWireName == null)
							{
								resultData.FailureException = ImapResponse.Fail("Failed to process LIST command with root folder path", context.CachedCommand, context.RootFolderPath);
								ImapUtilities.LogExceptionDetails(context.Log, context.CachedCommand, resultData.FailureException);
								resultData.Mailboxes.Clear();
								return null;
							}
							context.Log.Assert(!string.IsNullOrEmpty(imapMailboxNameFromWireName), "GetImapMailboxNameFromWireName should either return null for failure or non-emty string for success", new object[0]);
							imapMailbox.Name = imapMailboxNameFromWireName;
							if (value == 1 && string.Equals(imapMailbox.Name, ImapMailbox.Inbox, StringComparison.InvariantCultureIgnoreCase))
							{
								flag = true;
							}
						}
					}
				}
				if (value == 1 && !flag)
				{
					foreach (ImapMailbox imapMailbox2 in resultData.Mailboxes)
					{
						imapMailbox2.Name = ImapMailbox.Inbox + c.ToString() + imapMailbox2.Name;
					}
					ImapMailbox imapMailbox3 = new ImapMailbox(context.RootFolderPath.TrimEnd(new char[]
					{
						c
					}));
					imapMailbox3.Name = ImapMailbox.Inbox;
					resultData.Mailboxes.Insert(0, imapMailbox3);
					context.UpdateRootPathProcessingFlags(ImapRootPathProcessingFlags.FolderPathPrefixIsInbox);
					context.Log.Debug("Treating the RootFolderPath prefix {0} as being the user's Inbox", new object[]
					{
						imapMailbox3.NameOnTheWire
					});
				}
			}
			return resultData.Mailboxes;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000560C File Offset: 0x0000380C
		private static void OnEndLogOffInternal(IAsyncResult asyncResult)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005610 File Offset: 0x00003810
		private static IAsyncResult CreateAsyncResultAndBeginCommand<TOutput>(ImapConnectionContext context, ImapCommand commandToExecute, bool processResponse, AsyncCallback commandCompletionCallback, AsyncCallback externalCallback, object externalCallbackState) where TOutput : class
		{
			AsyncResult<ImapConnectionContext, TOutput> asyncResult = new AsyncResult<ImapConnectionContext, TOutput>(context, context, externalCallback, externalCallbackState);
			asyncResult.PendingAsyncResult = context.NetworkFacade.BeginCommand(commandToExecute, processResponse, context, commandCompletionCallback, asyncResult);
			return asyncResult;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005640 File Offset: 0x00003840
		private static void ProcessResultAndCompleteRequest<TOutput>(IAsyncResult asyncResult, ImapConnectionCore.ResultConverter<TOutput> resultConverter) where TOutput : class
		{
			AsyncResult<ImapConnectionContext, TOutput> asyncResult2 = (AsyncResult<ImapConnectionContext, TOutput>)asyncResult.AsyncState;
			AsyncOperationResult<ImapResultData> asyncOperationResult = asyncResult2.State.NetworkFacade.EndCommand(asyncResult);
			if (!asyncOperationResult.IsSucceeded)
			{
				ImapUtilities.LogExceptionDetails(asyncResult2.State.Log, asyncResult2.State.CachedCommand, asyncOperationResult.Exception);
				asyncResult2.ProcessCompleted(asyncOperationResult.Exception);
				return;
			}
			ImapResultData data = asyncOperationResult.Data;
			TOutput result = default(TOutput);
			if (data.Status == ImapStatus.Ok)
			{
				result = resultConverter(data, asyncResult2.State);
			}
			else
			{
				data.FailureException = ImapConnectionCore.BuildFailureException(asyncResult2.State.CachedCommand, data.Status);
			}
			if (data.FailureException == null)
			{
				asyncResult2.State.Log.Debug("{0}: Command completed successfully.", new object[]
				{
					asyncResult2.State.CachedCommand.ToPiiCleanString()
				});
			}
			else
			{
				ImapUtilities.LogExceptionDetails(asyncResult2.State.Log, asyncResult2.State.CachedCommand, data.FailureException);
			}
			asyncResult2.ProcessCompleted(result, data.FailureException);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005750 File Offset: 0x00003950
		private static string GetImapMailboxNameFromWireName(ImapConnectionContext context, string mailboxName, char separator)
		{
			if (string.IsNullOrEmpty(context.RootFolderPath))
			{
				return mailboxName;
			}
			if ((context.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.UnableToProcess) == ImapRootPathProcessingFlags.UnableToProcess)
			{
				context.Log.Debug("Wire name {0} not translated. Unable to process server responses.", new object[]
				{
					mailboxName
				});
				return null;
			}
			context.Log.Assert((context.ImapRootPathProcessingFlags & (ImapRootPathProcessingFlags.FlagsInitialized | ImapRootPathProcessingFlags.FlagsDetermined)) == (ImapRootPathProcessingFlags.FlagsInitialized | ImapRootPathProcessingFlags.FlagsDetermined), "The ImapRootPathProcessingFlags should already be initialized and determined when processing a mailbox name", new object[0]);
			string text = null;
			if ((context.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.FolderPathPrefixIsInbox) == ImapRootPathProcessingFlags.FolderPathPrefixIsInbox)
			{
				if ((context.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.ResponseIncludesRootPathPrefix) == ImapRootPathProcessingFlags.ResponseIncludesRootPathPrefix)
				{
					if (!mailboxName.StartsWith(context.RootFolderPath))
					{
						context.Log.Debug("Wire name {0} not translated. It does not begin with expected prefix {1}", new object[]
						{
							mailboxName,
							context.RootFolderPath
						});
						return null;
					}
					text = ImapMailbox.Inbox + separator.ToString() + mailboxName.Substring(context.RootFolderPath.Length);
				}
				else
				{
					text = ImapMailbox.Inbox + separator.ToString() + mailboxName;
				}
			}
			else if ((context.ImapRootPathProcessingFlags & ImapRootPathProcessingFlags.ResponseIncludesRootPathPrefix) == ImapRootPathProcessingFlags.ResponseIncludesRootPathPrefix)
			{
				if (!mailboxName.StartsWith(context.RootFolderPath))
				{
					context.Log.Debug("Wire name {0} not translated. It does not begin with expected prefix {1}", new object[]
					{
						mailboxName,
						context.RootFolderPath
					});
					return null;
				}
				text = mailboxName.Substring(context.RootFolderPath.Length);
				if (string.IsNullOrEmpty(text))
				{
					context.Log.Debug("Wire name {0} not translated. Removing the prefix {1} makes it an empty string", new object[]
					{
						mailboxName,
						context.RootFolderPath
					});
					return null;
				}
			}
			context.Log.Debug("Wire name {0} translated to actual name {1}", new object[]
			{
				mailboxName,
				text
			});
			return text;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000058F4 File Offset: 0x00003AF4
		private static Exception BuildFailureException(ImapCommand command, ImapStatus completionCode)
		{
			switch (completionCode)
			{
			case ImapStatus.No:
			{
				string msg = string.Format(CultureInfo.InvariantCulture, "Error while executing [{0}]", new object[]
				{
					(command == null) ? "Initial handshake" : command.ToPiiCleanString()
				});
				return new ItemLevelTransientException(msg);
			}
			case ImapStatus.Bad:
			{
				Exception innerException = ImapConnectionCore.BuildBadResponseException(command);
				return new ItemLevelPermanentException(LocalizedString.Empty, innerException);
			}
			case ImapStatus.Bye:
				return new ImapConnectionException(CXStrings.ImapServerDisconnected, RetryPolicy.Backoff);
			default:
			{
				string message = "Unknown response failure code: " + completionCode;
				throw new InvalidOperationException(message);
			}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005990 File Offset: 0x00003B90
		private static Exception BuildBadResponseException(ImapCommand command)
		{
			string failureReason = string.Format(CultureInfo.InvariantCulture, "Error while executing [{0}]", new object[]
			{
				(command == null) ? "Initial handshake" : command.ToPiiCleanString()
			});
			return new ImapBadResponseException(failureReason);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000059D0 File Offset: 0x00003BD0
		public static AsyncOperationResult<DBNull> ConnectAndAuthenticate(ImapConnectionContext connectionContext, IServerCapabilities requiredCapabilities = null, AsyncCallback callback = null, object callbackState = null)
		{
			AsyncOperationResult<DBNull> asyncOperationResult = ImapConnectionCore.ConnectAndAuthenticate(connectionContext, callback, callbackState);
			if (!asyncOperationResult.IsSucceeded)
			{
				connectionContext.Log.Debug("Imap.ConnectAndAuthenticate failed, ex: {0}.", new object[]
				{
					asyncOperationResult.Exception
				});
				throw asyncOperationResult.Exception;
			}
			if (requiredCapabilities == null)
			{
				return asyncOperationResult;
			}
			AsyncOperationResult<ImapServerCapabilities> asyncOperationResult2 = ImapConnectionCore.Capabilities(connectionContext, callback, callbackState);
			if (!asyncOperationResult2.IsSucceeded)
			{
				connectionContext.Log.Debug("ImapConnectionCore.ConnectAndAuthenticate.Capabilities ex: ex: {0}", new object[]
				{
					asyncOperationResult2.Exception
				});
				throw asyncOperationResult2.Exception;
			}
			ImapServerCapabilities data = asyncOperationResult2.Data;
			if (data.Supports(requiredCapabilities))
			{
				return asyncOperationResult;
			}
			IEnumerable<string> values = requiredCapabilities.NotIn(data);
			string text = string.Format("Missing capabilities: {0}", string.Join(", ", values));
			connectionContext.Log.Debug("ImapConnectionCore.ConnectAndAuthenticate, missing capabilities: {0}", new object[]
			{
				text
			});
			throw new MissingCapabilitiesException(text);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005AB2 File Offset: 0x00003CB2
		public static AsyncOperationResult<DBNull> ConnectAndAuthenticate(ImapConnectionContext connectionContext, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndConnectAndAuthenticate(ImapConnectionCore.BeginConnectAndAuthenticate(connectionContext, callback, callbackState));
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005AC1 File Offset: 0x00003CC1
		public static AsyncOperationResult<ImapServerCapabilities> Capabilities(ImapConnectionContext connectionContext, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndCapabilities(ImapConnectionCore.BeginCapabilities(connectionContext, callback, callbackState));
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public static AsyncOperationResult<DBNull> Expunge(ImapConnectionContext connectionContext, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndExpunge(ImapConnectionCore.BeginExpunge(connectionContext, callback, callbackState));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005ADF File Offset: 0x00003CDF
		public static AsyncOperationResult<ImapMailbox> SelectImapMailbox(ImapConnectionContext connectionContext, ImapMailbox imapMailbox, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndSelectImapMailbox(ImapConnectionCore.BeginSelectImapMailbox(connectionContext, imapMailbox, callback, callbackState));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005AEF File Offset: 0x00003CEF
		public static AsyncOperationResult<ImapResultData> GetMessageInfoByRange(ImapConnectionContext connectionContext, string start, string end, bool uidFetch, IList<string> messageDataItems, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndGetMessageInfoByRange(ImapConnectionCore.BeginGetMessageInfoByRange(connectionContext, start, end, uidFetch, messageDataItems, callback, callbackState));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005B05 File Offset: 0x00003D05
		public static AsyncOperationResult<ImapResultData> GetMessageItemByUid(ImapConnectionContext connectionContext, string uid, IList<string> messageBodyDataItems, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndGetMessageItemByUid(ImapConnectionCore.BeginGetMessageItemByUid(connectionContext, uid, messageBodyDataItems, callback, callbackState));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005B17 File Offset: 0x00003D17
		public static AsyncOperationResult<string> AppendMessageToImapMailbox(ImapConnectionContext connectionContext, string mailboxName, ImapMailFlags messageFlags, Stream messageMimeStream, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndAppendMessageToImapMailbox(ImapConnectionCore.BeginAppendMessageToImapMailbox(connectionContext, mailboxName, messageFlags, messageMimeStream, callback, callbackState));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005B2B File Offset: 0x00003D2B
		public static AsyncOperationResult<IList<string>> SearchForMessageByMessageId(ImapConnectionContext connectionContext, string messageId, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndSearchForMessageByMessageId(ImapConnectionCore.BeginSearchForMessageByMessageId(connectionContext, messageId, callback, callbackState));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005B3B File Offset: 0x00003D3B
		public static AsyncOperationResult<DBNull> StoreMessageFlags(ImapConnectionContext connectionContext, string uid, ImapMailFlags flagsToStore, ImapMailFlags previousFlags, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndStoreMessageFlags(ImapConnectionCore.BeginStoreMessageFlags(connectionContext, uid, flagsToStore, previousFlags, callback, callbackState));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005B4F File Offset: 0x00003D4F
		public static AsyncOperationResult<DBNull> CreateImapMailbox(ImapConnectionContext connectionContext, string mailboxName, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndCreateImapMailbox(ImapConnectionCore.BeginCreateImapMailbox(connectionContext, mailboxName, callback, callbackState));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005B5F File Offset: 0x00003D5F
		public static AsyncOperationResult<DBNull> DeleteImapMailbox(ImapConnectionContext connectionContext, string mailboxName, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndDeleteImapMailbox(ImapConnectionCore.BeginDeleteImapMailbox(connectionContext, mailboxName, callback, callbackState));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005B6F File Offset: 0x00003D6F
		public static AsyncOperationResult<DBNull> RenameImapMailbox(ImapConnectionContext connectionContext, string oldMailboxName, string newMailboxName, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndRenameImapMailbox(ImapConnectionCore.BeginRenameImapMailbox(connectionContext, oldMailboxName, newMailboxName, callback, callbackState));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005B81 File Offset: 0x00003D81
		public static AsyncOperationResult<IList<ImapMailbox>> ListImapMailboxesByLevel(ImapConnectionContext connectionContext, int level, char separator, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndListImapMailboxesByLevel(ImapConnectionCore.BeginListImapMailboxesByLevel(connectionContext, level, separator, callback, callbackState));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005B93 File Offset: 0x00003D93
		public static AsyncOperationResult<DBNull> LogOff(ImapConnectionContext connectionContext, AsyncCallback callback = null, object callbackState = null)
		{
			return ImapConnectionCore.EndLogOff(ImapConnectionCore.BeginLogOff(connectionContext, callback, callbackState));
		}

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x060000D0 RID: 208
		private delegate TOutput ResultConverter<TOutput>(ImapResultData resultData, ImapConnectionContext context);
	}
}
