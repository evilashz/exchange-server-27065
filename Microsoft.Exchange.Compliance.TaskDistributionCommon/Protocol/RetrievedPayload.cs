using System;
using System.Collections.Generic;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol
{
	// Token: 0x02000053 RID: 83
	public class RetrievedPayload : Payload
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000B59C File Offset: 0x0000979C
		static RetrievedPayload()
		{
			RetrievedPayload.description.ComplianceStructureId = 5;
			RetrievedPayload.description.RegisterStringPropertyGetterAndSetter(0, (RetrievedPayload item) => item.PayloadId, delegate(RetrievedPayload item, string value)
			{
				item.PayloadId = value;
			});
			RetrievedPayload.description.RegisterStringPropertyGetterAndSetter(1, (RetrievedPayload item) => item.Bookmark, delegate(RetrievedPayload item, string value)
			{
				item.Bookmark = value;
			});
			RetrievedPayload.description.RegisterBytePropertyGetterAndSetter(0, (RetrievedPayload item) => item.IsComplete ? 1 : 0, delegate(RetrievedPayload item, byte value)
			{
				item.IsComplete = (value == 1);
			});
			RetrievedPayload.description.RegisterCollectionPropertyAccessors(0, () => CollectionItemType.Blob, (RetrievedPayload item) => item.Children.Count, (RetrievedPayload item, int index) => item.Children[index], delegate(RetrievedPayload item, object value, int index)
			{
				item.Children.Add((byte[])value);
			});
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000B70C File Offset: 0x0000990C
		public static ComplianceSerializationDescription<RetrievedPayload> Description
		{
			get
			{
				return RetrievedPayload.description;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000B713 File Offset: 0x00009913
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000B71B File Offset: 0x0000991B
		public bool IsComplete { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000B724 File Offset: 0x00009924
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000B72C File Offset: 0x0000992C
		public string Bookmark { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000B735 File Offset: 0x00009935
		public List<byte[]> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B73D File Offset: 0x0000993D
		public static implicit operator byte[](RetrievedPayload payload)
		{
			return ComplianceSerializer.Serialize<RetrievedPayload>(RetrievedPayload.Description, payload);
		}

		// Token: 0x040001A9 RID: 425
		private static ComplianceSerializationDescription<RetrievedPayload> description = new ComplianceSerializationDescription<RetrievedPayload>();

		// Token: 0x040001AA RID: 426
		private List<byte[]> children = new List<byte[]>();
	}
}
