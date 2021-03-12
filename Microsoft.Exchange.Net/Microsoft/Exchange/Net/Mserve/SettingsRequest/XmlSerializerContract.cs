using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008D2 RID: 2258
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x00072D64 File Offset: 0x00070F64
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderSettings();
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x00072D6B File Offset: 0x00070F6B
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterSettings();
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x00072D74 File Offset: 0x00070F74
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Net.Mserve.SettingsRequest.Settings:HMSETTINGS:::False:"] = "Read33_Settings";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x00072DB4 File Offset: 0x00070FB4
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Net.Mserve.SettingsRequest.Settings:HMSETTINGS:::False:"] = "Write33_Settings";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x00072DF4 File Offset: 0x00070FF4
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Net.Mserve.SettingsRequest.Settings:HMSETTINGS:::False:", new SettingsSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x00072E34 File Offset: 0x00071034
		public override bool CanSerialize(Type type)
		{
			return type == typeof(Settings);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x00072E4B File Offset: 0x0007104B
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(Settings))
			{
				return new SettingsSerializer();
			}
			return null;
		}

		// Token: 0x040029F2 RID: 10738
		private Hashtable readMethods;

		// Token: 0x040029F3 RID: 10739
		private Hashtable writeMethods;

		// Token: 0x040029F4 RID: 10740
		private Hashtable typedSerializers;
	}
}
