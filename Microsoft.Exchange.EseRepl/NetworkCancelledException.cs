using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000049 RID: 73
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkCancelledException : NetworkTransportException
	{
		// Token: 0x0600025F RID: 607 RVA: 0x000090C9 File Offset: 0x000072C9
		public NetworkCancelledException() : base(Strings.NetworkCancelled)
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000090DB File Offset: 0x000072DB
		public NetworkCancelledException(Exception innerException) : base(Strings.NetworkCancelled, innerException)
		{
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000090EE File Offset: 0x000072EE
		protected NetworkCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000090F8 File Offset: 0x000072F8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
