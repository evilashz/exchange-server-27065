using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.RbacDefinition;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000034 RID: 52
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExRbacRoleGroupNotFoundException : LocalizedException
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x0004968A File Offset: 0x0004788A
		public ExRbacRoleGroupNotFoundException(Guid guid, string groupName) : base(Strings.ExRbacRoleGroupNotFoundException(guid, groupName))
		{
			this.guid = guid;
			this.groupName = groupName;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000496A7 File Offset: 0x000478A7
		public ExRbacRoleGroupNotFoundException(Guid guid, string groupName, Exception innerException) : base(Strings.ExRbacRoleGroupNotFoundException(guid, groupName), innerException)
		{
			this.guid = guid;
			this.groupName = groupName;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000496C8 File Offset: 0x000478C8
		protected ExRbacRoleGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
			this.groupName = (string)info.GetValue("groupName", typeof(string));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0004971D File Offset: 0x0004791D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
			info.AddValue("groupName", this.groupName);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0004974E File Offset: 0x0004794E
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00049756 File Offset: 0x00047956
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x0400005A RID: 90
		private readonly Guid guid;

		// Token: 0x0400005B RID: 91
		private readonly string groupName;
	}
}
