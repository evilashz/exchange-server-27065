using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200109E RID: 4254
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnexpectedRoleTypeException : LocalizedException
	{
		// Token: 0x0600B215 RID: 45589 RVA: 0x00299594 File Offset: 0x00297794
		public UnexpectedRoleTypeException(string roleDN, RoleType invalid, RoleType expected) : base(Strings.UnexpectedRoleTypeException(roleDN, invalid, expected))
		{
			this.roleDN = roleDN;
			this.invalid = invalid;
			this.expected = expected;
		}

		// Token: 0x0600B216 RID: 45590 RVA: 0x002995B9 File Offset: 0x002977B9
		public UnexpectedRoleTypeException(string roleDN, RoleType invalid, RoleType expected, Exception innerException) : base(Strings.UnexpectedRoleTypeException(roleDN, invalid, expected), innerException)
		{
			this.roleDN = roleDN;
			this.invalid = invalid;
			this.expected = expected;
		}

		// Token: 0x0600B217 RID: 45591 RVA: 0x002995E0 File Offset: 0x002977E0
		protected UnexpectedRoleTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.roleDN = (string)info.GetValue("roleDN", typeof(string));
			this.invalid = (RoleType)info.GetValue("invalid", typeof(RoleType));
			this.expected = (RoleType)info.GetValue("expected", typeof(RoleType));
		}

		// Token: 0x0600B218 RID: 45592 RVA: 0x00299658 File Offset: 0x00297858
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("roleDN", this.roleDN);
			info.AddValue("invalid", this.invalid);
			info.AddValue("expected", this.expected);
		}

		// Token: 0x170038BA RID: 14522
		// (get) Token: 0x0600B219 RID: 45593 RVA: 0x002996AA File Offset: 0x002978AA
		public string RoleDN
		{
			get
			{
				return this.roleDN;
			}
		}

		// Token: 0x170038BB RID: 14523
		// (get) Token: 0x0600B21A RID: 45594 RVA: 0x002996B2 File Offset: 0x002978B2
		public RoleType Invalid
		{
			get
			{
				return this.invalid;
			}
		}

		// Token: 0x170038BC RID: 14524
		// (get) Token: 0x0600B21B RID: 45595 RVA: 0x002996BA File Offset: 0x002978BA
		public RoleType Expected
		{
			get
			{
				return this.expected;
			}
		}

		// Token: 0x04006220 RID: 25120
		private readonly string roleDN;

		// Token: 0x04006221 RID: 25121
		private readonly RoleType invalid;

		// Token: 0x04006222 RID: 25122
		private readonly RoleType expected;
	}
}
