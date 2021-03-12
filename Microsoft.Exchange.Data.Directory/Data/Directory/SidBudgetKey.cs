using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009CD RID: 2509
	internal class SidBudgetKey : LookupBudgetKey
	{
		// Token: 0x06007426 RID: 29734 RVA: 0x0017F2DC File Offset: 0x0017D4DC
		public SidBudgetKey(SecurityIdentifier sid, BudgetType budgetType, bool isServiceAccount, ADSessionSettings settings) : base(budgetType, isServiceAccount)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.NtAccount = SidToAccountMap.Singleton.Get(sid).ToString();
			this.SessionSettings = settings;
			if (sid.AccountDomainSid == null && (sid.IsWellKnown(WellKnownSidType.LocalSystemSid) || sid.IsWellKnown(WellKnownSidType.NetworkServiceSid)))
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceDebug((long)this.GetHashCode(), "[SidBudgetKey.ctor] Using domain sid for local computer account.");
				this.Sid = SidBudgetKey.localMachineSid.Member;
			}
			else
			{
				this.Sid = sid;
			}
			this.cachedToString = this.GetCachedToString();
			this.cachedHashCode = (base.BudgetType.GetHashCode() ^ this.Sid.GetHashCode() ^ base.IsServiceAccountBudget.GetHashCode());
		}

		// Token: 0x17002962 RID: 10594
		// (get) Token: 0x06007427 RID: 29735 RVA: 0x0017F3C0 File Offset: 0x0017D5C0
		// (set) Token: 0x06007428 RID: 29736 RVA: 0x0017F3C8 File Offset: 0x0017D5C8
		public ADSessionSettings SessionSettings { get; private set; }

		// Token: 0x17002963 RID: 10595
		// (get) Token: 0x06007429 RID: 29737 RVA: 0x0017F3D1 File Offset: 0x0017D5D1
		// (set) Token: 0x0600742A RID: 29738 RVA: 0x0017F3D9 File Offset: 0x0017D5D9
		public SecurityIdentifier Sid { get; private set; }

		// Token: 0x17002964 RID: 10596
		// (get) Token: 0x0600742B RID: 29739 RVA: 0x0017F3E2 File Offset: 0x0017D5E2
		// (set) Token: 0x0600742C RID: 29740 RVA: 0x0017F3EA File Offset: 0x0017D5EA
		public string NtAccount { get; private set; }

		// Token: 0x0600742D RID: 29741 RVA: 0x0017F3F3 File Offset: 0x0017D5F3
		public override string ToString()
		{
			return this.cachedToString;
		}

		// Token: 0x0600742E RID: 29742 RVA: 0x0017F3FB File Offset: 0x0017D5FB
		public override int GetHashCode()
		{
			return this.cachedHashCode;
		}

		// Token: 0x0600742F RID: 29743 RVA: 0x0017F404 File Offset: 0x0017D604
		public override bool Equals(object obj)
		{
			SidBudgetKey sidBudgetKey = obj as SidBudgetKey;
			return !(sidBudgetKey == null) && (sidBudgetKey.BudgetType == base.BudgetType && sidBudgetKey.Sid == this.Sid) && sidBudgetKey.IsServiceAccountBudget == base.IsServiceAccountBudget;
		}

		// Token: 0x06007430 RID: 29744 RVA: 0x0017F490 File Offset: 0x0017D690
		internal override IThrottlingPolicy InternalLookup()
		{
			IRecipientSession session = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, this.SessionSettings, 201, "InternalLookup", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\throttling\\SidBudgetKey.cs");
			return base.ADRetryLookup(delegate
			{
				MiniRecipient recipient = session.FindMiniRecipientBySid<MiniRecipient>(this.Sid, null);
				return this.GetPolicyForRecipient(recipient);
			});
		}

		// Token: 0x06007431 RID: 29745 RVA: 0x0017F4E4 File Offset: 0x0017D6E4
		private string GetCachedToString()
		{
			string text = base.BudgetType.ToString();
			StringBuilder stringBuilder = new StringBuilder(this.NtAccount.Length + text.Length + 11);
			stringBuilder.Append("Sid~");
			stringBuilder.Append(this.NtAccount);
			stringBuilder.Append("~");
			stringBuilder.Append(text);
			stringBuilder.Append("~");
			stringBuilder.Append(base.IsServiceAccountBudget ? "true" : "false");
			return stringBuilder.ToString();
		}

		// Token: 0x04004B04 RID: 19204
		private readonly string cachedToString;

		// Token: 0x04004B05 RID: 19205
		private readonly int cachedHashCode;

		// Token: 0x04004B06 RID: 19206
		private static LazyMember<SecurityIdentifier> localMachineSid = new LazyMember<SecurityIdentifier>(delegate()
		{
			ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 44, "localMachineSid", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\throttling\\SidBudgetKey.cs");
			session.UseConfigNC = false;
			session.UseGlobalCatalog = true;
			SecurityIdentifier result = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADComputer adcomputer = session.FindLocalComputer();
				if (adcomputer != null)
				{
					result = adcomputer.Sid;
					return;
				}
				ExTraceGlobals.ClientThrottlingTracer.TraceError(0L, "[SidBudgetKe.LocalMachineSidInitializer] FindLocalComputer returned null.  Using local machine sid instead.");
			});
			if (!adoperationResult.Succeeded)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceError<Exception>(0L, "[SidBudgetKey.LocalMachineSidInitializer] Domain computer lookup failed. Using local machine sid instead.  Exception: {0}", adoperationResult.Exception);
			}
			return result ?? new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
		});
	}
}
