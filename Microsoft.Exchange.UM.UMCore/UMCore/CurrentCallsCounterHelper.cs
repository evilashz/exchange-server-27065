using System;
using System.Threading;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000104 RID: 260
	internal class CurrentCallsCounterHelper
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x0001D448 File Offset: 0x0001B648
		private CurrentCallsCounterHelper()
		{
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001D450 File Offset: 0x0001B650
		internal static CurrentCallsCounterHelper Instance
		{
			get
			{
				return CurrentCallsCounterHelper.instance;
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001D457 File Offset: 0x0001B657
		internal void Init()
		{
			this.currentCallsCounterTimer = new Timer(new TimerCallback(this.UpdateCurrentCallsCounters), null, Constants.UpdateCurrentCallsTimerInterval, Constants.UpdateCurrentCallsTimerInterval);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001D47B File Offset: 0x0001B67B
		internal void Shutdown()
		{
			if (this.currentCallsCounterTimer != null)
			{
				this.currentCallsCounterTimer.Dispose();
				this.currentCallsCounterTimer = null;
			}
			this.UpdateCurrentCallsCounters(null);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001D4A0 File Offset: 0x0001B6A0
		internal void AnalyzeCall(BaseUMCallSession callSession)
		{
			CallContext currentCallContext = callSession.CurrentCallContext;
			if (currentCallContext == null)
			{
				return;
			}
			CallType callType = currentCallContext.CallType;
			if (callType != null && callType != 7)
			{
				this.currentCalls += 1L;
			}
			switch (callType)
			{
			case 1:
				this.currentUnauthenticatedPilotNumberCalls += 1L;
				break;
			case 2:
				this.currentAutoAttendantCalls += 1L;
				return;
			case 3:
				this.currentSubscriberAccessCalls += 1L;
				return;
			case 4:
				this.currentVoiceCalls += 1L;
				return;
			case 5:
				this.currentPlayOnPhoneCalls += 1L;
				return;
			case 6:
				this.currentFaxCalls += 1L;
				return;
			case 7:
			case 9:
				break;
			case 8:
				this.currentPromptEditingCalls += 1L;
				return;
			case 10:
				this.currentPlayOnPhoneCalls += 1L;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001D588 File Offset: 0x0001B788
		internal void UpdatePerformanceCounters()
		{
			Util.SetCounter(GeneralCounters.CurrentCalls, this.currentCalls);
			Util.SetCounter(GeneralCounters.CurrentFaxCalls, this.currentFaxCalls);
			Util.SetCounter(GeneralCounters.CurrentVoicemailCalls, this.currentVoiceCalls);
			Util.SetCounter(GeneralCounters.CurrentPlayOnPhoneCalls, this.currentPlayOnPhoneCalls);
			Util.SetCounter(GeneralCounters.CurrentAutoAttendantCalls, this.currentAutoAttendantCalls);
			Util.SetCounter(GeneralCounters.CurrentPromptEditingCalls, this.currentPromptEditingCalls);
			Util.SetCounter(GeneralCounters.CurrentSubscriberAccessCalls, this.currentSubscriberAccessCalls);
			Util.SetCounter(GeneralCounters.CurrentUnauthenticatedPilotNumberCalls, this.currentUnauthenticatedPilotNumberCalls);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001D618 File Offset: 0x0001B818
		internal void Reset()
		{
			this.currentCalls = 0L;
			this.currentFaxCalls = 0L;
			this.currentVoiceCalls = 0L;
			this.currentPlayOnPhoneCalls = 0L;
			this.currentAutoAttendantCalls = 0L;
			this.currentPromptEditingCalls = 0L;
			this.currentSubscriberAccessCalls = 0L;
			this.currentUnauthenticatedPilotNumberCalls = 0L;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001D668 File Offset: 0x0001B868
		private void UpdateCurrentCallsCounters(object state)
		{
			if (UmServiceGlobals.ArePerfCountersEnabled)
			{
				lock (UmServiceGlobals.VoipPlatform.CallSessionHashTable.SyncRoot)
				{
					this.Reset();
					foreach (object obj in UmServiceGlobals.VoipPlatform.CallSessionHashTable.Values)
					{
						BaseUMCallSession callSession = (BaseUMCallSession)obj;
						this.AnalyzeCall(callSession);
					}
					this.UpdatePerformanceCounters();
				}
			}
		}

		// Token: 0x04000818 RID: 2072
		private static CurrentCallsCounterHelper instance = new CurrentCallsCounterHelper();

		// Token: 0x04000819 RID: 2073
		private Timer currentCallsCounterTimer;

		// Token: 0x0400081A RID: 2074
		private long currentCalls;

		// Token: 0x0400081B RID: 2075
		private long currentFaxCalls;

		// Token: 0x0400081C RID: 2076
		private long currentVoiceCalls;

		// Token: 0x0400081D RID: 2077
		private long currentPlayOnPhoneCalls;

		// Token: 0x0400081E RID: 2078
		private long currentAutoAttendantCalls;

		// Token: 0x0400081F RID: 2079
		private long currentPromptEditingCalls;

		// Token: 0x04000820 RID: 2080
		private long currentSubscriberAccessCalls;

		// Token: 0x04000821 RID: 2081
		private long currentUnauthenticatedPilotNumberCalls;
	}
}
