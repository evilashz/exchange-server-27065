using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000393 RID: 915
	[DebuggerStepThrough]
	[DataContract(Name = "InvalidUserLicenseOptionException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class InvalidUserLicenseOptionException : InvalidParameterException
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0008C087 File Offset: 0x0008A287
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x0008C08F File Offset: 0x0008A28F
		[DataMember]
		public string AccountName
		{
			get
			{
				return this.AccountNameField;
			}
			set
			{
				this.AccountNameField = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0008C098 File Offset: 0x0008A298
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x0008C0A0 File Offset: 0x0008A2A0
		[DataMember]
		public string ServicePlanName
		{
			get
			{
				return this.ServicePlanNameField;
			}
			set
			{
				this.ServicePlanNameField = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0008C0A9 File Offset: 0x0008A2A9
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x0008C0B1 File Offset: 0x0008A2B1
		[DataMember]
		public string SkuPartNumber
		{
			get
			{
				return this.SkuPartNumberField;
			}
			set
			{
				this.SkuPartNumberField = value;
			}
		}

		// Token: 0x0400100D RID: 4109
		private string AccountNameField;

		// Token: 0x0400100E RID: 4110
		private string ServicePlanNameField;

		// Token: 0x0400100F RID: 4111
		private string SkuPartNumberField;
	}
}
