using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HA.Services
{
	// Token: 0x02000327 RID: 807
	[DataContract(Name = "DatabaseServerInformationFault", Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/")]
	public class DatabaseServerInformationFault
	{
		// Token: 0x06002129 RID: 8489 RVA: 0x00099C94 File Offset: 0x00097E94
		public DatabaseServerInformationFault()
		{
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00099C9C File Offset: 0x00097E9C
		public DatabaseServerInformationFault(DatabaseServerInformationFaultType code, Exception ex) : this()
		{
			this.m_errorCode = code;
			this.m_type = ex.GetType().FullName;
			this.m_message = ex.Message;
			this.m_stackTrace = ex.StackTrace;
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x00099CD4 File Offset: 0x00097ED4
		// (set) Token: 0x0600212C RID: 8492 RVA: 0x00099CDC File Offset: 0x00097EDC
		[DataMember(Name = "ErrorCode", IsRequired = false, Order = 0)]
		public DatabaseServerInformationFaultType ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
			set
			{
				this.m_errorCode = value;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x00099CE5 File Offset: 0x00097EE5
		// (set) Token: 0x0600212E RID: 8494 RVA: 0x00099CED File Offset: 0x00097EED
		[DataMember(Name = "Type", IsRequired = false, Order = 1)]
		public string Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x00099CF6 File Offset: 0x00097EF6
		// (set) Token: 0x06002130 RID: 8496 RVA: 0x00099CFE File Offset: 0x00097EFE
		[DataMember(Name = "Message", IsRequired = false, Order = 2)]
		public string Message
		{
			get
			{
				return this.m_message;
			}
			set
			{
				this.m_message = value;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x00099D07 File Offset: 0x00097F07
		// (set) Token: 0x06002132 RID: 8498 RVA: 0x00099D0F File Offset: 0x00097F0F
		[DataMember(Name = "StackTrace", IsRequired = false, Order = 3)]
		public string StackTrace
		{
			get
			{
				return this.m_stackTrace;
			}
			set
			{
				this.m_stackTrace = value;
			}
		}

		// Token: 0x04000D67 RID: 3431
		private DatabaseServerInformationFaultType m_errorCode;

		// Token: 0x04000D68 RID: 3432
		private string m_type;

		// Token: 0x04000D69 RID: 3433
		private string m_message;

		// Token: 0x04000D6A RID: 3434
		private string m_stackTrace;
	}
}
