using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002D5 RID: 725
	public class SortBoundaries
	{
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x000A16FD File Offset: 0x0009F8FD
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x000A1705 File Offset: 0x0009F905
		public Strings.IDs TextID
		{
			get
			{
				return this.textID;
			}
			set
			{
				this.textID = value;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x000A170E File Offset: 0x0009F90E
		// (set) Token: 0x06001C0B RID: 7179 RVA: 0x000A1716 File Offset: 0x0009F916
		public Strings.IDs AscendingID
		{
			get
			{
				return this.ascendingID;
			}
			set
			{
				this.ascendingID = value;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x000A171F File Offset: 0x0009F91F
		// (set) Token: 0x06001C0D RID: 7181 RVA: 0x000A1727 File Offset: 0x0009F927
		public Strings.IDs DescendingID
		{
			get
			{
				return this.descendingID;
			}
			set
			{
				this.descendingID = value;
			}
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x000A1730 File Offset: 0x0009F930
		public SortBoundaries(Strings.IDs textID, Strings.IDs ascendingID, Strings.IDs descendingID)
		{
			this.textID = textID;
			this.ascendingID = ascendingID;
			this.descendingID = descendingID;
		}

		// Token: 0x040014B6 RID: 5302
		private Strings.IDs textID;

		// Token: 0x040014B7 RID: 5303
		private Strings.IDs ascendingID;

		// Token: 0x040014B8 RID: 5304
		private Strings.IDs descendingID;
	}
}
