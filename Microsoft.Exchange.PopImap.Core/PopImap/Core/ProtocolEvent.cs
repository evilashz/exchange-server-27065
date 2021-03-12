using System;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000025 RID: 37
	internal struct ProtocolEvent
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00007334 File Offset: 0x00005534
		public ProtocolEvent(string id)
		{
			this.id = id;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000733D File Offset: 0x0000553D
		public override string ToString()
		{
			return this.id;
		}

		// Token: 0x04000115 RID: 277
		public static readonly ProtocolEvent Connect = new ProtocolEvent("+");

		// Token: 0x04000116 RID: 278
		public static readonly ProtocolEvent Disconnect = new ProtocolEvent("-");

		// Token: 0x04000117 RID: 279
		public static readonly ProtocolEvent Send = new ProtocolEvent(">");

		// Token: 0x04000118 RID: 280
		public static readonly ProtocolEvent Receive = new ProtocolEvent("<");

		// Token: 0x04000119 RID: 281
		public static readonly ProtocolEvent Information = new ProtocolEvent("*");

		// Token: 0x0400011A RID: 282
		private string id;
	}
}
