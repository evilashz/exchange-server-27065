using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.AirSync;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.ComposeMail
{
	// Token: 0x02000060 RID: 96
	[XmlType(Namespace = "ComposeMail", TypeName = "SendMail")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class SendMail
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000053F1 File Offset: 0x000035F1
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000053F9 File Offset: 0x000035F9
		[XmlElement(ElementName = "ClientId")]
		public string ClientId { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00005402 File Offset: 0x00003602
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000540A File Offset: 0x0000360A
		[XmlElement(ElementName = "AccountId")]
		public string AccountId { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00005413 File Offset: 0x00003613
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00005424 File Offset: 0x00003624
		[XmlElement(ElementName = "SaveInSentItems")]
		public EmptyTag SerializableSaveInSentItems
		{
			get
			{
				if (!this.SaveInSentItems)
				{
					return null;
				}
				return new EmptyTag();
			}
			set
			{
				this.SaveInSentItems = (value != null);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00005433 File Offset: 0x00003633
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000543B File Offset: 0x0000363B
		[XmlIgnore]
		public bool SaveInSentItems { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005444 File Offset: 0x00003644
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00005456 File Offset: 0x00003656
		[XmlElement(ElementName = "Mime")]
		public XmlCDataSection SerializableMime
		{
			get
			{
				return new XmlDocument().CreateCDataSection(this.Mime);
			}
			set
			{
				this.Mime = value.Value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005464 File Offset: 0x00003664
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000546C File Offset: 0x0000366C
		[XmlIgnore]
		public string Mime { get; set; }
	}
}
