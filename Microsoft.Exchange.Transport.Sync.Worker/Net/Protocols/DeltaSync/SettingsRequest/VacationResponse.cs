using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000123 RID: 291
	[XmlType(TypeName = "VacationResponse", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class VacationResponse
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0001BEB6 File Offset: 0x0001A0B6
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0001BEBE File Offset: 0x0001A0BE
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

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0001BECE File Offset: 0x0001A0CE
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x0001BED6 File Offset: 0x0001A0D6
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

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0001BEDF File Offset: 0x0001A0DF
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x0001BEE7 File Offset: 0x0001A0E7
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

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
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

		// Token: 0x040004CC RID: 1228
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Mode", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public VacationResponseMode internalMode;

		// Token: 0x040004CD RID: 1229
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalModeSpecified;

		// Token: 0x040004CE RID: 1230
		[XmlElement(ElementName = "StartTime", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalStartTime;

		// Token: 0x040004CF RID: 1231
		[XmlElement(ElementName = "EndTime", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalEndTime;

		// Token: 0x040004D0 RID: 1232
		[XmlElement(ElementName = "Message", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalMessage;
	}
}
