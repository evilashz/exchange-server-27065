using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Diagnostics;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000072 RID: 114
	public class SimpleQueryTargets
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00011D18 File Offset: 0x0000FF18
		public static void Initialize()
		{
			SimpleQueryTargets.Instance.Register<QueryableLogTransactionInformation>(new SimpleQueryTargets.SingleRowSimpleQueryTarget<QueryableLogTransactionInformation>("ParseCommitCtx", new Type[]
			{
				typeof(string)
			}, delegate(object[] parameters)
			{
				string text = parameters[0] as string;
				if (!string.IsNullOrEmpty(text))
				{
					byte[] buffer;
					try
					{
						buffer = HexConverter.HexStringToByteArray(text.Replace(" ", string.Empty));
					}
					catch (FormatException ex)
					{
						NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
						throw new DiagnosticQueryException(ex.Message);
					}
					LogTransactionInformationParser logTransactionInformationParser = new LogTransactionInformationParser();
					logTransactionInformationParser.Parse(buffer);
					return new QueryableLogTransactionInformation(logTransactionInformationParser.LogTransactionInformationList);
				}
				throw new DiagnosticQueryException(DiagnosticQueryStrings.InvalidCommitCtxFormat());
			}), Visibility.Public);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00011D6C File Offset: 0x0000FF6C
		public static void MountEventHandler(StoreDatabase database)
		{
		}
	}
}
