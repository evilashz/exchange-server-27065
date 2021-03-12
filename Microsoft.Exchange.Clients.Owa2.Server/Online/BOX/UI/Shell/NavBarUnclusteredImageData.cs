using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.UI.Shell
{
	// Token: 0x0200007F RID: 127
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "NavBarUnclusteredImageData", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.UI.Shell")]
	public class NavBarUnclusteredImageData : IExtensibleDataObject
	{
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0000E6CC File Offset: 0x0000C8CC
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

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000E6D5 File Offset: 0x0000C8D5
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0000E6DD File Offset: 0x0000C8DD
		[DataMember]
		public string AltText
		{
			get
			{
				return this.AltTextField;
			}
			set
			{
				this.AltTextField = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000E6E6 File Offset: 0x0000C8E6
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0000E6EE File Offset: 0x0000C8EE
		[DataMember]
		public string Url
		{
			get
			{
				return this.UrlField;
			}
			set
			{
				this.UrlField = value;
			}
		}

		// Token: 0x04000292 RID: 658
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000293 RID: 659
		private string AltTextField;

		// Token: 0x04000294 RID: 660
		private string UrlField;
	}
}
