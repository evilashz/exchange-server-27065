using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200018E RID: 398
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMessage : IDisposable
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060007D3 RID: 2003
		IPropertyBag PropertyBag { get; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060007D4 RID: 2004
		bool IsAssociated { get; }

		// Token: 0x060007D5 RID: 2005
		IEnumerable<IRecipient> GetRecipients();

		// Token: 0x060007D6 RID: 2006
		IRecipient CreateRecipient();

		// Token: 0x060007D7 RID: 2007
		void RemoveRecipient(int rowId);

		// Token: 0x060007D8 RID: 2008
		IEnumerable<IAttachmentHandle> GetAttachments();

		// Token: 0x060007D9 RID: 2009
		IAttachment CreateAttachment();

		// Token: 0x060007DA RID: 2010
		void Save();

		// Token: 0x060007DB RID: 2011
		void SetLongTermId(StoreLongTermId longTermId);
	}
}
