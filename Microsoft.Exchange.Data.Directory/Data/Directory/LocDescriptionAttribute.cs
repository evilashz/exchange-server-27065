using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A56 RID: 2646
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06007EB3 RID: 32435 RVA: 0x001A3894 File Offset: 0x001A1A94
		public LocDescriptionAttribute(DirectoryStrings.IDs ids) : base(DirectoryStrings.GetLocalizedString(ids))
		{
		}
	}
}
