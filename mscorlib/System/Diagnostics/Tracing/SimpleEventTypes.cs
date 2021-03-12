using System;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000424 RID: 1060
	internal class SimpleEventTypes<T> : TraceLoggingEventTypes
	{
		// Token: 0x0600353E RID: 13630 RVA: 0x000CE7FE File Offset: 0x000CC9FE
		private SimpleEventTypes(TraceLoggingTypeInfo<T> typeInfo) : base(typeInfo.Name, typeInfo.Tags, new TraceLoggingTypeInfo[]
		{
			typeInfo
		})
		{
			this.typeInfo = typeInfo;
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000CE823 File Offset: 0x000CCA23
		public static SimpleEventTypes<T> Instance
		{
			get
			{
				return SimpleEventTypes<T>.instance ?? SimpleEventTypes<T>.InitInstance();
			}
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000CE834 File Offset: 0x000CCA34
		private static SimpleEventTypes<T> InitInstance()
		{
			SimpleEventTypes<T> value = new SimpleEventTypes<T>(TraceLoggingTypeInfo<T>.Instance);
			Interlocked.CompareExchange<SimpleEventTypes<T>>(ref SimpleEventTypes<T>.instance, value, null);
			return SimpleEventTypes<T>.instance;
		}

		// Token: 0x0400179F RID: 6047
		private static SimpleEventTypes<T> instance;

		// Token: 0x040017A0 RID: 6048
		internal readonly TraceLoggingTypeInfo<T> typeInfo;
	}
}
