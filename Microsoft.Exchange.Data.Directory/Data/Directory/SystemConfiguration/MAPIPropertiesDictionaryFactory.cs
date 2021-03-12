using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002ED RID: 749
	internal static class MAPIPropertiesDictionaryFactory
	{
		// Token: 0x060022FA RID: 8954 RVA: 0x00098188 File Offset: 0x00096388
		internal static MAPIPropertiesDictionary GetPropertiesDictionary()
		{
			if (MAPIPropertiesDictionaryFactory.propertiesDictionary == null)
			{
				lock (MAPIPropertiesDictionaryFactory.objLock)
				{
					if (MAPIPropertiesDictionaryFactory.propertiesDictionary == null)
					{
						MAPIPropertiesDictionaryFactory.propertiesDictionary = new MAPIPropertiesDictionary();
					}
				}
			}
			return MAPIPropertiesDictionaryFactory.propertiesDictionary;
		}

		// Token: 0x040015C1 RID: 5569
		private static object objLock = new object();

		// Token: 0x040015C2 RID: 5570
		private static volatile MAPIPropertiesDictionary propertiesDictionary;
	}
}
