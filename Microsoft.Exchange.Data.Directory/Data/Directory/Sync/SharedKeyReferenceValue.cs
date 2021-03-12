using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200091B RID: 2331
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SharedKeyReferenceValue
	{
		// Token: 0x17002790 RID: 10128
		// (get) Token: 0x06006F58 RID: 28504 RVA: 0x00176B14 File Offset: 0x00174D14
		// (set) Token: 0x06006F59 RID: 28505 RVA: 0x00176B1C File Offset: 0x00174D1C
		[XmlAttribute]
		public int Version
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

		// Token: 0x17002791 RID: 10129
		// (get) Token: 0x06006F5A RID: 28506 RVA: 0x00176B25 File Offset: 0x00174D25
		// (set) Token: 0x06006F5B RID: 28507 RVA: 0x00176B2D File Offset: 0x00174D2D
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

		// Token: 0x17002792 RID: 10130
		// (get) Token: 0x06006F5C RID: 28508 RVA: 0x00176B36 File Offset: 0x00174D36
		// (set) Token: 0x06006F5D RID: 28509 RVA: 0x00176B3E File Offset: 0x00174D3E
		[XmlAttribute]
		public string ContextId
		{
			get
			{
				return this.contextIdField;
			}
			set
			{
				this.contextIdField = value;
			}
		}

		// Token: 0x0400483C RID: 18492
		private int versionField;

		// Token: 0x0400483D RID: 18493
		private string keyGroupIdField;

		// Token: 0x0400483E RID: 18494
		private string contextIdField;
	}
}
