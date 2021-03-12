using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AC4 RID: 2756
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TenantIsArrivingException : ADTransientException
	{
		// Token: 0x06008090 RID: 32912 RVA: 0x001A569E File Offset: 0x001A389E
		public TenantIsArrivingException(string dn) : base(DirectoryStrings.TenantIsArrivingException(dn))
		{
			this.dn = dn;
		}

		// Token: 0x06008091 RID: 32913 RVA: 0x001A56B3 File Offset: 0x001A38B3
		public TenantIsArrivingException(string dn, Exception innerException) : base(DirectoryStrings.TenantIsArrivingException(dn), innerException)
		{
			this.dn = dn;
		}

		// Token: 0x06008092 RID: 32914 RVA: 0x001A56C9 File Offset: 0x001A38C9
		protected TenantIsArrivingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dn = (string)info.GetValue("dn", typeof(string));
		}

		// Token: 0x06008093 RID: 32915 RVA: 0x001A56F3 File Offset: 0x001A38F3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dn", this.dn);
		}

		// Token: 0x17002ED7 RID: 11991
		// (get) Token: 0x06008094 RID: 32916 RVA: 0x001A570E File Offset: 0x001A390E
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x040055B1 RID: 21937
		private readonly string dn;
	}
}
