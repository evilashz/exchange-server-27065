using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000069 RID: 105
	internal class AutocopyTagEnforcer : TagEnforcerBase
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x0001A5E6 File Offset: 0x000187E6
		internal AutocopyTagEnforcer(MailboxDataForTags mailboxDataForTags, ElcTagSubAssistant elcTagSubAssistant) : base(mailboxDataForTags, elcTagSubAssistant)
		{
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001A5F0 File Offset: 0x000187F0
		internal override bool IsEnabled()
		{
			return false;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001A5F3 File Offset: 0x000187F3
		internal override void Invoke()
		{
		}

		// Token: 0x040002F9 RID: 761
		private static readonly Trace Tracer = ExTraceGlobals.AutocopyTagEnforcerTracer;

		// Token: 0x040002FA RID: 762
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
