using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000382 RID: 898
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class UpdateMailboxAssociationType : BaseRequestType
	{
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x00029F48 File Offset: 0x00028148
		// (set) Token: 0x06001C65 RID: 7269 RVA: 0x00029F50 File Offset: 0x00028150
		public MailboxAssociationType Association
		{
			get
			{
				return this.associationField;
			}
			set
			{
				this.associationField = value;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x00029F59 File Offset: 0x00028159
		// (set) Token: 0x06001C67 RID: 7271 RVA: 0x00029F61 File Offset: 0x00028161
		public MasterMailboxType Master
		{
			get
			{
				return this.masterField;
			}
			set
			{
				this.masterField = value;
			}
		}

		// Token: 0x040012D4 RID: 4820
		private MailboxAssociationType associationField;

		// Token: 0x040012D5 RID: 4821
		private MasterMailboxType masterField;
	}
}
