using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.RightsManagementServices.Online
{
	// Token: 0x0200073D RID: 1853
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "TenantProvisioningMessage", Namespace = "http://microsoft.com/RightsManagementServiceOnline/2011/04")]
	[DebuggerStepThrough]
	public class TenantProvisioningMessage : IExtensibleDataObject
	{
		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x060041AD RID: 16813 RVA: 0x0010C97C File Offset: 0x0010AB7C
		// (set) Token: 0x060041AE RID: 16814 RVA: 0x0010C984 File Offset: 0x0010AB84
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

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x060041AF RID: 16815 RVA: 0x0010C98D File Offset: 0x0010AB8D
		// (set) Token: 0x060041B0 RID: 16816 RVA: 0x0010C995 File Offset: 0x0010AB95
		[DataMember]
		public string TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x060041B1 RID: 16817 RVA: 0x0010C99E File Offset: 0x0010AB9E
		// (set) Token: 0x060041B2 RID: 16818 RVA: 0x0010C9A6 File Offset: 0x0010ABA6
		[DataMember(Order = 1)]
		public string FriendlyName
		{
			get
			{
				return this.FriendlyNameField;
			}
			set
			{
				this.FriendlyNameField = value;
			}
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x0010C9AF File Offset: 0x0010ABAF
		// (set) Token: 0x060041B4 RID: 16820 RVA: 0x0010C9B7 File Offset: 0x0010ABB7
		[DataMember(Order = 2)]
		public TenantStatus Status
		{
			get
			{
				return this.StatusField;
			}
			set
			{
				this.StatusField = value;
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x0010C9C0 File Offset: 0x0010ABC0
		// (set) Token: 0x060041B6 RID: 16822 RVA: 0x0010C9C8 File Offset: 0x0010ABC8
		[DataMember(Order = 3)]
		public string InitialDomain
		{
			get
			{
				return this.InitialDomainField;
			}
			set
			{
				this.InitialDomainField = value;
			}
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x060041B7 RID: 16823 RVA: 0x0010C9D1 File Offset: 0x0010ABD1
		// (set) Token: 0x060041B8 RID: 16824 RVA: 0x0010C9D9 File Offset: 0x0010ABD9
		[DataMember(Order = 4)]
		public long Version
		{
			get
			{
				return this.VersionField;
			}
			set
			{
				this.VersionField = value;
			}
		}

		// Token: 0x04002979 RID: 10617
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400297A RID: 10618
		private string TenantIdField;

		// Token: 0x0400297B RID: 10619
		private string FriendlyNameField;

		// Token: 0x0400297C RID: 10620
		private TenantStatus StatusField;

		// Token: 0x0400297D RID: 10621
		private string InitialDomainField;

		// Token: 0x0400297E RID: 10622
		private long VersionField;
	}
}
