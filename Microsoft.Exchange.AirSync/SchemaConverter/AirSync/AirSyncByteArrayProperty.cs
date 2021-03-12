using System;
using System.IO;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.Wbxml;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000139 RID: 313
	internal class AirSyncByteArrayProperty : AirSyncProperty, IByteArrayProperty, IProperty
	{
		// Token: 0x06000F86 RID: 3974 RVA: 0x00058D0F File Offset: 0x00056F0F
		public AirSyncByteArrayProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00058D1C File Offset: 0x00056F1C
		public byte[] ByteArrayData
		{
			get
			{
				if (this.byteData == null)
				{
					AirSyncBlobXmlNode airSyncBlobXmlNode = (AirSyncBlobXmlNode)base.XmlNode;
					if (airSyncBlobXmlNode != null && airSyncBlobXmlNode.Stream != null && airSyncBlobXmlNode.Stream.CanSeek && airSyncBlobXmlNode.Stream.CanRead)
					{
						this.byteData = new byte[airSyncBlobXmlNode.Stream.Length];
						airSyncBlobXmlNode.Stream.Seek(0L, SeekOrigin.Begin);
						airSyncBlobXmlNode.Stream.Read(this.byteData, 0, (int)airSyncBlobXmlNode.Stream.Length);
					}
				}
				return this.byteData;
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00058DAD File Offset: 0x00056FAD
		public override void Unbind()
		{
			base.Unbind();
			this.byteData = null;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00058DBC File Offset: 0x00056FBC
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IByteArrayProperty byteArrayProperty = srcProperty as IByteArrayProperty;
			if (byteArrayProperty == null)
			{
				throw new UnexpectedTypeException("IByteArrayProperty", srcProperty);
			}
			byte[] byteArrayData = byteArrayProperty.ByteArrayData;
			if (PropertyState.Modified == srcProperty.State && byteArrayData != null)
			{
				base.CreateAirSyncNode(base.AirSyncTagNames[0], byteArrayData);
			}
		}

		// Token: 0x04000A66 RID: 2662
		private byte[] byteData;
	}
}
