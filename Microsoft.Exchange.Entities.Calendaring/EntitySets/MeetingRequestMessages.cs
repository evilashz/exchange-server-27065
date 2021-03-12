using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.Calendaring.EntitySets.MeetingRequestCommands;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x0200005E RID: 94
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MeetingRequestMessages : StorageEntitySet<MeetingRequestMessages, MeetingRequestMessage, IMeetingRequestMessageCommandFactory, IMailboxSession>, IMeetingRequestMessages, IEntitySet<MeetingRequestMessage>
	{
		// Token: 0x06000279 RID: 633 RVA: 0x00009AA1 File Offset: 0x00007CA1
		protected internal MeetingRequestMessages(IStorageEntitySetScope<IMailboxSession> parentScope, IEvents events) : base(parentScope, "MeetingRequestMessages", MeetingRequestMessageCommandFactory.Instance)
		{
			this.Events = events;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00009ABB File Offset: 0x00007CBB
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00009AC3 File Offset: 0x00007CC3
		public IEvents Events { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00009ACC File Offset: 0x00007CCC
		internal virtual MeetingRequestMessageDataProvider MeetingRequestMessageDataProvider
		{
			get
			{
				MeetingRequestMessageDataProvider result;
				if ((result = this.meetingRequestMessageDataProvider) == null)
				{
					result = (this.meetingRequestMessageDataProvider = new MeetingRequestMessageDataProvider(this));
				}
				return result;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009AF4 File Offset: 0x00007CF4
		public void Respond(RespondToEventParameters parameters, CommandContext context = null)
		{
			RespondToMeetingRequestMessage respondToMeetingRequestMessage = base.CommandFactory.CreateRespondCommand(this, parameters);
			respondToMeetingRequestMessage.Execute(context);
		}

		// Token: 0x040000AC RID: 172
		private MeetingRequestMessageDataProvider meetingRequestMessageDataProvider;
	}
}
