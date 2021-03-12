using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000014 RID: 20
	internal sealed class StringPool
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003F69 File Offset: 0x00002169
		public StringPool(IEqualityComparer<string> equalityComparer) : this(null, equalityComparer)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003F74 File Offset: 0x00002174
		public StringPool(ICollection<string> initialStrings, IEqualityComparer<string> equalityComparer)
		{
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			if (initialStrings != null)
			{
				this.stringDictionary = new Dictionary<string, string>(initialStrings.Count, equalityComparer);
				using (IEnumerator<string> enumerator = initialStrings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						this.stringDictionary.Add(text, text);
					}
					return;
				}
			}
			this.stringDictionary = new Dictionary<string, string>(equalityComparer);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004004 File Offset: 0x00002204
		public string Intern(string stringToIntern)
		{
			if (stringToIntern == null)
			{
				return null;
			}
			return this.IsInterned(stringToIntern) ?? this.AddStringToDictionary(stringToIntern);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004020 File Offset: 0x00002220
		public string IsInterned(string stringToIntern)
		{
			if (stringToIntern == null)
			{
				return null;
			}
			if (stringToIntern.Length == 0)
			{
				return string.Empty;
			}
			try
			{
				this.readWriterLock.EnterReadLock();
				string result;
				if (this.stringDictionary.TryGetValue(stringToIntern, out result))
				{
					return result;
				}
			}
			finally
			{
				try
				{
					this.readWriterLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return null;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004094 File Offset: 0x00002294
		private string AddStringToDictionary(string stringToIntern)
		{
			string result;
			try
			{
				this.readWriterLock.EnterWriteLock();
				string text;
				if (this.stringDictionary.TryGetValue(stringToIntern, out text))
				{
					result = text;
				}
				else
				{
					this.stringDictionary.Add(stringToIntern, stringToIntern);
					result = stringToIntern;
				}
			}
			finally
			{
				try
				{
					this.readWriterLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x0400005B RID: 91
		private Dictionary<string, string> stringDictionary;

		// Token: 0x0400005C RID: 92
		private ReaderWriterLockSlim readWriterLock = new ReaderWriterLockSlim();
	}
}
