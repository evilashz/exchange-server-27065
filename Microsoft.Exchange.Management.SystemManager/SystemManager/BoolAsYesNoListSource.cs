using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000037 RID: 55
	public class BoolAsYesNoListSource : ObjectListSource
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00008910 File Offset: 0x00006B10
		public BoolAsYesNoListSource()
		{
			bool[] array = new bool[2];
			array[0] = true;
			base..ctor(array);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008930 File Offset: 0x00006B30
		protected override string GetValueText(object objectValue)
		{
			if (!Convert.ToBoolean(objectValue))
			{
				return Strings.NoString.ToString();
			}
			return Strings.YesString.ToString();
		}
	}
}
