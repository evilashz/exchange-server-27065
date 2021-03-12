using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x0200006E RID: 110
	[DataContract(Name = "NavBarInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[KnownType(typeof(ShellInfo))]
	public class NavBarInfo : IExtensibleDataObject
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000DDB2 File Offset: 0x0000BFB2
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000DDBA File Offset: 0x0000BFBA
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

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000DDC3 File Offset: 0x0000BFC3
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000DDCB File Offset: 0x0000BFCB
		[DataMember]
		public string NavBarDataJson
		{
			get
			{
				return this.NavBarDataJsonField;
			}
			set
			{
				this.NavBarDataJsonField = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		[DataMember]
		public string SharedCSSUrl
		{
			get
			{
				return this.SharedCSSUrlField;
			}
			set
			{
				this.SharedCSSUrlField = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000DDE5 File Offset: 0x0000BFE5
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000DDED File Offset: 0x0000BFED
		[DataMember]
		public string SharedJSUrl
		{
			get
			{
				return this.SharedJSUrlField;
			}
			set
			{
				this.SharedJSUrlField = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000DDF6 File Offset: 0x0000BFF6
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000DDFE File Offset: 0x0000BFFE
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

		// Token: 0x040001DE RID: 478
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001DF RID: 479
		private string NavBarDataJsonField;

		// Token: 0x040001E0 RID: 480
		private string SharedCSSUrlField;

		// Token: 0x040001E1 RID: 481
		private string SharedJSUrlField;

		// Token: 0x040001E2 RID: 482
		private string VersionField;
	}
}
