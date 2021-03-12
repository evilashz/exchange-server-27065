using System;
using System.Security.AccessControl;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000A3 RID: 163
	internal class DestinationFolderWrapper : FolderWrapper, IDestinationFolder, IFolder, IDisposable
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x00038346 File Offset: 0x00036546
		public DestinationFolderWrapper(IDestinationFolder folder, CommonUtils.CreateContextDelegate createContext) : base(folder, createContext)
		{
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00038384 File Offset: 0x00036584
		PropProblemData[] IDestinationFolder.SetSecurityDescriptor(SecurityProp secProp, RawSecurityDescriptor sd)
		{
			PropProblemData[] result = null;
			base.CreateContext("IDestinationFolder.SetSecurityDescriptor", new DataContext[]
			{
				new SimpleValueDataContext("SecProp", secProp),
				new SimpleValueDataContext("SD", CommonUtils.GetSDDLString(sd))
			}).Execute(delegate
			{
				result = ((IDestinationFolder)this.WrappedObject).SetSecurityDescriptor(secProp, sd);
			}, true);
			return result;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0003844C File Offset: 0x0003664C
		bool IDestinationFolder.SetSearchCriteria(RestrictionData restriction, byte[][] entryIds, SearchCriteriaFlags flags)
		{
			bool result = false;
			base.CreateContext("IDestinationFolder.SetSearchCriteria", new DataContext[]
			{
				new RestrictionDataContext(restriction),
				new EntryIDsDataContext(entryIds),
				new SimpleValueDataContext("Flags", flags)
			}).Execute(delegate
			{
				result = ((IDestinationFolder)this.WrappedObject).SetSearchCriteria(restriction, entryIds, flags);
			}, true);
			return result;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00038514 File Offset: 0x00036714
		IFxProxy IDestinationFolder.GetFxProxy(FastTransferFlags flags)
		{
			IFxProxy result = null;
			base.CreateContext("IDestinationFolder.GetFxProxy", new DataContext[0]).Execute(delegate
			{
				result = ((IDestinationFolder)this.WrappedObject).GetFxProxy(flags);
			}, true);
			if (result == null)
			{
				return null;
			}
			return new FxProxyWrapper(result, base.CreateContext);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000385AC File Offset: 0x000367AC
		void IDestinationFolder.SetReadFlagsOnMessages(SetReadFlags flags, byte[][] entryIds)
		{
			base.CreateContext("IDestinationFolder.SetReadFlagsOnMessages", new DataContext[]
			{
				new SimpleValueDataContext("Flags", flags),
				new EntryIDsDataContext(entryIds)
			}).Execute(delegate
			{
				((IDestinationFolder)this.WrappedObject).SetReadFlagsOnMessages(flags, entryIds);
			}, true);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00038650 File Offset: 0x00036850
		void IDestinationFolder.SetMessageProps(byte[] entryId, PropValueData[] propValues)
		{
			base.CreateContext("IDestinationFolder.SetMessageProps", new DataContext[]
			{
				new EntryIDsDataContext(entryId)
			}).Execute(delegate
			{
				((IDestinationFolder)this.WrappedObject).SetMessageProps(entryId, propValues);
			}, true);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000386D8 File Offset: 0x000368D8
		void IDestinationFolder.SetRules(RuleData[] rules)
		{
			base.CreateContext("IDestinationFolder.SetRules", new DataContext[]
			{
				new RulesDataContext(rules)
			}).Execute(delegate
			{
				((IDestinationFolder)this.WrappedObject).SetRules(rules);
			}, true);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0003875C File Offset: 0x0003695C
		void IDestinationFolder.SetACL(SecurityProp secProp, PropValueData[][] aclData)
		{
			base.CreateContext("IDestinationFolder.SetACL", new DataContext[]
			{
				new SimpleValueDataContext("SecProp", secProp),
				new PropValuesDataContext(aclData)
			}).Execute(delegate
			{
				((IDestinationFolder)this.WrappedObject).SetACL(secProp, aclData);
			}, true);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00038800 File Offset: 0x00036A00
		void IDestinationFolder.SetExtendedAcl(AclFlags aclFlags, PropValueData[][] aclData)
		{
			base.CreateContext("IDestinationFolder.SetExtendedAcl", new DataContext[]
			{
				new SimpleValueDataContext("AclFlags", aclFlags),
				new PropValuesDataContext(aclData)
			}).Execute(delegate
			{
				((IDestinationFolder)this.WrappedObject).SetExtendedAcl(aclFlags, aclData);
			}, true);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0003888A File Offset: 0x00036A8A
		void IDestinationFolder.Flush()
		{
			base.CreateContext("IDestinationFolder.Flush", new DataContext[0]).Execute(delegate
			{
				((IDestinationFolder)base.WrappedObject).Flush();
			}, true);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000388E8 File Offset: 0x00036AE8
		Guid IDestinationFolder.LinkMailPublicFolder(LinkMailPublicFolderFlags flags, byte[] objectId)
		{
			Guid result = Guid.Empty;
			base.CreateContext("IDestinationFolder.LinkMailPublicFolder", new DataContext[]
			{
				new SimpleValueDataContext("Flags", flags),
				new EntryIDsDataContext(objectId)
			}).Execute(delegate
			{
				result = ((IDestinationFolder)this.WrappedObject).LinkMailPublicFolder(flags, objectId);
			}, true);
			return result;
		}
	}
}
