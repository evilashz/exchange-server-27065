using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002BF RID: 703
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractAttachmentCollection : IAttachmentCollection, IEnumerable<AttachmentHandle>, IEnumerable
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x00085C3B File Offset: 0x00083E3B
		public virtual int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00085C42 File Offset: 0x00083E42
		public virtual IEnumerator<AttachmentHandle> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00085C49 File Offset: 0x00083E49
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x00085C51 File Offset: 0x00083E51
		public virtual IAttachment CreateIAttachment(AttachmentType type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00085C58 File Offset: 0x00083E58
		public virtual IAttachment CreateIAttachment(AttachmentType type, IAttachment attachment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00085C5F File Offset: 0x00083E5F
		public virtual IAttachment OpenIAttachment(AttachmentHandle handle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00085C66 File Offset: 0x00083E66
		public virtual bool Remove(AttachmentId attachmentId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00085C6D File Offset: 0x00083E6D
		public virtual bool Remove(AttachmentHandle handle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00085C74 File Offset: 0x00083E74
		public virtual IList<AttachmentHandle> GetHandles()
		{
			throw new NotImplementedException();
		}
	}
}
