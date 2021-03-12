using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200005F RID: 95
	public class DialGroups
	{
		// Token: 0x06000397 RID: 919 RVA: 0x0000D14E File Offset: 0x0000B34E
		public DialGroups()
		{
			this.dialGroups = new Dictionary<string, List<DialGroupEntry>>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000D166 File Offset: 0x0000B366
		public Dictionary<string, List<DialGroupEntry>> DialPermissionGroups
		{
			get
			{
				return this.dialGroups;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D170 File Offset: 0x0000B370
		public static bool HaveIntersection(MultiValuedProperty<DialGroupEntry> configuredEntries, MultiValuedProperty<string> selectedEntries)
		{
			if (configuredEntries.Count == 0 || selectedEntries.Count == 0)
			{
				return false;
			}
			DialGroups dialGroups = new DialGroups();
			dialGroups.Add(configuredEntries);
			List<DialGroupEntry> list = null;
			foreach (string name in selectedEntries)
			{
				bool flag = dialGroups.TryGetValue(name, out list);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000D1F0 File Offset: 0x0000B3F0
		public List<DialGroupEntry> Get(string name)
		{
			return this.dialGroups[name];
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000D1FE File Offset: 0x0000B3FE
		public bool TryGetValue(string name, out List<DialGroupEntry> entryList)
		{
			return this.dialGroups.TryGetValue(name, out entryList);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D210 File Offset: 0x0000B410
		public void Add(MultiValuedProperty<DialGroupEntry> entries)
		{
			if (entries == null)
			{
				return;
			}
			foreach (DialGroupEntry dg in entries)
			{
				this.Add(dg);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000D264 File Offset: 0x0000B464
		internal void Set(string name, List<DialGroupEntry> rules)
		{
			this.dialGroups[name] = rules;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000D274 File Offset: 0x0000B474
		internal void Add(DialGroupEntry dg)
		{
			if (dg == null)
			{
				return;
			}
			List<DialGroupEntry> list = null;
			if (!this.dialGroups.TryGetValue(dg.Name, out list))
			{
				list = new List<DialGroupEntry>();
				this.dialGroups[dg.Name] = list;
			}
			list.Add(dg);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		internal void Add(DialGroupEntry[] entries)
		{
			if (entries == null)
			{
				return;
			}
			foreach (DialGroupEntry dg in entries)
			{
				this.Add(dg);
			}
		}

		// Token: 0x04000296 RID: 662
		private Dictionary<string, List<DialGroupEntry>> dialGroups;
	}
}
