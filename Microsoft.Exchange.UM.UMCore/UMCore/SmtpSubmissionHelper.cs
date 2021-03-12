using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Exchange.UM.ExSmtpClient;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002E3 RID: 739
	internal static class SmtpSubmissionHelper
	{
		// Token: 0x0600167E RID: 5758 RVA: 0x0005F950 File Offset: 0x0005DB50
		public static void SubmitMessage(MessageItem message, string senderAddress, Guid senderOrgGuid, string recipientAddress, OutboundConversionOptions submissionConversionOptions, string requestId)
		{
			ValidateArgument.NotNull(message, "message");
			ValidateArgument.NotNull(senderAddress, "sender");
			ValidateArgument.NotNull(recipientAddress, "recipient");
			ValidateArgument.NotNull(submissionConversionOptions, "submissionConversionOptions");
			ValidateArgument.NotNull(requestId, "requestId");
			InternalExchangeServer internalExchangeServer = null;
			try
			{
				lock (SmtpSubmissionHelper.bridgeheadPicker)
				{
					internalExchangeServer = SmtpSubmissionHelper.bridgeheadPicker.PickNextServer(new ADObjectId(Guid.Empty));
				}
				if (internalExchangeServer == null)
				{
					CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "No bridgehead server available to process request '{0}'", new object[]
					{
						requestId
					});
					throw new NoHubTransportServerAvailableException();
				}
				SmtpSubmissionHelper.SubmitMessage(message, senderAddress, senderOrgGuid, recipientAddress, submissionConversionOptions, internalExchangeServer);
				PIIMessage data = PIIMessage.Create(PIIType._User, recipientAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Successfully submitted voice mail message to '_User', request '{0}'", new object[]
				{
					requestId
				});
			}
			catch (Exception e)
			{
				if (!SmtpSubmissionHelper.CheckTransientSmtpFailure(e))
				{
					throw;
				}
				SmtpSubmissionHelper.HandleTransientSmtpFailure(e, internalExchangeServer, recipientAddress);
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0005FA68 File Offset: 0x0005DC68
		private static void SubmitMessage(MessageItem message, string senderAddress, Guid senderOrgGuid, string recipientAddress, OutboundConversionOptions submissionConversionOptions, InternalExchangeServer smtpServer)
		{
			Util.IncrementCounter(AvailabilityCounters.PercentageHubTransportAccessFailures_Base);
			using (SmtpClient smtpClient = new SmtpClient(smtpServer.MachineName, 25, new SmtpClientDebugOutput()))
			{
				smtpClient.AuthCredentials(CredentialCache.DefaultNetworkCredentials);
				smtpClient.From = senderAddress;
				smtpClient.To = new string[]
				{
					recipientAddress
				};
				if (CommonConstants.UseDataCenterCallRouting && senderOrgGuid != Guid.Empty)
				{
					smtpClient.FromParameters.Add(new KeyValuePair<string, string>("XATTRDIRECT", "Originating"));
					smtpClient.FromParameters.Add(new KeyValuePair<string, string>("XATTRORGID", "xorgid:" + senderOrgGuid));
				}
				PIIMessage[] data = new PIIMessage[]
				{
					PIIMessage.Create(PIIType._User, recipientAddress),
					PIIMessage.Create(PIIType._User, senderAddress)
				};
				CallIdTracer.TracePfd(ExTraceGlobals.PFDUMCallAcceptanceTracer, 0, data, "PFD UMS {0} - Submitting voice mail message to  _User1 Sender : _User2 using {1} server ", new object[]
				{
					11578,
					smtpServer.MachineName
				});
				using (MemoryStream memoryStream = new MemoryStream())
				{
					ItemConversion.ConvertItemToSummaryTnef(message, memoryStream, submissionConversionOptions);
					memoryStream.Position = 0L;
					smtpClient.DataStream = memoryStream;
					smtpClient.Submit();
				}
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0005FBD8 File Offset: 0x0005DDD8
		private static bool CheckTransientSmtpFailure(Exception e)
		{
			return e is AlreadyConnectedToSMTPServerException || e is FailedToConnectToSMTPServerException || e is MustBeTlsForAuthException || e is AuthFailureException || e is AuthApiFailureException || e is InvalidSmtpServerResponseException || e is UnexpectedSmtpServerResponseException || e is NotConnectedToSMTPServerException || e is SocketException || (e is IOException && e.InnerException != null && e.InnerException is SocketException);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0005FC54 File Offset: 0x0005DE54
		private static void HandleTransientSmtpFailure(Exception e, InternalExchangeServer smtpServer, string recipientAddress)
		{
			Util.IncrementCounter(AvailabilityCounters.PercentageHubTransportAccessFailures);
			PIIMessage data = PIIMessage.Create(PIIType._User, recipientAddress);
			CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, data, "Failed to submit the message to '_User'. Exception: '{0}'", new object[]
			{
				e
			});
			SmtpSubmissionHelper.bridgeheadPicker.ServerUnavailable(smtpServer);
			throw new SmtpSubmissionException(e);
		}

		// Token: 0x04000D57 RID: 3415
		private const string DirectionalityParam = "XATTRDIRECT";

		// Token: 0x04000D58 RID: 3416
		private const string DirectionalityOriginating = "Originating";

		// Token: 0x04000D59 RID: 3417
		private const string OrganizationIdParam = "XATTRORGID";

		// Token: 0x04000D5A RID: 3418
		private static BridgeheadPicker bridgeheadPicker = new BridgeheadPicker();
	}
}
