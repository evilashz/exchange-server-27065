using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.GroupMailbox.Common
{
	// Token: 0x0200005F RID: 95
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600031E RID: 798 RVA: 0x00011D0C File Offset: 0x0000FF0C
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
