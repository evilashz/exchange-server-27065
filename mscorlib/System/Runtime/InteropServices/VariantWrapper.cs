using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000934 RID: 2356
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class VariantWrapper
	{
		// Token: 0x06006105 RID: 24837 RVA: 0x0014AC36 File Offset: 0x00148E36
		[__DynamicallyInvokable]
		public VariantWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x06006106 RID: 24838 RVA: 0x0014AC45 File Offset: 0x00148E45
		[__DynamicallyInvokable]
		public object WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002AD5 RID: 10965
		private object m_WrappedObject;
	}
}
