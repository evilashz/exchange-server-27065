using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.UI.Shell
{
	// Token: 0x0200007C RID: 124
	[DebuggerStepThrough]
	[DataContract(Name = "NavBarLinkData", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.UI.Shell")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class NavBarLinkData : IExtensibleDataObject
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000E5AD File Offset: 0x0000C7AD
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0000E5B5 File Offset: 0x0000C7B5
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

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000E5BE File Offset: 0x0000C7BE
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0000E5C6 File Offset: 0x0000C7C6
		[DataMember]
		public string Id
		{
			get
			{
				return this.IdField;
			}
			set
			{
				this.IdField = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000E5CF File Offset: 0x0000C7CF
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0000E5D7 File Offset: 0x0000C7D7
		[DataMember]
		public string MenuName
		{
			get
			{
				return this.MenuNameField;
			}
			set
			{
				this.MenuNameField = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		[DataMember]
		public NavBarLinkData[] SubLinks
		{
			get
			{
				return this.SubLinksField;
			}
			set
			{
				this.SubLinksField = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000E5F1 File Offset: 0x0000C7F1
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0000E5F9 File Offset: 0x0000C7F9
		[DataMember]
		public string TargetWindow
		{
			get
			{
				return this.TargetWindowField;
			}
			set
			{
				this.TargetWindowField = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000E602 File Offset: 0x0000C802
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0000E60A File Offset: 0x0000C80A
		[DataMember]
		public string Text
		{
			get
			{
				return this.TextField;
			}
			set
			{
				this.TextField = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000E613 File Offset: 0x0000C813
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0000E61B File Offset: 0x0000C81B
		[DataMember]
		public string Title
		{
			get
			{
				return this.TitleField;
			}
			set
			{
				this.TitleField = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000E624 File Offset: 0x0000C824
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0000E62C File Offset: 0x0000C82C
		[DataMember]
		public string Url
		{
			get
			{
				return this.UrlField;
			}
			set
			{
				this.UrlField = value;
			}
		}

		// Token: 0x04000283 RID: 643
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000284 RID: 644
		private string IdField;

		// Token: 0x04000285 RID: 645
		private string MenuNameField;

		// Token: 0x04000286 RID: 646
		private NavBarLinkData[] SubLinksField;

		// Token: 0x04000287 RID: 647
		private string TargetWindowField;

		// Token: 0x04000288 RID: 648
		private string TextField;

		// Token: 0x04000289 RID: 649
		private string TitleField;

		// Token: 0x0400028A RID: 650
		private string UrlField;
	}
}
