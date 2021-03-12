using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200033A RID: 826
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetNonIndexableItemDetailsType : BaseRequestType
	{
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x00029032 File Offset: 0x00027232
		// (set) Token: 0x06001A9C RID: 6812 RVA: 0x0002903A File Offset: 0x0002723A
		[XmlArrayItem("LegacyDN", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Mailboxes
		{
			get
			{
				return this.mailboxesField;
			}
			set
			{
				this.mailboxesField = value;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x00029043 File Offset: 0x00027243
		// (set) Token: 0x06001A9E RID: 6814 RVA: 0x0002904B File Offset: 0x0002724B
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

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x00029054 File Offset: 0x00027254
		// (set) Token: 0x06001AA0 RID: 6816 RVA: 0x0002905C File Offset: 0x0002725C
		[XmlIgnore]
		public bool PageSizeSpecified
		{
			get
			{
				return this.pageSizeFieldSpecified;
			}
			set
			{
				this.pageSizeFieldSpecified = value;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00029065 File Offset: 0x00027265
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x0002906D File Offset: 0x0002726D
		public string PageItemReference
		{
			get
			{
				return this.pageItemReferenceField;
			}
			set
			{
				this.pageItemReferenceField = value;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x00029076 File Offset: 0x00027276
		// (set) Token: 0x06001AA4 RID: 6820 RVA: 0x0002907E File Offset: 0x0002727E
		public SearchPageDirectionType PageDirection
		{
			get
			{
				return this.pageDirectionField;
			}
			set
			{
				this.pageDirectionField = value;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x00029087 File Offset: 0x00027287
		// (set) Token: 0x06001AA6 RID: 6822 RVA: 0x0002908F File Offset: 0x0002728F
		[XmlIgnore]
		public bool PageDirectionSpecified
		{
			get
			{
				return this.pageDirectionFieldSpecified;
			}
			set
			{
				this.pageDirectionFieldSpecified = value;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x00029098 File Offset: 0x00027298
		// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x000290A0 File Offset: 0x000272A0
		public bool SearchArchiveOnly
		{
			get
			{
				return this.searchArchiveOnlyField;
			}
			set
			{
				this.searchArchiveOnlyField = value;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x000290A9 File Offset: 0x000272A9
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x000290B1 File Offset: 0x000272B1
		[XmlIgnore]
		public bool SearchArchiveOnlySpecified
		{
			get
			{
				return this.searchArchiveOnlyFieldSpecified;
			}
			set
			{
				this.searchArchiveOnlyFieldSpecified = value;
			}
		}

		// Token: 0x040011CB RID: 4555
		private string[] mailboxesField;

		// Token: 0x040011CC RID: 4556
		private int pageSizeField;

		// Token: 0x040011CD RID: 4557
		private bool pageSizeFieldSpecified;

		// Token: 0x040011CE RID: 4558
		private string pageItemReferenceField;

		// Token: 0x040011CF RID: 4559
		private SearchPageDirectionType pageDirectionField;

		// Token: 0x040011D0 RID: 4560
		private bool pageDirectionFieldSpecified;

		// Token: 0x040011D1 RID: 4561
		private bool searchArchiveOnlyField;

		// Token: 0x040011D2 RID: 4562
		private bool searchArchiveOnlyFieldSpecified;
	}
}
