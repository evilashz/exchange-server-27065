using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x0200002A RID: 42
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x0000A505 File Offset: 0x00008705
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
