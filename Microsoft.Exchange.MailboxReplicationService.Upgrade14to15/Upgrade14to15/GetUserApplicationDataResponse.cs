using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000041 RID: 65
	[DataContract(Name = "GetUserApplicationDataResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetUserApplicationDataResponse : IExtensibleDataObject
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000348E File Offset: 0x0000168E
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00003496 File Offset: 0x00001696
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

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000349F File Offset: 0x0000169F
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000034A7 File Offset: 0x000016A7
		[DataMember]
		public XmlElement GetUserApplicationDataResult
		{
			get
			{
				return this.GetUserApplicationDataResultField;
			}
			set
			{
				this.GetUserApplicationDataResultField = value;
			}
		}

		// Token: 0x040000C5 RID: 197
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000C6 RID: 198
		private XmlElement GetUserApplicationDataResultField;
	}
}
