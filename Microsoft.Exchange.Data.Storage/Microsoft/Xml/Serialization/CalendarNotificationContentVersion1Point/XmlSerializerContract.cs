using System;
using System.Collections;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.CalendarNotificationContentVersion1Point0
{
	// Token: 0x02000EF9 RID: 3833
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x17002314 RID: 8980
		// (get) Token: 0x0600841D RID: 33821 RVA: 0x0023DDE0 File Offset: 0x0023BFE0
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderCalendarNotificationContentVersion1Point0();
			}
		}

		// Token: 0x17002315 RID: 8981
		// (get) Token: 0x0600841E RID: 33822 RVA: 0x0023DDE7 File Offset: 0x0023BFE7
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterCalendarNotificationContentVersion1Point0();
			}
		}

		// Token: 0x17002316 RID: 8982
		// (get) Token: 0x0600841F RID: 33823 RVA: 0x0023DDF0 File Offset: 0x0023BFF0
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.Storage.VersionedXml.CalendarNotificationContentVersion1Point0::CalendarNotificationContent:True:"] = "Read7_CalendarNotificationContent";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17002317 RID: 8983
		// (get) Token: 0x06008420 RID: 33824 RVA: 0x0023DE30 File Offset: 0x0023C030
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.Storage.VersionedXml.CalendarNotificationContentVersion1Point0::CalendarNotificationContent:True:"] = "Write7_CalendarNotificationContent";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17002318 RID: 8984
		// (get) Token: 0x06008421 RID: 33825 RVA: 0x0023DE70 File Offset: 0x0023C070
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Data.Storage.VersionedXml.CalendarNotificationContentVersion1Point0::CalendarNotificationContent:True:", new CalendarNotificationContentVersion1Point0Serializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x06008422 RID: 33826 RVA: 0x0023DEB0 File Offset: 0x0023C0B0
		public override bool CanSerialize(Type type)
		{
			return type == typeof(CalendarNotificationContentVersion1Point0);
		}

		// Token: 0x06008423 RID: 33827 RVA: 0x0023DEC7 File Offset: 0x0023C0C7
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(CalendarNotificationContentVersion1Point0))
			{
				return new CalendarNotificationContentVersion1Point0Serializer();
			}
			return null;
		}

		// Token: 0x04005870 RID: 22640
		private Hashtable readMethods;

		// Token: 0x04005871 RID: 22641
		private Hashtable writeMethods;

		// Token: 0x04005872 RID: 22642
		private Hashtable typedSerializers;
	}
}
