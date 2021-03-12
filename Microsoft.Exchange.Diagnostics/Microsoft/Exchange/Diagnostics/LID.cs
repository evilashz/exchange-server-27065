using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000009 RID: 9
	public struct LID
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002505 File Offset: 0x00000705
		private LID(uint value)
		{
			this.value = value;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000250E File Offset: 0x0000070E
		public uint Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002516 File Offset: 0x00000716
		public static explicit operator LID(uint value)
		{
			return new LID(value);
		}

		// Token: 0x0400003B RID: 59
		private uint value;
	}
}
