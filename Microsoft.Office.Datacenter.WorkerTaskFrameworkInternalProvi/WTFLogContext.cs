using System;
using System.Text;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200002E RID: 46
	internal struct WTFLogContext
	{
		// Token: 0x06000314 RID: 788 RVA: 0x0000AF5C File Offset: 0x0000915C
		public override string ToString()
		{
			int capacity = this.WorkItemInstance.Length + this.WorkItemType.Length + this.WorkItemDefinition.Length + this.WorkItemCreatedBy.Length + this.WorkItemResult.Length + this.ComponentName.Length + this.ProcessAndThread.Length + this.CallerMethod.Length + this.CallerSourceLine.Length + this.Message.Length + 12;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			stringBuilder.Append(this.WorkItemInstance);
			stringBuilder.Append('/');
			stringBuilder.Append(this.WorkItemType);
			stringBuilder.Append('/');
			stringBuilder.Append(this.WorkItemDefinition);
			stringBuilder.Append('/');
			stringBuilder.Append(this.WorkItemCreatedBy);
			stringBuilder.Append('/');
			stringBuilder.Append(this.WorkItemResult);
			stringBuilder.Append('/');
			stringBuilder.Append(this.ComponentName);
			stringBuilder.Append('/');
			stringBuilder.Append(this.ProcessAndThread);
			stringBuilder.Append('/');
			stringBuilder.Append(this.CallerMethod);
			stringBuilder.Append('/');
			stringBuilder.Append(this.CallerSourceLine);
			stringBuilder.Append('/');
			stringBuilder.Append(this.Message);
			return stringBuilder.ToString();
		}

		// Token: 0x04000119 RID: 281
		public string WorkItemInstance;

		// Token: 0x0400011A RID: 282
		public string WorkItemType;

		// Token: 0x0400011B RID: 283
		public string WorkItemDefinition;

		// Token: 0x0400011C RID: 284
		public string WorkItemCreatedBy;

		// Token: 0x0400011D RID: 285
		public string WorkItemResult;

		// Token: 0x0400011E RID: 286
		public string ComponentName;

		// Token: 0x0400011F RID: 287
		public string ProcessAndThread;

		// Token: 0x04000120 RID: 288
		public string LogLevel;

		// Token: 0x04000121 RID: 289
		public string CallerMethod;

		// Token: 0x04000122 RID: 290
		public string CallerSourceLine;

		// Token: 0x04000123 RID: 291
		public string Message;
	}
}
