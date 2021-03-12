using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001EF RID: 495
	internal class OWADispatchOperationSelector : WebHttpDispatchOperationSelector
	{
		// Token: 0x06001180 RID: 4480 RVA: 0x000431D6 File Offset: 0x000413D6
		internal OWADispatchOperationSelector(ServiceEndpoint endpoint) : base(endpoint)
		{
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000431E0 File Offset: 0x000413E0
		protected override string SelectOperation(ref Message message, out bool uriMatched)
		{
			string text = base.SelectOperation(ref message, out uriMatched);
			HttpRequestMessageProperty httpRequestMessageProperty = (HttpRequestMessageProperty)message.Properties[HttpRequestMessageProperty.Name];
			if (uriMatched)
			{
				httpRequestMessageProperty.Headers[OWADispatchOperationSelector.Action] = text;
				return text;
			}
			if (httpRequestMessageProperty == null)
			{
				throw new FaultException(Strings.MissingHttpRequestMessageProperty);
			}
			text = httpRequestMessageProperty.Headers[OWADispatchOperationSelector.Action];
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			throw new FaultException(Strings.MissingActionHeader);
		}

		// Token: 0x04000A50 RID: 2640
		internal static readonly string Action = "Action";
	}
}
