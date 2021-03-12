using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000191 RID: 401
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FailedSearchMailboxType
	{
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0002416C File Offset: 0x0002236C
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x00024174 File Offset: 0x00022374
		public string Mailbox
		{
			get
			{
				return this.mailboxField;
			}
			set
			{
				this.mailboxField = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0002417D File Offset: 0x0002237D
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x00024185 File Offset: 0x00022385
		public int ErrorCode
		{
			get
			{
				return this.errorCodeField;
			}
			set
			{
				this.errorCodeField = value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0002418E File Offset: 0x0002238E
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x00024196 File Offset: 0x00022396
		public string ErrorMessage
		{
			get
			{
				return this.errorMessageField;
			}
			set
			{
				this.errorMessageField = value;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0002419F File Offset: 0x0002239F
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x000241A7 File Offset: 0x000223A7
		public bool IsArchive
		{
			get
			{
				return this.isArchiveField;
			}
			set
			{
				this.isArchiveField = value;
			}
		}

		// Token: 0x04000BE6 RID: 3046
		private string mailboxField;

		// Token: 0x04000BE7 RID: 3047
		private int errorCodeField;

		// Token: 0x04000BE8 RID: 3048
		private string errorMessageField;

		// Token: 0x04000BE9 RID: 3049
		private bool isArchiveField;
	}
}
