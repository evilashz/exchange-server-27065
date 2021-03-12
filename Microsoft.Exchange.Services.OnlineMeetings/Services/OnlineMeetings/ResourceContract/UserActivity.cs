using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x020000B5 RID: 181
	[DataContract(Name = "UserActivity")]
	internal class UserActivity : Resource
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x0000AABB File Offset: 0x00008CBB
		public UserActivity(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x040002CF RID: 719
		public const string Token = "userActivity";
	}
}
