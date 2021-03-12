using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000B9 RID: 185
	public sealed class TraceContentBuilder
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x000179E1 File Offset: 0x00015BE1
		private TraceContentBuilder(int maximumChunks)
		{
			this.content = new List<string>(400);
			this.maximumChunks = maximumChunks;
			this.line = new StringBuilder(6144);
			this.length = 0;
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00017A17 File Offset: 0x00015C17
		public static int MaximumChunkLength
		{
			get
			{
				return 6144;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00017A1E File Offset: 0x00015C1E
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00017A26 File Offset: 0x00015C26
		public static TraceContentBuilder Create()
		{
			return new TraceContentBuilder(25);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00017A2F File Offset: 0x00015C2F
		public static TraceContentBuilder Create(int maximumChunks)
		{
			return new TraceContentBuilder(maximumChunks);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00017A37 File Offset: 0x00015C37
		public void Append(int value)
		{
			this.Append(value.ToString());
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00017A46 File Offset: 0x00015C46
		public void Append(uint value)
		{
			this.Append(value.ToString());
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00017A55 File Offset: 0x00015C55
		public void Append(double value)
		{
			this.Append(value.ToString());
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00017A64 File Offset: 0x00015C64
		public void Append(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.line.Append(value);
				this.length += value.Length;
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00017A8E File Offset: 0x00015C8E
		public void AppendLine(string value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00017A9D File Offset: 0x00015C9D
		public void AppendLine()
		{
			this.Commit();
			this.content.Add(TraceContentBuilder.newLine);
			this.length += TraceContentBuilder.newLine.Length;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00017ACC File Offset: 0x00015CCC
		public void AppendFormat(string format, params object[] args)
		{
			this.Append(string.Format(format, args));
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00017ADC File Offset: 0x00015CDC
		public void Indent(int depth)
		{
			this.Commit();
			switch (depth)
			{
			case 0:
				break;
			case 1:
				this.content.Add(TraceContentBuilder.tabOne);
				this.length += TraceContentBuilder.tabOne.Length;
				return;
			case 2:
				this.content.Add(TraceContentBuilder.tabTwo);
				this.length += TraceContentBuilder.tabTwo.Length;
				return;
			case 3:
				this.content.Add(TraceContentBuilder.tabThree);
				this.length += TraceContentBuilder.tabThree.Length;
				break;
			default:
				return;
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00017B80 File Offset: 0x00015D80
		public void Remove()
		{
			if (this.line.Length > 0)
			{
				this.length -= this.line.Length;
				this.line.Clear();
				return;
			}
			if (this.content.Count > 0)
			{
				int index = this.content.Count - 1;
				this.length -= this.content[index].Length;
				this.content.RemoveAt(index);
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00017C08 File Offset: 0x00015E08
		public List<string> ToWideString()
		{
			List<string> list = new List<string>(this.maximumChunks);
			this.Commit();
			if (this.content.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(TraceContentBuilder.MaximumChunkLength);
				for (int i = 0; i < this.content.Count; i++)
				{
					if (stringBuilder.Length + this.content[i].Length > TraceContentBuilder.MaximumChunkLength)
					{
						list.Add(stringBuilder.ToString());
						stringBuilder.Clear();
					}
					stringBuilder.Append(this.content[i]);
				}
				if (stringBuilder.Length > 0)
				{
					list.Add(stringBuilder.ToString());
				}
			}
			return list;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00017CB4 File Offset: 0x00015EB4
		public string ToTallString()
		{
			this.Commit();
			if (this.content.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(this.length);
				for (int i = 0; i < this.content.Count; i++)
				{
					string text = this.content[i];
					if (object.ReferenceEquals(text, TraceContentBuilder.newLine))
					{
						stringBuilder.AppendLine();
					}
					else if (object.ReferenceEquals(text, TraceContentBuilder.tabOne))
					{
						stringBuilder.Append("\t");
					}
					else if (object.ReferenceEquals(text, TraceContentBuilder.tabTwo))
					{
						stringBuilder.Append("\t\t");
					}
					else if (object.ReferenceEquals(text, TraceContentBuilder.tabThree))
					{
						stringBuilder.Append("\t\t\t");
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00017D88 File Offset: 0x00015F88
		public override string ToString()
		{
			return this.ToTallString();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000180C8 File Offset: 0x000162C8
		private static IEnumerable<string> SplitLongLine(string line)
		{
			if (line.Length > TraceContentBuilder.MaximumChunkLength)
			{
				int half = line.Length / 2;
				string left = line.Substring(0, half);
				string right = line.Substring(half);
				if (left.Length > TraceContentBuilder.MaximumChunkLength)
				{
					foreach (string segment in TraceContentBuilder.SplitLongLine(left))
					{
						yield return segment;
					}
				}
				else
				{
					yield return left;
				}
				if (right.Length > TraceContentBuilder.MaximumChunkLength)
				{
					foreach (string segment2 in TraceContentBuilder.SplitLongLine(right))
					{
						yield return segment2;
					}
				}
				else
				{
					yield return right;
				}
			}
			else
			{
				yield return line;
			}
			yield break;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000180E8 File Offset: 0x000162E8
		private void Commit()
		{
			if (this.line.Length > TraceContentBuilder.MaximumChunkLength)
			{
				foreach (string item in TraceContentBuilder.SplitLongLine(this.line.ToString()))
				{
					this.content.Add(item);
				}
				this.line.Clear();
				return;
			}
			if (this.line.Length > 0)
			{
				this.content.Add(this.line.ToString());
				this.line.Clear();
			}
		}

		// Token: 0x04000727 RID: 1831
		private const int MaximumChunksLength = 6144;

		// Token: 0x04000728 RID: 1832
		private const int DefaultMaximumChunks = 25;

		// Token: 0x04000729 RID: 1833
		private static string tabOne = "`t";

		// Token: 0x0400072A RID: 1834
		private static string tabTwo = "`t`t";

		// Token: 0x0400072B RID: 1835
		private static string tabThree = "`t`t`t";

		// Token: 0x0400072C RID: 1836
		private static string newLine = "`r`n";

		// Token: 0x0400072D RID: 1837
		private readonly StringBuilder line;

		// Token: 0x0400072E RID: 1838
		private readonly List<string> content;

		// Token: 0x0400072F RID: 1839
		private readonly int maximumChunks;

		// Token: 0x04000730 RID: 1840
		private int length;
	}
}
