using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000072 RID: 114
	[CollectionDataContract(Name = "SchedulingTemplates")]
	internal class SchedulingTemplates : Resource
	{
		// Token: 0x06000336 RID: 822 RVA: 0x000099ED File Offset: 0x00007BED
		public SchedulingTemplates(string selfUri) : base(selfUri)
		{
		}
	}
}
