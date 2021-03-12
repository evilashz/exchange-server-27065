using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200003D RID: 61
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "GetTenantApplicationDataResponse", Namespace = "http://tempuri.org/")]
	public class GetTenantApplicationDataResponse : IExtensibleDataObject
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000033D5 File Offset: 0x000015D5
		// (set) Token: 0x0600019A RID: 410 RVA: 0x000033DD File Offset: 0x000015DD
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000033E6 File Offset: 0x000015E6
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000033EE File Offset: 0x000015EE
		[DataMember]
		public XmlElement GetTenantApplicationDataResult
		{
			get
			{
				return this.GetTenantApplicationDataResultField;
			}
			set
			{
				this.GetTenantApplicationDataResultField = value;
			}
		}

		// Token: 0x040000BC RID: 188
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000BD RID: 189
		private XmlElement GetTenantApplicationDataResultField;
	}
}
