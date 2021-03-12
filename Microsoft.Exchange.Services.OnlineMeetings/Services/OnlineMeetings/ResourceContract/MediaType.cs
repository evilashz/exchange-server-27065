using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200007D RID: 125
	[DataContract(Name = "MediaType")]
	internal enum MediaType
	{
		// Token: 0x0400022C RID: 556
		[EnumMember(Value = "Audio")]
		Audio,
		// Token: 0x0400022D RID: 557
		[EnumMember(Value = "MainVideo")]
		MainVideo,
		// Token: 0x0400022E RID: 558
		[EnumMember(Value = "PanoramicVideo")]
		PanoramicVideo,
		// Token: 0x0400022F RID: 559
		[EnumMember(Value = "ApplicationSharing")]
		ApplicationSharing,
		// Token: 0x04000230 RID: 560
		[EnumMember(Value = "Chat")]
		Chat,
		// Token: 0x04000231 RID: 561
		[EnumMember(Value = "WhiteBoarding")]
		WhiteBoarding,
		// Token: 0x04000232 RID: 562
		[EnumMember(Value = "PowerPoint")]
		PowerPoint,
		// Token: 0x04000233 RID: 563
		[EnumMember(Value = "FileSharing")]
		FileSharing,
		// Token: 0x04000234 RID: 564
		[EnumMember(Value = "Polling")]
		Polling
	}
}
