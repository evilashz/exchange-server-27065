using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000056 RID: 86
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class WorkItemNotFoundException : LocalizedException
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00011459 File Offset: 0x0000F659
		public WorkItemNotFoundException(string workitemId) : base(Strings.WorkItemNotFoundException(workitemId))
		{
			this.workitemId = workitemId;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001146E File Offset: 0x0000F66E
		public WorkItemNotFoundException(string workitemId, Exception innerException) : base(Strings.WorkItemNotFoundException(workitemId), innerException)
		{
			this.workitemId = workitemId;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00011484 File Offset: 0x0000F684
		protected WorkItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.workitemId = (string)info.GetValue("workitemId", typeof(string));
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000114AE File Offset: 0x0000F6AE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("workitemId", this.workitemId);
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000114C9 File Offset: 0x0000F6C9
		public string WorkitemId
		{
			get
			{
				return this.workitemId;
			}
		}

		// Token: 0x040001C6 RID: 454
		private readonly string workitemId;
	}
}
