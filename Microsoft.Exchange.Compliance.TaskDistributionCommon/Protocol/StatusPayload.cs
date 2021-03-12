using System;
using System.Collections.Generic;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol
{
	// Token: 0x02000052 RID: 82
	internal class StatusPayload : Payload
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000B420 File Offset: 0x00009620
		static StatusPayload()
		{
			StatusPayload.description.ComplianceStructureId = 7;
			StatusPayload.description.RegisterStringPropertyGetterAndSetter(0, (StatusPayload item) => item.PayloadId, delegate(StatusPayload item, string value)
			{
				item.PayloadId = value;
			});
			StatusPayload.description.RegisterCollectionPropertyAccessors(0, () => CollectionItemType.String, (StatusPayload item) => item.QueuedMessages.Count, (StatusPayload item, int index) => item.QueuedMessages[index], delegate(StatusPayload item, object value, int index)
			{
				item.QueuedMessages.Add((string)value);
			});
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000B506 File Offset: 0x00009706
		public static ComplianceSerializationDescription<StatusPayload> Description
		{
			get
			{
				return StatusPayload.description;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000B50D File Offset: 0x0000970D
		public List<string> QueuedMessages
		{
			get
			{
				return this.queuedMessages;
			}
		}

		// Token: 0x040001A1 RID: 417
		private static ComplianceSerializationDescription<StatusPayload> description = new ComplianceSerializationDescription<StatusPayload>();

		// Token: 0x040001A2 RID: 418
		private List<string> queuedMessages = new List<string>();
	}
}
