using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002F9 RID: 761
	internal class TrackingFatalException : TrackingBaseException
	{
		// Token: 0x0600167F RID: 5759 RVA: 0x00069010 File Offset: 0x00067210
		public TrackingFatalException(TrackingError trackingError, Exception innerException, bool isAlreadyLogged) : base(trackingError, innerException, isAlreadyLogged)
		{
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0006901B File Offset: 0x0006721B
		public static TrackingFatalException RaiseE(ErrorCode errorCode)
		{
			throw new TrackingFatalException(errorCode, string.Empty, string.Empty, string.Empty, new object[0]);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00069038 File Offset: 0x00067238
		public static TrackingFatalException RaiseED(ErrorCode errorCode, string dataFormat, params object[] dataParams)
		{
			throw new TrackingFatalException(errorCode, string.Empty, string.Empty, dataFormat, dataParams);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0006904C File Offset: 0x0006724C
		public static TrackingFatalException RaiseET(ErrorCode errorCode, string target)
		{
			throw new TrackingFatalException(errorCode, target, string.Empty, string.Empty, new object[0]);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00069065 File Offset: 0x00067265
		public static TrackingFatalException RaiseETD(ErrorCode errorCode, string target, string dataFormat, params object[] dataParams)
		{
			throw new TrackingFatalException(errorCode, target, string.Empty, dataFormat, dataParams);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00069075 File Offset: 0x00067275
		public static TrackingFatalException RaiseETX(ErrorCode errorCode, string target, string exception)
		{
			throw new TrackingFatalException(errorCode, target, exception, string.Empty, new object[0]);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0006908A File Offset: 0x0006728A
		public static TrackingFatalException RaiseEDX(ErrorCode errorCode, string exception, string dataFormat, params object[] dataParams)
		{
			throw new TrackingFatalException(errorCode, string.Empty, exception, dataFormat, dataParams);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0006909A File Offset: 0x0006729A
		public static TrackingFatalException AddAndRaiseE(TrackingErrorCollection errors, ErrorCode errorCode)
		{
			throw new TrackingFatalException(errors, errorCode, string.Empty, string.Empty, string.Empty, new object[0]);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x000690B8 File Offset: 0x000672B8
		public static TrackingFatalException AddAndRaiseED(TrackingErrorCollection errors, ErrorCode errorCode, string dataFormat, params object[] dataParams)
		{
			throw new TrackingFatalException(errors, errorCode, string.Empty, string.Empty, dataFormat, dataParams);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x000690CD File Offset: 0x000672CD
		public static TrackingFatalException AddAndRaiseETD(TrackingErrorCollection errors, ErrorCode errorCode, string target, string dataFormat, params object[] dataParams)
		{
			throw new TrackingFatalException(errors, errorCode, target, string.Empty, dataFormat, dataParams);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000690DF File Offset: 0x000672DF
		public static TrackingFatalException AddAndRaiseETX(TrackingErrorCollection errors, ErrorCode errorCode, string target, string exception)
		{
			throw new TrackingFatalException(errors, errorCode, target, exception, string.Empty, new object[0]);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000690F5 File Offset: 0x000672F5
		private TrackingFatalException(ErrorCode errorCode, string target, string exception, string dataFormat, params object[] dataParams) : base(new TrackingError(errorCode, target, TrackingBaseException.FormatData(dataFormat, dataParams), exception), null, false)
		{
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00069110 File Offset: 0x00067310
		private TrackingFatalException(TrackingErrorCollection errors, ErrorCode errorCode, string target, string exception, string dataFormat, params object[] dataParams) : base(errors.Add(errorCode, target, string.Format(dataFormat, dataParams), exception), null, true)
		{
		}
	}
}
