using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200007F RID: 127
	internal sealed class GetUserSettingsForUser : GetUserSettingsCommandBase
	{
		// Token: 0x06000361 RID: 865 RVA: 0x00015890 File Offset: 0x00013A90
		internal GetUserSettingsForUser(ADRecipient callerADRecipient, SecurityIdentifier callerSid, CallContext callContext) : base(callContext)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<ADRecipient>((long)this.GetHashCode(), "GetUserSettingsForUser constructor called for '{0}'.", callerADRecipient);
			this.callerSid = callerSid;
			this.callerADRecipient = callerADRecipient;
			Common.LoadAuthenticatingUserInfo(this.callerADRecipient);
			ADUser aduser = callerADRecipient as ADUser;
			this.callerSearchRoot = ((aduser != null) ? aduser.QueryBaseDN : null);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000158ED File Offset: 0x00013AED
		protected override IStandardBudget AcquireBudget()
		{
			return StandardBudget.Acquire(this.callerSid, BudgetType.Ews, Common.GetSessionSettingsForCallerScope());
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00015900 File Offset: 0x00013B00
		protected override void AddToQueryList(UserResultMapping userResultMapping, IBudget budget)
		{
			if (this.IsCaller(userResultMapping))
			{
				this.SetCallerResult(userResultMapping);
				return;
			}
			base.AddToADQueryList(userResultMapping, this.callerADRecipient.OrganizationId, this.callerSearchRoot, budget);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001592C File Offset: 0x00013B2C
		private bool IsCaller(UserResultMapping userResultMapping)
		{
			bool result = false;
			if (this.callerADRecipient.EmailAddresses != null)
			{
				foreach (ProxyAddress proxyAddress in this.callerADRecipient.EmailAddresses)
				{
					if (proxyAddress != null && userResultMapping.SmtpProxyAddress.CompareTo(proxyAddress) == 0)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000159A8 File Offset: 0x00013BA8
		private void SetCallerResult(UserResultMapping userResultMapping)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "SetCallerResult() called for '{0}'.", userResultMapping.Mailbox);
			userResultMapping.Result = new ADQueryResult(userResultMapping)
			{
				Result = new Result<ADRecipient>(this.callerADRecipient, null)
			};
		}

		// Token: 0x0400030C RID: 780
		private SecurityIdentifier callerSid;

		// Token: 0x0400030D RID: 781
		private ADRecipient callerADRecipient;

		// Token: 0x0400030E RID: 782
		private ADObjectId callerSearchRoot;
	}
}
