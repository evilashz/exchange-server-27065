using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001DF RID: 479
	public class ExchangeErrorProvider : ErrorProvider
	{
		// Token: 0x060015B8 RID: 5560 RVA: 0x00059467 File Offset: 0x00057667
		public ExchangeErrorProvider(IContainer container) : base(container)
		{
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00059470 File Offset: 0x00057670
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x00059477 File Offset: 0x00057677
		public override bool RightToLeft
		{
			get
			{
				return LayoutHelper.CultureInfoIsRightToLeft;
			}
			set
			{
			}
		}
	}
}
