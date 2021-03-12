using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E0D RID: 3597
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class E12DomainServersGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600A53D RID: 42301 RVA: 0x00285A3E File Offset: 0x00283C3E
		public E12DomainServersGroupNotFoundException(string dn, string dc) : base(Strings.E12DomainServersGroupNotFoundException(dn, dc))
		{
			this.dn = dn;
			this.dc = dc;
		}

		// Token: 0x0600A53E RID: 42302 RVA: 0x00285A5B File Offset: 0x00283C5B
		public E12DomainServersGroupNotFoundException(string dn, string dc, Exception innerException) : base(Strings.E12DomainServersGroupNotFoundException(dn, dc), innerException)
		{
			this.dn = dn;
			this.dc = dc;
		}

		// Token: 0x0600A53F RID: 42303 RVA: 0x00285A7C File Offset: 0x00283C7C
		protected E12DomainServersGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dn = (string)info.GetValue("dn", typeof(string));
			this.dc = (string)info.GetValue("dc", typeof(string));
		}

		// Token: 0x0600A540 RID: 42304 RVA: 0x00285AD1 File Offset: 0x00283CD1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dn", this.dn);
			info.AddValue("dc", this.dc);
		}

		// Token: 0x17003626 RID: 13862
		// (get) Token: 0x0600A541 RID: 42305 RVA: 0x00285AFD File Offset: 0x00283CFD
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x17003627 RID: 13863
		// (get) Token: 0x0600A542 RID: 42306 RVA: 0x00285B05 File Offset: 0x00283D05
		public string Dc
		{
			get
			{
				return this.dc;
			}
		}

		// Token: 0x04005F8C RID: 24460
		private readonly string dn;

		// Token: 0x04005F8D RID: 24461
		private readonly string dc;
	}
}
