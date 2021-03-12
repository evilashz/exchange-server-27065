using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol
{
	// Token: 0x0200004F RID: 79
	public class PayloadReference
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000AE34 File Offset: 0x00009034
		static PayloadReference()
		{
			PayloadReference.description.ComplianceStructureId = 3;
			PayloadReference.description.RegisterIntegerPropertyGetterAndSetter(0, (PayloadReference item) => item.Count, delegate(PayloadReference item, int value)
			{
				item.Count = value;
			});
			PayloadReference.description.RegisterStringPropertyGetterAndSetter(0, (PayloadReference item) => item.PayloadId, delegate(PayloadReference item, string value)
			{
				item.PayloadId = value;
			});
			PayloadReference.description.RegisterStringPropertyGetterAndSetter(1, (PayloadReference item) => item.Bookmark, delegate(PayloadReference item, string value)
			{
				item.Bookmark = value;
			});
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000AF25 File Offset: 0x00009125
		public static ComplianceSerializationDescription<PayloadReference> Description
		{
			get
			{
				return PayloadReference.description;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000AF2C File Offset: 0x0000912C
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000AF34 File Offset: 0x00009134
		public int Count { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000AF3D File Offset: 0x0000913D
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000AF45 File Offset: 0x00009145
		public string Bookmark { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000AF4E File Offset: 0x0000914E
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000AF56 File Offset: 0x00009156
		public string PayloadId { get; set; }

		// Token: 0x0600020A RID: 522 RVA: 0x0000AF5F File Offset: 0x0000915F
		public static implicit operator byte[](PayloadReference payload)
		{
			return ComplianceSerializer.Serialize<PayloadReference>(PayloadReference.Description, payload);
		}

		// Token: 0x0400017A RID: 378
		private static ComplianceSerializationDescription<PayloadReference> description = new ComplianceSerializationDescription<PayloadReference>();
	}
}
