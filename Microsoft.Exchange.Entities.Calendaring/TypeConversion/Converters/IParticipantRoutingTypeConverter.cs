using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x0200007A RID: 122
	internal interface IParticipantRoutingTypeConverter
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000316 RID: 790
		ConvertValue<Participant[], Participant[]> ConvertToEntity { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000317 RID: 791
		ConvertValue<Participant[], Participant[]> ConvertToStorage { get; }
	}
}
