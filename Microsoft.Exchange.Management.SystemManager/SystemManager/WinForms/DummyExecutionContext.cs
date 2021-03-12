using System;
using System.Data;
using System.Windows.Forms.Design;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200009B RID: 155
	internal class DummyExecutionContext : CommandExecutionContext
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x000137B9 File Offset: 0x000119B9
		public override void Open(IUIService service)
		{
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000137BB File Offset: 0x000119BB
		public override void Execute(TaskProfileBase profile, DataRow row, DataObjectStore store)
		{
			profile.Run(null, row, store);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000137C6 File Offset: 0x000119C6
		public override void Close()
		{
		}
	}
}
