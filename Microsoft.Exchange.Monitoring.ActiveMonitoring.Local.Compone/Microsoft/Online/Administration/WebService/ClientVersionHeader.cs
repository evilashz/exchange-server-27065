using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200034F RID: 847
	[DataContract(Name = "ClientVersionHeader", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ClientVersionHeader : IExtensibleDataObject
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0008BC36 File Offset: 0x00089E36
		// (set) Token: 0x060015F2 RID: 5618 RVA: 0x0008BC3E File Offset: 0x00089E3E
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

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0008BC47 File Offset: 0x00089E47
		// (set) Token: 0x060015F4 RID: 5620 RVA: 0x0008BC4F File Offset: 0x00089E4F
		[DataMember]
		public Guid ClientId
		{
			get
			{
				return this.ClientIdField;
			}
			set
			{
				this.ClientIdField = value;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0008BC58 File Offset: 0x00089E58
		// (set) Token: 0x060015F6 RID: 5622 RVA: 0x0008BC60 File Offset: 0x00089E60
		[DataMember]
		public string Version
		{
			get
			{
				return this.VersionField;
			}
			set
			{
				this.VersionField = value;
			}
		}

		// Token: 0x04000FEC RID: 4076
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000FED RID: 4077
		private Guid ClientIdField;

		// Token: 0x04000FEE RID: 4078
		private string VersionField;
	}
}
