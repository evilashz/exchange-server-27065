using System;

namespace System.Security
{
	// Token: 0x020001BC RID: 444
	internal interface ISecurityElementFactory
	{
		// Token: 0x06001BCD RID: 7117
		SecurityElement CreateSecurityElement();

		// Token: 0x06001BCE RID: 7118
		object Copy();

		// Token: 0x06001BCF RID: 7119
		string GetTag();

		// Token: 0x06001BD0 RID: 7120
		string Attribute(string attributeName);
	}
}
