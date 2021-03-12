using System;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005CE RID: 1486
	[Serializable]
	internal struct ConstArray
	{
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600455A RID: 17754 RVA: 0x000FDF77 File Offset: 0x000FC177
		public IntPtr Signature
		{
			get
			{
				return this.m_constArray;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x000FDF7F File Offset: 0x000FC17F
		public int Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x17000A88 RID: 2696
		public unsafe byte this[int index]
		{
			[SecuritySafeCritical]
			get
			{
				if (index < 0 || index >= this.m_length)
				{
					throw new IndexOutOfRangeException();
				}
				return ((byte*)this.m_constArray.ToPointer())[index];
			}
		}

		// Token: 0x04001C92 RID: 7314
		internal int m_length;

		// Token: 0x04001C93 RID: 7315
		internal IntPtr m_constArray;
	}
}
