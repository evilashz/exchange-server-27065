using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000036 RID: 54
	public class BoolListSource : ObjectListSource
	{
		// Token: 0x0600021D RID: 541 RVA: 0x000088D4 File Offset: 0x00006AD4
		public BoolListSource()
		{
			bool[] array = new bool[2];
			array[0] = true;
			base..ctor(array);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000088F3 File Offset: 0x00006AF3
		protected override string GetValueText(object objectValue)
		{
			return Convert.ToBoolean(objectValue) ? Strings.TrueString : Strings.FalseString;
		}
	}
}
