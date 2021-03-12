using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002E4 RID: 740
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetLinkPreview : AsyncServiceCommand<GetLinkPreviewResponse>
	{
		// Token: 0x060018E9 RID: 6377 RVA: 0x0005652E File Offset: 0x0005472E
		public GetLinkPreview(CallContext callContext, GetLinkPreviewRequest request) : base(callContext)
		{
			this.request = request;
			OwsLogRegistry.Register(GetLinkPreview.GetLinkPreviewActionName, GetLinkPreview.GetLinkPreviewMetadataType, new Type[0]);
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00056554 File Offset: 0x00054754
		private static bool GetActiveViewsConvergenceFlightEnabled(RequestDetailsLogger logger)
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			if (userContext == null)
			{
				logger.Set(GetLinkPreviewMetadata.UserContextNull, 1);
				logger.Set(GetLinkPreviewMetadata.ActiveViewConvergenceEnabled, 0);
				return false;
			}
			if (!userContext.FeaturesManager.ClientServerSettings.ActiveViewConvergence.Enabled)
			{
				logger.Set(GetLinkPreviewMetadata.ActiveViewConvergenceEnabled, 0);
				return false;
			}
			logger.Set(GetLinkPreviewMetadata.ActiveViewConvergenceEnabled, 1);
			return true;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0005698C File Offset: 0x00054B8C
		protected override async Task<GetLinkPreviewResponse> InternalExecute()
		{
			GetLinkPreviewResponse getLinkPreviewResponse;
			try
			{
				DataProviderInformation dataProviderInformation = null;
				long elapsedTimeToWebPageStepCompletion = 0L;
				long elapsedTimeToRegExStepCompletion = 0L;
				base.CallContext.ProtocolLog.Set(GetLinkPreviewMetadata.Url, this.request.Url);
				if (GetLinkPreview.GetPreviewsDisabled())
				{
					return this.CreateDisabledResponse();
				}
				if (Interlocked.Increment(ref GetLinkPreview.getPreviewRequestCount) > GetLinkPreview.getPreviewRequestCountMax)
				{
					return this.CreateErrorResponse("MaxConcurrentRequestExceeded", "The maximum number of concurrent requests has been exceeded.");
				}
				bool activeViewConvergenceEnabled = GetLinkPreview.GetActiveViewsConvergenceFlightEnabled(base.CallContext.ProtocolLog);
				Stopwatch stopwatch = Stopwatch.StartNew();
				LinkPreviewDataProvider dataProvider = null;
				dataProvider = LinkPreviewDataProvider.GetDataProvider(this.request, base.CallContext.ProtocolLog, activeViewConvergenceEnabled);
				dataProviderInformation = await dataProvider.GetDataProviderInformation();
				elapsedTimeToWebPageStepCompletion = stopwatch.ElapsedMilliseconds;
				getLinkPreviewResponse = dataProvider.CreatePreview(dataProviderInformation);
				stopwatch.Stop();
				elapsedTimeToRegExStepCompletion = stopwatch.ElapsedMilliseconds;
				getLinkPreviewResponse.ElapsedTimeToWebPageStepCompletion = elapsedTimeToWebPageStepCompletion;
				getLinkPreviewResponse.ElapsedTimeToRegExStepCompletion = elapsedTimeToRegExStepCompletion;
				getLinkPreviewResponse.WebPageContentLength = dataProvider.ContentLength;
				this.LogWebMethodData(getLinkPreviewResponse);
			}
			catch (OwaPermanentException exception)
			{
				getLinkPreviewResponse = this.CreateErrorResponse(exception);
			}
			catch (LocalizedException exception2)
			{
				getLinkPreviewResponse = this.CreateErrorResponse(exception2);
			}
			catch (HttpRequestException requestException)
			{
				getLinkPreviewResponse = this.CreateErrorResponse(requestException);
			}
			catch (TaskCanceledException)
			{
				getLinkPreviewResponse = this.CreateErrorResponse("RequestTimeout", "The web page request timed out.");
			}
			catch (WebException webException)
			{
				getLinkPreviewResponse = this.CreateErrorResponse(webException);
			}
			finally
			{
				Interlocked.Decrement(ref GetLinkPreview.getPreviewRequestCount);
			}
			return getLinkPreviewResponse;
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000569D4 File Offset: 0x00054BD4
		private void LogWebMethodData(GetLinkPreviewResponse getLinkPreviewResponse)
		{
			base.CallContext.ProtocolLog.Set(GetLinkPreviewMetadata.ElapsedTimeToWebPageStepCompletion, getLinkPreviewResponse.ElapsedTimeToWebPageStepCompletion);
			base.CallContext.ProtocolLog.Set(GetLinkPreviewMetadata.ElapsedTimeToRegExStepCompletion, getLinkPreviewResponse.ElapsedTimeToRegExStepCompletion);
			base.CallContext.ProtocolLog.Set(GetLinkPreviewMetadata.WebPageContentLength, getLinkPreviewResponse.WebPageContentLength);
			base.CallContext.ProtocolLog.Set(GetLinkPreviewMetadata.DescriptionTagCount, getLinkPreviewResponse.DescriptionTagCount);
			base.CallContext.ProtocolLog.Set(GetLinkPreviewMetadata.ImageTagCount, getLinkPreviewResponse.ImageTagCount);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00056A8C File Offset: 0x00054C8C
		private static bool GetPreviewsDisabled()
		{
			bool result = true;
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null && UserContextManager.GetUserContext(httpContext).FeaturesManager != null)
			{
				result = !UserContextManager.GetUserContext(httpContext).FeaturesManager.ClientServerSettings.InlinePreview.Enabled;
			}
			return result;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00056AD4 File Offset: 0x00054CD4
		private static int CalculateGetPreviewRequestCountMax()
		{
			int num;
			int num2;
			ThreadPool.GetMaxThreads(out num, out num2);
			return (int)Math.Round((double)num2 * 0.1);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00056AFC File Offset: 0x00054CFC
		public static void ThrowInvalidRequestException(HttpResponseMessage responseMessage)
		{
			string error = responseMessage.StatusCode.ToString();
			string reasonPhrase = responseMessage.ReasonPhrase;
			OwaInvalidRequestException ex = new OwaInvalidRequestException(reasonPhrase);
			GetLinkPreview.SetErrorData(ex, error);
			throw ex;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00056B30 File Offset: 0x00054D30
		public static void ThrowInvalidRequestException(string error, string errorMessage)
		{
			OwaInvalidRequestException ex = new OwaInvalidRequestException(errorMessage);
			GetLinkPreview.SetErrorData(ex, error);
			throw ex;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00056B4C File Offset: 0x00054D4C
		public static void ThrowLocalizedException(string error, LocalizedException localizedException)
		{
			GetLinkPreview.SetErrorData(localizedException, error);
			throw localizedException;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00056B56 File Offset: 0x00054D56
		private static void SetErrorData(Exception exception, string error)
		{
			exception.Data["ErrorKey"] = error;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00056B69 File Offset: 0x00054D69
		private static string GetExceptionMessage(Exception exception)
		{
			if (exception.InnerException != null)
			{
				return exception.InnerException.Message;
			}
			return exception.Message;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00056B88 File Offset: 0x00054D88
		private GetLinkPreviewResponse CreateErrorResponse(HttpRequestException requestException)
		{
			WebException ex = requestException.InnerException as WebException;
			if (ex != null)
			{
				return this.CreateErrorResponse(ex);
			}
			string name = requestException.GetType().Name;
			string exceptionMessage = GetLinkPreview.GetExceptionMessage(requestException);
			return this.CreateErrorResponse(name, exceptionMessage);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00056BC8 File Offset: 0x00054DC8
		private GetLinkPreviewResponse CreateErrorResponse(WebException webException)
		{
			string error;
			if (webException.Status == WebExceptionStatus.ProtocolError)
			{
				string str = null;
				HttpWebResponse httpWebResponse = webException.Response as HttpWebResponse;
				if (httpWebResponse != null)
				{
					str = ((int)httpWebResponse.StatusCode).ToString();
				}
				error = webException.Status.ToString() + str;
			}
			else
			{
				error = webException.Status.ToString();
			}
			string exceptionMessage = GetLinkPreview.GetExceptionMessage(webException);
			return this.CreateErrorResponse(error, exceptionMessage);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00056C38 File Offset: 0x00054E38
		private GetLinkPreviewResponse CreateErrorResponse(Exception exception)
		{
			string error = (exception.Data["ErrorKey"] != null) ? exception.Data["ErrorKey"].ToString() : exception.GetType().Name;
			string exceptionMessage = GetLinkPreview.GetExceptionMessage(exception);
			return this.CreateErrorResponse(error, exceptionMessage);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00056C8C File Offset: 0x00054E8C
		private GetLinkPreviewResponse CreateErrorResponse(string error, string errorMessage)
		{
			base.CallContext.ProtocolLog.Set(GetLinkPreviewMetadata.Error, error);
			base.CallContext.ProtocolLog.Set(GetLinkPreviewMetadata.ErrorMessage, errorMessage);
			return new GetLinkPreviewResponse
			{
				Error = error,
				ErrorMessage = errorMessage
			};
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00056CE0 File Offset: 0x00054EE0
		private GetLinkPreviewResponse CreateDisabledResponse()
		{
			base.CallContext.ProtocolLog.Set(GetLinkPreviewMetadata.DisabledResponse, 1);
			return new GetLinkPreviewResponse
			{
				IsDisabled = true
			};
		}

		// Token: 0x04000D76 RID: 3446
		private const string ErrorKey = "ErrorKey";

		// Token: 0x04000D77 RID: 3447
		private const double IoThreadFractionForLinkPreviewRequests = 0.1;

		// Token: 0x04000D78 RID: 3448
		private readonly GetLinkPreviewRequest request;

		// Token: 0x04000D79 RID: 3449
		private static int getPreviewRequestCount;

		// Token: 0x04000D7A RID: 3450
		private static readonly int getPreviewRequestCountMax = GetLinkPreview.CalculateGetPreviewRequestCountMax();

		// Token: 0x04000D7B RID: 3451
		private static readonly string GetLinkPreviewActionName = typeof(GetLinkPreview).Name;

		// Token: 0x04000D7C RID: 3452
		private static readonly Type GetLinkPreviewMetadataType = typeof(GetLinkPreviewMetadata);
	}
}
