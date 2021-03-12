using System;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync.Wbxml
{
	// Token: 0x0200029D RID: 669
	internal class AirSyncBlobXmlNode : XmlElement
	{
		// Token: 0x0600186A RID: 6250 RVA: 0x0008F848 File Offset: 0x0008DA48
		public AirSyncBlobXmlNode(string prefix, string localName, string namespaceURI, XmlDocument doc) : base(prefix, localName, namespaceURI, doc)
		{
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x0008F864 File Offset: 0x0008DA64
		// (set) Token: 0x0600186C RID: 6252 RVA: 0x0008F86C File Offset: 0x0008DA6C
		public Stream Stream
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

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x0008F888 File Offset: 0x0008DA88
		// (set) Token: 0x0600186E RID: 6254 RVA: 0x0008F890 File Offset: 0x0008DA90
		public byte[] ByteArray
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

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x0008F8AC File Offset: 0x0008DAAC
		// (set) Token: 0x06001870 RID: 6256 RVA: 0x0008F8CA File Offset: 0x0008DACA
		public long StreamDataSize
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

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0008F8D3 File Offset: 0x0008DAD3
		// (set) Token: 0x06001872 RID: 6258 RVA: 0x0008F8DB File Offset: 0x0008DADB
		public XmlNodeType OriginalNodeType
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

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x0008F8E4 File Offset: 0x0008DAE4
		// (set) Token: 0x06001874 RID: 6260 RVA: 0x0008F8EB File Offset: 0x0008DAEB
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

		// Token: 0x06001875 RID: 6261 RVA: 0x0008F8F8 File Offset: 0x0008DAF8
		public void CopyStream(Stream outputStream)
		{
			if (this.stream == null)
			{
				return;
			}
			if (this.stream.CanSeek)
			{
				this.stream.Seek(0L, SeekOrigin.Begin);
			}
			else
			{
				AirSyncDiagnostics.TraceError<Type>(ExTraceGlobals.ConversionTracer, this, "this.streamis of {0} type.", this.stream.GetType());
			}
			AirSyncStream airSyncStream = this.stream as AirSyncStream;
			if (airSyncStream != null)
			{
				airSyncStream.CopyStream(outputStream, (int)this.StreamDataSize);
				return;
			}
			StreamHelper.CopyStream(this.stream, outputStream, (int)this.StreamDataSize);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0008F97C File Offset: 0x0008DB7C
		public override int GetHashCode()
		{
			AirSyncStream airSyncStream = this.stream as AirSyncStream;
			if (airSyncStream != null)
			{
				return airSyncStream.StreamHash;
			}
			if (this.byteArray != null)
			{
				return (int)StreamHelper.UpdateCrc32(0U, this.byteArray, 0, this.byteArray.Length);
			}
			return base.GetHashCode();
		}

		// Token: 0x04000F22 RID: 3874
		private Stream stream;

		// Token: 0x04000F23 RID: 3875
		private long streamDataSize = -1L;

		// Token: 0x04000F24 RID: 3876
		private byte[] byteArray;

		// Token: 0x04000F25 RID: 3877
		private XmlNodeType originalNodeType = XmlNodeType.CDATA;
	}
}
