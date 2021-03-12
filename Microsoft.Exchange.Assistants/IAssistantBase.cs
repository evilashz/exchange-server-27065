using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200005C RID: 92
	internal interface IAssistantBase
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002CB RID: 715
		LocalizedString Name { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002CC RID: 716
		string NonLocalizedName { get; }

		// Token: 0x060002CD RID: 717
		void OnShutdown();
	}
}
