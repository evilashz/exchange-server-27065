using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000533 RID: 1331
	internal class TransientErrorCache
	{
		// Token: 0x06002FB5 RID: 12213 RVA: 0x000C0FAC File Offset: 0x000BF1AC
		private TransientErrorCache()
		{
			this.errors = new Dictionary<string, CASServiceError>();
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000C0FCC File Offset: 0x000BF1CC
		public void Add(CASServiceError error)
		{
			if (error == null)
			{
				throw new ArgumentNullException("error");
			}
			lock (this.lockObject)
			{
				if (!this.errors.ContainsKey(error.GetKey()))
				{
					this.errors.Add(error.GetKey(), error);
				}
			}
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000C103C File Offset: 0x000BF23C
		public bool ContainsError(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentException("key");
			}
			key = key.ToLowerInvariant();
			bool result;
			lock (this.lockObject)
			{
				result = this.errors.ContainsKey(key);
			}
			return result;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000C10A0 File Offset: 0x000BF2A0
		public bool ContainsError(string key1, string key2)
		{
			if (string.IsNullOrEmpty(key1) || string.IsNullOrEmpty(key2))
			{
				throw new ArgumentException("key");
			}
			key1 = key1.ToLowerInvariant();
			key2 = key2.ToLowerInvariant();
			bool result;
			lock (this.lockObject)
			{
				result = this.errors.ContainsKey(key1 + "_" + key2);
			}
			return result;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000C1120 File Offset: 0x000BF320
		internal void Remove(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentException("key");
			}
			key = key.ToLowerInvariant();
			lock (this.lockObject)
			{
				if (this.errors.ContainsKey(key))
				{
					this.errors.Remove(key);
				}
			}
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000C1190 File Offset: 0x000BF390
		internal void Remove(string key1, string key2)
		{
			if (string.IsNullOrEmpty(key1) || string.IsNullOrEmpty(key2))
			{
				throw new ArgumentException("key");
			}
			key1 = key1.ToLowerInvariant();
			key2 = key2.ToLowerInvariant();
			string key3 = key1 + "_" + key2;
			lock (this.lockObject)
			{
				if (this.errors.ContainsKey(key3))
				{
					this.errors.Remove(key3);
				}
			}
		}

		// Token: 0x04002211 RID: 8721
		internal static TransientErrorCache EWSTransientCache = new TransientErrorCache();

		// Token: 0x04002212 RID: 8722
		internal static TransientErrorCache EASTransientCache = new TransientErrorCache();

		// Token: 0x04002213 RID: 8723
		internal static TransientErrorCache OWAInternalTransientCache = new TransientErrorCache();

		// Token: 0x04002214 RID: 8724
		internal static TransientErrorCache OWAExternalTransientCache = new TransientErrorCache();

		// Token: 0x04002215 RID: 8725
		internal static TransientErrorCache EcpInternalTransientCache = new TransientErrorCache();

		// Token: 0x04002216 RID: 8726
		internal static TransientErrorCache EcpExternalTransientCache = new TransientErrorCache();

		// Token: 0x04002217 RID: 8727
		internal static TransientErrorCache POPTransientErrorCache = new TransientErrorCache();

		// Token: 0x04002218 RID: 8728
		internal static TransientErrorCache IMAPTransientErrorCache = new TransientErrorCache();

		// Token: 0x04002219 RID: 8729
		internal static TransientErrorCache PowerShellTransientErrorCache = new TransientErrorCache();

		// Token: 0x0400221A RID: 8730
		internal static TransientErrorCache CalendarTransientErrorCache = new TransientErrorCache();

		// Token: 0x0400221B RID: 8731
		private object lockObject = new object();

		// Token: 0x0400221C RID: 8732
		private Dictionary<string, CASServiceError> errors;
	}
}
