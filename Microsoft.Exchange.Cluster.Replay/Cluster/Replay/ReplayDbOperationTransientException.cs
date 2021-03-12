using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003EA RID: 1002
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayDbOperationTransientException : TaskServerTransientException
	{
		// Token: 0x06002902 RID: 10498 RVA: 0x000B8F39 File Offset: 0x000B7139
		public ReplayDbOperationTransientException(string opError) : base(ReplayStrings.ReplayDbOperationTransientException(opError))
		{
			this.opError = opError;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000B8F53 File Offset: 0x000B7153
		public ReplayDbOperationTransientException(string opError, Exception innerException) : base(ReplayStrings.ReplayDbOperationTransientException(opError), innerException)
		{
			this.opError = opError;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000B8F6E File Offset: 0x000B716E
		protected ReplayDbOperationTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.opError = (string)info.GetValue("opError", typeof(string));
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000B8F98 File Offset: 0x000B7198
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("opError", this.opError);
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x000B8FB3 File Offset: 0x000B71B3
		public string OpError
		{
			get
			{
				return this.opError;
			}
		}

		// Token: 0x04001401 RID: 5121
		private readonly string opError;
	}
}
