using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001F4 RID: 500
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class ConvertIdResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x00025AB1 File Offset: 0x00023CB1
		// (set) Token: 0x06001440 RID: 5184 RVA: 0x00025AB9 File Offset: 0x00023CB9
		public AlternateIdBaseType AlternateId
		{
			get
			{
				return this.alternateIdField;
			}
			set
			{
				this.alternateIdField = value;
			}
		}

		// Token: 0x04000DF8 RID: 3576
		private AlternateIdBaseType alternateIdField;
	}
}
