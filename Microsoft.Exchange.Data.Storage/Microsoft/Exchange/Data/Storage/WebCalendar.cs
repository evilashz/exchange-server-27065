using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DE8 RID: 3560
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WebCalendar
	{
		// Token: 0x06007A71 RID: 31345 RVA: 0x0021D47C File Offset: 0x0021B67C
		static WebCalendar()
		{
			WebCalendar.RegisterPrefixes();
		}

		// Token: 0x06007A72 RID: 31346 RVA: 0x0021D4D4 File Offset: 0x0021B6D4
		public static void RegisterPrefixes()
		{
			WebRequest.RegisterPrefix("webcal", new WebCalendar.WebCalRequestCreator(Uri.UriSchemeHttp));
			WebRequest.RegisterPrefix("webcals", new WebCalendar.WebCalRequestCreator(Uri.UriSchemeHttps));
		}

		// Token: 0x06007A73 RID: 31347 RVA: 0x0021D520 File Offset: 0x0021B720
		public static SubscribeResultsWebCal Subscribe(MailboxSession mailboxSession, string iCalUrlString, string folderName = null)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			Util.ThrowOnNullOrEmptyArgument(iCalUrlString, "iCalUrlString");
			Uri iCalUrl = null;
			if (!PublishingUrl.IsAbsoluteUriString(iCalUrlString, out iCalUrl) || !Array.Exists<string>(WebCalendar.validWebCalendarSchemes, (string scheme) => StringComparer.OrdinalIgnoreCase.Equals(scheme, iCalUrl.Scheme)))
			{
				throw new InvalidSharingDataException("iCalUrlString", iCalUrlString);
			}
			string text;
			if ((text = folderName) == null && (text = WebCalendar.GetFolderNameFromInternetCalendar(iCalUrl)) == null)
			{
				text = (WebCalendar.GetFolderNameFromUrl(iCalUrl) ?? ClientStrings.Calendar.ToString(mailboxSession.InternalPreferedCulture));
			}
			folderName = text;
			PublishingSubscriptionData newSubscription = WebCalendar.CreateSubscriptionData(iCalUrl, folderName);
			return WebCalendar.InternalSubscribe(mailboxSession, newSubscription, null, null);
		}

		// Token: 0x06007A74 RID: 31348 RVA: 0x0021D5D0 File Offset: 0x0021B7D0
		internal static SubscribeResultsWebCal InternalSubscribe(MailboxSession mailboxSession, PublishingSubscriptionData newSubscription, string initiatorSmtpAddress, string initiatorName)
		{
			if (mailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion < Server.E14SP1MinVersion)
			{
				throw new NotSupportedWithMailboxVersionException();
			}
			StoreObjectId storeObjectId = null;
			SubscribeResultsWebCal result;
			using (PublishingSubscriptionManager publishingSubscriptionManager = new PublishingSubscriptionManager(mailboxSession))
			{
				PublishingFolderManager publishingFolderManager = new PublishingFolderManager(mailboxSession);
				PublishingSubscriptionData existing = publishingSubscriptionManager.GetExisting(newSubscription.Key);
				PublishingSubscriptionData publishingSubscriptionData = existing ?? newSubscription;
				IdAndName idAndName = publishingFolderManager.EnsureFolder(publishingSubscriptionData);
				if (publishingSubscriptionData.LocalFolderId == null || !publishingSubscriptionData.LocalFolderId.Equals(idAndName.Id))
				{
					storeObjectId = (publishingSubscriptionData.LocalFolderId = idAndName.Id);
				}
				PublishingSubscriptionData publishingSubscriptionData2 = publishingSubscriptionManager.CreateOrUpdate(publishingSubscriptionData, false);
				if (!publishingSubscriptionData.LocalFolderId.Equals(publishingSubscriptionData2.LocalFolderId))
				{
					idAndName = publishingFolderManager.GetFolder(publishingSubscriptionData2);
				}
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, StoreObjectId>(0L, "{0}: WebCalendar.InternalSubscribe will request a sync for folder id {1}.", mailboxSession.MailboxOwner, idAndName.Id);
				SyncAssistantInvoker.SyncFolder(mailboxSession, idAndName.Id);
				result = new SubscribeResultsWebCal(SharingDataType.Calendar, initiatorSmtpAddress, initiatorName, publishingSubscriptionData.RemoteFolderName, publishingSubscriptionData.PublishingUrl, idAndName.Id, storeObjectId != null, idAndName.Name);
			}
			return result;
		}

		// Token: 0x06007A75 RID: 31349 RVA: 0x0021D6FC File Offset: 0x0021B8FC
		private static PublishingSubscriptionData CreateSubscriptionData(Uri iCalUrl, string folderName)
		{
			return new PublishingSubscriptionData(SharingDataType.Calendar.PublishName, iCalUrl, folderName, null);
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x0021D710 File Offset: 0x0021B910
		private static string GetFolderNameFromInternetCalendar(Uri iCalUrl)
		{
			if (iCalUrl.Scheme == "holidays")
			{
				return null;
			}
			HttpWebRequest httpWebRequest = WebRequest.Create(iCalUrl) as HttpWebRequest;
			httpWebRequest.AddRange(0, 511);
			httpWebRequest.Timeout = 10000;
			try
			{
				using (WebResponse response = httpWebRequest.GetResponse())
				{
					using (Stream responseStream = response.GetResponseStream())
					{
						byte[] array = new byte[512];
						int newSize = responseStream.Read(array, 0, 512);
						Array.Resize<byte>(ref array, newSize);
						string @string = Encoding.UTF8.GetString(array);
						int num = @string.IndexOf("X-WR-CALNAME:", StringComparison.InvariantCultureIgnoreCase);
						if (num > 0)
						{
							int num2 = @string.IndexOf(Environment.NewLine, num);
							if (num2 > num)
							{
								num += "X-WR-CALNAME:".Length;
								return @string.Substring(num, num2 - num).Trim();
							}
						}
					}
				}
			}
			catch (WebException arg)
			{
				ExTraceGlobals.SharingTracer.TraceError<WebException>(0L, "WebCalendar.GetFolderNameFromInternetCalendar: Unable to determine the calendar folder name due to {0}.", arg);
			}
			return null;
		}

		// Token: 0x06007A77 RID: 31351 RVA: 0x0021D840 File Offset: 0x0021BA40
		private static string GetFolderNameFromUrl(Uri iCalUrl)
		{
			string input = iCalUrl.LocalPath.Replace("+", " ").Replace(" ", "_");
			Match match = WebCalendar.regex.Match(input);
			if (match.Success)
			{
				return match.Result("${name}");
			}
			return null;
		}

		// Token: 0x0400544F RID: 21583
		public const string UriSchemeWebCal = "webcal";

		// Token: 0x04005450 RID: 21584
		public const string UriSchemeWebCals = "webcals";

		// Token: 0x04005451 RID: 21585
		public const string UriSchemeHolidayCalendars = "holidays";

		// Token: 0x04005452 RID: 21586
		private const string IcsCalendarNameHeader = "X-WR-CALNAME:";

		// Token: 0x04005453 RID: 21587
		private const int IcsMaximumBytesToRead = 512;

		// Token: 0x04005454 RID: 21588
		private const int IcsRequestTimeout = 10000;

		// Token: 0x04005455 RID: 21589
		private static readonly string[] validWebCalendarSchemes = new string[]
		{
			Uri.UriSchemeHttp,
			Uri.UriSchemeHttps,
			"webcal",
			"webcals",
			"holidays"
		};

		// Token: 0x04005456 RID: 21590
		private static readonly Regex regex = new Regex("/(?<name>[^/ ]+).ics$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x02000DE9 RID: 3561
		private class WebCalRequestCreator : IWebRequestCreate
		{
			// Token: 0x06007A79 RID: 31353 RVA: 0x0021D89B File Offset: 0x0021BA9B
			public WebCalRequestCreator(string newUriScheme)
			{
				this.NewUriScheme = newUriScheme;
			}

			// Token: 0x06007A7A RID: 31354 RVA: 0x0021D8AC File Offset: 0x0021BAAC
			public WebRequest Create(Uri uri)
			{
				return WebRequest.Create(new UriBuilder(uri)
				{
					Scheme = this.NewUriScheme
				}.Uri);
			}

			// Token: 0x170020C0 RID: 8384
			// (get) Token: 0x06007A7B RID: 31355 RVA: 0x0021D8D7 File Offset: 0x0021BAD7
			// (set) Token: 0x06007A7C RID: 31356 RVA: 0x0021D8DF File Offset: 0x0021BADF
			private string NewUriScheme { get; set; }
		}
	}
}
