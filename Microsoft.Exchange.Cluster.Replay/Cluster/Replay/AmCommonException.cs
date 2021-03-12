using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000459 RID: 1113
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmCommonException : AmServerException
	{
		// Token: 0x06002B54 RID: 11092 RVA: 0x000BD402 File Offset: 0x000BB602
		public AmCommonException(string error) : base(ReplayStrings.AmCommonException(error))
		{
			this.error = error;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000BD41C File Offset: 0x000BB61C
		public AmCommonException(string error, Exception innerException) : base(ReplayStrings.AmCommonException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000BD437 File Offset: 0x000BB637
		protected AmCommonException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000BD461 File Offset: 0x000BB661
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x000BD47C File Offset: 0x000BB67C
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001497 RID: 5271
		private readonly string error;
	}
}
