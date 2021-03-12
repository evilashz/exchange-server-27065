using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005A8 RID: 1448
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PushNotificationCafeUnexpectedResponse : LocalizedException
	{
		// Token: 0x060026E0 RID: 9952 RVA: 0x000DE15A File Offset: 0x000DC35A
		public PushNotificationCafeUnexpectedResponse(string response, string requestHeaders, string responseHeaders, string body) : base(Strings.PushNotificationCafeUnexpectedResponse(response, requestHeaders, responseHeaders, body))
		{
			this.response = response;
			this.requestHeaders = requestHeaders;
			this.responseHeaders = responseHeaders;
			this.body = body;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000DE189 File Offset: 0x000DC389
		public PushNotificationCafeUnexpectedResponse(string response, string requestHeaders, string responseHeaders, string body, Exception innerException) : base(Strings.PushNotificationCafeUnexpectedResponse(response, requestHeaders, responseHeaders, body), innerException)
		{
			this.response = response;
			this.requestHeaders = requestHeaders;
			this.responseHeaders = responseHeaders;
			this.body = body;
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000DE1BC File Offset: 0x000DC3BC
		protected PushNotificationCafeUnexpectedResponse(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.response = (string)info.GetValue("response", typeof(string));
			this.requestHeaders = (string)info.GetValue("requestHeaders", typeof(string));
			this.responseHeaders = (string)info.GetValue("responseHeaders", typeof(string));
			this.body = (string)info.GetValue("body", typeof(string));
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000DE254 File Offset: 0x000DC454
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("response", this.response);
			info.AddValue("requestHeaders", this.requestHeaders);
			info.AddValue("responseHeaders", this.responseHeaders);
			info.AddValue("body", this.body);
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x000DE2AD File Offset: 0x000DC4AD
		public string Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x000DE2B5 File Offset: 0x000DC4B5
		public string RequestHeaders
		{
			get
			{
				return this.requestHeaders;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x000DE2BD File Offset: 0x000DC4BD
		public string ResponseHeaders
		{
			get
			{
				return this.responseHeaders;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x000DE2C5 File Offset: 0x000DC4C5
		public string Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x04001C7C RID: 7292
		private readonly string response;

		// Token: 0x04001C7D RID: 7293
		private readonly string requestHeaders;

		// Token: 0x04001C7E RID: 7294
		private readonly string responseHeaders;

		// Token: 0x04001C7F RID: 7295
		private readonly string body;
	}
}
