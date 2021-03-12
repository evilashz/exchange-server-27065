using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.ItemOperations
{
	// Token: 0x020000A4 RID: 164
	[ClassAccessLevel(AccessLevel.Implementation)]
	public static class Store
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000A707 File Offset: 0x00008907
		public static string Mailbox
		{
			get
			{
				return "Mailbox";
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000A70E File Offset: 0x0000890E
		public static string DocumentLibrary
		{
			get
			{
				return "Document Library";
			}
		}
	}
}
