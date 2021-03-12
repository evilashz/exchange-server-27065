using System;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000E7 RID: 231
	internal static class TnefCommon
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x000315E8 File Offset: 0x0002F7E8
		public static bool BytesEqualToPattern(byte[] buffer, int offset, string pattern)
		{
			int length = pattern.Length;
			for (int i = 0; i < pattern.Length; i++)
			{
				if ((char)buffer[offset + i] != pattern[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00031620 File Offset: 0x0002F820
		public static bool BytesEqualToPattern(byte[] buffer, int offset, byte[] pattern)
		{
			for (int i = 0; i < pattern.Length; i++)
			{
				if (buffer[offset + i] != pattern[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00031648 File Offset: 0x0002F848
		public static int StrZLength(byte[] buffer, int offset, int maxEndOffset)
		{
			int num = offset;
			while (num < maxEndOffset && buffer[num] != 0)
			{
				num++;
			}
			return num - offset;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00031669 File Offset: 0x0002F869
		public static bool IsUnicodeCodepage(int messageCodePage)
		{
			return messageCodePage == 1200 || messageCodePage == 1201 || messageCodePage == 12000 || messageCodePage == 12001 || messageCodePage == 65005 || messageCodePage == 65006;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000316E0 File Offset: 0x0002F8E0
		// Note: this type is marked as 'beforefieldinit'.
		static TnefCommon()
		{
			byte[] padding = new byte[4];
			TnefCommon.Padding = padding;
			TnefCommon.OidOle1Storage = new byte[]
			{
				42,
				134,
				72,
				134,
				247,
				20,
				3,
				10,
				3,
				1,
				1
			};
			TnefCommon.OidMacBinary = new byte[]
			{
				42,
				134,
				72,
				134,
				247,
				20,
				3,
				11,
				1
			};
			TnefCommon.MuidOOP = new byte[]
			{
				129,
				43,
				31,
				164,
				190,
				163,
				16,
				25,
				157,
				110,
				0,
				221,
				1,
				15,
				84,
				2
			};
			TnefCommon.oleGuid = new byte[]
			{
				192,
				0,
				0,
				0,
				0,
				0,
				0,
				70
			};
			TnefCommon.MessageIID = new Guid(131847, 0, 0, TnefCommon.oleGuid);
			TnefCommon.MessageClassLegacyPrefix = "Microsoft Mail v3.0 ";
			TnefCommon.MessageClassMappingTable = new TnefCommon.MessageClassMapping[]
			{
				new TnefCommon.MessageClassMapping("IPM.Microsoft Mail.Note", "IPM.Note", false, false),
				new TnefCommon.MessageClassMapping("IPM.Microsoft Mail.Note", "IPM", false, false),
				new TnefCommon.MessageClassMapping("IPM.Microsoft Mail.Read Receipt", "Report.IPM.Note.IPNRN", false, false),
				new TnefCommon.MessageClassMapping("IPM.Microsoft Mail.Non-Delivery", "Report.IPM.Note.NDR", false, false),
				new TnefCommon.MessageClassMapping("IPM.Microsoft Schedule.MtgRespP", "IPM.Schedule.Meeting.Resp.Pos", true, true),
				new TnefCommon.MessageClassMapping("IPM.Microsoft Schedule.MtgRespN", "IPM.Schedule.Meeting.Resp.Neg", true, true),
				new TnefCommon.MessageClassMapping("IPM.Microsoft Schedule.MtgRespA", "IPM.Schedule.Meeting.Resp.Tent", true, true),
				new TnefCommon.MessageClassMapping("IPM.Microsoft Schedule.MtgReq", "IPM.Schedule.Meeting.Request", true, false),
				new TnefCommon.MessageClassMapping("IPM.Microsoft Schedule.MtgCncl", "IPM.Schedule.Meeting.Canceled", true, false)
			};
		}

		// Token: 0x040007FF RID: 2047
		public const int TnefSignature = 574529400;

		// Token: 0x04000800 RID: 2048
		public const int MaxTnefVersion = 65536;

		// Token: 0x04000801 RID: 2049
		public const int AttributeHeaderLength = 9;

		// Token: 0x04000802 RID: 2050
		public const int CheckSumLength = 2;

		// Token: 0x04000803 RID: 2051
		public const int StringNameKind = 1;

		// Token: 0x04000804 RID: 2052
		public const int MaxNestingDepth = 100;

		// Token: 0x04000805 RID: 2053
		public static readonly byte[] HexDigit = new byte[]
		{
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			65,
			66,
			67,
			68,
			69,
			70
		};

		// Token: 0x04000806 RID: 2054
		public static readonly byte[] Padding;

		// Token: 0x04000807 RID: 2055
		public static readonly byte[] OidOle1Storage;

		// Token: 0x04000808 RID: 2056
		public static readonly byte[] OidMacBinary;

		// Token: 0x04000809 RID: 2057
		public static readonly byte[] MuidOOP;

		// Token: 0x0400080A RID: 2058
		private static readonly byte[] oleGuid;

		// Token: 0x0400080B RID: 2059
		public static readonly Guid MessageIID;

		// Token: 0x0400080C RID: 2060
		public static readonly string MessageClassLegacyPrefix;

		// Token: 0x0400080D RID: 2061
		public static readonly TnefCommon.MessageClassMapping[] MessageClassMappingTable;

		// Token: 0x020000E8 RID: 232
		public struct MessageClassMapping
		{
			// Token: 0x06000944 RID: 2372 RVA: 0x000318A3 File Offset: 0x0002FAA3
			public MessageClassMapping(string legacyName, string mapiName, bool splus, bool splusResponse)
			{
				this.LegacyName = legacyName;
				this.MapiName = mapiName;
				this.Splus = splus;
				this.SplusResponse = splusResponse;
			}

			// Token: 0x0400080E RID: 2062
			public string LegacyName;

			// Token: 0x0400080F RID: 2063
			public string MapiName;

			// Token: 0x04000810 RID: 2064
			public bool Splus;

			// Token: 0x04000811 RID: 2065
			public bool SplusResponse;
		}
	}
}
