using System;
using System.Collections;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.ApplicationLogic.TextMessaging;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.TextMessagingHostingData
{
	// Token: 0x02000222 RID: 546
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x000539A1 File Offset: 0x00051BA1
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderTextMessagingHostingData();
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x000539A8 File Offset: 0x00051BA8
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterTextMessagingHostingData();
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x000539B0 File Offset: 0x00051BB0
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData:::False:"] = "Read20_TextMessagingHostingData";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x000539F0 File Offset: 0x00051BF0
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData:::False:"] = "Write20_TextMessagingHostingData";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00053A30 File Offset: 0x00051C30
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData:::False:", new TextMessagingHostingDataSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00053A70 File Offset: 0x00051C70
		public override bool CanSerialize(Type type)
		{
			return type == typeof(TextMessagingHostingData);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00053A87 File Offset: 0x00051C87
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(TextMessagingHostingData))
			{
				return new TextMessagingHostingDataSerializer();
			}
			return null;
		}

		// Token: 0x04000B01 RID: 2817
		private Hashtable readMethods;

		// Token: 0x04000B02 RID: 2818
		private Hashtable writeMethods;

		// Token: 0x04000B03 RID: 2819
		private Hashtable typedSerializers;
	}
}
