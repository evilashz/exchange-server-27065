using System;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x020000AA RID: 170
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class TrackingStringsLocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x000196C8 File Offset: 0x000178C8
		public TrackingStringsLocDescriptionAttribute(CoreStrings.IDs ids) : base(CoreStrings.GetLocalizedString(ids))
		{
		}
	}
}
