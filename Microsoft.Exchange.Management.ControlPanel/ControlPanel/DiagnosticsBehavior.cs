using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Management.DDIService;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001A5 RID: 421
	public class DiagnosticsBehavior : IEndpointBehavior, IErrorHandler, IDispatchMessageInspector
	{
		// Token: 0x06002377 RID: 9079 RVA: 0x0006C978 File Offset: 0x0006AB78
		public static string GetErrorCause(Exception exception)
		{
			string result;
			if (!DiagnosticsBehavior.KnownDDIExceptions.TryGetValue(exception.GetType(), out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0006C9A0 File Offset: 0x0006ABA0
		public static void CheckSystemProbeCookie(HttpContext context)
		{
			HttpCookie httpCookie = context.Request.Cookies["xsysprobeid"];
			if (httpCookie != null)
			{
				Guid guid;
				SystemProbe.ActivityId = (Guid.TryParse(httpCookie.Value, out guid) ? guid : Guid.Empty);
			}
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x0006C9E2 File Offset: 0x0006ABE2
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x0006C9E4 File Offset: 0x0006ABE4
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x0006C9E8 File Offset: 0x0006ABE8
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			EcpEventLogConstants.Tuple_EcpWebServiceStarted.LogEvent(new object[]
			{
				EcpEventLogExtensions.GetUserNameToLog(),
				endpoint.Address.Uri
			});
			endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(this);
			endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
			foreach (DispatchOperation dispatchOperation in endpointDispatcher.DispatchRuntime.Operations)
			{
				if (dispatchOperation.Formatter != null)
				{
					dispatchOperation.Formatter = new DiagnosticsBehavior.SerializationPerformanceTracker(dispatchOperation.Formatter);
				}
			}
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x0006CA98 File Offset: 0x0006AC98
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0006CA9A File Offset: 0x0006AC9A
		bool IErrorHandler.HandleError(Exception error)
		{
			ErrorHandlingUtil.SendReportForCriticalException(HttpContext.Current, error);
			return false;
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x0006CAA8 File Offset: 0x0006ACA8
		void IErrorHandler.ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			string value = DiagnosticsBehavior.GetErrorCause(error) ?? error.GetType().FullName;
			EcpPerfCounters.WebServiceErrors.Increment();
			EcpEventLogConstants.Tuple_WebServiceFailed.LogPeriodicFailure(EcpEventLogExtensions.GetUserNameToLog(), HttpContext.Current.GetRequestUrlForLog(), error, EcpEventLogExtensions.GetFlightInfoForLog());
			ExTraceGlobals.EventLogTracer.TraceError<string, EcpTraceFormatter<Exception>>(0, 0L, "{0}'s webservice request failed with exception: {1}", EcpEventLogExtensions.GetUserNameToLog(), error.GetTraceFormatter());
			HttpContext.Current.Response.AddHeader("X-ECP-ERROR", value);
			DDIHelper.Trace("Webservice request failed with exception: {0}", new object[]
			{
				error.GetTraceFormatter()
			});
			if (fault != null && version == MessageVersion.None)
			{
				MessageProperties properties = fault.Properties;
				fault = Message.CreateMessage(version, string.Empty, new JsonFaultDetail(error), new DataContractJsonSerializer(typeof(JsonFaultDetail)));
				fault.Properties.CopyProperties(properties);
			}
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0006CB85 File Offset: 0x0006AD85
		object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			DiagnosticsBehavior.CheckSystemProbeCookie(HttpContext.Current);
			PerfRecord.Current.WebServiceCallStarted();
			return null;
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x0006CB9C File Offset: 0x0006AD9C
		void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
		{
			PerfRecord.Current.WebServiceCallCompleted();
			if (reply != null)
			{
				HttpResponseMessageProperty httpResponseMessageProperty;
				if (reply.Properties.ContainsKey(HttpResponseMessageProperty.Name))
				{
					httpResponseMessageProperty = (HttpResponseMessageProperty)reply.Properties[HttpResponseMessageProperty.Name];
				}
				else
				{
					httpResponseMessageProperty = new HttpResponseMessageProperty();
					reply.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProperty);
				}
				httpResponseMessageProperty.Headers.Set("Cache-Control", "no-cache, no-store");
			}
			RbacPrincipal.Current.RbacConfiguration.TroubleshootingContext.TraceOperationCompletedAndUpdateContext();
			ActivityContextManager.CleanupActivityContext(HttpContext.Current);
		}

		// Token: 0x04001DEF RID: 7663
		public const string NotAvailableForPartner = "notavailableforpartner";

		// Token: 0x04001DF0 RID: 7664
		public const string ServiceNotExist = "servicenotexist";

		// Token: 0x04001DF1 RID: 7665
		private static readonly Dictionary<Type, string> KnownDDIExceptions = new Dictionary<Type, string>
		{
			{
				typeof(SchemaNotExistException),
				"servicenotexist"
			},
			{
				typeof(WorkflowNotExistException),
				"servicenotexist"
			}
		};

		// Token: 0x020001A6 RID: 422
		private class SerializationPerformanceTracker : IDispatchMessageFormatter
		{
			// Token: 0x06002383 RID: 9091 RVA: 0x0006CC7B File Offset: 0x0006AE7B
			public SerializationPerformanceTracker(IDispatchMessageFormatter formatter)
			{
				this.formatter = formatter;
			}

			// Token: 0x06002384 RID: 9092 RVA: 0x0006CC8C File Offset: 0x0006AE8C
			public void DeserializeRequest(Message message, object[] parameters)
			{
				using (EcpPerformanceData.WcfSerialization.StartRequestTimer())
				{
					IPrincipal currentPrincipal = Thread.CurrentPrincipal;
					try
					{
						Thread.CurrentPrincipal = RbacPrincipal.Current;
						this.formatter.DeserializeRequest(message, parameters);
					}
					finally
					{
						Thread.CurrentPrincipal = currentPrincipal;
					}
				}
			}

			// Token: 0x06002385 RID: 9093 RVA: 0x0006CCF4 File Offset: 0x0006AEF4
			public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
			{
				Message result2;
				using (EcpPerformanceData.WcfSerialization.StartRequestTimer())
				{
					result2 = this.formatter.SerializeReply(messageVersion, parameters, result);
				}
				return result2;
			}

			// Token: 0x04001DF2 RID: 7666
			private IDispatchMessageFormatter formatter;
		}
	}
}
