using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000071 RID: 113
	internal sealed class CrimsonReader<T> : CrimsonOperation<T> where T : IPersistence, new()
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x0001B524 File Offset: 0x00019724
		internal CrimsonReader() : this(null, null, null)
		{
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001B52F File Offset: 0x0001972F
		internal CrimsonReader(string serviceName) : this(serviceName, null, null)
		{
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001B53A File Offset: 0x0001973A
		internal CrimsonReader(string serviceName, EventBookmark bookmark) : this(serviceName, bookmark, null)
		{
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001B545 File Offset: 0x00019745
		internal CrimsonReader(string serviceName, EventBookmark bookmark, string channelName) : base(bookmark, channelName)
		{
			this.ServiceName = serviceName;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001B556 File Offset: 0x00019756
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0001B55E File Offset: 0x0001975E
		public string ServiceName { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001B567 File Offset: 0x00019767
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0001B56F File Offset: 0x0001976F
		internal bool EndOfEventsReached { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001B578 File Offset: 0x00019778
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x0001B580 File Offset: 0x00019780
		internal bool IsReverseDirection { get; set; }

		// Token: 0x0600068A RID: 1674 RVA: 0x0001B589 File Offset: 0x00019789
		internal override void Cleanup()
		{
			if (this.reader != null)
			{
				this.reader.Dispose();
				this.reader = null;
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001B5A5 File Offset: 0x000197A5
		internal T ReadNext()
		{
			return this.ReadNext(false);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001B5B0 File Offset: 0x000197B0
		internal T ReadNext(bool isForce)
		{
			T result = default(T);
			if (!this.EndOfEventsReached || isForce)
			{
				this.Initialize(isForce);
				using (EventRecord eventRecord = this.reader.ReadEvent())
				{
					if (eventRecord != null)
					{
						result = base.EventToObject(eventRecord);
					}
					else
					{
						this.EndOfEventsReached = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001B614 File Offset: 0x00019814
		internal T ReadLast()
		{
			T result = default(T);
			this.Initialize(false);
			this.reader.Seek(SeekOrigin.End, 0L);
			using (EventRecord eventRecord = this.reader.ReadEvent())
			{
				if (eventRecord != null)
				{
					result = base.EventToObject(eventRecord);
				}
			}
			return result;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001B770 File Offset: 0x00019970
		internal IEnumerable<T> ReadAll()
		{
			T o = default(T);
			for (;;)
			{
				o = this.ReadNext(false);
				if (this.EndOfEventsReached)
				{
					break;
				}
				yield return o;
			}
			yield break;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001B790 File Offset: 0x00019990
		protected override string GetDefaultXPathQuery()
		{
			return CrimsonHelper.BuildXPathQueryString(base.ChannelName, this.ServiceName, base.QueryStartTime, base.QueryEndTime, base.QueryUserPropertyCondition);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001B7C2 File Offset: 0x000199C2
		private void Initialize()
		{
			this.Initialize(false);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001B7CC File Offset: 0x000199CC
		private void Initialize(bool force)
		{
			if (!base.IsInitialized || force)
			{
				this.Cleanup();
				EventLogQuery queryObject = base.GetQueryObject();
				if (queryObject != null)
				{
					queryObject.ReverseDirection = this.IsReverseDirection;
				}
				EventBookmark bookMark = base.BookMark;
				this.reader = new EventLogReader(queryObject, bookMark);
				base.IsInitialized = true;
				this.EndOfEventsReached = false;
			}
		}

		// Token: 0x0400043D RID: 1085
		private EventLogReader reader;
	}
}
