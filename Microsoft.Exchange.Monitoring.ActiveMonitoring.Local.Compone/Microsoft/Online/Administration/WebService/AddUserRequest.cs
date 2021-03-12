using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002E2 RID: 738
	[DataContract(Name = "AddUserRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class AddUserRequest : Request
	{
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0008ADC8 File Offset: 0x00088FC8
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x0008ADD0 File Offset: 0x00088FD0
		[DataMember]
		public bool? ForceChangePassword
		{
			get
			{
				return this.ForceChangePasswordField;
			}
			set
			{
				this.ForceChangePasswordField = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0008ADD9 File Offset: 0x00088FD9
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x0008ADE1 File Offset: 0x00088FE1
		[DataMember]
		public AccountSkuIdentifier[] LicenseAssignment
		{
			get
			{
				return this.LicenseAssignmentField;
			}
			set
			{
				this.LicenseAssignmentField = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0008ADEA File Offset: 0x00088FEA
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x0008ADF2 File Offset: 0x00088FF2
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

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0008ADFB File Offset: 0x00088FFB
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x0008AE03 File Offset: 0x00089003
		[DataMember]
		public string Password
		{
			get
			{
				return this.PasswordField;
			}
			set
			{
				this.PasswordField = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0008AE0C File Offset: 0x0008900C
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x0008AE14 File Offset: 0x00089014
		[DataMember]
		public User User
		{
			get
			{
				return this.UserField;
			}
			set
			{
				this.UserField = value;
			}
		}

		// Token: 0x04000F46 RID: 3910
		private bool? ForceChangePasswordField;

		// Token: 0x04000F47 RID: 3911
		private AccountSkuIdentifier[] LicenseAssignmentField;

		// Token: 0x04000F48 RID: 3912
		private LicenseOption[] LicenseOptionsField;

		// Token: 0x04000F49 RID: 3913
		private string PasswordField;

		// Token: 0x04000F4A RID: 3914
		private User UserField;
	}
}
