using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000249 RID: 585
	internal class ProxyHeaderXmlWriter
	{
		// Token: 0x06000F6C RID: 3948 RVA: 0x0004BD6C File Offset: 0x00049F6C
		public static void AddProxyHeader(Message message, ProxyHeaderValue proxyHeaderValue)
		{
			ProxyHeaderXmlWriter.RemoveAnyWSHeaders(message);
			ProxyHeaderXmlWriter.RemoveAnyProxyHeaders(message);
			string text = null;
			switch (proxyHeaderValue.ProxyHeaderType)
			{
			case ProxyHeaderType.SuggesterSid:
				text = "ProxySuggesterSid";
				break;
			case ProxyHeaderType.FullToken:
				text = "ProxySecurityContext";
				break;
			case ProxyHeaderType.PartnerToken:
				text = "ProxyPartnerToken";
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				MessageHeader header = MessageHeader.CreateHeader(text, "http://schemas.microsoft.com/exchange/services/2006/types", Convert.ToBase64String(proxyHeaderValue.Value));
				message.Headers.Add(header);
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0004BDE3 File Offset: 0x00049FE3
		public static void RemoveProxyHeaders(Message message)
		{
			ProxyHeaderXmlWriter.RemoveAnyWSHeaders(message);
			ProxyHeaderXmlWriter.RemoveAnyProxyHeaders(message);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0004BDF1 File Offset: 0x00049FF1
		private static void RemoveAnyWSHeaders(Message message)
		{
			ProxyHeaderXmlWriter.RemoveHeaders(message, "http://schemas.microsoft.com/ws/2005/05/addressing/none");
			ProxyHeaderXmlWriter.RemoveHeaders(message, "http://www.w3.org/2005/08/addressing");
			ProxyHeaderXmlWriter.RemoveHeaders(message, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0004BE14 File Offset: 0x0004A014
		private static void RemoveHeaders(Message message, string xmlNamespace)
		{
			List<string> list = new List<string>(message.Headers.Count);
			foreach (MessageHeaderInfo messageHeaderInfo in message.Headers)
			{
				if (messageHeaderInfo.Namespace == xmlNamespace)
				{
					list.Add(messageHeaderInfo.Name);
				}
			}
			foreach (string name in list)
			{
				message.Headers.RemoveAll(name, xmlNamespace);
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0004BECC File Offset: 0x0004A0CC
		private static void RemoveAnyProxyHeaders(Message message)
		{
			int count = message.Headers.Count;
			message.Headers.RemoveAll("ProxySecurityContext", "http://schemas.microsoft.com/exchange/services/2006/types");
			message.Headers.RemoveAll("ProxySuggesterSid", "http://schemas.microsoft.com/exchange/services/2006/types");
			message.Headers.RemoveAll("ProxyPartnerToken", "http://schemas.microsoft.com/exchange/services/2006/types");
		}
	}
}
