using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200067D RID: 1661
	[Serializable]
	public class XMLSerializableCompressed<T> : XMLSerializableBase where T : class
	{
		// Token: 0x06004D82 RID: 19842 RVA: 0x0011DEEE File Offset: 0x0011C0EE
		public XMLSerializableCompressed()
		{
			this.internalValue = default(T);
			this.internalData = null;
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x0011DF14 File Offset: 0x0011C114
		public XMLSerializableCompressed(T value)
		{
			this.internalValue = value;
			this.internalData = null;
		}

		// Token: 0x06004D84 RID: 19844 RVA: 0x0011DF35 File Offset: 0x0011C135
		public static implicit operator T(XMLSerializableCompressed<T> proxy)
		{
			return proxy.Value;
		}

		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x06004D85 RID: 19845 RVA: 0x0011DF40 File Offset: 0x0011C140
		// (set) Token: 0x06004D86 RID: 19846 RVA: 0x0011DFB8 File Offset: 0x0011C1B8
		[XmlIgnore]
		public T Value
		{
			get
			{
				T result;
				lock (this.Locker)
				{
					if (this.internalData != null && this.internalValue == null)
					{
						string serializedXML = this.DecompressString(this.internalData);
						this.internalValue = XMLSerializableBase.Deserialize<T>(serializedXML, this.XmlRawProperty);
					}
					result = this.internalValue;
				}
				return result;
			}
			set
			{
				lock (this.Locker)
				{
					this.internalValue = value;
					this.internalData = null;
				}
			}
		}

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x06004D87 RID: 19847 RVA: 0x0011E000 File Offset: 0x0011C200
		// (set) Token: 0x06004D88 RID: 19848 RVA: 0x0011E074 File Offset: 0x0011C274
		[XmlElement("Data")]
		public byte[] Data
		{
			get
			{
				byte[] result;
				lock (this.Locker)
				{
					if (this.internalValue != null && this.internalData == null)
					{
						this.internalData = StringUtil.CompressString(XMLSerializableBase.Serialize(this.internalValue, false));
					}
					result = this.internalData;
				}
				return result;
			}
			set
			{
				lock (this.Locker)
				{
					this.internalData = value;
					this.internalValue = default(T);
				}
			}
		}

		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x06004D89 RID: 19849 RVA: 0x0011E0C4 File Offset: 0x0011C2C4
		protected virtual PropertyDefinition XmlRawProperty
		{
			get
			{
				return XMLSerializableBase.ConfigurationXmlRawProperty();
			}
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x0011E0CC File Offset: 0x0011C2CC
		public string DecompressString(byte[] data)
		{
			string result;
			try
			{
				result = StringUtil.DecompressString(data);
			}
			catch (InvalidDataException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(this.XmlRawProperty.Name, ex.Message), this.XmlRawProperty, Convert.ToBase64String(data)), ex);
			}
			catch (EndOfStreamException ex2)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(this.XmlRawProperty.Name, ex2.Message), this.XmlRawProperty, Convert.ToBase64String(data)), ex2);
			}
			return result;
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x0011E160 File Offset: 0x0011C360
		public override string ToString()
		{
			lock (this.Locker)
			{
				if (this.internalValue != null)
				{
					return this.internalValue.ToString();
				}
			}
			return null;
		}

		// Token: 0x040034B3 RID: 13491
		private readonly object Locker = new object();

		// Token: 0x040034B4 RID: 13492
		private T internalValue;

		// Token: 0x040034B5 RID: 13493
		private byte[] internalData;
	}
}
