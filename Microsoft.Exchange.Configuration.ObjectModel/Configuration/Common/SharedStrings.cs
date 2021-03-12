using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Common
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SharedStrings
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000411B File Offset: 0x0000231B
		public static LocalizedString GenericConditionFailure
		{
			get
			{
				return Strings.GenericConditionFailure;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004122 File Offset: 0x00002322
		public static LocalizedString ExceptionSetupFileNotFound(string fileName)
		{
			return Strings.ExceptionSetupFileNotFound(fileName);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000412A File Offset: 0x0000232A
		public static LocalizedString LogCheckpoint(object checkPoint)
		{
			return Strings.LogCheckpoint(checkPoint);
		}
	}
}
