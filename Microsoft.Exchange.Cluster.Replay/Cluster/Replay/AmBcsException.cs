using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000483 RID: 1155
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmBcsException : AmServerException
	{
		// Token: 0x06002C37 RID: 11319 RVA: 0x000BEF21 File Offset: 0x000BD121
		public AmBcsException(string bcsError) : base(ReplayStrings.AmBcsException(bcsError))
		{
			this.bcsError = bcsError;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000BEF3B File Offset: 0x000BD13B
		public AmBcsException(string bcsError, Exception innerException) : base(ReplayStrings.AmBcsException(bcsError), innerException)
		{
			this.bcsError = bcsError;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000BEF56 File Offset: 0x000BD156
		protected AmBcsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.bcsError = (string)info.GetValue("bcsError", typeof(string));
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000BEF80 File Offset: 0x000BD180
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("bcsError", this.bcsError);
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x000BEF9B File Offset: 0x000BD19B
		public string BcsError
		{
			get
			{
				return this.bcsError;
			}
		}

		// Token: 0x040014D2 RID: 5330
		private readonly string bcsError;
	}
}
