using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000FE RID: 254
	public class EmailRecipientCollection : IEnumerable<EmailRecipient>, IEnumerable
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x0001B3E4 File Offset: 0x000195E4
		internal EmailRecipientCollection(MessageImplementation message, RecipientType recipientType) : this(message, recipientType, false)
		{
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001B3EF File Offset: 0x000195EF
		internal EmailRecipientCollection(MessageImplementation message, RecipientType recipientType, bool isReadOnly)
		{
			this.message = message;
			this.recipientType = recipientType;
			this.isReadOnly = isReadOnly;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001B417 File Offset: 0x00019617
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001B424 File Offset: 0x00019624
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0001B42C File Offset: 0x0001962C
		internal object Cache
		{
			get
			{
				return this.cache;
			}
			set
			{
				this.cache = value;
			}
		}

		// Token: 0x17000209 RID: 521
		public EmailRecipient this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001B444 File Offset: 0x00019644
		public void Add(EmailRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (this.isReadOnly)
			{
				throw new NotSupportedException(EmailMessageStrings.CollectionIsReadOnly);
			}
			if (recipient.MimeRecipient.Parent != null || recipient.TnefRecipient.TnefMessage != null)
			{
				throw new ArgumentException(EmailMessageStrings.RecipientAlreadyHasParent, "recipient");
			}
			this.message.AddRecipient(this.recipientType, ref this.cache, recipient);
			this.list.Add(recipient);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001B4C0 File Offset: 0x000196C0
		public void Clear()
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException(EmailMessageStrings.CollectionIsReadOnly);
			}
			this.message.ClearRecipients(this.recipientType, ref this.cache);
			this.list.Clear();
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001B4F7 File Offset: 0x000196F7
		IEnumerator<EmailRecipient> IEnumerable<EmailRecipient>.GetEnumerator()
		{
			return new EmailRecipientCollection.Enumerator(this);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001B504 File Offset: 0x00019704
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new EmailRecipientCollection.Enumerator(this);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001B511 File Offset: 0x00019711
		public EmailRecipientCollection.Enumerator GetEnumerator()
		{
			return new EmailRecipientCollection.Enumerator(this);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001B51C File Offset: 0x0001971C
		public bool Remove(EmailRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (this.isReadOnly)
			{
				throw new NotSupportedException(EmailMessageStrings.CollectionIsReadOnly);
			}
			if (!this.list.Contains(recipient))
			{
				return false;
			}
			this.message.RemoveRecipient(this.recipientType, ref this.cache, recipient);
			return this.list.Remove(recipient);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001B57E File Offset: 0x0001977E
		internal void InternalAdd(EmailRecipient recipient)
		{
			this.list.Add(recipient);
		}

		// Token: 0x04000435 RID: 1077
		private MessageImplementation message;

		// Token: 0x04000436 RID: 1078
		private List<EmailRecipient> list = new List<EmailRecipient>();

		// Token: 0x04000437 RID: 1079
		private RecipientType recipientType;

		// Token: 0x04000438 RID: 1080
		private bool isReadOnly;

		// Token: 0x04000439 RID: 1081
		private object cache;

		// Token: 0x020000FF RID: 255
		public struct Enumerator : IEnumerator<EmailRecipient>, IDisposable, IEnumerator
		{
			// Token: 0x060007B8 RID: 1976 RVA: 0x0001B58C File Offset: 0x0001978C
			internal Enumerator(EmailRecipientCollection collection)
			{
				this.collection = collection;
				this.index = -1;
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001B59C File Offset: 0x0001979C
			object IEnumerator.Current
			{
				get
				{
					if (this.collection == null)
					{
						throw new InvalidOperationException(EmailMessageStrings.ErrorInit);
					}
					if (this.index == -1)
					{
						throw new InvalidOperationException(EmailMessageStrings.ErrorBeforeFirst);
					}
					if (this.index == this.collection.Count)
					{
						throw new InvalidOperationException(EmailMessageStrings.ErrorAfterLast);
					}
					return this.collection[this.index];
				}
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001B600 File Offset: 0x00019800
			public EmailRecipient Current
			{
				get
				{
					if (this.collection == null)
					{
						throw new InvalidOperationException(EmailMessageStrings.ErrorInit);
					}
					if (this.index == -1)
					{
						throw new InvalidOperationException(EmailMessageStrings.ErrorBeforeFirst);
					}
					if (this.index == this.collection.Count)
					{
						throw new InvalidOperationException(EmailMessageStrings.ErrorAfterLast);
					}
					return this.collection[this.index];
				}
			}

			// Token: 0x060007BB RID: 1979 RVA: 0x0001B664 File Offset: 0x00019864
			public bool MoveNext()
			{
				if (this.collection == null)
				{
					throw new InvalidOperationException(EmailMessageStrings.ErrorInit);
				}
				return this.index != this.collection.Count && ++this.index < this.collection.Count;
			}

			// Token: 0x060007BC RID: 1980 RVA: 0x0001B6B7 File Offset: 0x000198B7
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x060007BD RID: 1981 RVA: 0x0001B6C0 File Offset: 0x000198C0
			public void Dispose()
			{
				this.Reset();
			}

			// Token: 0x0400043A RID: 1082
			private EmailRecipientCollection collection;

			// Token: 0x0400043B RID: 1083
			private int index;
		}
	}
}
