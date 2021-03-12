using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001E2 RID: 482
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class OwaEventVerbAttribute : Attribute
	{
		// Token: 0x06001105 RID: 4357 RVA: 0x00040FDB File Offset: 0x0003F1DB
		public OwaEventVerbAttribute(OwaEventVerb verb)
		{
			this.verb = verb;
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x00040FF1 File Offset: 0x0003F1F1
		public OwaEventVerb Verb
		{
			get
			{
				return this.verb;
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00040FF9 File Offset: 0x0003F1F9
		public static OwaEventVerb Parse(string verb)
		{
			if (verb == null)
			{
				throw new ArgumentNullException("verb");
			}
			if (string.Equals(verb, "POST", StringComparison.OrdinalIgnoreCase))
			{
				return OwaEventVerb.Post;
			}
			if (string.Equals(verb, "GET", StringComparison.OrdinalIgnoreCase))
			{
				return OwaEventVerb.Get;
			}
			return OwaEventVerb.Unsupported;
		}

		// Token: 0x04000A0C RID: 2572
		private OwaEventVerb verb = OwaEventVerb.Post;
	}
}
