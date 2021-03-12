using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200028B RID: 651
	internal class EasDeviceBudgetWrapper : StandardBudgetWrapperBase<EasDeviceBudget>, IEasDeviceBudget, IStandardBudget, IBudget, IDisposable
	{
		// Token: 0x060017EF RID: 6127 RVA: 0x0008D2BF File Offset: 0x0008B4BF
		internal EasDeviceBudgetWrapper(EasDeviceBudget innerBudget) : base(innerBudget)
		{
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0008D2C8 File Offset: 0x0008B4C8
		protected override EasDeviceBudget ReacquireBudget()
		{
			return EasDeviceBudgetCache.Singleton.Get(base.Owner);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0008D2DA File Offset: 0x0008B4DA
		public void AddInteractiveCall()
		{
			base.GetInnerBudget().AddInteractiveCall();
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0008D2E7 File Offset: 0x0008B4E7
		public void AddCall()
		{
			base.GetInnerBudget().AddCall();
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0008D2F4 File Offset: 0x0008B4F4
		public float Percentage
		{
			get
			{
				return base.GetInnerBudget().Percentage;
			}
		}
	}
}
