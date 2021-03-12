using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F05 RID: 3845
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotTransportServerException : LocalizedException
	{
		// Token: 0x0600AA02 RID: 43522 RVA: 0x0028CA72 File Offset: 0x0028AC72
		public NotTransportServerException(string fqdn) : base(Strings.NotTransportServer(fqdn))
		{
			this.fqdn = fqdn;
		}

		// Token: 0x0600AA03 RID: 43523 RVA: 0x0028CA87 File Offset: 0x0028AC87
		public NotTransportServerException(string fqdn, Exception innerException) : base(Strings.NotTransportServer(fqdn), innerException)
		{
			this.fqdn = fqdn;
		}

		// Token: 0x0600AA04 RID: 43524 RVA: 0x0028CA9D File Offset: 0x0028AC9D
		protected NotTransportServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fqdn = (string)info.GetValue("fqdn", typeof(string));
		}

		// Token: 0x0600AA05 RID: 43525 RVA: 0x0028CAC7 File Offset: 0x0028ACC7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fqdn", this.fqdn);
		}

		// Token: 0x1700370B RID: 14091
		// (get) Token: 0x0600AA06 RID: 43526 RVA: 0x0028CAE2 File Offset: 0x0028ACE2
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x04006071 RID: 24689
		private readonly string fqdn;
	}
}
