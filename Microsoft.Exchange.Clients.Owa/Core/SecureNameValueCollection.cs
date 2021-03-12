using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000244 RID: 580
	public class SecureNameValueCollection : DisposeTrackableBase, IEnumerable<string>, IEnumerable
	{
		// Token: 0x06001376 RID: 4982 RVA: 0x000782CC File Offset: 0x000764CC
		public SecureNameValueCollection()
		{
			this.names = new List<string>();
			this.unsecuredValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.secureValues = new Dictionary<string, SecureString>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00078300 File Offset: 0x00076500
		public void AddUnsecureNameValue(string name, string value)
		{
			if (name == null || value == null)
			{
				throw new ArgumentException("name or value is null");
			}
			if (this.secureValues.ContainsKey(name))
			{
				throw new InvalidOperationException("Name was already added as secure pair. Name value:" + name);
			}
			if (this.unsecuredValues.ContainsKey(name))
			{
				throw new ArgumentException("Name is already in the collection. Name:" + name);
			}
			this.names.Add(name);
			this.unsecuredValues.Add(name, value);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00078378 File Offset: 0x00076578
		public void AddSecureNameValue(string name, SecureString value)
		{
			if (name == null || value == null)
			{
				throw new ArgumentException("name or value is null");
			}
			if (this.unsecuredValues.ContainsKey(name))
			{
				throw new InvalidOperationException("Name was already added unsecure pair. Name value:" + name);
			}
			if (this.secureValues.ContainsKey(name))
			{
				throw new ArgumentException("Name is already in the collection. Name:" + name);
			}
			this.names.Add(name);
			this.secureValues.Add(name, value);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x000783ED File Offset: 0x000765ED
		public IEnumerator<string> GetEnumerator()
		{
			return this.names.GetEnumerator();
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x000783FF File Offset: 0x000765FF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00078407 File Offset: 0x00076607
		public bool TryGetSecureValue(string name, out SecureString value)
		{
			value = null;
			return this.secureValues.TryGetValue(name, out value);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0007841E File Offset: 0x0007661E
		public bool TryGetUnsecureValue(string name, out string value)
		{
			value = null;
			return this.unsecuredValues.TryGetValue(name, out value);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00078435 File Offset: 0x00076635
		public bool ContainsUnsecureValue(string name)
		{
			return this.unsecuredValues.ContainsKey(name);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00078443 File Offset: 0x00076643
		public bool ContainsSecureValue(string name)
		{
			return this.secureValues.ContainsKey(name);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00078454 File Offset: 0x00076654
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && !this.isDisposed)
			{
				foreach (SecureString secureString in this.secureValues.Values)
				{
					secureString.Dispose();
				}
				this.unsecuredValues = null;
				this.secureValues = null;
				this.isDisposed = true;
			}
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000784CC File Offset: 0x000766CC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SecureNameValueCollection>(this);
		}

		// Token: 0x04000D73 RID: 3443
		private List<string> names;

		// Token: 0x04000D74 RID: 3444
		private Dictionary<string, string> unsecuredValues;

		// Token: 0x04000D75 RID: 3445
		private Dictionary<string, SecureString> secureValues;

		// Token: 0x04000D76 RID: 3446
		private bool isDisposed;
	}
}
