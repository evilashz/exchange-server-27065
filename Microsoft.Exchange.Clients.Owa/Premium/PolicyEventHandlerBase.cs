using System;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200048F RID: 1167
	internal abstract class PolicyEventHandlerBase : OwaEventHandlerBase
	{
		// Token: 0x06002D23 RID: 11555 RVA: 0x000FDAC7 File Offset: 0x000FBCC7
		protected PolicyEventHandlerBase(PolicyProvider policyProvider)
		{
			this.policyProvider = policyProvider;
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000FDB10 File Offset: 0x000FBD10
		[OwaEventParameter("id", typeof(OwaStoreObjectId), false, true)]
		[OwaEvent("GetPolicyMenu")]
		[OwaEventVerb(OwaEventVerb.Get)]
		public void GetPolicyMenu()
		{
			if (!this.policyProvider.IsPolicyEnabled(base.UserContext.MailboxSession))
			{
				throw new OwaInvalidRequestException("The mailbox is not enabled for " + this.policyProvider.ToString());
			}
			OwaStoreObjectId owaStoreObjectId = base.GetParameter("id") as OwaStoreObjectId;
			PolicyContextMenuBase policyMenu = this.InternalGetPolicyMenu(ref owaStoreObjectId);
			if (owaStoreObjectId != null && !owaStoreObjectId.IsConversationId)
			{
				this.DoPolicy((MailboxSession)owaStoreObjectId.GetSession(base.UserContext), owaStoreObjectId.StoreObjectId, true, delegate(StoreObject storeObject)
				{
					bool isInherited;
					Guid? policyTag = this.policyProvider.GetPolicyTag(storeObject, out isInherited);
					policyMenu.SetStates(isInherited, policyTag);
				});
			}
			policyMenu.Render(this.Writer);
		}

		// Token: 0x06002D25 RID: 11557
		protected abstract PolicyContextMenuBase InternalGetPolicyMenu(ref OwaStoreObjectId itemId);

		// Token: 0x06002D26 RID: 11558 RVA: 0x000FDBEC File Offset: 0x000FBDEC
		[OwaEventParameter("tag", typeof(string), false, false)]
		[OwaEventParameter("id", typeof(OwaStoreObjectId), true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEvent("ApplyPolicy")]
		public void ApplyPolicy()
		{
			OwaStoreObjectId[] itemIdsFromParameter = this.GetItemIdsFromParameter();
			Guid tagGuid = new Guid((string)base.GetParameter("tag"));
			MailboxSession mailboxSession = (MailboxSession)itemIdsFromParameter[0].GetSession(base.UserContext);
			foreach (OwaStoreObjectId owaStoreObjectId in itemIdsFromParameter)
			{
				this.DoPolicy(mailboxSession, owaStoreObjectId.StoreObjectId, false, delegate(StoreObject storeObject)
				{
					this.policyProvider.ApplyPolicyTag(storeObject, tagGuid);
				});
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000FDC90 File Offset: 0x000FBE90
		[OwaEventParameter("id", typeof(OwaStoreObjectId), true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEvent("InheritPolicy")]
		public void InheritPolicy()
		{
			OwaStoreObjectId[] itemIdsFromParameter = this.GetItemIdsFromParameter();
			MailboxSession mailboxSession = (MailboxSession)itemIdsFromParameter[0].GetSession(base.UserContext);
			foreach (OwaStoreObjectId owaStoreObjectId in itemIdsFromParameter)
			{
				this.DoPolicy(mailboxSession, owaStoreObjectId.StoreObjectId, false, delegate(StoreObject storeObject)
				{
					this.policyProvider.ClearPolicyTag(storeObject);
				});
			}
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000FDCF8 File Offset: 0x000FBEF8
		private OwaStoreObjectId[] GetItemIdsFromParameter()
		{
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("id");
			if (ConversationUtilities.ContainsConversationItem(base.UserContext, array))
			{
				OwaStoreObjectId localFolderId = (OwaStoreObjectId)base.GetParameter("fId");
				array = ConversationUtilities.GetLocalItemIds(base.UserContext, array, localFolderId);
			}
			if (array.Length > 500)
			{
				throw new OwaInvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Setting move policy on {0} item(s) in a single request is not supported", new object[]
				{
					array.Length
				}));
			}
			return array;
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000FDD76 File Offset: 0x000FBF76
		private void DoPolicy(MailboxSession mailboxSession, StoreObjectId storeObjectId, bool readOnly, PolicyEventHandlerBase.PolicyDelegate policyDelegate)
		{
			if (storeObjectId.IsFolderId)
			{
				this.DoPolicyOnFolder(mailboxSession, storeObjectId, readOnly, policyDelegate);
				return;
			}
			this.DoPolicyOnItem(mailboxSession, storeObjectId, readOnly, policyDelegate);
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000FDD98 File Offset: 0x000FBF98
		private void DoPolicyOnFolder(MailboxSession mailboxSession, StoreObjectId folderId, bool readOnly, PolicyEventHandlerBase.PolicyDelegate policyDelegate)
		{
			using (Folder folder = this.policyProvider.OpenFolderForPolicyTag(mailboxSession, folderId))
			{
				policyDelegate(folder);
				if (!readOnly)
				{
					folder.Save();
				}
			}
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x000FDDE4 File Offset: 0x000FBFE4
		private void DoPolicyOnItem(MailboxSession mailboxSession, StoreObjectId messageId, bool readOnly, PolicyEventHandlerBase.PolicyDelegate policyDelegate)
		{
			using (Item item = this.policyProvider.OpenItemForPolicyTag(mailboxSession, messageId))
			{
				if (!readOnly)
				{
					item.OpenAsReadWrite();
				}
				policyDelegate(item);
				if (!readOnly)
				{
					item.Save(SaveMode.NoConflictResolution);
				}
			}
		}

		// Token: 0x04001DDD RID: 7645
		public const int MaxSelectionSize = 500;

		// Token: 0x04001DDE RID: 7646
		public const string MethodGetPolicyMenu = "GetPolicyMenu";

		// Token: 0x04001DDF RID: 7647
		public const string MethodApplyPolicy = "ApplyPolicy";

		// Token: 0x04001DE0 RID: 7648
		public const string MethodInheritPolicy = "InheritPolicy";

		// Token: 0x04001DE1 RID: 7649
		public const string Id = "id";

		// Token: 0x04001DE2 RID: 7650
		public const string FolderId = "fId";

		// Token: 0x04001DE3 RID: 7651
		public const string TagGuid = "tag";

		// Token: 0x04001DE4 RID: 7652
		private PolicyProvider policyProvider;

		// Token: 0x02000490 RID: 1168
		// (Invoke) Token: 0x06002D2E RID: 11566
		private delegate void PolicyDelegate(StoreObject storeObject);
	}
}
