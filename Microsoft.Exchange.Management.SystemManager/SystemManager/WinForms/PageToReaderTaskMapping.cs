using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000B9 RID: 185
	internal class PageToReaderTaskMapping : Dictionary<string, List<string>>
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x0001632C File Offset: 0x0001452C
		public PageToReaderTaskMapping(IList<ReaderTaskProfile> list, Dictionary<string, List<string>> pageToObjectMapping)
		{
			using (Dictionary<string, List<string>>.KeyCollection.Enumerator enumerator = pageToObjectMapping.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string key = enumerator.Current;
					this.pageExecutionStatus.Add(key, false);
					base.Add(key, (from c in list
					where pageToObjectMapping[key].Contains(c.DataObjectName)
					select c.Name).ToList<string>());
					this.isReadOnDemand = true;
				}
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00016418 File Offset: 0x00014618
		public bool IsExecuted(string page)
		{
			if (!this.isReadOnDemand)
			{
				return this.allTasksExecuted;
			}
			return page != null && this.pageExecutionStatus.ContainsKey(page) && this.pageExecutionStatus[page];
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00016448 File Offset: 0x00014648
		public void Execute(string page)
		{
			if (!this.isReadOnDemand)
			{
				this.allTasksExecuted = true;
				return;
			}
			this.pageExecutionStatus[page] = true;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00016468 File Offset: 0x00014668
		public void Reset()
		{
			List<string> list = this.pageExecutionStatus.Keys.ToList<string>();
			foreach (string key in list)
			{
				this.pageExecutionStatus[key] = false;
			}
			this.allTasksExecuted = false;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00016508 File Offset: 0x00014708
		public bool CanTaskExecuted(string pageName, string taskName)
		{
			if (!this.isReadOnDemand)
			{
				return !this.allTasksExecuted;
			}
			return base[pageName].Contains(taskName) && (from c in this.pageExecutionStatus
			where c.Value && this[c.Key].Contains(taskName)
			select c).Count<KeyValuePair<string, bool>>() == 0;
		}

		// Token: 0x04000202 RID: 514
		private Dictionary<string, bool> pageExecutionStatus = new Dictionary<string, bool>();

		// Token: 0x04000203 RID: 515
		private bool isReadOnDemand;

		// Token: 0x04000204 RID: 516
		private bool allTasksExecuted;
	}
}
