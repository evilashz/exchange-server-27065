using System;
using System.Collections.Generic;
using System.Data;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000A8 RID: 168
	public abstract class FileSaver : Saver
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x00014214 File Offset: 0x00012414
		public FileSaver() : base(string.Empty, string.Empty)
		{
			this.workUnits = new WorkUnitCollection();
			this.workUnit = new WorkUnit(string.Empty, null);
			this.workUnits.Add(this.workUnit);
			this.FilePathParameterName = "FilePath";
			this.savedResults = new List<object>();
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00014274 File Offset: 0x00012474
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0001427C File Offset: 0x0001247C
		[DDIDataColumnExist]
		public string FilePathParameterName { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00014285 File Offset: 0x00012485
		public override List<object> SavedResults
		{
			get
			{
				return this.savedResults;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001428D File Offset: 0x0001248D
		public override object WorkUnits
		{
			get
			{
				return this.workUnits;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00014295 File Offset: 0x00012495
		public override string CommandToRun
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001429C File Offset: 0x0001249C
		public override string ModifiedParametersDescription
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000142A3 File Offset: 0x000124A3
		public override void Cancel()
		{
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000142A5 File Offset: 0x000124A5
		protected void OnStart()
		{
			this.workUnit.Errors.Clear();
			this.workUnit.Status = WorkUnitStatus.InProgress;
			this.workUnit.ExecutedCommandText = this.workUnit.Description;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000142DC File Offset: 0x000124DC
		protected void OnComplete(bool succeeded, Exception exception)
		{
			if (!succeeded)
			{
				int num = LocalizedException.GenerateErrorCode(exception);
				this.workUnit.Errors.Add(new ErrorRecord(exception, num.ToString("X"), ErrorCategory.OperationStopped, null));
			}
			this.workUnit.Status = (succeeded ? WorkUnitStatus.Completed : WorkUnitStatus.Failed);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001432C File Offset: 0x0001252C
		public override void UpdateWorkUnits(DataRow row)
		{
			if (!string.IsNullOrEmpty(base.WorkUnitTextColumn))
			{
				this.workUnit.Text = row[base.WorkUnitTextColumn].ToString();
			}
			if (!string.IsNullOrEmpty(base.WorkUnitIconColumn))
			{
				this.workUnit.Icon = WinformsHelper.GetIconFromIconLibrary(row[base.WorkUnitIconColumn].ToString());
			}
		}

		// Token: 0x040001BD RID: 445
		protected WorkUnit workUnit;

		// Token: 0x040001BE RID: 446
		private WorkUnitCollection workUnits;

		// Token: 0x040001BF RID: 447
		private List<object> savedResults;
	}
}
