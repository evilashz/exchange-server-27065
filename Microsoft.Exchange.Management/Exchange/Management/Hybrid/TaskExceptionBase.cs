using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000902 RID: 2306
	[Serializable]
	internal abstract class TaskExceptionBase : LocalizedException
	{
		// Token: 0x060051E1 RID: 20961 RVA: 0x00152E3D File Offset: 0x0015103D
		public TaskExceptionBase(string exceptionSubtask, string taskName, Exception innerException, IEnumerable<LocalizedString> exceptionErrors) : base(new LocalizedString(taskName), innerException)
		{
			this.Subtask = exceptionSubtask;
			this.Errors = exceptionErrors;
		}

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x060051E2 RID: 20962 RVA: 0x00152E5B File Offset: 0x0015105B
		// (set) Token: 0x060051E3 RID: 20963 RVA: 0x00152E63 File Offset: 0x00151063
		public IEnumerable<LocalizedString> Errors { get; private set; }

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x060051E4 RID: 20964 RVA: 0x00152E6C File Offset: 0x0015106C
		// (set) Token: 0x060051E5 RID: 20965 RVA: 0x00152E74 File Offset: 0x00151074
		public string Subtask { get; private set; }

		// Token: 0x060051E6 RID: 20966 RVA: 0x00152E80 File Offset: 0x00151080
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(HybridStrings.ErrorTaskExceptionTemplate(this.Subtask, this.Message));
			if (this.Errors != null && this.Errors.Count<LocalizedString>() > 0)
			{
				stringBuilder.AppendLine();
				foreach (LocalizedString value in this.Errors)
				{
					stringBuilder.AppendLine(value);
				}
			}
			for (Exception innerException = base.InnerException; innerException != null; innerException = innerException.InnerException)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(innerException.Message);
				if (innerException.InnerException == null)
				{
					stringBuilder.AppendLine(innerException.StackTrace);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
