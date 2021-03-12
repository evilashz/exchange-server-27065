using System;
using System.IO;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	internal class AirSyncRtfBodyProperty : AirSyncProperty, IBodyProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x0005B44A File Offset: 0x0005964A
		public AirSyncRtfBodyProperty(string xmlNodeNamespace, string textBodyTag, bool requiresClientSupport, AirSyncBodyProperty bodyProperty) : base(xmlNodeNamespace, textBodyTag, requiresClientSupport)
		{
			this.bodyProperty = bodyProperty;
			base.ClientChangeTracked = true;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x0005B464 File Offset: 0x00059664
		public bool IsOnSMIMEMessage
		{
			get
			{
				throw new NotImplementedException("IsOnSMIMEMessage should not be called");
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0005B470 File Offset: 0x00059670
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

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0005B49A File Offset: 0x0005969A
		public bool RtfPresent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0005B49D File Offset: 0x0005969D
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

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x0005B4C6 File Offset: 0x000596C6
		public Stream TextData
		{
			get
			{
				throw new NotImplementedException("Programming Error!! This method should never be called");
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0005B4D2 File Offset: 0x000596D2
		public bool TextPresent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0005B4D5 File Offset: 0x000596D5
		public int TextSize
		{
			get
			{
				throw new NotImplementedException("Programming Error!! This method should never be called");
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0005B4E1 File Offset: 0x000596E1
		public Stream GetTextData(int length)
		{
			throw new NotImplementedException("Programming Error!! This method should never be called");
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0005B4ED File Offset: 0x000596ED
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			this.bodyProperty.CreateRtfNode();
		}

		// Token: 0x04000A80 RID: 2688
		private AirSyncBodyProperty bodyProperty;
	}
}
