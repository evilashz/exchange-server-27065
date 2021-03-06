using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x0200001B RID: 27
	internal class DetailsTemplateUndoEngine : UndoEngine
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00005EF2 File Offset: 0x000040F2
		public DetailsTemplateUndoEngine(IServiceProvider provider) : base(provider)
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005F08 File Offset: 0x00004108
		public void DoUndo()
		{
			if (this.currentPos > 0)
			{
				UndoEngine.UndoUnit undoUnit = this.undoUnitList[this.currentPos - 1];
				undoUnit.Undo();
				this.currentPos--;
			}
			this.UpdateUndoRedoMenuCommandsStatus();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005F4C File Offset: 0x0000414C
		public void DoRedo()
		{
			if (this.currentPos < this.undoUnitList.Count)
			{
				UndoEngine.UndoUnit undoUnit = this.undoUnitList[this.currentPos];
				undoUnit.Undo();
				this.currentPos++;
			}
			this.UpdateUndoRedoMenuCommandsStatus();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005F98 File Offset: 0x00004198
		private void UpdateUndoRedoMenuCommandsStatus()
		{
			IMenuCommandService menuCommandService = base.GetService(typeof(IMenuCommandService)) as IMenuCommandService;
			if (menuCommandService != null)
			{
				MenuCommand menuCommand = menuCommandService.FindCommand(StandardCommands.Undo);
				MenuCommand menuCommand2 = menuCommandService.FindCommand(StandardCommands.Redo);
				if (menuCommand != null)
				{
					menuCommand.Enabled = (this.currentPos > 0 && this.undoUnitList.Count > 0);
				}
				if (menuCommand2 != null)
				{
					menuCommand2.Enabled = (this.currentPos < this.undoUnitList.Count && this.undoUnitList.Count > 0);
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006028 File Offset: 0x00004228
		protected override void AddUndoUnit(UndoEngine.UndoUnit unit)
		{
			this.undoUnitList.RemoveRange(this.currentPos, this.undoUnitList.Count - this.currentPos);
			if (this.undoUnitList.Count > 100)
			{
				this.undoUnitList.RemoveAt(0);
			}
			this.undoUnitList.Add(unit);
			this.currentPos = this.undoUnitList.Count;
			this.UpdateUndoRedoMenuCommandsStatus();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006096 File Offset: 0x00004296
		protected override void DiscardUndoUnit(UndoEngine.UndoUnit unit)
		{
			this.undoUnitList.Remove(unit);
			this.currentPos--;
			base.DiscardUndoUnit(unit);
			this.UpdateUndoRedoMenuCommandsStatus();
		}

		// Token: 0x04000054 RID: 84
		private const int MaxUndoCount = 100;

		// Token: 0x04000055 RID: 85
		private List<UndoEngine.UndoUnit> undoUnitList = new List<UndoEngine.UndoUnit>();

		// Token: 0x04000056 RID: 86
		private int currentPos;
	}
}
