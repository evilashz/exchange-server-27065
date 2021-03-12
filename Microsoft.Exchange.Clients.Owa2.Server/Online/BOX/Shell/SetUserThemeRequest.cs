using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x02000072 RID: 114
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SetUserThemeRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[DebuggerStepThrough]
	public class SetUserThemeRequest : IExtensibleDataObject
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000E003 File Offset: 0x0000C203
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000E00B File Offset: 0x0000C20B
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000E014 File Offset: 0x0000C214
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000E01C File Offset: 0x0000C21C
		[DataMember]
		public string ThemeId
		{
			get
			{
				return this.ThemeIdField;
			}
			set
			{
				this.ThemeIdField = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000E025 File Offset: 0x0000C225
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000E02D File Offset: 0x0000C22D
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

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000E036 File Offset: 0x0000C236
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000E03E File Offset: 0x0000C23E
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

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000E047 File Offset: 0x0000C247
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000E04F File Offset: 0x0000C24F
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000E058 File Offset: 0x0000C258
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000E060 File Offset: 0x0000C260
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

		// Token: 0x040001FF RID: 511
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000200 RID: 512
		private string ThemeIdField;

		// Token: 0x04000201 RID: 513
		private string TrackingGuidField;

		// Token: 0x04000202 RID: 514
		private string UserPrincipalNameField;

		// Token: 0x04000203 RID: 515
		private string UserPuidField;

		// Token: 0x04000204 RID: 516
		private WorkloadAuthenticationId WorkloadIdField;
	}
}
