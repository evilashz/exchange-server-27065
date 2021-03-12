using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CmdletLogEntries
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002A10 File Offset: 0x00000C10
		internal CmdletLogEntries()
		{
			this.InitCmdletLogEntries();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A1E File Offset: 0x00000C1E
		internal void AddEntry(string entry)
		{
			this.logEntries[this.currentIndentation].Add(entry);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A37 File Offset: 0x00000C37
		internal void ClearAllEntries()
		{
			this.InitCmdletLogEntries();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A3F File Offset: 0x00000C3F
		internal void ClearCurrentIndentationEntries()
		{
			this.logEntries[this.currentIndentation].Clear();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002A57 File Offset: 0x00000C57
		internal void IncreaseIndentation()
		{
			this.currentIndentation++;
			this.logEntries.Add(this.currentIndentation, new List<string>());
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A80 File Offset: 0x00000C80
		internal void DecreaseIndentation()
		{
			this.ClearCurrentIndentationEntries();
			this.logEntries.Remove(this.currentIndentation--);
			if (this.currentIndentation < 0)
			{
				throw new InvalidOperationException("Cannot decrease Indentation below 0");
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C14 File Offset: 0x00000E14
		internal IEnumerable<string> GetAllEntries()
		{
			for (int i = 0; i < this.currentIndentation + 1; i++)
			{
				for (int j = 0; j < this.logEntries[i].Count; j++)
				{
					yield return this.logEntries[i][j];
				}
			}
			yield break;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002D60 File Offset: 0x00000F60
		internal IEnumerable<string> GetCurrentIndentationEntries()
		{
			for (int i = 0; i < this.logEntries[this.currentIndentation].Count; i++)
			{
				yield return this.logEntries[this.currentIndentation][i];
			}
			yield break;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002D7D File Offset: 0x00000F7D
		private void InitCmdletLogEntries()
		{
			this.currentIndentation = 0;
			this.logEntries = new Dictionary<int, List<string>>();
			this.logEntries.Add(this.currentIndentation, new List<string>());
		}

		// Token: 0x04000013 RID: 19
		private Dictionary<int, List<string>> logEntries;

		// Token: 0x04000014 RID: 20
		private int currentIndentation;
	}
}
