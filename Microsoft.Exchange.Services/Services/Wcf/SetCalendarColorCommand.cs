using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000969 RID: 2409
	internal sealed class SetCalendarColorCommand : ServiceCommand<CalendarActionItemIdResponse>
	{
		// Token: 0x0600453F RID: 17727 RVA: 0x000F2484 File Offset: 0x000F0684
		public SetCalendarColorCommand(CallContext callContext, Microsoft.Exchange.Services.Core.Types.ItemId itemId, CalendarColor calendarColor) : base(callContext)
		{
			this.itemId = itemId;
			this.calendarColor = calendarColor;
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x000F249C File Offset: 0x000F069C
		protected override CalendarActionItemIdResponse InternalExecute()
		{
			if (this.itemId == null || string.IsNullOrEmpty(this.itemId.Id) || string.IsNullOrEmpty(this.itemId.ChangeKey))
			{
				ExTraceGlobals.SetCalendarColorCallTracer.TraceError<string, string>((long)this.GetHashCode(), "Invalid calendar folderid supplied. ItemId.Id: {0}, ItemId.ChangeKey: {1}", (this.itemId == null || this.itemId.Id == null) ? "is null" : this.itemId.Id, (this.itemId == null || this.itemId.ChangeKey == null) ? "is null" : this.itemId.ChangeKey);
				return new CalendarActionItemIdResponse(CalendarActionError.CalendarActionInvalidItemId);
			}
			return new SetCalendarColor(base.MailboxIdentityMailboxSession, base.IdConverter.ConvertItemIdToIdAndSessionReadWrite(this.itemId).Id, this.calendarColor).Execute();
		}

		// Token: 0x0400284E RID: 10318
		private readonly Microsoft.Exchange.Services.Core.Types.ItemId itemId;

		// Token: 0x0400284F RID: 10319
		private readonly CalendarColor calendarColor;
	}
}
