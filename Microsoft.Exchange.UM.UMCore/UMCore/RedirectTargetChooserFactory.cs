using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001C6 RID: 454
	internal static class RedirectTargetChooserFactory
	{
		// Token: 0x06000D3E RID: 3390 RVA: 0x0003A7AC File Offset: 0x000389AC
		public static IRedirectTargetChooser CreateFromRecipient(CallContext callContext, UMRecipient recipient)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			PIIMessage data = PIIMessage.Create(PIIType._User, recipient);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, data, "Creating IRedirectTargetChooser from recipient '_User'", new object[0]);
			if (recipient.RequiresLegacyRedirectForSubscriberAccess || recipient.RequiresLegacyRedirectForCallAnswering)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "RedirectTargetChooserFactory.CreateFromRecipient() Value = \"LegacyServerChooser\"", new object[0]);
				return new LegacyUMServerChooser(callContext, recipient);
			}
			CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, null, "RedirectTargetChooserFactory.CreateFromRecipient() Unspecified condition", new object[0]);
			throw new ArgumentException();
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003A840 File Offset: 0x00038A40
		public static IRedirectTargetChooser CreateFromFqdn(CallContext callContext, string forestFqdn, string phoneNumber)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (forestFqdn == null)
			{
				throw new ArgumentNullException("forestFqdn");
			}
			if (phoneNumber == null)
			{
				throw new ArgumentNullException("phoneNumber");
			}
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, data, "Creating IRedirectTargetChooser from forest FQDN '{0}', phone number = '_PhoneNumber'", new object[]
			{
				forestFqdn
			});
			return new DataCenterForestChooser(callContext, forestFqdn, phoneNumber);
		}
	}
}
