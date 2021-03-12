using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000030 RID: 48
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MessagePropertyFilter : ExcludingPropertyFilter, IMessagePropertyFilter, IPropertyFilter
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000E85A File Offset: 0x0000CA5A
		public MessagePropertyFilter(ICollection<PropertyTag> excludedPropertyTags) : base(excludedPropertyTags)
		{
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000E863 File Offset: 0x0000CA63
		public bool IncludeRecipients
		{
			get
			{
				return base.IncludeProperty(PropertyTag.MessageRecipients);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000E870 File Offset: 0x0000CA70
		public bool IncludeAttachments
		{
			get
			{
				return base.IncludeProperty(PropertyTag.MessageAttachments);
			}
		}
	}
}
