using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x0200001D RID: 29
	internal class XmlSerializerContract2 : XmlSerializerImplementation
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00005563 File Offset: 0x00003763
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderEdgeSyncCredential();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000556A File Offset: 0x0000376A
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterEdgeSyncCredential();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00005574 File Offset: 0x00003774
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.MessageSecurity.EdgeSync.EdgeSyncCredential::"] = "Read3_EdgeSyncCredential";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000055B4 File Offset: 0x000037B4
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.MessageSecurity.EdgeSync.EdgeSyncCredential::"] = "Write3_EdgeSyncCredential";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000055F4 File Offset: 0x000037F4
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.MessageSecurity.EdgeSync.EdgeSyncCredential::", new EdgeSyncCredentialSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005634 File Offset: 0x00003834
		public override bool CanSerialize(Type type)
		{
			return type == typeof(EdgeSyncCredential);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000564B File Offset: 0x0000384B
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(EdgeSyncCredential))
			{
				return new EdgeSyncCredentialSerializer();
			}
			return null;
		}

		// Token: 0x0400007F RID: 127
		private Hashtable readMethods;

		// Token: 0x04000080 RID: 128
		private Hashtable writeMethods;

		// Token: 0x04000081 RID: 129
		private Hashtable typedSerializers;
	}
}
