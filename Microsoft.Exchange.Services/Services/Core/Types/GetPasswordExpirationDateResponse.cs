using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000505 RID: 1285
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetPasswordExpirationDateResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetPasswordExpirationDateResponse : ResponseMessage
	{
		// Token: 0x0600251F RID: 9503 RVA: 0x000A56EA File Offset: 0x000A38EA
		public GetPasswordExpirationDateResponse()
		{
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000A56F2 File Offset: 0x000A38F2
		internal GetPasswordExpirationDateResponse(ServiceResultCode code, ServiceError error, DateTime passwordExpirationDate) : base(code, error)
		{
			this.PasswordExpirationDate = passwordExpirationDate;
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x000A5703 File Offset: 0x000A3903
		// (set) Token: 0x06002522 RID: 9506 RVA: 0x000A570B File Offset: 0x000A390B
		[IgnoreDataMember]
		public DateTime PasswordExpirationDate { get; set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x000A5714 File Offset: 0x000A3914
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x000A5735 File Offset: 0x000A3935
		[DataMember(Name = "PasswordExpirationDate", IsRequired = true)]
		[XmlIgnore]
		public string PasswordExpirationDateString
		{
			get
			{
				return this.PasswordExpirationDate.ToString();
			}
			set
			{
				this.PasswordExpirationDate = DateTime.Parse(value);
			}
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000A5743 File Offset: 0x000A3943
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetPasswordExpirationDateResponseMessage;
		}
	}
}
