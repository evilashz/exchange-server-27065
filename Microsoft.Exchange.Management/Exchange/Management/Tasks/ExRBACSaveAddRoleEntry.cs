using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001003 RID: 4099
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExRBACSaveAddRoleEntry : LocalizedException
	{
		// Token: 0x0600AECA RID: 44746 RVA: 0x00293657 File Offset: 0x00291857
		public ExRBACSaveAddRoleEntry(string entryName, string roleId, string error) : base(Strings.ExRBACSaveAddRoleEntry(entryName, roleId, error))
		{
			this.entryName = entryName;
			this.roleId = roleId;
			this.error = error;
		}

		// Token: 0x0600AECB RID: 44747 RVA: 0x0029367C File Offset: 0x0029187C
		public ExRBACSaveAddRoleEntry(string entryName, string roleId, string error, Exception innerException) : base(Strings.ExRBACSaveAddRoleEntry(entryName, roleId, error), innerException)
		{
			this.entryName = entryName;
			this.roleId = roleId;
			this.error = error;
		}

		// Token: 0x0600AECC RID: 44748 RVA: 0x002936A4 File Offset: 0x002918A4
		protected ExRBACSaveAddRoleEntry(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.entryName = (string)info.GetValue("entryName", typeof(string));
			this.roleId = (string)info.GetValue("roleId", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600AECD RID: 44749 RVA: 0x00293719 File Offset: 0x00291919
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("entryName", this.entryName);
			info.AddValue("roleId", this.roleId);
			info.AddValue("error", this.error);
		}

		// Token: 0x170037DB RID: 14299
		// (get) Token: 0x0600AECE RID: 44750 RVA: 0x00293756 File Offset: 0x00291956
		public string EntryName
		{
			get
			{
				return this.entryName;
			}
		}

		// Token: 0x170037DC RID: 14300
		// (get) Token: 0x0600AECF RID: 44751 RVA: 0x0029375E File Offset: 0x0029195E
		public string RoleId
		{
			get
			{
				return this.roleId;
			}
		}

		// Token: 0x170037DD RID: 14301
		// (get) Token: 0x0600AED0 RID: 44752 RVA: 0x00293766 File Offset: 0x00291966
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04006141 RID: 24897
		private readonly string entryName;

		// Token: 0x04006142 RID: 24898
		private readonly string roleId;

		// Token: 0x04006143 RID: 24899
		private readonly string error;
	}
}
