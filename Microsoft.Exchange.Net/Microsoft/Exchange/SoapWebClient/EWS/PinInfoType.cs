using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200019E RID: 414
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PinInfoType
	{
		// Token: 0x040009C2 RID: 2498
		public string PIN;

		// Token: 0x040009C3 RID: 2499
		public bool IsValid;

		// Token: 0x040009C4 RID: 2500
		public bool PinExpired;

		// Token: 0x040009C5 RID: 2501
		public bool LockedOut;

		// Token: 0x040009C6 RID: 2502
		public bool FirstTimeUser;
	}
}
