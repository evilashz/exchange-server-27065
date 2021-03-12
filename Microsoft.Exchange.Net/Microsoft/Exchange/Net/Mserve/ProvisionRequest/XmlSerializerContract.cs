using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionRequest
{
	// Token: 0x0200089D RID: 2205
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x0006A77D File Offset: 0x0006897D
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderProvision();
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x0006A784 File Offset: 0x00068984
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterProvision();
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x0006A78C File Offset: 0x0006898C
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision:DeltaSyncV2:::False:"] = "Read5_Provision";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x0006A7CC File Offset: 0x000689CC
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision:DeltaSyncV2:::False:"] = "Write5_Provision";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x0006A80C File Offset: 0x00068A0C
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision:DeltaSyncV2:::False:", new ProvisionSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x0006A84C File Offset: 0x00068A4C
		public override bool CanSerialize(Type type)
		{
			return type == typeof(Provision);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x0006A863 File Offset: 0x00068A63
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(Provision))
			{
				return new ProvisionSerializer();
			}
			return null;
		}

		// Token: 0x040028FE RID: 10494
		private Hashtable readMethods;

		// Token: 0x040028FF RID: 10495
		private Hashtable writeMethods;

		// Token: 0x04002900 RID: 10496
		private Hashtable typedSerializers;
	}
}
