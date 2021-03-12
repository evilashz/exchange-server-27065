using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200049F RID: 1183
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncrementalReseedFailedException : TransientException
	{
		// Token: 0x06002CD1 RID: 11473 RVA: 0x000C017D File Offset: 0x000BE37D
		public IncrementalReseedFailedException(string msg, uint error) : base(ReplayStrings.IncrementalReseedFailedException(msg, error))
		{
			this.msg = msg;
			this.error = error;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000C019A File Offset: 0x000BE39A
		public IncrementalReseedFailedException(string msg, uint error, Exception innerException) : base(ReplayStrings.IncrementalReseedFailedException(msg, error), innerException)
		{
			this.msg = msg;
			this.error = error;
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000C01B8 File Offset: 0x000BE3B8
		protected IncrementalReseedFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
			this.error = (uint)info.GetValue("error", typeof(uint));
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000C020D File Offset: 0x000BE40D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x000C0239 File Offset: 0x000BE439
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x000C0241 File Offset: 0x000BE441
		public uint Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040014FC RID: 5372
		private readonly string msg;

		// Token: 0x040014FD RID: 5373
		private readonly uint error;
	}
}
