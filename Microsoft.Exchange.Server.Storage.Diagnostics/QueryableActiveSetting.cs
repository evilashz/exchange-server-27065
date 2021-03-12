using System;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000041 RID: 65
	public class QueryableActiveSetting
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		public QueryableActiveSetting(string name, bool isReadOnce, object value)
		{
			this.Name = name;
			this.Type = (isReadOnce ? QueryableActiveSetting.ReadOnceStatus.ReadOnce : QueryableActiveSetting.ReadOnceStatus.Dynamic);
			this.Value = value;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000D8E7 File Offset: 0x0000BAE7
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000D8EF File Offset: 0x0000BAEF
		public string Name { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000D900 File Offset: 0x0000BB00
		public QueryableActiveSetting.ReadOnceStatus Type { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000D909 File Offset: 0x0000BB09
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000D911 File Offset: 0x0000BB11
		public object Value { get; private set; }

		// Token: 0x02000042 RID: 66
		public enum ReadOnceStatus
		{
			// Token: 0x04000126 RID: 294
			ReadOnce,
			// Token: 0x04000127 RID: 295
			Dynamic
		}
	}
}
