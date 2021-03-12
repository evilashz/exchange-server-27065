using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x02000076 RID: 118
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SuiteServiceInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[DebuggerStepThrough]
	public class SuiteServiceInfo : IExtensibleDataObject
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000E100 File Offset: 0x0000C300
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000E108 File Offset: 0x0000C308
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000E111 File Offset: 0x0000C311
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000E119 File Offset: 0x0000C319
		[DataMember]
		public string[] SuiteServiceProxyOriginAllowedList
		{
			get
			{
				return this.SuiteServiceProxyOriginAllowedListField;
			}
			set
			{
				this.SuiteServiceProxyOriginAllowedListField = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000E122 File Offset: 0x0000C322
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000E12A File Offset: 0x0000C32A
		[DataMember]
		public string SuiteServiceProxyScriptUrl
		{
			get
			{
				return this.SuiteServiceProxyScriptUrlField;
			}
			set
			{
				this.SuiteServiceProxyScriptUrlField = value;
			}
		}

		// Token: 0x0400020C RID: 524
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400020D RID: 525
		private string[] SuiteServiceProxyOriginAllowedListField;

		// Token: 0x0400020E RID: 526
		private string SuiteServiceProxyScriptUrlField;
	}
}
