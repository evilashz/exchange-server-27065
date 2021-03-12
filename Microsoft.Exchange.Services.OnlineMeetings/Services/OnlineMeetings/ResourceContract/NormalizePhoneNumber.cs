using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x020000AF RID: 175
	[Get(typeof(NormalizePhoneNumber))]
	[Parent("user")]
	[DataContract(Name = "NormalizePhoneNumber")]
	internal class NormalizePhoneNumber : Resource
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0000A8DA File Offset: 0x00008ADA
		public NormalizePhoneNumber(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x040002CC RID: 716
		public const string Token = "NormalizePhoneNumber";
	}
}
