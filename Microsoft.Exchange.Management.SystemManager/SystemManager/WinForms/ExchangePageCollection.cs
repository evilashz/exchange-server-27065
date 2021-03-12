using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000121 RID: 289
	public class ExchangePageCollection : Collection<ExchangePage>
	{
		// Token: 0x06000B2C RID: 2860 RVA: 0x00027F01 File Offset: 0x00026101
		internal ExchangePageCollection(DataContextFlags owner)
		{
			this.owner = owner;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00027F10 File Offset: 0x00026110
		protected override void InsertItem(int index, ExchangePage item)
		{
			base.InsertItem(index, item);
			this.ExchangePage_ContextChanged(item, EventArgs.Empty);
			item.ContextChanged += this.ExchangePage_ContextChanged;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00027F38 File Offset: 0x00026138
		protected override void RemoveItem(int index)
		{
			base.Items[index].ContextChanged -= this.ExchangePage_ContextChanged;
			base.RemoveItem(index);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00027F60 File Offset: 0x00026160
		private void ExchangePage_ContextChanged(object sender, EventArgs e)
		{
			ExchangePage exchangePage = (ExchangePage)sender;
			if (exchangePage.Context != null)
			{
				exchangePage.Context.Flags = this.owner;
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00027F8D File Offset: 0x0002618D
		protected override void SetItem(int index, ExchangePage item)
		{
			base.RemoveAt(index);
			this.InsertItem(index, item);
		}

		// Token: 0x040004AD RID: 1197
		private DataContextFlags owner;
	}
}
