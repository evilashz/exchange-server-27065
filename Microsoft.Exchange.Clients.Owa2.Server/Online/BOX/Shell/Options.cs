using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x02000069 RID: 105
	[DataContract(Name = "Options", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class Options : IExtensibleDataObject
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000DC8A File Offset: 0x0000BE8A
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000DC92 File Offset: 0x0000BE92
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000DC9B File Offset: 0x0000BE9B
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000DCA3 File Offset: 0x0000BEA3
		[DataMember]
		public Filters? Filters
		{
			get
			{
				return this.FiltersField;
			}
			set
			{
				this.FiltersField = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
		[DataMember]
		public bool ReturnFooterInfo
		{
			get
			{
				return this.ReturnFooterInfoField;
			}
			set
			{
				this.ReturnFooterInfoField = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000DCBD File Offset: 0x0000BEBD
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000DCC5 File Offset: 0x0000BEC5
		[DataMember]
		public bool ReturnHttpsUrls
		{
			get
			{
				return this.ReturnHttpsUrlsField;
			}
			set
			{
				this.ReturnHttpsUrlsField = value;
			}
		}

		// Token: 0x040001AB RID: 427
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001AC RID: 428
		private Filters? FiltersField;

		// Token: 0x040001AD RID: 429
		private bool ReturnFooterInfoField;

		// Token: 0x040001AE RID: 430
		private bool ReturnHttpsUrlsField;
	}
}
