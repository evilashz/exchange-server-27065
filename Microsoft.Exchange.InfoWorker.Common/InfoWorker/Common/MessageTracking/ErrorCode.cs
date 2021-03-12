using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200029D RID: 669
	internal enum ErrorCode
	{
		// Token: 0x04000C8C RID: 3212
		[ErrorCodeInformation(IsTransientError = true, ShowToIWUser = true, ShowDetailToIW = false, RequiredProperties = (RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Target), Message = Strings.IDs.TrackingErrorLogSearchServiceDown)]
		LogSearchConnection,
		// Token: 0x04000C8D RID: 3213
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = false, ShowDetailToIW = false, RequiredProperties = (RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Target), Message = Strings.IDs.TrackingErrorCrossForestMisconfiguration)]
		CrossForestMisconfiguration,
		// Token: 0x04000C8E RID: 3214
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = false, ShowDetailToIW = false, RequiredProperties = (RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Target), Message = Strings.IDs.TrackingErrorCrossPremiseMisconfiguration, MultiMessageSearchMessage = Strings.IDs.TrackingErrorCrossPremiseMisconfigurationMultiMessageSearch)]
		CrossPremiseMisconfiguration,
		// Token: 0x04000C8F RID: 3215
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = false, ShowDetailToIW = false, RequiredProperties = (RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Target), Message = Strings.IDs.TrackingErrorCrossForestAuthentication)]
		CrossForestAuthentication,
		// Token: 0x04000C90 RID: 3216
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = false, ShowDetailToIW = false, RequiredProperties = (RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Target), Message = Strings.IDs.TrackingErrorCrossPremiseAuthentication)]
		CrossPremiseAuthentication,
		// Token: 0x04000C91 RID: 3217
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = true, ShowDetailToIW = true, RequiredProperties = RequiredProperty.None, Message = Strings.IDs.TrackingErrorBudgetExceeded, MultiMessageSearchMessage = Strings.IDs.TrackingErrorBudgetExceededMultiMessageSearch)]
		BudgetExceeded,
		// Token: 0x04000C92 RID: 3218
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = true, ShowDetailToIW = true, RequiredProperties = RequiredProperty.None, Message = Strings.IDs.TrackingErrorTimeBudgetExceeded)]
		TimeBudgetExceeded,
		// Token: 0x04000C93 RID: 3219
		[ErrorCodeInformation(IsTransientError = true, ShowToIWUser = true, ShowDetailToIW = true, RequiredProperties = RequiredProperty.None, Message = Strings.IDs.TrackingTotalBudgetExceeded)]
		TotalBudgetExceeded,
		// Token: 0x04000C94 RID: 3220
		[ErrorCodeInformation(IsTransientError = true, ShowToIWUser = true, ShowDetailToIW = false, RequiredProperties = (RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Target), Message = Strings.IDs.TrackingErrorConnectivity)]
		Connectivity,
		// Token: 0x04000C95 RID: 3221
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = true, ShowDetailToIW = true, RequiredProperties = RequiredProperty.None, Message = Strings.IDs.TrackingErrorLegacySender, MultiMessageSearchMessage = Strings.IDs.TrackingErrorLegacySenderMultiMessageSearch)]
		LegacySender,
		// Token: 0x04000C96 RID: 3222
		[ErrorCodeInformation(IsTransientError = true, ShowToIWUser = true, ShowDetailToIW = false, RequiredProperties = RequiredProperty.None, Message = Strings.IDs.TrackingTransientError, MultiMessageSearchMessage = Strings.IDs.TrackingTransientErrorMultiMessageSearch)]
		GeneralTransientFailure,
		// Token: 0x04000C97 RID: 3223
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = true, ShowDetailToIW = false, RequiredProperties = RequiredProperty.None, Message = Strings.IDs.TrackingPermanentError, MultiMessageSearchMessage = Strings.IDs.TrackingPermanentErrorMultiMessageSearch)]
		GeneralFatalFailure,
		// Token: 0x04000C98 RID: 3224
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = false, ShowDetailToIW = false, RequiredProperties = RequiredProperty.None, Message = Strings.IDs.TrackingErrorReadStatus)]
		ReadStatusError,
		// Token: 0x04000C99 RID: 3225
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = true, ShowDetailToIW = true, RequiredProperties = RequiredProperty.None, Message = Strings.IDs.TrackingLogVersionIncompatible)]
		LogVersionIncompatible,
		// Token: 0x04000C9A RID: 3226
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = true, ShowDetailToIW = false, RequiredProperties = RequiredProperty.None, Message = Strings.IDs.TrackingErrorModerationDecisionLogsFromE14Rtm)]
		ModerationDecisionLogsFromE14Rtm,
		// Token: 0x04000C9B RID: 3227
		[ErrorCodeInformation(IsTransientError = true, ShowToIWUser = false, ShowDetailToIW = false, RequiredProperties = (RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Target), Message = Strings.IDs.TrackingErrorQueueViewerRpc)]
		QueueViewerConnectionFailure,
		// Token: 0x04000C9C RID: 3228
		[ErrorCodeInformation(IsTransientError = true, ShowToIWUser = true, ShowDetailToIW = false, RequiredProperties = (RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Target), Message = Strings.IDs.TrackingErrorCASUriDiscovery)]
		CASUriDiscoveryFailure,
		// Token: 0x04000C9D RID: 3229
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = true, ShowDetailToIW = false, RequiredProperties = (RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Data), Message = Strings.IDs.TrackingErrorInvalidADData)]
		InvalidADData,
		// Token: 0x04000C9E RID: 3230
		[ErrorCodeInformation(IsTransientError = false, ShowToIWUser = true, ShowDetailToIW = false, RequiredProperties = (RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Data), Message = Strings.IDs.TrackingErrorPermanentUnexpected)]
		UnexpectedErrorPermanent,
		// Token: 0x04000C9F RID: 3231
		[ErrorCodeInformation(IsTransientError = true, ShowToIWUser = true, ShowDetailToIW = false, RequiredProperties = (RequiredProperty.Server | RequiredProperty.Domain | RequiredProperty.Data), Message = Strings.IDs.TrackingErrorTransientUnexpected)]
		UnexpectedErrorTransient
	}
}
