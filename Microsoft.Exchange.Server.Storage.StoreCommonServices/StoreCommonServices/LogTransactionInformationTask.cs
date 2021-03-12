using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000099 RID: 153
	internal class LogTransactionInformationTask : ILogTransactionInformation
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x0001E58F File Offset: 0x0001C78F
		public LogTransactionInformationTask()
		{
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001E597 File Offset: 0x0001C797
		public LogTransactionInformationTask(TaskTypeId taskTypeId)
		{
			this.taskTypeId = taskTypeId;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001E5A6 File Offset: 0x0001C7A6
		public TaskTypeId TaskTypeId
		{
			get
			{
				return this.taskTypeId;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001E5AE File Offset: 0x0001C7AE
		public override string ToString()
		{
			return string.Format("Task:\nIdentifier: {0}\n", this.taskTypeId);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001E5C5 File Offset: 0x0001C7C5
		public byte Type()
		{
			return 5;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001E5C8 File Offset: 0x0001C7C8
		public int Serialize(byte[] buffer, int offset)
		{
			int num = offset;
			if (buffer != null)
			{
				buffer[offset] = this.Type();
			}
			offset++;
			if (buffer != null)
			{
				buffer[offset] = (byte)this.taskTypeId;
			}
			offset++;
			return offset - num;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001E5FC File Offset: 0x0001C7FC
		public void Parse(byte[] buffer, ref int offset)
		{
			byte b = buffer[offset++];
			this.taskTypeId = (TaskTypeId)buffer[offset++];
		}

		// Token: 0x040003EC RID: 1004
		private TaskTypeId taskTypeId;
	}
}
