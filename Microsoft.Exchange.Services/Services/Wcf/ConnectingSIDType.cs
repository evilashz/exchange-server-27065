using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DBE RID: 3518
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ConnectingSIDType
	{
		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x0600597E RID: 22910 RVA: 0x00118066 File Offset: 0x00116266
		// (set) Token: 0x0600597F RID: 22911 RVA: 0x0011806E File Offset: 0x0011626E
		[XmlElement]
		[DataMember(Order = 1)]
		public string PrincipalName
		{
			get
			{
				return this.principalNameField;
			}
			set
			{
				this.principalNameField = value;
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06005980 RID: 22912 RVA: 0x00118077 File Offset: 0x00116277
		// (set) Token: 0x06005981 RID: 22913 RVA: 0x0011807F File Offset: 0x0011627F
		[DataMember(Order = 2)]
		[XmlElement]
		public string SID
		{
			get
			{
				return this.sIDField;
			}
			set
			{
				this.sIDField = value;
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x06005982 RID: 22914 RVA: 0x00118088 File Offset: 0x00116288
		// (set) Token: 0x06005983 RID: 22915 RVA: 0x00118090 File Offset: 0x00116290
		[XmlElement]
		[DataMember(Order = 3)]
		public string PrimarySmtpAddress
		{
			get
			{
				return this.primarySmtpAddressField;
			}
			set
			{
				this.primarySmtpAddressField = value;
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x06005984 RID: 22916 RVA: 0x00118099 File Offset: 0x00116299
		// (set) Token: 0x06005985 RID: 22917 RVA: 0x001180A1 File Offset: 0x001162A1
		[XmlElement]
		[DataMember(Order = 4)]
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddressField;
			}
			set
			{
				this.smtpAddressField = value;
			}
		}

		// Token: 0x0400318C RID: 12684
		private string principalNameField;

		// Token: 0x0400318D RID: 12685
		private string sIDField;

		// Token: 0x0400318E RID: 12686
		private string primarySmtpAddressField;

		// Token: 0x0400318F RID: 12687
		private string smtpAddressField;
	}
}
