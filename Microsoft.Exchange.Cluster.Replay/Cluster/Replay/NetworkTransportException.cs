using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003AE RID: 942
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkTransportException : TransientException
	{
		// Token: 0x060027B8 RID: 10168 RVA: 0x000B6739 File Offset: 0x000B4939
		public NetworkTransportException(string err) : base(ReplayStrings.NetworkTransportError(err))
		{
			this.err = err;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000B674E File Offset: 0x000B494E
		public NetworkTransportException(string err, Exception innerException) : base(ReplayStrings.NetworkTransportError(err), innerException)
		{
			this.err = err;
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000B6764 File Offset: 0x000B4964
		protected NetworkTransportException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.err = (string)info.GetValue("err", typeof(string));
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000B678E File Offset: 0x000B498E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("err", this.err);
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x000B67A9 File Offset: 0x000B49A9
		public string Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x040013A7 RID: 5031
		private readonly string err;
	}
}
