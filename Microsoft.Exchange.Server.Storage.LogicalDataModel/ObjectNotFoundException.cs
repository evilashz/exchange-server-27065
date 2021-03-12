using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200005A RID: 90
	public class ObjectNotFoundException : StoreException
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x000459B8 File Offset: 0x00043BB8
		public ObjectNotFoundException(LID lid, Guid mailboxGuid, string message) : base(lid, ErrorCodeValue.NotFound, message)
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000459CE File Offset: 0x00043BCE
		public ObjectNotFoundException(LID lid, Guid mailboxGuid, string message, Exception innerException) : base(lid, ErrorCodeValue.NotFound, message, innerException)
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x000459E6 File Offset: 0x00043BE6
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x040003EF RID: 1007
		private Guid mailboxGuid;
	}
}
