using System;
using Microsoft.Exchange.Entities.Calendaring.EntitySets.MeetingRequestCommands;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x0200005D RID: 93
	internal sealed class MeetingRequestMessageCommandFactory : IMeetingRequestMessageCommandFactory, IEntityCommandFactory<MeetingRequestMessages, MeetingRequestMessage>
	{
		// Token: 0x06000271 RID: 625 RVA: 0x00009A48 File Offset: 0x00007C48
		private MeetingRequestMessageCommandFactory()
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009A50 File Offset: 0x00007C50
		public RespondToMeetingRequestMessage CreateRespondCommand(MeetingRequestMessages scope, RespondToEventParameters respondToEventParameters)
		{
			return new RespondToMeetingRequestMessage
			{
				Scope = scope,
				Parameters = respondToEventParameters
			};
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009A72 File Offset: 0x00007C72
		public ICreateEntityCommand<MeetingRequestMessages, MeetingRequestMessage> CreateCreateCommand(MeetingRequestMessage entity, MeetingRequestMessages scope)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009A79 File Offset: 0x00007C79
		public IDeleteEntityCommand<MeetingRequestMessages> CreateDeleteCommand(string key, MeetingRequestMessages scope)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009A80 File Offset: 0x00007C80
		public IFindEntitiesCommand<MeetingRequestMessages, MeetingRequestMessage> CreateFindCommand(IEntityQueryOptions queryOptions, MeetingRequestMessages scope)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009A87 File Offset: 0x00007C87
		public IReadEntityCommand<MeetingRequestMessages, MeetingRequestMessage> CreateReadCommand(string key, MeetingRequestMessages scope)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009A8E File Offset: 0x00007C8E
		public IUpdateEntityCommand<MeetingRequestMessages, MeetingRequestMessage> CreateUpdateCommand(string key, MeetingRequestMessage entity, MeetingRequestMessages scope)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000AB RID: 171
		public static readonly IMeetingRequestMessageCommandFactory Instance = new MeetingRequestMessageCommandFactory();
	}
}
