using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D19 RID: 3353
	public abstract class UMPromptTaskBase<TIdentity> : SystemConfigurationObjectActionTask<TIdentity, UMDialPlan> where TIdentity : IIdentityParameter, new()
	{
		// Token: 0x170027E1 RID: 10209
		// (get) Token: 0x060080A5 RID: 32933 RVA: 0x0020E6AC File Offset: 0x0020C8AC
		// (set) Token: 0x060080A6 RID: 32934 RVA: 0x0020E6B4 File Offset: 0x0020C8B4
		public override TIdentity Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170027E2 RID: 10210
		// (get) Token: 0x060080A7 RID: 32935
		// (set) Token: 0x060080A8 RID: 32936
		public abstract UMAutoAttendantIdParameter UMAutoAttendant { get; set; }

		// Token: 0x170027E3 RID: 10211
		// (get) Token: 0x060080A9 RID: 32937
		// (set) Token: 0x060080AA RID: 32938
		public abstract UMDialPlanIdParameter UMDialPlan { get; set; }

		// Token: 0x170027E4 RID: 10212
		// (get) Token: 0x060080AB RID: 32939 RVA: 0x0020E6BD File Offset: 0x0020C8BD
		// (set) Token: 0x060080AC RID: 32940 RVA: 0x0020E6C5 File Offset: 0x0020C8C5
		protected UMAutoAttendant AutoAttendant { get; set; }

		// Token: 0x060080AD RID: 32941 RVA: 0x0020E6CE File Offset: 0x0020C8CE
		protected override IConfigDataProvider CreateSession()
		{
			if (this.UMDialPlan != null)
			{
				return base.CreateSession();
			}
			if (this.UMAutoAttendant != null)
			{
				return this.CreateSessionForAA();
			}
			ExAssert.RetailAssert(false, "Invalid option. Either UMAutoAttendant or UMDialplan optons are valid");
			return null;
		}

		// Token: 0x060080AE RID: 32942 RVA: 0x0020E6FC File Offset: 0x0020C8FC
		private IConfigDataProvider CreateSessionForAA()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.CreateSession();
			this.AutoAttendant = (UMAutoAttendant)base.GetDataObject<UMAutoAttendant>(this.UMAutoAttendant, configurationSession, null, new LocalizedString?(Strings.NonExistantAutoAttendant(this.UMAutoAttendant.ToString())), new LocalizedString?(Strings.MultipleAutoAttendantsWithSameId(this.UMAutoAttendant.ToString())));
			configurationSession = (IConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(configurationSession, this.AutoAttendant.OrganizationId, true);
			base.VerifyIsWithinScopes(configurationSession, this.AutoAttendant, true, new DataAccessTask<UMDialPlan>.ADObjectOutOfScopeString(Strings.ScopeErrorOnAutoAttendant));
			this.Identity = (TIdentity)((object)new UMDialPlanIdParameter(this.AutoAttendant.UMDialPlan));
			return configurationSession;
		}
	}
}
