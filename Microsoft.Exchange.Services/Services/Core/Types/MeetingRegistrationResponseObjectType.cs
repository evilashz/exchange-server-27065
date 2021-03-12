using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005F4 RID: 1524
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MeetingRegistrationResponseObjectType : WellKnownResponseObjectType
	{
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000B3B6C File Offset: 0x000B1D6C
		// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x000B3B74 File Offset: 0x000B1D74
		[DateTimeString]
		[DataMember(EmitDefaultValue = false)]
		public string ProposedStart { get; set; }

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000B3B7D File Offset: 0x000B1D7D
		// (set) Token: 0x06002EF6 RID: 12022 RVA: 0x000B3B85 File Offset: 0x000B1D85
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string ProposedEnd { get; set; }

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x000B3B8E File Offset: 0x000B1D8E
		public bool HasTimeProposal
		{
			get
			{
				return this.ProposedStart != null && this.ProposedEnd != null;
			}
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000B3BA6 File Offset: 0x000B1DA6
		public bool HasStartAndEndProposedTimeOrNone()
		{
			return !(this.ProposedStart == null ^ this.ProposedEnd == null);
		}
	}
}
