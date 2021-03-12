using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000244 RID: 580
	internal class MailboxIdServerInfo : BaseServerIdInfo
	{
		// Token: 0x06000F53 RID: 3923 RVA: 0x0004B614 File Offset: 0x00049814
		public static MailboxIdServerInfo Create(string primarySmtpAddress)
		{
			return MailboxIdServerInfo.Create(new MailboxId(primarySmtpAddress));
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0004B624 File Offset: 0x00049824
		public static MailboxIdServerInfo Create(MailboxId mailboxId)
		{
			if (string.IsNullOrEmpty(mailboxId.SmtpAddress) && string.IsNullOrEmpty(mailboxId.MailboxGuid))
			{
				throw new ArgumentException("MailboxId.smtpAddress and mailbox guid are both null or empty.", "mailboxId");
			}
			Guid mdbGuid;
			int serverVersion;
			string serverFQDN;
			Guid guid;
			string cafeFQDN;
			bool crossForest;
			if (MailboxIdServerInfo.TryGetServerDataForMailbox(mailboxId, out mdbGuid, out serverVersion, out serverFQDN, out guid, out cafeFQDN, out crossForest))
			{
				return new MailboxIdServerInfo(mailboxId, mdbGuid, serverVersion, serverFQDN, guid, cafeFQDN, crossForest);
			}
			return null;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0004B67F File Offset: 0x0004987F
		private MailboxIdServerInfo(MailboxId mailboxId, Guid mdbGuid, int serverVersion, string serverFQDN, Guid mailboxGuid, string cafeFQDN, bool crossForest) : base(serverFQDN, mdbGuid, serverVersion, cafeFQDN, crossForest)
		{
			this.mailboxId = mailboxId;
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0004B69E File Offset: 0x0004989E
		public string PrimarySmtpAddress
		{
			get
			{
				return this.mailboxId.SmtpAddress;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0004B6AB File Offset: 0x000498AB
		public MailboxId MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0004B6B3 File Offset: 0x000498B3
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0004B6BC File Offset: 0x000498BC
		private static bool TryGetServerDataForMailbox(MailboxId mailboxId, out Guid mdbGuid, out int serverVersion, out string serverFQDN, out Guid mailboxGuid, out string cafeFQDN, out bool proxyToCafe)
		{
			mdbGuid = Guid.Empty;
			serverVersion = BaseServerIdInfo.InvalidServerVersion;
			serverFQDN = null;
			mailboxGuid = Guid.Empty;
			proxyToCafe = false;
			cafeFQDN = null;
			bool result;
			try
			{
				Guid guid = (!string.IsNullOrEmpty(mailboxId.MailboxGuid)) ? new Guid(mailboxId.MailboxGuid) : Guid.Empty;
				ADUser aduser = null;
				if ((!(guid != Guid.Empty) || !ADIdentityInformationCache.Singleton.TryGetADUser(guid, CallContext.Current.ADRecipientSessionContext, out aduser)) && (string.IsNullOrEmpty(mailboxId.SmtpAddress) || !ADIdentityInformationCache.Singleton.TryGetADUser(mailboxId.SmtpAddress, CallContext.Current.ADRecipientSessionContext, out aduser)))
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[MailboxIdServerInfo::GetServerData] Cannot retrieve AD user object for MailboxId: {0}.", MailboxIdServerInfo.GetMailboxIdTraceString(mailboxId));
				}
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Ews.HttpProxyToCafe.Enabled || (aduser != null && aduser.Database != null && !string.Equals(TopologyProvider.LocalForestFqdn, aduser.Database.PartitionFQDN, StringComparison.OrdinalIgnoreCase)))
				{
					proxyToCafe = true;
				}
				if (!proxyToCafe)
				{
					ExchangePrincipal exchangePrincipal = null;
					if (CallContext.Current.IsHybridPublicFolderAccessRequest && !ExchangePrincipalCache.TryGetExchangePrincipalForHybridPublicFolderAccess(CallContext.Current.EffectiveCallerSid, CallContext.Current.ADRecipientSessionContext, out exchangePrincipal, false))
					{
						ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug(0L, "[MailboxIdServerInfo::TryGetServerDataForMailbox] Unable to get RemoteUserMailboxPrincipal for hybrid PF access.");
					}
					if (exchangePrincipal == null)
					{
						exchangePrincipal = ExchangePrincipalCache.GetFromCache(mailboxId, CallContext.Current.ADRecipientSessionContext);
					}
					if (!exchangePrincipal.MailboxInfo.IsRemote)
					{
						serverVersion = exchangePrincipal.MailboxInfo.Location.ServerVersion;
						serverFQDN = exchangePrincipal.MailboxInfo.Location.ServerFqdn;
						mdbGuid = exchangePrincipal.MailboxInfo.GetDatabaseGuid();
						mailboxGuid = ((guid != Guid.Empty) ? guid : exchangePrincipal.MailboxInfo.MailboxGuid);
						result = true;
					}
					else
					{
						ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[MailboxIdServerInfo::TryGetServerDataForMailbox] MailboxId: {0} is remote mailbox. Server FQDN cannot be determined.", MailboxIdServerInfo.GetMailboxIdTraceString(mailboxId));
						result = false;
					}
				}
				else
				{
					mdbGuid = aduser.Database.ObjectGuid;
					mailboxGuid = ((guid != Guid.Empty) ? guid : aduser.ExchangeGuid);
					cafeFQDN = CafeHelper.GetSourceCafeServer(CallContext.Current.HttpContext.Request);
					CallContext.Current.HttpContext.Items["AnchorMailboxHintKey"] = aduser.UserPrincipalName;
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[MailboxIdServerInfo::TryGetServerDataForMailbox] MailboxId: {0} is in a different resource forest. Server FQDN cannot be determined.", MailboxIdServerInfo.GetMailboxIdTraceString(mailboxId));
					result = true;
				}
			}
			catch (NonExistentMailboxException)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[MailboxIdServerInfo::TryGetServerDataForMailbox] Tried to get ExchangePrincipal for MailboxId: {0}, but mailbox does not exist.", MailboxIdServerInfo.GetMailboxIdTraceString(mailboxId));
				result = false;
			}
			catch (ADConfigurationException ex)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, string>(0L, "[MailboxIdServerInfo::TryGetServerDataForMailbox] Tried to get ExchangePrincipal for MailboxId: {0}, but received an AD config exception: '{1}'", MailboxIdServerInfo.GetMailboxIdTraceString(mailboxId), ex.Message);
				result = false;
			}
			catch (InvalidSmtpAddressException)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[MailboxIdServerInfo::TryGetServerDataForMailbox]  Tried to get ExchangePrincipal for MailboxId: {0}, but received an InvalidSmtpAddressException.", MailboxIdServerInfo.GetMailboxIdTraceString(mailboxId));
				result = false;
			}
			return result;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0004B9D0 File Offset: 0x00049BD0
		private static string GetMailboxIdTraceString(MailboxId mailboxId)
		{
			return string.Format("SMTP: '{0}'; GUID: '{1}'", string.IsNullOrEmpty(mailboxId.SmtpAddress) ? "<NULL>" : mailboxId.SmtpAddress, string.IsNullOrEmpty(mailboxId.MailboxGuid) ? "<NULL>" : mailboxId.MailboxGuid);
		}

		// Token: 0x04000BAE RID: 2990
		private readonly MailboxId mailboxId;

		// Token: 0x04000BAF RID: 2991
		private readonly Guid mailboxGuid;
	}
}
