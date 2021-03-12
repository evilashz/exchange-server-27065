using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000050 RID: 80
	[Get(typeof(OnlineMeetingResource))]
	[DataContract(Name = "onlineMeetingResource")]
	[Put(typeof(OnlineMeetingResource), typeof(OnlineMeetingResource))]
	[Parent("onlineMeetingEligibleValues")]
	[Delete]
	internal class OnlineMeetingResource : MeetingResource, IEtagProvider
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x000091B5 File Offset: 0x000073B5
		public OnlineMeetingResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x000091BE File Offset: 0x000073BE
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x000091C6 File Offset: 0x000073C6
		public string ETag { get; set; }

		// Token: 0x040001A8 RID: 424
		public const string Token = "myOnlineMeeting";
	}
}
