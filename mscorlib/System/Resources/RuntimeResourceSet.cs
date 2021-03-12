using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;

namespace System.Resources
{
	// Token: 0x02000370 RID: 880
	internal sealed class RuntimeResourceSet : ResourceSet, IEnumerable
	{
		// Token: 0x06002C6E RID: 11374 RVA: 0x000A9AD8 File Offset: 0x000A7CD8
		[SecurityCritical]
		internal RuntimeResourceSet(string fileName) : base(false)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this._defaultReader = new ResourceReader(stream, this._resCache);
			this.Reader = this._defaultReader;
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000A9B24 File Offset: 0x000A7D24
		[SecurityCritical]
		internal RuntimeResourceSet(Stream stream) : base(false)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._defaultReader = new ResourceReader(stream, this._resCache);
			this.Reader = this._defaultReader;
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000A9B5C File Offset: 0x000A7D5C
		protected override void Dispose(bool disposing)
		{
			if (this.Reader == null)
			{
				return;
			}
			if (disposing)
			{
				IResourceReader reader = this.Reader;
				lock (reader)
				{
					this._resCache = null;
					if (this._defaultReader != null)
					{
						this._defaultReader.Close();
						this._defaultReader = null;
					}
					this._caseInsensitiveTable = null;
					base.Dispose(disposing);
					return;
				}
			}
			this._resCache = null;
			this._caseInsensitiveTable = null;
			this._defaultReader = null;
			base.Dispose(disposing);
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000A9BF0 File Offset: 0x000A7DF0
		public override IDictionaryEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000A9BF8 File Offset: 0x000A7DF8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000A9C00 File Offset: 0x000A7E00
		private IDictionaryEnumerator GetEnumeratorHelper()
		{
			IResourceReader reader = this.Reader;
			if (reader == null || this._resCache == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
			}
			return reader.GetEnumerator();
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000A9C38 File Offset: 0x000A7E38
		public override string GetString(string key)
		{
			object @object = this.GetObject(key, false, true);
			return (string)@object;
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000A9C58 File Offset: 0x000A7E58
		public override string GetString(string key, bool ignoreCase)
		{
			object @object = this.GetObject(key, ignoreCase, true);
			return (string)@object;
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000A9C75 File Offset: 0x000A7E75
		public override object GetObject(string key)
		{
			return this.GetObject(key, false, false);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000A9C80 File Offset: 0x000A7E80
		public override object GetObject(string key, bool ignoreCase)
		{
			return this.GetObject(key, ignoreCase, false);
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000A9C8C File Offset: 0x000A7E8C
		private object GetObject(string key, bool ignoreCase, bool isString)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.Reader == null || this._resCache == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
			}
			object obj = null;
			IResourceReader reader = this.Reader;
			object result;
			lock (reader)
			{
				if (this.Reader == null)
				{
					throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
				}
				ResourceLocator resourceLocator;
				if (this._defaultReader != null)
				{
					int num = -1;
					if (this._resCache.TryGetValue(key, out resourceLocator))
					{
						obj = resourceLocator.Value;
						num = resourceLocator.DataPosition;
					}
					if (num == -1 && obj == null)
					{
						num = this._defaultReader.FindPosForResource(key);
					}
					if (num != -1 && obj == null)
					{
						ResourceTypeCode value;
						if (isString)
						{
							obj = this._defaultReader.LoadString(num);
							value = ResourceTypeCode.String;
						}
						else
						{
							obj = this._defaultReader.LoadObject(num, out value);
						}
						resourceLocator = new ResourceLocator(num, ResourceLocator.CanCache(value) ? obj : null);
						Dictionary<string, ResourceLocator> resCache = this._resCache;
						lock (resCache)
						{
							this._resCache[key] = resourceLocator;
						}
					}
					if (obj != null || !ignoreCase)
					{
						return obj;
					}
				}
				if (!this._haveReadFromReader)
				{
					if (ignoreCase && this._caseInsensitiveTable == null)
					{
						this._caseInsensitiveTable = new Dictionary<string, ResourceLocator>(StringComparer.OrdinalIgnoreCase);
					}
					if (this._defaultReader == null)
					{
						IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
						while (enumerator.MoveNext())
						{
							DictionaryEntry entry = enumerator.Entry;
							string key2 = (string)entry.Key;
							ResourceLocator value2 = new ResourceLocator(-1, entry.Value);
							this._resCache.Add(key2, value2);
							if (ignoreCase)
							{
								this._caseInsensitiveTable.Add(key2, value2);
							}
						}
						if (!ignoreCase)
						{
							this.Reader.Close();
						}
					}
					else
					{
						ResourceReader.ResourceEnumerator enumeratorInternal = this._defaultReader.GetEnumeratorInternal();
						while (enumeratorInternal.MoveNext())
						{
							string key3 = (string)enumeratorInternal.Key;
							int dataPosition = enumeratorInternal.DataPosition;
							ResourceLocator value3 = new ResourceLocator(dataPosition, null);
							this._caseInsensitiveTable.Add(key3, value3);
						}
					}
					this._haveReadFromReader = true;
				}
				object obj2 = null;
				bool flag3 = false;
				bool keyInWrongCase = false;
				if (this._defaultReader != null && this._resCache.TryGetValue(key, out resourceLocator))
				{
					flag3 = true;
					obj2 = this.ResolveResourceLocator(resourceLocator, key, this._resCache, keyInWrongCase);
				}
				if (!flag3 && ignoreCase && this._caseInsensitiveTable.TryGetValue(key, out resourceLocator))
				{
					keyInWrongCase = true;
					obj2 = this.ResolveResourceLocator(resourceLocator, key, this._resCache, keyInWrongCase);
				}
				result = obj2;
			}
			return result;
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000A9F54 File Offset: 0x000A8154
		private object ResolveResourceLocator(ResourceLocator resLocation, string key, Dictionary<string, ResourceLocator> copyOfCache, bool keyInWrongCase)
		{
			object obj = resLocation.Value;
			if (obj == null)
			{
				IResourceReader reader = this.Reader;
				ResourceTypeCode value;
				lock (reader)
				{
					obj = this._defaultReader.LoadObject(resLocation.DataPosition, out value);
				}
				if (!keyInWrongCase && ResourceLocator.CanCache(value))
				{
					resLocation.Value = obj;
					copyOfCache[key] = resLocation;
				}
			}
			return obj;
		}

		// Token: 0x040011D2 RID: 4562
		internal const int Version = 2;

		// Token: 0x040011D3 RID: 4563
		private Dictionary<string, ResourceLocator> _resCache;

		// Token: 0x040011D4 RID: 4564
		private ResourceReader _defaultReader;

		// Token: 0x040011D5 RID: 4565
		private Dictionary<string, ResourceLocator> _caseInsensitiveTable;

		// Token: 0x040011D6 RID: 4566
		private bool _haveReadFromReader;
	}
}
