using System;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x0200004C RID: 76
	internal class MruCacheDiagnosticEntryInfo
	{
		// Token: 0x0600020C RID: 524 RVA: 0x0000A223 File Offset: 0x00008423
		public MruCacheDiagnosticEntryInfo(string identifier, TimeSpan timeToLive)
		{
			this.Identifier = identifier;
			this.TimeToLive = timeToLive;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000A239 File Offset: 0x00008439
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000A241 File Offset: 0x00008441
		public string Identifier { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000A24A File Offset: 0x0000844A
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000A252 File Offset: 0x00008452
		public TimeSpan TimeToLive { get; private set; }
	}
}
