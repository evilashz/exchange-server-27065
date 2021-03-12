using System;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x0200001B RID: 27
	public class NativeMethodsWrapperFactory
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006274 File Offset: 0x00004474
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000627B File Offset: 0x0000447B
		internal static Func<INativeMethodsWrapper> CreateNativeMethodsWrapperFunc
		{
			get
			{
				return NativeMethodsWrapperFactory.createNativeMethodsWrapperFunc;
			}
			set
			{
				NativeMethodsWrapperFactory.createNativeMethodsWrapperFunc = value;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006283 File Offset: 0x00004483
		internal static INativeMethodsWrapper CreateNativeMethodsWrapper()
		{
			return NativeMethodsWrapperFactory.CreateNativeMethodsWrapperFunc();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000628F File Offset: 0x0000448F
		private static INativeMethodsWrapper CreateRealNativeMethodsWrapper()
		{
			return new NativeMethodsWrapper();
		}

		// Token: 0x0400007F RID: 127
		private static Func<INativeMethodsWrapper> createNativeMethodsWrapperFunc = new Func<INativeMethodsWrapper>(NativeMethodsWrapperFactory.CreateRealNativeMethodsWrapper);
	}
}
