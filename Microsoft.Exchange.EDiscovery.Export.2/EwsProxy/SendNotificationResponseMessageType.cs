using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200022D RID: 557
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SendNotificationResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000263B3 File Offset: 0x000245B3
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x000263BB File Offset: 0x000245BB
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

		// Token: 0x04000EB0 RID: 3760
		private NotificationType notificationField;
	}
}
