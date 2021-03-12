using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	public class DelegateUser : IConfigurable
	{
		// Token: 0x06000A79 RID: 2681 RVA: 0x0002CE85 File Offset: 0x0002B085
		public DelegateUser(string id, DelegateRoleType roleType, string scope)
		{
			this.id = id;
			this.scope = scope;
			this.roleType = roleType;
			this.identity = new DelegateUserId(id);
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0002CEAE File Offset: 0x0002B0AE
		public string Identity
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0002CEB6 File Offset: 0x0002B0B6
		public string Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002CEBE File Offset: 0x0002B0BE
		public DelegateRoleType Role
		{
			get
			{
				return this.roleType;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002CEC6 File Offset: 0x0002B0C6
		ObjectId IConfigurable.Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002CECE File Offset: 0x0002B0CE
		ValidationError[] IConfigurable.Validate()
		{
			return ValidationError.None;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002CED5 File Offset: 0x0002B0D5
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0002CED8 File Offset: 0x0002B0D8
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002CEDB File Offset: 0x0002B0DB
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002CEE2 File Offset: 0x0002B0E2
		void IConfigurable.ResetChangeTracking()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400022F RID: 559
		private readonly string id;

		// Token: 0x04000230 RID: 560
		private DelegateUserId identity;

		// Token: 0x04000231 RID: 561
		private readonly string scope;

		// Token: 0x04000232 RID: 562
		private DelegateRoleType roleType;
	}
}
