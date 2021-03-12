using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200006B RID: 107
	public abstract class ReadOnlyEnvelopeRecipientCollection : IEnumerable<EnvelopeRecipient>, IEnumerable
	{
		// Token: 0x0600022A RID: 554 RVA: 0x00006861 File Offset: 0x00004A61
		internal ReadOnlyEnvelopeRecipientCollection()
		{
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600022B RID: 555
		public abstract int Count { get; }

		// Token: 0x1700008F RID: 143
		public abstract EnvelopeRecipient this[int index]
		{
			get;
		}

		// Token: 0x0600022D RID: 557
		public abstract bool Contains(RoutingAddress address);

		// Token: 0x0600022E RID: 558 RVA: 0x00006869 File Offset: 0x00004A69
		IEnumerator<EnvelopeRecipient> IEnumerable<EnvelopeRecipient>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006876 File Offset: 0x00004A76
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000230 RID: 560
		public abstract EnvelopeRecipientCollection.Enumerator GetEnumerator();
	}
}
