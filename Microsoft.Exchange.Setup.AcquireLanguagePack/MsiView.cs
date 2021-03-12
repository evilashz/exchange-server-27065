using System;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000013 RID: 19
	internal sealed class MsiView : MsiBase
	{
		// Token: 0x06000051 RID: 81 RVA: 0x0000305D File Offset: 0x0000125D
		public MsiView(MsiDatabase database, string query)
		{
			this.OpenView(database, query);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000306D File Offset: 0x0000126D
		public MsiRecord FetchNextRecord()
		{
			return new MsiRecord(this);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003078 File Offset: 0x00001278
		private void OpenView(MsiDatabase database, string query)
		{
			ValidationHelper.ThrowIfNull(database, "database");
			ValidationHelper.ThrowIfNullOrEmpty(query, "query");
			SafeMsiHandle safeMsiHandle;
			uint num = MsiNativeMethods.DatabaseOpenView(database.Handle, query, out safeMsiHandle);
			MsiHelper.ThrowIfNotSuccess(num);
			num = MsiNativeMethods.ViewExecute(safeMsiHandle, IntPtr.Zero);
			if (num != 0U)
			{
				safeMsiHandle.Dispose();
				throw new MsiException(num);
			}
			base.Handle = safeMsiHandle;
		}
	}
}
