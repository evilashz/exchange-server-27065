using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000321 RID: 801
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06001905 RID: 6405 RVA: 0x00073A3E File Offset: 0x00071C3E
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
