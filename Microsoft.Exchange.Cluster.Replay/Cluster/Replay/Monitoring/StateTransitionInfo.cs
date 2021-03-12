using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001CB RID: 459
	internal class StateTransitionInfo
	{
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0004B5F4 File Offset: 0x000497F4
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x0004B5FC File Offset: 0x000497FC
		public TransientErrorInfo StateInfo { get; private set; }

		// Token: 0x0600123D RID: 4669 RVA: 0x0004B605 File Offset: 0x00049805
		public StateTransitionInfo() : this(true)
		{
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0004B610 File Offset: 0x00049810
		public static StateTransitionInfo ConstructFromPersisted(TransientErrorInfoPersisted errorInfo)
		{
			StateTransitionInfo stateTransitionInfo;
			if (errorInfo == null)
			{
				stateTransitionInfo = new StateTransitionInfo();
			}
			else
			{
				stateTransitionInfo = new StateTransitionInfo(false);
				stateTransitionInfo.StateInfo = TransientErrorInfo.ConstructFromPersisted(errorInfo);
			}
			return stateTransitionInfo;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0004B63E File Offset: 0x0004983E
		private StateTransitionInfo(bool fCreateTransientErrorInfo)
		{
			if (fCreateTransientErrorInfo)
			{
				this.StateInfo = new TransientErrorInfo();
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0004B654 File Offset: 0x00049854
		public TransientErrorInfoPersisted ConvertToSerializable()
		{
			return new TransientErrorInfoPersisted
			{
				CurrentErrorState = StateTransitionInfo.ConvertErrorTypeToSerializable(this.StateInfo.CurrentErrorState),
				LastSuccessTransitionUtc = DateTimeHelper.ToPersistedString(this.LastTransitionIntoStateUtc),
				LastFailureTransitionUtc = DateTimeHelper.ToPersistedString(this.LastTransitionOutOfStateUtc)
			};
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x0004B6A0 File Offset: 0x000498A0
		public bool IsSuccess
		{
			get
			{
				return this.StateInfo.CurrentErrorState == TransientErrorInfo.ErrorType.Success;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0004B6B0 File Offset: 0x000498B0
		public TimeSpan SuccessDuration
		{
			get
			{
				return this.StateInfo.SuccessDuration;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x0004B6BD File Offset: 0x000498BD
		public TimeSpan FailedDuration
		{
			get
			{
				return this.StateInfo.FailedDuration;
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0004B6CA File Offset: 0x000498CA
		public void ReportSuccess()
		{
			this.StateInfo.ReportSuccess();
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0004B6D7 File Offset: 0x000498D7
		public void ReportFailure()
		{
			this.StateInfo.ReportFailure();
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0004B6E4 File Offset: 0x000498E4
		public DateTime LastTransitionIntoStateUtc
		{
			get
			{
				return this.StateInfo.LastSuccessTransitionUtc;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x0004B6F1 File Offset: 0x000498F1
		public DateTime LastTransitionOutOfStateUtc
		{
			get
			{
				return this.StateInfo.LastFailureTransitionUtc;
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004B700 File Offset: 0x00049900
		internal static ErrorTypePersisted ConvertErrorTypeToSerializable(TransientErrorInfo.ErrorType errorType)
		{
			switch (errorType)
			{
			case TransientErrorInfo.ErrorType.Unknown:
				return ErrorTypePersisted.Unknown;
			case TransientErrorInfo.ErrorType.Success:
				return ErrorTypePersisted.Success;
			case TransientErrorInfo.ErrorType.Failure:
				return ErrorTypePersisted.Failure;
			default:
				DiagCore.RetailAssert(false, "Missing case for enumeration: {0}", new object[]
				{
					errorType
				});
				return ErrorTypePersisted.Unknown;
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0004B748 File Offset: 0x00049948
		internal static TransientErrorInfo.ErrorType ConvertErrorTypeFromSerializable(ErrorTypePersisted errorType)
		{
			switch (errorType)
			{
			case ErrorTypePersisted.Unknown:
				return TransientErrorInfo.ErrorType.Unknown;
			case ErrorTypePersisted.Success:
				return TransientErrorInfo.ErrorType.Success;
			case ErrorTypePersisted.Failure:
				return TransientErrorInfo.ErrorType.Failure;
			default:
				DiagCore.RetailAssert(false, "Missing case for enumeration: {0}", new object[]
				{
					errorType
				});
				return TransientErrorInfo.ErrorType.Unknown;
			}
		}
	}
}
