using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000098 RID: 152
	[DataContract(Name = "LockDevice")]
	internal class LockDevice : Resource
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000A606 File Offset: 0x00008806
		public LockDevice(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x040002A5 RID: 677
		public const string Token = "lockDevice";
	}
}
