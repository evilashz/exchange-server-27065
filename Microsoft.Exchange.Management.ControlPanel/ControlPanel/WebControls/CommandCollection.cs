using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000575 RID: 1397
	public class CommandCollection : Collection<Command>
	{
		// Token: 0x0600410B RID: 16651 RVA: 0x000C6370 File Offset: 0x000C4570
		public Command FindCommandByName(string name)
		{
			foreach (Command command in this)
			{
				if (string.Equals(command.Name, name, StringComparison.InvariantCultureIgnoreCase))
				{
					return command;
				}
			}
			return null;
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x000C63C8 File Offset: 0x000C45C8
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x000C63D1 File Offset: 0x000C45D1
		protected override void InsertItem(int index, Command item)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x000C63EE File Offset: 0x000C45EE
		protected override void SetItem(int index, Command item)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.SetItem(index, item);
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x000C640B File Offset: 0x000C460B
		protected override void RemoveItem(int index)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.RemoveItem(index);
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x000C6427 File Offset: 0x000C4627
		protected override void ClearItems()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.ClearItems();
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x000C6444 File Offset: 0x000C4644
		public bool ContainsVisibleCommands()
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Visible)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002B2B RID: 11051
		private bool isReadOnly;
	}
}
