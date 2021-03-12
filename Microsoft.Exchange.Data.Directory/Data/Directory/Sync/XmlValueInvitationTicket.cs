using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200090D RID: 2317
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueInvitationTicket
	{
		// Token: 0x17002779 RID: 10105
		// (get) Token: 0x06006F1C RID: 28444 RVA: 0x0017690F File Offset: 0x00174B0F
		// (set) Token: 0x06006F1D RID: 28445 RVA: 0x00176917 File Offset: 0x00174B17
		[XmlElement(Order = 0)]
		public InvitationTicketValue InvitationTicket
		{
			get
			{
				return this.invitationTicketField;
			}
			set
			{
				this.invitationTicketField = value;
			}
		}

		// Token: 0x04004825 RID: 18469
		private InvitationTicketValue invitationTicketField;
	}
}
