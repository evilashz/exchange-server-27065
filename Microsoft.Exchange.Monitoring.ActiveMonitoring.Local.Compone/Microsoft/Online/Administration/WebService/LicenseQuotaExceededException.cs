using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000396 RID: 918
	[DataContract(Name = "LicenseQuotaExceededException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class LicenseQuotaExceededException : InvalidUserLicenseException
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x0008C116 File Offset: 0x0008A316
		// (set) Token: 0x06001689 RID: 5769 RVA: 0x0008C11E File Offset: 0x0008A31E
		[DataMember]
		public int? ConsumedLicenses
		{
			get
			{
				return this.ConsumedLicensesField;
			}
			set
			{
				this.ConsumedLicensesField = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0008C127 File Offset: 0x0008A327
		// (set) Token: 0x0600168B RID: 5771 RVA: 0x0008C12F File Offset: 0x0008A32F
		[DataMember]
		public int? TotalLicenses
		{
			get
			{
				return this.TotalLicensesField;
			}
			set
			{
				this.TotalLicensesField = value;
			}
		}

		// Token: 0x04001014 RID: 4116
		private int? ConsumedLicensesField;

		// Token: 0x04001015 RID: 4117
		private int? TotalLicensesField;
	}
}
