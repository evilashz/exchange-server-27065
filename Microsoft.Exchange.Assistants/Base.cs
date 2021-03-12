using System;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000015 RID: 21
	internal class Base
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004139 File Offset: 0x00002339
		internal EventLogger Logger
		{
			get
			{
				return SingletonEventLogger.Logger;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004140 File Offset: 0x00002340
		public void CatchMeIfYouCan(CatchMe function, string nonLocalizedAssistantName)
		{
			try
			{
				Util.CatchMeIfYouCan(function, nonLocalizedAssistantName);
			}
			catch (AIException ex)
			{
				ExTraceGlobals.PFDTracer.TraceError<Base, AIException>((long)this.GetHashCode(), "{0}: Exception thrown: {1}", this, ex);
				this.LogEvent(AssistantsEventLogConstants.Tuple_GenericException, null, new object[]
				{
					ex.ToString()
				});
				if (ex is AIGrayException)
				{
					this.LogEvent(AssistantsEventLogConstants.Tuple_GrayException, ex.ToString(), new object[]
					{
						ex.ToString()
					});
				}
				throw;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000041C8 File Offset: 0x000023C8
		public virtual void ExportToQueryableObject(QueryableObject queryableObject)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000041CA File Offset: 0x000023CA
		internal void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			this.Logger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000041DA File Offset: 0x000023DA
		internal void TracePfd(string format, params object[] args)
		{
			ExTraceGlobals.PFDTracer.TracePfd((long)this.GetHashCode(), format, args);
		}
	}
}
