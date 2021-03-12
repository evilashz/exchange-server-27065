using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.RightsManagementServices.Online
{
	// Token: 0x02000734 RID: 1844
	[DataContract(Name = "TenantEnrollmentInfo", Namespace = "http://microsoft.com/RightsManagementServiceOnline/2011/04")]
	[KnownType(typeof(TenantInfo))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class TenantEnrollmentInfo : IExtensibleDataObject
	{
		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x0010C6A6 File Offset: 0x0010A8A6
		// (set) Token: 0x0600416A RID: 16746 RVA: 0x0010C6AE File Offset: 0x0010A8AE
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

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x0600416B RID: 16747 RVA: 0x0010C6B7 File Offset: 0x0010A8B7
		// (set) Token: 0x0600416C RID: 16748 RVA: 0x0010C6BF File Offset: 0x0010A8BF
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

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x0010C6C8 File Offset: 0x0010A8C8
		// (set) Token: 0x0600416E RID: 16750 RVA: 0x0010C6D0 File Offset: 0x0010A8D0
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

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x0010C6D9 File Offset: 0x0010A8D9
		// (set) Token: 0x06004170 RID: 16752 RVA: 0x0010C6E1 File Offset: 0x0010A8E1
		[DataMember(Order = 2)]
		public string OnpremiseRightsMgmtSvcDomainName
		{
			get
			{
				return this.OnpremiseRightsMgmtSvcDomainNameField;
			}
			set
			{
				this.OnpremiseRightsMgmtSvcDomainNameField = value;
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x06004171 RID: 16753 RVA: 0x0010C6EA File Offset: 0x0010A8EA
		// (set) Token: 0x06004172 RID: 16754 RVA: 0x0010C6F2 File Offset: 0x0010A8F2
		[DataMember(Order = 3)]
		public CryptoModeScheme CryptoMode
		{
			get
			{
				return this.CryptoModeField;
			}
			set
			{
				this.CryptoModeField = value;
			}
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x0010C6FB File Offset: 0x0010A8FB
		// (set) Token: 0x06004174 RID: 16756 RVA: 0x0010C703 File Offset: 0x0010A903
		[DataMember(Order = 4)]
		public HierarchyType Hierarchy
		{
			get
			{
				return this.HierarchyField;
			}
			set
			{
				this.HierarchyField = value;
			}
		}

		// Token: 0x04002942 RID: 10562
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002943 RID: 10563
		private string TenantIdField;

		// Token: 0x04002944 RID: 10564
		private string FriendlyNameField;

		// Token: 0x04002945 RID: 10565
		private string OnpremiseRightsMgmtSvcDomainNameField;

		// Token: 0x04002946 RID: 10566
		private CryptoModeScheme CryptoModeField;

		// Token: 0x04002947 RID: 10567
		private HierarchyType HierarchyField;
	}
}
