using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Microsoft.Exchange.Data.Common
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public struct LocalizedString : ISerializable, ILocalizedString, IFormattable, IEquatable<LocalizedString>
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002B04 File Offset: 0x00000D04
		public LocalizedString(string id, ExchangeResourceManager resourceManager, params object[] inserts)
		{
			this = new LocalizedString(id, null, false, false, resourceManager, inserts);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B14 File Offset: 0x00000D14
		public LocalizedString(string id, string stringId, bool showStringIdIfError, bool showAssistanceInfoIfError, ExchangeResourceManager resourceManager, params object[] inserts)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (resourceManager == null)
			{
				throw new ArgumentNullException("resourceManager");
			}
			this.Id = id;
			this.stringId = stringId;
			this.showStringIdInUIIfError = showStringIdIfError;
			this.showAssistanceInfoInUIIfError = showAssistanceInfoIfError;
			this.ResourceManager = resourceManager;
			this.DeserializedFallback = null;
			this.Inserts = ((inserts != null && inserts.Length > 0) ? inserts : null);
			this.formatParameters = ((this.Inserts != null) ? new ReadOnlyCollection<object>(this.Inserts) : null);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B9C File Offset: 0x00000D9C
		public LocalizedString(string value)
		{
			this.Id = value;
			this.stringId = null;
			this.showStringIdInUIIfError = false;
			this.showAssistanceInfoInUIIfError = false;
			this.Inserts = null;
			this.ResourceManager = null;
			this.DeserializedFallback = null;
			this.formatParameters = null;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002BD8 File Offset: 0x00000DD8
		private LocalizedString(string format, object[] inserts)
		{
			this.Id = format;
			this.stringId = null;
			this.showStringIdInUIIfError = false;
			this.showAssistanceInfoInUIIfError = false;
			this.Inserts = inserts;
			this.ResourceManager = null;
			this.DeserializedFallback = null;
			this.formatParameters = ((this.Inserts != null) ? new ReadOnlyCollection<object>(this.Inserts) : null);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002C34 File Offset: 0x00000E34
		private LocalizedString(SerializationInfo info, StreamingContext context)
		{
			this.Inserts = (object[])info.GetValue("inserts", typeof(object[]));
			this.formatParameters = ((this.Inserts != null) ? new ReadOnlyCollection<object>(this.Inserts) : null);
			this.ResourceManager = null;
			this.Id = null;
			this.stringId = null;
			this.showStringIdInUIIfError = false;
			this.showAssistanceInfoInUIIfError = false;
			this.DeserializedFallback = null;
			string text = null;
			try
			{
				string @string = info.GetString("baseName");
				text = info.GetString("fallback");
				if (!string.IsNullOrEmpty(@string))
				{
					string string2 = info.GetString("assemblyName");
					Assembly assembly = Assembly.Load(new AssemblyName(string2));
					this.ResourceManager = ExchangeResourceManager.GetResourceManager(@string, assembly);
					this.Id = info.GetString("id");
					if (this.ResourceManager.GetString(this.Id) == null)
					{
						this.ResourceManager = null;
					}
					else
					{
						this.DeserializedFallback = text;
						try
						{
							this.stringId = info.GetString("stringId");
							this.showStringIdInUIIfError = info.GetBoolean("showStringIdInUIIfError");
							this.showAssistanceInfoInUIIfError = info.GetBoolean("showAssistanceInfoInUIIfError");
						}
						catch (SerializationException)
						{
							this.stringId = null;
							this.showStringIdInUIIfError = false;
							this.showAssistanceInfoInUIIfError = false;
						}
					}
				}
			}
			catch (SerializationException)
			{
				this.ResourceManager = null;
			}
			catch (FileNotFoundException)
			{
				this.ResourceManager = null;
			}
			catch (FileLoadException)
			{
				this.ResourceManager = null;
			}
			catch (MissingManifestResourceException)
			{
				this.ResourceManager = null;
			}
			if (this.ResourceManager == null)
			{
				this.Id = text;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002DEC File Offset: 0x00000FEC
		public bool IsEmpty
		{
			get
			{
				return null == this.Id;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002DF7 File Offset: 0x00000FF7
		public string FullId
		{
			get
			{
				return ((this.ResourceManager != null) ? this.ResourceManager.BaseName : string.Empty) + this.Id;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002E1E File Offset: 0x0000101E
		public int BaseId
		{
			get
			{
				return this.FullId.GetHashCode();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002E2B File Offset: 0x0000102B
		public string StringId
		{
			get
			{
				if (this.stringId != null)
				{
					return this.stringId;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002E41 File Offset: 0x00001041
		public bool ShowStringIdInUIIfError
		{
			get
			{
				return this.showStringIdInUIIfError;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002E49 File Offset: 0x00001049
		public bool ShowAssistanceInfoInUIIfError
		{
			get
			{
				return this.showAssistanceInfoInUIIfError;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002E51 File Offset: 0x00001051
		LocalizedString ILocalizedString.LocalizedString
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002E59 File Offset: 0x00001059
		public ReadOnlyCollection<object> FormatParameters
		{
			get
			{
				return this.formatParameters;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002E61 File Offset: 0x00001061
		public static bool operator ==(LocalizedString s1, LocalizedString s2)
		{
			return s1.Equals(s2);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002E6B File Offset: 0x0000106B
		public static bool operator !=(LocalizedString s1, LocalizedString s2)
		{
			return !s1.Equals(s2);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E78 File Offset: 0x00001078
		public static implicit operator string(LocalizedString value)
		{
			return value.ToString();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002E88 File Offset: 0x00001088
		public static LocalizedString Join(object separator, object[] value)
		{
			if (value == null || value.Length == 0)
			{
				return LocalizedString.Empty;
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			object[] array = new object[value.Length + 1];
			array[0] = separator;
			Array.Copy(value, 0, array, 1, value.Length);
			StringBuilder stringBuilder = new StringBuilder(6 * value.Length);
			stringBuilder.Append("{");
			for (int i = 1; i < value.Length; i++)
			{
				stringBuilder.Append(i);
				stringBuilder.Append("}{0}{");
			}
			stringBuilder.Append(value.Length + "}");
			return new LocalizedString(stringBuilder.ToString(), array);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F28 File Offset: 0x00001128
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			object[] array = null;
			if (this.Inserts != null && this.Inserts.Length > 0)
			{
				array = new object[this.Inserts.Length];
				for (int i = 0; i < this.Inserts.Length; i++)
				{
					object obj = this.Inserts[i];
					if (obj != null)
					{
						if (obj is ILocalizedString)
						{
							obj = ((ILocalizedString)obj).LocalizedString;
						}
						else if (!obj.GetType().GetTypeInfo().IsSerializable && !(obj is ISerializable))
						{
							object obj2 = LocalizedString.TranslateObject(obj, CultureInfo.InvariantCulture);
							if (obj2 == obj)
							{
								obj = obj.ToString();
							}
							else
							{
								obj = obj2;
							}
						}
					}
					array[i] = obj;
				}
			}
			info.AddValue("inserts", array);
			if (this.ResourceManager == null)
			{
				if (this.DeserializedFallback == null)
				{
					info.AddValue("fallback", this.Id);
				}
				else
				{
					info.AddValue("fallback", this.DeserializedFallback);
				}
				info.AddValue("baseName", string.Empty);
				return;
			}
			info.AddValue("baseName", this.ResourceManager.BaseName);
			info.AddValue("assemblyName", this.ResourceManager.AssemblyName);
			info.AddValue("id", this.Id);
			info.AddValue("stringId", this.stringId);
			info.AddValue("showStringIdInUIIfError", this.showStringIdInUIIfError);
			info.AddValue("showAssistanceInfoInUIIfError", this.showAssistanceInfoInUIIfError);
			if (this.DeserializedFallback == null)
			{
				info.AddValue("fallback", this.ResourceManager.GetString(this.Id, CultureInfo.InvariantCulture));
				return;
			}
			info.AddValue("fallback", this.DeserializedFallback);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000030D0 File Offset: 0x000012D0
		public LocalizedString RecreateWithNewParams(params object[] inserts)
		{
			return new LocalizedString(this.Id, this.StringId, this.ShowStringIdInUIIfError, this.ShowAssistanceInfoInUIIfError, this.ResourceManager, inserts);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000030F6 File Offset: 0x000012F6
		public override string ToString()
		{
			return ((IFormattable)this).ToString(null, null);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000310A File Offset: 0x0000130A
		public string ToString(IFormatProvider formatProvider)
		{
			return ((IFormattable)this).ToString(null, formatProvider);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003120 File Offset: 0x00001320
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			if (this.IsEmpty)
			{
				return string.Empty;
			}
			format = ((this.ResourceManager != null) ? this.ResourceManager.GetString(this.Id, formatProvider as CultureInfo) : this.Id);
			if (this.Inserts != null && this.Inserts.Length > 0)
			{
				object[] array = new object[this.Inserts.Length];
				for (int i = 0; i < this.Inserts.Length; i++)
				{
					object obj = this.Inserts[i];
					if (obj is ILocalizedString)
					{
						obj = ((ILocalizedString)obj).LocalizedString;
					}
					else
					{
						obj = LocalizedString.TranslateObject(obj, formatProvider);
					}
					array[i] = obj;
				}
				try
				{
					return string.Format(formatProvider, format, array);
				}
				catch (FormatException)
				{
					if (this.DeserializedFallback == null)
					{
						throw;
					}
					return string.Format(formatProvider, this.DeserializedFallback, array);
				}
				return format;
			}
			return format;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003204 File Offset: 0x00001404
		public override int GetHashCode()
		{
			int num = (this.Id != null) ? this.Id.GetHashCode() : 0;
			int num2 = (this.ResourceManager != null) ? this.ResourceManager.GetHashCode() : 0;
			int num3 = num ^ num2;
			if (this.Inserts != null)
			{
				for (int i = 0; i < this.Inserts.Length; i++)
				{
					num3 = (num3 ^ i ^ ((this.Inserts[i] != null) ? this.Inserts[i].GetHashCode() : 0));
				}
			}
			return num3;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000327E File Offset: 0x0000147E
		public override bool Equals(object obj)
		{
			return obj is LocalizedString && this.Equals((LocalizedString)obj);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003298 File Offset: 0x00001498
		public bool Equals(LocalizedString that)
		{
			if (!string.Equals(this.Id, that.Id, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (!string.Equals(this.stringId, that.stringId, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (this.showStringIdInUIIfError != that.showStringIdInUIIfError)
			{
				return false;
			}
			if (this.showAssistanceInfoInUIIfError != that.showAssistanceInfoInUIIfError)
			{
				return false;
			}
			if (null != this.ResourceManager ^ null != that.ResourceManager)
			{
				return false;
			}
			if (this.ResourceManager != null && !this.ResourceManager.Equals(that.ResourceManager))
			{
				return false;
			}
			if (null != this.Inserts ^ null != that.Inserts)
			{
				return false;
			}
			if (this.Inserts != null && that.Inserts != null)
			{
				if (this.Inserts.Length != that.Inserts.Length)
				{
					return false;
				}
				for (int i = 0; i < this.Inserts.Length; i++)
				{
					if (null != this.Inserts[i] ^ null != that.Inserts[i])
					{
						return false;
					}
					if (this.Inserts[i] != null && that.Inserts[i] != null && !this.Inserts[i].Equals(that.Inserts[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000033DD File Offset: 0x000015DD
		private static object TranslateObject(object badObject, IFormatProvider formatProvider)
		{
			if (badObject is Exception)
			{
				return ((Exception)badObject).Message;
			}
			return badObject;
		}

		// Token: 0x04000012 RID: 18
		public static readonly LocalizedString Empty = default(LocalizedString);

		// Token: 0x04000013 RID: 19
		internal readonly string Id;

		// Token: 0x04000014 RID: 20
		private readonly object[] Inserts;

		// Token: 0x04000015 RID: 21
		private readonly string stringId;

		// Token: 0x04000016 RID: 22
		private readonly bool showStringIdInUIIfError;

		// Token: 0x04000017 RID: 23
		private readonly bool showAssistanceInfoInUIIfError;

		// Token: 0x04000018 RID: 24
		private readonly ExchangeResourceManager ResourceManager;

		// Token: 0x04000019 RID: 25
		private readonly string DeserializedFallback;

		// Token: 0x0400001A RID: 26
		private ReadOnlyCollection<object> formatParameters;
	}
}
