using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002D8 RID: 728
	internal sealed class JournalRuleData
	{
		// Token: 0x06002046 RID: 8262 RVA: 0x0007BAB0 File Offset: 0x00079CB0
		public JournalRuleData(TransportRule rule)
		{
			this.immutableId = rule.ImmutableId;
			this.xml = rule.Xml;
			this.priority = rule.Priority;
			this.name = rule.Name;
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x0007BAE8 File Offset: 0x00079CE8
		public Guid ImmutableId
		{
			get
			{
				return this.immutableId;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x0007BAF0 File Offset: 0x00079CF0
		public string Xml
		{
			get
			{
				return this.xml;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x0007BAF8 File Offset: 0x00079CF8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0007BB00 File Offset: 0x00079D00
		public int Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x0007BB08 File Offset: 0x00079D08
		public long ItemSize
		{
			get
			{
				long num = 0L;
				num += 4L;
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

		// Token: 0x040010E3 RID: 4323
		internal const int FixedObjectOverhead = 18;

		// Token: 0x040010E4 RID: 4324
		private readonly Guid immutableId;

		// Token: 0x040010E5 RID: 4325
		private string xml;

		// Token: 0x040010E6 RID: 4326
		private string name;

		// Token: 0x040010E7 RID: 4327
		private int priority;
	}
}
