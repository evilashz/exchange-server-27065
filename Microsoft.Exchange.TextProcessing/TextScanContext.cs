using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000043 RID: 67
	public class TextScanContext
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		public TextScanContext(string data)
		{
			this.data = data;
			this.fingerprint = null;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000EC0A File Offset: 0x0000CE0A
		public TextScanContext(Stream stream) : this(TextScanContext.ReadWholeStream(stream))
		{
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000EC18 File Offset: 0x0000CE18
		public bool IsNull
		{
			get
			{
				return this.data == null;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000EC23 File Offset: 0x0000CE23
		public string Data
		{
			get
			{
				return this.data ?? string.Empty;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public string NormalizedData
		{
			get
			{
				string result;
				if ((result = this.normalizedData) == null)
				{
					result = (this.normalizedData = this.Data.ToUpperInvariant());
				}
				return result;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000EC60 File Offset: 0x0000CE60
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000ECAD File Offset: 0x0000CEAD
		internal LshFingerprint Fingerprint
		{
			get
			{
				if (this.fingerprint == null)
				{
					LshFingerprint lshFingerprint;
					if (LshFingerprintGenerator.TryGetFingerprint(this.Data, out lshFingerprint, ""))
					{
						this.fingerprint = lshFingerprint;
					}
					else
					{
						LshFingerprint.TryCreateFingerprint(null, out lshFingerprint, "");
						this.fingerprint = lshFingerprint;
					}
				}
				return this.fingerprint;
			}
			set
			{
				this.fingerprint = value;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000ECB6 File Offset: 0x0000CEB6
		internal void SetTrieScanComplete(long trieID)
		{
			if (this.scannedTrieIDs == null)
			{
				this.scannedTrieIDs = new Dictionary<long, bool>(8);
			}
			this.scannedTrieIDs[trieID] = true;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000ECD9 File Offset: 0x0000CED9
		internal bool IsTrieScanComplete(long trieID)
		{
			return this.scannedTrieIDs != null && this.scannedTrieIDs.ContainsKey(trieID);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000ECF1 File Offset: 0x0000CEF1
		internal void AddMatchedTermSetID(long id)
		{
			if (this.detectedTermIds == null)
			{
				this.detectedTermIds = new Dictionary<long, bool>(256);
			}
			this.detectedTermIds[id] = true;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000ED18 File Offset: 0x0000CF18
		internal bool IsMatchedTermSet(long id)
		{
			return this.detectedTermIds != null && this.detectedTermIds.ContainsKey(id);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000ED30 File Offset: 0x0000CF30
		internal bool TryGetCachedResult(long id, out bool result)
		{
			result = false;
			return this.cachedResult != null && this.cachedResult.TryGetValue(id, out result);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000ED4C File Offset: 0x0000CF4C
		internal void SetCachedResult(long id, bool result)
		{
			if (this.cachedResult == null)
			{
				this.cachedResult = new Dictionary<long, bool>();
			}
			this.cachedResult[id] = result;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000ED70 File Offset: 0x0000CF70
		private static string ReadWholeStream(Stream stream)
		{
			string result;
			using (StreamReader streamReader = new StreamReader(stream))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x04000161 RID: 353
		private readonly string data;

		// Token: 0x04000162 RID: 354
		private string normalizedData;

		// Token: 0x04000163 RID: 355
		private LshFingerprint fingerprint;

		// Token: 0x04000164 RID: 356
		private Dictionary<long, bool> scannedTrieIDs;

		// Token: 0x04000165 RID: 357
		private Dictionary<long, bool> detectedTermIds;

		// Token: 0x04000166 RID: 358
		private Dictionary<long, bool> cachedResult;
	}
}
