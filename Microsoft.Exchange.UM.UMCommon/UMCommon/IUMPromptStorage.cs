using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000084 RID: 132
	internal interface IUMPromptStorage : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000481 RID: 1153
		void CreatePrompt(string promptName, string audioBytes);

		// Token: 0x06000482 RID: 1154
		string GetPrompt(string promptName);

		// Token: 0x06000483 RID: 1155
		string[] GetPromptNames();

		// Token: 0x06000484 RID: 1156
		string[] GetPromptNames(TimeSpan timeSinceLastModified);

		// Token: 0x06000485 RID: 1157
		void DeletePrompts(string[] prompts);

		// Token: 0x06000486 RID: 1158
		void DeleteAllPrompts();
	}
}
