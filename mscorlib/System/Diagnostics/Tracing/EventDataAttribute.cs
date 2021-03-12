using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000414 RID: 1044
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	[__DynamicallyInvokable]
	public class EventDataAttribute : Attribute
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060034DA RID: 13530 RVA: 0x000CD983 File Offset: 0x000CBB83
		// (set) Token: 0x060034DB RID: 13531 RVA: 0x000CD98B File Offset: 0x000CBB8B
		[__DynamicallyInvokable]
		public string Name { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x000CD994 File Offset: 0x000CBB94
		// (set) Token: 0x060034DD RID: 13533 RVA: 0x000CD99C File Offset: 0x000CBB9C
		internal EventLevel Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x000CD9A5 File Offset: 0x000CBBA5
		// (set) Token: 0x060034DF RID: 13535 RVA: 0x000CD9AD File Offset: 0x000CBBAD
		internal EventOpcode Opcode
		{
			get
			{
				return this.opcode;
			}
			set
			{
				this.opcode = value;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x000CD9B6 File Offset: 0x000CBBB6
		// (set) Token: 0x060034E1 RID: 13537 RVA: 0x000CD9BE File Offset: 0x000CBBBE
		internal EventKeywords Keywords { get; set; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x000CD9C7 File Offset: 0x000CBBC7
		// (set) Token: 0x060034E3 RID: 13539 RVA: 0x000CD9CF File Offset: 0x000CBBCF
		internal EventTags Tags { get; set; }

		// Token: 0x060034E4 RID: 13540 RVA: 0x000CD9D8 File Offset: 0x000CBBD8
		[__DynamicallyInvokable]
		public EventDataAttribute()
		{
		}

		// Token: 0x04001762 RID: 5986
		private EventLevel level = (EventLevel)(-1);

		// Token: 0x04001763 RID: 5987
		private EventOpcode opcode = (EventOpcode)(-1);
	}
}
