using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200020A RID: 522
	public abstract class ShowDialogCommandAction : ResultsCommandAction
	{
		// Token: 0x060017AF RID: 6063 RVA: 0x000637AC File Offset: 0x000619AC
		public ShowDialogCommandAction()
		{
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000637D0 File Offset: 0x000619D0
		protected override void OnExecute()
		{
			base.OnExecute();
			ExchangePropertyPageControl exchangePropertyPageControl = this.CreateDialogControl();
			DataContext context = exchangePropertyPageControl.Context;
			if (context != null)
			{
				context.DataSaved += delegate(object param0, EventArgs param1)
				{
					this.RefreshResultsThreadSafely(context);
				};
				context.RefreshOnSave = (context.RefreshOnSave ?? base.GetDefaultRefreshObject());
			}
			base.ResultPane.ShowDialog(exchangePropertyPageControl);
		}

		// Token: 0x060017B1 RID: 6065
		protected abstract ExchangePropertyPageControl CreateDialogControl();
	}
}
