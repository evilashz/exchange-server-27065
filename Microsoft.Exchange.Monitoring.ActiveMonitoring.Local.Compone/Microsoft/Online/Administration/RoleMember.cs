using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003C6 RID: 966
	[DataContract(Name = "RoleMember", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class RoleMember : IExtensibleDataObject
	{
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x0008C6DA File Offset: 0x0008A8DA
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x0008C6E2 File Offset: 0x0008A8E2
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

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x0008C6EB File Offset: 0x0008A8EB
		// (set) Token: 0x0600173B RID: 5947 RVA: 0x0008C6F3 File Offset: 0x0008A8F3
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

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x0008C6FC File Offset: 0x0008A8FC
		// (set) Token: 0x0600173D RID: 5949 RVA: 0x0008C704 File Offset: 0x0008A904
		[DataMember]
		public string EmailAddress
		{
			get
			{
				return this.EmailAddressField;
			}
			set
			{
				this.EmailAddressField = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x0008C70D File Offset: 0x0008A90D
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x0008C715 File Offset: 0x0008A915
		[DataMember]
		public bool? IsLicensed
		{
			get
			{
				return this.IsLicensedField;
			}
			set
			{
				this.IsLicensedField = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x0008C71E File Offset: 0x0008A91E
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x0008C726 File Offset: 0x0008A926
		[DataMember]
		public DateTime? LastDirSyncTime
		{
			get
			{
				return this.LastDirSyncTimeField;
			}
			set
			{
				this.LastDirSyncTimeField = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x0008C72F File Offset: 0x0008A92F
		// (set) Token: 0x06001743 RID: 5955 RVA: 0x0008C737 File Offset: 0x0008A937
		[DataMember]
		public Guid? ObjectId
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

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x0008C740 File Offset: 0x0008A940
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x0008C748 File Offset: 0x0008A948
		[DataMember]
		public ProvisioningStatus? OverallProvisioningStatus
		{
			get
			{
				return this.OverallProvisioningStatusField;
			}
			set
			{
				this.OverallProvisioningStatusField = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x0008C751 File Offset: 0x0008A951
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x0008C759 File Offset: 0x0008A959
		[DataMember]
		public RoleMemberType RoleMemberType
		{
			get
			{
				return this.RoleMemberTypeField;
			}
			set
			{
				this.RoleMemberTypeField = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x0008C762 File Offset: 0x0008A962
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x0008C76A File Offset: 0x0008A96A
		[DataMember]
		public ValidationStatus? ValidationStatus
		{
			get
			{
				return this.ValidationStatusField;
			}
			set
			{
				this.ValidationStatusField = value;
			}
		}

		// Token: 0x04001078 RID: 4216
		private ExtensionDataObject extensionDataField;

		// Token: 0x04001079 RID: 4217
		private string DisplayNameField;

		// Token: 0x0400107A RID: 4218
		private string EmailAddressField;

		// Token: 0x0400107B RID: 4219
		private bool? IsLicensedField;

		// Token: 0x0400107C RID: 4220
		private DateTime? LastDirSyncTimeField;

		// Token: 0x0400107D RID: 4221
		private Guid? ObjectIdField;

		// Token: 0x0400107E RID: 4222
		private ProvisioningStatus? OverallProvisioningStatusField;

		// Token: 0x0400107F RID: 4223
		private RoleMemberType RoleMemberTypeField;

		// Token: 0x04001080 RID: 4224
		private ValidationStatus? ValidationStatusField;
	}
}
