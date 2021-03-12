using System;
using System.Security.Principal;

namespace System.Security.Permissions
{
	// Token: 0x020002D7 RID: 727
	[Serializable]
	internal class IDRole
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x00088A44 File Offset: 0x00086C44
		internal SecurityIdentifier Sid
		{
			[SecurityCritical]
			get
			{
				if (string.IsNullOrEmpty(this.m_role))
				{
					return null;
				}
				if (this.m_sid == null)
				{
					NTAccount identity = new NTAccount(this.m_role);
					IdentityReferenceCollection identityReferenceCollection = NTAccount.Translate(new IdentityReferenceCollection(1)
					{
						identity
					}, typeof(SecurityIdentifier), false);
					this.m_sid = (identityReferenceCollection[0] as SecurityIdentifier);
				}
				return this.m_sid;
			}
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x00088AB4 File Offset: 0x00086CB4
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("Identity");
			if (this.m_authenticated)
			{
				securityElement.AddAttribute("Authenticated", "true");
			}
			if (this.m_id != null)
			{
				securityElement.AddAttribute("ID", SecurityElement.Escape(this.m_id));
			}
			if (this.m_role != null)
			{
				securityElement.AddAttribute("Role", SecurityElement.Escape(this.m_role));
			}
			return securityElement;
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x00088B24 File Offset: 0x00086D24
		internal void FromXml(SecurityElement e)
		{
			string text = e.Attribute("Authenticated");
			if (text != null)
			{
				this.m_authenticated = (string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0);
			}
			else
			{
				this.m_authenticated = false;
			}
			string text2 = e.Attribute("ID");
			if (text2 != null)
			{
				this.m_id = text2;
			}
			else
			{
				this.m_id = null;
			}
			string text3 = e.Attribute("Role");
			if (text3 != null)
			{
				this.m_role = text3;
				return;
			}
			this.m_role = null;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x00088B9B File Offset: 0x00086D9B
		public override int GetHashCode()
		{
			return (this.m_authenticated ? 0 : 101) + ((this.m_id == null) ? 0 : this.m_id.GetHashCode()) + ((this.m_role == null) ? 0 : this.m_role.GetHashCode());
		}

		// Token: 0x04000E63 RID: 3683
		internal bool m_authenticated;

		// Token: 0x04000E64 RID: 3684
		internal string m_id;

		// Token: 0x04000E65 RID: 3685
		internal string m_role;

		// Token: 0x04000E66 RID: 3686
		[NonSerialized]
		private SecurityIdentifier m_sid;
	}
}
