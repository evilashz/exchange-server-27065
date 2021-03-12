using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x020008AE RID: 2222
	public class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x0006C681 File Offset: 0x0006A881
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderRecipientSyncState();
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x0006C688 File Offset: 0x0006A888
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterRecipientSyncState();
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06002FA5 RID: 12197 RVA: 0x0006C690 File Offset: 0x0006A890
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Net.Mserve.RecipientSyncState::"] = "Read3_RecipientSyncState";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x0006C6D0 File Offset: 0x0006A8D0
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Net.Mserve.RecipientSyncState::"] = "Write3_RecipientSyncState";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x0006C710 File Offset: 0x0006A910
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Net.Mserve.RecipientSyncState::", new RecipientSyncStateSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x0006C750 File Offset: 0x0006A950
		public override bool CanSerialize(Type type)
		{
			return type == typeof(RecipientSyncState);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x0006C767 File Offset: 0x0006A967
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(RecipientSyncState))
			{
				return new RecipientSyncStateSerializer();
			}
			return null;
		}

		// Token: 0x0400293E RID: 10558
		private Hashtable readMethods;

		// Token: 0x0400293F RID: 10559
		private Hashtable writeMethods;

		// Token: 0x04002940 RID: 10560
		private Hashtable typedSerializers;
	}
}
