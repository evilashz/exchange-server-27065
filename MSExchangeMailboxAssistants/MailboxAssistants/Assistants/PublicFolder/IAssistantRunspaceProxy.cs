using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000164 RID: 356
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAssistantRunspaceProxy : IDisposable
	{
		// Token: 0x06000E6D RID: 3693
		T RunPSCommand<T>(PSCommand command, out ErrorRecord error, IPublicFolderMailboxLoggerBase logger) where T : class;

		// Token: 0x06000E6E RID: 3694
		Collection<PSObject> RunPowershellScript(string scriptFile, Dictionary<string, string> scriptParameters, out Collection<ErrorRecord> errors, IPublicFolderMailboxLoggerBase logger);
	}
}
