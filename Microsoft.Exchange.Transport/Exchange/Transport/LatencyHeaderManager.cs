using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200016F RID: 367
	internal static class LatencyHeaderManager
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x00040BEC File Offset: 0x0003EDEC
		public static void HandleLatencyHeaders(IList<IPRange> internalSmtpServers, HeaderList headers, DateTime localArrivalTime, LatencyComponent previousHopDeliveryLatencyComponent)
		{
			LatencyHeaderManager.HandleLatencyHeaders(internalSmtpServers, headers, localArrivalTime, previousHopDeliveryLatencyComponent, LatencyComponent.None, TimeSpan.Zero);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00040C00 File Offset: 0x0003EE00
		public static void HandleLatencyHeaders(IList<IPRange> internalSmtpServers, HeaderList headers, DateTime localArrivalTime, LatencyComponent previousHopDeliveryLatencyComponent, LatencyComponent previousHopSubComponent, TimeSpan previousHopSubComponentLatency)
		{
			if (!LatencyTracker.MessageLatencyEnabled)
			{
				return;
			}
			bool flag = false;
			string latencyInProgressFqdn;
			LatencyInProgressParser latencyInProgressParser;
			if (!LatencyHeaderManager.TryGetInProgressHeaderInfo(headers, out latencyInProgressFqdn, out latencyInProgressParser))
			{
				ExTraceGlobals.PreviousHopLatencyTracer.TraceDebug(0L, "So this server is treated as First E14 Server.");
				flag = true;
			}
			Header[] array = headers.FindAll(HeaderId.Received);
			if (array == null || array.Length < 2)
			{
				ExTraceGlobals.PreviousHopLatencyTracer.TraceDebug(0L, "Received Header list is null or contains less than 2 received headers. No Previous Hop Latency Headers will be added.");
				return;
			}
			if (flag)
			{
				LatencyHeaderManager.StampPreviousHopLatencyForFirstE14Server(internalSmtpServers, headers, localArrivalTime, array);
				return;
			}
			LatencyHeaderManager.StampPreviousHopLatencyForSubsequentE14Server(latencyInProgressFqdn, latencyInProgressParser, internalSmtpServers, headers, localArrivalTime, array, previousHopDeliveryLatencyComponent, previousHopSubComponent, previousHopSubComponentLatency);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00040C78 File Offset: 0x0003EE78
		public static void FinalizeLatencyHeadersOnHub(TransportMailItem mailItem, string localServerFqdn)
		{
			if (!LatencyTracker.MessageLatencyEnabled)
			{
				return;
			}
			bool flag = false;
			if (mailItem.LatencyTracker != null || LatencyTracker.ServerLatencyTrackingEnabled)
			{
				foreach (MailRecipient mailRecipient in mailItem.Recipients.AllUnprocessed)
				{
					NextHopType nextHopType = mailRecipient.NextHop.NextHopType;
					if (nextHopType.IsHubRelayDeliveryType || nextHopType.IsSmtpConnectorDeliveryType || nextHopType.DeliveryType == DeliveryType.SmtpDeliveryToMailbox || nextHopType.DeliveryType == DeliveryType.SmtpRelayWithinAdSiteToEdge)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				LatencyHeaderManager.StampLatencyInProgressHeader(mailItem, DateTime.MinValue, localServerFqdn);
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00040D28 File Offset: 0x0003EF28
		public static void FinalizeLatencyHeadersOnFrontend(TransportMailItem mailItem, string localServerFqdn)
		{
			LatencyHeaderManager.CheckAndStampLatencyInProgressHeader(mailItem, DateTime.MinValue, localServerFqdn);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00040D36 File Offset: 0x0003EF36
		public static void FinalizeLatencyHeadersOnMailboxTransportSubmission(TransportMailItem mailItem, DateTime originalReceiveTime, string localServerFqdn)
		{
			LatencyHeaderManager.CheckAndStampLatencyInProgressHeader(mailItem, originalReceiveTime, localServerFqdn);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00040D40 File Offset: 0x0003EF40
		public static void StampPreviousHopMessageLatency(string latencyHeaderValue, HeaderList headers)
		{
			if (!LatencyTracker.MessageLatencyEnabled)
			{
				return;
			}
			if (!string.IsNullOrEmpty(latencyHeaderValue))
			{
				Util.AppendAsciiHeader(headers, "X-MS-Exchange-Organization-MessageLatency", latencyHeaderValue);
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00040D60 File Offset: 0x0003EF60
		public static IList<string> GetPreviousHops(IReadOnlyMailItem mailItem)
		{
			if (!LatencyTracker.MessageLatencyEnabled)
			{
				return null;
			}
			List<string> list = null;
			for (Header header = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-MessageLatency"); header != null; header = mailItem.RootPart.Headers.FindNext(header))
			{
				string headerValue = Util.GetHeaderValue(header);
				if (!string.IsNullOrEmpty(headerValue))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(headerValue);
				}
			}
			return list;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00040DC5 File Offset: 0x0003EFC5
		private static void CheckAndStampLatencyInProgressHeader(TransportMailItem mailItem, DateTime originalReceiveTime, string localServerFqdn)
		{
			if (!LatencyTracker.MessageLatencyEnabled)
			{
				return;
			}
			if (mailItem.LatencyTracker != null || LatencyTracker.ServerLatencyTrackingEnabled)
			{
				LatencyHeaderManager.StampLatencyInProgressHeader(mailItem, originalReceiveTime, localServerFqdn);
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00040DE8 File Offset: 0x0003EFE8
		private static void StampLatencyInProgressHeader(TransportMailItem mailItem, DateTime originalReceiveTime, string localServerFqdn)
		{
			string headerValue = LatencyFormatter.FormatLatencyInProgressHeader(mailItem, localServerFqdn, originalReceiveTime, false);
			LatencyHeaderManager.StampHeader(mailItem, "X-MS-Exchange-Organization-MessageHighPrecisionLatencyInProgress", headerValue);
			if (LatencyTracker.TreeLatencyTrackingEnabled)
			{
				headerValue = LatencyFormatter.FormatLatencyInProgressHeader(mailItem, localServerFqdn, originalReceiveTime, true);
				LatencyHeaderManager.StampHeader(mailItem, "X-MS-Exchange-Organization-OrderedPrecisionLatencyInProgress", headerValue);
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00040E28 File Offset: 0x0003F028
		private static void StampHeader(TransportMailItem mailItem, string headerName, string headerValue)
		{
			if (string.IsNullOrEmpty(headerValue))
			{
				mailItem.RootPart.Headers.RemoveAll(headerName);
				return;
			}
			Util.SetAsciiHeader(mailItem.RootPart.Headers, headerName, headerValue);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00040E58 File Offset: 0x0003F058
		private static bool TryGetInProgressHeaderInfo(HeaderList headers, out string inProgressFqdn, out LatencyInProgressParser parser)
		{
			inProgressFqdn = null;
			parser = null;
			string text = null;
			string text2;
			LatencyInProgressParser latencyInProgressParser;
			if (LatencyHeaderManager.TryGetInProgressInfoFromHeader(headers, "X-MS-Exchange-Organization-MessageHighPrecisionLatencyInProgress", out text2, out latencyInProgressParser))
			{
				parser = latencyInProgressParser;
				inProgressFqdn = text2;
			}
			if (parser == null)
			{
				return false;
			}
			if (!LatencyTracker.TreeLatencyTrackingEnabled)
			{
				return true;
			}
			LatencyInProgressParser latencyInProgressParser2;
			if (LatencyHeaderManager.TryGetInProgressInfoFromHeader(headers, "X-MS-Exchange-Organization-OrderedPrecisionLatencyInProgress", out text, out latencyInProgressParser2))
			{
				parser = latencyInProgressParser2;
				inProgressFqdn = text;
			}
			if (text != text2 || latencyInProgressParser2.PreDeliveryTime != latencyInProgressParser.PreDeliveryTime)
			{
				inProgressFqdn = text2;
				parser = latencyInProgressParser;
			}
			return true;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00040ED0 File Offset: 0x0003F0D0
		private static bool TryGetInProgressInfoFromHeader(HeaderList headers, string latencyInProgressOrgHeader, out string latencyInProgressFqdn, out LatencyInProgressParser latencyInProgressParser)
		{
			latencyInProgressFqdn = null;
			latencyInProgressParser = null;
			Header header = headers.FindFirst(latencyInProgressOrgHeader);
			if (header == null)
			{
				ExTraceGlobals.PreviousHopLatencyTracer.TraceDebug<string>(0L, "{0} header not found.", latencyInProgressOrgHeader);
				return false;
			}
			string headerValue = Util.GetHeaderValue(header);
			if (string.IsNullOrEmpty(headerValue))
			{
				ExTraceGlobals.PreviousHopLatencyTracer.TraceDebug<string>(0L, "{0} header value is null or Empty.", latencyInProgressOrgHeader);
				return false;
			}
			latencyInProgressParser = new LatencyInProgressParser();
			if (!latencyInProgressParser.TryParse(headerValue))
			{
				ExTraceGlobals.PreviousHopLatencyTracer.TraceDebug<string>(0L, "Failed parsing inProgressHeader: {0}. This server will be treated as First E14 server.", headerValue);
				return false;
			}
			latencyInProgressFqdn = headerValue.Substring(latencyInProgressParser.ServerFqdnStartIndex, latencyInProgressParser.ServerFqdnLength);
			if (string.IsNullOrEmpty(latencyInProgressFqdn))
			{
				ExTraceGlobals.PreviousHopLatencyTracer.TraceDebug<string>(0L, "Failed to read server FQDN for inProgressHeader: {0}. This server will be treated as First E14 server.", headerValue);
				return false;
			}
			return true;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00040F80 File Offset: 0x0003F180
		private static bool StampPreviousHopMessageLatency(string fqdn, TimeSpan totalLatency, HeaderList headers)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				return false;
			}
			if (totalLatency < TimeSpan.Zero)
			{
				if ((TimeSpan.Zero - totalLatency).Ticks >= 20000000L)
				{
					LatencyHeaderManager.logger.LogEvent(TransportEventLogConstants.Tuple_ExternalServersLatencyTimeNotSync, null, new object[0]);
				}
				totalLatency = TimeSpan.Zero;
			}
			string latencyHeaderValue = LatencyFormatter.FormatExternalLatencyHeader(fqdn, totalLatency);
			LatencyHeaderManager.StampPreviousHopMessageLatency(latencyHeaderValue, headers);
			return true;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00040FF0 File Offset: 0x0003F1F0
		private static bool TryParseDateTime(string s, out DateTime time)
		{
			string[] array = new string[]
			{
				"(UTC)",
				"UTC"
			};
			if (string.IsNullOrEmpty(s))
			{
				time = DateTime.MinValue;
				return false;
			}
			DateTime dateTime;
			if (DateTime.TryParse(s, out dateTime))
			{
				time = dateTime.ToUniversalTime();
				return true;
			}
			bool flag = false;
			foreach (string text in array)
			{
				if (s.EndsWith(text))
				{
					s = s.Substring(0, s.Length - text.Length);
					flag = true;
					break;
				}
			}
			if (flag)
			{
				DateTimeStyles styles = DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal;
				return DateTime.TryParse(s, CultureInfo.CurrentCulture, styles, out time) || DateTime.TryParse(s, CultureInfo.InvariantCulture, styles, out time);
			}
			time = DateTime.MinValue;
			return false;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x000410C0 File Offset: 0x0003F2C0
		private static bool StampPreviousHopMessageLatency(HeaderList headers, ReceivedHeader recentReceivedHeader, ReceivedHeader olderReceivedHeader)
		{
			if (recentReceivedHeader == null || olderReceivedHeader == null)
			{
				return false;
			}
			DateTime d;
			DateTime d2;
			if (!string.IsNullOrEmpty(olderReceivedHeader.Date) && !string.IsNullOrEmpty(recentReceivedHeader.Date) && LatencyHeaderManager.TryParseDateTime(recentReceivedHeader.Date, out d) && LatencyHeaderManager.TryParseDateTime(olderReceivedHeader.Date, out d2))
			{
				TimeSpan totalLatency = d - d2;
				string recentServerFqdn = LatencyHeaderManager.GetRecentServerFqdn(recentReceivedHeader, olderReceivedHeader);
				return !string.IsNullOrEmpty(recentServerFqdn) && LatencyHeaderManager.StampPreviousHopMessageLatency(recentServerFqdn, totalLatency, headers);
			}
			return false;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00041134 File Offset: 0x0003F334
		private static void StampPreviousHopLatencyForFirstE14Server(IList<IPRange> internalSmtpServers, HeaderList headers, DateTime localArrivalTime, Header[] receivedHeaderList)
		{
			int i;
			for (i = 0; i < receivedHeaderList.Length - 1; i++)
			{
				ReceivedHeader receivedHeader = receivedHeaderList[i] as ReceivedHeader;
				ReceivedHeader receivedHeader2 = receivedHeaderList[i + 1] as ReceivedHeader;
				if (receivedHeader == null || receivedHeader2 == null || (!Util.IsReceivedHeaderFromAddressTrusted(internalSmtpServers, receivedHeader) && !Util.IsPickupReceivedHeader(receivedHeader)))
				{
					break;
				}
			}
			ReceivedHeader lastProcessedReceivedHeader = LatencyHeaderManager.StampPreviousHopMessageLatencies(headers, receivedHeaderList, i - 1);
			long latencySeconds = LatencyHeaderManager.HandleExternalServerLatency(lastProcessedReceivedHeader, headers, localArrivalTime);
			LatencyTracker.UpdateExternalPartnerServerLatency(latencySeconds);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x000411A0 File Offset: 0x0003F3A0
		private static void StampPreviousHopLatencyForSubsequentE14Server(string latencyInProgressFqdn, LatencyInProgressParser latencyInProgressParser, IList<IPRange> trustedServers, HeaderList headers, DateTime localArrivalTime, Header[] receivedHeaderList, LatencyComponent previousHopDeliveryLatencyComponent, LatencyComponent previousHopSubComponent, TimeSpan previousHopSubComponentLatency)
		{
			DateTime localArrivalTime2 = localArrivalTime;
			int num = 0;
			for (int i = 0; i < receivedHeaderList.Length - 1; i++)
			{
				ReceivedHeader receivedHeader = receivedHeaderList[i] as ReceivedHeader;
				ReceivedHeader receivedHeader2 = receivedHeaderList[i + 1] as ReceivedHeader;
				if (receivedHeader == null || receivedHeader2 == null || LatencyHeaderManager.IsReceivedHeaderFromEquals(receivedHeader, latencyInProgressFqdn) || (!Util.IsReceivedHeaderFromAddressTrusted(trustedServers, receivedHeader) && !Util.IsPickupReceivedHeader(receivedHeader)) || string.IsNullOrEmpty(receivedHeader2.Date) || !LatencyHeaderManager.TryParseDateTime(receivedHeader2.Date, out localArrivalTime2))
				{
					break;
				}
				num = i + 1;
			}
			string latencyHeaderValue = LatencyFormatter.FormatLatencyHeader(latencyInProgressParser, localArrivalTime2, previousHopDeliveryLatencyComponent, previousHopSubComponent, previousHopSubComponentLatency);
			LatencyHeaderManager.StampPreviousHopMessageLatency(latencyHeaderValue, headers);
			ReceivedHeader lastProcessedReceivedHeader = LatencyHeaderManager.StampPreviousHopMessageLatencies(headers, receivedHeaderList, num - 1);
			if (previousHopDeliveryLatencyComponent != LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionSmtpOut)
			{
				LatencyHeaderManager.HandleExternalServerLatency(lastProcessedReceivedHeader, headers, localArrivalTime);
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0004125C File Offset: 0x0003F45C
		private static bool IsReceivedHeaderFromEquals(ReceivedHeader receivedHeader, string fqdn)
		{
			return receivedHeader != null && !string.IsNullOrEmpty(fqdn) && ((!string.IsNullOrEmpty(receivedHeader.From) && receivedHeader.From.Equals(fqdn, StringComparison.OrdinalIgnoreCase)) || (!string.IsNullOrEmpty(receivedHeader.FromTcpInfo) && receivedHeader.FromTcpInfo.Equals(fqdn, StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x000412B4 File Offset: 0x0003F4B4
		private static string GetRecentServerFqdn(ReceivedHeader recentReceivedHeader, ReceivedHeader olderReceivedHeader)
		{
			string[] array;
			if (Util.IsPickupReceivedHeader(recentReceivedHeader))
			{
				array = new string[]
				{
					olderReceivedHeader.By,
					olderReceivedHeader.ByTcpInfo,
					recentReceivedHeader.From,
					recentReceivedHeader.FromTcpInfo
				};
			}
			else
			{
				array = new string[]
				{
					recentReceivedHeader.From,
					olderReceivedHeader.By,
					recentReceivedHeader.FromTcpInfo,
					olderReceivedHeader.ByTcpInfo
				};
			}
			string result = null;
			for (int i = 0; i < array.Length; i++)
			{
				if (!string.IsNullOrEmpty(array[i]))
				{
					result = array[i];
					break;
				}
			}
			return result;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0004134C File Offset: 0x0003F54C
		private static ReceivedHeader StampPreviousHopMessageLatencies(HeaderList headers, Header[] receivedHeaderList, int startIndex)
		{
			ReceivedHeader receivedHeader = null;
			for (int i = startIndex; i >= 0; i--)
			{
				ReceivedHeader recentReceivedHeader = receivedHeaderList[i] as ReceivedHeader;
				ReceivedHeader receivedHeader2 = receivedHeaderList[i + 1] as ReceivedHeader;
				bool flag = LatencyHeaderManager.StampPreviousHopMessageLatency(headers, recentReceivedHeader, receivedHeader2);
				if (receivedHeader == null && flag)
				{
					receivedHeader = receivedHeader2;
				}
			}
			return receivedHeader;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00041394 File Offset: 0x0003F594
		private static long HandleExternalServerLatency(ReceivedHeader lastProcessedReceivedHeader, HeaderList headers, DateTime localArrivalTime)
		{
			DateTime lastProcessedReceivedHeaderTime = DateTime.MaxValue;
			DateTime dateTime;
			if (lastProcessedReceivedHeader != null && LatencyHeaderManager.TryParseDateTime(lastProcessedReceivedHeader.Date, out dateTime))
			{
				lastProcessedReceivedHeaderTime = dateTime;
			}
			DateTime dateTime2 = LatencyHeaderManager.UpdateOrgArrivalTimeIfNeccessary(headers, lastProcessedReceivedHeaderTime);
			long num = 0L;
			if (dateTime2 != DateTime.MaxValue && localArrivalTime > dateTime2)
			{
				num = (long)(localArrivalTime - dateTime2).TotalSeconds;
				LatencyTracker.UpdateExternalServerLatency(num);
			}
			return num;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x000413F8 File Offset: 0x0003F5F8
		private static DateTime UpdateOrgArrivalTimeIfNeccessary(HeaderList headers, DateTime lastProcessedReceivedHeaderTime)
		{
			Header header = headers.FindFirst("X-MS-Exchange-Organization-OriginalArrivalTime");
			DateTime dateTime = DateTime.MaxValue;
			if (header == null || !Util.TryParseOrganizationalMessageArrivalTime(header.Value, out dateTime) || dateTime.ToUniversalTime() > lastProcessedReceivedHeaderTime)
			{
				if (lastProcessedReceivedHeaderTime != DateTime.MaxValue)
				{
					string value = Util.FormatOrganizationalMessageArrivalTime(lastProcessedReceivedHeaderTime);
					if (header != null)
					{
						header.Value = value;
					}
					else
					{
						header = new AsciiTextHeader("X-MS-Exchange-Organization-OriginalArrivalTime", value);
						headers.AppendChild(header);
					}
				}
				dateTime = lastProcessedReceivedHeaderTime;
			}
			else if (dateTime != DateTime.MaxValue)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			return dateTime;
		}

		// Token: 0x04000805 RID: 2053
		private const string LatencyOrgHeader = "X-MS-Exchange-Organization-MessageLatency";

		// Token: 0x04000806 RID: 2054
		private const string HighPrecisionLatencyInProgressOrgHeader = "X-MS-Exchange-Organization-MessageHighPrecisionLatencyInProgress";

		// Token: 0x04000807 RID: 2055
		private const string TreeLatencyInProgressOrgHeader = "X-MS-Exchange-Organization-OrderedPrecisionLatencyInProgress";

		// Token: 0x04000808 RID: 2056
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.GeneralTracer.Category, TransportEventLog.GetEventSource());
	}
}
