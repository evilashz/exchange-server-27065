using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EDC RID: 3804
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuspendWhenReadyToCompleteCannotBeUsedOnAutoResumeJobsException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A92B RID: 43307 RVA: 0x0028B3AD File Offset: 0x002895AD
		public SuspendWhenReadyToCompleteCannotBeUsedOnAutoResumeJobsException(string name) : base(Strings.ErrorSuspendWhenReadyToCompleteCannotBeUsedOnAutoResumeJobs(name))
		{
			this.name = name;
		}

		// Token: 0x0600A92C RID: 43308 RVA: 0x0028B3C2 File Offset: 0x002895C2
		public SuspendWhenReadyToCompleteCannotBeUsedOnAutoResumeJobsException(string name, Exception innerException) : base(Strings.ErrorSuspendWhenReadyToCompleteCannotBeUsedOnAutoResumeJobs(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600A92D RID: 43309 RVA: 0x0028B3D8 File Offset: 0x002895D8
		protected SuspendWhenReadyToCompleteCannotBeUsedOnAutoResumeJobsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600A92E RID: 43310 RVA: 0x0028B402 File Offset: 0x00289602
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170036D8 RID: 14040
		// (get) Token: 0x0600A92F RID: 43311 RVA: 0x0028B41D File Offset: 0x0028961D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400603E RID: 24638
		private readonly string name;
	}
}
