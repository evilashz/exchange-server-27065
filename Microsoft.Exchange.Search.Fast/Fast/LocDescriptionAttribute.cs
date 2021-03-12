using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x0200002D RID: 45
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600027F RID: 639 RVA: 0x0000F0E5 File Offset: 0x0000D2E5
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
