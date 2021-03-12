using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003B8 RID: 952
	[DataServiceKey("SelectionTarget")]
	[Serializable]
	public class MailFilterListReport : FfoReportObject
	{
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x0008C8CA File Offset: 0x0008AACA
		// (set) Token: 0x060021D6 RID: 8662 RVA: 0x0008C8D2 File Offset: 0x0008AAD2
		[Column(Name = "Organization")]
		public string Organization { get; internal set; }

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0008C8DB File Offset: 0x0008AADB
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x0008C8E3 File Offset: 0x0008AAE3
		[Column(Name = "SelectionTarget")]
		[ODataInput("SelectionTarget")]
		public string SelectionTarget { get; internal set; }

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x0008C8EC File Offset: 0x0008AAEC
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x0008C8F4 File Offset: 0x0008AAF4
		[Column(Name = "Display")]
		public string Display { get; internal set; }

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x0008C8FD File Offset: 0x0008AAFD
		// (set) Token: 0x060021DC RID: 8668 RVA: 0x0008C905 File Offset: 0x0008AB05
		[Column(Name = "Value")]
		public string Value { get; internal set; }

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x0008C90E File Offset: 0x0008AB0E
		// (set) Token: 0x060021DE RID: 8670 RVA: 0x0008C916 File Offset: 0x0008AB16
		[Column(Name = "ParentTarget")]
		public string ParentTarget { get; internal set; }

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x0008C91F File Offset: 0x0008AB1F
		// (set) Token: 0x060021E0 RID: 8672 RVA: 0x0008C927 File Offset: 0x0008AB27
		[Column(Name = "ParentValue")]
		public string ParentValue { get; internal set; }

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0008C930 File Offset: 0x0008AB30
		// (set) Token: 0x060021E2 RID: 8674 RVA: 0x0008C938 File Offset: 0x0008AB38
		[ODataInput("Domain")]
		public string Domain { get; private set; }
	}
}
