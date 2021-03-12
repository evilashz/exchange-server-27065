using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x0200002F RID: 47
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropertyFilterFactory : IPropertyFilterFactory
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000E7CE File Offset: 0x0000C9CE
		public PropertyFilterFactory(ICollection<PropertyTag> excludedPropertyTags)
		{
			this.propertyCopyToFilterTopLevel = new ExcludingPropertyFilter(excludedPropertyTags);
			this.messageCopyToFilterTopLevel = new MessagePropertyFilter(excludedPropertyTags);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000E7EE File Offset: 0x0000C9EE
		public IPropertyFilter GetAttachmentCopyToFilter(bool isTopLevel)
		{
			if (isTopLevel)
			{
				return this.propertyCopyToFilterTopLevel;
			}
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000E7FF File Offset: 0x0000C9FF
		public IMessagePropertyFilter GetMessageCopyToFilter(bool isTopLevel)
		{
			if (isTopLevel)
			{
				return this.messageCopyToFilterTopLevel;
			}
			return IncludeAllMessagePropertyFilter.Instance;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000E810 File Offset: 0x0000CA10
		public IPropertyFilter GetFolderCopyToFilter()
		{
			return this.propertyCopyToFilterTopLevel;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000E818 File Offset: 0x0000CA18
		public IPropertyFilter GetCopySubfolderFilter()
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000E81F File Offset: 0x0000CA1F
		public IPropertyFilter GetCopyFolderFilter()
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000E826 File Offset: 0x0000CA26
		public IPropertyFilter GetAttachmentFilter(bool isTopLevel)
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000E82D File Offset: 0x0000CA2D
		public IPropertyFilter GetEmbeddedMessageFilter(bool isTopLevel)
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000E834 File Offset: 0x0000CA34
		public IPropertyFilter GetMessageFilter(bool isTopLevel)
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000E83B File Offset: 0x0000CA3B
		public IPropertyFilter GetRecipientFilter()
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000E842 File Offset: 0x0000CA42
		public IPropertyFilter GetIcsHierarchyFilter(bool includeFid, bool includeChangeNumber)
		{
			return IncludeAllPropertyFilter.Instance;
		}

		// Token: 0x040000D5 RID: 213
		private IPropertyFilter propertyCopyToFilterTopLevel;

		// Token: 0x040000D6 RID: 214
		private IMessagePropertyFilter messageCopyToFilterTopLevel;

		// Token: 0x040000D7 RID: 215
		public static IPropertyFilterFactory IncludeAllFactory = new PropertyFilterFactory(Array<PropertyTag>.Empty);
	}
}
