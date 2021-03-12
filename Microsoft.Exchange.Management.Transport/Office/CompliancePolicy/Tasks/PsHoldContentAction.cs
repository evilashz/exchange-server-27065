using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000C3 RID: 195
	public class PsHoldContentAction : PsComplianceRuleActionBase
	{
		// Token: 0x06000708 RID: 1800 RVA: 0x0001E34B File Offset: 0x0001C54B
		public PsHoldContentAction(int holdDurationDays, HoldDurationHint holdDurationDisplayHint)
		{
			this.HoldDurationDays = holdDurationDays;
			this.HoldDurationDisplayHint = holdDurationDisplayHint;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001E361 File Offset: 0x0001C561
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x0001E369 File Offset: 0x0001C569
		public int HoldDurationDays { get; private set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001E372 File Offset: 0x0001C572
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x0001E37A File Offset: 0x0001C57A
		public HoldDurationHint HoldDurationDisplayHint { get; private set; }

		// Token: 0x0600070D RID: 1805 RVA: 0x0001E384 File Offset: 0x0001C584
		internal override Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action ToEngineAction()
		{
			return new HoldAction(new List<Argument>
			{
				new Value(this.HoldDurationDays.ToString(CultureInfo.InvariantCulture)),
				new Value(this.HoldDurationDisplayHint.ToString())
			}, null);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001E3D7 File Offset: 0x0001C5D7
		internal static PsHoldContentAction FromEngineAction(HoldAction action)
		{
			return new PsHoldContentAction(action.HoldDurationDays, action.HoldDurationDisplayHint);
		}
	}
}
