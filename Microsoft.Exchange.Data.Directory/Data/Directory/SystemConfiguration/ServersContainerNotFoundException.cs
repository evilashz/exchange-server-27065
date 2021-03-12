using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A98 RID: 2712
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServersContainerNotFoundException : MandatoryContainerNotFoundException
	{
		// Token: 0x06007FC6 RID: 32710 RVA: 0x001A472C File Offset: 0x001A292C
		public ServersContainerNotFoundException() : base(DirectoryStrings.ServersContainerNotFoundException)
		{
		}

		// Token: 0x06007FC7 RID: 32711 RVA: 0x001A4739 File Offset: 0x001A2939
		public ServersContainerNotFoundException(Exception innerException) : base(DirectoryStrings.ServersContainerNotFoundException, innerException)
		{
		}

		// Token: 0x06007FC8 RID: 32712 RVA: 0x001A4747 File Offset: 0x001A2947
		protected ServersContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007FC9 RID: 32713 RVA: 0x001A4751 File Offset: 0x001A2951
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
