using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000198 RID: 408
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class IncludeAllPropertyFilterFactory : IPropertyFilterFactory
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x0001BDAD File Offset: 0x00019FAD
		private IncludeAllPropertyFilterFactory()
		{
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001BDB5 File Offset: 0x00019FB5
		public IPropertyFilter GetAttachmentCopyToFilter(bool isTopLevel)
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001BDBC File Offset: 0x00019FBC
		public IMessagePropertyFilter GetMessageCopyToFilter(bool isTopLevel)
		{
			return IncludeAllMessagePropertyFilter.Instance;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001BDC3 File Offset: 0x00019FC3
		public IPropertyFilter GetFolderCopyToFilter()
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001BDCA File Offset: 0x00019FCA
		public IPropertyFilter GetCopySubfolderFilter()
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001BDD1 File Offset: 0x00019FD1
		public IPropertyFilter GetCopyFolderFilter()
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001BDD8 File Offset: 0x00019FD8
		public IPropertyFilter GetAttachmentFilter(bool isTopLevel)
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001BDDF File Offset: 0x00019FDF
		public IPropertyFilter GetEmbeddedMessageFilter(bool isTopLevel)
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001BDE6 File Offset: 0x00019FE6
		public IPropertyFilter GetMessageFilter(bool isTopLevel)
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001BDED File Offset: 0x00019FED
		public IPropertyFilter GetRecipientFilter()
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		public IPropertyFilter GetIcsHierarchyFilter(bool includeFid, bool includeChangeNumber)
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x040003DD RID: 989
		public static IPropertyFilterFactory Instance = new IncludeAllPropertyFilterFactory();
	}
}
