using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000370 RID: 880
	internal class FindInGALSpeechRecognition : SpeechRecognition
	{
		// Token: 0x06001C57 RID: 7255 RVA: 0x00070BF9 File Offset: 0x0006EDF9
		public FindInGALSpeechRecognition(RequestParameters requestParameters, SpeechRecognitionResultsPriority priority) : base(requestParameters, priority)
		{
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00070CDC File Offset: 0x0006EEDC
		public override void AddRecoRequestAsync(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate callback)
		{
			FindInGALSpeechRecognition.<>c__DisplayClass14 CS$<>8__locals1 = new FindInGALSpeechRecognition.<>c__DisplayClass14();
			CS$<>8__locals1.callback = callback;
			CS$<>8__locals1.<>4__this = this;
			ValidateArgument.NotNull(CS$<>8__locals1.callback, "callback");
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<Guid>((long)this.GetHashCode(), "FindInGALSpeechRecognition.AddRecoRequestAsync - RequestId='{0}'", base.Parameters.RequestId);
			base.CollectAndLogStatisticsInformation(MobileSpeechRecoRequestStepLogId.AddRecoRequest, -1);
			try
			{
				FindInGALSpeechRecognition.<>c__DisplayClass16 CS$<>8__locals2 = new FindInGALSpeechRecognition.<>c__DisplayClass16();
				CS$<>8__locals2.CS$<>8__locals15 = CS$<>8__locals1;
				CS$<>8__locals2.orgMbox = null;
				this.LogMethodCallStatistics("FindInGALSpeechRecognition.AddRecoRequesetAsync - GetOrgMailbox", delegate
				{
					CS$<>8__locals2.orgMbox = CS$<>8__locals2.CS$<>8__locals15.<>4__this.GetOrgMailbox(CS$<>8__locals2.CS$<>8__locals15.<>4__this.Parameters.OrgId);
				});
				CS$<>8__locals2.orgMailboxExchangePrincipal = null;
				this.LogMethodCallStatistics("FindInGALSpeechRecognition.AddRecoRequesetAsync - ExchangePrincipal.FromADUser", delegate
				{
					CS$<>8__locals2.orgMailboxExchangePrincipal = ExchangePrincipal.FromADUser(CS$<>8__locals2.orgMbox, null);
				});
				CS$<>8__locals2.ewsUri = null;
				this.LogMethodCallStatistics("FindInGALSpeechRecognition.AddRecoRequesetAsync - BackEndLocator.GetBackEndWebServicesUrl", delegate
				{
					CS$<>8__locals2.ewsUri = BackEndLocator.GetBackEndWebServicesUrl(CS$<>8__locals2.orgMailboxExchangePrincipal.MailboxInfo);
				});
				ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<Guid, Uri>((long)this.GetHashCode(), "FindInGALSpeechRecognition.AddRecoRequestAsync - RequestId='{0}', EWS service URL='{1}'", base.Parameters.RequestId, CS$<>8__locals2.ewsUri);
				if (!(CS$<>8__locals2.ewsUri != null))
				{
					ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "FindInGALSpeechRecognition.AddRecoRequestAsync - ewsUri is null");
					throw new HttpException(404, Strings.CouldNotFindServiceUrl(CS$<>8__locals2.orgMbox.PrimarySmtpAddress.ToString()));
				}
				this.ewsUri = CS$<>8__locals2.ewsUri;
				this.orgMboxSmtpAddress = CS$<>8__locals2.orgMbox.PrimarySmtpAddress;
				FindInGALSpeechRecognitionEWSBinding ewsBinding = null;
				this.LogMethodCallStatistics("FindInGALSpeechRecognition.AddRecoRequesetAsync - FindInGALSpeechRecognitionEWSBinding", delegate
				{
					ewsBinding = new FindInGALSpeechRecognitionEWSBinding(CS$<>8__locals1.<>4__this.orgMboxSmtpAddress, CS$<>8__locals1.<>4__this.ewsUri, CS$<>8__locals1.callback);
				});
				StartFindInGALSpeechRecognitionType request = new StartFindInGALSpeechRecognitionType();
				request.Culture = base.Parameters.Culture.Name;
				request.TimeZone = base.Parameters.TimeZone.Id;
				request.TenantGuid = base.Parameters.TenantGuid.ToString();
				request.UserObjectGuid = base.Parameters.UserObjectGuid.ToString();
				this.LogMethodCallStatistics("FindInGALSpeechRecognition.AddRecoRequesetAsync - ewsBinding.BeginStartFindInGALSpeechRecognition", delegate
				{
					ewsBinding.BeginStartFindInGALSpeechRecognition(request, new AsyncCallback(CS$<>8__locals1.<>4__this.OnAddRecoRequestCompleted), ewsBinding);
				});
				ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<Guid>((long)this.GetHashCode(), "FindInGALSpeechRecognition.AddRecoRequestAsync - RequestId='{0}', Called BeginStartFindInGALSpeechRecognition", base.Parameters.RequestId);
			}
			catch (ObjectNotFoundException e)
			{
				this.HandleException(e, ResponseCodeType.ErrorInternalServerError, CS$<>8__locals1.callback);
			}
			catch (BackEndLocatorException e2)
			{
				this.HandleException(e2, ResponseCodeType.ErrorInternalServerError, CS$<>8__locals1.callback);
			}
			catch (HttpException e3)
			{
				this.HandleException(e3, ResponseCodeType.ErrorInternalServerError, CS$<>8__locals1.callback);
			}
			catch (SoapException e4)
			{
				this.HandleException(e4, ResponseCodeType.ErrorInternalServerError, CS$<>8__locals1.callback);
			}
			catch (WebException e5)
			{
				this.HandleException(e5, ResponseCodeType.ErrorInternalServerError, CS$<>8__locals1.callback);
			}
			catch (ArgumentOutOfRangeException e6)
			{
				this.HandleException(e6, ResponseCodeType.ErrorInvalidRequest, CS$<>8__locals1.callback);
			}
			catch (Exception e7)
			{
				this.HandleUnexpectedException(e7, CS$<>8__locals1.callback);
			}
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00071070 File Offset: 0x0006F270
		public override void RecognizeAsync(byte[] audioBytes, SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate callback)
		{
			ValidateArgument.NotNull(audioBytes, "audioBytes");
			ValidateArgument.NotNull(callback, "callback");
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<Guid, Uri>((long)this.GetHashCode(), "FindInGALSpeechRecognition.RecognizeAsync - RequestId='{0}', EWS URL='{1}'", base.Parameters.RequestId, this.ewsUri);
			base.CollectAndLogStatisticsInformation(MobileSpeechRecoRequestStepLogId.Recognize, audioBytes.Length);
			try
			{
				FindInGALSpeechRecognitionEWSBinding findInGALSpeechRecognitionEWSBinding = new FindInGALSpeechRecognitionEWSBinding(this.orgMboxSmtpAddress, this.ewsUri, callback);
				findInGALSpeechRecognitionEWSBinding.BeginCompleteFindInGALSpeechRecognition(new CompleteFindInGALSpeechRecognitionType
				{
					RecognitionId = this.recognitionId,
					AudioData = audioBytes
				}, new AsyncCallback(this.OnRecognizeCompleted), findInGALSpeechRecognitionEWSBinding);
				ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<Guid>((long)this.GetHashCode(), "FindInGALSpeechRecognition.RecognizeAsync - RequestId='{0}', Called BeginCompleteFindInGALSpeechRecognition", base.Parameters.RequestId);
			}
			catch (WebException e)
			{
				this.HandleException(e, ResponseCodeType.ErrorInternalServerError, callback);
			}
			catch (Exception e2)
			{
				this.HandleUnexpectedException(e2, callback);
			}
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0007115C File Offset: 0x0006F35C
		private static SpeechRecognitionProcessor.SpeechHttpStatus MapEwsResponseCodeToHttpErrorCode(ResponseCodeType responseCode)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug(0L, "FindInGALSpeechRecognition.MapEwsResponseCodeToHttpErrorCode");
			if (FindInGALSpeechRecognition.errorCodeMapping.ContainsKey(responseCode))
			{
				return FindInGALSpeechRecognition.errorCodeMapping[responseCode];
			}
			return SpeechRecognitionProcessor.SpeechHttpStatus.InternalServerError;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00071190 File Offset: 0x0006F390
		private void LogMethodCallStatistics(string methodName, Action action)
		{
			Stopwatch stopwatch = new Stopwatch();
			ExDateTime utcNow = ExDateTime.UtcNow;
			stopwatch.Start();
			action();
			stopwatch.Stop();
			base.CollectAndLogMethodCallStatisticInformation(methodName, utcNow, stopwatch.Elapsed);
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x000711CC File Offset: 0x0006F3CC
		private void OnAddRecoRequestCompleted(IAsyncResult asyncResult)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Entering FindInGALSpeechRecognition.OnAddRecoRequestCompleted - RequestId='{0}'", base.Parameters.RequestId);
			base.CollectAndLogStatisticsInformation(MobileSpeechRecoRequestStepLogId.AddRecoRequestCompleted, -1);
			FindInGALSpeechRecognitionEWSBinding findInGALSpeechRecognitionEWSBinding = asyncResult.AsyncState as FindInGALSpeechRecognitionEWSBinding;
			SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate speechProcessorAsyncCompletedDelegate = findInGALSpeechRecognitionEWSBinding.State as SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate;
			try
			{
				StartFindInGALSpeechRecognitionResponseMessageType startFindInGALSpeechRecognitionResponseMessageType = findInGALSpeechRecognitionEWSBinding.EndStartFindInGALSpeechRecognition(asyncResult);
				this.recognitionId = startFindInGALSpeechRecognitionResponseMessageType.RecognitionId;
				SpeechRecognitionProcessor.SpeechHttpStatus httpStatus = FindInGALSpeechRecognition.MapEwsResponseCodeToHttpErrorCode(startFindInGALSpeechRecognitionResponseMessageType.ResponseCode);
				SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args = new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(string.Empty, httpStatus);
				speechProcessorAsyncCompletedDelegate(args);
			}
			catch (WebException e)
			{
				this.HandleException(e, ResponseCodeType.ErrorInternalServerError, speechProcessorAsyncCompletedDelegate);
			}
			catch (SoapException e2)
			{
				this.HandleException(e2, ResponseCodeType.ErrorInternalServerError, speechProcessorAsyncCompletedDelegate);
			}
			catch (Exception e3)
			{
				this.HandleUnexpectedException(e3, speechProcessorAsyncCompletedDelegate);
			}
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000712A4 File Offset: 0x0006F4A4
		private ADUser GetOrgMailbox(OrganizationId orgId)
		{
			ADUser organizationMailboxForRouting = GrammarMailboxFileStore.GetOrganizationMailboxForRouting(orgId);
			if (organizationMailboxForRouting == null)
			{
				ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "FindInGALSpeechRecognition.GetOrgMailbox - GetOrganizationMailboxForRouting returned no mailboxes, orgId='{0}'", orgId);
				throw new HttpException(404, Strings.CouldNotFindOrgMailbox);
			}
			return organizationMailboxForRouting;
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x000712E4 File Offset: 0x0006F4E4
		private void OnRecognizeCompleted(IAsyncResult asyncResult)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Entering SpeechRecognition.OnRecognizeCompleted - RequestId='{0}'", base.Parameters.RequestId);
			base.CollectAndLogStatisticsInformation(MobileSpeechRecoRequestStepLogId.RecognizeCompleted, -1);
			FindInGALSpeechRecognitionEWSBinding findInGALSpeechRecognitionEWSBinding = asyncResult.AsyncState as FindInGALSpeechRecognitionEWSBinding;
			SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate speechProcessorAsyncCompletedDelegate = findInGALSpeechRecognitionEWSBinding.State as SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate;
			try
			{
				CompleteFindInGALSpeechRecognitionResponseMessageType completeFindInGALSpeechRecognitionResponseMessageType = findInGALSpeechRecognitionEWSBinding.EndCompleteFindInGALSpeechRecognition(asyncResult);
				SpeechRecognitionProcessor.SpeechHttpStatus speechHttpStatus = FindInGALSpeechRecognition.MapEwsResponseCodeToHttpErrorCode(completeFindInGALSpeechRecognitionResponseMessageType.ResponseCode);
				SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args = new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs((speechHttpStatus == SpeechRecognitionProcessor.SpeechHttpStatus.Success) ? completeFindInGALSpeechRecognitionResponseMessageType.RecognitionResult.Result : string.Empty, speechHttpStatus);
				speechProcessorAsyncCompletedDelegate(args);
			}
			catch (WebException e)
			{
				this.HandleException(e, ResponseCodeType.ErrorInternalServerError, speechProcessorAsyncCompletedDelegate);
			}
			catch (Exception e2)
			{
				this.HandleUnexpectedException(e2, speechProcessorAsyncCompletedDelegate);
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x000713AC File Offset: 0x0006F5AC
		private void HandleUnexpectedException(Exception e, SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate callback)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceError<Exception>((long)this.GetHashCode(), "FindInGALSpeechRecognition - HandleUnexpectedException='{0}'", e);
			ExWatson.SendReport(e, ReportOptions.None, null);
			this.HandleException(e, ResponseCodeType.ErrorInternalServerError, callback);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x000713D8 File Offset: 0x0006F5D8
		private void HandleException(Exception e, ResponseCodeType responseCode, SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate callback)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceError<Exception, ResponseCodeType>((long)this.GetHashCode(), "FindInGALSpeechRecognition - Exception='{0}', Error Code='{1}'", e, responseCode);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SpeechRecoRequestFailed, null, new object[]
			{
				base.RequestId,
				base.Parameters.UserObjectGuid,
				base.Parameters.TenantGuid,
				CommonUtil.ToEventLogString(e)
			});
			SpeechRecognitionProcessor.SpeechHttpStatus httpStatus = FindInGALSpeechRecognition.MapEwsResponseCodeToHttpErrorCode(responseCode);
			callback(new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(string.Empty, httpStatus));
		}

		// Token: 0x04001006 RID: 4102
		private static readonly Dictionary<ResponseCodeType, SpeechRecognitionProcessor.SpeechHttpStatus> errorCodeMapping = new Dictionary<ResponseCodeType, SpeechRecognitionProcessor.SpeechHttpStatus>
		{
			{
				ResponseCodeType.NoError,
				SpeechRecognitionProcessor.SpeechHttpStatus.Success
			},
			{
				ResponseCodeType.ErrorUMServerUnavailable,
				SpeechRecognitionProcessor.SpeechHttpStatus.ServiceUnavailable
			},
			{
				ResponseCodeType.ErrorInvalidRequest,
				SpeechRecognitionProcessor.SpeechHttpStatus.BadRequest
			},
			{
				ResponseCodeType.ErrorRecipientNotFound,
				SpeechRecognitionProcessor.SpeechHttpStatus.BadRequest
			},
			{
				ResponseCodeType.ErrorRecognizerNotInstalled,
				SpeechRecognitionProcessor.SpeechHttpStatus.BadRequest
			},
			{
				ResponseCodeType.ErrorNoSpeechDetected,
				SpeechRecognitionProcessor.SpeechHttpStatus.NoSpeechDetected
			}
		};

		// Token: 0x04001007 RID: 4103
		private Uri ewsUri;

		// Token: 0x04001008 RID: 4104
		private SmtpAddress orgMboxSmtpAddress;

		// Token: 0x04001009 RID: 4105
		private RecognitionIdType recognitionId;
	}
}
