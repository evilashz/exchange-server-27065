using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200011E RID: 286
	internal sealed class GetConfig : BaseAsyncOmexCommand
	{
		// Token: 0x06000BCF RID: 3023 RVA: 0x000303DF File Offset: 0x0002E5DF
		public GetConfig(OmexWebServiceUrlsCache urlsCache) : base(urlsCache, "GetConfig")
		{
			if (urlsCache == null)
			{
				throw new ArgumentNullException("urlsCache");
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000303FC File Offset: 0x0002E5FC
		public void Execute(GetConfig.SuccessCallback successCallback, BaseAsyncCommand.FailureCallback failureCallback)
		{
			if (successCallback == null)
			{
				throw new ArgumentNullException("successCallback");
			}
			if (failureCallback == null)
			{
				throw new ArgumentNullException("failureCallback");
			}
			this.successCallback = successCallback;
			this.failureCallback = failureCallback;
			try
			{
				Uri uri = new Uri(GetConfig.CreateUrlWithQueryString(this.urlsCache.ConfigServiceUrl, this.requestId));
				base.InternalExecute(uri);
			}
			catch (UriFormatException exception)
			{
				this.InternalFailureCallback(exception, null);
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00030474 File Offset: 0x0002E674
		protected override void PrepareRequest(HttpWebRequest request)
		{
			request.AllowAutoRedirect = true;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00030480 File Offset: 0x0002E680
		protected override void ParseResponse(byte[] responseBuffer, int responseBufferSize)
		{
			XDocument responseXDocument;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(responseBuffer, 0, responseBufferSize))
				{
					responseXDocument = XDocument.Load(memoryStream);
				}
			}
			catch (XmlException exception)
			{
				this.InternalFailureCallback(exception, null);
				return;
			}
			List<ConfigResponseUrl> list = GetConfig.CreateConfigResponseUrls(responseXDocument, this.uri, this.requestId, new BaseAsyncCommand.LogResponseParseFailureEventCallback(this.LogResponseParseFailureEvent));
			if (list == null)
			{
				this.InternalFailureCallback(null, "GetConfig.ParseResponse: Unable to parse urls");
				return;
			}
			base.LogResponseParsed();
			this.successCallback(list);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00030514 File Offset: 0x0002E714
		protected override void ProcessResponse(HttpWebResponse response)
		{
			string responseHeader = response.GetResponseHeader("X-Office-CacheDuration");
			if (!string.IsNullOrWhiteSpace(responseHeader))
			{
				try
				{
					int num = Convert.ToInt32(responseHeader);
					num = ((num > 60) ? num : 60);
					this.urlsCache.CacheLifeTimeFromHeaderInSeconds = (long)(num * 60);
				}
				catch (FormatException exception)
				{
					this.InternalFailureCallback(exception, null);
				}
				catch (OverflowException exception2)
				{
					this.InternalFailureCallback(exception2, null);
				}
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0003058C File Offset: 0x0002E78C
		protected override void LogResponseParseFailureEvent(ExEventLog.EventTuple eventTuple, string periodicKey, object messageArg)
		{
			ExtensionDiagnostics.Logger.LogEvent(eventTuple, this.periodicKey, new object[]
			{
				this.scenario,
				this.requestId,
				this.uri,
				messageArg
			});
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000305D4 File Offset: 0x0002E7D4
		internal static string CreateUrlWithQueryString(string baseUrl, string requestId)
		{
			string installedOwaVersion = DefaultExtensionTable.GetInstalledOwaVersion();
			string url = ExtensionData.AppendUnencodedQueryParameter(baseUrl, "CV", installedOwaVersion);
			url = ExtensionData.AppendUnencodedQueryParameter(url, "Client", "WAC_Outlook");
			return ExtensionData.AppendUnencodedQueryParameter(url, "corr", requestId);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000306B8 File Offset: 0x0002E8B8
		internal static List<ConfigResponseUrl> CreateConfigResponseUrls(XDocument responseXDocument, Uri uri, string requestId, BaseAsyncCommand.LogResponseParseFailureEventCallback logParseFailureCallback)
		{
			XName nameXName = OmexConstants.OfficeNamespace + "name";
			IEnumerable<XElement> source = from xElement in responseXDocument.Descendants(OmexConstants.OfficeNamespace + "token")
			select xElement;
			Dictionary<string, XElement> tokenDictionary = null;
			try
			{
				tokenDictionary = source.ToDictionary((XElement t) => t.Attribute(nameXName).Value);
			}
			catch (NullReferenceException)
			{
				BaseAsyncCommand.Tracer.TraceError<XDocument>(0L, "GetConfig.CreateConfigResponseUrls: Token name attribute missing from response xml: {0}", responseXDocument);
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_ConfigResponseTokenNamesMissing, null, new object[]
				{
					GetConfig.ScenarioProcessConfig,
					requestId,
					uri
				});
				return null;
			}
			IEnumerable<ConfigResponseUrl> collection = from urlElement in responseXDocument.Descendants(OmexConstants.OfficeNamespace + "service")
			where urlElement.Attribute(nameXName).Value == "AppStateQuery15" || urlElement.Attribute(nameXName).Value == "AppInstallInfoQuery15" || urlElement.Attribute(nameXName).Value == "AppInfoQuery15"
			select new ConfigResponseUrl(urlElement, tokenDictionary, logParseFailureCallback);
			List<ConfigResponseUrl> list = null;
			try
			{
				list = new List<ConfigResponseUrl>(collection);
			}
			catch (NullReferenceException)
			{
				BaseAsyncCommand.Tracer.TraceError<XDocument>(0L, "GetConfig.CreateConfigResponseUrls: Service name attribute is missing from response xml: {0}", responseXDocument);
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_ConfigResponseServiceNameMissing, null, new object[]
				{
					GetConfig.ScenarioProcessConfig,
					requestId,
					uri
				});
				return null;
			}
			if (list.Any((ConfigResponseUrl t) => t.Url == null))
			{
				BaseAsyncCommand.Tracer.TraceError<XDocument>(0L, "GetConfig.CreateConfigResponseUrls: one or more urls is null: {0}", responseXDocument);
				return null;
			}
			if (list.Count != 3)
			{
				BaseAsyncCommand.Tracer.TraceError<XDocument>(0L, "GetConfig.CreateConfigResponseUrls: urls are missing: {0}", responseXDocument);
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_ConfigResponseUrlsMissing, null, new object[]
				{
					GetConfig.ScenarioProcessConfig,
					requestId,
					uri
				});
				return null;
			}
			return list;
		}

		// Token: 0x04000608 RID: 1544
		private const string CVParameterName = "CV";

		// Token: 0x04000609 RID: 1545
		private const string ClientParameterName = "Client";

		// Token: 0x0400060A RID: 1546
		private const string CorrelationIdParameterName = "corr";

		// Token: 0x0400060B RID: 1547
		private const int MinCacheLifeTimeInMinutes = 60;

		// Token: 0x0400060C RID: 1548
		private GetConfig.SuccessCallback successCallback;

		// Token: 0x0400060D RID: 1549
		private static string ScenarioProcessConfig = "ProcessConfig";

		// Token: 0x0200011F RID: 287
		// (Invoke) Token: 0x06000BDB RID: 3035
		internal delegate void SuccessCallback(List<ConfigResponseUrl> configResponses);
	}
}
