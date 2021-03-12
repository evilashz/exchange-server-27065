using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000060 RID: 96
	[Post(typeof(OnlineMeetingInput), typeof(OnlineMeetingResource))]
	[Get(typeof(MyOnlineMeetingsResource))]
	[CollectionDataContract(Name = "myOnlineMeetings")]
	internal class MyOnlineMeetingsResource : Resource
	{
		// Token: 0x060002BE RID: 702 RVA: 0x000092F8 File Offset: 0x000074F8
		public MyOnlineMeetingsResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00009301 File Offset: 0x00007501
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000930E File Offset: 0x0000750E
		public ResourceCollection<OnlineMeetingResource> MyOnlineMeetings
		{
			get
			{
				return base.GetValue<ResourceCollection<OnlineMeetingResource>>("myOnlineMeeting");
			}
			set
			{
				base.SetValue<ResourceCollection<OnlineMeetingResource>>("myOnlineMeeting", value);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000931C File Offset: 0x0000751C
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00009329 File Offset: 0x00007529
		public ResourceCollection<AssignedOnlineMeetingResource> AssignedOnlineMeetings
		{
			get
			{
				return base.GetValue<ResourceCollection<AssignedOnlineMeetingResource>>("myAssignedOnlineMeeting");
			}
			set
			{
				base.SetValue<ResourceCollection<AssignedOnlineMeetingResource>>("myAssignedOnlineMeeting", value);
			}
		}

		// Token: 0x040001CC RID: 460
		public const string Token = "myOnlineMeetings";
	}
}
