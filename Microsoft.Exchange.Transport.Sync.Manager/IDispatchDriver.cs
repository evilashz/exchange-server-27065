using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDispatchDriver
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600025C RID: 604
		// (remove) Token: 0x0600025D RID: 605
		event EventHandler<EventArgs> PrimingEvent;

		// Token: 0x0600025E RID: 606
		void AddDiagnosticInfoTo(XElement componentElement);
	}
}
