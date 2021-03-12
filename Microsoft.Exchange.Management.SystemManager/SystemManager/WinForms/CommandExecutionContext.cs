using System;
using System.Data;
using System.Windows.Forms.Design;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200009A RID: 154
	internal abstract class CommandExecutionContext : IDisposable
	{
		// Token: 0x06000503 RID: 1283
		public abstract void Open(IUIService service);

		// Token: 0x06000504 RID: 1284
		public abstract void Close();

		// Token: 0x06000505 RID: 1285
		public abstract void Execute(TaskProfileBase profile, DataRow row, DataObjectStore store);

		// Token: 0x06000506 RID: 1286 RVA: 0x00013794 File Offset: 0x00011994
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000137A3 File Offset: 0x000119A3
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x000137AE File Offset: 0x000119AE
		public virtual bool ShouldReload
		{
			get
			{
				return true;
			}
		}
	}
}
