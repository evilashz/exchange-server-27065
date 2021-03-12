using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008E2 RID: 2274
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class UnmanagedFunctionPointerAttribute : Attribute
	{
		// Token: 0x06005ED1 RID: 24273 RVA: 0x00146AD6 File Offset: 0x00144CD6
		[__DynamicallyInvokable]
		public UnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
		{
			this.m_callingConvention = callingConvention;
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06005ED2 RID: 24274 RVA: 0x00146AE5 File Offset: 0x00144CE5
		[__DynamicallyInvokable]
		public CallingConvention CallingConvention
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x040029A2 RID: 10658
		private CallingConvention m_callingConvention;

		// Token: 0x040029A3 RID: 10659
		[__DynamicallyInvokable]
		public CharSet CharSet;

		// Token: 0x040029A4 RID: 10660
		[__DynamicallyInvokable]
		public bool BestFitMapping;

		// Token: 0x040029A5 RID: 10661
		[__DynamicallyInvokable]
		public bool ThrowOnUnmappableChar;

		// Token: 0x040029A6 RID: 10662
		[__DynamicallyInvokable]
		public bool SetLastError;
	}
}
