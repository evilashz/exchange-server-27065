using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000055 RID: 85
	[CollectionDataContract(Name = "ConferenceAccessLevels")]
	internal class ConferenceAccessLevels : Resource
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00009217 File Offset: 0x00007417
		public ConferenceAccessLevels(string selfUri) : base(selfUri)
		{
		}
	}
}
