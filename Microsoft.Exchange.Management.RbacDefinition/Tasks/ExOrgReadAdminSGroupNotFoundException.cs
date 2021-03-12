using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.RbacDefinition;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000033 RID: 51
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExOrgReadAdminSGroupNotFoundException : LocalizedException
	{
		// Token: 0x060000AE RID: 174 RVA: 0x0004960D File Offset: 0x0004780D
		public ExOrgReadAdminSGroupNotFoundException(Guid guid) : base(Strings.ExOrgReadAdminSGroupNotFoundException(guid))
		{
			this.guid = guid;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00049622 File Offset: 0x00047822
		public ExOrgReadAdminSGroupNotFoundException(Guid guid, Exception innerException) : base(Strings.ExOrgReadAdminSGroupNotFoundException(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00049638 File Offset: 0x00047838
		protected ExOrgReadAdminSGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00049662 File Offset: 0x00047862
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00049682 File Offset: 0x00047882
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04000059 RID: 89
		private readonly Guid guid;
	}
}
