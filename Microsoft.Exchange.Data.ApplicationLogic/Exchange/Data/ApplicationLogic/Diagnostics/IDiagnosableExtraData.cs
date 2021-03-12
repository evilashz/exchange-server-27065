using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x02000093 RID: 147
	internal interface IDiagnosableExtraData : IDiagnosable
	{
		// Token: 0x06000679 RID: 1657
		void SetData(XElement data);

		// Token: 0x0600067A RID: 1658
		void OnStop();
	}
}
