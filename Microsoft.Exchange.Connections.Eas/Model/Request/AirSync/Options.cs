using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSync
{
	// Token: 0x02000097 RID: 151
	[XmlType(Namespace = "AirSync", TypeName = "Options")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Options
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00009CE9 File Offset: 0x00007EE9
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00009CF1 File Offset: 0x00007EF1
		[XmlElement(ElementName = "Class")]
		public string Class { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00009CFA File Offset: 0x00007EFA
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00009D02 File Offset: 0x00007F02
		[XmlElement(ElementName = "BodyPreference", Namespace = "AirSyncBase")]
		public BodyPreference BodyPreference { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00009D0B File Offset: 0x00007F0B
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00009D13 File Offset: 0x00007F13
		[XmlElement(ElementName = "Conflict")]
		public byte? Conflict { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00009D1C File Offset: 0x00007F1C
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00009D24 File Offset: 0x00007F24
		[XmlElement(ElementName = "FilterType")]
		public byte? FilterType { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00009D2D File Offset: 0x00007F2D
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00009D35 File Offset: 0x00007F35
		[XmlElement(ElementName = "MIMESupport")]
		public byte? MimeSupport { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00009D3E File Offset: 0x00007F3E
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00009D46 File Offset: 0x00007F46
		[XmlElement(ElementName = "MIMETruncation")]
		public byte? MimeTruncation { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00009D4F File Offset: 0x00007F4F
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00009D57 File Offset: 0x00007F57
		[XmlElement(ElementName = "MaxItems")]
		public int? MaxItems { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00009D60 File Offset: 0x00007F60
		[XmlIgnore]
		public bool ConflictSpecified
		{
			get
			{
				return this.Conflict != null;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00009D7C File Offset: 0x00007F7C
		[XmlIgnore]
		public bool FilterTypeSpecified
		{
			get
			{
				return this.FilterType != null;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00009D98 File Offset: 0x00007F98
		[XmlIgnore]
		public bool MimeSupportSpecified
		{
			get
			{
				return this.MimeSupport != null;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00009DB4 File Offset: 0x00007FB4
		[XmlIgnore]
		public bool MimeTruncationSpecified
		{
			get
			{
				return this.MimeTruncation != null;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00009DD0 File Offset: 0x00007FD0
		[XmlIgnore]
		public bool MaxItemsSpecified
		{
			get
			{
				return this.MaxItems != null;
			}
		}
	}
}
