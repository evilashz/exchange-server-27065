using System;
using Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands;

namespace Microsoft.Exchange.Entities.Calendaring.MessageProcessing
{
	// Token: 0x0200006F RID: 111
	internal class ProcessSeriesMeetingRequest : ProcessMeetingMessageCommand
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000ADF6 File Offset: 0x00008FF6
		protected override void ProcessMessage()
		{
			if (this.SeriesExists())
			{
				this.ProcessSeriesMeetingUpdate();
				return;
			}
			this.ProcessSeriesMeetingCreation();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000AE0D File Offset: 0x0000900D
		private bool SeriesExists()
		{
			return base.Event.Id != null;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000AE20 File Offset: 0x00009020
		private void ProcessSeriesMeetingUpdate()
		{
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000AE24 File Offset: 0x00009024
		private void ProcessSeriesMeetingCreation()
		{
			base.Event.Occurrences = base.MeetingMessage.OccurrencesExceptionalViewProperties;
			CreateReceivedSeries createReceivedSeries = new CreateReceivedSeries
			{
				Entity = base.Event,
				Scope = this.Scope
			};
			createReceivedSeries.Execute(null);
		}
	}
}
