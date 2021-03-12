using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x0200001E RID: 30
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00003B97 File Offset: 0x00001D97
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
