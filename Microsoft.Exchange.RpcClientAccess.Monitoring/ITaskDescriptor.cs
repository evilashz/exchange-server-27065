using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000049 RID: 73
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ITaskDescriptor
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001CC RID: 460
		IPropertyBag Properties { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001CD RID: 461
		LocalizedString TaskTitle { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001CE RID: 462
		LocalizedString TaskDescription { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001CF RID: 463
		TaskType TaskType { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001D0 RID: 464
		IEnumerable<ContextProperty> DependentProperties { get; }
	}
}
