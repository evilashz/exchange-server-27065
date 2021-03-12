using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A33 RID: 2611
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCalendarPublishingRequest : CalendarSharingRequestBase
	{
		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x060049A7 RID: 18855 RVA: 0x00102B07 File Offset: 0x00100D07
		// (set) Token: 0x060049A8 RID: 18856 RVA: 0x00102B0F File Offset: 0x00100D0F
		[DataMember]
		public bool Publish { get; set; }

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x060049A9 RID: 18857 RVA: 0x00102B18 File Offset: 0x00100D18
		// (set) Token: 0x060049AA RID: 18858 RVA: 0x00102B20 File Offset: 0x00100D20
		[DataMember]
		public string DetailLevel { get; set; }

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x060049AB RID: 18859 RVA: 0x00102B29 File Offset: 0x00100D29
		public DetailLevelEnumType DetailLevelEnum
		{
			get
			{
				return this.detailLevelEnum;
			}
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x00102B31 File Offset: 0x00100D31
		internal override void ValidateRequest()
		{
			base.ValidateRequest();
			if (this.Publish && !Enum.TryParse<DetailLevelEnumType>(this.DetailLevel, out this.detailLevelEnum))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
		}

		// Token: 0x04002A10 RID: 10768
		private DetailLevelEnumType detailLevelEnum;
	}
}
