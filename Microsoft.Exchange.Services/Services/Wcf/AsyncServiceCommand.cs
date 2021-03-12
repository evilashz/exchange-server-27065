using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B69 RID: 2921
	internal abstract class AsyncServiceCommand<T> : WcfServiceCommandBase
	{
		// Token: 0x060052BB RID: 21179 RVA: 0x0010B929 File Offset: 0x00109B29
		protected AsyncServiceCommand(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x0010BB04 File Offset: 0x00109D04
		internal async Task<T> Execute()
		{
			base.PrepareBudgetAndActivityScope();
			T result;
			if (this.scope != null)
			{
				base.PreExecute();
				try
				{
					result = await this.DetectDuplicatedCallOrExecute();
					goto IL_149;
				}
				finally
				{
					base.PostExecute();
				}
			}
			result = await this.DetectDuplicatedCallOrExecute();
			IL_149:
			return result;
		}

		// Token: 0x060052BD RID: 21181
		protected abstract Task<T> InternalExecute();

		// Token: 0x060052BE RID: 21182 RVA: 0x0010BD2C File Offset: 0x00109F2C
		private async Task<T> DetectDuplicatedCallOrExecute()
		{
			T results;
			T result;
			if (base.CallContext.TryGetResponse<T>(out results))
			{
				result = results;
			}
			else
			{
				try
				{
					results = await this.InternalExecute();
					base.CallContext.SetResponse<T>(results, null);
					result = results;
				}
				catch (FaultException exception)
				{
					base.CallContext.SetResponse<T>(default(T), exception);
					throw;
				}
				catch (LocalizedException exception2)
				{
					base.CallContext.SetResponse<T>(default(T), exception2);
					throw;
				}
				finally
				{
					base.LogRequestTraces();
				}
			}
			return result;
		}
	}
}
