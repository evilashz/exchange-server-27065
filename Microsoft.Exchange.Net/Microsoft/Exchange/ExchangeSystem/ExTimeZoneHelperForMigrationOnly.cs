using System;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x0200006A RID: 106
	public static class ExTimeZoneHelperForMigrationOnly
	{
		// Token: 0x060003AB RID: 939 RVA: 0x0000F48C File Offset: 0x0000D68C
		public static object ToUtcIfDateTime(object value)
		{
			if (value is DateTime[])
			{
				DateTime[] array = value as DateTime[];
				ExDateTime[] array2 = new ExDateTime[array.Length];
				for (int num = 0; num != array.Length; num++)
				{
					array2[num] = ((ExDateTime)array[num]).ToUtc();
				}
				value = array2;
			}
			else if (value is ExDateTime[])
			{
				ExDateTime[] array3 = value as ExDateTime[];
				for (int num2 = 0; num2 != array3.Length; num2++)
				{
					array3[num2] = array3[num2].ToUtc();
				}
			}
			else if (value is ExDateTime)
			{
				value = ((ExDateTime)value).ToUtc();
			}
			else if (value is DateTime)
			{
				value = new ExDateTime(ExTimeZone.UtcTimeZone, (DateTime)value);
			}
			return value;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000F567 File Offset: 0x0000D767
		public static object ToExDateTimeIfObjectIsDateTime(object value)
		{
			return ExTimeZoneHelperForMigrationOnly.ToExDateTimeIfObjectIsDateTime(ExTimeZone.UnspecifiedTimeZone, value);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000F574 File Offset: 0x0000D774
		public static object ToExDateTimeIfObjectIsDateTime(ExTimeZone timeZone, object value)
		{
			if (value is DateTime)
			{
				return ExTimeZoneHelperForMigrationOnly.ToExDateTime(timeZone, (DateTime)value);
			}
			DateTime[] array = value as DateTime[];
			if (array != null)
			{
				return ExTimeZoneHelperForMigrationOnly.ToExDateTimeArray(timeZone, array);
			}
			return value;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		public static ExDateTime[] ToExDateTimeArray(ExTimeZone timeZone, DateTime[] value)
		{
			ExDateTime[] array = new ExDateTime[value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				array[i] = ExTimeZoneHelperForMigrationOnly.ToExDateTime(timeZone, value[i]);
			}
			return array;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000F5F3 File Offset: 0x0000D7F3
		private static ExDateTime ToExDateTime(ExTimeZone timeZone, DateTime value)
		{
			if (value < TimeLibConsts.MinSystemDateTimeValue)
			{
				value = TimeLibConsts.MinSystemDateTimeValue;
			}
			else if (value > TimeLibConsts.MaxSystemDateTimeValue)
			{
				value = TimeLibConsts.MaxSystemDateTimeValue;
			}
			return new ExDateTime(timeZone, value);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000F626 File Offset: 0x0000D826
		public static object ToLegacyUtcIfDateTime(object value)
		{
			return ExTimeZoneHelperForMigrationOnly.ToLegacyUtcIfDateTime(ExTimeZone.CurrentTimeZone, value);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000F634 File Offset: 0x0000D834
		public static object ToLegacyUtcIfDateTime(ExTimeZone timeZone, object value)
		{
			object result;
			if (value is ExDateTime)
			{
				result = (DateTime)((ExDateTime)value).ToUtc();
			}
			else if (value is ExDateTime[])
			{
				ExDateTime[] array = (ExDateTime[])value;
				DateTime[] array2 = new DateTime[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = (DateTime)array[i].ToUtc();
				}
				result = array2;
			}
			else if (value is DateTime)
			{
				ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(false, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Mid, "ExTimeZoneHelperForMigrationOnly.ToLegacyUtcIfDateTime: DateTime argument", new object[0]);
				ExDateTime exDateTime = new ExDateTime(timeZone, (DateTime)value);
				result = (DateTime)exDateTime.ToUtc();
			}
			else if (value is DateTime[])
			{
				ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(false, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Mid, "ExTimeZoneHelperForMigrationOnly.ToLegacyUtcIfDateTime: DateTime[] argument", new object[0]);
				DateTime[] array3 = (DateTime[])value;
				for (int j = 0; j < array3.Length; j++)
				{
					ExDateTime exDateTime2 = new ExDateTime(timeZone, array3[j]);
					array3[j] = (DateTime)exDateTime2.ToUtc();
				}
				result = array3;
			}
			else
			{
				result = value;
			}
			return result;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000F75B File Offset: 0x0000D95B
		public static void CheckValidationLevel<Arg0>(bool condition, ExTimeZoneHelperForMigrationOnly.ValidationLevel level, string message, Arg0 arg0)
		{
			if (!condition && level >= ExTimeZoneHelperForMigrationOnly.CurrentValidationLevel)
			{
				throw new ExTimeLibException(string.Format(message, arg0));
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000F77A File Offset: 0x0000D97A
		public static void CheckValidationLevel(bool condition, ExTimeZoneHelperForMigrationOnly.ValidationLevel level, string message, params object[] args)
		{
			if (!condition && level >= ExTimeZoneHelperForMigrationOnly.CurrentValidationLevel)
			{
				throw new ExTimeLibException(string.Format(message, args));
			}
		}

		// Token: 0x040001CB RID: 459
		private static ExTimeZoneHelperForMigrationOnly.ValidationLevel CurrentValidationLevel = ExTimeZoneHelperForMigrationOnly.ValidationLevel.Highest;

		// Token: 0x0200006B RID: 107
		public enum ValidationLevel
		{
			// Token: 0x040001CD RID: 461
			Lowest,
			// Token: 0x040001CE RID: 462
			VeryLow,
			// Token: 0x040001CF RID: 463
			Low,
			// Token: 0x040001D0 RID: 464
			Mid,
			// Token: 0x040001D1 RID: 465
			High,
			// Token: 0x040001D2 RID: 466
			VeryHigh,
			// Token: 0x040001D3 RID: 467
			Highest
		}
	}
}
