using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008BE RID: 2238
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirSyncStatusValue
	{
		// Token: 0x17002759 RID: 10073
		// (get) Token: 0x06006E93 RID: 28307 RVA: 0x001764A7 File Offset: 0x001746A7
		// (set) Token: 0x06006E94 RID: 28308 RVA: 0x001764AF File Offset: 0x001746AF
		[XmlAttribute]
		public string AttributeSetName
		{
			get
			{
				return this.attributeSetNameField;
			}
			set
			{
				this.attributeSetNameField = value;
			}
		}

		// Token: 0x1700275A RID: 10074
		// (get) Token: 0x06006E95 RID: 28309 RVA: 0x001764B8 File Offset: 0x001746B8
		// (set) Token: 0x06006E96 RID: 28310 RVA: 0x001764C0 File Offset: 0x001746C0
		[XmlAttribute]
		public DirSyncState State
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
			}
		}

		// Token: 0x1700275B RID: 10075
		// (get) Token: 0x06006E97 RID: 28311 RVA: 0x001764C9 File Offset: 0x001746C9
		// (set) Token: 0x06006E98 RID: 28312 RVA: 0x001764D1 File Offset: 0x001746D1
		[XmlAttribute(DataType = "positiveInteger")]
		public string Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x040047E2 RID: 18402
		private string attributeSetNameField;

		// Token: 0x040047E3 RID: 18403
		private DirSyncState stateField;

		// Token: 0x040047E4 RID: 18404
		private string versionField;
	}
}
