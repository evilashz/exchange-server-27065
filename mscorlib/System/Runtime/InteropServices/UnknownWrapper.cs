using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000933 RID: 2355
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class UnknownWrapper
	{
		// Token: 0x06006103 RID: 24835 RVA: 0x0014AC1F File Offset: 0x00148E1F
		[__DynamicallyInvokable]
		public UnknownWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06006104 RID: 24836 RVA: 0x0014AC2E File Offset: 0x00148E2E
		[__DynamicallyInvokable]
		public object WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002AD4 RID: 10964
		private object m_WrappedObject;
	}
}
