using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200059E RID: 1438
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "ApprovalRequestDataType")]
	[Serializable]
	public class ApprovalRequestDataType
	{
		// Token: 0x060028B4 RID: 10420 RVA: 0x000AC7EC File Offset: 0x000AA9EC
		public ApprovalRequestDataType()
		{
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000AC7F4 File Offset: 0x000AA9F4
		internal ApprovalRequestDataType(MessageItem approvalRequest)
		{
			this.IsUndecidedApprovalRequest = approvalRequest.IsValidUndecidedApprovalRequest();
			if (approvalRequest.GetValueAsNullable<int>(MessageItemSchema.ApprovalDecision) != null)
			{
				this.ApprovalDecision = approvalRequest.GetValueAsNullable<int>(MessageItemSchema.ApprovalDecision).Value;
			}
			this.ApprovalDecisionMaker = approvalRequest.GetValueOrDefault<string>(MessageItemSchema.ApprovalDecisionMaker);
			ExDateTime? valueAsNullable = approvalRequest.GetValueAsNullable<ExDateTime>(MessageItemSchema.ApprovalDecisionTime);
			if (valueAsNullable != null)
			{
				this.ApprovalDecisionTime = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(valueAsNullable.Value);
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x000AC879 File Offset: 0x000AAA79
		// (set) Token: 0x060028B7 RID: 10423 RVA: 0x000AC881 File Offset: 0x000AAA81
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public bool IsUndecidedApprovalRequest { get; set; }

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x000AC88A File Offset: 0x000AAA8A
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x000AC892 File Offset: 0x000AAA92
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public int ApprovalDecision { get; set; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x000AC89B File Offset: 0x000AAA9B
		// (set) Token: 0x060028BB RID: 10427 RVA: 0x000AC8A3 File Offset: 0x000AAAA3
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string ApprovalDecisionMaker { get; set; }

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x000AC8AC File Offset: 0x000AAAAC
		// (set) Token: 0x060028BD RID: 10429 RVA: 0x000AC8B4 File Offset: 0x000AAAB4
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string ApprovalDecisionTime { get; set; }
	}
}
