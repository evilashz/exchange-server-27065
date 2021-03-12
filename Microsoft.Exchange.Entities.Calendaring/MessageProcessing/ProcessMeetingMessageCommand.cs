using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.EntitySets;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.MessageProcessing
{
	// Token: 0x0200006E RID: 110
	internal abstract class ProcessMeetingMessageCommand : EntityCommand<Events, VoidResult>
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		public MeetingMessage MeetingMessage { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000ADC9 File Offset: 0x00008FC9
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000ADD1 File Offset: 0x00008FD1
		public Event Event { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000ADDA File Offset: 0x00008FDA
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.MeetingMessageProcessingTracer;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000ADE1 File Offset: 0x00008FE1
		protected sealed override VoidResult OnExecute()
		{
			this.ProcessMessage();
			return VoidResult.Value;
		}

		// Token: 0x060002E7 RID: 743
		protected abstract void ProcessMessage();
	}
}
