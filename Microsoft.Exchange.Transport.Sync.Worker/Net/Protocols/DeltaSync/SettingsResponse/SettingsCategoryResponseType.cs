using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200015C RID: 348
	[XmlType(TypeName = "SettingsCategoryResponseType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class SettingsCategoryResponseType
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0001D215 File Offset: 0x0001B415
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x0001D21D File Offset: 0x0001B41D
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

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0001D22D File Offset: 0x0001B42D
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x0001D248 File Offset: 0x0001B448
		[XmlIgnore]
		public Fault Fault
		{
			get
			{
				if (this.internalFault == null)
				{
					this.internalFault = new Fault();
				}
				return this.internalFault;
			}
			set
			{
				this.internalFault = value;
			}
		}

		// Token: 0x040005AE RID: 1454
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x040005AF RID: 1455
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040005B0 RID: 1456
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Fault), ElementName = "Fault", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public Fault internalFault;
	}
}
