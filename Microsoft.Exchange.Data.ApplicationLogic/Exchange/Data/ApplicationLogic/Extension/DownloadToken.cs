using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security.AntiXss;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000119 RID: 281
	internal sealed class DownloadToken : BaseAsyncOmexCommand
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x0002F915 File Offset: 0x0002DB15
		public DownloadToken(OmexWebServiceUrlsCache urlsCache) : base(urlsCache, "DownloadToken")
		{
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002F924 File Offset: 0x0002DB24
		public void Execute(List<TokenRenewRequestAsset> extensionTokenRenewList, string deploymentId, BaseAsyncCommand.GetLoggedMailboxIdentifierCallback getloggedMailboxIdentifierCallback, DownloadToken.SuccessCallback successCallback, BaseAsyncCommand.FailureCallback failureCallback)
		{
			if (extensionTokenRenewList == null)
			{
				throw new ArgumentNullException("extensionTokenRenewList");
			}
			if (getloggedMailboxIdentifierCallback == null)
			{
				throw new ArgumentNullException("getloggedMailboxIdentifierCallback");
			}
			if (successCallback == null)
			{
				throw new ArgumentNullException("successCallback");
			}
			if (failureCallback == null)
			{
				throw new ArgumentNullException("failureCallback");
			}
			if (deploymentId == null || string.IsNullOrWhiteSpace(deploymentId))
			{
				throw new ArgumentNullException("deploymentId");
			}
			this.getLoggedMailboxIdentifierCallback = getloggedMailboxIdentifierCallback;
			this.successCallback = successCallback;
			this.failureCallback = failureCallback;
			this.extensionTokenRenewList = extensionTokenRenewList;
			this.deploymentId = deploymentId;
			if (this.urlsCache.IsInitialized)
			{
				this.InternalExecute();
				return;
			}
			this.urlsCache.Initialize(new OmexWebServiceUrlsCache.InitializeCompletionCallback(this.UrlsCacheInitializationCompletionCallback));
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002F9D1 File Offset: 0x0002DBD1
		private void UrlsCacheInitializationCompletionCallback(bool isInitialized)
		{
			if (isInitialized)
			{
				this.InternalExecute();
				return;
			}
			this.InternalFailureCallback(null, "UrlsCache initialization failed. AppState method won't be called");
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002F9EC File Offset: 0x0002DBEC
		private void InternalExecute()
		{
			base.ResetRequestID();
			string uriString = this.urlsCache.DownloadUrl + DownloadToken.CreateQueryString(this.deploymentId, this.requestId);
			Uri uri = new Uri(uriString);
			base.InternalExecute(uri);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002FA30 File Offset: 0x0002DC30
		internal static string CreateQueryString(string deploymentId, string requestId)
		{
			string installedOwaVersion = DefaultExtensionTable.GetInstalledOwaVersion();
			return string.Format("?av=MOW&ret=1&build={0}&deployId={1}&corr={2}", installedOwaVersion, deploymentId, requestId);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002FA54 File Offset: 0x0002DC54
		protected override void PrepareRequest(HttpWebRequest request)
		{
			request.AllowAutoRedirect = true;
			request.Method = "POST";
			request.ContentType = "application/json";
			byte[] bytes = Encoding.UTF8.GetBytes(this.CreateRequestBody());
			request.ContentLength = (long)bytes.Length;
			using (Stream requestStream = request.GetRequestStream())
			{
				requestStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002FAC8 File Offset: 0x0002DCC8
		private string CreateRequestBody()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (TokenRenewRequestAsset tokenRenewRequestAsset in this.extensionTokenRenewList)
			{
				stringBuilder.AppendFormat("<o:etoken o:cm=\"{0}\" o:token=\"{1}\"/>", tokenRenewRequestAsset.MarketplaceContentMarket, AntiXssEncoder.HtmlEncode(HttpUtility.UrlDecode(tokenRenewRequestAsset.Etoken), false));
			}
			return string.Format("<o:etokens xmlns:o=\"urn:schemas-microsoft-com:office:office\">{0}</o:etokens>", stringBuilder.ToString());
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002FB50 File Offset: 0x0002DD50
		protected override long GetMaxResponseBufferSize()
		{
			return 30720L;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002FB58 File Offset: 0x0002DD58
		protected override void ParseResponse(byte[] responseBuffer, int responseBufferSize)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(responseBuffer, 0, responseBuffer.Length))
				{
					SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
					safeXmlDocument.PreserveWhitespace = true;
					safeXmlDocument.Load(memoryStream);
					XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(safeXmlDocument.NameTable);
					xmlNamespaceManager.AddNamespace("o", "urn:schemas-microsoft-com:office:office");
					XmlNodeList xmlNodeList = safeXmlDocument.SelectNodes("/o:assets/o:asset", xmlNamespaceManager);
					foreach (object obj in xmlNodeList)
					{
						XmlNode xmlNode = (XmlNode)obj;
						string attributeStringValue = ExtensionData.GetAttributeStringValue(xmlNode, "o:cm");
						string attributeStringValue2 = ExtensionData.GetAttributeStringValue(xmlNode, "o:assetid");
						string value = HttpUtility.UrlEncode(HttpUtility.HtmlDecode(ExtensionData.GetAttributeStringValue(xmlNode, "o:etok")));
						bool flag = false;
						string optionalAttributeStringValue = ExtensionData.GetOptionalAttributeStringValue(xmlNode, "o:status", string.Empty);
						foreach (TokenRenewRequestAsset tokenRenewRequestAsset in this.extensionTokenRenewList)
						{
							if (string.Equals(tokenRenewRequestAsset.MarketplaceAssetID, attributeStringValue2, StringComparison.OrdinalIgnoreCase) && string.Equals(tokenRenewRequestAsset.MarketplaceContentMarket, attributeStringValue, StringComparison.OrdinalIgnoreCase))
							{
								tokenRenewRequestAsset.IsResponseFound = true;
								if ("1".Equals(optionalAttributeStringValue, StringComparison.OrdinalIgnoreCase))
								{
									dictionary.Add(tokenRenewRequestAsset.ExtensionID, value);
								}
								else if ("6".Equals(optionalAttributeStringValue, StringComparison.OrdinalIgnoreCase))
								{
									dictionary2.Add(tokenRenewRequestAsset.ExtensionID, "2.1");
								}
								else
								{
									dictionary2.Add(tokenRenewRequestAsset.ExtensionID, "2.0");
								}
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							BaseAsyncCommand.Tracer.TraceError<string, string, string>(0L, "The returned token does not match the asset and marketplace in the request. Asset id: {0} Marketplace: {1} Status code: {2}.", attributeStringValue2, attributeStringValue, optionalAttributeStringValue);
							ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_MismatchedReturnedToken, attributeStringValue2, new object[]
							{
								this.scenario,
								this.requestId,
								base.GetLoggedMailboxIdentifier(),
								attributeStringValue2,
								attributeStringValue,
								optionalAttributeStringValue
							});
						}
					}
					foreach (TokenRenewRequestAsset tokenRenewRequestAsset2 in this.extensionTokenRenewList)
					{
						if (!tokenRenewRequestAsset2.IsResponseFound)
						{
							dictionary2.Add(tokenRenewRequestAsset2.ExtensionID, "2.0");
						}
					}
				}
			}
			catch (XmlException exception)
			{
				this.InternalFailureCallback(exception, null);
				return;
			}
			base.LogResponseParsed();
			this.successCallback(dictionary, dictionary2);
		}

		// Token: 0x040005E9 RID: 1513
		private const string DownloadQueryStringFormat = "?av=MOW&ret=1&build={0}&deployId={1}&corr={2}";

		// Token: 0x040005EA RID: 1514
		private const string PostRequestMethod = "POST";

		// Token: 0x040005EB RID: 1515
		private const string ResponseNamespaceUri = "urn:schemas-microsoft-com:office:office";

		// Token: 0x040005EC RID: 1516
		private const string ResponseNamespacePrefix = "o";

		// Token: 0x040005ED RID: 1517
		private const string TokenRenewSuccessfulStatusCode = "1";

		// Token: 0x040005EE RID: 1518
		private const string TokenExpiredForRenewStatusCode = "6";

		// Token: 0x040005EF RID: 1519
		private const string ResponseTokenPath = "/o:assets/o:asset";

		// Token: 0x040005F0 RID: 1520
		private const string StatusAttributeTagName = "o:status";

		// Token: 0x040005F1 RID: 1521
		private const string AssetIdAttributeTagName = "o:assetid";

		// Token: 0x040005F2 RID: 1522
		private const string MarketplaceAttributeTagName = "o:cm";

		// Token: 0x040005F3 RID: 1523
		private const string EtokenAttributeTagName = "o:etok";

		// Token: 0x040005F4 RID: 1524
		private const string RequestBodyFormat = "<o:etokens xmlns:o=\"urn:schemas-microsoft-com:office:office\">{0}</o:etokens>";

		// Token: 0x040005F5 RID: 1525
		private const string SubRequestFormat = "<o:etoken o:cm=\"{0}\" o:token=\"{1}\"/>";

		// Token: 0x040005F6 RID: 1526
		private const string ContentType = "application/json";

		// Token: 0x040005F7 RID: 1527
		private DownloadToken.SuccessCallback successCallback;

		// Token: 0x040005F8 RID: 1528
		private string deploymentId;

		// Token: 0x040005F9 RID: 1529
		private List<TokenRenewRequestAsset> extensionTokenRenewList;

		// Token: 0x0200011A RID: 282
		// (Invoke) Token: 0x06000BB7 RID: 2999
		internal delegate void SuccessCallback(Dictionary<string, string> newTokens, Dictionary<string, string> appStatusCodes);
	}
}
