using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x02000900 RID: 2304
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x0007B0E7 File Offset: 0x000792E7
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderSettings();
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x0007B0EE File Offset: 0x000792EE
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterSettings();
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x0007B0F8 File Offset: 0x000792F8
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Net.Mserve.SettingsResponse.Settings:HMSETTINGS:::False:"] = "Read43_Settings";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x0007B138 File Offset: 0x00079338
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Net.Mserve.SettingsResponse.Settings:HMSETTINGS:::False:"] = "Write43_Settings";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x0007B178 File Offset: 0x00079378
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Net.Mserve.SettingsResponse.Settings:HMSETTINGS:::False:", new SettingsSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x0007B1B8 File Offset: 0x000793B8
		public override bool CanSerialize(Type type)
		{
			return type == typeof(Settings);
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x0007B1CF File Offset: 0x000793CF
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(Settings))
			{
				return new SettingsSerializer();
			}
			return null;
		}

		// Token: 0x04002B04 RID: 11012
		private Hashtable readMethods;

		// Token: 0x04002B05 RID: 11013
		private Hashtable writeMethods;

		// Token: 0x04002B06 RID: 11014
		private Hashtable typedSerializers;
	}
}
