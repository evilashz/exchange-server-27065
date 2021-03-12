using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.ApplicationLogic.UM;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200019D RID: 413
	internal class PartnerTranscriptionData : ITranscriptionData
	{
		// Token: 0x06000C2B RID: 3115 RVA: 0x00034B40 File Offset: 0x00032D40
		internal PartnerTranscriptionData(Stream stream) : this(PartnerTranscriptionData.GetTranscriptionXmlFromStream(stream))
		{
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00034B4E File Offset: 0x00032D4E
		internal PartnerTranscriptionData(string xml) : this(PartnerTranscriptionData.GetTranscriptionXmlFromText(xml))
		{
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00034B5C File Offset: 0x00032D5C
		internal PartnerTranscriptionData(XmlDocument transcriptionXml)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(transcriptionXml.NameTable);
			xmlNamespaceManager.AddNamespace("evm", "http://schemas.microsoft.com/exchange/um/2010/evm");
			this.Confidence = 0f;
			this.RecognitionResult = RecoResultType.Skipped;
			this.RecognitionError = RecoErrorType.Other;
			this.TranscriptionXml = transcriptionXml;
			XmlNode xmlNode = this.TranscriptionXml.SelectSingleNode("//evm:ASR/@lang", xmlNamespaceManager);
			if (xmlNode == null)
			{
				throw new ArgumentException("transcriptionXml");
			}
			this.Language = new CultureInfo(xmlNode.Value);
			float confidence = 0f;
			xmlNode = this.TranscriptionXml.SelectSingleNode("//evm:ASR/@confidence", xmlNamespaceManager);
			if (xmlNode == null || !float.TryParse(xmlNode.Value, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out confidence))
			{
				throw new ArgumentException("transcriptionXml");
			}
			this.Confidence = confidence;
			xmlNode = this.TranscriptionXml.SelectSingleNode("//evm:ASR/@recognitionResult", xmlNamespaceManager);
			if (xmlNode == null)
			{
				throw new ArgumentException("transcriptionXml");
			}
			this.RecognitionResult = EnumValidator<RecoResultType>.Parse(xmlNode.Value, EnumParseOptions.IgnoreCase);
			xmlNode = this.TranscriptionXml.SelectSingleNode("//evm:ASR/@recognitionError", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.RecognitionError = EnumValidator<RecoErrorType>.Parse(xmlNode.Value, EnumParseOptions.IgnoreCase);
			}
			xmlNode = this.TranscriptionXml.SelectSingleNode("//evm:ASR/evm:ErrorInformation", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.ErrorInformation = xmlNode.InnerText;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00034C9D File Offset: 0x00032E9D
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00034CA5 File Offset: 0x00032EA5
		public RecoResultType RecognitionResult { get; private set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00034CAE File Offset: 0x00032EAE
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x00034CB6 File Offset: 0x00032EB6
		public float Confidence { get; private set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00034CBF File Offset: 0x00032EBF
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x00034CC7 File Offset: 0x00032EC7
		public CultureInfo Language { get; private set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00034CD0 File Offset: 0x00032ED0
		public ConfidenceBandType ConfidenceBand
		{
			get
			{
				if ((double)this.Confidence > LocConfig.Instance[this.Language].Transcription.HighConfidence)
				{
					return ConfidenceBandType.High;
				}
				if ((double)this.Confidence > LocConfig.Instance[this.Language].Transcription.LowConfidence)
				{
					return ConfidenceBandType.Medium;
				}
				return ConfidenceBandType.Low;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00034D28 File Offset: 0x00032F28
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x00034D30 File Offset: 0x00032F30
		public XmlDocument TranscriptionXml { get; private set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00034D39 File Offset: 0x00032F39
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x00034D41 File Offset: 0x00032F41
		public RecoErrorType RecognitionError { get; private set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00034D4A File Offset: 0x00032F4A
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x00034D52 File Offset: 0x00032F52
		public string ErrorInformation { get; private set; }

		// Token: 0x06000C3B RID: 3131 RVA: 0x00034D5B File Offset: 0x00032F5B
		internal static bool IsPartnerError(RecoErrorType error)
		{
			return error == RecoErrorType.Rejected || error == RecoErrorType.SystemError || error == RecoErrorType.Timeout || error == RecoErrorType.BadRequest || error == RecoErrorType.Other;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00034D74 File Offset: 0x00032F74
		private static XmlDocument GetTranscriptionXmlFromStream(Stream inputStream)
		{
			if (!VoiceMailPreviewSchema.IsValidTranscription(inputStream))
			{
				throw new ArgumentException("inputStream");
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			inputStream.Position = 0L;
			xmlDocument.Load(inputStream);
			return xmlDocument;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00034DAC File Offset: 0x00032FAC
		private static XmlDocument GetTranscriptionXmlFromText(string xml)
		{
			XmlDocument transcriptionXmlFromStream;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
			{
				transcriptionXmlFromStream = PartnerTranscriptionData.GetTranscriptionXmlFromStream(memoryStream);
			}
			return transcriptionXmlFromStream;
		}
	}
}
