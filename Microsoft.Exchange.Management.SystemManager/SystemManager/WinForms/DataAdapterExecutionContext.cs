using System;
using System.Data;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000099 RID: 153
	internal abstract class DataAdapterExecutionContext : IDisposable
	{
		// Token: 0x060004FD RID: 1277
		public abstract void Open(IUIService service, WorkUnitCollection workUnits, bool enforceViewEntireForest, ResultsLoaderProfile profile);

		// Token: 0x060004FE RID: 1278
		public abstract void Close();

		// Token: 0x060004FF RID: 1279
		public abstract void Execute(AbstractDataTableFiller filler, DataTable table, ResultsLoaderProfile profile);

		// Token: 0x06000500 RID: 1280 RVA: 0x00013772 File Offset: 0x00011972
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00013781 File Offset: 0x00011981
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}
	}
}
