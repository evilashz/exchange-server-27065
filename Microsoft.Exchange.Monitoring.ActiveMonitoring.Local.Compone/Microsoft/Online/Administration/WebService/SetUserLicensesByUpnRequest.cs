using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002F0 RID: 752
	[DataContract(Name = "SetUserLicensesByUpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class SetUserLicensesByUpnRequest : Request
	{
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0008B07A File Offset: 0x0008927A
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x0008B082 File Offset: 0x00089282
		[DataMember]
		public AccountSkuIdentifier[] AddLicenses
		{
			get
			{
				return this.AddLicensesField;
			}
			set
			{
				this.AddLicensesField = value;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0008B08B File Offset: 0x0008928B
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x0008B093 File Offset: 0x00089293
		[DataMember]
		public LicenseOption[] LicenseOptions
		{
			get
			{
				return this.LicenseOptionsField;
			}
			set
			{
				this.LicenseOptionsField = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0008B09C File Offset: 0x0008929C
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x0008B0A4 File Offset: 0x000892A4
		[DataMember]
		public AccountSkuIdentifier[] RemoveLicenses
		{
			get
			{
				return this.RemoveLicensesField;
			}
			set
			{
				this.RemoveLicensesField = value;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x0008B0AD File Offset: 0x000892AD
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x0008B0B5 File Offset: 0x000892B5
		[DataMember]
		public string UserPrincipalName
		{
			get
			{
				return this.UserPrincipalNameField;
			}
			set
			{
				this.UserPrincipalNameField = value;
			}
		}

		// Token: 0x04000F68 RID: 3944
		private AccountSkuIdentifier[] AddLicensesField;

		// Token: 0x04000F69 RID: 3945
		private LicenseOption[] LicenseOptionsField;

		// Token: 0x04000F6A RID: 3946
		private AccountSkuIdentifier[] RemoveLicensesField;

		// Token: 0x04000F6B RID: 3947
		private string UserPrincipalNameField;
	}
}
