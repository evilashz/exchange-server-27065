using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol
{
	// Token: 0x02000051 RID: 81
	public class WorkPayload : Payload
	{
		// Token: 0x06000229 RID: 553 RVA: 0x0000B258 File Offset: 0x00009458
		static WorkPayload()
		{
			WorkPayload.description.ComplianceStructureId = 4;
			WorkPayload.description.RegisterStringPropertyGetterAndSetter(0, (WorkPayload item) => item.PayloadId, delegate(WorkPayload item, string value)
			{
				item.PayloadId = value;
			});
			WorkPayload.description.RegisterIntegerPropertyGetterAndSetter(0, (WorkPayload item) => EnumConverter.ConvertEnumToInteger<WorkDefinitionType>(item.WorkDefinitionType), delegate(WorkPayload item, int value)
			{
				item.WorkDefinitionType = EnumConverter.ConvertIntegerToEnum<WorkDefinitionType>(value);
			});
			WorkPayload.description.RegisterIntegerPropertyGetterAndSetter(1, (WorkPayload item) => item.PayloadSerializationVersion, delegate(WorkPayload item, int value)
			{
				item.PayloadSerializationVersion = value;
			});
			WorkPayload.description.RegisterBlobPropertyGetterAndSetter(0, (WorkPayload item) => item.WorkDefinition, delegate(WorkPayload item, byte[] value)
			{
				item.WorkDefinition = value;
			});
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000B38E File Offset: 0x0000958E
		public static ComplianceSerializationDescription<WorkPayload> Description
		{
			get
			{
				return WorkPayload.description;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000B395 File Offset: 0x00009595
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000B39D File Offset: 0x0000959D
		public WorkDefinitionType WorkDefinitionType { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000B3A6 File Offset: 0x000095A6
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000B3AE File Offset: 0x000095AE
		public int PayloadSerializationVersion { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000B3B7 File Offset: 0x000095B7
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000B3BF File Offset: 0x000095BF
		public byte[] WorkDefinition { get; set; }

		// Token: 0x06000231 RID: 561 RVA: 0x0000B3C8 File Offset: 0x000095C8
		public static implicit operator byte[](WorkPayload payload)
		{
			return ComplianceSerializer.Serialize<WorkPayload>(WorkPayload.Description, payload);
		}

		// Token: 0x04000195 RID: 405
		private static ComplianceSerializationDescription<WorkPayload> description = new ComplianceSerializationDescription<WorkPayload>();
	}
}
