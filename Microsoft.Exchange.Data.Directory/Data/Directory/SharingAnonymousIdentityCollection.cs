using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200019C RID: 412
	[Serializable]
	internal sealed class SharingAnonymousIdentityCollection : IEquatable<SharingAnonymousIdentityCollection>
	{
		// Token: 0x0600118C RID: 4492 RVA: 0x000556E5 File Offset: 0x000538E5
		internal SharingAnonymousIdentityCollection(MultiValuedProperty<string> sharingAnonymousIdentities)
		{
			if (sharingAnonymousIdentities == null)
			{
				throw new ArgumentNullException("mvp");
			}
			this.sharingAnonymousIdentities = sharingAnonymousIdentities;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00055702 File Offset: 0x00053902
		public bool Changed
		{
			get
			{
				return this.sharingAnonymousIdentities.Changed;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x0005570F File Offset: 0x0005390F
		public int Count
		{
			get
			{
				return this.sharingAnonymousIdentities.Count;
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0005571C File Offset: 0x0005391C
		public bool Equals(SharingAnonymousIdentityCollection other)
		{
			return other != null && this.sharingAnonymousIdentities.Equals(other.sharingAnonymousIdentities);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00055734 File Offset: 0x00053934
		public void Clear()
		{
			this.sharingAnonymousIdentities.Clear();
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00055741 File Offset: 0x00053941
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SharingAnonymousIdentityCollection);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0005574F File Offset: 0x0005394F
		public override int GetHashCode()
		{
			return this.sharingAnonymousIdentities.GetHashCode();
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0005575C File Offset: 0x0005395C
		public bool Contains(string urlId)
		{
			return null != this.Find(urlId);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0005576C File Offset: 0x0005396C
		public void AddOrUpdate(string urlIdPrefix, string urlId, string folderId)
		{
			string text = this.FindByFolder(urlIdPrefix, folderId);
			if (!string.IsNullOrEmpty(text))
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(urlId, this.ParseIdentity(text).Id))
				{
					return;
				}
				this.sharingAnonymousIdentities.Remove(text);
			}
			this.sharingAnonymousIdentities.Add(this.FormatIdentity(urlId, folderId));
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000557C4 File Offset: 0x000539C4
		public string GetFolder(string urlIdentity)
		{
			string text = this.Find(urlIdentity);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return this.ParseIdentity(text).Folder;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000557F0 File Offset: 0x000539F0
		public string FindExistingUrlId(string urlIdPrefix, string folderId)
		{
			string text = this.FindByFolder(urlIdPrefix, folderId);
			if (!string.IsNullOrEmpty(text))
			{
				return this.ParseIdentity(text).Id;
			}
			return null;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0005581C File Offset: 0x00053A1C
		public bool Remove(string urlIdentity)
		{
			string text = this.Find(urlIdentity);
			return !string.IsNullOrEmpty(text) && this.sharingAnonymousIdentities.Remove(text);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00055847 File Offset: 0x00053A47
		internal ReadOnlyCollection<string> GetRawSharingAnonymousIdentities()
		{
			return new ReadOnlyCollection<string>(this.sharingAnonymousIdentities);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0005586C File Offset: 0x00053A6C
		private string Find(string urlIdentity)
		{
			string stringToStart = urlIdentity + ":";
			return this.sharingAnonymousIdentities.Find((string identity) => identity.StartsWith(stringToStart, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000558D0 File Offset: 0x00053AD0
		private string FindByFolder(string urlIdprefix, string folderId)
		{
			string stringToEnd = ":" + folderId;
			return this.sharingAnonymousIdentities.Find((string identity) => identity.StartsWith(urlIdprefix, StringComparison.OrdinalIgnoreCase) && identity.EndsWith(stringToEnd, StringComparison.Ordinal));
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00055912 File Offset: 0x00053B12
		private string FormatIdentity(string urlId, string folderId)
		{
			return urlId + ":" + folderId;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00055920 File Offset: 0x00053B20
		private SharingAnonymousIdentityCollection.IdAndFolder ParseIdentity(string identity)
		{
			int num = identity.IndexOf(":");
			if (num == -1)
			{
				return default(SharingAnonymousIdentityCollection.IdAndFolder);
			}
			return new SharingAnonymousIdentityCollection.IdAndFolder
			{
				Id = identity.Substring(0, num),
				Folder = identity.Substring(num + 1)
			};
		}

		// Token: 0x04000A30 RID: 2608
		private const string Delimiter = ":";

		// Token: 0x04000A31 RID: 2609
		private readonly MultiValuedProperty<string> sharingAnonymousIdentities;

		// Token: 0x0200019D RID: 413
		private struct IdAndFolder
		{
			// Token: 0x04000A32 RID: 2610
			public string Id;

			// Token: 0x04000A33 RID: 2611
			public string Folder;
		}
	}
}
