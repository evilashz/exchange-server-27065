using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200019A RID: 410
	internal class RedirectionHistory
	{
		// Token: 0x060011E0 RID: 4576 RVA: 0x00048F58 File Offset: 0x00047158
		public static byte[] GenerateRedirectionHistory(string originalAddress, RedirectionHistoryReason type, DateTime time)
		{
			byte[] result;
			try
			{
				using (RedirectionHistory.RedirectionHistoryElement redirectionHistoryElement = new RedirectionHistory.RedirectionHistoryElement(originalAddress, type, time))
				{
					byte[] bytes = redirectionHistoryElement.GetBytes();
					RedirectionHistory.diag.TraceDebug<int>(0L, "RedirectionHistory Element structure has {0} bytes", bytes.Length);
					using (RedirectionHistory.FlatEntry flatEntry = new RedirectionHistory.FlatEntry(bytes))
					{
						byte[] bytes2 = flatEntry.GetBytes();
						using (RedirectionHistory.FlatEntryList flatEntryList = new RedirectionHistory.FlatEntryList(bytes2))
						{
							result = flatEntryList.GetBytes();
						}
					}
				}
			}
			catch (IOException arg)
			{
				RedirectionHistory.diag.TraceError<IOException>(0L, "Failed to generate redirection history from ORCPT due to exception {0}", arg);
				result = null;
			}
			return result;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004901C File Offset: 0x0004721C
		public static byte[] GenerateRedirectionHistoryFromOrcpt(string orcpt)
		{
			int num = orcpt.IndexOf(';');
			if (num == -1 || num == orcpt.Length - 1)
			{
				RedirectionHistory.diag.TraceError<string>(0L, "Failed to generate redirection history from ORCPT due to invalid ORCPT {0}", orcpt);
				return null;
			}
			return RedirectionHistory.GenerateRedirectionHistory(orcpt.Substring(num + 1), RedirectionHistoryReason.Rsar, DateTime.UtcNow);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0004906C File Offset: 0x0004726C
		public static bool TryDecodeRedirectionHistory(byte[] redirectionHistoryBlob, out string address, out RedirectionHistoryReason type, out DateTime time)
		{
			address = string.Empty;
			type = RedirectionHistoryReason.Rsar;
			time = DateTime.MinValue;
			if (redirectionHistoryBlob == null || redirectionHistoryBlob.Length < 28)
			{
				RedirectionHistory.diag.TraceError(0L, "Failed to decode redirection history blob, redirectionHistory is not valid");
				return false;
			}
			if (BitConverter.ToUInt32(redirectionHistoryBlob, 0) != 1U)
			{
				RedirectionHistory.diag.TraceError(0L, "Failed to decode redirection history blob, the number of FlatEntry is not 1");
				return false;
			}
			if ((ulong)BitConverter.ToUInt32(redirectionHistoryBlob, 4) != (ulong)((long)(redirectionHistoryBlob.Length - 8)))
			{
				RedirectionHistory.diag.TraceError(0L, "Failed to decode redirection history blob, the length of FlatEntry is not correct");
				return false;
			}
			if ((ulong)BitConverter.ToUInt32(redirectionHistoryBlob, 8) != (ulong)((long)(redirectionHistoryBlob.Length - 12)))
			{
				RedirectionHistory.diag.TraceError(0L, "Failed to decode redirection history blob, the length of RedirectionHistoryElement is not correct");
				return false;
			}
			switch (BitConverter.ToUInt32(redirectionHistoryBlob, 12))
			{
			case 0U:
				type = RedirectionHistoryReason.Rsar;
				break;
			case 1U:
				type = RedirectionHistoryReason.Orar;
				break;
			default:
				RedirectionHistory.diag.TraceError(0L, "Failed to decode redirection history blob, Unknow redirection type");
				return false;
			}
			try
			{
				time = DateTime.FromFileTimeUtc(BitConverter.ToInt64(redirectionHistoryBlob, 16));
				string @string = Encoding.ASCII.GetString(redirectionHistoryBlob, 24, redirectionHistoryBlob.Length - 24);
				string text = @string;
				char[] trimChars = new char[1];
				address = text.TrimEnd(trimChars);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				RedirectionHistory.diag.TraceError<string>(0L, "Failed to decode redirection history blob due to exception {0}", ex.Message);
				return false;
			}
			return true;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000491B0 File Offset: 0x000473B0
		public static void SetRedirectionHistoryOnRecipient(MailRecipient recipient, string originalAddressString)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(recipient.ORcpt))
			{
				int num = recipient.ORcpt.IndexOf(';');
				if (num != -1 && num != recipient.ORcpt.Length - 1)
				{
					return;
				}
			}
			if (!flag)
			{
				RedirectionHistory.SetORcpt(recipient, originalAddressString);
			}
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x000491FC File Offset: 0x000473FC
		public static void SetORcpt(MailRecipient recipient, string originalAddressString)
		{
			ProxyAddress proxyAddress;
			if (!SmtpProxyAddress.TryDeencapsulate(originalAddressString, out proxyAddress))
			{
				recipient.ORcpt = "rfc822;" + originalAddressString;
				return;
			}
			if (proxyAddress.Prefix == ProxyAddressPrefix.X400)
			{
				recipient.ORcpt = "x400;" + proxyAddress.AddressString;
				return;
			}
			recipient.ORcpt = "rfc822;" + originalAddressString;
		}

		// Token: 0x04000975 RID: 2421
		private const int MiniumFlatEntryListLength = 28;

		// Token: 0x04000976 RID: 2422
		private const int FlatEntryListLengthOffset = 4;

		// Token: 0x04000977 RID: 2423
		private const int FlatEntryLengthOffset = 8;

		// Token: 0x04000978 RID: 2424
		private const int RedirectionHistoryOffset = 12;

		// Token: 0x04000979 RID: 2425
		private const int RedirectionHistoryTimeOffset = 16;

		// Token: 0x0400097A RID: 2426
		private const int RedirectionHistoryAddressOffset = 24;

		// Token: 0x0400097B RID: 2427
		private const string Rfc822Prefix = "rfc822;";

		// Token: 0x0400097C RID: 2428
		private const string X400Prefix = "x400;";

		// Token: 0x0400097D RID: 2429
		private static readonly Trace diag = ExTraceGlobals.OrarTracer;

		// Token: 0x0200019B RID: 411
		private class Writer : BinaryWriter
		{
			// Token: 0x060011E7 RID: 4583 RVA: 0x00049273 File Offset: 0x00047473
			public Writer() : base(new MemoryStream())
			{
			}

			// Token: 0x060011E8 RID: 4584 RVA: 0x00049280 File Offset: 0x00047480
			public byte[] GetBytes()
			{
				this.Flush();
				return ((MemoryStream)this.BaseStream).ToArray();
			}
		}

		// Token: 0x0200019C RID: 412
		private class FlatEntryList : RedirectionHistory.Writer
		{
			// Token: 0x060011E9 RID: 4585 RVA: 0x00049298 File Offset: 0x00047498
			public FlatEntryList(byte[] abEntries)
			{
				this.Write(1U);
				this.Write((uint)abEntries.Length);
				this.Write(abEntries, 0, abEntries.Length);
			}
		}

		// Token: 0x0200019D RID: 413
		private class FlatEntry : RedirectionHistory.Writer
		{
			// Token: 0x060011EA RID: 4586 RVA: 0x000492BB File Offset: 0x000474BB
			public FlatEntry(byte[] abEntry)
			{
				this.Write((uint)abEntry.Length);
				this.Write(abEntry, 0, abEntry.Length);
			}
		}

		// Token: 0x0200019E RID: 414
		private class RedirectionHistoryElement : RedirectionHistory.Writer
		{
			// Token: 0x060011EB RID: 4587 RVA: 0x000492D8 File Offset: 0x000474D8
			public RedirectionHistoryElement(string originalAddress, RedirectionHistoryReason type, DateTime time)
			{
				this.Write((uint)type);
				this.Write(time.ToFileTimeUtc());
				byte[] bytes = Encoding.ASCII.GetBytes(originalAddress + '\0');
				this.Write(bytes, 0, bytes.Length);
				int num = 4 - bytes.Length % 4;
				if (num != 4)
				{
					this.Write(new byte[num]);
				}
				RedirectionHistory.diag.TraceDebug<int>((long)this.GetHashCode(), "RedirectionHistory {0} bytes padding", num);
			}
		}
	}
}
