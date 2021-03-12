using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000488 RID: 1160
	internal class RemoteNotificationRequester
	{
		// Token: 0x06002767 RID: 10087 RVA: 0x00091E48 File Offset: 0x00090048
		static RemoteNotificationRequester()
		{
			CertificateValidationManager.RegisterCallback("RemoteNotification", new RemoteCertificateValidationCallback(RemoteNotificationRequester.SslCertificateValidationCallback));
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x00091E80 File Offset: 0x00090080
		public static RemoteNotificationRequester Instance
		{
			get
			{
				if (RemoteNotificationRequester.instance == null)
				{
					RemoteNotificationRequester.instance = new RemoteNotificationRequester();
				}
				return RemoteNotificationRequester.instance;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x00091E98 File Offset: 0x00090098
		public int TotalInTrasitRequests
		{
			get
			{
				return this.totalInTrasitRequests;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x00091EA0 File Offset: 0x000900A0
		public virtual ManualResetEventSlim UnderRequestLimitEvent
		{
			get
			{
				return this.underRequestLimitEvent;
			}
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x0009221C File Offset: 0x0009041C
		public virtual async Task SendNotificationsAsync(PusherQueue queueToProcess)
		{
			bool success = false;
			try
			{
				await Task.Delay(TimeSpan.FromMinutes((double)queueToProcess.FailureCount));
				this.IncrementInTransit();
				PusherQueuePayload[] payloads = queueToProcess.GetPayloads();
				HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(queueToProcess.DestinationUrl));
				webRequest.PreAuthenticate = true;
				CertificateValidationManager.SetComponentId(webRequest, "RemoteNotification");
				webRequest.ContentType = "text/plain";
				webRequest.Method = "POST";
				DateTime pushTime = DateTime.UtcNow;
				await RemoteNotificationRequester.SendRequest(webRequest, RemoteNotificationRequester.GeneratePayload(payloads), queueToProcess.DestinationUrl, payloads.Length);
				await RemoteNotificationRequester.ReceiveResponseAsync(webRequest);
				success = true;
				IEnumerable<NotificationPayloadBase> payloadsToBePushed = from p in payloads
				select p.Payload;
				NotificationStatisticsManager.Instance.NotificationPushed(queueToProcess.DestinationUrl, payloadsToBePushed, pushTime);
			}
			catch (WebException webException)
			{
				RemoteNotificationRequester.HandleWebException(webException, queueToProcess);
			}
			finally
			{
				queueToProcess.SendComplete(success);
				this.DecrementInTransit();
			}
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x00092288 File Offset: 0x00090488
		private static string GeneratePayload(PusherQueuePayload[] payloads)
		{
			RemoteNotificationPayload[] array = (from p in payloads
			select new RemoteNotificationPayload(1, JsonConverter.ToJSON(p.Payload), p.ChannelIds.ToArray<string>())).ToArray<RemoteNotificationPayload>();
			return JsonConverter.ToJSON(array);
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000922C4 File Offset: 0x000904C4
		private void DecrementInTransit()
		{
			int num = Interlocked.Decrement(ref this.totalInTrasitRequests);
			if (num < 50)
			{
				if (!this.underRequestLimitEvent.IsSet)
				{
					OwaServerTraceLogger.AppendToLog(new PusherLogEvent(PusherEventType.ConcurrentLimit)
					{
						OverLimit = false,
						InTransitCount = num
					});
				}
				this.underRequestLimitEvent.Set();
			}
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x00092318 File Offset: 0x00090518
		private void IncrementInTransit()
		{
			int num = Interlocked.Increment(ref this.totalInTrasitRequests);
			if (num >= 50)
			{
				if (this.underRequestLimitEvent.IsSet)
				{
					OwaServerTraceLogger.AppendToLog(new PusherLogEvent(PusherEventType.ConcurrentLimit)
					{
						OverLimit = true,
						InTransitCount = num
					});
				}
				this.underRequestLimitEvent.Reset();
			}
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x000924FC File Offset: 0x000906FC
		private static async Task SendRequest(HttpWebRequest webRequest, string payload, string destination, int payloadCount)
		{
			OwaServerTraceLogger.AppendToLog(new PusherLogEvent(PusherEventType.Push)
			{
				Destination = destination,
				PayloadCount = payloadCount
			});
			byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);
			using (Stream requestStream = await webRequest.GetRequestStreamAsync())
			{
				requestStream.Write(payloadBytes, 0, payloadBytes.Length);
				requestStream.Close();
			}
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x00092778 File Offset: 0x00090978
		private static async Task ReceiveResponseAsync(HttpWebRequest webRequest)
		{
			StringBuilder responseBuilder;
			using (WebResponse webResponse = await webRequest.GetResponseAsync())
			{
				using (Stream responseStream = webResponse.GetResponseStream())
				{
					responseBuilder = await BufferedStreamReader.ReadAsync(responseStream);
					responseStream.Close();
					webResponse.Close();
				}
			}
			RemoteNotificationRequester.ProcessResponse(responseBuilder.ToString());
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000927C0 File Offset: 0x000909C0
		private static void ProcessResponse(string responseString)
		{
			if (!string.IsNullOrWhiteSpace(responseString))
			{
				string[] array = responseString.Split(RemoteNotificationRequester.ChannelIdSeparators, StringSplitOptions.None);
				foreach (string channelId in array)
				{
					RemoteNotificationManager.Instance.CleanUpChannel(channelId);
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>(0L, "Pusher removed explicitly lost channels. LostChannelIds: {0}", responseString);
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x00092814 File Offset: 0x00090A14
		private static void HandleWebException(WebException webException, PusherQueue queue)
		{
			OwaServerTraceLogger.AppendToLog(new PusherLogEvent(PusherEventType.PushFailed)
			{
				Destination = queue.DestinationUrl,
				HandledException = webException
			});
			HttpWebResponse httpWebResponse = webException.Response as HttpWebResponse;
			if (httpWebResponse != null)
			{
				httpWebResponse.Close();
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x00092856 File Offset: 0x00090A56
		private static bool SslCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		// Token: 0x040016F9 RID: 5881
		public const string ChannelIdsHeaderName = "CIDs";

		// Token: 0x040016FA RID: 5882
		public const string NotificationsCountHeaderName = "NotifCount";

		// Token: 0x040016FB RID: 5883
		public const string ChannelIdSeparator = ",";

		// Token: 0x040016FC RID: 5884
		private const int MaxTotalInTransitRequests = 50;

		// Token: 0x040016FD RID: 5885
		private const string CertificateValidationComponentId = "RemoteNotification";

		// Token: 0x040016FE RID: 5886
		private static readonly string[] ChannelIdSeparators = new string[]
		{
			","
		};

		// Token: 0x040016FF RID: 5887
		private static RemoteNotificationRequester instance;

		// Token: 0x04001700 RID: 5888
		private int totalInTrasitRequests;

		// Token: 0x04001701 RID: 5889
		public ManualResetEventSlim underRequestLimitEvent = new ManualResetEventSlim(true);
	}
}
