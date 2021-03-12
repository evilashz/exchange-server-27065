using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200011B RID: 283
	internal sealed class ConfigResponseUrl
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0002FE60 File Offset: 0x0002E060
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x0002FE68 File Offset: 0x0002E068
		internal string ServiceName { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0002FE71 File Offset: 0x0002E071
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0002FE79 File Offset: 0x0002E079
		internal string Url { get; set; }

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002FE82 File Offset: 0x0002E082
		internal ConfigResponseUrl()
		{
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002FE8C File Offset: 0x0002E08C
		internal ConfigResponseUrl(XElement element, Dictionary<string, XElement> tokenDictionary, BaseAsyncCommand.LogResponseParseFailureEventCallback logParseFailureCallback)
		{
			this.ServiceName = ConfigResponseUrl.ParseServiceName(element, OmexConstants.OfficeNamespace + "name", logParseFailureCallback);
			if (!string.IsNullOrWhiteSpace(this.ServiceName))
			{
				this.Url = ConfigResponseUrl.ParseUrl(element, OmexConstants.OfficeNamespace + "url", tokenDictionary, this.ServiceName, logParseFailureCallback);
			}
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002FEEC File Offset: 0x0002E0EC
		private static string ParseServiceName(XElement element, XName serviceNameKey, BaseAsyncCommand.LogResponseParseFailureEventCallback logParseFailureCallback)
		{
			string text = (string)element.Attribute(serviceNameKey);
			if (string.IsNullOrWhiteSpace(text))
			{
				ConfigResponseUrl.Tracer.TraceError<XElement>(0L, "ConfigResponseUrl.ParseServiceName: Unable to parse serviceName for: {0}", element);
				logParseFailureCallback(ApplicationLogicEventLogConstants.Tuple_ConfigResponseServiceNameParseFailed, null, element);
			}
			return text;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002FF30 File Offset: 0x0002E130
		private static string ParseUrl(XElement element, XName urlKey, Dictionary<string, XElement> tokenDictionary, string serviceName, BaseAsyncCommand.LogResponseParseFailureEventCallback logParseFailureCallback)
		{
			XElement xelement = element.Element(urlKey);
			if (xelement == null || xelement.Value == null)
			{
				ConfigResponseUrl.Tracer.TraceError<XElement>(0L, "ConfigResponseUrl.ParseUrl: Unable to parse url for: {0}", element);
				logParseFailureCallback(ApplicationLogicEventLogConstants.Tuple_ConfigResponseUrlParseFailed, serviceName, element);
				return null;
			}
			string value = xelement.Value;
			string[] array = value.Split(new char[]
			{
				'[',
				']'
			});
			if (array.Length == 1)
			{
				ConfigResponseUrl.Tracer.TraceDebug<XElement>(0L, "ConfigResponseUrl.ParseUrl: Url contains no tokens: {0}", element);
				logParseFailureCallback(ApplicationLogicEventLogConstants.Tuple_ConfigResponseUrlNoTokens, serviceName, element);
				return value;
			}
			if (array.Length != 3)
			{
				ConfigResponseUrl.Tracer.TraceError<XElement>(0L, "ConfigResponseUrl.ParseUrl: Expected one token in the response element. Unable to parse url for: {0}", element);
				logParseFailureCallback(ApplicationLogicEventLogConstants.Tuple_ConfigResponseUrlTooManyTokens, serviceName, element);
				return null;
			}
			if (tokenDictionary == null || tokenDictionary.Count == 0)
			{
				ConfigResponseUrl.Tracer.TraceError<XElement>(0L, "ConfigResponseUrl.ParseUrl: No tokens in the response. Unable to parse url for: {0}", element);
				logParseFailureCallback(ApplicationLogicEventLogConstants.Tuple_ConfigResponseUrlTokenNotFound, serviceName, element);
				return null;
			}
			XElement xelement2 = null;
			if (!tokenDictionary.TryGetValue(array[1], out xelement2))
			{
				ConfigResponseUrl.Tracer.TraceError<XElement>(0L, "ConfigResponseUrl.ParseUrl: Token not found. Unable to parse url for: {0}", element);
				logParseFailureCallback(ApplicationLogicEventLogConstants.Tuple_ConfigResponseUrlTokenNotFound, serviceName, element);
				return null;
			}
			string value2 = xelement2.Value;
			string text = array[0] + value2 + array[2];
			if (!Uri.IsWellFormedUriString(text, UriKind.Absolute))
			{
				ConfigResponseUrl.Tracer.TraceError<XElement>(0L, "ConfigResponseUrl.ParseUrl: Constructed url is not well formed for: {0}", element);
				logParseFailureCallback(ApplicationLogicEventLogConstants.Tuple_ConfigResponseUrlNotWellFormed, serviceName, element);
				return null;
			}
			return text;
		}

		// Token: 0x040005FA RID: 1530
		private const char LeftTokenDelimiter = '[';

		// Token: 0x040005FB RID: 1531
		private const char RightTokenDelimiter = ']';

		// Token: 0x040005FC RID: 1532
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;
	}
}
