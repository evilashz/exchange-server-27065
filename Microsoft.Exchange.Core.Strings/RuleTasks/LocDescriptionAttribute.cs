using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core.RuleTasks
{
	// Token: 0x0200002A RID: 42
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x000133A8 File Offset: 0x000115A8
		public LocDescriptionAttribute(RulesTasksStrings.IDs ids) : base(RulesTasksStrings.GetLocalizedString(ids))
		{
		}
	}
}
