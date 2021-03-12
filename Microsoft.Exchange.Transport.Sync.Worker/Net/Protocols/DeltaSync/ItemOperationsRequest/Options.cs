using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest
{
	// Token: 0x020000BA RID: 186
	[XmlType(TypeName = "Options", Namespace = "ItemOperations:")]
	[Serializable]
	public class Options
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001A0B5 File Offset: 0x000182B5
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0001A0D0 File Offset: 0x000182D0
		[XmlIgnore]
		public Report Report
		{
			get
			{
				if (this.internalReport == null)
				{
					this.internalReport = new Report();
				}
				return this.internalReport;
			}
			set
			{
				this.internalReport = value;
			}
		}

		// Token: 0x0400039E RID: 926
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Report), ElementName = "Report", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public Report internalReport;
	}
}
