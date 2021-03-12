using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x0200085B RID: 2139
	[DataContract(Name = "DomainResponseInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DebuggerStepThrough]
	[Serializable]
	internal class DomainResponseInfo : ResponseInfo
	{
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x00065FBB File Offset: 0x000641BB
		// (set) Token: 0x06002DAF RID: 11695 RVA: 0x00065FC3 File Offset: 0x000641C3
		[DataMember]
		internal Guid? DomainGuid
		{
			get
			{
				return this.DomainGuidField;
			}
			set
			{
				this.DomainGuidField = value;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x00065FCC File Offset: 0x000641CC
		// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x00065FD4 File Offset: 0x000641D4
		[DataMember]
		internal string DomainName
		{
			get
			{
				return this.DomainNameField;
			}
			set
			{
				this.DomainNameField = value;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x00065FDD File Offset: 0x000641DD
		// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x00065FE5 File Offset: 0x000641E5
		[DataMember]
		internal ProvisioningSource SourceId
		{
			get
			{
				return this.SourceIdField;
			}
			set
			{
				this.SourceIdField = value;
			}
		}

		// Token: 0x040027BC RID: 10172
		[OptionalField]
		private Guid? DomainGuidField;

		// Token: 0x040027BD RID: 10173
		[OptionalField]
		private string DomainNameField;

		// Token: 0x040027BE RID: 10174
		[OptionalField]
		private ProvisioningSource SourceIdField;
	}
}
