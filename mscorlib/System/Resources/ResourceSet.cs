using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Resources
{
	// Token: 0x0200036D RID: 877
	[ComVisible(true)]
	[Serializable]
	public class ResourceSet : IDisposable, IEnumerable
	{
		// Token: 0x06002C47 RID: 11335 RVA: 0x000A8AD5 File Offset: 0x000A6CD5
		protected ResourceSet()
		{
			this.CommonInit();
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000A8AE3 File Offset: 0x000A6CE3
		internal ResourceSet(bool junk)
		{
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000A8AEB File Offset: 0x000A6CEB
		public ResourceSet(string fileName)
		{
			this.Reader = new ResourceReader(fileName);
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000A8B0B File Offset: 0x000A6D0B
		[SecurityCritical]
		public ResourceSet(Stream stream)
		{
			this.Reader = new ResourceReader(stream);
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000A8B2B File Offset: 0x000A6D2B
		public ResourceSet(IResourceReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Reader = reader;
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000A8B54 File Offset: 0x000A6D54
		private void CommonInit()
		{
			this.Table = new Hashtable();
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000A8B61 File Offset: 0x000A6D61
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000A8B6C File Offset: 0x000A6D6C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IResourceReader reader = this.Reader;
				this.Reader = null;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this.Reader = null;
			this._caseInsensitiveTable = null;
			this.Table = null;
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000A8BA8 File Offset: 0x000A6DA8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000A8BB1 File Offset: 0x000A6DB1
		public virtual Type GetDefaultReader()
		{
			return typeof(ResourceReader);
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000A8BBD File Offset: 0x000A6DBD
		public virtual Type GetDefaultWriter()
		{
			return typeof(ResourceWriter);
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000A8BC9 File Offset: 0x000A6DC9
		[ComVisible(false)]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000A8BD1 File Offset: 0x000A6DD1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000A8BDC File Offset: 0x000A6DDC
		private IDictionaryEnumerator GetEnumeratorHelper()
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
			}
			return table.GetEnumerator();
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000A8C0C File Offset: 0x000A6E0C
		public virtual string GetString(string name)
		{
			object objectInternal = this.GetObjectInternal(name);
			string result;
			try
			{
				result = (string)objectInternal;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", new object[]
				{
					name
				}));
			}
			return result;
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000A8C58 File Offset: 0x000A6E58
		public virtual string GetString(string name, bool ignoreCase)
		{
			object obj = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", new object[]
				{
					name
				}));
			}
			if (text != null || !ignoreCase)
			{
				return text;
			}
			obj = this.GetCaseInsensitiveObjectInternal(name);
			string result;
			try
			{
				result = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", new object[]
				{
					name
				}));
			}
			return result;
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000A8CE4 File Offset: 0x000A6EE4
		public virtual object GetObject(string name)
		{
			return this.GetObjectInternal(name);
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000A8CF0 File Offset: 0x000A6EF0
		public virtual object GetObject(string name, bool ignoreCase)
		{
			object objectInternal = this.GetObjectInternal(name);
			if (objectInternal != null || !ignoreCase)
			{
				return objectInternal;
			}
			return this.GetCaseInsensitiveObjectInternal(name);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000A8D14 File Offset: 0x000A6F14
		protected virtual void ReadResources()
		{
			IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object value = enumerator.Value;
				this.Table.Add(enumerator.Key, value);
			}
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000A8D50 File Offset: 0x000A6F50
		private object GetObjectInternal(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
			}
			return table[name];
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000A8D90 File Offset: 0x000A6F90
		private object GetCaseInsensitiveObjectInternal(string name)
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
			}
			Hashtable hashtable = this._caseInsensitiveTable;
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
				IDictionaryEnumerator enumerator = table.GetEnumerator();
				while (enumerator.MoveNext())
				{
					hashtable.Add(enumerator.Key, enumerator.Value);
				}
				this._caseInsensitiveTable = hashtable;
			}
			return hashtable[name];
		}

		// Token: 0x040011B0 RID: 4528
		[NonSerialized]
		protected IResourceReader Reader;

		// Token: 0x040011B1 RID: 4529
		protected Hashtable Table;

		// Token: 0x040011B2 RID: 4530
		private Hashtable _caseInsensitiveTable;
	}
}
