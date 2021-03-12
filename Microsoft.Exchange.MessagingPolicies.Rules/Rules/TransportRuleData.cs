using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000034 RID: 52
	internal sealed class TransportRuleData
	{
		// Token: 0x060001CE RID: 462 RVA: 0x00009864 File Offset: 0x00007A64
		public TransportRuleData(TransportRule rule)
		{
			this.xml = rule.Xml;
			this.priority = rule.Priority;
			this.name = rule.Name;
			this.ImmutableId = rule.ImmutableId;
			this.whenChangedUTC = rule.WhenChangedUTCCopy;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000098B3 File Offset: 0x00007AB3
		public string Xml
		{
			get
			{
				return this.xml;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000098BB File Offset: 0x00007ABB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000098C3 File Offset: 0x00007AC3
		public int Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000098CB File Offset: 0x00007ACB
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x000098D3 File Offset: 0x00007AD3
		public DateTime? WhenChangedUTC
		{
			get
			{
				return this.whenChangedUTC;
			}
			set
			{
				this.whenChangedUTC = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000098DC File Offset: 0x00007ADC
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x000098E4 File Offset: 0x00007AE4
		public Guid ImmutableId { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000098F0 File Offset: 0x00007AF0
		public long ItemSize
		{
			get
			{
				long num = 0L;
				num += 4L;
				num += 8L;
				num += 16L;
				if (this.xml != null)
				{
					num += (long)(this.xml.Length * 2);
				}
				if (this.name != null)
				{
					num += (long)(this.name.Length * 2);
				}
				return num + 36L;
			}
		}

		// Token: 0x04000169 RID: 361
		internal const int FixedObjectOverhead = 18;

		// Token: 0x0400016A RID: 362
		private string xml;

		// Token: 0x0400016B RID: 363
		private string name;

		// Token: 0x0400016C RID: 364
		private int priority;

		// Token: 0x0400016D RID: 365
		private DateTime? whenChangedUTC;
	}
}
