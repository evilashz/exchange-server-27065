using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x02000133 RID: 307
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetMailTipsResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x000250DB File Offset: 0x000232DB
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x000250E3 File Offset: 0x000232E3
		[XmlArrayItem(IsNullable = false)]
		public MailTipsResponseMessageType[] ResponseMessages
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

		// Token: 0x04000683 RID: 1667
		private MailTipsResponseMessageType[] responseMessagesField;
	}
}
