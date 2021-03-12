using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x020000B4 RID: 180
	[DataContract(Name = "Telemetry")]
	internal class Telemetry : Resource
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0000AA72 File Offset: 0x00008C72
		public Telemetry(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000AA7B File Offset: 0x00008C7B
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000AA88 File Offset: 0x00008C88
		[DataMember(Name = "StartTime", EmitDefaultValue = false)]
		public DateTime StartTime
		{
			get
			{
				return base.GetValue<DateTime>("StartTime");
			}
			set
			{
				base.SetValue<DateTime>("StartTime", value);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000AA9B File Offset: 0x00008C9B
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		[DataMember(Name = "EndTime", EmitDefaultValue = false)]
		public DateTime EndTime
		{
			get
			{
				return base.GetValue<DateTime>("EndTime");
			}
			set
			{
				base.SetValue<DateTime>("EndTime", value);
			}
		}
	}
}
