using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000030 RID: 48
	public class EmptyTwir : ITWIR
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0000EE78 File Offset: 0x0000D078
		public int GetColumnSize(Column column)
		{
			return ((IColumn)column).GetSize(this);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000EE90 File Offset: 0x0000D090
		public object GetColumnValue(Column column)
		{
			return ((IColumn)column).GetValue(this);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000EEA6 File Offset: 0x0000D0A6
		public int GetPhysicalColumnSize(PhysicalColumn column)
		{
			return 0;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000EEA9 File Offset: 0x0000D0A9
		public object GetPhysicalColumnValue(PhysicalColumn column)
		{
			return null;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000EEAC File Offset: 0x0000D0AC
		public int GetPropertyColumnSize(PropertyColumn column)
		{
			return 0;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000EEAF File Offset: 0x0000D0AF
		public object GetPropertyColumnValue(PropertyColumn column)
		{
			return null;
		}

		// Token: 0x040000C4 RID: 196
		public static ITWIR Instance = new EmptyTwir();
	}
}
