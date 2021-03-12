using System;
using System.ServiceModel.Configuration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000204 RID: 516
	public class OWAWebHttpBehaviorExtensionElement : BehaviorExtensionElement
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x000485BA File Offset: 0x000467BA
		public override Type BehaviorType
		{
			get
			{
				return typeof(OWAWebHttpBehavior);
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x000485C6 File Offset: 0x000467C6
		protected override object CreateBehavior()
		{
			return new OWAWebHttpBehavior();
		}
	}
}
