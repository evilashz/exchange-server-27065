using System;
using System.Globalization;
using System.Net.Mime;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001D6 RID: 470
	internal class SipNotifyMwiTarget : MwiTargetBase
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0003C91F File Offset: 0x0003AB1F
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x0003C927 File Offset: 0x0003AB27
		public UMSipPeer Peer { get; private set; }

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003C930 File Offset: 0x0003AB30
		internal SipNotifyMwiTarget(UMSipPeer peer, OrganizationId orgId) : base(peer.ToUMIPGateway(orgId), SipNotifyMwiTarget.instanceNameSuffix)
		{
			this.Peer = peer;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0003C94C File Offset: 0x0003AB4C
		public override void SendMessageAsync(MwiMessage message)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.MWITracer, this.GetHashCode(), "SipNotifyMwiTarget.SendMessageAsync(message={0})", new object[]
			{
				message
			});
			ExAssert.RetailAssert(SipNotifyMwiTarget.initialized, "SipNotifyMwiTarget is not initialized!");
			base.SendMessageAsync(message);
			PlatformSipUri targetUri = this.GetTargetUri(message.UserExtension, this.Peer.Address.ToString(), this.Peer.Port);
			string text = string.Format(CultureInfo.InvariantCulture, "Messages-Waiting: {0}\r\nMessage-Account: {1}\r\nVoice-Message: {2}/{3}\r\n", new object[]
			{
				(message.UnreadVoicemailCount > 0) ? "yes" : "no",
				targetUri,
				message.UnreadVoicemailCount,
				message.TotalVoicemailCount - message.UnreadVoicemailCount
			});
			CallIdTracer.TraceDebug(ExTraceGlobals.MWITracer, this.GetHashCode(), "SipNotifyMwiTarget.SendMessageAsync: sipUri={0} body={1}", new object[]
			{
				targetUri,
				text
			});
			PlatformSignalingHeader[] headers = new PlatformSignalingHeader[]
			{
				Platform.Builder.CreateSignalingHeader("Contact", string.Format(CultureInfo.InvariantCulture, "<{0}>", new object[]
				{
					targetUri.ToString()
				}))
			};
			UmServiceGlobals.VoipPlatform.SendNotifyMessageAsync(targetUri, this.Peer, SipNotifyMwiTarget.messageSummaryType, Encoding.UTF8.GetBytes(text), "message-summary", headers, message);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003CAAC File Offset: 0x0003ACAC
		internal static void Initialize()
		{
			UmServiceGlobals.VoipPlatform.OnSendNotifyMessageCompleted += new VoipPlatformEventHandler<SendNotifyMessageCompletedEventArgs>(SipNotifyMwiTarget.SendNotifyMessageCompleted);
			SipNotifyMwiTarget.initialized = true;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003CACA File Offset: 0x0003ACCA
		internal static void Uninitialize()
		{
			if (SipNotifyMwiTarget.initialized)
			{
				UmServiceGlobals.VoipPlatform.OnSendNotifyMessageCompleted -= new VoipPlatformEventHandler<SendNotifyMessageCompletedEventArgs>(SipNotifyMwiTarget.SendNotifyMessageCompleted);
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003CAEC File Offset: 0x0003ACEC
		private static void SendNotifyMessageCompleted(object sender, SendNotifyMessageCompletedEventArgs args)
		{
			MwiMessage mwiMessage = args.UserState as MwiMessage;
			if (mwiMessage != null)
			{
				TimeSpan timeSpan = ExDateTime.UtcNow.Subtract(mwiMessage.EventTimeUtc);
				CallIdTracer.TraceDebug(ExTraceGlobals.MWITracer, (sender == null) ? 0 : sender.GetHashCode(), "SipNotifyMwiTarget.SendNotifyMessageCompleted(Message={0}. Latency={1}. Error={2})", new object[]
				{
					mwiMessage,
					timeSpan.TotalMilliseconds,
					args.Error
				});
				if (args.ResponseCode == 603 && Utils.IsUriValid(mwiMessage.UserExtension, UMUriType.SipName))
				{
					args.Error = null;
					CallIdTracer.TraceDebug(ExTraceGlobals.MWITracer, 0, "Ignoring 603 error for {0}.", new object[]
					{
						mwiMessage.UserExtension
					});
				}
				if (mwiMessage.CompletionCallback != null)
				{
					MwiDeliveryException error = null;
					if (args.Error != null)
					{
						error = new MwiTargetException(mwiMessage.CurrentTarget.Name, args.ResponseCode, args.ResponseReason ?? args.Error.Message, args.Error);
					}
					else
					{
						MwiDiagnostics.SetCounterValue(GeneralCounters.AverageMWILatency, SipNotifyMwiTarget.averageTotalMwiLatency.Update(timeSpan.TotalMilliseconds));
					}
					SipNotifyMwiTarget sipNotifyMwiTarget = (SipNotifyMwiTarget)mwiMessage.CurrentTarget;
					sipNotifyMwiTarget.UpdatePerformanceCounters(mwiMessage, error);
					mwiMessage.CompletionCallback(mwiMessage, error);
				}
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003CC38 File Offset: 0x0003AE38
		private PlatformSipUri GetTargetUri(string extension, string host, int port)
		{
			PlatformSipUri platformSipUri;
			if (Utils.IsUriValid(extension, UMUriType.SipName))
			{
				platformSipUri = Platform.Builder.CreateSipUri("sip:" + extension);
				platformSipUri.UserParameter = UserParameter.None;
			}
			else
			{
				platformSipUri = Platform.Builder.CreateSipUri(SipUriScheme.Sip, extension, host);
				platformSipUri.UserParameter = UserParameter.Phone;
			}
			platformSipUri.Port = port;
			return platformSipUri;
		}

		// Token: 0x04000AA1 RID: 2721
		private const string MwiBodyFormat = "Messages-Waiting: {0}\r\nMessage-Account: {1}\r\nVoice-Message: {2}/{3}\r\n";

		// Token: 0x04000AA2 RID: 2722
		private const string MwiYes = "yes";

		// Token: 0x04000AA3 RID: 2723
		private const string MwiNo = "no";

		// Token: 0x04000AA4 RID: 2724
		private const string MwiMessageSummaryHeader = "message-summary";

		// Token: 0x04000AA5 RID: 2725
		private static System.Net.Mime.ContentType messageSummaryType = new System.Net.Mime.ContentType("application/simple-message-summary");

		// Token: 0x04000AA6 RID: 2726
		private static MovingAverage averageTotalMwiLatency = new MovingAverage(50);

		// Token: 0x04000AA7 RID: 2727
		private static string instanceNameSuffix = typeof(UMIPGateway).Name;

		// Token: 0x04000AA8 RID: 2728
		private static bool initialized;
	}
}
