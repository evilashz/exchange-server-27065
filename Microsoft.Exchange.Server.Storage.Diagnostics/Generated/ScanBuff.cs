using System;

namespace Microsoft.Exchange.Server.Storage.Diagnostics.Generated
{
	// Token: 0x02000026 RID: 38
	public abstract class ScanBuff
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600013E RID: 318
		// (set) Token: 0x0600013F RID: 319
		public abstract int Pos { get; set; }

		// Token: 0x06000140 RID: 320
		public abstract int Read();

		// Token: 0x06000141 RID: 321
		public abstract int Peek();

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000142 RID: 322
		public abstract int ReadPos { get; }

		// Token: 0x06000143 RID: 323
		public abstract string GetString(int b, int e);

		// Token: 0x040000DD RID: 221
		public const int EOF = -1;
	}
}
