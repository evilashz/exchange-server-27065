using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x020000E7 RID: 231
	internal class CmdletRunner
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x0001C878 File Offset: 0x0001AA78
		internal static IEnumerable<PSObject> RunCmdlets(IEnumerable<string> cmdlets)
		{
			PSLanguageMode languageMode = Runspace.DefaultRunspace.SessionStateProxy.LanguageMode;
			if (languageMode != PSLanguageMode.RestrictedLanguage)
			{
				Runspace.DefaultRunspace.SessionStateProxy.LanguageMode = PSLanguageMode.RestrictedLanguage;
			}
			List<PSObject> list = new List<PSObject>();
			try
			{
				foreach (string text in cmdlets)
				{
					using (Pipeline pipeline = Runspace.DefaultRunspace.CreateNestedPipeline())
					{
						pipeline.Commands.AddScript(text);
						IEnumerable<PSObject> collection = pipeline.Invoke();
						list.AddRange(collection);
						IEnumerable<object> enumerable = pipeline.Error.ReadToEnd();
						if (enumerable.Any<object>())
						{
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.AppendLine(text);
							foreach (object obj in enumerable)
							{
								stringBuilder.AppendLine(obj.ToString());
							}
							throw new CmdletExecutionException(stringBuilder.ToString());
						}
					}
				}
			}
			finally
			{
				Runspace.DefaultRunspace.SessionStateProxy.LanguageMode = languageMode;
			}
			return list;
		}
	}
}
