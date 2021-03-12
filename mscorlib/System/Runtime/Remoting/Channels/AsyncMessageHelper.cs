using System;
using System.Reflection;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000806 RID: 2054
	internal static class AsyncMessageHelper
	{
		// Token: 0x0600589F RID: 22687 RVA: 0x00137AA0 File Offset: 0x00135CA0
		internal static void GetOutArgs(ParameterInfo[] syncParams, object[] syncArgs, object[] endArgs)
		{
			int num = 0;
			for (int i = 0; i < syncParams.Length; i++)
			{
				if (syncParams[i].IsOut || syncParams[i].ParameterType.IsByRef)
				{
					endArgs[num++] = syncArgs[i];
				}
			}
		}
	}
}
