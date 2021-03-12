using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.online.nws.change._2010._01
{
	// Token: 0x020003FF RID: 1023
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "NetworkSyntheticFault", Namespace = "http://schemas.microsoft.com/online/nws/change/2010/01")]
	[DebuggerStepThrough]
	public class NetworkSyntheticFault : IExtensibleDataObject
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0008D8AB File Offset: 0x0008BAAB
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x0008D8B3 File Offset: 0x0008BAB3
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

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x0008D8BC File Offset: 0x0008BABC
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x0008D8C4 File Offset: 0x0008BAC4
		[DataMember]
		public string ComponentName
		{
			get
			{
				return this.ComponentNameField;
			}
			set
			{
				this.ComponentNameField = value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x0008D8CD File Offset: 0x0008BACD
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x0008D8D5 File Offset: 0x0008BAD5
		[DataMember]
		public string ErrorDescription
		{
			get
			{
				return this.ErrorDescriptionField;
			}
			set
			{
				this.ErrorDescriptionField = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x0008D8DE File Offset: 0x0008BADE
		// (set) Token: 0x0600195A RID: 6490 RVA: 0x0008D8E6 File Offset: 0x0008BAE6
		[DataMember]
		public string ServerName
		{
			get
			{
				return this.ServerNameField;
			}
			set
			{
				this.ServerNameField = value;
			}
		}

		// Token: 0x040011BA RID: 4538
		private ExtensionDataObject extensionDataField;

		// Token: 0x040011BB RID: 4539
		private string ComponentNameField;

		// Token: 0x040011BC RID: 4540
		private string ErrorDescriptionField;

		// Token: 0x040011BD RID: 4541
		private string ServerNameField;
	}
}
