using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000844 RID: 2116
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(CloudMSExchDelegateListLink))]
	[XmlInclude(typeof(MSExchDelegateListLink))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public abstract class DirectoryLinkUserToUser : DirectoryLink
	{
		// Token: 0x060068F2 RID: 26866 RVA: 0x001715DF File Offset: 0x0016F7DF
		public DirectoryLinkUserToUser()
		{
			this.sourceClassField = DirectoryObjectClass.User;
			this.targetClassField = DirectoryObjectClass.User;
		}

		// Token: 0x17002525 RID: 9509
		// (get) Token: 0x060068F3 RID: 26867 RVA: 0x001715F7 File Offset: 0x0016F7F7
		// (set) Token: 0x060068F4 RID: 26868 RVA: 0x001715FF File Offset: 0x0016F7FF
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

		// Token: 0x17002526 RID: 9510
		// (get) Token: 0x060068F5 RID: 26869 RVA: 0x00171608 File Offset: 0x0016F808
		// (set) Token: 0x060068F6 RID: 26870 RVA: 0x00171610 File Offset: 0x0016F810
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

		// Token: 0x17002527 RID: 9511
		// (get) Token: 0x060068F7 RID: 26871 RVA: 0x00171619 File Offset: 0x0016F819
		// (set) Token: 0x060068F8 RID: 26872 RVA: 0x00171621 File Offset: 0x0016F821
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

		// Token: 0x17002528 RID: 9512
		// (get) Token: 0x060068F9 RID: 26873 RVA: 0x0017162A File Offset: 0x0016F82A
		// (set) Token: 0x060068FA RID: 26874 RVA: 0x00171632 File Offset: 0x0016F832
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

		// Token: 0x040044EA RID: 17642
		private DirectoryObjectClass sourceClassField;

		// Token: 0x040044EB RID: 17643
		private bool sourceClassFieldSpecified;

		// Token: 0x040044EC RID: 17644
		private DirectoryObjectClass targetClassField;

		// Token: 0x040044ED RID: 17645
		private bool targetClassFieldSpecified;
	}
}
