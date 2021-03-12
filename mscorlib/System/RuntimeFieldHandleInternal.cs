using System;
using System.Security;

namespace System
{
	// Token: 0x02000135 RID: 309
	internal struct RuntimeFieldHandleInternal
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0003776C File Offset: 0x0003596C
		internal static RuntimeFieldHandleInternal EmptyHandle
		{
			get
			{
				return default(RuntimeFieldHandleInternal);
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00037782 File Offset: 0x00035982
		internal bool IsNullHandle()
		{
			return this.m_handle.IsNull();
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0003778F File Offset: 0x0003598F
		internal IntPtr Value
		{
			[SecurityCritical]
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00037797 File Offset: 0x00035997
		[SecurityCritical]
		internal RuntimeFieldHandleInternal(IntPtr value)
		{
			this.m_handle = value;
		}

		// Token: 0x0400066C RID: 1644
		internal IntPtr m_handle;
	}
}
