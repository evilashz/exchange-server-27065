using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000450 RID: 1104
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OwaServerLogger : ExtensibleLogger
	{
		// Token: 0x0600253F RID: 9535 RVA: 0x00086657 File Offset: 0x00084857
		public OwaServerLogger() : base(new OwaServerLogConfiguration())
		{
			ActivityContext.RegisterMetadata(typeof(OwaServerLogger.LoggerData));
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x00086673 File Offset: 0x00084873
		public static void Initialize()
		{
			if (OwaServerLogger.instance == null)
			{
				OwaServerLogger.instance = new OwaServerLogger();
			}
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x00086686 File Offset: 0x00084886
		public static void AppendToLog(ILogEvent logEvent)
		{
			if (OwaServerLogger.instance != null)
			{
				OwaServerLogger.instance.LogEvent(logEvent);
			}
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x0008669C File Offset: 0x0008489C
		internal static void LogUserContextData(HttpContext httpContext, RequestDetailsLogger logger)
		{
			if (OwaServerLogger.instance == null || !OwaServerLogger.instance.Configuration.IsLoggingEnabled)
			{
				return;
			}
			IMailboxContext mailboxContext = UserContextManager.GetMailboxContext(httpContext, null, false);
			if (mailboxContext != null && mailboxContext.ExchangePrincipal != null)
			{
				if (mailboxContext.IsExplicitLogon && mailboxContext.LogonIdentity != null)
				{
					logger.Set(OwaServerLogger.LoggerData.User, mailboxContext.LogonIdentity.PrimarySmtpAddress);
				}
				logger.Set(OwaServerLogger.LoggerData.PrimarySmtpAddress, mailboxContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress);
				logger.Set(OwaServerLogger.LoggerData.MailboxGuid, mailboxContext.ExchangePrincipal.MailboxInfo.MailboxGuid);
				logger.Set(OwaServerLogger.LoggerData.RecipientType, mailboxContext.ExchangePrincipal.RecipientTypeDetails);
				Guid tenantGuid = mailboxContext.ExchangePrincipal.MailboxInfo.OrganizationId.GetTenantGuid();
				if (tenantGuid != Guid.Empty)
				{
					logger.Set(OwaServerLogger.LoggerData.TenantGuid, tenantGuid);
				}
				string text = mailboxContext.Key.UserContextId.ToString(CultureInfo.InvariantCulture);
				logger.Set(OwaServerLogger.LoggerData.UserContext, text);
				if (!OwaServerLogger.TryAppendToIISLog(httpContext.Response, "&{0}={1}", new object[]
				{
					UserContextCookie.UserContextCookiePrefix,
					text
				}))
				{
					ExTraceGlobals.RequestTracer.TraceWarning<Guid, string>((long)httpContext.GetHashCode(), "RequestId: {0}; Error appending UserContext '{1}' to IIS log.", logger.ActivityScope.ActivityId, text);
				}
				UserContext userContext = mailboxContext as UserContext;
				if (userContext != null && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.LogTenantInfo.Enabled)
				{
					if (!string.IsNullOrEmpty(userContext.LogEventCommonData.TenantDomain))
					{
						OwaServerLogger.TryAppendToIISLog(httpContext.Response, "&{0}={1}", new object[]
						{
							"domain",
							userContext.LogEventCommonData.TenantDomain
						});
						logger.Set(OwaServerLogger.LoggerData.Tenant, userContext.LogEventCommonData.TenantDomain);
					}
					if (userContext.IsBposUser && !string.IsNullOrEmpty(userContext.BposSkuCapability))
					{
						OwaServerLogger.TryAppendToIISLog(httpContext.Response, "&{0}={1}", new object[]
						{
							"bpossku",
							userContext.BposSkuCapability
						});
						logger.Set(OwaServerLogger.LoggerData.ServicePlan, userContext.BposSkuCapability);
					}
					if (!string.IsNullOrEmpty(userContext.LogEventCommonData.Flights))
					{
						logger.Set(OwaServerLogger.LoggerData.Flights, userContext.LogEventCommonData.Flights);
					}
					if (!string.IsNullOrEmpty(userContext.LogEventCommonData.Features))
					{
						logger.Set(OwaServerLogger.LoggerData.Features, userContext.LogEventCommonData.Features);
					}
					if (userContext.UserCulture != null && !string.IsNullOrEmpty(userContext.UserCulture.Name))
					{
						logger.Set(OwaServerLogger.LoggerData.UserContextCulture, userContext.UserCulture.Name);
					}
				}
			}
			UserContextStatistics userContextStatistics = UserContextManager.GetUserContextStatistics(httpContext);
			if (userContextStatistics != null)
			{
				logger.Set(OwaServerLogger.LoggerData.UserContextLatency, userContextStatistics.AcquireLatency);
				logger.Set(OwaServerLogger.LoggerData.UserContextCreated, userContextStatistics.Created ? 1 : 0);
			}
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000869C8 File Offset: 0x00084BC8
		internal static void LogHttpContextData(HttpContext httpContext, RequestDetailsLogger logger)
		{
			if (OwaServerLogger.instance == null || !OwaServerLogger.instance.Configuration.IsLoggingEnabled)
			{
				return;
			}
			if (logger.Get(ExtensibleLoggerMetadata.EventId) == null)
			{
				if (!logger.ActivityScope.Statistics.Any<KeyValuePair<OperationKey, OperationStatistics>>())
				{
					return;
				}
				OwaServerLogger.SetEventId(httpContext, logger);
			}
			logger.Set(OwaServerLogger.LoggerData.ContentLength, httpContext.Request.ContentLength);
			logger.Set(ServiceLatencyMetadata.HttpPipelineLatency, httpContext.Items[ServiceLatencyMetadata.HttpPipelineLatency]);
			NameValueCollection headers = httpContext.Request.Headers;
			string value = headers["X-OWA-ActionId"];
			if (!string.IsNullOrEmpty(value))
			{
				logger.Set(OwaServerLogger.LoggerData.ClientActionId, value);
			}
			string value2 = headers["X-OWA-ActionName"];
			if (!string.IsNullOrEmpty(value2))
			{
				logger.Set(OwaServerLogger.LoggerData.ClientActionName, value2);
			}
			string value3 = headers["X-EXT-ClientName"];
			if (!string.IsNullOrEmpty(value3))
			{
				logger.Set(OwaServerLogger.LoggerData.ExternalClientName, value3);
			}
			string value4 = headers["X-EXT-CorrelationId"];
			if (!string.IsNullOrEmpty(value4))
			{
				logger.Set(OwaServerLogger.LoggerData.ExternalCorrelationId, value4);
			}
			string sourceCafeServer = CafeHelper.GetSourceCafeServer(httpContext.Request);
			if (!string.IsNullOrEmpty(sourceCafeServer))
			{
				logger.Set(OwaServerLogger.LoggerData.FrontEndServer, sourceCafeServer);
			}
			string value5 = headers["X-OWA-OfflineRejectCode"];
			if (!string.IsNullOrEmpty(value5))
			{
				logger.Set(OwaServerLogger.LoggerData.OfflineRejectCode, value5);
			}
			string text = headers["logonLatency"];
			bool flag = UserContextUtilities.IsDifferentMailbox(httpContext);
			if (!string.IsNullOrEmpty(text) || flag)
			{
				IMailboxContext mailboxContext = UserContextManager.GetMailboxContext(httpContext, null, false);
				if (!string.IsNullOrEmpty(text))
				{
					logger.Set(OwaServerLogger.LoggerData.LogonLatencyName, text);
					string userContext = string.Empty;
					if (mailboxContext != null)
					{
						userContext = mailboxContext.Key.UserContextId.ToString(CultureInfo.InvariantCulture);
					}
					string[] keys = new string[]
					{
						"LGN.L"
					};
					string[] values = new string[]
					{
						text
					};
					Datapoint datapoint = new Datapoint(DatapointConsumer.Analytics, "LogonLatency", DateTime.UtcNow.ToString("o"), keys, values);
					ClientLogEvent logEvent = new ClientLogEvent(datapoint, userContext);
					OwaClientLogger.AppendToLog(logEvent);
				}
				if (flag && mailboxContext is SharedContext && httpContext.Items.Contains("CallContext"))
				{
					CallContext callContext = httpContext.Items["CallContext"] as CallContext;
					logger.Set(OwaServerLogger.LoggerData.User, callContext.GetEffectiveAccessingSmtpAddress());
				}
			}
			string value6 = httpContext.Items["BackEndAuthenticator"] as string;
			if (!string.IsNullOrEmpty(value6))
			{
				logger.Set(OwaServerLogger.LoggerData.BackendAuthenticator, value6);
			}
			object obj = httpContext.Items["TotalBERehydrationModuleLatency"];
			if (obj != null)
			{
				logger.Set(OwaServerLogger.LoggerData.RehydrationModuleLatency, obj);
			}
			string value7 = headers["X-OWA-Test-PassThruProxy"];
			if (!string.IsNullOrEmpty(value7))
			{
				logger.Set(OwaServerLogger.LoggerData.PassThroughProxy, value7);
			}
			string value8 = headers["X-SuiteServiceProxyOrigin"];
			if (!string.IsNullOrEmpty(value8))
			{
				logger.Set(OwaServerLogger.LoggerData.SuiteServiceProxyOrigin, value8);
			}
			HttpCookie httpCookie = httpContext.Request.Cookies["ClientId"];
			if (httpCookie != null)
			{
				logger.Set(OwaServerLogger.LoggerData.ClientId, httpCookie.Value);
			}
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x00086D20 File Offset: 0x00084F20
		protected override ICollection<KeyValuePair<string, object>> GetComponentSpecificData(IActivityScope activityScope, string eventId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(20);
			IEnumerable<KeyValuePair<string, object>> formattableMetadata = activityScope.GetFormattableMetadata(OwsLogRegistry.GetRegisteredValues(eventId));
			foreach (KeyValuePair<string, object> keyValuePair in formattableMetadata)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			ExtensibleLogger.CopyPIIProperty(activityScope, dictionary, OwaServerLogger.LoggerData.PrimarySmtpAddress, "PSA");
			ExtensibleLogger.CopyPIIProperty(activityScope, dictionary, OwaServerLogger.LoggerData.User, "user");
			ExtensibleLogger.CopyProperties(activityScope, dictionary, OwaServerLogger.EnumToShortKeyMapping);
			if (Globals.LogErrorDetails)
			{
				ExtensibleLogger.CopyProperty(activityScope, dictionary, ServiceCommonMetadata.GenericErrors, "ErrInfo");
				string property = activityScope.GetProperty(ServiceCommonMetadata.ErrorCode);
				if ((!string.IsNullOrEmpty(activityScope.GetProperty(ServiceCommonMetadata.GenericErrors)) || (!string.IsNullOrEmpty(property) && property != "Success" && property != "0")) && HttpContext.Current != null)
				{
					string key = OwaServerLogger.EnumToShortKeyMapping[OwaServerLogger.LoggerData.UserAgent];
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, HttpContext.Current.Request.UserAgent);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x00086E60 File Offset: 0x00085060
		internal static void LogWcfLatency(HttpContext httpContext)
		{
			RequestDetailsLogger getRequestDetailsLogger = OwaApplication.GetRequestDetailsLogger;
			int num = (int)httpContext.Items[ServiceLatencyMetadata.HttpPipelineLatency];
			getRequestDetailsLogger.Set(OwaServerLogger.LoggerData.WcfLatency, (int)getRequestDetailsLogger.ActivityScope.TotalMilliseconds - num);
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x00086EAB File Offset: 0x000850AB
		protected override bool IsInterestingEvent(IActivityScope activityScope, ActivityEventType eventType)
		{
			return base.IsInterestingEvent(activityScope, eventType) && (activityScope.GetProperty(ExtensibleLoggerMetadata.EventId) != null || activityScope.Statistics.Any<KeyValuePair<OperationKey, OperationStatistics>>());
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x00086ED4 File Offset: 0x000850D4
		private static void SetEventId(HttpContext httpContext, RequestDetailsLogger logger)
		{
			RequestContext requestContext = RequestContext.Get(httpContext);
			string value;
			if (requestContext == null)
			{
				value = "OwaRequest";
			}
			else if (requestContext.RequestType == OwaRequestType.ServiceRequest)
			{
				value = (httpContext.Request.Headers[OWADispatchOperationSelector.Action] ?? requestContext.RequestType.ToString());
			}
			else
			{
				value = requestContext.RequestType.ToString();
			}
			logger.Set(ExtensibleLoggerMetadata.EventId, value);
			logger.Set(OwaServerLogger.LoggerData.ClientActionName, httpContext.Request.CurrentExecutionFilePath.ToLower());
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x00086F68 File Offset: 0x00085168
		private static bool TryAppendToIISLog(HttpResponse response, string format, params object[] args)
		{
			string param = string.Format(format, args);
			try
			{
				response.AppendToLog(param);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x04001518 RID: 5400
		internal const string PrimarySmtpAddressKey = "PSA";

		// Token: 0x04001519 RID: 5401
		private const string UserKey = "user";

		// Token: 0x0400151A RID: 5402
		private const string OfflineRejectCodeKey = "X-OWA-OfflineRejectCode";

		// Token: 0x0400151B RID: 5403
		private const string ExternalClientNameKey = "X-EXT-ClientName";

		// Token: 0x0400151C RID: 5404
		private const string ExternalCorrelationIdKey = "X-EXT-CorrelationId";

		// Token: 0x0400151D RID: 5405
		private const string LogonLatencyHeaderName = "logonLatency";

		// Token: 0x0400151E RID: 5406
		private const string BposSku = "bpossku";

		// Token: 0x0400151F RID: 5407
		private const string DomainName = "domain";

		// Token: 0x04001520 RID: 5408
		private const string ErrorCodeSuccess = "Success";

		// Token: 0x04001521 RID: 5409
		private const string ErrorCodeSuccessAsZero = "0";

		// Token: 0x04001522 RID: 5410
		public static readonly Dictionary<Enum, string> EnumToShortKeyMapping = new Dictionary<Enum, string>
		{
			{
				OwaServerLogger.LoggerData.MailboxGuid,
				"MG"
			},
			{
				OwaServerLogger.LoggerData.TenantGuid,
				"TG"
			},
			{
				OwaServerLogger.LoggerData.UserContext,
				UserContextCookie.UserContextCookiePrefix
			},
			{
				OwaServerLogger.LoggerData.CanaryStatus,
				"CN.S"
			},
			{
				OwaServerLogger.LoggerData.CanaryValidationBegin,
				"CN.B"
			},
			{
				OwaServerLogger.LoggerData.CanaryValidationEnd,
				"CN.E"
			},
			{
				OwaServerLogger.LoggerData.CanaryCreationTime,
				"CN.T"
			},
			{
				OwaServerLogger.LoggerData.CanaryLogData,
				"CN.L"
			},
			{
				OwaServerLogger.LoggerData.ClientActionId,
				"ActionId"
			},
			{
				OwaServerLogger.LoggerData.ClientActionName,
				"CAN"
			},
			{
				OwaServerLogger.LoggerData.BackendAuthenticator,
				"BEA"
			},
			{
				OwaServerLogger.LoggerData.GetOWAMiniRecipientBegin,
				"GOM.B"
			},
			{
				OwaServerLogger.LoggerData.GetOWAMiniRecipientEnd,
				"GOM.E"
			},
			{
				OwaServerLogger.LoggerData.RehydrationModuleLatency,
				"RHML"
			},
			{
				OwaServerLogger.LoggerData.OnPostAuthorizeRequestBegin,
				"PST.B"
			},
			{
				OwaServerLogger.LoggerData.OnPostAuthorizeRequestEnd,
				"PST.E"
			},
			{
				OwaServerLogger.LoggerData.OnPostAuthorizeRequestLatency,
				"PST.L"
			},
			{
				OwaServerLogger.LoggerData.OnPostAuthorizeRequestLatencyDetails,
				"PST.D"
			},
			{
				OwaServerLogger.LoggerData.OwaMessageInspectorReceiveRequestBegin,
				"OMB.B"
			},
			{
				OwaServerLogger.LoggerData.OwaMessageInspectorReceiveRequestEnd,
				"OMB.E"
			},
			{
				OwaServerLogger.LoggerData.OwaMessageInspectorEndRequestBegin,
				"OME.B"
			},
			{
				OwaServerLogger.LoggerData.OwaMessageInspectorEndRequestEnd,
				"OME.E"
			},
			{
				OwaServerLogger.LoggerData.OwaRequestPipelineLatency,
				"ORP.L"
			},
			{
				OwaServerLogger.LoggerData.UserContextLoadBegin,
				"UCL.B"
			},
			{
				OwaServerLogger.LoggerData.UserContextLoadEnd,
				"UCL.E"
			},
			{
				OwaServerLogger.LoggerData.UserContextLatency,
				"UC.L"
			},
			{
				OwaServerLogger.LoggerData.UserContextCreated,
				"UC.C"
			},
			{
				OwaServerLogger.LoggerData.UserContextCulture,
				"UC.CUL"
			},
			{
				OwaServerLogger.LoggerData.IsMowaClient,
				"Mowa"
			},
			{
				OwaServerLogger.LoggerData.IsOfflineEnabled,
				"Off"
			},
			{
				OwaServerLogger.LoggerData.ContentLength,
				"CNTL"
			},
			{
				OwaServerLogger.LoggerData.CorrelationId,
				"CorrelationID"
			},
			{
				OwaServerLogger.LoggerData.FrontEndServer,
				"FE"
			},
			{
				OwaServerLogger.LoggerData.WcfLatency,
				"WCF"
			},
			{
				OwaServerLogger.LoggerData.OfflineRejectCode,
				"ORC"
			},
			{
				OwaServerLogger.LoggerData.Tenant,
				"DOM"
			},
			{
				OwaServerLogger.LoggerData.AppCache,
				"AC"
			},
			{
				OwaServerLogger.LoggerData.ServicePlan,
				"SKU"
			},
			{
				OwaServerLogger.LoggerData.LogonLatencyName,
				"LGN.L"
			},
			{
				ServiceCommonMetadata.ErrorCode,
				"Err"
			},
			{
				ServiceCommonMetadata.IsDuplicatedAction,
				"Dup"
			},
			{
				BudgetMetadata.BeginBudgetConnections,
				"BMD.BBC"
			},
			{
				BudgetMetadata.EndBudgetConnections,
				"BMD.EBC"
			},
			{
				BudgetMetadata.BeginBudgetHangingConnections,
				"BMD.BBHC"
			},
			{
				BudgetMetadata.EndBudgetHangingConnections,
				"BMD.EBHC"
			},
			{
				BudgetMetadata.BeginBudgetAD,
				"BMD.BBAD"
			},
			{
				BudgetMetadata.EndBudgetAD,
				"BMD.EBAD"
			},
			{
				BudgetMetadata.BeginBudgetCAS,
				"BMD.BBCAS"
			},
			{
				BudgetMetadata.EndBudgetCAS,
				"BMD.EBCAS"
			},
			{
				BudgetMetadata.BeginBudgetRPC,
				"BMD.BBRPC"
			},
			{
				BudgetMetadata.EndBudgetRPC,
				"BMD.EBRPC"
			},
			{
				BudgetMetadata.BeginBudgetFindCount,
				"BMD.BBFC"
			},
			{
				BudgetMetadata.EndBudgetFindCount,
				"BMD.EBFC"
			},
			{
				BudgetMetadata.BeginBudgetSubscriptions,
				"BMD.BBS"
			},
			{
				BudgetMetadata.EndBudgetSubscriptions,
				"BMD.EBS"
			},
			{
				BudgetMetadata.ThrottlingDelay,
				"Thr"
			},
			{
				BudgetMetadata.ThrottlingRequestType,
				"BMD.TRT"
			},
			{
				ServiceTaskMetadata.ADCount,
				"CmdAD.C"
			},
			{
				ServiceTaskMetadata.ADLatency,
				"CmdAD.L"
			},
			{
				ServiceTaskMetadata.RpcCount,
				"CmdRPC.C"
			},
			{
				ServiceTaskMetadata.RpcLatency,
				"CmdRPC.L"
			},
			{
				ServiceTaskMetadata.WatsonReportCount,
				"CmdWR.C"
			},
			{
				ServiceTaskMetadata.ServiceCommandBegin,
				"SCmd.B"
			},
			{
				ServiceTaskMetadata.ServiceCommandEnd,
				"SCmd.E"
			},
			{
				ServiceLatencyMetadata.CoreExecutionLatency,
				"CmdT"
			},
			{
				ServiceLatencyMetadata.RecipientLookupLatency,
				"RLL"
			},
			{
				ServiceLatencyMetadata.ExchangePrincipalLatency,
				"EPL"
			},
			{
				ServiceLatencyMetadata.HttpPipelineLatency,
				"HPL"
			},
			{
				OwaServerLogger.LoggerData.CallContextInitBegin,
				"CC.B"
			},
			{
				OwaServerLogger.LoggerData.CallContextInitEnd,
				"CC.E"
			},
			{
				ServiceLatencyMetadata.CallContextInitLatency,
				"CC.L"
			},
			{
				ServiceLatencyMetadata.PreExecutionLatency,
				"PreL"
			},
			{
				OwaServerLogger.LoggerData.Flights,
				"FLT"
			},
			{
				OwaServerLogger.LoggerData.Features,
				"FTR"
			},
			{
				OwaServerLogger.LoggerData.ExternalClientName,
				"ECN"
			},
			{
				OwaServerLogger.LoggerData.ExternalCorrelationId,
				"ECI"
			},
			{
				OwaServerLogger.LoggerData.PassThroughProxy,
				"PTP"
			},
			{
				OwaServerLogger.LoggerData.IsRequest,
				"Rqt"
			},
			{
				OwaServerLogger.LoggerData.SuiteServiceProxyOrigin,
				"SSPOrigin"
			},
			{
				OwaServerLogger.LoggerData.RequestStartTime,
				"RQST"
			},
			{
				OwaServerLogger.LoggerData.RequestEndTime,
				"RQET"
			},
			{
				OwaServerLogger.LoggerData.UserAgent,
				"UA"
			},
			{
				OwaServerLogger.LoggerData.ClientBuildVersion,
				"cbld"
			},
			{
				OwaServerLogger.LoggerData.RequestVersion,
				"ReqVer"
			},
			{
				OwaServerLogger.LoggerData.RecipientType,
				"RecT"
			},
			{
				OwaServerLogger.LoggerData.UsingWcfHttpHandler,
				"WCFH"
			},
			{
				OwaServerLogger.LoggerData.ClientId,
				"ClientId"
			}
		};

		// Token: 0x04001523 RID: 5411
		private static OwaServerLogger instance;

		// Token: 0x02000451 RID: 1105
		internal enum LoggerData
		{
			// Token: 0x04001525 RID: 5413
			UserContext,
			// Token: 0x04001526 RID: 5414
			PrimarySmtpAddress,
			// Token: 0x04001527 RID: 5415
			Tenant,
			// Token: 0x04001528 RID: 5416
			ServicePlan,
			// Token: 0x04001529 RID: 5417
			LogonLatencyName,
			// Token: 0x0400152A RID: 5418
			MailboxGuid,
			// Token: 0x0400152B RID: 5419
			TenantGuid,
			// Token: 0x0400152C RID: 5420
			CallContextInitBegin,
			// Token: 0x0400152D RID: 5421
			CallContextInitEnd,
			// Token: 0x0400152E RID: 5422
			CanaryStatus,
			// Token: 0x0400152F RID: 5423
			CanaryCreationTime,
			// Token: 0x04001530 RID: 5424
			CanaryValidationBegin,
			// Token: 0x04001531 RID: 5425
			CanaryValidationEnd,
			// Token: 0x04001532 RID: 5426
			CanaryLogData,
			// Token: 0x04001533 RID: 5427
			ClientActionId,
			// Token: 0x04001534 RID: 5428
			ClientActionName,
			// Token: 0x04001535 RID: 5429
			AppCache,
			// Token: 0x04001536 RID: 5430
			BackendAuthenticator,
			// Token: 0x04001537 RID: 5431
			RehydrationModuleLatency,
			// Token: 0x04001538 RID: 5432
			IsMowaClient,
			// Token: 0x04001539 RID: 5433
			IsOfflineEnabled,
			// Token: 0x0400153A RID: 5434
			ContentLength,
			// Token: 0x0400153B RID: 5435
			GetOWAMiniRecipientBegin,
			// Token: 0x0400153C RID: 5436
			GetOWAMiniRecipientEnd,
			// Token: 0x0400153D RID: 5437
			CorrelationId,
			// Token: 0x0400153E RID: 5438
			FrontEndServer,
			// Token: 0x0400153F RID: 5439
			OwaMessageInspectorReceiveRequestBegin,
			// Token: 0x04001540 RID: 5440
			OwaMessageInspectorReceiveRequestEnd,
			// Token: 0x04001541 RID: 5441
			OwaMessageInspectorEndRequestBegin,
			// Token: 0x04001542 RID: 5442
			OwaMessageInspectorEndRequestEnd,
			// Token: 0x04001543 RID: 5443
			OnPostAuthorizeRequestBegin,
			// Token: 0x04001544 RID: 5444
			OnPostAuthorizeRequestEnd,
			// Token: 0x04001545 RID: 5445
			OnPostAuthorizeRequestLatency,
			// Token: 0x04001546 RID: 5446
			OnPostAuthorizeRequestLatencyDetails,
			// Token: 0x04001547 RID: 5447
			OwaRequestPipelineLatency,
			// Token: 0x04001548 RID: 5448
			UserContextLoadBegin,
			// Token: 0x04001549 RID: 5449
			UserContextLoadEnd,
			// Token: 0x0400154A RID: 5450
			UserContextLatency,
			// Token: 0x0400154B RID: 5451
			UserContextCreated,
			// Token: 0x0400154C RID: 5452
			WcfLatency,
			// Token: 0x0400154D RID: 5453
			OfflineRejectCode,
			// Token: 0x0400154E RID: 5454
			Flights,
			// Token: 0x0400154F RID: 5455
			Features,
			// Token: 0x04001550 RID: 5456
			ExternalClientName,
			// Token: 0x04001551 RID: 5457
			ExternalCorrelationId,
			// Token: 0x04001552 RID: 5458
			MailboxSessionDuration,
			// Token: 0x04001553 RID: 5459
			MailboxSessionLockTime,
			// Token: 0x04001554 RID: 5460
			UserContextDipose1,
			// Token: 0x04001555 RID: 5461
			UserContextDipose2,
			// Token: 0x04001556 RID: 5462
			UserContextDipose3,
			// Token: 0x04001557 RID: 5463
			UserContextDipose4,
			// Token: 0x04001558 RID: 5464
			PassThroughProxy,
			// Token: 0x04001559 RID: 5465
			IsRequest,
			// Token: 0x0400155A RID: 5466
			SuiteServiceProxyOrigin,
			// Token: 0x0400155B RID: 5467
			RequestStartTime,
			// Token: 0x0400155C RID: 5468
			RequestEndTime,
			// Token: 0x0400155D RID: 5469
			UserAgent,
			// Token: 0x0400155E RID: 5470
			ClientBuildVersion,
			// Token: 0x0400155F RID: 5471
			RequestVersion,
			// Token: 0x04001560 RID: 5472
			RecipientType,
			// Token: 0x04001561 RID: 5473
			UsingWcfHttpHandler,
			// Token: 0x04001562 RID: 5474
			User,
			// Token: 0x04001563 RID: 5475
			ClientId,
			// Token: 0x04001564 RID: 5476
			UserContextCulture
		}
	}
}
