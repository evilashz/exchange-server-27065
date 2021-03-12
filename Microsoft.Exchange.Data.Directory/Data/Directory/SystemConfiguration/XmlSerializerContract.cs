using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005F9 RID: 1529
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x060048B5 RID: 18613 RVA: 0x0010D45E File Offset: 0x0010B65E
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderAutoAttendantSettings();
			}
		}

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x060048B6 RID: 18614 RVA: 0x0010D465 File Offset: 0x0010B665
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterAutoAttendantSettings();
			}
		}

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x060048B7 RID: 18615 RVA: 0x0010D46C File Offset: 0x0010B66C
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.Directory.SystemConfiguration.AutoAttendantSettings::"] = "Read5_AutoAttendantSettings";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x060048B8 RID: 18616 RVA: 0x0010D4AC File Offset: 0x0010B6AC
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.Directory.SystemConfiguration.AutoAttendantSettings::"] = "Write5_AutoAttendantSettings";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x060048B9 RID: 18617 RVA: 0x0010D4EC File Offset: 0x0010B6EC
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Data.Directory.SystemConfiguration.AutoAttendantSettings::", new AutoAttendantSettingsSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x0010D52C File Offset: 0x0010B72C
		public override bool CanSerialize(Type type)
		{
			return type == typeof(AutoAttendantSettings);
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x0010D543 File Offset: 0x0010B743
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(AutoAttendantSettings))
			{
				return new AutoAttendantSettingsSerializer();
			}
			return null;
		}

		// Token: 0x04003228 RID: 12840
		private Hashtable readMethods;

		// Token: 0x04003229 RID: 12841
		private Hashtable writeMethods;

		// Token: 0x0400322A RID: 12842
		private Hashtable typedSerializers;
	}
}
