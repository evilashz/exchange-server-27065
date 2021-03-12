using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000395 RID: 917
	internal class SpeechRecognitionProcessor
	{
		// Token: 0x06001D50 RID: 7504 RVA: 0x00074AA8 File Offset: 0x00072CA8
		public SpeechRecognitionProcessor(HttpContext httpContext)
		{
			this.speechScenario = SpeechRecognitionProcessor.CreateSpeechRecognitionScenario(httpContext, out this.budget);
			this.HttpContext = httpContext;
			this.parameters = this.speechScenario.Parameters;
			this.userContext = this.speechScenario.UserContext;
			this.maxAudioSize = AppConfigLoader.GetConfigIntValue("SpeechRecognitionMaxAudioSize", 0, int.MaxValue, 32500);
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x00074B1C File Offset: 0x00072D1C
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x00074B24 File Offset: 0x00072D24
		public HttpContext HttpContext { get; private set; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x00074B30 File Offset: 0x00072D30
		private string RequestId
		{
			get
			{
				if (this.parameters == null)
				{
					return string.Empty;
				}
				return this.parameters.RequestId.ToString();
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x00074B64 File Offset: 0x00072D64
		private string UserObjectGuid
		{
			get
			{
				if (this.parameters == null)
				{
					return string.Empty;
				}
				return this.parameters.UserObjectGuid.ToString();
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x00074B98 File Offset: 0x00072D98
		private string TenantGuid
		{
			get
			{
				if (this.parameters == null)
				{
					return string.Empty;
				}
				return this.parameters.TenantGuid.ToString();
			}
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x00074BCC File Offset: 0x00072DCC
		public IAsyncResult BeginRecognition(AsyncCallback callback, object context)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionProcessor.BeginRecognition");
			this.CollectAndLogStatisticsInformation(SpeechLoggerProcessType.BeginRequest, -1);
			this.clientCallback = callback;
			this.asyncResult = new SpeechRecognitionAsyncResult(new AsyncCallback(this.OnRequestCompleted), context);
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoRecognition));
			return this.asyncResult;
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x00074C30 File Offset: 0x00072E30
		private static SpeechRecognitionScenarioBase CreateSpeechRecognitionScenario(HttpContext httpContext, out IStandardBudget budget)
		{
			ValidateArgument.NotNull(httpContext, "httpContext is null");
			budget = null;
			Exception ex = null;
			SpeechRecognitionScenarioBase result = null;
			try
			{
				Guid guid = Guid.NewGuid();
				string text;
				MobileSpeechRecoRequestType mobileSpeechRecoRequestType;
				CultureInfo cultureInfo;
				ExTimeZone exTimeZone;
				SpeechRecognitionProcessor.GetQueryStringParameters(httpContext.Request, out text, out mobileSpeechRecoRequestType, out cultureInfo, out exTimeZone);
				ExTraceGlobals.SpeechRecognitionTracer.TraceDebug(0L, "SpeechRecognitionProcessor.CreateSpeechRecognitionProcessor - requestId='{0}', tag='{1}', requestType='{2}', culture='{3}', timeZone='{4}'", new object[]
				{
					guid,
					text,
					mobileSpeechRecoRequestType,
					cultureInfo,
					exTimeZone
				});
				Guid guid2;
				Guid guid3;
				OrganizationId organizationId;
				UserContext userContext;
				SpeechRecognitionProcessor.GetUserIdentity(httpContext, out guid2, out guid3, out organizationId, out userContext);
				ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<Guid, Guid, OrganizationId>(0L, "SpeechRecognitionProcessor.CreateSpeechRecognitionProcessor - userObjectGuid='{0}', tenantGuid='{1}', orgId='{2}'", guid2, guid3, organizationId);
				RequestParameters requestParameters = new RequestParameters(guid, text, mobileSpeechRecoRequestType, cultureInfo, exTimeZone, guid2, guid3, organizationId);
				switch (mobileSpeechRecoRequestType)
				{
				case MobileSpeechRecoRequestType.FindInGAL:
				case MobileSpeechRecoRequestType.FindInPersonalContacts:
				case MobileSpeechRecoRequestType.StaticGrammarsCombined:
					throw new ArgumentOutOfRangeException("operation", mobileSpeechRecoRequestType, "Invalid parameter");
				case MobileSpeechRecoRequestType.FindPeople:
					result = new FindPeopleSpeechRecognitionScenario(requestParameters, userContext);
					break;
				case MobileSpeechRecoRequestType.CombinedScenarios:
					result = new CombinedSpeechRecognitionScenario(requestParameters, userContext);
					break;
				case MobileSpeechRecoRequestType.DaySearch:
				case MobileSpeechRecoRequestType.AppointmentCreation:
					result = new SingleSpeechRecognitionScenario(requestParameters, userContext);
					break;
				default:
					ExAssert.RetailAssert(false, "Invalid request type '{0}'", new object[]
					{
						mobileSpeechRecoRequestType
					});
					break;
				}
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SpeechRecoRequestParams, null, new object[]
				{
					guid,
					text,
					mobileSpeechRecoRequestType,
					cultureInfo,
					exTimeZone,
					guid2,
					guid3,
					organizationId
				});
				string text2 = null;
				HttpRequest request = httpContext.Request;
				if (request.QueryString != null)
				{
					text2 = request.QueryString.ToString();
				}
				if (request.Headers != null && !string.IsNullOrEmpty(request.Headers["X-OWA-CorrelationId"]))
				{
					text2 = text2 + "." + request.Headers["X-OWA-CorrelationId"];
				}
				SpeechRecognitionProcessor.InitializeThrottlingBudget(userContext, text2, out budget);
			}
			catch (OverBudgetException ex2)
			{
				ex = ex2;
			}
			catch (ArgumentOutOfRangeException ex3)
			{
				ex = ex3;
			}
			catch (Exception ex4)
			{
				ex = ex4;
				ExWatson.SendReport(ex4, ReportOptions.None, null);
			}
			finally
			{
				if (ex != null)
				{
					ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<Exception>(0L, "SpeechRecognitionProcessor.CreateSpeechRecognitionProcessor - Exception='{0}'", ex);
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_InvalidSpeechRecoRequest, null, new object[]
					{
						CommonUtil.ToEventLogString(ex)
					});
					SpeechRecognitionProcessor.SpeechHttpStatus status = SpeechRecognitionProcessor.MapInvalidRequestToHttpStatus(ex);
					result = new InvalidRequestSpeechRecognitionScenario(status);
				}
			}
			return result;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x00074F04 File Offset: 0x00073104
		private static void InitializeThrottlingBudget(UserContext userContext, string description, out IStandardBudget budget)
		{
			ValidateArgument.NotNull(userContext, "userContext is null");
			budget = null;
			try
			{
				string callerInfo = string.IsNullOrEmpty(description) ? "SpeechRecognitionProcessor.InitializeThrottlingBudget" : description;
				budget = StandardBudget.Acquire(userContext.ExchangePrincipal.Sid, BudgetType.OwaVoice, userContext.ExchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings());
				budget.CheckOverBudget();
				budget.StartConnection(callerInfo);
				budget.StartLocal(callerInfo, default(TimeSpan));
			}
			catch (Exception ex)
			{
				if (budget != null)
				{
					budget.Dispose();
					budget = null;
				}
				throw ex;
			}
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x00074FA0 File Offset: 0x000731A0
		private static void GetQueryStringParameters(HttpRequest request, out string tag, out MobileSpeechRecoRequestType requestType, out CultureInfo culture, out ExTimeZone timeZone)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug(0L, "Entering SpeechRecognitionProcessor.GetQueryStringParameters");
			NameValueCollection queryString = request.QueryString;
			string text = queryString["tag"];
			string text2 = queryString["operation"];
			string text3 = queryString["culture"];
			string text4 = queryString["timezone"];
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentOutOfRangeException("tag", "Parameter was not specified");
			}
			tag = text;
			if (string.IsNullOrEmpty(text4))
			{
				throw new ArgumentOutOfRangeException("timezone", "Parameter was not specified");
			}
			if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(text4, out timeZone))
			{
				throw new ArgumentOutOfRangeException("timezone", text4, "Invalid parameter");
			}
			if (text2 == null)
			{
				throw new ArgumentOutOfRangeException("operation", "Parameter was not specified");
			}
			if (!EnumValidator.TryParse<MobileSpeechRecoRequestType>(text2, EnumParseOptions.IgnoreCase, out requestType))
			{
				throw new ArgumentOutOfRangeException("operation", text2, "Invalid parameter");
			}
			if (text3 == null)
			{
				throw new ArgumentOutOfRangeException("culture", "Parameter was not specified");
			}
			try
			{
				culture = new CultureInfo(text3);
			}
			catch (ArgumentException ex)
			{
				throw new ArgumentOutOfRangeException("culture", text3, ex.Message);
			}
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x000750BC File Offset: 0x000732BC
		private static void GetUserIdentity(HttpContext httpContext, out Guid userObjectGuid, out Guid tenantGuid, out OrganizationId orgId, out UserContext userContext)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug(0L, "Entering SpeechRecognitionProcessor.GetUserIdentity");
			userContext = UserContextManager.GetUserContext(httpContext);
			userObjectGuid = userContext.ExchangePrincipal.ObjectId.ObjectGuid;
			orgId = userContext.ExchangePrincipal.MailboxInfo.OrganizationId;
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(orgId);
			tenantGuid = iadsystemConfigurationLookup.GetExternalDirectoryOrganizationId();
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<Guid, Guid, OrganizationId>(0L, "SpeechRecognitionProcessor.GetUserIdentity - userObjectGuid='{0}', tenantGuid='{1}', orgId='{2}'", userObjectGuid, tenantGuid, orgId);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x00075142 File Offset: 0x00073342
		private static SpeechRecognitionProcessor.SpeechHttpStatus MapInvalidRequestToHttpStatus(Exception ex)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<Exception>(0L, "SpeechRecognitionProcessor.MapInvalidRequestToHttpStatus - ex='{0}'", ex);
			if (ex is ArgumentOutOfRangeException)
			{
				return SpeechRecognitionProcessor.SpeechHttpStatus.BadRequest;
			}
			if (ex is OverBudgetException)
			{
				return SpeechRecognitionProcessor.SpeechHttpStatus.MaxRequestsExceeded;
			}
			return SpeechRecognitionProcessor.SpeechHttpStatus.InternalServerError;
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x00075178 File Offset: 0x00073378
		private void DoRecognition(object state)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionProcessor.DoRecognition");
			try
			{
				this.speechScenario.StartRecoRequestAsync(new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate(this.OnRecognizeCompleted));
				this.CollectAndLogStatisticsInformation(SpeechLoggerProcessType.BeginReadAudio, -1);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ReadAudioBytesAsync));
				ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "SpeechRecognitionProcessor.DoRecognition - Called scenario.StartRecoRequestAsync");
			}
			catch (Exception e)
			{
				this.HandleUnexpectedException(e);
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x00075200 File Offset: 0x00073400
		private void ReadAudioBytesAsync(object state)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionProcessor.ReadAudioBytesAsync");
			SpeechRecognitionProcessor.SpeechStreamBuffer speechStreamBuffer = new SpeechRecognitionProcessor.SpeechStreamBuffer();
			try
			{
				this.audioMemoryStream = new MemoryStream(16250);
				speechStreamBuffer.AudioStream = this.HttpContext.Request.GetBufferlessInputStream();
				speechStreamBuffer.AudioBuffer = new byte[400];
				speechStreamBuffer.AudioStream.BeginRead(speechStreamBuffer.AudioBuffer, 0, 400, new AsyncCallback(this.ReadAudioChunkCompleted), speechStreamBuffer);
			}
			catch (HttpException e)
			{
				this.SignalRecognizeWithEmptyAudioAndBailOut(speechStreamBuffer, e, SpeechRecognitionProcessor.SpeechHttpStatus.InternalServerError, true);
			}
			catch (Exception e2)
			{
				this.SignalRecognizeWithEmptyAudioAndBailOut(speechStreamBuffer, e2, SpeechRecognitionProcessor.SpeechHttpStatus.InternalServerError, false);
			}
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x000752C4 File Offset: 0x000734C4
		private void ReadAudioChunkCompleted(IAsyncResult asyncResult)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionProcessor.BeginReadAudioBytes");
			SpeechRecognitionProcessor.SpeechStreamBuffer speechStreamBuffer = null;
			try
			{
				speechStreamBuffer = (asyncResult.AsyncState as SpeechRecognitionProcessor.SpeechStreamBuffer);
				int num = speechStreamBuffer.AudioStream.EndRead(asyncResult);
				if (num == 0)
				{
					ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "SpeechRecognitionProcessor.BeginReadAudioBytes - End of stream");
					this.SignalRecognizeAsync();
					speechStreamBuffer.Dispose();
				}
				else
				{
					ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "SpeechRecognitionProcessor.BeginReadAudioBytes - Read chunk");
					this.audioMemoryStream.Write(speechStreamBuffer.AudioBuffer, 0, num);
					if (this.audioMemoryStream.Length > (long)this.maxAudioSize)
					{
						ExTraceGlobals.SpeechRecognitionTracer.TraceError<int>((long)this.GetHashCode(), "SpeechRecognitionProcessor.BeginReadAudioBytes - Max audio size ({0}) exceeded", this.maxAudioSize);
						this.SignalRecognizeWithEmptyAudioAndBailOut(speechStreamBuffer, new ArgumentException("Max audio size exceeded"), SpeechRecognitionProcessor.SpeechHttpStatus.BadRequest, true);
					}
					else
					{
						speechStreamBuffer.AudioStream.BeginRead(speechStreamBuffer.AudioBuffer, 0, 400, new AsyncCallback(this.ReadAudioChunkCompleted), speechStreamBuffer);
					}
				}
			}
			catch (HttpException e)
			{
				this.SignalRecognizeWithEmptyAudioAndBailOut(speechStreamBuffer, e, SpeechRecognitionProcessor.SpeechHttpStatus.InternalServerError, true);
			}
			catch (Exception e2)
			{
				this.SignalRecognizeWithEmptyAudioAndBailOut(speechStreamBuffer, e2, SpeechRecognitionProcessor.SpeechHttpStatus.InternalServerError, false);
			}
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x00075404 File Offset: 0x00073604
		private void SignalRecognizeWithEmptyAudioAndBailOut(SpeechRecognitionProcessor.SpeechStreamBuffer speechStreamBuffer, Exception e, SpeechRecognitionProcessor.SpeechHttpStatus status, bool expectedException)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceError<string, int, string>((long)this.GetHashCode(), "SignalRecognizeWithEmptyAudioAndBailOut - exception='{0}' Status code='{1}', Status message='{2}'", e.Message, status.StatusCode, status.StatusDescription);
			if (this.audioMemoryStream != null)
			{
				this.audioMemoryStream.Close();
			}
			this.audioMemoryStream = new MemoryStream();
			this.SignalRecognizeAsync();
			if (speechStreamBuffer != null)
			{
				speechStreamBuffer.Dispose();
			}
			if (expectedException)
			{
				this.HandleException(e, status);
				return;
			}
			this.HandleUnexpectedException(e);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0007547C File Offset: 0x0007367C
		private void SignalRecognizeAsync()
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionProcessor.SignalRecognizeAsync");
			this.CollectAndLogStatisticsInformation(SpeechLoggerProcessType.EndReadAudio, this.audioMemoryStream.ToArray().Length);
			this.speechScenario.SetAudio(this.audioMemoryStream.ToArray());
			this.audioMemoryStream.Close();
			this.audioMemoryStream = null;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x000754DB File Offset: 0x000736DB
		private void OnRecognizeCompleted(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionProcessor.OnRecognizeCompleted");
			if (args.HttpStatus == SpeechRecognitionProcessor.SpeechHttpStatus.Success)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.HandleRecoResults), args);
				return;
			}
			this.CompleteRequest(args);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0007551C File Offset: 0x0007371C
		private void HandleRecoResults(object state)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionProcessor.HandleRecoResults");
			try
			{
				SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs speechProcessorAsyncCompletedArgs = (SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs)state;
				string responseText;
				SpeechRecognitionProcessor.SpeechHttpStatus httpStatus;
				SpeechRecognitionResultHandler.HandleRecoResult(speechProcessorAsyncCompletedArgs.ResponseText, this.parameters, this.HttpContext, this.userContext, out responseText, out httpStatus);
				SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args = new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(responseText, httpStatus);
				this.CompleteRequest(args);
			}
			catch (ArgumentException e)
			{
				this.HandleException(e, SpeechRecognitionProcessor.SpeechHttpStatus.BadRequest);
			}
			catch (Exception e2)
			{
				this.HandleUnexpectedException(e2);
			}
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000755B4 File Offset: 0x000737B4
		private void CompleteRequest(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args)
		{
			if (this.IsSpeechRequestNotCompleted())
			{
				ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<int, string>((long)this.GetHashCode(), "SpeechRecognitionProcessor - Status code='{0}', Status message='{1}'", args.HttpStatus.StatusCode, args.HttpStatus.StatusDescription);
				this.CollectAndLogStatisticsInformation(SpeechLoggerProcessType.RequestCompleted, -1);
				this.asyncResult.StatusCode = args.HttpStatus.StatusCode;
				this.asyncResult.StatusDescription = args.HttpStatus.StatusDescription;
				this.asyncResult.ResponseText = args.ResponseText;
				this.asyncResult.ThrottlingDelay = -1.0;
				this.asyncResult.ThrottlingNotEnforcedReason = string.Empty;
				if (this.budget != null)
				{
					try
					{
						this.budget.EndLocal();
						DelayEnforcementResults delayEnforcementResults = ResourceLoadDelayInfo.EnforceDelay(this.budget, new WorkloadSettings(WorkloadType.OwaVoice, false), null, TimeSpan.MaxValue, null);
						if (delayEnforcementResults != null && delayEnforcementResults.DelayInfo != null)
						{
							ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "SpeechRecognitionProcessor - Request id={0}, Delayed amount={1}s, Capped delay={2}s, Delay Required={3}, NotEnforcedReason={4}", new object[]
							{
								this.RequestId,
								delayEnforcementResults.DelayedAmount.TotalSeconds,
								delayEnforcementResults.DelayInfo.Delay.TotalSeconds,
								delayEnforcementResults.DelayInfo.Required,
								delayEnforcementResults.NotEnforcedReason
							});
							this.asyncResult.ThrottlingDelay = delayEnforcementResults.DelayedAmount.TotalSeconds;
							this.asyncResult.ThrottlingNotEnforcedReason = delayEnforcementResults.NotEnforcedReason;
						}
						this.budget.EndConnection();
					}
					finally
					{
						this.budget.Dispose();
						this.budget = null;
					}
				}
				this.asyncResult.IsCompleted = true;
				return;
			}
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "SpeechRecognitionProcessor.CompleteRequest: speech request already completed, ignoring this request.");
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x00075798 File Offset: 0x00073998
		private bool IsSpeechRequestNotCompleted()
		{
			int num = Interlocked.CompareExchange(ref this.speechRequestCompleted, 1, 0);
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<int>((long)this.GetHashCode(), "SpeechRecognitionProcessor.SpeechRequestNotCompleted value of speechRequestCompleted:{0}", num);
			return num == 0;
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x000757CE File Offset: 0x000739CE
		private void OnRequestCompleted(IAsyncResult asyncResult)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionProcessor.OnRequestCompleted");
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.InvokeClientCallback), asyncResult);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000757FC File Offset: 0x000739FC
		private void InvokeClientCallback(object state)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionProcessor.InvokeClientCallback");
			if (this.clientCallback != null)
			{
				SpeechRecognitionAsyncResult speechRecognitionAsyncResult = state as SpeechRecognitionAsyncResult;
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SpeechRecoRequestCompleted, null, new object[]
				{
					this.RequestId,
					speechRecognitionAsyncResult.StatusCode,
					CommonUtil.ToEventLogString(speechRecognitionAsyncResult.StatusDescription),
					CommonUtil.ToEventLogString(speechRecognitionAsyncResult.ResponseText),
					speechRecognitionAsyncResult.ThrottlingDelay,
					CommonUtil.ToEventLogString(speechRecognitionAsyncResult.ThrottlingNotEnforcedReason)
				});
				this.clientCallback(state as IAsyncResult);
			}
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000758AA File Offset: 0x00073AAA
		private void HandleUnexpectedException(Exception e)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceError<Exception>((long)this.GetHashCode(), "SpeechRecognitionProcessor - HandleUnexpectedException='{0}'", e);
			ExWatson.SendReport(e, ReportOptions.None, null);
			this.HandleException(e, SpeechRecognitionProcessor.SpeechHttpStatus.InternalServerError);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000758D8 File Offset: 0x00073AD8
		private void HandleException(Exception e, SpeechRecognitionProcessor.SpeechHttpStatus status)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceError<Exception, int, string>((long)this.GetHashCode(), "SpeechRecognitionProcessor - Exception='{0}', Status Code='{1}', Status Description='{2}'", e, status.StatusCode, status.StatusDescription);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SpeechRecoRequestFailed, null, new object[]
			{
				this.RequestId,
				this.UserObjectGuid,
				this.TenantGuid,
				CommonUtil.ToEventLogString(e)
			});
			this.CompleteRequest(new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(string.Empty, status));
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x00075958 File Offset: 0x00073B58
		private void CollectAndLogStatisticsInformation(SpeechLoggerProcessType processType, int audioLength)
		{
			if (this.parameters != null)
			{
				SpeechProcessorStatisticsLogger.SpeechProcessorStatisticsLogRow row = this.CollectStatisticsLog(processType, audioLength);
				SpeechRecognitionHandler.SpeechProcessorStatisticsLogger.Append(row);
			}
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00075984 File Offset: 0x00073B84
		private SpeechProcessorStatisticsLogger.SpeechProcessorStatisticsLogRow CollectStatisticsLog(SpeechLoggerProcessType processType, int audioLength)
		{
			return new SpeechProcessorStatisticsLogger.SpeechProcessorStatisticsLogRow
			{
				RequestId = this.parameters.RequestId,
				Culture = this.parameters.Culture,
				Tag = this.parameters.Tag,
				TenantGuid = new Guid?(this.parameters.TenantGuid),
				UserObjectGuid = new Guid?(this.parameters.UserObjectGuid),
				TimeZone = this.parameters.TimeZone.ToString(),
				StartTime = ExDateTime.UtcNow,
				ProcessType = new SpeechLoggerProcessType?(processType),
				AudioLength = audioLength
			};
		}

		// Token: 0x04001089 RID: 4233
		private const string TagParameterName = "tag";

		// Token: 0x0400108A RID: 4234
		private const string RequestTypeParameterName = "operation";

		// Token: 0x0400108B RID: 4235
		private const string CultureParameterName = "culture";

		// Token: 0x0400108C RID: 4236
		private const string TimeZoneParameterName = "timezone";

		// Token: 0x0400108D RID: 4237
		private const string SpeechRecognitionMaxAudioSize = "SpeechRecognitionMaxAudioSize";

		// Token: 0x0400108E RID: 4238
		private const int AudioChunkReadSize = 400;

		// Token: 0x0400108F RID: 4239
		private const int DefaultMaxAudioSize = 32500;

		// Token: 0x04001090 RID: 4240
		private const int TypicalAudioSize = 16250;

		// Token: 0x04001091 RID: 4241
		private readonly int maxAudioSize = 32500;

		// Token: 0x04001092 RID: 4242
		private AsyncCallback clientCallback;

		// Token: 0x04001093 RID: 4243
		private SpeechRecognitionAsyncResult asyncResult;

		// Token: 0x04001094 RID: 4244
		private MemoryStream audioMemoryStream;

		// Token: 0x04001095 RID: 4245
		private int speechRequestCompleted;

		// Token: 0x04001096 RID: 4246
		private IStandardBudget budget;

		// Token: 0x04001097 RID: 4247
		private SpeechRecognitionScenarioBase speechScenario;

		// Token: 0x04001098 RID: 4248
		private RequestParameters parameters;

		// Token: 0x04001099 RID: 4249
		private UserContext userContext;

		// Token: 0x02000396 RID: 918
		// (Invoke) Token: 0x06001D6C RID: 7532
		internal delegate void SpeechProcessorAsyncCompletedDelegate(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args);

		// Token: 0x02000397 RID: 919
		// (Invoke) Token: 0x06001D70 RID: 7536
		internal delegate void SpeechProcessorResultsCompletedDelegate(SpeechRecognition helper);

		// Token: 0x02000398 RID: 920
		internal class SpeechProcessorAsyncCompletedArgs
		{
			// Token: 0x06001D73 RID: 7539 RVA: 0x00075A2B File Offset: 0x00073C2B
			public SpeechProcessorAsyncCompletedArgs(string responseText, SpeechRecognitionProcessor.SpeechHttpStatus httpStatus)
			{
				this.ResponseText = responseText;
				this.HttpStatus = httpStatus;
			}

			// Token: 0x17000698 RID: 1688
			// (get) Token: 0x06001D74 RID: 7540 RVA: 0x00075A41 File Offset: 0x00073C41
			// (set) Token: 0x06001D75 RID: 7541 RVA: 0x00075A49 File Offset: 0x00073C49
			public string ResponseText { get; private set; }

			// Token: 0x17000699 RID: 1689
			// (get) Token: 0x06001D76 RID: 7542 RVA: 0x00075A52 File Offset: 0x00073C52
			// (set) Token: 0x06001D77 RID: 7543 RVA: 0x00075A5A File Offset: 0x00073C5A
			public SpeechRecognitionProcessor.SpeechHttpStatus HttpStatus { get; private set; }
		}

		// Token: 0x02000399 RID: 921
		internal class SpeechHttpStatus
		{
			// Token: 0x06001D78 RID: 7544 RVA: 0x00075A63 File Offset: 0x00073C63
			private SpeechHttpStatus(int statusCode, string statusDescription)
			{
				this.StatusCode = statusCode;
				this.StatusDescription = statusDescription;
			}

			// Token: 0x1700069A RID: 1690
			// (get) Token: 0x06001D79 RID: 7545 RVA: 0x00075A79 File Offset: 0x00073C79
			// (set) Token: 0x06001D7A RID: 7546 RVA: 0x00075A81 File Offset: 0x00073C81
			public int StatusCode { get; private set; }

			// Token: 0x1700069B RID: 1691
			// (get) Token: 0x06001D7B RID: 7547 RVA: 0x00075A8A File Offset: 0x00073C8A
			// (set) Token: 0x06001D7C RID: 7548 RVA: 0x00075A92 File Offset: 0x00073C92
			public string StatusDescription { get; private set; }

			// Token: 0x0400109D RID: 4253
			public static readonly SpeechRecognitionProcessor.SpeechHttpStatus Success = new SpeechRecognitionProcessor.SpeechHttpStatus(200, null);

			// Token: 0x0400109E RID: 4254
			public static readonly SpeechRecognitionProcessor.SpeechHttpStatus NoSpeechDetected = new SpeechRecognitionProcessor.SpeechHttpStatus(451, "No Speech Detected");

			// Token: 0x0400109F RID: 4255
			public static readonly SpeechRecognitionProcessor.SpeechHttpStatus MaxRequestsExceeded = new SpeechRecognitionProcessor.SpeechHttpStatus(452, "Maximum Requests Exceeded");

			// Token: 0x040010A0 RID: 4256
			public static readonly SpeechRecognitionProcessor.SpeechHttpStatus NoContactWithEmailAddress = new SpeechRecognitionProcessor.SpeechHttpStatus(453, "No User With Email Address");

			// Token: 0x040010A1 RID: 4257
			public static readonly SpeechRecognitionProcessor.SpeechHttpStatus BadRequest = new SpeechRecognitionProcessor.SpeechHttpStatus(400, null);

			// Token: 0x040010A2 RID: 4258
			public static readonly SpeechRecognitionProcessor.SpeechHttpStatus ServiceUnavailable = new SpeechRecognitionProcessor.SpeechHttpStatus(503, null);

			// Token: 0x040010A3 RID: 4259
			public static readonly SpeechRecognitionProcessor.SpeechHttpStatus InternalServerError = new SpeechRecognitionProcessor.SpeechHttpStatus(500, null);
		}

		// Token: 0x0200039A RID: 922
		private class SpeechStreamBuffer : DisposableBase
		{
			// Token: 0x1700069C RID: 1692
			// (get) Token: 0x06001D7E RID: 7550 RVA: 0x00075B25 File Offset: 0x00073D25
			// (set) Token: 0x06001D7F RID: 7551 RVA: 0x00075B2D File Offset: 0x00073D2D
			public byte[] AudioBuffer { get; set; }

			// Token: 0x1700069D RID: 1693
			// (get) Token: 0x06001D80 RID: 7552 RVA: 0x00075B36 File Offset: 0x00073D36
			// (set) Token: 0x06001D81 RID: 7553 RVA: 0x00075B3E File Offset: 0x00073D3E
			public Stream AudioStream { get; set; }

			// Token: 0x06001D82 RID: 7554 RVA: 0x00075B47 File Offset: 0x00073D47
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<SpeechRecognitionProcessor.SpeechStreamBuffer>(this);
			}

			// Token: 0x06001D83 RID: 7555 RVA: 0x00075B4F File Offset: 0x00073D4F
			protected override void InternalDispose(bool disposing)
			{
				if (disposing && this.AudioStream != null)
				{
					this.AudioStream.Close();
					this.AudioStream = null;
				}
			}
		}
	}
}
