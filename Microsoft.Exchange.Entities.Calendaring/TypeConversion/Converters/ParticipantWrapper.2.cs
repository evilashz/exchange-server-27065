using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x02000080 RID: 128
	internal class ParticipantWrapper : ParticipantWrapper<Participant>
	{
		// Token: 0x0600032C RID: 812 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		public ParticipantWrapper(Participant participant) : base(participant)
		{
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000B8CD File Offset: 0x00009ACD
		public override Participant Participant
		{
			get
			{
				return base.Original;
			}
		}
	}
}
