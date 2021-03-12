using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000268 RID: 616
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class FindFolderParentType
	{
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x000272C5 File Offset: 0x000254C5
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x000272CD File Offset: 0x000254CD
		[XmlArrayItem("CalendarFolder", typeof(CalendarFolderType), IsNullable = false)]
		[XmlArrayItem("Folder", typeof(FolderType), IsNullable = false)]
		[XmlArrayItem("TasksFolder", typeof(TasksFolderType), IsNullable = false)]
		[XmlArrayItem("ContactsFolder", typeof(ContactsFolderType), IsNullable = false)]
		[XmlArrayItem("SearchFolder", typeof(SearchFolderType), IsNullable = false)]
		public BaseFolderType[] Folders
		{
			get
			{
				return this.foldersField;
			}
			set
			{
				this.foldersField = value;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x000272D6 File Offset: 0x000254D6
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x000272DE File Offset: 0x000254DE
		[XmlAttribute]
		public int IndexedPagingOffset
		{
			get
			{
				return this.indexedPagingOffsetField;
			}
			set
			{
				this.indexedPagingOffsetField = value;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x000272E7 File Offset: 0x000254E7
		// (set) Token: 0x0600171F RID: 5919 RVA: 0x000272EF File Offset: 0x000254EF
		[XmlIgnore]
		public bool IndexedPagingOffsetSpecified
		{
			get
			{
				return this.indexedPagingOffsetFieldSpecified;
			}
			set
			{
				this.indexedPagingOffsetFieldSpecified = value;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x000272F8 File Offset: 0x000254F8
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x00027300 File Offset: 0x00025500
		[XmlAttribute]
		public int NumeratorOffset
		{
			get
			{
				return this.numeratorOffsetField;
			}
			set
			{
				this.numeratorOffsetField = value;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x00027309 File Offset: 0x00025509
		// (set) Token: 0x06001723 RID: 5923 RVA: 0x00027311 File Offset: 0x00025511
		[XmlIgnore]
		public bool NumeratorOffsetSpecified
		{
			get
			{
				return this.numeratorOffsetFieldSpecified;
			}
			set
			{
				this.numeratorOffsetFieldSpecified = value;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0002731A File Offset: 0x0002551A
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x00027322 File Offset: 0x00025522
		[XmlAttribute]
		public int AbsoluteDenominator
		{
			get
			{
				return this.absoluteDenominatorField;
			}
			set
			{
				this.absoluteDenominatorField = value;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0002732B File Offset: 0x0002552B
		// (set) Token: 0x06001727 RID: 5927 RVA: 0x00027333 File Offset: 0x00025533
		[XmlIgnore]
		public bool AbsoluteDenominatorSpecified
		{
			get
			{
				return this.absoluteDenominatorFieldSpecified;
			}
			set
			{
				this.absoluteDenominatorFieldSpecified = value;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x0002733C File Offset: 0x0002553C
		// (set) Token: 0x06001729 RID: 5929 RVA: 0x00027344 File Offset: 0x00025544
		[XmlAttribute]
		public bool IncludesLastItemInRange
		{
			get
			{
				return this.includesLastItemInRangeField;
			}
			set
			{
				this.includesLastItemInRangeField = value;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x0002734D File Offset: 0x0002554D
		// (set) Token: 0x0600172B RID: 5931 RVA: 0x00027355 File Offset: 0x00025555
		[XmlIgnore]
		public bool IncludesLastItemInRangeSpecified
		{
			get
			{
				return this.includesLastItemInRangeFieldSpecified;
			}
			set
			{
				this.includesLastItemInRangeFieldSpecified = value;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0002735E File Offset: 0x0002555E
		// (set) Token: 0x0600172D RID: 5933 RVA: 0x00027366 File Offset: 0x00025566
		[XmlAttribute]
		public int TotalItemsInView
		{
			get
			{
				return this.totalItemsInViewField;
			}
			set
			{
				this.totalItemsInViewField = value;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0002736F File Offset: 0x0002556F
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x00027377 File Offset: 0x00025577
		[XmlIgnore]
		public bool TotalItemsInViewSpecified
		{
			get
			{
				return this.totalItemsInViewFieldSpecified;
			}
			set
			{
				this.totalItemsInViewFieldSpecified = value;
			}
		}

		// Token: 0x04000FA5 RID: 4005
		private BaseFolderType[] foldersField;

		// Token: 0x04000FA6 RID: 4006
		private int indexedPagingOffsetField;

		// Token: 0x04000FA7 RID: 4007
		private bool indexedPagingOffsetFieldSpecified;

		// Token: 0x04000FA8 RID: 4008
		private int numeratorOffsetField;

		// Token: 0x04000FA9 RID: 4009
		private bool numeratorOffsetFieldSpecified;

		// Token: 0x04000FAA RID: 4010
		private int absoluteDenominatorField;

		// Token: 0x04000FAB RID: 4011
		private bool absoluteDenominatorFieldSpecified;

		// Token: 0x04000FAC RID: 4012
		private bool includesLastItemInRangeField;

		// Token: 0x04000FAD RID: 4013
		private bool includesLastItemInRangeFieldSpecified;

		// Token: 0x04000FAE RID: 4014
		private int totalItemsInViewField;

		// Token: 0x04000FAF RID: 4015
		private bool totalItemsInViewFieldSpecified;
	}
}
