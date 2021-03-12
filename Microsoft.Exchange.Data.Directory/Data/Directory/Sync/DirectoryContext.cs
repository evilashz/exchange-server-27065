using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008A8 RID: 2216
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryContext
	{
		// Token: 0x06006E0C RID: 28172 RVA: 0x00176028 File Offset: 0x00174228
		public DirectoryContext()
		{
			this.inScopeField = true;
		}

		// Token: 0x17002720 RID: 10016
		// (get) Token: 0x06006E0D RID: 28173 RVA: 0x00176037 File Offset: 0x00174237
		// (set) Token: 0x06006E0E RID: 28174 RVA: 0x0017603F File Offset: 0x0017423F
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

		// Token: 0x17002721 RID: 10017
		// (get) Token: 0x06006E0F RID: 28175 RVA: 0x00176048 File Offset: 0x00174248
		// (set) Token: 0x06006E10 RID: 28176 RVA: 0x00176050 File Offset: 0x00174250
		[DefaultValue(true)]
		[XmlAttribute]
		public bool InScope
		{
			get
			{
				return this.inScopeField;
			}
			set
			{
				this.inScopeField = value;
			}
		}

		// Token: 0x040047A4 RID: 18340
		private string contextIdField;

		// Token: 0x040047A5 RID: 18341
		private bool inScopeField;
	}
}
