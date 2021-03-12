using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200002F RID: 47
	internal class CrimsonChannelReader : IDisposable
	{
		// Token: 0x060000DD RID: 221 RVA: 0x000051D1 File Offset: 0x000033D1
		public CrimsonChannelReader(string channelName, string channelPath, string xPathQuery) : this(new EventLogQuery(CrimsonChannelReader.GetChannelPath(channelName, channelPath), PathType.LogName, xPathQuery), null)
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000051E8 File Offset: 0x000033E8
		public CrimsonChannelReader(EventLogQuery query, EventLogReaderAdapter adapter = null)
		{
			this.Query = query;
			this.reader = (adapter ?? new EventLogReaderAdapter(this.Query));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005210 File Offset: 0x00003410
		public static string GenerateXPathFilterForTimeRange(string channelName, string channelPath, DateTime createTime, DateTime endTime, string additionalConstraints = null)
		{
			return string.Format("<QueryList><Query Id=\"0\" Path=\"{0}\"><Select Path=\"{0}\">*[System[TimeCreated[@SystemTime&gt;='{1}' and @SystemTime&lt;='{2}']]{3}]</Select></Query></QueryList>", new object[]
			{
				CrimsonChannelReader.GetChannelPath(channelName, channelPath),
				createTime.ToString("o"),
				endTime.ToString("o"),
				additionalConstraints ?? string.Empty
			});
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005268 File Offset: 0x00003468
		public static string GenerateXPathFilterForFailedEventsInTimeRange(string channelName, string channelPath, DateTime createTime, DateTime endTime, string additionalConstraints = null)
		{
			return string.Format("<QueryList><Query Id=\"0\" Path=\"{0}\"><Select Path=\"{0}\">*[System[(Level=1  or Level=2 or Level=3) and TimeCreated[@SystemTime&gt;='{1}' and @SystemTime&lt;='{2}']]{3}]</Select></Query></QueryList>", new object[]
			{
				CrimsonChannelReader.GetChannelPath(channelName, channelPath),
				createTime.ToString("o"),
				endTime.ToString("o"),
				additionalConstraints ?? string.Empty
			});
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000053A8 File Offset: 0x000035A8
		public IEnumerable<EventRecord> ReadAll()
		{
			for (;;)
			{
				EventRecord record = this.reader.ReadEvent();
				if (record == null)
				{
					break;
				}
				yield return record;
			}
			yield break;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000053C5 File Offset: 0x000035C5
		public EventRecord ReadOne()
		{
			return this.reader.ReadEvent();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000054E0 File Offset: 0x000036E0
		public IEnumerable<EventRecord> ReadFirstNEvents(int n)
		{
			while (n > 0)
			{
				EventRecord record = this.reader.ReadEvent();
				if (record == null)
				{
					break;
				}
				n--;
				yield return record;
			}
			yield break;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005504 File Offset: 0x00003704
		public void Dispose()
		{
			if (this.reader != null)
			{
				this.reader.Dispose();
				this.reader = null;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005520 File Offset: 0x00003720
		protected static string GetChannelPath(string channelName, string channelPath)
		{
			if (channelName == null)
			{
				throw new ArgumentNullException("channelName");
			}
			if (channelPath == null)
			{
				throw new ArgumentNullException("channelPath");
			}
			return string.Format("{0}/{1}", channelName, channelPath);
		}

		// Token: 0x040000A7 RID: 167
		private const string XPathTimeRangeFilter = "<QueryList><Query Id=\"0\" Path=\"{0}\"><Select Path=\"{0}\">*[System[TimeCreated[@SystemTime&gt;='{1}' and @SystemTime&lt;='{2}']]{3}]</Select></Query></QueryList>";

		// Token: 0x040000A8 RID: 168
		private const string XPathFailedEventsTimeRangeFilter = "<QueryList><Query Id=\"0\" Path=\"{0}\"><Select Path=\"{0}\">*[System[(Level=1  or Level=2 or Level=3) and TimeCreated[@SystemTime&gt;='{1}' and @SystemTime&lt;='{2}']]{3}]</Select></Query></QueryList>";

		// Token: 0x040000A9 RID: 169
		private const string ChannelPathFormat = "{0}/{1}";

		// Token: 0x040000AA RID: 170
		protected readonly EventLogQuery Query;

		// Token: 0x040000AB RID: 171
		protected EventLogReaderAdapter reader;
	}
}
