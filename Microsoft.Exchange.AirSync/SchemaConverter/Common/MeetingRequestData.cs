using System;
using System.Collections;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200018C RID: 396
	[Serializable]
	internal class MeetingRequestData : INestedData
	{
		// Token: 0x06001116 RID: 4374 RVA: 0x0005E082 File Offset: 0x0005C282
		public MeetingRequestData(int protocolVersion)
		{
			this.protocolVersion = protocolVersion;
			this.subProperties = new Hashtable(MeetingRequestData.keysV141.Length);
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x0005E0A3 File Offset: 0x0005C2A3
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x0005E0E2 File Offset: 0x0005C2E2
		public bool AllDayEvent
		{
			get
			{
				return this.subProperties.Contains(MeetingRequestData.keysPreV14[0]) && ((string)this.subProperties[MeetingRequestData.keysPreV14[0]]).Equals("1", StringComparison.Ordinal);
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[0]] = (value ? "1" : "0");
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0005E108 File Offset: 0x0005C308
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x0005E154 File Offset: 0x0005C354
		public bool DisallowNewTimeProposal
		{
			get
			{
				return this.subProperties.Contains(MeetingRequestData.keysV14[16]) && ((string)this.subProperties[MeetingRequestData.keysV14[16]]).Equals("1", StringComparison.Ordinal);
			}
			set
			{
				this.subProperties[MeetingRequestData.keysV14[16]] = (value ? "1" : "0");
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0005E178 File Offset: 0x0005C378
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x0005E1C8 File Offset: 0x0005C3C8
		public int BusyStatus
		{
			get
			{
				if (!this.subProperties.Contains(MeetingRequestData.keysPreV14[12]))
				{
					return -1;
				}
				int result;
				if (!int.TryParse((string)this.subProperties[MeetingRequestData.keysPreV14[12]], NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
				{
					return -1;
				}
				return result;
			}
			set
			{
				if (value >= MeetingRequestData.busyStatusArray.Length || value < 0)
				{
					this.subProperties[MeetingRequestData.keysPreV14[12]] = MeetingRequestData.busyStatusArray[2];
					return;
				}
				this.subProperties[MeetingRequestData.keysPreV14[12]] = MeetingRequestData.busyStatusArray[value];
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x0005E218 File Offset: 0x0005C418
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x0005E24E File Offset: 0x0005C44E
		public string[] Categories
		{
			get
			{
				if (this.subProperties.Contains(MeetingRequestData.keysPreV14[15]))
				{
					return (string[])this.subProperties[MeetingRequestData.keysPreV14[15]];
				}
				return new string[0];
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[15]] = value;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x0005E264 File Offset: 0x0005C464
		// (set) Token: 0x06001120 RID: 4384 RVA: 0x0005E2B7 File Offset: 0x0005C4B7
		public ExDateTime DtStamp
		{
			get
			{
				if (this.subProperties.Contains(MeetingRequestData.keysPreV14[2]))
				{
					return TimeZoneConverter.Parse((string)this.subProperties[MeetingRequestData.keysPreV14[2]], "yyyy-MM-dd\\THH:mm:ss.fff\\Z", this.protocolVersion, "DtStamp");
				}
				return ExDateTime.MinValue;
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[2]] = TimeZoneConverter.ToString(value, "yyyy-MM-dd\\THH:mm:ss.fff\\Z", this.protocolVersion);
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0005E2DC File Offset: 0x0005C4DC
		// (set) Token: 0x06001122 RID: 4386 RVA: 0x0005E32F File Offset: 0x0005C52F
		public ExDateTime EndTime
		{
			get
			{
				if (this.subProperties.Contains(MeetingRequestData.keysPreV14[3]))
				{
					return TimeZoneConverter.Parse((string)this.subProperties[MeetingRequestData.keysPreV14[3]], "yyyy-MM-dd\\THH:mm:ss.fff\\Z", this.protocolVersion, "EndTime");
				}
				return ExDateTime.MinValue;
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[3]] = TimeZoneConverter.ToString(value, "yyyy-MM-dd\\THH:mm:ss.fff\\Z", this.protocolVersion);
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0005E354 File Offset: 0x0005C554
		// (set) Token: 0x06001124 RID: 4388 RVA: 0x0005E389 File Offset: 0x0005C589
		public string GlobalObjId
		{
			get
			{
				if (this.subProperties.Contains(MeetingRequestData.keysPreV14[14]))
				{
					return (string)this.subProperties[MeetingRequestData.keysPreV14[14]];
				}
				return string.Empty;
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[14]] = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0005E3A0 File Offset: 0x0005C5A0
		// (set) Token: 0x06001126 RID: 4390 RVA: 0x0005E3EC File Offset: 0x0005C5EC
		public int InstanceType
		{
			get
			{
				if (!this.subProperties.Contains(MeetingRequestData.keysPreV14[4]))
				{
					return -1;
				}
				int result;
				if (int.TryParse((string)this.subProperties[MeetingRequestData.keysPreV14[4]], NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
				{
					return result;
				}
				return -1;
			}
			set
			{
				if (value >= MeetingRequestData.instanceTypeArray.Length || value < 0)
				{
					throw new ConversionException("InstanceType=" + value);
				}
				this.subProperties[MeetingRequestData.keysPreV14[4]] = MeetingRequestData.instanceTypeArray[value].ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0005E444 File Offset: 0x0005C644
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x0005E477 File Offset: 0x0005C677
		public string Location
		{
			get
			{
				if (this.subProperties.Contains(MeetingRequestData.keysPreV14[5]))
				{
					return (string)this.subProperties[MeetingRequestData.keysPreV14[5]];
				}
				return string.Empty;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.subProperties[MeetingRequestData.keysPreV14[5]] = value;
				}
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x0005E494 File Offset: 0x0005C694
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x0005E4C3 File Offset: 0x0005C6C3
		public EnhancedLocationData EnhancedLocation
		{
			get
			{
				if (this.subProperties.Contains(MeetingRequestData.keysPreV14[5]))
				{
					return (EnhancedLocationData)this.subProperties[MeetingRequestData.keysPreV14[5]];
				}
				return null;
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[5]] = value;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x0005E4D8 File Offset: 0x0005C6D8
		// (set) Token: 0x0600112C RID: 4396 RVA: 0x0005E50B File Offset: 0x0005C70B
		public string Organizer
		{
			get
			{
				if (this.subProperties.Contains(MeetingRequestData.keysPreV14[6]))
				{
					return (string)this.subProperties[MeetingRequestData.keysPreV14[6]];
				}
				return string.Empty;
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[6]] = value;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0005E520 File Offset: 0x0005C720
		// (set) Token: 0x0600112E RID: 4398 RVA: 0x0005E590 File Offset: 0x0005C790
		public ExDateTime RecurrenceId
		{
			get
			{
				if (!this.subProperties.Contains(MeetingRequestData.keysPreV14[7]))
				{
					return ExDateTime.MinValue;
				}
				ExDateTime result;
				if (!ExDateTime.TryParseExact((string)this.subProperties[MeetingRequestData.keysPreV14[7]], "yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
				{
					throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidDateTime, null, false)
					{
						ErrorStringForProtocolLogger = "InvalidDateTimeInMeetingRequestData3"
					};
				}
				return result;
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[7]] = value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo);
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0005E5B5 File Offset: 0x0005C7B5
		// (set) Token: 0x06001130 RID: 4400 RVA: 0x0005E5E6 File Offset: 0x0005C7E6
		public RecurrenceData Recurrences
		{
			get
			{
				if (this.subProperties.Contains(MeetingRequestData.keysPreV14[10]))
				{
					return (RecurrenceData)this.subProperties[MeetingRequestData.keysPreV14[10]];
				}
				return null;
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[10]] = value;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0005E5FC File Offset: 0x0005C7FC
		// (set) Token: 0x06001132 RID: 4402 RVA: 0x0005E648 File Offset: 0x0005C848
		public int Reminder
		{
			get
			{
				if (!this.subProperties.Contains(MeetingRequestData.keysPreV14[8]))
				{
					return -1;
				}
				int result;
				if (int.TryParse((string)this.subProperties[MeetingRequestData.keysPreV14[8]], NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
				{
					return result;
				}
				return -1;
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[8]] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0005E668 File Offset: 0x0005C868
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x0005E6B4 File Offset: 0x0005C8B4
		public bool ResponseRequested
		{
			get
			{
				return !this.subProperties.Contains(MeetingRequestData.keysPreV14[9]) || !((string)this.subProperties[MeetingRequestData.keysPreV14[9]]).Equals("0", StringComparison.Ordinal);
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[9]] = (value ? "1" : "0");
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0005E6D8 File Offset: 0x0005C8D8
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x0005E713 File Offset: 0x0005C913
		public int Sensitivity
		{
			get
			{
				if (this.subProperties.Contains(MeetingRequestData.keysPreV14[11]))
				{
					return int.Parse((string)this.subProperties[MeetingRequestData.keysPreV14[11]], CultureInfo.InvariantCulture);
				}
				return -1;
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[11]] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0005E734 File Offset: 0x0005C934
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x0005E787 File Offset: 0x0005C987
		public ExDateTime StartTime
		{
			get
			{
				if (this.subProperties.Contains(MeetingRequestData.keysPreV14[1]))
				{
					return TimeZoneConverter.Parse((string)this.subProperties[MeetingRequestData.keysPreV14[1]], "yyyy-MM-dd\\THH:mm:ss.fff\\Z", this.protocolVersion, "StartTime");
				}
				return ExDateTime.MinValue;
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[1]] = TimeZoneConverter.ToString(value, "yyyy-MM-dd\\THH:mm:ss.fff\\Z", this.protocolVersion);
			}
		}

		// Token: 0x17000685 RID: 1669
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x0005E7AC File Offset: 0x0005C9AC
		public AirSyncMeetingMessageType MeetingMessageType
		{
			set
			{
				IDictionary dictionary = this.subProperties;
				object key = MeetingRequestData.keysV141[17];
				int num = (int)value;
				dictionary[key] = num.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0005E7DA File Offset: 0x0005C9DA
		public IDictionary SubProperties
		{
			get
			{
				return this.subProperties;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0005E7E2 File Offset: 0x0005C9E2
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x0005E817 File Offset: 0x0005CA17
		public string TimeZoneSubProperty
		{
			get
			{
				if (this.subProperties.Contains(MeetingRequestData.keysPreV14[13]))
				{
					return (string)this.subProperties[MeetingRequestData.keysPreV14[13]];
				}
				return string.Empty;
			}
			set
			{
				this.subProperties[MeetingRequestData.keysPreV14[13]] = value;
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0005E82D File Offset: 0x0005CA2D
		public static string[] GetKeysForVersion(int version)
		{
			if (version < 140)
			{
				return MeetingRequestData.keysPreV14;
			}
			if (version == 140)
			{
				return MeetingRequestData.keysV14;
			}
			return MeetingRequestData.keysV141;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0005E850 File Offset: 0x0005CA50
		public static string GetEmailNamespaceForKey(int keyIndex)
		{
			if (keyIndex < MeetingRequestData.keysV14.Length)
			{
				return "Email:";
			}
			if (keyIndex < MeetingRequestData.keysV141.Length)
			{
				return "Email2:";
			}
			throw new InvalidOperationException(string.Format("keyIndex value {0} is invalid.", keyIndex));
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0005E887 File Offset: 0x0005CA87
		public bool HasSubProperty(MeetingRequestData.Tags tag)
		{
			return null != this.subProperties[MeetingRequestData.keysPreV14[(int)tag]];
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0005E8A1 File Offset: 0x0005CAA1
		public void Clear()
		{
			this.subProperties.Clear();
		}

		// Token: 0x04000AF0 RID: 2800
		private static readonly string[] keysPreV14 = new string[]
		{
			"AllDayEvent",
			"StartTime",
			"DtStamp",
			"EndTime",
			"InstanceType",
			"Location",
			"Organizer",
			"RecurrenceId",
			"Reminder",
			"ResponseRequested",
			"Recurrences",
			"Sensitivity",
			"IntDBusyStatus",
			"TimeZone",
			"GlobalObjId",
			"Categories"
		};

		// Token: 0x04000AF1 RID: 2801
		private static readonly string[] keysV14 = new string[]
		{
			"AllDayEvent",
			"StartTime",
			"DtStamp",
			"EndTime",
			"InstanceType",
			"Location",
			"Organizer",
			"RecurrenceId",
			"Reminder",
			"ResponseRequested",
			"Recurrences",
			"Sensitivity",
			"IntDBusyStatus",
			"TimeZone",
			"GlobalObjId",
			"Categories",
			"DisallowNewTimeProposal"
		};

		// Token: 0x04000AF2 RID: 2802
		private static readonly string[] keysV141 = new string[]
		{
			"AllDayEvent",
			"StartTime",
			"DtStamp",
			"EndTime",
			"InstanceType",
			"Location",
			"Organizer",
			"RecurrenceId",
			"Reminder",
			"ResponseRequested",
			"Recurrences",
			"Sensitivity",
			"IntDBusyStatus",
			"TimeZone",
			"GlobalObjId",
			"Categories",
			"DisallowNewTimeProposal",
			"MeetingMessageType"
		};

		// Token: 0x04000AF3 RID: 2803
		private static readonly string[] keysV160 = new string[]
		{
			"AllDayEvent",
			"StartTime",
			"DtStamp",
			"EndTime",
			"InstanceType",
			"Location",
			"Organizer",
			"Reminder",
			"ResponseRequested",
			"Recurrences",
			"Sensitivity",
			"IntDBusyStatus",
			"GlobalObjId",
			"Categories",
			"DisallowNewTimeProposal",
			"MeetingMessageType"
		};

		// Token: 0x04000AF4 RID: 2804
		private static readonly string[] busyStatusArray = new string[]
		{
			"0",
			"1",
			"2",
			"3"
		};

		// Token: 0x04000AF5 RID: 2805
		private static readonly int[] instanceTypeArray = new int[]
		{
			0,
			2,
			3,
			1
		};

		// Token: 0x04000AF6 RID: 2806
		private readonly int protocolVersion;

		// Token: 0x04000AF7 RID: 2807
		private IDictionary subProperties;

		// Token: 0x0200018D RID: 397
		public enum Tags
		{
			// Token: 0x04000AF9 RID: 2809
			AllDayEvent,
			// Token: 0x04000AFA RID: 2810
			StartTime,
			// Token: 0x04000AFB RID: 2811
			DtStamp,
			// Token: 0x04000AFC RID: 2812
			EndTime,
			// Token: 0x04000AFD RID: 2813
			InstanceType,
			// Token: 0x04000AFE RID: 2814
			Location,
			// Token: 0x04000AFF RID: 2815
			Organizer,
			// Token: 0x04000B00 RID: 2816
			RecurrenceId,
			// Token: 0x04000B01 RID: 2817
			Reminder,
			// Token: 0x04000B02 RID: 2818
			ResponseRequested,
			// Token: 0x04000B03 RID: 2819
			Recurrences,
			// Token: 0x04000B04 RID: 2820
			Sensitivity,
			// Token: 0x04000B05 RID: 2821
			BusyStatus,
			// Token: 0x04000B06 RID: 2822
			TimeZone,
			// Token: 0x04000B07 RID: 2823
			GlobalObjId,
			// Token: 0x04000B08 RID: 2824
			Categories,
			// Token: 0x04000B09 RID: 2825
			DisallowNewTimeProposal,
			// Token: 0x04000B0A RID: 2826
			MeetingMessageType
		}
	}
}
