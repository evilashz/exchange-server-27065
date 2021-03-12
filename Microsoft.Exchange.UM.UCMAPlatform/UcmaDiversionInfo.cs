using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Internal.Utilities;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000045 RID: 69
	internal class UcmaDiversionInfo : PlatformDiversionInfo
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000D000 File Offset: 0x0000B200
		public UcmaDiversionInfo(string header, string calledParty, string userAtHost, RedirectReason reason, DiversionSource source) : base(header, calledParty, userAtHost, reason, source)
		{
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000D010 File Offset: 0x0000B210
		public static List<PlatformDiversionInfo> CreateDiversionInfoList(DiversionContext diversionContext)
		{
			string header = null;
			string value = null;
			List<PlatformDiversionInfo> result;
			try
			{
				List<PlatformDiversionInfo> list = new List<PlatformDiversionInfo>(2);
				if (diversionContext != null)
				{
					DiversionSource diversionSource = UcmaDiversionInfo.GetDiversionSource(diversionContext);
					Collection<DivertedDestination> allDivertedDestinations = diversionContext.GetAllDivertedDestinations();
					if (allDivertedDestinations != null)
					{
						foreach (DivertedDestination destination in allDivertedDestinations)
						{
							UcmaDiversionInfo.AddDiversion(list, diversionSource, destination, out header, out value);
						}
					}
					if (diversionSource == DiversionSource.HistoryInfo)
					{
						UcmaDiversionInfo.RemoveTrailingHistoryInfoEntries(list);
						UcmaDiversionInfo.RemoveHistoryInfoEntriesWithoutReason(list);
					}
					UcmaDiversionInfo.RemoveDuplicateDiversionInfoEntries(list);
				}
				result = list;
			}
			catch (MessageParsingException)
			{
				throw new InvalidSIPHeaderException("INVITE", header, value);
			}
			return result;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000D0CB File Offset: 0x0000B2CB
		private static void RemoveHistoryInfoEntriesWithoutReason(List<PlatformDiversionInfo> infoList)
		{
			infoList.RemoveAll((PlatformDiversionInfo o) => o.RedirectReason == RedirectReason.None);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		private static void RemoveTrailingHistoryInfoEntries(List<PlatformDiversionInfo> infoList)
		{
			if (infoList.Count > 0)
			{
				int i = infoList.Count - 1;
				string originalCalledParty = infoList[i].OriginalCalledParty;
				while (i >= 0)
				{
					string originalCalledParty2 = infoList[i].OriginalCalledParty;
					if (!string.Equals(originalCalledParty, originalCalledParty2, StringComparison.OrdinalIgnoreCase))
					{
						break;
					}
					infoList.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000D148 File Offset: 0x0000B348
		private static void RemoveDuplicateDiversionInfoEntries(List<PlatformDiversionInfo> infoList)
		{
			if (infoList.Count > 0)
			{
				int i = 0;
				Dictionary<string, int> dictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
				while (i < infoList.Count)
				{
					string originalCalledParty = infoList[i].OriginalCalledParty;
					if (dictionary.ContainsKey(originalCalledParty))
					{
						infoList.RemoveAt(i);
					}
					else
					{
						dictionary[originalCalledParty] = 1;
						i++;
					}
				}
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		private static void AddDiversion(List<PlatformDiversionInfo> infoList, DiversionSource source, DivertedDestination destination, out string name, out string value)
		{
			name = string.Empty;
			value = string.Empty;
			if (destination != null)
			{
				name = destination.DiversionHeader.Name;
				value = destination.DiversionHeader.GetValue();
				string empty = string.Empty;
				string calledParty = UcmaDiversionInfo.GetCalledParty(destination.Uri, out empty);
				RedirectReason reason = UcmaDiversionInfo.FindDiversionReason(destination.Reason);
				infoList.Add(new PlatformDiversionInfo(value, calledParty, empty, reason, source));
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000D210 File Offset: 0x0000B410
		private static string GetCalledParty(string uri, out string userAtHost)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, null, "GetCalledParty {0}?", new object[]
			{
				uri
			});
			TelUri telUri = null;
			SipUriParser sipUriParser = null;
			string text = null;
			string text2 = null;
			userAtHost = string.Empty;
			if (!string.IsNullOrEmpty(uri))
			{
				if (TelUri.TryParse(uri, 1, ref telUri, ref text2))
				{
					text = telUri.CleanedNumber;
				}
				else if (SipUriParser.TryParse(uri, ref sipUriParser))
				{
					text = sipUriParser.User;
					userAtHost = sipUriParser.UserAtHost;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new MessageParsingException();
			}
			return UcmaDiversionInfo.GetExtensionFromUserPart(text);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000D298 File Offset: 0x0000B498
		private static string GetExtensionFromUserPart(string userPart)
		{
			string text = string.Empty;
			text = userPart;
			if (text.IndexOf(';') != -1)
			{
				string[] array = text.Split(new char[]
				{
					';'
				});
				text = array[0];
			}
			if (text.IndexOf(':') != -1)
			{
				string[] array2 = text.Split(new char[]
				{
					':'
				});
				text = array2[0];
			}
			return text;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000D2F7 File Offset: 0x0000B4F7
		private static DiversionSource GetDiversionSource(DiversionContext context)
		{
			if (context.Source != 1)
			{
				return DiversionSource.Diversion;
			}
			return DiversionSource.HistoryInfo;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000D308 File Offset: 0x0000B508
		private static RedirectReason FindDiversionReason(string reason)
		{
			if (string.IsNullOrEmpty(reason))
			{
				return RedirectReason.None;
			}
			reason = reason.ToLowerInvariant();
			if (reason.Contains("user-busy"))
			{
				return RedirectReason.UserBusy;
			}
			if (reason.Contains("no-answer"))
			{
				return RedirectReason.NoAnswer;
			}
			if (reason.Contains("unconditional"))
			{
				return RedirectReason.Unconditional;
			}
			if (reason.Contains("deflection"))
			{
				return RedirectReason.Deflection;
			}
			if (reason.Contains("unavailable"))
			{
				return RedirectReason.Unavailable;
			}
			return RedirectReason.Other;
		}

		// Token: 0x040000FE RID: 254
		private const string DiversionReasonName = "reason";

		// Token: 0x040000FF RID: 255
		private const string DiversionReasonUserBusy = "user-busy";

		// Token: 0x04000100 RID: 256
		private const string DiversionReasonNoAnswer = "no-answer";

		// Token: 0x04000101 RID: 257
		private const string DiversionReasonUnconditional = "unconditional";

		// Token: 0x04000102 RID: 258
		private const string DiversionReasonDeflection = "deflection";

		// Token: 0x04000103 RID: 259
		private const string DiversionReasonUnavailable = "unavailable";
	}
}
