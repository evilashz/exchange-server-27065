using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200005F RID: 95
	[CollectionDataContract(Name = "LobbyBypassSettings")]
	internal class LobbyBypassSettings : Resource
	{
		// Token: 0x060002BD RID: 701 RVA: 0x000092EF File Offset: 0x000074EF
		public LobbyBypassSettings(string selfUri) : base(selfUri)
		{
		}
	}
}
