using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000103 RID: 259
	[XmlType(TypeName = "StatusType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class StatusType
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001AFA6 File Offset: 0x000191A6
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0001AFAE File Offset: 0x000191AE
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

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0001AFBE File Offset: 0x000191BE
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0001AFD9 File Offset: 0x000191D9
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

		// Token: 0x04000438 RID: 1080
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x04000439 RID: 1081
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x0400043A RID: 1082
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Fault), ElementName = "Fault", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public Fault internalFault;
	}
}
