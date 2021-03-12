using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D9B RID: 3483
	[XmlType(TypeName = "InvalidRecipientType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public sealed class InvalidRecipient
	{
		// Token: 0x060077D2 RID: 30674 RVA: 0x00211175 File Offset: 0x0020F375
		public InvalidRecipient()
		{
		}

		// Token: 0x060077D3 RID: 30675 RVA: 0x0021117D File Offset: 0x0020F37D
		internal InvalidRecipient(string smtpAddress, InvalidRecipientResponseCodeType responseCode) : this(smtpAddress, responseCode, null)
		{
		}

		// Token: 0x060077D4 RID: 30676 RVA: 0x00211188 File Offset: 0x0020F388
		internal InvalidRecipient(string smtpAddress, InvalidRecipientResponseCodeType responseCode, string messageText)
		{
			this.SmtpAddress = smtpAddress;
			this.ResponseCode = responseCode;
			this.MessageText = messageText;
		}

		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x060077D5 RID: 30677 RVA: 0x002111A5 File Offset: 0x0020F3A5
		// (set) Token: 0x060077D6 RID: 30678 RVA: 0x002111AD File Offset: 0x0020F3AD
		public string SmtpAddress { get; set; }

		// Token: 0x17002008 RID: 8200
		// (get) Token: 0x060077D7 RID: 30679 RVA: 0x002111B6 File Offset: 0x0020F3B6
		// (set) Token: 0x060077D8 RID: 30680 RVA: 0x002111BE File Offset: 0x0020F3BE
		public InvalidRecipientResponseCodeType ResponseCode { get; set; }

		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x060077D9 RID: 30681 RVA: 0x002111C7 File Offset: 0x0020F3C7
		// (set) Token: 0x060077DA RID: 30682 RVA: 0x002111CF File Offset: 0x0020F3CF
		public string MessageText { get; set; }

		// Token: 0x060077DB RID: 30683 RVA: 0x002111D8 File Offset: 0x0020F3D8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"SmtpAddress=",
				this.SmtpAddress,
				", ResponseCode=",
				this.ResponseCode,
				", MessageText=",
				this.MessageText
			});
		}
	}
}
