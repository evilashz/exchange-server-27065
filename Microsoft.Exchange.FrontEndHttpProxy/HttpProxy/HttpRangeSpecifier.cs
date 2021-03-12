using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200008B RID: 139
	internal class HttpRangeSpecifier
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x0001946C File Offset: 0x0001766C
		public HttpRangeSpecifier()
		{
			this.RangeUnitSpecifier = "bytes";
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001948A File Offset: 0x0001768A
		public Collection<HttpRange> RangeCollection
		{
			get
			{
				return this.rangeCollection;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00019492 File Offset: 0x00017692
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0001949A File Offset: 0x0001769A
		public string RangeUnitSpecifier { get; set; }

		// Token: 0x06000438 RID: 1080 RVA: 0x000194A4 File Offset: 0x000176A4
		public static HttpRangeSpecifier Parse(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException("value");
			}
			HttpRangeSpecifier httpRangeSpecifier = new HttpRangeSpecifier();
			string message;
			if (!HttpRangeSpecifier.TryParseInternal(value, httpRangeSpecifier, out message))
			{
				throw new ArgumentException(message);
			}
			return httpRangeSpecifier;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000194E0 File Offset: 0x000176E0
		private static bool TryParseInternal(string value, HttpRangeSpecifier specifier, out string parseFailureReason)
		{
			if (specifier == null)
			{
				throw new ArgumentNullException("specifier");
			}
			HttpRangeSpecifier.StrSegment strSegment = new HttpRangeSpecifier.StrSegment(value);
			HttpRangeSpecifier.ParseState parseState = HttpRangeSpecifier.ParseState.Start;
			parseFailureReason = string.Empty;
			int i = 0;
			int length = value.Length;
			long rangeStart = -1L;
			while (i < length)
			{
				char c = value[i];
				switch (parseState)
				{
				case HttpRangeSpecifier.ParseState.Start:
					if (c != ' ' && c != '\t')
					{
						if (strSegment.Start == -1)
						{
							strSegment.Start = i;
						}
						if (c == '=')
						{
							strSegment.SetLengthFromTerminatingIndex(i);
							strSegment.Trim();
							specifier.RangeUnitSpecifier = strSegment.ToString();
							parseState = HttpRangeSpecifier.ParseState.RangeStart;
							rangeStart = -1L;
							strSegment.Reset();
						}
					}
					break;
				case HttpRangeSpecifier.ParseState.RangeStart:
					if (c != ' ' && c != '\t')
					{
						if (strSegment.Start == -1)
						{
							strSegment.Start = i;
						}
						if (c == '-' || c == ',')
						{
							strSegment.SetLengthFromTerminatingIndex(i);
							strSegment.Trim();
							if (c != '-')
							{
								parseFailureReason = "Invalid range, missing '-' character at " + (strSegment.Start + strSegment.Length);
								return false;
							}
							if (strSegment.Length > 0 && !long.TryParse(strSegment.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out rangeStart))
							{
								parseFailureReason = "Could not parse first-byte-pos at " + strSegment.Start;
								return false;
							}
							parseState = HttpRangeSpecifier.ParseState.RangeEnd;
							strSegment.Reset();
						}
					}
					break;
				case HttpRangeSpecifier.ParseState.RangeEnd:
					if (c != ' ' && c != '\t')
					{
						if (strSegment.Start == -1)
						{
							strSegment.Start = i;
						}
						if (c == ',')
						{
							strSegment.SetLengthFromTerminatingIndex(i);
							strSegment.Trim();
							if (!HttpRangeSpecifier.ProcessRangeEnd(specifier, ref parseFailureReason, strSegment, rangeStart))
							{
								return false;
							}
							rangeStart = -1L;
							parseState = HttpRangeSpecifier.ParseState.RangeStart;
							strSegment.Reset();
						}
					}
					break;
				}
				i++;
			}
			if (strSegment.Start != -1)
			{
				strSegment.SetLengthFromTerminatingIndex(i);
				strSegment.Trim();
				if (parseState == HttpRangeSpecifier.ParseState.Start)
				{
					specifier.RangeUnitSpecifier = strSegment.ToString();
				}
				if (parseState == HttpRangeSpecifier.ParseState.RangeStart)
				{
					parseFailureReason = "Invalid range, missing '-' character at " + (strSegment.Start + strSegment.Length);
					return false;
				}
			}
			else
			{
				if (parseState == HttpRangeSpecifier.ParseState.Start)
				{
					parseFailureReason = "Did not find range unit specifier";
					return false;
				}
				if (parseState == HttpRangeSpecifier.ParseState.RangeStart)
				{
					parseFailureReason = "Expected range value at the end.";
					return false;
				}
			}
			if (parseState == HttpRangeSpecifier.ParseState.RangeEnd && !HttpRangeSpecifier.ProcessRangeEnd(specifier, ref parseFailureReason, strSegment, rangeStart))
			{
				return false;
			}
			if (specifier.RangeCollection.Count == 0)
			{
				parseFailureReason = "No ranges found.";
				return false;
			}
			return true;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001971C File Offset: 0x0001791C
		private static bool ProcessRangeEnd(HttpRangeSpecifier specifier, ref string parseFailureReason, HttpRangeSpecifier.StrSegment currentSegment, long rangeStart)
		{
			long rangeEnd = -1L;
			if (currentSegment.Start >= 0 && currentSegment.Length > 0 && !long.TryParse(currentSegment.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out rangeEnd))
			{
				parseFailureReason = "Could not parse last-byte-pos at " + currentSegment.Start;
				return false;
			}
			if (!HttpRangeSpecifier.AddRange(specifier, rangeStart, rangeEnd))
			{
				parseFailureReason = "Invalid range specification near " + currentSegment.Start;
				return false;
			}
			return true;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00019790 File Offset: 0x00017990
		private static bool AddRange(HttpRangeSpecifier specifier, long rangeStart, long rangeEnd)
		{
			try
			{
				specifier.RangeCollection.Add(new HttpRange(rangeStart, rangeEnd));
			}
			catch (ArgumentOutOfRangeException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0400036A RID: 874
		private readonly Collection<HttpRange> rangeCollection = new Collection<HttpRange>();

		// Token: 0x0200008C RID: 140
		private enum ParseState
		{
			// Token: 0x0400036D RID: 877
			Start,
			// Token: 0x0400036E RID: 878
			RangeStart,
			// Token: 0x0400036F RID: 879
			RangeEnd
		}

		// Token: 0x0200008D RID: 141
		private class StrSegment
		{
			// Token: 0x0600043C RID: 1084 RVA: 0x000197CC File Offset: 0x000179CC
			public StrSegment(string source)
			{
				if (source == null)
				{
					throw new ArgumentNullException("source");
				}
				this.source = source;
				this.Reset();
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x0600043D RID: 1085 RVA: 0x000197EF File Offset: 0x000179EF
			// (set) Token: 0x0600043E RID: 1086 RVA: 0x000197F7 File Offset: 0x000179F7
			public int Start { get; set; }

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600043F RID: 1087 RVA: 0x00019800 File Offset: 0x00017A00
			// (set) Token: 0x06000440 RID: 1088 RVA: 0x00019808 File Offset: 0x00017A08
			public int Length { get; set; }

			// Token: 0x06000441 RID: 1089 RVA: 0x00019811 File Offset: 0x00017A11
			public void SetLengthFromTerminatingIndex(int terminatingIndex)
			{
				this.Length = terminatingIndex - this.Start;
			}

			// Token: 0x06000442 RID: 1090 RVA: 0x00019824 File Offset: 0x00017A24
			public void Trim()
			{
				if (this.Start + this.Length > this.source.Length)
				{
					throw new InvalidOperationException("Source too short.");
				}
				while (this.Length > 0 && this.Start < this.source.Length)
				{
					if (!char.IsWhiteSpace(this.source[this.Start]))
					{
						break;
					}
					this.Start++;
					this.Length--;
				}
				while (this.Length > 0 && char.IsWhiteSpace(this.source[this.Start + this.Length - 1]))
				{
					this.Length--;
				}
			}

			// Token: 0x06000443 RID: 1091 RVA: 0x000198E0 File Offset: 0x00017AE0
			public void Reset()
			{
				this.Start = -1;
				this.Length = 0;
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x000198F0 File Offset: 0x00017AF0
			public override string ToString()
			{
				return this.source.Substring(this.Start, this.Length);
			}

			// Token: 0x04000370 RID: 880
			private readonly string source;
		}
	}
}
