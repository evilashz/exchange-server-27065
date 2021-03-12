using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200084D RID: 2125
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlInclude(typeof(MSExchBypassModerationFromDLMembersLink))]
	[XmlInclude(typeof(DLMemSubmitPerms))]
	[XmlInclude(typeof(DLMemRejectPerms))]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public abstract class DirectoryLinkAddressListObjectToGroup : DirectoryLink
	{
		// Token: 0x06006A54 RID: 27220 RVA: 0x001728A2 File Offset: 0x00170AA2
		public DirectoryLinkAddressListObjectToGroup()
		{
			this.targetClassField = DirectoryObjectClass.Group;
		}

		// Token: 0x170025CA RID: 9674
		// (get) Token: 0x06006A55 RID: 27221 RVA: 0x001728B1 File Offset: 0x00170AB1
		// (set) Token: 0x06006A56 RID: 27222 RVA: 0x001728B9 File Offset: 0x00170AB9
		[XmlAttribute]
		public DirectoryObjectClassAddressList SourceClass
		{
			get
			{
				return this.sourceClassField;
			}
			set
			{
				this.sourceClassField = value;
			}
		}

		// Token: 0x170025CB RID: 9675
		// (get) Token: 0x06006A57 RID: 27223 RVA: 0x001728C2 File Offset: 0x00170AC2
		// (set) Token: 0x06006A58 RID: 27224 RVA: 0x001728CA File Offset: 0x00170ACA
		[XmlAttribute]
		public DirectoryObjectClass TargetClass
		{
			get
			{
				return this.targetClassField;
			}
			set
			{
				this.targetClassField = value;
			}
		}

		// Token: 0x170025CC RID: 9676
		// (get) Token: 0x06006A59 RID: 27225 RVA: 0x001728D3 File Offset: 0x00170AD3
		// (set) Token: 0x06006A5A RID: 27226 RVA: 0x001728DB File Offset: 0x00170ADB
		[XmlIgnore]
		public bool TargetClassSpecified
		{
			get
			{
				return this.targetClassFieldSpecified;
			}
			set
			{
				this.targetClassFieldSpecified = value;
			}
		}

		// Token: 0x0400458F RID: 17807
		private DirectoryObjectClassAddressList sourceClassField;

		// Token: 0x04004590 RID: 17808
		private DirectoryObjectClass targetClassField;

		// Token: 0x04004591 RID: 17809
		private bool targetClassFieldSpecified;
	}
}
