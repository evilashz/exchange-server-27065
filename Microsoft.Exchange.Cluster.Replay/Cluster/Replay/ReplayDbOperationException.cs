using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003E9 RID: 1001
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayDbOperationException : TaskServerException
	{
		// Token: 0x060028FD RID: 10493 RVA: 0x000B8EB7 File Offset: 0x000B70B7
		public ReplayDbOperationException(string opError) : base(ReplayStrings.ReplayDbOperationException(opError))
		{
			this.opError = opError;
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000B8ED1 File Offset: 0x000B70D1
		public ReplayDbOperationException(string opError, Exception innerException) : base(ReplayStrings.ReplayDbOperationException(opError), innerException)
		{
			this.opError = opError;
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000B8EEC File Offset: 0x000B70EC
		protected ReplayDbOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.opError = (string)info.GetValue("opError", typeof(string));
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000B8F16 File Offset: 0x000B7116
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("opError", this.opError);
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x000B8F31 File Offset: 0x000B7131
		public string OpError
		{
			get
			{
				return this.opError;
			}
		}

		// Token: 0x04001400 RID: 5120
		private readonly string opError;
	}
}
