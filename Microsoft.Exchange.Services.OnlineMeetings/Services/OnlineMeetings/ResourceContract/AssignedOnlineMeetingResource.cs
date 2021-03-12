using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000051 RID: 81
	[Parent("onlineMeetingEligibleValues")]
	[Get(typeof(OnlineMeetingResource))]
	[DataContract(Name = "myAssignedOnlineMeetingResource")]
	internal class AssignedOnlineMeetingResource : OnlineMeetingResource
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x000091CF File Offset: 0x000073CF
		public AssignedOnlineMeetingResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x040001AA RID: 426
		public new const string Token = "myAssignedOnlineMeeting";
	}
}
