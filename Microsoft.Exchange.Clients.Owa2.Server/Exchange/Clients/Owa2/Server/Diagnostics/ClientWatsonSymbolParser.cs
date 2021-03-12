using System;
using System.IO;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000428 RID: 1064
	internal abstract class ClientWatsonSymbolParser<T> where T : IJavaScriptSymbol
	{
		// Token: 0x0600246A RID: 9322
		public abstract bool TryParseSymbolData(string value, ClientWatsonFunctionNamePool functionNamePool, out T javaScriptSymbol);

		// Token: 0x0600246B RID: 9323 RVA: 0x000834AC File Offset: 0x000816AC
		public virtual bool ExtractScriptPackageName(string symbolFilePath, string scriptFilePath, out string scriptFileName)
		{
			bool result;
			try
			{
				scriptFileName = Path.GetFileNameWithoutExtension(scriptFilePath);
				if (string.IsNullOrEmpty(scriptFileName))
				{
					result = false;
				}
				else
				{
					if (scriptFileName.EndsWith(".min"))
					{
						scriptFileName = scriptFileName.Substring(0, scriptFileName.Length - 4);
					}
					result = true;
				}
			}
			catch (ArgumentException)
			{
				scriptFileName = null;
				result = false;
			}
			return result;
		}
	}
}
