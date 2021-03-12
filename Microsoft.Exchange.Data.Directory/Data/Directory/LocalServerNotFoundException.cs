using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A8D RID: 2701
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LocalServerNotFoundException : ADOperationException
	{
		// Token: 0x06007F91 RID: 32657 RVA: 0x001A4288 File Offset: 0x001A2488
		public LocalServerNotFoundException(string fqdn) : base(DirectoryStrings.LocalServerNotFound(fqdn))
		{
			this.fqdn = fqdn;
		}

		// Token: 0x06007F92 RID: 32658 RVA: 0x001A429D File Offset: 0x001A249D
		public LocalServerNotFoundException(string fqdn, Exception innerException) : base(DirectoryStrings.LocalServerNotFound(fqdn), innerException)
		{
			this.fqdn = fqdn;
		}

		// Token: 0x06007F93 RID: 32659 RVA: 0x001A42B3 File Offset: 0x001A24B3
		protected LocalServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fqdn = (string)info.GetValue("fqdn", typeof(string));
		}

		// Token: 0x06007F94 RID: 32660 RVA: 0x001A42DD File Offset: 0x001A24DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fqdn", this.fqdn);
		}

		// Token: 0x17002EB4 RID: 11956
		// (get) Token: 0x06007F95 RID: 32661 RVA: 0x001A42F8 File Offset: 0x001A24F8
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x0400558E RID: 21902
		private readonly string fqdn;
	}
}
