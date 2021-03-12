using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x0200030E RID: 782
	public class StorageAgentState : ICloneableInternal
	{
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x000806EF File Offset: 0x0007E8EF
		// (set) Token: 0x060021FE RID: 8702 RVA: 0x000806F7 File Offset: 0x0007E8F7
		internal Agent AssociatedAgent
		{
			get
			{
				return this.associatedAgent;
			}
			set
			{
				this.associatedAgent = value;
			}
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x00080708 File Offset: 0x0007E908
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x040011CA RID: 4554
		private Agent associatedAgent;
	}
}
