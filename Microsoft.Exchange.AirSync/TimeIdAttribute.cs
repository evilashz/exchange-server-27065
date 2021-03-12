using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000293 RID: 659
	[AttributeUsage(AttributeTargets.Field)]
	internal class TimeIdAttribute : Attribute
	{
		// Token: 0x06001836 RID: 6198 RVA: 0x0008E020 File Offset: 0x0008C220
		public TimeIdAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0008E02F File Offset: 0x0008C22F
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x0008E037 File Offset: 0x0008C237
		public string Name { get; set; }
	}
}
