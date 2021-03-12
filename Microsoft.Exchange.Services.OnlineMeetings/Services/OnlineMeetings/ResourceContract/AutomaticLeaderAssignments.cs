using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000053 RID: 83
	[CollectionDataContract(Name = "AutomaticLeaderAssignments")]
	internal class AutomaticLeaderAssignments : Resource
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000091E1 File Offset: 0x000073E1
		public AutomaticLeaderAssignments(string selfUri) : base(selfUri)
		{
		}
	}
}
