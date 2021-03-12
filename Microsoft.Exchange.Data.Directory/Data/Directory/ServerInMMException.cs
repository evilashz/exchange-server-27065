using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A5E RID: 2654
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerInMMException : SuitabilityException
	{
		// Token: 0x06007ED4 RID: 32468 RVA: 0x001A3AF6 File Offset: 0x001A1CF6
		public ServerInMMException(string fqdn) : base(DirectoryStrings.ErrorIsServerInMaintenanceMode(fqdn))
		{
			this.fqdn = fqdn;
		}

		// Token: 0x06007ED5 RID: 32469 RVA: 0x001A3B0B File Offset: 0x001A1D0B
		public ServerInMMException(string fqdn, Exception innerException) : base(DirectoryStrings.ErrorIsServerInMaintenanceMode(fqdn), innerException)
		{
			this.fqdn = fqdn;
		}

		// Token: 0x06007ED6 RID: 32470 RVA: 0x001A3B21 File Offset: 0x001A1D21
		protected ServerInMMException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fqdn = (string)info.GetValue("fqdn", typeof(string));
		}

		// Token: 0x06007ED7 RID: 32471 RVA: 0x001A3B4B File Offset: 0x001A1D4B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fqdn", this.fqdn);
		}

		// Token: 0x17002EB3 RID: 11955
		// (get) Token: 0x06007ED8 RID: 32472 RVA: 0x001A3B66 File Offset: 0x001A1D66
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x0400558D RID: 21901
		private readonly string fqdn;
	}
}
