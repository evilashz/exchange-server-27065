using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200014A RID: 330
	[XmlType(TypeName = "ServiceSettingsPropertiesType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ServiceSettingsPropertiesType
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001C713 File Offset: 0x0001A913
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x0001C71B File Offset: 0x0001A91B
		[XmlIgnore]
		public int ServiceTimeOut
		{
			get
			{
				return this.internalServiceTimeOut;
			}
			set
			{
				this.internalServiceTimeOut = value;
				this.internalServiceTimeOutSpecified = true;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0001C72B File Offset: 0x0001A92B
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0001C733 File Offset: 0x0001A933
		[XmlIgnore]
		public int MinSyncPollInterval
		{
			get
			{
				return this.internalMinSyncPollInterval;
			}
			set
			{
				this.internalMinSyncPollInterval = value;
				this.internalMinSyncPollIntervalSpecified = true;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0001C743 File Offset: 0x0001A943
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0001C74B File Offset: 0x0001A94B
		[XmlIgnore]
		public int MinSettingsPollInterval
		{
			get
			{
				return this.internalMinSettingsPollInterval;
			}
			set
			{
				this.internalMinSettingsPollInterval = value;
				this.internalMinSettingsPollIntervalSpecified = true;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0001C75B File Offset: 0x0001A95B
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x0001C763 File Offset: 0x0001A963
		[XmlIgnore]
		public double SyncMultiplier
		{
			get
			{
				return this.internalSyncMultiplier;
			}
			set
			{
				this.internalSyncMultiplier = value;
				this.internalSyncMultiplierSpecified = true;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0001C773 File Offset: 0x0001A973
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x0001C77B File Offset: 0x0001A97B
		[XmlIgnore]
		public int MaxObjectsInSync
		{
			get
			{
				return this.internalMaxObjectsInSync;
			}
			set
			{
				this.internalMaxObjectsInSync = value;
				this.internalMaxObjectsInSyncSpecified = true;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0001C78B File Offset: 0x0001A98B
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x0001C793 File Offset: 0x0001A993
		[XmlIgnore]
		public int MaxNumberOfEmailAdds
		{
			get
			{
				return this.internalMaxNumberOfEmailAdds;
			}
			set
			{
				this.internalMaxNumberOfEmailAdds = value;
				this.internalMaxNumberOfEmailAddsSpecified = true;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0001C7A3 File Offset: 0x0001A9A3
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x0001C7AB File Offset: 0x0001A9AB
		[XmlIgnore]
		public int MaxNumberOfFolderAdds
		{
			get
			{
				return this.internalMaxNumberOfFolderAdds;
			}
			set
			{
				this.internalMaxNumberOfFolderAdds = value;
				this.internalMaxNumberOfFolderAddsSpecified = true;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0001C7BB File Offset: 0x0001A9BB
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x0001C7C3 File Offset: 0x0001A9C3
		[XmlIgnore]
		public int MaxNumberOfStatelessObjects
		{
			get
			{
				return this.internalMaxNumberOfStatelessObjects;
			}
			set
			{
				this.internalMaxNumberOfStatelessObjects = value;
				this.internalMaxNumberOfStatelessObjectsSpecified = true;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0001C7D3 File Offset: 0x0001A9D3
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x0001C7DB File Offset: 0x0001A9DB
		[XmlIgnore]
		public int DefaultStatelessEmailWindowSize
		{
			get
			{
				return this.internalDefaultStatelessEmailWindowSize;
			}
			set
			{
				this.internalDefaultStatelessEmailWindowSize = value;
				this.internalDefaultStatelessEmailWindowSizeSpecified = true;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0001C7EB File Offset: 0x0001A9EB
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x0001C7F3 File Offset: 0x0001A9F3
		[XmlIgnore]
		public int MaxStatelessEmailWindowSize
		{
			get
			{
				return this.internalMaxStatelessEmailWindowSize;
			}
			set
			{
				this.internalMaxStatelessEmailWindowSize = value;
				this.internalMaxStatelessEmailWindowSizeSpecified = true;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0001C803 File Offset: 0x0001AA03
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x0001C80B File Offset: 0x0001AA0B
		[XmlIgnore]
		public int MaxTotalLengthOfForwardingAddresses
		{
			get
			{
				return this.internalMaxTotalLengthOfForwardingAddresses;
			}
			set
			{
				this.internalMaxTotalLengthOfForwardingAddresses = value;
				this.internalMaxTotalLengthOfForwardingAddressesSpecified = true;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0001C81B File Offset: 0x0001AA1B
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x0001C823 File Offset: 0x0001AA23
		[XmlIgnore]
		public int MaxVacationResponseMessageLength
		{
			get
			{
				return this.internalMaxVacationResponseMessageLength;
			}
			set
			{
				this.internalMaxVacationResponseMessageLength = value;
				this.internalMaxVacationResponseMessageLengthSpecified = true;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x0001C833 File Offset: 0x0001AA33
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x0001C83B File Offset: 0x0001AA3B
		[XmlIgnore]
		public string MinVacationResponseStartTime
		{
			get
			{
				return this.internalMinVacationResponseStartTime;
			}
			set
			{
				this.internalMinVacationResponseStartTime = value;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0001C844 File Offset: 0x0001AA44
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x0001C84C File Offset: 0x0001AA4C
		[XmlIgnore]
		public string MaxVacationResponseEndTime
		{
			get
			{
				return this.internalMaxVacationResponseEndTime;
			}
			set
			{
				this.internalMaxVacationResponseEndTime = value;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0001C855 File Offset: 0x0001AA55
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x0001C85D File Offset: 0x0001AA5D
		[XmlIgnore]
		public int MaxNumberOfProvisionCommands
		{
			get
			{
				return this.internalMaxNumberOfProvisionCommands;
			}
			set
			{
				this.internalMaxNumberOfProvisionCommands = value;
				this.internalMaxNumberOfProvisionCommandsSpecified = true;
			}
		}

		// Token: 0x04000535 RID: 1333
		[XmlElement(ElementName = "ServiceTimeOut", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalServiceTimeOut;

		// Token: 0x04000536 RID: 1334
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalServiceTimeOutSpecified;

		// Token: 0x04000537 RID: 1335
		[XmlElement(ElementName = "MinSyncPollInterval", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMinSyncPollInterval;

		// Token: 0x04000538 RID: 1336
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMinSyncPollIntervalSpecified;

		// Token: 0x04000539 RID: 1337
		[XmlElement(ElementName = "MinSettingsPollInterval", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMinSettingsPollInterval;

		// Token: 0x0400053A RID: 1338
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMinSettingsPollIntervalSpecified;

		// Token: 0x0400053B RID: 1339
		[XmlElement(ElementName = "SyncMultiplier", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "double", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public double internalSyncMultiplier;

		// Token: 0x0400053C RID: 1340
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalSyncMultiplierSpecified;

		// Token: 0x0400053D RID: 1341
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxObjectsInSync", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxObjectsInSync;

		// Token: 0x0400053E RID: 1342
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxObjectsInSyncSpecified;

		// Token: 0x0400053F RID: 1343
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxNumberOfEmailAdds", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxNumberOfEmailAdds;

		// Token: 0x04000540 RID: 1344
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxNumberOfEmailAddsSpecified;

		// Token: 0x04000541 RID: 1345
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxNumberOfFolderAdds", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxNumberOfFolderAdds;

		// Token: 0x04000542 RID: 1346
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxNumberOfFolderAddsSpecified;

		// Token: 0x04000543 RID: 1347
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxNumberOfStatelessObjects", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxNumberOfStatelessObjects;

		// Token: 0x04000544 RID: 1348
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxNumberOfStatelessObjectsSpecified;

		// Token: 0x04000545 RID: 1349
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "DefaultStatelessEmailWindowSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalDefaultStatelessEmailWindowSize;

		// Token: 0x04000546 RID: 1350
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalDefaultStatelessEmailWindowSizeSpecified;

		// Token: 0x04000547 RID: 1351
		[XmlElement(ElementName = "MaxStatelessEmailWindowSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxStatelessEmailWindowSize;

		// Token: 0x04000548 RID: 1352
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxStatelessEmailWindowSizeSpecified;

		// Token: 0x04000549 RID: 1353
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxTotalLengthOfForwardingAddresses", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxTotalLengthOfForwardingAddresses;

		// Token: 0x0400054A RID: 1354
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxTotalLengthOfForwardingAddressesSpecified;

		// Token: 0x0400054B RID: 1355
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxVacationResponseMessageLength", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxVacationResponseMessageLength;

		// Token: 0x0400054C RID: 1356
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxVacationResponseMessageLengthSpecified;

		// Token: 0x0400054D RID: 1357
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MinVacationResponseStartTime", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalMinVacationResponseStartTime;

		// Token: 0x0400054E RID: 1358
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxVacationResponseEndTime", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalMaxVacationResponseEndTime;

		// Token: 0x0400054F RID: 1359
		[XmlElement(ElementName = "MaxNumberOfProvisionCommands", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxNumberOfProvisionCommands;

		// Token: 0x04000550 RID: 1360
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxNumberOfProvisionCommandsSpecified;
	}
}
