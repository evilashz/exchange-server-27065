using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace System.Security.Principal
{
	// Token: 0x020002F6 RID: 758
	[ComVisible(true)]
	[Serializable]
	public class GenericPrincipal : ClaimsPrincipal
	{
		// Token: 0x06002775 RID: 10101 RVA: 0x000902E0 File Offset: 0x0008E4E0
		public GenericPrincipal(IIdentity identity, string[] roles)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identity = identity;
			if (roles != null)
			{
				this.m_roles = new string[roles.Length];
				for (int i = 0; i < roles.Length; i++)
				{
					this.m_roles[i] = roles[i];
				}
			}
			else
			{
				this.m_roles = null;
			}
			this.AddIdentityWithRoles(this.m_identity, this.m_roles);
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x00090350 File Offset: 0x0008E550
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			ClaimsIdentity claimsIdentity = null;
			foreach (ClaimsIdentity claimsIdentity2 in base.Identities)
			{
				if (claimsIdentity2 != null)
				{
					claimsIdentity = claimsIdentity2;
					break;
				}
			}
			if (this.m_roles != null && this.m_roles.Length != 0 && claimsIdentity != null)
			{
				claimsIdentity.ExternalClaims.Add(new RoleClaimProvider("LOCAL AUTHORITY", this.m_roles, claimsIdentity).Claims);
				return;
			}
			if (claimsIdentity == null)
			{
				this.AddIdentityWithRoles(this.m_identity, this.m_roles);
			}
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000903EC File Offset: 0x0008E5EC
		[SecuritySafeCritical]
		private void AddIdentityWithRoles(IIdentity identity, string[] roles)
		{
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				claimsIdentity = claimsIdentity.Clone();
			}
			else
			{
				claimsIdentity = new ClaimsIdentity(identity);
			}
			if (roles != null && roles.Length != 0)
			{
				claimsIdentity.ExternalClaims.Add(new RoleClaimProvider("LOCAL AUTHORITY", roles, claimsIdentity).Claims);
			}
			base.AddIdentity(claimsIdentity);
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x0009043D File Offset: 0x0008E63D
		public override IIdentity Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00090448 File Offset: 0x0008E648
		public override bool IsInRole(string role)
		{
			if (role == null || this.m_roles == null)
			{
				return false;
			}
			for (int i = 0; i < this.m_roles.Length; i++)
			{
				if (this.m_roles[i] != null && string.Compare(this.m_roles[i], role, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return base.IsInRole(role);
		}

		// Token: 0x04000F51 RID: 3921
		private IIdentity m_identity;

		// Token: 0x04000F52 RID: 3922
		private string[] m_roles;
	}
}
