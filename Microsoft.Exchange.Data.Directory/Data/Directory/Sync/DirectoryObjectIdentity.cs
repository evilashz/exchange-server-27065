using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200094E RID: 2382
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[Serializable]
	public class DirectoryObjectIdentity
	{
		// Token: 0x170027E4 RID: 10212
		// (get) Token: 0x06007029 RID: 28713 RVA: 0x001771FE File Offset: 0x001753FE
		// (set) Token: 0x0600702A RID: 28714 RVA: 0x00177206 File Offset: 0x00175406
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

		// Token: 0x170027E5 RID: 10213
		// (get) Token: 0x0600702B RID: 28715 RVA: 0x0017720F File Offset: 0x0017540F
		// (set) Token: 0x0600702C RID: 28716 RVA: 0x00177217 File Offset: 0x00175417
		[XmlAttribute]
		public DirectoryObjectClass ObjectClass
		{
			get
			{
				return this.objectClassField;
			}
			set
			{
				this.objectClassField = value;
			}
		}

		// Token: 0x170027E6 RID: 10214
		// (get) Token: 0x0600702D RID: 28717 RVA: 0x00177220 File Offset: 0x00175420
		// (set) Token: 0x0600702E RID: 28718 RVA: 0x00177228 File Offset: 0x00175428
		[XmlAttribute]
		public string ObjectId
		{
			get
			{
				return this.objectIdField;
			}
			set
			{
				this.objectIdField = value;
			}
		}

		// Token: 0x040048C7 RID: 18631
		private string contextIdField;

		// Token: 0x040048C8 RID: 18632
		private DirectoryObjectClass objectClassField;

		// Token: 0x040048C9 RID: 18633
		private string objectIdField;
	}
}
