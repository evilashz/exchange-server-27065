using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001005 RID: 4101
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExRBACFailedToUpdateCustomRole : LocalizedException
	{
		// Token: 0x0600AED8 RID: 44760 RVA: 0x00293886 File Offset: 0x00291A86
		public ExRBACFailedToUpdateCustomRole(string roleName, string targetCustomRoleName, string error) : base(Strings.ExRBACFailedToUpdateCustomRole(roleName, targetCustomRoleName, error))
		{
			this.roleName = roleName;
			this.targetCustomRoleName = targetCustomRoleName;
			this.error = error;
		}

		// Token: 0x0600AED9 RID: 44761 RVA: 0x002938AB File Offset: 0x00291AAB
		public ExRBACFailedToUpdateCustomRole(string roleName, string targetCustomRoleName, string error, Exception innerException) : base(Strings.ExRBACFailedToUpdateCustomRole(roleName, targetCustomRoleName, error), innerException)
		{
			this.roleName = roleName;
			this.targetCustomRoleName = targetCustomRoleName;
			this.error = error;
		}

		// Token: 0x0600AEDA RID: 44762 RVA: 0x002938D4 File Offset: 0x00291AD4
		protected ExRBACFailedToUpdateCustomRole(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.roleName = (string)info.GetValue("roleName", typeof(string));
			this.targetCustomRoleName = (string)info.GetValue("targetCustomRoleName", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600AEDB RID: 44763 RVA: 0x00293949 File Offset: 0x00291B49
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("roleName", this.roleName);
			info.AddValue("targetCustomRoleName", this.targetCustomRoleName);
			info.AddValue("error", this.error);
		}

		// Token: 0x170037E1 RID: 14305
		// (get) Token: 0x0600AEDC RID: 44764 RVA: 0x00293986 File Offset: 0x00291B86
		public string RoleName
		{
			get
			{
				return this.roleName;
			}
		}

		// Token: 0x170037E2 RID: 14306
		// (get) Token: 0x0600AEDD RID: 44765 RVA: 0x0029398E File Offset: 0x00291B8E
		public string TargetCustomRoleName
		{
			get
			{
				return this.targetCustomRoleName;
			}
		}

		// Token: 0x170037E3 RID: 14307
		// (get) Token: 0x0600AEDE RID: 44766 RVA: 0x00293996 File Offset: 0x00291B96
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04006147 RID: 24903
		private readonly string roleName;

		// Token: 0x04006148 RID: 24904
		private readonly string targetCustomRoleName;

		// Token: 0x04006149 RID: 24905
		private readonly string error;
	}
}
