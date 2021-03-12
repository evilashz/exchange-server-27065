using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000108 RID: 264
	[XmlType(TypeName = "Condition", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Condition
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0001B255 File Offset: 0x00019455
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x0001B270 File Offset: 0x00019470
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

		// Token: 0x04000449 RID: 1097
		[XmlElement(Type = typeof(Clause), ElementName = "Clause", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Clause internalClause;
	}
}
