using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000400 RID: 1024
	[XmlType("CompleteFindInGALSpeechRecognitionRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CompleteFindInGALSpeechRecognitionRequest : BaseRequest
	{
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0009EE96 File Offset: 0x0009D096
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x0009EE9E File Offset: 0x0009D09E
		[XmlElement("RecognitionId")]
		[DataMember(Name = "RecognitionId", IsRequired = true)]
		public RecognitionId RecognitionId { get; set; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0009EEA7 File Offset: 0x0009D0A7
		// (set) Token: 0x06001D11 RID: 7441 RVA: 0x0009EEAF File Offset: 0x0009D0AF
		[XmlElement("AudioData")]
		[DataMember(Name = "AudioData", IsRequired = true)]
		public byte[] AudioData { get; set; }

		// Token: 0x06001D12 RID: 7442 RVA: 0x0009EEB8 File Offset: 0x0009D0B8
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CompleteFindInGALSpeechRecognitionCommand(callContext, this);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0009EEC1 File Offset: 0x0009D0C1
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0009EEC4 File Offset: 0x0009D0C4
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
