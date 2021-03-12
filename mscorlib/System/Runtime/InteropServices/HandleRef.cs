using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200091D RID: 2333
	[ComVisible(true)]
	public struct HandleRef
	{
		// Token: 0x06005F89 RID: 24457 RVA: 0x00147F3D File Offset: 0x0014613D
		public HandleRef(object wrapper, IntPtr handle)
		{
			this.m_wrapper = wrapper;
			this.m_handle = handle;
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06005F8A RID: 24458 RVA: 0x00147F4D File Offset: 0x0014614D
		public object Wrapper
		{
			get
			{
				return this.m_wrapper;
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06005F8B RID: 24459 RVA: 0x00147F55 File Offset: 0x00146155
		public IntPtr Handle
		{
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x06005F8C RID: 24460 RVA: 0x00147F5D File Offset: 0x0014615D
		public static explicit operator IntPtr(HandleRef value)
		{
			return value.m_handle;
		}

		// Token: 0x06005F8D RID: 24461 RVA: 0x00147F65 File Offset: 0x00146165
		public static IntPtr ToIntPtr(HandleRef value)
		{
			return value.m_handle;
		}

		// Token: 0x04002A97 RID: 10903
		internal object m_wrapper;

		// Token: 0x04002A98 RID: 10904
		internal IntPtr m_handle;
	}
}
