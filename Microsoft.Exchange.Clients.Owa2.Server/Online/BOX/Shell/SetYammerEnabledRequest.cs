using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x02000071 RID: 113
	[DataContract(Name = "SetYammerEnabledRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class SetYammerEnabledRequest : IExtensibleDataObject
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000DF84 File Offset: 0x0000C184
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000DF8C File Offset: 0x0000C18C
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

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000DF95 File Offset: 0x0000C195
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000DF9D File Offset: 0x0000C19D
		[DataMember]
		public bool Enabled
		{
			get
			{
				return this.EnabledField;
			}
			set
			{
				this.EnabledField = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000DFA6 File Offset: 0x0000C1A6
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000DFAE File Offset: 0x0000C1AE
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

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000DFB7 File Offset: 0x0000C1B7
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000DFBF File Offset: 0x0000C1BF
		[DataMember]
		public string TrackingGuid
		{
			get
			{
				return this.TrackingGuidField;
			}
			set
			{
				this.TrackingGuidField = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000DFD0 File Offset: 0x0000C1D0
		[DataMember]
		public string UserPrincipalName
		{
			get
			{
				return this.UserPrincipalNameField;
			}
			set
			{
				this.UserPrincipalNameField = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000DFD9 File Offset: 0x0000C1D9
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0000DFE1 File Offset: 0x0000C1E1
		[DataMember]
		public string UserPuid
		{
			get
			{
				return this.UserPuidField;
			}
			set
			{
				this.UserPuidField = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000DFEA File Offset: 0x0000C1EA
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0000DFF2 File Offset: 0x0000C1F2
		[DataMember]
		public WorkloadAuthenticationId WorkloadId
		{
			get
			{
				return this.WorkloadIdField;
			}
			set
			{
				this.WorkloadIdField = value;
			}
		}

		// Token: 0x040001F8 RID: 504
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001F9 RID: 505
		private bool EnabledField;

		// Token: 0x040001FA RID: 506
		private string TenantIdField;

		// Token: 0x040001FB RID: 507
		private string TrackingGuidField;

		// Token: 0x040001FC RID: 508
		private string UserPrincipalNameField;

		// Token: 0x040001FD RID: 509
		private string UserPuidField;

		// Token: 0x040001FE RID: 510
		private WorkloadAuthenticationId WorkloadIdField;
	}
}
