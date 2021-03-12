using System;
using System.Collections;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.TextMessagingSettingsVersion1Point0
{
	// Token: 0x02000F03 RID: 3843
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x1700231F RID: 8991
		// (get) Token: 0x0600846A RID: 33898 RVA: 0x002417FD File Offset: 0x0023F9FD
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderTextMessagingSettingsVersion1Point0();
			}
		}

		// Token: 0x17002320 RID: 8992
		// (get) Token: 0x0600846B RID: 33899 RVA: 0x00241804 File Offset: 0x0023FA04
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterTextMessagingSettingsVersion1Point0();
			}
		}

		// Token: 0x17002321 RID: 8993
		// (get) Token: 0x0600846C RID: 33900 RVA: 0x0024180C File Offset: 0x0023FA0C
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.Storage.VersionedXml.TextMessagingSettingsVersion1Point0::TextMessagingSettings:True:"] = "Read9_TextMessagingSettings";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17002322 RID: 8994
		// (get) Token: 0x0600846D RID: 33901 RVA: 0x0024184C File Offset: 0x0023FA4C
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.Storage.VersionedXml.TextMessagingSettingsVersion1Point0::TextMessagingSettings:True:"] = "Write9_TextMessagingSettings";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17002323 RID: 8995
		// (get) Token: 0x0600846E RID: 33902 RVA: 0x0024188C File Offset: 0x0023FA8C
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Data.Storage.VersionedXml.TextMessagingSettingsVersion1Point0::TextMessagingSettings:True:", new TextMessagingSettingsVersion1Point0Serializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x0600846F RID: 33903 RVA: 0x002418CC File Offset: 0x0023FACC
		public override bool CanSerialize(Type type)
		{
			return type == typeof(TextMessagingSettingsVersion1Point0);
		}

		// Token: 0x06008470 RID: 33904 RVA: 0x002418E3 File Offset: 0x0023FAE3
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(TextMessagingSettingsVersion1Point0))
			{
				return new TextMessagingSettingsVersion1Point0Serializer();
			}
			return null;
		}

		// Token: 0x040058B0 RID: 22704
		private Hashtable readMethods;

		// Token: 0x040058B1 RID: 22705
		private Hashtable writeMethods;

		// Token: 0x040058B2 RID: 22706
		private Hashtable typedSerializers;
	}
}
