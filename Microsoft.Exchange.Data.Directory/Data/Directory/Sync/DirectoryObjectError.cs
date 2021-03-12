using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200094F RID: 2383
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryObjectError
	{
		// Token: 0x170027E7 RID: 10215
		// (get) Token: 0x06007030 RID: 28720 RVA: 0x00177239 File Offset: 0x00175439
		// (set) Token: 0x06007031 RID: 28721 RVA: 0x00177241 File Offset: 0x00175441
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

		// Token: 0x170027E8 RID: 10216
		// (get) Token: 0x06007032 RID: 28722 RVA: 0x0017724A File Offset: 0x0017544A
		// (set) Token: 0x06007033 RID: 28723 RVA: 0x00177252 File Offset: 0x00175452
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

		// Token: 0x170027E9 RID: 10217
		// (get) Token: 0x06007034 RID: 28724 RVA: 0x0017725B File Offset: 0x0017545B
		// (set) Token: 0x06007035 RID: 28725 RVA: 0x00177263 File Offset: 0x00175463
		[XmlIgnore]
		public bool ObjectClassSpecified
		{
			get
			{
				return this.objectClassFieldSpecified;
			}
			set
			{
				this.objectClassFieldSpecified = value;
			}
		}

		// Token: 0x170027EA RID: 10218
		// (get) Token: 0x06007036 RID: 28726 RVA: 0x0017726C File Offset: 0x0017546C
		// (set) Token: 0x06007037 RID: 28727 RVA: 0x00177274 File Offset: 0x00175474
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

		// Token: 0x170027EB RID: 10219
		// (get) Token: 0x06007038 RID: 28728 RVA: 0x0017727D File Offset: 0x0017547D
		// (set) Token: 0x06007039 RID: 28729 RVA: 0x00177285 File Offset: 0x00175485
		[XmlAttribute]
		public DirectoryObjectErrorCode ErrorCode
		{
			get
			{
				return this.errorCodeField;
			}
			set
			{
				this.errorCodeField = value;
			}
		}

		// Token: 0x170027EC RID: 10220
		// (get) Token: 0x0600703A RID: 28730 RVA: 0x0017728E File Offset: 0x0017548E
		// (set) Token: 0x0600703B RID: 28731 RVA: 0x00177296 File Offset: 0x00175496
		[XmlAttribute]
		public string ErrorDetail
		{
			get
			{
				return this.errorDetailField;
			}
			set
			{
				this.errorDetailField = value;
			}
		}

		// Token: 0x040048CA RID: 18634
		private string contextIdField;

		// Token: 0x040048CB RID: 18635
		private DirectoryObjectClass objectClassField;

		// Token: 0x040048CC RID: 18636
		private bool objectClassFieldSpecified;

		// Token: 0x040048CD RID: 18637
		private string objectIdField;

		// Token: 0x040048CE RID: 18638
		private DirectoryObjectErrorCode errorCodeField;

		// Token: 0x040048CF RID: 18639
		private string errorDetailField;
	}
}
