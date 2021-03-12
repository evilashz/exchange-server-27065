using System;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x02000019 RID: 25
	public class ResolveControlEventArgs : EventArgs
	{
		// Token: 0x060000AB RID: 171 RVA: 0x0000339A File Offset: 0x0000159A
		public ResolveControlEventArgs(string controlId)
		{
			this.controlID = controlId;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000033A9 File Offset: 0x000015A9
		public string ControlID
		{
			get
			{
				return this.controlID;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000033B1 File Offset: 0x000015B1
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000033B9 File Offset: 0x000015B9
		public Control Control
		{
			get
			{
				return this.control;
			}
			set
			{
				this.control = value;
			}
		}

		// Token: 0x0400002B RID: 43
		private string controlID;

		// Token: 0x0400002C RID: 44
		private Control control;
	}
}
