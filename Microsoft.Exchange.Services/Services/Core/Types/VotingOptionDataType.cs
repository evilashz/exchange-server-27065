using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200068A RID: 1674
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "VotingOptionDataType")]
	[Serializable]
	public class VotingOptionDataType
	{
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x000B8502 File Offset: 0x000B6702
		// (set) Token: 0x0600332F RID: 13103 RVA: 0x000B850A File Offset: 0x000B670A
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string DisplayName { get; set; }

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x000B8513 File Offset: 0x000B6713
		// (set) Token: 0x06003331 RID: 13105 RVA: 0x000B851B File Offset: 0x000B671B
		[IgnoreDataMember]
		public SendPromptType SendPrompt { get; set; }

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000B8524 File Offset: 0x000B6724
		// (set) Token: 0x06003333 RID: 13107 RVA: 0x000B8531 File Offset: 0x000B6731
		[DataMember(Name = "SendPrompt", Order = 2)]
		[XmlIgnore]
		public string SendPromptString
		{
			get
			{
				return EnumUtilities.ToString<SendPromptType>(this.SendPrompt);
			}
			set
			{
				this.SendPrompt = EnumUtilities.Parse<SendPromptType>(value);
			}
		}
	}
}
