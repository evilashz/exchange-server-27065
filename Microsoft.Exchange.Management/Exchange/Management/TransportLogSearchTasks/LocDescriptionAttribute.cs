using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.TransportLogSearchTasks
{
	// Token: 0x0200120D RID: 4621
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600BA2E RID: 47662 RVA: 0x002A7536 File Offset: 0x002A5736
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
