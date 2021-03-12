using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000545 RID: 1349
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("ResolveNamesResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ResolveNamesResponseMessage : ResponseMessage
	{
		// Token: 0x06002638 RID: 9784 RVA: 0x000A6412 File Offset: 0x000A4612
		public ResolveNamesResponseMessage()
		{
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000A641A File Offset: 0x000A461A
		internal ResolveNamesResponseMessage(ServiceResultCode code, ServiceError error, ResolutionSet resolutionSet) : base(code, error)
		{
			this.resolutionSet = resolutionSet;
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x000A642B File Offset: 0x000A462B
		// (set) Token: 0x0600263B RID: 9787 RVA: 0x000A6433 File Offset: 0x000A4633
		[XmlElement("ResolutionSet")]
		[DataMember(Name = "ResolutionSet", EmitDefaultValue = false, Order = 1)]
		public ResolutionSet ResolutionSet
		{
			get
			{
				return this.resolutionSet;
			}
			set
			{
				this.resolutionSet = value;
			}
		}

		// Token: 0x040015EE RID: 5614
		private ResolutionSet resolutionSet;
	}
}
