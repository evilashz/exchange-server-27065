using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000046 RID: 70
	internal abstract class ABit
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000F299 File Offset: 0x0000D499
		public static Func<int[], uint, int> GetBitsFunc
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000F29C File Offset: 0x0000D49C
		public static Action<int[], uint, int> SetBitsAction
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000F29F File Offset: 0x0000D49F
		public static Func<uint, int> GetPhysicalIndexFunc
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000F2A2 File Offset: 0x0000D4A2
		public static Func<int, uint, int, int> NewValueInPhysicalIndexFunc
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000F2A5 File Offset: 0x0000D4A5
		public static Func<int, uint, int> CurrentValueInLogicIndexFunc
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000176 RID: 374
		public const int BitsForCount = 0;
	}
}
