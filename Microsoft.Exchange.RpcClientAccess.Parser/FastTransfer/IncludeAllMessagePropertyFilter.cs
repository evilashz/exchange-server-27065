using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000195 RID: 405
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class IncludeAllMessagePropertyFilter : IMessagePropertyFilter, IPropertyFilter
	{
		// Token: 0x060007EE RID: 2030 RVA: 0x0001BD79 File Offset: 0x00019F79
		public bool IncludeProperty(PropertyTag propertyTag)
		{
			return true;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0001BD7C File Offset: 0x00019F7C
		public bool IncludeRecipients
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001BD7F File Offset: 0x00019F7F
		public bool IncludeAttachments
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040003DB RID: 987
		public static readonly IMessagePropertyFilter Instance = new IncludeAllMessagePropertyFilter();
	}
}
