using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200057C RID: 1404
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600412A RID: 16682 RVA: 0x0011A5F6 File Offset: 0x001187F6
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
