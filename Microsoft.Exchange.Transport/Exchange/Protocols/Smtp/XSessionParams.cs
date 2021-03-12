using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200049B RID: 1179
	internal class XSessionParams
	{
		// Token: 0x06003574 RID: 13684 RVA: 0x000DAC3C File Offset: 0x000D8E3C
		public XSessionParams(Guid mdbGuid, XSessionType sessionType = XSessionType.Undefined)
		{
			ArgumentValidator.ThrowIfInvalidValue<Guid>("mdbGuid", mdbGuid, (Guid x) => x != Guid.Empty);
			this.MdbGuid = mdbGuid;
			this.Type = sessionType;
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000DAC7A File Offset: 0x000D8E7A
		// (set) Token: 0x06003576 RID: 13686 RVA: 0x000DAC82 File Offset: 0x000D8E82
		public Guid MdbGuid { get; private set; }

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x000DAC8B File Offset: 0x000D8E8B
		// (set) Token: 0x06003578 RID: 13688 RVA: 0x000DAC93 File Offset: 0x000D8E93
		public XSessionType Type { get; private set; }
	}
}
