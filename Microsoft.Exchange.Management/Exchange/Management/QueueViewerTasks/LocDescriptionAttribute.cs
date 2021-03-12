using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02001213 RID: 4627
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600BA83 RID: 47747 RVA: 0x002A84B4 File Offset: 0x002A66B4
		public LocDescriptionAttribute(QueueViewerStrings.IDs ids) : base(QueueViewerStrings.GetLocalizedString(ids))
		{
		}
	}
}
