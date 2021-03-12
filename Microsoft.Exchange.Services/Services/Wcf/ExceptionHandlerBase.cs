using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.DispatchPipe.Ews;
using Microsoft.Exchange.Services.EventLogs;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DB5 RID: 3509
	internal abstract class ExceptionHandlerBase : IErrorHandler
	{
		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06005949 RID: 22857 RVA: 0x00116FEF File Offset: 0x001151EF
		// (set) Token: 0x0600594A RID: 22858 RVA: 0x00116FF6 File Offset: 0x001151F6
		public static FaultCode InternalServerErrorFaultCode { get; protected set; }

		// Token: 0x0600594B RID: 22859 RVA: 0x00116FFE File Offset: 0x001151FE
		internal static FaultException HandleInternalServerError(object responsibleObject, Exception exception)
		{
			ExceptionHandlerBase.ReportException(responsibleObject, exception);
			return ExceptionHandlerBase.CreateInternalServerErrorException(exception);
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x00117010 File Offset: 0x00115210
		internal static void ReportException(object responsibleObject, Exception exception)
		{
			if (EwsOperationContextBase.Current != null && EwsOperationContextBase.Current.RequestMessage != null)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<Message, string, string>(0L, "[ExceptionHandlerInspector::HandleInternalServerError] Request that caused exception: {0}.  Exception Class: {1}, Exception Message: {2}", EwsOperationContextBase.Current.RequestMessage, exception.GetType().FullName, exception.Message);
			}
			bool flag = true;
			if (exception is FaultException)
			{
				FaultException ex = exception as FaultException;
				if (ex.Code.IsSenderFault)
				{
					flag = false;
				}
			}
			if (flag)
			{
				ServiceDiagnostics.ReportException(exception, ServicesEventLogConstants.Tuple_InternalServerError, responsibleObject, "Encountered unhandled exception: {0}");
			}
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x00117090 File Offset: 0x00115290
		internal static FaultException CreateInternalServerErrorException(Exception exception)
		{
			return FaultExceptionUtilities.CreateFault(new InternalServerErrorException(exception), FaultParty.Receiver);
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x0011709E File Offset: 0x0011529E
		public bool HandleError(Exception error)
		{
			ExceptionHandlerBase.ReportException(this, error);
			return true;
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x001170A8 File Offset: 0x001152A8
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			Message message = fault;
			if (this.TryProvideCommunicationExceptionFault(error, version, ref fault))
			{
				ExWatson.SetWatsonReportAlreadySent(error);
				return;
			}
			FaultException ex = error as FaultException;
			if (ex == null)
			{
				ex = ExceptionHandlerBase.HandleInternalServerError(this, error);
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(RequestDetailsLogger.Current, error, "ExceptionHandlerBase_ProvideFault_Error");
			}
			else if (!EWSSettings.IsFromEwsAssemblies(ex.Source))
			{
				ExceptionHandlerBase.ReportException(this, error);
			}
			else
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(RequestDetailsLogger.Current, ex, "ExceptionHandlerBase_ProvideFault_FaultException");
			}
			if (fault == null)
			{
				MessageFault fault2 = ex.CreateMessageFault();
				fault = Message.CreateMessage(version, fault2, "*");
			}
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x00117130 File Offset: 0x00115330
		private bool TryProvideCommunicationExceptionFault(Exception error, MessageVersion version, ref Message fault)
		{
			bool result = false;
			CommunicationException ex = error as CommunicationException;
			if (ex != null)
			{
				if (ex.GetType().FullName.Contains("MustUnderstandSoapException") && fault != null)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)this.GetHashCode(), "[ExceptionHandlerInspector::ProvideFault] Request failed due to the presence of soap:mustUnderstand on a header that EWS did not understand.");
					result = true;
				}
				else if (ex.InnerException != null)
				{
					LocalizedException ex2 = null;
					if (ex.InnerException is InvalidOperationException && ex.InnerException.InnerException != null && (ex.InnerException.InnerException is XmlException || ex.InnerException.InnerException is InvalidParameterException))
					{
						ex2 = new InvalidRequestException(ex);
					}
					else if (ex.InnerException is QuotaExceededException)
					{
						ex2 = new InvalidRequestException(CoreResources.IDs.ErrorInvalidRequestQuotaExceeded, ex);
					}
					if (ex2 != null)
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<CommunicationException>((long)this.GetHashCode(), "[ExceptionHandlerInspector::ProvideFault] encountered a communication exception. Likely was unable to deserialize the request due to the following error: {0}", ex);
						FaultException ex3 = FaultExceptionUtilities.CreateFault(ex2, FaultParty.Sender);
						MessageFault fault2 = ex3.CreateMessageFault();
						fault = Message.CreateMessage(version, fault2, "*");
						result = true;
					}
				}
			}
			return result;
		}
	}
}
