using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000783 RID: 1923
	[XmlType(TypeName = "FailedSearchMailboxType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "FailedSearchMailbox", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class FailedSearchMailbox
	{
		// Token: 0x06003965 RID: 14693 RVA: 0x000CB381 File Offset: 0x000C9581
		public FailedSearchMailbox()
		{
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000CB389 File Offset: 0x000C9589
		internal FailedSearchMailbox(string mailbox, int errorCode, string errorMessage)
		{
			this.mailbox = mailbox;
			this.errorCode = errorCode;
			this.errorMessage = errorMessage;
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06003967 RID: 14695 RVA: 0x000CB3A6 File Offset: 0x000C95A6
		// (set) Token: 0x06003968 RID: 14696 RVA: 0x000CB3AE File Offset: 0x000C95AE
		[DataMember(Name = "Mailbox", IsRequired = true)]
		[XmlElement("Mailbox")]
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
			set
			{
				this.mailbox = value;
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06003969 RID: 14697 RVA: 0x000CB3B7 File Offset: 0x000C95B7
		// (set) Token: 0x0600396A RID: 14698 RVA: 0x000CB3BF File Offset: 0x000C95BF
		[DataMember(Name = "ErrorCode", IsRequired = true)]
		[XmlElement("ErrorCode")]
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x0600396B RID: 14699 RVA: 0x000CB3C8 File Offset: 0x000C95C8
		// (set) Token: 0x0600396C RID: 14700 RVA: 0x000CB3D0 File Offset: 0x000C95D0
		[DataMember(Name = "ErrorMessage", IsRequired = true)]
		[XmlElement("ErrorMessage")]
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			set
			{
				this.errorMessage = value;
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x0600396D RID: 14701 RVA: 0x000CB3D9 File Offset: 0x000C95D9
		// (set) Token: 0x0600396E RID: 14702 RVA: 0x000CB3E1 File Offset: 0x000C95E1
		[XmlElement("IsArchive")]
		[DataMember(Name = "IsArchive", IsRequired = true)]
		public bool IsArchive
		{
			get
			{
				return this.isArchive;
			}
			set
			{
				this.isArchive = value;
			}
		}

		// Token: 0x04001FF6 RID: 8182
		private string mailbox;

		// Token: 0x04001FF7 RID: 8183
		private int errorCode;

		// Token: 0x04001FF8 RID: 8184
		private string errorMessage;

		// Token: 0x04001FF9 RID: 8185
		private bool isArchive;
	}
}
