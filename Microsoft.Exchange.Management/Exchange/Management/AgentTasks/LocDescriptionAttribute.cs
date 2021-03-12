using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x02001209 RID: 4617
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600BA12 RID: 47634 RVA: 0x002A6FEA File Offset: 0x002A51EA
		public LocDescriptionAttribute(AgentStrings.IDs ids) : base(AgentStrings.GetLocalizedString(ids))
		{
		}
	}
}
