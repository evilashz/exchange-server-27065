using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Common.LocStrings
{
	// Token: 0x0200029D RID: 669
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06001890 RID: 6288 RVA: 0x0005C038 File Offset: 0x0005A238
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
