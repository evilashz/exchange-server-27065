using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200004F RID: 79
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x000112DC File Offset: 0x0000F4DC
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
