using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002EF RID: 751
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ManagedFolderInformationType
	{
		// Token: 0x040012BC RID: 4796
		public bool CanDelete;

		// Token: 0x040012BD RID: 4797
		[XmlIgnore]
		public bool CanDeleteSpecified;

		// Token: 0x040012BE RID: 4798
		public bool CanRenameOrMove;

		// Token: 0x040012BF RID: 4799
		[XmlIgnore]
		public bool CanRenameOrMoveSpecified;

		// Token: 0x040012C0 RID: 4800
		public bool MustDisplayComment;

		// Token: 0x040012C1 RID: 4801
		[XmlIgnore]
		public bool MustDisplayCommentSpecified;

		// Token: 0x040012C2 RID: 4802
		public bool HasQuota;

		// Token: 0x040012C3 RID: 4803
		[XmlIgnore]
		public bool HasQuotaSpecified;

		// Token: 0x040012C4 RID: 4804
		public bool IsManagedFoldersRoot;

		// Token: 0x040012C5 RID: 4805
		[XmlIgnore]
		public bool IsManagedFoldersRootSpecified;

		// Token: 0x040012C6 RID: 4806
		public string ManagedFolderId;

		// Token: 0x040012C7 RID: 4807
		public string Comment;

		// Token: 0x040012C8 RID: 4808
		public int StorageQuota;

		// Token: 0x040012C9 RID: 4809
		[XmlIgnore]
		public bool StorageQuotaSpecified;

		// Token: 0x040012CA RID: 4810
		public int FolderSize;

		// Token: 0x040012CB RID: 4811
		[XmlIgnore]
		public bool FolderSizeSpecified;

		// Token: 0x040012CC RID: 4812
		public string HomePage;
	}
}
