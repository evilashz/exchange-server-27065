using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.Redirection
{
	// Token: 0x02000005 RID: 5
	internal static class RedirectionUtil
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000026F1 File Offset: 0x000008F1
		public static void MarkProcessedByRedirectionAgent(MailItem mailItem)
		{
			mailItem.Properties["ProcessedByRedirectionAgent"] = true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002709 File Offset: 0x00000909
		public static void ClearProcessedByRedirectionAgent(MailItem mailItem)
		{
			mailItem.Properties.Remove("ProcessedByRedirectionAgent");
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000271C File Offset: 0x0000091C
		public static bool WasProcessedByRedirectionAgent(MailItem mailItem)
		{
			object obj;
			bool flag = mailItem.Properties.TryGetValue("ProcessedByRedirectionAgent", out obj);
			bool? flag2 = obj as bool?;
			return flag && flag2 != null && flag2.Value;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000275D File Offset: 0x0000095D
		public static void SetRedirectionAddress(MailItem mailItem, string redirectionAddress)
		{
			mailItem.Properties["RedirectionAddress"] = redirectionAddress;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002770 File Offset: 0x00000970
		public static void RemoveRedirectionAddress(MailItem mailItem)
		{
			mailItem.Properties.Remove("RedirectionAddress");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002784 File Offset: 0x00000984
		public static string GetRedirectionAddress(MailItem mailItem)
		{
			object obj;
			if (!mailItem.Properties.TryGetValue("RedirectionAddress", out obj))
			{
				return null;
			}
			return obj as string;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027B0 File Offset: 0x000009B0
		public static ProxyAddress GetForwardingSmtpAddress(EnvelopeRecipient recipient)
		{
			object obj;
			if (!recipient.Properties.TryGetValue("Microsoft.Exchange.Transport.DirectoryData.ForwardingSmtpAddress", out obj))
			{
				return null;
			}
			string text = obj as string;
			if (text == null)
			{
				return null;
			}
			return ProxyAddress.Parse(text);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027E8 File Offset: 0x000009E8
		public static bool GetDeliverAndForward(EnvelopeRecipient recipient)
		{
			object obj;
			if (recipient.Properties.TryGetValue("Microsoft.Exchange.Transport.DirectoryData.DeliverToMailboxAndForward", out obj))
			{
				bool? flag = obj as bool?;
				return flag == null || flag.Value;
			}
			return true;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000282C File Offset: 0x00000A2C
		public static Guid? GetMailboxGuid(EnvelopeRecipient recipient)
		{
			object obj;
			if (recipient.Properties.TryGetValue("Microsoft.Exchange.Transport.DirectoryData.ExchangeGuid", out obj))
			{
				return obj as Guid?;
			}
			return null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002864 File Offset: 0x00000A64
		public static Guid? GetMailboxDatabaseGuid(EnvelopeRecipient recipient)
		{
			ADObjectId adobjectId = null;
			object obj;
			if (recipient.Properties.TryGetValue("Microsoft.Exchange.Transport.DirectoryData.Database", out obj))
			{
				adobjectId = (obj as ADObjectId);
			}
			if (adobjectId == null)
			{
				return null;
			}
			return new Guid?(adobjectId.ObjectGuid);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028A8 File Offset: 0x00000AA8
		public static IThrottlingPolicy GetThrottlingPolicy(EnvelopeRecipient recipient, OrganizationId orgId)
		{
			ADObjectId adobjectId = null;
			object obj;
			if (recipient.Properties.TryGetValue("Microsoft.Exchange.Transport.DirectoryData.ThrottlingPolicy", out obj))
			{
				adobjectId = (obj as ADObjectId);
			}
			IThrottlingPolicy result;
			if (adobjectId != null)
			{
				result = ThrottlingPolicyCache.Singleton.Get(orgId, adobjectId);
			}
			else
			{
				result = ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy();
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000028F4 File Offset: 0x00000AF4
		public static string GetPrimarySmtpAddress(ADRawEntry entry)
		{
			SmtpAddress value = (SmtpAddress)entry[ADRecipientSchema.PrimarySmtpAddress];
			if (value == SmtpAddress.Empty)
			{
				return null;
			}
			if (!value.IsValidAddress)
			{
				return null;
			}
			return value.ToString();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000293C File Offset: 0x00000B3C
		public static bool TryResolveForwardingSmtpAddress(IADRecipientCache cache, ProxyAddress address, out ADRawEntry entry)
		{
			Result<ADRawEntry> result = cache.FindAndCacheRecipient(address);
			entry = result.Data;
			return result.Data != null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002968 File Offset: 0x00000B68
		public static void LogErrorWithMessageId(ExEventLog.EventTuple tuple, long internalMessageId, params object[] args)
		{
			RedirectionUtil.LogEvent(tuple, new object[]
			{
				internalMessageId,
				args
			});
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002990 File Offset: 0x00000B90
		public static void LogEvent(ExEventLog.EventTuple tuple, params object[] args)
		{
			RedirectionUtil.logger.LogEvent(tuple, null, args);
		}

		// Token: 0x04000004 RID: 4
		private static ExEventLog logger = new ExEventLog(new Guid("7D2A0005-2C75-42ac-B495-8FE62F3B4FCF"), "MSExchange Messaging Policies");
	}
}
