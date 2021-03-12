using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x0200123F RID: 4671
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600BC45 RID: 48197 RVA: 0x002AC767 File Offset: 0x002AA967
		public LocDescriptionAttribute(UpdatableHelpStrings.IDs ids) : base(UpdatableHelpStrings.GetLocalizedString(ids))
		{
		}
	}
}
