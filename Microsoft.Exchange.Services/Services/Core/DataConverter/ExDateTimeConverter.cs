using System;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001E4 RID: 484
	internal class ExDateTimeConverter : BaseConverter
	{
		// Token: 0x06000CD6 RID: 3286 RVA: 0x00042261 File Offset: 0x00040461
		public static string ToSoapHeaderTimeZoneRelatedXsdDateTime(ExDateTime systemDateTime)
		{
			return ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime, ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010));
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00042278 File Offset: 0x00040478
		public static string ToSoapHeaderTimeZoneRelatedXsdDateTime(ExDateTime systemDateTime, bool supportsExchange2010SchemaVersion)
		{
			if (supportsExchange2010SchemaVersion)
			{
				return ExDateTimeConverter.ToOffsetXsdDateTime(systemDateTime, EWSSettings.RequestTimeZone);
			}
			return systemDateTime.UniversalTime.ToString((EWSSettings.DateTimePrecision == DateTimePrecision.Milliseconds) ? "yyyy-MM-ddTHH:mm:ss.fff\\Z" : "yyyy-MM-ddTHH:mm:ss\\Z", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000422BC File Offset: 0x000404BC
		public static string ToUtcXsdDateTime(ExDateTime systemDateTime)
		{
			return systemDateTime.UniversalTime.ToString((EWSSettings.DateTimePrecision == DateTimePrecision.Milliseconds) ? "yyyy-MM-ddTHH:mm:ss.fff\\Z" : "yyyy-MM-ddTHH:mm:ss\\Z", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000422F1 File Offset: 0x000404F1
		public static string ToUtcXsdTime(TimeSpan timeSpan)
		{
			return timeSpan.ToString();
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00042300 File Offset: 0x00040500
		public static ExDateTime Parse(string propertyString)
		{
			ExDateTime result;
			try
			{
				DateTime dateTime = DateTime.Parse(propertyString, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
				result = (ExDateTime)dateTime;
			}
			catch (ArgumentException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string, ArgumentException>(0L, "[ExDateTimeConverter::Parse] DateTime.Parse threw an ArgumentException for string '{0}': {1}", propertyString, ex);
				throw new InvalidValueForPropertyException(CoreResources.IDs.ErrorInvalidValueForPropertyDate, ex);
			}
			catch (FormatException ex2)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string, FormatException>(0L, "[ExDateTimeConverter::Parse] DateTime.Parse threw a FormatException for string '{0}': {1}", propertyString, ex2);
				throw new InvalidValueForPropertyException(CoreResources.IDs.ErrorInvalidValueForPropertyDate, ex2);
			}
			return result;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0004238C File Offset: 0x0004058C
		public static string ToOffsetXsdDateTime(ExDateTime dateTime, ExTimeZone displayTimeZone)
		{
			ExDateTime exDateTime = displayTimeZone.ConvertDateTime(dateTime);
			if (exDateTime.Bias.Ticks == 0L)
			{
				return exDateTime.ToString((EWSSettings.DateTimePrecision == DateTimePrecision.Milliseconds) ? "yyyy-MM-ddTHH:mm:ss.fff\\Z" : "yyyy-MM-ddTHH:mm:ss\\Z", CultureInfo.InvariantCulture);
			}
			string str = exDateTime.ToString((EWSSettings.DateTimePrecision == DateTimePrecision.Milliseconds) ? "yyyy-MM-ddTHH:mm:ss.fff" : "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
			string str2 = ExDateTimeConverter.ConvertBiasToString(exDateTime.Bias);
			return str + str2;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0004240C File Offset: 0x0004060C
		public override object ConvertToObject(string propertyString)
		{
			ExDateTime exDateTime = ExDateTimeConverter.ParseTimeZoneRelated(propertyString, EWSSettings.RequestTimeZone);
			exDateTime = ExTimeZone.UtcTimeZone.ConvertDateTime(exDateTime);
			return exDateTime;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00042437 File Offset: 0x00040637
		public override string ConvertToString(object propertyValue)
		{
			return ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime((ExDateTime)propertyValue);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00042444 File Offset: 0x00040644
		private static string ConvertBiasToString(TimeSpan bias)
		{
			if (bias.Ticks < 0L)
			{
				return (-bias.Hours).ToString("-00", CultureInfo.InvariantCulture) + (-bias.Minutes).ToString(":00", CultureInfo.InvariantCulture);
			}
			return bias.Hours.ToString("+00", CultureInfo.InvariantCulture) + bias.Minutes.ToString(":00", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000424D0 File Offset: 0x000406D0
		public static ExDateTime ParseTimeZoneRelated(string propertyString, ExTimeZone timeZone)
		{
			ExDateTime result;
			try
			{
				DateTime dateTime = XmlConvert.ToDateTime(propertyString, XmlDateTimeSerializationMode.RoundtripKind);
				if (dateTime.Kind == DateTimeKind.Unspecified)
				{
					result = new ExDateTime(timeZone, dateTime);
				}
				else
				{
					if (dateTime.Kind == DateTimeKind.Local)
					{
						dateTime = XmlConvert.ToDateTime(propertyString, XmlDateTimeSerializationMode.Utc);
					}
					result = new ExDateTime(timeZone, dateTime, null);
				}
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidValueForPropertyException(CoreResources.IDs.ErrorInvalidValueForPropertyDate, innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidValueForPropertyException(CoreResources.IDs.ErrorInvalidValueForPropertyDate, innerException2);
			}
			return result;
		}

		// Token: 0x04000A76 RID: 2678
		private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";

		// Token: 0x04000A77 RID: 2679
		private const string DateTimeFormatZulu = "yyyy-MM-ddTHH:mm:ss\\Z";

		// Token: 0x04000A78 RID: 2680
		private const string DateTimeFormatPrecise = "yyyy-MM-ddTHH:mm:ss.fff";

		// Token: 0x04000A79 RID: 2681
		private const string DateTimeFormatPreciseZulu = "yyyy-MM-ddTHH:mm:ss.fff\\Z";
	}
}
