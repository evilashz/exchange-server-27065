using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000100 RID: 256
	internal class ItemPropertiesDataContext : DataContext
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x000128C9 File Offset: 0x00010AC9
		public ItemPropertiesDataContext(ItemPropertiesBase props)
		{
			this.props = props;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000128D8 File Offset: 0x00010AD8
		public override string ToString()
		{
			return string.Format("ItemProps: {0}", this.props);
		}

		// Token: 0x04000566 RID: 1382
		private ItemPropertiesBase props;
	}
}
