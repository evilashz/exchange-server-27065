using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AC5 RID: 2757
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TenantIsLockedDownForRelocationException : ADTransientException
	{
		// Token: 0x06008095 RID: 32917 RVA: 0x001A5716 File Offset: 0x001A3916
		public TenantIsLockedDownForRelocationException(string dn) : base(DirectoryStrings.TenantIsLockedDownForRelocationException(dn))
		{
			this.dn = dn;
		}

		// Token: 0x06008096 RID: 32918 RVA: 0x001A572B File Offset: 0x001A392B
		public TenantIsLockedDownForRelocationException(string dn, Exception innerException) : base(DirectoryStrings.TenantIsLockedDownForRelocationException(dn), innerException)
		{
			this.dn = dn;
		}

		// Token: 0x06008097 RID: 32919 RVA: 0x001A5741 File Offset: 0x001A3941
		protected TenantIsLockedDownForRelocationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dn = (string)info.GetValue("dn", typeof(string));
		}

		// Token: 0x06008098 RID: 32920 RVA: 0x001A576B File Offset: 0x001A396B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dn", this.dn);
		}

		// Token: 0x17002ED8 RID: 11992
		// (get) Token: 0x06008099 RID: 32921 RVA: 0x001A5786 File Offset: 0x001A3986
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x040055B2 RID: 21938
		private readonly string dn;
	}
}
