using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000D2 RID: 210
	[XmlType(TypeName = "ReportAsNotSpam", Namespace = "ItemOperations:")]
	[Serializable]
	public class ReportAsNotSpam
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001A6B1 File Offset: 0x000188B1
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001A6B9 File Offset: 0x000188B9
		[XmlIgnore]
		public string ServerId
		{
			get
			{
				return this.internalServerId;
			}
			set
			{
				this.internalServerId = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001A6C2 File Offset: 0x000188C2
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001A6CA File Offset: 0x000188CA
		[XmlIgnore]
		public int Status
		{
			get
			{
				return this.internalStatus;
			}
			set
			{
				this.internalStatus = value;
				this.internalStatusSpecified = true;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001A6DA File Offset: 0x000188DA
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001A6F5 File Offset: 0x000188F5
		[XmlIgnore]
		public SubResultType Report
		{
			get
			{
				if (this.internalReport == null)
				{
					this.internalReport = new SubResultType();
				}
				return this.internalReport;
			}
			set
			{
				this.internalReport = value;
			}
		}

		// Token: 0x040003CE RID: 974
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "ItemOperations:")]
		public string internalServerId;

		// Token: 0x040003CF RID: 975
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x040003D0 RID: 976
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040003D1 RID: 977
		[XmlElement(Type = typeof(SubResultType), ElementName = "Report", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubResultType internalReport;
	}
}
