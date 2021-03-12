using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011F2 RID: 4594
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_RemoteEndDisconnected : LocalizedException
	{
		// Token: 0x0600B997 RID: 47511 RVA: 0x002A62C1 File Offset: 0x002A44C1
		public TUC_RemoteEndDisconnected() : base(Strings.RemoteEndDisconnected)
		{
		}

		// Token: 0x0600B998 RID: 47512 RVA: 0x002A62CE File Offset: 0x002A44CE
		public TUC_RemoteEndDisconnected(Exception innerException) : base(Strings.RemoteEndDisconnected, innerException)
		{
		}

		// Token: 0x0600B999 RID: 47513 RVA: 0x002A62DC File Offset: 0x002A44DC
		protected TUC_RemoteEndDisconnected(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B99A RID: 47514 RVA: 0x002A62E6 File Offset: 0x002A44E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
