using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000945 RID: 2373
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class CredentialValue
	{
		// Token: 0x170027DA RID: 10202
		// (get) Token: 0x0600700E RID: 28686 RVA: 0x0017711C File Offset: 0x0017531C
		// (set) Token: 0x0600700F RID: 28687 RVA: 0x00177124 File Offset: 0x00175324
		[XmlAttribute]
		public CredentialType CredentialType
		{
			get
			{
				return this.credentialTypeField;
			}
			set
			{
				this.credentialTypeField = value;
			}
		}

		// Token: 0x170027DB RID: 10203
		// (get) Token: 0x06007010 RID: 28688 RVA: 0x0017712D File Offset: 0x0017532D
		// (set) Token: 0x06007011 RID: 28689 RVA: 0x00177135 File Offset: 0x00175335
		[XmlAttribute]
		public string KeyStoreId
		{
			get
			{
				return this.keyStoreIdField;
			}
			set
			{
				this.keyStoreIdField = value;
			}
		}

		// Token: 0x170027DC RID: 10204
		// (get) Token: 0x06007012 RID: 28690 RVA: 0x0017713E File Offset: 0x0017533E
		// (set) Token: 0x06007013 RID: 28691 RVA: 0x00177146 File Offset: 0x00175346
		[XmlAttribute]
		public string KeyGroupId
		{
			get
			{
				return this.keyGroupIdField;
			}
			set
			{
				this.keyGroupIdField = value;
			}
		}

		// Token: 0x040048B6 RID: 18614
		private CredentialType credentialTypeField;

		// Token: 0x040048B7 RID: 18615
		private string keyStoreIdField;

		// Token: 0x040048B8 RID: 18616
		private string keyGroupIdField;
	}
}
