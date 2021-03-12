using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008A1 RID: 2209
	[DataContract(Name = "SecretEncryptionFailureFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class SecretEncryptionFailureFault : IExtensibleDataObject
	{
		// Token: 0x1700271E RID: 10014
		// (get) Token: 0x06006DE7 RID: 28135 RVA: 0x00175F66 File Offset: 0x00174166
		// (set) Token: 0x06006DE8 RID: 28136 RVA: 0x00175F6E File Offset: 0x0017416E
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

		// Token: 0x1700271F RID: 10015
		// (get) Token: 0x06006DE9 RID: 28137 RVA: 0x00175F77 File Offset: 0x00174177
		// (set) Token: 0x06006DEA RID: 28138 RVA: 0x00175F7F File Offset: 0x0017417F
		[DataMember(EmitDefaultValue = false)]
		public string Location
		{
			get
			{
				return this.LocationField;
			}
			set
			{
				this.LocationField = value;
			}
		}

		// Token: 0x04004790 RID: 18320
		private ExtensionDataObject extensionDataField;

		// Token: 0x04004791 RID: 18321
		private string LocationField;
	}
}
