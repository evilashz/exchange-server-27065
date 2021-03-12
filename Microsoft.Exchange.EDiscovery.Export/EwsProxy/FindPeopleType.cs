using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000384 RID: 900
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FindPeopleType : BaseRequestType
	{
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00029F9C File Offset: 0x0002819C
		// (set) Token: 0x06001C6F RID: 7279 RVA: 0x00029FA4 File Offset: 0x000281A4
		public PersonaResponseShapeType PersonaShape
		{
			get
			{
				return this.personaShapeField;
			}
			set
			{
				this.personaShapeField = value;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x00029FAD File Offset: 0x000281AD
		// (set) Token: 0x06001C71 RID: 7281 RVA: 0x00029FB5 File Offset: 0x000281B5
		public IndexedPageViewType IndexedPageItemView
		{
			get
			{
				return this.indexedPageItemViewField;
			}
			set
			{
				this.indexedPageItemViewField = value;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00029FBE File Offset: 0x000281BE
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x00029FC6 File Offset: 0x000281C6
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

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00029FCF File Offset: 0x000281CF
		// (set) Token: 0x06001C75 RID: 7285 RVA: 0x00029FD7 File Offset: 0x000281D7
		public RestrictionType AggregationRestriction
		{
			get
			{
				return this.aggregationRestrictionField;
			}
			set
			{
				this.aggregationRestrictionField = value;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00029FE0 File Offset: 0x000281E0
		// (set) Token: 0x06001C77 RID: 7287 RVA: 0x00029FE8 File Offset: 0x000281E8
		[XmlArrayItem("FieldOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FieldOrderType[] SortOrder
		{
			get
			{
				return this.sortOrderField;
			}
			set
			{
				this.sortOrderField = value;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00029FF1 File Offset: 0x000281F1
		// (set) Token: 0x06001C79 RID: 7289 RVA: 0x00029FF9 File Offset: 0x000281F9
		public TargetFolderIdType ParentFolderId
		{
			get
			{
				return this.parentFolderIdField;
			}
			set
			{
				this.parentFolderIdField = value;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x0002A002 File Offset: 0x00028202
		// (set) Token: 0x06001C7B RID: 7291 RVA: 0x0002A00A File Offset: 0x0002820A
		public string QueryString
		{
			get
			{
				return this.queryStringField;
			}
			set
			{
				this.queryStringField = value;
			}
		}

		// Token: 0x040012D8 RID: 4824
		private PersonaResponseShapeType personaShapeField;

		// Token: 0x040012D9 RID: 4825
		private IndexedPageViewType indexedPageItemViewField;

		// Token: 0x040012DA RID: 4826
		private RestrictionType restrictionField;

		// Token: 0x040012DB RID: 4827
		private RestrictionType aggregationRestrictionField;

		// Token: 0x040012DC RID: 4828
		private FieldOrderType[] sortOrderField;

		// Token: 0x040012DD RID: 4829
		private TargetFolderIdType parentFolderIdField;

		// Token: 0x040012DE RID: 4830
		private string queryStringField;
	}
}
