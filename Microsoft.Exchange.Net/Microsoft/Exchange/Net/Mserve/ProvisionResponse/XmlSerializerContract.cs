using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionResponse
{
	// Token: 0x020008A6 RID: 2214
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06002F6B RID: 12139 RVA: 0x0006BB78 File Offset: 0x00069D78
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderProvision();
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06002F6C RID: 12140 RVA: 0x0006BB7F File Offset: 0x00069D7F
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterProvision();
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06002F6D RID: 12141 RVA: 0x0006BB88 File Offset: 0x00069D88
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision:DeltaSyncV2:::False:"] = "Read6_Provision";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x0006BBC8 File Offset: 0x00069DC8
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision:DeltaSyncV2:::False:"] = "Write6_Provision";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06002F6F RID: 12143 RVA: 0x0006BC08 File Offset: 0x00069E08
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision:DeltaSyncV2:::False:", new ProvisionSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x0006BC48 File Offset: 0x00069E48
		public override bool CanSerialize(Type type)
		{
			return type == typeof(Provision);
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x0006BC5F File Offset: 0x00069E5F
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(Provision))
			{
				return new ProvisionSerializer();
			}
			return null;
		}

		// Token: 0x0400291E RID: 10526
		private Hashtable readMethods;

		// Token: 0x0400291F RID: 10527
		private Hashtable writeMethods;

		// Token: 0x04002920 RID: 10528
		private Hashtable typedSerializers;
	}
}
