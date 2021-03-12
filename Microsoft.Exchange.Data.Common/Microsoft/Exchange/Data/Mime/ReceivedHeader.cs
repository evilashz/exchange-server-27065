using System;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200007F RID: 127
	public class ReceivedHeader : Header
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x0001C233 File Offset: 0x0001A433
		internal ReceivedHeader() : base("Received", HeaderId.Received)
		{
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001C244 File Offset: 0x0001A444
		public ReceivedHeader(string from, string fromTcpInfo, string by, string byTcpInfo, string forMailbox, string with, string id, string via, string date) : base("Received", HeaderId.Received)
		{
			int num = -1;
			int num2 = ByteString.StringToBytesCount(from, true);
			int num3 = ByteString.StringToBytesCount(fromTcpInfo, true);
			int num4 = ByteString.StringToBytesCount(by, true);
			int num5 = ByteString.StringToBytesCount(byTcpInfo, true);
			int num6 = ByteString.StringToBytesCount(forMailbox, true);
			int num7 = ByteString.StringToBytesCount(with, true);
			int num8 = ByteString.StringToBytesCount(id, true);
			int num9 = ByteString.StringToBytesCount(via, true);
			int num10 = ByteString.StringToBytesCount(date, false);
			num += ((from != null) ? (num2 + ReceivedHeader.ParamFrom.Length + 2) : 0);
			num += ((fromTcpInfo != null) ? (num3 + ((from == null) ? (ReceivedHeader.ParamFrom.Length + 1) : 0) + 3) : 0);
			num += ((by != null) ? (num4 + ReceivedHeader.ParamBy.Length + 2) : 0);
			num += ((byTcpInfo != null) ? (num5 + ((by == null) ? (ReceivedHeader.ParamBy.Length + 1) : 0) + 3) : 0);
			num += ((forMailbox != null) ? (num6 + ReceivedHeader.ParamFor.Length + 2) : 0);
			num += ((with != null) ? (num7 + ReceivedHeader.ParamWith.Length + 2) : 0);
			num += ((id != null) ? (num8 + ReceivedHeader.ParamId.Length + 2) : 0);
			num += ((via != null) ? (num9 + ReceivedHeader.ParamVia.Length + 2) : 0);
			num += ((date != null) ? (num10 + ((-1 == num) ? 3 : 2)) : 0);
			if (-1 == num)
			{
				return;
			}
			byte[] array = new byte[num];
			int num11 = 0;
			this.AppendNameValue(ReceivedHeader.ParamFrom, from, array, ref num11);
			if (fromTcpInfo != null)
			{
				if (from == null)
				{
					this.AppendName(ReceivedHeader.ParamFrom, array, ref num11);
				}
				array[num11++] = 32;
				array[num11++] = 40;
				ByteString.StringToBytes(fromTcpInfo, array, num11, true);
				num11 += num3;
				array[num11++] = 41;
			}
			this.AppendNameValue(ReceivedHeader.ParamBy, by, array, ref num11);
			if (byTcpInfo != null)
			{
				if (by == null)
				{
					this.AppendName(ReceivedHeader.ParamBy, array, ref num11);
				}
				array[num11++] = 32;
				array[num11++] = 40;
				ByteString.StringToBytes(byTcpInfo, array, num11, true);
				num11 += num5;
				array[num11++] = 41;
			}
			this.AppendNameValue(ReceivedHeader.ParamFor, forMailbox, array, ref num11);
			this.AppendNameValue(ReceivedHeader.ParamWith, with, array, ref num11);
			this.AppendNameValue(ReceivedHeader.ParamId, id, array, ref num11);
			this.AppendNameValue(ReceivedHeader.ParamVia, via, array, ref num11);
			if (date != null)
			{
				ByteString.ValidateStringArgument(date, false);
				array[num11++] = 59;
				array[num11++] = 32;
				ByteString.StringToBytes(date, array, num11, false);
				num11 += num10;
			}
			this.RawValue = array;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001C4CF File Offset: 0x0001A6CF
		public string From
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				return this.fromValue;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0001C4E5 File Offset: 0x0001A6E5
		public string FromTcpInfo
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				return this.fromTcpInfoValue;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001C4FB File Offset: 0x0001A6FB
		public string By
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				return this.byValue;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0001C511 File Offset: 0x0001A711
		public string ByTcpInfo
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				return this.byTcpInfoValue;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001C527 File Offset: 0x0001A727
		public string Via
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				return this.viaValue;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001C53D File Offset: 0x0001A73D
		public string With
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				return this.withValue;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001C553 File Offset: 0x0001A753
		public string Id
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				return this.idValue;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0001C569 File Offset: 0x0001A769
		public string For
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				return this.forValue;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001C57F File Offset: 0x0001A77F
		public string Date
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				return this.dateValue;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0001C595 File Offset: 0x0001A795
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x0001C59E File Offset: 0x0001A79E
		public sealed override string Value
		{
			get
			{
				return base.GetRawValue(true);
			}
			set
			{
				throw new NotSupportedException(Strings.UnicodeMimeHeaderReceivedNotSupported);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001C5AA File Offset: 0x0001A7AA
		public sealed override bool RequiresSMTPUTF8
		{
			get
			{
				return !MimeString.IsPureASCII(base.Lines);
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001C5BA File Offset: 0x0001A7BA
		internal override void RawValueAboutToChange()
		{
			this.parsed = false;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001C5C4 File Offset: 0x0001A7C4
		public sealed override MimeNode Clone()
		{
			ReceivedHeader receivedHeader = new ReceivedHeader();
			this.CopyTo(receivedHeader);
			return receivedHeader;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		public sealed override void CopyTo(object destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destination == this)
			{
				return;
			}
			ReceivedHeader receivedHeader = destination as ReceivedHeader;
			if (receivedHeader == null)
			{
				throw new ArgumentException(Strings.CantCopyToDifferentObjectType);
			}
			base.CopyTo(destination);
			receivedHeader.parsed = this.parsed;
			receivedHeader.fromValue = this.fromValue;
			receivedHeader.fromTcpInfoValue = this.fromTcpInfoValue;
			receivedHeader.byValue = this.byValue;
			receivedHeader.byTcpInfoValue = this.byTcpInfoValue;
			receivedHeader.viaValue = this.viaValue;
			receivedHeader.withValue = this.withValue;
			receivedHeader.idValue = this.idValue;
			receivedHeader.forValue = this.forValue;
			receivedHeader.dateValue = this.dateValue;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001C694 File Offset: 0x0001A894
		public sealed override bool IsValueValid(string value)
		{
			return false;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001C697 File Offset: 0x0001A897
		internal override MimeNode ValidateNewChild(MimeNode newChild, MimeNode refChild)
		{
			throw new NotSupportedException(Strings.ChildrenCannotBeAddedToReceivedHeader);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001C6A4 File Offset: 0x0001A8A4
		private void AppendNameValue(byte[] name, string value, byte[] array, ref int offset)
		{
			if (value != null)
			{
				this.AppendName(name, array, ref offset);
				array[offset++] = 32;
				int num = ByteString.StringToBytes(value, array, offset, true);
				offset += num;
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
		private void AppendName(byte[] name, byte[] array, ref int offset)
		{
			if (offset != 0)
			{
				array[offset++] = 32;
			}
			Buffer.BlockCopy(name, 0, array, offset, name.Length);
			offset += name.Length;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001C714 File Offset: 0x0001A914
		private void Reset()
		{
			this.parsed = false;
			this.fromValue = null;
			this.fromTcpInfoValue = null;
			this.byValue = null;
			this.byTcpInfoValue = null;
			this.viaValue = null;
			this.withValue = null;
			this.idValue = null;
			this.forValue = null;
			this.dateValue = null;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001C768 File Offset: 0x0001A968
		private void Parse()
		{
			this.Reset();
			this.parsed = true;
			DecodingOptions headerDecodingOptions = base.GetHeaderDecodingOptions();
			ValueParser valueParser = new ValueParser(base.Lines, headerDecodingOptions.AllowUTF8);
			MimeStringList mimeStringList = default(MimeStringList);
			MimeStringList mimeStringList2 = default(MimeStringList);
			MimeString mimeString = default(MimeString);
			MimeString mimeString2 = MimeString.Empty;
			ReceivedHeader.ParseState parseState = ReceivedHeader.ParseState.None;
			for (;;)
			{
				valueParser.ParseWhitespace(true, ref mimeStringList);
				byte b = valueParser.ParseGet();
				if (b == 0)
				{
					break;
				}
				if (59 == b)
				{
					parseState = ReceivedHeader.ParseState.Date;
				}
				else if (40 == b && parseState == ReceivedHeader.ParseState.DomainValue)
				{
					parseState = ReceivedHeader.ParseState.DomainAddress;
				}
				else
				{
					valueParser.ParseUnget();
					mimeString = valueParser.ParseToken();
					if (mimeString.Length == 0)
					{
						valueParser.ParseGet();
						mimeStringList2.TakeOverAppend(ref mimeStringList);
						valueParser.ParseAppendLastByte(ref mimeStringList2);
						continue;
					}
					ReceivedHeader.ParseState parseState2 = this.StateFromToken(mimeString);
					if (ReceivedHeader.ParseState.None != parseState2)
					{
						parseState = parseState2;
					}
				}
				switch (parseState)
				{
				case ReceivedHeader.ParseState.Domain:
				case ReceivedHeader.ParseState.OptInfo:
					if (mimeString2.Length != 0)
					{
						this.FinishClause(ref mimeString2, ref mimeStringList2, headerDecodingOptions.AllowUTF8);
					}
					else
					{
						mimeStringList2.Reset();
					}
					mimeString2 = mimeString;
					valueParser.ParseWhitespace(false, ref mimeStringList);
					mimeStringList.Reset();
					parseState++;
					break;
				case ReceivedHeader.ParseState.DomainValue:
					mimeStringList2.TakeOverAppend(ref mimeStringList);
					mimeStringList2.AppendFragment(mimeString);
					break;
				case ReceivedHeader.ParseState.DomainAddress:
				{
					bool flag = mimeString2.CompareEqI(ReceivedHeader.ParamFrom);
					this.FinishClause(ref mimeString2, ref mimeStringList2, headerDecodingOptions.AllowUTF8);
					mimeStringList.Reset();
					valueParser.ParseUnget();
					valueParser.ParseComment(true, false, ref mimeStringList2, true);
					byte[] sz = mimeStringList2.GetSz();
					string text = (sz == null) ? null : ByteString.BytesToString(sz, headerDecodingOptions.AllowUTF8);
					if (flag)
					{
						this.fromTcpInfoValue = text;
					}
					else
					{
						this.byTcpInfoValue = text;
					}
					mimeStringList2.Reset();
					parseState = ReceivedHeader.ParseState.None;
					break;
				}
				case ReceivedHeader.ParseState.OptInfoValue:
					mimeStringList2.TakeOverAppend(ref mimeStringList);
					mimeStringList2.AppendFragment(mimeString);
					break;
				case ReceivedHeader.ParseState.Date:
				{
					this.FinishClause(ref mimeString2, ref mimeStringList2, headerDecodingOptions.AllowUTF8);
					mimeStringList.Reset();
					valueParser.ParseWhitespace(false, ref mimeStringList);
					valueParser.ParseToEnd(ref mimeStringList2);
					byte[] sz2 = mimeStringList2.GetSz();
					this.dateValue = ((sz2 == null) ? null : ByteString.BytesToString(sz2, false));
					break;
				}
				case ReceivedHeader.ParseState.None:
					mimeStringList2.Reset();
					break;
				}
			}
			this.FinishClause(ref mimeString2, ref mimeStringList2, headerDecodingOptions.AllowUTF8);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001C9C2 File Offset: 0x0001ABC2
		internal override void ForceParse()
		{
			if (!this.parsed)
			{
				this.Parse();
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001C9D4 File Offset: 0x0001ABD4
		private void FinishClause(ref MimeString param, ref MimeStringList value, bool allowUTF8)
		{
			if (param.Length != 0)
			{
				byte[] sz = value.GetSz();
				string text = (sz == null) ? null : ByteString.BytesToString(sz, allowUTF8);
				uint num = param.ComputeCrcI();
				if (num <= 2556329580U)
				{
					if (num != 271896810U)
					{
						if (num != 2115158205U)
						{
							if (num == 2556329580U)
							{
								this.fromValue = text;
							}
						}
						else
						{
							this.byValue = text;
						}
					}
					else
					{
						this.forValue = text;
					}
				}
				else if (num != 3117694226U)
				{
					if (num != 3740702146U)
					{
						if (num == 4276123055U)
						{
							this.idValue = text;
						}
					}
					else
					{
						this.viaValue = text;
					}
				}
				else
				{
					this.withValue = text;
				}
				value.Reset();
				param = MimeString.Empty;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001CA8C File Offset: 0x0001AC8C
		private ReceivedHeader.ParseState StateFromToken(MimeString token)
		{
			uint num = token.ComputeCrcI();
			if (num <= 2556329580U)
			{
				if (num == 271896810U)
				{
					return ReceivedHeader.ParseState.OptInfo;
				}
				if (num == 2115158205U)
				{
					return ReceivedHeader.ParseState.Domain;
				}
				if (num == 2556329580U)
				{
					return ReceivedHeader.ParseState.Domain;
				}
			}
			else
			{
				if (num == 3117694226U)
				{
					return ReceivedHeader.ParseState.OptInfo;
				}
				if (num == 3740702146U)
				{
					return ReceivedHeader.ParseState.OptInfo;
				}
				if (num == 4276123055U)
				{
					return ReceivedHeader.ParseState.OptInfo;
				}
			}
			return ReceivedHeader.ParseState.None;
		}

		// Token: 0x040003B0 RID: 944
		internal const bool AllowUTF8Value = true;

		// Token: 0x040003B1 RID: 945
		private const uint ParamFromCRC = 2556329580U;

		// Token: 0x040003B2 RID: 946
		private const uint ParamByCRC = 2115158205U;

		// Token: 0x040003B3 RID: 947
		private const uint ParamViaCRC = 3740702146U;

		// Token: 0x040003B4 RID: 948
		private const uint ParamWithCRC = 3117694226U;

		// Token: 0x040003B5 RID: 949
		private const uint ParamIdCRC = 4276123055U;

		// Token: 0x040003B6 RID: 950
		private const uint ParamForCRC = 271896810U;

		// Token: 0x040003B7 RID: 951
		private static readonly byte[] ParamFrom = ByteString.StringToBytes("from", true);

		// Token: 0x040003B8 RID: 952
		private static readonly byte[] ParamBy = ByteString.StringToBytes("by", true);

		// Token: 0x040003B9 RID: 953
		private static readonly byte[] ParamVia = ByteString.StringToBytes("via", true);

		// Token: 0x040003BA RID: 954
		private static readonly byte[] ParamWith = ByteString.StringToBytes("with", true);

		// Token: 0x040003BB RID: 955
		private static readonly byte[] ParamId = ByteString.StringToBytes("id", true);

		// Token: 0x040003BC RID: 956
		private static readonly byte[] ParamFor = ByteString.StringToBytes("for", true);

		// Token: 0x040003BD RID: 957
		private static readonly byte[] ParamDate = ByteString.StringToBytes("date", true);

		// Token: 0x040003BE RID: 958
		private static readonly byte[] ParamFromTcpInfo = ByteString.StringToBytes("x-from-tcp-info", true);

		// Token: 0x040003BF RID: 959
		private static readonly byte[] ParamByTcpInfo = ByteString.StringToBytes("x-by-tcp-info", true);

		// Token: 0x040003C0 RID: 960
		private string fromValue;

		// Token: 0x040003C1 RID: 961
		private string fromTcpInfoValue;

		// Token: 0x040003C2 RID: 962
		private string byValue;

		// Token: 0x040003C3 RID: 963
		private string byTcpInfoValue;

		// Token: 0x040003C4 RID: 964
		private string viaValue;

		// Token: 0x040003C5 RID: 965
		private string withValue;

		// Token: 0x040003C6 RID: 966
		private string idValue;

		// Token: 0x040003C7 RID: 967
		private string forValue;

		// Token: 0x040003C8 RID: 968
		private string dateValue;

		// Token: 0x040003C9 RID: 969
		private bool parsed;

		// Token: 0x02000080 RID: 128
		private enum ParseState
		{
			// Token: 0x040003CB RID: 971
			Domain,
			// Token: 0x040003CC RID: 972
			DomainValue,
			// Token: 0x040003CD RID: 973
			DomainAddress,
			// Token: 0x040003CE RID: 974
			OptInfo,
			// Token: 0x040003CF RID: 975
			OptInfoValue,
			// Token: 0x040003D0 RID: 976
			Date,
			// Token: 0x040003D1 RID: 977
			None
		}
	}
}
