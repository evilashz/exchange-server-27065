using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E0C RID: 3596
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExRbacRoleGroupInMultiTenantException : LocalizedException
	{
		// Token: 0x0600A537 RID: 42295 RVA: 0x0028596D File Offset: 0x00283B6D
		public ExRbacRoleGroupInMultiTenantException(Guid guid, string groupName) : base(Strings.ExRbacRoleGroupInMultiTenantException(guid, groupName))
		{
			this.guid = guid;
			this.groupName = groupName;
		}

		// Token: 0x0600A538 RID: 42296 RVA: 0x0028598A File Offset: 0x00283B8A
		public ExRbacRoleGroupInMultiTenantException(Guid guid, string groupName, Exception innerException) : base(Strings.ExRbacRoleGroupInMultiTenantException(guid, groupName), innerException)
		{
			this.guid = guid;
			this.groupName = groupName;
		}

		// Token: 0x0600A539 RID: 42297 RVA: 0x002859A8 File Offset: 0x00283BA8
		protected ExRbacRoleGroupInMultiTenantException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
			this.groupName = (string)info.GetValue("groupName", typeof(string));
		}

		// Token: 0x0600A53A RID: 42298 RVA: 0x002859FD File Offset: 0x00283BFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
			info.AddValue("groupName", this.groupName);
		}

		// Token: 0x17003624 RID: 13860
		// (get) Token: 0x0600A53B RID: 42299 RVA: 0x00285A2E File Offset: 0x00283C2E
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17003625 RID: 13861
		// (get) Token: 0x0600A53C RID: 42300 RVA: 0x00285A36 File Offset: 0x00283C36
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x04005F8A RID: 24458
		private readonly Guid guid;

		// Token: 0x04005F8B RID: 24459
		private readonly string groupName;
	}
}
