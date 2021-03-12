using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Logging
{
	// Token: 0x02000768 RID: 1896
	internal struct ProtocolEvent
	{
		// Token: 0x0600256C RID: 9580 RVA: 0x0004E9F4 File Offset: 0x0004CBF4
		public ProtocolEvent(string id)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			this.id = id;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x0004EA08 File Offset: 0x0004CC08
		public override string ToString()
		{
			return this.id;
		}

		// Token: 0x040022C3 RID: 8899
		public static readonly ProtocolEvent Connect = new ProtocolEvent("+");

		// Token: 0x040022C4 RID: 8900
		public static readonly ProtocolEvent Disconnect = new ProtocolEvent("-");

		// Token: 0x040022C5 RID: 8901
		public static readonly ProtocolEvent Send = new ProtocolEvent(">");

		// Token: 0x040022C6 RID: 8902
		public static readonly ProtocolEvent Receive = new ProtocolEvent("<");

		// Token: 0x040022C7 RID: 8903
		public static readonly ProtocolEvent Information = new ProtocolEvent("*");

		// Token: 0x040022C8 RID: 8904
		private string id;
	}
}
