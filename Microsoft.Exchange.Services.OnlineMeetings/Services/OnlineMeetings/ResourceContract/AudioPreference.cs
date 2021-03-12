using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200007E RID: 126
	[DataContract(Name = "audioPreference")]
	internal enum AudioPreference
	{
		// Token: 0x04000236 RID: 566
		[EnumMember(Value = "PhoneAudio")]
		PhoneAudio,
		// Token: 0x04000237 RID: 567
		[EnumMember(Value = "VoipAudio")]
		VoipAudio
	}
}
