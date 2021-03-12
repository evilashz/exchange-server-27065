using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000214 RID: 532
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SearchParametersType
	{
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x00026195 File Offset: 0x00024395
		// (set) Token: 0x06001511 RID: 5393 RVA: 0x0002619D File Offset: 0x0002439D
		public RestrictionType Restriction
		{
			get
			{
				return this.restrictionField;
			}
			set
			{
				this.restrictionField = value;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x000261A6 File Offset: 0x000243A6
		// (set) Token: 0x06001513 RID: 5395 RVA: 0x000261AE File Offset: 0x000243AE
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderIdType), IsNullable = false)]
		public BaseFolderIdType[] BaseFolderIds
		{
			get
			{
				return this.baseFolderIdsField;
			}
			set
			{
				this.baseFolderIdsField = value;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x000261B7 File Offset: 0x000243B7
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x000261BF File Offset: 0x000243BF
		[XmlAttribute]
		public SearchFolderTraversalType Traversal
		{
			get
			{
				return this.traversalField;
			}
			set
			{
				this.traversalField = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x000261C8 File Offset: 0x000243C8
		// (set) Token: 0x06001517 RID: 5399 RVA: 0x000261D0 File Offset: 0x000243D0
		[XmlIgnore]
		public bool TraversalSpecified
		{
			get
			{
				return this.traversalFieldSpecified;
			}
			set
			{
				this.traversalFieldSpecified = value;
			}
		}

		// Token: 0x04000E84 RID: 3716
		private RestrictionType restrictionField;

		// Token: 0x04000E85 RID: 3717
		private BaseFolderIdType[] baseFolderIdsField;

		// Token: 0x04000E86 RID: 3718
		private SearchFolderTraversalType traversalField;

		// Token: 0x04000E87 RID: 3719
		private bool traversalFieldSpecified;
	}
}
