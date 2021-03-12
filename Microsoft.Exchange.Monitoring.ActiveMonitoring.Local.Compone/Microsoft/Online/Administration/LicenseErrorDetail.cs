using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003FC RID: 1020
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "LicenseErrorDetail", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class LicenseErrorDetail : IExtensibleDataObject
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x0008D846 File Offset: 0x0008BA46
		// (set) Token: 0x06001948 RID: 6472 RVA: 0x0008D84E File Offset: 0x0008BA4E
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

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x0008D857 File Offset: 0x0008BA57
		// (set) Token: 0x0600194A RID: 6474 RVA: 0x0008D85F File Offset: 0x0008BA5F
		[DataMember]
		public string[] DependsOnServicePlans
		{
			get
			{
				return this.DependsOnServicePlansField;
			}
			set
			{
				this.DependsOnServicePlansField = value;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x0008D868 File Offset: 0x0008BA68
		// (set) Token: 0x0600194C RID: 6476 RVA: 0x0008D870 File Offset: 0x0008BA70
		[DataMember]
		public LicenseErrorType ErrorType
		{
			get
			{
				return this.ErrorTypeField;
			}
			set
			{
				this.ErrorTypeField = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x0008D879 File Offset: 0x0008BA79
		// (set) Token: 0x0600194E RID: 6478 RVA: 0x0008D881 File Offset: 0x0008BA81
		[DataMember]
		public bool IsWarning
		{
			get
			{
				return this.IsWarningField;
			}
			set
			{
				this.IsWarningField = value;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x0008D88A File Offset: 0x0008BA8A
		// (set) Token: 0x06001950 RID: 6480 RVA: 0x0008D892 File Offset: 0x0008BA92
		[DataMember]
		public string[] ServicePlans
		{
			get
			{
				return this.ServicePlansField;
			}
			set
			{
				this.ServicePlansField = value;
			}
		}

		// Token: 0x040011AE RID: 4526
		private ExtensionDataObject extensionDataField;

		// Token: 0x040011AF RID: 4527
		private string[] DependsOnServicePlansField;

		// Token: 0x040011B0 RID: 4528
		private LicenseErrorType ErrorTypeField;

		// Token: 0x040011B1 RID: 4529
		private bool IsWarningField;

		// Token: 0x040011B2 RID: 4530
		private string[] ServicePlansField;
	}
}
