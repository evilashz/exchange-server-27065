using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000F1 RID: 241
	internal interface IPAAEvaluator : IDisposeTrackable, IDisposable
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060007ED RID: 2029
		// (set) Token: 0x060007EE RID: 2030
		IDataLoader UserDataLoader { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060007EF RID: 2031
		// (set) Token: 0x060007F0 RID: 2032
		IPAAStore PAAStorage { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060007F1 RID: 2033
		// (set) Token: 0x060007F2 RID: 2034
		string CallId { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060007F3 RID: 2035
		// (set) Token: 0x060007F4 RID: 2036
		Breadcrumbs Crumbs { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060007F5 RID: 2037
		bool EvaluationTimedOut { get; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060007F6 RID: 2038
		bool SubscriberHasPAAConfigured { get; }

		// Token: 0x060007F7 RID: 2039
		bool GetEffectivePAA(out PersonalAutoAttendant personalAutoAttendant);
	}
}
