using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200045C RID: 1116
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbOperationException : AmCommonException
	{
		// Token: 0x06002B62 RID: 11106 RVA: 0x000BD53F File Offset: 0x000BB73F
		public AmDbOperationException(string opError) : base(ReplayStrings.AmDbOperationException(opError))
		{
			this.opError = opError;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000BD559 File Offset: 0x000BB759
		public AmDbOperationException(string opError, Exception innerException) : base(ReplayStrings.AmDbOperationException(opError), innerException)
		{
			this.opError = opError;
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000BD574 File Offset: 0x000BB774
		protected AmDbOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.opError = (string)info.GetValue("opError", typeof(string));
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000BD59E File Offset: 0x000BB79E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("opError", this.opError);
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000BD5B9 File Offset: 0x000BB7B9
		public string OpError
		{
			get
			{
				return this.opError;
			}
		}

		// Token: 0x04001499 RID: 5273
		private readonly string opError;
	}
}
