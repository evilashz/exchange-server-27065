using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002F7 RID: 759
	internal class TrackingBaseException : Exception
	{
		// Token: 0x06001670 RID: 5744 RVA: 0x00068EC0 File Offset: 0x000670C0
		protected TrackingBaseException(TrackingError trackingError, Exception innerException, bool isAlreadyLogged) : base(trackingError.ToString(), innerException)
		{
			this.trackingError = trackingError;
			this.IsAlreadyLogged = isAlreadyLogged;
			ErrorCode errorCode;
			if (EnumValidator<ErrorCode>.TryParse(trackingError.ErrorCode, EnumParseOptions.Default, out errorCode))
			{
				this.isOverBudgetError = (errorCode == ErrorCode.BudgetExceeded || errorCode == ErrorCode.TimeBudgetExceeded || errorCode == ErrorCode.TotalBudgetExceeded);
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x00068F0D File Offset: 0x0006710D
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x00068F15 File Offset: 0x00067115
		public bool IsAlreadyLogged { get; private set; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x00068F1E File Offset: 0x0006711E
		public TrackingError TrackingError
		{
			get
			{
				return this.trackingError;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x00068F26 File Offset: 0x00067126
		public bool IsOverBudgetError
		{
			get
			{
				return this.isOverBudgetError;
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00068F2E File Offset: 0x0006712E
		internal static string FormatData(string dataFormat, params object[] dataParams)
		{
			if (!string.IsNullOrEmpty(dataFormat))
			{
				return string.Format(dataFormat, dataParams);
			}
			return string.Empty;
		}

		// Token: 0x04000E84 RID: 3716
		private bool isOverBudgetError;

		// Token: 0x04000E85 RID: 3717
		protected TrackingError trackingError;
	}
}
