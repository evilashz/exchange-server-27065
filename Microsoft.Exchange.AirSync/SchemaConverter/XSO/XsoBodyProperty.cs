using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000206 RID: 518
	internal class XsoBodyProperty : XsoProperty, IBodyProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x0600140E RID: 5134 RVA: 0x0007412B File Offset: 0x0007232B
		public XsoBodyProperty(PropertyType type) : base(null, type)
		{
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00074143 File Offset: 0x00072343
		public XsoBodyProperty() : base(null)
		{
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0007415C File Offset: 0x0007235C
		public bool IsOnSMIMEMessage
		{
			get
			{
				MessageItem messageItem = base.XsoItem as MessageItem;
				return messageItem != null && ObjectClass.IsSmime(messageItem.ClassName);
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00074188 File Offset: 0x00072388
		public Stream RtfData
		{
			get
			{
				if (!this.RtfPresent)
				{
					throw new ConversionException("Cannot pull RtfData, Rtf property not present on this item");
				}
				if (this.rtfData == null)
				{
					Item item = (Item)base.XsoItem;
					item.Load();
					long num;
					IList<AttachmentLink> list;
					this.rtfData = BodyConversionUtilities.ConvertToRtfStream(item, -1L, out num, out list);
					this.rtfSize = (int)num;
				}
				return this.rtfData;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x000741E4 File Offset: 0x000723E4
		public bool RtfPresent
		{
			get
			{
				Item item = (Item)base.XsoItem;
				return item.Body != null && item.Body.Format == BodyFormat.ApplicationRtf;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00074218 File Offset: 0x00072418
		public int RtfSize
		{
			get
			{
				if (this.rtfSize < 0)
				{
					if (!this.RtfPresent)
					{
						throw new ConversionException("Cannot pull RtfSize, Rtf property not present on this item");
					}
					Item item = (Item)base.XsoItem;
					long size = item.Body.Size;
					if (size > 2147483647L || size < 0L)
					{
						throw new ConversionException("Invalid body size: " + size.ToString(CultureInfo.InvariantCulture));
					}
					this.rtfSize = (int)size;
				}
				return this.rtfSize;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x00074291 File Offset: 0x00072491
		public Stream TextData
		{
			get
			{
				if (!this.TextPresent)
				{
					throw new ConversionException("Cannot pull TextData, TextBody property not present on this item");
				}
				if (this.textData == null)
				{
					this.GetTextBody();
				}
				return this.textData;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x000742BC File Offset: 0x000724BC
		public bool TextPresent
		{
			get
			{
				Item item = (Item)base.XsoItem;
				return item.Body != null;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x000742E0 File Offset: 0x000724E0
		public int TextSize
		{
			get
			{
				if (this.textSize < 0)
				{
					if (!this.TextPresent)
					{
						throw new ConversionException("Cannot pull TextData, TextBody property not present on this item");
					}
					this.GetTextBody();
				}
				return this.textSize;
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0007430A File Offset: 0x0007250A
		public Stream GetTextData(int length)
		{
			if (!this.TextPresent)
			{
				throw new ConversionException("Cannot pull TextData, TextBody property not present on this item");
			}
			if (length == 0)
			{
				return XsoBodyProperty.emptyStream;
			}
			return this.GetTextBody(length);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00074330 File Offset: 0x00072530
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			Item item = (Item)base.XsoItem;
			IBodyProperty bodyProperty = (IBodyProperty)srcProperty;
			if (bodyProperty.RtfPresent)
			{
				if (bodyProperty.RtfData != null && bodyProperty.RtfData.Length > 0L)
				{
					using (Stream stream = XsoBodyProperty.OpenBodyWriteStream(item, BodyFormat.ApplicationRtf))
					{
						StreamHelper.CopyStream(bodyProperty.RtfData, stream);
						return;
					}
				}
				using (TextWriter textWriter = item.Body.OpenTextWriter(BodyFormat.TextPlain))
				{
					textWriter.Write(string.Empty);
					return;
				}
			}
			if (bodyProperty.TextPresent)
			{
				using (Stream stream2 = XsoBodyProperty.OpenBodyWriteStream(item, BodyFormat.TextPlain))
				{
					StreamHelper.CopyStream(bodyProperty.TextData, stream2);
					return;
				}
			}
			throw new ConversionException("Source body property does not have Rtf or Text body present");
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00074414 File Offset: 0x00072614
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			Item item = (Item)base.XsoItem;
			using (TextWriter textWriter = item.Body.OpenTextWriter(BodyFormat.TextPlain))
			{
				textWriter.Write(string.Empty);
			}
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00074464 File Offset: 0x00072664
		public override void Unbind()
		{
			this.textData = null;
			this.rtfData = null;
			this.textSize = -1;
			this.rtfSize = -1;
			base.Unbind();
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00074488 File Offset: 0x00072688
		private static Stream OpenBodyWriteStream(Item item, BodyFormat bodyFormat)
		{
			BodyWriteConfiguration configuration = new BodyWriteConfiguration(bodyFormat, "utf-8");
			return item.Body.OpenWriteStream(configuration);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x000744AD File Offset: 0x000726AD
		private void GetTextBody()
		{
			this.textData = this.GetTextBody(-1);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x000744BC File Offset: 0x000726BC
		private Stream GetTextBody(int length)
		{
			Item item = (Item)base.XsoItem;
			if (string.Equals(item.ClassName, "IPM.Note.SMIME", StringComparison.OrdinalIgnoreCase))
			{
				string s = Strings.SMIMENotSupportedBodyText.ToString(item.Session.PreferedCulture);
				this.textData = new MemoryStream(Encoding.UTF8.GetBytes(s));
				return this.textData;
			}
			long num;
			IList<AttachmentLink> list;
			Stream result = BodyConversionUtilities.ConvertToPlainTextStream(item, (long)length, out num, out list);
			this.textSize = (int)num;
			return result;
		}

		// Token: 0x04000C50 RID: 3152
		private static readonly Stream emptyStream = new MemoryStream(0);

		// Token: 0x04000C51 RID: 3153
		private int textSize = -1;

		// Token: 0x04000C52 RID: 3154
		private int rtfSize = -1;

		// Token: 0x04000C53 RID: 3155
		private Stream textData;

		// Token: 0x04000C54 RID: 3156
		private Stream rtfData;
	}
}
