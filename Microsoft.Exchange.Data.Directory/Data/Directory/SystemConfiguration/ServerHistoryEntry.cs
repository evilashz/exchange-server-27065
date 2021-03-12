using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200062A RID: 1578
	[Serializable]
	public class ServerHistoryEntry : ADConfigurationObject
	{
		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x06004A82 RID: 19074 RVA: 0x0011347B File Offset: 0x0011167B
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ServerHistoryEntry.MostDerivedClass;
			}
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x00113482 File Offset: 0x00111682
		internal override ADObjectSchema Schema
		{
			get
			{
				return ServerHistoryEntry.SchemaInstance;
			}
		}

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x06004A84 RID: 19076 RVA: 0x00113489 File Offset: 0x00111689
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x00113490 File Offset: 0x00111690
		// (set) Token: 0x06004A86 RID: 19078 RVA: 0x001134A2 File Offset: 0x001116A2
		public DateTime Timestamp
		{
			get
			{
				return (DateTime)this[ServerHistoryEntrySchema.Timestamp];
			}
			set
			{
				this[ServerHistoryEntrySchema.Timestamp] = value;
			}
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x001134B8 File Offset: 0x001116B8
		internal static object TimestampUtcGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ServerHistoryEntrySchema.Timestamp];
			if (!string.IsNullOrEmpty(text))
			{
				return new DateTime?(DateTime.ParseExact(text, "yyyyMMddHHmmss'.0Z'", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal));
			}
			return null;
		}

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x06004A88 RID: 19080 RVA: 0x001134FC File Offset: 0x001116FC
		// (set) Token: 0x06004A89 RID: 19081 RVA: 0x0011350E File Offset: 0x0011170E
		public int Version
		{
			get
			{
				return (int)this[ServerHistoryEntrySchema.Version];
			}
			set
			{
				this[ServerHistoryEntrySchema.Version] = value;
			}
		}

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x06004A8A RID: 19082 RVA: 0x00113521 File Offset: 0x00111721
		// (set) Token: 0x06004A8B RID: 19083 RVA: 0x00113533 File Offset: 0x00111733
		public byte[] Data
		{
			get
			{
				return (byte[])this[ServerHistoryEntrySchema.Data];
			}
			set
			{
				this[ServerHistoryEntrySchema.Data] = value;
			}
		}

		// Token: 0x0400338C RID: 13196
		internal static readonly string MostDerivedClass = "msExchMSOForwardSyncCookie";

		// Token: 0x0400338D RID: 13197
		private static readonly ServerHistoryEntrySchema SchemaInstance = ObjectSchema.GetInstance<ServerHistoryEntrySchema>();
	}
}
