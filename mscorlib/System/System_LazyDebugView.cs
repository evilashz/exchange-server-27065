﻿using System;
using System.Threading;

namespace System
{
	// Token: 0x020000F9 RID: 249
	internal sealed class System_LazyDebugView<T>
	{
		// Token: 0x06000F23 RID: 3875 RVA: 0x0002F1BF File Offset: 0x0002D3BF
		public System_LazyDebugView(Lazy<T> lazy)
		{
			this.m_lazy = lazy;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0002F1CE File Offset: 0x0002D3CE
		public bool IsValueCreated
		{
			get
			{
				return this.m_lazy.IsValueCreated;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0002F1DB File Offset: 0x0002D3DB
		public T Value
		{
			get
			{
				return this.m_lazy.ValueForDebugDisplay;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0002F1E8 File Offset: 0x0002D3E8
		public LazyThreadSafetyMode Mode
		{
			get
			{
				return this.m_lazy.Mode;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0002F1F5 File Offset: 0x0002D3F5
		public bool IsValueFaulted
		{
			get
			{
				return this.m_lazy.IsValueFaulted;
			}
		}

		// Token: 0x0400059E RID: 1438
		private readonly Lazy<T> m_lazy;
	}
}
