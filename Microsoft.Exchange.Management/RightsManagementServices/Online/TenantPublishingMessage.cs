using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.RightsManagementServices.Online
{
	// Token: 0x0200073E RID: 1854
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "TenantPublishingMessage", Namespace = "http://microsoft.com/RightsManagementServiceOnline/2011/04")]
	[DebuggerStepThrough]
	public class TenantPublishingMessage : IExtensibleDataObject
	{
		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x060041BA RID: 16826 RVA: 0x0010C9EA File Offset: 0x0010ABEA
		// (set) Token: 0x060041BB RID: 16827 RVA: 0x0010C9F2 File Offset: 0x0010ABF2
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

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x060041BC RID: 16828 RVA: 0x0010C9FB File Offset: 0x0010ABFB
		// (set) Token: 0x060041BD RID: 16829 RVA: 0x0010CA03 File Offset: 0x0010AC03
		[DataMember]
		public string TenantCertificationUrl
		{
			get
			{
				return this.TenantCertificationUrlField;
			}
			set
			{
				this.TenantCertificationUrlField = value;
			}
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x060041BE RID: 16830 RVA: 0x0010CA0C File Offset: 0x0010AC0C
		// (set) Token: 0x060041BF RID: 16831 RVA: 0x0010CA14 File Offset: 0x0010AC14
		[DataMember]
		public string TenantLicensingUrl
		{
			get
			{
				return this.TenantLicensingUrlField;
			}
			set
			{
				this.TenantLicensingUrlField = value;
			}
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x0010CA1D File Offset: 0x0010AC1D
		// (set) Token: 0x060041C1 RID: 16833 RVA: 0x0010CA25 File Offset: 0x0010AC25
		[DataMember(Order = 2)]
		public string TenantKeyExportUrl
		{
			get
			{
				return this.TenantKeyExportUrlField;
			}
			set
			{
				this.TenantKeyExportUrlField = value;
			}
		}

		// Token: 0x0400297F RID: 10623
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002980 RID: 10624
		private string TenantCertificationUrlField;

		// Token: 0x04002981 RID: 10625
		private string TenantLicensingUrlField;

		// Token: 0x04002982 RID: 10626
		private string TenantKeyExportUrlField;
	}
}
