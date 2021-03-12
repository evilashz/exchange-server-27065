using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x0200085A RID: 2138
	[DataContract(Name = "CompanyResponseInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class CompanyResponseInfo : ResponseInfo
	{
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x00065F80 File Offset: 0x00064180
		// (set) Token: 0x06002DA8 RID: 11688 RVA: 0x00065F88 File Offset: 0x00064188
		[DataMember]
		internal Guid? CompanyGuid
		{
			get
			{
				return this.CompanyGuidField;
			}
			set
			{
				this.CompanyGuidField = value;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x00065F91 File Offset: 0x00064191
		// (set) Token: 0x06002DAA RID: 11690 RVA: 0x00065F99 File Offset: 0x00064199
		[DataMember]
		internal int CompanyId
		{
			get
			{
				return this.CompanyIdField;
			}
			set
			{
				this.CompanyIdField = value;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x00065FA2 File Offset: 0x000641A2
		// (set) Token: 0x06002DAC RID: 11692 RVA: 0x00065FAA File Offset: 0x000641AA
		[DataMember]
		internal string CompanyName
		{
			get
			{
				return this.CompanyNameField;
			}
			set
			{
				this.CompanyNameField = value;
			}
		}

		// Token: 0x040027B9 RID: 10169
		[OptionalField]
		private Guid? CompanyGuidField;

		// Token: 0x040027BA RID: 10170
		[OptionalField]
		private int CompanyIdField;

		// Token: 0x040027BB RID: 10171
		[OptionalField]
		private string CompanyNameField;
	}
}
