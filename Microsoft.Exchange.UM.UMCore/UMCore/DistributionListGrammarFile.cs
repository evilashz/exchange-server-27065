using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000124 RID: 292
	internal class DistributionListGrammarFile : SearchGrammarFile
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x0002282B File Offset: 0x00020A2B
		internal DistributionListGrammarFile(CultureInfo culture, string filePath) : base(culture, true)
		{
			this.filePath = filePath;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0002283C File Offset: 0x00020A3C
		public override Uri BaseUri
		{
			get
			{
				return new Uri(Utils.GrammarPathFromCulture(base.Culture));
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0002284E File Offset: 0x00020A4E
		internal override string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00022856 File Offset: 0x00020A56
		internal override bool HasEntries
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002285C File Offset: 0x00020A5C
		internal static GrammarIdentifier GetGrammarIdentifier(CallContext callContext)
		{
			ExAssert.RetailAssert(callContext.CallType == 3, "Invalid call type = {0}", new object[]
			{
				callContext.CallType.ToString()
			});
			UMSubscriber callerInfo = callContext.CallerInfo;
			ADRecipient adrecipient = callerInfo.ADRecipient;
			ExAssert.RetailAssert(adrecipient != null, "subscriber.ADRecipient = null");
			ExAssert.RetailAssert(adrecipient.OrganizationId != null, "subscriber.ADRecipient.OrganizationId = null");
			ADObjectId organizationalUnit = adrecipient.OrganizationId.OrganizationalUnit;
			PIIMessage data = PIIMessage.Create(PIIType._User, adrecipient);
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, data, "DistributionListGrammarFile::GetGrammarIdentifier() - User = '_User', OU = '{0}'", new object[]
			{
				(organizationalUnit != null) ? organizationalUnit.ToString() : "<null>"
			});
			return new GrammarIdentifier(adrecipient.OrganizationId, callContext.Culture, GrammarFileNames.GetFileNameForDL());
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00022928 File Offset: 0x00020B28
		internal static void LogErrorEvent()
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ContactsNoGrammarFileWarning, null, new object[0]);
		}

		// Token: 0x0400088B RID: 2187
		private readonly string filePath;
	}
}
