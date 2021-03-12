using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200011A RID: 282
	internal interface IDiagnosable
	{
		// Token: 0x06000839 RID: 2105
		string GetDiagnosticComponentName();

		// Token: 0x0600083A RID: 2106
		XElement GetDiagnosticInfo(DiagnosableParameters parameters);
	}
}
