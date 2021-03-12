using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200001E RID: 30
	internal abstract class AsrSearchResult
	{
		// Token: 0x06000189 RID: 393 RVA: 0x000077F0 File Offset: 0x000059F0
		internal static AsrSearchResult Create(IUMRecognitionPhrase result, UMSubscriber subscriber, Guid tenantGuid)
		{
			string text = (string)result["ResultType"];
			string a;
			if ((a = text) != null)
			{
				if (a == "DirectoryContact")
				{
					return new AsrDirectorySearchResult(result, tenantGuid);
				}
				if (a == "Department")
				{
					return new AsrDepartmentSearchResult(result);
				}
				if (a == "PersonalContact")
				{
					return new AsrPersonalContactsSearchResult(result, subscriber);
				}
			}
			throw new InvalidResultTypeException(text);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000785B File Offset: 0x00005A5B
		internal static AsrSearchResult Create(string result)
		{
			return new AsrExtensionSearchResult(result);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007863 File Offset: 0x00005A63
		internal static AsrSearchResult Create(CustomMenuKeyMapping result)
		{
			return new AsrDepartmentSearchResult(result);
		}

		// Token: 0x0600018C RID: 396
		internal abstract void SetManagerVariables(ActivityManager manager, BaseUMCallSession vo);
	}
}
