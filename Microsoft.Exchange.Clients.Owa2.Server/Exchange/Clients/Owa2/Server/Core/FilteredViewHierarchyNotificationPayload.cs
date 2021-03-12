using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000172 RID: 370
	[DataContract]
	internal class FilteredViewHierarchyNotificationPayload : HierarchyNotificationPayload
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0003357E File Offset: 0x0003177E
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x00033590 File Offset: 0x00031790
		[DataMember(Name = "Filter", IsRequired = false)]
		public string FilterString
		{
			get
			{
				return this.Filter.ToString();
			}
			set
			{
				this.Filter = (ViewFilter)Enum.Parse(typeof(ViewFilter), value);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x000335AD File Offset: 0x000317AD
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x000335B5 File Offset: 0x000317B5
		[IgnoreDataMember]
		internal ViewFilter Filter { get; set; }
	}
}
