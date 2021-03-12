using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E0B RID: 3595
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AOGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600A532 RID: 42290 RVA: 0x002858F5 File Offset: 0x00283AF5
		public AOGroupNotFoundException(string dn) : base(Strings.AOGroupNotFoundException(dn))
		{
			this.dn = dn;
		}

		// Token: 0x0600A533 RID: 42291 RVA: 0x0028590A File Offset: 0x00283B0A
		public AOGroupNotFoundException(string dn, Exception innerException) : base(Strings.AOGroupNotFoundException(dn), innerException)
		{
			this.dn = dn;
		}

		// Token: 0x0600A534 RID: 42292 RVA: 0x00285920 File Offset: 0x00283B20
		protected AOGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dn = (string)info.GetValue("dn", typeof(string));
		}

		// Token: 0x0600A535 RID: 42293 RVA: 0x0028594A File Offset: 0x00283B4A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dn", this.dn);
		}

		// Token: 0x17003623 RID: 13859
		// (get) Token: 0x0600A536 RID: 42294 RVA: 0x00285965 File Offset: 0x00283B65
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x04005F89 RID: 24457
		private readonly string dn;
	}
}
