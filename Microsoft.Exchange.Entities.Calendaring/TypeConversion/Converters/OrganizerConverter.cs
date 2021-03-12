using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x0200007D RID: 125
	internal class OrganizerConverter : ParticipantConverter<Participant, ParticipantWrapper, Organizer>
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000B5A5 File Offset: 0x000097A5
		public OrganizerConverter(IParticipantRoutingTypeConverter routingTypeConverter) : base(routingTypeConverter)
		{
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000B5AE File Offset: 0x000097AE
		protected override ParticipantWrapper WrapStorageType(Participant value)
		{
			return new ParticipantWrapper(value);
		}
	}
}
