using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000039 RID: 57
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CancelEvent : CancelEventBase
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000061BD File Offset: 0x000043BD
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000061C5 File Offset: 0x000043C5
		public int? SeriesSequenceNumber { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000061CE File Offset: 0x000043CE
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.CancelEventTracer;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000061D8 File Offset: 0x000043D8
		protected override VoidResult OnExecute()
		{
			StoreObjectId id = this.Scope.IdConverter.ToStoreObjectId(base.EntityKey);
			this.Scope.EventDataProvider.CancelEvent(id, base.Parameters, this.SeriesSequenceNumber, true, null, null);
			return VoidResult.Value;
		}
	}
}
