using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001219 RID: 4633
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600BAE0 RID: 47840 RVA: 0x002A9605 File Offset: 0x002A7805
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
