using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.Analysis.Features;

namespace Microsoft.Exchange.Management.Analysis.Builders
{
	// Token: 0x02000054 RID: 84
	internal sealed class SettingBuilder<T, TParent> : ISettingFeatureBuilder, IFeatureBuilder
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00007F72 File Offset: 0x00006172
		public SettingBuilder(SettingBuildContext<T> context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.context = context;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007FEC File Offset: 0x000061EC
		public Setting<T> SetValue(Func<Result<TParent>, Result<T>> setFunction)
		{
			this.context.SetFunction = delegate(Result x)
			{
				IEnumerable<Result<T>> result;
				try
				{
					result = new Result<T>[]
					{
						setFunction((Result<TParent>)x)
					};
				}
				catch (Exception exception)
				{
					result = new Result<T>[]
					{
						new Result<T>(exception)
					};
				}
				return result;
			};
			return (Setting<T>)this.context.Construct();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008080 File Offset: 0x00006280
		public Setting<T> SetMultipleValues(Func<Result<TParent>, IEnumerable<Result<T>>> setFunction)
		{
			this.context.SetFunction = delegate(Result x)
			{
				IEnumerable<Result<T>> result;
				try
				{
					result = setFunction((Result<TParent>)x);
				}
				catch (Exception exception)
				{
					result = new Result<T>[]
					{
						new Result<T>(exception)
					};
				}
				return result;
			};
			return (Setting<T>)this.context.Construct();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000080C1 File Offset: 0x000062C1
		void IFeatureBuilder.AddFeature(Feature feature)
		{
			((IFeatureBuilder)this.context).AddFeature(feature);
		}

		// Token: 0x04000141 RID: 321
		private SettingBuildContext<T> context;
	}
}
