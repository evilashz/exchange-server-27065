using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000346 RID: 838 RVA: 0x0000C5BD File Offset: 0x0000A7BD
		public LocDescriptionAttribute(CoreStrings.IDs ids) : base(CoreStrings.GetLocalizedString(ids))
		{
		}
	}
}
