using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x0200074A RID: 1866
	[DataContract]
	public class LinkedInConnections : IExtensibleDataObject
	{
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x0004C47E File Offset: 0x0004A67E
		// (set) Token: 0x0600246E RID: 9326 RVA: 0x0004C486 File Offset: 0x0004A686
		[DataMember(Name = "values")]
		public List<LinkedInPerson> People { get; set; }

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x0004C48F File Offset: 0x0004A68F
		// (set) Token: 0x06002470 RID: 9328 RVA: 0x0004C497 File Offset: 0x0004A697
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
