using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000A2 RID: 162
	internal abstract class FolderWrapper : WrapperBase<IFolder>, IFolder, IDisposable
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x00037B99 File Offset: 0x00035D99
		public FolderWrapper(IFolder folder, CommonUtils.CreateContextDelegate createContext) : base(folder, createContext)
		{
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00037BD0 File Offset: 0x00035DD0
		FolderRec IFolder.GetFolderRec(PropTag[] additionalPtagsToLoad, GetFolderRecFlags flags)
		{
			FolderRec result = null;
			base.CreateContext("IFolder.GetFolderRec", new DataContext[]
			{
				new PropTagsDataContext(additionalPtagsToLoad),
				new SimpleValueDataContext("Flags", flags)
			}).Execute(delegate
			{
				result = this.WrappedObject.GetFolderRec(additionalPtagsToLoad, flags);
			}, true);
			return result;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00037C78 File Offset: 0x00035E78
		byte[] IFolder.GetFolderId()
		{
			byte[] result = null;
			base.CreateContext("IFolder.GetFolderID", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetFolderId();
			}, true);
			return result;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00037CF4 File Offset: 0x00035EF4
		List<MessageRec> IFolder.EnumerateMessages(EnumerateMessagesFlags emFlags, PropTag[] additionalPtagsToLoad)
		{
			List<MessageRec> result = null;
			base.CreateContext("IFolder.EnumerateMessages", new DataContext[]
			{
				new SimpleValueDataContext("Flags", emFlags),
				new PropTagsDataContext(additionalPtagsToLoad)
			}).Execute(delegate
			{
				result = this.WrappedObject.EnumerateMessages(emFlags, additionalPtagsToLoad);
			}, true);
			return result;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00037DAC File Offset: 0x00035FAC
		List<MessageRec> IFolder.LookupMessages(PropTag ptagToLookup, List<byte[]> keysToLookup, PropTag[] additionalPtagsToLoad)
		{
			List<MessageRec> result = null;
			base.CreateContext("IFolder.LookupMessages", new DataContext[]
			{
				new SimpleValueDataContext("PropTag", ptagToLookup),
				new EntryIDsDataContext(keysToLookup),
				new PropTagsDataContext(additionalPtagsToLoad)
			}).Execute(delegate
			{
				result = this.WrappedObject.LookupMessages(ptagToLookup, keysToLookup, additionalPtagsToLoad);
			}, true);
			return result;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00037E6C File Offset: 0x0003606C
		RawSecurityDescriptor IFolder.GetSecurityDescriptor(SecurityProp secProp)
		{
			RawSecurityDescriptor result = null;
			base.CreateContext("IFolder.GetSecurityDescriptor", new DataContext[]
			{
				new SimpleValueDataContext("SecProp", secProp)
			}).Execute(delegate
			{
				result = this.WrappedObject.GetSecurityDescriptor(secProp);
			}, true);
			return result;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00037EFC File Offset: 0x000360FC
		void IFolder.SetContentsRestriction(RestrictionData restriction)
		{
			base.CreateContext("IFolder.SetContentsRestriction", new DataContext[]
			{
				new RestrictionDataContext(restriction)
			}).Execute(delegate
			{
				this.WrappedObject.SetContentsRestriction(restriction);
			}, true);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00037F78 File Offset: 0x00036178
		void IFolder.DeleteMessages(byte[][] entryIds)
		{
			base.CreateContext("IFolder.DeleteMessages", new DataContext[]
			{
				new EntryIDsDataContext(entryIds)
			}).Execute(delegate
			{
				this.WrappedObject.DeleteMessages(entryIds);
			}, true);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00037FF8 File Offset: 0x000361F8
		PropValueData[] IFolder.GetProps(PropTag[] pta)
		{
			PropValueData[] result = null;
			base.CreateContext("IFolder.GetProps", new DataContext[]
			{
				new PropTagsDataContext(pta)
			}).Execute(delegate
			{
				result = this.WrappedObject.GetProps(pta);
			}, true);
			return result;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0003808C File Offset: 0x0003628C
		void IFolder.GetSearchCriteria(out RestrictionData restriction, out byte[][] entryIds, out SearchState state)
		{
			RestrictionData restrictionInt = null;
			byte[][] entryIdsInt = null;
			SearchState stateInt = SearchState.None;
			base.CreateContext("IFolder.GetSearchCriteria", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.GetSearchCriteria(out restrictionInt, out entryIdsInt, out stateInt);
			}, true);
			restriction = restrictionInt;
			entryIds = entryIdsInt;
			state = stateInt;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00038124 File Offset: 0x00036324
		RuleData[] IFolder.GetRules(PropTag[] extraProps)
		{
			RuleData[] result = null;
			base.CreateContext("IFolder.GetRules", new DataContext[]
			{
				new PropTagsDataContext(extraProps)
			}).Execute(delegate
			{
				result = this.WrappedObject.GetRules(extraProps);
			}, true);
			return result;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000381B0 File Offset: 0x000363B0
		PropValueData[][] IFolder.GetACL(SecurityProp secProp)
		{
			PropValueData[][] result = null;
			base.CreateContext("IFolder.GetACL", new DataContext[]
			{
				new SimpleValueDataContext("SecProp", secProp)
			}).Execute(delegate
			{
				result = this.WrappedObject.GetACL(secProp);
			}, true);
			return result;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00038248 File Offset: 0x00036448
		PropValueData[][] IFolder.GetExtendedAcl(AclFlags aclFlags)
		{
			PropValueData[][] result = null;
			base.CreateContext("IFolder.GetExtendedAcl", new DataContext[]
			{
				new SimpleValueDataContext("AclFlags", aclFlags)
			}).Execute(delegate
			{
				result = this.WrappedObject.GetExtendedAcl(aclFlags);
			}, true);
			return result;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000382E0 File Offset: 0x000364E0
		PropProblemData[] IFolder.SetProps(PropValueData[] pva)
		{
			PropProblemData[] result = null;
			base.CreateContext("IFolder.SetProps", new DataContext[]
			{
				new PropValuesDataContext(pva)
			}).Execute(delegate
			{
				result = this.WrappedObject.SetProps(pva);
			}, true);
			return result;
		}
	}
}
