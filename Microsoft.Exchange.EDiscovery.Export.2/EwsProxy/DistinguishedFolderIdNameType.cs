using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000F4 RID: 244
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum DistinguishedFolderIdNameType
	{
		// Token: 0x04000806 RID: 2054
		calendar,
		// Token: 0x04000807 RID: 2055
		contacts,
		// Token: 0x04000808 RID: 2056
		deleteditems,
		// Token: 0x04000809 RID: 2057
		drafts,
		// Token: 0x0400080A RID: 2058
		inbox,
		// Token: 0x0400080B RID: 2059
		journal,
		// Token: 0x0400080C RID: 2060
		notes,
		// Token: 0x0400080D RID: 2061
		outbox,
		// Token: 0x0400080E RID: 2062
		sentitems,
		// Token: 0x0400080F RID: 2063
		tasks,
		// Token: 0x04000810 RID: 2064
		msgfolderroot,
		// Token: 0x04000811 RID: 2065
		publicfoldersroot,
		// Token: 0x04000812 RID: 2066
		root,
		// Token: 0x04000813 RID: 2067
		junkemail,
		// Token: 0x04000814 RID: 2068
		searchfolders,
		// Token: 0x04000815 RID: 2069
		voicemail,
		// Token: 0x04000816 RID: 2070
		recoverableitemsroot,
		// Token: 0x04000817 RID: 2071
		recoverableitemsdeletions,
		// Token: 0x04000818 RID: 2072
		recoverableitemsversions,
		// Token: 0x04000819 RID: 2073
		recoverableitemspurges,
		// Token: 0x0400081A RID: 2074
		archiveroot,
		// Token: 0x0400081B RID: 2075
		archivemsgfolderroot,
		// Token: 0x0400081C RID: 2076
		archivedeleteditems,
		// Token: 0x0400081D RID: 2077
		archiveinbox,
		// Token: 0x0400081E RID: 2078
		archiverecoverableitemsroot,
		// Token: 0x0400081F RID: 2079
		archiverecoverableitemsdeletions,
		// Token: 0x04000820 RID: 2080
		archiverecoverableitemsversions,
		// Token: 0x04000821 RID: 2081
		archiverecoverableitemspurges,
		// Token: 0x04000822 RID: 2082
		syncissues,
		// Token: 0x04000823 RID: 2083
		conflicts,
		// Token: 0x04000824 RID: 2084
		localfailures,
		// Token: 0x04000825 RID: 2085
		serverfailures,
		// Token: 0x04000826 RID: 2086
		recipientcache,
		// Token: 0x04000827 RID: 2087
		quickcontacts,
		// Token: 0x04000828 RID: 2088
		conversationhistory,
		// Token: 0x04000829 RID: 2089
		adminauditlogs,
		// Token: 0x0400082A RID: 2090
		todosearch,
		// Token: 0x0400082B RID: 2091
		mycontacts,
		// Token: 0x0400082C RID: 2092
		directory,
		// Token: 0x0400082D RID: 2093
		imcontactlist,
		// Token: 0x0400082E RID: 2094
		peopleconnect,
		// Token: 0x0400082F RID: 2095
		favorites
	}
}
