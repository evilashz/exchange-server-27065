using System;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200012E RID: 302
	public static class DisplayFormats
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0002B3AC File Offset: 0x000295AC
		public static ICustomTextConverter Default
		{
			get
			{
				return TextConverter.DefaultConverter;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0002B3B3 File Offset: 0x000295B3
		public static ICustomTextConverter EnhancedTimeSpanAsDays
		{
			get
			{
				return DisplayFormats.enhancedTimeSpanAsDays;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002B3BA File Offset: 0x000295BA
		public static ICustomTextConverter EnhancedTimeSpanAsHours
		{
			get
			{
				return DisplayFormats.enhancedTimeSpanAsHours;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0002B3C1 File Offset: 0x000295C1
		public static ICustomTextConverter EnhancedTimeSpanAsMinutes
		{
			get
			{
				return DisplayFormats.enhancedTimeSpanAsMinutes;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0002B3C8 File Offset: 0x000295C8
		public static ICustomTextConverter EnhancedTimeSpanAsSeconds
		{
			get
			{
				return DisplayFormats.enhancedTimeSpanAsSeconds;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0002B3CF File Offset: 0x000295CF
		public static ICustomTextConverter EnhancedTimeSpanAsDetailedFormat
		{
			get
			{
				return DisplayFormats.enhancedTimeSpanAsDetailedFormat;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0002B3D6 File Offset: 0x000295D6
		public static ICustomTextConverter ByteQuantifiedSizeAsKb
		{
			get
			{
				return DisplayFormats.byteQuantifiedSizeAsKb;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0002B3DD File Offset: 0x000295DD
		public static ICustomTextConverter ByteQuantifiedSizeAsMb
		{
			get
			{
				return DisplayFormats.byteQuantifiedSizeAsMb;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0002B3E4 File Offset: 0x000295E4
		public static ICustomTextConverter ByteQuantifiedSizeAsDetailedFormat
		{
			get
			{
				return DisplayFormats.byteQuantifiedSizeAsDetailedFormat;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0002B3EB File Offset: 0x000295EB
		public static ICustomTextConverter BooleanAsStatus
		{
			get
			{
				return DisplayFormats.booleanAsStatus;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002B3F2 File Offset: 0x000295F2
		public static ICustomTextConverter BooleanAsMountStatus
		{
			get
			{
				return DisplayFormats.booleanAsMountStatus;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0002B3F9 File Offset: 0x000295F9
		public static ICustomTextConverter BooleanAsYesNo
		{
			get
			{
				return DisplayFormats.booleanAsYesNo;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002B400 File Offset: 0x00029600
		public static ICustomTextConverter AdObjectIdAsName
		{
			get
			{
				return DisplayFormats.adObjectIdAsName;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002B407 File Offset: 0x00029607
		public static ICustomTextConverter NullableDateTimeAsLogTime
		{
			get
			{
				return DisplayFormats.nullableDateTimeAsLogTime;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0002B40E File Offset: 0x0002960E
		public static ICustomTextConverter IntegerAsPercentage
		{
			get
			{
				return DisplayFormats.integerAsPercentage;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0002B415 File Offset: 0x00029615
		public static ICustomTextConverter SmtpDomainWithSubdomainsListAsString
		{
			get
			{
				return DisplayFormats.smtpDomainWithSubdomainsListAsStringCoverter;
			}
		}

		// Token: 0x040004E3 RID: 1251
		private static readonly ICustomTextConverter enhancedTimeSpanAsDays = new EnhancedTimeSpanAsDaysCoverter();

		// Token: 0x040004E4 RID: 1252
		private static readonly ICustomTextConverter enhancedTimeSpanAsHours = new EnhancedTimeSpanAsHoursCoverter();

		// Token: 0x040004E5 RID: 1253
		private static readonly ICustomTextConverter enhancedTimeSpanAsMinutes = new EnhancedTimeSpanAsMinutesCoverter();

		// Token: 0x040004E6 RID: 1254
		private static readonly ICustomTextConverter enhancedTimeSpanAsSeconds = new EnhancedTimeSpanAsSecondsCoverter();

		// Token: 0x040004E7 RID: 1255
		private static readonly ICustomTextConverter enhancedTimeSpanAsDetailedFormat = new EnhancedTimeSpanAsDetailedFormatCoverter();

		// Token: 0x040004E8 RID: 1256
		private static readonly ICustomTextConverter byteQuantifiedSizeAsKb = new ByteQuantifiedSizeAsKbCoverter();

		// Token: 0x040004E9 RID: 1257
		private static readonly ICustomTextConverter byteQuantifiedSizeAsMb = new ByteQuantifiedSizeAsMbCoverter();

		// Token: 0x040004EA RID: 1258
		private static readonly ICustomTextConverter byteQuantifiedSizeAsDetailedFormat = new ByteQuantifiedSizeAsDetailedFormatCoverter();

		// Token: 0x040004EB RID: 1259
		private static readonly ICustomTextConverter booleanAsStatus = new BooleanAsStatusCoverter();

		// Token: 0x040004EC RID: 1260
		private static readonly ICustomTextConverter booleanAsMountStatus = new BooleanAsMountStatusCoverter();

		// Token: 0x040004ED RID: 1261
		private static readonly ICustomTextConverter booleanAsYesNo = new BooleanAsYesNoConverter();

		// Token: 0x040004EE RID: 1262
		private static readonly ICustomTextConverter adObjectIdAsName = new AdObjectIdAsNameCoverter();

		// Token: 0x040004EF RID: 1263
		private static readonly ICustomTextConverter nullableDateTimeAsLogTime = new NullableDateTimeAsLogTimeCoverter();

		// Token: 0x040004F0 RID: 1264
		private static readonly ICustomTextConverter integerAsPercentage = new IntegerAsPercentageConverter();

		// Token: 0x040004F1 RID: 1265
		private static readonly ICustomTextConverter smtpDomainWithSubdomainsListAsStringCoverter = new SmtpDomainWithSubdomainsListAsStringCoverter();
	}
}
