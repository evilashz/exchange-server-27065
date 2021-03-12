using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200013C RID: 316
	internal interface IFolder : IDisposable
	{
		// Token: 0x06000A65 RID: 2661
		FolderRec GetFolderRec(PropTag[] additionalPtagsToLoad, GetFolderRecFlags flags);

		// Token: 0x06000A66 RID: 2662
		byte[] GetFolderId();

		// Token: 0x06000A67 RID: 2663
		List<MessageRec> EnumerateMessages(EnumerateMessagesFlags emFlags, PropTag[] additionalPtagsToLoad);

		// Token: 0x06000A68 RID: 2664
		List<MessageRec> LookupMessages(PropTag ptagToLookup, List<byte[]> keysToLookup, PropTag[] additionalPtagsToLoad);

		// Token: 0x06000A69 RID: 2665
		RawSecurityDescriptor GetSecurityDescriptor(SecurityProp secProp);

		// Token: 0x06000A6A RID: 2666
		void SetContentsRestriction(RestrictionData restriction);

		// Token: 0x06000A6B RID: 2667
		void DeleteMessages(byte[][] entryIds);

		// Token: 0x06000A6C RID: 2668
		PropValueData[] GetProps(PropTag[] pta);

		// Token: 0x06000A6D RID: 2669
		void GetSearchCriteria(out RestrictionData restriction, out byte[][] entryIds, out SearchState state);

		// Token: 0x06000A6E RID: 2670
		RuleData[] GetRules(PropTag[] extraProps);

		// Token: 0x06000A6F RID: 2671
		PropValueData[][] GetACL(SecurityProp secProp);

		// Token: 0x06000A70 RID: 2672
		PropValueData[][] GetExtendedAcl(AclFlags aclFlags);

		// Token: 0x06000A71 RID: 2673
		PropProblemData[] SetProps(PropValueData[] pva);
	}
}
