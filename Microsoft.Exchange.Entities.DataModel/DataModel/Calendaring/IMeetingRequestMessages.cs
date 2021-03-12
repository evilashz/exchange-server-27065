using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x0200005E RID: 94
	public interface IMeetingRequestMessages : IEntitySet<MeetingRequestMessage>
	{
		// Token: 0x060002F8 RID: 760
		void Respond(RespondToEventParameters parameters, CommandContext context = null);
	}
}
