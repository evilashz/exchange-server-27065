using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200004E RID: 78
	public class ScenarioData : ConcurrentDictionary<string, string>, IDisposable
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x00017490 File Offset: 0x00015690
		public ScenarioData()
		{
			base["SID"] = Guid.NewGuid().ToString();
			base["S"] = "DEF";
			this.AssignOnThread();
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000174D7 File Offset: 0x000156D7
		public ScenarioData(IEnumerable<KeyValuePair<string, string>> dictionary) : base(dictionary)
		{
			this.AssignOnThread();
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x000174E6 File Offset: 0x000156E6
		public static ScenarioData Current
		{
			get
			{
				if (ScenarioData.current == null)
				{
					ScenarioData.current = new ScenarioData();
				}
				return ScenarioData.current;
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000174FE File Offset: 0x000156FE
		public static ScenarioData FromString(string data)
		{
			return new ScenarioData(UserAgentSerializer.FromUserAgent(data));
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001750B File Offset: 0x0001570B
		public override string ToString()
		{
			return UserAgentSerializer.ToUserAgent(this);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00017514 File Offset: 0x00015714
		public bool Remove(string key)
		{
			string text;
			return base.TryRemove(key, out text);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001752A File Offset: 0x0001572A
		public void Dispose()
		{
			if (ScenarioData.current == this)
			{
				ScenarioData.current = null;
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001753A File Offset: 0x0001573A
		private void AssignOnThread()
		{
			if (ScenarioData.current == null)
			{
				ScenarioData.current = this;
			}
		}

		// Token: 0x040001DC RID: 476
		[ThreadStatic]
		private static ScenarioData current;
	}
}
