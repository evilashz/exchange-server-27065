using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000023 RID: 35
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000070E8 File Offset: 0x000052E8
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderLogQuery();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000070EF File Offset: 0x000052EF
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterLogQuery();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000070F8 File Offset: 0x000052F8
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Transport.Logging.Search.LogQuery::"] = "Read28_LogQuery";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00007138 File Offset: 0x00005338
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Transport.Logging.Search.LogQuery::"] = "Write28_LogQuery";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00007178 File Offset: 0x00005378
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Transport.Logging.Search.LogQuery::", new LogQuerySerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000071B8 File Offset: 0x000053B8
		public override bool CanSerialize(Type type)
		{
			return type == typeof(LogQuery);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000071CF File Offset: 0x000053CF
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(LogQuery))
			{
				return new LogQuerySerializer();
			}
			return null;
		}

		// Token: 0x04000055 RID: 85
		private Hashtable readMethods;

		// Token: 0x04000056 RID: 86
		private Hashtable writeMethods;

		// Token: 0x04000057 RID: 87
		private Hashtable typedSerializers;
	}
}
