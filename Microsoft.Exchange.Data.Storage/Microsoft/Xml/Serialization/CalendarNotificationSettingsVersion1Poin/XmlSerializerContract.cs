using System;
using System.Collections;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.CalendarNotificationSettingsVersion1Point0
{
	// Token: 0x02000EFE RID: 3838
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x1700231A RID: 8986
		// (get) Token: 0x0600844A RID: 33866 RVA: 0x00240012 File Offset: 0x0023E212
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderCalendarNotificationSettingsVersion1Point0();
			}
		}

		// Token: 0x1700231B RID: 8987
		// (get) Token: 0x0600844B RID: 33867 RVA: 0x00240019 File Offset: 0x0023E219
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterCalendarNotificationSettingsVersion1Point0();
			}
		}

		// Token: 0x1700231C RID: 8988
		// (get) Token: 0x0600844C RID: 33868 RVA: 0x00240020 File Offset: 0x0023E220
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.Storage.VersionedXml.CalendarNotificationSettingsVersion1Point0::CalendarNotificationSettings:True:"] = "Read16_CalendarNotificationSettings";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x1700231D RID: 8989
		// (get) Token: 0x0600844D RID: 33869 RVA: 0x00240060 File Offset: 0x0023E260
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.Storage.VersionedXml.CalendarNotificationSettingsVersion1Point0::CalendarNotificationSettings:True:"] = "Write16_CalendarNotificationSettings";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x1700231E RID: 8990
		// (get) Token: 0x0600844E RID: 33870 RVA: 0x002400A0 File Offset: 0x0023E2A0
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Data.Storage.VersionedXml.CalendarNotificationSettingsVersion1Point0::CalendarNotificationSettings:True:", new CalendarNotificationSettingsVersion1Point0Serializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x0600844F RID: 33871 RVA: 0x002400E0 File Offset: 0x0023E2E0
		public override bool CanSerialize(Type type)
		{
			return type == typeof(CalendarNotificationSettingsVersion1Point0);
		}

		// Token: 0x06008450 RID: 33872 RVA: 0x002400F7 File Offset: 0x0023E2F7
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(CalendarNotificationSettingsVersion1Point0))
			{
				return new CalendarNotificationSettingsVersion1Point0Serializer();
			}
			return null;
		}

		// Token: 0x04005892 RID: 22674
		private Hashtable readMethods;

		// Token: 0x04005893 RID: 22675
		private Hashtable writeMethods;

		// Token: 0x04005894 RID: 22676
		private Hashtable typedSerializers;
	}
}
