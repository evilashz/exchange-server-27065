using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Configuration.SQM
{
	// Token: 0x0200026A RID: 618
	internal class SqmModule : ITaskModule, ICriticalFeature
	{
		// Token: 0x0600155F RID: 5471 RVA: 0x0004EECD File Offset: 0x0004D0CD
		public SqmModule(TaskContext context)
		{
			this.taskContext = context;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0004EEE8 File Offset: 0x0004D0E8
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0004EEEB File Offset: 0x0004D0EB
		public void Init(ITaskEvent task)
		{
			task.Release += this.WriteSQMData;
			task.Error += this.AppendSqmErrorRecord;
			this.ResetSQMDataRecord();
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0004EF17 File Offset: 0x0004D117
		public void Dispose()
		{
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0004EF19 File Offset: 0x0004D119
		private void ResetSQMDataRecord()
		{
			this.sqmStartTimeStamp = ExDateTime.Now;
			this.sqmErrors.Clear();
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0004EF34 File Offset: 0x0004D134
		private void AppendSqmErrorRecord(object sender, GenericEventArg<TaskErrorEventArg> e)
		{
			if (e.Data.ExceptionHandled)
			{
				return;
			}
			if (this.sqmErrors.Count < 6)
			{
				LocalizedException ex = e.Data.Exception as LocalizedException;
				this.sqmErrors.Add(new SqmErrorRecord(e.Data.GetType().Name, (ex == null) ? "Unknown" : ex.StringId));
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0004EFA0 File Offset: 0x0004D1A0
		private void WriteSQMData(object sender, EventArgs e)
		{
			TaskInvocationInfo invocationInfo = this.taskContext.InvocationInfo;
			string commandName = invocationInfo.CommandName;
			string[] array = new string[invocationInfo.UserSpecifiedParameters.Keys.Count];
			invocationInfo.UserSpecifiedParameters.Keys.CopyTo(array, 0);
			CmdletSqmSession.Instance.WriteSQMSession(commandName, (array.Length == 0) ? new string[]
			{
				"No bound parameter"
			} : array, invocationInfo.ShellHostName, (uint)(this.taskContext.CurrentObjectIndex + 1), (uint)(ExDateTime.Now - this.sqmStartTimeStamp).Milliseconds, this.sqmErrors.ToArray());
		}

		// Token: 0x04000674 RID: 1652
		private readonly TaskContext taskContext;

		// Token: 0x04000675 RID: 1653
		private ExDateTime sqmStartTimeStamp;

		// Token: 0x04000676 RID: 1654
		private List<SqmErrorRecord> sqmErrors = new List<SqmErrorRecord>(6);
	}
}
