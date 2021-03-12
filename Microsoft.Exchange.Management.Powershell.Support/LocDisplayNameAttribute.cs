using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000050 RID: 80
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDisplayNameAttribute : LocalizedDisplayNameAttribute
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x000112EA File Offset: 0x0000F4EA
		public LocDisplayNameAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
