using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000020 RID: 32
	internal interface IAssistantType
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E0 RID: 224
		LocalizedString Name { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E1 RID: 225
		string NonLocalizedName { get; }
	}
}
