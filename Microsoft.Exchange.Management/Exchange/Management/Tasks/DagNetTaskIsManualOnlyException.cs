using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200103D RID: 4157
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagNetTaskIsManualOnlyException : LocalizedException
	{
		// Token: 0x0600AFEA RID: 45034 RVA: 0x00295169 File Offset: 0x00293369
		public DagNetTaskIsManualOnlyException(string taskName, string dagName) : base(Strings.DagNetTaskIsManualOnly(taskName, dagName))
		{
			this.taskName = taskName;
			this.dagName = dagName;
		}

		// Token: 0x0600AFEB RID: 45035 RVA: 0x00295186 File Offset: 0x00293386
		public DagNetTaskIsManualOnlyException(string taskName, string dagName, Exception innerException) : base(Strings.DagNetTaskIsManualOnly(taskName, dagName), innerException)
		{
			this.taskName = taskName;
			this.dagName = dagName;
		}

		// Token: 0x0600AFEC RID: 45036 RVA: 0x002951A4 File Offset: 0x002933A4
		protected DagNetTaskIsManualOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.taskName = (string)info.GetValue("taskName", typeof(string));
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600AFED RID: 45037 RVA: 0x002951F9 File Offset: 0x002933F9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("taskName", this.taskName);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17003813 RID: 14355
		// (get) Token: 0x0600AFEE RID: 45038 RVA: 0x00295225 File Offset: 0x00293425
		public string TaskName
		{
			get
			{
				return this.taskName;
			}
		}

		// Token: 0x17003814 RID: 14356
		// (get) Token: 0x0600AFEF RID: 45039 RVA: 0x0029522D File Offset: 0x0029342D
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x04006179 RID: 24953
		private readonly string taskName;

		// Token: 0x0400617A RID: 24954
		private readonly string dagName;
	}
}
