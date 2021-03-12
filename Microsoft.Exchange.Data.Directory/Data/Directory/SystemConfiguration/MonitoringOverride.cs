using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004FD RID: 1277
	[Serializable]
	public class MonitoringOverride : ADConfigurationObject
	{
		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x060038A0 RID: 14496 RVA: 0x000DB70E File Offset: 0x000D990E
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000DB718 File Offset: 0x000D9918
		internal static object ApplyVersionGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[MonitoringOverrideSchema.ApplyVersionRaw];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			object result;
			try
			{
				result = ServerVersion.ParseFromSerialNumber(text);
			}
			catch (FormatException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("ApplyVersion", ex.Message), MonitoringOverrideSchema.ApplyVersionRaw, propertyBag[MonitoringOverrideSchema.ApplyVersionRaw]), ex);
			}
			return result;
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000DB788 File Offset: 0x000D9988
		internal static void ApplyVersionSetter(object value, IPropertyBag propertyBag)
		{
			ServerVersion serverVersion = (ServerVersion)value;
			propertyBag[MonitoringOverrideSchema.ApplyVersionRaw] = serverVersion.ToString(true);
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000DB7B0 File Offset: 0x000D99B0
		internal static object ExpirationTimeGetter(IPropertyBag propertyBag)
		{
			string s = (string)propertyBag[MonitoringOverrideSchema.ExpirationTimeRaw];
			DateTime dateTime;
			if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime))
			{
				return dateTime.ToUniversalTime();
			}
			return null;
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000DB7F0 File Offset: 0x000D99F0
		internal static void ExpirationTimeSetter(object value, IPropertyBag propertyBag)
		{
			DateTime? dateTime = value as DateTime?;
			if (dateTime == null)
			{
				propertyBag[MonitoringOverrideSchema.ExpirationTimeRaw] = string.Empty;
			}
			propertyBag[MonitoringOverrideSchema.ExpirationTimeRaw] = dateTime.Value.ToString("u");
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x000DB841 File Offset: 0x000D9A41
		// (set) Token: 0x060038A6 RID: 14502 RVA: 0x000DB853 File Offset: 0x000D9A53
		public string HealthSet
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.HealthSet];
			}
			set
			{
				this[MonitoringOverrideSchema.HealthSet] = value;
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x060038A7 RID: 14503 RVA: 0x000DB861 File Offset: 0x000D9A61
		// (set) Token: 0x060038A8 RID: 14504 RVA: 0x000DB873 File Offset: 0x000D9A73
		public string MonitoringItemName
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.MonitoringItemName];
			}
			set
			{
				this[MonitoringOverrideSchema.MonitoringItemName] = value;
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x000DB881 File Offset: 0x000D9A81
		public string PropertyName
		{
			get
			{
				return (string)this[ADObjectSchema.RawName];
			}
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x060038AA RID: 14506 RVA: 0x000DB893 File Offset: 0x000D9A93
		// (set) Token: 0x060038AB RID: 14507 RVA: 0x000DB8A5 File Offset: 0x000D9AA5
		public string PropertyValue
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.PropertyValue];
			}
			set
			{
				this[MonitoringOverrideSchema.PropertyValue] = value;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x060038AC RID: 14508 RVA: 0x000DB8B3 File Offset: 0x000D9AB3
		// (set) Token: 0x060038AD RID: 14509 RVA: 0x000DB8C5 File Offset: 0x000D9AC5
		public ServerVersion ApplyVersion
		{
			get
			{
				return (ServerVersion)this[MonitoringOverrideSchema.ApplyVersion];
			}
			set
			{
				this[MonitoringOverrideSchema.ApplyVersion] = value;
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x060038AE RID: 14510 RVA: 0x000DB8D3 File Offset: 0x000D9AD3
		// (set) Token: 0x060038AF RID: 14511 RVA: 0x000DB8E5 File Offset: 0x000D9AE5
		public DateTime? ExpirationTime
		{
			get
			{
				return (DateTime?)this[MonitoringOverrideSchema.ExpirationTime];
			}
			set
			{
				this[MonitoringOverrideSchema.ExpirationTime] = value;
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x060038B0 RID: 14512 RVA: 0x000DB8F8 File Offset: 0x000D9AF8
		// (set) Token: 0x060038B1 RID: 14513 RVA: 0x000DB90A File Offset: 0x000D9B0A
		public string CreatedBy
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.CreatedBy];
			}
			set
			{
				this[MonitoringOverrideSchema.CreatedBy] = value;
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000DB918 File Offset: 0x000D9B18
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x000DB920 File Offset: 0x000D9B20
		internal override ADObjectSchema Schema
		{
			get
			{
				return MonitoringOverride.schema;
			}
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060038B4 RID: 14516 RVA: 0x000DB927 File Offset: 0x000D9B27
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MonitoringOverride.mostDerivedClass;
			}
		}

		// Token: 0x040026A5 RID: 9893
		internal static readonly ADObjectId RdnContainer = new ADObjectId("CN=Monitoring Settings");

		// Token: 0x040026A6 RID: 9894
		internal static readonly string ContainerName = "Overrides";

		// Token: 0x040026A7 RID: 9895
		private static MonitoringOverrideSchema schema = ObjectSchema.GetInstance<MonitoringOverrideSchema>();

		// Token: 0x040026A8 RID: 9896
		private static string mostDerivedClass = "msExchMonitoringOverride";
	}
}
