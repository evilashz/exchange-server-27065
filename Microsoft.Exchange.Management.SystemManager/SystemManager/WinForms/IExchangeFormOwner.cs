using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000139 RID: 313
	public interface IExchangeFormOwner
	{
		// Token: 0x06000C5F RID: 3167
		void OnExchangeFormLoad(ExchangeForm form);

		// Token: 0x06000C60 RID: 3168
		void OnExchangeFormClosed(ExchangeForm formToClose);
	}
}
