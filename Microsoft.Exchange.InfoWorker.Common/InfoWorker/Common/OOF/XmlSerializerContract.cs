using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000028 RID: 40
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000530C File Offset: 0x0000350C
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderUserOofSettingsSerializer();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005313 File Offset: 0x00003513
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterUserOofSettingsSerializer();
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000531C File Offset: 0x0000351C
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.InfoWorker.Common.OOF.UserOofSettingsSerializer::UserOofSettings:True:"] = "Read8_UserOofSettings";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000535C File Offset: 0x0000355C
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["Microsoft.Exchange.InfoWorker.Common.OOF.UserOofSettingsSerializer::UserOofSettings:True:"] = "Write7_UserOofSettings";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000539C File Offset: 0x0000359C
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("Microsoft.Exchange.InfoWorker.Common.OOF.UserOofSettingsSerializer::UserOofSettings:True:", new UserOofSettingsSerializerSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanSerialize(Type type)
		{
			return type == typeof(UserOofSettingsSerializer);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000053F3 File Offset: 0x000035F3
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(UserOofSettingsSerializer))
			{
				return new UserOofSettingsSerializerSerializer();
			}
			return null;
		}

		// Token: 0x0400006F RID: 111
		private Hashtable readMethods;

		// Token: 0x04000070 RID: 112
		private Hashtable writeMethods;

		// Token: 0x04000071 RID: 113
		private Hashtable typedSerializers;
	}
}
