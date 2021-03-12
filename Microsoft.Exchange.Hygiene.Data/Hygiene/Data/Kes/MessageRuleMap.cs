using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Kes
{
	// Token: 0x020001DF RID: 479
	internal class MessageRuleMap : ConfigurablePropertyBag
	{
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00040C74 File Offset: 0x0003EE74
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.FileGuid.ToString());
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x00040C9A File Offset: 0x0003EE9A
		// (set) Token: 0x0600144D RID: 5197 RVA: 0x00040CAC File Offset: 0x0003EEAC
		public Guid FileGuid
		{
			get
			{
				return (Guid)this[MessageRuleMap.FileGuidProperty];
			}
			set
			{
				this[MessageRuleMap.FileGuidProperty] = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x00040CBF File Offset: 0x0003EEBF
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x00040CD1 File Offset: 0x0003EED1
		public long RuleID
		{
			get
			{
				return (long)this[MessageRuleMap.RuleIDProperty];
			}
			set
			{
				this[MessageRuleMap.RuleIDProperty] = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00040CE4 File Offset: 0x0003EEE4
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x00040CF6 File Offset: 0x0003EEF6
		public byte ProcessingGroup
		{
			get
			{
				return (byte)this[MessageRuleMap.ProcessingGroupProperty];
			}
			set
			{
				this[MessageRuleMap.ProcessingGroupProperty] = value;
			}
		}

		// Token: 0x040009F8 RID: 2552
		public static readonly HygienePropertyDefinition FileGuidProperty = new HygienePropertyDefinition("id_FileGuid", typeof(Guid));

		// Token: 0x040009F9 RID: 2553
		public static readonly HygienePropertyDefinition RuleIDProperty = new HygienePropertyDefinition("bi_RuleId", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040009FA RID: 2554
		public static readonly HygienePropertyDefinition ProcessingGroupProperty = new HygienePropertyDefinition("ti_ProcessingGroup", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
