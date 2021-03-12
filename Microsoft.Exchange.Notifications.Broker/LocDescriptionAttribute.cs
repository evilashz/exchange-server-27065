using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200003F RID: 63
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000CDFE File Offset: 0x0000AFFE
		public LocDescriptionAttribute(ServiceStrings.IDs ids) : base(ServiceStrings.GetLocalizedString(ids))
		{
		}
	}
}
