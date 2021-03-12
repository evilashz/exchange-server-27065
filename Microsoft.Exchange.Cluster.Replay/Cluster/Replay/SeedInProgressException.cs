using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200043A RID: 1082
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeedInProgressException : SeederServerException
	{
		// Token: 0x06002AB1 RID: 10929 RVA: 0x000BC199 File Offset: 0x000BA399
		public SeedInProgressException(string errMessage) : base(ReplayStrings.SeedInProgressException(errMessage))
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000BC1B3 File Offset: 0x000BA3B3
		public SeedInProgressException(string errMessage, Exception innerException) : base(ReplayStrings.SeedInProgressException(errMessage), innerException)
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000BC1CE File Offset: 0x000BA3CE
		protected SeedInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000BC1F8 File Offset: 0x000BA3F8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x000BC213 File Offset: 0x000BA413
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x04001470 RID: 5232
		private readonly string errMessage;
	}
}
