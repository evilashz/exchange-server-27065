using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200007C RID: 124
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidFqdnException : LocalizedException
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x00016680 File Offset: 0x00014880
		public InvalidFqdnException(string fqdn) : base(Strings.InvalidFqdn(fqdn))
		{
			this.fqdn = fqdn;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00016695 File Offset: 0x00014895
		public InvalidFqdnException(string fqdn, Exception innerException) : base(Strings.InvalidFqdn(fqdn), innerException)
		{
			this.fqdn = fqdn;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000166AB File Offset: 0x000148AB
		protected InvalidFqdnException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fqdn = (string)info.GetValue("fqdn", typeof(string));
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x000166D5 File Offset: 0x000148D5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fqdn", this.fqdn);
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x000166F0 File Offset: 0x000148F0
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x040002FC RID: 764
		private readonly string fqdn;
	}
}
