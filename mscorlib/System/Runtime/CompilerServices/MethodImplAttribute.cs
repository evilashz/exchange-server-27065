using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000892 RID: 2194
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class MethodImplAttribute : Attribute
	{
		// Token: 0x06005C97 RID: 23703 RVA: 0x00144CF8 File Offset: 0x00142EF8
		internal MethodImplAttribute(MethodImplAttributes methodImplAttributes)
		{
			MethodImplOptions methodImplOptions = MethodImplOptions.Unmanaged | MethodImplOptions.ForwardRef | MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall | MethodImplOptions.Synchronized | MethodImplOptions.NoInlining | MethodImplOptions.AggressiveInlining | MethodImplOptions.NoOptimization | MethodImplOptions.SecurityMitigations;
			this._val = (MethodImplOptions)(methodImplAttributes & (MethodImplAttributes)methodImplOptions);
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x00144D1A File Offset: 0x00142F1A
		[__DynamicallyInvokable]
		public MethodImplAttribute(MethodImplOptions methodImplOptions)
		{
			this._val = methodImplOptions;
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x00144D29 File Offset: 0x00142F29
		public MethodImplAttribute(short value)
		{
			this._val = (MethodImplOptions)value;
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x00144D38 File Offset: 0x00142F38
		public MethodImplAttribute()
		{
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06005C9B RID: 23707 RVA: 0x00144D40 File Offset: 0x00142F40
		[__DynamicallyInvokable]
		public MethodImplOptions Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x0400296E RID: 10606
		internal MethodImplOptions _val;

		// Token: 0x0400296F RID: 10607
		public MethodCodeType MethodCodeType;
	}
}
