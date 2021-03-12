using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001F8 RID: 504
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Pop3Response
	{
		// Token: 0x060010B0 RID: 4272 RVA: 0x00036495 File Offset: 0x00034695
		internal Pop3Response(string responseLine)
		{
			SyncUtilities.ThrowIfArgumentNull("responseLine", responseLine);
			this.headline = responseLine;
			this.type = this.ParseResponseType();
			this.extendedResponse = this.ParseExtendedResponse();
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x000364C7 File Offset: 0x000346C7
		internal Pop3ResponseType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x000364CF File Offset: 0x000346CF
		internal Stream MessageStream
		{
			get
			{
				if (this.messageStream == null)
				{
					this.messageStream = TemporaryStorage.Create();
				}
				return this.messageStream;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x000364EA File Offset: 0x000346EA
		internal string Headline
		{
			get
			{
				return this.headline;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x000364F2 File Offset: 0x000346F2
		internal int ListingCount
		{
			get
			{
				if (this.listings == null)
				{
					return 0;
				}
				return this.listings.Count;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (set) Token: 0x060010B5 RID: 4277 RVA: 0x00036509 File Offset: 0x00034709
		internal int ListingCapacity
		{
			set
			{
				if (this.listings == null)
				{
					this.listings = new List<string>(value);
					return;
				}
				this.listings.Capacity = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0003652C File Offset: 0x0003472C
		internal bool HasPermanentError
		{
			get
			{
				return this.HasExtendedResponseCode("[SYS/PERM]");
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00036539 File Offset: 0x00034739
		internal bool HasInUseAuthenticationError
		{
			get
			{
				return this.HasExtendedResponseCode("[IN-USE]");
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00036546 File Offset: 0x00034746
		internal bool HasSystemTemporaryError
		{
			get
			{
				return this.HasExtendedResponseCode("[SYS/TEMP]");
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x00036553 File Offset: 0x00034753
		internal bool HasLogInDelayAuthenticationError
		{
			get
			{
				return this.HasExtendedResponseCode("[LOGIN-DELAY]");
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x00036560 File Offset: 0x00034760
		internal bool HasExtendedResponse
		{
			get
			{
				return this.extendedResponse != null;
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00036570 File Offset: 0x00034770
		public override string ToString()
		{
			if (this.listings != null)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} and listings (count = {1})", new object[]
				{
					this.headline,
					this.listings.Count
				});
			}
			return this.headline;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x000365BF File Offset: 0x000347BF
		internal void AppendListing(string responseLine)
		{
			if (this.listings == null)
			{
				this.listings = new List<string>(1);
			}
			this.listings.Add(responseLine);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000365E1 File Offset: 0x000347E1
		internal ExDateTime ParseReceivedDate(bool useSentTime)
		{
			return SyncUtilities.GetReceivedDate(this.messageStream, useSentTime);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000365F0 File Offset: 0x000347F0
		internal Exception TryParseSTATResponse(out int emailDropCount)
		{
			emailDropCount = -1;
			string[] array = this.headline.Split(Pop3Response.wordDelimiters, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length < 3)
			{
				return new FormatException("'drop listing' must have at least 3 words");
			}
			if (!int.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out emailDropCount))
			{
				return new FormatException("'emailDropCount' must be a number.");
			}
			if (emailDropCount < 0)
			{
				return new FormatException("'emailDropCount' must be non-negative.");
			}
			return null;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00036650 File Offset: 0x00034850
		internal Exception TryParseUIDLResponse(Pop3ResultData pop3ResultData)
		{
			if (this.listings == null || this.listings.Count != pop3ResultData.EmailDropCount)
			{
				return new FormatException("'uidl listing' must match the number of emails.");
			}
			pop3ResultData.AllocateUniqueIds();
			List<int> list = null;
			for (int i = 0; i < this.listings.Count; i++)
			{
				string[] array = this.listings[i].Split(Pop3Response.wordDelimiters, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length < 2)
				{
					return new FormatException("'uidl listing' must have at least 2 words");
				}
				int num;
				if (!int.TryParse(array[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					return new FormatException("'id' must be a number.");
				}
				if (num <= 0)
				{
					return new FormatException("'id' must be greater than zero.");
				}
				if (num <= pop3ResultData.EmailDropCount)
				{
					string text = array[1];
					if (!string.IsNullOrEmpty(text))
					{
						if (!pop3ResultData.HasUniqueId(num))
						{
							pop3ResultData.SetUniqueId(num, text);
						}
						else
						{
							if (list == null)
							{
								list = new List<int>();
							}
							list.Add(num);
						}
					}
				}
			}
			if (list != null)
			{
				foreach (int id in list)
				{
					pop3ResultData.SetUniqueId(id, null);
				}
			}
			return null;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00036780 File Offset: 0x00034980
		internal Exception TryParseLISTResponse(Pop3ResultData pop3ResultData)
		{
			if (this.listings == null || this.listings.Count != pop3ResultData.EmailDropCount)
			{
				return new FormatException("'scan listing' must match the number of emails.");
			}
			pop3ResultData.AllocateEmailSizes();
			for (int i = 0; i < this.listings.Count; i++)
			{
				string[] array = this.listings[i].Split(Pop3Response.wordDelimiters, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length < 2)
				{
					return new FormatException("'scan listing' must have at least 2 words");
				}
				int num;
				if (!int.TryParse(array[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					return new FormatException("'id' must be a number.");
				}
				long num2;
				if (!long.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
				{
					return new FormatException("'size' must be a number.");
				}
				if (num <= 0)
				{
					return new FormatException("'id' must be greater than zero.");
				}
				if (num2 < 0L)
				{
					return new FormatException("'size' must be non-negative.");
				}
				if (num <= pop3ResultData.EmailDropCount)
				{
					pop3ResultData.SetEmailSize(num, num2);
				}
			}
			return null;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0003686C File Offset: 0x00034A6C
		internal Exception TryParseCapaResponse(out bool uniqueIdSupport, out int? retentionDays)
		{
			uniqueIdSupport = false;
			retentionDays = null;
			int i = 0;
			while (i < this.listings.Count)
			{
				bool flag = this.listings[i].StartsWith("UIDL", StringComparison.OrdinalIgnoreCase);
				bool flag2 = this.listings[i].StartsWith("EXPIRE ", StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					uniqueIdSupport = true;
					goto IL_4F;
				}
				if (flag2)
				{
					goto IL_4F;
				}
				IL_E1:
				i++;
				continue;
				IL_4F:
				string[] array = this.listings[i].Split(Pop3Response.wordDelimiters, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length < 2)
				{
					goto IL_E1;
				}
				if (string.CompareOrdinal(array[1], "NEVER") == 0)
				{
					retentionDays = new int?(int.MaxValue);
					goto IL_E1;
				}
				int value;
				if (!int.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
				{
					return new FormatException("'retentionDays' must be a number.");
				}
				if (retentionDays < 0)
				{
					return new FormatException("'retentionDays' must be non-negative.");
				}
				retentionDays = new int?(value);
				goto IL_E1;
			}
			return null;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00036970 File Offset: 0x00034B70
		private Pop3ResponseType ParseResponseType()
		{
			if (this.headline.StartsWith("+OK", StringComparison.Ordinal))
			{
				return Pop3ResponseType.ok;
			}
			if (this.headline.StartsWith("-ERR", StringComparison.Ordinal))
			{
				return Pop3ResponseType.err;
			}
			if (this.headline.StartsWith("+ ", StringComparison.Ordinal))
			{
				return Pop3ResponseType.sendMore;
			}
			return Pop3ResponseType.unknown;
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000369BD File Offset: 0x00034BBD
		private bool HasExtendedResponseCode(string extendedResponseCode)
		{
			return this.HasExtendedResponse && this.extendedResponse.Equals(extendedResponseCode, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000369D8 File Offset: 0x00034BD8
		private string ParseExtendedResponse()
		{
			if (this.type != Pop3ResponseType.err)
			{
				return null;
			}
			string[] array = this.headline.Split(Pop3Response.wordDelimiters, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length < 2)
			{
				return null;
			}
			string text = array[1];
			if (!text.StartsWith("["))
			{
				return null;
			}
			int num = text.IndexOf(']');
			if (num == -1)
			{
				return null;
			}
			return text.Substring(0, num + 1);
		}

		// Token: 0x04000967 RID: 2407
		private const string OK = "+OK";

		// Token: 0x04000968 RID: 2408
		private const string ERR = "-ERR";

		// Token: 0x04000969 RID: 2409
		private const string SystemPermanentError = "[SYS/PERM]";

		// Token: 0x0400096A RID: 2410
		private const string SystemTemporaryError = "[SYS/TEMP]";

		// Token: 0x0400096B RID: 2411
		private const string LogInDelayTemporaryError = "[LOGIN-DELAY]";

		// Token: 0x0400096C RID: 2412
		private const string InUseTemporaryError = "[IN-USE]";

		// Token: 0x0400096D RID: 2413
		private const string SendMore = "+ ";

		// Token: 0x0400096E RID: 2414
		private const string Uidl = "UIDL";

		// Token: 0x0400096F RID: 2415
		private const string Expire = "EXPIRE ";

		// Token: 0x04000970 RID: 2416
		private const string Never = "NEVER";

		// Token: 0x04000971 RID: 2417
		private static readonly char[] wordDelimiters = new char[]
		{
			' ',
			'\t'
		};

		// Token: 0x04000972 RID: 2418
		private string headline;

		// Token: 0x04000973 RID: 2419
		private string extendedResponse;

		// Token: 0x04000974 RID: 2420
		private List<string> listings;

		// Token: 0x04000975 RID: 2421
		private Stream messageStream;

		// Token: 0x04000976 RID: 2422
		private Pop3ResponseType type;
	}
}
