using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol
{
	// Token: 0x0200004C RID: 76
	public class Target
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		static Target()
		{
			Target.description.ComplianceStructureId = 2;
			Target.description.RegisterIntegerPropertyGetterAndSetter(0, (Target item) => EnumConverter.ConvertEnumToInteger<Target.Type>(item.TargetType), delegate(Target item, int value)
			{
				item.TargetType = EnumConverter.ConvertIntegerToEnum<Target.Type>(value);
			});
			Target.description.RegisterStringPropertyGetterAndSetter(0, (Target item) => item.Identifier, delegate(Target item, string value)
			{
				item.Identifier = value;
			});
			Target.description.RegisterStringPropertyGetterAndSetter(1, (Target item) => item.Folder, delegate(Target item, string value)
			{
				item.Folder = value;
			});
			Target.description.RegisterGuidPropertyGetterAndSetter(0, (Target item) => item.Database, delegate(Target item, Guid value)
			{
				item.Database = value;
			});
			Target.description.RegisterGuidPropertyGetterAndSetter(1, (Target item) => item.Mailbox, delegate(Target item, Guid value)
			{
				item.Mailbox = value;
			});
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000AD43 File Offset: 0x00008F43
		public static ComplianceSerializationDescription<Target> Description
		{
			get
			{
				return Target.description;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000AD4A File Offset: 0x00008F4A
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000AD52 File Offset: 0x00008F52
		public Target.Type TargetType { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000AD5B File Offset: 0x00008F5B
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000AD63 File Offset: 0x00008F63
		public string Identifier { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000AD6C File Offset: 0x00008F6C
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000AD74 File Offset: 0x00008F74
		public string Server { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000AD7D File Offset: 0x00008F7D
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000AD85 File Offset: 0x00008F85
		public Guid Database { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000AD8E File Offset: 0x00008F8E
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000AD96 File Offset: 0x00008F96
		public Guid Mailbox { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000AD9F File Offset: 0x00008F9F
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000ADA7 File Offset: 0x00008FA7
		public string Folder { get; set; }

		// Token: 0x060001F2 RID: 498 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		public Target Clone()
		{
			return (Target)base.MemberwiseClone();
		}

		// Token: 0x04000161 RID: 353
		private static ComplianceSerializationDescription<Target> description = new ComplianceSerializationDescription<Target>();

		// Token: 0x0200004D RID: 77
		public enum Type
		{
			// Token: 0x04000173 RID: 371
			MailboxSmtpAddress,
			// Token: 0x04000174 RID: 372
			MailboxGuid,
			// Token: 0x04000175 RID: 373
			QueryFilter,
			// Token: 0x04000176 RID: 374
			InactiveMailboxes,
			// Token: 0x04000177 RID: 375
			TenantMaster,
			// Token: 0x04000178 RID: 376
			Driver = 99
		}
	}
}
