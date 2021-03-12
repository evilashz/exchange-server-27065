using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000069 RID: 105
	public class SecureNameValueCollection : DisposeTrackableBase, IEnumerable<string>, IEnumerable
	{
		// Token: 0x06000339 RID: 825 RVA: 0x00013B10 File Offset: 0x00011D10
		public SecureNameValueCollection()
		{
			this.names = new List<string>();
			this.unsecuredValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.secureValues = new Dictionary<string, SecureString>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00013B44 File Offset: 0x00011D44
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

		// Token: 0x0600033B RID: 827 RVA: 0x00013BBC File Offset: 0x00011DBC
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

		// Token: 0x0600033C RID: 828 RVA: 0x00013C31 File Offset: 0x00011E31
		public IEnumerator<string> GetEnumerator()
		{
			return this.names.GetEnumerator();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00013C43 File Offset: 0x00011E43
		public bool TryGetSecureValue(string name, out SecureString value)
		{
			value = null;
			return this.secureValues.TryGetValue(name, out value);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00013C5A File Offset: 0x00011E5A
		public bool TryGetUnsecureValue(string name, out string value)
		{
			value = null;
			return this.unsecuredValues.TryGetValue(name, out value);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00013C71 File Offset: 0x00011E71
		public bool ContainsUnsecureValue(string name)
		{
			return this.unsecuredValues.ContainsKey(name);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00013C7F File Offset: 0x00011E7F
		public bool ContainsSecureValue(string name)
		{
			return this.secureValues.ContainsKey(name);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00013C8D File Offset: 0x00011E8D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00013C98 File Offset: 0x00011E98
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

		// Token: 0x06000343 RID: 835 RVA: 0x00013D10 File Offset: 0x00011F10
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SecureNameValueCollection>(this);
		}

		// Token: 0x04000233 RID: 563
		private List<string> names;

		// Token: 0x04000234 RID: 564
		private Dictionary<string, string> unsecuredValues;

		// Token: 0x04000235 RID: 565
		private Dictionary<string, SecureString> secureValues;

		// Token: 0x04000236 RID: 566
		private bool isDisposed;
	}
}
