using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200002A RID: 42
	internal static class Configuration
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000BE60 File Offset: 0x0000A060
		static Configuration()
		{
			Configuration.parameterCollection = ConfigurationManager.AppSettings;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000BE8A File Offset: 0x0000A08A
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000BEAA File Offset: 0x0000A0AA
		internal static int DLExpansionLimit
		{
			get
			{
				if (Configuration.dlExpansionLimit == -1)
				{
					Configuration.dlExpansionLimit = Configuration.ReadIntValue("DLExpansionLimit", 10);
				}
				return Configuration.dlExpansionLimit;
			}
			set
			{
				Configuration.dlExpansionLimit = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000BED5 File Offset: 0x0000A0D5
		internal static int MaxNumberOfLocalMeetingsPerBatch
		{
			get
			{
				if (Configuration.maxNumberOfLocalMeetingsPerBatch == -1)
				{
					Configuration.maxNumberOfLocalMeetingsPerBatch = Configuration.ReadIntValue("MaxNumberOfLocalMeetingsPerBatch", 200);
				}
				return Configuration.maxNumberOfLocalMeetingsPerBatch;
			}
			set
			{
				Configuration.maxNumberOfLocalMeetingsPerBatch = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000BEDD File Offset: 0x0000A0DD
		internal static int WebRequestTimeoutInSeconds
		{
			get
			{
				if (Configuration.webRequestTimeoutInSeconds == -1)
				{
					Configuration.webRequestTimeoutInSeconds = Configuration.ReadIntValue("WebRequestTimeoutInSeconds", 25);
				}
				return Configuration.webRequestTimeoutInSeconds;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000BEFD File Offset: 0x0000A0FD
		internal static int MaxMeetingsToProcessIncludingDuplicates
		{
			get
			{
				if (Configuration.maxMeetingsToProcessIncludingDuplicates == -1)
				{
					Configuration.maxMeetingsToProcessIncludingDuplicates = Configuration.ReadIntValue("MaxMeetingsToProcessIncludingDuplicates", 1000);
				}
				return Configuration.maxMeetingsToProcessIncludingDuplicates;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000BF20 File Offset: 0x0000A120
		internal static int MaxMeetingsPerMailbox
		{
			get
			{
				if (Configuration.maxMeetingsPerMailbox == -1)
				{
					Configuration.maxMeetingsPerMailbox = Configuration.ReadIntValue("MaxMeetingsPerMailbox", 500);
				}
				return Configuration.maxMeetingsPerMailbox;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000BF43 File Offset: 0x0000A143
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000BF70 File Offset: 0x0000A170
		internal static bool IgnoreCertificateErrors
		{
			get
			{
				if (Configuration.ignoreCertificateErrors == null)
				{
					Configuration.ignoreCertificateErrors = new bool?(Configuration.ReadBooleanValue("IgnoreCertificateErrors", false));
				}
				return Configuration.ignoreCertificateErrors.Value;
			}
			set
			{
				Configuration.ignoreCertificateErrors = new bool?(value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000BF7D File Offset: 0x0000A17D
		internal static bool CalendarRepairOppositeMailboxEwsEnabled
		{
			get
			{
				if (Configuration.calendarRepairOppositeMailboxEwsEnabled == null)
				{
					Configuration.calendarRepairOppositeMailboxEwsEnabled = new bool?(Configuration.ReadBooleanValue("CalendarRepairOppositeMailboxEwsEnabled", true));
				}
				return Configuration.calendarRepairOppositeMailboxEwsEnabled.Value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000BFAA File Offset: 0x0000A1AA
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000BFD7 File Offset: 0x0000A1D7
		internal static bool CalendarRepairForceEwsUsage
		{
			get
			{
				if (Configuration.calendarRepairForceEwsUsage == null)
				{
					Configuration.calendarRepairForceEwsUsage = new bool?(Configuration.ReadBooleanValue("CalendarRepairForceEwsUsage", false));
				}
				return Configuration.calendarRepairForceEwsUsage.Value;
			}
			set
			{
				Configuration.calendarRepairForceEwsUsage = new bool?(value);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		private static int ReadIntValue(string name, int defaultValue)
		{
			int result;
			if (int.TryParse(Configuration.parameterCollection[name], out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000C008 File Offset: 0x0000A208
		private static bool ReadBooleanValue(string name, bool defaultValue)
		{
			bool result;
			if (bool.TryParse(Configuration.parameterCollection[name], out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x040000E5 RID: 229
		private const int DefaultDLExpansionLimit = 10;

		// Token: 0x040000E6 RID: 230
		private const bool DefaultIgnoreCertificateErrors = false;

		// Token: 0x040000E7 RID: 231
		private const bool DefaultCalendarRepairOppositeMailboxEwsEnabled = true;

		// Token: 0x040000E8 RID: 232
		private const bool DefaultCalendarRepairForceEwsUsage = false;

		// Token: 0x040000E9 RID: 233
		private const int DefaultMaxNumberOfLocalMeetingsPerBatch = 200;

		// Token: 0x040000EA RID: 234
		private const int DefaultWebRequestTimeoutInSeconds = 25;

		// Token: 0x040000EB RID: 235
		private const int DefaultMaxMeetingsPerMailbox = 500;

		// Token: 0x040000EC RID: 236
		private const int DefaultMaxMeetingsToProcessIncludingDuplicates = 1000;

		// Token: 0x040000ED RID: 237
		private static NameValueCollection parameterCollection;

		// Token: 0x040000EE RID: 238
		private static int maxNumberOfLocalMeetingsPerBatch = -1;

		// Token: 0x040000EF RID: 239
		private static int dlExpansionLimit = -1;

		// Token: 0x040000F0 RID: 240
		private static int webRequestTimeoutInSeconds = -1;

		// Token: 0x040000F1 RID: 241
		private static int maxMeetingsPerMailbox = -1;

		// Token: 0x040000F2 RID: 242
		private static int maxMeetingsToProcessIncludingDuplicates = -1;

		// Token: 0x040000F3 RID: 243
		private static bool? ignoreCertificateErrors;

		// Token: 0x040000F4 RID: 244
		private static bool? calendarRepairOppositeMailboxEwsEnabled;

		// Token: 0x040000F5 RID: 245
		private static bool? calendarRepairForceEwsUsage;
	}
}
