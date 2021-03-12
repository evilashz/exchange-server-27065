using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000370 RID: 880
	[DataContract(Name = "RestoreUserLicenseErrorException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class RestoreUserLicenseErrorException : DataOperationException
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0008BEA3 File Offset: 0x0008A0A3
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0008BEAB File Offset: 0x0008A0AB
		[DataMember]
		public AccountSkuIdentifier[] Licenses
		{
			get
			{
				return this.LicensesField;
			}
			set
			{
				this.LicensesField = value;
			}
		}

		// Token: 0x04001001 RID: 4097
		private AccountSkuIdentifier[] LicensesField;
	}
}
