using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x02000BF8 RID: 3064
	internal class UpdatableExchangeHelpProgressEventArgs : EventArgs
	{
		// Token: 0x06007392 RID: 29586 RVA: 0x001D5E30 File Offset: 0x001D4030
		internal UpdatableExchangeHelpProgressEventArgs(UpdatePhase phase, LocalizedString subTask, int numerator, int denominator)
		{
			LocalizedString value = LocalizedString.Empty;
			switch (phase)
			{
			case UpdatePhase.Checking:
				value = UpdatableHelpStrings.UpdatePhaseChecking;
				break;
			case UpdatePhase.Downloading:
				value = UpdatableHelpStrings.UpdatePhaseDownloading;
				break;
			case UpdatePhase.Extracting:
				value = UpdatableHelpStrings.UpdatePhaseExtracting;
				break;
			case UpdatePhase.Validating:
				value = UpdatableHelpStrings.UpdatePhaseValidating;
				break;
			case UpdatePhase.Installing:
				value = UpdatableHelpStrings.UpdatePhaseInstalling;
				break;
			case UpdatePhase.Finalizing:
				value = UpdatableHelpStrings.UpdatePhaseFinalizing;
				break;
			case UpdatePhase.Rollback:
				value = UpdatableHelpStrings.UpdatePhaseRollback;
				break;
			}
			this.ProgressStatus = ((!subTask.Equals(LocalizedString.Empty)) ? UpdatableHelpStrings.UpdateStatus2(value, subTask) : UpdatableHelpStrings.UpdateStatus1(value));
			this.Activity = UpdatableHelpStrings.UpdateModuleName;
			if (denominator != 0)
			{
				this.PercentCompleted = Math.Abs(numerator) * 100 / Math.Abs(denominator);
				return;
			}
			this.PercentCompleted = 0;
		}

		// Token: 0x17002392 RID: 9106
		// (get) Token: 0x06007393 RID: 29587 RVA: 0x001D5F05 File Offset: 0x001D4105
		// (set) Token: 0x06007394 RID: 29588 RVA: 0x001D5F0D File Offset: 0x001D410D
		internal LocalizedString Activity { get; private set; }

		// Token: 0x17002393 RID: 9107
		// (get) Token: 0x06007395 RID: 29589 RVA: 0x001D5F16 File Offset: 0x001D4116
		// (set) Token: 0x06007396 RID: 29590 RVA: 0x001D5F1E File Offset: 0x001D411E
		internal LocalizedString ProgressStatus { get; private set; }

		// Token: 0x17002394 RID: 9108
		// (get) Token: 0x06007397 RID: 29591 RVA: 0x001D5F27 File Offset: 0x001D4127
		// (set) Token: 0x06007398 RID: 29592 RVA: 0x001D5F2F File Offset: 0x001D412F
		internal int PercentCompleted { get; private set; }
	}
}
