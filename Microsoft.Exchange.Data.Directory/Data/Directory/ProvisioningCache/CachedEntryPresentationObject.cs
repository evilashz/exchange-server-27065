using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x0200079F RID: 1951
	[Serializable]
	public class CachedEntryPresentationObject
	{
		// Token: 0x0600611E RID: 24862 RVA: 0x0014A151 File Offset: 0x00148351
		public CachedEntryPresentationObject(Guid key, string value) : this(key, Guid.Empty, value)
		{
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x0014A160 File Offset: 0x00148360
		public CachedEntryPresentationObject(Guid key, Guid orgId, string value)
		{
			this.CacheKey = key;
			this.OrganizationId = orgId;
			this.Value = value;
		}

		// Token: 0x170022BC RID: 8892
		// (get) Token: 0x06006120 RID: 24864 RVA: 0x0014A17D File Offset: 0x0014837D
		// (set) Token: 0x06006121 RID: 24865 RVA: 0x0014A185 File Offset: 0x00148385
		public Guid CacheKey
		{
			get
			{
				return this.cacheKey;
			}
			private set
			{
				this.cacheKey = value;
			}
		}

		// Token: 0x170022BD RID: 8893
		// (get) Token: 0x06006122 RID: 24866 RVA: 0x0014A18E File Offset: 0x0014838E
		// (set) Token: 0x06006123 RID: 24867 RVA: 0x0014A196 File Offset: 0x00148396
		public Guid OrganizationId
		{
			get
			{
				return this.organizationId;
			}
			private set
			{
				this.organizationId = value;
			}
		}

		// Token: 0x170022BE RID: 8894
		// (get) Token: 0x06006124 RID: 24868 RVA: 0x0014A19F File Offset: 0x0014839F
		// (set) Token: 0x06006125 RID: 24869 RVA: 0x0014A1A7 File Offset: 0x001483A7
		public string Value
		{
			get
			{
				return this.cacheValue;
			}
			private set
			{
				this.cacheValue = value;
			}
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x0014A1B0 File Offset: 0x001483B0
		public static CachedEntryPresentationObject TryFromReceivedData(byte[] buffer, int bufLen, out Exception ex)
		{
			ex = null;
			CachedEntryPresentationObject result = null;
			try
			{
				result = CachedEntryPresentationObject.FromReceivedData(buffer, bufLen);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			return result;
		}

		// Token: 0x06006127 RID: 24871 RVA: 0x0014A1E4 File Offset: 0x001483E4
		public static CachedEntryPresentationObject FromReceivedData(byte[] buffer, int bufLen)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (bufLen <= 0)
			{
				throw new ArgumentException("bufLen is less than zero.");
			}
			if (buffer.Length < bufLen)
			{
				throw new ArgumentException("The buffer is too small.");
			}
			string @string = Encoding.UTF8.GetString(buffer, 0, bufLen);
			string[] array = @string.Split(new char[]
			{
				'\n'
			});
			if (array.Length < 2)
			{
				throw new ArgumentException(string.Format("Invalid provisioning cache dump object received: {0}.", @string));
			}
			string value = null;
			if (array.Length == 3 && !string.IsNullOrWhiteSpace(array[2]))
			{
				value = array[2];
			}
			return new CachedEntryPresentationObject(new Guid(array[0]), new Guid(array[1]), value);
		}

		// Token: 0x0400411A RID: 16666
		private const char Delimer = '\n';

		// Token: 0x0400411B RID: 16667
		private Guid cacheKey;

		// Token: 0x0400411C RID: 16668
		private Guid organizationId;

		// Token: 0x0400411D RID: 16669
		private string cacheValue;
	}
}
