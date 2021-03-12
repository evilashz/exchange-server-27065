using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x020000A0 RID: 160
	internal static class CalendarStrings
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x00025488 File Offset: 0x00023688
		static CalendarStrings()
		{
			CalendarStrings.stringIDs.Add(1651570042U, "PropertyOutsideOfComponent");
			CalendarStrings.stringIDs.Add(2204331647U, "EmptyParameterName");
			CalendarStrings.stringIDs.Add(1363187990U, "ByMonthDayOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(3611840403U, "ByMonthDayOutOfRange");
			CalendarStrings.stringIDs.Add(1799497654U, "ByYearDayOutOfRange");
			CalendarStrings.stringIDs.Add(1321868363U, "InvalidReaderState");
			CalendarStrings.stringIDs.Add(2313244969U, "ByMinuteOutOfRange");
			CalendarStrings.stringIDs.Add(3235259578U, "NotAllComponentsClosed");
			CalendarStrings.stringIDs.Add(1503765159U, "MultivalueNotPermittedOnWkSt");
			CalendarStrings.stringIDs.Add(1299235546U, "BySecondOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(1730444109U, "ExpectedS");
			CalendarStrings.stringIDs.Add(1379077484U, "StreamHasAlreadyBeenClosed");
			CalendarStrings.stringIDs.Add(4275464087U, "UnknownRecurrenceProperty");
			CalendarStrings.stringIDs.Add(3837794365U, "ByYearDayOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(3721804466U, "ByMonthOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(1219670026U, "ParameterNameMissing");
			CalendarStrings.stringIDs.Add(4016499044U, "InvalidValueTypeForProperty");
			CalendarStrings.stringIDs.Add(3742088828U, "InvalidDateTimeLength");
			CalendarStrings.stringIDs.Add(3387824169U, "UnknownDayOfWeek");
			CalendarStrings.stringIDs.Add(3544698171U, "CountLessThanZero");
			CalendarStrings.stringIDs.Add(1730444116U, "ExpectedZ");
			CalendarStrings.stringIDs.Add(861244994U, "ExpectedWOrD");
			CalendarStrings.stringIDs.Add(3395058257U, "ParametersNotPermittedOnComponentTag");
			CalendarStrings.stringIDs.Add(226115752U, "DurationDataNotEndedProperly");
			CalendarStrings.stringIDs.Add(1672858014U, "ByDayOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(1478317853U, "PropertyTruncated");
			CalendarStrings.stringIDs.Add(1956952952U, "IntervalMustBePositive");
			CalendarStrings.stringIDs.Add(3830022583U, "MultivalueNotPermittedOnCount");
			CalendarStrings.stringIDs.Add(3872783322U, "ExpectedPlusMinus");
			CalendarStrings.stringIDs.Add(3537482179U, "InvalidParameterValue");
			CalendarStrings.stringIDs.Add(250418233U, "EmptyPropertyName");
			CalendarStrings.stringIDs.Add(2555380673U, "InvalidParameterId");
			CalendarStrings.stringIDs.Add(1730444106U, "ExpectedT");
			CalendarStrings.stringIDs.Add(1565539446U, "ByWeekNoOutOfRange");
			CalendarStrings.stringIDs.Add(472912471U, "BySecondOutOfRange");
			CalendarStrings.stringIDs.Add(1730444131U, "ExpectedM");
			CalendarStrings.stringIDs.Add(2118029539U, "UnknownFrequencyValue");
			CalendarStrings.stringIDs.Add(4188603411U, "InvalidTimeFormat");
			CalendarStrings.stringIDs.Add(2880341830U, "InvalidCharacterInQuotedString");
			CalendarStrings.stringIDs.Add(3073868642U, "CountNotPermittedWithUntil");
			CalendarStrings.stringIDs.Add(716036182U, "InvalidState");
			CalendarStrings.stringIDs.Add(3099580008U, "StreamMustAllowRead");
			CalendarStrings.stringIDs.Add(3740878728U, "StreamIsReadOnly");
			CalendarStrings.stringIDs.Add(3606802482U, "InvalidToken");
			CalendarStrings.stringIDs.Add(1140546334U, "InvalidDateFormat");
			CalendarStrings.stringIDs.Add(680249202U, "InvalidStateForOperation");
			CalendarStrings.stringIDs.Add(2830151143U, "InvalidCharacterInRecurrence");
			CalendarStrings.stringIDs.Add(2466566499U, "InvalidCharacterInPropertyName");
			CalendarStrings.stringIDs.Add(1110024291U, "ParameterValuesCannotContainDoubleQuote");
			CalendarStrings.stringIDs.Add(27421126U, "ByMinuteOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(2905506355U, "InvalidCharacterInParameterText");
			CalendarStrings.stringIDs.Add(1730444110U, "ExpectedP");
			CalendarStrings.stringIDs.Add(1177599875U, "CannotStartOnProperty");
			CalendarStrings.stringIDs.Add(738606535U, "StreamMustAllowWrite");
			CalendarStrings.stringIDs.Add(797792475U, "UnexpectedEndOfStream");
			CalendarStrings.stringIDs.Add(3550209514U, "WkStOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(318352933U, "InvalidValueFormat");
			CalendarStrings.stringIDs.Add(1631030892U, "InvalidCharacter");
			CalendarStrings.stringIDs.Add(608577876U, "CannotStartPropertyInParameter");
			CalendarStrings.stringIDs.Add(861974305U, "InvalidCharacterInParameterName");
			CalendarStrings.stringIDs.Add(1430833556U, "EndTagWithoutBegin");
			CalendarStrings.stringIDs.Add(1184842547U, "InvalidStateToWriteValue");
			CalendarStrings.stringIDs.Add(3689402045U, "InvalidTimeStringLength");
			CalendarStrings.stringIDs.Add(3128171671U, "InvalidComponentId");
			CalendarStrings.stringIDs.Add(3822021355U, "ByHourOutOfRange");
			CalendarStrings.stringIDs.Add(3394177979U, "ByMonthOutOfRange");
			CalendarStrings.stringIDs.Add(1045189704U, "ExpectedTAfterDate");
			CalendarStrings.stringIDs.Add(3955879836U, "UntilNotPermittedWithCount");
			CalendarStrings.stringIDs.Add(3018409416U, "UtcOffsetTimespanCannotContainDays");
			CalendarStrings.stringIDs.Add(3513540288U, "MultivalueNotPermittedOnFreq");
			CalendarStrings.stringIDs.Add(2311921382U, "ExpectedHMS");
			CalendarStrings.stringIDs.Add(1558925939U, "InvalidPropertyId");
			CalendarStrings.stringIDs.Add(1793182840U, "CountOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(1766527382U, "MultivalueNotPermittedOnUntil");
			CalendarStrings.stringIDs.Add(3223577931U, "ValueAlreadyRead");
			CalendarStrings.stringIDs.Add(2347585579U, "MultivalueNotPermittedOnInterval");
			CalendarStrings.stringIDs.Add(401491057U, "BySetPosOutOfRange");
			CalendarStrings.stringIDs.Add(2998051666U, "InvalidUtcOffsetLength");
			CalendarStrings.stringIDs.Add(411844418U, "ComponentNameMismatch");
			CalendarStrings.stringIDs.Add(3608438261U, "NonwritableStream");
			CalendarStrings.stringIDs.Add(3273146598U, "IntervalOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(522329279U, "InvalidCharacterInPropertyValue");
			CalendarStrings.stringIDs.Add(2128475288U, "ByHourOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(1746426911U, "UntilOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(3590683541U, "OffsetOutOfRange");
			CalendarStrings.stringIDs.Add(3651971229U, "EmptyComponentName");
			CalendarStrings.stringIDs.Add(2363588404U, "BySetPosOnlyPermittedOnce");
			CalendarStrings.stringIDs.Add(3089920790U, "StateMustBeComponent");
			CalendarStrings.stringIDs.Add(4182777507U, "ByWeekNoOnlyPermittedOnce");
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00025BB8 File Offset: 0x00023DB8
		public static string PropertyOutsideOfComponent
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("PropertyOutsideOfComponent");
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00025BC9 File Offset: 0x00023DC9
		public static string EmptyParameterName
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("EmptyParameterName");
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00025BDA File Offset: 0x00023DDA
		public static string ByMonthDayOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByMonthDayOnlyPermittedOnce");
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00025BEB File Offset: 0x00023DEB
		public static string ByMonthDayOutOfRange
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByMonthDayOutOfRange");
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00025BFC File Offset: 0x00023DFC
		public static string ByYearDayOutOfRange
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByYearDayOutOfRange");
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00025C0D File Offset: 0x00023E0D
		public static string InvalidReaderState
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidReaderState");
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00025C1E File Offset: 0x00023E1E
		public static string ByMinuteOutOfRange
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByMinuteOutOfRange");
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00025C2F File Offset: 0x00023E2F
		public static string NotAllComponentsClosed
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("NotAllComponentsClosed");
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00025C40 File Offset: 0x00023E40
		public static string MultivalueNotPermittedOnWkSt
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("MultivalueNotPermittedOnWkSt");
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00025C51 File Offset: 0x00023E51
		public static string BySecondOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("BySecondOnlyPermittedOnce");
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00025C62 File Offset: 0x00023E62
		public static string ExpectedS
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ExpectedS");
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00025C73 File Offset: 0x00023E73
		public static string StreamHasAlreadyBeenClosed
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("StreamHasAlreadyBeenClosed");
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00025C84 File Offset: 0x00023E84
		public static string UnknownRecurrenceProperty
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("UnknownRecurrenceProperty");
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00025C95 File Offset: 0x00023E95
		public static string ByYearDayOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByYearDayOnlyPermittedOnce");
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00025CA6 File Offset: 0x00023EA6
		public static string ByMonthOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByMonthOnlyPermittedOnce");
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00025CB7 File Offset: 0x00023EB7
		public static string ParameterNameMissing
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ParameterNameMissing");
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00025CC8 File Offset: 0x00023EC8
		public static string InvalidValueTypeForProperty
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidValueTypeForProperty");
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00025CD9 File Offset: 0x00023ED9
		public static string InvalidDateTimeLength
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidDateTimeLength");
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00025CEA File Offset: 0x00023EEA
		public static string UnknownDayOfWeek
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("UnknownDayOfWeek");
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00025CFB File Offset: 0x00023EFB
		public static string CountLessThanZero
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("CountLessThanZero");
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00025D0C File Offset: 0x00023F0C
		public static string ExpectedZ
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ExpectedZ");
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00025D1D File Offset: 0x00023F1D
		public static string ExpectedWOrD
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ExpectedWOrD");
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00025D2E File Offset: 0x00023F2E
		public static string ParametersNotPermittedOnComponentTag
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ParametersNotPermittedOnComponentTag");
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00025D3F File Offset: 0x00023F3F
		public static string DurationDataNotEndedProperly
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("DurationDataNotEndedProperly");
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00025D50 File Offset: 0x00023F50
		public static string ByDayOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByDayOnlyPermittedOnce");
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00025D61 File Offset: 0x00023F61
		public static string PropertyTruncated
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("PropertyTruncated");
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00025D72 File Offset: 0x00023F72
		public static string IntervalMustBePositive
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("IntervalMustBePositive");
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00025D83 File Offset: 0x00023F83
		public static string MultivalueNotPermittedOnCount
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("MultivalueNotPermittedOnCount");
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00025D94 File Offset: 0x00023F94
		public static string ExpectedPlusMinus
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ExpectedPlusMinus");
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00025DA5 File Offset: 0x00023FA5
		public static string InvalidParameterValue
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidParameterValue");
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00025DB6 File Offset: 0x00023FB6
		public static string EmptyPropertyName
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("EmptyPropertyName");
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00025DC7 File Offset: 0x00023FC7
		public static string InvalidParameterId
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidParameterId");
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00025DD8 File Offset: 0x00023FD8
		public static string ExpectedT
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ExpectedT");
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00025DE9 File Offset: 0x00023FE9
		public static string ByWeekNoOutOfRange
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByWeekNoOutOfRange");
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00025DFA File Offset: 0x00023FFA
		public static string BySecondOutOfRange
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("BySecondOutOfRange");
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00025E0B File Offset: 0x0002400B
		public static string ExpectedM
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ExpectedM");
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00025E1C File Offset: 0x0002401C
		public static string UnknownFrequencyValue
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("UnknownFrequencyValue");
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00025E2D File Offset: 0x0002402D
		public static string InvalidTimeFormat
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidTimeFormat");
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00025E3E File Offset: 0x0002403E
		public static string InvalidCharacterInQuotedString
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidCharacterInQuotedString");
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00025E4F File Offset: 0x0002404F
		public static string CountNotPermittedWithUntil
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("CountNotPermittedWithUntil");
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00025E60 File Offset: 0x00024060
		public static string InvalidState
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidState");
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00025E71 File Offset: 0x00024071
		public static string StreamMustAllowRead
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("StreamMustAllowRead");
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00025E82 File Offset: 0x00024082
		public static string StreamIsReadOnly
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("StreamIsReadOnly");
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00025E93 File Offset: 0x00024093
		public static string InvalidToken
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidToken");
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00025EA4 File Offset: 0x000240A4
		public static string InvalidDateFormat
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidDateFormat");
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00025EB5 File Offset: 0x000240B5
		public static string InvalidStateForOperation
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidStateForOperation");
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00025EC6 File Offset: 0x000240C6
		public static string InvalidCharacterInRecurrence
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidCharacterInRecurrence");
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00025ED7 File Offset: 0x000240D7
		public static string InvalidCharacterInPropertyName
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidCharacterInPropertyName");
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00025EE8 File Offset: 0x000240E8
		public static string ParameterValuesCannotContainDoubleQuote
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ParameterValuesCannotContainDoubleQuote");
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00025EF9 File Offset: 0x000240F9
		public static string ByMinuteOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByMinuteOnlyPermittedOnce");
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00025F0A File Offset: 0x0002410A
		public static string InvalidCharacterInParameterText
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidCharacterInParameterText");
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00025F1B File Offset: 0x0002411B
		public static string ExpectedP
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ExpectedP");
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00025F2C File Offset: 0x0002412C
		public static string CannotStartOnProperty
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("CannotStartOnProperty");
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00025F3D File Offset: 0x0002413D
		public static string StreamMustAllowWrite
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("StreamMustAllowWrite");
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00025F4E File Offset: 0x0002414E
		public static string UnexpectedEndOfStream
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("UnexpectedEndOfStream");
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00025F5F File Offset: 0x0002415F
		public static string WkStOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("WkStOnlyPermittedOnce");
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00025F70 File Offset: 0x00024170
		public static string InvalidValueFormat
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidValueFormat");
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00025F81 File Offset: 0x00024181
		public static string InvalidCharacter
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidCharacter");
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00025F92 File Offset: 0x00024192
		public static string CannotStartPropertyInParameter
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("CannotStartPropertyInParameter");
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00025FA3 File Offset: 0x000241A3
		public static string InvalidCharacterInParameterName
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidCharacterInParameterName");
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00025FB4 File Offset: 0x000241B4
		public static string EndTagWithoutBegin
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("EndTagWithoutBegin");
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00025FC5 File Offset: 0x000241C5
		public static string InvalidStateToWriteValue
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidStateToWriteValue");
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00025FD6 File Offset: 0x000241D6
		public static string InvalidTimeStringLength
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidTimeStringLength");
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00025FE7 File Offset: 0x000241E7
		public static string InvalidComponentId
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidComponentId");
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00025FF8 File Offset: 0x000241F8
		public static string ByHourOutOfRange
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByHourOutOfRange");
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00026009 File Offset: 0x00024209
		public static string ByMonthOutOfRange
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByMonthOutOfRange");
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0002601A File Offset: 0x0002421A
		public static string ExpectedTAfterDate
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ExpectedTAfterDate");
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0002602B File Offset: 0x0002422B
		public static string UntilNotPermittedWithCount
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("UntilNotPermittedWithCount");
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0002603C File Offset: 0x0002423C
		public static string UtcOffsetTimespanCannotContainDays
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("UtcOffsetTimespanCannotContainDays");
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0002604D File Offset: 0x0002424D
		public static string MultivalueNotPermittedOnFreq
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("MultivalueNotPermittedOnFreq");
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0002605E File Offset: 0x0002425E
		public static string ExpectedHMS
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ExpectedHMS");
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0002606F File Offset: 0x0002426F
		public static string InvalidPropertyId
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidPropertyId");
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00026080 File Offset: 0x00024280
		public static string CountOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("CountOnlyPermittedOnce");
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00026091 File Offset: 0x00024291
		public static string MultivalueNotPermittedOnUntil
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("MultivalueNotPermittedOnUntil");
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x000260A2 File Offset: 0x000242A2
		public static string ValueAlreadyRead
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ValueAlreadyRead");
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x000260B3 File Offset: 0x000242B3
		public static string MultivalueNotPermittedOnInterval
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("MultivalueNotPermittedOnInterval");
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x000260C4 File Offset: 0x000242C4
		public static string BySetPosOutOfRange
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("BySetPosOutOfRange");
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x000260D5 File Offset: 0x000242D5
		public static string InvalidUtcOffsetLength
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidUtcOffsetLength");
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x000260E6 File Offset: 0x000242E6
		public static string ComponentNameMismatch
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ComponentNameMismatch");
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x000260F7 File Offset: 0x000242F7
		public static string NonwritableStream
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("NonwritableStream");
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00026108 File Offset: 0x00024308
		public static string IntervalOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("IntervalOnlyPermittedOnce");
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00026119 File Offset: 0x00024319
		public static string InvalidCharacterInPropertyValue
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("InvalidCharacterInPropertyValue");
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0002612A File Offset: 0x0002432A
		public static string ByHourOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByHourOnlyPermittedOnce");
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0002613B File Offset: 0x0002433B
		public static string UntilOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("UntilOnlyPermittedOnce");
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0002614C File Offset: 0x0002434C
		public static string OffsetOutOfRange
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("OffsetOutOfRange");
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0002615D File Offset: 0x0002435D
		public static string EmptyComponentName
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("EmptyComponentName");
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0002616E File Offset: 0x0002436E
		public static string BySetPosOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("BySetPosOnlyPermittedOnce");
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0002617F File Offset: 0x0002437F
		public static string StateMustBeComponent
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("StateMustBeComponent");
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00026190 File Offset: 0x00024390
		public static string ByWeekNoOnlyPermittedOnce
		{
			get
			{
				return CalendarStrings.ResourceManager.GetString("ByWeekNoOnlyPermittedOnce");
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000261A1 File Offset: 0x000243A1
		public static string GetLocalizedString(CalendarStrings.IDs key)
		{
			return CalendarStrings.ResourceManager.GetString(CalendarStrings.stringIDs[(uint)key]);
		}

		// Token: 0x040004E2 RID: 1250
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(89);

		// Token: 0x040004E3 RID: 1251
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.CalendarStrings", typeof(CalendarStrings).GetTypeInfo().Assembly);

		// Token: 0x020000A1 RID: 161
		public enum IDs : uint
		{
			// Token: 0x040004E5 RID: 1253
			PropertyOutsideOfComponent = 1651570042U,
			// Token: 0x040004E6 RID: 1254
			EmptyParameterName = 2204331647U,
			// Token: 0x040004E7 RID: 1255
			ByMonthDayOnlyPermittedOnce = 1363187990U,
			// Token: 0x040004E8 RID: 1256
			ByMonthDayOutOfRange = 3611840403U,
			// Token: 0x040004E9 RID: 1257
			ByYearDayOutOfRange = 1799497654U,
			// Token: 0x040004EA RID: 1258
			InvalidReaderState = 1321868363U,
			// Token: 0x040004EB RID: 1259
			ByMinuteOutOfRange = 2313244969U,
			// Token: 0x040004EC RID: 1260
			NotAllComponentsClosed = 3235259578U,
			// Token: 0x040004ED RID: 1261
			MultivalueNotPermittedOnWkSt = 1503765159U,
			// Token: 0x040004EE RID: 1262
			BySecondOnlyPermittedOnce = 1299235546U,
			// Token: 0x040004EF RID: 1263
			ExpectedS = 1730444109U,
			// Token: 0x040004F0 RID: 1264
			StreamHasAlreadyBeenClosed = 1379077484U,
			// Token: 0x040004F1 RID: 1265
			UnknownRecurrenceProperty = 4275464087U,
			// Token: 0x040004F2 RID: 1266
			ByYearDayOnlyPermittedOnce = 3837794365U,
			// Token: 0x040004F3 RID: 1267
			ByMonthOnlyPermittedOnce = 3721804466U,
			// Token: 0x040004F4 RID: 1268
			ParameterNameMissing = 1219670026U,
			// Token: 0x040004F5 RID: 1269
			InvalidValueTypeForProperty = 4016499044U,
			// Token: 0x040004F6 RID: 1270
			InvalidDateTimeLength = 3742088828U,
			// Token: 0x040004F7 RID: 1271
			UnknownDayOfWeek = 3387824169U,
			// Token: 0x040004F8 RID: 1272
			CountLessThanZero = 3544698171U,
			// Token: 0x040004F9 RID: 1273
			ExpectedZ = 1730444116U,
			// Token: 0x040004FA RID: 1274
			ExpectedWOrD = 861244994U,
			// Token: 0x040004FB RID: 1275
			ParametersNotPermittedOnComponentTag = 3395058257U,
			// Token: 0x040004FC RID: 1276
			DurationDataNotEndedProperly = 226115752U,
			// Token: 0x040004FD RID: 1277
			ByDayOnlyPermittedOnce = 1672858014U,
			// Token: 0x040004FE RID: 1278
			PropertyTruncated = 1478317853U,
			// Token: 0x040004FF RID: 1279
			IntervalMustBePositive = 1956952952U,
			// Token: 0x04000500 RID: 1280
			MultivalueNotPermittedOnCount = 3830022583U,
			// Token: 0x04000501 RID: 1281
			ExpectedPlusMinus = 3872783322U,
			// Token: 0x04000502 RID: 1282
			InvalidParameterValue = 3537482179U,
			// Token: 0x04000503 RID: 1283
			EmptyPropertyName = 250418233U,
			// Token: 0x04000504 RID: 1284
			InvalidParameterId = 2555380673U,
			// Token: 0x04000505 RID: 1285
			ExpectedT = 1730444106U,
			// Token: 0x04000506 RID: 1286
			ByWeekNoOutOfRange = 1565539446U,
			// Token: 0x04000507 RID: 1287
			BySecondOutOfRange = 472912471U,
			// Token: 0x04000508 RID: 1288
			ExpectedM = 1730444131U,
			// Token: 0x04000509 RID: 1289
			UnknownFrequencyValue = 2118029539U,
			// Token: 0x0400050A RID: 1290
			InvalidTimeFormat = 4188603411U,
			// Token: 0x0400050B RID: 1291
			InvalidCharacterInQuotedString = 2880341830U,
			// Token: 0x0400050C RID: 1292
			CountNotPermittedWithUntil = 3073868642U,
			// Token: 0x0400050D RID: 1293
			InvalidState = 716036182U,
			// Token: 0x0400050E RID: 1294
			StreamMustAllowRead = 3099580008U,
			// Token: 0x0400050F RID: 1295
			StreamIsReadOnly = 3740878728U,
			// Token: 0x04000510 RID: 1296
			InvalidToken = 3606802482U,
			// Token: 0x04000511 RID: 1297
			InvalidDateFormat = 1140546334U,
			// Token: 0x04000512 RID: 1298
			InvalidStateForOperation = 680249202U,
			// Token: 0x04000513 RID: 1299
			InvalidCharacterInRecurrence = 2830151143U,
			// Token: 0x04000514 RID: 1300
			InvalidCharacterInPropertyName = 2466566499U,
			// Token: 0x04000515 RID: 1301
			ParameterValuesCannotContainDoubleQuote = 1110024291U,
			// Token: 0x04000516 RID: 1302
			ByMinuteOnlyPermittedOnce = 27421126U,
			// Token: 0x04000517 RID: 1303
			InvalidCharacterInParameterText = 2905506355U,
			// Token: 0x04000518 RID: 1304
			ExpectedP = 1730444110U,
			// Token: 0x04000519 RID: 1305
			CannotStartOnProperty = 1177599875U,
			// Token: 0x0400051A RID: 1306
			StreamMustAllowWrite = 738606535U,
			// Token: 0x0400051B RID: 1307
			UnexpectedEndOfStream = 797792475U,
			// Token: 0x0400051C RID: 1308
			WkStOnlyPermittedOnce = 3550209514U,
			// Token: 0x0400051D RID: 1309
			InvalidValueFormat = 318352933U,
			// Token: 0x0400051E RID: 1310
			InvalidCharacter = 1631030892U,
			// Token: 0x0400051F RID: 1311
			CannotStartPropertyInParameter = 608577876U,
			// Token: 0x04000520 RID: 1312
			InvalidCharacterInParameterName = 861974305U,
			// Token: 0x04000521 RID: 1313
			EndTagWithoutBegin = 1430833556U,
			// Token: 0x04000522 RID: 1314
			InvalidStateToWriteValue = 1184842547U,
			// Token: 0x04000523 RID: 1315
			InvalidTimeStringLength = 3689402045U,
			// Token: 0x04000524 RID: 1316
			InvalidComponentId = 3128171671U,
			// Token: 0x04000525 RID: 1317
			ByHourOutOfRange = 3822021355U,
			// Token: 0x04000526 RID: 1318
			ByMonthOutOfRange = 3394177979U,
			// Token: 0x04000527 RID: 1319
			ExpectedTAfterDate = 1045189704U,
			// Token: 0x04000528 RID: 1320
			UntilNotPermittedWithCount = 3955879836U,
			// Token: 0x04000529 RID: 1321
			UtcOffsetTimespanCannotContainDays = 3018409416U,
			// Token: 0x0400052A RID: 1322
			MultivalueNotPermittedOnFreq = 3513540288U,
			// Token: 0x0400052B RID: 1323
			ExpectedHMS = 2311921382U,
			// Token: 0x0400052C RID: 1324
			InvalidPropertyId = 1558925939U,
			// Token: 0x0400052D RID: 1325
			CountOnlyPermittedOnce = 1793182840U,
			// Token: 0x0400052E RID: 1326
			MultivalueNotPermittedOnUntil = 1766527382U,
			// Token: 0x0400052F RID: 1327
			ValueAlreadyRead = 3223577931U,
			// Token: 0x04000530 RID: 1328
			MultivalueNotPermittedOnInterval = 2347585579U,
			// Token: 0x04000531 RID: 1329
			BySetPosOutOfRange = 401491057U,
			// Token: 0x04000532 RID: 1330
			InvalidUtcOffsetLength = 2998051666U,
			// Token: 0x04000533 RID: 1331
			ComponentNameMismatch = 411844418U,
			// Token: 0x04000534 RID: 1332
			NonwritableStream = 3608438261U,
			// Token: 0x04000535 RID: 1333
			IntervalOnlyPermittedOnce = 3273146598U,
			// Token: 0x04000536 RID: 1334
			InvalidCharacterInPropertyValue = 522329279U,
			// Token: 0x04000537 RID: 1335
			ByHourOnlyPermittedOnce = 2128475288U,
			// Token: 0x04000538 RID: 1336
			UntilOnlyPermittedOnce = 1746426911U,
			// Token: 0x04000539 RID: 1337
			OffsetOutOfRange = 3590683541U,
			// Token: 0x0400053A RID: 1338
			EmptyComponentName = 3651971229U,
			// Token: 0x0400053B RID: 1339
			BySetPosOnlyPermittedOnce = 2363588404U,
			// Token: 0x0400053C RID: 1340
			StateMustBeComponent = 3089920790U,
			// Token: 0x0400053D RID: 1341
			ByWeekNoOnlyPermittedOnce = 4182777507U
		}
	}
}
