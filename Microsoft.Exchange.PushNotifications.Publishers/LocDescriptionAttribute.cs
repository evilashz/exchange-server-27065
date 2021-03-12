using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000101 RID: 257
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x00019D19 File Offset: 0x00017F19
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
