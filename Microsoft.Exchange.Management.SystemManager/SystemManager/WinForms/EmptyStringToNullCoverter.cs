using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000180 RID: 384
	internal class EmptyStringToNullCoverter : TextConverter
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x0003AF27 File Offset: 0x00039127
		protected override object ParseObject(string s, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(s))
			{
				return null;
			}
			return base.ParseObject(s, provider);
		}
	}
}
