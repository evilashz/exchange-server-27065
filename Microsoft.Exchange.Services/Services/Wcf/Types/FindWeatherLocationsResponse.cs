using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B58 RID: 2904
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindWeatherLocationsResponse : BaseJsonResponse
	{
		// Token: 0x0600525C RID: 21084 RVA: 0x0010B3A3 File Offset: 0x001095A3
		internal FindWeatherLocationsResponse()
		{
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x0600525D RID: 21085 RVA: 0x0010B3AB File Offset: 0x001095AB
		// (set) Token: 0x0600525E RID: 21086 RVA: 0x0010B3B3 File Offset: 0x001095B3
		[DataMember(IsRequired = false)]
		public string ErrorMessage { get; set; }

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x0600525F RID: 21087 RVA: 0x0010B3BC File Offset: 0x001095BC
		// (set) Token: 0x06005260 RID: 21088 RVA: 0x0010B3C4 File Offset: 0x001095C4
		[DataMember(IsRequired = true)]
		public WeatherLocationId[] WeatherLocationIds { get; set; }
	}
}
