using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000124 RID: 292
	internal class MultiLinkSyncHelper
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x0002F8E4 File Offset: 0x0002DAE4
		public static Dictionary<object, ArrayList> GetIncompatibleParametersDictionaryForCommonMultiLink()
		{
			Dictionary<object, ArrayList> dictionary = new Dictionary<object, ArrayList>();
			dictionary[ADRecipientSchema.AcceptMessagesOnlyFromSendersOrMembers] = new ArrayList(new object[]
			{
				ADRecipientSchema.AcceptMessagesOnlyFrom,
				ADRecipientSchema.AcceptMessagesOnlyFromDLMembers,
				"RawAcceptMessagesOnlyFrom"
			});
			dictionary[ADRecipientSchema.AcceptMessagesOnlyFrom] = new ArrayList(new object[]
			{
				"RawAcceptMessagesOnlyFrom"
			});
			dictionary[ADRecipientSchema.RejectMessagesFromSendersOrMembers] = new ArrayList(new object[]
			{
				ADRecipientSchema.RejectMessagesFrom,
				ADRecipientSchema.RejectMessagesFromDLMembers,
				"RawRejectMessagesFrom"
			});
			dictionary[ADRecipientSchema.RejectMessagesFrom] = new ArrayList(new object[]
			{
				"RawRejectMessagesFrom"
			});
			dictionary[ADRecipientSchema.BypassModerationFromSendersOrMembers] = new ArrayList(new object[]
			{
				MailEnabledRecipientSchema.BypassModerationFrom,
				MailEnabledRecipientSchema.BypassModerationFromDLMembers,
				"RawBypassModerationFrom"
			});
			dictionary[ADRecipientSchema.BypassModerationFrom] = new ArrayList(new object[]
			{
				"RawBypassModerationFrom"
			});
			dictionary[ADRecipientSchema.GrantSendOnBehalfTo] = new ArrayList(new object[]
			{
				"RawGrantSendOnBehalfTo"
			});
			dictionary[ADRecipientSchema.ModeratedBy] = new ArrayList(new object[]
			{
				"RawModeratedBy"
			});
			dictionary[ADRecipientSchema.ForwardingAddress] = new ArrayList(new object[]
			{
				"RawForwardingAddress"
			});
			return dictionary;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002FA5C File Offset: 0x0002DC5C
		public static void ValidateIncompatibleParameters(PropertyBag parameters, Dictionary<object, ArrayList> incompatibleParametersDictionary, Task.ErrorLoggerDelegate writeError)
		{
			foreach (object obj in incompatibleParametersDictionary.Keys)
			{
				if (parameters.IsModified(obj))
				{
					foreach (object obj2 in incompatibleParametersDictionary[obj])
					{
						if (parameters.IsModified(obj2))
						{
							writeError(new RecipientTaskException(Strings.ErrorConflictingRestrictionParameters(MultiLinkSyncHelper.GetPropertyName(obj), MultiLinkSyncHelper.GetPropertyName(obj2))), ExchangeErrorCategory.Client, null);
						}
					}
				}
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002FB1C File Offset: 0x0002DD1C
		private static string GetPropertyName(object property)
		{
			if (property is ADPropertyDefinition)
			{
				return (property as ADPropertyDefinition).Name;
			}
			return property.ToString();
		}

		// Token: 0x0400054E RID: 1358
		public const string RawAcceptMessagesOnlyFrom = "RawAcceptMessagesOnlyFrom";

		// Token: 0x0400054F RID: 1359
		public const string RawBypassModerationFrom = "RawBypassModerationFrom";

		// Token: 0x04000550 RID: 1360
		public const string RawRejectMessagesFrom = "RawRejectMessagesFrom";

		// Token: 0x04000551 RID: 1361
		public const string RawGrantSendOnBehalfTo = "RawGrantSendOnBehalfTo";

		// Token: 0x04000552 RID: 1362
		public const string RawModeratedBy = "RawModeratedBy";

		// Token: 0x04000553 RID: 1363
		public const string RawForwardingAddress = "RawForwardingAddress";

		// Token: 0x04000554 RID: 1364
		public const string RawMembers = "RawMembers";

		// Token: 0x04000555 RID: 1365
		public const string RawCoManagedBy = "RawCoManagedBy";

		// Token: 0x04000556 RID: 1366
		public const string Members = "Members";

		// Token: 0x04000557 RID: 1367
		public const string AddedMembers = "AddedMembers";

		// Token: 0x04000558 RID: 1368
		public const string RemovedMembers = "RemovedMembers";

		// Token: 0x04000559 RID: 1369
		public const string RawSiteMailboxOwners = "RawSiteMailboxOwners";

		// Token: 0x0400055A RID: 1370
		public const string RawSiteMailboxUsers = "RawSiteMailboxUsers";
	}
}
