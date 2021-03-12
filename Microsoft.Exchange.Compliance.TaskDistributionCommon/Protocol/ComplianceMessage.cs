using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol
{
	// Token: 0x02000054 RID: 84
	public class ComplianceMessage
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000B808 File Offset: 0x00009A08
		static ComplianceMessage()
		{
			ComplianceMessage.description.ComplianceStructureId = 1;
			ComplianceMessage.description.RegisterBytePropertyGetterAndSetter(0, (ComplianceMessage item) => (byte)item.ComplianceMessageType, delegate(ComplianceMessage item, byte value)
			{
				item.ComplianceMessageType = (ComplianceMessageType)value;
			});
			ComplianceMessage.description.RegisterIntegerPropertyGetterAndSetter(0, (ComplianceMessage item) => (int)item.WorkDefinitionType, delegate(ComplianceMessage item, int value)
			{
				item.WorkDefinitionType = (WorkDefinitionType)value;
			});
			ComplianceMessage.description.RegisterGuidPropertyGetterAndSetter(0, (ComplianceMessage item) => item.CorrelationId, delegate(ComplianceMessage item, Guid value)
			{
				item.CorrelationId = value;
			});
			ComplianceMessage.description.RegisterStringPropertyGetterAndSetter(0, (ComplianceMessage item) => item.MessageId, delegate(ComplianceMessage item, string value)
			{
				item.MessageId = value;
			});
			ComplianceMessage.description.RegisterStringPropertyGetterAndSetter(1, (ComplianceMessage item) => item.MessageSourceId, delegate(ComplianceMessage item, string value)
			{
				item.MessageSourceId = value;
			});
			ComplianceMessage.description.RegisterStringPropertyGetterAndSetter(2, (ComplianceMessage item) => item.Culture, delegate(ComplianceMessage item, string value)
			{
				item.Culture = value;
			});
			ComplianceMessage.description.RegisterComplexPropertyAsBlobGetterAndSetter<Target>(0, (ComplianceMessage item) => item.MessageTarget, delegate(ComplianceMessage item, Target value)
			{
				item.MessageTarget = value;
			}, Target.Description);
			ComplianceMessage.description.RegisterComplexPropertyAsBlobGetterAndSetter<Target>(1, (ComplianceMessage item) => item.MessageSource, delegate(ComplianceMessage item, Target value)
			{
				item.MessageSource = value;
			}, Target.Description);
			ComplianceMessage.description.RegisterBlobPropertyGetterAndSetter(2, (ComplianceMessage item) => item.Payload, delegate(ComplianceMessage item, byte[] value)
			{
				item.Payload = value;
			});
			ComplianceMessage.description.RegisterBlobPropertyGetterAndSetter(3, (ComplianceMessage item) => item.TenantId, delegate(ComplianceMessage item, byte[] value)
			{
				item.TenantId = value;
			});
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000BAE6 File Offset: 0x00009CE6
		public static ComplianceSerializationDescription<ComplianceMessage> Description
		{
			get
			{
				return ComplianceMessage.description;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000BAED File Offset: 0x00009CED
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000BAF5 File Offset: 0x00009CF5
		public ComplianceMessageType ComplianceMessageType { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000BAFE File Offset: 0x00009CFE
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000BB06 File Offset: 0x00009D06
		public WorkDefinitionType WorkDefinitionType { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000BB0F File Offset: 0x00009D0F
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000BB17 File Offset: 0x00009D17
		public Guid CorrelationId { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000BB20 File Offset: 0x00009D20
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000BB28 File Offset: 0x00009D28
		public string MessageId { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000BB31 File Offset: 0x00009D31
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000BB39 File Offset: 0x00009D39
		public string MessageSourceId { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000BB42 File Offset: 0x00009D42
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000BB4A File Offset: 0x00009D4A
		public byte[] TenantId { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000BB53 File Offset: 0x00009D53
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000BB5B File Offset: 0x00009D5B
		public Target MessageTarget { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000BB64 File Offset: 0x00009D64
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000BB6C File Offset: 0x00009D6C
		public Target MessageSource { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000BB75 File Offset: 0x00009D75
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000BB7D File Offset: 0x00009D7D
		public string Culture { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000BB86 File Offset: 0x00009D86
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000BB8E File Offset: 0x00009D8E
		public byte[] Payload { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000BB97 File Offset: 0x00009D97
		internal ProtocolContext ProtocolContext
		{
			get
			{
				return this.protocolContext;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		public ComplianceMessage Clone()
		{
			ComplianceMessage complianceMessage = (ComplianceMessage)base.MemberwiseClone();
			complianceMessage.protocolContext = new ProtocolContext();
			return complianceMessage;
		}

		// Token: 0x040001B7 RID: 439
		private static ComplianceSerializationDescription<ComplianceMessage> description = new ComplianceSerializationDescription<ComplianceMessage>();

		// Token: 0x040001B8 RID: 440
		private ProtocolContext protocolContext = new ProtocolContext();
	}
}
