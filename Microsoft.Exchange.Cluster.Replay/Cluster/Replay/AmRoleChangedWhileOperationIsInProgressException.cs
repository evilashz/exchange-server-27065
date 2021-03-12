using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200047D RID: 1149
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmRoleChangedWhileOperationIsInProgressException : AmDbActionException
	{
		// Token: 0x06002C10 RID: 11280 RVA: 0x000BE920 File Offset: 0x000BCB20
		public AmRoleChangedWhileOperationIsInProgressException(string roleStart, string roleCurrent) : base(ReplayStrings.AmRoleChangedWhileOperationIsInProgress(roleStart, roleCurrent))
		{
			this.roleStart = roleStart;
			this.roleCurrent = roleCurrent;
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000BE942 File Offset: 0x000BCB42
		public AmRoleChangedWhileOperationIsInProgressException(string roleStart, string roleCurrent, Exception innerException) : base(ReplayStrings.AmRoleChangedWhileOperationIsInProgress(roleStart, roleCurrent), innerException)
		{
			this.roleStart = roleStart;
			this.roleCurrent = roleCurrent;
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000BE968 File Offset: 0x000BCB68
		protected AmRoleChangedWhileOperationIsInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.roleStart = (string)info.GetValue("roleStart", typeof(string));
			this.roleCurrent = (string)info.GetValue("roleCurrent", typeof(string));
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000BE9BD File Offset: 0x000BCBBD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("roleStart", this.roleStart);
			info.AddValue("roleCurrent", this.roleCurrent);
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000BE9E9 File Offset: 0x000BCBE9
		public string RoleStart
		{
			get
			{
				return this.roleStart;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x000BE9F1 File Offset: 0x000BCBF1
		public string RoleCurrent
		{
			get
			{
				return this.roleCurrent;
			}
		}

		// Token: 0x040014C3 RID: 5315
		private readonly string roleStart;

		// Token: 0x040014C4 RID: 5316
		private readonly string roleCurrent;
	}
}
