using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.PushNotifications.Server.Wcf
{
	// Token: 0x02000028 RID: 40
	internal class PublishUserNotificationBehavior : WebHttpBehavior
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000044A2 File Offset: 0x000026A2
		protected override IDispatchMessageFormatter GetRequestDispatchFormatter(OperationDescription operationDescription, ServiceEndpoint endpoint)
		{
			return new PublishUserNotificationBehavior.PublishUserNotificationMessageFormatter(base.GetRequestDispatchFormatter(operationDescription, endpoint));
		}

		// Token: 0x02000029 RID: 41
		private class PublishUserNotificationMessageFormatter : IDispatchMessageFormatter
		{
			// Token: 0x06000102 RID: 258 RVA: 0x000044B9 File Offset: 0x000026B9
			public PublishUserNotificationMessageFormatter(IDispatchMessageFormatter innerFormatter)
			{
				this.innerFormatter = innerFormatter;
			}

			// Token: 0x06000103 RID: 259 RVA: 0x000044C8 File Offset: 0x000026C8
			public void DeserializeRequest(Message message, object[] parameters)
			{
				this.innerFormatter.DeserializeRequest(message, parameters);
				RemoteUserNotification remoteUserNotification = parameters[0] as RemoteUserNotification;
				HttpRequestMessageProperty httpRequestMessageProperty = message.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
				if (remoteUserNotification != null && httpRequestMessageProperty != null)
				{
					remoteUserNotification.Payload.SetUserId(httpRequestMessageProperty.Headers["X-PUN-UserId"]);
					remoteUserNotification.Payload.SetTenantId(httpRequestMessageProperty.Headers["X-PUN-TenantId"]);
				}
			}

			// Token: 0x06000104 RID: 260 RVA: 0x0000453D File Offset: 0x0000273D
			public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
			{
				return this.innerFormatter.SerializeReply(messageVersion, parameters, result);
			}

			// Token: 0x04000062 RID: 98
			private IDispatchMessageFormatter innerFormatter;
		}
	}
}
