using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003F6 RID: 1014
	[__DynamicallyInvokable]
	public class EventCommandEventArgs : EventArgs
	{
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x000C97DA File Offset: 0x000C79DA
		// (set) Token: 0x060033E8 RID: 13288 RVA: 0x000C97E2 File Offset: 0x000C79E2
		[__DynamicallyInvokable]
		public EventCommand Command { [__DynamicallyInvokable] get; internal set; }

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060033E9 RID: 13289 RVA: 0x000C97EB File Offset: 0x000C79EB
		// (set) Token: 0x060033EA RID: 13290 RVA: 0x000C97F3 File Offset: 0x000C79F3
		[__DynamicallyInvokable]
		public IDictionary<string, string> Arguments { [__DynamicallyInvokable] get; internal set; }

		// Token: 0x060033EB RID: 13291 RVA: 0x000C97FC File Offset: 0x000C79FC
		[__DynamicallyInvokable]
		public bool EnableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, true);
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000C982B File Offset: 0x000C7A2B
		[__DynamicallyInvokable]
		public bool DisableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, false);
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000C985C File Offset: 0x000C7A5C
		internal EventCommandEventArgs(EventCommand command, IDictionary<string, string> arguments, EventSource eventSource, EventListener listener, int perEventSourceSessionId, int etwSessionId, bool enable, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.Command = command;
			this.Arguments = arguments;
			this.eventSource = eventSource;
			this.listener = listener;
			this.perEventSourceSessionId = perEventSourceSessionId;
			this.etwSessionId = etwSessionId;
			this.enable = enable;
			this.level = level;
			this.matchAnyKeyword = matchAnyKeyword;
		}

		// Token: 0x040016C5 RID: 5829
		internal EventSource eventSource;

		// Token: 0x040016C6 RID: 5830
		internal EventDispatcher dispatcher;

		// Token: 0x040016C7 RID: 5831
		internal EventListener listener;

		// Token: 0x040016C8 RID: 5832
		internal int perEventSourceSessionId;

		// Token: 0x040016C9 RID: 5833
		internal int etwSessionId;

		// Token: 0x040016CA RID: 5834
		internal bool enable;

		// Token: 0x040016CB RID: 5835
		internal EventLevel level;

		// Token: 0x040016CC RID: 5836
		internal EventKeywords matchAnyKeyword;

		// Token: 0x040016CD RID: 5837
		internal EventCommandEventArgs nextCommand;
	}
}
