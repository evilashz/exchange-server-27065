using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000BD RID: 189
	public class AttachmentCollection : IEnumerable<Attachment>, IEnumerable
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00009A39 File Offset: 0x00007C39
		public int Count
		{
			get
			{
				return this.message.AttachmentCollection_Count();
			}
		}

		// Token: 0x17000128 RID: 296
		public Attachment this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index > this.Count - 1)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				Attachment attachment = (Attachment)this.message.AttachmentCollection_Indexer(index);
				if (attachment != null)
				{
					return attachment;
				}
				attachment = new Attachment(this.message);
				AttachmentCookie cookie = this.message.AttachmentCollection_CacheAttachment(index, attachment);
				attachment.Cookie = cookie;
				return attachment;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00009AB3 File Offset: 0x00007CB3
		internal AttachmentCollection(EmailMessage message)
		{
			this.message = message;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00009AC2 File Offset: 0x00007CC2
		public Attachment Add()
		{
			return this.Add(null, null);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00009ACC File Offset: 0x00007CCC
		public Attachment Add(string fileName)
		{
			return this.Add(fileName, null);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00009AD8 File Offset: 0x00007CD8
		public Attachment Add(string fileName, string contentType)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				contentType = "application/octet-stream";
			}
			Attachment attachment = new Attachment(this.message);
			AttachmentCookie cookie = this.message.AttachmentCollection_AddAttachment(attachment);
			attachment.Cookie = cookie;
			if (!string.IsNullOrEmpty(fileName))
			{
				this.message.Attachment_SetFileName(cookie, fileName);
			}
			this.message.Attachment_SetContentType(cookie, contentType);
			this.version++;
			return attachment;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00009B48 File Offset: 0x00007D48
		public bool Remove(Attachment attachment)
		{
			if (attachment == null)
			{
				throw new ArgumentNullException("item");
			}
			bool result = this.message.AttachmentCollection_RemoveAttachment(attachment.Cookie);
			this.version++;
			return result;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00009B84 File Offset: 0x00007D84
		public void Clear()
		{
			this.message.AttachmentCollection_ClearAttachments();
			this.version++;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00009B9F File Offset: 0x00007D9F
		public AttachmentCollection.Enumerator GetEnumerator()
		{
			return new AttachmentCollection.Enumerator(this);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00009BA7 File Offset: 0x00007DA7
		IEnumerator<Attachment> IEnumerable<Attachment>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00009BB4 File Offset: 0x00007DB4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00009BC1 File Offset: 0x00007DC1
		internal void InvalidateEnumerators()
		{
			this.version++;
		}

		// Token: 0x0400026A RID: 618
		private EmailMessage message;

		// Token: 0x0400026B RID: 619
		private int version;

		// Token: 0x020000BE RID: 190
		public struct Enumerator : IEnumerator<Attachment>, IDisposable, IEnumerator
		{
			// Token: 0x06000449 RID: 1097 RVA: 0x00009BD1 File Offset: 0x00007DD1
			internal Enumerator(AttachmentCollection collection)
			{
				this.collection = collection;
				this.index = -1;
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x0600044A RID: 1098 RVA: 0x00009BE4 File Offset: 0x00007DE4
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

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x0600044B RID: 1099 RVA: 0x00009C48 File Offset: 0x00007E48
			public Attachment Current
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

			// Token: 0x0600044C RID: 1100 RVA: 0x00009CAC File Offset: 0x00007EAC
			public bool MoveNext()
			{
				if (this.collection == null)
				{
					throw new InvalidOperationException(EmailMessageStrings.ErrorInit);
				}
				return this.index != this.collection.Count && ++this.index < this.collection.Count;
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x00009CFF File Offset: 0x00007EFF
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x0600044E RID: 1102 RVA: 0x00009D08 File Offset: 0x00007F08
			public void Dispose()
			{
				this.Reset();
			}

			// Token: 0x0400026C RID: 620
			private AttachmentCollection collection;

			// Token: 0x0400026D RID: 621
			private int index;
		}
	}
}
