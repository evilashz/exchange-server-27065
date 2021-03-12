using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000930 RID: 2352
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class CurrencyWrapper
	{
		// Token: 0x060060FA RID: 24826 RVA: 0x0014AB46 File Offset: 0x00148D46
		[__DynamicallyInvokable]
		public CurrencyWrapper(decimal obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x0014AB55 File Offset: 0x00148D55
		[__DynamicallyInvokable]
		public CurrencyWrapper(object obj)
		{
			if (!(obj is decimal))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDecimal"), "obj");
			}
			this.m_WrappedObject = (decimal)obj;
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x060060FC RID: 24828 RVA: 0x0014AB86 File Offset: 0x00148D86
		[__DynamicallyInvokable]
		public decimal WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002AD1 RID: 10961
		private decimal m_WrappedObject;
	}
}
