using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009C6 RID: 2502
	internal class OptionsRbacEngine
	{
		// Token: 0x060046DA RID: 18138 RVA: 0x000FC205 File Offset: 0x000FA405
		public OptionsRbacEngine(CallContext callContext, bool forceNewRunspace = false)
		{
			this.runspaceConfig = ExchangeRunspaceConfigurationCache.Singleton.Get(callContext.EffectiveCaller, null, forceNewRunspace);
			this.rbacQueryCache = new Dictionary<string, bool>();
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x000FC230 File Offset: 0x000FA430
		static OptionsRbacEngine()
		{
			RbacQuery.RegisterQueryProcessor("ClosedCampus", new ClosedCampusQueryProcessor());
			RbacQuery.RegisterQueryProcessor("PopImapDisabled", new PopImapDisabledQueryProcessor());
			RbacQuery.RegisterQueryProcessor("BusinessLiveId", new LiveIdInstanceQueryProcessor(LiveIdInstanceType.Business));
			RbacQuery.RegisterQueryProcessor("ConsumerLiveId", new LiveIdInstanceQueryProcessor(LiveIdInstanceType.Consumer));
			RbacQuery.RegisterQueryProcessor("True", new TrueQueryProcessor());
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x000FC294 File Offset: 0x000FA494
		public bool EvaluateExpression(string rbacQueryExpression)
		{
			string[] source = rbacQueryExpression.Split(new char[]
			{
				','
			});
			return source.Any((string queryExpressionPart) => this.EvaluateExpressionPart(queryExpressionPart));
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x000FC2D0 File Offset: 0x000FA4D0
		private bool EvaluateExpressionPart(string rbacQueryExpressionPart)
		{
			string[] source = rbacQueryExpressionPart.Split(new char[]
			{
				'+'
			});
			return source.All((string role) => this.IsInSingleRole(role));
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x000FC304 File Offset: 0x000FA504
		private bool IsInSingleRole(string role)
		{
			if (this.rbacQueryCache.ContainsKey(role))
			{
				return this.rbacQueryCache[role];
			}
			bool flag = new RbacQuery(role).IsInRole(this.runspaceConfig);
			this.rbacQueryCache[role] = flag;
			return flag;
		}

		// Token: 0x040028B5 RID: 10421
		private Dictionary<string, bool> rbacQueryCache;

		// Token: 0x040028B6 RID: 10422
		private ExchangeRunspaceConfiguration runspaceConfig;
	}
}
