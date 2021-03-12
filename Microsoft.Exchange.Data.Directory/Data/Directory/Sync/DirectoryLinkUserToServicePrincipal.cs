using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000954 RID: 2388
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public abstract class DirectoryLinkUserToServicePrincipal : DirectoryLink
	{
		// Token: 0x06007094 RID: 28820 RVA: 0x00177581 File Offset: 0x00175781
		public DirectoryLinkUserToServicePrincipal()
		{
			this.sourceClassField = DirectoryObjectClass.User;
			this.targetClassField = DirectoryObjectClass.ServicePrincipal;
		}

		// Token: 0x17002816 RID: 10262
		// (get) Token: 0x06007095 RID: 28821 RVA: 0x00177598 File Offset: 0x00175798
		// (set) Token: 0x06007096 RID: 28822 RVA: 0x001775A0 File Offset: 0x001757A0
		[XmlAttribute]
		public DirectoryObjectClass SourceClass
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

		// Token: 0x17002817 RID: 10263
		// (get) Token: 0x06007097 RID: 28823 RVA: 0x001775A9 File Offset: 0x001757A9
		// (set) Token: 0x06007098 RID: 28824 RVA: 0x001775B1 File Offset: 0x001757B1
		[XmlIgnore]
		public bool SourceClassSpecified
		{
			get
			{
				return this.sourceClassFieldSpecified;
			}
			set
			{
				this.sourceClassFieldSpecified = value;
			}
		}

		// Token: 0x17002818 RID: 10264
		// (get) Token: 0x06007099 RID: 28825 RVA: 0x001775BA File Offset: 0x001757BA
		// (set) Token: 0x0600709A RID: 28826 RVA: 0x001775C2 File Offset: 0x001757C2
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

		// Token: 0x17002819 RID: 10265
		// (get) Token: 0x0600709B RID: 28827 RVA: 0x001775CB File Offset: 0x001757CB
		// (set) Token: 0x0600709C RID: 28828 RVA: 0x001775D3 File Offset: 0x001757D3
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

		// Token: 0x04004900 RID: 18688
		private DirectoryObjectClass sourceClassField;

		// Token: 0x04004901 RID: 18689
		private bool sourceClassFieldSpecified;

		// Token: 0x04004902 RID: 18690
		private DirectoryObjectClass targetClassField;

		// Token: 0x04004903 RID: 18691
		private bool targetClassFieldSpecified;
	}
}
