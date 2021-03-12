using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Data.ApplicationLogic.Directory
{
	// Token: 0x020000E7 RID: 231
	internal class RecipientHelper
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x00025CAC File Offset: 0x00023EAC
		internal static SecurityIdentifier TryGetMasterAccountSid(ADRecipient adRecipient)
		{
			SecurityIdentifier result;
			if (adRecipient.IsLinked && adRecipient.MasterAccountSid != null && !RecipientHelper.IsWellKnownSid(adRecipient.MasterAccountSid))
			{
				RecipientHelper.Tracer.TraceDebug<ADRecipient, SecurityIdentifier>(0L, "RecipientIdentity {0} has MasterAccountSid: {1}", adRecipient, adRecipient.MasterAccountSid);
				result = adRecipient.MasterAccountSid;
			}
			else
			{
				RecipientHelper.Tracer.TraceDebug<ADRecipient>(0L, "RecipientIdentity {0} has no MasterAccountSid.", adRecipient);
				result = null;
			}
			return result;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00025D14 File Offset: 0x00023F14
		internal static bool IsWellKnownSid(SecurityIdentifier sid)
		{
			bool result = false;
			foreach (WellKnownSidType wellKnownSidType in RecipientHelper.wellKnownSidTypes)
			{
				if (sid.IsWellKnown(wellKnownSidType))
				{
					RecipientHelper.Tracer.TraceDebug<SecurityIdentifier, WellKnownSidType>(0L, "sid {0} is well known sid: {1}", sid, wellKnownSidType);
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x0400048C RID: 1164
		private static readonly Trace Tracer = ExTraceGlobals.CommonAlgorithmTracer;

		// Token: 0x0400048D RID: 1165
		private static WellKnownSidType[] wellKnownSidTypes = (WellKnownSidType[])Enum.GetValues(typeof(WellKnownSidType));
	}
}
