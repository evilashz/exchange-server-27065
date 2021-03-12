using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000CD RID: 205
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x00013D2C File Offset: 0x00011F2C
		public LocDescriptionAttribute(DrmStrings.IDs ids) : base(DrmStrings.GetLocalizedString(ids))
		{
		}
	}
}
