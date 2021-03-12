using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000F1 RID: 241
	internal class ExecutionContext
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x00012058 File Offset: 0x00010258
		private ExecutionContext(DataContext[] contexts)
		{
			this.contexts = new List<DataContext>(contexts.Length);
			foreach (DataContext dataContext in contexts)
			{
				if (dataContext != null)
				{
					this.contexts.Add(dataContext);
				}
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001209C File Offset: 0x0001029C
		public static ExecutionContext Create(params DataContext[] contexts)
		{
			return new ExecutionContext(contexts);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000120A4 File Offset: 0x000102A4
		public static string GetDataContext(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (Exception ex = exception; ex != null; ex = ex.InnerException)
			{
				ExecutionContext.DataContextHolder contextHolder = ExecutionContext.GetContextHolder(ex, false);
				if (contextHolder != null && contextHolder.Contexts != null && contextHolder.Contexts.Count > 0)
				{
					foreach (string text in contextHolder.Contexts)
					{
						if (stringBuilder.Length == 0)
						{
							stringBuilder.Append(text);
						}
						else
						{
							stringBuilder.AppendFormat(Environment.NewLine + "{0}", text);
						}
					}
					return stringBuilder.ToString();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00012164 File Offset: 0x00010364
		public static ExceptionSide? GetExceptionSide(Exception exception)
		{
			for (Exception ex = exception; ex != null; ex = ex.InnerException)
			{
				ExecutionContext.DataContextHolder contextHolder = ExecutionContext.GetContextHolder(ex, false);
				if (contextHolder != null && contextHolder.Side != null)
				{
					return contextHolder.Side;
				}
			}
			return null;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000121AC File Offset: 0x000103AC
		public static OperationType GetOperationType(Exception exception)
		{
			for (Exception ex = exception; ex != null; ex = ex.InnerException)
			{
				ExecutionContext.DataContextHolder contextHolder = ExecutionContext.GetContextHolder(ex, false);
				if (contextHolder != null && contextHolder.OperationType != OperationType.None)
				{
					return contextHolder.OperationType;
				}
			}
			return OperationType.None;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000121E4 File Offset: 0x000103E4
		public static void SetExceptionSide(Exception exception, ExceptionSide? side)
		{
			ExecutionContext.DataContextHolder contextHolder = ExecutionContext.GetContextHolder(exception, true);
			if (contextHolder != null)
			{
				contextHolder.Side = side;
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00012204 File Offset: 0x00010404
		public static void StampCurrentDataContext(Exception exception)
		{
			ExecutionContext.DataContextHolder contextHolder = ExecutionContext.GetContextHolder(exception, true);
			if (contextHolder != null && ExecutionContext.currentContexts != null)
			{
				foreach (DataContext dataContext in ExecutionContext.currentContexts)
				{
					contextHolder.Contexts.Add(dataContext.ToString());
					if (contextHolder.Side == null)
					{
						OperationSideDataContext operationSideDataContext = dataContext as OperationSideDataContext;
						if (operationSideDataContext != null)
						{
							contextHolder.Side = new ExceptionSide?(operationSideDataContext.Side);
						}
					}
					if (contextHolder.OperationType == OperationType.None)
					{
						OperationDataContext operationDataContext = dataContext as OperationDataContext;
						if (operationDataContext != null && operationDataContext.OperationType != OperationType.None)
						{
							contextHolder.OperationType = operationDataContext.OperationType;
						}
					}
				}
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x000122CC File Offset: 0x000104CC
		public void Execute(Action operation)
		{
			if (ExecutionContext.currentContexts == null)
			{
				ExecutionContext.currentContexts = new Stack<DataContext>();
			}
			try
			{
				for (int i = this.contexts.Count - 1; i >= 0; i--)
				{
					ExecutionContext.currentContexts.Push(this.contexts[i]);
				}
				ExecutionContext.currentContexts.Push(SeparatorDataContext.Separator);
				operation();
			}
			catch (Exception exception)
			{
				if (ExecutionContext.GetContextHolder(exception, false) == null)
				{
					ExecutionContext.StampCurrentDataContext(exception);
				}
				throw;
			}
			finally
			{
				ExecutionContext.currentContexts.Pop();
				for (int j = 0; j < this.contexts.Count; j++)
				{
					ExecutionContext.currentContexts.Pop();
				}
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00012390 File Offset: 0x00010590
		private static ExecutionContext.DataContextHolder GetContextHolder(Exception exception, bool createIfMissing)
		{
			if (exception == null || exception.Data == null)
			{
				return null;
			}
			ExecutionContext.DataContextHolder dataContextHolder;
			if (exception.Data.Contains("Microsoft.Exchange.MailboxReplicationService.DataContext"))
			{
				dataContextHolder = (ExecutionContext.DataContextHolder)exception.Data["Microsoft.Exchange.MailboxReplicationService.DataContext"];
			}
			else if (createIfMissing)
			{
				dataContextHolder = new ExecutionContext.DataContextHolder();
				exception.Data["Microsoft.Exchange.MailboxReplicationService.DataContext"] = dataContextHolder;
			}
			else
			{
				dataContextHolder = null;
			}
			return dataContextHolder;
		}

		// Token: 0x04000550 RID: 1360
		private const string ExceptionDataKey = "Microsoft.Exchange.MailboxReplicationService.DataContext";

		// Token: 0x04000551 RID: 1361
		[ThreadStatic]
		private static Stack<DataContext> currentContexts;

		// Token: 0x04000552 RID: 1362
		private List<DataContext> contexts;

		// Token: 0x020000F2 RID: 242
		[Serializable]
		private class DataContextHolder
		{
			// Token: 0x06000915 RID: 2325 RVA: 0x000123F3 File Offset: 0x000105F3
			public DataContextHolder()
			{
				this.contexts = new List<string>();
			}

			// Token: 0x06000916 RID: 2326 RVA: 0x00012406 File Offset: 0x00010606
			protected DataContextHolder(SerializationInfo info, StreamingContext context)
			{
			}

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001240E File Offset: 0x0001060E
			public List<string> Contexts
			{
				get
				{
					return this.contexts;
				}
			}

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x06000918 RID: 2328 RVA: 0x00012416 File Offset: 0x00010616
			// (set) Token: 0x06000919 RID: 2329 RVA: 0x0001241E File Offset: 0x0001061E
			public ExceptionSide? Side { get; set; }

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x0600091A RID: 2330 RVA: 0x00012427 File Offset: 0x00010627
			// (set) Token: 0x0600091B RID: 2331 RVA: 0x0001242F File Offset: 0x0001062F
			public OperationType OperationType { get; set; }

			// Token: 0x04000553 RID: 1363
			private List<string> contexts;
		}
	}
}
