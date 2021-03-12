using System;
using System.Collections;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.WorkHoursInCalendar
{
	// Token: 0x02000F08 RID: 3848
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x17002325 RID: 8997
		// (get) Token: 0x0600848F RID: 33935 RVA: 0x00242EB9 File Offset: 0x002410B9
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderWorkHoursInCalendar();
			}
		}

		// Token: 0x17002326 RID: 8998
		// (get) Token: 0x06008490 RID: 33936 RVA: 0x00242EC0 File Offset: 0x002410C0
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterWorkHoursInCalendar();
			}
		}

		// Token: 0x17002327 RID: 8999
		// (get) Token: 0x06008491 RID: 33937 RVA: 0x00242EC8 File Offset: 0x002410C8
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.Storage.WorkHoursInCalendar:WorkingHours.xsd:Root:True:"] = "Read9_Root";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17002328 RID: 9000
		// (get) Token: 0x06008492 RID: 33938 RVA: 0x00242F08 File Offset: 0x00241108
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Data.Storage.WorkHoursInCalendar:WorkingHours.xsd:Root:True:"] = "Write9_Root";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17002329 RID: 9001
		// (get) Token: 0x06008493 RID: 33939 RVA: 0x00242F48 File Offset: 0x00241148
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Data.Storage.WorkHoursInCalendar:WorkingHours.xsd:Root:True:", new WorkHoursInCalendarSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x06008494 RID: 33940 RVA: 0x00242F88 File Offset: 0x00241188
		public override bool CanSerialize(Type type)
		{
			return type == typeof(WorkHoursInCalendar);
		}

		// Token: 0x06008495 RID: 33941 RVA: 0x00242F9F File Offset: 0x0024119F
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(WorkHoursInCalendar))
			{
				return new WorkHoursInCalendarSerializer();
			}
			return null;
		}

		// Token: 0x040058C7 RID: 22727
		private Hashtable readMethods;

		// Token: 0x040058C8 RID: 22728
		private Hashtable writeMethods;

		// Token: 0x040058C9 RID: 22729
		private Hashtable typedSerializers;
	}
}
