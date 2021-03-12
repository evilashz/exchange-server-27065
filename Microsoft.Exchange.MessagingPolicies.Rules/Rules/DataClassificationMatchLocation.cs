using System;
using System.Collections.Generic;
using Microsoft.Filtering;
using Microsoft.Filtering.Results;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000011 RID: 17
	public class DataClassificationMatchLocation
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004238 File Offset: 0x00002438
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00004240 File Offset: 0x00002440
		public int Offset { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004249 File Offset: 0x00002449
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004251 File Offset: 0x00002451
		public int Length { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000425A File Offset: 0x0000245A
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00004262 File Offset: 0x00002462
		public DataClassificationSourceInfo MatchingSourceInfo { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000426B File Offset: 0x0000246B
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00004273 File Offset: 0x00002473
		public List<DataClassificationMatchLocation> SecondaryLocations { get; set; }

		// Token: 0x06000098 RID: 152 RVA: 0x0000427C File Offset: 0x0000247C
		private DataClassificationMatchLocation()
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004284 File Offset: 0x00002484
		public DataClassificationMatchLocation(int offset, int length, DataClassificationSourceInfo matchingSourceInfo)
		{
			if (matchingSourceInfo == null)
			{
				throw new ArgumentNullException("matchingSourceInfo");
			}
			this.Offset = offset;
			this.Length = length;
			this.MatchingSourceInfo = matchingSourceInfo;
			this.SecondaryLocations = new List<DataClassificationMatchLocation>();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000042BC File Offset: 0x000024BC
		internal Tuple<string, string> GetMatchData(MailMessage message, int matchSurroundLength)
		{
			StreamContent subjectPrependedStreamContent = RuleAgentResultUtils.GetSubjectPrependedStreamContent(message.GetUnifiedContentResults().Streams[this.MatchingSourceInfo.SourceId]);
			long num = (long)(this.Offset - matchSurroundLength);
			int startIndex = matchSurroundLength;
			long num2 = (long)matchSurroundLength;
			if (this.Offset - matchSurroundLength < 0)
			{
				num = 0L;
				num2 = (long)this.Offset;
				startIndex = this.Offset;
			}
			num2 += (long)(this.Length + matchSurroundLength);
			string text = subjectPrependedStreamContent.ReadTextChunk(num, (int)num2);
			return new Tuple<string, string>(text.Substring(startIndex, this.Length), text);
		}

		// Token: 0x040000BC RID: 188
		private const string ToStringFormat = "Name:{0}/Id:{1}.";
	}
}
