using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014C RID: 332
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationObjectNotFoundInADException : StoragePermanentException
	{
		// Token: 0x060015E6 RID: 5606 RVA: 0x0006E9D3 File Offset: 0x0006CBD3
		public MigrationObjectNotFoundInADException(string legDN, string server) : base(Strings.MigrationObjectNotFoundInADError(legDN, server))
		{
			this.legDN = legDN;
			this.server = server;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0006E9F0 File Offset: 0x0006CBF0
		public MigrationObjectNotFoundInADException(string legDN, string server, Exception innerException) : base(Strings.MigrationObjectNotFoundInADError(legDN, server), innerException)
		{
			this.legDN = legDN;
			this.server = server;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0006EA10 File Offset: 0x0006CC10
		protected MigrationObjectNotFoundInADException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.legDN = (string)info.GetValue("legDN", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0006EA65 File Offset: 0x0006CC65
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("legDN", this.legDN);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0006EA91 File Offset: 0x0006CC91
		public string LegDN
		{
			get
			{
				return this.legDN;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x0006EA99 File Offset: 0x0006CC99
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04000ADE RID: 2782
		private readonly string legDN;

		// Token: 0x04000ADF RID: 2783
		private readonly string server;
	}
}
