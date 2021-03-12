using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002FF RID: 767
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class MobileDevice : ADConfigurationObject
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x0009A612 File Offset: 0x00098812
		// (set) Token: 0x0600239F RID: 9119 RVA: 0x0009A624 File Offset: 0x00098824
		public string FriendlyName
		{
			get
			{
				return (string)this[MobileDeviceSchema.FriendlyName];
			}
			internal set
			{
				this[MobileDeviceSchema.FriendlyName] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.FriendlyName, value);
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x0009A642 File Offset: 0x00098842
		// (set) Token: 0x060023A1 RID: 9121 RVA: 0x0009A654 File Offset: 0x00098854
		public string DeviceId
		{
			get
			{
				return (string)this[MobileDeviceSchema.DeviceId];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceId] = value;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x0009A662 File Offset: 0x00098862
		// (set) Token: 0x060023A3 RID: 9123 RVA: 0x0009A674 File Offset: 0x00098874
		public string DeviceImei
		{
			get
			{
				return (string)this[MobileDeviceSchema.DeviceImei];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceImei] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.DeviceImei, value);
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x0009A692 File Offset: 0x00098892
		// (set) Token: 0x060023A5 RID: 9125 RVA: 0x0009A6A4 File Offset: 0x000988A4
		public string DeviceMobileOperator
		{
			get
			{
				return (string)this[MobileDeviceSchema.DeviceMobileOperator];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceMobileOperator] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.DeviceMobileOperator, value);
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0009A6C2 File Offset: 0x000988C2
		// (set) Token: 0x060023A7 RID: 9127 RVA: 0x0009A6D4 File Offset: 0x000988D4
		public string DeviceOS
		{
			get
			{
				return (string)this[MobileDeviceSchema.DeviceOS];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceOS] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.DeviceOS, value);
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x0009A6F2 File Offset: 0x000988F2
		// (set) Token: 0x060023A9 RID: 9129 RVA: 0x0009A704 File Offset: 0x00098904
		public string DeviceOSLanguage
		{
			get
			{
				return (string)this[MobileDeviceSchema.DeviceOSLanguage];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceOSLanguage] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.DeviceOSLanguage, value);
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x0009A722 File Offset: 0x00098922
		// (set) Token: 0x060023AB RID: 9131 RVA: 0x0009A734 File Offset: 0x00098934
		public string DeviceTelephoneNumber
		{
			get
			{
				return (string)this[MobileDeviceSchema.DeviceTelephoneNumber];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceTelephoneNumber] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.DeviceTelephoneNumber, value);
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0009A752 File Offset: 0x00098952
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x0009A764 File Offset: 0x00098964
		public string DeviceType
		{
			get
			{
				return (string)this[MobileDeviceSchema.DeviceType];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceType] = value;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x0009A772 File Offset: 0x00098972
		// (set) Token: 0x060023AF RID: 9135 RVA: 0x0009A784 File Offset: 0x00098984
		public string DeviceUserAgent
		{
			get
			{
				return (string)this[MobileDeviceSchema.DeviceUserAgent];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceUserAgent] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.DeviceUserAgent, value);
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x0009A7A2 File Offset: 0x000989A2
		// (set) Token: 0x060023B1 RID: 9137 RVA: 0x0009A7B4 File Offset: 0x000989B4
		public string DeviceModel
		{
			get
			{
				return (string)this[MobileDeviceSchema.DeviceModel];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceModel] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.DeviceModel, value);
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x0009A7D4 File Offset: 0x000989D4
		// (set) Token: 0x060023B3 RID: 9139 RVA: 0x0009A816 File Offset: 0x00098A16
		public DateTime? FirstSyncTime
		{
			get
			{
				DateTime? result = this[MobileDeviceSchema.FirstSyncTime] as DateTime?;
				if (result != null)
				{
					return new DateTime?(result.Value.ToUniversalTime());
				}
				return result;
			}
			internal set
			{
				this[MobileDeviceSchema.FirstSyncTime] = value;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x0009A829 File Offset: 0x00098A29
		// (set) Token: 0x060023B5 RID: 9141 RVA: 0x0009A83B File Offset: 0x00098A3B
		public string UserDisplayName
		{
			get
			{
				return (string)this[MobileDeviceSchema.UserDisplayName];
			}
			internal set
			{
				this[MobileDeviceSchema.UserDisplayName] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.UserDisplayName, value);
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x0009A859 File Offset: 0x00098A59
		// (set) Token: 0x060023B7 RID: 9143 RVA: 0x0009A86B File Offset: 0x00098A6B
		public DeviceAccessState DeviceAccessState
		{
			get
			{
				return (DeviceAccessState)this[MobileDeviceSchema.DeviceAccessState];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceAccessState] = value;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x0009A87E File Offset: 0x00098A7E
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x0009A890 File Offset: 0x00098A90
		public DeviceAccessStateReason DeviceAccessStateReason
		{
			get
			{
				return (DeviceAccessStateReason)this[MobileDeviceSchema.DeviceAccessStateReason];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceAccessStateReason] = value;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x0009A8A3 File Offset: 0x00098AA3
		// (set) Token: 0x060023BB RID: 9147 RVA: 0x0009A8B5 File Offset: 0x00098AB5
		public ADObjectId DeviceAccessControlRule
		{
			get
			{
				return (ADObjectId)this[MobileDeviceSchema.DeviceAccessControlRule];
			}
			internal set
			{
				this[MobileDeviceSchema.DeviceAccessControlRule] = value;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x0009A8C3 File Offset: 0x00098AC3
		// (set) Token: 0x060023BD RID: 9149 RVA: 0x0009A8D5 File Offset: 0x00098AD5
		public string ClientVersion
		{
			get
			{
				return (string)this[MobileDeviceSchema.ClientVersion];
			}
			internal set
			{
				this[MobileDeviceSchema.ClientVersion] = MobileDevice.TrimStringValue(this.Schema, MobileDeviceSchema.ClientVersion, value);
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x0009A8F3 File Offset: 0x00098AF3
		// (set) Token: 0x060023BF RID: 9151 RVA: 0x0009A905 File Offset: 0x00098B05
		public MobileClientType ClientType
		{
			get
			{
				return (MobileClientType)this[MobileDeviceSchema.ClientType];
			}
			internal set
			{
				this[MobileDeviceSchema.ClientType] = value;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x0009A918 File Offset: 0x00098B18
		// (set) Token: 0x060023C1 RID: 9153 RVA: 0x0009A92A File Offset: 0x00098B2A
		public bool IsManaged
		{
			get
			{
				return (bool)this[MobileDeviceSchema.IsManaged];
			}
			internal set
			{
				this[MobileDeviceSchema.IsManaged] = value;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x0009A93D File Offset: 0x00098B3D
		// (set) Token: 0x060023C3 RID: 9155 RVA: 0x0009A94F File Offset: 0x00098B4F
		public bool IsCompliant
		{
			get
			{
				return (bool)this[MobileDeviceSchema.IsCompliant];
			}
			internal set
			{
				this[MobileDeviceSchema.IsCompliant] = value;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x0009A962 File Offset: 0x00098B62
		// (set) Token: 0x060023C5 RID: 9157 RVA: 0x0009A974 File Offset: 0x00098B74
		public bool IsDisabled
		{
			get
			{
				return (bool)this[MobileDeviceSchema.IsDisabled];
			}
			internal set
			{
				this[MobileDeviceSchema.IsDisabled] = value;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x0009A987 File Offset: 0x00098B87
		internal override ADObjectSchema Schema
		{
			get
			{
				return MobileDevice.schema;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x0009A98E File Offset: 0x00098B8E
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchActiveSyncDevice";
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x0009A995 File Offset: 0x00098B95
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x0009A99C File Offset: 0x00098B9C
		internal static ADObjectId GetRootId(ADUser adUser)
		{
			return adUser.Id.GetChildId("ExchangeActiveSyncDevices");
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x0009A9AE File Offset: 0x00098BAE
		internal static ADObjectId GetRootId(ADObjectId adUserId)
		{
			return adUserId.GetChildId("ExchangeActiveSyncDevices");
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x0009A9BC File Offset: 0x00098BBC
		internal static int GetStringConstraintLength(ADObjectSchema schema, ADPropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			schema.InitializeAutogeneratedConstraints();
			int num = int.MaxValue;
			foreach (PropertyDefinitionConstraint propertyDefinitionConstraint in propertyDefinition.AllConstraints)
			{
				StringLengthConstraint stringLengthConstraint = propertyDefinitionConstraint as StringLengthConstraint;
				if (stringLengthConstraint != null && stringLengthConstraint.MaxLength < num)
				{
					num = stringLengthConstraint.MaxLength;
				}
			}
			return num;
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x0009AA4C File Offset: 0x00098C4C
		internal static string TrimStringValue(ADObjectSchema schema, ADPropertyDefinition propertyDefinition, string value)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			int stringConstraintLength = MobileDevice.GetStringConstraintLength(schema, propertyDefinition);
			if (value.Length > stringConstraintLength)
			{
				return value.Remove(stringConstraintLength);
			}
			return value;
		}

		// Token: 0x0400162C RID: 5676
		internal const string MostDerivedClass = "msExchActiveSyncDevice";

		// Token: 0x0400162D RID: 5677
		private static MobileDeviceSchema schema = ObjectSchema.GetInstance<MobileDeviceSchema>();
	}
}
