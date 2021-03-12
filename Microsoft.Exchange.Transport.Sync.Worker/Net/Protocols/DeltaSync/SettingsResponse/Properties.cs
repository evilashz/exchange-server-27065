using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000161 RID: 353
	[XmlType(TypeName = "Properties", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Properties
	{
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0001D41E File Offset: 0x0001B61E
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0001D426 File Offset: 0x0001B626
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

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0001D436 File Offset: 0x0001B636
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x0001D451 File Offset: 0x0001B651
		[XmlIgnore]
		public ServiceSettingsPropertiesType Get
		{
			get
			{
				if (this.internalGet == null)
				{
					this.internalGet = new ServiceSettingsPropertiesType();
				}
				return this.internalGet;
			}
			set
			{
				this.internalGet = value;
			}
		}

		// Token: 0x040005C2 RID: 1474
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalStatus;

		// Token: 0x040005C3 RID: 1475
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040005C4 RID: 1476
		[XmlElement(Type = typeof(ServiceSettingsPropertiesType), ElementName = "Get", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ServiceSettingsPropertiesType internalGet;
	}
}
