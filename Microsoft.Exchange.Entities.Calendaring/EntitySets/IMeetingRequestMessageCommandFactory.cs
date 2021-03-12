using System;
using Microsoft.Exchange.Entities.Calendaring.EntitySets.MeetingRequestCommands;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x0200005A RID: 90
	internal interface IMeetingRequestMessageCommandFactory : IEntityCommandFactory<MeetingRequestMessages, MeetingRequestMessage>
	{
		// Token: 0x06000266 RID: 614
		RespondToMeetingRequestMessage CreateRespondCommand(MeetingRequestMessages scope, RespondToEventParameters parameters);
	}
}
