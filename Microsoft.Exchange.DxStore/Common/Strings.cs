using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200008F RID: 143
	internal static class Strings
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x00013A14 File Offset: 0x00011C14
		static Strings()
		{
			Strings.stringIDs.Add(2536273634U, "DxStoreInstanceStaleStore");
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00013A64 File Offset: 0x00011C64
		public static LocalizedString DxStoreServerException(string errMsg)
		{
			return new LocalizedString("DxStoreServerException", Strings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00013A8C File Offset: 0x00011C8C
		public static LocalizedString DxStoreClientTransientException(string errMsg)
		{
			return new LocalizedString("DxStoreClientTransientException", Strings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00013AB4 File Offset: 0x00011CB4
		public static LocalizedString DxStoreInstanceServerException(string errMsg2)
		{
			return new LocalizedString("DxStoreInstanceServerException", Strings.ResourceManager, new object[]
			{
				errMsg2
			});
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00013ADC File Offset: 0x00011CDC
		public static LocalizedString DxStoreServerTransientException(string errMsg)
		{
			return new LocalizedString("DxStoreServerTransientException", Strings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00013B04 File Offset: 0x00011D04
		public static LocalizedString DxStoreClientException(string errMsg)
		{
			return new LocalizedString("DxStoreClientException", Strings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00013B2C File Offset: 0x00011D2C
		public static LocalizedString DxStoreAccessClientTransientException(string errMsg1)
		{
			return new LocalizedString("DxStoreAccessClientTransientException", Strings.ResourceManager, new object[]
			{
				errMsg1
			});
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00013B54 File Offset: 0x00011D54
		public static LocalizedString DxStoreCommandConstraintFailed(string phase)
		{
			return new LocalizedString("DxStoreCommandConstraintFailed", Strings.ResourceManager, new object[]
			{
				phase
			});
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00013B7C File Offset: 0x00011D7C
		public static LocalizedString DxStoreInstanceNotReady(string currentState)
		{
			return new LocalizedString("DxStoreInstanceNotReady", Strings.ResourceManager, new object[]
			{
				currentState
			});
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00013BA4 File Offset: 0x00011DA4
		public static LocalizedString DxStoreAccessServerTransientException(string errMsg1)
		{
			return new LocalizedString("DxStoreAccessServerTransientException", Strings.ResourceManager, new object[]
			{
				errMsg1
			});
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00013BCC File Offset: 0x00011DCC
		public static LocalizedString DxStoreAccessClientException(string errMsg2)
		{
			return new LocalizedString("DxStoreAccessClientException", Strings.ResourceManager, new object[]
			{
				errMsg2
			});
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00013BF4 File Offset: 0x00011DF4
		public static LocalizedString DxStoreManagerServerTransientException(string errMsg5)
		{
			return new LocalizedString("DxStoreManagerServerTransientException", Strings.ResourceManager, new object[]
			{
				errMsg5
			});
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00013C1C File Offset: 0x00011E1C
		public static LocalizedString DxStoreInstanceClientException(string errMsg2)
		{
			return new LocalizedString("DxStoreInstanceClientException", Strings.ResourceManager, new object[]
			{
				errMsg2
			});
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00013C44 File Offset: 0x00011E44
		public static LocalizedString DxStoreBindingNotSupportedException(string bindingStr)
		{
			return new LocalizedString("DxStoreBindingNotSupportedException", Strings.ResourceManager, new object[]
			{
				bindingStr
			});
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00013C6C File Offset: 0x00011E6C
		public static LocalizedString DxStoreManagerClientException(string errMsg4)
		{
			return new LocalizedString("DxStoreManagerClientException", Strings.ResourceManager, new object[]
			{
				errMsg4
			});
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00013C94 File Offset: 0x00011E94
		public static LocalizedString DxStoreManagerGroupNotFoundException(string groupName)
		{
			return new LocalizedString("DxStoreManagerGroupNotFoundException", Strings.ResourceManager, new object[]
			{
				groupName
			});
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00013CBC File Offset: 0x00011EBC
		public static LocalizedString DxStoreInstanceComponentNotInitialized(string component)
		{
			return new LocalizedString("DxStoreInstanceComponentNotInitialized", Strings.ResourceManager, new object[]
			{
				component
			});
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00013CE4 File Offset: 0x00011EE4
		public static LocalizedString SerializeError(string err)
		{
			return new LocalizedString("SerializeError", Strings.ResourceManager, new object[]
			{
				err
			});
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00013D0C File Offset: 0x00011F0C
		public static LocalizedString DxStoreInstanceStaleStore
		{
			get
			{
				return new LocalizedString("DxStoreInstanceStaleStore", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00013D24 File Offset: 0x00011F24
		public static LocalizedString DxStoreInstanceKeyNotFound(string keyName)
		{
			return new LocalizedString("DxStoreInstanceKeyNotFound", Strings.ResourceManager, new object[]
			{
				keyName
			});
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00013D4C File Offset: 0x00011F4C
		public static LocalizedString DxStoreManagerServerException(string errMsg4)
		{
			return new LocalizedString("DxStoreManagerServerException", Strings.ResourceManager, new object[]
			{
				errMsg4
			});
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00013D74 File Offset: 0x00011F74
		public static LocalizedString DxStoreInstanceServerTransientException(string errMsg3)
		{
			return new LocalizedString("DxStoreInstanceServerTransientException", Strings.ResourceManager, new object[]
			{
				errMsg3
			});
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00013D9C File Offset: 0x00011F9C
		public static LocalizedString DxStoreManagerClientTransientException(string errMsg5)
		{
			return new LocalizedString("DxStoreManagerClientTransientException", Strings.ResourceManager, new object[]
			{
				errMsg5
			});
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00013DC4 File Offset: 0x00011FC4
		public static LocalizedString DxStoreInstanceClientTransientException(string errMsg3)
		{
			return new LocalizedString("DxStoreInstanceClientTransientException", Strings.ResourceManager, new object[]
			{
				errMsg3
			});
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00013DEC File Offset: 0x00011FEC
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000267 RID: 615
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x04000268 RID: 616
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.DxStore.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000090 RID: 144
		public enum IDs : uint
		{
			// Token: 0x0400026A RID: 618
			DxStoreInstanceStaleStore = 2536273634U
		}

		// Token: 0x02000091 RID: 145
		private enum ParamIDs
		{
			// Token: 0x0400026C RID: 620
			DxStoreServerException,
			// Token: 0x0400026D RID: 621
			DxStoreClientTransientException,
			// Token: 0x0400026E RID: 622
			DxStoreInstanceServerException,
			// Token: 0x0400026F RID: 623
			DxStoreServerTransientException,
			// Token: 0x04000270 RID: 624
			DxStoreClientException,
			// Token: 0x04000271 RID: 625
			DxStoreAccessClientTransientException,
			// Token: 0x04000272 RID: 626
			DxStoreCommandConstraintFailed,
			// Token: 0x04000273 RID: 627
			DxStoreInstanceNotReady,
			// Token: 0x04000274 RID: 628
			DxStoreAccessServerTransientException,
			// Token: 0x04000275 RID: 629
			DxStoreAccessClientException,
			// Token: 0x04000276 RID: 630
			DxStoreManagerServerTransientException,
			// Token: 0x04000277 RID: 631
			DxStoreInstanceClientException,
			// Token: 0x04000278 RID: 632
			DxStoreBindingNotSupportedException,
			// Token: 0x04000279 RID: 633
			DxStoreManagerClientException,
			// Token: 0x0400027A RID: 634
			DxStoreManagerGroupNotFoundException,
			// Token: 0x0400027B RID: 635
			DxStoreInstanceComponentNotInitialized,
			// Token: 0x0400027C RID: 636
			SerializeError,
			// Token: 0x0400027D RID: 637
			DxStoreInstanceKeyNotFound,
			// Token: 0x0400027E RID: 638
			DxStoreManagerServerException,
			// Token: 0x0400027F RID: 639
			DxStoreInstanceServerTransientException,
			// Token: 0x04000280 RID: 640
			DxStoreManagerClientTransientException,
			// Token: 0x04000281 RID: 641
			DxStoreInstanceClientTransientException
		}
	}
}
