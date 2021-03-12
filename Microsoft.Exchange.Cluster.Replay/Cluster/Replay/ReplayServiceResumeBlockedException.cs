using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003F9 RID: 1017
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceResumeBlockedException : TaskServerException
	{
		// Token: 0x06002951 RID: 10577 RVA: 0x000B9839 File Offset: 0x000B7A39
		public ReplayServiceResumeBlockedException(string previousError) : base(ReplayStrings.ReplayServiceResumeBlockedException(previousError))
		{
			this.previousError = previousError;
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000B9853 File Offset: 0x000B7A53
		public ReplayServiceResumeBlockedException(string previousError, Exception innerException) : base(ReplayStrings.ReplayServiceResumeBlockedException(previousError), innerException)
		{
			this.previousError = previousError;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000B986E File Offset: 0x000B7A6E
		protected ReplayServiceResumeBlockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.previousError = (string)info.GetValue("previousError", typeof(string));
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000B9898 File Offset: 0x000B7A98
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("previousError", this.previousError);
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x000B98B3 File Offset: 0x000B7AB3
		public string PreviousError
		{
			get
			{
				return this.previousError;
			}
		}

		// Token: 0x04001414 RID: 5140
		private readonly string previousError;
	}
}
