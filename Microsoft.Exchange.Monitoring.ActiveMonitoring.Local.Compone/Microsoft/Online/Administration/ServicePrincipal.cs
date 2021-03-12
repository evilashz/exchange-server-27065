using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003B3 RID: 947
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ServicePrincipal", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[KnownType(typeof(ServicePrincipalExtended))]
	public class ServicePrincipal : IExtensibleDataObject
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0008C2E4 File Offset: 0x0008A4E4
		// (set) Token: 0x060016C1 RID: 5825 RVA: 0x0008C2EC File Offset: 0x0008A4EC
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

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0008C2F5 File Offset: 0x0008A4F5
		// (set) Token: 0x060016C3 RID: 5827 RVA: 0x0008C2FD File Offset: 0x0008A4FD
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

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x0008C306 File Offset: 0x0008A506
		// (set) Token: 0x060016C5 RID: 5829 RVA: 0x0008C30E File Offset: 0x0008A50E
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

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0008C317 File Offset: 0x0008A517
		// (set) Token: 0x060016C7 RID: 5831 RVA: 0x0008C31F File Offset: 0x0008A51F
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

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0008C328 File Offset: 0x0008A528
		// (set) Token: 0x060016C9 RID: 5833 RVA: 0x0008C330 File Offset: 0x0008A530
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

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0008C339 File Offset: 0x0008A539
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x0008C341 File Offset: 0x0008A541
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

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0008C34A File Offset: 0x0008A54A
		// (set) Token: 0x060016CD RID: 5837 RVA: 0x0008C352 File Offset: 0x0008A552
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

		// Token: 0x04001025 RID: 4133
		private ExtensionDataObject extensionDataField;

		// Token: 0x04001026 RID: 4134
		private bool? AccountEnabledField;

		// Token: 0x04001027 RID: 4135
		private Guid? AppPrincipalIdField;

		// Token: 0x04001028 RID: 4136
		private string DisplayNameField;

		// Token: 0x04001029 RID: 4137
		private Guid? ObjectIdField;

		// Token: 0x0400102A RID: 4138
		private string[] ServicePrincipalNamesField;

		// Token: 0x0400102B RID: 4139
		private bool? TrustedForDelegationField;
	}
}
