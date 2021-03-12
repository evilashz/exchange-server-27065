using System;
using System.Security.AccessControl;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200013E RID: 318
	internal interface IDestinationFolder : IFolder, IDisposable
	{
		// Token: 0x06000A77 RID: 2679
		PropProblemData[] SetSecurityDescriptor(SecurityProp secProp, RawSecurityDescriptor sd);

		// Token: 0x06000A78 RID: 2680
		bool SetSearchCriteria(RestrictionData restriction, byte[][] entryIds, SearchCriteriaFlags flags);

		// Token: 0x06000A79 RID: 2681
		IFxProxy GetFxProxy(FastTransferFlags flags);

		// Token: 0x06000A7A RID: 2682
		void SetReadFlagsOnMessages(SetReadFlags flags, byte[][] entryIds);

		// Token: 0x06000A7B RID: 2683
		void SetRules(RuleData[] rules);

		// Token: 0x06000A7C RID: 2684
		void SetACL(SecurityProp secProp, PropValueData[][] aclData);

		// Token: 0x06000A7D RID: 2685
		void SetExtendedAcl(AclFlags aclFlags, PropValueData[][] aclData);

		// Token: 0x06000A7E RID: 2686
		void Flush();

		// Token: 0x06000A7F RID: 2687
		Guid LinkMailPublicFolder(LinkMailPublicFolderFlags flags, byte[] objectId);

		// Token: 0x06000A80 RID: 2688
		void SetMessageProps(byte[] entryId, PropValueData[] propValues);
	}
}
