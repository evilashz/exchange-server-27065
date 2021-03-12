using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200013C RID: 316
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PartialStepsOverLimitException : Exception
	{
		// Token: 0x06000D01 RID: 3329 RVA: 0x00051BA9 File Offset: 0x0004FDA9
		public PartialStepsOverLimitException(string partialStep, int limit) : base(Strings.PartialStepCountExceededException(partialStep, limit))
		{
			this.partialStep = partialStep;
			this.limit = limit;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00051BCB File Offset: 0x0004FDCB
		public PartialStepsOverLimitException(string partialStep, int limit, Exception innerException) : base(Strings.PartialStepCountExceededException(partialStep, limit), innerException)
		{
			this.partialStep = partialStep;
			this.limit = limit;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00051BF0 File Offset: 0x0004FDF0
		protected PartialStepsOverLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.partialStep = (string)info.GetValue("partialStep", typeof(string));
			this.limit = (int)info.GetValue("limit", typeof(int));
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00051C45 File Offset: 0x0004FE45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("partialStep", this.partialStep);
			info.AddValue("limit", this.limit);
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00051C71 File Offset: 0x0004FE71
		public string PartialStep
		{
			get
			{
				return this.partialStep;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00051C79 File Offset: 0x0004FE79
		public int Limit
		{
			get
			{
				return this.limit;
			}
		}

		// Token: 0x04000837 RID: 2103
		private readonly string partialStep;

		// Token: 0x04000838 RID: 2104
		private readonly int limit;
	}
}
