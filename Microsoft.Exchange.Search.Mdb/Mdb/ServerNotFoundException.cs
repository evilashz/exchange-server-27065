using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000038 RID: 56
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ServerNotFoundException : ComponentFailedPermanentException
	{
		// Token: 0x060001CD RID: 461 RVA: 0x0000D287 File Offset: 0x0000B487
		public ServerNotFoundException(string fqdn) : base(Strings.ServerNotFound(fqdn))
		{
			this.fqdn = fqdn;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000D29C File Offset: 0x0000B49C
		public ServerNotFoundException(string fqdn, Exception innerException) : base(Strings.ServerNotFound(fqdn), innerException)
		{
			this.fqdn = fqdn;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000D2B2 File Offset: 0x0000B4B2
		protected ServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fqdn = (string)info.GetValue("fqdn", typeof(string));
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fqdn", this.fqdn);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000D2F7 File Offset: 0x0000B4F7
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x04000135 RID: 309
		private readonly string fqdn;
	}
}
