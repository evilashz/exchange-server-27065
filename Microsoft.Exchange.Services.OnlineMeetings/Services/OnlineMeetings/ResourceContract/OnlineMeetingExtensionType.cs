using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000064 RID: 100
	[DataContract(Name = "OnlineMeetingExtensionType")]
	internal enum OnlineMeetingExtensionType
	{
		// Token: 0x040001D6 RID: 470
		Undefined,
		// Token: 0x040001D7 RID: 471
		RoamedOrganizerData,
		// Token: 0x040001D8 RID: 472
		RoamedParticipantData
	}
}
