using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011D8 RID: 4568
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoMailboxServersFound : LocalizedException
	{
		// Token: 0x0600B915 RID: 47381 RVA: 0x002A5640 File Offset: 0x002A3840
		public NoMailboxServersFound() : base(Strings.NoMailboxServersFound)
		{
		}

		// Token: 0x0600B916 RID: 47382 RVA: 0x002A564D File Offset: 0x002A384D
		public NoMailboxServersFound(Exception innerException) : base(Strings.NoMailboxServersFound, innerException)
		{
		}

		// Token: 0x0600B917 RID: 47383 RVA: 0x002A565B File Offset: 0x002A385B
		protected NoMailboxServersFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B918 RID: 47384 RVA: 0x002A5665 File Offset: 0x002A3865
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
