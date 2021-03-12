using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200002F RID: 47
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00003BDC File Offset: 0x00001DDC
		public LocDescriptionAttribute(ClientAPIStrings.IDs ids) : base(ClientAPIStrings.GetLocalizedString(ids))
		{
		}
	}
}
