using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000159 RID: 345
	[XmlType(TypeName = "VacationResponse", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class VacationResponse
	{
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0001D0DA File Offset: 0x0001B2DA
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x0001D0E2 File Offset: 0x0001B2E2
		[XmlIgnore]
		public VacationResponseMode Mode
		{
			get
			{
				return this.internalMode;
			}
			set
			{
				this.internalMode = value;
				this.internalModeSpecified = true;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0001D0F2 File Offset: 0x0001B2F2
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0001D0FA File Offset: 0x0001B2FA
		[XmlIgnore]
		public string StartTime
		{
			get
			{
				return this.internalStartTime;
			}
			set
			{
				this.internalStartTime = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0001D103 File Offset: 0x0001B303
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0001D10B File Offset: 0x0001B30B
		[XmlIgnore]
		public string EndTime
		{
			get
			{
				return this.internalEndTime;
			}
			set
			{
				this.internalEndTime = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0001D114 File Offset: 0x0001B314
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x0001D11C File Offset: 0x0001B31C
		[XmlIgnore]
		public string Message
		{
			get
			{
				return this.internalMessage;
			}
			set
			{
				this.internalMessage = value;
			}
		}

		// Token: 0x040005A5 RID: 1445
		[XmlElement(ElementName = "Mode", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public VacationResponseMode internalMode;

		// Token: 0x040005A6 RID: 1446
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalModeSpecified;

		// Token: 0x040005A7 RID: 1447
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "StartTime", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalStartTime;

		// Token: 0x040005A8 RID: 1448
		[XmlElement(ElementName = "EndTime", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalEndTime;

		// Token: 0x040005A9 RID: 1449
		[XmlElement(ElementName = "Message", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalMessage;
	}
}
