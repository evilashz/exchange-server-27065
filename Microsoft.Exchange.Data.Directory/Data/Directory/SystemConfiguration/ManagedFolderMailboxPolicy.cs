using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000421 RID: 1057
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ManagedFolderMailboxPolicy : MailboxPolicy
	{
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x000C033F File Offset: 0x000BE53F
		internal override ADObjectSchema Schema
		{
			get
			{
				return ManagedFolderMailboxPolicy.schema;
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06002F91 RID: 12177 RVA: 0x000C0346 File Offset: 0x000BE546
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ManagedFolderMailboxPolicy.mostDerivedClass;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x000C034D File Offset: 0x000BE54D
		internal override ADObjectId ParentPath
		{
			get
			{
				return ManagedFolderMailboxPolicy.parentPath;
			}
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000C0354 File Offset: 0x000BE554
		internal override bool CheckForAssociatedUsers()
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.DistinguishedName, base.Id.DistinguishedName),
				new ExistsFilter(ManagedFolderMailboxPolicySchema.AssociatedUsers)
			});
			ManagedFolderMailboxPolicy[] array = base.Session.Find<ManagedFolderMailboxPolicy>(null, QueryScope.SubTree, filter, null, 1);
			return array != null && array.Length > 0;
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x000C03B1 File Offset: 0x000BE5B1
		// (set) Token: 0x06002F95 RID: 12181 RVA: 0x000C03C3 File Offset: 0x000BE5C3
		public MultiValuedProperty<ADObjectId> ManagedFolderLinks
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ManagedFolderMailboxPolicySchema.ManagedFolderLinks];
			}
			set
			{
				this[ManagedFolderMailboxPolicySchema.ManagedFolderLinks] = value;
			}
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000C0414 File Offset: 0x000BE614
		internal ValidationError AddManagedFolderToPolicy(IConfigurationSession session, ELCFolder elcFolderToAdd)
		{
			ValidationError result;
			if (elcFolderToAdd.FolderType != ElcFolderType.ManagedCustomFolder)
			{
				ADObjectId adobjectId = this.ManagedFolderLinks.Find(delegate(ADObjectId id)
				{
					ELCFolder elcfolder = session.Read<ELCFolder>(id);
					return elcfolder != null && elcfolder.FolderType == elcFolderToAdd.FolderType;
				});
				if (adobjectId == null)
				{
					try
					{
						this.ManagedFolderLinks.Add(elcFolderToAdd.Id);
						return null;
					}
					catch (InvalidOperationException ex)
					{
						return new PropertyValidationError(DirectoryStrings.ErrorInvalidFolderLinksAddition(elcFolderToAdd.Name, ex.Message), ManagedFolderMailboxPolicySchema.ManagedFolderLinks, this);
					}
				}
				if (adobjectId == elcFolderToAdd.Id)
				{
					return new PropertyValidationError(DirectoryStrings.ErrorDuplicateManagedFolderAddition(elcFolderToAdd.Name), ManagedFolderMailboxPolicySchema.ManagedFolderLinks, this);
				}
				return new PropertyValidationError(DirectoryStrings.ErrorDefaultElcFolderTypeExists(elcFolderToAdd.Name, elcFolderToAdd.FolderType.ToString()), ManagedFolderMailboxPolicySchema.ManagedFolderLinks, this);
			}
			else
			{
				try
				{
					this.ManagedFolderLinks.Add(elcFolderToAdd.Id);
					result = null;
				}
				catch (InvalidOperationException ex2)
				{
					result = new PropertyValidationError(new LocalizedString(ex2.Message), ManagedFolderMailboxPolicySchema.ManagedFolderLinks, this);
				}
			}
			return result;
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x000C058C File Offset: 0x000BE78C
		internal bool AreDefaultManagedFolderLinksUnique(IConfigurationSession session)
		{
			List<ELCFolder> list = new List<ELCFolder>();
			foreach (ADObjectId entryId in this.ManagedFolderLinks)
			{
				ELCFolder elcFolderToCheck = session.Read<ELCFolder>(entryId);
				if (elcFolderToCheck != null && elcFolderToCheck.FolderType != ElcFolderType.ManagedCustomFolder)
				{
					ELCFolder elcfolder = list.Find((ELCFolder folder) => folder.FolderType == elcFolderToCheck.FolderType);
					if (elcfolder == null)
					{
						list.Add(elcFolderToCheck);
					}
					else if (elcfolder.Id.ObjectGuid != elcFolderToCheck.Id.ObjectGuid)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04002019 RID: 8217
		private static ManagedFolderMailboxPolicySchema schema = ObjectSchema.GetInstance<ManagedFolderMailboxPolicySchema>();

		// Token: 0x0400201A RID: 8218
		private static string mostDerivedClass = "msExchMailboxRecipientTemplate";

		// Token: 0x0400201B RID: 8219
		private static ADObjectId parentPath = new ADObjectId("CN=ELC Mailbox Policies");
	}
}
