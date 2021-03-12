using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002EF RID: 751
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SetUserLicensesRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class SetUserLicensesRequest : Request
	{
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0008B02E File Offset: 0x0008922E
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x0008B036 File Offset: 0x00089236
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

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x0008B03F File Offset: 0x0008923F
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x0008B047 File Offset: 0x00089247
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0008B050 File Offset: 0x00089250
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x0008B058 File Offset: 0x00089258
		[DataMember]
		public Guid ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0008B061 File Offset: 0x00089261
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x0008B069 File Offset: 0x00089269
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

		// Token: 0x04000F64 RID: 3940
		private AccountSkuIdentifier[] AddLicensesField;

		// Token: 0x04000F65 RID: 3941
		private LicenseOption[] LicenseOptionsField;

		// Token: 0x04000F66 RID: 3942
		private Guid ObjectIdField;

		// Token: 0x04000F67 RID: 3943
		private AccountSkuIdentifier[] RemoveLicensesField;
	}
}
