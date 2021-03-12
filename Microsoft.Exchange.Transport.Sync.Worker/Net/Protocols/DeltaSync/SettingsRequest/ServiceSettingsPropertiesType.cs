using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000114 RID: 276
	[XmlType(TypeName = "ServiceSettingsPropertiesType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ServiceSettingsPropertiesType
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0001B4EE File Offset: 0x000196EE
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x0001B4F6 File Offset: 0x000196F6
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

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001B506 File Offset: 0x00019706
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x0001B50E File Offset: 0x0001970E
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

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001B51E File Offset: 0x0001971E
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x0001B526 File Offset: 0x00019726
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

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001B536 File Offset: 0x00019736
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x0001B53E File Offset: 0x0001973E
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

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001B54E File Offset: 0x0001974E
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x0001B556 File Offset: 0x00019756
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

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001B566 File Offset: 0x00019766
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x0001B56E File Offset: 0x0001976E
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

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001B57E File Offset: 0x0001977E
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x0001B586 File Offset: 0x00019786
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

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001B596 File Offset: 0x00019796
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x0001B59E File Offset: 0x0001979E
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

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0001B5AE File Offset: 0x000197AE
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x0001B5B6 File Offset: 0x000197B6
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

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001B5C6 File Offset: 0x000197C6
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x0001B5CE File Offset: 0x000197CE
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

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001B5DE File Offset: 0x000197DE
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0001B5E6 File Offset: 0x000197E6
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

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0001B5F6 File Offset: 0x000197F6
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0001B5FE File Offset: 0x000197FE
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

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0001B60E File Offset: 0x0001980E
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0001B616 File Offset: 0x00019816
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

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001B61F File Offset: 0x0001981F
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x0001B627 File Offset: 0x00019827
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

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0001B630 File Offset: 0x00019830
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x0001B638 File Offset: 0x00019838
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

		// Token: 0x0400045C RID: 1116
		[XmlElement(ElementName = "ServiceTimeOut", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalServiceTimeOut;

		// Token: 0x0400045D RID: 1117
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalServiceTimeOutSpecified;

		// Token: 0x0400045E RID: 1118
		[XmlElement(ElementName = "MinSyncPollInterval", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMinSyncPollInterval;

		// Token: 0x0400045F RID: 1119
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMinSyncPollIntervalSpecified;

		// Token: 0x04000460 RID: 1120
		[XmlElement(ElementName = "MinSettingsPollInterval", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMinSettingsPollInterval;

		// Token: 0x04000461 RID: 1121
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMinSettingsPollIntervalSpecified;

		// Token: 0x04000462 RID: 1122
		[XmlElement(ElementName = "SyncMultiplier", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "double", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public double internalSyncMultiplier;

		// Token: 0x04000463 RID: 1123
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalSyncMultiplierSpecified;

		// Token: 0x04000464 RID: 1124
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxObjectsInSync", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxObjectsInSync;

		// Token: 0x04000465 RID: 1125
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxObjectsInSyncSpecified;

		// Token: 0x04000466 RID: 1126
		[XmlElement(ElementName = "MaxNumberOfEmailAdds", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxNumberOfEmailAdds;

		// Token: 0x04000467 RID: 1127
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxNumberOfEmailAddsSpecified;

		// Token: 0x04000468 RID: 1128
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxNumberOfFolderAdds", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxNumberOfFolderAdds;

		// Token: 0x04000469 RID: 1129
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxNumberOfFolderAddsSpecified;

		// Token: 0x0400046A RID: 1130
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxNumberOfStatelessObjects", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxNumberOfStatelessObjects;

		// Token: 0x0400046B RID: 1131
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxNumberOfStatelessObjectsSpecified;

		// Token: 0x0400046C RID: 1132
		[XmlElement(ElementName = "DefaultStatelessEmailWindowSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalDefaultStatelessEmailWindowSize;

		// Token: 0x0400046D RID: 1133
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalDefaultStatelessEmailWindowSizeSpecified;

		// Token: 0x0400046E RID: 1134
		[XmlElement(ElementName = "MaxStatelessEmailWindowSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxStatelessEmailWindowSize;

		// Token: 0x0400046F RID: 1135
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxStatelessEmailWindowSizeSpecified;

		// Token: 0x04000470 RID: 1136
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxTotalLengthOfForwardingAddresses", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxTotalLengthOfForwardingAddresses;

		// Token: 0x04000471 RID: 1137
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxTotalLengthOfForwardingAddressesSpecified;

		// Token: 0x04000472 RID: 1138
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxVacationResponseMessageLength", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxVacationResponseMessageLength;

		// Token: 0x04000473 RID: 1139
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxVacationResponseMessageLengthSpecified;

		// Token: 0x04000474 RID: 1140
		[XmlElement(ElementName = "MinVacationResponseStartTime", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalMinVacationResponseStartTime;

		// Token: 0x04000475 RID: 1141
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxVacationResponseEndTime", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalMaxVacationResponseEndTime;

		// Token: 0x04000476 RID: 1142
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxNumberOfProvisionCommands", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxNumberOfProvisionCommands;

		// Token: 0x04000477 RID: 1143
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxNumberOfProvisionCommandsSpecified;
	}
}
