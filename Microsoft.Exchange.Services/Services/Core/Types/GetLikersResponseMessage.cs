using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000500 RID: 1280
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetLikersResponseMessage : ResponseMessage
	{
		// Token: 0x0600250A RID: 9482 RVA: 0x000A55FA File Offset: 0x000A37FA
		public GetLikersResponseMessage()
		{
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000A5602 File Offset: 0x000A3802
		internal GetLikersResponseMessage(ServiceResultCode code, ServiceError error, GetLikersResponseMessage response) : base(code, error)
		{
			if (response != null)
			{
				this.personas = response.personas;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x000A561B File Offset: 0x000A381B
		// (set) Token: 0x0600250D RID: 9485 RVA: 0x000A5623 File Offset: 0x000A3823
		[DataMember]
		public Persona[] Personas
		{
			get
			{
				return this.personas;
			}
			set
			{
				this.personas = value;
			}
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000A562C File Offset: 0x000A382C
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetLikersResponseMessage;
		}

		// Token: 0x040015A6 RID: 5542
		private Persona[] personas;
	}
}
