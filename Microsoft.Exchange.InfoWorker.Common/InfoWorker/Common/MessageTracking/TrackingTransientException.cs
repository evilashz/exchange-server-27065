using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002F8 RID: 760
	internal class TrackingTransientException : TrackingBaseException
	{
		// Token: 0x06001676 RID: 5750 RVA: 0x00068F45 File Offset: 0x00067145
		public TrackingTransientException(TrackingError trackingError, Exception innerException, bool isAlreadyLogged) : base(trackingError, innerException, isAlreadyLogged)
		{
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00068F50 File Offset: 0x00067150
		public static TrackingTransientException RaiseE(ErrorCode errorCode)
		{
			throw new TrackingTransientException(errorCode, string.Empty, string.Empty, string.Empty, new object[0]);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00068F6D File Offset: 0x0006716D
		public static TrackingTransientException RaiseETD(ErrorCode errorCode, string target, string dataFormat, params object[] dataParams)
		{
			throw new TrackingTransientException(errorCode, target, string.Empty, dataFormat, dataParams);
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00068F7D File Offset: 0x0006717D
		public static TrackingTransientException RaiseETX(ErrorCode errorCode, string target, string exception)
		{
			throw new TrackingTransientException(errorCode, target, exception, string.Empty, new object[0]);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x00068F92 File Offset: 0x00067192
		public static TrackingTransientException AddAndRaiseE(TrackingErrorCollection errors, ErrorCode errorCode)
		{
			throw new TrackingTransientException(errors, errorCode, string.Empty, string.Empty, string.Empty, new object[0]);
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00068FB0 File Offset: 0x000671B0
		public static TrackingTransientException AddAndRaiseETD(TrackingErrorCollection errors, ErrorCode errorCode, string target, string dataFormat, params object[] dataParams)
		{
			throw new TrackingTransientException(errors, errorCode, target, string.Empty, dataFormat, dataParams);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00068FC2 File Offset: 0x000671C2
		public static TrackingTransientException AddAndRaiseETX(TrackingErrorCollection errors, ErrorCode errorCode, string target, string exception)
		{
			throw new TrackingTransientException(errors, errorCode, target, exception, string.Empty, new object[0]);
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00068FD8 File Offset: 0x000671D8
		private TrackingTransientException(ErrorCode errorCode, string target, string exception, string dataFormat, params object[] dataParams) : base(new TrackingError(errorCode, target, TrackingBaseException.FormatData(dataFormat, dataParams), exception), null, false)
		{
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00068FF3 File Offset: 0x000671F3
		private TrackingTransientException(TrackingErrorCollection errors, ErrorCode errorCode, string target, string exception, string dataFormat, params object[] dataParams) : base(errors.Add(errorCode, target, string.Format(dataFormat, dataParams), exception), null, true)
		{
		}
	}
}
