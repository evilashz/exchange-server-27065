using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AC3 RID: 2755
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TenantIsRelocatedException : ADTransientException
	{
		// Token: 0x0600808B RID: 32907 RVA: 0x001A5626 File Offset: 0x001A3826
		public TenantIsRelocatedException(string dn) : base(DirectoryStrings.TenantIsRelocatedException(dn))
		{
			this.dn = dn;
		}

		// Token: 0x0600808C RID: 32908 RVA: 0x001A563B File Offset: 0x001A383B
		public TenantIsRelocatedException(string dn, Exception innerException) : base(DirectoryStrings.TenantIsRelocatedException(dn), innerException)
		{
			this.dn = dn;
		}

		// Token: 0x0600808D RID: 32909 RVA: 0x001A5651 File Offset: 0x001A3851
		protected TenantIsRelocatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dn = (string)info.GetValue("dn", typeof(string));
		}

		// Token: 0x0600808E RID: 32910 RVA: 0x001A567B File Offset: 0x001A387B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dn", this.dn);
		}

		// Token: 0x17002ED6 RID: 11990
		// (get) Token: 0x0600808F RID: 32911 RVA: 0x001A5696 File Offset: 0x001A3896
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x040055B0 RID: 21936
		private readonly string dn;
	}
}
