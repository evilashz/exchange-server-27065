using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011CF RID: 4559
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SUC_NotMailboxServer : LocalizedException
	{
		// Token: 0x0600B8ED RID: 47341 RVA: 0x002A5369 File Offset: 0x002A3569
		public SUC_NotMailboxServer() : base(Strings.NotMailboxServer)
		{
		}

		// Token: 0x0600B8EE RID: 47342 RVA: 0x002A5376 File Offset: 0x002A3576
		public SUC_NotMailboxServer(Exception innerException) : base(Strings.NotMailboxServer, innerException)
		{
		}

		// Token: 0x0600B8EF RID: 47343 RVA: 0x002A5384 File Offset: 0x002A3584
		protected SUC_NotMailboxServer(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8F0 RID: 47344 RVA: 0x002A538E File Offset: 0x002A358E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
