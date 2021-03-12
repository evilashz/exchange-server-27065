using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000729 RID: 1833
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FacebookUsersList
	{
		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x00047996 File Offset: 0x00045B96
		// (set) Token: 0x060022FA RID: 8954 RVA: 0x0004799E File Offset: 0x00045B9E
		[DataMember(Name = "data")]
		public List<FacebookUser> Users { get; set; }

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x000479A7 File Offset: 0x00045BA7
		// (set) Token: 0x060022FC RID: 8956 RVA: 0x000479AF File Offset: 0x00045BAF
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
