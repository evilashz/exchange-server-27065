using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200029B RID: 667
	internal class RegistrySession : IConfigDataProvider
	{
		// Token: 0x06001822 RID: 6178 RVA: 0x0004BC06 File Offset: 0x00049E06
		public RegistrySession() : this(false)
		{
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0004BC0F File Offset: 0x00049E0F
		public RegistrySession(bool ignoreErrorsOnRead) : this(ignoreErrorsOnRead, Registry.LocalMachine)
		{
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0004BC20 File Offset: 0x00049E20
		public RegistrySession(bool ignoreErrorsOnRead, RegistryKey root)
		{
			ArgumentValidator.ThrowIfNull("root", root);
			this.RootKey = root;
			this.IgnoreReadErrors = ignoreErrorsOnRead;
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0004BC72 File Offset: 0x00049E72
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x0004BC7A File Offset: 0x00049E7A
		public RegistryKey RootKey { get; private set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x0004BC83 File Offset: 0x00049E83
		// (set) Token: 0x06001828 RID: 6184 RVA: 0x0004BC8B File Offset: 0x00049E8B
		public bool IgnoreReadErrors { get; private set; }

		// Token: 0x06001829 RID: 6185 RVA: 0x0004BC94 File Offset: 0x00049E94
		public T Read<T>() where T : RegistryObject, new()
		{
			T t = Activator.CreateInstance<T>();
			RegistryObject registryObject = t;
			this.ReadObject(registryObject.RegistrySchema.DefaultRegistryKeyPath, registryObject.RegistrySchema.DefaultName, this.IgnoreReadErrors, ref registryObject);
			return t;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0004BCD4 File Offset: 0x00049ED4
		public T Read<T>(RegistryObjectId identity) where T : RegistryObject, new()
		{
			T t = Activator.CreateInstance<T>();
			RegistryObject registryObject = t;
			this.ReadObject(identity.RegistryKeyPath ?? registryObject.RegistrySchema.DefaultRegistryKeyPath, identity.Name ?? registryObject.RegistrySchema.DefaultName, this.IgnoreReadErrors, ref registryObject);
			return t;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0004BD28 File Offset: 0x00049F28
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			RegistryObject registryObject = t as RegistryObject;
			RegistryObjectId registryObjectId;
			if (identity != null)
			{
				registryObjectId = (identity as RegistryObjectId);
			}
			else
			{
				registryObjectId = new RegistryObjectId(registryObject.RegistrySchema.DefaultRegistryKeyPath, registryObject.RegistrySchema.DefaultName);
			}
			this.ReadObject(registryObjectId.RegistryKeyPath, registryObjectId.Name, this.IgnoreReadErrors, ref registryObject);
			return t;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0004BDB0 File Offset: 0x00049FB0
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			if (filter != null || !deepSearch || sortBy != null)
			{
				throw new NotSupportedException();
			}
			RegistryObjectId registryId = rootId as RegistryObjectId;
			List<IConfigurable> list = new List<IConfigurable>();
			foreach (T t in this.Find<T>(registryId))
			{
				list.Add(t);
			}
			return list.ToArray();
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0004BE10 File Offset: 0x0004A010
		public T[] Find<T>(RegistryObjectId registryId) where T : IConfigurable, new()
		{
			string[] array = null;
			using (RegistryKey registryKey = this.RootKey.OpenSubKey(registryId.RegistryKeyPath, true))
			{
				if (registryKey == null)
				{
					return new T[0];
				}
				array = registryKey.GetSubKeyNames();
			}
			List<T> list = new List<T>();
			if (array != null)
			{
				foreach (string folderName in array)
				{
					T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
					RegistryObject registryObject = t as RegistryObject;
					this.ReadObject(registryId.RegistryKeyPath, folderName, this.IgnoreReadErrors, ref registryObject);
					list.Add(t);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0004BEE4 File Offset: 0x0004A0E4
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0004BEEC File Offset: 0x0004A0EC
		public void Save(IConfigurable instance)
		{
			RegistryObject registryObject = instance as RegistryObject;
			RegistryObjectId registryObjectId = registryObject.Identity as RegistryObjectId;
			string folderPath = (registryObjectId != null) ? registryObjectId.RegistryKeyPath : registryObject.RegistrySchema.DefaultRegistryKeyPath;
			string text = (registryObjectId != null) ? registryObjectId.Name : registryObject.RegistrySchema.DefaultName;
			using (RegistryKey registryKey = this.CreateRegistryPathIfMissing(folderPath))
			{
				if (!registryKey.GetSubKeyNames().Contains(text, StringComparer.OrdinalIgnoreCase))
				{
					RegistryWriter.Instance.CreateSubKey(registryKey, text);
				}
				ObjectState objectState = instance.ObjectState;
				foreach (PropertyDefinition propertyDefinition in registryObject.ObjectSchema.AllProperties)
				{
					SimpleProviderPropertyDefinition simpleProviderPropertyDefinition = (SimpleProviderPropertyDefinition)propertyDefinition;
					if (!simpleProviderPropertyDefinition.IsCalculated && !this.excludedPersistentProperties.Contains(simpleProviderPropertyDefinition))
					{
						if ((registryObject[simpleProviderPropertyDefinition] != null && registryObject[simpleProviderPropertyDefinition].Equals(simpleProviderPropertyDefinition.DefaultValue)) || (registryObject[simpleProviderPropertyDefinition] == null && simpleProviderPropertyDefinition.DefaultValue == null))
						{
							RegistryWriter.Instance.DeleteValue(registryKey, text, simpleProviderPropertyDefinition.Name);
						}
						else
						{
							RegistryWriter.Instance.SetValue(registryKey, text, simpleProviderPropertyDefinition.Name, registryObject[simpleProviderPropertyDefinition], RegistryValueKind.String);
						}
					}
				}
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0004C054 File Offset: 0x0004A254
		public void Delete(IConfigurable instance)
		{
			RegistryObject registryObject = instance as RegistryObject;
			RegistryObjectId registryObjectId = registryObject.Identity as RegistryObjectId;
			using (RegistryKey registryKey = this.RootKey.OpenSubKey(registryObjectId.RegistryKeyPath, true))
			{
				if (registryKey != null)
				{
					foreach (PropertyDefinition propertyDefinition in registryObject.ObjectSchema.AllProperties)
					{
						SimpleProviderPropertyDefinition simpleProviderPropertyDefinition = (SimpleProviderPropertyDefinition)propertyDefinition;
						if (!this.excludedPersistentProperties.Contains(simpleProviderPropertyDefinition))
						{
							RegistryWriter.Instance.DeleteValue(registryKey, registryObjectId.Name, simpleProviderPropertyDefinition.Name);
						}
					}
					bool flag = false;
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(registryObjectId.Name, false))
					{
						if (registryKey2.GetValueNames().Length == 0 && registryKey2.GetSubKeyNames().Length == 0)
						{
							flag = true;
						}
					}
					if (flag)
					{
						registryKey.DeleteSubKey(registryObjectId.Name, true);
					}
				}
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x0004C16C File Offset: 0x0004A36C
		public string Source
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0004C17C File Offset: 0x0004A37C
		private RegistryKey CreateRegistryPathIfMissing(string folderPath)
		{
			RegistryKey registryKey = this.RootKey.OpenSubKey(folderPath, true);
			if (registryKey != null)
			{
				return registryKey;
			}
			return this.RootKey.CreateSubKey(folderPath);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0004C1A8 File Offset: 0x0004A3A8
		private void ReadObject(string folderPath, string folderName, ref RegistryObject instance)
		{
			RegistryObjectSchema registrySchema = instance.RegistrySchema;
			using (RegistryKey registryKey = this.RootKey.OpenSubKey(folderPath))
			{
				if (registryKey == null || !registryKey.GetSubKeyNames().Contains(folderName, StringComparer.OrdinalIgnoreCase))
				{
					return;
				}
				foreach (PropertyDefinition propertyDefinition in registrySchema.AllProperties)
				{
					SimpleProviderPropertyDefinition simpleProviderPropertyDefinition = (SimpleProviderPropertyDefinition)propertyDefinition;
					if (!simpleProviderPropertyDefinition.IsCalculated && !this.excludedPersistentProperties.Contains(simpleProviderPropertyDefinition))
					{
						object obj = RegistryReader.Instance.GetValue<object>(registryKey, folderName, simpleProviderPropertyDefinition.Name, simpleProviderPropertyDefinition.DefaultValue);
						try
						{
							obj = ValueConvertor.ConvertValue(obj, simpleProviderPropertyDefinition.Type, null);
						}
						catch (Exception ex)
						{
							instance.AddValidationError(new PropertyValidationError(DataStrings.ErrorCannotConvertFromString(obj as string, simpleProviderPropertyDefinition.Type.Name, ex.Message), simpleProviderPropertyDefinition, obj));
							continue;
						}
						try
						{
							instance[simpleProviderPropertyDefinition] = obj;
						}
						catch (DataValidationException ex2)
						{
							instance.AddValidationError(ex2.Error);
						}
					}
				}
			}
			instance.propertyBag[SimpleProviderObjectSchema.Identity] = new RegistryObjectId(folderPath, folderName);
			instance.ResetChangeTracking(true);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0004C310 File Offset: 0x0004A510
		private void ReadObject(string folderPath, string folderName, bool ignoreErrors, ref RegistryObject instance)
		{
			if (!this.IgnoreReadErrors)
			{
				this.ReadObject(folderPath, folderName, ref instance);
				return;
			}
			try
			{
				this.ReadObject(folderPath, folderName, ref instance);
			}
			catch (IOException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x04000E3E RID: 3646
		private readonly ProviderPropertyDefinition[] excludedPersistentProperties = new ProviderPropertyDefinition[]
		{
			SimpleProviderObjectSchema.Identity,
			SimpleProviderObjectSchema.ObjectState,
			SimpleProviderObjectSchema.ExchangeVersion
		};
	}
}
