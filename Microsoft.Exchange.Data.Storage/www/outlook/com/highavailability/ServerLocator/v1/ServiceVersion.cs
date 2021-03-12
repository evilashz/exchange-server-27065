using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace www.outlook.com.highavailability.ServerLocator.v1
{
	// Token: 0x02000D34 RID: 3380
	[DataContract(Name = "ServiceVersion", Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class ServiceVersion : IExtensibleDataObject
	{
		// Token: 0x17001F70 RID: 8048
		// (get) Token: 0x06007533 RID: 30003 RVA: 0x00208289 File Offset: 0x00206489
		// (set) Token: 0x06007534 RID: 30004 RVA: 0x00208291 File Offset: 0x00206491
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

		// Token: 0x17001F71 RID: 8049
		// (get) Token: 0x06007535 RID: 30005 RVA: 0x0020829A File Offset: 0x0020649A
		// (set) Token: 0x06007536 RID: 30006 RVA: 0x002082A2 File Offset: 0x002064A2
		[DataMember(IsRequired = true)]
		public long Version
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

		// Token: 0x04005182 RID: 20866
		private ExtensionDataObject extensionDataField;

		// Token: 0x04005183 RID: 20867
		private long VersionField;
	}
}
