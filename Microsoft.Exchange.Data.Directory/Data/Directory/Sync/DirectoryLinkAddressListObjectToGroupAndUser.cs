using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000957 RID: 2391
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[XmlInclude(typeof(PublicDelegates))]
	[Serializable]
	public abstract class DirectoryLinkAddressListObjectToGroupAndUser : DirectoryLink
	{
		// Token: 0x1700281E RID: 10270
		// (get) Token: 0x060070A6 RID: 28838 RVA: 0x00177637 File Offset: 0x00175837
		// (set) Token: 0x060070A7 RID: 28839 RVA: 0x0017763F File Offset: 0x0017583F
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

		// Token: 0x1700281F RID: 10271
		// (get) Token: 0x060070A8 RID: 28840 RVA: 0x00177648 File Offset: 0x00175848
		// (set) Token: 0x060070A9 RID: 28841 RVA: 0x00177650 File Offset: 0x00175850
		[XmlAttribute]
		public DirectoryObjectClassGroupAndUser TargetClass
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

		// Token: 0x0400490C RID: 18700
		private DirectoryObjectClassAddressList sourceClassField;

		// Token: 0x0400490D RID: 18701
		private DirectoryObjectClassGroupAndUser targetClassField;
	}
}
