using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002B2 RID: 690
	public abstract class CalendarVisual
	{
		// Token: 0x06001B07 RID: 6919 RVA: 0x0009B5FD File Offset: 0x000997FD
		public CalendarVisual(int dataIndex)
		{
			if (dataIndex < 0)
			{
				throw new ArgumentOutOfRangeException("dataIndex");
			}
			this.dataIndex = dataIndex;
			this.rect = new Rect();
			this.adjustedRect = new Rect();
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x0009B631 File Offset: 0x00099831
		public Rect Rect
		{
			get
			{
				return this.rect;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0009B639 File Offset: 0x00099839
		public Rect AdjustedRect
		{
			get
			{
				return this.adjustedRect;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0009B641 File Offset: 0x00099841
		public int DataIndex
		{
			get
			{
				return this.dataIndex;
			}
		}

		// Token: 0x04001322 RID: 4898
		private Rect adjustedRect;

		// Token: 0x04001323 RID: 4899
		private Rect rect;

		// Token: 0x04001324 RID: 4900
		private int dataIndex;
	}
}
