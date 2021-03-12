using System;
using System.Net;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000117 RID: 279
	internal sealed class DownloadApp : BaseAsyncOmexCommand
	{
		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002F729 File Offset: 0x0002D929
		public DownloadApp() : base(null, null)
		{
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002F733 File Offset: 0x0002D933
		public DownloadApp(OmexWebServiceUrlsCache urlsCache) : base(urlsCache, "DownloadAppForUpdate")
		{
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002F744 File Offset: 0x0002D944
		public void Execute(IDownloadAppRequestAsset asset, string deploymentId, BaseAsyncCommand.GetLoggedMailboxIdentifierCallback getloggedMailboxIdentifierCallback, DownloadApp.SuccessCallback successCallback, BaseAsyncCommand.FailureCallback failureCallback)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
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
			this.downloadRequestAsset = asset;
			this.periodicKey = asset.MarketplaceAssetID;
			string uriString = this.urlsCache.DownloadUrl + DownloadApp.CreateQueryString(asset, deploymentId, this.requestId);
			Uri uri = new Uri(uriString);
			base.InternalExecute(uri);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002F7E4 File Offset: 0x0002D9E4
		internal static string CreateQueryString(IDownloadAppRequestAsset asset, string deploymentId, string requestId)
		{
			string installedOwaVersion = DefaultExtensionTable.GetInstalledOwaVersion();
			string text = string.Format("?cmu={0}&av=MOW&ret=0&assetid={1}&build={2}&deployId={3}&corr={4}", new object[]
			{
				asset.MarketplaceContentMarket,
				asset.MarketplaceAssetID,
				installedOwaVersion,
				deploymentId,
				requestId
			});
			if (!string.IsNullOrWhiteSpace(asset.Etoken))
			{
				text += string.Format("&clienttoken={0}", asset.Etoken);
			}
			return text;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002F84D File Offset: 0x0002DA4D
		protected override void PrepareRequest(HttpWebRequest request)
		{
			request.AllowAutoRedirect = true;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002F856 File Offset: 0x0002DA56
		protected override long GetMaxResponseBufferSize()
		{
			return 393216L;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002F860 File Offset: 0x0002DA60
		protected override void ParseResponse(byte[] responseBuffer, int responseBufferSize)
		{
			ExtensionData extensionData = null;
			try
			{
				extensionData = ExtensionData.ParseOsfManifest(responseBuffer, responseBufferSize, this.downloadRequestAsset.MarketplaceAssetID, this.downloadRequestAsset.MarketplaceContentMarket, ExtensionType.MarketPlace, this.downloadRequestAsset.Scope, this.downloadRequestAsset.Enabled, this.downloadRequestAsset.DisableReason, string.Empty, this.downloadRequestAsset.Etoken);
			}
			catch (OwaExtensionOperationException exception)
			{
				this.InternalFailureCallback(exception, null);
				return;
			}
			base.LogResponseParsed();
			this.successCallback(extensionData, this.uri);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002F8F4 File Offset: 0x0002DAF4
		internal void TestParseResponse(IDownloadAppRequestAsset requestAsset, DownloadApp.SuccessCallback successCallback, BaseAsyncCommand.FailureCallback failureCallback, byte[] responseBuffer, int responseBufferSize)
		{
			this.downloadRequestAsset = requestAsset;
			this.successCallback = successCallback;
			this.failureCallback = failureCallback;
			this.ParseResponse(responseBuffer, responseBufferSize);
		}

		// Token: 0x040005E6 RID: 1510
		private const string DownloadQueryStringFormat = "?cmu={0}&av=MOW&ret=0&assetid={1}&build={2}&deployId={3}&corr={4}";

		// Token: 0x040005E7 RID: 1511
		private DownloadApp.SuccessCallback successCallback;

		// Token: 0x040005E8 RID: 1512
		private IDownloadAppRequestAsset downloadRequestAsset;

		// Token: 0x02000118 RID: 280
		// (Invoke) Token: 0x06000BAA RID: 2986
		internal delegate void SuccessCallback(ExtensionData extensionData, Uri uri);
	}
}
