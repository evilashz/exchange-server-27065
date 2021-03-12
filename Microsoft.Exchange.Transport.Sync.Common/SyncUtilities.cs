using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Security.Dkm;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x020000F9 RID: 249
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SyncUtilities
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00023368 File Offset: 0x00021568
		internal static string Fqdn
		{
			get
			{
				if (SyncUtilities.fqdn == null)
				{
					string hostName = Dns.GetHostName();
					IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
					SyncUtilities.fqdn = hostEntry.HostName;
				}
				return SyncUtilities.fqdn;
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0002339C File Offset: 0x0002159C
		public static bool IsDatacenterMode()
		{
			if (SyncUtilities.datacenterMode == null)
			{
				lock (SyncUtilities.syncRoot)
				{
					if (SyncUtilities.datacenterMode == null)
					{
						try
						{
							SyncUtilities.datacenterMode = new bool?(VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled || SyncUtilities.IsEnabledInEnterprise());
						}
						catch (CannotDetermineExchangeModeException ex)
						{
							CommonLoggingHelper.SyncLogSession.LogError((TSLID)8UL, "Unable to determine exchange mode. Assuming not datacenter. Error: {0}", new object[]
							{
								ex
							});
							SyncUtilities.datacenterMode = new bool?(false);
						}
					}
				}
			}
			return SyncUtilities.datacenterMode.Value;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00023470 File Offset: 0x00021670
		public static string GetNextSessionId()
		{
			return Interlocked.Increment(ref SyncUtilities.nextSessionId).ToString("X16", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00023499 File Offset: 0x00021699
		public static string SecureStringToString(SecureString secureString)
		{
			return secureString.AsUnsecureString();
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000234A1 File Offset: 0x000216A1
		public static SecureString StringToSecureString(string clearString)
		{
			return clearString.AsSecureString();
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000234A9 File Offset: 0x000216A9
		public static void ThrowIfArgumentNull(string name, object arg)
		{
			if (arg == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000234B5 File Offset: 0x000216B5
		public static void ThrowIfArgumentNullOrEmpty(string name, string arg)
		{
			if (string.IsNullOrEmpty(arg))
			{
				throw new ArgumentException("The value is set to null or empty", name);
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000234CB File Offset: 0x000216CB
		public static void ThrowIfArgumentNullOrEmpty(string name, ICollection arg)
		{
			if (arg == null || arg.Count == 0)
			{
				throw new ArgumentException("The collection is set to null or is empty", name);
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000234E4 File Offset: 0x000216E4
		public static void ThrowIfArgumentInvalid(string name, bool condition)
		{
			if (!condition)
			{
				throw new ArgumentException("The argument is invalid", name);
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000234F8 File Offset: 0x000216F8
		public static void ThrowIfGuidEmpty(string argumentName, Guid arg)
		{
			if (object.Equals(arg, Guid.Empty))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Guid {0} is Guid.Empty.", new object[]
				{
					argumentName
				}));
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0002353D File Offset: 0x0002173D
		public static void ThrowIfArgumentLessThanZero(string name, long arg)
		{
			if (arg < 0L)
			{
				throw new ArgumentOutOfRangeException(name, arg, "The value is set to less than 0.");
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00023556 File Offset: 0x00021756
		public static void ThrowIfArgumentLessThanZero(string name, int arg)
		{
			if (arg < 0)
			{
				throw new ArgumentOutOfRangeException(name, arg, "The value is set to less than 0.");
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0002356E File Offset: 0x0002176E
		public static void ThrowIfArgumentLessThanEqualToZero(string name, int arg)
		{
			if (arg <= 0)
			{
				throw new ArgumentOutOfRangeException(name, arg, "The value is set to less than equal to 0.");
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00023586 File Offset: 0x00021786
		public static void ThrowIfArgumentLessThanZero(string name, TimeSpan arg)
		{
			if (arg.TotalSeconds < 0.0)
			{
				throw new ArgumentOutOfRangeException(name, arg, "The TimeSpan value is negative.");
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000235AC File Offset: 0x000217AC
		public static void ThrowIfArg1LessThenArg2(string name1, long arg1, string name2, long arg2)
		{
			if (arg1 < arg2)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} value({1}) is set to less than {2} value({3}).", new object[]
				{
					name1,
					arg1,
					name2,
					arg2
				}));
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000235F4 File Offset: 0x000217F4
		public static void ThrowIfArg1LessThenArg2(string name1, ExDateTime arg1, string name2, ExDateTime arg2)
		{
			if (arg1 < arg2)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} value({1}) is set to less than {2} value({3}).", new object[]
				{
					name1,
					arg1,
					name2,
					arg2
				}));
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00023644 File Offset: 0x00021844
		public static void ThrowIfArgumentOutOfRange(string argumentName, double arg, double inclusiveMin, double inclusiveMax)
		{
			if (arg < inclusiveMin || arg > inclusiveMax)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "{0}({1}) not in the range [{2}-{3}]", new object[]
				{
					argumentName,
					arg,
					inclusiveMin,
					inclusiveMax
				});
				throw new ArgumentOutOfRangeException(argumentName, arg, message);
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000236A0 File Offset: 0x000218A0
		public static TOut SafeGetProperty<TOut>(IStorePropertyBag item, PropertyDefinition propertyDefinition)
		{
			return SyncUtilities.SafeGetProperty<TOut>(item, propertyDefinition, default(TOut));
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000236C0 File Offset: 0x000218C0
		public static TOut SafeGetProperty<TOut>(IStorePropertyBag item, PropertyDefinition propertyDefinition, TOut defaultValue)
		{
			object obj = null;
			StorePropertyDefinition storePropertyDefinition = propertyDefinition as StorePropertyDefinition;
			if (storePropertyDefinition != null)
			{
				obj = item.TryGetProperty(storePropertyDefinition);
			}
			if (obj == null || obj is PropertyError)
			{
				return defaultValue;
			}
			return (TOut)((object)obj);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000236F4 File Offset: 0x000218F4
		public static DateTime ToDateTime(string dateTimeString)
		{
			return new DateHeader("<empty>", DateTime.UtcNow)
			{
				Value = dateTimeString
			}.UtcDateTime;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00023720 File Offset: 0x00021920
		public static ExDateTime GetReceivedDate(Stream mimeStream, bool useSentTime)
		{
			ExDateTime? exDateTime = null;
			try
			{
				using (MimeReader mimeReader = new MimeReader(Streams.CreateSuppressCloseWrapper(mimeStream)))
				{
					if (mimeReader.ReadNextPart())
					{
						while (mimeReader.HeaderReader.ReadNextHeader())
						{
							if (mimeReader.HeaderReader.HeaderId == HeaderId.Received)
							{
								ReceivedHeader receivedHeader = Header.ReadFrom(mimeReader.HeaderReader) as ReceivedHeader;
								if (receivedHeader != null && receivedHeader.Date != null)
								{
									DateTime dateTime = SyncUtilities.ToDateTime(receivedHeader.Date);
									return new ExDateTime(ExTimeZone.UtcTimeZone, dateTime);
								}
							}
							if (useSentTime && mimeReader.HeaderReader.HeaderId == HeaderId.Date)
							{
								DateHeader dateHeader = Header.ReadFrom(mimeReader.HeaderReader) as DateHeader;
								if (dateHeader != null)
								{
									exDateTime = new ExDateTime?(new ExDateTime(ExTimeZone.UtcTimeZone, dateHeader.DateTime));
								}
							}
						}
					}
				}
			}
			finally
			{
				mimeStream.Seek(0L, SeekOrigin.Begin);
			}
			if (exDateTime != null)
			{
				return exDateTime.Value;
			}
			return ExDateTime.MinValue;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00023844 File Offset: 0x00021A44
		public static bool ExistsInCollection<T>(T value, IEnumerable<T> collection, IEqualityComparer<T> comparer)
		{
			foreach (T x in collection)
			{
				if (comparer.Equals(x, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00023898 File Offset: 0x00021A98
		public static bool CompareByteArrays(byte[] first, byte[] second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			if (first.Length != second.Length)
			{
				return false;
			}
			for (int i = 0; i < second.Length; i++)
			{
				if (first[i] != second[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000238E4 File Offset: 0x00021AE4
		public static bool TryGetConnectedAccountsDetailsUrl(IExchangePrincipal subscriptionExchangePrincipal, AggregationSubscription subscription, SyncLogSession syncLogSession, out string connectedAccountsDetailsUrl)
		{
			SyncUtilities.ThrowIfArgumentNull("subscriptionExchangePrincipal", subscriptionExchangePrincipal);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			connectedAccountsDetailsUrl = null;
			AggregationSubscriptionType subscriptionType = subscription.SubscriptionType;
			switch (subscriptionType)
			{
			case AggregationSubscriptionType.Pop:
				return EcpUtilities.TryGetPopSubscriptionDetailsUrl(subscriptionExchangePrincipal, (PopAggregationSubscription)subscription, syncLogSession, out connectedAccountsDetailsUrl);
			case (AggregationSubscriptionType)3:
				break;
			case AggregationSubscriptionType.DeltaSyncMail:
				return EcpUtilities.TryGetHotmailSubscriptionDetailsUrl(subscriptionExchangePrincipal, (DeltaSyncAggregationSubscription)subscription, syncLogSession, out connectedAccountsDetailsUrl);
			default:
				if (subscriptionType == AggregationSubscriptionType.IMAP)
				{
					return EcpUtilities.TryGetImapSubscriptionDetailsUrl(subscriptionExchangePrincipal, (IMAPAggregationSubscription)subscription, syncLogSession, out connectedAccountsDetailsUrl);
				}
				if (subscriptionType == AggregationSubscriptionType.Facebook)
				{
					throw new InvalidOperationException("Facebook subscriptions are not viewable under 'Connected Accounts Details'");
				}
				break;
			}
			throw new InvalidOperationException("Unknown subscription type: " + subscription.SubscriptionType);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00023984 File Offset: 0x00021B84
		public static bool HasUnicodeCharacters(string toCheck)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("toCheck", toCheck);
			foreach (char c in toCheck)
			{
				if (c > 'ÿ')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000239C0 File Offset: 0x00021BC0
		public static bool HasUnicodeCharacters(SecureString toCheck)
		{
			SyncUtilities.ThrowIfArgumentNull("toCheck", toCheck);
			if (toCheck.Length == 0)
			{
				throw new ArgumentException("The value has no contents", "toCheck");
			}
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.SecureStringToCoTaskMemUnicode(toCheck);
				for (int i = 0; i < toCheck.Length; i++)
				{
					char c = (char)Marshal.ReadInt16(intPtr, i * Marshal.SizeOf(typeof(short)));
					if (c > 'ÿ')
					{
						return true;
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeCoTaskMemUnicode(intPtr);
				}
			}
			return false;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00023A5C File Offset: 0x00021C5C
		[Conditional("DEBUG")]
		public static void CheckCallStackForTest()
		{
			string stackTrace = Environment.StackTrace;
			if (!stackTrace.Contains("Internal.Exchange.MailboxTransport"))
			{
				string[] separator = new string[]
				{
					Environment.NewLine
				};
				string[] array = stackTrace.Split(separator, StringSplitOptions.None);
				string str = array[2].Replace("at ", string.Empty).Trim();
				throw new InvalidOperationException("Only test code is to call the method: " + str);
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00023AC4 File Offset: 0x00021CC4
		public static void GetHoursAndDaysWithoutSuccessfulSync(AggregationSubscription subscription, bool useLastSyncTimeAsReference, out int days, out int hours)
		{
			TimeSpan timeWithoutSuccessfulSync = SyncUtilities.GetTimeWithoutSuccessfulSync(subscription, useLastSyncTimeAsReference);
			hours = Convert.ToInt32(Math.Floor(timeWithoutSuccessfulSync.TotalHours));
			days = Convert.ToInt32(Math.Floor(timeWithoutSuccessfulSync.TotalDays));
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00023AFF File Offset: 0x00021CFF
		public static string SelectTimeBasedString(int days, int hours, string daySingularString, string dayPluralString, string hourSingularString, string hourPluralString, string defaultString)
		{
			if (days > 0)
			{
				if (days > 1)
				{
					return dayPluralString;
				}
				return daySingularString;
			}
			else
			{
				if (hours <= 0)
				{
					return defaultString;
				}
				if (hours > 1)
				{
					return hourPluralString;
				}
				return hourSingularString;
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00023B20 File Offset: 0x00021D20
		public static void RunUserWorkItemOnNewThreadAndBlockCurrentThread(ThreadStart userWorkItem)
		{
			if (userWorkItem != null)
			{
				Thread thread = new Thread(userWorkItem);
				thread.Start();
				thread.Join();
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00023B43 File Offset: 0x00021D43
		public static long ConvertToLong(double doubleValue)
		{
			if (doubleValue > 9.223372036854776E+18)
			{
				return long.MaxValue;
			}
			if (doubleValue < -9.223372036854776E+18)
			{
				return long.MinValue;
			}
			return (long)doubleValue;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00023B73 File Offset: 0x00021D73
		public static bool IsDetailedAggregationStatusDueToResourceProtectionThrottling(DetailedAggregationStatus status)
		{
			return status == DetailedAggregationStatus.RemoteServerIsSlow || status == DetailedAggregationStatus.RemoteServerIsBackedOff || status == DetailedAggregationStatus.RemoteServerIsPoisonous || status == DetailedAggregationStatus.SyncStateSizeError;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00023B8C File Offset: 0x00021D8C
		public static string ComputeSHA512Hash(string inputString)
		{
			if (string.IsNullOrEmpty(inputString))
			{
				return inputString;
			}
			string result;
			using (SHA512Cng sha512Cng = new SHA512Cng())
			{
				byte[] inArray = sha512Cng.ComputeHash(Encoding.UTF8.GetBytes(inputString));
				string text = Convert.ToBase64String(inArray);
				result = text;
			}
			return result;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00023BE4 File Offset: 0x00021DE4
		public static string GenerateMessageId(string id)
		{
			return string.Format("<{0}@{1}>", id, SyncUtilities.Fqdn);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00023BF6 File Offset: 0x00021DF6
		public static bool IsContactSubscriptionType(AggregationSubscriptionType subscriptionType)
		{
			return subscriptionType == AggregationSubscriptionType.Facebook || subscriptionType == AggregationSubscriptionType.LinkedIn;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00023C04 File Offset: 0x00021E04
		public static MailboxSession OpenMailboxSessionAndHaveCompleteExchangePrincipal(Guid mailboxGuid, Guid databaseGuid, SyncUtilities.MailboxSessionOpener mailboxSessionOpenner)
		{
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfArgumentNull("mailboxSessionOpenner", mailboxSessionOpenner);
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromMailboxData(mailboxGuid, databaseGuid, Array<CultureInfo>.Empty);
			MailboxSession mailboxSession = mailboxSessionOpenner(exchangePrincipal);
			bool flag = false;
			try
			{
				mailboxSession.ReconstructExchangePrincipal();
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					mailboxSession.Dispose();
					mailboxSession = null;
				}
			}
			return mailboxSession;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00023C70 File Offset: 0x00021E70
		public static IExchangeGroupKey CreateExchangeGroupKey()
		{
			return PeopleConnectExchangeGroupKeyFactory.Create();
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00023C77 File Offset: 0x00021E77
		public static bool IsEnabledInEnterprise()
		{
			return PeopleConnectRegistryReader.Read().DogfoodInEnterprise;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00023C84 File Offset: 0x00021E84
		public static bool VerifyNestedInnerExceptionType(Exception exception, Type exceptionType)
		{
			for (Exception innerException = exception.InnerException; innerException != null; innerException = innerException.InnerException)
			{
				if (exceptionType.IsAssignableFrom(innerException.GetType()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00023CB8 File Offset: 0x00021EB8
		private static TimeSpan GetTimeWithoutSuccessfulSync(AggregationSubscription subscription, bool useLastSyncTimeAsReference)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			DateTime d = DateTime.UtcNow;
			if (useLastSyncTimeAsReference)
			{
				d = ((subscription.LastSyncTime != null) ? subscription.LastSyncTime.Value : subscription.CreationTime);
			}
			return d - subscription.AdjustedLastSuccessfulSyncTime;
		}

		// Token: 0x040003FF RID: 1023
		public static readonly int MaximumFqdnLength = 126;

		// Token: 0x04000400 RID: 1024
		public static readonly DateTime ZeroTime = new DateTime(504911232000000000L, DateTimeKind.Utc);

		// Token: 0x04000401 RID: 1025
		public static readonly ExDateTime ExZeroTime = (ExDateTime)SyncUtilities.ZeroTime;

		// Token: 0x04000402 RID: 1026
		public static readonly long DataNotAvailable = -1L;

		// Token: 0x04000403 RID: 1027
		public static readonly string WorkerClientInfoString = "Client=TransportSync;Action=SyncWorker";

		// Token: 0x04000404 RID: 1028
		private static long nextSessionId = DateTime.UtcNow.Ticks;

		// Token: 0x04000405 RID: 1029
		private static bool? datacenterMode;

		// Token: 0x04000406 RID: 1030
		private static object syncRoot = new object();

		// Token: 0x04000407 RID: 1031
		private static string fqdn;

		// Token: 0x020000FA RID: 250
		// (Invoke) Token: 0x0600078C RID: 1932
		public delegate MailboxSession MailboxSessionOpener(IExchangePrincipal exchangePrincipal);
	}
}
