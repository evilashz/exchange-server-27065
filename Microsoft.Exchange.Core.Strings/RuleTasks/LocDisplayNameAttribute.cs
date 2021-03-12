using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core.RuleTasks
{
	// Token: 0x0200002B RID: 43
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public sealed class LocDisplayNameAttribute : LocalizedDisplayNameAttribute
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x000133B6 File Offset: 0x000115B6
		public LocDisplayNameAttribute(RulesTasksStrings.IDs ids) : base(RulesTasksStrings.GetLocalizedString(ids))
		{
		}
	}
}
