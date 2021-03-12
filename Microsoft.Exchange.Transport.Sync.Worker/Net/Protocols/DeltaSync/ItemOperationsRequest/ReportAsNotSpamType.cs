using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest
{
	// Token: 0x020000B9 RID: 185
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "ReportAsNotSpamType", Namespace = "ItemOperations:")]
	[Serializable]
	public class ReportAsNotSpamType : ItemOpsBaseType
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001A089 File Offset: 0x00018289
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0001A0A4 File Offset: 0x000182A4
		[XmlIgnore]
		public Options Options
		{
			get
			{
				if (this.internalOptions == null)
				{
					this.internalOptions = new Options();
				}
				return this.internalOptions;
			}
			set
			{
				this.internalOptions = value;
			}
		}

		// Token: 0x0400039D RID: 925
		[XmlElement(Type = typeof(Options), ElementName = "Options", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Options internalOptions;
	}
}
