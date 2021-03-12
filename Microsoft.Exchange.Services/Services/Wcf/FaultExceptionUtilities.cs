using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml.Schema;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B85 RID: 2949
	internal static class FaultExceptionUtilities
	{
		// Token: 0x060055E0 RID: 21984 RVA: 0x00110BE0 File Offset: 0x0010EDE0
		public static bool GetIsTransient(Exception exception)
		{
			bool result = false;
			if (exception != null)
			{
				if (exception is TransientException)
				{
					result = true;
				}
				else if (exception is OverBudgetException)
				{
					result = true;
				}
				else
				{
					object obj = exception.Data["IsTransient"];
					if (obj is bool)
					{
						result = (bool)obj;
					}
				}
			}
			return result;
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x00110C2C File Offset: 0x0010EE2C
		internal static FaultException CreateFault(LocalizedException exception, FaultParty faultParty)
		{
			EwsMessageFaultDetail messageFault = new EwsMessageFaultDetail(exception, faultParty, ExchangeVersion.UnsafeGetCurrent());
			return FaultExceptionUtilities.CreateFaultException(messageFault, exception);
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x00110C50 File Offset: 0x0010EE50
		internal static FaultException CreateFault(LocalizedException exception, FaultParty faultParty, ExchangeVersion currentExchangeVersion)
		{
			EwsMessageFaultDetail messageFault = new EwsMessageFaultDetail(exception, faultParty, currentExchangeVersion);
			return FaultExceptionUtilities.CreateFaultException(messageFault, exception);
		}

		// Token: 0x060055E3 RID: 21987 RVA: 0x00110C70 File Offset: 0x0010EE70
		internal static FaultException CreateAvailabilityFault(LocalizedException exception, FaultParty faultParty)
		{
			AvailabilityMessageFaultDetail messageFault = new AvailabilityMessageFaultDetail(exception, faultParty);
			return FaultExceptionUtilities.CreateFaultException(messageFault, exception);
		}

		// Token: 0x060055E4 RID: 21988 RVA: 0x00110C8C File Offset: 0x0010EE8C
		internal static FaultException CreateUMFault(LocalizedException exception, FaultParty faultParty)
		{
			UmMessageFaultDetail messageFault = new UmMessageFaultDetail(exception, faultParty);
			return FaultExceptionUtilities.CreateFaultException(messageFault, exception);
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x00110CA8 File Offset: 0x0010EEA8
		private static FaultException CreateFaultException(MessageFault messageFault, Exception exception)
		{
			FaultException ex = new FaultException(messageFault, string.Empty);
			ex.Source = EWSSettings.SimpleAssemblyName;
			ex.Data["IsTransient"] = FaultExceptionUtilities.GetIsTransient(exception);
			if (exception != null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(RequestDetailsLogger.Current, "FaultInnerException", exception.ToString());
			}
			if (ex.Code != null && ex.Code.SubCode != null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, ServiceCommonMetadata.ErrorCode, ex.Code.SubCode.Name);
			}
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, ServiceCommonMetadata.HttpStatus, "500");
			return ex;
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x00110D68 File Offset: 0x0010EF68
		internal static FaultException DealWithSchemaViolation(SchemaValidationException exception, Message request)
		{
			ExchangeVersion exchangeVersion = (ExchangeVersion)request.Properties["WS_ServerVersionKey"];
			if (exchangeVersion.Equals(ExchangeVersion.Latest))
			{
				return FaultExceptionUtilities.CreateFault(exception, FaultParty.Sender);
			}
			Stream stream = (Stream)request.Properties["MessageStream"];
			string methodName = (string)request.Properties["MethodName"];
			FaultException result;
			try
			{
				try
				{
					stream.Position = 0L;
				}
				catch (InvalidOperationException)
				{
					return FaultExceptionUtilities.CreateFault(exception, FaultParty.Sender);
				}
				catch (ArgumentOutOfRangeException)
				{
					return FaultExceptionUtilities.CreateFault(exception, FaultParty.Sender);
				}
				SchemaValidator schemaValidator = new SchemaValidator(delegate(XmlSchemaException xmlException, SoapSavvyReader.SoapSection soapSection)
				{
					throw new SchemaValidationException(xmlException, xmlException.LineNumber, xmlException.LinePosition, xmlException.Message);
				});
				try
				{
					schemaValidator.ValidateMessage(stream, ExchangeVersion.Latest, MessageInspectorManager.IsEWSRequest(methodName), true);
				}
				catch (SchemaValidationException)
				{
					return FaultExceptionUtilities.CreateFault(exception, FaultParty.Sender);
				}
				result = FaultExceptionUtilities.CreateFault(new IncorrectSchemaVersionException(), FaultParty.Sender);
			}
			finally
			{
				BufferedRegionStream bufferedRegionStream = stream as BufferedRegionStream;
				if (bufferedRegionStream != null)
				{
					bufferedRegionStream.Dispose();
					request.Properties["MessageStream"] = null;
				}
			}
			return result;
		}

		// Token: 0x04002ED9 RID: 11993
		private const string IsTransientKey = "IsTransient";
	}
}
