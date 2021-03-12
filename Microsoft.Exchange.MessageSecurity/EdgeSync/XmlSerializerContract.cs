using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x02000018 RID: 24
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004E09 File Offset: 0x00003009
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderEdgeSubscriptionData();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004E10 File Offset: 0x00003010
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterEdgeSubscriptionData();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004E18 File Offset: 0x00003018
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.MessageSecurity.EdgeSync.EdgeSubscriptionData::"] = "Read3_EdgeSubscriptionData";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004E58 File Offset: 0x00003058
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.MessageSecurity.EdgeSync.EdgeSubscriptionData::"] = "Write3_EdgeSubscriptionData";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004E98 File Offset: 0x00003098
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.MessageSecurity.EdgeSync.EdgeSubscriptionData::", new EdgeSubscriptionDataSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004ED8 File Offset: 0x000030D8
		public override bool CanSerialize(Type type)
		{
			return type == typeof(EdgeSubscriptionData);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004EEF File Offset: 0x000030EF
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(EdgeSubscriptionData))
			{
				return new EdgeSubscriptionDataSerializer();
			}
			return null;
		}

		// Token: 0x04000073 RID: 115
		private Hashtable readMethods;

		// Token: 0x04000074 RID: 116
		private Hashtable writeMethods;

		// Token: 0x04000075 RID: 117
		private Hashtable typedSerializers;
	}
}
