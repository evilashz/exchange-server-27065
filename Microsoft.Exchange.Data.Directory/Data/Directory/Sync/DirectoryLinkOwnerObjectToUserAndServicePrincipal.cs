using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200085B RID: 2139
	[DesignerCategory("code")]
	[XmlInclude(typeof(Owner))]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public abstract class DirectoryLinkOwnerObjectToUserAndServicePrincipal : DirectoryLink
	{
		// Token: 0x17002626 RID: 9766
		// (get) Token: 0x06006B35 RID: 27445 RVA: 0x00173589 File Offset: 0x00171789
		// (set) Token: 0x06006B36 RID: 27446 RVA: 0x00173591 File Offset: 0x00171791
		[XmlAttribute]
		public DirectoryObjectClassContainsOwner SourceClass
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

		// Token: 0x17002627 RID: 9767
		// (get) Token: 0x06006B37 RID: 27447 RVA: 0x0017359A File Offset: 0x0017179A
		// (set) Token: 0x06006B38 RID: 27448 RVA: 0x001735A2 File Offset: 0x001717A2
		[XmlAttribute]
		public DirectoryObjectClassUserAndServicePrincipal TargetClass
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

		// Token: 0x040045EB RID: 17899
		private DirectoryObjectClassContainsOwner sourceClassField;

		// Token: 0x040045EC RID: 17900
		private DirectoryObjectClassUserAndServicePrincipal targetClassField;
	}
}
