using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement.SOAP.Server;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B47 RID: 2887
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RmsServerInfoManager
	{
		// Token: 0x0600686B RID: 26731 RVA: 0x001BA5C1 File Offset: 0x001B87C1
		public static void InitializeServerInfoMap(string serverMapPath, int maxCount, RmsPerformanceCounters performanceCounters)
		{
			ArgumentValidator.ThrowIfNullOrEmpty(serverMapPath, "serverMapPath");
			RmsServerInfoManager.serverInfoMap = new ExternalRmsServerInfoMap(serverMapPath, maxCount, (RmsServerInfoManager.perfCounters == null) ? null : RmsServerInfoManager.perfCounters.ServerInfoMapPerfCounters);
			RmsServerInfoManager.perfCounters = performanceCounters;
			RmsServerInfoManager.initialized = true;
		}

		// Token: 0x0600686C RID: 26732 RVA: 0x001BA5FC File Offset: 0x001B87FC
		public static IAsyncResult BeginAcquireServerInfo(RmsClientManagerContext context, Uri licenseUri, object state, AsyncCallback callback)
		{
			RmsServerInfoManager.ThrowIfNotInitialized();
			ArgumentValidator.ThrowIfNull("licenseUri", licenseUri);
			AcquireServerInfoAsyncResult acquireServerInfoAsyncResult = new AcquireServerInfoAsyncResult(context, licenseUri, state, callback);
			acquireServerInfoAsyncResult.AddBreadCrumb(Constants.State.BeginAcquireServerInfo);
			ExternalRMSServerInfo externalRMSServerInfo;
			if (RmsServerInfoManager.serverInfoMap.TryGet(licenseUri, out externalRMSServerInfo))
			{
				acquireServerInfoAsyncResult.InvokeCallback();
				return acquireServerInfoAsyncResult;
			}
			RmsClientManagerLog.LogUriEvent(RmsClientManagerLog.RmsClientManagerFeature.ServerInfo, RmsClientManagerLog.RmsClientManagerEvent.Acquire, context, licenseUri);
			WebProxy localServerProxy;
			try
			{
				localServerProxy = RmsClientManagerUtils.GetLocalServerProxy(true);
			}
			catch (ExchangeConfigurationException value)
			{
				acquireServerInfoAsyncResult.InvokeCallback(value);
				return acquireServerInfoAsyncResult;
			}
			bool flag = RmsServerInfoManager.outstandingFindServiceLocationCalls.EnqueueResult(licenseUri, acquireServerInfoAsyncResult);
			if (flag)
			{
				acquireServerInfoAsyncResult.ServerWSManager = new ServerWSManager(licenseUri, RmsServerInfoManager.perfCounters, acquireServerInfoAsyncResult.LatencyTracker, localServerProxy, RmsClientManager.AppSettings.RmsSoapQueriesTimeout);
				ServiceType[] serviceTypes = new ServiceType[]
				{
					ServiceType.CertificationWSService,
					ServiceType.ServerLicensingWSService,
					ServiceType.CertificationMexService,
					ServiceType.ServerLicensingMexService
				};
				RmsClientManager.TracePass(null, context.SystemProbeId, "Querying the RMS server {0} for server info", new object[]
				{
					licenseUri
				});
				acquireServerInfoAsyncResult.AddBreadCrumb(Constants.State.BeginFindServiceLocationsFirstRequest);
				try
				{
					acquireServerInfoAsyncResult.ServerWSManager.BeginFindServiceLocations(serviceTypes, RmsClientManagerUtils.WrapCallbackWithUnhandledExceptionHandlerAndUpdatePoisonContext(new AsyncCallback(RmsServerInfoManager.AcquireServiceLocationCallback)), acquireServerInfoAsyncResult);
					return acquireServerInfoAsyncResult;
				}
				catch (InvalidOperationException ex)
				{
					RmsClientManager.TraceFail(null, context.SystemProbeId, "Hit an exception during BeginFindServiceLocations {0}", new object[]
					{
						ex
					});
					acquireServerInfoAsyncResult.InvokeCallback(new RightsManagementException(RightsManagementFailureCode.FindServiceLocationFailed, ServerStrings.FailedToFindServerInfo(licenseUri), ex));
					return acquireServerInfoAsyncResult;
				}
			}
			RmsClientManager.TracePass(null, context.SystemProbeId, "A request for server info for the license uri {0} is already pending. Enqueuing the result", new object[]
			{
				licenseUri
			});
			acquireServerInfoAsyncResult.AddBreadCrumb(Constants.State.BeginFindServiceLocationsPendingRequest);
			return acquireServerInfoAsyncResult;
		}

		// Token: 0x0600686D RID: 26733 RVA: 0x001BA794 File Offset: 0x001B8994
		public static ExternalRMSServerInfo EndAcquireServerInfo(IAsyncResult asyncResult)
		{
			RmsServerInfoManager.ThrowIfNotInitialized();
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			AcquireServerInfoAsyncResult acquireServerInfoAsyncResult = asyncResult as AcquireServerInfoAsyncResult;
			if (acquireServerInfoAsyncResult == null)
			{
				throw new InvalidOperationException("asyncResult cannot be null and has to be type of AcquireServerInfoAsyncResult");
			}
			acquireServerInfoAsyncResult.AddBreadCrumb(Constants.State.EndAcquireServerInfo);
			if (!acquireServerInfoAsyncResult.IsCompleted)
			{
				acquireServerInfoAsyncResult.InternalWaitForCompletion();
			}
			Exception ex = acquireServerInfoAsyncResult.Result as Exception;
			if (ex != null)
			{
				RmsServerInfoManager.Tracer.TraceError<Uri, Exception>(0L, "EndAcquireServerInfo hit an exception; and as a result of it, '{0}' will be marked as negative entry in cache. Exception - {1}", acquireServerInfoAsyncResult.LicenseUri, ex);
				RightsManagementException ex2 = ex as RightsManagementException;
				if (ex2 == null || ex2.IsPermanent)
				{
					RmsServerInfoManager.AddNegativeServerInfo(acquireServerInfoAsyncResult.LicenseUri);
				}
				RmsClientManagerLog.LogException(RmsClientManagerLog.RmsClientManagerFeature.ServerInfo, acquireServerInfoAsyncResult.Context, ex);
				throw ex;
			}
			ExternalRMSServerInfo externalRMSServerInfo;
			if (!RmsServerInfoManager.serverInfoMap.TryGet(acquireServerInfoAsyncResult.LicenseUri, out externalRMSServerInfo))
			{
				RmsClientManagerLog.LogAcquireServerInfoResult(acquireServerInfoAsyncResult.Context, null);
				RmsServerInfoManager.Tracer.TraceError(0L, "Could not find the server info in the cache");
				return null;
			}
			RmsClientManagerLog.LogAcquireServerInfoResult(acquireServerInfoAsyncResult.Context, externalRMSServerInfo);
			if (externalRMSServerInfo.IsNegativeEntry)
			{
				RmsServerInfoManager.Tracer.TraceError(0L, "RmsServerInfoManager found requested server info in cache; but it is a negative entry!");
				return null;
			}
			return externalRMSServerInfo;
		}

		// Token: 0x0600686E RID: 26734 RVA: 0x001BA88C File Offset: 0x001B8A8C
		public static void AddNegativeServerInfo(Uri licenseUri)
		{
			ArgumentValidator.ThrowIfNull("licenseUri", licenseUri);
			RmsServerInfoManager.ThrowIfNotInitialized();
			ExternalRMSServerInfo externalRMSServerInfo = new ExternalRMSServerInfo(licenseUri);
			externalRMSServerInfo.MarkAsNegative();
			RmsServerInfoManager.serverInfoMap.Add(externalRMSServerInfo);
		}

		// Token: 0x0600686F RID: 26735 RVA: 0x001BA8C4 File Offset: 0x001B8AC4
		private static void AcquireServiceLocationCallback(IAsyncResult asyncResult)
		{
			RmsServerInfoManager.Tracer.TraceDebug(0L, "AcquireServiceLocationCallback invoked");
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			ArgumentValidator.ThrowIfNull("asyncResult.AsyncState", asyncResult.AsyncState);
			AcquireServerInfoAsyncResult acquireServerInfoAsyncResult = asyncResult.AsyncState as AcquireServerInfoAsyncResult;
			if (acquireServerInfoAsyncResult == null)
			{
				throw new InvalidOperationException("asyncResult.AsyncState has to be type of AcquireServerInfoAsyncResult.");
			}
			acquireServerInfoAsyncResult.AddBreadCrumb(Constants.State.AcquireServiceLocationCallback);
			Exception ex = null;
			try
			{
				acquireServerInfoAsyncResult.ServiceLocationResponses = acquireServerInfoAsyncResult.ServerWSManager.EndFindServiceLocations(asyncResult);
				LocalizedString value;
				if (!RmsServerInfoManager.ValidateResponsesAndUpdateServerInfo(acquireServerInfoAsyncResult, out value))
				{
					ex = new RightsManagementException(RightsManagementFailureCode.FindServiceLocationFailed, ServerStrings.ValidationForServiceLocationResponseFailed(acquireServerInfoAsyncResult.LicenseUri, value));
				}
				else
				{
					HttpSessionConfig httpSessionConfig = new HttpSessionConfig();
					WebProxy localServerProxy = RmsClientManagerUtils.GetLocalServerProxy(true);
					if (localServerProxy != null)
					{
						httpSessionConfig.Proxy = localServerProxy;
					}
					acquireServerInfoAsyncResult.HttpClient = new HttpClient();
					acquireServerInfoAsyncResult.AddBreadCrumb(Constants.State.BeginDownloadCertificationMexData);
					acquireServerInfoAsyncResult.LatencyTracker.BeginTrackRmsLatency(RmsOperationType.AcquireCertificationMexData);
					acquireServerInfoAsyncResult.HttpClient.BeginDownload(acquireServerInfoAsyncResult.CertificationMExUri, httpSessionConfig, RmsClientManagerUtils.WrapCancellableCallbackWithUnhandledExceptionHandlerAndUpdatePoisonContext(new CancelableAsyncCallback(RmsServerInfoManager.AcquireCertificationMexCallback)), acquireServerInfoAsyncResult);
				}
			}
			catch (RightsManagementException ex2)
			{
				RmsClientManager.TraceFail(null, acquireServerInfoAsyncResult.Context.SystemProbeId, "Hit an exception during AcquireServiceLocationCallback {0}", new object[]
				{
					ex2
				});
				ex = ex2;
			}
			catch (ExchangeConfigurationException ex3)
			{
				RmsClientManager.TraceFail(null, acquireServerInfoAsyncResult.Context.SystemProbeId, "Hit an exception during AcquireServiceLocationCallback {0}", new object[]
				{
					ex3
				});
				ex = ex3;
			}
			finally
			{
				if (ex != null)
				{
					acquireServerInfoAsyncResult.Release();
					RmsServerInfoManager.Tracer.TraceError(0L, "AcquireServiceLocationCallback: Invoking find service location callbacks");
					RmsServerInfoManager.outstandingFindServiceLocationCalls.InvokeCallbacks(acquireServerInfoAsyncResult.LicenseUri, ex);
				}
			}
		}

		// Token: 0x06006870 RID: 26736 RVA: 0x001BAA70 File Offset: 0x001B8C70
		private static void AcquireCertificationMexCallback(ICancelableAsyncResult asyncResult)
		{
			RmsServerInfoManager.Tracer.TraceDebug(0L, "AcquireCertificationMexCallback invoked");
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			ArgumentValidator.ThrowIfNull("asyncResult.AsyncState", asyncResult.AsyncState);
			AcquireServerInfoAsyncResult acquireServerInfoAsyncResult = asyncResult.AsyncState as AcquireServerInfoAsyncResult;
			if (acquireServerInfoAsyncResult == null)
			{
				throw new InvalidOperationException("asyncResult.AsyncState has to be type of AcquireServerInfoAsyncResult.");
			}
			acquireServerInfoAsyncResult.AddBreadCrumb(Constants.State.AcquireCertificationMexCallback);
			Exception ex = null;
			try
			{
				DownloadResult downloadResult = acquireServerInfoAsyncResult.HttpClient.EndDownload(asyncResult);
				acquireServerInfoAsyncResult.LatencyTracker.EndTrackRmsLatency(RmsOperationType.AcquireCertificationMexData);
				if (downloadResult.IsSucceeded)
				{
					Uri targetUriFromResponse = RmsClientManagerUtils.GetTargetUriFromResponse(downloadResult.ResponseStream);
					if (targetUriFromResponse == null)
					{
						ex = new RightsManagementException(RightsManagementFailureCode.FailedToExtractTargetUriFromMex, ServerStrings.FailedToFindTargetUriFromMExData(acquireServerInfoAsyncResult.CertificationMExUri), acquireServerInfoAsyncResult.LicenseUri.ToString());
					}
					else
					{
						acquireServerInfoAsyncResult.ServerInfo.CertificationWSTargetUri = TokenTarget.Fix(targetUriFromResponse);
						acquireServerInfoAsyncResult.AddBreadCrumb(Constants.State.BeginDownloadServerLicensingMexData);
						WebProxy localServerProxy;
						try
						{
							localServerProxy = RmsClientManagerUtils.GetLocalServerProxy(true);
						}
						catch (ExchangeConfigurationException ex2)
						{
							ex = ex2;
							return;
						}
						HttpSessionConfig httpSessionConfig = new HttpSessionConfig();
						if (localServerProxy != null)
						{
							httpSessionConfig.Proxy = localServerProxy;
						}
						acquireServerInfoAsyncResult.LatencyTracker.BeginTrackRmsLatency(RmsOperationType.AcquireServerLicensingMexData);
						acquireServerInfoAsyncResult.HttpClient.BeginDownload(acquireServerInfoAsyncResult.ServerLicensingMExUri, httpSessionConfig, RmsClientManagerUtils.WrapCancellableCallbackWithUnhandledExceptionHandlerAndUpdatePoisonContext(new CancelableAsyncCallback(RmsServerInfoManager.AcquireServerLicensingMexCallback)), acquireServerInfoAsyncResult);
					}
				}
				else
				{
					RmsClientManager.TraceFail(null, acquireServerInfoAsyncResult.Context.SystemProbeId, "Failed to download data from certification MEx {0}.  Exception is {1}", new object[]
					{
						acquireServerInfoAsyncResult.CertificationMExUri,
						downloadResult.Exception
					});
					ex = new RightsManagementException(RightsManagementFailureCode.FailedToDownloadMexData, ServerStrings.FailedToDownloadCertificationMExData(downloadResult.ResponseUri), downloadResult.Exception, acquireServerInfoAsyncResult.LicenseUri.ToString());
					((RightsManagementException)ex).IsPermanent = !downloadResult.IsRetryable;
				}
			}
			finally
			{
				if (ex != null)
				{
					acquireServerInfoAsyncResult.Release();
					RmsServerInfoManager.Tracer.TraceDebug(0L, "AcquireServiceLocationCallback: Invoking find service location callbacks");
					RmsServerInfoManager.outstandingFindServiceLocationCalls.InvokeCallbacks(acquireServerInfoAsyncResult.LicenseUri, ex);
				}
			}
		}

		// Token: 0x06006871 RID: 26737 RVA: 0x001BAC7C File Offset: 0x001B8E7C
		private static void AcquireServerLicensingMexCallback(ICancelableAsyncResult asyncResult)
		{
			RmsServerInfoManager.Tracer.TraceDebug(0L, "AcquireServerLicensingMexCallback invoked");
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			ArgumentValidator.ThrowIfNull("asyncResult.AsyncState", asyncResult.AsyncState);
			AcquireServerInfoAsyncResult acquireServerInfoAsyncResult = asyncResult.AsyncState as AcquireServerInfoAsyncResult;
			if (acquireServerInfoAsyncResult == null)
			{
				throw new InvalidOperationException("asyncResult.AsyncState has to be type of AcquireServerInfoAsyncResult.");
			}
			acquireServerInfoAsyncResult.AddBreadCrumb(Constants.State.AcquireServerLicensingMexCallback);
			Exception ex = null;
			try
			{
				DownloadResult downloadResult = acquireServerInfoAsyncResult.HttpClient.EndDownload(asyncResult);
				acquireServerInfoAsyncResult.LatencyTracker.EndTrackRmsLatency(RmsOperationType.AcquireServerLicensingMexData);
				if (downloadResult.IsSucceeded)
				{
					Uri targetUriFromResponse = RmsClientManagerUtils.GetTargetUriFromResponse(downloadResult.ResponseStream);
					if (targetUriFromResponse == null)
					{
						ex = new RightsManagementException(RightsManagementFailureCode.FailedToExtractTargetUriFromMex, ServerStrings.FailedToFindTargetUriFromMExData(acquireServerInfoAsyncResult.ServerLicensingMExUri), acquireServerInfoAsyncResult.LicenseUri.ToString());
					}
					else
					{
						acquireServerInfoAsyncResult.ServerInfo.ServerLicensingWSTargetUri = TokenTarget.Fix(targetUriFromResponse);
						RmsServerInfoManager.serverInfoMap.Add(acquireServerInfoAsyncResult.ServerInfo);
					}
				}
				else
				{
					RmsClientManager.TraceFail(null, acquireServerInfoAsyncResult.Context.SystemProbeId, "Failed to download data from server licensing MEx {0}.  Exception is {1}", new object[]
					{
						acquireServerInfoAsyncResult.ServerLicensingMExUri,
						downloadResult.Exception
					});
					ex = new RightsManagementException(RightsManagementFailureCode.FailedToDownloadMexData, ServerStrings.FailedToDownloadServerLicensingMExData(downloadResult.ResponseUri), downloadResult.Exception, acquireServerInfoAsyncResult.LicenseUri.ToString());
					((RightsManagementException)ex).IsPermanent = !downloadResult.IsRetryable;
				}
			}
			finally
			{
				acquireServerInfoAsyncResult.Release();
			}
			RmsServerInfoManager.Tracer.TraceDebug(0L, "AcquireServiceLocationCallback: Invoking find service location callbacks");
			RmsServerInfoManager.outstandingFindServiceLocationCalls.InvokeCallbacks(acquireServerInfoAsyncResult.LicenseUri, ex);
		}

		// Token: 0x06006872 RID: 26738 RVA: 0x001BAE0C File Offset: 0x001B900C
		private static bool ValidateResponsesAndUpdateServerInfo(AcquireServerInfoAsyncResult result, out LocalizedString errStr)
		{
			errStr = LocalizedString.Empty;
			if (result.ServiceLocationResponses == null)
			{
				errStr = ServerStrings.InvalidServiceLocationResponse;
				RmsServerInfoManager.Tracer.TraceError(0L, "ServiceLocation responses is null");
				return false;
			}
			if (result.ServiceLocationResponses.Length != 4)
			{
				errStr = ServerStrings.IncorrectEntriesInServiceLocationResponse(result.ServiceLocationResponses.Length, 4);
				RmsServerInfoManager.Tracer.TraceError<int>(0L, "Number of entries in the response is {0}. Expected 4", result.ServiceLocationResponses.Length);
				return false;
			}
			ServiceLocationResponse[] serviceLocationResponses = result.ServiceLocationResponses;
			int i = 0;
			while (i < serviceLocationResponses.Length)
			{
				ServiceLocationResponse serviceLocationResponse = serviceLocationResponses[i];
				Uri uri;
				bool result2;
				if (!RMUtil.TryCreateUri(serviceLocationResponse.URL, out uri))
				{
					errStr = ServerStrings.InvalidRmsUrl(serviceLocationResponse.Type, serviceLocationResponse.URL);
					RmsServerInfoManager.Tracer.TraceError<string>(0L, "Invalid response from the service location. {0} is an invalid URI", serviceLocationResponse.URL);
					result2 = false;
				}
				else
				{
					if (RMUtil.IsWellFormedRmServiceUrl(uri))
					{
						switch (serviceLocationResponse.Type)
						{
						case ServiceType.ServerLicensingWSService:
							if (uri.Scheme != Uri.UriSchemeHttps)
							{
								errStr = ServerStrings.InvalidUrlScheme(serviceLocationResponse.Type, uri);
								RmsServerInfoManager.Tracer.TraceError<Uri>(0L, "The Uri scheme for the server licensing WS service {0} is not https", uri);
								return false;
							}
							result.ServerInfo.ServerLicensingWSPipeline = uri;
							break;
						case ServiceType.CertificationWSService:
							if (uri.Scheme != Uri.UriSchemeHttps)
							{
								errStr = ServerStrings.InvalidUrlScheme(serviceLocationResponse.Type, uri);
								RmsServerInfoManager.Tracer.TraceError<Uri>(0L, "The Uri scheme for the certification WS service {0} is not https", uri);
								return false;
							}
							result.ServerInfo.CertificationWSPipeline = uri;
							break;
						case ServiceType.ServerLicensingMexService:
							result.ServerLicensingMExUri = RmsServerInfoManager.ConvertToWsdlUrl(serviceLocationResponse.URL);
							break;
						case ServiceType.CertificationMexService:
							result.CertificationMExUri = RmsServerInfoManager.ConvertToWsdlUrl(serviceLocationResponse.URL);
							break;
						}
						i++;
						continue;
					}
					errStr = ServerStrings.InvalidRmsUrl(serviceLocationResponse.Type, serviceLocationResponse.URL);
					RmsServerInfoManager.Tracer.TraceError<string>(0L, "Invalid response from the service location. {0} is an invalid RMS URI", serviceLocationResponse.URL);
					result2 = false;
				}
				return result2;
			}
			if (result.ServerInfo.CertificationWSPipeline == null || result.ServerInfo.ServerLicensingWSPipeline == null || result.CertificationMExUri == null || result.ServerLicensingMExUri == null)
			{
				errStr = ServerStrings.InvalidServiceLocationResponse;
				return false;
			}
			return true;
		}

		// Token: 0x06006873 RID: 26739 RVA: 0x001BB068 File Offset: 0x001B9268
		private static void ThrowIfNotInitialized()
		{
			if (!RmsServerInfoManager.initialized)
			{
				throw new InvalidOperationException("Server Info map is not initialized");
			}
		}

		// Token: 0x06006874 RID: 26740 RVA: 0x001BB07C File Offset: 0x001B927C
		private static Uri ConvertToWsdlUrl(string inputUri)
		{
			Uri result;
			if (inputUri.EndsWith("/mex", StringComparison.OrdinalIgnoreCase) && Uri.TryCreate(inputUri.Replace("/mex", "?wsdl"), UriKind.Absolute, out result))
			{
				return result;
			}
			RmsServerInfoManager.Tracer.TraceError<string>(0L, "The input Uri is not valid {0}", inputUri);
			return null;
		}

		// Token: 0x04003B5D RID: 15197
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;

		// Token: 0x04003B5E RID: 15198
		private static readonly PerTenantQueryController<Uri> outstandingFindServiceLocationCalls = new PerTenantQueryController<Uri>(EqualityComparer<Uri>.Default);

		// Token: 0x04003B5F RID: 15199
		private static ExternalRmsServerInfoMap serverInfoMap;

		// Token: 0x04003B60 RID: 15200
		private static RmsPerformanceCounters perfCounters;

		// Token: 0x04003B61 RID: 15201
		private static bool initialized;
	}
}
