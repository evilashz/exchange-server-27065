using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200006D RID: 109
	[DataContract(Name = "PhoneInformationResource")]
	[Parent("user")]
	[Get(typeof(PhoneInformationResource))]
	internal class PhoneInformationResource : Resource
	{
		// Token: 0x06000319 RID: 793 RVA: 0x00009878 File Offset: 0x00007A78
		public PhoneInformationResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x040001F7 RID: 503
		public const string Token = "phoneInformation";
	}
}
