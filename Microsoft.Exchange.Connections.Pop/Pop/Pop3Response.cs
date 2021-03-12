using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Pop3Response
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000056E1 File Offset: 0x000038E1
		internal Pop3Response(string responseLine)
		{
			this.headline = responseLine;
			this.type = this.ParseResponseType();
			this.extendedResponse = this.ParseExtendedResponse();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005708 File Offset: 0x00003908
		internal Pop3ResponseType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005710 File Offset: 0x00003910
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000572B File Offset: 0x0000392B
		internal string Headline
		{
			get
			{
				return this.headline;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005733 File Offset: 0x00003933
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

		// Token: 0x17000033 RID: 51
		// (set) Token: 0x060000BB RID: 187 RVA: 0x0000574A File Offset: 0x0000394A
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000576D File Offset: 0x0000396D
		internal bool HasPermanentError
		{
			get
			{
				return this.HasExtendedResponseCode("[SYS/PERM]");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000577A File Offset: 0x0000397A
		internal bool HasInUseAuthenticationError
		{
			get
			{
				return this.HasExtendedResponseCode("[IN-USE]");
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005787 File Offset: 0x00003987
		internal bool HasSystemTemporaryError
		{
			get
			{
				return this.HasExtendedResponseCode("[SYS/TEMP]");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005794 File Offset: 0x00003994
		internal bool HasLogInDelayAuthenticationError
		{
			get
			{
				return this.HasExtendedResponseCode("[LOGIN-DELAY]");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000057A1 File Offset: 0x000039A1
		internal bool HasExtendedResponse
		{
			get
			{
				return this.extendedResponse != null;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000057B0 File Offset: 0x000039B0
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

		// Token: 0x060000C2 RID: 194 RVA: 0x000057FF File Offset: 0x000039FF
		internal void AppendListing(string responseLine)
		{
			if (this.listings == null)
			{
				this.listings = new List<string>(1);
			}
			this.listings.Add(responseLine);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005821 File Offset: 0x00003A21
		internal ExDateTime ParseReceivedDate(bool useSentTime)
		{
			return this.GetReceivedDate(this.messageStream, useSentTime);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005830 File Offset: 0x00003A30
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

		// Token: 0x060000C5 RID: 197 RVA: 0x00005890 File Offset: 0x00003A90
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

		// Token: 0x060000C6 RID: 198 RVA: 0x000059C0 File Offset: 0x00003BC0
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

		// Token: 0x060000C7 RID: 199 RVA: 0x00005AAC File Offset: 0x00003CAC
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

		// Token: 0x060000C8 RID: 200 RVA: 0x00005BB0 File Offset: 0x00003DB0
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

		// Token: 0x060000C9 RID: 201 RVA: 0x00005BFD File Offset: 0x00003DFD
		private bool HasExtendedResponseCode(string extendedResponseCode)
		{
			return this.HasExtendedResponse && this.extendedResponse.Equals(extendedResponseCode, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005C18 File Offset: 0x00003E18
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

		// Token: 0x060000CB RID: 203 RVA: 0x00005C78 File Offset: 0x00003E78
		private ExDateTime GetReceivedDate(Stream mimeStream, bool useSentTime)
		{
			ExDateTime? exDateTime = null;
			try
			{
				using (MimeReader mimeReader = new MimeReader(new SuppressCloseStream(mimeStream)))
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
									DateTime dateTime = this.ToDateTime(receivedHeader.Date);
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

		// Token: 0x060000CC RID: 204 RVA: 0x00005D9C File Offset: 0x00003F9C
		private DateTime ToDateTime(string dateTimeString)
		{
			return new DateHeader("<empty>", DateTime.UtcNow)
			{
				Value = dateTimeString
			}.UtcDateTime;
		}

		// Token: 0x04000096 RID: 150
		private const string OK = "+OK";

		// Token: 0x04000097 RID: 151
		private const string ERR = "-ERR";

		// Token: 0x04000098 RID: 152
		private const string SystemPermanentError = "[SYS/PERM]";

		// Token: 0x04000099 RID: 153
		private const string SystemTemporaryError = "[SYS/TEMP]";

		// Token: 0x0400009A RID: 154
		private const string LogInDelayTemporaryError = "[LOGIN-DELAY]";

		// Token: 0x0400009B RID: 155
		private const string InUseTemporaryError = "[IN-USE]";

		// Token: 0x0400009C RID: 156
		private const string SendMore = "+ ";

		// Token: 0x0400009D RID: 157
		private const string Uidl = "UIDL";

		// Token: 0x0400009E RID: 158
		private const string Expire = "EXPIRE ";

		// Token: 0x0400009F RID: 159
		private const string Never = "NEVER";

		// Token: 0x040000A0 RID: 160
		private const string EmptyDateHeader = "<empty>";

		// Token: 0x040000A1 RID: 161
		private static readonly char[] wordDelimiters = new char[]
		{
			' ',
			'\t'
		};

		// Token: 0x040000A2 RID: 162
		private readonly string headline;

		// Token: 0x040000A3 RID: 163
		private readonly string extendedResponse;

		// Token: 0x040000A4 RID: 164
		private List<string> listings;

		// Token: 0x040000A5 RID: 165
		private Stream messageStream;

		// Token: 0x040000A6 RID: 166
		private Pop3ResponseType type;
	}
}
