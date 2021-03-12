using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000061 RID: 97
	internal class ProvisionedFolder
	{
		// Token: 0x06000366 RID: 870 RVA: 0x00017BCC File Offset: 0x00015DCC
		internal ProvisionedFolder(StoreObjectId folderId, string displayName, string fullFolderPath, string containerClass, bool isProvisionedFolder, ContentSetting[] elcContentSettings, Guid elcFolderGuid, bool inheritedPolicy)
		{
			this.folderId = folderId;
			this.displayName = displayName;
			this.fullFolderPath = fullFolderPath;
			this.containerClass = containerClass;
			this.isProvisionedFolder = isProvisionedFolder;
			this.elcFolderGuid = elcFolderGuid;
			this.inheritedPolicy = inheritedPolicy;
			if (elcContentSettings != null)
			{
				this.elcPolicies = new List<ElcPolicySettings>();
				foreach (ContentSetting elcContentSetting in elcContentSettings)
				{
					ElcPolicySettings.ParseContentSettings(this.elcPolicies, elcContentSetting);
				}
				this.elcPolicies.Sort(delegate(ElcPolicySettings first, ElcPolicySettings second)
				{
					if (first == second)
					{
						return 0;
					}
					string text = first.MessageClass.TrimEnd(new char[]
					{
						'*'
					});
					string text2 = second.MessageClass.TrimEnd(new char[]
					{
						'*'
					});
					if (text.Length > text2.Length)
					{
						return -1;
					}
					if (text.Length < text2.Length)
					{
						return 1;
					}
					if (first.MessageClass.EndsWith("*"))
					{
						return 1;
					}
					if (second.MessageClass.EndsWith("*"))
					{
						return -1;
					}
					return 0;
				});
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00017C82 File Offset: 0x00015E82
		internal StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00017C8A File Offset: 0x00015E8A
		internal List<ElcPolicySettings> ElcPolicies
		{
			get
			{
				return this.elcPolicies;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00017C92 File Offset: 0x00015E92
		internal Dictionary<string, ContentSetting> ItemClassToPolicyMapping
		{
			get
			{
				return this.itemClassToPolicyMapping;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00017C9A File Offset: 0x00015E9A
		internal bool IsProvisionedFolder
		{
			get
			{
				return this.isProvisionedFolder;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00017CA2 File Offset: 0x00015EA2
		internal string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00017CAA File Offset: 0x00015EAA
		internal string FullFolderPath
		{
			get
			{
				return this.fullFolderPath;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00017CB2 File Offset: 0x00015EB2
		internal string ContainerClass
		{
			get
			{
				return this.containerClass;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00017CBA File Offset: 0x00015EBA
		internal Guid ElcFolderGuid
		{
			get
			{
				return this.elcFolderGuid;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00017CC2 File Offset: 0x00015EC2
		internal bool InheritedPolicy
		{
			get
			{
				return this.inheritedPolicy;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00017CCA File Offset: 0x00015ECA
		internal Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00017CD2 File Offset: 0x00015ED2
		internal List<string> ValidatedPolicies
		{
			get
			{
				return this.validatedPolicies;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00017CDA File Offset: 0x00015EDA
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00017CE2 File Offset: 0x00015EE2
		internal VersionedId[] CurrentItems
		{
			get
			{
				return this.currentItems;
			}
			set
			{
				this.currentItems = value;
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00017D20 File Offset: 0x00015F20
		internal void RemovePolicy(ContentSetting policyToRemove)
		{
			if (policyToRemove == null)
			{
				return;
			}
			this.elcPolicies.RemoveAll((ElcPolicySettings policy) => policy.ContentSettings.Guid.Equals(policyToRemove.Guid));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00017D5C File Offset: 0x00015F5C
		internal bool BindToFolder(MailboxSession mailboxSession)
		{
			try
			{
				this.folder = Folder.Bind(mailboxSession, this.folderId);
			}
			catch (ObjectNotFoundException arg)
			{
				ProvisionedFolder.Tracer.TraceDebug<string, StoreObjectId, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: Failed to open folder '{1}' because the folder was not found or was inaccessible. Exception: '{2}'", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), this.folderId, arg);
				return false;
			}
			return true;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00017DD0 File Offset: 0x00015FD0
		internal void DisposeFolder()
		{
			if (this.folder != null)
			{
				this.folder.Dispose();
				this.folder = null;
			}
		}

		// Token: 0x040002D7 RID: 727
		private static readonly Trace Tracer = ExTraceGlobals.CommonEnforcerOperationsTracer;

		// Token: 0x040002D8 RID: 728
		private StoreObjectId folderId;

		// Token: 0x040002D9 RID: 729
		private List<ElcPolicySettings> elcPolicies;

		// Token: 0x040002DA RID: 730
		private bool isProvisionedFolder;

		// Token: 0x040002DB RID: 731
		private string displayName;

		// Token: 0x040002DC RID: 732
		private string fullFolderPath;

		// Token: 0x040002DD RID: 733
		private string containerClass;

		// Token: 0x040002DE RID: 734
		private Guid elcFolderGuid;

		// Token: 0x040002DF RID: 735
		private bool inheritedPolicy;

		// Token: 0x040002E0 RID: 736
		private Folder folder;

		// Token: 0x040002E1 RID: 737
		private Dictionary<string, ContentSetting> itemClassToPolicyMapping = new Dictionary<string, ContentSetting>();

		// Token: 0x040002E2 RID: 738
		private List<string> validatedPolicies = new List<string>();

		// Token: 0x040002E3 RID: 739
		private VersionedId[] currentItems;
	}
}
