using System;

namespace Microsoft.Exchange.Data.Generated
{
	// Token: 0x0200023D RID: 573
	public abstract class ScanBuff
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060013B3 RID: 5043
		// (set) Token: 0x060013B4 RID: 5044
		public abstract int Pos { get; set; }

		// Token: 0x060013B5 RID: 5045
		public abstract int Read();

		// Token: 0x060013B6 RID: 5046
		public abstract int Peek();

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060013B7 RID: 5047
		public abstract int ReadPos { get; }

		// Token: 0x060013B8 RID: 5048
		public abstract string GetString(int b, int e);

		// Token: 0x04000B8B RID: 2955
		public const int EOF = -1;
	}
}
