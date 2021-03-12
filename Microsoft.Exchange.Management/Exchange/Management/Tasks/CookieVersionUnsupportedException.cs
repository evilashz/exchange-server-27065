using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010C9 RID: 4297
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CookieVersionUnsupportedException : LocalizedException
	{
		// Token: 0x0600B2E2 RID: 45794 RVA: 0x0029A74D File Offset: 0x0029894D
		public CookieVersionUnsupportedException(int version) : base(Strings.CookieVersionUnsupportedException(version))
		{
			this.version = version;
		}

		// Token: 0x0600B2E3 RID: 45795 RVA: 0x0029A762 File Offset: 0x00298962
		public CookieVersionUnsupportedException(int version, Exception innerException) : base(Strings.CookieVersionUnsupportedException(version), innerException)
		{
			this.version = version;
		}

		// Token: 0x0600B2E4 RID: 45796 RVA: 0x0029A778 File Offset: 0x00298978
		protected CookieVersionUnsupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.version = (int)info.GetValue("version", typeof(int));
		}

		// Token: 0x0600B2E5 RID: 45797 RVA: 0x0029A7A2 File Offset: 0x002989A2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("version", this.version);
		}

		// Token: 0x170038DB RID: 14555
		// (get) Token: 0x0600B2E6 RID: 45798 RVA: 0x0029A7BD File Offset: 0x002989BD
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x04006241 RID: 25153
		private readonly int version;
	}
}
