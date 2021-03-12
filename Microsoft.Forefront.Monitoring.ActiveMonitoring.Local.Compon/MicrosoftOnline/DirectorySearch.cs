using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000E2 RID: 226
	[XmlInclude(typeof(SearchForUsersValue))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public abstract class DirectorySearch
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x0001F7FA File Offset: 0x0001D9FA
		public DirectorySearch()
		{
			this.propertyField = new string[0];
			this.includeMetadataField = false;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001F815 File Offset: 0x0001DA15
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0001F81D File Offset: 0x0001DA1D
		[XmlAttribute]
		public int PageSize
		{
			get
			{
				return this.pageSizeField;
			}
			set
			{
				this.pageSizeField = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001F826 File Offset: 0x0001DA26
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0001F82E File Offset: 0x0001DA2E
		[XmlAttribute]
		public int SortBufferSize
		{
			get
			{
				return this.sortBufferSizeField;
			}
			set
			{
				this.sortBufferSizeField = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001F837 File Offset: 0x0001DA37
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0001F83F File Offset: 0x0001DA3F
		[XmlAttribute]
		public SortOrder SortOrder
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001F848 File Offset: 0x0001DA48
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001F850 File Offset: 0x0001DA50
		[XmlAttribute]
		public SortProperty SortProperty
		{
			get
			{
				return this.sortPropertyField;
			}
			set
			{
				this.sortPropertyField = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001F859 File Offset: 0x0001DA59
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0001F861 File Offset: 0x0001DA61
		[XmlAttribute]
		public string[] Property
		{
			get
			{
				return this.propertyField;
			}
			set
			{
				this.propertyField = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001F86A File Offset: 0x0001DA6A
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0001F872 File Offset: 0x0001DA72
		[DefaultValue(false)]
		[XmlAttribute]
		public bool IncludeMetadata
		{
			get
			{
				return this.includeMetadataField;
			}
			set
			{
				this.includeMetadataField = value;
			}
		}

		// Token: 0x04000387 RID: 903
		private int pageSizeField;

		// Token: 0x04000388 RID: 904
		private int sortBufferSizeField;

		// Token: 0x04000389 RID: 905
		private SortOrder sortOrderField;

		// Token: 0x0400038A RID: 906
		private SortProperty sortPropertyField;

		// Token: 0x0400038B RID: 907
		private string[] propertyField;

		// Token: 0x0400038C RID: 908
		private bool includeMetadataField;
	}
}
