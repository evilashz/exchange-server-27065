using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200011C RID: 284
	internal sealed class GetAppState : BaseAsyncOmexCommand
	{
		// Token: 0x06000BC3 RID: 3011 RVA: 0x00030097 File Offset: 0x0002E297
		public GetAppState(OmexWebServiceUrlsCache urlsCache) : base(urlsCache, "GetAppState")
		{
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000300A8 File Offset: 0x0002E2A8
		public void Execute(IEnumerable<IAppStateRequestAsset> requestAssets, string deploymentId, BaseAsyncCommand.GetLoggedMailboxIdentifierCallback getloggedMailboxIdentifierCallback, GetAppState.SuccessCallback successCallback, BaseAsyncCommand.FailureCallback failureCallback)
		{
			if (requestAssets == null || requestAssets.Count<IAppStateRequestAsset>() == 0)
			{
				throw new ArgumentException("assets must be passed", "assets");
			}
			if (requestAssets.Count<IAppStateRequestAsset>() > 100)
			{
				throw new ArgumentOutOfRangeException("assets count exceeds 100");
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
			this.getLoggedMailboxIdentifierCallback = getloggedMailboxIdentifierCallback;
			this.successCallback = successCallback;
			this.failureCallback = failureCallback;
			this.appStateRequestAssets = requestAssets;
			this.deploymentId = deploymentId;
			if (this.urlsCache.IsInitialized)
			{
				this.InternalExecute(requestAssets, deploymentId);
				return;
			}
			this.urlsCache.Initialize(new OmexWebServiceUrlsCache.InitializeCompletionCallback(this.UrlsCacheInitializationCompletionCallback));
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00030163 File Offset: 0x0002E363
		private void UrlsCacheInitializationCompletionCallback(bool isInitialized)
		{
			if (isInitialized)
			{
				this.InternalExecute(this.appStateRequestAssets, this.deploymentId);
				return;
			}
			this.InternalFailureCallback(null, "UrlsCache initialization failed. AppState method won't be called");
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00030188 File Offset: 0x0002E388
		private void InternalExecute(IEnumerable<IAppStateRequestAsset> requestAssets, string deploymentId)
		{
			string arg = GetAppState.CreateQueryString(requestAssets);
			Uri uri = new Uri(this.urlsCache.AppStateUrl + string.Format("?ma={0}&deployId={1}&corr={2}", arg, deploymentId, this.requestId));
			base.InternalExecute(uri);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000301D4 File Offset: 0x0002E3D4
		internal static string CreateQueryString(IEnumerable<IAppStateRequestAsset> requestAssets)
		{
			IEnumerable<IAppStateRequestAsset> enumerable = from asset in requestAssets
			orderby asset.MarketplaceContentMarket
			select asset;
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			foreach (IAppStateRequestAsset appStateRequestAsset in enumerable)
			{
				if (text != appStateRequestAsset.MarketplaceContentMarket)
				{
					if (text != null)
					{
						stringBuilder.Append(";");
					}
					stringBuilder.Append(appStateRequestAsset.MarketplaceContentMarket);
					stringBuilder.Append(":");
					text = appStateRequestAsset.MarketplaceContentMarket;
				}
				else
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(appStateRequestAsset.MarketplaceAssetID);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x000302A4 File Offset: 0x0002E4A4
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
			List<AppStateResponseAsset> list = GetAppState.CreateAppStateResponseAssets(responseXDocument, new BaseAsyncCommand.LogResponseParseFailureEventCallback(this.LogResponseParseFailureEvent));
			if (list.Count == 0)
			{
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_EmptyAppStateResponse, null, new object[]
				{
					this.scenario,
					this.requestId,
					base.GetLoggedMailboxIdentifier(),
					this.uri
				});
				this.InternalFailureCallback(null, "GetAppState.ParseResponse: No asset responses were returned");
				return;
			}
			base.LogResponseParsed();
			this.successCallback(list, this.uri);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00030398 File Offset: 0x0002E598
		internal static List<AppStateResponseAsset> CreateAppStateResponseAssets(XDocument responseXDocument, BaseAsyncCommand.LogResponseParseFailureEventCallback logParseFailureCallback)
		{
			IEnumerable<AppStateResponseAsset> collection = from assetElement in responseXDocument.Descendants(OmexConstants.OfficeNamespace + "asset")
			select new AppStateResponseAsset(assetElement, logParseFailureCallback);
			return new List<AppStateResponseAsset>(collection);
		}

		// Token: 0x040005FF RID: 1535
		public const int MaxRequestAssets = 100;

		// Token: 0x04000600 RID: 1536
		private const string QueryStringFormat = "?ma={0}&deployId={1}&corr={2}";

		// Token: 0x04000601 RID: 1537
		private const string QueryStringCultureDelimiter = ":";

		// Token: 0x04000602 RID: 1538
		private const string QueryStringAssetIDDelimiter = ",";

		// Token: 0x04000603 RID: 1539
		private const string QueryStringCultureGroupDelimiter = ";";

		// Token: 0x04000604 RID: 1540
		private GetAppState.SuccessCallback successCallback;

		// Token: 0x04000605 RID: 1541
		private IEnumerable<IAppStateRequestAsset> appStateRequestAssets;

		// Token: 0x04000606 RID: 1542
		private string deploymentId;

		// Token: 0x0200011D RID: 285
		// (Invoke) Token: 0x06000BCC RID: 3020
		internal delegate void SuccessCallback(List<AppStateResponseAsset> appStateResponses, Uri uri);
	}
}
