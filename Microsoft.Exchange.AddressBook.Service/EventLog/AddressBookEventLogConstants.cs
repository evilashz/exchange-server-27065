using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AddressBook.EventLog
{
	// Token: 0x0200004B RID: 75
	internal static class AddressBookEventLogConstants
	{
		// Token: 0x040001C9 RID: 457
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AddressBookServiceStartSuccess = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001CA RID: 458
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AddressBookServiceStopSuccess = new ExEventLog.EventTuple(263145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001CB RID: 459
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SpnRegisterFailure = new ExEventLog.EventTuple(3221488618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001CC RID: 460
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorRemovingPrivilegesOnStart = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001CD RID: 461
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnexpectedExceptionOnStart = new ExEventLog.EventTuple(3221488620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001CE RID: 462
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnexpectedExceptionOnStop = new ExEventLog.EventTuple(3221488621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001CF RID: 463
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoEndpointsConfigured = new ExEventLog.EventTuple(3221488623U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001D0 RID: 464
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcRegisterInterfaceFailure = new ExEventLog.EventTuple(3221488624U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001D1 RID: 465
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BadConfigParameter = new ExEventLog.EventTuple(3221488625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001D2 RID: 466
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToFindLocalServerInAD = new ExEventLog.EventTuple(3221488626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001D3 RID: 467
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExchangeRpcClientAccessDisabled = new ExEventLog.EventTuple(2147746803U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001D4 RID: 468
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_StartingMSExchangeMapiAddressBookAppPool = new ExEventLog.EventTuple(264144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040001D5 RID: 469
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MSExchangeMapiAddressBookAppPoolStartSuccess = new ExEventLog.EventTuple(264145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001D6 RID: 470
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_StoppingMSExchangeMapiAddressBookAppPool = new ExEventLog.EventTuple(264146U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040001D7 RID: 471
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MSExchangeMapiAddressBookAppPoolStopSuccess = new ExEventLog.EventTuple(264147U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001D8 RID: 472
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_MapiAddressBookRemovingPrivilegeErrorOnStart = new ExEventLog.EventTuple(3221489620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0200004C RID: 76
		private enum Category : short
		{
			// Token: 0x040001DA RID: 474
			General = 1
		}

		// Token: 0x0200004D RID: 77
		internal enum Message : uint
		{
			// Token: 0x040001DC RID: 476
			AddressBookServiceStartSuccess = 263144U,
			// Token: 0x040001DD RID: 477
			AddressBookServiceStopSuccess,
			// Token: 0x040001DE RID: 478
			SpnRegisterFailure = 3221488618U,
			// Token: 0x040001DF RID: 479
			ErrorRemovingPrivilegesOnStart,
			// Token: 0x040001E0 RID: 480
			UnexpectedExceptionOnStart,
			// Token: 0x040001E1 RID: 481
			UnexpectedExceptionOnStop,
			// Token: 0x040001E2 RID: 482
			NoEndpointsConfigured = 3221488623U,
			// Token: 0x040001E3 RID: 483
			RpcRegisterInterfaceFailure,
			// Token: 0x040001E4 RID: 484
			BadConfigParameter,
			// Token: 0x040001E5 RID: 485
			FailedToFindLocalServerInAD,
			// Token: 0x040001E6 RID: 486
			ExchangeRpcClientAccessDisabled = 2147746803U,
			// Token: 0x040001E7 RID: 487
			StartingMSExchangeMapiAddressBookAppPool = 264144U,
			// Token: 0x040001E8 RID: 488
			MSExchangeMapiAddressBookAppPoolStartSuccess,
			// Token: 0x040001E9 RID: 489
			StoppingMSExchangeMapiAddressBookAppPool,
			// Token: 0x040001EA RID: 490
			MSExchangeMapiAddressBookAppPoolStopSuccess,
			// Token: 0x040001EB RID: 491
			MapiAddressBookRemovingPrivilegeErrorOnStart = 3221489620U
		}
	}
}
