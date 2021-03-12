using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004D1 RID: 1233
	[OwaEventSegmentation(Feature.UMIntegration)]
	[OwaEventNamespace("ReadVMessage")]
	internal sealed class ReadVoiceMessageEventHandler : MessageEventHandler
	{
		// Token: 0x06002F00 RID: 12032 RVA: 0x0010E7A4 File Offset: 0x0010C9A4
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(ReadVoiceMessageEventHandler));
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x0010E7B5 File Offset: 0x0010C9B5
		[OwaEvent("CallUM")]
		[OwaEventParameter("phNum", typeof(string), false, false)]
		[OwaEventParameter("msgID", typeof(OwaStoreObjectId), false, false)]
		public void CreateUMPhoneCall()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ReadVoiceMessageEventHandler.CreateUMPhoneCall");
			this.PlayOnPhone(false);
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x0010E7D4 File Offset: 0x0010C9D4
		[OwaEventParameter("paaID", typeof(string), false, false)]
		[OwaEventParameter("phNum", typeof(string), false, false)]
		[OwaEvent("CallPaa")]
		public void CreateUMPersonalAutoAttendantPhoneCall()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ReadVoiceMessageEventHandler.CreateUMPersonalAutoAttendantPhoneCall");
			this.PlayOnPhone(true);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x0010E7F4 File Offset: 0x0010C9F4
		private void PlayOnPhone(bool isPAA)
		{
			string text = (string)base.GetParameter("phNum");
			string lastUMCallId = base.UserContext.LastUMCallId;
			using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
			{
				if (!umclientCommon.IsPlayOnPhoneEnabled())
				{
					throw new OwaEventHandlerException("User is not enabled for Play on Phone", LocalizedStrings.GetNonEncoded(1385527863));
				}
				if (!string.IsNullOrEmpty(lastUMCallId))
				{
					UMCallState umcallState = UMCallState.Disconnected;
					try
					{
						UMCallInfo callInfo = umclientCommon.GetCallInfo(lastUMCallId);
						umcallState = callInfo.CallState;
					}
					catch (InvalidCallIdException)
					{
						ExTraceGlobals.UnifiedMessagingTracer.TraceDebug((long)this.GetHashCode(), "Failed to get call status from Unified Messaging");
					}
					catch (Exception exception)
					{
						if (this.HandleUMException(exception))
						{
							return;
						}
						throw;
					}
					if (umcallState != UMCallState.Disconnected)
					{
						base.RenderPartialFailure(460647110, new Strings.IDs?(-937065163), ButtonDialogIcon.Warning);
						return;
					}
					base.UserContext.LastUMCallId = null;
				}
				try
				{
					if (isPAA)
					{
						string s = (string)base.GetParameter("paaID");
						Guid paaIdentity = new Guid(Convert.FromBase64String(s));
						base.UserContext.LastUMCallId = umclientCommon.PlayOnPhonePAAGreeting(paaIdentity, text);
					}
					else
					{
						OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("msgID");
						ExchangePrincipal playOnPhoneExchangePrincipal = this.GetPlayOnPhoneExchangePrincipal(owaStoreObjectId);
						using (UMClientCommon umclientCommon2 = new UMClientCommon(playOnPhoneExchangePrincipal))
						{
							string base64ObjectId = Utilities.ProviderSpecificIdFromStoreObjectId(owaStoreObjectId.StoreObjectId.ToBase64String());
							base.UserContext.LastUMCallId = umclientCommon2.PlayOnPhone(base64ObjectId, text);
						}
					}
					base.UserContext.LastUMPhoneNumber = text;
					if (string.Compare(umclientCommon.GetUMProperties().PlayOnPhoneDialString, text, StringComparison.InvariantCultureIgnoreCase) != 0)
					{
						umclientCommon.SetPlayOnPhoneDialString(text);
					}
				}
				catch (Exception exception2)
				{
					if (this.HandleUMException(exception2))
					{
						return;
					}
					throw;
				}
				this.Writer.Write(base.UserContext.LastUMCallId);
			}
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x0010EA30 File Offset: 0x0010CC30
		[OwaEvent("Hangup")]
		public void HangupUMPhoneCall()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ReadVoiceMessageEventHandler.HangupUMPhoneCall");
			string lastUMCallId = base.UserContext.LastUMCallId;
			using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
			{
				if (!umclientCommon.IsPlayOnPhoneEnabled())
				{
					throw new OwaEventHandlerException("User is not enabled for Play on Phone", LocalizedStrings.GetNonEncoded(1385527863));
				}
				if (!string.IsNullOrEmpty(lastUMCallId))
				{
					try
					{
						UMCallInfo callInfo = umclientCommon.GetCallInfo(lastUMCallId);
						UMCallState callState = callInfo.CallState;
						if (callInfo.CallState != UMCallState.Disconnected)
						{
							umclientCommon.Disconnect(lastUMCallId);
							base.UserContext.LastUMCallId = null;
						}
					}
					catch (InvalidCallIdException)
					{
						ExTraceGlobals.UnifiedMessagingTracer.TraceDebug((long)this.GetHashCode(), "Failed to get call status from Unified Messaging");
					}
					catch (Exception exception)
					{
						if (this.HandleUMException(exception))
						{
							return;
						}
						throw;
					}
				}
				base.UserContext.LastUMCallId = null;
				base.UserContext.LastUMPhoneNumber = null;
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x0010EB38 File Offset: 0x0010CD38
		[OwaEvent("CallStatus")]
		public void GetUMCallStatus()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ReadVoiceMessageEventHandler.GetUMCallStatus");
			string lastUMCallId = base.UserContext.LastUMCallId;
			UMCallState umcallState = UMCallState.Disconnected;
			using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
			{
				if (!umclientCommon.IsPlayOnPhoneEnabled())
				{
					throw new OwaEventHandlerException("User is not enabled for Play on Phone", LocalizedStrings.GetNonEncoded(1385527863));
				}
				if (!string.IsNullOrEmpty(lastUMCallId))
				{
					try
					{
						UMCallInfo callInfo = umclientCommon.GetCallInfo(lastUMCallId);
						umcallState = callInfo.CallState;
					}
					catch (InvalidCallIdException)
					{
						ExTraceGlobals.UnifiedMessagingTracer.TraceDebug((long)this.GetHashCode(), "Failed to get call status from Unified Messaging");
					}
					catch (Exception exception)
					{
						if (this.HandleUMException(exception))
						{
							return;
						}
						throw;
					}
					if (umcallState == UMCallState.Disconnected || umcallState == UMCallState.Idle)
					{
						base.UserContext.LastUMCallId = null;
					}
				}
			}
			TextWriter writer = this.Writer;
			int num = (int)umcallState;
			writer.Write(num.ToString());
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x0010EC38 File Offset: 0x0010CE38
		[OwaEvent("GetAttachExt")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		public void GetAttachmentExtension()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ReadVoiceMessageEventHandler.GetAttachmentExtension");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			Item item = Utilities.GetItem<Item>(base.UserContext, owaStoreObjectId, new PropertyDefinition[0]);
			Exception ex = null;
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			if (rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted && base.UserContext.IsIrmEnabled)
			{
				try
				{
					rightsManagedMessageItem.Decode(Utilities.CreateOutboundConversionOptions(base.UserContext), true);
				}
				catch (RightsManagementPermanentException ex2)
				{
					ex = ex2;
				}
				catch (RightsManagementTransientException ex3)
				{
					ex = ex3;
				}
			}
			if (ex != null)
			{
				throw new OwaEventHandlerException(LocalizedStrings.GetNonEncoded(756278106), LocalizedStrings.GetNonEncoded(-229902107), ex);
			}
			Attachment latestVoiceMailAttachment = Utilities.GetLatestVoiceMailAttachment(item, base.UserContext);
			if (latestVoiceMailAttachment != null)
			{
				string text = latestVoiceMailAttachment.FileExtension;
				if (text == null)
				{
					text = string.Empty;
				}
				Utilities.HtmlEncode(text, this.Writer);
				return;
			}
			throw new OwaEventHandlerException(LocalizedStrings.GetNonEncoded(756278106), LocalizedStrings.GetNonEncoded(-229902107));
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x0010ED4C File Offset: 0x0010CF4C
		private bool HandleUMException(Exception exception)
		{
			if (exception is InvalidCallIdException)
			{
				ExTraceGlobals.UnifiedMessagingTracer.TraceDebug((long)this.GetHashCode(), "Failed to get call status from Unified Messaging");
				return true;
			}
			if (exception is InvalidObjectIdException)
			{
				base.RenderPartialFailure(982875461, new Strings.IDs?(-937065163), ButtonDialogIcon.Warning);
				return true;
			}
			if (exception is DialingRulesException)
			{
				base.RenderPartialFailure(852800569, new Strings.IDs?(-937065163), ButtonDialogIcon.Warning);
				return true;
			}
			if (exception is IPGatewayNotFoundException)
			{
				base.RenderPartialFailure(-600244331, new Strings.IDs?(-937065163), ButtonDialogIcon.Warning);
				return true;
			}
			if (exception is UMServerNotFoundException)
			{
				base.RenderPartialFailure(-620095015, new Strings.IDs?(-937065163), ButtonDialogIcon.Warning);
				return true;
			}
			if (exception is TransportException)
			{
				base.RenderPartialFailure(-2078057245, new Strings.IDs?(-937065163), ButtonDialogIcon.Warning);
				return true;
			}
			if (exception is InvalidPhoneNumberException)
			{
				base.RenderPartialFailure(-74757282, new Strings.IDs?(-937065163), ButtonDialogIcon.Warning);
				return true;
			}
			if (exception is InvalidSipUriException)
			{
				base.RenderPartialFailure(-74757282, new Strings.IDs?(-937065163), ButtonDialogIcon.Warning);
				return true;
			}
			return false;
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x0010EE5C File Offset: 0x0010D05C
		private ExchangePrincipal GetPlayOnPhoneExchangePrincipal(OwaStoreObjectId owaStoreObjectId)
		{
			ExchangePrincipal exchangePrincipal;
			if (owaStoreObjectId.IsOtherMailbox)
			{
				if (!base.UserContext.DelegateSessionManager.TryGetExchangePrincipal(owaStoreObjectId.MailboxOwnerLegacyDN, out exchangePrincipal))
				{
					throw new ObjectNotFoundException(ServerStrings.CannotFindExchangePrincipal);
				}
			}
			else
			{
				exchangePrincipal = base.UserContext.ExchangePrincipal;
			}
			return exchangePrincipal;
		}

		// Token: 0x040020E5 RID: 8421
		public const string EventNamespace = "ReadVMessage";

		// Token: 0x040020E6 RID: 8422
		public const string MethodCreateUMPhoneCall = "CallUM";

		// Token: 0x040020E7 RID: 8423
		public const string MethodCreateUMPersonalAutoAttendantPhoneCall = "CallPaa";

		// Token: 0x040020E8 RID: 8424
		public const string MethodHangupUMPhoneCall = "Hangup";

		// Token: 0x040020E9 RID: 8425
		public const string MethodGetUMCallStatus = "CallStatus";

		// Token: 0x040020EA RID: 8426
		public const string MethodGetAttachmentExtension = "GetAttachExt";

		// Token: 0x040020EB RID: 8427
		public const string PhoneNumber = "phNum";

		// Token: 0x040020EC RID: 8428
		public const string MessageId = "msgID";

		// Token: 0x040020ED RID: 8429
		public const string PersonalAutoAttendantId = "paaID";
	}
}
