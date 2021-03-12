using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDisplayNameAttribute : LocalizedDisplayNameAttribute
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00004F90 File Offset: 0x00003190
		public LocDisplayNameAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
