using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200006A RID: 106
	internal abstract class CallRejectionCounterHelper
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0001723D File Offset: 0x0001543D
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00017244 File Offset: 0x00015444
		public static CallRejectionCounterHelper Instance { get; private set; }

		// Token: 0x060004B0 RID: 1200
		public abstract void SetCounters(Exception ex, Action<bool> userDelegate, bool arg, bool isDiagnosticCall);

		// Token: 0x060004B2 RID: 1202 RVA: 0x00017254 File Offset: 0x00015454
		static CallRejectionCounterHelper()
		{
			if (CommonConstants.UseDataCenterLogging)
			{
				CallRejectionCounterHelper.Instance = new CallRejectionCounterHelper.DatacenterCallRejectionCounterHelper();
				return;
			}
			CallRejectionCounterHelper.Instance = new CallRejectionCounterHelper.EnterpriseCallRejectionCounterHelper();
		}

		// Token: 0x0200006B RID: 107
		private class EnterpriseCallRejectionCounterHelper : CallRejectionCounterHelper
		{
			// Token: 0x060004B3 RID: 1203 RVA: 0x00017272 File Offset: 0x00015472
			public override void SetCounters(Exception ex, Action<bool> userDelegate, bool arg, bool isDiagnosticCall)
			{
				if (!isDiagnosticCall)
				{
					userDelegate(arg);
				}
			}
		}

		// Token: 0x0200006C RID: 108
		private class DatacenterCallRejectionCounterHelper : CallRejectionCounterHelper
		{
			// Token: 0x060004B5 RID: 1205 RVA: 0x00017288 File Offset: 0x00015488
			public override void SetCounters(Exception ex, Action<bool> userDelegate, bool arg, bool isDiagnosticCall)
			{
				if (!isDiagnosticCall)
				{
					if (ex != null)
					{
						CallRejectedException ex2 = ex as CallRejectedException;
						if (ex2 != null && ex2.Reason.Category == 1)
						{
							return;
						}
					}
					userDelegate(arg);
				}
			}
		}
	}
}
