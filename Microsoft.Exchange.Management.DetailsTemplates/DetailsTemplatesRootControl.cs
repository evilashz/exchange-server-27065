using System;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000011 RID: 17
	internal class DetailsTemplatesRootControl : ExchangeUserControl
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003BF8 File Offset: 0x00001DF8
		protected override bool ProcessMnemonic(char charCode)
		{
			return false;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003BFB File Offset: 0x00001DFB
		[ReadOnly(true)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
		}
	}
}
