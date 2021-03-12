using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200033C RID: 828
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetNonIndexableItemStatisticsType : BaseRequestType
	{
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x000290C2 File Offset: 0x000272C2
		// (set) Token: 0x06001AAD RID: 6829 RVA: 0x000290CA File Offset: 0x000272CA
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

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x000290D3 File Offset: 0x000272D3
		// (set) Token: 0x06001AAF RID: 6831 RVA: 0x000290DB File Offset: 0x000272DB
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

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x000290E4 File Offset: 0x000272E4
		// (set) Token: 0x06001AB1 RID: 6833 RVA: 0x000290EC File Offset: 0x000272EC
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

		// Token: 0x040011D6 RID: 4566
		private string[] mailboxesField;

		// Token: 0x040011D7 RID: 4567
		private bool searchArchiveOnlyField;

		// Token: 0x040011D8 RID: 4568
		private bool searchArchiveOnlyFieldSpecified;
	}
}
