using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000234 RID: 564
	public class NotificationServiceClient : IDisposable
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x000476A4 File Offset: 0x000458A4
		static NotificationServiceClient()
		{
			NotificationServiceClient.xmlNamespaces.Add("t", "http://schemas.microsoft.com/exchange/services/2006/types");
			NotificationServiceClient.xmlNamespaces.Add("m", "http://schemas.microsoft.com/exchange/services/2006/messages");
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x000476D8 File Offset: 0x000458D8
		public NotificationServiceClient(string url, string callerData)
		{
			this.url = url;
			this.callerData = callerData;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x000476FC File Offset: 0x000458FC
		public void SendNotificationAsync(SendNotificationResponse sendNotification, NotificationServiceClient.SendNotificationResultCallback resultCallback, object state)
		{
			this.sendNotification = sendNotification;
			this.callback = resultCallback;
			this.asyncState = state;
			this.webRequest = this.PrepareHttpWebRequest();
			AsyncCallback asyncCallback = new AsyncCallback(NotificationServiceClient.GetRequestStreamCallback);
			this.operationTimeoutTimer = new Timer(new TimerCallback(NotificationServiceClient.TimeoutCallback), this, this.timeout, -1);
			try
			{
				this.webRequest.BeginGetRequestStream(asyncCallback, this);
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.PushSubscriptionTracer.TraceError<InvalidOperationException>((long)this.GetHashCode(), "NotificationServiceClient::SendNotificationAsync Exception: {0}", ex);
				this.MakeCallbackAndDispose(this.asyncState, null, ex);
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x000477A0 File Offset: 0x000459A0
		private static void GetRequestStreamCallback(IAsyncResult asyncResult)
		{
			NotificationServiceClient notificationServiceClient = (NotificationServiceClient)asyncResult.AsyncState;
			notificationServiceClient.CreateSendNotificationRequestAsync(asyncResult);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x000477C0 File Offset: 0x000459C0
		private static void GetResponseCallback(IAsyncResult asyncResult)
		{
			NotificationServiceClient notificationServiceClient = (NotificationServiceClient)asyncResult.AsyncState;
			notificationServiceClient.HandleResponse(asyncResult);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x000477E0 File Offset: 0x000459E0
		private static void ResponseStreamReadCallback(IAsyncResult asyncResult)
		{
			NotificationServiceClient notificationServiceClient = asyncResult.AsyncState as NotificationServiceClient;
			Exception ex = null;
			int num = 0;
			lock (notificationServiceClient)
			{
				if (notificationServiceClient.operationTimedOut != null)
				{
					return;
				}
				try
				{
					num = notificationServiceClient.responseStream.EndRead(asyncResult);
				}
				catch (IOException ex2)
				{
					ExTraceGlobals.PushSubscriptionTracer.TraceError<IOException>((long)notificationServiceClient.GetHashCode(), "NotificationServiceClient::ResponseStreamReadCallback: Exception {0}", ex2);
					ex = ex2;
				}
				if (num == 0)
				{
					notificationServiceClient.operationTimedOut = new bool?(false);
				}
			}
			if (ex != null)
			{
				notificationServiceClient.MakeCallbackAndDispose(notificationServiceClient.asyncState, null, ex);
			}
			else
			{
				if (num == 0)
				{
					Exception exception = null;
					SendNotificationResult result = null;
					try
					{
						result = notificationServiceClient.ReadSendNotificationResult();
					}
					catch (InvalidOperationException ex3)
					{
						exception = ex3;
					}
					notificationServiceClient.MakeCallbackAndDispose(notificationServiceClient.asyncState, result, exception);
					return;
				}
				notificationServiceClient.responseBufferBytesRead += num;
				if (notificationServiceClient.responseBufferBytesRead >= notificationServiceClient.responseLimitInBytes)
				{
					notificationServiceClient.MakeCallbackAndDispose(notificationServiceClient.asyncState, null, new InvalidOperationException(string.Format("Response to Push Notification was larger than allowed. Limit : {0} bytes", notificationServiceClient.responseLimitInBytes)));
					return;
				}
				notificationServiceClient.SetUpResponseStreamRead();
				return;
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00047914 File Offset: 0x00045B14
		private static void TimeoutCallback(object state)
		{
			NotificationServiceClient notificationServiceClient = state as NotificationServiceClient;
			lock (notificationServiceClient)
			{
				if (notificationServiceClient.operationTimedOut != null)
				{
					return;
				}
				notificationServiceClient.operationTimedOut = new bool?(true);
			}
			ExTraceGlobals.PushSubscriptionTracer.TraceError((long)notificationServiceClient.GetHashCode(), "NotificationServiceClient::TimeoutCallback: The PushNotification timed out");
			notificationServiceClient.MakeCallbackAndDispose(notificationServiceClient.asyncState, null, new WebException("Request timed out", WebExceptionStatus.Timeout));
			HttpWebRequest httpWebRequest = notificationServiceClient.webRequest;
			httpWebRequest.Abort();
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x000479A8 File Offset: 0x00045BA8
		private void CreateSendNotificationRequestAsync(IAsyncResult requestAsyncResult)
		{
			try
			{
				using (Stream stream = this.webRequest.EndGetRequestStream(requestAsyncResult))
				{
					this.WriteSendNotificationRequestToStream(this.sendNotification, stream);
				}
			}
			catch (WebException webEx)
			{
				this.TraceWebException(webEx, "CreateSendNotificationRequestAsync");
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.PushSubscriptionTracer.TraceError<InvalidOperationException>((long)this.GetHashCode(), "NotificationServiceClient::CreateSendNotificationRequestAsync Exception: {0}", ex);
				this.MakeCallbackAndDispose(this.asyncState, null, ex);
				return;
			}
			AsyncCallback asyncCallback = new AsyncCallback(NotificationServiceClient.GetResponseCallback);
			try
			{
				this.webRequest.BeginGetResponse(asyncCallback, this);
			}
			catch (InvalidOperationException ex2)
			{
				ExTraceGlobals.PushSubscriptionTracer.TraceError<InvalidOperationException>((long)this.GetHashCode(), "NotificationServiceClient::CreateSendNotificationRequestAsync Exception: {0}", ex2);
				this.MakeCallbackAndDispose(this.asyncState, null, ex2);
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00047A90 File Offset: 0x00045C90
		private void HandleResponse(IAsyncResult responseAsyncResult)
		{
			try
			{
				this.webResponse = this.webRequest.EndGetResponse(responseAsyncResult);
				this.responseStream = this.webResponse.GetResponseStream();
				this.SetUpInitialStreamRead();
			}
			catch (WebException webEx)
			{
				this.TraceWebException(webEx, "HandleResponse");
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.PushSubscriptionTracer.TraceError<InvalidOperationException>((long)this.GetHashCode(), "NotificationServiceClient::HandleResponse Exception: {0}", ex);
				this.MakeCallbackAndDispose(this.asyncState, null, ex);
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00047B1C File Offset: 0x00045D1C
		private void TraceWebException(WebException webEx, string source)
		{
			ExTraceGlobals.PushSubscriptionTracer.TraceError<string, WebException>((long)this.GetHashCode(), "NotificationServiceClient::{0} Exception: {1}", source, webEx);
			this.MakeCallbackAndDispose(this.asyncState, null, webEx);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00047B44 File Offset: 0x00045D44
		private void SetUpInitialStreamRead()
		{
			this.responseBufferBytesRead = 0;
			this.responseBuffer = new byte[this.responseLimitInBytes + 1];
			this.SetUpResponseStreamRead();
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00047B68 File Offset: 0x00045D68
		private IAsyncResult SetUpResponseStreamRead()
		{
			int count = this.responseLimitInBytes - this.responseBufferBytesRead + 1;
			AsyncCallback asyncCallback = new AsyncCallback(NotificationServiceClient.ResponseStreamReadCallback);
			return this.responseStream.BeginRead(this.responseBuffer, this.responseBufferBytesRead, count, asyncCallback, this);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00047BAC File Offset: 0x00045DAC
		private void WriteSendNotificationRequestToStream(SendNotificationResponse requestData, Stream requestStream)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(requestStream, new XmlWriterSettings
			{
				Encoding = new UTF8Encoding(false)
			}))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(NotificationRequestServerVersion));
				XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(SendNotificationResponse));
				xmlWriter.WriteStartElement("soap11", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
				xmlWriter.WriteStartElement("soap11", "Header", "http://schemas.xmlsoap.org/soap/envelope/");
				if (this.RequestServerVersionValue != null)
				{
					xmlSerializer.Serialize(xmlWriter, this.RequestServerVersionValue, NotificationServiceClient.xmlNamespaces);
				}
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("soap11", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
				xmlSerializer2.Serialize(xmlWriter, requestData, NotificationServiceClient.xmlNamespaces);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00047C88 File Offset: 0x00045E88
		private HttpWebRequest PrepareHttpWebRequest()
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.url);
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.Timeout = this.timeout;
			httpWebRequest.ContentType = "text/xml; charset=utf-8";
			httpWebRequest.Method = "POST";
			httpWebRequest.Accept = "text/xml";
			httpWebRequest.ServicePoint.Expect100Continue = false;
			httpWebRequest.KeepAlive = false;
			if (!string.IsNullOrEmpty(this.callerData))
			{
				httpWebRequest.Headers.Add("CallerData", this.callerData);
			}
			httpWebRequest.Headers.Add("SOAPAction", "http://schemas.microsoft.com/exchange/services/2006/messages/SendNotification");
			return httpWebRequest;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00047D28 File Offset: 0x00045F28
		private SendNotificationResult ReadSendNotificationResult()
		{
			SendNotificationResult result;
			using (this.responseStream = new MemoryStream(this.responseBuffer))
			{
				XmlReader xmlReader = this.CreateXmlReader(this.responseStream);
				result = this.ParseNotificationResults(xmlReader);
			}
			return result;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00047D7C File Offset: 0x00045F7C
		private SendNotificationResult ParseNotificationResults(XmlReader xmlReader)
		{
			SendNotificationResult sendNotificationResult = new SendNotificationResult();
			try
			{
				xmlReader.Read();
				while (!xmlReader.EOF)
				{
					if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "SubscriptionStatus")
					{
						try
						{
							string value = xmlReader.ReadString();
							sendNotificationResult.SubscriptionStatus = (SubscriptionStatus)Enum.Parse(typeof(SubscriptionStatus), value);
							break;
						}
						catch (ArgumentException)
						{
							sendNotificationResult.SubscriptionStatus = SubscriptionStatus.Invalid;
							break;
						}
					}
					xmlReader.Read();
				}
			}
			catch (XmlException innerException)
			{
				throw new InvalidOperationException("Malformed SOAP response", innerException);
			}
			return sendNotificationResult;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00047E1C File Offset: 0x0004601C
		private XmlReader CreateXmlReader(Stream stream)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
			xmlReaderSettings.IgnoreComments = true;
			xmlReaderSettings.IgnoreWhitespace = true;
			XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(stream);
			xmlTextReader.Normalization = false;
			return XmlReader.Create(xmlTextReader, xmlReaderSettings);
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00047E5B File Offset: 0x0004605B
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x00047E63 File Offset: 0x00046063
		public NotificationRequestServerVersion RequestServerVersionValue
		{
			get
			{
				return this.requestServerVersionValueField;
			}
			set
			{
				this.requestServerVersionValueField = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x00047E6C File Offset: 0x0004606C
		public ICredentials Credentials
		{
			set
			{
				this.credentials = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x00047E75 File Offset: 0x00046075
		public int Timeout
		{
			set
			{
				this.timeout = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00047E7E File Offset: 0x0004607E
		public int ResponseLimitInBytes
		{
			set
			{
				this.responseLimitInBytes = value;
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00047E88 File Offset: 0x00046088
		private void MakeCallbackAndDispose(object state, SendNotificationResult result, Exception exception)
		{
			NotificationServiceClient.SendNotificationResultCallback sendNotificationResultCallback = null;
			lock (this)
			{
				if (this.callback == null)
				{
					return;
				}
				sendNotificationResultCallback = this.callback;
				this.callback = null;
			}
			if (this.operationTimeoutTimer != null)
			{
				this.operationTimeoutTimer.Change(-1, -1);
			}
			sendNotificationResultCallback(state, result, exception);
			this.Dispose();
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00047F00 File Offset: 0x00046100
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00047F0C File Offset: 0x0004610C
		private void Dispose(bool isDisposing)
		{
			if (this.webRequest != null)
			{
				this.webRequest.Abort();
			}
			if (this.webResponse != null)
			{
				this.webResponse.Close();
			}
			if (this.operationTimeoutTimer != null)
			{
				this.operationTimeoutTimer.Dispose();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000B34 RID: 2868
		private const string SOAPEnvelopeElementName = "Envelope";

		// Token: 0x04000B35 RID: 2869
		private const string SOAPHeaderElementName = "Header";

		// Token: 0x04000B36 RID: 2870
		private const string SOAPBodyElementName = "Body";

		// Token: 0x04000B37 RID: 2871
		private const string SubscriptionStatusElementName = "SubscriptionStatus";

		// Token: 0x04000B38 RID: 2872
		private const string SOAPActionHeaderName = "SOAPAction";

		// Token: 0x04000B39 RID: 2873
		private const string CallerDataHeaderName = "CallerData";

		// Token: 0x04000B3A RID: 2874
		private const string SOAPActionHeaderValue = "http://schemas.microsoft.com/exchange/services/2006/messages/SendNotification";

		// Token: 0x04000B3B RID: 2875
		private readonly string callerData;

		// Token: 0x04000B3C RID: 2876
		private static XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();

		// Token: 0x04000B3D RID: 2877
		private NotificationRequestServerVersion requestServerVersionValueField;

		// Token: 0x04000B3E RID: 2878
		private string url;

		// Token: 0x04000B3F RID: 2879
		private ICredentials credentials;

		// Token: 0x04000B40 RID: 2880
		private int timeout;

		// Token: 0x04000B41 RID: 2881
		private int responseLimitInBytes;

		// Token: 0x04000B42 RID: 2882
		private SendNotificationResponse sendNotification;

		// Token: 0x04000B43 RID: 2883
		private NotificationServiceClient.SendNotificationResultCallback callback;

		// Token: 0x04000B44 RID: 2884
		private object asyncState;

		// Token: 0x04000B45 RID: 2885
		private HttpWebRequest webRequest;

		// Token: 0x04000B46 RID: 2886
		private WebResponse webResponse;

		// Token: 0x04000B47 RID: 2887
		private bool? operationTimedOut = null;

		// Token: 0x04000B48 RID: 2888
		private byte[] responseBuffer;

		// Token: 0x04000B49 RID: 2889
		private int responseBufferBytesRead;

		// Token: 0x04000B4A RID: 2890
		private Stream responseStream;

		// Token: 0x04000B4B RID: 2891
		private Timer operationTimeoutTimer;

		// Token: 0x02000235 RID: 565
		// (Invoke) Token: 0x06000EB0 RID: 3760
		public delegate void SendNotificationResultCallback(object state, SendNotificationResult result, Exception exception);
	}
}
