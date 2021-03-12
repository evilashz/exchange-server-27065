using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000566 RID: 1382
	public class WriteFileActivity : Activity
	{
		// Token: 0x06004062 RID: 16482 RVA: 0x000C4764 File Offset: 0x000C2964
		public WriteFileActivity() : base("WriteFileActivity")
		{
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x000C477C File Offset: 0x000C297C
		protected WriteFileActivity(WriteFileActivity activity) : base(activity)
		{
			this.InputVariable = activity.InputVariable;
			this.OutputFileNameVariable = activity.OutputFileNameVariable;
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x000C47A8 File Offset: 0x000C29A8
		public override Activity Clone()
		{
			return new WriteFileActivity(this);
		}

		// Token: 0x170024F5 RID: 9461
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x000C47B0 File Offset: 0x000C29B0
		// (set) Token: 0x06004066 RID: 16486 RVA: 0x000C47B8 File Offset: 0x000C29B8
		[DDIMandatoryValue]
		public string InputVariable { get; set; }

		// Token: 0x170024F6 RID: 9462
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x000C47C1 File Offset: 0x000C29C1
		// (set) Token: 0x06004068 RID: 16488 RVA: 0x000C47C9 File Offset: 0x000C29C9
		[DDIMandatoryValue]
		public string OutputFileNameVariable { get; set; }

		// Token: 0x06004069 RID: 16489 RVA: 0x000C47D4 File Offset: 0x000C29D4
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			DataRow dataRow = dataTable.Rows[0];
			string value = (string)input[this.InputVariable];
			string path = (string)input[this.OutputFileNameVariable];
			RunResult runResult = new RunResult();
			try
			{
				runResult.ErrorOccur = true;
				using (StreamWriter streamWriter = new StreamWriter(File.Open(path, FileMode.CreateNew)))
				{
					streamWriter.WriteLine(value);
				}
				runResult.ErrorOccur = false;
			}
			catch (UnauthorizedAccessException e)
			{
				this.HandleFileSaveException(e);
			}
			catch (DirectoryNotFoundException e2)
			{
				this.HandleFileSaveException(e2);
			}
			catch (PathTooLongException e3)
			{
				this.HandleFileSaveException(e3);
			}
			catch (SecurityException e4)
			{
				this.HandleFileSaveException(e4);
			}
			catch (IOException e5)
			{
				this.HandleFileSaveException(e5);
			}
			return runResult;
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x000C48D4 File Offset: 0x000C2AD4
		public override PowerShellResults[] GetStatusReport(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			if (base.ErrorBehavior == ErrorBehavior.SilentlyContinue)
			{
				this.errorRecords.Clear();
			}
			PowerShellResults[] array = new PowerShellResults[]
			{
				new PowerShellResults()
			};
			array[0].ErrorRecords = this.errorRecords.ToArray();
			return array;
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x000C491A File Offset: 0x000C2B1A
		private void HandleFileSaveException(Exception e)
		{
			this.errorRecords.Add(new ErrorRecord(e));
		}

		// Token: 0x04002ADD RID: 10973
		private List<ErrorRecord> errorRecords = new List<ErrorRecord>();
	}
}
