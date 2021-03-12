using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.AssistantsClientResources;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Mwi
{
	// Token: 0x0200010B RID: 267
	internal class SmsVoicemailNotification : SmsNotification
	{
		// Token: 0x06000AF7 RID: 2807 RVA: 0x000474AE File Offset: 0x000456AE
		internal SmsVoicemailNotification(MailboxSession session, CultureInfo preferredCulture, StoreObject item, UMDialPlan dialPlan) : base(session, preferredCulture, item, dialPlan)
		{
			this.voicemailDuration = base.ReadStoreProperty<int>(MessageItemSchema.VoiceMessageDuration, 0);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000474D0 File Offset: 0x000456D0
		protected override void BuildSmsMessage()
		{
			string text = string.Empty;
			base.BuildSmsMessage();
			int num = base.RemainingChar;
			if (num <= 0)
			{
				return;
			}
			try
			{
				string text2;
				if (ObjectClass.IsOfClass(base.Item.ClassName, "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA") || ObjectClass.IsOfClass(base.Item.ClassName, "IPM.Note.rpmsg.Microsoft.Voicemail.UM"))
				{
					text2 = Strings.SMSProtectedVoicemail.ToString(base.GetMailboxCulture());
				}
				else
				{
					using (Stream stream = base.Item.OpenPropertyStream(MessageItemSchema.AsrData, PropertyOpenMode.ReadOnly))
					{
						using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
						{
							XmlDocument xmlDocument = new SafeXmlDocument();
							xmlDocument.Load(streamReader);
							XmlNode documentElement = xmlDocument.DocumentElement;
							float transcriptionConfidence = SmsVoicemailNotification.GetTranscriptionConfidence(documentElement);
							text2 = ((transcriptionConfidence > 0.3f) ? this.RenderVoiceMailPreview(documentElement) : Strings.SMSLowConfidenceTranscription.ToString(base.GetMailboxCulture()));
							ExTraceGlobals.MWITracer.TraceDebug<float, string>((long)this.GetHashCode(), "Confidence: {0}. Text: {1}", transcriptionConfidence, text2);
						}
					}
				}
				num--;
				if (text2.Length > num)
				{
					num -= "...".Length;
					if (num > 0)
					{
						text = " ";
						text += text2.Substring(0, num);
						text += "...";
					}
				}
				else
				{
					text = " ";
					text += text2;
				}
				base.Body += text;
			}
			catch (ObjectNotFoundException ex)
			{
				ExTraceGlobals.MWITracer.TraceDebug<string>((long)this.GetHashCode(), "ObjectNotFoundException occured while trying to add EVM Text to the SMS. Exception: {0}", ex.Message);
			}
			catch (IOException ex2)
			{
				ExTraceGlobals.MWITracer.TraceDebug<string>((long)this.GetHashCode(), "IOException occured while trying to add EVM Text to the SMS. Exception: {0}", ex2.Message);
			}
			catch (XmlException ex3)
			{
				ExTraceGlobals.MWITracer.TraceDebug<string>((long)this.GetHashCode(), "XmlException occured while trying to add EVM Text to the SMS. Exception: {0}", ex3.Message);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0004771C File Offset: 0x0004591C
		protected override string FullBaseMessage
		{
			get
			{
				return Strings.SMSNewVoicemailWithCallerInfo(this.voicemailDuration, base.Name, base.CallerId).ToString(base.GetMailboxCulture());
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00047750 File Offset: 0x00045950
		protected override string BaseMessageWithName
		{
			get
			{
				return Strings.SMSNewVoicemailWithCallerId(this.voicemailDuration, base.Name).ToString(base.GetMailboxCulture());
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0004777C File Offset: 0x0004597C
		protected override string BaseMessageWithCallerId
		{
			get
			{
				return Strings.SMSNewVoicemailWithCallerId(this.voicemailDuration, base.CallerId).ToString(base.GetMailboxCulture());
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x000477A8 File Offset: 0x000459A8
		protected override int MinimumMaxUsableCharacters
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000477AC File Offset: 0x000459AC
		private static float GetTranscriptionConfidence(XmlNode rootNode)
		{
			float result = 0f;
			XmlAttribute xmlAttribute = rootNode.Attributes["confidence"];
			if (xmlAttribute != null && !string.IsNullOrEmpty(xmlAttribute.Value))
			{
				float.TryParse(xmlAttribute.Value, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out result);
			}
			return result;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000477FC File Offset: 0x000459FC
		private string RenderVoiceMailPreview(XmlNode root)
		{
			EvmTranscriptWriter evmTranscriptWriter = TextEvmTranscriptWriter.Create(base.GetMailboxCulture());
			evmTranscriptWriter.WriteTranscript(root);
			return evmTranscriptWriter.ToString();
		}

		// Token: 0x040006FA RID: 1786
		private const string UnCompleteString = "...";

		// Token: 0x040006FB RID: 1787
		private const int MinimumUsableCharacters = 9;

		// Token: 0x040006FC RID: 1788
		private const float MinimumTranscriptionConfidence = 0.3f;

		// Token: 0x040006FD RID: 1789
		private int voicemailDuration;
	}
}
