using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200090E RID: 2318
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class InvitationTicketValue
	{
		// Token: 0x1700277A RID: 10106
		// (get) Token: 0x06006F1F RID: 28447 RVA: 0x00176928 File Offset: 0x00174B28
		// (set) Token: 0x06006F20 RID: 28448 RVA: 0x00176930 File Offset: 0x00174B30
		[XmlAttribute]
		public int Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x1700277B RID: 10107
		// (get) Token: 0x06006F21 RID: 28449 RVA: 0x00176939 File Offset: 0x00174B39
		// (set) Token: 0x06006F22 RID: 28450 RVA: 0x00176941 File Offset: 0x00174B41
		[XmlAttribute]
		public string Ticket
		{
			get
			{
				return this.ticketField;
			}
			set
			{
				this.ticketField = value;
			}
		}

		// Token: 0x04004826 RID: 18470
		private int typeField;

		// Token: 0x04004827 RID: 18471
		private string ticketField;
	}
}
