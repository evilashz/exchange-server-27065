using System;
using System.Net;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000113 RID: 275
	internal class ClientAccessRulesEvaluationContext : RulesEvaluationContext
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x0001E1D4 File Offset: 0x0001C3D4
		public ClientAccessRulesEvaluationContext(RuleCollection rules, string username, IPEndPoint remoteEndpoint, ClientAccessProtocol protocol, ClientAccessAuthenticationMethod authenticationType, IReadOnlyPropertyBag userPropertyBag, ObjectSchema userSchema, Action<ClientAccessRulesEvaluationContext> denyAccessDelegate, Action<Rule, ClientAccessRulesAction> whatIfActionDelegate, long traceId) : base(rules)
		{
			this.AuthenticationType = authenticationType;
			this.UserName = username;
			this.RemoteEndpoint = remoteEndpoint;
			this.Protocol = protocol;
			this.User = userPropertyBag;
			this.UserSchema = userSchema;
			this.DenyAccessDelegate = denyAccessDelegate;
			this.WhatIfActionDelegate = whatIfActionDelegate;
			this.WhatIf = (whatIfActionDelegate != null);
			base.Tracer = new ClientAccessRulesTracer(traceId);
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0001E241 File Offset: 0x0001C441
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x0001E249 File Offset: 0x0001C449
		public string UserName { get; private set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0001E252 File Offset: 0x0001C452
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0001E25A File Offset: 0x0001C45A
		public IPEndPoint RemoteEndpoint { get; private set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001E263 File Offset: 0x0001C463
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x0001E26B File Offset: 0x0001C46B
		public ClientAccessAuthenticationMethod AuthenticationMethod { get; private set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001E274 File Offset: 0x0001C474
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x0001E27C File Offset: 0x0001C47C
		public ClientAccessProtocol Protocol { get; private set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0001E285 File Offset: 0x0001C485
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x0001E28D File Offset: 0x0001C48D
		public ClientAccessAuthenticationMethod AuthenticationType { get; private set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0001E296 File Offset: 0x0001C496
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x0001E29E File Offset: 0x0001C49E
		public IReadOnlyPropertyBag User { get; private set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0001E2A7 File Offset: 0x0001C4A7
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x0001E2AF File Offset: 0x0001C4AF
		public ObjectSchema UserSchema { get; private set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001E2B8 File Offset: 0x0001C4B8
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0001E2C0 File Offset: 0x0001C4C0
		public Action<ClientAccessRulesEvaluationContext> DenyAccessDelegate { get; private set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0001E2C9 File Offset: 0x0001C4C9
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0001E2D1 File Offset: 0x0001C4D1
		internal bool WhatIf { get; private set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0001E2DA File Offset: 0x0001C4DA
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x0001E2E2 File Offset: 0x0001C4E2
		internal Action<Rule, ClientAccessRulesAction> WhatIfActionDelegate { get; private set; }
	}
}
