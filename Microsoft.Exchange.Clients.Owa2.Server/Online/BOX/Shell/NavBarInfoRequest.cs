using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x0200006B RID: 107
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "NavBarInfoRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[KnownType(typeof(ShellInfoRequest))]
	public class NavBarInfoRequest : IExtensibleDataObject
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000DCD6 File Offset: 0x0000BED6
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000DCDE File Offset: 0x0000BEDE
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000DCE7 File Offset: 0x0000BEE7
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000DCEF File Offset: 0x0000BEEF
		[DataMember]
		public string BrandId
		{
			get
			{
				return this.BrandIdField;
			}
			set
			{
				this.BrandIdField = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000DD00 File Offset: 0x0000BF00
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

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000DD09 File Offset: 0x0000BF09
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000DD11 File Offset: 0x0000BF11
		[DataMember]
		public NavBarMainLinkID CurrentMainLinkID
		{
			get
			{
				return this.CurrentMainLinkIDField;
			}
			set
			{
				this.CurrentMainLinkIDField = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000DD1A File Offset: 0x0000BF1A
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000DD22 File Offset: 0x0000BF22
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

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000DD2B File Offset: 0x0000BF2B
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000DD33 File Offset: 0x0000BF33
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000DD44 File Offset: 0x0000BF44
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000DD4D File Offset: 0x0000BF4D
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000DD55 File Offset: 0x0000BF55
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

		// Token: 0x040001BD RID: 445
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001BE RID: 446
		private string BrandIdField;

		// Token: 0x040001BF RID: 447
		private string CultureNameField;

		// Token: 0x040001C0 RID: 448
		private NavBarMainLinkID CurrentMainLinkIDField;

		// Token: 0x040001C1 RID: 449
		private string TrackingGuidField;

		// Token: 0x040001C2 RID: 450
		private string UserPrincipalNameField;

		// Token: 0x040001C3 RID: 451
		private string UserPuidField;

		// Token: 0x040001C4 RID: 452
		private WorkloadAuthenticationId WorkloadIdField;
	}
}
