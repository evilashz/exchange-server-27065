using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200006C RID: 108
	public abstract class EnvelopeRecipientCollection : ReadOnlyEnvelopeRecipientCollection
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00006883 File Offset: 0x00004A83
		internal EnvelopeRecipientCollection()
		{
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000232 RID: 562
		public abstract bool CanAdd { get; }

		// Token: 0x06000233 RID: 563
		public abstract void Add(RoutingAddress address);

		// Token: 0x06000234 RID: 564
		public abstract void Clear();

		// Token: 0x06000235 RID: 565
		public abstract bool Remove(EnvelopeRecipient recipient);

		// Token: 0x06000236 RID: 566
		public abstract int Remove(RoutingAddress address);

		// Token: 0x06000237 RID: 567
		public abstract bool Remove(EnvelopeRecipient recipient, DsnType dsnType, SmtpResponse smtpResponse);

		// Token: 0x06000238 RID: 568
		public abstract bool Remove(EnvelopeRecipient recipient, DsnType dsnType, SmtpResponse smtpResponse, string sourceContext);

		// Token: 0x0200006D RID: 109
		public struct Enumerator : IEnumerator<EnvelopeRecipient>, IDisposable, IEnumerator
		{
			// Token: 0x06000239 RID: 569 RVA: 0x0000688B File Offset: 0x00004A8B
			internal Enumerator(IEnumerable items, Converter<object, EnvelopeRecipient> converter)
			{
				this.items = items;
				this.enumerator = items.GetEnumerator();
				this.converter = converter;
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x0600023A RID: 570 RVA: 0x000068A7 File Offset: 0x00004AA7
			object IEnumerator.Current
			{
				get
				{
					return this.converter(this.enumerator.Current);
				}
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x0600023B RID: 571 RVA: 0x000068BF File Offset: 0x00004ABF
			public EnvelopeRecipient Current
			{
				get
				{
					return this.converter(this.enumerator.Current);
				}
			}

			// Token: 0x0600023C RID: 572 RVA: 0x000068D7 File Offset: 0x00004AD7
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x0600023D RID: 573 RVA: 0x000068E4 File Offset: 0x00004AE4
			public void Reset()
			{
				this.enumerator = this.items.GetEnumerator();
			}

			// Token: 0x0600023E RID: 574 RVA: 0x000068F7 File Offset: 0x00004AF7
			public void Dispose()
			{
			}

			// Token: 0x040001C3 RID: 451
			private IEnumerable items;

			// Token: 0x040001C4 RID: 452
			private IEnumerator enumerator;

			// Token: 0x040001C5 RID: 453
			private Converter<object, EnvelopeRecipient> converter;
		}
	}
}
