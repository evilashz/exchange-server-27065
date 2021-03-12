using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200006D RID: 109
	internal class CallRejectedException : LocalizedException
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000172C4 File Offset: 0x000154C4
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x000172CC File Offset: 0x000154CC
		internal PlatformSignalingHeader DiagnosticHeader { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000172D5 File Offset: 0x000154D5
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x000172DD File Offset: 0x000154DD
		internal CallEndingReason Reason { get; private set; }

		// Token: 0x060004BB RID: 1211 RVA: 0x000172E6 File Offset: 0x000154E6
		private CallRejectedException(LocalizedString exceptionString, Exception innerException) : base(exceptionString, innerException)
		{
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000172F0 File Offset: 0x000154F0
		internal static CallRejectedException Create(LocalizedString exceptionString, CallEndingReason endingReason, string extraInfoFormatString, params object[] args)
		{
			return CallRejectedException.Create(exceptionString, null, endingReason, extraInfoFormatString, args);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000172FC File Offset: 0x000154FC
		internal static CallRejectedException Create(LocalizedString exceptionString, Exception innerException, CallEndingReason endingReason, string extraInfoFormatString, params object[] args)
		{
			return new CallRejectedException(exceptionString, innerException)
			{
				Reason = endingReason,
				DiagnosticHeader = CallRejectedException.RenderDiagnosticHeader(endingReason, extraInfoFormatString, args)
			};
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00017328 File Offset: 0x00015528
		internal static PlatformSignalingHeader RenderDiagnosticHeader(CallEndingReason endingReason, string extraInfoFormatString, params object[] args)
		{
			int errorCode = endingReason.ErrorCode;
			string reason = endingReason.Reason;
			bool useDataCenterCallRouting = CommonConstants.UseDataCenterCallRouting;
			string text = string.Empty;
			try
			{
				text = Utils.GetOwnerHostFqdn();
			}
			catch (LocalizedException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "Couldnt get correct Source string to use in Diagnostic header. Error : {0}", new object[]
				{
					ex
				});
			}
			if (endingReason.AdminScopeOfAction == null && useDataCenterCallRouting)
			{
				errorCode = CallEndingReason.DatacenterInternalError.ErrorCode;
				reason = CallEndingReason.DatacenterInternalError.Reason;
			}
			string name = "ms-diagnostics-public";
			string text2 = null;
			if (!string.IsNullOrEmpty(extraInfoFormatString))
			{
				text2 = string.Format(CultureInfo.InvariantCulture, extraInfoFormatString, args);
			}
			string text3 = (text2 == null) ? reason : string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				reason,
				text2
			});
			string value;
			if (endingReason.TypeOfCallEnding == null)
			{
				value = string.Format(CultureInfo.InvariantCulture, "{0};reason=\"{1}\"", new object[]
				{
					errorCode,
					text3
				});
			}
			else
			{
				value = string.Format(CultureInfo.InvariantCulture, "{0};source=\"{1}\";reason=\"{2}\"", new object[]
				{
					errorCode,
					text,
					text3
				});
			}
			CallRejectedException.LogCallRejection(endingReason, (text2 != null) ? text2 : string.Empty);
			return Platform.Builder.CreateSignalingHeader(name, value);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00017480 File Offset: 0x00015680
		internal static void LogCallRejection(CallEndingReason endingReason, string extraInfo)
		{
			if (CommonConstants.UseDataCenterLogging)
			{
				CallRejectionLogger.CallRejectionLogRow callRejectionLogRow = new CallRejectionLogger.CallRejectionLogRow();
				callRejectionLogRow.UMServerName = Utils.GetLocalHostName();
				callRejectionLogRow.TimeStamp = (DateTime)ExDateTime.UtcNow;
				callRejectionLogRow.ErrorCode = endingReason.ErrorCode;
				callRejectionLogRow.ErrorType = endingReason.TypeOfCallEnding.ToString();
				callRejectionLogRow.ErrorCategory = endingReason.Category.ToString();
				callRejectionLogRow.ErrorDescription = endingReason.Reason.ToString();
				callRejectionLogRow.ExtraInfo = extraInfo;
				CallRejectionLogger.Instance.Append(callRejectionLogRow);
			}
		}
	}
}
