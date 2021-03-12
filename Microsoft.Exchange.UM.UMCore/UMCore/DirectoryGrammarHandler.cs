using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200026A RID: 618
	internal abstract class DirectoryGrammarHandler
	{
		// Token: 0x06001255 RID: 4693 RVA: 0x000510C0 File Offset: 0x0004F2C0
		public static DirectoryGrammarHandler CreateHandler(OrganizationId orgId)
		{
			if (!CommonConstants.UseDataCenterCallRouting)
			{
				return new StaticDirectoryGrammarHandler(orgId);
			}
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(orgId, null);
			bool flag = !GrammarFileDistributionShare.SpeechGrammarsFolderExists(orgId) && !iadrecipientLookup.TenantSizeExceedsThreshold(GrammarRecipientHelper.GetUserFilter(), 10);
			if (flag)
			{
				return new DynamicDirectoryGrammarHandler(orgId);
			}
			return new StaticDirectoryGrammarHandler(orgId);
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001256 RID: 4694
		public abstract bool DeleteFileAfterUse { get; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0005110F File Offset: 0x0004F30F
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00051117 File Offset: 0x0004F317
		public OrganizationId OrgId { get; private set; }

		// Token: 0x06001259 RID: 4697 RVA: 0x00051120 File Offset: 0x0004F320
		protected DirectoryGrammarHandler(OrganizationId orgId)
		{
			this.OrgId = orgId;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00051130 File Offset: 0x0004F330
		public static OrganizationId GetOrganizationId(CallContext callContext)
		{
			ValidateArgument.NotNull(callContext, "callContext");
			OrganizationId organizationId = null;
			switch (callContext.CallType)
			{
			case 1:
				ExAssert.RetailAssert(callContext.DialPlan != null, "callContext.DialPlan is null");
				ExAssert.RetailAssert(callContext.DialPlan.OrganizationId != null, "callContext.DialPlan.OrganizationId is null");
				organizationId = callContext.DialPlan.OrganizationId;
				break;
			case 2:
				ExAssert.RetailAssert(callContext.AutoAttendantInfo != null, "callContext.AutoAttendantInfo is null");
				ExAssert.RetailAssert(callContext.AutoAttendantInfo.OrganizationId != null, "callContext.AutoAttendantInfo.OrganizationId is null");
				organizationId = callContext.AutoAttendantInfo.OrganizationId;
				break;
			case 3:
			{
				UMSubscriber callerInfo = callContext.CallerInfo;
				ADRecipient adrecipient = callerInfo.ADRecipient;
				ExAssert.RetailAssert(adrecipient != null, "subscriber.ADRecipient is null");
				ExAssert.RetailAssert(adrecipient.OrganizationId != null, "subscriber.ADRecipient.OrganizationId is null");
				organizationId = adrecipient.OrganizationId;
				break;
			}
			default:
				ExAssert.RetailAssert(false, "Unsupported call type '{0}' for directory grammar handler", new object[]
				{
					callContext.CallType
				});
				break;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "DirectoryGrammarHandler.GetOrganizationId() - orgId='{0}'", new object[]
			{
				organizationId
			});
			return organizationId;
		}

		// Token: 0x0600125B RID: 4699
		public abstract void PrepareGrammarAsync(CallContext callContext, DirectoryGrammarHandler.GrammarType grammarType);

		// Token: 0x0600125C RID: 4700
		public abstract void PrepareGrammarAsync(ADRecipient recipient, CultureInfo culture);

		// Token: 0x0600125D RID: 4701
		public abstract SearchGrammarFile WaitForPrepareGrammarCompletion();

		// Token: 0x0200026B RID: 619
		public enum GrammarType
		{
			// Token: 0x04000C11 RID: 3089
			User,
			// Token: 0x04000C12 RID: 3090
			DL
		}
	}
}
