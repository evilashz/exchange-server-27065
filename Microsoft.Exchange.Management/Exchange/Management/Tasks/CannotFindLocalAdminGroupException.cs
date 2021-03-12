using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E1D RID: 3613
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotFindLocalAdminGroupException : LocalizedException
	{
		// Token: 0x0600A593 RID: 42387 RVA: 0x002863D9 File Offset: 0x002845D9
		public CannotFindLocalAdminGroupException(string dn) : base(Strings.CannotFindLocalAdminGroupException(dn))
		{
			this.dn = dn;
		}

		// Token: 0x0600A594 RID: 42388 RVA: 0x002863EE File Offset: 0x002845EE
		public CannotFindLocalAdminGroupException(string dn, Exception innerException) : base(Strings.CannotFindLocalAdminGroupException(dn), innerException)
		{
			this.dn = dn;
		}

		// Token: 0x0600A595 RID: 42389 RVA: 0x00286404 File Offset: 0x00284604
		protected CannotFindLocalAdminGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dn = (string)info.GetValue("dn", typeof(string));
		}

		// Token: 0x0600A596 RID: 42390 RVA: 0x0028642E File Offset: 0x0028462E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dn", this.dn);
		}

		// Token: 0x1700363C RID: 13884
		// (get) Token: 0x0600A597 RID: 42391 RVA: 0x00286449 File Offset: 0x00284649
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x04005FA2 RID: 24482
		private readonly string dn;
	}
}
