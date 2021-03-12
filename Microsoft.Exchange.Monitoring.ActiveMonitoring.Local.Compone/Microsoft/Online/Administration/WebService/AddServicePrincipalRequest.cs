using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002F4 RID: 756
	[DataContract(Name = "AddServicePrincipalRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddServicePrincipalRequest : Request
	{
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0008B166 File Offset: 0x00089366
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x0008B16E File Offset: 0x0008936E
		[DataMember]
		public bool? AccountEnabled
		{
			get
			{
				return this.AccountEnabledField;
			}
			set
			{
				this.AccountEnabledField = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0008B177 File Offset: 0x00089377
		// (set) Token: 0x060014A9 RID: 5289 RVA: 0x0008B17F File Offset: 0x0008937F
		[DataMember]
		public Guid? AppPrincipalId
		{
			get
			{
				return this.AppPrincipalIdField;
			}
			set
			{
				this.AppPrincipalIdField = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0008B188 File Offset: 0x00089388
		// (set) Token: 0x060014AB RID: 5291 RVA: 0x0008B190 File Offset: 0x00089390
		[DataMember]
		public ServicePrincipalCredential[] Credentials
		{
			get
			{
				return this.CredentialsField;
			}
			set
			{
				this.CredentialsField = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0008B199 File Offset: 0x00089399
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x0008B1A1 File Offset: 0x000893A1
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.DisplayNameField;
			}
			set
			{
				this.DisplayNameField = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0008B1AA File Offset: 0x000893AA
		// (set) Token: 0x060014AF RID: 5295 RVA: 0x0008B1B2 File Offset: 0x000893B2
		[DataMember]
		public string[] ServicePrincipalNames
		{
			get
			{
				return this.ServicePrincipalNamesField;
			}
			set
			{
				this.ServicePrincipalNamesField = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0008B1BB File Offset: 0x000893BB
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x0008B1C3 File Offset: 0x000893C3
		[DataMember]
		public bool? TrustedForDelegation
		{
			get
			{
				return this.TrustedForDelegationField;
			}
			set
			{
				this.TrustedForDelegationField = value;
			}
		}

		// Token: 0x04000F74 RID: 3956
		private bool? AccountEnabledField;

		// Token: 0x04000F75 RID: 3957
		private Guid? AppPrincipalIdField;

		// Token: 0x04000F76 RID: 3958
		private ServicePrincipalCredential[] CredentialsField;

		// Token: 0x04000F77 RID: 3959
		private string DisplayNameField;

		// Token: 0x04000F78 RID: 3960
		private string[] ServicePrincipalNamesField;

		// Token: 0x04000F79 RID: 3961
		private bool? TrustedForDelegationField;
	}
}
