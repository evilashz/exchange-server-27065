using System;
using System.Security.AccessControl;
using Microsoft.Exchange.PST;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000006 RID: 6
	internal class PstDestinationFolder : PstFolder, IDestinationFolder, IFolder, IDisposable
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003B72 File Offset: 0x00001D72
		bool IDestinationFolder.SetSearchCriteria(RestrictionData restriction, byte[][] entryIds, SearchCriteriaFlags flags)
		{
			return true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003B75 File Offset: 0x00001D75
		PropProblemData[] IDestinationFolder.SetSecurityDescriptor(SecurityProp secProp, RawSecurityDescriptor sd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003B7C File Offset: 0x00001D7C
		IFxProxy IDestinationFolder.GetFxProxy(FastTransferFlags flags)
		{
			MrsTracer.Provider.Function("PstDestinationFolder.IDestinationFolder.GetFxProxy", new object[0]);
			return new PSTFxProxy(base.Folder);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003B9E File Offset: 0x00001D9E
		void IDestinationFolder.SetReadFlagsOnMessages(SetReadFlags flags, byte[][] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003BA5 File Offset: 0x00001DA5
		void IDestinationFolder.SetMessageProps(byte[] entryId, PropValueData[] propValues)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003BAC File Offset: 0x00001DAC
		void IDestinationFolder.SetRules(RuleData[] rules)
		{
			MrsTracer.Provider.Function("PstDestinationFolder.IDestinationFolder.SetRules", new object[0]);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003BC3 File Offset: 0x00001DC3
		void IDestinationFolder.SetACL(SecurityProp secProp, PropValueData[][] aclData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003BCA File Offset: 0x00001DCA
		void IDestinationFolder.SetExtendedAcl(AclFlags aclFlags, PropValueData[][] aclData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003BD4 File Offset: 0x00001DD4
		void IDestinationFolder.Flush()
		{
			try
			{
				base.Folder.PstMailbox.IPst.Flush();
			}
			catch (PSTExceptionBase innerException)
			{
				throw new UnableToCreatePSTMessagePermanentException(base.Folder.PstMailbox.IPst.FileName, innerException);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003C28 File Offset: 0x00001E28
		Guid IDestinationFolder.LinkMailPublicFolder(LinkMailPublicFolderFlags flags, byte[] objectId)
		{
			throw new NotImplementedException();
		}
	}
}
