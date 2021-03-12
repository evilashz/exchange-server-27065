using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AntispamCommon;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.HeaderConversion
{
	// Token: 0x02000012 RID: 18
	internal class SclHeaders
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003D24 File Offset: 0x00001F24
		public SclHeaders(int sclDeleteThreshold, int ehfSpamScoreJunkThreshold, int ehfSpamScoreDeleteTreshold, int partnerSpamScoreJunkThreshold, int partnerSpamScoreDeleteTreshold, IEnumerable<IPRange> hotmailWomsIPRanges)
		{
			this.Initialize(sclDeleteThreshold, ehfSpamScoreJunkThreshold, ehfSpamScoreDeleteTreshold, partnerSpamScoreJunkThreshold, partnerSpamScoreDeleteTreshold, hotmailWomsIPRanges);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003D3B File Offset: 0x00001F3B
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003D43 File Offset: 0x00001F43
		public int SclDeleteThreshold
		{
			get
			{
				return this.sclDeleteThreshold;
			}
			set
			{
				this.sclDeleteThreshold = value;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003D4C File Offset: 0x00001F4C
		public void UpdateScl(HeaderList headers)
		{
			int? num;
			SclHeaders.SpamHeaderConverter spamHeaderConverter;
			this.UpdateScl(headers, null, null, null, out num, out spamHeaderConverter);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003D68 File Offset: 0x00001F68
		public void UpdateScl(HeaderList headers, string agentName, string eventTopic, EndOfHeadersEventArgs eventArgs, out int? sclOut, out SclHeaders.SpamHeaderConverter spamHeaderConverter)
		{
			sclOut = null;
			spamHeaderConverter = null;
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			int arg;
			if (CommonUtils.TryGetValidScl(headers, out arg))
			{
				SclHeaders.Tracer.TraceDebug<int>((long)this.GetHashCode(), "X-MS-Exchange-Organization-SCL header already exists with value '{0}'", arg);
				return;
			}
			foreach (SclHeaders.SpamHeaderConverter spamHeaderConverter2 in this.headerConverters)
			{
				Header header = spamHeaderConverter2.FindSpamHeader(headers);
				if (header != null)
				{
					int? spamScore = spamHeaderConverter2.ExtractSpamScore(header);
					int num = spamHeaderConverter2.ConvertToScl(spamScore, this.sclDeleteThreshold);
					if (!string.IsNullOrEmpty(agentName) && !string.IsNullOrEmpty(eventTopic) && eventArgs != null)
					{
						this.LogStampScl(agentName, eventTopic, eventArgs, num, header);
					}
					SclHeaders.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Writing X-MS-Exchange-Organization-SCL header with a value of '{0}'.", num);
					Header header2 = Header.Create("X-MS-Exchange-Organization-SCL");
					header2.Value = num.ToString(CultureInfo.InvariantCulture);
					headers.AppendChild(header2);
					sclOut = new int?(num);
					spamHeaderConverter = spamHeaderConverter2;
					break;
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003E94 File Offset: 0x00002094
		private static bool IsHeader(Header header, string name)
		{
			return string.Equals(header.Name, name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000409C File Offset: 0x0000229C
		private static IEnumerable<Header> GetCandidateHeaders(HeaderList headers, bool immediatePerimeter)
		{
			bool foundReceivedHeaders = false;
			bool hasMoreHeaders = false;
			using (MimeNode.Enumerator<Header> enumerator = headers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HeaderId != HeaderId.Received)
					{
						hasMoreHeaders = true;
						break;
					}
					foundReceivedHeaders = true;
				}
				if (foundReceivedHeaders && hasMoreHeaders)
				{
					do
					{
						yield return enumerator.Current;
					}
					while (enumerator.MoveNext() && (!immediatePerimeter || enumerator.Current.HeaderId != HeaderId.Received));
				}
			}
			yield break;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000040C0 File Offset: 0x000022C0
		private void Initialize(int sclDeleteThreshold, int ehfSpamScoreJunkThreshold, int ehfSpamScoreDeleteThreshold, int partnerSpamScoreJunkThreshold, int partnerSpamScoreDeleteThreshold, IEnumerable<IPRange> hotmailWomsIPRanges)
		{
			this.sclDeleteThreshold = sclDeleteThreshold;
			this.headerConverters = new List<SclHeaders.SpamHeaderConverter>();
			this.headerConverters.Add(new SclHeaders.HotmailSpamHeaderConverter(hotmailWomsIPRanges));
			this.headerConverters.Add(new SclHeaders.EhfSpamHeaderConverter(ehfSpamScoreJunkThreshold, ehfSpamScoreDeleteThreshold));
			this.headerConverters.Add(new SclHeaders.PartnerSpamHeaderConverter(partnerSpamScoreJunkThreshold, partnerSpamScoreDeleteThreshold));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004118 File Offset: 0x00002318
		private void LogStampScl(string agentName, string eventTopic, EndOfHeadersEventArgs eventArgs, int scl, Header sourceHeader)
		{
			LogEntry logEntry = new LogEntry("SCL", scl.ToString(), sourceHeader.Name + ":" + sourceHeader.Value);
			AgentLog.Instance.LogStampScl(agentName, eventTopic, eventArgs, eventArgs.SmtpSession, eventArgs.MailItem, logEntry);
		}

		// Token: 0x0400004E RID: 78
		private const int MaxHeaderLength = 256;

		// Token: 0x0400004F RID: 79
		private static readonly Trace Tracer = ExTraceGlobals.UtilitiesTracer;

		// Token: 0x04000050 RID: 80
		private int sclDeleteThreshold;

		// Token: 0x04000051 RID: 81
		private List<SclHeaders.SpamHeaderConverter> headerConverters;

		// Token: 0x02000013 RID: 19
		public abstract class SpamHeaderConverter
		{
			// Token: 0x0600007C RID: 124
			public abstract Header FindSpamHeader(HeaderList headers);

			// Token: 0x0600007D RID: 125
			public abstract int? ExtractSpamScore(Header header);

			// Token: 0x0600007E RID: 126
			public abstract int ConvertToScl(int? spamScore, int sclDeleteThreshold);
		}

		// Token: 0x02000014 RID: 20
		public class HotmailSpamHeaderConverter : SclHeaders.SpamHeaderConverter
		{
			// Token: 0x06000080 RID: 128 RVA: 0x0000417D File Offset: 0x0000237D
			public HotmailSpamHeaderConverter(IEnumerable<IPRange> womsIPRanges)
			{
				this.womsIPRanges = new List<IPRange>(womsIPRanges);
			}

			// Token: 0x06000081 RID: 129 RVA: 0x00004194 File Offset: 0x00002394
			public override Header FindSpamHeader(HeaderList headers)
			{
				if (this.IsNdr(headers))
				{
					AsciiTextHeader asciiTextHeader = headers.FindFirst("X-MS-Exchange-Organization-OriginalClientIPAddress") as AsciiTextHeader;
					if (asciiTextHeader != null && this.IsLegitimateNdr(asciiTextHeader))
					{
						return asciiTextHeader;
					}
				}
				return this.FindDeliveryHeader(headers);
			}

			// Token: 0x06000082 RID: 130 RVA: 0x000041D0 File Offset: 0x000023D0
			public override int? ExtractSpamScore(Header header)
			{
				int? result = new int?(-1);
				if (header != null && !string.IsNullOrEmpty(header.Value) && header.Value.Length <= 256)
				{
					if (string.Equals(header.Name, "X-MS-Exchange-Organization-OriginalClientIPAddress", StringComparison.OrdinalIgnoreCase))
					{
						result = new int?(0);
					}
					else
					{
						result = this.ExtractDValue(header);
					}
				}
				return result;
			}

			// Token: 0x06000083 RID: 131 RVA: 0x00004230 File Offset: 0x00002430
			public override int ConvertToScl(int? spamScore, int sclDeleteThreshold)
			{
				int valueOrDefault = spamScore.GetValueOrDefault();
				if (spamScore != null)
				{
					switch (valueOrDefault)
					{
					case 0:
						return 0;
					case 1:
						break;
					case 2:
						return sclDeleteThreshold - 1;
					case 3:
						return sclDeleteThreshold;
					default:
						return sclDeleteThreshold - 1;
					}
				}
				return 1;
			}

			// Token: 0x06000084 RID: 132 RVA: 0x00004274 File Offset: 0x00002474
			private static bool TryDecodeHeader(Header header, out string decodedHeader)
			{
				decodedHeader = null;
				try
				{
					byte[] bytes = Convert.FromBase64String(header.Value);
					decodedHeader = Encoding.ASCII.GetString(bytes);
				}
				catch (FormatException)
				{
				}
				catch (ArgumentException)
				{
				}
				return !string.IsNullOrEmpty(decodedHeader);
			}

			// Token: 0x06000085 RID: 133 RVA: 0x000042CC File Offset: 0x000024CC
			private static bool IsValidPairWithName(string pair, char name)
			{
				return pair.Length >= 2 && pair[0] == name && pair[1] == '=';
			}

			// Token: 0x06000086 RID: 134 RVA: 0x000042F0 File Offset: 0x000024F0
			private Header FindDeliveryHeader(HeaderList headers)
			{
				Header header = null;
				bool flag = false;
				foreach (Header header2 in SclHeaders.GetCandidateHeaders(headers, true))
				{
					if (SclHeaders.IsHeader(header2, "X-Message-Delivery"))
					{
						header = header2;
					}
					else if (SclHeaders.IsHeader(header2, "X-Message-Info"))
					{
						flag = true;
						break;
					}
				}
				if (flag && header != null)
				{
					SclHeaders.Tracer.TraceDebug<string>((long)this.GetHashCode(), "X-Message-Delivery header found with value: {0}", header.Value);
					return header;
				}
				return null;
			}

			// Token: 0x06000087 RID: 135 RVA: 0x00004384 File Offset: 0x00002584
			private int? ExtractDValue(Header header)
			{
				int? result = new int?(-1);
				string text;
				if (SclHeaders.HotmailSpamHeaderConverter.TryDecodeHeader(header, out text))
				{
					string[] array = text.Split(new char[]
					{
						';'
					});
					if (array != null && array.Length > 0 && SclHeaders.HotmailSpamHeaderConverter.IsValidPairWithName(array[0], 'V'))
					{
						result = null;
						bool flag = false;
						foreach (string text2 in array)
						{
							if (SclHeaders.HotmailSpamHeaderConverter.IsValidPairWithName(text2, 'D'))
							{
								result = new int?(-1);
								if (!flag)
								{
									flag = true;
									int num;
									if (text2.Length > 2 && int.TryParse(text2.Substring(2), out num) && num >= 0 && num <= 3)
									{
										result = new int?(num);
									}
								}
							}
						}
					}
				}
				return result;
			}

			// Token: 0x06000088 RID: 136 RVA: 0x00004440 File Offset: 0x00002640
			private bool IsNdr(HeaderList headers)
			{
				ContentTypeHeader contentTypeHeader = headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
				if (contentTypeHeader != null && contentTypeHeader.MediaType != null && contentTypeHeader.SubType != null && contentTypeHeader.MediaType.StartsWith("multipart", StringComparison.OrdinalIgnoreCase) && contentTypeHeader.SubType.StartsWith("report", StringComparison.OrdinalIgnoreCase))
				{
					SclHeaders.Tracer.TraceDebug<string>((long)this.GetHashCode(), "This message has a content type of {0} and is being treated as a report.", contentTypeHeader.SubType);
					return true;
				}
				return false;
			}

			// Token: 0x06000089 RID: 137 RVA: 0x000044B4 File Offset: 0x000026B4
			private bool IsLegitimateNdr(AsciiTextHeader clientIPAddressHeader)
			{
				IPAddress ipaddress;
				if (clientIPAddressHeader.Value != null && IPAddress.TryParse(clientIPAddressHeader.Value, out ipaddress))
				{
					foreach (IPRange iprange in this.womsIPRanges)
					{
						if (iprange.Contains(ipaddress))
						{
							SclHeaders.Tracer.TraceDebug<IPAddress, IPRange>((long)this.GetHashCode(), "Client IP Address {0} matches WOMS IP range {1}.", ipaddress, iprange);
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x04000052 RID: 82
			private const string MessageDeliveryHeaderName = "X-Message-Delivery";

			// Token: 0x04000053 RID: 83
			private const string MessageInfoHeaderName = "X-Message-Info";

			// Token: 0x04000054 RID: 84
			private const int InvalidD = -1;

			// Token: 0x04000055 RID: 85
			private const int MinD = 0;

			// Token: 0x04000056 RID: 86
			private const int MaxD = 3;

			// Token: 0x04000057 RID: 87
			private const int NdrDValue = 0;

			// Token: 0x04000058 RID: 88
			private List<IPRange> womsIPRanges;
		}

		// Token: 0x02000015 RID: 21
		public class EhfSpamHeaderConverter : SclHeaders.SpamHeaderConverter
		{
			// Token: 0x0600008A RID: 138 RVA: 0x00004540 File Offset: 0x00002740
			public EhfSpamHeaderConverter(int ehfSpamScoreJunkThreshold, int ehfSpamScoreDeleteThreshold)
			{
				this.ehfSpamScoreJunkThreshold = ehfSpamScoreJunkThreshold;
				this.ehfSpamScoreDeleteThreshold = ehfSpamScoreDeleteThreshold;
			}

			// Token: 0x0600008B RID: 139 RVA: 0x00004558 File Offset: 0x00002758
			public override Header FindSpamHeader(HeaderList headers)
			{
				Header result = null;
				foreach (Header header in SclHeaders.GetCandidateHeaders(headers, true))
				{
					if (SclHeaders.IsHeader(header, "X-SpamScore"))
					{
						SclHeaders.Tracer.TraceDebug((long)this.GetHashCode(), "X-SpamScore header found");
						result = header;
					}
					else if (SclHeaders.IsHeader(header, "X-SafeListed-IP"))
					{
						SclHeaders.Tracer.TraceDebug((long)this.GetHashCode(), "X-SafeListed-IP header found");
						result = header;
						break;
					}
				}
				return result;
			}

			// Token: 0x0600008C RID: 140 RVA: 0x000045F0 File Offset: 0x000027F0
			public override int? ExtractSpamScore(Header header)
			{
				int? result = new int?(int.MinValue);
				if (header != null)
				{
					string text;
					if (header.TryGetValue(out text))
					{
						if (!string.IsNullOrEmpty(text) && text.Length <= 256)
						{
							int value;
							if (string.Equals(header.Name, "X-SafeListed-IP", StringComparison.OrdinalIgnoreCase))
							{
								result = null;
							}
							else if (int.TryParse(text, out value))
							{
								result = new int?(value);
							}
							else
							{
								SclHeaders.Tracer.TraceError<string, string>((long)this.GetHashCode(), "EHF spam header {0} has non-integer value: {1}", header.Name, text);
							}
						}
					}
					else
					{
						SclHeaders.Tracer.TraceError<string>((long)this.GetHashCode(), "EHF spam header value could not be obtained: {0}", header.Name);
					}
				}
				return result;
			}

			// Token: 0x0600008D RID: 141 RVA: 0x0000469C File Offset: 0x0000289C
			public override int ConvertToScl(int? spamScore, int sclDeleteThreshold)
			{
				if (spamScore == -2147483648)
				{
					return sclDeleteThreshold - 1;
				}
				if (spamScore == null)
				{
					return 0;
				}
				if (spamScore < this.ehfSpamScoreJunkThreshold)
				{
					return 1;
				}
				if (spamScore < this.ehfSpamScoreDeleteThreshold)
				{
					return sclDeleteThreshold - 1;
				}
				return sclDeleteThreshold;
			}

			// Token: 0x04000059 RID: 89
			private const string SpamScoreHeaderName = "X-SpamScore";

			// Token: 0x0400005A RID: 90
			private const string SafeListedHeaderName = "X-SafeListed-IP";

			// Token: 0x0400005B RID: 91
			private const int InvalidSpamScore = -2147483648;

			// Token: 0x0400005C RID: 92
			private readonly int ehfSpamScoreJunkThreshold;

			// Token: 0x0400005D RID: 93
			private readonly int ehfSpamScoreDeleteThreshold;
		}

		// Token: 0x02000016 RID: 22
		public class PartnerSpamHeaderConverter : SclHeaders.SpamHeaderConverter
		{
			// Token: 0x0600008E RID: 142 RVA: 0x0000471B File Offset: 0x0000291B
			public PartnerSpamHeaderConverter(int partnerSpamScoreJunkThreshold, int partnerSpamScoreDeleteThreshold)
			{
				this.partnerSpamScoreJunkThreshold = partnerSpamScoreJunkThreshold;
				this.partnerSpamScoreDeleteThreshold = partnerSpamScoreDeleteThreshold;
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00004734 File Offset: 0x00002934
			public override Header FindSpamHeader(HeaderList headers)
			{
				Header result = null;
				foreach (Header header in SclHeaders.GetCandidateHeaders(headers, false))
				{
					if (SclHeaders.IsHeader(header, "X-PartnerSpamRecommendation"))
					{
						SclHeaders.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Partner spam recommendation header found: {0}", header.Name);
						result = header;
						break;
					}
				}
				return result;
			}

			// Token: 0x06000090 RID: 144 RVA: 0x000047AC File Offset: 0x000029AC
			public override int? ExtractSpamScore(Header header)
			{
				int? result = new int?(-1);
				if (header != null)
				{
					string text;
					if (header.TryGetValue(out text))
					{
						if (!string.IsNullOrEmpty(text) && text.Length <= 256)
						{
							int num;
							if (int.TryParse(text, out num) && num >= 0)
							{
								result = new int?(num);
							}
							else
							{
								SclHeaders.Tracer.TraceError<string, string>((long)this.GetHashCode(), "Partner spam header {0} has non-integer value: {1}", header.Name, text);
							}
						}
					}
					else
					{
						SclHeaders.Tracer.TraceError<string>((long)this.GetHashCode(), "Partner spam header value could not be obtained: {0}", header.Name);
					}
				}
				return result;
			}

			// Token: 0x06000091 RID: 145 RVA: 0x00004838 File Offset: 0x00002A38
			public override int ConvertToScl(int? spamScore, int sclDeleteThreshold)
			{
				if (spamScore == null)
				{
					throw new ArgumentNullException("spamScore");
				}
				if (spamScore < -1)
				{
					throw new ArgumentException("Spam score should not be less than InvalidSpamScore", "spamScore");
				}
				if (spamScore == -1)
				{
					return sclDeleteThreshold - 1;
				}
				if (spamScore < this.partnerSpamScoreJunkThreshold)
				{
					return 1;
				}
				if (spamScore < this.partnerSpamScoreDeleteThreshold)
				{
					return sclDeleteThreshold - 1;
				}
				return sclDeleteThreshold;
			}

			// Token: 0x0400005E RID: 94
			private const string SpamScoreHeaderName = "X-PartnerSpamRecommendation";

			// Token: 0x0400005F RID: 95
			private const int InvalidSpamScore = -1;

			// Token: 0x04000060 RID: 96
			private readonly int partnerSpamScoreJunkThreshold;

			// Token: 0x04000061 RID: 97
			private readonly int partnerSpamScoreDeleteThreshold;
		}
	}
}
