using System;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.WBXml
{
	// Token: 0x02000078 RID: 120
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class WBxmlBlobNode : XmlElement
	{
		// Token: 0x06000234 RID: 564 RVA: 0x000076CF File Offset: 0x000058CF
		internal WBxmlBlobNode(string prefix, string localName, string namespaceUri, XmlDocument doc) : base(prefix, localName, namespaceUri, doc)
		{
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000076EB File Offset: 0x000058EB
		// (set) Token: 0x06000236 RID: 566 RVA: 0x000076F2 File Offset: 0x000058F2
		public override string InnerText
		{
			get
			{
				return "*** blob ***";
			}
			set
			{
				throw new NotImplementedException("This kind of node should contain a blob, not a string");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000237 RID: 567 RVA: 0x000076FE File Offset: 0x000058FE
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00007706 File Offset: 0x00005906
		internal StreamAccessor Stream
		{
			get
			{
				return this.stream;
			}
			set
			{
				if (this.byteArray != null)
				{
					throw new InvalidOperationException("byteArray has already been assigned");
				}
				this.stream = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000772A File Offset: 0x0000592A
		internal byte[] ByteArray
		{
			get
			{
				return this.byteArray;
			}
			set
			{
				if (this.stream != null)
				{
					throw new InvalidOperationException("stream has already been assigned");
				}
				this.byteArray = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00007746 File Offset: 0x00005946
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00007764 File Offset: 0x00005964
		internal long StreamDataSize
		{
			get
			{
				if (this.streamDataSize < 0L)
				{
					return this.Stream.Length;
				}
				return this.streamDataSize;
			}
			set
			{
				this.streamDataSize = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000776D File Offset: 0x0000596D
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00007775 File Offset: 0x00005975
		internal XmlNodeType OriginalNodeType
		{
			get
			{
				return this.originalNodeType;
			}
			set
			{
				this.originalNodeType = value;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000777E File Offset: 0x0000597E
		public override int GetHashCode()
		{
			if (this.byteArray != null)
			{
				return (int)WBXmlStreamHelper.UpdateCrc32(0U, this.byteArray, 0, this.byteArray.Length);
			}
			return base.GetHashCode();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000077A4 File Offset: 0x000059A4
		internal void CopyStream(Stream outputStream)
		{
			if (this.stream == null)
			{
				return;
			}
			if (this.stream.CanSeek)
			{
				this.stream.Seek(0L, SeekOrigin.Begin);
			}
			WBXmlStreamHelper.CopyStream(this.stream, outputStream, (int)this.StreamDataSize);
		}

		// Token: 0x040003E2 RID: 994
		private StreamAccessor stream;

		// Token: 0x040003E3 RID: 995
		private long streamDataSize = -1L;

		// Token: 0x040003E4 RID: 996
		private byte[] byteArray;

		// Token: 0x040003E5 RID: 997
		private XmlNodeType originalNodeType = XmlNodeType.CDATA;
	}
}
