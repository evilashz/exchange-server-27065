using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200015A RID: 346
	internal interface IDiagnosableObject
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060009E8 RID: 2536
		string HashableIdentity { get; }

		// Token: 0x060009E9 RID: 2537
		XElement GetDiagnosticInfo(string argument);
	}
}
