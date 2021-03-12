using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core
{
	// Token: 0x020000BE RID: 190
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x00012E15 File Offset: 0x00011015
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
