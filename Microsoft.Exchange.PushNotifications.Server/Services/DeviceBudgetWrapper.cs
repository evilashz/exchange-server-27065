using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x0200001B RID: 27
	internal class DeviceBudgetWrapper : BudgetWrapper<DeviceBudget>, IDeviceBudget, IBudget, IDisposable
	{
		// Token: 0x0600009C RID: 156 RVA: 0x0000368A File Offset: 0x0000188A
		internal DeviceBudgetWrapper(DeviceBudget innerBudget) : base(innerBudget)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003693 File Offset: 0x00001893
		public bool TryApproveSendNotification(out OverBudgetException obe)
		{
			return base.GetInnerBudget().TryApproveSendNotification(out obe);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000036A1 File Offset: 0x000018A1
		public bool TryApproveInvalidNotification(out OverBudgetException obe)
		{
			return base.GetInnerBudget().TryApproveInvalidNotification(out obe);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000036AF File Offset: 0x000018AF
		protected override DeviceBudget ReacquireBudget()
		{
			return DeviceBudget.Get(base.Owner as DeviceBudgetKey);
		}
	}
}
