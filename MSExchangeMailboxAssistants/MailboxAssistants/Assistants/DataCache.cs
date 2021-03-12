using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000014 RID: 20
	internal sealed class DataCache<KeyType, ResourceType>
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00004EC4 File Offset: 0x000030C4
		public DataCache(DataCache<KeyType, ResourceType>.CreateResource resourceCreator) : this(resourceCreator, false)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004ECE File Offset: 0x000030CE
		public DataCache(DataCache<KeyType, ResourceType>.CreateResource resourceCreator, bool doReverseMapping)
		{
			this.resourceCreator = resourceCreator;
			if (doReverseMapping)
			{
				this.reverseDictionary = new Dictionary<ResourceType, KeyType>(25);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004F05 File Offset: 0x00003105
		internal KeyType GetKey(ResourceType resource)
		{
			return this.reverseDictionary[resource];
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004F14 File Offset: 0x00003114
		internal ResourceType GetResource(KeyType key, object mapper)
		{
			ResourceType resourceType = default(ResourceType);
			bool flag = false;
			bool flag2 = false;
			try
			{
				this.readerWriterLock.AcquireReaderLock(-1);
				flag = true;
				if (this.resources.TryGetValue(key, out resourceType))
				{
					this.readerWriterLock.ReleaseReaderLock();
					flag = false;
				}
				else
				{
					this.readerWriterLock.ReleaseReaderLock();
					flag = false;
					this.readerWriterLock.AcquireWriterLock(-1);
					flag2 = true;
					if (!this.resources.TryGetValue(key, out resourceType))
					{
						resourceType = this.resourceCreator(mapper);
						this.resources[key] = resourceType;
						if (this.reverseDictionary != null)
						{
							this.reverseDictionary.Add(resourceType, key);
						}
					}
					this.readerWriterLock.ReleaseWriterLock();
					flag2 = false;
				}
			}
			finally
			{
				if (flag)
				{
					this.readerWriterLock.ReleaseReaderLock();
				}
				else if (flag2)
				{
					this.readerWriterLock.ReleaseWriterLock();
				}
			}
			return resourceType;
		}

		// Token: 0x040000E8 RID: 232
		private DataCache<KeyType, ResourceType>.CreateResource resourceCreator;

		// Token: 0x040000E9 RID: 233
		private Dictionary<KeyType, ResourceType> resources = new Dictionary<KeyType, ResourceType>(25);

		// Token: 0x040000EA RID: 234
		private Dictionary<ResourceType, KeyType> reverseDictionary;

		// Token: 0x040000EB RID: 235
		private ReaderWriterLock readerWriterLock = new ReaderWriterLock();

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x060000BB RID: 187
		internal delegate ResourceType CreateResource(object obj);
	}
}
