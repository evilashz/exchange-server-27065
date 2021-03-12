using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000B48 RID: 2888
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "UnseenData")]
	[Serializable]
	public class UnseenDataType
	{
		// Token: 0x060051DE RID: 20958 RVA: 0x0010B035 File Offset: 0x00109235
		public UnseenDataType()
		{
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x0010B03D File Offset: 0x0010923D
		public UnseenDataType(int unseenCount, string lastVisitedTimeString)
		{
			this.UnseenCount = unseenCount;
			this.LastVisitedTime = lastVisitedTimeString;
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x060051E0 RID: 20960 RVA: 0x0010B053 File Offset: 0x00109253
		// (set) Token: 0x060051E1 RID: 20961 RVA: 0x0010B05B File Offset: 0x0010925B
		[DataMember(Name = "UnseenCount", IsRequired = true)]
		public int UnseenCount { get; set; }

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x060051E2 RID: 20962 RVA: 0x0010B064 File Offset: 0x00109264
		// (set) Token: 0x060051E3 RID: 20963 RVA: 0x0010B06C File Offset: 0x0010926C
		[DateTimeString]
		[DataMember(IsRequired = true)]
		public string LastVisitedTime { get; set; }
	}
}
