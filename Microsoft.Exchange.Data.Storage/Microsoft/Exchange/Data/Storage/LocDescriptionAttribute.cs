using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000B8 RID: 184
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06001214 RID: 4628 RVA: 0x000673B2 File Offset: 0x000655B2
		public LocDescriptionAttribute(ServerStrings.IDs ids) : base(ServerStrings.GetLocalizedString(ids))
		{
		}
	}
}
