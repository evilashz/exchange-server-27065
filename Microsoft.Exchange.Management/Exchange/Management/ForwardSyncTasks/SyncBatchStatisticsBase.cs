using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000359 RID: 857
	public class SyncBatchStatisticsBase
	{
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0008217A File Offset: 0x0008037A
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x00082182 File Offset: 0x00080382
		public TimeSpan ResponseTime { get; set; }

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0008218B File Offset: 0x0008038B
		// (set) Token: 0x06001D91 RID: 7569 RVA: 0x00082193 File Offset: 0x00080393
		public int ObjectCount { get; set; }

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x0008219C File Offset: 0x0008039C
		// (set) Token: 0x06001D93 RID: 7571 RVA: 0x000821A4 File Offset: 0x000803A4
		public int LinkCount { get; set; }

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x000821AD File Offset: 0x000803AD
		// (set) Token: 0x06001D95 RID: 7573 RVA: 0x000821B5 File Offset: 0x000803B5
		public SizeAndCountStatistics ObjectSize { get; set; }

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x000821BE File Offset: 0x000803BE
		// (set) Token: 0x06001D97 RID: 7575 RVA: 0x000821C6 File Offset: 0x000803C6
		public double ObjectsPerSecond { get; set; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x000821CF File Offset: 0x000803CF
		// (set) Token: 0x06001D99 RID: 7577 RVA: 0x000821D7 File Offset: 0x000803D7
		public double ObjectBytesPerSecond { get; set; }

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x000821E0 File Offset: 0x000803E0
		// (set) Token: 0x06001D9B RID: 7579 RVA: 0x000821E8 File Offset: 0x000803E8
		public SizeAndCountStatistics LinkSize { get; set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x000821F1 File Offset: 0x000803F1
		// (set) Token: 0x06001D9D RID: 7581 RVA: 0x000821F9 File Offset: 0x000803F9
		public double LinksPerSecond { get; set; }

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x00082202 File Offset: 0x00080402
		// (set) Token: 0x06001D9F RID: 7583 RVA: 0x0008220A File Offset: 0x0008040A
		public double LinkBytesPerSecond { get; set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x00082213 File Offset: 0x00080413
		// (set) Token: 0x06001DA1 RID: 7585 RVA: 0x0008221B File Offset: 0x0008041B
		public Dictionary<string, int> ObjectTypes { get; set; }

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00082224 File Offset: 0x00080424
		public static int SerializedSize(object obj)
		{
			StringWriter stringWriter = new StringWriter();
			XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
			xmlSerializer.Serialize(new XmlTextWriter(stringWriter), obj);
			return stringWriter.ToString().Length * 2;
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0008228C File Offset: 0x0008048C
		public virtual void Calculate(IEnumerable<DirectoryObject> Objects, IEnumerable<DirectoryLink> Links)
		{
			this.ObjectCount = Objects.Count<DirectoryObject>();
			this.LinkCount = Links.Count<DirectoryLink>();
			if (SyncBatchStatisticsBase.<Calculate>o__SiteContainer0.<>p__Site1 == null)
			{
				SyncBatchStatisticsBase.<Calculate>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(SyncBatchStatisticsBase)));
			}
			this.ObjectSize = SyncBatchStatisticsBase.<Calculate>o__SiteContainer0.<>p__Site1.Target(SyncBatchStatisticsBase.<Calculate>o__SiteContainer0.<>p__Site1, SizeAndCountStatistics.Calculate(from o in Objects
			select SyncBatchStatisticsBase.SerializedSize(o)));
			this.ObjectsPerSecond = (double)this.ObjectCount / this.ResponseTime.TotalSeconds;
			this.ObjectBytesPerSecond = (double)this.ObjectSize.Sum / this.ResponseTime.TotalSeconds;
			if (SyncBatchStatisticsBase.<Calculate>o__SiteContainer0.<>p__Site2 == null)
			{
				SyncBatchStatisticsBase.<Calculate>o__SiteContainer0.<>p__Site2 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(SyncBatchStatisticsBase)));
			}
			this.LinkSize = SyncBatchStatisticsBase.<Calculate>o__SiteContainer0.<>p__Site2.Target(SyncBatchStatisticsBase.<Calculate>o__SiteContainer0.<>p__Site2, SizeAndCountStatistics.Calculate(from o in Links
			select SyncBatchStatisticsBase.SerializedSize(o)));
			this.LinksPerSecond = (double)this.LinkCount / this.ResponseTime.TotalSeconds;
			this.LinkBytesPerSecond = (double)this.LinkSize.Sum / this.ResponseTime.TotalSeconds;
			this.ObjectTypes = (from o in Objects
			group o by o.GetType().Name).ToDictionary((IGrouping<string, DirectoryObject> g) => g.Key, (IGrouping<string, DirectoryObject> g) => g.Count<DirectoryObject>());
		}

		// Token: 0x0200129C RID: 4764
		[CompilerGenerated]
		private static class <Calculate>o__SiteContainer0
		{
			// Token: 0x04006867 RID: 26727
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site1;

			// Token: 0x04006868 RID: 26728
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site2;
		}
	}
}
