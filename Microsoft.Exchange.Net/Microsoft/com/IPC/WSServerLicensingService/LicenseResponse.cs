using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.com.IPC.WSService;

namespace Microsoft.com.IPC.WSServerLicensingService
{
	// Token: 0x02000A08 RID: 2568
	[DataContract(Name = "LicenseResponse", Namespace = "http://microsoft.com/IPC/WSServerLicensingService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	public class LicenseResponse : IExtensibleDataObject
	{
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x0008D607 File Offset: 0x0008B807
		// (set) Token: 0x06003800 RID: 14336 RVA: 0x0008D60F File Offset: 0x0008B80F
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

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x0008D618 File Offset: 0x0008B818
		// (set) Token: 0x06003802 RID: 14338 RVA: 0x0008D620 File Offset: 0x0008B820
		[DataMember(EmitDefaultValue = false)]
		public string EndUseLicense
		{
			get
			{
				return this.EndUseLicenseField;
			}
			set
			{
				this.EndUseLicenseField = value;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x0008D629 File Offset: 0x0008B829
		// (set) Token: 0x06003804 RID: 14340 RVA: 0x0008D631 File Offset: 0x0008B831
		[DataMember(EmitDefaultValue = false)]
		public ActiveFederationFault Fault
		{
			get
			{
				return this.FaultField;
			}
			set
			{
				this.FaultField = value;
			}
		}

		// Token: 0x04002F68 RID: 12136
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002F69 RID: 12137
		private string EndUseLicenseField;

		// Token: 0x04002F6A RID: 12138
		private ActiveFederationFault FaultField;
	}
}
