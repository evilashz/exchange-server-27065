using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000857 RID: 2135
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[XmlInclude(typeof(Manager))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public abstract class DirectoryLinkPersonToPerson : DirectoryLink
	{
		// Token: 0x17002620 RID: 9760
		// (get) Token: 0x06006B1D RID: 27421 RVA: 0x001733D9 File Offset: 0x001715D9
		// (set) Token: 0x06006B1E RID: 27422 RVA: 0x001733E1 File Offset: 0x001715E1
		[XmlAttribute]
		public DirectoryObjectClassPerson SourceClass
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

		// Token: 0x17002621 RID: 9761
		// (get) Token: 0x06006B1F RID: 27423 RVA: 0x001733EA File Offset: 0x001715EA
		// (set) Token: 0x06006B20 RID: 27424 RVA: 0x001733F2 File Offset: 0x001715F2
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

		// Token: 0x040045E5 RID: 17893
		private DirectoryObjectClassPerson sourceClassField;

		// Token: 0x040045E6 RID: 17894
		private DirectoryObjectClassPerson targetClassField;
	}
}
