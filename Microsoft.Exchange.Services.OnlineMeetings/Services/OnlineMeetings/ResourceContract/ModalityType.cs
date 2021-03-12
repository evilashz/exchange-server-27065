using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200007C RID: 124
	[DataContract(Name = "modalityType")]
	internal enum ModalityType
	{
		// Token: 0x04000225 RID: 549
		[EnumMember(Value = "Audio")]
		Audio,
		// Token: 0x04000226 RID: 550
		[EnumMember(Value = "Video")]
		Video,
		// Token: 0x04000227 RID: 551
		[EnumMember(Value = "PhoneAudio")]
		PhoneAudio,
		// Token: 0x04000228 RID: 552
		[EnumMember(Value = "ApplicationSharing")]
		ApplicationSharing,
		// Token: 0x04000229 RID: 553
		[EnumMember(Value = "Messaging")]
		Messaging,
		// Token: 0x0400022A RID: 554
		[EnumMember(Value = "DataCollaboration")]
		DataCollaboration
	}
}
