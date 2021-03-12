using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200025C RID: 604
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x000608F9 File Offset: 0x0005EAF9
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderSetupComponentInfo();
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00060900 File Offset: 0x0005EB00
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterSetupComponentInfo();
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00060908 File Offset: 0x0005EB08
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Management.Deployment.SetupComponentInfo::"] = "Read13_SetupComponentInfo";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00060948 File Offset: 0x0005EB48
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.Management.Deployment.SetupComponentInfo::"] = "Write13_SetupComponentInfo";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00060988 File Offset: 0x0005EB88
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.Management.Deployment.SetupComponentInfo::", new SetupComponentInfoSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000609C8 File Offset: 0x0005EBC8
		public override bool CanSerialize(Type type)
		{
			return type == typeof(SetupComponentInfo);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000609DF File Offset: 0x0005EBDF
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(SetupComponentInfo))
			{
				return new SetupComponentInfoSerializer();
			}
			return null;
		}

		// Token: 0x040009DC RID: 2524
		private Hashtable readMethods;

		// Token: 0x040009DD RID: 2525
		private Hashtable writeMethods;

		// Token: 0x040009DE RID: 2526
		private Hashtable typedSerializers;
	}
}
