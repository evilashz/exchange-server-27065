using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008C7 RID: 2247
	internal static class ComplianceUtil
	{
		// Token: 0x06003F91 RID: 16273 RVA: 0x000DBACC File Offset: 0x000D9CCC
		public static bool TryCreateArchiveService(CallContext callContext, IRemoteArchiveRequest remoteArchiveRequest, bool hasIds, Action createArchiveServiceAction)
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				return false;
			}
			if (remoteArchiveRequest.ArchiveService != null)
			{
				return true;
			}
			if (!hasIds || !ComplianceUtil.IsDiscoveryRequest(callContext))
			{
				return false;
			}
			createArchiveServiceAction();
			return remoteArchiveRequest.ArchiveService != null;
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x000DBB20 File Offset: 0x000D9D20
		internal static bool IsDiscoveryRequest(CallContext callContext)
		{
			return (!string.IsNullOrEmpty(callContext.UserAgent) && callContext.UserAgent.IndexOf("EDiscovery", StringComparison.InvariantCultureIgnoreCase) >= 0) || (callContext.ManagementRole != null && callContext.ManagementRole.UserRoles != null && callContext.ManagementRole.UserRoles.Contains("MailboxSearch"));
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x000DBB7F File Offset: 0x000D9D7F
		internal static ExchangeServiceBinding GetArchiveServiceForFolder(CallContext callContext, IEnumerable<BaseFolderId> folderIds)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (folderIds == null)
			{
				throw new ArgumentNullException("folderIds");
			}
			return ComplianceUtil.GetArchiveServiceInfoForFolder(callContext, folderIds.First<BaseFolderId>());
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000DBBAC File Offset: 0x000D9DAC
		internal static ExchangeServiceBinding GetArchiveServiceForItemId(CallContext callContext, BaseItemId itemId)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (itemId == null)
			{
				throw new ArgumentNullException("itemId");
			}
			ExchangeServiceBinding result;
			try
			{
				IdHeaderInformation header = IdConverter.ConvertFromConcatenatedId(itemId.GetId(), BasicTypes.Item, null, false);
				result = ComplianceUtil.GetArchiveServiceInfoForItem(callContext, header);
			}
			catch (LocalizedException ex)
			{
				ExTraceGlobals.ItemCallTracer.TraceError<string, string>(0L, "[ComplianceUtil::GetArchiveServiceForItemId] Encountered exception in getting archive service.  Exception class: {0}, Message: {1}", ex.GetType().FullName, ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x000DBC28 File Offset: 0x000D9E28
		internal static ExchangeServiceBinding GetArchiveServiceForItemIdList(CallContext callContext, IEnumerable<BaseItemId> itemIds)
		{
			return ComplianceUtil.GetArchiveServiceForItemId(callContext, itemIds.First<BaseItemId>());
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x000DBC36 File Offset: 0x000D9E36
		internal static ExchangeServiceBinding GetArchiveServiceForItemIdList(CallContext callContext, List<XmlNode> idNodes)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (idNodes == null)
			{
				throw new ArgumentNullException("idNodes");
			}
			if (idNodes.Count == 0)
			{
				return null;
			}
			return ComplianceUtil.GetArchiveServiceForSingleItemId(callContext, idNodes[0]);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x000DBC6C File Offset: 0x000D9E6C
		internal static ExchangeServiceBinding GetArchiveServiceForSingleItemId(CallContext callContext, XmlNode idNode)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (idNode == null)
			{
				throw new ArgumentNullException("idNode");
			}
			ExchangeServiceBinding result;
			try
			{
				result = ComplianceUtil.GetArchiveServiceForItemId(callContext, (XmlElement)idNode);
			}
			catch (LocalizedException ex)
			{
				ExTraceGlobals.ItemCallTracer.TraceError<string, string>(0L, "[ComplianceUtil::GetArchiveServiceForSingleId] Encountered exception in getting archive service.  Exception class: {0}, Message: {1}", ex.GetType().FullName, ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x000DBCDC File Offset: 0x000D9EDC
		internal static ExchangeServiceBinding GetArchiveServiceInfoForFolder(CallContext callContext, BaseFolderId folderId)
		{
			if (folderId is DistinguishedFolderId)
			{
				DistinguishedFolderId distinguishedFolderId = folderId as DistinguishedFolderId;
				if (IdConverter.IsArchiveDistinguishedFolderId(distinguishedFolderId))
				{
					string text = (distinguishedFolderId.Mailbox != null) ? distinguishedFolderId.Mailbox.EmailAddress : null;
					if (!string.IsNullOrEmpty(text))
					{
						ADUser aduser;
						ADIdentityInformationCache.Singleton.TryGetADUser(text, callContext.EffectiveCaller.GetADRecipientSessionContext(), out aduser);
						if (aduser != null && aduser.ArchiveState == ArchiveState.HostedProvisioned)
						{
							ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(aduser, null);
							if (callContext.AuthZBehavior.IsAllowedToPrivilegedOpenMailbox(exchangePrincipal))
							{
								return EwsClientHelper.GetArchiveServiceBinding(callContext.EffectiveCaller, exchangePrincipal, aduser, new string[]
								{
									"MailboxSearchApplication"
								});
							}
							ExTraceGlobals.FolderCallTracer.TraceError(0L, "[ComplianceUtil::GetArchiveServiceInfoForFolder] The caller is not allowed to open the target archive mailbox.  Skip getting archive service.");
						}
						else
						{
							ExTraceGlobals.FolderCallTracer.TraceError(0L, "[ComplianceUtil::GetArchiveServiceInfoForFolder] AD user was not found or the remote archive was not privisioned.  Skip getting archive service.");
						}
					}
					else
					{
						ExTraceGlobals.FolderCallTracer.TraceError(0L, "[ComplianceUtil::GetArchiveServiceInfoForFolder] Email address is not present.  Skip getting archive service.");
					}
				}
				else
				{
					ExTraceGlobals.FolderCallTracer.TraceDebug(0L, "[ComplianceUtil::GetArchiveServiceInfoForFolder] Folder is not an archive distinguished folder.  Skip getting archive service.");
				}
			}
			else
			{
				ExTraceGlobals.FolderCallTracer.TraceDebug(0L, "[ComplianceUtil::GetArchiveServiceInfoForFolder] Folder is not a distinguished folder.  Skip getting archive service.");
			}
			return null;
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x000DBDE4 File Offset: 0x000D9FE4
		internal static ExchangeServiceBinding GetArchiveServiceInfoForItem(CallContext callContext, IdHeaderInformation header)
		{
			if (header.IdStorageType == IdStorageType.MailboxItemMailboxGuidBased)
			{
				if (header.MailboxId != null && !string.IsNullOrEmpty(header.MailboxId.MailboxGuid))
				{
					Guid guid;
					if (Guid.TryParse(header.MailboxId.MailboxGuid, out guid))
					{
						ADUser aduser;
						ADIdentityInformationCache.Singleton.TryGetADUser(guid, callContext.EffectiveCaller.GetADRecipientSessionContext(), out aduser);
						if (aduser != null && aduser.ArchiveState == ArchiveState.HostedProvisioned && aduser.ArchiveGuid.Equals(guid))
						{
							ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(aduser, null);
							if (callContext.AuthZBehavior.IsAllowedToPrivilegedOpenMailbox(exchangePrincipal))
							{
								return EwsClientHelper.GetArchiveServiceBinding(callContext.EffectiveCaller, exchangePrincipal, aduser, new string[]
								{
									"MailboxSearchApplication"
								});
							}
							ExTraceGlobals.ItemCallTracer.TraceError(0L, "[ComplianceUtil::GetArchiveServiceInfoForItem] The caller is not allowed to open the target archive mailbox.  Skip getting archive service.");
						}
						else
						{
							ExTraceGlobals.ItemCallTracer.TraceError(0L, "[ComplianceUtil::GetArchiveServiceInfoForItem] AD user was not found or the remote archive was not privisioned or the mailbox guid in item id did not match archive mailbox guid.  Skip getting archive service.");
						}
					}
					else
					{
						ExTraceGlobals.ItemCallTracer.TraceError(0L, "[ComplianceUtil::GetArchiveServiceInfoForItem] MailboxId is not a valid guid.");
					}
				}
				else
				{
					ExTraceGlobals.ItemCallTracer.TraceError(0L, "[ComplianceUtil::GetArchiveServiceInfoForItem] MailboxId does not exist.");
				}
			}
			else
			{
				ExTraceGlobals.ItemCallTracer.TraceDebug(0L, "[ComplianceUtil::GetArchiveServiceInfoForItem] Unsupported storage type for determining cross premise archive: " + header.IdStorageType);
			}
			return null;
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x000DBF10 File Offset: 0x000DA110
		internal static ExchangeServiceBinding GetArchiveServiceForItemId(CallContext callContext, XmlElement singleIdXml)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (singleIdXml == null)
			{
				throw new ArgumentNullException("singleIdXml");
			}
			if (IdConverter.IsIdXml(singleIdXml))
			{
				string text;
				IdHeaderInformation header = IdConverter.ExtractIdInformation(singleIdXml, IdConverter.ConvertOption.IgnoreChangeKey, BasicTypes.Item, out text, null);
				return ComplianceUtil.GetArchiveServiceInfoForItem(callContext, header);
			}
			return null;
		}

		// Token: 0x04002463 RID: 9315
		private const string DiscoveryUserAgentSignature = "EDiscovery";

		// Token: 0x04002464 RID: 9316
		private const string MailboxSearchUserRole = "MailboxSearch";

		// Token: 0x04002465 RID: 9317
		private const string MailboxSearchApplicationRole = "MailboxSearchApplication";
	}
}
