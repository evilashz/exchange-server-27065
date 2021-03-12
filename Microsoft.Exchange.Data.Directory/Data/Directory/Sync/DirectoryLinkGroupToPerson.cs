using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000855 RID: 2133
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(MSExchCoManagedByLink))]
	[XmlInclude(typeof(ManagedBy))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public abstract class DirectoryLinkGroupToPerson : DirectoryLink
	{
		// Token: 0x06006B11 RID: 27409 RVA: 0x00173339 File Offset: 0x00171539
		public DirectoryLinkGroupToPerson()
		{
			this.sourceClassField = DirectoryObjectClass.Group;
		}

		// Token: 0x1700261D RID: 9757
		// (get) Token: 0x06006B12 RID: 27410 RVA: 0x00173348 File Offset: 0x00171548
		// (set) Token: 0x06006B13 RID: 27411 RVA: 0x00173350 File Offset: 0x00171550
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

		// Token: 0x1700261E RID: 9758
		// (get) Token: 0x06006B14 RID: 27412 RVA: 0x00173359 File Offset: 0x00171559
		// (set) Token: 0x06006B15 RID: 27413 RVA: 0x00173361 File Offset: 0x00171561
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

		// Token: 0x1700261F RID: 9759
		// (get) Token: 0x06006B16 RID: 27414 RVA: 0x0017336A File Offset: 0x0017156A
		// (set) Token: 0x06006B17 RID: 27415 RVA: 0x00173372 File Offset: 0x00171572
		[XmlAttribute]
		public DirectoryObjectClassPerson TargetClass
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

		// Token: 0x040045E2 RID: 17890
		private DirectoryObjectClass sourceClassField;

		// Token: 0x040045E3 RID: 17891
		private bool sourceClassFieldSpecified;

		// Token: 0x040045E4 RID: 17892
		private DirectoryObjectClassPerson targetClassField;
	}
}
