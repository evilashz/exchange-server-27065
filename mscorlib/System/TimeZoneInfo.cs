using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System
{
	// Token: 0x02000146 RID: 326
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class TimeZoneInfo : IEquatable<TimeZoneInfo>, ISerializable, IDeserializationCallback
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00039120 File Offset: 0x00037320
		[__DynamicallyInvokable]
		public string Id
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00039128 File Offset: 0x00037328
		[__DynamicallyInvokable]
		public string DisplayName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_displayName != null)
				{
					return this.m_displayName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0003913E File Offset: 0x0003733E
		[__DynamicallyInvokable]
		public string StandardName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_standardDisplayName != null)
				{
					return this.m_standardDisplayName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x00039154 File Offset: 0x00037354
		[__DynamicallyInvokable]
		public string DaylightName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_daylightDisplayName != null)
				{
					return this.m_daylightDisplayName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0003916A File Offset: 0x0003736A
		[__DynamicallyInvokable]
		public TimeSpan BaseUtcOffset
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_baseUtcOffset;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x00039172 File Offset: 0x00037372
		[__DynamicallyInvokable]
		public bool SupportsDaylightSavingTime
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_supportsDaylightSavingTime;
			}
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0003917A File Offset: 0x0003737A
		public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
		{
			if (this.m_adjustmentRules == null)
			{
				return new TimeZoneInfo.AdjustmentRule[0];
			}
			return (TimeZoneInfo.AdjustmentRule[])this.m_adjustmentRules.Clone();
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0003919C File Offset: 0x0003739C
		[__DynamicallyInvokable]
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset)
		{
			if (!this.SupportsDaylightSavingTime)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeOffsetIsNotAmbiguous"), "dateTimeOffset");
			}
			DateTime dateTime = TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime;
			bool flag = false;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
				flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime, adjustmentRuleForTime, daylightTime);
			}
			if (!flag)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeOffsetIsNotAmbiguous"), "dateTimeOffset");
			}
			TimeSpan[] array = new TimeSpan[2];
			TimeSpan timeSpan = this.m_baseUtcOffset + adjustmentRuleForTime.BaseUtcOffsetDelta;
			if (adjustmentRuleForTime.DaylightDelta > TimeSpan.Zero)
			{
				array[0] = timeSpan;
				array[1] = timeSpan + adjustmentRuleForTime.DaylightDelta;
			}
			else
			{
				array[0] = timeSpan + adjustmentRuleForTime.DaylightDelta;
				array[1] = timeSpan;
			}
			return array;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00039288 File Offset: 0x00037488
		[__DynamicallyInvokable]
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
		{
			if (!this.SupportsDaylightSavingTime)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsNotAmbiguous"), "dateTime");
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, TimeZoneInfoOptions.None, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				TimeZoneInfo.CachedData cachedData2 = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData2.Utc, this, TimeZoneInfoOptions.None, cachedData2);
			}
			else
			{
				dateTime2 = dateTime;
			}
			bool flag = false;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime);
				flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime2, adjustmentRuleForTime, daylightTime);
			}
			if (!flag)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsNotAmbiguous"), "dateTime");
			}
			TimeSpan[] array = new TimeSpan[2];
			TimeSpan timeSpan = this.m_baseUtcOffset + adjustmentRuleForTime.BaseUtcOffsetDelta;
			if (adjustmentRuleForTime.DaylightDelta > TimeSpan.Zero)
			{
				array[0] = timeSpan;
				array[1] = timeSpan + adjustmentRuleForTime.DaylightDelta;
			}
			else
			{
				array[0] = timeSpan + adjustmentRuleForTime.DaylightDelta;
				array[1] = timeSpan;
			}
			return array;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000393AD File Offset: 0x000375AD
		[__DynamicallyInvokable]
		public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset)
		{
			return TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x000393BC File Offset: 0x000375BC
		[__DynamicallyInvokable]
		public TimeSpan GetUtcOffset(DateTime dateTime)
		{
			return this.GetUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x000393CC File Offset: 0x000375CC
		internal static TimeSpan GetLocalUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return cachedData.Local.GetUtcOffset(dateTime, flags, cachedData);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000393ED File Offset: 0x000375ED
		internal TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.GetUtcOffset(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000393FC File Offset: 0x000375FC
		private TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				if (cachedData.GetCorrespondingKind(this) != DateTimeKind.Local)
				{
					DateTime time = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, flags);
					return TimeZoneInfo.GetUtcOffsetFromUtc(time, this);
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return this.m_baseUtcOffset;
				}
				return TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this);
			}
			return TimeZoneInfo.GetUtcOffset(dateTime, this, flags);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00039464 File Offset: 0x00037664
		[__DynamicallyInvokable]
		public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset)
		{
			return this.m_supportsDaylightSavingTime && this.IsAmbiguousTime(TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00039490 File Offset: 0x00037690
		[__DynamicallyInvokable]
		public bool IsAmbiguousTime(DateTime dateTime)
		{
			return this.IsAmbiguousTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0003949C File Offset: 0x0003769C
		internal bool IsAmbiguousTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			if (!this.m_supportsDaylightSavingTime)
			{
				return false;
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				TimeZoneInfo.CachedData cachedData2 = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData2.Utc, this, flags, cachedData2);
			}
			else
			{
				dateTime2 = dateTime;
			}
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime);
				return TimeZoneInfo.GetIsAmbiguousTime(dateTime2, adjustmentRuleForTime, daylightTime);
			}
			return false;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00039528 File Offset: 0x00037728
		[__DynamicallyInvokable]
		public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset)
		{
			bool result;
			TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this, out result);
			return result;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00039546 File Offset: 0x00037746
		[__DynamicallyInvokable]
		public bool IsDaylightSavingTime(DateTime dateTime)
		{
			return this.IsDaylightSavingTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00039555 File Offset: 0x00037755
		internal bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.IsDaylightSavingTime(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00039564 File Offset: 0x00037764
		private bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (!this.m_supportsDaylightSavingTime || this.m_adjustmentRules == null)
			{
				return false;
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return false;
				}
				bool result;
				TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this, out result);
				return result;
			}
			else
			{
				dateTime2 = dateTime;
			}
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime);
				return TimeZoneInfo.GetIsDaylightSavings(dateTime2, adjustmentRuleForTime, daylightTime, flags);
			}
			return false;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000395F0 File Offset: 0x000377F0
		[__DynamicallyInvokable]
		public bool IsInvalidTime(DateTime dateTime)
		{
			bool result = false;
			if (dateTime.Kind == DateTimeKind.Unspecified || (dateTime.Kind == DateTimeKind.Local && TimeZoneInfo.s_cachedData.GetCorrespondingKind(this) == DateTimeKind.Local))
			{
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime);
				if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
					result = TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0003964F File Offset: 0x0003784F
		public static void ClearCachedData()
		{
			TimeZoneInfo.s_cachedData = new TimeZoneInfo.CachedData();
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0003965B File Offset: 0x0003785B
		public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00039669 File Offset: 0x00037869
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00039678 File Offset: 0x00037878
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
		{
			if (dateTime.Kind == DateTimeKind.Local && string.Compare(sourceTimeZoneId, TimeZoneInfo.Local.Id, StringComparison.OrdinalIgnoreCase) == 0)
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, cachedData);
			}
			if (dateTime.Kind == DateTimeKind.Utc && string.Compare(sourceTimeZoneId, TimeZoneInfo.Utc.Id, StringComparison.OrdinalIgnoreCase) == 0)
			{
				TimeZoneInfo.CachedData cachedData2 = TimeZoneInfo.s_cachedData;
				return TimeZoneInfo.ConvertTime(dateTime, cachedData2.Utc, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, cachedData2);
			}
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00039708 File Offset: 0x00037908
		[__DynamicallyInvokable]
		public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTime utcDateTime = dateTimeOffset.UtcDateTime;
			TimeSpan utcOffsetFromUtc = TimeZoneInfo.GetUtcOffsetFromUtc(utcDateTime, destinationTimeZone);
			long num = utcDateTime.Ticks + utcOffsetFromUtc.Ticks;
			if (num > DateTimeOffset.MaxValue.Ticks)
			{
				return DateTimeOffset.MaxValue;
			}
			if (num < DateTimeOffset.MinValue.Ticks)
			{
				return DateTimeOffset.MinValue;
			}
			return new DateTimeOffset(num, utcOffsetFromUtc);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00039778 File Offset: 0x00037978
		[__DynamicallyInvokable]
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			if (dateTime.Ticks == 0L)
			{
				TimeZoneInfo.ClearCachedData();
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
			}
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x000397D0 File Offset: 0x000379D0
		[__DynamicallyInvokable]
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000397E0 File Offset: 0x000379E0
		internal static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x000397F0 File Offset: 0x000379F0
		private static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (sourceTimeZone == null)
			{
				throw new ArgumentNullException("sourceTimeZone");
			}
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTimeKind correspondingKind = cachedData.GetCorrespondingKind(sourceTimeZone);
			if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && dateTime.Kind != DateTimeKind.Unspecified && dateTime.Kind != correspondingKind)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ConvertMismatch"), "sourceTimeZone");
			}
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = sourceTimeZone.GetAdjustmentRuleForTime(dateTime);
			TimeSpan t = sourceTimeZone.BaseUtcOffset;
			if (adjustmentRuleForTime != null)
			{
				t += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
					if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsInvalid"), "dateTime");
					}
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(dateTime, adjustmentRuleForTime, daylightTime, flags);
					t += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			DateTimeKind correspondingKind2 = cachedData.GetCorrespondingKind(destinationTimeZone);
			if (dateTime.Kind != DateTimeKind.Unspecified && correspondingKind != DateTimeKind.Unspecified && correspondingKind == correspondingKind2)
			{
				return dateTime;
			}
			long ticks = dateTime.Ticks - t.Ticks;
			bool isAmbiguousDst = false;
			DateTime dateTime2 = TimeZoneInfo.ConvertUtcToTimeZone(ticks, destinationTimeZone, out isAmbiguousDst);
			if (correspondingKind2 == DateTimeKind.Local)
			{
				return new DateTime(dateTime2.Ticks, DateTimeKind.Local, isAmbiguousDst);
			}
			return new DateTime(dateTime2.Ticks, correspondingKind2);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00039930 File Offset: 0x00037B30
		public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00039954 File Offset: 0x00037B54
		public static DateTime ConvertTimeToUtc(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, TimeZoneInfoOptions.None, cachedData);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00039988 File Offset: 0x00037B88
		internal static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, flags, cachedData);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x000399BC File Offset: 0x00037BBC
		public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, cachedData.Utc, TimeZoneInfoOptions.None, cachedData);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x000399DE File Offset: 0x00037BDE
		[__DynamicallyInvokable]
		public bool Equals(TimeZoneInfo other)
		{
			return other != null && string.Compare(this.m_id, other.m_id, StringComparison.OrdinalIgnoreCase) == 0 && this.HasSameRules(other);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00039A00 File Offset: 0x00037C00
		public override bool Equals(object obj)
		{
			TimeZoneInfo timeZoneInfo = obj as TimeZoneInfo;
			return timeZoneInfo != null && this.Equals(timeZoneInfo);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00039A20 File Offset: 0x00037C20
		public static TimeZoneInfo FromSerializedString(string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (source.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSerializedString", new object[]
				{
					source
				}), "source");
			}
			return TimeZoneInfo.StringSerializer.GetDeserializedTimeZoneInfo(source);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00039A5D File Offset: 0x00037C5D
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_id.ToUpper(CultureInfo.InvariantCulture).GetHashCode();
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00039A74 File Offset: 0x00037C74
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo.CachedData obj = cachedData;
			lock (obj)
			{
				if (cachedData.m_readOnlySystemTimeZones == null)
				{
					PermissionSet permissionSet = new PermissionSet(PermissionState.None);
					permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
					permissionSet.Assert();
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
					{
						if (registryKey != null)
						{
							foreach (string id in registryKey.GetSubKeyNames())
							{
								TimeZoneInfo timeZoneInfo;
								Exception ex;
								TimeZoneInfo.TryGetTimeZone(id, false, out timeZoneInfo, out ex, cachedData);
							}
						}
						cachedData.m_allSystemTimeZonesRead = true;
					}
					List<TimeZoneInfo> list;
					if (cachedData.m_systemTimeZones != null)
					{
						list = new List<TimeZoneInfo>(cachedData.m_systemTimeZones.Values);
					}
					else
					{
						list = new List<TimeZoneInfo>();
					}
					list.Sort(new TimeZoneInfo.TimeZoneInfoComparer());
					cachedData.m_readOnlySystemTimeZones = new ReadOnlyCollection<TimeZoneInfo>(list);
				}
			}
			return cachedData.m_readOnlySystemTimeZones;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00039B88 File Offset: 0x00037D88
		public bool HasSameRules(TimeZoneInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.m_baseUtcOffset != other.m_baseUtcOffset || this.m_supportsDaylightSavingTime != other.m_supportsDaylightSavingTime)
			{
				return false;
			}
			TimeZoneInfo.AdjustmentRule[] adjustmentRules = this.m_adjustmentRules;
			TimeZoneInfo.AdjustmentRule[] adjustmentRules2 = other.m_adjustmentRules;
			bool flag = (adjustmentRules == null && adjustmentRules2 == null) || (adjustmentRules != null && adjustmentRules2 != null);
			if (!flag)
			{
				return false;
			}
			if (adjustmentRules != null)
			{
				if (adjustmentRules.Length != adjustmentRules2.Length)
				{
					return false;
				}
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					if (!adjustmentRules[i].Equals(adjustmentRules2[i]))
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00039C18 File Offset: 0x00037E18
		[__DynamicallyInvokable]
		public static TimeZoneInfo Local
		{
			[__DynamicallyInvokable]
			get
			{
				return TimeZoneInfo.s_cachedData.Local;
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00039C24 File Offset: 0x00037E24
		public string ToSerializedString()
		{
			return TimeZoneInfo.StringSerializer.GetSerializedString(this);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00039C2C File Offset: 0x00037E2C
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00039C34 File Offset: 0x00037E34
		[__DynamicallyInvokable]
		public static TimeZoneInfo Utc
		{
			[__DynamicallyInvokable]
			get
			{
				return TimeZoneInfo.s_cachedData.Utc;
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00039C40 File Offset: 0x00037E40
		[SecurityCritical]
		private TimeZoneInfo(Win32Native.TimeZoneInformation zone, bool dstDisabled)
		{
			if (string.IsNullOrEmpty(zone.StandardName))
			{
				this.m_id = "Local";
			}
			else
			{
				this.m_id = zone.StandardName;
			}
			this.m_baseUtcOffset = new TimeSpan(0, -zone.Bias, 0);
			if (!dstDisabled)
			{
				Win32Native.RegistryTimeZoneInformation timeZoneInformation = new Win32Native.RegistryTimeZoneInformation(zone);
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, zone.Bias);
				if (adjustmentRule != null)
				{
					this.m_adjustmentRules = new TimeZoneInfo.AdjustmentRule[1];
					this.m_adjustmentRules[0] = adjustmentRule;
				}
			}
			TimeZoneInfo.ValidateTimeZoneInfo(this.m_id, this.m_baseUtcOffset, this.m_adjustmentRules, out this.m_supportsDaylightSavingTime);
			this.m_displayName = zone.StandardName;
			this.m_standardDisplayName = zone.StandardName;
			this.m_daylightDisplayName = zone.DaylightName;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00039D18 File Offset: 0x00037F18
		private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			bool flag;
			TimeZoneInfo.ValidateTimeZoneInfo(id, baseUtcOffset, adjustmentRules, out flag);
			if (!disableDaylightSavingTime && adjustmentRules != null && adjustmentRules.Length != 0)
			{
				this.m_adjustmentRules = (TimeZoneInfo.AdjustmentRule[])adjustmentRules.Clone();
			}
			this.m_id = id;
			this.m_baseUtcOffset = baseUtcOffset;
			this.m_displayName = displayName;
			this.m_standardDisplayName = standardDisplayName;
			this.m_daylightDisplayName = (disableDaylightSavingTime ? null : daylightDisplayName);
			this.m_supportsDaylightSavingTime = (flag && !disableDaylightSavingTime);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00039D92 File Offset: 0x00037F92
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, standardDisplayName, null, false);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00039DA0 File Offset: 0x00037FA0
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00039DB0 File Offset: 0x00037FB0
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00039DC4 File Offset: 0x00037FC4
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				bool flag;
				TimeZoneInfo.ValidateTimeZoneInfo(this.m_id, this.m_baseUtcOffset, this.m_adjustmentRules, out flag);
				if (flag != this.m_supportsDaylightSavingTime)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_CorruptField", new object[]
					{
						"SupportsDaylightSavingTime"
					}));
				}
			}
			catch (ArgumentException innerException)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), innerException);
			}
			catch (InvalidTimeZoneException innerException2)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), innerException2);
			}
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00039E54 File Offset: 0x00038054
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Id", this.m_id);
			info.AddValue("DisplayName", this.m_displayName);
			info.AddValue("StandardName", this.m_standardDisplayName);
			info.AddValue("DaylightName", this.m_daylightDisplayName);
			info.AddValue("BaseUtcOffset", this.m_baseUtcOffset);
			info.AddValue("AdjustmentRules", this.m_adjustmentRules);
			info.AddValue("SupportsDaylightSavingTime", this.m_supportsDaylightSavingTime);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00039EEC File Offset: 0x000380EC
		private TimeZoneInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_id = (string)info.GetValue("Id", typeof(string));
			this.m_displayName = (string)info.GetValue("DisplayName", typeof(string));
			this.m_standardDisplayName = (string)info.GetValue("StandardName", typeof(string));
			this.m_daylightDisplayName = (string)info.GetValue("DaylightName", typeof(string));
			this.m_baseUtcOffset = (TimeSpan)info.GetValue("BaseUtcOffset", typeof(TimeSpan));
			this.m_adjustmentRules = (TimeZoneInfo.AdjustmentRule[])info.GetValue("AdjustmentRules", typeof(TimeZoneInfo.AdjustmentRule[]));
			this.m_supportsDaylightSavingTime = (bool)info.GetValue("SupportsDaylightSavingTime", typeof(bool));
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00039FF0 File Offset: 0x000381F0
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime)
		{
			if (this.m_adjustmentRules == null || this.m_adjustmentRules.Length == 0)
			{
				return null;
			}
			DateTime date = dateTime.Date;
			for (int i = 0; i < this.m_adjustmentRules.Length; i++)
			{
				if (this.m_adjustmentRules[i].DateStart <= date && this.m_adjustmentRules[i].DateEnd >= date)
				{
					return this.m_adjustmentRules[i];
				}
			}
			return null;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0003A060 File Offset: 0x00038260
		[SecurityCritical]
		private static bool CheckDaylightSavingTimeNotSupported(Win32Native.TimeZoneInformation timeZone)
		{
			return timeZone.DaylightDate.Year == timeZone.StandardDate.Year && timeZone.DaylightDate.Month == timeZone.StandardDate.Month && timeZone.DaylightDate.DayOfWeek == timeZone.StandardDate.DayOfWeek && timeZone.DaylightDate.Day == timeZone.StandardDate.Day && timeZone.DaylightDate.Hour == timeZone.StandardDate.Hour && timeZone.DaylightDate.Minute == timeZone.StandardDate.Minute && timeZone.DaylightDate.Second == timeZone.StandardDate.Second && timeZone.DaylightDate.Milliseconds == timeZone.StandardDate.Milliseconds;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0003A138 File Offset: 0x00038338
		private static DateTime ConvertUtcToTimeZone(long ticks, TimeZoneInfo destinationTimeZone, out bool isAmbiguousLocalDst)
		{
			DateTime time;
			if (ticks > DateTime.MaxValue.Ticks)
			{
				time = DateTime.MaxValue;
			}
			else if (ticks < DateTime.MinValue.Ticks)
			{
				time = DateTime.MinValue;
			}
			else
			{
				time = new DateTime(ticks);
			}
			ticks += TimeZoneInfo.GetUtcOffsetFromUtc(time, destinationTimeZone, out isAmbiguousLocalDst).Ticks;
			DateTime result;
			if (ticks > DateTime.MaxValue.Ticks)
			{
				result = DateTime.MaxValue;
			}
			else if (ticks < DateTime.MinValue.Ticks)
			{
				result = DateTime.MinValue;
			}
			else
			{
				result = new DateTime(ticks);
			}
			return result;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0003A1CC File Offset: 0x000383CC
		[SecurityCritical]
		private static TimeZoneInfo.AdjustmentRule CreateAdjustmentRuleFromTimeZoneInformation(Win32Native.RegistryTimeZoneInformation timeZoneInformation, DateTime startDate, DateTime endDate, int defaultBaseUtcOffset)
		{
			if (timeZoneInformation.StandardDate.Month == 0)
			{
				if (timeZoneInformation.Bias == defaultBaseUtcOffset)
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, TimeSpan.Zero, TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue, 1, 1), TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue.AddMilliseconds(1.0), 1, 1), new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0));
			}
			else
			{
				TimeZoneInfo.TransitionTime daylightTransitionStart;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out daylightTransitionStart, true))
				{
					return null;
				}
				TimeZoneInfo.TransitionTime transitionTime;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime, false))
				{
					return null;
				}
				if (daylightTransitionStart.Equals(transitionTime))
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, new TimeSpan(0, -timeZoneInformation.DaylightBias, 0), daylightTransitionStart, transitionTime, new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0));
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0003A28C File Offset: 0x0003848C
		[SecuritySafeCritical]
		private static string FindIdFromTimeZoneInformation(Win32Native.TimeZoneInformation timeZone, out bool dstDisabled)
		{
			dstDisabled = false;
			try
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
				permissionSet.Assert();
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
				{
					if (registryKey == null)
					{
						return null;
					}
					foreach (string text in registryKey.GetSubKeyNames())
					{
						if (TimeZoneInfo.TryCompareTimeZoneInformationToRegistry(timeZone, text, out dstDisabled))
						{
							return text;
						}
					}
				}
			}
			finally
			{
				PermissionSet.RevertAssert();
			}
			return null;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0003A338 File Offset: 0x00038538
		private static DaylightTimeStruct GetDaylightTime(int year, TimeZoneInfo.AdjustmentRule rule)
		{
			TimeSpan daylightDelta = rule.DaylightDelta;
			DateTime start = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionStart);
			DateTime end = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionEnd);
			return new DaylightTimeStruct(start, end, daylightDelta);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0003A370 File Offset: 0x00038570
		private static bool GetIsDaylightSavings(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime, TimeZoneInfoOptions flags)
		{
			if (rule == null)
			{
				return false;
			}
			DateTime startTime;
			DateTime endTime;
			if (time.Kind == DateTimeKind.Local)
			{
				startTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + daylightTime.Delta));
				endTime = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : daylightTime.End);
			}
			else
			{
				bool flag = rule.DaylightDelta > TimeSpan.Zero;
				startTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + (flag ? rule.DaylightDelta : TimeSpan.Zero)));
				endTime = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : (daylightTime.End + (flag ? (-rule.DaylightDelta) : TimeSpan.Zero)));
			}
			bool flag2 = TimeZoneInfo.CheckIsDst(startTime, time, endTime, false);
			if (flag2 && time.Kind == DateTimeKind.Local && TimeZoneInfo.GetIsAmbiguousTime(time, rule, daylightTime))
			{
				flag2 = time.IsAmbiguousDaylightSavingTime();
			}
			return flag2;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0003A4D0 File Offset: 0x000386D0
		private static bool GetIsDaylightSavingsFromUtc(DateTime time, int Year, TimeSpan utc, TimeZoneInfo.AdjustmentRule rule, out bool isAmbiguousLocalDst, TimeZoneInfo zone)
		{
			isAmbiguousLocalDst = false;
			if (rule == null)
			{
				return false;
			}
			TimeSpan t = utc + rule.BaseUtcOffsetDelta;
			DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(Year, rule);
			bool ignoreYearAdjustment = false;
			DateTime dateTime;
			if (rule.IsStartDateMarkerForBeginningOfYear() && daylightTime.Start.Year > DateTime.MinValue.Year)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.Start.Year - 1, 12, 31));
				if (adjustmentRuleForTime != null && adjustmentRuleForTime.IsEndDateMarkerForEndOfYear())
				{
					dateTime = TimeZoneInfo.GetDaylightTime(daylightTime.Start.Year - 1, adjustmentRuleForTime).Start - utc - adjustmentRuleForTime.BaseUtcOffsetDelta;
					ignoreYearAdjustment = true;
				}
				else
				{
					dateTime = new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) - t;
				}
			}
			else
			{
				dateTime = daylightTime.Start - t;
			}
			DateTime dateTime2;
			if (rule.IsEndDateMarkerForEndOfYear() && daylightTime.End.Year < DateTime.MaxValue.Year)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime2 = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.End.Year + 1, 1, 1));
				if (adjustmentRuleForTime2 != null && adjustmentRuleForTime2.IsStartDateMarkerForBeginningOfYear())
				{
					if (adjustmentRuleForTime2.IsEndDateMarkerForEndOfYear())
					{
						dateTime2 = new DateTime(daylightTime.End.Year + 1, 12, 31) - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					else
					{
						dateTime2 = TimeZoneInfo.GetDaylightTime(daylightTime.End.Year + 1, adjustmentRuleForTime2).End - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					ignoreYearAdjustment = true;
				}
				else
				{
					dateTime2 = new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) - t - rule.DaylightDelta;
				}
			}
			else
			{
				dateTime2 = daylightTime.End - t - rule.DaylightDelta;
			}
			DateTime t2;
			DateTime t3;
			if (daylightTime.Delta.Ticks > 0L)
			{
				t2 = dateTime2 - daylightTime.Delta;
				t3 = dateTime2;
			}
			else
			{
				t2 = dateTime;
				t3 = dateTime - daylightTime.Delta;
			}
			bool flag = TimeZoneInfo.CheckIsDst(dateTime, time, dateTime2, ignoreYearAdjustment);
			if (flag)
			{
				isAmbiguousLocalDst = (time >= t2 && time < t3);
				if (!isAmbiguousLocalDst && t2.Year != t3.Year)
				{
					try
					{
						DateTime dateTime3 = t2.AddYears(1);
						DateTime dateTime4 = t3.AddYears(1);
						isAmbiguousLocalDst = (time >= t2 && time < t3);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
					if (!isAmbiguousLocalDst)
					{
						try
						{
							DateTime dateTime3 = t2.AddYears(-1);
							DateTime dateTime4 = t3.AddYears(-1);
							isAmbiguousLocalDst = (time >= t2 && time < t3);
						}
						catch (ArgumentOutOfRangeException)
						{
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0003A80C File Offset: 0x00038A0C
		private static bool CheckIsDst(DateTime startTime, DateTime time, DateTime endTime, bool ignoreYearAdjustment)
		{
			if (!ignoreYearAdjustment)
			{
				int year = startTime.Year;
				int year2 = endTime.Year;
				if (year != year2)
				{
					endTime = endTime.AddYears(year - year2);
				}
				int year3 = time.Year;
				if (year != year3)
				{
					time = time.AddYears(year - year3);
				}
			}
			bool result;
			if (startTime > endTime)
			{
				result = (time < endTime || time >= startTime);
			}
			else
			{
				result = (time >= startTime && time < endTime);
			}
			return result;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0003A888 File Offset: 0x00038A88
		private static bool GetIsAmbiguousTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime t;
			DateTime t2;
			if (rule.DaylightDelta > TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				t = daylightTime.End;
				t2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				t = daylightTime.Start;
				t2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = (time >= t2 && time < t);
			if (!flag && t.Year != t2.Year)
			{
				try
				{
					DateTime t3 = t.AddYears(1);
					DateTime t4 = t2.AddYears(1);
					flag = (time >= t4 && time < t3);
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime t3 = t.AddYears(-1);
						DateTime t4 = t2.AddYears(-1);
						flag = (time >= t4 && time < t3);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0003A9B0 File Offset: 0x00038BB0
		private static bool GetIsInvalidTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime t;
			DateTime t2;
			if (rule.DaylightDelta < TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				t = daylightTime.End;
				t2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				t = daylightTime.Start;
				t2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = (time >= t && time < t2);
			if (!flag && t.Year != t2.Year)
			{
				try
				{
					DateTime t3 = t.AddYears(1);
					DateTime t4 = t2.AddYears(1);
					flag = (time >= t3 && time < t4);
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime t3 = t.AddYears(-1);
						DateTime t4 = t2.AddYears(-1);
						flag = (time >= t3 && time < t4);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0003AAD8 File Offset: 0x00038CD8
		[SecuritySafeCritical]
		private static TimeZoneInfo GetLocalTimeZone(TimeZoneInfo.CachedData cachedData)
		{
			Win32Native.DynamicTimeZoneInformation dynamicTimeZoneInformation = default(Win32Native.DynamicTimeZoneInformation);
			long num = (long)UnsafeNativeMethods.GetDynamicTimeZoneInformation(out dynamicTimeZoneInformation);
			if (num == -1L)
			{
				return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
			}
			Win32Native.TimeZoneInformation timeZoneInformation = new Win32Native.TimeZoneInformation(dynamicTimeZoneInformation);
			bool dynamicDaylightTimeDisabled = dynamicTimeZoneInformation.DynamicDaylightTimeDisabled;
			TimeZoneInfo result;
			Exception ex;
			if (!string.IsNullOrEmpty(dynamicTimeZoneInformation.TimeZoneKeyName) && TimeZoneInfo.TryGetTimeZone(dynamicTimeZoneInformation.TimeZoneKeyName, dynamicDaylightTimeDisabled, out result, out ex, cachedData) == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return result;
			}
			string text = TimeZoneInfo.FindIdFromTimeZoneInformation(timeZoneInformation, out dynamicDaylightTimeDisabled);
			TimeZoneInfo result2;
			Exception ex2;
			if (text != null && TimeZoneInfo.TryGetTimeZone(text, dynamicDaylightTimeDisabled, out result2, out ex2, cachedData) == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return result2;
			}
			return TimeZoneInfo.GetLocalTimeZoneFromWin32Data(timeZoneInformation, dynamicDaylightTimeDisabled);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0003AB74 File Offset: 0x00038D74
		[SecurityCritical]
		private static TimeZoneInfo GetLocalTimeZoneFromWin32Data(Win32Native.TimeZoneInformation timeZoneInformation, bool dstDisabled)
		{
			try
			{
				return new TimeZoneInfo(timeZoneInformation, dstDisabled);
			}
			catch (ArgumentException)
			{
			}
			catch (InvalidTimeZoneException)
			{
			}
			if (!dstDisabled)
			{
				try
				{
					return new TimeZoneInfo(timeZoneInformation, true);
				}
				catch (ArgumentException)
				{
				}
				catch (InvalidTimeZoneException)
				{
				}
			}
			return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0003ABF4 File Offset: 0x00038DF4
		[__DynamicallyInvokable]
		public static TimeZoneInfo FindSystemTimeZoneById(string id)
		{
			if (string.Compare(id, "UTC", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return TimeZoneInfo.Utc;
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0 || id.Length > 255 || id.Contains("\0"))
			{
				throw new TimeZoneNotFoundException(Environment.GetResourceString("TimeZoneNotFound_MissingRegistryData", new object[]
				{
					id
				}));
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo.CachedData obj = cachedData;
			TimeZoneInfo result;
			Exception ex;
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult;
			lock (obj)
			{
				timeZoneInfoResult = TimeZoneInfo.TryGetTimeZone(id, false, out result, out ex, cachedData);
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return result;
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException)
			{
				throw new InvalidTimeZoneException(Environment.GetResourceString("InvalidTimeZone_InvalidRegistryData", new object[]
				{
					id
				}), ex);
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.SecurityException)
			{
				throw new SecurityException(Environment.GetResourceString("Security_CannotReadRegistryData", new object[]
				{
					id
				}), ex);
			}
			throw new TimeZoneNotFoundException(Environment.GetResourceString("TimeZoneNotFound_MissingRegistryData", new object[]
			{
				id
			}), ex);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0003ACFC File Offset: 0x00038EFC
		private static TimeSpan GetUtcOffset(DateTime time, TimeZoneInfo zone, TimeZoneInfoOptions flags)
		{
			TimeSpan timeSpan = zone.BaseUtcOffset;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(time);
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(time.Year, adjustmentRuleForTime);
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(time, adjustmentRuleForTime, daylightTime, flags);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0003AD60 File Offset: 0x00038F60
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out flag);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0003AD78 File Offset: 0x00038F78
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out isDaylightSavings, out flag);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0003AD90 File Offset: 0x00038F90
		internal static TimeSpan GetDateTimeNowUtcOffsetFromUtc(DateTime time, out bool isAmbiguousLocalDst)
		{
			isAmbiguousLocalDst = false;
			int year = time.Year;
			TimeZoneInfo.OffsetAndRule oneYearLocalFromUtc = TimeZoneInfo.s_cachedData.GetOneYearLocalFromUtc(year);
			TimeSpan timeSpan = oneYearLocalFromUtc.offset;
			if (oneYearLocalFromUtc.rule != null)
			{
				timeSpan += oneYearLocalFromUtc.rule.BaseUtcOffsetDelta;
				if (oneYearLocalFromUtc.rule.HasDaylightSaving)
				{
					bool isDaylightSavingsFromUtc = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, year, oneYearLocalFromUtc.offset, oneYearLocalFromUtc.rule, out isAmbiguousLocalDst, TimeZoneInfo.Local);
					timeSpan += (isDaylightSavingsFromUtc ? oneYearLocalFromUtc.rule.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0003AE1C File Offset: 0x0003901C
		internal static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings, out bool isAmbiguousLocalDst)
		{
			isDaylightSavings = false;
			isAmbiguousLocalDst = false;
			TimeSpan timeSpan = zone.BaseUtcOffset;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime;
			int year;
			if (time > TimeZoneInfo.s_maxDateOnly)
			{
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(DateTime.MaxValue);
				year = 9999;
			}
			else if (time < TimeZoneInfo.s_minDateOnly)
			{
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(DateTime.MinValue);
				year = 1;
			}
			else
			{
				DateTime dateTime = time + timeSpan;
				year = dateTime.Year;
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(dateTime);
			}
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					isDaylightSavings = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, year, zone.m_baseUtcOffset, adjustmentRuleForTime, out isAmbiguousLocalDst, zone);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0003AED0 File Offset: 0x000390D0
		[SecurityCritical]
		private static bool TransitionTimeFromTimeZoneInformation(Win32Native.RegistryTimeZoneInformation timeZoneInformation, out TimeZoneInfo.TransitionTime transitionTime, bool readStartDate)
		{
			if (timeZoneInformation.StandardDate.Month == 0)
			{
				transitionTime = default(TimeZoneInfo.TransitionTime);
				return false;
			}
			if (readStartDate)
			{
				if (timeZoneInformation.DaylightDate.Year == 0)
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.DaylightDate.Hour, (int)timeZoneInformation.DaylightDate.Minute, (int)timeZoneInformation.DaylightDate.Second, (int)timeZoneInformation.DaylightDate.Milliseconds), (int)timeZoneInformation.DaylightDate.Month, (int)timeZoneInformation.DaylightDate.Day, (DayOfWeek)timeZoneInformation.DaylightDate.DayOfWeek);
				}
				else
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.DaylightDate.Hour, (int)timeZoneInformation.DaylightDate.Minute, (int)timeZoneInformation.DaylightDate.Second, (int)timeZoneInformation.DaylightDate.Milliseconds), (int)timeZoneInformation.DaylightDate.Month, (int)timeZoneInformation.DaylightDate.Day);
				}
			}
			else if (timeZoneInformation.StandardDate.Year == 0)
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.StandardDate.Hour, (int)timeZoneInformation.StandardDate.Minute, (int)timeZoneInformation.StandardDate.Second, (int)timeZoneInformation.StandardDate.Milliseconds), (int)timeZoneInformation.StandardDate.Month, (int)timeZoneInformation.StandardDate.Day, (DayOfWeek)timeZoneInformation.StandardDate.DayOfWeek);
			}
			else
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.StandardDate.Hour, (int)timeZoneInformation.StandardDate.Minute, (int)timeZoneInformation.StandardDate.Second, (int)timeZoneInformation.StandardDate.Milliseconds), (int)timeZoneInformation.StandardDate.Month, (int)timeZoneInformation.StandardDate.Day);
			}
			return true;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0003B090 File Offset: 0x00039290
		private static DateTime TransitionTimeToDateTime(int year, TimeZoneInfo.TransitionTime transitionTime)
		{
			DateTime timeOfDay = transitionTime.TimeOfDay;
			DateTime result;
			if (transitionTime.IsFixedDateRule)
			{
				int num = DateTime.DaysInMonth(year, transitionTime.Month);
				result = new DateTime(year, transitionTime.Month, (num < transitionTime.Day) ? num : transitionTime.Day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
			}
			else if (transitionTime.Week <= 4)
			{
				result = new DateTime(year, transitionTime.Month, 1, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
				int dayOfWeek = (int)result.DayOfWeek;
				int num2 = transitionTime.DayOfWeek - (DayOfWeek)dayOfWeek;
				if (num2 < 0)
				{
					num2 += 7;
				}
				num2 += 7 * (transitionTime.Week - 1);
				if (num2 > 0)
				{
					result = result.AddDays((double)num2);
				}
			}
			else
			{
				int day = DateTime.DaysInMonth(year, transitionTime.Month);
				result = new DateTime(year, transitionTime.Month, day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
				int dayOfWeek2 = (int)result.DayOfWeek;
				int num3 = dayOfWeek2 - (int)transitionTime.DayOfWeek;
				if (num3 < 0)
				{
					num3 += 7;
				}
				if (num3 > 0)
				{
					result = result.AddDays((double)(-(double)num3));
				}
			}
			return result;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0003B1E4 File Offset: 0x000393E4
		[SecurityCritical]
		private static bool TryCreateAdjustmentRules(string id, Win32Native.RegistryTimeZoneInformation defaultTimeZoneInformation, out TimeZoneInfo.AdjustmentRule[] rules, out Exception e, int defaultBaseUtcOffset)
		{
			e = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format(CultureInfo.InvariantCulture, "{0}\\{1}\\Dynamic DST", "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
				{
					if (registryKey == null)
					{
						TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(defaultTimeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, defaultBaseUtcOffset);
						if (adjustmentRule == null)
						{
							rules = null;
						}
						else
						{
							rules = new TimeZoneInfo.AdjustmentRule[1];
							rules[0] = adjustmentRule;
						}
						return true;
					}
					int num = (int)registryKey.GetValue("FirstEntry", -1, RegistryValueOptions.None);
					int num2 = (int)registryKey.GetValue("LastEntry", -1, RegistryValueOptions.None);
					if (num == -1 || num2 == -1 || num > num2)
					{
						rules = null;
						return false;
					}
					byte[] array = registryKey.GetValue(num.ToString(CultureInfo.InvariantCulture), null, RegistryValueOptions.None) as byte[];
					if (array == null || array.Length != 44)
					{
						rules = null;
						return false;
					}
					Win32Native.RegistryTimeZoneInformation timeZoneInformation = new Win32Native.RegistryTimeZoneInformation(array);
					if (num == num2)
					{
						TimeZoneInfo.AdjustmentRule adjustmentRule2 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, defaultBaseUtcOffset);
						if (adjustmentRule2 == null)
						{
							rules = null;
						}
						else
						{
							rules = new TimeZoneInfo.AdjustmentRule[1];
							rules[0] = adjustmentRule2;
						}
						return true;
					}
					List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>(1);
					TimeZoneInfo.AdjustmentRule adjustmentRule3 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation, DateTime.MinValue.Date, new DateTime(num, 12, 31), defaultBaseUtcOffset);
					if (adjustmentRule3 != null)
					{
						list.Add(adjustmentRule3);
					}
					for (int i = num + 1; i < num2; i++)
					{
						array = (registryKey.GetValue(i.ToString(CultureInfo.InvariantCulture), null, RegistryValueOptions.None) as byte[]);
						if (array == null || array.Length != 44)
						{
							rules = null;
							return false;
						}
						timeZoneInformation = new Win32Native.RegistryTimeZoneInformation(array);
						TimeZoneInfo.AdjustmentRule adjustmentRule4 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation, new DateTime(i, 1, 1), new DateTime(i, 12, 31), defaultBaseUtcOffset);
						if (adjustmentRule4 != null)
						{
							list.Add(adjustmentRule4);
						}
					}
					array = (registryKey.GetValue(num2.ToString(CultureInfo.InvariantCulture), null, RegistryValueOptions.None) as byte[]);
					timeZoneInformation = new Win32Native.RegistryTimeZoneInformation(array);
					if (array == null || array.Length != 44)
					{
						rules = null;
						return false;
					}
					TimeZoneInfo.AdjustmentRule adjustmentRule5 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation, new DateTime(num2, 1, 1), DateTime.MaxValue.Date, defaultBaseUtcOffset);
					if (adjustmentRule5 != null)
					{
						list.Add(adjustmentRule5);
					}
					rules = list.ToArray();
					if (rules != null && rules.Length == 0)
					{
						rules = null;
					}
				}
			}
			catch (InvalidCastException ex)
			{
				rules = null;
				e = ex;
				return false;
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				rules = null;
				e = ex2;
				return false;
			}
			catch (ArgumentException ex3)
			{
				rules = null;
				e = ex3;
				return false;
			}
			return true;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0003B4F8 File Offset: 0x000396F8
		[SecurityCritical]
		private static bool TryCompareStandardDate(Win32Native.TimeZoneInformation timeZone, Win32Native.RegistryTimeZoneInformation registryTimeZoneInfo)
		{
			return timeZone.Bias == registryTimeZoneInfo.Bias && timeZone.StandardBias == registryTimeZoneInfo.StandardBias && timeZone.StandardDate.Year == registryTimeZoneInfo.StandardDate.Year && timeZone.StandardDate.Month == registryTimeZoneInfo.StandardDate.Month && timeZone.StandardDate.DayOfWeek == registryTimeZoneInfo.StandardDate.DayOfWeek && timeZone.StandardDate.Day == registryTimeZoneInfo.StandardDate.Day && timeZone.StandardDate.Hour == registryTimeZoneInfo.StandardDate.Hour && timeZone.StandardDate.Minute == registryTimeZoneInfo.StandardDate.Minute && timeZone.StandardDate.Second == registryTimeZoneInfo.StandardDate.Second && timeZone.StandardDate.Milliseconds == registryTimeZoneInfo.StandardDate.Milliseconds;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0003B5F0 File Offset: 0x000397F0
		[SecuritySafeCritical]
		private static bool TryCompareTimeZoneInformationToRegistry(Win32Native.TimeZoneInformation timeZone, string id, out bool dstDisabled)
		{
			dstDisabled = false;
			bool result;
			try
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
				permissionSet.Assert();
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
				{
					if (registryKey == null)
					{
						result = false;
					}
					else
					{
						byte[] array = (byte[])registryKey.GetValue("TZI", null, RegistryValueOptions.None);
						if (array == null || array.Length != 44)
						{
							result = false;
						}
						else
						{
							Win32Native.RegistryTimeZoneInformation registryTimeZoneInformation = new Win32Native.RegistryTimeZoneInformation(array);
							if (!TimeZoneInfo.TryCompareStandardDate(timeZone, registryTimeZoneInformation))
							{
								result = false;
							}
							else
							{
								bool flag = dstDisabled || TimeZoneInfo.CheckDaylightSavingTimeNotSupported(timeZone) || (timeZone.DaylightBias == registryTimeZoneInformation.DaylightBias && timeZone.DaylightDate.Year == registryTimeZoneInformation.DaylightDate.Year && timeZone.DaylightDate.Month == registryTimeZoneInformation.DaylightDate.Month && timeZone.DaylightDate.DayOfWeek == registryTimeZoneInformation.DaylightDate.DayOfWeek && timeZone.DaylightDate.Day == registryTimeZoneInformation.DaylightDate.Day && timeZone.DaylightDate.Hour == registryTimeZoneInformation.DaylightDate.Hour && timeZone.DaylightDate.Minute == registryTimeZoneInformation.DaylightDate.Minute && timeZone.DaylightDate.Second == registryTimeZoneInformation.DaylightDate.Second && timeZone.DaylightDate.Milliseconds == registryTimeZoneInformation.DaylightDate.Milliseconds);
								if (flag)
								{
									string strA = registryKey.GetValue("Std", string.Empty, RegistryValueOptions.None) as string;
									flag = (string.Compare(strA, timeZone.StandardName, StringComparison.Ordinal) == 0);
								}
								result = flag;
							}
						}
					}
				}
			}
			finally
			{
				PermissionSet.RevertAssert();
			}
			return result;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0003B800 File Offset: 0x00039A00
		[SecuritySafeCritical]
		[FileIOPermission(SecurityAction.Assert, AllLocalFiles = FileIOPermissionAccess.PathDiscovery)]
		private static string TryGetLocalizedNameByMuiNativeResource(string resource)
		{
			if (string.IsNullOrEmpty(resource))
			{
				return string.Empty;
			}
			string[] array = resource.Split(new char[]
			{
				','
			}, StringSplitOptions.None);
			if (array.Length != 2)
			{
				return string.Empty;
			}
			string path = Environment.UnsafeGetFolderPath(Environment.SpecialFolder.System);
			string path2 = array[0].TrimStart(new char[]
			{
				'@'
			});
			string filePath;
			try
			{
				filePath = Path.Combine(path, path2);
			}
			catch (ArgumentException)
			{
				return string.Empty;
			}
			int num;
			if (!int.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				return string.Empty;
			}
			num = -num;
			string result;
			try
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(260);
				stringBuilder.Length = 260;
				int num2 = 260;
				int num3 = 0;
				long num4 = 0L;
				if (!UnsafeNativeMethods.GetFileMUIPath(16, filePath, null, ref num3, stringBuilder, ref num2, ref num4))
				{
					StringBuilderCache.Release(stringBuilder);
					result = string.Empty;
				}
				else
				{
					result = TimeZoneInfo.TryGetLocalizedNameByNativeResource(StringBuilderCache.GetStringAndRelease(stringBuilder), num);
				}
			}
			catch (EntryPointNotFoundException)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0003B910 File Offset: 0x00039B10
		[SecurityCritical]
		private static string TryGetLocalizedNameByNativeResource(string filePath, int resource)
		{
			using (SafeLibraryHandle safeLibraryHandle = UnsafeNativeMethods.LoadLibraryEx(filePath, IntPtr.Zero, 2))
			{
				if (!safeLibraryHandle.IsInvalid)
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(500);
					stringBuilder.Length = 500;
					int num = UnsafeNativeMethods.LoadString(safeLibraryHandle, resource, stringBuilder, stringBuilder.Length);
					if (num != 0)
					{
						return StringBuilderCache.GetStringAndRelease(stringBuilder);
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0003B988 File Offset: 0x00039B88
		private static bool TryGetLocalizedNamesByRegistryKey(RegistryKey key, out string displayName, out string standardName, out string daylightName)
		{
			displayName = string.Empty;
			standardName = string.Empty;
			daylightName = string.Empty;
			string text = key.GetValue("MUI_Display", string.Empty, RegistryValueOptions.None) as string;
			string text2 = key.GetValue("MUI_Std", string.Empty, RegistryValueOptions.None) as string;
			string text3 = key.GetValue("MUI_Dlt", string.Empty, RegistryValueOptions.None) as string;
			if (!string.IsNullOrEmpty(text))
			{
				displayName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				standardName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text2);
			}
			if (!string.IsNullOrEmpty(text3))
			{
				daylightName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text3);
			}
			if (string.IsNullOrEmpty(displayName))
			{
				displayName = (key.GetValue("Display", string.Empty, RegistryValueOptions.None) as string);
			}
			if (string.IsNullOrEmpty(standardName))
			{
				standardName = (key.GetValue("Std", string.Empty, RegistryValueOptions.None) as string);
			}
			if (string.IsNullOrEmpty(daylightName))
			{
				daylightName = (key.GetValue("Dlt", string.Empty, RegistryValueOptions.None) as string);
			}
			return true;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0003BA84 File Offset: 0x00039C84
		[SecuritySafeCritical]
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneByRegistryKey(string id, out TimeZoneInfo value, out Exception e)
		{
			e = null;
			TimeZoneInfo.TimeZoneInfoResult result;
			try
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
				permissionSet.Assert();
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
				{
					if (registryKey == null)
					{
						value = null;
						result = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
					}
					else
					{
						byte[] array = registryKey.GetValue("TZI", null, RegistryValueOptions.None) as byte[];
						if (array == null || array.Length != 44)
						{
							value = null;
							result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
						}
						else
						{
							Win32Native.RegistryTimeZoneInformation registryTimeZoneInformation = new Win32Native.RegistryTimeZoneInformation(array);
							TimeZoneInfo.AdjustmentRule[] adjustmentRules;
							string displayName;
							string standardDisplayName;
							string daylightDisplayName;
							if (!TimeZoneInfo.TryCreateAdjustmentRules(id, registryTimeZoneInformation, out adjustmentRules, out e, registryTimeZoneInformation.Bias))
							{
								value = null;
								result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
							}
							else if (!TimeZoneInfo.TryGetLocalizedNamesByRegistryKey(registryKey, out displayName, out standardDisplayName, out daylightDisplayName))
							{
								value = null;
								result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
							}
							else
							{
								try
								{
									value = new TimeZoneInfo(id, new TimeSpan(0, -registryTimeZoneInformation.Bias, 0), displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
									result = TimeZoneInfo.TimeZoneInfoResult.Success;
								}
								catch (ArgumentException ex)
								{
									value = null;
									e = ex;
									result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
								}
								catch (InvalidTimeZoneException ex2)
								{
									value = null;
									e = ex2;
									result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
								}
							}
						}
					}
				}
			}
			finally
			{
				PermissionSet.RevertAssert();
			}
			return result;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0003BBD0 File Offset: 0x00039DD0
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZone(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData)
		{
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.Success;
			e = null;
			TimeZoneInfo timeZoneInfo = null;
			if (cachedData.m_systemTimeZones != null && cachedData.m_systemTimeZones.TryGetValue(id, out timeZoneInfo))
			{
				if (dstDisabled && timeZoneInfo.m_supportsDaylightSavingTime)
				{
					value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName);
				}
				else
				{
					value = new TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, false);
				}
				return timeZoneInfoResult;
			}
			if (!cachedData.m_allSystemTimeZonesRead)
			{
				timeZoneInfoResult = TimeZoneInfo.TryGetTimeZoneByRegistryKey(id, out timeZoneInfo, out e);
				if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.Success)
				{
					if (cachedData.m_systemTimeZones == null)
					{
						cachedData.m_systemTimeZones = new Dictionary<string, TimeZoneInfo>();
					}
					cachedData.m_systemTimeZones.Add(id, timeZoneInfo);
					if (dstDisabled && timeZoneInfo.m_supportsDaylightSavingTime)
					{
						value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName);
					}
					else
					{
						value = new TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, false);
					}
				}
				else
				{
					value = null;
				}
			}
			else
			{
				timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
				value = null;
			}
			return timeZoneInfoResult;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0003BCF9 File Offset: 0x00039EF9
		internal static bool UtcOffsetOutOfRange(TimeSpan offset)
		{
			return offset.TotalHours < -14.0 || offset.TotalHours > 14.0;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0003BD24 File Offset: 0x00039F24
		private static void ValidateTimeZoneInfo(string id, TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule[] adjustmentRules, out bool adjustmentRulesSupportDst)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidId", new object[]
				{
					id
				}), "id");
			}
			if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset", Environment.GetResourceString("ArgumentOutOfRange_UtcOffset"));
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TimeSpanHasSeconds"), "baseUtcOffset");
			}
			adjustmentRulesSupportDst = false;
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				adjustmentRulesSupportDst = true;
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					TimeZoneInfo.AdjustmentRule adjustmentRule2 = adjustmentRule;
					adjustmentRule = adjustmentRules[i];
					if (adjustmentRule == null)
					{
						throw new InvalidTimeZoneException(Environment.GetResourceString("Argument_AdjustmentRulesNoNulls"));
					}
					if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset + adjustmentRule.DaylightDelta))
					{
						throw new InvalidTimeZoneException(Environment.GetResourceString("ArgumentOutOfRange_UtcOffsetAndDaylightDelta"));
					}
					if (adjustmentRule2 != null && adjustmentRule.DateStart <= adjustmentRule2.DateEnd)
					{
						throw new InvalidTimeZoneException(Environment.GetResourceString("Argument_AdjustmentRulesOutOfOrder"));
					}
				}
			}
		}

		// Token: 0x040006AD RID: 1709
		private string m_id;

		// Token: 0x040006AE RID: 1710
		private string m_displayName;

		// Token: 0x040006AF RID: 1711
		private string m_standardDisplayName;

		// Token: 0x040006B0 RID: 1712
		private string m_daylightDisplayName;

		// Token: 0x040006B1 RID: 1713
		private TimeSpan m_baseUtcOffset;

		// Token: 0x040006B2 RID: 1714
		private bool m_supportsDaylightSavingTime;

		// Token: 0x040006B3 RID: 1715
		private TimeZoneInfo.AdjustmentRule[] m_adjustmentRules;

		// Token: 0x040006B4 RID: 1716
		private const string c_timeZonesRegistryHive = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones";

		// Token: 0x040006B5 RID: 1717
		private const string c_timeZonesRegistryHivePermissionList = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones";

		// Token: 0x040006B6 RID: 1718
		private const string c_displayValue = "Display";

		// Token: 0x040006B7 RID: 1719
		private const string c_daylightValue = "Dlt";

		// Token: 0x040006B8 RID: 1720
		private const string c_standardValue = "Std";

		// Token: 0x040006B9 RID: 1721
		private const string c_muiDisplayValue = "MUI_Display";

		// Token: 0x040006BA RID: 1722
		private const string c_muiDaylightValue = "MUI_Dlt";

		// Token: 0x040006BB RID: 1723
		private const string c_muiStandardValue = "MUI_Std";

		// Token: 0x040006BC RID: 1724
		private const string c_timeZoneInfoValue = "TZI";

		// Token: 0x040006BD RID: 1725
		private const string c_firstEntryValue = "FirstEntry";

		// Token: 0x040006BE RID: 1726
		private const string c_lastEntryValue = "LastEntry";

		// Token: 0x040006BF RID: 1727
		private const string c_utcId = "UTC";

		// Token: 0x040006C0 RID: 1728
		private const string c_localId = "Local";

		// Token: 0x040006C1 RID: 1729
		private const int c_maxKeyLength = 255;

		// Token: 0x040006C2 RID: 1730
		private const int c_regByteLength = 44;

		// Token: 0x040006C3 RID: 1731
		private const long c_ticksPerMillisecond = 10000L;

		// Token: 0x040006C4 RID: 1732
		private const long c_ticksPerSecond = 10000000L;

		// Token: 0x040006C5 RID: 1733
		private const long c_ticksPerMinute = 600000000L;

		// Token: 0x040006C6 RID: 1734
		private const long c_ticksPerHour = 36000000000L;

		// Token: 0x040006C7 RID: 1735
		private const long c_ticksPerDay = 864000000000L;

		// Token: 0x040006C8 RID: 1736
		private const long c_ticksPerDayRange = 863999990000L;

		// Token: 0x040006C9 RID: 1737
		private static TimeZoneInfo.CachedData s_cachedData = new TimeZoneInfo.CachedData();

		// Token: 0x040006CA RID: 1738
		private static DateTime s_maxDateOnly = new DateTime(9999, 12, 31);

		// Token: 0x040006CB RID: 1739
		private static DateTime s_minDateOnly = new DateTime(1, 1, 2);

		// Token: 0x02000ACF RID: 2767
		private enum TimeZoneInfoResult
		{
			// Token: 0x0400312D RID: 12589
			Success,
			// Token: 0x0400312E RID: 12590
			TimeZoneNotFoundException,
			// Token: 0x0400312F RID: 12591
			InvalidTimeZoneException,
			// Token: 0x04003130 RID: 12592
			SecurityException
		}

		// Token: 0x02000AD0 RID: 2768
		private class CachedData
		{
			// Token: 0x06006929 RID: 26921 RVA: 0x001692B4 File Offset: 0x001674B4
			private TimeZoneInfo CreateLocal()
			{
				TimeZoneInfo result;
				lock (this)
				{
					TimeZoneInfo timeZoneInfo = this.m_localTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = TimeZoneInfo.GetLocalTimeZone(this);
						timeZoneInfo = new TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, false);
						this.m_localTimeZone = timeZoneInfo;
					}
					result = timeZoneInfo;
				}
				return result;
			}

			// Token: 0x170011E1 RID: 4577
			// (get) Token: 0x0600692A RID: 26922 RVA: 0x00169334 File Offset: 0x00167534
			public TimeZoneInfo Local
			{
				get
				{
					TimeZoneInfo timeZoneInfo = this.m_localTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = this.CreateLocal();
					}
					return timeZoneInfo;
				}
			}

			// Token: 0x0600692B RID: 26923 RVA: 0x00169358 File Offset: 0x00167558
			private TimeZoneInfo CreateUtc()
			{
				TimeZoneInfo result;
				lock (this)
				{
					TimeZoneInfo timeZoneInfo = this.m_utcTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = TimeZoneInfo.CreateCustomTimeZone("UTC", TimeSpan.Zero, "UTC", "UTC");
						this.m_utcTimeZone = timeZoneInfo;
					}
					result = timeZoneInfo;
				}
				return result;
			}

			// Token: 0x170011E2 RID: 4578
			// (get) Token: 0x0600692C RID: 26924 RVA: 0x001693C0 File Offset: 0x001675C0
			public TimeZoneInfo Utc
			{
				get
				{
					TimeZoneInfo timeZoneInfo = this.m_utcTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = this.CreateUtc();
					}
					return timeZoneInfo;
				}
			}

			// Token: 0x0600692D RID: 26925 RVA: 0x001693E4 File Offset: 0x001675E4
			public DateTimeKind GetCorrespondingKind(TimeZoneInfo timeZone)
			{
				DateTimeKind result;
				if (timeZone == this.m_utcTimeZone)
				{
					result = DateTimeKind.Utc;
				}
				else if (timeZone == this.m_localTimeZone)
				{
					result = DateTimeKind.Local;
				}
				else
				{
					result = DateTimeKind.Unspecified;
				}
				return result;
			}

			// Token: 0x0600692E RID: 26926 RVA: 0x00169414 File Offset: 0x00167614
			[SecuritySafeCritical]
			private static TimeZoneInfo GetCurrentOneYearLocal()
			{
				Win32Native.TimeZoneInformation timeZoneInformation = default(Win32Native.TimeZoneInformation);
				long num = (long)UnsafeNativeMethods.GetTimeZoneInformation(out timeZoneInformation);
				TimeZoneInfo result;
				if (num == -1L)
				{
					result = TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
				}
				else
				{
					result = TimeZoneInfo.GetLocalTimeZoneFromWin32Data(timeZoneInformation, false);
				}
				return result;
			}

			// Token: 0x0600692F RID: 26927 RVA: 0x0016945C File Offset: 0x0016765C
			public TimeZoneInfo.OffsetAndRule GetOneYearLocalFromUtc(int year)
			{
				TimeZoneInfo.OffsetAndRule offsetAndRule = this.m_oneYearLocalFromUtc;
				if (offsetAndRule == null || offsetAndRule.year != year)
				{
					TimeZoneInfo currentOneYearLocal = TimeZoneInfo.CachedData.GetCurrentOneYearLocal();
					TimeZoneInfo.AdjustmentRule rule = (currentOneYearLocal.m_adjustmentRules == null) ? null : currentOneYearLocal.m_adjustmentRules[0];
					offsetAndRule = new TimeZoneInfo.OffsetAndRule(year, currentOneYearLocal.BaseUtcOffset, rule);
					this.m_oneYearLocalFromUtc = offsetAndRule;
				}
				return offsetAndRule;
			}

			// Token: 0x04003131 RID: 12593
			private volatile TimeZoneInfo m_localTimeZone;

			// Token: 0x04003132 RID: 12594
			private volatile TimeZoneInfo m_utcTimeZone;

			// Token: 0x04003133 RID: 12595
			public Dictionary<string, TimeZoneInfo> m_systemTimeZones;

			// Token: 0x04003134 RID: 12596
			public ReadOnlyCollection<TimeZoneInfo> m_readOnlySystemTimeZones;

			// Token: 0x04003135 RID: 12597
			public bool m_allSystemTimeZonesRead;

			// Token: 0x04003136 RID: 12598
			private volatile TimeZoneInfo.OffsetAndRule m_oneYearLocalFromUtc;
		}

		// Token: 0x02000AD1 RID: 2769
		private class OffsetAndRule
		{
			// Token: 0x06006931 RID: 26929 RVA: 0x001694B8 File Offset: 0x001676B8
			public OffsetAndRule(int year, TimeSpan offset, TimeZoneInfo.AdjustmentRule rule)
			{
				this.year = year;
				this.offset = offset;
				this.rule = rule;
			}

			// Token: 0x04003137 RID: 12599
			public int year;

			// Token: 0x04003138 RID: 12600
			public TimeSpan offset;

			// Token: 0x04003139 RID: 12601
			public TimeZoneInfo.AdjustmentRule rule;
		}

		// Token: 0x02000AD2 RID: 2770
		[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		[Serializable]
		public sealed class AdjustmentRule : IEquatable<TimeZoneInfo.AdjustmentRule>, ISerializable, IDeserializationCallback
		{
			// Token: 0x170011E3 RID: 4579
			// (get) Token: 0x06006932 RID: 26930 RVA: 0x001694D5 File Offset: 0x001676D5
			public DateTime DateStart
			{
				get
				{
					return this.m_dateStart;
				}
			}

			// Token: 0x170011E4 RID: 4580
			// (get) Token: 0x06006933 RID: 26931 RVA: 0x001694DD File Offset: 0x001676DD
			public DateTime DateEnd
			{
				get
				{
					return this.m_dateEnd;
				}
			}

			// Token: 0x170011E5 RID: 4581
			// (get) Token: 0x06006934 RID: 26932 RVA: 0x001694E5 File Offset: 0x001676E5
			public TimeSpan DaylightDelta
			{
				get
				{
					return this.m_daylightDelta;
				}
			}

			// Token: 0x170011E6 RID: 4582
			// (get) Token: 0x06006935 RID: 26933 RVA: 0x001694ED File Offset: 0x001676ED
			public TimeZoneInfo.TransitionTime DaylightTransitionStart
			{
				get
				{
					return this.m_daylightTransitionStart;
				}
			}

			// Token: 0x170011E7 RID: 4583
			// (get) Token: 0x06006936 RID: 26934 RVA: 0x001694F5 File Offset: 0x001676F5
			public TimeZoneInfo.TransitionTime DaylightTransitionEnd
			{
				get
				{
					return this.m_daylightTransitionEnd;
				}
			}

			// Token: 0x170011E8 RID: 4584
			// (get) Token: 0x06006937 RID: 26935 RVA: 0x001694FD File Offset: 0x001676FD
			internal TimeSpan BaseUtcOffsetDelta
			{
				get
				{
					return this.m_baseUtcOffsetDelta;
				}
			}

			// Token: 0x170011E9 RID: 4585
			// (get) Token: 0x06006938 RID: 26936 RVA: 0x00169508 File Offset: 0x00167708
			internal bool HasDaylightSaving
			{
				get
				{
					return this.DaylightDelta != TimeSpan.Zero || this.DaylightTransitionStart.TimeOfDay != DateTime.MinValue || this.DaylightTransitionEnd.TimeOfDay != DateTime.MinValue.AddMilliseconds(1.0);
				}
			}

			// Token: 0x06006939 RID: 26937 RVA: 0x0016956C File Offset: 0x0016776C
			public bool Equals(TimeZoneInfo.AdjustmentRule other)
			{
				bool flag = other != null && this.m_dateStart == other.m_dateStart && this.m_dateEnd == other.m_dateEnd && this.m_daylightDelta == other.m_daylightDelta && this.m_baseUtcOffsetDelta == other.m_baseUtcOffsetDelta;
				return flag && this.m_daylightTransitionEnd.Equals(other.m_daylightTransitionEnd) && this.m_daylightTransitionStart.Equals(other.m_daylightTransitionStart);
			}

			// Token: 0x0600693A RID: 26938 RVA: 0x001695F6 File Offset: 0x001677F6
			public override int GetHashCode()
			{
				return this.m_dateStart.GetHashCode();
			}

			// Token: 0x0600693B RID: 26939 RVA: 0x00169603 File Offset: 0x00167803
			private AdjustmentRule()
			{
			}

			// Token: 0x0600693C RID: 26940 RVA: 0x0016960C File Offset: 0x0016780C
			public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
				return new TimeZoneInfo.AdjustmentRule
				{
					m_dateStart = dateStart,
					m_dateEnd = dateEnd,
					m_daylightDelta = daylightDelta,
					m_daylightTransitionStart = daylightTransitionStart,
					m_daylightTransitionEnd = daylightTransitionEnd,
					m_baseUtcOffsetDelta = TimeSpan.Zero
				};
			}

			// Token: 0x0600693D RID: 26941 RVA: 0x0016965C File Offset: 0x0016785C
			internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
				adjustmentRule.m_baseUtcOffsetDelta = baseUtcOffsetDelta;
				return adjustmentRule;
			}

			// Token: 0x0600693E RID: 26942 RVA: 0x00169680 File Offset: 0x00167880
			internal bool IsStartDateMarkerForBeginningOfYear()
			{
				return this.DaylightTransitionStart.Month == 1 && this.DaylightTransitionStart.Day == 1 && this.DaylightTransitionStart.TimeOfDay.Hour == 0 && this.DaylightTransitionStart.TimeOfDay.Minute == 0 && this.DaylightTransitionStart.TimeOfDay.Second == 0 && this.m_dateStart.Year == this.m_dateEnd.Year;
			}

			// Token: 0x0600693F RID: 26943 RVA: 0x00169714 File Offset: 0x00167914
			internal bool IsEndDateMarkerForEndOfYear()
			{
				return this.DaylightTransitionEnd.Month == 1 && this.DaylightTransitionEnd.Day == 1 && this.DaylightTransitionEnd.TimeOfDay.Hour == 0 && this.DaylightTransitionEnd.TimeOfDay.Minute == 0 && this.DaylightTransitionEnd.TimeOfDay.Second == 0 && this.m_dateStart.Year == this.m_dateEnd.Year;
			}

			// Token: 0x06006940 RID: 26944 RVA: 0x001697A8 File Offset: 0x001679A8
			private static void ValidateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				if (dateStart.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), "dateStart");
				}
				if (dateEnd.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), "dateEnd");
				}
				if (daylightTransitionStart.Equals(daylightTransitionEnd))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_TransitionTimesAreIdentical"), "daylightTransitionEnd");
				}
				if (dateStart > dateEnd)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_OutOfOrderDateTimes"), "dateStart");
				}
				if (TimeZoneInfo.UtcOffsetOutOfRange(daylightDelta))
				{
					throw new ArgumentOutOfRangeException("daylightDelta", daylightDelta, Environment.GetResourceString("ArgumentOutOfRange_UtcOffset"));
				}
				if (daylightDelta.Ticks % 600000000L != 0L)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_TimeSpanHasSeconds"), "daylightDelta");
				}
				if (dateStart.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTimeOfDay"), "dateStart");
				}
				if (dateEnd.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTimeOfDay"), "dateEnd");
				}
			}

			// Token: 0x06006941 RID: 26945 RVA: 0x001698C8 File Offset: 0x00167AC8
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(this.m_dateStart, this.m_dateEnd, this.m_daylightDelta, this.m_daylightTransitionStart, this.m_daylightTransitionEnd);
				}
				catch (ArgumentException innerException)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), innerException);
				}
			}

			// Token: 0x06006942 RID: 26946 RVA: 0x0016991C File Offset: 0x00167B1C
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("DateStart", this.m_dateStart);
				info.AddValue("DateEnd", this.m_dateEnd);
				info.AddValue("DaylightDelta", this.m_daylightDelta);
				info.AddValue("DaylightTransitionStart", this.m_daylightTransitionStart);
				info.AddValue("DaylightTransitionEnd", this.m_daylightTransitionEnd);
				info.AddValue("BaseUtcOffsetDelta", this.m_baseUtcOffsetDelta);
			}

			// Token: 0x06006943 RID: 26947 RVA: 0x001699B4 File Offset: 0x00167BB4
			private AdjustmentRule(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_dateStart = (DateTime)info.GetValue("DateStart", typeof(DateTime));
				this.m_dateEnd = (DateTime)info.GetValue("DateEnd", typeof(DateTime));
				this.m_daylightDelta = (TimeSpan)info.GetValue("DaylightDelta", typeof(TimeSpan));
				this.m_daylightTransitionStart = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionStart", typeof(TimeZoneInfo.TransitionTime));
				this.m_daylightTransitionEnd = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionEnd", typeof(TimeZoneInfo.TransitionTime));
				object valueNoThrow = info.GetValueNoThrow("BaseUtcOffsetDelta", typeof(TimeSpan));
				if (valueNoThrow != null)
				{
					this.m_baseUtcOffsetDelta = (TimeSpan)valueNoThrow;
				}
			}

			// Token: 0x0400313A RID: 12602
			private DateTime m_dateStart;

			// Token: 0x0400313B RID: 12603
			private DateTime m_dateEnd;

			// Token: 0x0400313C RID: 12604
			private TimeSpan m_daylightDelta;

			// Token: 0x0400313D RID: 12605
			private TimeZoneInfo.TransitionTime m_daylightTransitionStart;

			// Token: 0x0400313E RID: 12606
			private TimeZoneInfo.TransitionTime m_daylightTransitionEnd;

			// Token: 0x0400313F RID: 12607
			private TimeSpan m_baseUtcOffsetDelta;
		}

		// Token: 0x02000AD3 RID: 2771
		[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		[Serializable]
		public struct TransitionTime : IEquatable<TimeZoneInfo.TransitionTime>, ISerializable, IDeserializationCallback
		{
			// Token: 0x170011EA RID: 4586
			// (get) Token: 0x06006944 RID: 26948 RVA: 0x00169A9A File Offset: 0x00167C9A
			public DateTime TimeOfDay
			{
				get
				{
					return this.m_timeOfDay;
				}
			}

			// Token: 0x170011EB RID: 4587
			// (get) Token: 0x06006945 RID: 26949 RVA: 0x00169AA2 File Offset: 0x00167CA2
			public int Month
			{
				get
				{
					return (int)this.m_month;
				}
			}

			// Token: 0x170011EC RID: 4588
			// (get) Token: 0x06006946 RID: 26950 RVA: 0x00169AAA File Offset: 0x00167CAA
			public int Week
			{
				get
				{
					return (int)this.m_week;
				}
			}

			// Token: 0x170011ED RID: 4589
			// (get) Token: 0x06006947 RID: 26951 RVA: 0x00169AB2 File Offset: 0x00167CB2
			public int Day
			{
				get
				{
					return (int)this.m_day;
				}
			}

			// Token: 0x170011EE RID: 4590
			// (get) Token: 0x06006948 RID: 26952 RVA: 0x00169ABA File Offset: 0x00167CBA
			public DayOfWeek DayOfWeek
			{
				get
				{
					return this.m_dayOfWeek;
				}
			}

			// Token: 0x170011EF RID: 4591
			// (get) Token: 0x06006949 RID: 26953 RVA: 0x00169AC2 File Offset: 0x00167CC2
			public bool IsFixedDateRule
			{
				get
				{
					return this.m_isFixedDateRule;
				}
			}

			// Token: 0x0600694A RID: 26954 RVA: 0x00169ACA File Offset: 0x00167CCA
			public override bool Equals(object obj)
			{
				return obj is TimeZoneInfo.TransitionTime && this.Equals((TimeZoneInfo.TransitionTime)obj);
			}

			// Token: 0x0600694B RID: 26955 RVA: 0x00169AE2 File Offset: 0x00167CE2
			public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return t1.Equals(t2);
			}

			// Token: 0x0600694C RID: 26956 RVA: 0x00169AEC File Offset: 0x00167CEC
			public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return !t1.Equals(t2);
			}

			// Token: 0x0600694D RID: 26957 RVA: 0x00169AFC File Offset: 0x00167CFC
			public bool Equals(TimeZoneInfo.TransitionTime other)
			{
				bool flag = this.m_isFixedDateRule == other.m_isFixedDateRule && this.m_timeOfDay == other.m_timeOfDay && this.m_month == other.m_month;
				if (flag)
				{
					if (other.m_isFixedDateRule)
					{
						flag = (this.m_day == other.m_day);
					}
					else
					{
						flag = (this.m_week == other.m_week && this.m_dayOfWeek == other.m_dayOfWeek);
					}
				}
				return flag;
			}

			// Token: 0x0600694E RID: 26958 RVA: 0x00169B79 File Offset: 0x00167D79
			public override int GetHashCode()
			{
				return (int)this.m_month ^ (int)this.m_week << 8;
			}

			// Token: 0x0600694F RID: 26959 RVA: 0x00169B8A File Offset: 0x00167D8A
			public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
			{
				return TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, 1, day, DayOfWeek.Sunday, true);
			}

			// Token: 0x06006950 RID: 26960 RVA: 0x00169B97 File Offset: 0x00167D97
			public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
			{
				return TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, week, 1, dayOfWeek, false);
			}

			// Token: 0x06006951 RID: 26961 RVA: 0x00169BA4 File Offset: 0x00167DA4
			private static TimeZoneInfo.TransitionTime CreateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek, bool isFixedDateRule)
			{
				TimeZoneInfo.TransitionTime.ValidateTransitionTime(timeOfDay, month, week, day, dayOfWeek);
				return new TimeZoneInfo.TransitionTime
				{
					m_isFixedDateRule = isFixedDateRule,
					m_timeOfDay = timeOfDay,
					m_dayOfWeek = dayOfWeek,
					m_day = (byte)day,
					m_week = (byte)week,
					m_month = (byte)month
				};
			}

			// Token: 0x06006952 RID: 26962 RVA: 0x00169BFC File Offset: 0x00167DFC
			private static void ValidateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek)
			{
				if (timeOfDay.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), "timeOfDay");
				}
				if (month < 1 || month > 12)
				{
					throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_MonthParam"));
				}
				if (day < 1 || day > 31)
				{
					throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("ArgumentOutOfRange_DayParam"));
				}
				if (week < 1 || week > 5)
				{
					throw new ArgumentOutOfRangeException("week", Environment.GetResourceString("ArgumentOutOfRange_Week"));
				}
				if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
				{
					throw new ArgumentOutOfRangeException("dayOfWeek", Environment.GetResourceString("ArgumentOutOfRange_DayOfWeek"));
				}
				int num;
				int num2;
				int num3;
				timeOfDay.GetDatePart(out num, out num2, out num3);
				if (num != 1 || num2 != 1 || num3 != 1 || timeOfDay.Ticks % 10000L != 0L)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTicks"), "timeOfDay");
				}
			}

			// Token: 0x06006953 RID: 26963 RVA: 0x00169CE0 File Offset: 0x00167EE0
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.TransitionTime.ValidateTransitionTime(this.m_timeOfDay, (int)this.m_month, (int)this.m_week, (int)this.m_day, this.m_dayOfWeek);
				}
				catch (ArgumentException innerException)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), innerException);
				}
			}

			// Token: 0x06006954 RID: 26964 RVA: 0x00169D34 File Offset: 0x00167F34
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("TimeOfDay", this.m_timeOfDay);
				info.AddValue("Month", this.m_month);
				info.AddValue("Week", this.m_week);
				info.AddValue("Day", this.m_day);
				info.AddValue("DayOfWeek", this.m_dayOfWeek);
				info.AddValue("IsFixedDateRule", this.m_isFixedDateRule);
			}

			// Token: 0x06006955 RID: 26965 RVA: 0x00169DBC File Offset: 0x00167FBC
			private TransitionTime(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_timeOfDay = (DateTime)info.GetValue("TimeOfDay", typeof(DateTime));
				this.m_month = (byte)info.GetValue("Month", typeof(byte));
				this.m_week = (byte)info.GetValue("Week", typeof(byte));
				this.m_day = (byte)info.GetValue("Day", typeof(byte));
				this.m_dayOfWeek = (DayOfWeek)info.GetValue("DayOfWeek", typeof(DayOfWeek));
				this.m_isFixedDateRule = (bool)info.GetValue("IsFixedDateRule", typeof(bool));
			}

			// Token: 0x04003140 RID: 12608
			private DateTime m_timeOfDay;

			// Token: 0x04003141 RID: 12609
			private byte m_month;

			// Token: 0x04003142 RID: 12610
			private byte m_week;

			// Token: 0x04003143 RID: 12611
			private byte m_day;

			// Token: 0x04003144 RID: 12612
			private DayOfWeek m_dayOfWeek;

			// Token: 0x04003145 RID: 12613
			private bool m_isFixedDateRule;
		}

		// Token: 0x02000AD4 RID: 2772
		private sealed class StringSerializer
		{
			// Token: 0x06006956 RID: 26966 RVA: 0x00169E98 File Offset: 0x00168098
			public static string GetSerializedString(TimeZoneInfo zone)
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.Id));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.BaseUtcOffset.TotalMinutes.ToString(CultureInfo.InvariantCulture)));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DisplayName));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.StandardName));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DaylightName));
				stringBuilder.Append(';');
				TimeZoneInfo.AdjustmentRule[] adjustmentRules = zone.GetAdjustmentRules();
				if (adjustmentRules != null && adjustmentRules.Length != 0)
				{
					foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in adjustmentRules)
					{
						stringBuilder.Append('[');
						stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.DateStart.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo)));
						stringBuilder.Append(';');
						stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.DateEnd.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo)));
						stringBuilder.Append(';');
						stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.DaylightDelta.TotalMinutes.ToString(CultureInfo.InvariantCulture)));
						stringBuilder.Append(';');
						TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionStart, stringBuilder);
						stringBuilder.Append(';');
						TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionEnd, stringBuilder);
						stringBuilder.Append(';');
						if (adjustmentRule.BaseUtcOffsetDelta != TimeSpan.Zero)
						{
							stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.BaseUtcOffsetDelta.TotalMinutes.ToString(CultureInfo.InvariantCulture)));
							stringBuilder.Append(';');
						}
						stringBuilder.Append(']');
					}
				}
				stringBuilder.Append(';');
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}

			// Token: 0x06006957 RID: 26967 RVA: 0x0016A09C File Offset: 0x0016829C
			public static TimeZoneInfo GetDeserializedTimeZoneInfo(string source)
			{
				TimeZoneInfo.StringSerializer stringSerializer = new TimeZoneInfo.StringSerializer(source);
				string nextStringValue = stringSerializer.GetNextStringValue(false);
				TimeSpan nextTimeSpanValue = stringSerializer.GetNextTimeSpanValue(false);
				string nextStringValue2 = stringSerializer.GetNextStringValue(false);
				string nextStringValue3 = stringSerializer.GetNextStringValue(false);
				string nextStringValue4 = stringSerializer.GetNextStringValue(false);
				TimeZoneInfo.AdjustmentRule[] nextAdjustmentRuleArrayValue = stringSerializer.GetNextAdjustmentRuleArrayValue(false);
				TimeZoneInfo result;
				try
				{
					result = TimeZoneInfo.CreateCustomTimeZone(nextStringValue, nextTimeSpanValue, nextStringValue2, nextStringValue3, nextStringValue4, nextAdjustmentRuleArrayValue);
				}
				catch (ArgumentException innerException)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), innerException);
				}
				catch (InvalidTimeZoneException innerException2)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), innerException2);
				}
				return result;
			}

			// Token: 0x06006958 RID: 26968 RVA: 0x0016A13C File Offset: 0x0016833C
			private StringSerializer(string str)
			{
				this.m_serializedText = str;
				this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
			}

			// Token: 0x06006959 RID: 26969 RVA: 0x0016A154 File Offset: 0x00168354
			private static string SerializeSubstitute(string text)
			{
				text = text.Replace("\\", "\\\\");
				text = text.Replace("[", "\\[");
				text = text.Replace("]", "\\]");
				return text.Replace(";", "\\;");
			}

			// Token: 0x0600695A RID: 26970 RVA: 0x0016A1A8 File Offset: 0x001683A8
			private static void SerializeTransitionTime(TimeZoneInfo.TransitionTime time, StringBuilder serializedText)
			{
				serializedText.Append('[');
				serializedText.Append((time.IsFixedDateRule ? 1 : 0).ToString(CultureInfo.InvariantCulture));
				serializedText.Append(';');
				if (time.IsFixedDateRule)
				{
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.TimeOfDay.ToString("HH:mm:ss.FFF", DateTimeFormatInfo.InvariantInfo)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Month.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Day.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
				}
				else
				{
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.TimeOfDay.ToString("HH:mm:ss.FFF", DateTimeFormatInfo.InvariantInfo)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Month.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Week.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(((int)time.DayOfWeek).ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
				}
				serializedText.Append(']');
			}

			// Token: 0x0600695B RID: 26971 RVA: 0x0016A32B File Offset: 0x0016852B
			private static void VerifyIsEscapableCharacter(char c)
			{
				if (c != '\\' && c != ';' && c != '[' && c != ']')
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidEscapeSequence", new object[]
					{
						c
					}));
				}
			}

			// Token: 0x0600695C RID: 26972 RVA: 0x0016A360 File Offset: 0x00168560
			private void SkipVersionNextDataFields(int depth)
			{
				if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
				for (int i = this.m_currentTokenStartIndex; i < this.m_serializedText.Length; i++)
				{
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this.m_serializedText[i]);
						state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					}
					else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
					{
						char c = this.m_serializedText[i];
						if (c == '\0')
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
						}
						switch (c)
						{
						case '[':
							depth++;
							break;
						case '\\':
							state = TimeZoneInfo.StringSerializer.State.Escaped;
							break;
						case ']':
							depth--;
							if (depth == 0)
							{
								this.m_currentTokenStartIndex = i + 1;
								if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
								{
									this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
									return;
								}
								this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
								return;
							}
							break;
						}
					}
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
			}

			// Token: 0x0600695D RID: 26973 RVA: 0x0016A460 File Offset: 0x00168660
			private string GetNextStringValue(bool canEndWithoutSeparator)
			{
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					if (canEndWithoutSeparator)
					{
						return null;
					}
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				else
				{
					if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					StringBuilder stringBuilder = StringBuilderCache.Acquire(64);
					for (int i = this.m_currentTokenStartIndex; i < this.m_serializedText.Length; i++)
					{
						if (state == TimeZoneInfo.StringSerializer.State.Escaped)
						{
							TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this.m_serializedText[i]);
							stringBuilder.Append(this.m_serializedText[i]);
							state = TimeZoneInfo.StringSerializer.State.NotEscaped;
						}
						else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
						{
							char c = this.m_serializedText[i];
							if (c == '\0')
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
							}
							if (c == ';')
							{
								this.m_currentTokenStartIndex = i + 1;
								if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
								{
									this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
								}
								else
								{
									this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
								}
								return StringBuilderCache.GetStringAndRelease(stringBuilder);
							}
							switch (c)
							{
							case '[':
								throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
							case '\\':
								state = TimeZoneInfo.StringSerializer.State.Escaped;
								break;
							case ']':
								if (canEndWithoutSeparator)
								{
									this.m_currentTokenStartIndex = i;
									this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
									return stringBuilder.ToString();
								}
								throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
							default:
								stringBuilder.Append(this.m_serializedText[i]);
								break;
							}
						}
					}
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidEscapeSequence", new object[]
						{
							string.Empty
						}));
					}
					if (!canEndWithoutSeparator)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					this.m_currentTokenStartIndex = this.m_serializedText.Length;
					this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
					return StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
			}

			// Token: 0x0600695E RID: 26974 RVA: 0x0016A630 File Offset: 0x00168830
			private DateTime GetNextDateTimeValue(bool canEndWithoutSeparator, string format)
			{
				string nextStringValue = this.GetNextStringValue(canEndWithoutSeparator);
				DateTime result;
				if (!DateTime.TryParseExact(nextStringValue, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result))
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				return result;
			}

			// Token: 0x0600695F RID: 26975 RVA: 0x0016A668 File Offset: 0x00168868
			private TimeSpan GetNextTimeSpanValue(bool canEndWithoutSeparator)
			{
				int nextInt32Value = this.GetNextInt32Value(canEndWithoutSeparator);
				TimeSpan result;
				try
				{
					result = new TimeSpan(0, nextInt32Value, 0);
				}
				catch (ArgumentOutOfRangeException innerException)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), innerException);
				}
				return result;
			}

			// Token: 0x06006960 RID: 26976 RVA: 0x0016A6AC File Offset: 0x001688AC
			private int GetNextInt32Value(bool canEndWithoutSeparator)
			{
				string nextStringValue = this.GetNextStringValue(canEndWithoutSeparator);
				int result;
				if (!int.TryParse(nextStringValue, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out result))
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				return result;
			}

			// Token: 0x06006961 RID: 26977 RVA: 0x0016A6E4 File Offset: 0x001688E4
			private TimeZoneInfo.AdjustmentRule[] GetNextAdjustmentRuleArrayValue(bool canEndWithoutSeparator)
			{
				List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>(1);
				int num = 0;
				for (TimeZoneInfo.AdjustmentRule nextAdjustmentRuleValue = this.GetNextAdjustmentRuleValue(true); nextAdjustmentRuleValue != null; nextAdjustmentRuleValue = this.GetNextAdjustmentRuleValue(true))
				{
					list.Add(nextAdjustmentRuleValue);
					num++;
				}
				if (!canEndWithoutSeparator)
				{
					if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
				}
				if (num == 0)
				{
					return null;
				}
				return list.ToArray();
			}

			// Token: 0x06006962 RID: 26978 RVA: 0x0016A770 File Offset: 0x00168970
			private TimeZoneInfo.AdjustmentRule GetNextAdjustmentRuleValue(bool canEndWithoutSeparator)
			{
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					if (canEndWithoutSeparator)
					{
						return null;
					}
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				else
				{
					if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					if (this.m_serializedText[this.m_currentTokenStartIndex] == ';')
					{
						return null;
					}
					if (this.m_serializedText[this.m_currentTokenStartIndex] != '[')
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					this.m_currentTokenStartIndex++;
					DateTime nextDateTimeValue = this.GetNextDateTimeValue(false, "MM:dd:yyyy");
					DateTime nextDateTimeValue2 = this.GetNextDateTimeValue(false, "MM:dd:yyyy");
					TimeSpan nextTimeSpanValue = this.GetNextTimeSpanValue(false);
					TimeZoneInfo.TransitionTime nextTransitionTimeValue = this.GetNextTransitionTimeValue(false);
					TimeZoneInfo.TransitionTime nextTransitionTimeValue2 = this.GetNextTransitionTimeValue(false);
					TimeSpan baseUtcOffsetDelta = TimeSpan.Zero;
					if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					if ((this.m_serializedText[this.m_currentTokenStartIndex] >= '0' && this.m_serializedText[this.m_currentTokenStartIndex] <= '9') || this.m_serializedText[this.m_currentTokenStartIndex] == '-' || this.m_serializedText[this.m_currentTokenStartIndex] == '+')
					{
						baseUtcOffsetDelta = this.GetNextTimeSpanValue(false);
					}
					if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					if (this.m_serializedText[this.m_currentTokenStartIndex] != ']')
					{
						this.SkipVersionNextDataFields(1);
					}
					else
					{
						this.m_currentTokenStartIndex++;
					}
					TimeZoneInfo.AdjustmentRule result;
					try
					{
						result = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(nextDateTimeValue, nextDateTimeValue2, nextTimeSpanValue, nextTransitionTimeValue, nextTransitionTimeValue2, baseUtcOffsetDelta);
					}
					catch (ArgumentException innerException)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), innerException);
					}
					if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
					}
					else
					{
						this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
					}
					return result;
				}
			}

			// Token: 0x06006963 RID: 26979 RVA: 0x0016A988 File Offset: 0x00168B88
			private TimeZoneInfo.TransitionTime GetNextTransitionTimeValue(bool canEndWithoutSeparator)
			{
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || (this.m_currentTokenStartIndex < this.m_serializedText.Length && this.m_serializedText[this.m_currentTokenStartIndex] == ']'))
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				if (this.m_serializedText[this.m_currentTokenStartIndex] != '[')
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				this.m_currentTokenStartIndex++;
				int nextInt32Value = this.GetNextInt32Value(false);
				if (nextInt32Value != 0 && nextInt32Value != 1)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				DateTime nextDateTimeValue = this.GetNextDateTimeValue(false, "HH:mm:ss.FFF");
				nextDateTimeValue = new DateTime(1, 1, 1, nextDateTimeValue.Hour, nextDateTimeValue.Minute, nextDateTimeValue.Second, nextDateTimeValue.Millisecond);
				int nextInt32Value2 = this.GetNextInt32Value(false);
				TimeZoneInfo.TransitionTime result;
				if (nextInt32Value == 1)
				{
					int nextInt32Value3 = this.GetNextInt32Value(false);
					try
					{
						result = TimeZoneInfo.TransitionTime.CreateFixedDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value3);
						goto IL_15B;
					}
					catch (ArgumentException innerException)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), innerException);
					}
				}
				int nextInt32Value4 = this.GetNextInt32Value(false);
				int nextInt32Value5 = this.GetNextInt32Value(false);
				try
				{
					result = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value4, (DayOfWeek)nextInt32Value5);
				}
				catch (ArgumentException innerException2)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), innerException2);
				}
				IL_15B:
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				if (this.m_serializedText[this.m_currentTokenStartIndex] != ']')
				{
					this.SkipVersionNextDataFields(1);
				}
				else
				{
					this.m_currentTokenStartIndex++;
				}
				bool flag = false;
				if (this.m_currentTokenStartIndex < this.m_serializedText.Length && this.m_serializedText[this.m_currentTokenStartIndex] == ';')
				{
					this.m_currentTokenStartIndex++;
					flag = true;
				}
				if (!flag && !canEndWithoutSeparator)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
				}
				else
				{
					this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
				}
				return result;
			}

			// Token: 0x04003146 RID: 12614
			private string m_serializedText;

			// Token: 0x04003147 RID: 12615
			private int m_currentTokenStartIndex;

			// Token: 0x04003148 RID: 12616
			private TimeZoneInfo.StringSerializer.State m_state;

			// Token: 0x04003149 RID: 12617
			private const int initialCapacityForString = 64;

			// Token: 0x0400314A RID: 12618
			private const char esc = '\\';

			// Token: 0x0400314B RID: 12619
			private const char sep = ';';

			// Token: 0x0400314C RID: 12620
			private const char lhs = '[';

			// Token: 0x0400314D RID: 12621
			private const char rhs = ']';

			// Token: 0x0400314E RID: 12622
			private const string escString = "\\";

			// Token: 0x0400314F RID: 12623
			private const string sepString = ";";

			// Token: 0x04003150 RID: 12624
			private const string lhsString = "[";

			// Token: 0x04003151 RID: 12625
			private const string rhsString = "]";

			// Token: 0x04003152 RID: 12626
			private const string escapedEsc = "\\\\";

			// Token: 0x04003153 RID: 12627
			private const string escapedSep = "\\;";

			// Token: 0x04003154 RID: 12628
			private const string escapedLhs = "\\[";

			// Token: 0x04003155 RID: 12629
			private const string escapedRhs = "\\]";

			// Token: 0x04003156 RID: 12630
			private const string dateTimeFormat = "MM:dd:yyyy";

			// Token: 0x04003157 RID: 12631
			private const string timeOfDayFormat = "HH:mm:ss.FFF";

			// Token: 0x02000CC7 RID: 3271
			private enum State
			{
				// Token: 0x04003840 RID: 14400
				Escaped,
				// Token: 0x04003841 RID: 14401
				NotEscaped,
				// Token: 0x04003842 RID: 14402
				StartOfToken,
				// Token: 0x04003843 RID: 14403
				EndOfLine
			}
		}

		// Token: 0x02000AD5 RID: 2773
		private class TimeZoneInfoComparer : IComparer<TimeZoneInfo>
		{
			// Token: 0x06006964 RID: 26980 RVA: 0x0016ABDC File Offset: 0x00168DDC
			int IComparer<TimeZoneInfo>.Compare(TimeZoneInfo x, TimeZoneInfo y)
			{
				int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
				if (num != 0)
				{
					return num;
				}
				return string.Compare(x.DisplayName, y.DisplayName, StringComparison.Ordinal);
			}
		}
	}
}
