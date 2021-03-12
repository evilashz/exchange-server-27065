using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000627 RID: 1575
	[Serializable]
	public class ForwardSyncCookieHeader : ADConfigurationObject
	{
		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x00113150 File Offset: 0x00111350
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ForwardSyncCookieHeader.MostDerivedClass;
			}
		}

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x00113157 File Offset: 0x00111357
		internal override ADObjectSchema Schema
		{
			get
			{
				return ForwardSyncCookieHeader.SchemaInstance;
			}
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x06004A69 RID: 19049 RVA: 0x0011315E File Offset: 0x0011135E
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x06004A6A RID: 19050 RVA: 0x00113165 File Offset: 0x00111365
		// (set) Token: 0x06004A6B RID: 19051 RVA: 0x00113177 File Offset: 0x00111377
		public DateTime Timestamp
		{
			get
			{
				return (DateTime)this[ForwardSyncCookieHeaderSchema.Timestamp];
			}
			set
			{
				this[ForwardSyncCookieHeaderSchema.Timestamp] = value;
			}
		}

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x06004A6C RID: 19052 RVA: 0x0011318A File Offset: 0x0011138A
		// (set) Token: 0x06004A6D RID: 19053 RVA: 0x0011319C File Offset: 0x0011139C
		public ForwardSyncCookieType Type
		{
			get
			{
				return (ForwardSyncCookieType)this[ForwardSyncCookieHeaderSchema.Type];
			}
			set
			{
				this[ForwardSyncCookieHeaderSchema.Type] = (int)value;
			}
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x001131B0 File Offset: 0x001113B0
		internal static object TimestampUtcGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ForwardSyncCookieHeaderSchema.Timestamp];
			if (!string.IsNullOrEmpty(text))
			{
				return new DateTime?(DateTime.ParseExact(text, "yyyyMMddHHmmss'.0Z'", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal));
			}
			return null;
		}

		// Token: 0x04003382 RID: 13186
		internal static readonly string MostDerivedClass = "msExchMSOForwardSyncCookie";

		// Token: 0x04003383 RID: 13187
		private static readonly ForwardSyncCookieHeaderSchema SchemaInstance = ObjectSchema.GetInstance<ForwardSyncCookieHeaderSchema>();
	}
}
