using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x02000048 RID: 72
	internal sealed class RegexState : StateNode
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00008EC7 File Offset: 0x000070C7
		public RegexState(int stateid) : base(stateid, false)
		{
			this.list = new List<int>();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008EDC File Offset: 0x000070DC
		public RegexState(int stateid, List<RegexTreeNode> nodes) : this(stateid)
		{
			this.Add(nodes);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00008EEC File Offset: 0x000070EC
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00008EF4 File Offset: 0x000070F4
		public bool Marked
		{
			get
			{
				return this.marked;
			}
			set
			{
				this.marked = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00008EFD File Offset: 0x000070FD
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00008F05 File Offset: 0x00007105
		public bool IsStartState
		{
			get
			{
				return this.startState;
			}
			set
			{
				this.startState = value;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008F10 File Offset: 0x00007110
		public bool Contains(int stateid)
		{
			foreach (int num in this.list)
			{
				if (num == stateid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008F68 File Offset: 0x00007168
		public void Add(List<RegexTreeNode> nodes)
		{
			if (nodes == null)
			{
				return;
			}
			int count = this.list.Count;
			if (this.list.Count == 0)
			{
				for (int i = 0; i < nodes.Count; i++)
				{
					this.list.Add(nodes[i].State);
					if (nodes[i].End)
					{
						base.IsFinal = true;
					}
				}
				return;
			}
			for (int j = 0; j < nodes.Count; j++)
			{
				int state = nodes[j].State;
				int num = 0;
				while (num < count && state != this.list[num])
				{
					num++;
				}
				if (num == count)
				{
					this.list.Add(state);
					if (nodes[j].End)
					{
						base.IsFinal = true;
					}
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009038 File Offset: 0x00007238
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			RegexState regexState = (RegexState)obj;
			if (this.list.Count != regexState.list.Count)
			{
				return false;
			}
			for (int i = 0; i < this.list.Count; i++)
			{
				int num = 0;
				while (num < regexState.list.Count && this.list[i] != regexState.list[num])
				{
					num++;
				}
				if (num == regexState.list.Count)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000090D5 File Offset: 0x000072D5
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040000E1 RID: 225
		private bool marked;

		// Token: 0x040000E2 RID: 226
		private bool startState;

		// Token: 0x040000E3 RID: 227
		private List<int> list;
	}
}
