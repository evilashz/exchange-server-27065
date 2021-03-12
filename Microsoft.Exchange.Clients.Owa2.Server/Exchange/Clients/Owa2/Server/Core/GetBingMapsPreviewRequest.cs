using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003C3 RID: 963
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetBingMapsPreviewRequest
	{
		// Token: 0x06001ED3 RID: 7891 RVA: 0x00076C0B File Offset: 0x00074E0B
		public GetBingMapsPreviewRequest()
		{
			this.Latitude = null;
			this.Longitude = null;
			this.LocationName = null;
			this.MapWidth = 0;
			this.MapHeight = 0;
			this.ParentItemId = null;
			this.MapControlKey = null;
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x00076C44 File Offset: 0x00074E44
		// (set) Token: 0x06001ED5 RID: 7893 RVA: 0x00076C4C File Offset: 0x00074E4C
		[DataMember]
		public string Latitude { get; set; }

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x00076C55 File Offset: 0x00074E55
		// (set) Token: 0x06001ED7 RID: 7895 RVA: 0x00076C5D File Offset: 0x00074E5D
		[DataMember]
		public string Longitude { get; set; }

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x00076C66 File Offset: 0x00074E66
		// (set) Token: 0x06001ED9 RID: 7897 RVA: 0x00076C6E File Offset: 0x00074E6E
		[DataMember]
		public string LocationName { get; set; }

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x00076C77 File Offset: 0x00074E77
		// (set) Token: 0x06001EDB RID: 7899 RVA: 0x00076C7F File Offset: 0x00074E7F
		[DataMember]
		public int MapWidth { get; set; }

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x00076C88 File Offset: 0x00074E88
		// (set) Token: 0x06001EDD RID: 7901 RVA: 0x00076C90 File Offset: 0x00074E90
		[DataMember]
		public int MapHeight { get; set; }

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x00076C99 File Offset: 0x00074E99
		// (set) Token: 0x06001EDF RID: 7903 RVA: 0x00076CA1 File Offset: 0x00074EA1
		[DataMember]
		public ItemId ParentItemId { get; set; }

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x00076CAA File Offset: 0x00074EAA
		// (set) Token: 0x06001EE1 RID: 7905 RVA: 0x00076CB2 File Offset: 0x00074EB2
		[DataMember]
		public string MapControlKey { get; set; }
	}
}
