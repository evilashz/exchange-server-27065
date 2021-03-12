using System;
using System.Collections.Generic;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000034 RID: 52
	public class FixedThrottleEntry : ThrottleDescriptionEntry
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00006267 File Offset: 0x00004467
		public FixedThrottleEntry(RecoveryActionId recoveryActionId, string resourceName, int localMinimumMinutesBetweenAttempts, int localMaximumAllowedAttemptsInOneHour, int localMaximumAllowedAttemptsInADay, int groupMinimumMinutesBetweenAttempts, int groupMaximumAllowedAttemptsInADay) : base(ThrottleEntryType.BaseConfig, recoveryActionId, ResponderCategory.Default, "*", "*", resourceName)
		{
			this.ThrottleParameters = new ThrottleParameters(true, localMinimumMinutesBetweenAttempts, localMaximumAllowedAttemptsInOneHour, localMaximumAllowedAttemptsInADay, groupMinimumMinutesBetweenAttempts, groupMaximumAllowedAttemptsInADay);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006292 File Offset: 0x00004492
		public FixedThrottleEntry(RecoveryActionId recoveryActionId, int localMinimumMinutesBetweenAttempts, int localMaximumAllowedAttemptsInOneHour, int localMaximumAllowedAttemptsInADay, int groupMinimumMinutesBetweenAttempts, int groupMaximumAllowedAttemptsInADay) : base(ThrottleEntryType.BaseConfig, recoveryActionId, ResponderCategory.Default, "*", "*", "*")
		{
			this.ThrottleParameters = new ThrottleParameters(true, localMinimumMinutesBetweenAttempts, localMaximumAllowedAttemptsInOneHour, localMaximumAllowedAttemptsInADay, groupMinimumMinutesBetweenAttempts, groupMaximumAllowedAttemptsInADay);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000062C0 File Offset: 0x000044C0
		public FixedThrottleEntry(RecoveryActionId recoveryActionId, ResponderCategory responderCategory, string responderTypeName, string responderName, string resourceName, ThrottleParameters throttleParameters) : base(ThrottleEntryType.Effective, recoveryActionId, responderCategory, responderTypeName, responderName, resourceName)
		{
			this.ThrottleParameters = throttleParameters.Clone();
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000062DD File Offset: 0x000044DD
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000062E5 File Offset: 0x000044E5
		public ThrottleParameters ThrottleParameters { get; private set; }

		// Token: 0x060001AC RID: 428 RVA: 0x000062EE File Offset: 0x000044EE
		internal override Dictionary<string, string> GetPropertyBag()
		{
			return this.ThrottleParameters.ToDictionary();
		}
	}
}
