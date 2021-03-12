using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EdgeSync.Common
{
	// Token: 0x02000035 RID: 53
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006EDB File Offset: 0x000050DB
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderStatus();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006EE2 File Offset: 0x000050E2
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterStatus();
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006EEC File Offset: 0x000050EC
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.EdgeSync.Common.Status::"] = "Read5_Status";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006F2C File Offset: 0x0000512C
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.EdgeSync.Common.Status::"] = "Write5_Status";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006F6C File Offset: 0x0000516C
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.EdgeSync.Common.Status::", new StatusSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006FAC File Offset: 0x000051AC
		public override bool CanSerialize(Type type)
		{
			return type == typeof(Status);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006FC3 File Offset: 0x000051C3
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(Status))
			{
				return new StatusSerializer();
			}
			return null;
		}

		// Token: 0x040000EF RID: 239
		private Hashtable readMethods;

		// Token: 0x040000F0 RID: 240
		private Hashtable writeMethods;

		// Token: 0x040000F1 RID: 241
		private Hashtable typedSerializers;
	}
}
