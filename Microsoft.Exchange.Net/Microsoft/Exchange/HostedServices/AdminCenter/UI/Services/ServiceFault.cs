using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x0200085C RID: 2140
	[DataContract(Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class ServiceFault : IExtensibleDataObject
	{
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x00065FF6 File Offset: 0x000641F6
		// (set) Token: 0x06002DB6 RID: 11702 RVA: 0x00065FFE File Offset: 0x000641FE
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

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x00066007 File Offset: 0x00064207
		// (set) Token: 0x06002DB8 RID: 11704 RVA: 0x0006600F File Offset: 0x0006420F
		[DataMember]
		internal string Detail
		{
			get
			{
				return this.DetailField;
			}
			set
			{
				this.DetailField = value;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x00066018 File Offset: 0x00064218
		// (set) Token: 0x06002DBA RID: 11706 RVA: 0x00066020 File Offset: 0x00064220
		[DataMember]
		internal FaultId Id
		{
			get
			{
				return this.IdField;
			}
			set
			{
				this.IdField = value;
			}
		}

		// Token: 0x040027BF RID: 10175
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040027C0 RID: 10176
		[OptionalField]
		private string DetailField;

		// Token: 0x040027C1 RID: 10177
		[OptionalField]
		private FaultId IdField;
	}
}
