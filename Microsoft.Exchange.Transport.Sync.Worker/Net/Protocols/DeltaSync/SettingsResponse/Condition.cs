using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000144 RID: 324
	[XmlType(TypeName = "Condition", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Condition
	{
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0001C5E9 File Offset: 0x0001A7E9
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x0001C604 File Offset: 0x0001A804
		[XmlIgnore]
		public Clause Clause
		{
			get
			{
				if (this.internalClause == null)
				{
					this.internalClause = new Clause();
				}
				return this.internalClause;
			}
			set
			{
				this.internalClause = value;
			}
		}

		// Token: 0x04000528 RID: 1320
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Clause), ElementName = "Clause", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public Clause internalClause;
	}
}
