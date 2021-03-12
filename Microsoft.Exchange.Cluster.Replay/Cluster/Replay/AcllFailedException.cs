using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000425 RID: 1061
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllFailedException : TransientException
	{
		// Token: 0x06002A37 RID: 10807 RVA: 0x000BB21B File Offset: 0x000B941B
		public AcllFailedException(string error) : base(ReplayStrings.AcllFailedException(error))
		{
			this.error = error;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000BB230 File Offset: 0x000B9430
		public AcllFailedException(string error, Exception innerException) : base(ReplayStrings.AcllFailedException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000BB246 File Offset: 0x000B9446
		protected AcllFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000BB270 File Offset: 0x000B9470
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002A3B RID: 10811 RVA: 0x000BB28B File Offset: 0x000B948B
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400144A RID: 5194
		private readonly string error;
	}
}
