using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000291 RID: 657
	internal class TimeEntry : ITimeEntry, IDisposable
	{
		// Token: 0x06001823 RID: 6179 RVA: 0x0008DB04 File Offset: 0x0008BD04
		private static IDictionary<TimeId, string> BuildShortenedIds()
		{
			Type typeFromHandle = typeof(TimeId);
			Type typeFromHandle2 = typeof(TimeIdAttribute);
			int length = Enum.GetValues(typeFromHandle).Length;
			IDictionary<TimeId, string> dictionary = new Dictionary<TimeId, string>(length);
			foreach (FieldInfo fieldInfo in from x in typeFromHandle.GetTypeInfo().DeclaredFields
			where x.IsStatic && x.IsPublic
			select x)
			{
				TimeIdAttribute[] array = (TimeIdAttribute[])fieldInfo.GetCustomAttributes(typeFromHandle2, false);
				TimeId key = (TimeId)fieldInfo.GetValue(null);
				dictionary.Add(key, array[0].Name);
			}
			return dictionary;
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0008DBD4 File Offset: 0x0008BDD4
		// (set) Token: 0x06001825 RID: 6181 RVA: 0x0008DBDC File Offset: 0x0008BDDC
		public TimeId TimeId { get; private set; }

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x0008DBE5 File Offset: 0x0008BDE5
		// (set) Token: 0x06001827 RID: 6183 RVA: 0x0008DBED File Offset: 0x0008BDED
		public DateTime StartTime { get; private set; }

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0008DBF6 File Offset: 0x0008BDF6
		// (set) Token: 0x06001829 RID: 6185 RVA: 0x0008DBFE File Offset: 0x0008BDFE
		public DateTime EndTime { get; private set; }

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0008DC07 File Offset: 0x0008BE07
		// (set) Token: 0x0600182B RID: 6187 RVA: 0x0008DC0F File Offset: 0x0008BE0F
		public int ThreadId { get; private set; }

		// Token: 0x0600182C RID: 6188 RVA: 0x0008DC18 File Offset: 0x0008BE18
		public TimeEntry(TimeId timeId, Action<TimeEntry> onRelease)
		{
			this.TimeId = timeId;
			this.ThreadId = ThreadIdProvider.ManagedThreadId;
			this.StartTime = TimeProvider.UtcNow;
			this.EndTime = DateTime.MinValue;
			if (onRelease == null)
			{
				throw new ArgumentNullException("onRelease");
			}
			this.onRelease = onRelease;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0008DC74 File Offset: 0x0008BE74
		public void Dispose()
		{
			this.VerifyThread();
			if (this.EndTime == DateTime.MinValue)
			{
				lock (this.instanceLock)
				{
					if (this.EndTime == DateTime.MinValue)
					{
						GC.SuppressFinalize(this);
						this.EndTime = TimeProvider.UtcNow;
						this.onRelease(this);
						this.onRelease = null;
					}
				}
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x0008DCFC File Offset: 0x0008BEFC
		public TimeSpan ElapsedInclusive
		{
			get
			{
				if (!(this.EndTime == DateTime.MinValue))
				{
					return this.EndTime - this.StartTime;
				}
				return TimeSpan.Zero;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x0008DD28 File Offset: 0x0008BF28
		public TimeSpan ElapsedExclusive
		{
			get
			{
				if (this.EndTime == DateTime.MinValue)
				{
					return TimeSpan.Zero;
				}
				TimeSpan timeSpan = TimeSpan.Zero;
				if (this.children != null)
				{
					lock (this.instanceLock)
					{
						if (this.children != null)
						{
							foreach (TimeEntry timeEntry in this.children)
							{
								timeSpan += timeEntry.ElapsedInclusive;
							}
						}
					}
				}
				TimeSpan timeSpan2 = this.ElapsedInclusive - timeSpan;
				if (!(timeSpan2 > TimeSpan.Zero))
				{
					return TimeSpan.Zero;
				}
				return timeSpan2;
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0008DDFC File Offset: 0x0008BFFC
		public void AddChild(TimeEntry childEntry)
		{
			this.VerifyThread();
			lock (this.instanceLock)
			{
				if (this.children == null)
				{
					this.children = new List<TimeEntry>();
				}
				this.children.Add(childEntry);
			}
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0008DE5C File Offset: 0x0008C05C
		public override string ToString()
		{
			string result;
			lock (this.instanceLock)
			{
				string text = "NONE";
				if (this.children != null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (TimeEntry timeEntry in this.children)
					{
						stringBuilder.Append(timeEntry.ToString() + ",");
					}
					text = stringBuilder.ToString();
				}
				if (this.TimeId == TimeId.Root)
				{
					result = string.Format("TID:{0}>>[{1}]", this.ThreadId, text);
				}
				else
				{
					result = string.Format("ID:{0},Start:{1},End:{2},Excl:{3} ms,Child:[{4}]", new object[]
					{
						TimeEntry.ShortenedIds[this.TimeId],
						this.StartTime.ToString("T"),
						(this.EndTime == DateTime.MinValue) ? "<NOT DONE>" : this.EndTime.ToString("T"),
						(int)this.ElapsedExclusive.TotalMilliseconds,
						text
					});
				}
			}
			return result;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0008DFD8 File Offset: 0x0008C1D8
		internal void VerifyThread()
		{
			if (ThreadIdProvider.ManagedThreadId != this.ThreadId)
			{
				throw new InvalidOperationException(string.Format("[TimeEntry] Start was called on thread {0} and was then used on thread {1}.  Must be used on single, consistent thread.", this.ThreadId, ThreadIdProvider.ManagedThreadId));
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0008E00C File Offset: 0x0008C20C
		internal List<TimeEntry> GetChildrenForTest()
		{
			return this.children;
		}

		// Token: 0x04000EBD RID: 3773
		private static readonly IDictionary<TimeId, string> ShortenedIds = TimeEntry.BuildShortenedIds();

		// Token: 0x04000EBE RID: 3774
		private object instanceLock = new object();

		// Token: 0x04000EBF RID: 3775
		private Action<TimeEntry> onRelease;

		// Token: 0x04000EC0 RID: 3776
		private List<TimeEntry> children;
	}
}
