using System;
using System.Security;

namespace System
{
	// Token: 0x02000131 RID: 305
	internal struct RuntimeMethodHandleInternal
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x000372F8 File Offset: 0x000354F8
		internal static RuntimeMethodHandleInternal EmptyHandle
		{
			get
			{
				return default(RuntimeMethodHandleInternal);
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0003730E File Offset: 0x0003550E
		internal bool IsNullHandle()
		{
			return this.m_handle.IsNull();
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0003731B File Offset: 0x0003551B
		internal IntPtr Value
		{
			[SecurityCritical]
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00037323 File Offset: 0x00035523
		[SecurityCritical]
		internal RuntimeMethodHandleInternal(IntPtr value)
		{
			this.m_handle = value;
		}

		// Token: 0x04000660 RID: 1632
		internal IntPtr m_handle;
	}
}
