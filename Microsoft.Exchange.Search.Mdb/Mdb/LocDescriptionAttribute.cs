using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000037 RID: 55
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000D279 File Offset: 0x0000B479
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
