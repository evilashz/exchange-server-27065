using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000236 RID: 566
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetEventsResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0002652E File Offset: 0x0002472E
		// (set) Token: 0x0600157F RID: 5503 RVA: 0x00026536 File Offset: 0x00024736
		public NotificationType Notification
		{
			get
			{
				return this.notificationField;
			}
			set
			{
				this.notificationField = value;
			}
		}

		// Token: 0x04000ECF RID: 3791
		private NotificationType notificationField;
	}
}
