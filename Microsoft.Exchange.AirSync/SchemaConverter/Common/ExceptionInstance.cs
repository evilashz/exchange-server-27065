using System;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000186 RID: 390
	[Serializable]
	internal class ExceptionInstance
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x0005D92B File Offset: 0x0005BB2B
		public ExceptionInstance(ExDateTime exceptionStartTime, byte deleted)
		{
			AirSyncDiagnostics.TraceInfo<ExDateTime, byte>(ExTraceGlobals.CommonTracer, this, "ExceptionInstance Created exceptionStartTime={0} deleted={1}", exceptionStartTime, deleted);
			this.exceptionStartTime = exceptionStartTime;
			this.deleted = deleted;
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0005D953 File Offset: 0x0005BB53
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x0005D95B File Offset: 0x0005BB5B
		public IPropertyContainer ModifiedException
		{
			get
			{
				return this.modifiedException;
			}
			set
			{
				this.modifiedException = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x0005D964 File Offset: 0x0005BB64
		public ExDateTime ExceptionStartTime
		{
			get
			{
				return this.exceptionStartTime;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0005D96C File Offset: 0x0005BB6C
		public byte Deleted
		{
			get
			{
				return this.deleted;
			}
		}

		// Token: 0x04000AE7 RID: 2791
		private byte deleted;

		// Token: 0x04000AE8 RID: 2792
		private ExDateTime exceptionStartTime;

		// Token: 0x04000AE9 RID: 2793
		private IPropertyContainer modifiedException;
	}
}
