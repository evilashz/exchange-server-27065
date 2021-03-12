using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Security.PartnerToken
{
	// Token: 0x02000094 RID: 148
	internal class PartnerIdentity : WindowsIdentity, IOrganizationScopedIdentity, IIdentity
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x000292C3 File Offset: 0x000274C3
		private PartnerIdentity(DelegatedPrincipal delegatedPrincipal, OrganizationId delegatedOrganizationId, IntPtr token) : base(token)
		{
			this.delegatedPrincipal = delegatedPrincipal;
			this.delegatedOrganizationId = delegatedOrganizationId;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000292DA File Offset: 0x000274DA
		public DelegatedPrincipal DelegatedPrincipal
		{
			get
			{
				return this.delegatedPrincipal;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x000292E2 File Offset: 0x000274E2
		public OrganizationId DelegatedOrganizationId
		{
			get
			{
				return this.delegatedOrganizationId;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000292EA File Offset: 0x000274EA
		string IIdentity.AuthenticationType
		{
			get
			{
				return "Partner";
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x000292F1 File Offset: 0x000274F1
		bool IIdentity.IsAuthenticated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000292F4 File Offset: 0x000274F4
		string IIdentity.Name
		{
			get
			{
				return this.delegatedPrincipal.ToString();
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00029301 File Offset: 0x00027501
		OrganizationId IOrganizationScopedIdentity.OrganizationId
		{
			get
			{
				return this.DelegatedOrganizationId;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0002930C File Offset: 0x0002750C
		public static PartnerIdentity Create(DelegatedPrincipal delegatedPrincipal, OrganizationId delegatedOrganizationId)
		{
			PartnerIdentity result;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				result = new PartnerIdentity(delegatedPrincipal, delegatedOrganizationId, current.Token);
			}
			return result;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0002934C File Offset: 0x0002754C
		IStandardBudget IOrganizationScopedIdentity.AcquireBudget()
		{
			return StandardBudget.Acquire(new DelegatedPrincipalBudgetKey(this.DelegatedPrincipal, BudgetType.Ews));
		}

		// Token: 0x04000551 RID: 1361
		private const string PartnerAuthenticationType = "Partner";

		// Token: 0x04000552 RID: 1362
		private readonly DelegatedPrincipal delegatedPrincipal;

		// Token: 0x04000553 RID: 1363
		private readonly OrganizationId delegatedOrganizationId;
	}
}
