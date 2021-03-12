using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Online.BOX.UI.Shell.AllSettings;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x02000073 RID: 115
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[KnownType(typeof(ShellSettingsRequest))]
	[KnownType(typeof(GetAlertRequest))]
	[DebuggerStepThrough]
	[DataContract(Name = "ShellServiceRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[KnownType(typeof(GetSuiteServiceInfoRequest))]
	public class ShellServiceRequest : IExtensibleDataObject
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000E071 File Offset: 0x0000C271
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000E079 File Offset: 0x0000C279
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

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000E082 File Offset: 0x0000C282
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000E08A File Offset: 0x0000C28A
		[DataMember]
		public string CultureName
		{
			get
			{
				return this.CultureNameField;
			}
			set
			{
				this.CultureNameField = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000E093 File Offset: 0x0000C293
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0000E09B File Offset: 0x0000C29B
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

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0000E0AC File Offset: 0x0000C2AC
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

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000E0B5 File Offset: 0x0000C2B5
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000E0BD File Offset: 0x0000C2BD
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

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000E0C6 File Offset: 0x0000C2C6
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000E0CE File Offset: 0x0000C2CE
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

		// Token: 0x04000205 RID: 517
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000206 RID: 518
		private string CultureNameField;

		// Token: 0x04000207 RID: 519
		private string TrackingGuidField;

		// Token: 0x04000208 RID: 520
		private string UserPrincipalNameField;

		// Token: 0x04000209 RID: 521
		private string UserPuidField;

		// Token: 0x0400020A RID: 522
		private WorkloadAuthenticationId WorkloadIdField;
	}
}
