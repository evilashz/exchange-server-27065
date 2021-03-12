using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Local.Components
{
	// Token: 0x020002A7 RID: 679
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06001689 RID: 5769 RVA: 0x00048AB9 File Offset: 0x00046CB9
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
