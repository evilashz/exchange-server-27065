using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200074F RID: 1871
	internal sealed class ObjectMapInfo
	{
		// Token: 0x06005293 RID: 21139 RVA: 0x00121E75 File Offset: 0x00120075
		internal ObjectMapInfo(int objectId, int numMembers, string[] memberNames, Type[] memberTypes)
		{
			this.objectId = objectId;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.memberTypes = memberTypes;
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x00121E9C File Offset: 0x0012009C
		internal bool isCompatible(int numMembers, string[] memberNames, Type[] memberTypes)
		{
			bool result = true;
			if (this.numMembers == numMembers)
			{
				for (int i = 0; i < numMembers; i++)
				{
					if (!this.memberNames[i].Equals(memberNames[i]))
					{
						result = false;
						break;
					}
					if (memberTypes != null && this.memberTypes[i] != memberTypes[i])
					{
						result = false;
						break;
					}
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040024F9 RID: 9465
		internal int objectId;

		// Token: 0x040024FA RID: 9466
		private int numMembers;

		// Token: 0x040024FB RID: 9467
		private string[] memberNames;

		// Token: 0x040024FC RID: 9468
		private Type[] memberTypes;
	}
}
