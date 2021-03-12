using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000840 RID: 2112
	[XmlInclude(typeof(AllowAccessTo))]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public abstract class DirectoryLinkAllowAccessTo : DirectoryLink
	{
		// Token: 0x1700251F RID: 9503
		// (get) Token: 0x060068DA RID: 26842 RVA: 0x0017142F File Offset: 0x0016F62F
		// (set) Token: 0x060068DB RID: 26843 RVA: 0x00171437 File Offset: 0x0016F637
		[XmlAttribute]
		public DirectoryObjectClassContainsAllowAccessTo SourceClass
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

		// Token: 0x17002520 RID: 9504
		// (get) Token: 0x060068DC RID: 26844 RVA: 0x00171440 File Offset: 0x0016F640
		// (set) Token: 0x060068DD RID: 26845 RVA: 0x00171448 File Offset: 0x0016F648
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

		// Token: 0x17002521 RID: 9505
		// (get) Token: 0x060068DE RID: 26846 RVA: 0x00171451 File Offset: 0x0016F651
		// (set) Token: 0x060068DF RID: 26847 RVA: 0x00171459 File Offset: 0x0016F659
		[XmlAttribute]
		public DirectoryObjectClassCanHaveAllowAccessTo TargetClass
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

		// Token: 0x17002522 RID: 9506
		// (get) Token: 0x060068E0 RID: 26848 RVA: 0x00171462 File Offset: 0x0016F662
		// (set) Token: 0x060068E1 RID: 26849 RVA: 0x0017146A File Offset: 0x0016F66A
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

		// Token: 0x040044E4 RID: 17636
		private DirectoryObjectClassContainsAllowAccessTo sourceClassField;

		// Token: 0x040044E5 RID: 17637
		private bool sourceClassFieldSpecified;

		// Token: 0x040044E6 RID: 17638
		private DirectoryObjectClassCanHaveAllowAccessTo targetClassField;

		// Token: 0x040044E7 RID: 17639
		private bool targetClassFieldSpecified;
	}
}
