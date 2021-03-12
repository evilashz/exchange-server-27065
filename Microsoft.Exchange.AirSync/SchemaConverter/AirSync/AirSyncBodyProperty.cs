using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000133 RID: 307
	internal class AirSyncBodyProperty : AirSyncProperty, IBodyProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x06000F70 RID: 3952 RVA: 0x0005872B File Offset: 0x0005692B
		public AirSyncBodyProperty(string xmlNodeNamespace, string textBodyTag, string bodyTruncatedTag, string bodySizeTag, bool forceBodyTruncatedTag, bool writeBodyTruncatedLast, bool requiresClientSupport) : base(xmlNodeNamespace, textBodyTag, requiresClientSupport)
		{
			base.ClientChangeTracked = true;
			this.textBodyTag = textBodyTag;
			this.forceBodyTruncatedTag = forceBodyTruncatedTag;
			this.bodyTruncatedTag = bodyTruncatedTag;
			this.bodySizeTag = bodySizeTag;
			this.writeBodyTruncatedLast = writeBodyTruncatedLast;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00058764 File Offset: 0x00056964
		public AirSyncBodyProperty(string xmlNodeNamespace, string textBodyTag, string rtfBodyTag, string bodyTruncatedTag, string bodySizeTag, bool forceBodyTruncatedTag, bool writeBodyTruncatedLast, bool requiresClientSupport) : this(xmlNodeNamespace, textBodyTag, rtfBodyTag, bodyTruncatedTag, bodySizeTag, forceBodyTruncatedTag, writeBodyTruncatedLast, requiresClientSupport, false)
		{
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00058788 File Offset: 0x00056988
		public AirSyncBodyProperty(string xmlNodeNamespace, string textBodyTag, string rtfBodyTag, string bodyTruncatedTag, string bodySizeTag, bool forceBodyTruncatedTag, bool writeBodyTruncatedLast, bool requiresClientSupport, bool cacheRtf) : base(xmlNodeNamespace, textBodyTag, rtfBodyTag, requiresClientSupport)
		{
			base.ClientChangeTracked = true;
			this.textBodyTag = textBodyTag;
			if (string.IsNullOrEmpty(rtfBodyTag))
			{
				throw new ArgumentNullException("rtfBodyTag");
			}
			this.rtfBodyTag = rtfBodyTag;
			this.cacheRtf = cacheRtf;
			this.forceBodyTruncatedTag = forceBodyTruncatedTag;
			this.bodyTruncatedTag = bodyTruncatedTag;
			this.bodySizeTag = bodySizeTag;
			this.writeBodyTruncatedLast = writeBodyTruncatedLast;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x000587F0 File Offset: 0x000569F0
		public bool IsOnSMIMEMessage
		{
			get
			{
				throw new NotImplementedException("IsOnSMIMEMessage should not be called");
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x000587FC File Offset: 0x000569FC
		public Stream RtfData
		{
			get
			{
				if (!this.RtfPresent)
				{
					throw new ConversionException("Cannot return RtfData when no RTF is present");
				}
				return new MemoryStream(Convert.FromBase64String(base.XmlNode.InnerText));
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00058826 File Offset: 0x00056A26
		public bool RtfPresent
		{
			get
			{
				return base.XmlNode.Name == this.rtfBodyTag;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00058843 File Offset: 0x00056A43
		public int RtfSize
		{
			get
			{
				if (!this.RtfPresent)
				{
					throw new ConversionException("Cannot return RtfSize when no RTF is present");
				}
				return base.XmlNode.InnerText.Length / 4 * 3;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0005886C File Offset: 0x00056A6C
		public Stream TextData
		{
			get
			{
				if (!this.TextPresent)
				{
					throw new ConversionException("Cannot return TextData no text is present");
				}
				return new MemoryStream(Encoding.UTF8.GetBytes(base.XmlNode.InnerText));
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0005889B File Offset: 0x00056A9B
		public bool TextPresent
		{
			get
			{
				return base.XmlNode.Name == this.textBodyTag;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x000588B8 File Offset: 0x00056AB8
		public int TextSize
		{
			get
			{
				if (!this.TextPresent)
				{
					throw new ConversionException("Cannot return TextData when no text is present");
				}
				return base.XmlNode.InnerText.Length;
			}
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000588DD File Offset: 0x00056ADD
		public void CreateRtfNode()
		{
			if (this.cachedRtfNode != null)
			{
				base.XmlParentNode.AppendChild(this.cachedRtfNode);
			}
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x000588F9 File Offset: 0x00056AF9
		public Stream GetTextData(int length)
		{
			throw new NotImplementedException("Programming Error!! This method should never be called");
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00058908 File Offset: 0x00056B08
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IBodyProperty bodyProperty = srcProperty as IBodyProperty;
			if (bodyProperty == null)
			{
				throw new UnexpectedTypeException("IBodyProperty", bodyProperty);
			}
			if (BodyUtility.IsAskingForMIMEData(bodyProperty, base.Options))
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			bool flag3 = false;
			this.cachedRtfNode = null;
			if (base.Options != null)
			{
				if (base.Options.Contains("ClientSupportsRtf"))
				{
					flag = (bool)base.Options["ClientSupportsRtf"];
				}
				if (base.Options.Contains("MaxTextBodySize"))
				{
					flag2 = true;
					num = (int)base.Options["MaxTextBodySize"];
				}
				if (base.Options.Contains("MaxRtfBodySize") && bodyProperty.RtfPresent)
				{
					int num2 = (int)base.Options["MaxRtfBodySize"];
					flag3 = (bodyProperty.RtfSize > (num2 / 3 + 1) * 4);
				}
			}
			if (flag && this.rtfBodyTag != null && bodyProperty.RtfPresent && !flag3)
			{
				AirSyncStream airSyncStream = bodyProperty.RtfData as AirSyncStream;
				if (airSyncStream == null || airSyncStream.Length == 0L)
				{
					return;
				}
				airSyncStream.DoBase64Conversion = true;
				if (flag2)
				{
					base.CreateAirSyncNode(this.bodyTruncatedTag, "0");
				}
				XmlNode xmlNode = base.AppendChildNode(base.XmlParentNode, this.rtfBodyTag, airSyncStream, -1L, XmlNodeType.Text);
				if (this.cacheRtf)
				{
					this.cachedRtfNode = xmlNode;
					return;
				}
			}
			else
			{
				if (!bodyProperty.TextPresent)
				{
					return;
				}
				if (!flag2 || bodyProperty.TextSize <= num)
				{
					Stream textData = bodyProperty.TextData;
					if (bodyProperty.TextSize == 0)
					{
						return;
					}
					if (!this.writeBodyTruncatedLast && this.forceBodyTruncatedTag)
					{
						base.CreateAirSyncNode(this.bodyTruncatedTag, "0");
					}
					base.AppendChildNode(base.XmlParentNode, this.textBodyTag, textData, -1L, XmlNodeType.Text);
					if (this.writeBodyTruncatedLast && this.forceBodyTruncatedTag)
					{
						base.CreateAirSyncNode(this.bodyTruncatedTag, "0");
						return;
					}
				}
				else
				{
					Stream textData2 = bodyProperty.GetTextData(num);
					if (bodyProperty.TextSize == 0)
					{
						return;
					}
					if (!this.writeBodyTruncatedLast)
					{
						base.CreateAirSyncNode(this.bodyTruncatedTag, "1");
					}
					if (this.bodySizeTag != null)
					{
						base.CreateAirSyncNode(this.bodySizeTag, bodyProperty.TextSize.ToString(CultureInfo.InvariantCulture));
					}
					base.AppendChildNode(base.XmlParentNode, this.textBodyTag, textData2, -1L, XmlNodeType.Text);
					if (this.writeBodyTruncatedLast)
					{
						base.CreateAirSyncNode(this.bodyTruncatedTag, "1");
					}
				}
			}
		}

		// Token: 0x04000A5B RID: 2651
		public const string OptionClientSupportsRtf = "ClientSupportsRtf";

		// Token: 0x04000A5C RID: 2652
		public const string OptionMaxRtfBodySize = "MaxRtfBodySize";

		// Token: 0x04000A5D RID: 2653
		public const string OptionMaxTextBodySize = "MaxTextBodySize";

		// Token: 0x04000A5E RID: 2654
		private string bodySizeTag;

		// Token: 0x04000A5F RID: 2655
		private string bodyTruncatedTag;

		// Token: 0x04000A60 RID: 2656
		private XmlNode cachedRtfNode;

		// Token: 0x04000A61 RID: 2657
		private bool cacheRtf;

		// Token: 0x04000A62 RID: 2658
		private bool forceBodyTruncatedTag;

		// Token: 0x04000A63 RID: 2659
		private string rtfBodyTag;

		// Token: 0x04000A64 RID: 2660
		private string textBodyTag;

		// Token: 0x04000A65 RID: 2661
		private bool writeBodyTruncatedLast;
	}
}
