using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001006 RID: 4102
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CorruptedRoleNeedsCleanupException : LocalizedException
	{
		// Token: 0x0600AEDF RID: 44767 RVA: 0x0029399E File Offset: 0x00291B9E
		public CorruptedRoleNeedsCleanupException(string roleId, string error) : base(Strings.CorruptedRoleNeedsCleanupException(roleId, error))
		{
			this.roleId = roleId;
			this.error = error;
		}

		// Token: 0x0600AEE0 RID: 44768 RVA: 0x002939BB File Offset: 0x00291BBB
		public CorruptedRoleNeedsCleanupException(string roleId, string error, Exception innerException) : base(Strings.CorruptedRoleNeedsCleanupException(roleId, error), innerException)
		{
			this.roleId = roleId;
			this.error = error;
		}

		// Token: 0x0600AEE1 RID: 44769 RVA: 0x002939DC File Offset: 0x00291BDC
		protected CorruptedRoleNeedsCleanupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.roleId = (string)info.GetValue("roleId", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600AEE2 RID: 44770 RVA: 0x00293A31 File Offset: 0x00291C31
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("roleId", this.roleId);
			info.AddValue("error", this.error);
		}

		// Token: 0x170037E4 RID: 14308
		// (get) Token: 0x0600AEE3 RID: 44771 RVA: 0x00293A5D File Offset: 0x00291C5D
		public string RoleId
		{
			get
			{
				return this.roleId;
			}
		}

		// Token: 0x170037E5 RID: 14309
		// (get) Token: 0x0600AEE4 RID: 44772 RVA: 0x00293A65 File Offset: 0x00291C65
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400614A RID: 24906
		private readonly string roleId;

		// Token: 0x0400614B RID: 24907
		private readonly string error;
	}
}
