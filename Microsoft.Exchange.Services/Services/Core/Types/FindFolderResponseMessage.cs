using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004DB RID: 1243
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("FindFolderResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class FindFolderResponseMessage : ResponseMessage
	{
		// Token: 0x06002463 RID: 9315 RVA: 0x000A4CC6 File Offset: 0x000A2EC6
		public FindFolderResponseMessage()
		{
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000A4CCE File Offset: 0x000A2ECE
		internal FindFolderResponseMessage(ServiceResultCode code, ServiceError error, FindFolderParentWrapper parentWrapper) : base(code, error)
		{
			this.RootFolder = parentWrapper;
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x000A4CDF File Offset: 0x000A2EDF
		// (set) Token: 0x06002466 RID: 9318 RVA: 0x000A4CE7 File Offset: 0x000A2EE7
		[DataMember(Name = "RootFolder", IsRequired = false)]
		[XmlElement("RootFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindFolderParentWrapper RootFolder { get; set; }
	}
}
