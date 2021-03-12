using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x02000136 RID: 310
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class BaseResponseMessageType
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0002511E File Offset: 0x0002331E
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x00025126 File Offset: 0x00023326
		public ArrayOfResponseMessagesType ResponseMessages
		{
			get
			{
				return this.responseMessagesField;
			}
			set
			{
				this.responseMessagesField = value;
			}
		}

		// Token: 0x040006A6 RID: 1702
		private ArrayOfResponseMessagesType responseMessagesField;
	}
}
