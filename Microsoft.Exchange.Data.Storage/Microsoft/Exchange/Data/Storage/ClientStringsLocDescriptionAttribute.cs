using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001C9 RID: 457
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class ClientStringsLocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06001879 RID: 6265 RVA: 0x000770FF File Offset: 0x000752FF
		public ClientStringsLocDescriptionAttribute(ClientStrings.IDs ids) : base(ClientStrings.GetLocalizedString(ids))
		{
		}
	}
}
