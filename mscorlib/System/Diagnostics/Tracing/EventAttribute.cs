using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003FA RID: 1018
	[AttributeUsage(AttributeTargets.Method)]
	[__DynamicallyInvokable]
	public sealed class EventAttribute : Attribute
	{
		// Token: 0x0600340F RID: 13327 RVA: 0x000C9BC8 File Offset: 0x000C7DC8
		[__DynamicallyInvokable]
		public EventAttribute(int eventId)
		{
			this.EventId = eventId;
			this.Level = EventLevel.Informational;
			this.m_opcodeSet = false;
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x000C9BE5 File Offset: 0x000C7DE5
		// (set) Token: 0x06003411 RID: 13329 RVA: 0x000C9BED File Offset: 0x000C7DED
		[__DynamicallyInvokable]
		public int EventId { [__DynamicallyInvokable] get; private set; }

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000C9BF6 File Offset: 0x000C7DF6
		// (set) Token: 0x06003413 RID: 13331 RVA: 0x000C9BFE File Offset: 0x000C7DFE
		[__DynamicallyInvokable]
		public EventLevel Level { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06003414 RID: 13332 RVA: 0x000C9C07 File Offset: 0x000C7E07
		// (set) Token: 0x06003415 RID: 13333 RVA: 0x000C9C0F File Offset: 0x000C7E0F
		[__DynamicallyInvokable]
		public EventKeywords Keywords { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x000C9C18 File Offset: 0x000C7E18
		// (set) Token: 0x06003417 RID: 13335 RVA: 0x000C9C20 File Offset: 0x000C7E20
		[__DynamicallyInvokable]
		public EventOpcode Opcode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_opcode;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_opcode = value;
				this.m_opcodeSet = true;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x000C9C30 File Offset: 0x000C7E30
		internal bool IsOpcodeSet
		{
			get
			{
				return this.m_opcodeSet;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06003419 RID: 13337 RVA: 0x000C9C38 File Offset: 0x000C7E38
		// (set) Token: 0x0600341A RID: 13338 RVA: 0x000C9C40 File Offset: 0x000C7E40
		[__DynamicallyInvokable]
		public EventTask Task { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x0600341B RID: 13339 RVA: 0x000C9C49 File Offset: 0x000C7E49
		// (set) Token: 0x0600341C RID: 13340 RVA: 0x000C9C51 File Offset: 0x000C7E51
		[__DynamicallyInvokable]
		public EventChannel Channel { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600341D RID: 13341 RVA: 0x000C9C5A File Offset: 0x000C7E5A
		// (set) Token: 0x0600341E RID: 13342 RVA: 0x000C9C62 File Offset: 0x000C7E62
		[__DynamicallyInvokable]
		public byte Version { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600341F RID: 13343 RVA: 0x000C9C6B File Offset: 0x000C7E6B
		// (set) Token: 0x06003420 RID: 13344 RVA: 0x000C9C73 File Offset: 0x000C7E73
		[__DynamicallyInvokable]
		public string Message { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06003421 RID: 13345 RVA: 0x000C9C7C File Offset: 0x000C7E7C
		// (set) Token: 0x06003422 RID: 13346 RVA: 0x000C9C84 File Offset: 0x000C7E84
		[__DynamicallyInvokable]
		public EventTags Tags { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x000C9C8D File Offset: 0x000C7E8D
		// (set) Token: 0x06003424 RID: 13348 RVA: 0x000C9C95 File Offset: 0x000C7E95
		[__DynamicallyInvokable]
		public EventActivityOptions ActivityOptions { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x040016E7 RID: 5863
		private EventOpcode m_opcode;

		// Token: 0x040016E8 RID: 5864
		private bool m_opcodeSet;
	}
}
