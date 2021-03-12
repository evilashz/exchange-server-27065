using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000281 RID: 641
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FreeBusyResponseType
	{
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x000277F1 File Offset: 0x000259F1
		// (set) Token: 0x060017B8 RID: 6072 RVA: 0x000277F9 File Offset: 0x000259F9
		public ResponseMessageType ResponseMessage
		{
			get
			{
				return this.responseMessageField;
			}
			set
			{
				this.responseMessageField = value;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00027802 File Offset: 0x00025A02
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x0002780A File Offset: 0x00025A0A
		public FreeBusyView FreeBusyView
		{
			get
			{
				return this.freeBusyViewField;
			}
			set
			{
				this.freeBusyViewField = value;
			}
		}

		// Token: 0x04000FFD RID: 4093
		private ResponseMessageType responseMessageField;

		// Token: 0x04000FFE RID: 4094
		private FreeBusyView freeBusyViewField;
	}
}
