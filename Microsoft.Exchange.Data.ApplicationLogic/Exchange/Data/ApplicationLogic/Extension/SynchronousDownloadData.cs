using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200012B RID: 299
	internal sealed class SynchronousDownloadData
	{
		// Token: 0x06000C2A RID: 3114 RVA: 0x00032564 File Offset: 0x00030764
		public MemoryStream Execute(string configServiceUrl, string marketplaceAssetID, string marketplaceQueryMarket, string deploymentId, string etoken = null)
		{
			string omexDownloadUrl = this.GetOmexDownloadUrl(configServiceUrl);
			Uri uri = SynchronousDownloadData.CreateDownloadUri(omexDownloadUrl, marketplaceQueryMarket, marketplaceAssetID, deploymentId, etoken);
			return SynchronousDownloadData.DownloadDataFromUri(uri, 393216L, new Func<long, bool, bool>(ExtensionData.ValidateManifestDownloadSize), false, true);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000325A0 File Offset: 0x000307A0
		private string GetOmexDownloadUrl(string configServiceUrl)
		{
			string downloadUrl;
			if (OmexWebServiceUrlsCache.Singleton.IsInitialized)
			{
				SynchronousDownloadData.Tracer.TraceDebug<string>(0L, "SynchronousDownloadData.DownloadDataFromOfficeMarketPlace: UrlsCache is initialized. Using download url: {0}", OmexWebServiceUrlsCache.Singleton.DownloadUrl);
				downloadUrl = OmexWebServiceUrlsCache.Singleton.DownloadUrl;
			}
			else
			{
				this.waitHandle = new AutoResetEvent(false);
				OmexWebServiceUrlsCache.Singleton.Initialize(configServiceUrl, new OmexWebServiceUrlsCache.InitializeCompletionCallback(this.UrlsCacheInitializationCompletionCallback));
				this.waitHandle.WaitOne(30000);
				if (!this.isUrlsCacheInitialized)
				{
					SynchronousDownloadData.Tracer.TraceError(0L, "SynchronousDownloadData.DownloadDataFromOfficeMarketPlace: UrlsCache initialization failed.");
					ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_DownloadDataFromOfficeMarketPlaceFailed, null, new object[]
					{
						"DownloadNewApp",
						null,
						"UrlsCache initialization failed."
					});
					throw new OwaExtensionOperationException(Strings.ErrorMarketplaceWebServicesUnavailable);
				}
				SynchronousDownloadData.Tracer.TraceDebug<string>(0L, "SynchronousDownloadData.DownloadDataFromOfficeMarketPlace: UrlsCache initialized. Using download url: {0}", OmexWebServiceUrlsCache.Singleton.DownloadUrl);
				downloadUrl = OmexWebServiceUrlsCache.Singleton.DownloadUrl;
			}
			return downloadUrl;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0003268E File Offset: 0x0003088E
		private void UrlsCacheInitializationCompletionCallback(bool isInitialized)
		{
			this.isUrlsCacheInitialized = isInitialized;
			this.waitHandle.Set();
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000326A4 File Offset: 0x000308A4
		private static Uri CreateDownloadUri(string marketplaceDownloadServiceUrl, string marketplaceQueryMarket, string marketplaceAssetID, string deploymentId, string etoken = null)
		{
			string text = string.Format("{0}?cmu={1}&av=OLW150&ret=0&assetid={2}&build={3}&deployId={4}", new object[]
			{
				marketplaceDownloadServiceUrl,
				marketplaceQueryMarket,
				marketplaceAssetID,
				DefaultExtensionTable.GetInstalledOwaVersion(),
				deploymentId
			});
			if (!string.IsNullOrWhiteSpace(etoken))
			{
				text += string.Format("&clienttoken={0}", etoken);
			}
			return new Uri(text);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00032700 File Offset: 0x00030900
		public static MemoryStream DownloadDataFromUri(Uri uri, long expectedMaxResponseSize, Func<long, bool, bool> responseValidationCallback, bool isUrlUserInput = true, bool isBposUser = true)
		{
			string text = Guid.NewGuid().ToString();
			if (isBposUser && isUrlUserInput && IPAddressUtil.IsIntranetAddress(uri))
			{
				throw new DownloadPermanentException();
			}
			string text2 = uri.OriginalString;
			if (text2.Contains("?"))
			{
				text2 = text2 + "&corr=" + text;
			}
			else
			{
				text2 = text2 + "?corr=" + text;
			}
			uri = new Uri(text2);
			MemoryStream result = null;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.AllowAutoRedirect = true;
			httpWebRequest.Timeout = 30000;
			httpWebRequest.CachePolicy = SynchronousDownloadData.NoCachePolicy;
			try
			{
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						long num = expectedMaxResponseSize + 1L;
						if (responseStream.CanSeek)
						{
							num = Math.Min(num, responseStream.Length);
						}
						byte[] array = new byte[num];
						int num2 = 0;
						int num3;
						do
						{
							num3 = responseStream.Read(array, num2, array.Length - num2);
							num2 += num3;
							responseValidationCallback((long)num2, true);
						}
						while (num3 > 0);
						result = new MemoryStream(array, 0, num2);
					}
				}
			}
			catch (Exception ex)
			{
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_DownloadDataFromOfficeMarketPlaceFailed, null, new object[]
				{
					"DownloadNewApp",
					text,
					uri,
					ExtensionDiagnostics.GetLoggedExceptionString(ex)
				});
				throw ex;
			}
			ExtensionDiagnostics.LogToDatacenterOnly(ApplicationLogicEventLogConstants.Tuple_DownloadDataFromOfficeMarketPlaceSucceeded, null, new object[]
			{
				"DownloadNewApp",
				text,
				uri
			});
			return result;
		}

		// Token: 0x04000649 RID: 1609
		private const int RequestTimeout = 30000;

		// Token: 0x0400064A RID: 1610
		private const string ScenarioDownloadNewApp = "DownloadNewApp";

		// Token: 0x0400064B RID: 1611
		internal const string ClientTokenFormat = "&clienttoken={0}";

		// Token: 0x0400064C RID: 1612
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x0400064D RID: 1613
		private static readonly HttpRequestCachePolicy NoCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

		// Token: 0x0400064E RID: 1614
		private EventWaitHandle waitHandle;

		// Token: 0x0400064F RID: 1615
		private bool isUrlsCacheInitialized;
	}
}
