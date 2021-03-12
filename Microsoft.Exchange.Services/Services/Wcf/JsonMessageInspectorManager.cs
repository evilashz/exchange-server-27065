using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DB8 RID: 3512
	internal class JsonMessageInspectorManager : MessageInspectorManager
	{
		// Token: 0x06005968 RID: 22888 RVA: 0x00117BDF File Offset: 0x00115DDF
		internal JsonMessageInspectorManager()
		{
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x00117BE8 File Offset: 0x00115DE8
		protected override object InternalAfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext, MessageBuffer buffer)
		{
			string text = "http://schemas.microsoft.com/exchange/services/2006/messages";
			string methodName = JsonMessageInspectorManager.GetMethodName(request);
			request.Headers.Action = string.Format("{0}/{1}", text, methodName);
			JsonMessageHeaderProcessor jsonMessageHeaderProcessor = new JsonMessageHeaderProcessor();
			if (JsonMessageInspectorManager.RequestNeedsHeaderProcessing(methodName))
			{
				if (buffer == null)
				{
					buffer = base.CreateMessageBuffer(request);
				}
				Message request2 = buffer.CreateMessage();
				jsonMessageHeaderProcessor.ProcessMessageHeaders(request2);
				request = buffer.CreateMessage();
			}
			request.Properties.Add("MethodName", methodName);
			request.Properties.Add("MethodNamespace", text);
			request.Properties.Add("MessageHeaderProcessor", jsonMessageHeaderProcessor);
			request.Properties.Add("ConnectionCostType", CostType.Connection);
			if (JsonMessageInspectorManager.IsOperationGetUserPhotoViaHttpGet(request))
			{
				request.Properties.Add("WebMethodEntry", WebMethodMetadata.Entries["GetUserPhoto:GET"]);
			}
			else
			{
				request.Properties.Add("WebMethodEntry", WebMethodEntry.JsonWebMethodEntry);
			}
			return base.InternalAfterReceiveRequest(ref request, channel, instanceContext, buffer);
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x00117CE8 File Offset: 0x00115EE8
		protected override void InternalBeforeSendReply(ref Message reply, object correlationState)
		{
			base.InternalBeforeSendReply(ref reply, correlationState);
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x00117CF2 File Offset: 0x00115EF2
		protected override void AddRequestResponseInspectors()
		{
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x00117CF4 File Offset: 0x00115EF4
		private static string GetMethodName(Message request)
		{
			string result = "Json";
			object obj;
			if (request.Properties.TryGetValue("HttpOperationName", out obj) && obj is string)
			{
				result = (string)obj;
			}
			return result;
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x00117D2B File Offset: 0x00115F2B
		private static bool RequestNeedsHeaderProcessing(string methodName)
		{
			return !string.IsNullOrEmpty(methodName) && !JsonMessageInspectorManager.noHeaderProcessingMethodMap.Value.Contains(methodName.ToLowerInvariant());
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x00117D50 File Offset: 0x00115F50
		private static bool IsOperationGetUserPhotoViaHttpGet(Message request)
		{
			if (request == null)
			{
				return false;
			}
			if (!"GetUserPhoto".Equals(JsonMessageInspectorManager.GetMethodName(request), StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			object obj;
			if (!request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj) || obj == null)
			{
				return false;
			}
			HttpRequestMessageProperty httpRequestMessageProperty = obj as HttpRequestMessageProperty;
			return httpRequestMessageProperty != null && "GET".Equals(httpRequestMessageProperty.Method, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04003187 RID: 12679
		private const string GetUserPhotoOperationName = "GetUserPhoto";

		// Token: 0x04003188 RID: 12680
		private const string GetUserPhotoHttpGetMetadataKey = "GetUserPhoto:GET";

		// Token: 0x04003189 RID: 12681
		private static Lazy<HashSet<string>> noHeaderProcessingMethodMap = new Lazy<HashSet<string>>(() => JsonMessageHeaderProcessor.BuildNoHeaderProcessingMap(typeof(JsonService)));
	}
}
