using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000977 RID: 2423
	internal sealed class RemoveBirthdayEventCommand : ServiceCommand<CalendarActionResponse>
	{
		// Token: 0x06004588 RID: 17800 RVA: 0x000F41D3 File Offset: 0x000F23D3
		public RemoveBirthdayEventCommand(CallContext callContext, Microsoft.Exchange.Services.Core.Types.ItemId contactId) : base(callContext)
		{
			this.contactId = contactId;
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x000F41E3 File Offset: 0x000F23E3
		protected override CalendarActionResponse InternalExecute()
		{
			return new CalendarActionResponse();
		}

		// Token: 0x0400286D RID: 10349
		private Microsoft.Exchange.Services.Core.Types.ItemId contactId;
	}
}
