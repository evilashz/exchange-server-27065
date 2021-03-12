using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000017 RID: 23
	internal class AnonCallerUtils
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00005DEC File Offset: 0x00003FEC
		internal static string ProcessResult(BaseUMCallSession vo, ActivityManager manager, ADRecipient recipient)
		{
			DialingPermissionsCheck dialingPermissionsCheck = DialingPermissionsCheckFactory.Create(vo);
			DialingPermissionsCheck.DialingPermissionsCheckResult perms = dialingPermissionsCheck.CheckDirectoryUser(recipient, null);
			AnonCallerUtils.SetVariables(recipient, perms, manager);
			string autoEvent = AnonCallerUtils.GetAutoEvent(perms);
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, recipient.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, null, data, "AnonCallerUtils::ProcessResult() Recipient: _UserDisplayName returning autoevent: {0}.", new object[]
			{
				autoEvent
			});
			return autoEvent;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005E48 File Offset: 0x00004048
		internal static void SetVariables(ADRecipient recipient, DialingPermissionsCheck.DialingPermissionsCheckResult perms, ActivityManager manager)
		{
			bool allowCall = perms.AllowCall;
			bool allowSendMessage = perms.AllowSendMessage;
			manager.WriteVariable("allowCall", allowCall);
			manager.WriteVariable("allowMessage", allowSendMessage);
			ContactSearchItem contactSearchItem = ContactSearchItem.CreateFromRecipient(recipient);
			if (allowSendMessage)
			{
				manager.GlobalManager.WriteVariable("directorySearchResult", contactSearchItem);
			}
			if (allowCall)
			{
				ContactInfo targetContactInfo = contactSearchItem.ToContactInfo(FoundByType.BusinessPhone);
				PhoneUtil.SetTransferTargetPhone(manager, TransferExtension.UserExtension, perms.NumberToDial, targetContactInfo);
				if (manager.Manager is AutoAttendantManager)
				{
					PhoneUtil.SetTransferTargetPhone(manager.Manager, TransferExtension.UserExtension, perms.NumberToDial, targetContactInfo);
				}
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005EDC File Offset: 0x000040DC
		internal static string GetAutoEvent(DialingPermissionsCheck.DialingPermissionsCheckResult perms)
		{
			string result = null;
			if (perms.IsProtectedUser)
			{
				result = "restrictedUser";
			}
			else if (!perms.AllowCall && !perms.AllowSendMessage)
			{
				result = "unreachableUser";
			}
			else if (perms.AllowCall && !perms.AllowSendMessage)
			{
				result = "allowCallOnly";
			}
			else if (!perms.AllowCall && perms.AllowSendMessage)
			{
				result = "allowSendMessageOnly";
			}
			return result;
		}
	}
}
