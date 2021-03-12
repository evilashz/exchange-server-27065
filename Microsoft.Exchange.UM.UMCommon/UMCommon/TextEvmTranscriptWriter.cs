using System;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200016E RID: 366
	internal class TextEvmTranscriptWriter : EvmTranscriptWriter
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x0002AB48 File Offset: 0x00028D48
		private TextEvmTranscriptWriter(CultureInfo language) : base(language)
		{
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002AB61 File Offset: 0x00028D61
		public static EvmTranscriptWriter Create(CultureInfo language)
		{
			return new TextEvmTranscriptWriter(language);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002AB69 File Offset: 0x00028D69
		public override string ToString()
		{
			return this.builder.ToString();
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002AB78 File Offset: 0x00028D78
		protected override void WriteEndOfParagraph()
		{
			this.builder.Append(Strings.EndOfParagraphMarker.ToString(base.Language));
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002ABA4 File Offset: 0x00028DA4
		protected override void WriteInformation(XmlElement element)
		{
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002ABA6 File Offset: 0x00028DA6
		protected override void WritePhoneNumber(XmlElement element)
		{
			this.WriteGenericFeature(element);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002ABB0 File Offset: 0x00028DB0
		protected override void WriteGenericFeature(XmlElement element)
		{
			foreach (object obj in element.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				this.builder.Append(xmlNode.InnerText);
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002AC14 File Offset: 0x00028E14
		protected override void WriteGenericTextElement(XmlElement element)
		{
			this.builder.Append(element.InnerText);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002AC28 File Offset: 0x00028E28
		protected override void WriteBreakElement(XmlElement element)
		{
			this.WriteEndOfParagraph();
			this.builder.Append(" ");
		}

		// Token: 0x04000625 RID: 1573
		private StringBuilder builder = new StringBuilder(1024);
	}
}
