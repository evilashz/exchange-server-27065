using System;
using System.Security;
using System.Threading;

namespace System.Runtime.Versioning
{
	// Token: 0x020006F9 RID: 1785
	internal static class MultitargetingHelpers
	{
		// Token: 0x06005046 RID: 20550 RVA: 0x0011A5E4 File Offset: 0x001187E4
		internal static string GetAssemblyQualifiedName(Type type, Func<Type, string> converter)
		{
			string text = null;
			if (type != null)
			{
				if (converter != null)
				{
					try
					{
						text = converter(type);
					}
					catch (Exception ex)
					{
						if (MultitargetingHelpers.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
					}
				}
				if (text == null)
				{
					text = MultitargetingHelpers.defaultConverter(type);
				}
			}
			return text;
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x0011A638 File Offset: 0x00118838
		private static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x0011A66D File Offset: 0x0011886D
		private static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || MultitargetingHelpers.IsCriticalException(ex);
		}

		// Token: 0x04002373 RID: 9075
		private static Func<Type, string> defaultConverter = (Type t) => t.AssemblyQualifiedName;
	}
}
