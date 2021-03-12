using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000859 RID: 2137
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(Member))]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public abstract class DirectoryLinkMemberObjectToAddressListObject : DirectoryLink
	{
		// Token: 0x17002624 RID: 9764
		// (get) Token: 0x06006B2B RID: 27435 RVA: 0x001734A9 File Offset: 0x001716A9
		// (set) Token: 0x06006B2C RID: 27436 RVA: 0x001734B1 File Offset: 0x001716B1
		[XmlAttribute]
		public DirectoryObjectClassContainsMember SourceClass
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

		// Token: 0x17002625 RID: 9765
		// (get) Token: 0x06006B2D RID: 27437 RVA: 0x001734BA File Offset: 0x001716BA
		// (set) Token: 0x06006B2E RID: 27438 RVA: 0x001734C2 File Offset: 0x001716C2
		[XmlAttribute]
		public DirectoryObjectClassCanBeMember TargetClass
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

		// Token: 0x040045E9 RID: 17897
		private DirectoryObjectClassContainsMember sourceClassField;

		// Token: 0x040045EA RID: 17898
		private DirectoryObjectClassCanBeMember targetClassField;
	}
}
