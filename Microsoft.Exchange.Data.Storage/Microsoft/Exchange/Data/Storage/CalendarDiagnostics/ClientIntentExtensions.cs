using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000363 RID: 867
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ClientIntentExtensions
	{
		// Token: 0x06002693 RID: 9875 RVA: 0x0009A952 File Offset: 0x00098B52
		internal static bool Includes(this ClientIntentFlags flags, ClientIntentFlags desiredFlags)
		{
			return (flags & desiredFlags) == desiredFlags;
		}
	}
}
