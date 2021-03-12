using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003B5 RID: 949
	[DataContract(Name = "ServicePrincipalCredential", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ServicePrincipalCredential : IExtensibleDataObject
	{
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x0008C37C File Offset: 0x0008A57C
		// (set) Token: 0x060016D3 RID: 5843 RVA: 0x0008C384 File Offset: 0x0008A584
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

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x0008C38D File Offset: 0x0008A58D
		// (set) Token: 0x060016D5 RID: 5845 RVA: 0x0008C395 File Offset: 0x0008A595
		[DataMember]
		public DateTime? EndDate
		{
			get
			{
				return this.EndDateField;
			}
			set
			{
				this.EndDateField = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x0008C39E File Offset: 0x0008A59E
		// (set) Token: 0x060016D7 RID: 5847 RVA: 0x0008C3A6 File Offset: 0x0008A5A6
		[DataMember]
		public Guid? KeyGroupId
		{
			get
			{
				return this.KeyGroupIdField;
			}
			set
			{
				this.KeyGroupIdField = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x0008C3AF File Offset: 0x0008A5AF
		// (set) Token: 0x060016D9 RID: 5849 RVA: 0x0008C3B7 File Offset: 0x0008A5B7
		[DataMember]
		public Guid? KeyId
		{
			get
			{
				return this.KeyIdField;
			}
			set
			{
				this.KeyIdField = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0008C3C0 File Offset: 0x0008A5C0
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x0008C3C8 File Offset: 0x0008A5C8
		[DataMember]
		public DateTime? StartDate
		{
			get
			{
				return this.StartDateField;
			}
			set
			{
				this.StartDateField = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x0008C3D1 File Offset: 0x0008A5D1
		// (set) Token: 0x060016DD RID: 5853 RVA: 0x0008C3D9 File Offset: 0x0008A5D9
		[DataMember]
		public ServicePrincipalCredentialType Type
		{
			get
			{
				return this.TypeField;
			}
			set
			{
				this.TypeField = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x0008C3E2 File Offset: 0x0008A5E2
		// (set) Token: 0x060016DF RID: 5855 RVA: 0x0008C3EA File Offset: 0x0008A5EA
		[DataMember]
		public ServicePrincipalCredentialUsage? Usage
		{
			get
			{
				return this.UsageField;
			}
			set
			{
				this.UsageField = value;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0008C3F3 File Offset: 0x0008A5F3
		// (set) Token: 0x060016E1 RID: 5857 RVA: 0x0008C3FB File Offset: 0x0008A5FB
		[DataMember]
		public string Value
		{
			get
			{
				return this.ValueField;
			}
			set
			{
				this.ValueField = value;
			}
		}

		// Token: 0x0400102D RID: 4141
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400102E RID: 4142
		private DateTime? EndDateField;

		// Token: 0x0400102F RID: 4143
		private Guid? KeyGroupIdField;

		// Token: 0x04001030 RID: 4144
		private Guid? KeyIdField;

		// Token: 0x04001031 RID: 4145
		private DateTime? StartDateField;

		// Token: 0x04001032 RID: 4146
		private ServicePrincipalCredentialType TypeField;

		// Token: 0x04001033 RID: 4147
		private ServicePrincipalCredentialUsage? UsageField;

		// Token: 0x04001034 RID: 4148
		private string ValueField;
	}
}
