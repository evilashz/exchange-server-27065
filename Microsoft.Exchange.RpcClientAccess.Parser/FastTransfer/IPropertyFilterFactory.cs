using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000197 RID: 407
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPropertyFilterFactory
	{
		// Token: 0x060007F6 RID: 2038
		IPropertyFilter GetAttachmentCopyToFilter(bool isTopLevel);

		// Token: 0x060007F7 RID: 2039
		IMessagePropertyFilter GetMessageCopyToFilter(bool isTopLevel);

		// Token: 0x060007F8 RID: 2040
		IPropertyFilter GetFolderCopyToFilter();

		// Token: 0x060007F9 RID: 2041
		IPropertyFilter GetCopyFolderFilter();

		// Token: 0x060007FA RID: 2042
		IPropertyFilter GetCopySubfolderFilter();

		// Token: 0x060007FB RID: 2043
		IPropertyFilter GetAttachmentFilter(bool isTopLevel);

		// Token: 0x060007FC RID: 2044
		IPropertyFilter GetEmbeddedMessageFilter(bool isTopLevel);

		// Token: 0x060007FD RID: 2045
		IPropertyFilter GetMessageFilter(bool isTopLevel);

		// Token: 0x060007FE RID: 2046
		IPropertyFilter GetRecipientFilter();

		// Token: 0x060007FF RID: 2047
		IPropertyFilter GetIcsHierarchyFilter(bool includeFid, bool includeChangeNumber);
	}
}
