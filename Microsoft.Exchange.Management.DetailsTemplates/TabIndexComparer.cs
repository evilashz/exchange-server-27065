using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x0200001E RID: 30
	internal class TabIndexComparer : IComparer<Control>
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000622E File Offset: 0x0000442E
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00006236 File Offset: 0x00004436
		public bool Win32Sort
		{
			get
			{
				return this.win32Sort;
			}
			set
			{
				this.win32Sort = value;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006240 File Offset: 0x00004440
		public int Compare(Control lhs, Control rhs)
		{
			if (!this.Win32Sort)
			{
				bool flag = lhs is GroupBox;
				bool flag2 = rhs is GroupBox;
				if (flag && !flag2)
				{
					return 1;
				}
				if (!flag && flag2)
				{
					return -1;
				}
				if (lhs.TabIndex > rhs.TabIndex)
				{
					return -1;
				}
				if (lhs.TabIndex == rhs.TabIndex)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (lhs.TabIndex < rhs.TabIndex)
				{
					return -1;
				}
				if (lhs.TabIndex == rhs.TabIndex)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000062BC File Offset: 0x000044BC
		public TabIndexComparer(bool win32Sort)
		{
			this.win32Sort = win32Sort;
		}

		// Token: 0x04000058 RID: 88
		private bool win32Sort = true;
	}
}
