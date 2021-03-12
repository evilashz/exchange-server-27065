using System;
using System.Net;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200002D RID: 45
	internal class ApnsFeedbackResponse
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00007390 File Offset: 0x00005590
		private ApnsFeedbackResponse(string token, ExDateTime timestamp)
		{
			this.Token = token;
			this.TimeStamp = timestamp;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000073A6 File Offset: 0x000055A6
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000073AE File Offset: 0x000055AE
		public ExDateTime TimeStamp { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000073B7 File Offset: 0x000055B7
		// (set) Token: 0x060001CE RID: 462 RVA: 0x000073BF File Offset: 0x000055BF
		public string Token { get; private set; }

		// Token: 0x060001CF RID: 463 RVA: 0x000073D4 File Offset: 0x000055D4
		public static ApnsFeedbackResponse FromApnsFormat(byte[] binaryForm)
		{
			ArgumentValidator.ThrowIfNull("binaryForm", binaryForm);
			ArgumentValidator.ThrowIfInvalidValue<byte[]>("binaryForm", binaryForm, (byte[] x) => x.Length == 38);
			ExDateTime timestamp = ApnsFeedbackResponse.ExtractTimestamp(binaryForm);
			IPAddress.NetworkToHostOrder(BitConverter.ToInt16(binaryForm, 4));
			string token = ApnsFeedbackResponse.ExtractToken(binaryForm);
			return new ApnsFeedbackResponse(token, timestamp);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007438 File Offset: 0x00005638
		public static ApnsFeedbackResponse FromFeedbackFileEntry(string feedbackFileLine)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("feedbackFileLine", feedbackFileLine);
			ExDateTime timestamp = default(ExDateTime);
			Exception ex = null;
			string[] array = feedbackFileLine.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 2 && array[0].Length == 64)
			{
				try
				{
					HexConverter.HexStringToByteArray(array[0]);
					timestamp = ExDateTime.Parse(ExTimeZone.UtcTimeZone, array[1]);
					return new ApnsFeedbackResponse(array[0], timestamp);
				}
				catch (FormatException ex2)
				{
					ex = ex2;
				}
			}
			throw new ApnsFeedbackException(Strings.ApnsFeedbackResponseInvalidFileLine(feedbackFileLine, (ex == null) ? string.Empty : ex.Message), ex);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000074E0 File Offset: 0x000056E0
		public override string ToString()
		{
			if (this.toStringCache == null)
			{
				this.toStringCache = string.Format("{{token:{0}; timestamp:{1}}}", this.Token, this.TimeStamp);
			}
			return this.toStringCache;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007514 File Offset: 0x00005714
		private static ExDateTime ExtractTimestamp(byte[] binaryForm)
		{
			int num = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(binaryForm, 0));
			return Constants.EpochBaseTime.AddSeconds((double)num);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000753D File Offset: 0x0000573D
		private static string ExtractToken(byte[] binaryForm)
		{
			return HexConverter.ByteArrayToHexString(binaryForm, 6, 32);
		}

		// Token: 0x040000AC RID: 172
		public const int Length = 38;

		// Token: 0x040000AD RID: 173
		private const int TimestampOffset = 0;

		// Token: 0x040000AE RID: 174
		private const int TokenLengthOffset = 4;

		// Token: 0x040000AF RID: 175
		private const int TokenOffset = 6;

		// Token: 0x040000B0 RID: 176
		private const int StringTokenSize = 64;

		// Token: 0x040000B1 RID: 177
		private string toStringCache;
	}
}
