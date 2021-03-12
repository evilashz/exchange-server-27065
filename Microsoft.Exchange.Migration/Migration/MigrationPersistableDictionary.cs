using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000054 RID: 84
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationPersistableDictionary
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0000F0E6 File Offset: 0x0000D2E6
		public MigrationPersistableDictionary()
		{
			this.propertyBag = new PersistableDictionary();
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000F0F9 File Offset: 0x0000D2F9
		protected PersistableDictionary PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000F101 File Offset: 0x0000D301
		public string Serialize()
		{
			return this.propertyBag.Serialize();
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000F10E File Offset: 0x0000D30E
		protected void DeserializeData(string serializedData)
		{
			this.propertyBag = PersistableDictionary.Create(serializedData);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000F11C File Offset: 0x0000D31C
		protected T Get<T>(string key)
		{
			return this.propertyBag.Get<T>(key);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000F12A File Offset: 0x0000D32A
		protected void Set<T>(string key, T value)
		{
			this.propertyBag.Set<T>(key, value);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000F13C File Offset: 0x0000D33C
		protected T? GetNullable<T>(string key) where T : struct
		{
			if (!this.propertyBag.Contains(key))
			{
				return null;
			}
			return this.propertyBag.Get<T?>(key);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000F16D File Offset: 0x0000D36D
		protected void SetNullable<T>(string key, T? value) where T : struct
		{
			if (value != null)
			{
				this.propertyBag.Set<T>(key, value.Value);
				return;
			}
			this.propertyBag.Remove(key);
		}

		// Token: 0x0400012A RID: 298
		private PersistableDictionary propertyBag;
	}
}
