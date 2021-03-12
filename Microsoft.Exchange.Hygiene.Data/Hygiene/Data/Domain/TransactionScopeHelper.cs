using System;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x02000132 RID: 306
	internal static class TransactionScopeHelper
	{
		// Token: 0x06000BE4 RID: 3044 RVA: 0x00025C8D File Offset: 0x00023E8D
		public static void Run(this DomainSession session, bool useTransaction, string transactionIdentifier, Action action)
		{
			if (useTransaction)
			{
				TransactionScopeHelper.RunWithTransaction(session, transactionIdentifier, action);
				return;
			}
			TransactionScopeHelper.RunWithoutTransaction(session, action);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00025CA4 File Offset: 0x00023EA4
		private static void RunWithTransaction(DomainSession session, string transactionIdentifier, Action action)
		{
			session.TraceDebug("Running with application transaction:{0}", new object[]
			{
				transactionIdentifier
			});
			if (!(session.DefaultDataProvider is ITransactionProvider))
			{
				session.TraceError("Default data provider:{0} for Domain session does not implement ITransactionProvider", new object[]
				{
					session.DefaultDataProvider
				});
				throw new InvalidOperationException(string.Format(HygieneDataStrings.ErrorTransactionNotSupported, session.DefaultDataProvider.GetType().Name, session.GetType().Name));
			}
			bool flag = false;
			try
			{
				ITransactionProvider transactionProvider = session.DefaultDataProvider as ITransactionProvider;
				transactionProvider.InvokeWithTransaction(action, transactionIdentifier);
				flag = true;
			}
			finally
			{
				if (flag)
				{
					session.TraceDebug("Application transaction completed:{0}", new object[]
					{
						transactionIdentifier
					});
				}
				else
				{
					session.TraceWarning("Application transaction failed:{0}", new object[]
					{
						transactionIdentifier
					});
				}
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00025D88 File Offset: 0x00023F88
		private static void RunWithoutTransaction(DomainSession session, Action action)
		{
			session.TraceDebug("Running without application transaction");
			action();
		}
	}
}
