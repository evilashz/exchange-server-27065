using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200024F RID: 591
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ResolveNamesResponseMessageType : ResponseMessageType
	{
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x00026A40 File Offset: 0x00024C40
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x00026A48 File Offset: 0x00024C48
		public ArrayOfResolutionType ResolutionSet
		{
			get
			{
				return this.resolutionSetField;
			}
			set
			{
				this.resolutionSetField = value;
			}
		}

		// Token: 0x04000F2C RID: 3884
		private ArrayOfResolutionType resolutionSetField;
	}
}
