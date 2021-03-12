using System;
using System.ServiceModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000314 RID: 788
	internal abstract class ServiceCommand<T> : WcfServiceCommandBase
	{
		// Token: 0x0600164B RID: 5707 RVA: 0x00073B36 File Offset: 0x00071D36
		protected ServiceCommand(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00073B40 File Offset: 0x00071D40
		internal T Execute()
		{
			base.PrepareBudgetAndActivityScope();
			T result;
			if (this.scope != null)
			{
				using (CpuTracker.StartCpuTracking("CMD"))
				{
					base.PreExecute();
					try
					{
						result = this.DetectDuplicatedCallOrExecute();
					}
					finally
					{
						base.PostExecute();
					}
					return result;
				}
			}
			result = this.DetectDuplicatedCallOrExecute();
			return result;
		}

		// Token: 0x0600164D RID: 5709
		protected abstract T InternalExecute();

		// Token: 0x0600164E RID: 5710 RVA: 0x00073BAC File Offset: 0x00071DAC
		private T DetectDuplicatedCallOrExecute()
		{
			T t;
			if (base.CallContext.TryGetResponse<T>(out t))
			{
				return t;
			}
			T result;
			try
			{
				t = this.InternalExecute();
				base.CallContext.SetResponse<T>(t, null);
				result = t;
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
			return result;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00073C44 File Offset: 0x00071E44
		public IdAndSession GetIdAndSession(BaseFolderId folder)
		{
			return new IdConverter(base.CallContext).ConvertFolderIdToIdAndSession(folder, IdConverter.ConvertOption.IgnoreChangeKey);
		}
	}
}
