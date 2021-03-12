using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200018B RID: 395
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class MemberType
	{
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0002402B File Offset: 0x0002222B
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x00024033 File Offset: 0x00022233
		public EmailAddressType Mailbox
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

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x0002403C File Offset: 0x0002223C
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x00024044 File Offset: 0x00022244
		public MemberStatusType Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x0002404D File Offset: 0x0002224D
		// (set) Token: 0x06001120 RID: 4384 RVA: 0x00024055 File Offset: 0x00022255
		[XmlIgnore]
		public bool StatusSpecified
		{
			get
			{
				return this.statusFieldSpecified;
			}
			set
			{
				this.statusFieldSpecified = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0002405E File Offset: 0x0002225E
		// (set) Token: 0x06001122 RID: 4386 RVA: 0x00024066 File Offset: 0x00022266
		[XmlAttribute]
		public string Key
		{
			get
			{
				return this.keyField;
			}
			set
			{
				this.keyField = value;
			}
		}

		// Token: 0x04000BC8 RID: 3016
		private EmailAddressType mailboxField;

		// Token: 0x04000BC9 RID: 3017
		private MemberStatusType statusField;

		// Token: 0x04000BCA RID: 3018
		private bool statusFieldSpecified;

		// Token: 0x04000BCB RID: 3019
		private string keyField;
	}
}
