using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000606 RID: 1542
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "CalendarPermission")]
	[Serializable]
	public class CalendarPermissionType : BasePermissionType
	{
		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x000B41C4 File Offset: 0x000B23C4
		// (set) Token: 0x06002F80 RID: 12160 RVA: 0x000B41CC File Offset: 0x000B23CC
		[IgnoreDataMember]
		[XmlElement]
		public CalendarPermissionReadAccess? ReadItems { get; set; }

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x000B41D8 File Offset: 0x000B23D8
		// (set) Token: 0x06002F82 RID: 12162 RVA: 0x000B420A File Offset: 0x000B240A
		[DataMember(Name = "ReadItems", EmitDefaultValue = false, Order = 1)]
		[XmlIgnore]
		public string ReadItemsString
		{
			get
			{
				if (this.ReadItems == null)
				{
					return null;
				}
				return EnumUtilities.ToString<CalendarPermissionReadAccess>(this.ReadItems.Value);
			}
			set
			{
				this.ReadItems = new CalendarPermissionReadAccess?(EnumUtilities.Parse<CalendarPermissionReadAccess>(value));
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x000B4220 File Offset: 0x000B2420
		// (set) Token: 0x06002F84 RID: 12164 RVA: 0x000B423B File Offset: 0x000B243B
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ReadItemsSpecified
		{
			get
			{
				return this.ReadItems != null;
			}
			set
			{
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x000B423D File Offset: 0x000B243D
		// (set) Token: 0x06002F86 RID: 12166 RVA: 0x000B4245 File Offset: 0x000B2445
		[XmlElement("CalendarPermissionLevel")]
		[IgnoreDataMember]
		public CalendarPermissionLevelType CalendarPermissionLevel { get; set; }

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x000B424E File Offset: 0x000B244E
		// (set) Token: 0x06002F88 RID: 12168 RVA: 0x000B425B File Offset: 0x000B245B
		[XmlIgnore]
		[DataMember(Name = "CalendarPermissionLevel", EmitDefaultValue = false, Order = 2)]
		public string CalendarPermissionLevelString
		{
			get
			{
				return EnumUtilities.ToString<CalendarPermissionLevelType>(this.CalendarPermissionLevel);
			}
			set
			{
				this.CalendarPermissionLevel = EnumUtilities.Parse<CalendarPermissionLevelType>(value);
			}
		}
	}
}
