using System;
using System.Collections.Generic;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol
{
	// Token: 0x02000050 RID: 80
	public class JobPayload : Payload
	{
		// Token: 0x06000212 RID: 530 RVA: 0x0000AFEC File Offset: 0x000091EC
		static JobPayload()
		{
			JobPayload.description.ComplianceStructureId = 5;
			JobPayload.description.RegisterStringPropertyGetterAndSetter(0, (JobPayload item) => item.PayloadId, delegate(JobPayload item, string value)
			{
				item.PayloadId = value;
			});
			JobPayload.description.RegisterStringPropertyGetterAndSetter(1, (JobPayload item) => item.JobId, delegate(JobPayload item, string value)
			{
				item.JobId = value;
			});
			JobPayload.description.RegisterBlobPropertyGetterAndSetter(0, (JobPayload item) => item.Payload, delegate(JobPayload item, byte[] value)
			{
				item.Payload = value;
			});
			JobPayload.description.RegisterComplexPropertyAsBlobGetterAndSetter<Target>(1, (JobPayload item) => item.Target, delegate(JobPayload item, Target value)
			{
				item.Target = value;
			}, Target.Description);
			JobPayload.description.RegisterCollectionPropertyAccessors(0, () => CollectionItemType.Blob, (JobPayload item) => item.Children.Count, (JobPayload item, int index) => item.Children[index], delegate(JobPayload item, object value, int index)
			{
				item.Children.Add((byte[])value);
			});
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000B1A6 File Offset: 0x000093A6
		public static ComplianceSerializationDescription<JobPayload> Description
		{
			get
			{
				return JobPayload.description;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000B1AD File Offset: 0x000093AD
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000B1B5 File Offset: 0x000093B5
		public byte[] Payload { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000B1BE File Offset: 0x000093BE
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000B1C6 File Offset: 0x000093C6
		public Target Target { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000B1CF File Offset: 0x000093CF
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000B1D7 File Offset: 0x000093D7
		public string JobId { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000B1E0 File Offset: 0x000093E0
		public List<byte[]> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000B1E8 File Offset: 0x000093E8
		public static implicit operator byte[](JobPayload payload)
		{
			return ComplianceSerializer.Serialize<JobPayload>(JobPayload.Description, payload);
		}

		// Token: 0x04000184 RID: 388
		private static ComplianceSerializationDescription<JobPayload> description = new ComplianceSerializationDescription<JobPayload>();

		// Token: 0x04000185 RID: 389
		private List<byte[]> children = new List<byte[]>();
	}
}
