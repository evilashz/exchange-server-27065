using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000392 RID: 914
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "InvalidLicenseConfigurationException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class InvalidLicenseConfigurationException : InvalidParameterException
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0008C06E File Offset: 0x0008A26E
		// (set) Token: 0x06001675 RID: 5749 RVA: 0x0008C076 File Offset: 0x0008A276
		[DataMember]
		public LicenseErrorDetail[] LicenseErrors
		{
			get
			{
				return this.LicenseErrorsField;
			}
			set
			{
				this.LicenseErrorsField = value;
			}
		}

		// Token: 0x0400100C RID: 4108
		private LicenseErrorDetail[] LicenseErrorsField;
	}
}
