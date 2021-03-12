using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000028 RID: 40
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00004F82 File Offset: 0x00003182
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
