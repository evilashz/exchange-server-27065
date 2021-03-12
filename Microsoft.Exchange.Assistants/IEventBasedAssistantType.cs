using System;
using Microsoft.Exchange.Data;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000021 RID: 33
	internal interface IEventBasedAssistantType : IAssistantType
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E2 RID: 226
		MapiEventTypeFlags EventMask { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E3 RID: 227
		bool NeedsMailboxSession { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E4 RID: 228
		PropertyDefinition[] PreloadItemProperties { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E5 RID: 229
		bool ProcessesPublicDatabases { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E6 RID: 230
		Guid Identity { get; }

		// Token: 0x060000E7 RID: 231
		IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo);
	}
}
