using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200014B RID: 331
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FailureInfo
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0003955F File Offset: 0x0003775F
		public LocalizedString ErrorMessage
		{
			get
			{
				return this.m_errorMessage;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00039567 File Offset: 0x00037767
		public ExtendedErrorInfo ExtendedErrorInfo
		{
			get
			{
				return this.m_extendedErrorInfo;
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0003956F File Offset: 0x0003776F
		public FailureInfo()
		{
			this.m_brokenFlags = FailureInfo.FailureFlags.Unknown;
			this.m_errorEventId = null;
			this.m_extendedErrorInfo = null;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0003959C File Offset: 0x0003779C
		public FailureInfo(FailureInfo other)
		{
			this.m_brokenFlags = other.m_brokenFlags;
			this.m_errorEventId = other.m_errorEventId;
			if (!other.m_errorMessage.IsEmpty)
			{
				this.m_errorMessage = new LocalizedString(other.m_errorMessage);
			}
			if (other.m_extendedErrorInfo != null)
			{
				this.m_extendedErrorInfo = new ExtendedErrorInfo(other.m_extendedErrorInfo.FailureException);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0003961C File Offset: 0x0003781C
		public bool IsFailed
		{
			get
			{
				bool result;
				lock (this)
				{
					result = (this.m_brokenFlags == FailureInfo.FailureFlags.Failed);
				}
				return result;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0003965C File Offset: 0x0003785C
		public bool IsDisconnected
		{
			get
			{
				bool result;
				lock (this)
				{
					result = (this.m_brokenFlags == FailureInfo.FailureFlags.Disconnected);
				}
				return result;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0003969C File Offset: 0x0003789C
		public uint ErrorEventId
		{
			get
			{
				uint? errorEventId = this.m_errorEventId;
				if (errorEventId == null)
				{
					return 0U;
				}
				return errorEventId.GetValueOrDefault();
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000396C2 File Offset: 0x000378C2
		public void SetBroken(uint? eventId, LocalizedString errorMessage, ExtendedErrorInfo extendedErrorInfo)
		{
			this.SetState(FailureInfo.FailureFlags.Failed, eventId, errorMessage, extendedErrorInfo);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000396CE File Offset: 0x000378CE
		public void SetBroken(ExEventLog.EventTuple eventTuple, LocalizedString errorMessage, ExtendedErrorInfo extendedErrorInfo)
		{
			this.SetState(FailureInfo.FailureFlags.Failed, new uint?(DiagCore.GetEventViewerEventId(eventTuple)), errorMessage, extendedErrorInfo);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000396E4 File Offset: 0x000378E4
		public void SetDisconnected(ExEventLog.EventTuple eventTuple, LocalizedString errorMessage)
		{
			this.SetState(FailureInfo.FailureFlags.Disconnected, new uint?(DiagCore.GetEventViewerEventId(eventTuple)), errorMessage, null);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000396FC File Offset: 0x000378FC
		public void Reset()
		{
			lock (this)
			{
				this.m_brokenFlags = FailureInfo.FailureFlags.NoFailure;
				this.m_errorEventId = null;
				this.m_errorMessage = LocalizedString.Empty;
				this.m_extendedErrorInfo = null;
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00039758 File Offset: 0x00037958
		public void PersistFailure(ReplayState replayState)
		{
			if (this.IsFailed)
			{
				replayState.ConfigBroken = true;
				replayState.ConfigBrokenMessage = this.ErrorMessage;
				replayState.ConfigBrokenEventId = (long)((ulong)this.ErrorEventId);
				replayState.ConfigBrokenExtendedErrorInfo = this.ExtendedErrorInfo;
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00039794 File Offset: 0x00037994
		private void SetState(FailureInfo.FailureFlags failureState, uint? errorEventId, LocalizedString errorMessage, ExtendedErrorInfo extendedErrorInfo)
		{
			lock (this)
			{
				this.m_brokenFlags = failureState;
				this.m_errorEventId = errorEventId;
				this.m_errorMessage = errorMessage;
				this.m_extendedErrorInfo = extendedErrorInfo;
			}
		}

		// Token: 0x0400057F RID: 1407
		private FailureInfo.FailureFlags m_brokenFlags;

		// Token: 0x04000580 RID: 1408
		private uint? m_errorEventId;

		// Token: 0x04000581 RID: 1409
		private LocalizedString m_errorMessage = LocalizedString.Empty;

		// Token: 0x04000582 RID: 1410
		private ExtendedErrorInfo m_extendedErrorInfo;

		// Token: 0x0200014C RID: 332
		internal enum FailureFlags
		{
			// Token: 0x04000584 RID: 1412
			Unknown,
			// Token: 0x04000585 RID: 1413
			Failed,
			// Token: 0x04000586 RID: 1414
			Disconnected,
			// Token: 0x04000587 RID: 1415
			NoFailure
		}
	}
}
