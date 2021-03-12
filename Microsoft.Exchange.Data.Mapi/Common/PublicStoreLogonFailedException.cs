using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000052 RID: 82
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PublicStoreLogonFailedException : MapiLogonFailedException
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000E616 File Offset: 0x0000C816
		public PublicStoreLogonFailedException(string server) : base(Strings.PublicStoreLogonFailedExceptionError(server))
		{
			this.server = server;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000E62B File Offset: 0x0000C82B
		public PublicStoreLogonFailedException(string server, Exception innerException) : base(Strings.PublicStoreLogonFailedExceptionError(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000E641 File Offset: 0x0000C841
		protected PublicStoreLogonFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000E66B File Offset: 0x0000C86B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000E686 File Offset: 0x0000C886
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x040001A2 RID: 418
		private readonly string server;
	}
}
