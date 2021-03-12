using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004C5 RID: 1221
	[XmlType(TypeName = "DelegateUserResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class DelegateUserResponseMessageType : ResponseMessage
	{
		// Token: 0x06002409 RID: 9225 RVA: 0x000A46E4 File Offset: 0x000A28E4
		public DelegateUserResponseMessageType()
		{
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000A46EC File Offset: 0x000A28EC
		internal DelegateUserResponseMessageType(ServiceResultCode code, ServiceError error, DelegateUserType delegateUser) : base(code, error)
		{
			this.delegateUser = delegateUser;
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x000A46FD File Offset: 0x000A28FD
		// (set) Token: 0x0600240C RID: 9228 RVA: 0x000A4705 File Offset: 0x000A2905
		[XmlElement("DelegateUser")]
		public DelegateUserType DelegateUser
		{
			get
			{
				return this.delegateUser;
			}
			set
			{
				this.delegateUser = value;
			}
		}

		// Token: 0x04001567 RID: 5479
		private DelegateUserType delegateUser;
	}
}
