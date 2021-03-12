using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004F1 RID: 1265
	internal sealed class PolicyProvider
	{
		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x0011AFF1 File Offset: 0x001191F1
		// (set) Token: 0x0600303F RID: 12351 RVA: 0x0011AFF9 File Offset: 0x001191F9
		private PolicyProvider.GetPoliciesDelegate GetPolicies { get; set; }

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x0011B002 File Offset: 0x00119202
		// (set) Token: 0x06003041 RID: 12353 RVA: 0x0011B00A File Offset: 0x0011920A
		private PolicyProvider.GetPolicyTagFromFolderDelegate GetPolicyTagFromFolder { get; set; }

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x0011B013 File Offset: 0x00119213
		// (set) Token: 0x06003043 RID: 12355 RVA: 0x0011B01B File Offset: 0x0011921B
		private PolicyProvider.GetPolicyTagFromItemDelegate GetPolicyTagFromItem { get; set; }

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x0011B024 File Offset: 0x00119224
		// (set) Token: 0x06003045 RID: 12357 RVA: 0x0011B02C File Offset: 0x0011922C
		private PolicyProvider.SetPolicyTagOnFolderDelegate SetPolicyTagOnFolder { get; set; }

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x0011B035 File Offset: 0x00119235
		// (set) Token: 0x06003047 RID: 12359 RVA: 0x0011B03D File Offset: 0x0011923D
		private PolicyProvider.SetPolicyTagOnItemDelegate SetPolicyTagOnItem { get; set; }

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06003048 RID: 12360 RVA: 0x0011B046 File Offset: 0x00119246
		// (set) Token: 0x06003049 RID: 12361 RVA: 0x0011B04E File Offset: 0x0011924E
		private PolicyProvider.ClearPolicyTagOnFolderDelegate ClearPolicyTagOnFolder { get; set; }

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x0011B057 File Offset: 0x00119257
		// (set) Token: 0x0600304B RID: 12363 RVA: 0x0011B05F File Offset: 0x0011925F
		private PolicyProvider.ClearPolicyTagOnItemDelegate ClearPolicyTagOnItem { get; set; }

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x0011B068 File Offset: 0x00119268
		// (set) Token: 0x0600304D RID: 12365 RVA: 0x0011B070 File Offset: 0x00119270
		private PropertyDefinition[] PolicyProperties { get; set; }

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x0011B079 File Offset: 0x00119279
		// (set) Token: 0x0600304F RID: 12367 RVA: 0x0011B081 File Offset: 0x00119281
		private string PolicyType { get; set; }

		// Token: 0x06003050 RID: 12368 RVA: 0x0011B08A File Offset: 0x0011928A
		private PolicyProvider()
		{
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x0011B094 File Offset: 0x00119294
		internal bool IsPolicyEnabled(MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			PolicyTagList allPolicies = this.GetAllPolicies(mailboxSession);
			return allPolicies != null && allPolicies.Count > 0;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x0011B0C5 File Offset: 0x001192C5
		internal PolicyTagList GetAllPolicies(MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			return this.GetPolicies(mailboxSession);
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x0011B0E4 File Offset: 0x001192E4
		internal void ClearPolicyTag(StoreObject itemOrFolder)
		{
			if (itemOrFolder == null)
			{
				throw new ArgumentNullException("itemOrFolder");
			}
			this.SetPolicyTag(itemOrFolder, true, null);
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x0011B110 File Offset: 0x00119310
		internal void ApplyPolicyTag(StoreObject itemOrFolder, Guid policyGuid)
		{
			if (itemOrFolder == null)
			{
				throw new ArgumentNullException("itemOrFolder");
			}
			this.SetPolicyTag(itemOrFolder, false, new Guid?(policyGuid));
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x0011B130 File Offset: 0x00119330
		private void SetPolicyTag(StoreObject itemOrFolder, bool isInherited, Guid? policyGuid)
		{
			MailboxSession mailboxSession = itemOrFolder.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new NotSupportedException("Only support item or folder in mailbox.");
			}
			if (!this.IsPolicyEnabled(mailboxSession))
			{
				throw new InvalidOperationException("The mailbox is not enabled for " + this.PolicyType);
			}
			if (isInherited)
			{
				if (itemOrFolder is Folder)
				{
					this.ClearPolicyTagOnFolder((Folder)itemOrFolder);
					return;
				}
				this.ClearPolicyTagOnItem(itemOrFolder);
				return;
			}
			else
			{
				PolicyTagList allPolicies = this.GetAllPolicies(mailboxSession);
				PolicyTag policyTag;
				if (!allPolicies.TryGetValue(policyGuid.Value, out policyTag))
				{
					throw new ArgumentException("policyGuid is invalid");
				}
				if (itemOrFolder is Folder)
				{
					this.SetPolicyTagOnFolder(policyTag, (Folder)itemOrFolder);
					return;
				}
				this.SetPolicyTagOnItem(policyTag, itemOrFolder);
				return;
			}
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x0011B1EC File Offset: 0x001193EC
		internal Guid? GetPolicyTag(StoreObject itemOrFolder, out bool isInherited)
		{
			if (itemOrFolder == null)
			{
				throw new ArgumentNullException("itemOrFolder");
			}
			bool flag;
			Guid? result;
			if (itemOrFolder is Folder)
			{
				result = this.GetPolicyTagFromFolder((Folder)itemOrFolder, out flag);
			}
			else
			{
				DateTime? dateTime;
				result = this.GetPolicyTagFromItem(itemOrFolder, out flag, out dateTime);
			}
			isInherited = !flag;
			return result;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x0011B23D File Offset: 0x0011943D
		internal Item OpenItemForPolicyTag(MailboxSession mailboxSession, StoreObjectId messageId)
		{
			return Item.Bind(mailboxSession, messageId, this.PolicyProperties);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x0011B24C File Offset: 0x0011944C
		internal Folder OpenFolderForPolicyTag(MailboxSession mailboxSession, StoreObjectId folderId)
		{
			return Folder.Bind(mailboxSession, folderId, this.PolicyProperties);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x0011B25B File Offset: 0x0011945B
		public override string ToString()
		{
			return this.PolicyType;
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x0011B290 File Offset: 0x00119490
		// Note: this type is marked as 'beforefieldinit'.
		static PolicyProvider()
		{
			PolicyProvider policyProvider = new PolicyProvider();
			policyProvider.PolicyType = "DeletePolicy";
			policyProvider.GetPolicies = ((MailboxSession mailboxSession) => mailboxSession.GetPolicyTagList(RetentionActionType.DeleteAndAllowRecovery));
			policyProvider.GetPolicyTagFromFolder = new PolicyProvider.GetPolicyTagFromFolderDelegate(PolicyTagHelper.GetPolicyTagForDeleteFromFolder);
			policyProvider.GetPolicyTagFromItem = new PolicyProvider.GetPolicyTagFromItemDelegate(PolicyTagHelper.GetPolicyTagForDeleteFromItem);
			policyProvider.SetPolicyTagOnFolder = new PolicyProvider.SetPolicyTagOnFolderDelegate(PolicyTagHelper.SetPolicyTagForDeleteOnFolder);
			policyProvider.SetPolicyTagOnItem = new PolicyProvider.SetPolicyTagOnItemDelegate(PolicyTagHelper.SetPolicyTagForDeleteOnItem);
			policyProvider.ClearPolicyTagOnFolder = new PolicyProvider.ClearPolicyTagOnFolderDelegate(PolicyTagHelper.ClearPolicyTagForDeleteOnFolder);
			policyProvider.ClearPolicyTagOnItem = new PolicyProvider.ClearPolicyTagOnItemDelegate(PolicyTagHelper.ClearPolicyTagForDeleteOnItem);
			policyProvider.PolicyProperties = PolicyTagHelper.RetentionProperties;
			PolicyProvider.DeletePolicyProvider = policyProvider;
			PolicyProvider policyProvider2 = new PolicyProvider();
			policyProvider2.PolicyType = "MovePolicy";
			policyProvider2.GetPolicies = ((MailboxSession mailboxSession) => mailboxSession.GetPolicyTagList(RetentionActionType.MoveToArchive));
			policyProvider2.GetPolicyTagFromFolder = new PolicyProvider.GetPolicyTagFromFolderDelegate(PolicyTagHelper.GetPolicyTagForArchiveFromFolder);
			policyProvider2.GetPolicyTagFromItem = delegate(StoreObject item, out bool isExplicit, out DateTime? moveToArchive)
			{
				bool flag;
				return PolicyTagHelper.GetPolicyTagForArchiveFromItem(item, out isExplicit, out flag, out moveToArchive);
			};
			policyProvider2.SetPolicyTagOnFolder = new PolicyProvider.SetPolicyTagOnFolderDelegate(PolicyTagHelper.SetPolicyTagForArchiveOnFolder);
			policyProvider2.SetPolicyTagOnItem = new PolicyProvider.SetPolicyTagOnItemDelegate(PolicyTagHelper.SetPolicyTagForArchiveOnItem);
			policyProvider2.ClearPolicyTagOnFolder = new PolicyProvider.ClearPolicyTagOnFolderDelegate(PolicyTagHelper.ClearPolicyTagForArchiveOnFolder);
			policyProvider2.ClearPolicyTagOnItem = new PolicyProvider.ClearPolicyTagOnItemDelegate(PolicyTagHelper.ClearPolicyTagForArchiveOnItem);
			policyProvider2.PolicyProperties = PolicyTagHelper.ArchiveProperties;
			PolicyProvider.MovePolicyProvider = policyProvider2;
		}

		// Token: 0x040021AA RID: 8618
		internal static readonly PolicyProvider DeletePolicyProvider;

		// Token: 0x040021AB RID: 8619
		internal static readonly PolicyProvider MovePolicyProvider;

		// Token: 0x020004F2 RID: 1266
		// (Invoke) Token: 0x0600305F RID: 12383
		private delegate PolicyTagList GetPoliciesDelegate(MailboxSession mailboxSession);

		// Token: 0x020004F3 RID: 1267
		// (Invoke) Token: 0x06003063 RID: 12387
		private delegate Guid? GetPolicyTagFromFolderDelegate(Folder folder, out bool isExplicit);

		// Token: 0x020004F4 RID: 1268
		// (Invoke) Token: 0x06003067 RID: 12391
		private delegate Guid? GetPolicyTagFromItemDelegate(StoreObject item, out bool isExplicit, out DateTime? actionDate);

		// Token: 0x020004F5 RID: 1269
		// (Invoke) Token: 0x0600306B RID: 12395
		private delegate void SetPolicyTagOnFolderDelegate(PolicyTag policyTag, Folder folder);

		// Token: 0x020004F6 RID: 1270
		// (Invoke) Token: 0x0600306F RID: 12399
		private delegate void SetPolicyTagOnItemDelegate(PolicyTag policyTag, StoreObject item);

		// Token: 0x020004F7 RID: 1271
		// (Invoke) Token: 0x06003073 RID: 12403
		private delegate void ClearPolicyTagOnFolderDelegate(Folder folder);

		// Token: 0x020004F8 RID: 1272
		// (Invoke) Token: 0x06003077 RID: 12407
		private delegate void ClearPolicyTagOnItemDelegate(StoreObject item);
	}
}
