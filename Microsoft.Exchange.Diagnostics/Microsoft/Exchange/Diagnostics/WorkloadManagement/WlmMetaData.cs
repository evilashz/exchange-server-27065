using System;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x02000204 RID: 516
	internal enum WlmMetaData
	{
		// Token: 0x04000ABA RID: 2746
		[DisplayName("WLM.Cl")]
		WorkloadClassification,
		// Token: 0x04000ABB RID: 2747
		[DisplayName("WLM.Type")]
		WorkloadType,
		// Token: 0x04000ABC RID: 2748
		[DisplayName("WLM.Int")]
		IsInteractive,
		// Token: 0x04000ABD RID: 2749
		[DisplayName("WLM.SvcA")]
		IsServiceAccount,
		// Token: 0x04000ABE RID: 2750
		[DisplayName("WLM.Bal")]
		BudgetBalance,
		// Token: 0x04000ABF RID: 2751
		[DisplayName("WLM.TS")]
		TimeOnServer,
		// Token: 0x04000AC0 RID: 2752
		[DisplayName("WLM.OBPolPart")]
		OverBudgetPolicyPart,
		// Token: 0x04000AC1 RID: 2753
		[DisplayName("WLM.OBPolVal")]
		OverBudgetPolicyValue,
		// Token: 0x04000AC2 RID: 2754
		[DisplayName("WLM.BT")]
		BudgetType
	}
}
